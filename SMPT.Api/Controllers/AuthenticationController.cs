using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;
using SMPT.Entities.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace SMPT.Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IPasswordHasher<User> _pwHasher;

        public AuthenticationController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IConfiguration config,
            HttpClient http,
            IPasswordHasher<User> pwHasher) : base(unitOfWork, mapper)
        {
            _logger = loggerFactory.CreateLogger("Auth");
            _config = config;
            _http = http;
            _pwHasher = pwHasher;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ApiResponse>> Auth([FromBody] SiiauCredentials credentials)
        {
            try
            {
                if (credentials == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = [new KeyValuePair<string, string>("error", "Credenciales incorrectas!")];
                    return BadRequest(_response);
                }

                if (!string.IsNullOrWhiteSpace(credentials.Pass))
                {
                    if (_http.BaseAddress == null)
                    {
                        _http.BaseAddress = new Uri(_config.GetValue<string>("SiiauAuthServer")!);
                    }

                    _http.DefaultRequestHeaders.Accept.Clear();
                    _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var resp = await _http.PostAsJsonAsync("siiau-validate", credentials);

                    var siiauUser = await resp.Content.ReadFromJsonAsync<SiiauUser>();

                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        if (siiauUser != null && siiauUser.Respuesta == null)
                        {
                            siiauUser.Codigo = credentials.Codigo;
                            siiauUser.Password = credentials.Pass;

                            User userDb = CreateOrFindUser(siiauUser);
                            if (userDb == null)
                            {
                                _response.StatusCode = HttpStatusCode.NotFound;
                                _response.ErrorMessage = [new KeyValuePair<string, string>("error", "Usuario no registrado en el sistema")];
                                return NotFound(_response);
                            }

                            var token = CreateJWT(userDb);

                            _response.StatusCode = HttpStatusCode.OK;
                            _response.Data = new JwtSecurityTokenHandler().WriteToken(token);
                            return Ok(_response);
                        }

                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessage = [new KeyValuePair<string, string>("error", "Credenciales incorrectas!")];
                        return BadRequest(_response);
                    }

                    _response.StatusCode = HttpStatusCode.ServiceUnavailable;
                    _response.ErrorMessage = [new KeyValuePair<string, string>("error", "El servicio de autenticación del CUValles no está disponible!")];
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessage = [new KeyValuePair<string, string>("error", "Credenciales incorrectas!")];
                return BadRequest(_response);
            }
            catch (Exception)
            {
                HandleServerError();
            }
            return _response;
        }

        private static User CreateOrFindUser(SiiauUser SiiauUser)
        {
            User user = DB().FirstOrDefault(x => x.Code == SiiauUser.Codigo && x.Password == SiiauUser.Password.ToString());

            return user;
        }

        private JwtSecurityToken CreateJWT(User userDb)
        {
            var jwt = _config.GetSection("JWT").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt!.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("code", userDb.Code.ToString()!),
                new Claim("name", userDb.Name!),
                new Claim("role", userDb.Role.Name!),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: signIn);
            return token;
        }

        private static List<User> DB()
        {
            var roleId = Guid.NewGuid();
            var list = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Code = 222977415,
                    Name = "Raidel",
                    Password = "raidel1988",
                    Email = "raidel@gmail.com",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsActive = true,
                    RoleId = roleId,
                    Role = new() { Id = roleId, Name = "Estudiante", Description = "" }
                }
            };
            return list;
        }
    }
}

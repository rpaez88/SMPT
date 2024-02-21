using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities;
using SMPT.Entities.DbSet;
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
        public async Task<CustomResponse<string>> Post([FromBody] SiiauCredentialsDto credentials)
        {
            var respApi = new CustomResponse<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Message = "Provided credentials error",
                Value = null
            };

            if (credentials != null)
            {
                if (!string.IsNullOrWhiteSpace(credentials.pass))
                {
                    if (_http.BaseAddress == null)
                    {
                        _http.BaseAddress = new Uri(_config.GetValue<string>("SiiauAuthServer")!);
                    }
                    _http.DefaultRequestHeaders.Accept.Clear();
                    _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await _http.PostAsJsonAsync("siiau-validate", credentials);
                    var siiauUser = await resp.Content.ReadFromJsonAsync<SiiauUserDto>();
                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        if (siiauUser != null && siiauUser.Respuesta == null)
                        {
                            siiauUser.Codigo = credentials.codigo;
                            siiauUser.Password = credentials.pass;

                            User userDb = CreateOrFindUser(siiauUser);
                            if (userDb == null)
                            {
                                respApi.StatusCode = (int)HttpStatusCode.NotFound;
                                respApi.Message = "Usuario no registrado en el sistema";
                            }
                            else
                            {
                                var token = CreateJWT(userDb);

                                respApi.StatusCode = (int)HttpStatusCode.OK;
                                respApi.Message = "Inicio de sesión exitoso";
                                respApi.Value = new JwtSecurityTokenHandler().WriteToken(token);
                            }
                        }
                        else
                        {
                            respApi.StatusCode = (int)HttpStatusCode.NotFound;
                            respApi.Message = "Credenciales incorrectas";
                        }
                    }
                    else
                    {
                        respApi.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                        respApi.Message = "El servidor de autenticación no está disponible!";
                    }
                }
            }

            return respApi;
        }

        private static User CreateOrFindUser(SiiauUserDto SiiauUser)
        {
            User user = DB().FirstOrDefault(x => x.Code == ((int)SiiauUser.Codigo) && x.Password == SiiauUser.Password.ToString());

            return user;
        }

        private JwtSecurityToken CreateJWT(User UserFromDataBase)
        {
            var jwt = _config.GetSection("JWT").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt!.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("code", UserFromDataBase.Code.ToString()!),
                new Claim("name", UserFromDataBase.Name!),
                new Claim("role", UserFromDataBase.Role.Name!),
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMPT.Shared.DTO;
using SMPT.Server.Controllers;
using SMPT.Server.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SMPT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly ILogger<LoginController> _logger;
        private readonly IPasswordHasher<User> _pwHasher;

        public LoginController(ILogger<LoginController> logger, IConfiguration config, HttpClient http, IPasswordHasher<User> pwHasher)
        {
            _logger = logger;
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
                            var token = CreateJWT(userDb);

                            respApi.StatusCode = (int)HttpStatusCode.OK;
                            respApi.Message = "Inicio de sesión exitoso";
                            respApi.Value = new JwtSecurityTokenHandler().WriteToken(token);
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
            User user = Models.User.DB().FirstOrDefault(x => x.Code == ((int)SiiauUser.Codigo) && x.Password == SiiauUser.Password.ToString());

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
                new Claim("codigo", UserFromDataBase.Code.ToString()!),
                new Claim("nombre", UserFromDataBase.Name!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: signIn);
            return token;
        }
    }
}

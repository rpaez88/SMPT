using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMPT.Shared.DTO;
using SMPT.Server.Controllers;
using SMPT.Server.Domain;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SMPT.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IConfiguration config, HttpClient http)
        {
            _logger = logger;
            //var builder = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            //Configuration = builder.Build();
            _config = config;
            _http = http;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<CustomResponse<string>> Post([FromBody] SiiauCredentialsDTO credentials)
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
                        _http.BaseAddress = new Uri(_config.GetValue<string>("SiiauAuthServer") ?? "http://148.202.89.11/d_alum/api/");
                    }
                    _http.DefaultRequestHeaders.Accept.Clear();
                    _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage resp = await _http.PostAsJsonAsync("siiau-validate", credentials);
                    var content = await resp.Content.ReadFromJsonAsync<JsonObject>();
                    if (resp.StatusCode == HttpStatusCode.OK)
                    {

                        if (content != null && !content.ContainsKey("respuesta"))
                        {
                            content["code"] = credentials.codigo;
                            content["password"] = credentials.pass;

                            User UserFromDataBase = CreateOrFindUser(content);
                            var token = CreateJWT(UserFromDataBase);

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

        private static User CreateOrFindUser(JsonObject SiiauUser)
        {
            User user = Domain.User.DB().FirstOrDefault(x => x.Code == ((int)SiiauUser["code"]) && x.Password == SiiauUser["password"].ToString());

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
                new Claim("nombre", UserFromDataBase.Name!),
                new Claim("tipoUsuario", UserFromDataBase.UserType!),
                new Claim("status", UserFromDataBase!.Status!),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: signIn);
            return token;
        }
    }
}

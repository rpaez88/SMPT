using AutoMapper;
using iText.StyledXmlParser.Jsoup.Parser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;
using SMPT.Entities.Dtos;
using SMPT.Entities.Dtos.User;
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
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthenticationController(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IConfiguration config,
            HttpClient http,
            IPasswordHasher<User> passwordHasher) : base(unitOfWork, mapper)
        {
            _logger = loggerFactory.CreateLogger("Auth");
            _config = config;
            _http = http;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ApiResponse>> Auth([FromBody] SiiauCredentials credentials)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessage = [new KeyValuePair<string, string>("error", "Credenciales incorrectas!")];
                    return BadRequest(_response);
                }

                //Buscar en BD
                var userDb = await _unitOfWork.Users.Find(x => x.Code == credentials.Codigo);

                if (userDb == null)
                {
                    //Buscar en SIIAU
                    if (_http.BaseAddress == null)
                    {
                        _http.BaseAddress = new Uri(_config.GetValue<string>("SiiauAuthServer")!);
                    }

                    _http.DefaultRequestHeaders.Accept.Clear();
                    _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var resp = await _http.PostAsJsonAsync("siiau-validate", credentials);
                    var siiauUser = await resp.Content.ReadFromJsonAsync<SiiauUser>();

                    //Si esta disponible el servicio del CUValles
                    if (resp.StatusCode == HttpStatusCode.OK)
                    {
                        //Si esiste el usuario en SIIAU con las credenciales proporsionadas
                        if (siiauUser != null && siiauUser.Respuesta == null)
                        {
                            siiauUser.Codigo = credentials.Codigo;
                            siiauUser.Password = credentials.Pass;

                            //Si es estudiante
                            if (siiauUser.TipoUsuario.Equals("alumno", StringComparison.CurrentCultureIgnoreCase))
                            {
                                var studentCareers = new List<Career>();
                                var studentCycles = new List<Cycle>();

                                //Crear carrera y ciclo si no existen
                                foreach (var item in siiauUser.Carrera)
                                {
                                    var cycleDb = await _unitOfWork.Cycles.Find(x => x.Name == item.CicloIngreso, false);
                                    var careerDb = await _unitOfWork.Careers.Find(x => x.Name == item.Descripcion, false);
                                    
                                    if (careerDb == null)
                                    {
                                        if (cycleDb == null)
                                        {
                                            cycleDb = new() { Name = item.CicloIngreso };
                                            studentCycles.Add(cycleDb);
                                            await _unitOfWork.Cycles.Add(cycleDb);
                                        }
                                        
                                        var career = new Career
                                        {
                                            Name = item.Descripcion,
                                            Cycles = new List<Cycle> { cycleDb }
                                        };

                                        studentCareers.Add(career);
                                        await _unitOfWork.Careers.Add(career);
                                    }
                                    else
                                    {
                                        if (cycleDb == null)
                                        {
                                            cycleDb = new Cycle
                                            {
                                                Name = item.Descripcion,
                                                Careers = new List<Career> { careerDb }
                                            };
                                            await _unitOfWork.Cycles.Add(cycleDb);
                                        }

                                        studentCareers.Add(careerDb);
                                    }
                                }

                                //Crear Student(usuario rol = "Estudiante") asignandole los Careers y Cycles
                                var studentRole = await _unitOfWork.Roles.Find(x => x.Alias == "student");
                                var graduateState = await _unitOfWork.StudentStates.Find(x => x.Name == "Egresado");

                                var student = new Student
                                {
                                    Code = (long)siiauUser.Codigo,
                                    Cycles = studentCycles,
                                    IsActive = siiauUser.Estatus.Equals("Activo"),
                                    Name = siiauUser.Nombre,
                                    RoleId = studentRole.Id,
                                    Role = studentRole,
                                    StateId = graduateState.Id,
                                    State = graduateState,
                                    Password = siiauUser.Password,
                                    Careers = studentCareers
                                };

                                student.Password = _passwordHasher.HashPassword(student, student.Password);

                                await _unitOfWork.Students.Add(student);

                                if (!await _unitOfWork.CompleteAsync())
                                {
                                    HandleServerError();
                                    return BadRequest(_response);
                                }

                                return await CreateToken(student);
                            }

                            //Si no es estudiante, debe registrarlo el administrador
                            _response.StatusCode = HttpStatusCode.NotFound;
                            _response.ErrorMessage = [
                                new KeyValuePair<string, string>("error", "Contacte al administrador para crear una cuenta de usuario!")
                            ];
                            return BadRequest(_response);
                        }

                        //Si no esiste el usuario en SIIAU
                        _response.StatusCode = HttpStatusCode.NotFound;
                        _response.ErrorMessage = [new KeyValuePair<string, string>("error", "Credenciales incorrectas!")];
                        return BadRequest(_response);
                    }

                    //Si no esta disponible el servicio del CUValles
                    _response.StatusCode = HttpStatusCode.ServiceUnavailable;
                    _response.ErrorMessage = [new KeyValuePair<string, string>("error", "El servicio de autenticación del CUValles no está disponible!")];
                    return BadRequest(_response);
                }
                else
                {
                    //Comparar password si no esta vacio
                    if (!string.IsNullOrEmpty(userDb.Password))
                    {
                        var result = _passwordHasher.VerifyHashedPassword(userDb, userDb.Password, credentials.Pass);

                        if (result == PasswordVerificationResult.Failed)
                        {
                            _response.StatusCode = HttpStatusCode.BadRequest;
                            _response.IsSuccess = false;
                            _response.ErrorMessage = [new KeyValuePair<string, string>("error", "Credenciales incorrectas!")];
                            return BadRequest(_response);
                        }

                        return await CreateToken(userDb);
                    }

                    //Actualizar password si esta vacio
                    userDb.Password = _passwordHasher.HashPassword(userDb, credentials.Pass);
                    await _unitOfWork.Users.Update(userDb);

                    if (!await _unitOfWork.CompleteAsync())
                    {
                        HandleServerError();
                        return BadRequest(_response);
                    }

                    return await CreateToken(userDb);
                }
            }
            catch (Exception)
            {
                HandleServerError();
            }
            return _response;
        }

        [NonAction]
        private async Task<ActionResult<ApiResponse>> CreateToken(User user)
        {
            Area? area = null;
            Career? career = null;
            if (user.Role == null)
            {
                user.Role = await _unitOfWork.Roles.GetById(user.RoleId);
                if (user.Role != null)
                {
                    switch (user.Role.Alias)
                    {
                        case "area-manager":
                            area = await _unitOfWork.Areas.Find(x => x.ManagerId == user.Id);
                            break;

                        case "coordinator":
                            career = await _unitOfWork.Careers.Find(x => x.CoordinatorId == user.Id);
                            break;
                    }
                    area = await _unitOfWork.Areas.Find(x => x.ManagerId == user.Id);
                }
            }
            var token = CreateJWT(user, area, career);
            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(_response);
        }

        private JwtSecurityToken CreateJWT(User userDb, Area? area = null, Career? career = null)
        {
            var jwt = _config.GetSection("JWT").Get<Jwt>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt!.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("code", userDb.Code.ToString()!),
                new Claim("name", userDb.Name!),
                new Claim("roleName", userDb.Role.Name!),
                new Claim("roleAlias", userDb.Role.Alias!),
            };

            if (area != null)
                claims.Append(new Claim("area", area.Name!));

            if (career != null)
                claims.Append(new Claim("career", career.Name!));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.Now.AddMinutes(10), signingCredentials: signIn);
            return token;
        }
    }
}

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
                                //Crear usuario
                                var studentRole = await _unitOfWork.Roles.Find(x => x.Alias == "student");
                                var studentUser = new User
                                {
                                    Code = (long)siiauUser.Codigo,
                                    Name = siiauUser.Nombre,
                                    RoleId = studentRole.Id,
                                    Role = studentRole,
                                    Password = siiauUser.Password,
                                    IsActive = siiauUser.Estatus.Equals("Activo"),
                                };
                                studentUser.Password = _passwordHasher.HashPassword(studentUser, studentUser.Password);
                                await _unitOfWork.Users.Add(studentUser);

                                var studentCareer = new Career();
                                var studentCycle = new Cycle();

                                //Crear carrera y ciclo si no existen
                                foreach (var item in siiauUser.Carrera)
                                {
                                    studentCycle = await _unitOfWork.Cycles.Find(x => x.Name == item.CicloIngreso);
                                    studentCareer = await _unitOfWork.Careers.Find(x => x.Name == item.Descripcion);

                                    if (studentCareer == null)
                                    {
                                        if (studentCycle == null)
                                        {
                                            studentCycle = new() { Name = item.CicloIngreso };
                                            await _unitOfWork.Cycles.Add(studentCycle);
                                        }

                                        studentCareer = new Career
                                        {
                                            Name = item.Descripcion,
                                            Cycles = new List<Cycle> { studentCycle }
                                        };
                                        await _unitOfWork.Careers.Add(studentCareer);
                                    }
                                    else
                                    {
                                        if (studentCycle == null)
                                        {
                                            studentCycle = new Cycle
                                            {
                                                Name = item.Descripcion,
                                                Careers = new List<Career> { studentCareer }
                                            };
                                            await _unitOfWork.Cycles.Add(studentCycle);
                                        }
                                        else
                                        {
                                            var cycleCareersIds = await _unitOfWork.Cycles.GetCareerIds(studentCycle.Id);
                                            if (cycleCareersIds == null || !cycleCareersIds.Any(x => x == studentCareer.Id))
                                            {
                                                studentCycle.Careers.Add(studentCareer);
                                                await _unitOfWork.Cycles.Update(studentCycle);
                                            }
                                        }
                                    }
                                }

                                //Crear Student asignandole User, Careers y Cycles
                                var graduateState = await _unitOfWork.StudentStates.Find(x => x.Name == "Egresado", false);
                                var student = new Student
                                {
                                    UserId = studentUser.Id,
                                    CycleId = studentCycle.Id,
                                    Name = siiauUser.Nombre,
                                    StateId = graduateState.Id,
                                    CareerId = studentCareer.Id,
                                };
                                await _unitOfWork.Students.Add(student);

                                if (!await _unitOfWork.CompleteAsync())
                                {
                                    HandleServerError();
                                    return BadRequest(_response);
                                }

                                return await CreateToken(studentUser);
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
            user.Role ??= await _unitOfWork.Roles.GetById(user.RoleId);

            switch (user.Role?.Alias)
            {
                case "area-manager":
                    area = await _unitOfWork.Areas.Find(x => x.ManagerId == user.Id);
                    break;

                case "coordinator":
                    career = await _unitOfWork.Careers.Find(x => x.CoordinatorId == user.Id);
                    break;

                case "student":
                {
                    var student = await _unitOfWork.Students.Find(x => x.UserId == user.Id);
                    if (student?.CareerId != null)
                    {
                        career = await _unitOfWork.Careers.GetById(student.CareerId ?? user.Id);
                    }
                    break;
                }
            }
            area = await _unitOfWork.Areas.Find(x => x.ManagerId == user.Id);

            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = CreateJWT(user, area, career);
            return Ok(_response);
        }

        private string CreateJWT(User userDb, Area? area = null, Career? career = null)
        {
            var jwt = _config.GetSection("JWT").Get<Jwt>();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt!.Subject),
                new Claim("userId", userDb.Id.ToString()!),
                new Claim("userCode", userDb.Code.ToString()!),
                new Claim("userName", userDb.Name!),
                new Claim("roleName", userDb.Role.Name),
                new Claim("roleAlias", userDb.Role.Alias),
            };

            if (area != null)
            {
                claims.Add(new Claim("areaId", area.Id.ToString()));
                claims.Add(new Claim("areaName", area.Name));
            }

            if (career != null)
            {
                claims.Add(new Claim("careerId", career.Id.ToString()));
                claims.Add(new Claim("careerName", career.Name));
            }

            //var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.Key));
            //var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var jwtSecurity = new JwtSecurityToken(
            //    issuer: jwt.Issuer,
            //    audience: jwt.Audience,
            //    claims: claims,
            //    expires: DateTime.Now.AddMinutes(10),
            //    signingCredentials: signIn);

            //return new JwtSecurityTokenHandler().WriteToken(jwtSecurity);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwt.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

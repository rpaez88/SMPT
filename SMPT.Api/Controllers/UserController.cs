using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;
using SMPT.Entities.Dtos;
using SMPT.Entities.Dtos.User;
using System.Net;

namespace SMPT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {

        private readonly IPasswordHasher<User> _passwordHasher;

        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IPasswordHasher<User> passwordHasher) : base(unitOfWork, mapper)
        {
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetUsers()
        {
            try
            {
                var users = await _unitOfWork.Users.GetAll();

                _response.StatusCode = HttpStatusCode.OK;
                _response.Data = _mapper.Map<IEnumerable<UserDto>>(users);

                return Ok(_response);
            }
            catch (Exception)
            {
                HandleServerError();
            }
            return _response;
        }

        [HttpGet]
        [Route("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetUser(Guid userId)
        {
            try
            {
                var user = await _unitOfWork.Users.GetById(userId);

                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Data = _mapper.Map<UserDto>(user);
                return Ok(_response);
            }
            catch (Exception)
            {
                HandleServerError();
            }
            return _response;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateUser([FromBody] CreateUserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (userDto == null)
                {
                    return BadRequest(userDto);
                }

                var existUser = await _unitOfWork.Users.Find(x =>
                    (!string.IsNullOrEmpty(userDto.Name) && x.Name.ToLower() == userDto.Name.ToLower()) ||
                    (!string.IsNullOrEmpty(userDto.Email) && (!string.IsNullOrEmpty(x.Email) && x.Email.ToLower() == userDto.Email.ToLower())) ||
                    x.Code == userDto.Code,
                    tracked: false);

                if (existUser != null)
                {
                    AddModelStateErrors(existUser, _mapper.Map<User>(userDto), ModelState);
                    return BadRequest(ModelState);
                }

                var user = _mapper.Map<User>(userDto);
                var heshedPassword = _passwordHasher.HashPassword(user, userDto.Password);
                user.Password = heshedPassword;

                await _unitOfWork.Users.Add(user);

                if (!await _unitOfWork.CompleteAsync())
                {
                    HandleServerError();
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.Created;
                _response.Data = _mapper.Map<UserDto>(user);
                return CreatedAtRoute("GetUser", new { userId = user.Id }, _response);
            }
            catch (Exception)
            {
                HandleServerError();
            }
            return _response;
        }

        [HttpPut("{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> UpdateUser(Guid userId, [FromBody] UpdateUserDto userDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (userDto == null || userId != userDto.Id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                if (await _unitOfWork.Users.Find(x => x.Id == userId, false) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                var otherUser = await _unitOfWork.Users.Find(x =>
                    ((!string.IsNullOrEmpty(userDto.Name) && x.Name.ToLower() == userDto.Name.ToLower()) ||
                    (!string.IsNullOrEmpty(userDto.Email) && (!string.IsNullOrEmpty(x.Email) && x.Email.ToLower() == userDto.Email.ToLower())) ||
                    x.Code == userDto.Code) && x.Id != userId,
                    tracked: false);

                if (otherUser != null)
                {
                    AddModelStateErrors(otherUser, _mapper.Map<User>(userDto), ModelState);
                    return BadRequest(ModelState);
                }

                var userFromBody = _mapper.Map<User>(userDto);

                await _unitOfWork.Users.Update(userFromBody);

                if (!await _unitOfWork.CompleteAsync())
                {
                    HandleServerError();
                    return BadRequest(_response);
                }

                return NoContent();
            }
            catch (Exception)
            {
                HandleServerError();
            }
            return _response;
        }

        //
        private static void AddModelStateErrors(User userDb, User userDto, ModelStateDictionary modelState)
        {
            if (userDto.Code == userDb.Code)
            {
                modelState.AddModelError("CodeExist", "Ya existe un usuario con este código!");
            }
            if (!string.IsNullOrEmpty(userDto.Name) && userDb.Name.ToLower() == userDto.Name.ToLower())
            {
                modelState.AddModelError("NameExist", "Ya existe un usuario con este nombre!");
            }
            if (!string.IsNullOrEmpty(userDto.Email) && (!string.IsNullOrEmpty(userDb.Email) && userDb.Email.ToLower() == userDto.Email.ToLower()))
            {
                modelState.AddModelError("EmailExist", "Ya existe un usuario con este correo!");
            }
        }
    }
}

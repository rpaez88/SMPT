using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMPT.DataServices.Repository;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;
using SMPT.Entities.Dtos;
using System.Net;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace SMPT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        [HttpGet]
        [Route("")]
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
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage = [new KeyValuePair<string, string>("error", "A ocurrido un error en el servidor, intentelo más tarde!")];
            }
            return _response;
        }

        [HttpGet]
        [Route("{userId:guid}")]
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
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage = [new KeyValuePair<string, string>("error", "A ocurrido un error en el servidor, intentelo más tarde!")];
            }
            return _response;
        }
    }
}

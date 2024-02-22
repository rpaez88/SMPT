using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.Dtos;
using System.Net;

namespace SMPT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected ApiResponse _response;

        public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }

        [NonAction]
        public void HandleServerError()
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.IsSuccess = false;
            _response.ErrorMessage = [new KeyValuePair<string, string>("error", "A ocurrido un error en el servidor, intentelo más tarde!")];
        }
    }
}

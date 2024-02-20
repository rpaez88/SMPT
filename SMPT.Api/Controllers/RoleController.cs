using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SMPT.DataServices.Repository.Interface;
using SMPT.Entities.DbSet;
using SMTP.Entities.Dtos;
using System.Net;

namespace SMPT.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        public RoleController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var roles = await _unitOfWork.GetRepository<Role>().GetAll();

            _response.StatusCode = HttpStatusCode.OK;
            _response.Data = _mapper.Map<IEnumerable<RoleDto>>(roles);

            return Ok(_response);
        }

        [HttpGet]
        [Route("{roleId:guid}")]
        public async Task<IActionResult> GetRole(Guid roleId)
        {
            var role = await _unitOfWork.GetRepository<Role>().GetById(roleId);

            if (role == null)
                return NotFound("Rol no encontrado");

            var result = _mapper.Map<RoleDto>(role);
            return Ok(result);
        }
    }
}

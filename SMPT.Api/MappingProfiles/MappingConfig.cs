using AutoMapper;
using SMPT.Entities.DbSet;
using SMTP.Entities.Dtos;

namespace SMPT.Api.MappingProfiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}

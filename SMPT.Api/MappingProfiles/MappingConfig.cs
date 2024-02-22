using AutoMapper;
using SMPT.Entities.DbSet;
using SMPT.Entities.Dtos.RoleDtos;
using SMPT.Entities.Dtos.User;

namespace SMPT.Api.MappingProfiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
        }
    }
}

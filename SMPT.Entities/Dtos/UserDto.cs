using SMPT.Entities.DbSet;

namespace SMPT.Entities.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        //var userDto = (UserDto)user; realizando un casteo utiliza este operador explicito, para mapear
        public static explicit operator User(UserDto user)
        {
            return new User
            {
                Id = user.Id,
                Code = user.Code,
                Name = user.Name,
                RoleId = user.Role.Id,
                Role = user.Role,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };
        }
    }
}

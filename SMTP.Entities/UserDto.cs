using SMPT.Entities.DbSet;
using SMTP.Entities.DbSet;

namespace SMTP.Entities
{
    public class UserDto : BaseEntity
    {
        public long Code { get; set; }

        public string? Email { get; set; }

        public required Role Role { get; set; }

        public bool IsActive { get; set; }

        //var userDto = (UserDto)user; realizando un casteo utiliza este operador explicito, para mapear
        public static explicit operator UserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role,
                Email = user.Email,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate,
                UpdatedDate = user.UpdatedDate
            };
        }
    }
}

using System.ComponentModel.DataAnnotations;
using SMPT.Entities.Dtos;

namespace SMPT.Entities.DbSet
{
    public class Role: BaseEntity
    {
        [MaxLength(255)]
        public string? Description { get; set; }

        public static explicit operator RoleDto(Role role)
        {
            return new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                //Description = role.Description,
                //CreatedDate = role.CreatedDate,
                //UpdatedDate = role.UpdatedDate
            };
        }
    }
}

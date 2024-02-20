using SMPT.Entities.DbSet;

namespace SMPT.Entities.Dtos
{
    public class RoleDto
    {
        public Guid Id;
        public string Name;
        public string Description;
        public DateTime CreatedDate;
        public DateTime UpdatedDate;

        public static explicit operator Role(RoleDto role)
        {
            return new Role
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                CreatedDate = role.CreatedDate,
                UpdatedDate = role.UpdatedDate
            };
        }
    }
}

using SMPT.Entities.DbSet;

namespace SMPT.Entities.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public string Description { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public DateTime UpdatedDate { get; set; }

        public static explicit operator Role(RoleDto role)
        {
            return new Role
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

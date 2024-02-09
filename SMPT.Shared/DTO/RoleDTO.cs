using System.ComponentModel.DataAnnotations;

namespace SMPT.Shared.DTO
{
    public class RoleDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}

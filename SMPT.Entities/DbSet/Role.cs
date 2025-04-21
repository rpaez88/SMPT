using System.ComponentModel.DataAnnotations;

namespace SMPT.Entities.DbSet
{
    public class Role: BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Alias { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}

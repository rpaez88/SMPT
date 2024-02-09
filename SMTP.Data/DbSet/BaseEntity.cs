using System.ComponentModel.DataAnnotations;

namespace SMTP.Entities.DbSet
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = new Guid();
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}

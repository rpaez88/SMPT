using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SMTP.Entities.DbSet;

namespace SMPT.Entities.DbSet
{
    public class User: BaseEntity
    {
        [Required]
        public required long Code { get; set; }

        [AllowNull]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required Guid RoleId { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        public virtual required Role Role { get; set; }

        [Required]
        public required bool IsActive { get; set; }
    }
}

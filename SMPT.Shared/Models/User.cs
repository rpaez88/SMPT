using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace SMPT.Shared.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [AllowNull]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [AllowNull]
        [EmailAddress]
        public string? Email { get; set; }

        [AllowNull]
        [MaxLength(30)]
        public string? Username { get; set; }

        [AllowNull]
        public string? Password { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public required Role Role { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}

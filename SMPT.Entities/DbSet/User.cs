using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class User: BaseEntity
    {
        [Required]
        public required long Code { get; set; }

        [AllowNull]
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(8)]
        [DataType(DataType.Password)]
        [RegularExpression(
            pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*.()_+]).{8,}$",
            ErrorMessage = "La contraseña no cumple con los requisitos de seguridad")]
        public string Password { get; set; }

        [Required]
        public required Guid RoleId { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        [JsonIgnore]
        public virtual required Role Role { get; set; }

        [Required]
        public required bool IsActive { get; set; }
    }
}

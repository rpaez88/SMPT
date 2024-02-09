using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SMPT.Server.Models
{
    public class Evidence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [AllowNull]
        public string Ovservations { get; set; }

        [AllowNull]
        public string FileUrl { get; set; }

        [Required]
        public required Student Student { get; set; }
    }
}

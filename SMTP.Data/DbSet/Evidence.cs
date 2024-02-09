using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SMPT.Entities.DbSet
{
    public class Evidence
    {
        [AllowNull]
        public string? Observaciones { get; set; }

        [AllowNull]
        public string? UrlArchivo { get; set; }

        [Required]
        public required Guid StudentId { get; set; }

        [Required]
        [ForeignKey("StudentId")]
        public virtual required Student Student { get; set; }

        [Required]
        public required Guid StateId { get; set; }

        [Required]
        [ForeignKey("StateId")]
        public virtual required EvidenceState State { get; set; }

        [Required]
        public required Guid AreaId { get; set; }

        [Required]
        [ForeignKey("AreaId")]
        public required Area Area { get; set; }
    }
}

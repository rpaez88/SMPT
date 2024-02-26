using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class Evidence : BaseEntity
    {
        [AllowNull]
        public string Observaciones { get; set; }

        [AllowNull]
        public string UrlArchivo { get; set; }

        [Required]
        public required Guid StudentId { get; set; }

        [Required]
        [ForeignKey("StudentId")]
        [JsonIgnore]
        public virtual required Student Student { get; set; }

        [Required]
        public required Guid StateId { get; set; }

        [Required]
        [ForeignKey("StateId")]
        [JsonIgnore]
        public virtual required EvidenceState State { get; set; }

        [Required]
        public required Guid AreaId { get; set; }

        [Required]
        [ForeignKey("AreaId")]
        [JsonIgnore]
        public required Area Area { get; set; }
    }
}

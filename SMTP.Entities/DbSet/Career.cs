using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SMTP.Entities.DbSet;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class Career: BaseEntity
    {
        public Career()
        {
            Cycles = new HashSet<Cycle>();
        }

        [AllowNull]
        public string? Descripcion { get; set; }

        [AllowNull]
        public Guid? CoordinatorId { get; set; }

        [AllowNull]
        [ForeignKey("CoordinatorId")]
        [JsonIgnore]
        public virtual User? Coordinator { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Cycle>? Cycles { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Student>? Students { get; set; }
    }
}

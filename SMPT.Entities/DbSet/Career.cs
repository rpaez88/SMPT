using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class Career: BaseEntity
    {
        public Career()
        {
            Cycles = new HashSet<Cycle>();
            Students = new HashSet<Student>();
        }

        [AllowNull]
        public string Descripcion { get; set; }

        [AllowNull]
        public Guid? CoordinatorId { get; set; }

        [AllowNull]
        [ForeignKey("CoordinatorId")]
        [JsonIgnore]
        public virtual User Coordinator { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Cycle> Cycles { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }
    }
}

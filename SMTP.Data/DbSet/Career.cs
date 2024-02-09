using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SMTP.Entities.DbSet;

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
        public virtual User? Coordinator { get; set; }

        [AllowNull]
        public virtual ICollection<Cycle>? Cycles { get; set; }

        [AllowNull]
        public virtual ICollection<Student>? Students { get; set; }
    }
}

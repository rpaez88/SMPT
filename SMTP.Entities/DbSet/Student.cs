using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SMPT.Entities.DbSet
{
    public class Student: User
    {
        public Student()
        {
            Careers = new HashSet<Career>();
            Evidences = new HashSet<Evidence>();
        }

        [Required]
        public required Guid CycleId { get; set; }

        [Required]
        [ForeignKey("CycleId")]
        public required Cycle Cycle { get; set; }

        [Required]
        public required Guid StateId { get; set; }

        [Required]
        [ForeignKey("StateId")]
        public virtual required StudentState State { get; set; }

        [Required]
        public virtual required ICollection<Career> Careers { get; set; }

        [AllowNull]
        public virtual ICollection<Evidence> Evidences { get; set; }
    }
}

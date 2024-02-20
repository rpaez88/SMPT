using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class Student: User
    {
        public Student()
        {
            Careers = new HashSet<Career>();
            Evidences = new HashSet<Evidence>();
        }

        [AllowNull]
        public Guid CycleId { get; set; }

        [AllowNull]
        [ForeignKey("CycleId")]
        [JsonIgnore]
        public virtual Cycle Cycle { get; set; }

        [AllowNull]
        public Guid StateId { get; set; }

        [AllowNull]
        [ForeignKey("StateId")]
        [JsonIgnore]
        public virtual StudentState State { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Career> Careers { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Evidence> Evidences { get; set; }
    }
}

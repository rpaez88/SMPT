using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class Student: BaseEntity
    {
        public Student()
        {
            Evidences = new HashSet<Evidence>();
        }

        [AllowNull]
        public Guid? UserId { get; set; }

        [AllowNull]
        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual User User { get; set; }

        [AllowNull]
        public Guid? CycleId { get; set; }

        [AllowNull]
        [ForeignKey("CycleId")]
        [JsonIgnore]
        public virtual Cycle Cycle { get; set; }

        [AllowNull]
        public Guid? StateId { get; set; }

        [AllowNull]
        [ForeignKey("StateId")]
        [JsonIgnore]
        public virtual StudentState State { get; set; }

        [AllowNull]
        public Guid? CareerId { get; set; }

        [AllowNull]
        [ForeignKey("CareerId")]
        [JsonIgnore]
        public virtual Career Career { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Evidence> Evidences { get; set; }
    }
}

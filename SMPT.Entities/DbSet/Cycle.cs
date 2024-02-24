using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class Cycle : BaseEntity
    {
        public Cycle()
        {
            Careers = new HashSet<Career>();
            Students = new HashSet<Student>();
        }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Career> Careers { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }
    }
}

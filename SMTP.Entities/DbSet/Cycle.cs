using SMTP.Entities.DbSet;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class Cycle : BaseEntity
    {
        public Cycle()
        {
            Careers = new HashSet<Career>();
        }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Career>? Careers { get; set; }
    }
}

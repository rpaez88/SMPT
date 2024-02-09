using SMTP.Entities.DbSet;
using System.Diagnostics.CodeAnalysis;

namespace SMPT.Entities.DbSet
{
    public class Cycle : BaseEntity
    {
        public Cycle()
        {
            Careers = new HashSet<Career>();
        }

        [AllowNull]
        public virtual ICollection<Career>? Careers { get; set; }
    }
}

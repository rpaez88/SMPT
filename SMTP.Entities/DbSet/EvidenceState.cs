using System.Diagnostics.CodeAnalysis;
using SMTP.Entities.DbSet;

namespace SMPT.Entities.DbSet
{
    public class EvidenceState : BaseEntity
    {
        [AllowNull]
        public string Description { get; set; }
    }
}

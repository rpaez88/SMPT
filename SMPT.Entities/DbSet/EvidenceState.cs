using System.Diagnostics.CodeAnalysis;

namespace SMPT.Entities.DbSet
{
    public class EvidenceState : BaseEntity
    {
        [AllowNull]
        public string Description { get; set; }
    }
}

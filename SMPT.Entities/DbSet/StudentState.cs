using System.Diagnostics.CodeAnalysis;

namespace SMPT.Entities.DbSet
{
    public class StudentState: BaseEntity
    {
        [AllowNull]
        public string Description { get; set; }
    }
}

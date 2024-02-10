using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using SMTP.Entities.DbSet;

namespace SMPT.Entities.DbSet
{
    public class StudentState: BaseEntity
    {
        [AllowNull]
        public string Description { get; set; }
    }
}

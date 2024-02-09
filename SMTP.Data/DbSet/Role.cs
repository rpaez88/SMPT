using System.ComponentModel.DataAnnotations;
using SMTP.Entities.DbSet;

namespace SMPT.Entities.DbSet
{
    public class Role: BaseEntity
    {
        [MaxLength(255)]
        public string? Description { get; set; }
    }
}

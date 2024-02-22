using System.ComponentModel.DataAnnotations;

namespace SMPT.Entities.DbSet
{
    public class Role: BaseEntity
    {
        [MaxLength(255)]
        public string Description { get; set; }
    }
}

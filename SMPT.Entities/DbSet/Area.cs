using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace SMPT.Entities.DbSet
{
    public class Area : BaseEntity
    {
        public Area()
        {
            Evidences = new HashSet<Evidence>();    
        }

        [MaxLength(20)]
        public string Alias { get; set; }

        [AllowNull]
        public Guid? ManagerId { get; set; }

        [AllowNull]
        [ForeignKey("ManagerId")]
        [JsonIgnore]
        public virtual User Manager { get; set; }

        [AllowNull]
        [JsonIgnore]
        public virtual ICollection<Evidence> Evidences { get; set; }
    }
}

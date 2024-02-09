using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SMPT.Server.Models
{
    public class Career
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [AllowNull]
        public string Description { get; set; }

        [AllowNull]
        public User Coordinator { get; set; }

        [AllowNull]
        public IEnumerable<Cycle> Cycles { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SMPT.Server.Models
{
    public class Student : User
    {
        [Required]
        public required Cycle IncomeCycle { get; set; }

        [Required]
        public required StudentState State { get; set; }

        [Required]
        public required IEnumerable<Career> Careers { get; set; }

        [AllowNull]
        public IEnumerable<Evidence> Evidences { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SMPT.Server.Models
{
    public class Coordinator : User
    {
        [Required]
        public required Career Career { get; set; }
    }
}

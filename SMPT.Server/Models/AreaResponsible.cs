using System.ComponentModel.DataAnnotations;

namespace SMPT.Server.Models
{
    public class AreaResponsible : User
    {
        [Required]
        public required Area Area { get; set; }
    }
}

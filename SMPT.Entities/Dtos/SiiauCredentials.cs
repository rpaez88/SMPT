using System.ComponentModel.DataAnnotations;

namespace SMPT.Entities.Dtos
{
    public class SiiauCredentials
    {
        [Required]
        public required long Codigo { get; set; }

        [Required]
        public required string Pass { get; set; }
    }
}

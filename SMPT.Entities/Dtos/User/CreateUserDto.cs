using System.ComponentModel.DataAnnotations;

namespace SMPT.Entities.Dtos.User
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public long Code { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [RegularExpression(
            pattern: "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*.()_+]).{8,}$",
            ErrorMessage = "La contraseña no cumple con los requisitos de seguridad")]
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
    }
}

using SMPT.Shared.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace SMPT.Server.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public required int Code { get; set; }

        [Required]
        public required string Name { get; set; }

        [AllowNull]
        [EmailAddress]
        public string Email { get; set; }

        [AllowNull]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }

        [Required]
        public required int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public required Role Role { get; set; }

        [Required]
        public required bool IsActive { get; set; }

        public static List<User> DB()
        {
            var list = new List<User>()
            {
                new User
                {
                    Code = 222977415,
                    Name = "Raidel",
                    Password = "raidel1988",
                    Email = "raidel@gmail.com",
                    CreationDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    IsActive = true,
                    RoleId = 1,
                    Role = new() { Id = 1, Name = "Estudiante", Description = "" }
                }
            };
            return list;
        }
        /*
         * 
            public User(string Name, string Career, string UserType, string Status)
            {
                this.Name = Name;
                this.Career = Career;
                this.UserType = UserType;
                this.Status = Status;
            }

            "nombre": "Mi nombre",
            "carrera": [
                {
                  "cicloIngreso": "2022B",
                  "escuela": "CUVALLES",
                  "descripcion": "MAESTRIA EN INGENIERIA DE SOFTWARE",
                  "nivelDesc": "MAESTRIA",
                  "ultimoCiclo": "2023B",
                  "clave": "string"
                }
            ],
            "tipoUsuario": "Alumno",
            "estatus": "Activo"
         */
    }
}

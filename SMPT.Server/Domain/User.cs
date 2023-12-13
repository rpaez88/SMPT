namespace SMPT.Server.Domain
{
    public class User
    {
        public int? Code { get; set; }
        public string? Name { get; set; }
        public string? Career { get; set; }
        public string? UserType { get; set; }
        public string? Status { get; set; }
        public string? Password { get; set; }

        public static List<User> DB()
        {
            var list = new List<User>()
            {
                new User
                {
                    Code = 222977415,
                    Name = "Raidel",
                    Career = "",
                    UserType = "Student",
                    Status = "OK",
                    Password = "raidel1988"
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

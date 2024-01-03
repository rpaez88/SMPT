using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMPT.Shared.DTO
{
    public class SiiauUserDto
    {
        public string Nombre { get; set; }
        public string TipoUsuario { get; set; }
        public string Estatus { get; set; }
        public CarreraDto[] Carrera { get; set; }
        public bool? Respuesta { get; set; }
        public int? Codigo { get; set; }
        public string? Password { get; set; }
    }
    /*
     {
        "nombre": "RAIDEL PAEZ LLOPIZ"
        "carrera": [
            {
                "cicloIngreso":"2022B",
                "escuela":"CUVALLES",
                "descripcion":"MAESTRIA EN INGENIERIA DE SOFTWARE",
                "nivelDesc":"MAESTRIA",
                "ultimoCiclo":"2024A",
                "clave":"MSWE"
            }
        ],
        "tipoUsuario":"Alumno",
        "estatus":"Activo"
    }*/
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SMPT.Shared.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;
    }
}

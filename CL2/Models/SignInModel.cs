using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class SignInModel
    {
        [Required, EmailAddress]
        [DisplayName("Correo Electrónico")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Recordar Usuario")]
        public bool RememberMe { get; set; }
    }
}

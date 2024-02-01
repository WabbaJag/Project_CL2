using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ForgotPasswordModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Ingrese un correo electrónico válido")]
        [DisplayName("Correo Electrónico Registrado")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}

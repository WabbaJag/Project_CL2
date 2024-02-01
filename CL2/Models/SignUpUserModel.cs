using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Ingrese su Nombre.")]
        [DisplayName("Nombre")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Ingrese su Apellido.")]
        [DisplayName("Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ingrese su dirección de correo.")]
        [DisplayName("Correo Electrónico")]       
        [EmailAddress(ErrorMessage = "Ingrese su dirección de correo.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Ingrese una contraseña.")]
        [Compare("ConfirmPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        [DisplayName("Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirme su contraseña.")]  
        [DisplayName("Confirmar Contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

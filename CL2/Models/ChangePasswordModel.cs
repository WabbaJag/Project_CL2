using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Ingrese la contraseña actual")]
        [DataType(DataType.Password)]
        [DisplayName("Contraseña Actual")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Ingrese la contraseña nueva")]
        [DataType(DataType.Password)]
        [DisplayName("Nueva Contraseña")]
        public string NewPassword { get; set; }


        [Required, DataType(DataType.Password)]
        [DisplayName("Confirmar Nueva Contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmNewPassword { get; set; }
    }
}

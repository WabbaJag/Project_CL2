using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CL2.Models.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Ingrese un Rol")]
        [DisplayName("Nombre del Rol")]
        public string RoleName { get; set; }
    }
}

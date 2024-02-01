using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [DisplayName("Nombre")]
        public string FirstName { get; set; }

        [DisplayName("Apellido")]
        public string LastName { get; set; }


        public DateTime? FechaNacimiento { get; set; }
    }
}

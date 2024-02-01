using CL2.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class Comision
    {
        public int ComisionID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Nombre de la Comisión")]
        public string NombreComision { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Detalles")]
        public string Detalle { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Tipo de Comisión")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Tema de Comisión")]
        public int TemaID { get; set; }


        //FK- Cada comision tiene 1 tema
        public Tema Tema { get; set; }

        // Navigational de comision legislativo 1 a muchos
        public ICollection<ComisionLegislativo> ComisionLegislativos { get; set; }

    }


}

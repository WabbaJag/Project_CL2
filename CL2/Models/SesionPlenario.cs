using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class SesionPlenario
    {

        public int SesionPlenarioID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Plenario")]
        public int PlenarioID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Número de Sesión")]

        public int NumeroSesion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Periodo de Sesión")]
        public string PeriodoSesionPlenario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Tipo de Sesión")]
        public string TipoSesionPlenario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Fecha de sesión")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaSesionPlenario { get; set; }


        // Navigational de SesionPlenario a Plenario, uno a muchos
        public Plenario Plenario{ get; set; }

        // Navigational de SesionPlenario a ControlPolitico, muchos a uno
        public ICollection<ControlPolitico> ControlPolitico { get; set; }

        // Navigational de SesionPlenario a SesionPlenarioActividad, muchos a uno
        public ICollection<ComentarioPlenario> ComentarioPlenario { get; set; }


    }
}

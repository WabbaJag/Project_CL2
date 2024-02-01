using CL2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class SesionComision
    {
        //Varibles de la tabla

        //Primary Key de Sesion Comision
        public int SesionComisionID { get; set; }

        //Foreing Key Comsion Legislativo (Dropdown)
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Comisión - Período Legis.")]
        public int ComisionLegislativoID { get; set; }

        //Variable de Numero de Sesion tiene la cualidad de ser Unique (Dropdown)
        //Se usa FluenteAPi para convertir el valor en Unique se ubica en el CL2Context
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Número Sesión")]
        public string NumeroSesionComision { get; set; }

        //Variable de Periodo Sesion
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Período de Sesión")]
        public string PeriodoSesionComision { get; set; }

        //Variable de fecha de la comision
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaSesionComision{ get; set; }

        //Tipo Sesion Comision
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Tipo de Sesión")]
        public string  TipoSesionComision { get; set; }

        //Navegation String de Sesion Comision
        //Recibo varios datos de Comision Legislativo ( 1 a Muchos )
        public ComisionLegislativo ComisionLegislativos { get; set; }

        //Envio varios datos a Sesion Comision Actividad ( Muchos a 1 )
        public ICollection<SesionComisionActividad> SesionComisionActividad { get; set; }
        

    }
}

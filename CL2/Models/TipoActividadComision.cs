using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class TipoActividadComision
    {
        //Primary Key Tipo de Actividad Comision
        public int TipoActividadComisionID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Detalle Actividad Comision")]
        public String DetalleActividadComision { get; set; }

        //Navigation
        //Envio varios datos a Sesion Comision Actividad ( Muchos a 1 )
        public ICollection<SesionComisionActividad> SesionComisionActividad { get; set; }

    }
}

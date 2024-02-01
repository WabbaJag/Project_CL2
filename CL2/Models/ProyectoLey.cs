using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CL2.Models
{
    public class ProyectoLey
    {
        public int ProyectoLeyID { get; set; }
        [DisplayName("No. de Expediente")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int? NumeroExpediente { get; set; }
        [DisplayName("Título")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public String TituloCorto { get; set; }
        [DisplayName("Título de Proyecto de Ley")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public String TituloCompleto { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Fecha de Presentación")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public DateTime? FechaPresentacion { get; set; }
        [DisplayName("Cantidad de Firmas")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int? CantidadFirmas { get; set; }

        //Navegation Key
        //Public SesionComisionActividad ProyectoID {get; set;}
        //Public SesionPlenarioActividad ProyectoID {get; set;}
        public ICollection<ProyectoDiputado> ProyectoDiputado {get; set;}
        public ICollection<ProyectoTema> TemasProyectos { get; set; }

        //Envio varios datos a Sesion Comision Actividad ( Muchos a 1 )
        public ICollection<SesionComisionActividad> SesionComisionActividad { get; set; }

        //Conexión con Sesion Plenario Actividad
        public ICollection<ComentarioPlenario> ComentarioPlenario { get; set; }
    }
}

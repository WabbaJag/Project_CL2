using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace CL2.Models
{
    public class DiscusionComision
    {
        //Llave Compuesta----------------------------------------
        //Primary Key Discusion Comision
        public int DiscusionComisionID { get; set; }

        //Foreing Key Sesion Comision (Dropdown)
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Sesión de Comisión")]
        public int SesionComisionID { get; set; }

        //Foreing Key Sesion Comision Activadad (Dropdown)
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Sesión Actividad de Comisión")]
        public int SesionComisionActividadID { get; set; }


        //-------------------------------------------------------

        //Foreing Key Diputado (Buscador integrado)
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Diputado")]
        public int DiputadoID { get; set; }

        //Variable Resumen Comentario
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Resumen Comentario")]
        public string ResumenComentario { get; set; }

        [DisplayName("Detalle Comentario")]
        public string DetalleComentario { get; set; }

        [DisplayName("Tipo Comentario")]
        public string TipoComentario { get; set; }

        //Navegation
        //Recibo varios datos de Sesion Comision Actividad ( 1 a Muchos )
        public SesionComisionActividad SesionComisionActividades { get; set; }
        //Recibo varios datos de Diputado ( 1 a Muchos )
        public Diputado Diputado { get; set; }

    }

}

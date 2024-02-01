using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ComentarioPlenario
    {
        //Variables 

        //Primary Key 
        public int ComentarioPlenarioID { get; set; }

        //Foreing Kay Sesion Comision (Dropdown)
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Sesión de Plenario")]
        public int SesionPlenarioID { get; set; }

        //Foreing Key Proyecto Ley (Dropdown)
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Proyecto de Ley")]
        public int ProyectoLeyID { get; set; }

        //Foreing Key Diputado (Dropdown)
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Diputado")]
        public int DiputadoID { get; set; }

        // Tipo de actividad sobre la cual se comentó: Primer Debate, Segundo Debate, Moción
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Tipo de Actividad")]

        public string TipoActividad { get; set; }
        
        // Posicion del diputado: A favor, en contra, neutral
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Posición")]

        public string PosicionComentario { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Resumen de comentario")]
        public string ResumenComentario { get; set; }

        public string DetalleComentario{ get; set; }





        //Navigation Keys
        public SesionPlenario SesionPlenario { get; set; }

        public Diputado Diputado { get; set; }

        public ProyectoLey ProyectoLey { get; set; }
    }
}


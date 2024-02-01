using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ControlPolitico
    {

        public int ControlPoliticoID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Sesion de Plenario")]
        public int SesionPlenarioID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Diputado")]
        public int DiputadoID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Tema")]
        public int TemaID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Resumen de comentario")]
        public string ResumenComentario { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Detalle de comentario")]
        public string DetalleComentario { get; set; }

        // A favor, en contra, neutral
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Posición frente a tema")]
        public string PosicionControl { get; set; }

        //Navigation Keys
        public SesionPlenario SesionPlenario { get; set; }

        //Relación de uno a muchos con Temas y Diputados
        public Tema Temas { get; set; }
        public Diputado Diputados { get; set; }
    }
}

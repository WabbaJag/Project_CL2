using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class SesionComisionActividad
    { 
        //Variables 

        //Llave Compuesta----------------------------------------
            //Primary Key Sesion Comision Actividad
            public int SesionComisionActividadID { get; set; }

            //Foreing Kay Sesion Comision (Dropdown)
            [Required(ErrorMessage = "Este campo es requerido.")]
            [DisplayName("Sesión de Comisión")]
            public int SesionComisionID { get; set; }
        //--------------------------------------------------------

        //Foreing Key Proyecto Ley (Dropdown)
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Proyecto de Ley")]
        public int ProyectoLeyID { get; set; }

        //Foreing Key Diputado (Dropdown)
        [DisplayName("Diputado Proponente de la Moción")]
        public int DiputadoID { get; set; }

        //Foreing Key Tipo Actividad Comision (Dropdown)
        //En este caso tipo de actividad va a tener dos datos Mocion y Dictamen
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Tipo de Actividad")]
        public int TipoActividadID { get; set; }

        //Variables opcionales dependiento el tipo de actividad de la Comision
            //Si el tipo de actividad de la comision es !MOCION! se debe de solicitar la siguiente informacion:
            //Variable de Nombre de la Mocion
            [DisplayName("Nombre de la Moción")]
            public string NumeroMocion { get; set; }
            
            //Tipo de mocion
            //Existen 5 tipos de mociones fijas 
            [DisplayName("Tipo de Moción")]
            public string TipoMocion { get; set; }
            


            //Variable Decision de la Mocion
            [DisplayName("Decisión de la Moción")]
            public string DecisionMocion { get; set; }

            //Si el tipo de actividad de la comision es !DICTAMEN! se debe de solicitar la siguiente informacion:

            //Variable 
            [DisplayName("Votos a Favor del Dictamen")]
            public int VotosFavorDictamen { get; set; }

            [DisplayName("Votos encontra del Dictamen")]
            public int VotosContraDictamen { get; set; }

            [DisplayName("Detalle del Dictamen")]
            public string DetalleActividad { get; set; }

        //Navegation 
        //Recibo varios datos de Comision Legislativo ( 1 a Muchos )
        public SesionComision SesionComisiones { get; set; }
        //Recibo varios datos de Proyecto Ley ( 1 a Muchos )
        public ProyectoLey ProyectosLey { get; set; }
        //Recibo varios datos de Tipo Activivdad  ( 1 a Muchos)
        public TipoActividadComision TipoActividadComisiones { get; set; }
        //Recibo varios datos de Diputado ( 1 a Muchos )
        public Diputado Diputado { get; set; }

        //Envio varios datos a Discusion Comision Actividad ( Muchos a 1 )
        public ICollection<DiscusionComision> DiscusionComision { get; set; }
    }
}

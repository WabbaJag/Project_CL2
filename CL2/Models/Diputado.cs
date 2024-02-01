using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class Diputado
    {
        public int DiputadoID { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Nombre")]
        [StringLength(50)]
        public string NombreDiputado { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Primer Apellido")]
        [StringLength(50)]
        public string PrimerApellido { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        [DisplayName("Segundo Apellido")]
        [StringLength(50)]
        public string SegundoApellido { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Provincia { get; set; }
        [DisplayName("Correo Electrónico")]
        public string CorreoDiputado { get; set; }
        [DisplayName("Número Telefónico")]
        public string TelefonoDiputado { get; set; }
        [DisplayName("Número Telefónico (alt.)")]
        public string TelefonoDiputado2 { get; set; }
        [DisplayName("Género")]
        public string GeneroDiputado { get; set; }
        [StringLength(200, ErrorMessage = "URL de Imagen no puede exceder 200 caracteres.")]
        public string Imagen { get; set; }

        [Display(Name = "Nombre Completo")]
        public string nombreCompleto
        {
            get
            {
                return PrimerApellido + " " + SegundoApellido + ", " + NombreDiputado;
            }
        }



        //Navegation Keys

        //conexion con la clase Proyecto Diputado
        public ICollection<ProyectoDiputado> ProyectosDiputado { get; set; }
        //conexion con clase IntegrantesPlenario
        public ICollection<IntegrantesPlenario> IntegrantesPlenarios { get; set; }
        //conexion con Comision Diputados
        public ICollection<ComisionDiputados> ComisionDiputados { get; set; }

        //Conexión con ControlPolitico
        public ICollection<ControlPolitico> ControlPolitico { get; set; }

        //Conexión con Sesion Plenario Actividad
        public ICollection<ComentarioPlenario> ComentarioPlenario { get; set; }

        //Conexión con Sesion Plenario Actividad

        //Envio varios datos a Sesion Comision Actividad ( Muchos a 1 )
        public ICollection<SesionComisionActividad> SesionComisionActividad { get; set; }

        //Conexión con Sesion Plenario Actividad
        public ICollection<DiscusionComision> DiscusionComision { get; set; }
    }
}

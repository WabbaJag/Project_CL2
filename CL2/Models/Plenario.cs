using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class Plenario
    {
        public int PlenarioID { get; set; }

        //para crear la relacion con Administración (FK)
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int AdministracionID { get; set; }
        public Administracion Administracion { get; set; }


        //conexion con clase IntegrantesPlenario
        public ICollection<IntegrantesPlenario> IntegrantesPlenarios { get; set; }

        //conexion con SesionPlenario
        public ICollection<SesionPlenario> SesionPlenarios { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class Fraccion
    {
        public int FraccionID { get; set; }
        [DisplayName("Nombre de fracción")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public String NombreFraccion { get; set;  }
        [DisplayName("Siglas de fracción")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public String SiglasFraccion { get; set; }


        //conexion con clase IntegrantesPlenario

        public ICollection<IntegrantesPlenario> IntegrantesPlenarios { get; set; }
    }


}

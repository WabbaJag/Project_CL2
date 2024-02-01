using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

//clase de unión entre Diputado y Plenario
namespace CL2.Models
{
    public class IntegrantesPlenario
    {
        //utiliza diputado y plenario como llave compuesta (definido en cl2context.cs)
        public int DiputadoID { get; set; }
        public int PlenarioID { get; set; }
        public int? FraccionID { get; set; }

        [StringLength(15)]
        [Display(Name = "Detalle")]
        public string Detalle_Puesto { get; set; }


        //public List<IntegrantesPlenario> integrantesPlenariosLista { get; set; }

        public Diputado Diputado { get; set; }
        public Plenario Plenario { get; set; }
        public Fraccion Fraccion { get; set; }

    }
}

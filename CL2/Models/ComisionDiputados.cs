using CL2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ComisionDiputados
    {
        [Key, ForeignKey("ComisionLegislativo")]
        public int ComisionLegislativoID { get; set; }
        [Key, ForeignKey("Diputado")]
        public int DiputadoID { get; set; }
        public String DetallePuesto { get; set; }

        //FK
        public ComisionLegislativo ComisionLegislativo { get; set; }
        public Diputado Diputado { get; set; }
    }
}

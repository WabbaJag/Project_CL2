using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ComisionLegislativo
    {
        public int ComisionLegislativoID { get; set; }

        public int ComisionID { get; set; }
        public int LegislativoID { get; set; }

        //FK
        public Legislativo Legislativo { get; set; }
        public Comision Comision { get; set; }
        public ICollection<SesionComision> SesionComision { get; set; }
        public ICollection<ComisionDiputados> ComisionDiputados { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models.ViewModel
{
    public class SesionComisionUtility
    {
        public IEnumerable<ComisionLegislativo> Comisiones { get; set; }
        public IEnumerable<SesionComision> SesionComisions { get; set; }
    }
}
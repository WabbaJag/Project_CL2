using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models.ViewModel
{
    public class SesionPlenarioGrupo
    {
        public SesionPlenario Sesion { get; set; }
        public IEnumerable<SesionPlenario> Sesions { get; set; }
        public ControlPolitico Control { get; set; }
        public IEnumerable<ControlPolitico> ControlPoliticos { get; set; }
    }
}
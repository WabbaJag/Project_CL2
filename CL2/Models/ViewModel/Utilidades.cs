using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models.ViewModels
{
    public class Utilidades
    {
        public IEnumerable<Administracion> Administracions{ get; set; }
        public IEnumerable<Legislativo> Legislativos { get; set; }
        public IEnumerable<Fraccion> Fraccions { get; set; }
        public IEnumerable<Tema> Temas { get; set; }
    }
}
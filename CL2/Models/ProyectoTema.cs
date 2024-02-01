using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ProyectoTema
    {
        public int ProyectoLeyID { get; set; }
        public int TemaID { get; set; }

        //Nevegations Key
        public ProyectoLey ProyectosLey { get; set; }
        public Tema Tema { get; set; }
    }
}

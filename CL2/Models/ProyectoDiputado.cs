using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class ProyectoDiputado
    {
        public int ProyectoLeyID { get; set; }
        public int DiputadoID { get; set; }

        //Navegation Keys
        public ProyectoLey ProyectosLey { get; set; }
        public Diputado Diputado { get; set; }

    }
}

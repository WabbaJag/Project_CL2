using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class Tema
    {
        public int TemaID { get; set;}

        [DisplayName("Tema")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string TemaDesc { get; set; }

        //Navegation Key
        public ICollection<ProyectoTema> TemasProyecto { get; set; }
       
        public ICollection<Comision> Comision { get; set; }
        //Conexion con Control Politico
        public ICollection<ControlPolitico> ControlPolitico { get; set; }

    }

    
}

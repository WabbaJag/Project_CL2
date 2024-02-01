using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class Administracion
    {
        public int AdministracionID { get; set; }
        [DisplayName("Período")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string AdministracionPeriodo { get; set; } // 2020-2024
        [DisplayName("Presidente de la República")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string PresidenteRepublica { get; set; }
        [DisplayName("Primer Año")]
        public int PrimerAnno {
            get {
                int año;                
                if (AdministracionPeriodo == null)
                {
                    año = 0;
                }
                else
                {
                    Int32.TryParse(AdministracionPeriodo.Substring(0, 4), out año);
                }
                return año;
            }
        }
        [DisplayName("Ultimo Año")]
        public int UltimoAnno
        {
            get
            {
                int año;
                if (AdministracionPeriodo == null)
                {
                    año = 0;
                }
                else
                {
                    Int32.TryParse(AdministracionPeriodo.Substring(5, 4), out año);
                }
                return año;
            }
        }

        //

        public ICollection<Legislativo> Legislativos { get; set; }


        //para crear la relación de uno a uno con Plenario
        public Plenario Plenario { get; set; }
    }
}

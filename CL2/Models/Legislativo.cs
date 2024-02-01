using CL2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CL2.Models
{
    public class Legislativo
    {
        public int LegislativoID { get; set; }
        [DisplayName("Años Legislativos")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string AnnoLegislativo { get; set; } //2020-2021

        [DisplayName("Período Administrativo")]
        public int AdministracionID { get; set; }

        [DisplayName("Primer Año")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int PrimerAnno
        {
            get
            {
                Int32.TryParse(AnnoLegislativo.Substring(0, 4), out int año);
                return año;
            }
        }
        [DisplayName("Ultimo Año")]
        public int UltimoAnno
        {
            get
            {
                Int32.TryParse(AnnoLegislativo.Substring(5, 4), out int año);
                return año;
            }
        }

        //navigation keys
        public Administracion Administracion { get; set; }

       public ICollection<ComisionLegislativo> ComisionLegislativo { get; set; }

        public ICollection<SesionComision> SesionComision { get; set; }
    }
}

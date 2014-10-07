using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class PlantillaDetalle:Generica
    {
        public int PlantillaId { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }
        public string Pregunta { get; set; }
        public int Orden { get; set; }
        public virtual Plantilla Plantilla { get; set; }
    }
}

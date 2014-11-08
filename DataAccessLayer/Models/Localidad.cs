using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Localidad:Generica
    {       
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        [DefaultValue("L000")]
        public string Clave { get; set; }
        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Nombre { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public decimal Altitud { get; set; }      
        public int PoblacionMasculina { get; set; }
        public int PoblacionFemenina { get; set; }
        public int PoblacionTotal { get; set; }
        public int MunicipioId { get; set; }
        public int? Orden { get; set; }
        public virtual Municipio Municipio { get; set; }
    }
}

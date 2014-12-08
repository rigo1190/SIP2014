using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Localidad:Generica
    {       
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]       
        public string Clave { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Nombre { get; set; }
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public decimal Altitud { get; set; }      
        public int PoblacionMasculina { get; set; }
        public int PoblacionFemenina { get; set; }
        public int PoblacionTotal { get; set; }

        [Index("IX_Orden_MunicipioId", 1, IsUnique = true)]
        public int MunicipioId { get; set; }

        [Index("IX_Orden_MunicipioId", 2)]
        public int Orden { get; set; }
        public int TipoLocalidadId { get; set; }
        public virtual TipoLocalidad TipoLocalidad { get; set; }
        public virtual Municipio Municipio { get; set; }
    }
}

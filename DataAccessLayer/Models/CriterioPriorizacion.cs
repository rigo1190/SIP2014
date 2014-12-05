using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class CriterioPriorizacion:Generica
    {
        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }

        [StringLength(100, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Nombre { get; set; }

        [Index(IsUnique = true)]
        public int Orden { get; set; }
        public bool AplicaConvenio { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AperturaProgramatica:Generica
    {

        public AperturaProgramatica() 
        {
            this.DetalleMetas = new HashSet<AperturaProgramaticaMeta>();
            this.DetalleSubElementos = new HashSet<AperturaProgramatica>();
        }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }
        public string Nombre { get; set; }

        [Index("IX_Orden_ParentId", 1, IsUnique = true)]
        public int Orden { get; set; }

        [Index("IX_Orden_ParentId", 2)]
        public int? ParentId { get; set; }        
        public int EjercicioId { get; set; }
        public int? Nivel { get; set; }
        public virtual Ejercicio Ejercicio { get; set; }
        public virtual AperturaProgramatica Parent { get; set; }
        public virtual ICollection<AperturaProgramaticaMeta> DetalleMetas { get; set; }
        public virtual ICollection<AperturaProgramatica> DetalleSubElementos { get; set; }
    }
}

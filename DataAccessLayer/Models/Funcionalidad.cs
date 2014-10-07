using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Funcionalidad:Generica
    {
        public Funcionalidad() 
        {
            this.DetalleSubElementos = new HashSet<Funcionalidad>();
        }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }
        public string Descripcion { get; set; }

        [Index("IX_Orden_ParentId", 1, IsUnique = true)]
        public int Orden { get; set; }

        [Index("IX_Orden_ParentId", 2)]
        public int? ParentId { get; set; }
        public int? Nivel { get; set; }
        public virtual Funcionalidad Parent { get; set; }
        public virtual ICollection<Funcionalidad> DetalleSubElementos { get; set; }

    }
}

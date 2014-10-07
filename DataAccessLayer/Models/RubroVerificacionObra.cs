using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class RubroVerificacionObra:Generica
    {
        public RubroVerificacionObra() 
        {
            this.DetalleSubRubros = new HashSet<RubroVerificacionObra>();
        }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }

        [StringLength(200, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Nombre { get; set; }

        [Index(IsUnique = true)]
        public int Orden { get; set; }
        public int? ParentId { get; set; }
        public virtual RubroVerificacionObra Parent { get; set; }
        public virtual ICollection<RubroVerificacionObra> DetalleSubRubros { get; set; }
    }
}

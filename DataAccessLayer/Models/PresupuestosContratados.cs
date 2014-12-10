using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class PresupuestosContratados:Generica
    {
        public PresupuestosContratados()
        {
            this.detalleEstimaciones = new HashSet<EstimacionesConceptos>();
        }

        public int ContratoDeObraId { get; set; }

        public int ConceptoDeObraId { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }


        public virtual ContratosDeObra ContratoDeObra { get; set; }

        public virtual ConceptosDeObra ConceptoDeObra { get; set; }

        public virtual ICollection<EstimacionesConceptos> detalleEstimaciones { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Estimaciones:Generica
    {
        public Estimaciones()
        {
            this.detalleConceptos = new HashSet<EstimacionesConceptos>();
        }

        public int ContratoDeObraId { get; set; }

        public int NumeroDeEstimacion { get; set; }
        
        public DateTime FechaDeEstimacion { get; set; }

        public decimal Importe { get; set; }

        public decimal AmortizacionAnticipo { get; set; }

        public decimal Retencion2AlMillar { get; set; }

        public decimal Retencion5AlMillar { get; set; }


        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

        public int Status { get; set; }

        //datos del cheque con el que lo cobrarian
        public virtual ContratosDeObra ContratoDeObra { get; set; }

        public virtual ICollection<EstimacionesConceptos> detalleConceptos { get; set; }

        
    }
}

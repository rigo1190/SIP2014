using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class EstimacionesProgramadas:Generica 
    {
        public EstimacionesProgramadas()
        {
            this.detalleConceptos = new HashSet<EstimacionesProgramadasConceptos>();
        }

        public int ContratoDeObraId { get; set; }

        public int NumeroDeEstimacion { get; set; }

        public DateTime FechaDeEstimacion { get; set; }


        public decimal ImporteEstimado { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }
        
        public decimal AmortizacionAnticipo { get; set; }
        public decimal Retencion5AlMillar { get; set; }
        public decimal Retencion2AlMillar { get; set; }
        public decimal Retencion2AlMillarSyV { get; set; }
        public decimal ImporteAPagar { get; set; }


        public virtual ContratosDeObra ContratoDeObra { get; set; }
        
        public virtual ICollection<EstimacionesProgramadasConceptos> detalleConceptos { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class EstimacionesConceptos:Generica
    {

        public int EstimacionId { get; set; }

        public int PresupuestoContratadoId { get; set; }


        public double Cantidad { get; set; }

        public decimal Subtotal { get; set; }


        public virtual Estimaciones Estimacion { get; set; }
        public virtual PresupuestosContratados PresupuestoContratado { get; set; }

    }
}

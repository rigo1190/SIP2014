using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class EstimacionesProgramadasConceptos:Generica
    {
        public int EstimacionProgramadaId { get; set; }

        public int PresupuestoContratadoId { get; set; }


        public double Cantidad { get; set; }

        public decimal Subtotal { get; set; }


        public virtual EstimacionesProgramadas EstimacionProgramada { get; set; }
        public virtual PresupuestosContratados PresupuestoContratado { get; set; }
    }
}

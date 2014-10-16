using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinancieroUnidadPresupuestal:Generica
    {
        public int TechoFinancieroId { get; set; }

        public int UnidadPresupuestalId { get; set; }

        public decimal Importe { get; set; }
        
        public decimal tmpImporteAsignado { get; set; }

        public decimal tmpImporteEjecutado { get; set; }


        public virtual TechoFinanciero TechoFinanciero { get; set; }
        public virtual UnidadPresupuestal UnidadPresupuestal { get; set; }
    }
}

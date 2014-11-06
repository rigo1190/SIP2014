using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinancieroBitacoraMovimientos:Generica
    {
        public int TechoFinancieroBitacoraId { get; set; }

        public int TechoFinancieroId { get; set; }

        public int Submovimiento { get; set; }

        public int UnidadPresupuestalId { get; set; }

        public decimal Incremento { get; set; }
        
        public decimal Decremento { get; set; }

        public virtual TechoFinancieroBitacora TechoFinancieroBitacora { get; set; }
        public virtual TechoFinanciero TechoFinanciero { get; set; }
        public virtual UnidadPresupuestal UnidadPresupuestal { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ObraFinanciamiento:Generica
    {
        public int ObraId { get; set; }
        public int FinanciamientoId { get; set; }
        public decimal Importe { get; set; }
        public virtual Obra Obra { get; set; }
        public virtual Financiamiento Financiamiento { get; set; }

    }
}

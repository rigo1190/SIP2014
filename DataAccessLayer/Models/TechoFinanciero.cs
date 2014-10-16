using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinanciero:Generica
    {
        public int EjercicioId { get; set; }
        public int FinanciamientoId { get; set; }

        public decimal Importe { get; set; }

        public decimal tmpImporteAsignado { get; set; }

        public decimal tmpImporteEjecutado { get; set; }

        public virtual Ejercicio Ejercicio { get; set; }
        public virtual Financiamiento Financiamiento { get; set; }


    }
}

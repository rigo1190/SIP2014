using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinanciero:Generica
    {
        public TechoFinanciero()
        {
            this.detalleUnidadesPresupuestales = new HashSet<TechoFinancieroUnidadPresupuestal>();
        }
        public int EjercicioId { get; set; }
        public int FinanciamientoId { get; set; }

        public decimal Importe { get; set; }

        public decimal tmpImporteAsignado { get; set; }

        public decimal tmpImporteEjecutado { get; set; }

        public virtual Ejercicio Ejercicio { get; set; }
        public virtual Financiamiento Financiamiento { get; set; }

        public virtual ICollection<TechoFinancieroUnidadPresupuestal> detalleUnidadesPresupuestales { get; set; }


        //public decimal GetImporteAsignado(object dataitem)
        //{
        //    return (from tfup in detalleUnidadesPresupuestales select tfup).Sum(p => p.Importe);
        //}

    }
}

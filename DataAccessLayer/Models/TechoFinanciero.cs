using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            this.detalleBitacoraMovimientos = new HashSet<TechoFinancieroBitacoraMovimientos>();
                 
        }

        [Index("IX_EjercicioId_FinanciamientoId", 1, IsUnique = true)]
        public int EjercicioId { get; set; }

        [Index("IX_EjercicioId_FinanciamientoId", 2)]
        public int FinanciamientoId { get; set; }
        public decimal Importe { get; set; }
        public decimal tmpImporteAsignado { get; set; }
        public decimal tmpImporteEjecutado { get; set; }
        public virtual Ejercicio Ejercicio { get; set; }
        public virtual Financiamiento Financiamiento { get; set; }
        public string Descripcion
        {
            get
            {
                return this.Financiamiento.Descripcion;
            }
        }

        public virtual ICollection<TechoFinancieroUnidadPresupuestal> detalleUnidadesPresupuestales { get; set; }
        public virtual ICollection<TechoFinancieroBitacoraMovimientos> detalleBitacoraMovimientos { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinancieroUnidadPresupuestal:Generica
    {
        public TechoFinancieroUnidadPresupuestal()
        {
            this.DetalleObraFinanciamiento = new HashSet<ObraFinanciamiento>();
        }

        [Index("IX_TechoFinancieroId_UnidadPresupuestalId", 1, IsUnique = true)]
        public int TechoFinancieroId { get; set; }

        [Index("IX_TechoFinancieroId_UnidadPresupuestalId", 2)]
        public int UnidadPresupuestalId { get; set; }
        public decimal Importe { get; set; }
        public decimal tmpImporteAsignado { get; set; }
        public decimal tmpImporteEjecutado { get; set; }
        public virtual TechoFinanciero TechoFinanciero { get; set; }
        public virtual UnidadPresupuestal UnidadPresupuestal { get; set; }
        public virtual ICollection<ObraFinanciamiento> DetalleObraFinanciamiento { get; set; }
        public decimal GetImporteAsignado
        {
            get
            {
                return (from of in DetalleObraFinanciamiento select of).Sum(of => of.Importe);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ObraFinanciamiento:Generica
    {
        [Index("IX_ObraId_TechoFinancieroUnidadPresupuestalId", 1, IsUnique = true)]
        public int ObraId { get; set; }

        [Index("IX_ObraId_TechoFinancieroUnidadPresupuestalId", 2)]
        public int TechoFinancieroUnidadPresupuestalId { get; set; }
        public decimal Importe { get; set; }
        public virtual Obra Obra { get; set; }
        public virtual TechoFinancieroUnidadPresupuestal TechoFinancieroUnidadPresupuestal { get; set; }

    }
}

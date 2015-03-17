using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Transferencia:Generica
    {
        public Transferencia() 
        {
            this.Detalle = new HashSet<TransferenciaDetalle>();
        }

        [Index("IX_Numero",IsUnique=true)]
        public int Numero { get; set; }
        public DateTime FechaElaboracion { get; set; }
        public int TechoFinancieroUnidadPresupuestalId { get; set; }
        public virtual TechoFinancieroUnidadPresupuestal TechoFinancieroUnidadPresupuestal { get; set; }
        public virtual ICollection<TransferenciaDetalle> Detalle { get; set; }
    }
}

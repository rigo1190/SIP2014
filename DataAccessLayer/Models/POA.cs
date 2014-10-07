using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class POA:Generica
    {
        public POA() 
        {
            this.Detalles = new HashSet<POADetalle>();
        }

        [Index("IX_EjercicioId_UnidadPresupuestalId", 1, IsUnique = true)]
        public int EjercicioId { get; set; }

        [Index("IX_EjercicioId_UnidadPresupuestalId",2)]
        public int UnidadPresupuestalId { get; set; }
        public virtual Ejercicio Ejercicio { get; set; }
        public virtual UnidadPresupuestal UnidadPresupuestal { get; set; }
        public virtual ICollection<POADetalle> Detalles { get; set; }
       
    }
}

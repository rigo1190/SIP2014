using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class POAPlantilla:Generica
    {
        [Index("IX_POADetalleId_PlantillaId", 1, IsUnique = true)]
        public int POADetalleId { get; set; }

        [Index("IX_POADetalleId_PlantillaId", 2)]
        public int PlantillaId { get; set; }
        public virtual POADetalle POADetalle { get; set; }
        public Plantilla Plantilla { get; set; }
        public virtual ICollection<POAPlantillaDetalle> Detalles { get; set; }

    }
}

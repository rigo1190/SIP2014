using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ObraPlantilla:Generica
    {
        public ObraPlantilla() 
        {
            this.Detalles = new HashSet<ObraPlantillaDetalle>();
        }

        [Index("IX_ObraId_PlantillaId", 1, IsUnique = true)]
        public int ObraId { get; set; }

        [Index("IX_ObraId_PlantillaId", 2)]
        public int PlantillaId { get; set; }
        public virtual Obra Obra { get; set; }
        public virtual Plantilla Plantilla { get; set; }
        public virtual ICollection<ObraPlantillaDetalle> Detalles { get; set; }
        
    }
}

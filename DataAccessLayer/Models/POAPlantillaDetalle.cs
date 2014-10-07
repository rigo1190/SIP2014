using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class POAPlantillaDetalle:Generica
    {
        [Index("IX_POAPlantillaId_PlantillaDetalleId", 1, IsUnique = true)]
        public int POAPlantillaId { get; set; }

        [Index("IX_POAPlantillaId_PlantillaDetalleId",2)]
        public int PlantillaDetalleId { get; set; }
        public enumRespuesta Respuesta { get; set; }
        public string Observaciones { get; set; }
        public string RutaArchivo { get; set; }
        public virtual POAPlantilla POAPlantilla { get; set; }
        public virtual PlantillaDetalle PlantillaDetalle { get; set; }
 
    }


}

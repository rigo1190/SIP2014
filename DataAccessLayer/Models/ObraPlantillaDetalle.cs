using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ObraPlantillaDetalle:Generica
    {
        [Index("IX_ObraPlantillaId_PlantillaDetalleId", 1, IsUnique = true)]
        public int ObraPlantillaId { get; set; }

        [Index("IX_ObraPlantillaId_PlantillaDetalleId", 2)]
        public int PlantillaDetalleId { get; set; }
        public enumRespuesta Respuesta { get; set; }       
        public string Observaciones { get; set; }
        public string RutaArchivo { get; set; }
        public virtual ObraPlantilla ObraPlantilla { get; set; }
        public virtual PlantillaDetalle PlantillaDetalle { get; set; }
    }

    public enum enumRespuesta 
    {
        Si=1,
        No=2,
        NoAplica=3
    }

}

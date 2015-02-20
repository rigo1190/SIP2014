using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class FundamentacionPlantilla:Generica
    {
        public int PlantillaDetalleId { get; set; }
        public string DescripcionCorta { get; set; }
        public string Fundamentacion { get; set; }
        public int RubroFundamentacionId { get; set; }
        public virtual PlantillaDetalle PlantillaDetalle { get; set; }
        public virtual RubroFundamentacion RubroFundamentacion { get; set; }

    }
}

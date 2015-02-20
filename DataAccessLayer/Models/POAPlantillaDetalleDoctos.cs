using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class POAPlantillaDetalleDoctos : Generica
    {
        public int POAPlantillaDetalleId { get; set; }

        [StringLength(250)]
        public string NombreArchivo { get; set; }

        [StringLength(250)]
        public string TipoArchivo { get; set; }
        public string Observaciones { get; set; }
        public virtual POAPlantillaDetalle POAPlantillaDetalle { get; set; }
        
    }
}

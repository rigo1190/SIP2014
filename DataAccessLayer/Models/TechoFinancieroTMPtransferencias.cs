using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinancieroTMPtransferencias:Generica
    {

        public int Usuario { get; set; }

        public int Financiamiento { get; set; }
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }        
        public decimal Importe { get; set; }


        public virtual UnidadPresupuestal Origen { get; set; }
        public virtual UnidadPresupuestal Destino { get; set; } 
         
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Financiamiento:Generica
    {
        public int AñoId { get; set; }
        public int FondoId { get; set; }
        public int ModalidadFinanciamientoId { get; set; }
        public virtual Año Año{ get; set; }
        public virtual Fondo Fondo { get; set; }
        public virtual ModalidadFinanciamiento ModalidadFinanciamiento { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class FondoLineamientos:Generica
    {

        public int FondoId { get; set; }

        public string TipoDeObrasYAcciones { get; set; }
        public string CalendarioDeIngresos { get; set; }
        public string VigenciaDePago { get; set; }

        public string NormatividadAplicable { get; set; }

        public string Contraparte { get; set; }
            
        public virtual Fondo Fondo { get; set; }


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinancieroStatus:Generica
    {

        public int EjercicioId { get; set; }

        public int Status { get; set; }

        public virtual Ejercicio Ejercicio { get; set; }

    }
}

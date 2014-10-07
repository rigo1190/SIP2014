using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Ejercicio:Generica
    {
        [Index(IsUnique = true)]
        public int Año { get; set; }
        public bool Activo { get; set; }
    }
}

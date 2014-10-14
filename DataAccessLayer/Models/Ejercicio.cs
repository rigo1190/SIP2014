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
        public decimal FactorIva { get; set; }
        public enumEstatusEjercicio Estatus { get; set; }
    }

    public enum enumEstatusEjercicio 
    {
        Nuevo=0,
        Activo=1,
        Cerrado=2
    }

}

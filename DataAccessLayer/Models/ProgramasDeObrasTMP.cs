using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Models
{
    public class ProgramasDeObrasTMP:Generica
    {
        public int Usuario { get; set; }

        public int ContratoDeObraId { get; set; }

        public int PresupuestoContratadoId { get; set; }

        public double Numero { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Inciso { get; set; }

        public string Descripcion { get; set; }
        
        public string UnidadDeMedida { get; set; }
 
        public double Cantidad { get; set; }

        public DateTime? Inicio { get; set; }

        public DateTime? Termino { get; set; }

        public int Status { get; set; }

        [StringLength(250, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string StatusNombre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ProgramasDeObras:Generica
    {

        public int ContratoDeObraId { get; set; }


        public int PresupuestoContratadoId { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Termino { get; set; }

        public int Status { get; set; }

        public virtual ContratosDeObra ContratoDeObra { get; set; }

        public virtual PresupuestosContratados PresupuestoContratado { get; set; }


    }
}

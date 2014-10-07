using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UsuarioUnidadPresupuestal:Generica
    {
        public int UsuarioId { get; set; }
        public int UnidadPresupuestalId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual UnidadPresupuestal UnidadPresupuestal { get; set; }
    }
}

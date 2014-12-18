using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class PresupuestosContratados:Generica
    {
        public PresupuestosContratados()
        {
            this.detalleEstimaciones = new HashSet<EstimacionesConceptos>();
        }

        public int ContratoDeObraId { get; set; }

        
        public int Nivel { get; set; }
        public int Orden { get; set; }


        public double Numero { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Inciso { get; set; }

        public string Descripcion { get; set; }


        public string UnidadDeMedida { get; set; }
        public double Cantidad { get; set; }
                
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }

        public int? ParentId { get; set; }

        public virtual ContratosDeObra ContratoDeObra { get; set; }

        public virtual PresupuestosContratados Parent { get; set; }

        public virtual ICollection<EstimacionesConceptos> detalleEstimaciones { get; set; }

    }
}

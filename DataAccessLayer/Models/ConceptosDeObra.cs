using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class ConceptosDeObra:Generica
    {

        public ConceptosDeObra()
        {
            this.detalleConceptos = new HashSet<ConceptosDeObra>();
            this.detallePresupuestos = new HashSet<PresupuestosContratados>();
        }
        public int ObraId { get; set; }
        
        public int Nivel { get; set; }
        public int Orden { get; set; }


        public double Numero { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Inciso { get; set; }

        public string Descripcion { get; set; }


        public string UnidadDeMedida { get; set; }
        public double Cantidad { get; set; }

        public int? ParentId { get; set; }
        
        public virtual Obra Obra { get; set; }

        public virtual ConceptosDeObra Parent { get; set; }

        public virtual ICollection<ConceptosDeObra> detalleConceptos { get; set; }

        public virtual ICollection<PresupuestosContratados> detallePresupuestos { get; set; }




    }
}

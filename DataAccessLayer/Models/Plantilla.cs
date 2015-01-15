using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Plantilla:Generica
    {

        public Plantilla() 
        {
            this.Detalles = new HashSet<Plantilla>();
            this.DetallePreguntas = new HashSet<PlantillaDetalle>();
        }       

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Clave { get; set; }
        public string Descripcion { get; set; }       

        [Index("IX_Orden_DependeDeId", 1, IsUnique = true)]
        public int Orden { get; set; }

        [Index("IX_Orden_DependeDeId", 2)]
        public int? DependeDeId { get; set; }      
        public virtual Plantilla DependeDe { get; set; }
        public virtual ICollection<Plantilla> Detalles { get; set; } 
        public virtual ICollection<PlantillaDetalle> DetallePreguntas { get; set; }
     
        [NotMapped]
        public Plantilla Padre
        {
            get
            {
                Plantilla pl;

                if (DependeDe == null)
                {
                    pl = this;
                }
                else
                {
                    pl = DependeDe.Padre;
                }

                return pl;
            }
        }


    }
}

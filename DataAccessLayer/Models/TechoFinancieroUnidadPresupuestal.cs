using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinancieroUnidadPresupuestal:Generica
    {
        public TechoFinancieroUnidadPresupuestal()
        {
            this.DetalleObraFinanciamiento = new HashSet<ObraFinanciamiento>();
        }

        [Index("IX_TechoFinancieroId_UnidadPresupuestalId", 1, IsUnique = true)]
        public int TechoFinancieroId { get; set; }

        [Index("IX_TechoFinancieroId_UnidadPresupuestalId", 2)]
        public int UnidadPresupuestalId { get; set; }
        public decimal Importe { get; set; }
        public decimal ImporteInicial { get; set; }
        public decimal tmpImporteAsignado { get; set; }
        public decimal tmpImporteEjecutado { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NumOficioAsignacionPresupuestal { get; set; }
        public DateTime FechaOficioAsignacionPresupuestal { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NumOficioAlcance { get; set; }
        public DateTime FechaOficioAlcance { get; set; }
        public string ObservacionesAlcance { get; set; }

        public virtual TechoFinanciero TechoFinanciero { get; set; }
        public virtual UnidadPresupuestal UnidadPresupuestal { get; set; }
        public virtual ICollection<ObraFinanciamiento> DetalleObraFinanciamiento { get; set; }
        public string Descripcion
        {
            get
            {
                return this.TechoFinanciero.Descripcion.ToUpper();
            }
        }
        public decimal GetImporteAsignado()
        {          
          return (from of in DetalleObraFinanciamiento select of).Sum(of => of.Importe);            
        }

        public decimal GetImporteDisponible()
        {
            return Importe - GetImporteAsignado();
        }

        public decimal GetImporteAsignado(int excluirObraFinanciamientoId)
        {
            return (from of in DetalleObraFinanciamiento select of).Where(of=> of.Id!=excluirObraFinanciamientoId).Sum(of => of.Importe);
        }

       
    }
}

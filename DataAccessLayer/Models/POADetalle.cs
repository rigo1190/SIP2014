using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class POADetalle:Generica
    {
        [Index("IX_Consecutivo_POAId", 1, IsUnique = true)]
        public int Consecutivo { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public int MunicipioId { get; set; }
        public string Localidad { get; set; }
        public int TipoLocalidadId { get; set; }        
        public bool EsAccion { get; set; }

        [Index("IX_Consecutivo_POAId", 2)]
        public int POAId { get; set; }
        public int AperturaProgramaticaId { get; set; }
        public int AperturaProgramaticaMetaId { get; set; }
        public int NumeroBeneficiarios { get; set; }
        public int CantidadUnidades { get; set; }
        public int Empleos { get; set; }
        public int Jornales { get; set; }
        public int SituacionObraId { get; set; }
        public enumModalidadObra ModalidadObra { get; set; }
        public decimal ImporteTotal { get; set; }
        public decimal ImporteLiberadoEjerciciosAnteriores { get; set; }
        public decimal ImportePresupuesto { get; set; }
        public int FuncionalidadId { get; set; }
        public int EjeId { get; set; }
        public int PlanSectorialId { get; set; }
        public int ModalidadId { get; set; }
        public int ProgramaId { get; set; }
        public int GrupoBeneficiarioId { get; set; }
        public int CriterioPriorizacionId { get; set; }
        public string Observaciones { get; set; }
        public virtual POA POA { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual TipoLocalidad TipoLocalidad { get; set; }
        public virtual AperturaProgramatica AperturaProgramatica { get; set; }
        public virtual AperturaProgramaticaMeta AperturaProgramaticaMeta { get; set; }
        public virtual SituacionObra SituacionObra { get; set; }
        public virtual Funcionalidad Funcionalidad { get; set; }
        public virtual Eje Eje { get; set; }
        public virtual PlanSectorial PlanSectorial { get; set; }
        public virtual Modalidad Modalidad { get; set; }
        public virtual Programa Programa { get; set; }
        public virtual GrupoBeneficiario GrupoBeneficiario { get; set; }
        public virtual CriterioPriorizacion CriterioPriorizacion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class AperturaProgramaticaMeta:Generica
    {
        [Index("IX_AperturaProgramaticaId_AperturaProgramaticaUnidadId_AperturaProgramaticaBeneficiarioId", 1, IsUnique = true)]
        public int AperturaProgramaticaId { get; set; }

        [Index("IX_AperturaProgramaticaId_AperturaProgramaticaUnidadId_AperturaProgramaticaBeneficiarioId", 2)]
        public int AperturaProgramaticaUnidadId { get; set; }

        [Index("IX_AperturaProgramaticaId_AperturaProgramaticaUnidadId_AperturaProgramaticaBeneficiarioId", 3)]
        public int AperturaProgramaticaBeneficiarioId { get; set; }
        public virtual AperturaProgramatica AperturaProgramatica{ get; set; }
        public virtual AperturaProgramaticaUnidad AperturaProgramaticaUnidad { get; set; }
        public virtual AperturaProgramaticaBeneficiario AperturaProgramaticaBeneficiario { get; set; }

        public string Descripcion 
        {
            get { return String.Format("{0} / {1}",this.AperturaProgramaticaUnidad.Nombre.ToUpper(),this.AperturaProgramaticaBeneficiario.Nombre.ToUpper()) ; }
        }


    }
}

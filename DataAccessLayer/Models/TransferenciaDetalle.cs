
using System.ComponentModel.DataAnnotations.Schema;
namespace DataAccessLayer.Models
{
    public class TransferenciaDetalle:Generica
    {
        [Index("IX_TransferenciaId_ObraId",1, IsUnique = true)]
        public int TransferenciaId { get; set; }

        [Index("IX_TransferenciaId_ObraId",2)]
        public int ObraId { get; set; }
        public decimal InversionEstimada { get; set; }
        public decimal Reduccion { get; set; }
        public decimal Ampliacion { get; set; }
        public decimal InversionModificada { get; set; }
        public string Observaciones { get; set; }
        public virtual Transferencia Transferencia { get; set; }
        public virtual Obra Obra { get; set; }

    }
   
}

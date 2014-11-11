using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TechoFinancieroBitacora:Generica
    {

        public TechoFinancieroBitacora(){
            this.detalleMovimientos = new HashSet<TechoFinancieroBitacoraMovimientos>();
        }

        public int EjercicioId { get; set; }
        public int Movimiento { get; set; }
        public  EnumTipoMovimientoTechoFinanciero  Tipo { get; set; }
        public DateTime Fecha { get; set; }

        public string Oficio { get; set; }
        public string Observaciones { get; set; }

        public virtual Ejercicio Ejercicio { get; set; }

        public virtual ICollection<TechoFinancieroBitacoraMovimientos> detalleMovimientos { get; set; }

    }


    public enum EnumTipoMovimientoTechoFinanciero
    {
        CargaInicial = 0, Transferencia = 1, NuevoFondo = 2
    }
}

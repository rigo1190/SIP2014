using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Models
{
    public class EstimacionesConceptosTMP:Generica
    {
        public int Usuario { get; set; }

        public int ContratoDeObraId { get; set; }

        public int PresupuestoContratadoId { get; set; }
        public double Numero { get; set; }
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Inciso { get; set; }

        public string Descripcion { get; set; }


        public string UnidadDeMedida { get; set; }
        public double CantidadContratada { get; set; }
        
        public double CantidadEstimadaAcumulada { get; set; }
        public double Cantidad { get; set; }


        public int Status { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string StatusNombre { get; set; }

        public int StatusFechas { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string StatusFechasNombre { get; set; }

        public decimal importe { get; set; }
        public decimal subtotal { get; set; }

        public decimal iva { get; set; }

        public decimal total { get; set; }

        public decimal amortizacion { get; set; }
        public decimal retencion5 { get; set; }
        public decimal retencion2 { get; set; }

        public decimal retencion2SyV { get; set; }

        public decimal sancion { get; set; }
        public decimal importeFinal { get; set; }

    }
}

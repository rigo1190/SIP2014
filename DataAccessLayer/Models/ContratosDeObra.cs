using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessLayer.Models
{
    public class ContratosDeObra:Generica
    {

        public ContratosDeObra()
        {
            this.detallePresupuesto = new HashSet<PresupuestosContratados>();
            this.detalleEstimaciones = new HashSet<Estimaciones>();
        }

        public int ObraId { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NumeroDeContrato { get; set; }

        public DateTime FechaDelContrato { get; set; }
        public decimal Subtotal { get; set; }
        public decimal IVA { get; set; }
        public decimal Total { get; set; }

        public double PorcentajeDeAnticipo { get; set; }

        public bool Descontar5AlMillar { get; set; }
        public bool Descontar2AlMillar { get; set; }

        public bool Descontar2AlMillarSV { get; set; }

        public DateTime FechaDeInicio { get; set; }
        public DateTime FechaDeTermino { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string RFCcontratista { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string ClaveContratista { get; set; }

        [StringLength(255, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string RazonSocialContratista { get; set; }

        [StringLength(255, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string ClavePresupuestal { get; set; }


        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NumeroFianza { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NumeroFianzaCumplimiento { get; set; }

 

        [StringLength(255, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string NombreAfianzadora { get; set; }

        public virtual Obra Obra { get; set; }

        public virtual ICollection<PresupuestosContratados> detallePresupuesto { get; set; }

        public virtual ICollection<Estimaciones> detalleEstimaciones { get; set; }

    }

}

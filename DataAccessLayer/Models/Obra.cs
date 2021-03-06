﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class Obra:Generica
    {
        public Obra() 
        {
            this.DetalleFinanciamientos = new HashSet<ObraFinanciamiento>();
        }

        [Index("IX_Consecutivo_POAId", 1, IsUnique = true)]
        public int Consecutivo { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50, ErrorMessage = "El campo {0} debe contener un máximo de {1} caracteres")]
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public int MunicipioId { get; set; }       
        public int LocalidadId { get; set; }        
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }  

        [Index("IX_Consecutivo_POAId", 2)]
        public int POAId { get; set; }
        public int POADetalleId { get; set; }            
        public int AperturaProgramaticaId { get; set; }
        public int? AperturaProgramaticaMetaId { get; set; }
        public int? AperturaProgramaticaUnidadId { get; set; }
        public int NumeroBeneficiarios { get; set; }
        public int CantidadUnidades { get; set; }
        public int Empleos { get; set; }
        public int Jornales { get; set; }
        public int SituacionObraId { get; set; }       
        public enumModalidadObra? ModalidadObra { get; set; }
        public string NumeroAnterior { get; set; }
        public decimal ImporteLiberadoEjerciciosAnteriores { get; set; } 
        public int? FuncionalidadId { get; set; }
        public int? EjeId { get; set; }
        public int? PlanSectorialId { get; set; }
        public int? ModalidadId { get; set; }
        public int? ProgramaId { get; set; }
        public int? GrupoBeneficiarioId { get; set; }
        public int CriterioPriorizacionId { get; set; }
        public string Convenio { get; set; }
        public string Observaciones { get; set; }
        public int? ObraAnteriorId { get; set; }
        public int? ObraOrigenId { get; set; }


        public int StatusControlFinanciero { get; set; }
        
        
        
        public virtual POA POA { get; set; }
        public virtual POADetalle POADetalle { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual Localidad Localidad { get; set; }       
        public virtual AperturaProgramatica AperturaProgramatica { get; set; }
        public virtual AperturaProgramaticaMeta AperturaProgramaticaMeta { get; set; }
        public virtual AperturaProgramaticaUnidad AperturaProgramaticaUnidad { get; set; }
        public virtual SituacionObra SituacionObra { get; set; }
        public virtual Funcionalidad Funcionalidad { get; set; }
        public virtual Eje Eje { get; set; }
        public virtual PlanSectorial PlanSectorial { get; set; }
        public virtual Modalidad Modalidad { get; set; }
        public virtual Programa Programa { get; set; }
        public virtual GrupoBeneficiario GrupoBeneficiario { get; set; }
        public virtual CriterioPriorizacion CriterioPriorizacion { get; set; }
        public virtual Obra ObraAnterior { get; set; }
        public virtual Obra ObraOrigen { get; set; }
        public virtual ICollection<ObraFinanciamiento> DetalleFinanciamientos { get; set; }
        public virtual ICollection<Obra> DetalleObrasDependientes { get; set; }




        public decimal GetImporteLiberadoEjerciciosAnteriores()
        {
            if (this.POADetalle.Extemporanea)
            {
                return this.ImporteLiberadoEjerciciosAnteriores;
            }
            else 
            {
                return this.POADetalle.ImporteLiberadoEjerciciosAnteriores;
            }

            return 0;
        }
        public decimal GetImporteAsignado()
        {
            return (from of in DetalleFinanciamientos select of).Sum(of => of.Importe);
        }
        public decimal GetCostoTotal()
        {
            return GetImporteLiberadoEjerciciosAnteriores() + GetImporteAsignado();
        }
       

    }

    public enum enumModalidadObra 
    {
        Contrato=1,
        [Display(Name="Administración directa")]
        AdministracionDirecta=2,
        [Display(Name ="Forma mixta")]
        Mixta = 3
    }
}

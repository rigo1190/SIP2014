﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace BusinessLogicLayer
{
    public class UnitOfWork : IDisposable
    {
        private Contexto contexto;

        public Contexto Contexto
        {
            get { return contexto; }            
        }
        private int usuarioId;
        private List<String> errors = new List<String>();
        private IBusinessLogic<Usuario> usuarioBusinessLogic;
        private IBusinessLogic<Rol> rolBusinessLogic;        
        private IBusinessLogic<Permiso> permisoBusinessLogic;
        private IBusinessLogic<OpcionSistema> opcionSistemaBusinessLogic;
        private IBusinessLogic<UsuarioRol> usuarioRolBusinessLogic;
        private IBusinessLogic<UsuarioUnidadPresupuestal> usuarioUnidadPresupuestalBusinessLogic;
        private IBusinessLogic<Ejercicio> ejercicioBusinessLogic;
        private IBusinessLogic<Municipio> municipioBusinessLogic;
        private IBusinessLogic<Localidad> localidadBusinessLogic;
        private IBusinessLogic<Fondo> fondoBusinessLogic;
        private IBusinessLogic<TipoFondo> tipoFondoBusinessLogic;
        private IBusinessLogic<FondoLineamientos> fondolineamientosBL;
        private IBusinessLogic<ModalidadFinanciamiento> modalidadFinanciamientoBusinessLogic;
        private IBusinessLogic<Año> añoBusinessLogic;
        private IBusinessLogic<Financiamiento> financiamientoBusinessLogic;
        private IBusinessLogic<TipoLocalidad> tipoLocalidadBusinessLogic;
        private IBusinessLogic<SituacionObra> situacionObraBusinessLogic;
        private IBusinessLogic<UnidadPresupuestal> unidadPresupuestalBusinessLogic;
        private IBusinessLogic<Sector> sectorBusinessLogic;
        private IBusinessLogic<POA> poaBusinessLogic;
        private IBusinessLogic<POADetalle> poaDetalleBusinessLogic;
        private IBusinessLogic<Plantilla> plantillaBusinessLogic;
        private IBusinessLogic<PlantillaDetalle> plantillaDetalleBusinessLogic;
        private IBusinessLogic<AperturaProgramatica> aperturaProgramaticaBusinessLogic;
        private IBusinessLogic<AperturaProgramaticaTipo> aperturaProgramaticaTipoBusinessLogic;
        private IBusinessLogic<AperturaProgramaticaMeta> aperturaProgramaticaMetaBusinessLogic;
        private IBusinessLogic<AperturaProgramaticaBeneficiario> aperturaProgramaticaBeneficiarioBusinessLogic;
        private IBusinessLogic<AperturaProgramaticaUnidad> aperturaProgramaticaUnidadBusinessLogic;
        private IBusinessLogic<Obra> obraBusinessLogic;
        private IBusinessLogic<ObraFinanciamiento> obraFinanciamientoBusinessLogic;
        private IBusinessLogic<ObraPlantilla> obraPlantillaBusinessLogic;
        private IBusinessLogic<ObraPlantillaDetalle> obraPlantillaDetalleBusinessLogic;
        private IBusinessLogic<POAPlantilla> poaPlantillaBusinessLogic; //Agregado por Rigoberto TS 25/09/2014
        private IBusinessLogic<POAPlantillaDetalle> poaPlantillaDetalleBusinessLogic;//Agregado por Rigoberto TS 25/09/2014
        private IBusinessLogic<Funcionalidad> funcionalidadBusinessLogic;
        private IBusinessLogic<Eje> ejeBusinessLogic;
        private IBusinessLogic<PlanSectorial> planSectorialBusinessLogic;
        private IBusinessLogic<Modalidad> modalidadBusinessLogic;
        private IBusinessLogic<Programa> programaBusinessLogic;
        private IBusinessLogic<GrupoBeneficiario> grupoBeneficiarioBusinessLogic;
        private IBusinessLogic<CriterioPriorizacion> criterioPriorizacionBusinessLogic;

        private IBusinessLogic<TechoFinancieroStatus> techofinancierostatusbussineslogic;
        private IBusinessLogic<TechoFinanciero> techoFinancieroBusinessLogic;
        private IBusinessLogic<TechoFinancieroUnidadPresupuestal> techoFinancieroUnidadPresupuestalBusinessLogic;

        private IBusinessLogic<TechoFinancieroBitacora> techofinancierobitacoraBL;
        private IBusinessLogic<TechoFinancieroBitacoraMovimientos> techofinancierobitacoramovimientosBL;
        private IBusinessLogic<TechoFinancieroTMPtransferencias> techofinancierotmptransferenciasBL;

        private IBusinessLogic<Firmas> firmasBL;

        private IBusinessLogic<ContratosDeObra> contratosdeobraBL;
        private IBusinessLogic<PresupuestosContratados> presupuestosscontratadosBL;
        private IBusinessLogic<Estimaciones> estimacionesBL;
        private IBusinessLogic<EstimacionesConceptos> estimacionesconceptosBL;
        private IBusinessLogic<EstimacionesConceptosTMP> estimacionesconceptostmpBL;

        private IBusinessLogic<ProgramasDeObras> programasdeobrasBL;
        private IBusinessLogic<ProgramasDeObrasTMP> programasdeobrastmpBL;
        private IBusinessLogic<EstimacionesProgramadas> estimacionesprogramadasBL;
        private IBusinessLogic<EstimacionesProgramadasConceptos> estimacionesprogramadasconceptosBL;
		
		private IBusinessLogic<FundamentacionPlantilla> fundamentacionPlantillaBL;
        private IBusinessLogic<RubroFundamentacion> rubroFundamentacionBL;
        private IBusinessLogic<POAPlantillaDetalleDoctos> poaPlantillaDetalleDoctosBL;
        private IBusinessLogic<Transferencia> transferenciaBusinessLogic;
        private IBusinessLogic<TransferenciaDetalle> transferenciaDetalleBusinessLogic;


        public UnitOfWork()
        {
            this.contexto = new Contexto();
        }

        public UnitOfWork(string usuarioId)
        {           
            this.usuarioId = Utilerias.StrToInt(usuarioId);
            this.contexto = new Contexto();
        }

        public IBusinessLogic<POAPlantillaDetalleDoctos> POAPlantillaDetalleDoctosBL
        {
            get
            {
                if (this.poaPlantillaDetalleDoctosBL == null)
                {
                    this.poaPlantillaDetalleDoctosBL = new GenericBusinessLogic<POAPlantillaDetalleDoctos>(contexto);
                }

                return poaPlantillaDetalleDoctosBL;
            }
        }

        public IBusinessLogic<RubroFundamentacion> RubroFundamentacionBL
        {
            get
            {
                if (this.rubroFundamentacionBL == null)
                {
                    this.rubroFundamentacionBL = new GenericBusinessLogic<RubroFundamentacion>(contexto);
                }

                return rubroFundamentacionBL;
            }
        }
        public IBusinessLogic<FundamentacionPlantilla> FundamentacionPlantillaBL
        {
            get
            {
                if (this.fundamentacionPlantillaBL == null)
                {
                    this.fundamentacionPlantillaBL = new GenericBusinessLogic<FundamentacionPlantilla>(contexto);
                }

                return fundamentacionPlantillaBL;
            }
        }

        public IBusinessLogic<Usuario> UsuarioBusinessLogic
        {
            get
            {
                if (this.usuarioBusinessLogic == null)
                {
                    this.usuarioBusinessLogic = new GenericBusinessLogic<Usuario>(contexto);
                }

                return usuarioBusinessLogic;
            }
        }

        public IBusinessLogic<Rol> RolBusinessLogic
        {
            get
            {
                if (this.rolBusinessLogic == null)
                {
                    this.rolBusinessLogic = new GenericBusinessLogic<Rol>(contexto);
                }

                return rolBusinessLogic;
            }
        }

        public IBusinessLogic<UsuarioRol> UsuarioRolBusinessLogic
        {
            get
            {
                if (this.usuarioRolBusinessLogic == null)
                {
                    this.usuarioRolBusinessLogic = new GenericBusinessLogic<UsuarioRol>(contexto);
                }

                return usuarioRolBusinessLogic;
            }
        }

        public IBusinessLogic<Permiso> PermisoBusinessLogic
        {
            get
            {
                if (this.permisoBusinessLogic == null)
                {
                    this.permisoBusinessLogic = new GenericBusinessLogic<Permiso>(contexto);
                }

                return permisoBusinessLogic;
            }
        }

        public IBusinessLogic<OpcionSistema> OpcionSistemaBusinessLogic
        {
            get
            {
                if (this.opcionSistemaBusinessLogic == null)
                {
                    this.opcionSistemaBusinessLogic = new GenericBusinessLogic<OpcionSistema>(contexto);
                }

                return opcionSistemaBusinessLogic;
            }
        }

        public IBusinessLogic<UsuarioUnidadPresupuestal> UsuarioUnidadPresupuestalBusinessLogic
        {
            get
            {
                if (this.usuarioUnidadPresupuestalBusinessLogic == null)
                {
                    this.usuarioUnidadPresupuestalBusinessLogic = new GenericBusinessLogic<UsuarioUnidadPresupuestal>(contexto);
                }

                return usuarioUnidadPresupuestalBusinessLogic;
            }
        }

        public IBusinessLogic<Ejercicio> EjercicioBusinessLogic
        {
            get
            {
                if (this.ejercicioBusinessLogic == null)
                {
                    this.ejercicioBusinessLogic = new GenericBusinessLogic<Ejercicio>(contexto);
                }

                return ejercicioBusinessLogic;
            }
        }


        public IBusinessLogic<Municipio> MunicipioBusinessLogic
        {
            get
            {
                if (this.municipioBusinessLogic == null)
                {
                    this.municipioBusinessLogic = new GenericBusinessLogic<Municipio>(contexto);
                }

                return municipioBusinessLogic;
            }
        }

        public IBusinessLogic<Localidad> LocalidadBusinessLogic
        {
            get
            {
                if (this.localidadBusinessLogic == null)
                {
                    this.localidadBusinessLogic = new GenericBusinessLogic<Localidad>(contexto);
                }

                return localidadBusinessLogic;
            }
        }

        public IBusinessLogic<Fondo> FondoBusinessLogic
        {
            get
            {
                if (this.fondoBusinessLogic == null)
                {
                    this.fondoBusinessLogic = new GenericBusinessLogic<Fondo>(contexto);
                }

                return fondoBusinessLogic;
            }
        }

        public IBusinessLogic<TipoFondo> TipoFondoBusinessLogic
        {
            get
            {
                if (this.tipoFondoBusinessLogic == null)
                {
                    this.tipoFondoBusinessLogic = new GenericBusinessLogic<TipoFondo>(contexto);
                }

                return tipoFondoBusinessLogic;
            }
        }


        public IBusinessLogic<FondoLineamientos> FondoLineamientosBL
        {
            get
            {
                if (this.fondolineamientosBL == null)
                {
                    this.fondolineamientosBL = new GenericBusinessLogic<FondoLineamientos>(contexto);
                }

                return this.fondolineamientosBL;
            }
        }
        public IBusinessLogic<ModalidadFinanciamiento> ModalidadFinanciamientoBusinessLogic
        {
            get
            {
                if (this.modalidadFinanciamientoBusinessLogic == null)
                {
                    this.modalidadFinanciamientoBusinessLogic = new GenericBusinessLogic<ModalidadFinanciamiento>(contexto);
                }

                return modalidadFinanciamientoBusinessLogic;
            }
        }

        public IBusinessLogic<Año> AñoBusinessLogic
        {
            get
            {
                if (this.añoBusinessLogic == null)
                {
                    this.añoBusinessLogic = new GenericBusinessLogic<Año>(contexto);
                }

                return añoBusinessLogic;
            }
        }

        public IBusinessLogic<Financiamiento> FinanciamientoBusinessLogic
        {
            get
            {
                if (this.financiamientoBusinessLogic == null)
                {
                    this.financiamientoBusinessLogic = new GenericBusinessLogic<Financiamiento>(contexto);
                }

                return financiamientoBusinessLogic;
            }
        }

        public IBusinessLogic<TipoLocalidad> TipoLocalidadBusinessLogic
        {
            get
            {
                if (this.tipoLocalidadBusinessLogic == null)
                {
                    this.tipoLocalidadBusinessLogic = new GenericBusinessLogic<TipoLocalidad>(contexto);
                }

                return tipoLocalidadBusinessLogic;
            }
        }

        public IBusinessLogic<SituacionObra> SituacionObraBusinessLogic
        {
            get
            {
                if (this.situacionObraBusinessLogic == null)
                {
                    this.situacionObraBusinessLogic = new GenericBusinessLogic<SituacionObra>(contexto);
                }

                return situacionObraBusinessLogic;
            }
        }

        public IBusinessLogic<UnidadPresupuestal> UnidadPresupuestalBusinessLogic
        {
            get
            {
                if (this.unidadPresupuestalBusinessLogic == null)
                {
                    this.unidadPresupuestalBusinessLogic = new GenericBusinessLogic<UnidadPresupuestal>(contexto);
                }

                return unidadPresupuestalBusinessLogic;
            }
        }

        public IBusinessLogic<Sector> SectorBusinessLogic
        {
            get
            {
                if (this.sectorBusinessLogic == null)
                {
                    this.sectorBusinessLogic = new GenericBusinessLogic<Sector>(contexto);
                }

                return sectorBusinessLogic;
            }
        }

        public IBusinessLogic<POA> POABusinessLogic
        {
            get
            {
                if (this.poaBusinessLogic == null)
                {
                    this.poaBusinessLogic = new GenericBusinessLogic<POA>(contexto);
                }

                return poaBusinessLogic;
            }
        }

        public IBusinessLogic<POADetalle> POADetalleBusinessLogic
        {
            get
            {
                if (this.poaDetalleBusinessLogic == null)
                {
                    this.poaDetalleBusinessLogic = new GenericBusinessLogic<POADetalle>(contexto);
                }

                return poaDetalleBusinessLogic;
            }
        }

        public IBusinessLogic<Plantilla> PlantillaBusinessLogic
        {
            get
            {
                if (this.plantillaBusinessLogic == null)
                {
                    this.plantillaBusinessLogic = new GenericBusinessLogic<Plantilla>(contexto);
                }

                return plantillaBusinessLogic;
            }
        }

        public IBusinessLogic<PlantillaDetalle> PlantillaDetalleBusinessLogic
        {
            get
            {
                if (this.plantillaDetalleBusinessLogic == null)
                {
                    this.plantillaDetalleBusinessLogic = new GenericBusinessLogic<PlantillaDetalle>(contexto);
                }

                return plantillaDetalleBusinessLogic;
            }
        }

        public IBusinessLogic<AperturaProgramatica> AperturaProgramaticaBusinessLogic
        {
            get
            {
                if (this.aperturaProgramaticaBusinessLogic == null)
                {
                    this.aperturaProgramaticaBusinessLogic = new GenericBusinessLogic<AperturaProgramatica>(contexto);
                }

                return aperturaProgramaticaBusinessLogic;
            }
        }

        public IBusinessLogic<AperturaProgramaticaTipo> AperturaProgramaticaTipoBusinessLogic
        {
            get
            {
                if (this.aperturaProgramaticaTipoBusinessLogic == null)
                {
                    this.aperturaProgramaticaTipoBusinessLogic = new GenericBusinessLogic<AperturaProgramaticaTipo>(contexto);
                }

                return aperturaProgramaticaTipoBusinessLogic;
            }
        }

        public IBusinessLogic<AperturaProgramaticaMeta> AperturaProgramaticaMetaBusinessLogic
        {
            get
            {
                if (this.aperturaProgramaticaMetaBusinessLogic == null)
                {
                    this.aperturaProgramaticaMetaBusinessLogic = new GenericBusinessLogic<AperturaProgramaticaMeta>(contexto);
                }

                return aperturaProgramaticaMetaBusinessLogic;
            }
        }

        public IBusinessLogic<AperturaProgramaticaBeneficiario> AperturaProgramaticaBeneficiarioBusinessLogic
        {
            get
            {
                if (this.aperturaProgramaticaBeneficiarioBusinessLogic == null)
                {
                    this.aperturaProgramaticaBeneficiarioBusinessLogic = new GenericBusinessLogic<AperturaProgramaticaBeneficiario>(contexto);
                }

                return aperturaProgramaticaBeneficiarioBusinessLogic;
            }
        }

        public IBusinessLogic<AperturaProgramaticaUnidad> AperturaProgramaticaUnidadBusinessLogic
        {
            get
            {
                if (this.aperturaProgramaticaUnidadBusinessLogic == null)
                {
                    this.aperturaProgramaticaUnidadBusinessLogic = new GenericBusinessLogic<AperturaProgramaticaUnidad>(contexto);
                }

                return aperturaProgramaticaUnidadBusinessLogic;
            }
        }

        public IBusinessLogic<Obra> ObraBusinessLogic
        {
            get
            {
                if (this.obraBusinessLogic == null)
                {
                    this.obraBusinessLogic = new GenericBusinessLogic<Obra>(contexto);
                }

                return obraBusinessLogic;
            }
        }

        public IBusinessLogic<ObraFinanciamiento> ObraFinanciamientoBusinessLogic
        {
            get
            {
                if (this.obraFinanciamientoBusinessLogic == null)
                {
                    this.obraFinanciamientoBusinessLogic = new GenericBusinessLogic<ObraFinanciamiento>(contexto);
                }

                return obraFinanciamientoBusinessLogic;
            }
        }

        public IBusinessLogic<ObraPlantilla> ObraPlantillaBusinessLogic
        {
            get
            {
                if (this.obraPlantillaBusinessLogic == null)
                {
                    this.obraPlantillaBusinessLogic = new GenericBusinessLogic<ObraPlantilla>(contexto);
                }

                return obraPlantillaBusinessLogic;
            }
        }

        public IBusinessLogic<ObraPlantillaDetalle> ObraPlantillaDetalleBusinessLogic
        {
            get
            {
                if (this.obraPlantillaDetalleBusinessLogic == null)
                {
                    this.obraPlantillaDetalleBusinessLogic = new GenericBusinessLogic<ObraPlantillaDetalle>(contexto);
                }

                return obraPlantillaDetalleBusinessLogic;
            }
        }


        /// <summary>
        /// Agregado por Rigoberto TS 
        /// 25/09/2014
        /// </summary>
        public IBusinessLogic<POAPlantilla> POAPlantillaBusinessLogic
        {
            get
            {
                if (this.poaPlantillaBusinessLogic == null)
                {
                    this.poaPlantillaBusinessLogic = new GenericBusinessLogic<POAPlantilla>(contexto);
                }

                return poaPlantillaBusinessLogic;
            }
        }

        /// <summary>
        /// Agregado por Rigoberto TS 
        /// 25/09/2014
        /// </summary>
        public IBusinessLogic<POAPlantillaDetalle> POAPlantillaDetalleBusinessLogic
        {
            get
            {
                if (this.poaPlantillaDetalleBusinessLogic == null)
                {
                    this.poaPlantillaDetalleBusinessLogic = new GenericBusinessLogic<POAPlantillaDetalle>(contexto);
                }

                return poaPlantillaDetalleBusinessLogic;
            }
        }

        public IBusinessLogic<Funcionalidad> FuncionalidadBusinessLogic
        {
            get
            {
                if (this.funcionalidadBusinessLogic == null)
                {
                    this.funcionalidadBusinessLogic = new GenericBusinessLogic<Funcionalidad>(contexto);
                }

                return funcionalidadBusinessLogic;
            }
        }

        public IBusinessLogic<Eje> EjeBusinessLogic
        {
            get
            {
                if (this.ejeBusinessLogic == null)
                {
                    this.ejeBusinessLogic = new GenericBusinessLogic<Eje>(contexto);
                }

                return ejeBusinessLogic;
            }
        }

        public IBusinessLogic<PlanSectorial> PlanSectorialBusinessLogic
        {
            get
            {
                if (this.planSectorialBusinessLogic == null)
                {
                    this.planSectorialBusinessLogic = new GenericBusinessLogic<PlanSectorial>(contexto);
                }

                return planSectorialBusinessLogic;
            }
        }

        public IBusinessLogic<Modalidad> ModalidadBusinessLogic
        {
            get
            {
                if (this.modalidadBusinessLogic == null)
                {
                    this.modalidadBusinessLogic = new GenericBusinessLogic<Modalidad>(contexto);
                }

                return modalidadBusinessLogic;
            }
        }

        public IBusinessLogic<Programa> ProgramaBusinessLogic
        {
            get
            {
                if (this.programaBusinessLogic == null)
                {
                    this.programaBusinessLogic = new GenericBusinessLogic<Programa>(contexto);
                }

                return programaBusinessLogic;
            }
        }

        public IBusinessLogic<GrupoBeneficiario> GrupoBeneficiarioBusinessLogic
        {
            get
            {
                if (this.grupoBeneficiarioBusinessLogic == null)
                {
                    this.grupoBeneficiarioBusinessLogic = new GenericBusinessLogic<GrupoBeneficiario>(contexto);
                }

                return grupoBeneficiarioBusinessLogic;
            }
        }

        public IBusinessLogic<CriterioPriorizacion> CriterioPriorizacionBusinessLogic
        {
            get
            {
                if (this.criterioPriorizacionBusinessLogic == null)
                {
                    this.criterioPriorizacionBusinessLogic = new GenericBusinessLogic<CriterioPriorizacion>(contexto);
                }

                return criterioPriorizacionBusinessLogic;
            }
        }


        public IBusinessLogic<TechoFinancieroStatus> TechoFinancieroStatusBusinessLogic
        {
            get
            {
                if (this.techofinancierostatusbussineslogic == null)
                {
                    this.techofinancierostatusbussineslogic = new GenericBusinessLogic<TechoFinancieroStatus>(contexto);
                }
                return techofinancierostatusbussineslogic;
            }
        }

        public IBusinessLogic<TechoFinanciero> TechoFinancieroBusinessLogic
        {
            get
            {
                if (this.techoFinancieroBusinessLogic == null)
                {
                    this.techoFinancieroBusinessLogic = new GenericBusinessLogic<TechoFinanciero>(contexto);
                }
                return techoFinancieroBusinessLogic;
            }
        }

        public IBusinessLogic<TechoFinancieroUnidadPresupuestal> TechoFinancieroUnidadPresuestalBusinessLogic
        {
            get
            {
                if (this.techoFinancieroUnidadPresupuestalBusinessLogic == null)
                {
                    this.techoFinancieroUnidadPresupuestalBusinessLogic = new GenericBusinessLogic<TechoFinancieroUnidadPresupuestal>(contexto);
                }
                return techoFinancieroUnidadPresupuestalBusinessLogic;
            }
        }


        public IBusinessLogic<TechoFinancieroBitacora> TechoFinancieroBitacoraBL
        {
            get
            {
                if (this.techofinancierobitacoraBL == null)
                {
                    this.techofinancierobitacoraBL = new GenericBusinessLogic<TechoFinancieroBitacora>(contexto);
                }
                return techofinancierobitacoraBL;
            }
        }


        public IBusinessLogic< TechoFinancieroBitacoraMovimientos> TechoFinancieroBitacoraMovimientosBL
        {
            get
            {
                if (this.techofinancierobitacoramovimientosBL == null)
                {
                    this.techofinancierobitacoramovimientosBL = new GenericBusinessLogic<TechoFinancieroBitacoraMovimientos>(contexto);
                }
                return this.techofinancierobitacoramovimientosBL;
            }
        }


        public IBusinessLogic<TechoFinancieroTMPtransferencias> TechoFinancieroTMPtransferenciasBL
        {
            get
            {
                if (this.techofinancierotmptransferenciasBL == null)
                {
                    this.techofinancierotmptransferenciasBL = new GenericBusinessLogic<TechoFinancieroTMPtransferencias>(contexto);
                }
                return this.techofinancierotmptransferenciasBL;
            }
        }


        public IBusinessLogic<Firmas> FirmasBL
        {
            get
            {
                if (this.firmasBL == null)
                {
                    this.firmasBL = new GenericBusinessLogic<Firmas>(contexto);
                }
                return this.firmasBL;
            }
        }

        public IBusinessLogic<ContratosDeObra> ContratosDeObraBL
        {
            get
            {
                if (this.contratosdeobraBL == null)
                {
                    this.contratosdeobraBL = new GenericBusinessLogic<ContratosDeObra>(contexto);
                         
                }
                return this.contratosdeobraBL;
            }
        }



        

        public IBusinessLogic<PresupuestosContratados> PresupuestosContratadosBL
        { 
            get {
                if (this.presupuestosscontratadosBL == null)
                {
                    this.presupuestosscontratadosBL = new GenericBusinessLogic<PresupuestosContratados>(contexto);
                         
                }
                return this.presupuestosscontratadosBL;
            } 
        }


        public IBusinessLogic<Estimaciones> EstimacionesBL
        {
            get
            {
                if (this.estimacionesBL == null)
                {
                    this.estimacionesBL = new GenericBusinessLogic<Estimaciones>(contexto);
                }
                return this.estimacionesBL;
            }
        }


        public IBusinessLogic<EstimacionesConceptos> EstimacionesConceptosBL
        {
            get{
                if (this.estimacionesconceptosBL == null)
                {
                    this.estimacionesconceptosBL = new GenericBusinessLogic<EstimacionesConceptos>(contexto);
                }
                return this.estimacionesconceptosBL;
            }
        }

        public IBusinessLogic<EstimacionesConceptosTMP> EstimacionesConceptosTMPBL
        {
            get
            {
                if (this.estimacionesconceptostmpBL == null)
                {
                    this.estimacionesconceptostmpBL = new GenericBusinessLogic<EstimacionesConceptosTMP>(contexto);
                }
                return this.estimacionesconceptostmpBL;
            }
        }

        public IBusinessLogic<ProgramasDeObras> ProgramasDeObrasBL
        {
            get
            {
                if (this.programasdeobrasBL == null)
                {
                    this.programasdeobrasBL = new GenericBusinessLogic<ProgramasDeObras>(contexto);
                }

                return this.programasdeobrasBL;
            }
        }


        public IBusinessLogic<ProgramasDeObrasTMP> ProgramasDeObraTMPBL
        {
            get
            {
                if (this.programasdeobrastmpBL == null)
                {
                    this.programasdeobrastmpBL = new GenericBusinessLogic<ProgramasDeObrasTMP>(contexto);
                }
                return this.programasdeobrastmpBL;
            }
        }
        
        public IBusinessLogic<EstimacionesProgramadas> EstimacionesProgramadasBL
        {
            get
            {
                if (this.estimacionesprogramadasBL == null)
                {
                    this.estimacionesprogramadasBL = new GenericBusinessLogic<EstimacionesProgramadas>(contexto);
                }
                return this.estimacionesprogramadasBL;
            }
        }

        public IBusinessLogic<EstimacionesProgramadasConceptos> EstimacionesProgramadasConceptosBL
        {
            get
            {
                if (this.estimacionesprogramadasconceptosBL == null)
                {
                    this.estimacionesprogramadasconceptosBL = new GenericBusinessLogic<EstimacionesProgramadasConceptos>(contexto);
                }
                return this.estimacionesprogramadasconceptosBL;
            }
        }

        public IBusinessLogic<Transferencia> TransferenciasBusinessLogic
        {
            get
            {
                if (this.transferenciaBusinessLogic == null)
                {
                    this.transferenciaBusinessLogic = new GenericBusinessLogic<Transferencia>(contexto);
                }

                return this.transferenciaBusinessLogic;
            }
        }

        public IBusinessLogic<TransferenciaDetalle> TransferenciaDetalleBusinessLogic
        {
            get
            {
                if (this.transferenciaDetalleBusinessLogic == null)
                {
                    this.transferenciaDetalleBusinessLogic = new GenericBusinessLogic<TransferenciaDetalle>(contexto);
                }

                return this.transferenciaDetalleBusinessLogic;
            }
        }
        
     
        public void SaveChanges()
        {
            try
            {
                errors.Clear();
                contexto.SaveChanges(usuarioId);
            }
            catch (DbEntityValidationException ex)
            {

                this.RollBack();

                foreach (var item in ex.EntityValidationErrors)
                {

                    errors.Add(String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors", item.Entry.Entity.GetType().Name, item.Entry.State));

                    foreach (var error in item.ValidationErrors)
                    {
                        errors.Add(String.Format("Propiedad: \"{0}\", Error: \"{1}\"", error.PropertyName, error.ErrorMessage));
                    }


                }

            }
            catch (DbUpdateException ex)
            {
                this.RollBack();
                errors.Add(String.Format("{0}", ex.InnerException.InnerException.Message));
            }
            catch (System.InvalidOperationException ex)
            {
                this.RollBack();
                errors.Add(String.Format("{0}", ex.Message));
            }
            catch (Exception ex)
            {
                this.RollBack();
                errors.Add(String.Format("{0}\n{1}", ex.Message, ex.InnerException.Message));
            }
            
        }

        public void RollBack()
        {

            var changedEntries = contexto.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged);

            // ojo....... respetar el orden en que se evalua el atributo State
            // primero Deleted,segundo Modified, al final y nada mas que al final Added
            // debido a que una vez que asignamos el estado Detached a una entidad,
            // desasociamos a esta del contexto y el filtro changedEntries.Where(etc....) genera un error

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }                       

        }

        public List<String> Errors 
        {
            get 
            {
                return errors;
            }
        }

        public object GetResult() 
        {
            if (errors.Count == 0) 
            {
                return new { OK = true };
            }

            return new  { OK = false, Errors = errors };
        }
        
        
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    contexto.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
    
}

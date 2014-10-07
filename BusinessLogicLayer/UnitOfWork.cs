using DataAccessLayer.Models;
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
        internal Contexto contexto;
        private List<String> errors = new List<String>();
        private IBusinessLogic<Usuario> usuarioBusinessLogic;
        private IBusinessLogic<UsuarioUnidadPresupuestal> usuarioUnidadPresupuestalBusinessLogic;
        private IBusinessLogic<Ejercicio> ejercicioBusinessLogic;
        private IBusinessLogic<Municipio> municipioBusinessLogic;
        private IBusinessLogic<Fondo> fondoBusinessLogic;
        private IBusinessLogic<ModalidadFinanciamiento> modalidadFinanciamientoBusinessLogic;
        private IBusinessLogic<Año> añoBusinessLogic;
        private IBusinessLogic<Financiamiento> financiamientoBusinessLogic;
        private IBusinessLogic<TipoLocalidad> tipoLocalidadBusinessLogic;
        private IBusinessLogic<SituacionObra> situacionObraBusinessLogic;
        private IBusinessLogic<UnidadPresupuestal> unidadPresupuestalBusinessLogic;
        private IBusinessLogic<POA> poaBusinessLogic;
        private IBusinessLogic<POADetalle> poaDetalleBusinessLogic;
        private IBusinessLogic<Plantilla> plantillaBusinessLogic;
        private IBusinessLogic<PlantillaDetalle> plantillaDetalleBusinessLogic;
        private IBusinessLogic<AperturaProgramatica> aperturaProgramaticaBusinessLogic;
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
       
        public UnitOfWork()
        {
            this.contexto = new Contexto();
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

     
        public void SaveChanges()
        {
            try
            {
                errors.Clear();
                contexto.SaveChanges();
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

        public List<String> Errors 
        {
            get 
            {
                return errors;
            }
        }


        public void RollBack()
        {
           
            var changedEntries = contexto.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged);

            #region < Pendiente revisar, esto podria cancelar toda una sesión de trabajo >

            //foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            //{
            //    entry.CurrentValues.SetValues(entry.OriginalValues);
            //    entry.State = EntityState.Unchanged;
            //}

            //foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            //{
            //    entry.State = EntityState.Detached;
            //} 

            #endregion

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
            
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

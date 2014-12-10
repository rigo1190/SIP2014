using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Contexto : DbContext
    {
        private int userId;
       
        public Contexto()
            : base("SIP")
        {
            System.Diagnostics.Debug.Print(Database.Connection.ConnectionString);
        }            

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<UsuarioUnidadPresupuestal>()
               .HasRequired(c => c.Usuario)
               .WithMany(d => d.DetalleUnidadesPresupuestales)
               .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<UsuarioRol>()
              .HasRequired(c => c.Usuario)
              .WithMany(d => d.DetalleRoles)
              .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<POADetalle>()
              .HasOptional(c => c.ObraAnterior)
              .WithMany()
              .HasForeignKey(c => c.ObraAnteriorId);

            modelBuilder.Entity<Obra>()
              .HasOptional(c => c.ObraOrigen)
              .WithMany(d => d.DetalleObrasDependientes)
              .HasForeignKey(c => c.ObraOrigenId);

            modelBuilder.Entity<ObraFinanciamiento>()
              .HasRequired(u => u.Obra)
              .WithMany(u => u.DetalleFinanciamientos)
              .HasForeignKey(u => u.ObraId)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<PlantillaDetalle>()
              .HasRequired(u => u.Plantilla)
              .WithMany(u => u.DetallePreguntas)
              .HasForeignKey(u => u.PlantillaId)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<ObraPlantillaDetalle>()
              .HasRequired(u => u.ObraPlantilla)
              .WithMany(u => u.Detalles)
              .HasForeignKey(u => u.ObraPlantillaId)
              .WillCascadeOnDelete(true);

            modelBuilder.Entity<AperturaProgramaticaMeta>()
             .HasRequired(u => u.AperturaProgramatica)
             .WithMany(u => u.DetalleMetas)
             .HasForeignKey(u => u.AperturaProgramaticaId)
             .WillCascadeOnDelete(true);         
            
        }

        public int SaveChanges(int userId)
        {

            var creados = this.ChangeTracker.Entries()
                            .Where(e => e.State == System.Data.Entity.EntityState.Added)
                            .Select(e => e.Entity).OfType<Generica>().ToList();

            foreach (var item in creados)
            {
                item.CreatedAt = DateTime.Now;
                item.CreatedById = (userId==0)?null:(int?)userId;
            }

            var modificados = this.ChangeTracker.Entries()
                            .Where(e => e.State == System.Data.Entity.EntityState.Modified)
                            .Select(e => e.Entity).OfType<Generica>().ToList();

            foreach (var item in modificados)
            {
                item.EditedAt = DateTime.Now;
                item.EditedById = (userId == 0) ? null : (int?)userId;
            }

            return SaveChanges();
            

        }

        public virtual DbSet<Ejercicio> Ejercicios { get; set; }
        public virtual DbSet<Año> Años { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<Localidad> Localidades { get; set; }
        public virtual DbSet<UnidadPresupuestal> UnidadesPresupuestales { get; set; }
        public virtual DbSet<TipoLocalidad> TiposLocalidad { get; set; }
        public virtual DbSet<SituacionObra> SituacionesObra { get; set; }
        public virtual DbSet<Fondo> Fondos { get; set; }
        public virtual DbSet<TipoFondo> TipoFondo { get; set; }
        public virtual DbSet<FondoLineamientos> FondoLineamientos { get; set; }
        public virtual DbSet<ModalidadFinanciamiento> ModalidadesFinanciamiento { get; set; }
        public virtual DbSet<Financiamiento> Financiamientos { get; set; }
        public virtual DbSet<AperturaProgramatica> AperturaProgramatica { get; set; }
        public virtual DbSet<AperturaProgramaticaTipo> AperturaProgramaticaTipo { get; set; }
        public virtual DbSet<AperturaProgramaticaMeta> AperturaProgramaticaMetas { get; set; }
        public virtual DbSet<AperturaProgramaticaUnidad> AperturaProgramaticaUnidades { get; set; }
        public virtual DbSet<AperturaProgramaticaBeneficiario> AperturaProgramaticaBeneficiarios { get; set; }
        public virtual DbSet<POA> POA { get; set; }
        public virtual DbSet<POADetalle> POADetalle { get; set; }
        public virtual DbSet<Obra> Obras { get; set; }
        public virtual DbSet<ObraFinanciamiento> ObraFinanciamientos { get; set; }
        public virtual DbSet<ObraPlantilla> ObraPlantilla { get; set; }
        public virtual DbSet<ObraPlantillaDetalle> ObraPlantillaDetalle { get; set; }        
        public virtual DbSet<Plantilla> Plantilla { get; set; }
        public virtual DbSet<PlantillaDetalle> PlantillaDetalle { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Permiso> Permisos { get; set; }
        public virtual DbSet<OpcionSistema> OpcionesSistema { get; set; }
        public virtual DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public virtual DbSet<UsuarioUnidadPresupuestal> UsuarioUnidadPresupuestal { get; set; }
        public virtual DbSet<POAPlantilla> POAPlantilla { get; set; } //Agregado por Rigoberto TS 25/09/2014
        public virtual DbSet<POAPlantillaDetalle> POAPlantillaDetalle { get; set; } //Agregado por Rigoberto TS 25/09/2014
        public virtual DbSet<Funcionalidad> Funcionalidad { get; set; }
        public virtual DbSet<Eje> Eje { get; set; }
        public virtual DbSet<PlanSectorial> PlanSectorial { get; set; }
        public virtual DbSet<Modalidad> Modalidad { get; set; }
        public virtual DbSet<Programa> Programa { get; set; }
        public virtual DbSet<GrupoBeneficiario> GrupoBeneficiario { get; set; }
        public virtual DbSet<CriterioPriorizacion> CriterioPriorizacion { get; set; }
        public virtual DbSet<TechoFinanciero> TechoFinanciero { get; set; }
        public virtual DbSet<TechoFinancieroUnidadPresupuestal> TechoFinancieroUnidadPresupuestal { get; set; }
        public virtual DbSet<TechoFinancieroStatus> TechoFinancieroStatus { get; set; }
        public virtual DbSet<TechoFinancieroBitacora> TechoFinancieroBitacora { get; set; }
        public virtual DbSet<TechoFinancieroBitacoraMovimientos> TechoFinancieroBitacoraMovimientos { get; set; }

        public virtual DbSet<TechoFinancieroTMPtransferencias> TechoFinancieroTMPtransferencias { get; set; }

        public virtual DbSet<Firmas> Firmas { get; set; }


        public virtual DbSet<ContratosDeObra> DBScontratosDeObra { get; set; }
        public virtual DbSet<ConceptosDeObra> DBSconceptosDeObra { get; set; }
        public virtual DbSet<PresupuestosContratados> DBSpresupuestosContratados { get; set; }

        public virtual DbSet<Estimaciones> DBSestimaciones { get; set; }
        public virtual DbSet<EstimacionesConceptos> DBSestimacionesConceptos { get; set; }

    }

}

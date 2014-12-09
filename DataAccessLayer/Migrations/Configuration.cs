namespace DataAccessLayer.Migrations
{
    using DataAccessLayer.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.Models.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataAccessLayer.Models.Contexto context)
        {
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
                       
           
            context.Roles.AddOrUpdate(
            
                new Rol { Id = 1 , Clave="R001", Nombre="Desarrollador", Orden=1},
                new Rol { Id = 2 , Clave="R002", Nombre="Ejecutivo", Orden = 2 },
                new Rol { Id = 3 , Clave="R003", Nombre="Administrador", Orden=3},
                new Rol { Id = 4 , Clave="R004", Nombre="Capturista", Orden=4},
                new Rol { Id = 5,  Clave="R005", Nombre="Analista", Orden = 5 }   
            );
                        

            context.Usuarios.AddOrUpdate(

               new Usuario { Id = 1, Login = "desarrollador", Password = "desarrollador", Nombre = "Usuario Desarrollador", Activo = true,RolId=1 },
               new Usuario { Id = 2, Login = "ejecutivo", Password = "ejecutivo", Nombre = "Usuario Ejecutivo", Activo = true, RolId = 2 },
               new Usuario { Id = 3, Login = "admin", Password = "admin", Nombre = "Usuario Administrador", Activo = true, RolId = 3 },
               new Usuario { Id = 4, Login = "sedarpa", Password = "sedarpa", Nombre = "Capturista de SEDARPA", Activo = true, RolId = 4 },
               new Usuario { Id = 5, Login = "iiev", Password = "iiev", Nombre = "Capturista de IIEV", Activo = true, RolId = 4 },
               new Usuario { Id = 6, Login = "inverbio", Password = "inverbio", Nombre = "Capturista de INVERBIO", Activo = true, RolId = 4 } ,
               new Usuario { Id = 7, Login = "analista", Password = "analista", Nombre = "Analista en SEFIPLAN", Activo = true, RolId = 5 }               
            );

           

            context.OpcionesSistema.AddOrUpdate(
            
                new OpcionSistema { Id = 1, Clave = "OS001", Descripcion = "Captura del proyecto de POA",Activo=true,Orden=1},
                new OpcionSistema { Id = 2, Clave = "OS002", Descripcion = "Ajuste del POA",Activo=true,Orden=2},
                new OpcionSistema { Id = 3, Clave = "OS003", Descripcion = "Catálogos",Activo=true,Orden=3},
                new OpcionSistema { Id = 4, Clave = "OS004", Descripcion = "Catálogo de Unidades presupuestales", Activo = true, Orden = 1,ParentId=3 },
                new OpcionSistema { Id = 5, Clave = "OS005", Descripcion = "Catálogo de Fondos", Activo = true, Orden = 2, ParentId = 3 },
                new OpcionSistema { Id = 6, Clave = "OS006", Descripcion = "Catálogo de Apertura programatica", Activo = true, Orden = 3, ParentId = 3 },
                new OpcionSistema { Id = 7, Clave = "OS007", Descripcion = "Catálogo de Municipios", Activo = true, Orden = 4, ParentId = 3 },
                new OpcionSistema { Id = 8, Clave = "OS008", Descripcion = "Catálogo de Ejercicios", Activo = true, Orden = 5, ParentId = 3 },
                new OpcionSistema { Id = 9, Clave = "OS009", Descripcion = "Catálogo de Usuarios", Activo = true, Orden = 6, ParentId = 3 }

            );

            context.Permisos.AddOrUpdate(

                new Permiso { Id = 1, RolId = 3, OpcionSistemaId = 4, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
                new Permiso { Id = 2, RolId = 3, OpcionSistemaId = 5, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
                new Permiso { Id = 3, RolId = 3, OpcionSistemaId = 6, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
                new Permiso { Id = 4, RolId = 4, OpcionSistemaId = 1, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
                new Permiso { Id = 5, RolId = 4, OpcionSistemaId = 2, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle }
                
            );            
            

            context.Ejercicios.AddOrUpdate(              
               new Ejercicio { Id = 1, Año = 2014, FactorIva = 1.6M, Estatus = enumEstatusEjercicio.Activo  },
               new Ejercicio { Id = 2, Año = 2015, FactorIva = 1.6M, Estatus = enumEstatusEjercicio.Nuevo   }               
            );

            context.UnidadesPresupuestales.AddOrUpdate(

              new UnidadPresupuestal { Id = 1, Clave = "102S11001", Abreviatura = "SEDARPA", Nombre = "Secretaria de Desarrollo Agropecuario, Rural y Pesca", Orden = 1 },
              new UnidadPresupuestal { Id = 2, Clave = "104C80803", Abreviatura = "IIEV", Nombre = "Instituto de Espacios Educativos", Orden = 2 },
              new UnidadPresupuestal { Id = 3, Clave = "104S80801", Abreviatura = "COBAEV", Nombre = "Colegio de Bachilleres del Estado de Veracruz", Orden = 3 }
              
           );

            UnidadPresupuestal sedarpa = context.UnidadesPresupuestales.Local.FirstOrDefault(u => u.Clave == "102S11001");

            sedarpa.DetalleSubUnidadesPresupuestales.Add(new UnidadPresupuestal { Id = 4, Clave = "102S80808", Abreviatura = "CODEPAP", Nombre = "Consejo de desarrollo del Papaloapan", Orden = 1 });
            sedarpa.DetalleSubUnidadesPresupuestales.Add(new UnidadPresupuestal { Id = 5, Clave = "102S80809", Abreviatura = "INVERBIO", Nombre = "Instituto Veracruzano de Bioenergéticos", Orden = 2 });



            Usuario usedarpa = context.Usuarios.Local.FirstOrDefault(u => u.Login == "sedarpa");
            Usuario uiiev = context.Usuarios.Local.FirstOrDefault(u => u.Login == "iiev");
            Usuario uinverbio = context.Usuarios.Local.FirstOrDefault(u => u.Login == "inverbio");

            usedarpa.DetalleUnidadesPresupuestales.Add(new UsuarioUnidadPresupuestal { UnidadPresupuestalId=1});
            uiiev.DetalleUnidadesPresupuestales.Add(new UsuarioUnidadPresupuestal { UnidadPresupuestalId = 2 });
            uinverbio.DetalleUnidadesPresupuestales.Add(new UsuarioUnidadPresupuestal { UnidadPresupuestalId = 5 });


            context.Municipios.AddOrUpdate(              
              new Municipio {Id=1,Clave="M001",Nombre="Acajete",Orden=1 },
              new Municipio {Id=2,Clave="M002",Nombre="Acatlán",Orden=2 },
              new Municipio {Id=3,Clave="M003",Nombre="Acayucan",Orden=3 },
              new Municipio {Id=4,Clave="M004",Nombre="Actopan",Orden=4 }              
            );

            context.Localidades.AddOrUpdate(
                new Localidad { Id = 1, Nombre = "Primer localidad de Acajete", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 1, MunicipioId = 1,TipoLocalidadId=1 },
                new Localidad { Id = 2, Nombre = "Segunda localidad de Acajete", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 2, MunicipioId = 1, TipoLocalidadId = 1 },
                new Localidad { Id = 3, Nombre = "Tercera localidad de Acajete", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 3, MunicipioId = 1, TipoLocalidadId = 1 },
                new Localidad { Id = 4, Nombre = "Primer localidad de Acatlán", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 1, MunicipioId = 2, TipoLocalidadId = 1 },
                new Localidad { Id = 5, Nombre = "Segunda localidad de Acatlán", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 2, MunicipioId = 2, TipoLocalidadId = 1 },
                new Localidad { Id = 6, Nombre = "Primer localidad de Acayucan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 1, MunicipioId = 3, TipoLocalidadId = 1 },
                new Localidad { Id = 7, Nombre = "Primer localidad de Actopan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 1, MunicipioId = 4, TipoLocalidadId = 1 },
                new Localidad { Id = 8, Nombre = "Segunda localidad de Actopan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 2, MunicipioId = 4, TipoLocalidadId = 1 },
                new Localidad { Id = 9, Nombre = "Tercera localidad de Actopan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 3, MunicipioId = 4, TipoLocalidadId = 1 },
                new Localidad { Id = 10, Nombre = "Cuarta localidad de Actopan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 4, MunicipioId = 4, TipoLocalidadId = 1 }

            );

            context.TiposLocalidad.AddOrUpdate(
             new TipoLocalidad { Id = 1,Clave="TL001",Nombre="Poblado urbano",Orden=1},
             new TipoLocalidad { Id = 2,Clave="TL002",Nombre="Poblado rural",Orden=2},
             new TipoLocalidad { Id = 3,Clave="TL003",Nombre="Colonia popular",Orden=3},
             new TipoLocalidad { Id = 4,Clave="TL004",Nombre="Poblado indígena",Orden=4}
           );

           context.SituacionesObra.AddOrUpdate(
             new SituacionObra { Id = 1, Clave = "SO001", Nombre = "Nueva", Orden = 1 },
             new SituacionObra { Id = 2, Clave = "SO002", Nombre = "Proceso (Concluir financieramente)", Orden = 2 },
             new SituacionObra { Id = 3, Clave = "SO003", Nombre = "Proceso (Concluir fisica y financieramente)", Orden = 3 }
           
           );

           context.TipoFondo.AddOrUpdate(
                new TipoFondo { Id = 1, Clave = "TF01", Nombre = "Federal", Orden = 1 }, 
                new TipoFondo { Id = 2, Clave = "TF02", Nombre = "Estatal", Orden = 2 },
                new TipoFondo { Id = 3, Clave = "TF03", Nombre = "Otros", Orden = 3 }
           );


           context.Fondos.AddOrUpdate(
             new Fondo { Id = 1, Clave = "F010", Abreviatura = "FORTAMUNDF", Nombre = "Fondo de Aportaciones para el Fortalecimiento de los Municipios y Demarcaciones Territoriales del Distrito Federal", Orden = 1,TipoFondoId=1 },
             new Fondo { Id = 2, Clave = "F020", Abreviatura = "FAIS", Nombre = "Fondo para la Infraestructura Social", Orden = 2, TipoFondoId = 1 },
             new Fondo { Id = 3, Clave = "F030", Abreviatura = "Otros", Nombre = "Otros fondos", Orden = 3, TipoFondoId = 3 },

             new Fondo { Id = 6, Clave = "F060", Abreviatura = "FAM", Nombre = "Fondo de Aportaciones Múltiples Educación Básica y Superior", Orden = 5, TipoFondoId = 1 },
             new Fondo { Id = 7, Clave = "F070", Abreviatura = "FAFEF", Nombre = "Fondo de Aportaciones para el Fortalecimiento de las Entidades Federativas", Orden = 6, TipoFondoId = 1 },

             new Fondo { Id = 8, Clave = "F080", Abreviatura = "FONREGION", Nombre = "Fondo Regional", Orden = 7,TipoFondoId=1 },
             new Fondo { Id = 9, Clave = "F090", Abreviatura = "APAZU", Nombre = "Programa de Agua Potable, Alcantarillado y Saneamiento", Orden = 8, TipoFondoId = 1 },
             new Fondo { Id = 10, Clave = "F100", Abreviatura = "PROSSAPYS", Nombre = "Programa para la Sustentabilidad de los Servicios de Agua Potable y Saneamiento en Comunidades Rurales", Orden = 9, TipoFondoId = 1 },
             new Fondo { Id = 11, Clave = "F110", Abreviatura = "PRODEPI", Nombre = "Programa de Infraestructura Básica para la Atención de los Pueblos Indígenas", Orden = 10, TipoFondoId = 1 },
             new Fondo { Id = 12, Clave = "F120", Abreviatura = "FOISSA", Nombre = "Fortalecimiento de la Infraestructura de Servicios de Salud", Orden = 11, TipoFondoId = 1 }


           );

           Fondo fais = context.Fondos.Local.FirstOrDefault(f => f.Clave == "F010");
           fais.DetalleSubFondos.Add(new Fondo { Id = 4, Clave = "F011", Abreviatura = "FISE", Nombre = "Fondo para la Infraestructura Social Estatal", Orden = 1,TipoFondoId=1 });
           fais.DetalleSubFondos.Add(new Fondo { Id = 5, Clave = "F012", Abreviatura = "FISM", Nombre = "Fondo para la Infraestructura Social Municipal ", Orden = 2,TipoFondoId=1 });


           context.FondoLineamientos.AddOrUpdate(
               new FondoLineamientos { Id=1, FondoId=4, TipoDeObrasYAcciones="Obras y acciones de infraestructura de ámbito regional o intermunicipal que combatan la pobreza extrema", CalendarioDeIngresos="Enero-Octubre", VigenciaDePago="Sin anualidad", NormatividadAplicable="Estatal", Contraparte="" },
               new FondoLineamientos { Id=2, FondoId=6, TipoDeObrasYAcciones="Acciones de asistencia social", CalendarioDeIngresos="Enero-Diciembre", VigenciaDePago="Sin anualidad", NormatividadAplicable="Estatal", Contraparte="" },
               new FondoLineamientos { Id=3, FondoId=7, TipoDeObrasYAcciones="Obras de infraestructura pública diversa y otros capítulos de gasto", CalendarioDeIngresos="Enero-Diciembre", VigenciaDePago="Sin anualidad", NormatividadAplicable="Estatal", Contraparte="" },

               new FondoLineamientos { Id = 4, FondoId = 8, TipoDeObrasYAcciones = "Infraestructura pública regional (hospitales, infraestructura eductiva media-superior, caminos, sistemas agua potable integrales)", CalendarioDeIngresos = "Conforme las ejecutoras den cumplimiento a los requisitos establecidos por la S.H.C.P", VigenciaDePago = "Deben estar registrados y autorizados por la S.H.C.P antes del 31 de Diciembre", NormatividadAplicable = "Federal", Contraparte = "" },
               new FondoLineamientos { Id=5, FondoId=9, TipoDeObrasYAcciones="Infraestructura en agua potable, alcantarillado y saneamiento en zonas urbanas", CalendarioDeIngresos="Inicia con la firma del convenio y posteriormente conforme al avance de las obras", VigenciaDePago="Al 31 de Diciembre", NormatividadAplicable="Federal", Contraparte="Requiere contraparte estatal" },
               new FondoLineamientos { Id=6, FondoId=10, TipoDeObrasYAcciones="Obras y acciones de agua potable y saneamiento en comunidades rurales", CalendarioDeIngresos="Inicia con la firma del convenio y posteriormente conforme al avance de las obras", VigenciaDePago="Al 31 de Diciembre", NormatividadAplicable="Federal", Contraparte="Requiere contraparte estatal" },
               new FondoLineamientos { Id=7, FondoId=11, TipoDeObrasYAcciones="Infraestructura carretera rural y de agua potable en zonas indigenas", CalendarioDeIngresos="Inicia con la firma del convenio y posteriormente conforme al avance de las obras", VigenciaDePago="Al 31 de Diciembre", NormatividadAplicable="Federal", Contraparte="Requiere contraparte estatal" },
               new FondoLineamientos { Id=8, FondoId=12, TipoDeObrasYAcciones="Obras y acciones de infraestructura hospitalaria", CalendarioDeIngresos="Inicia con la firma del convenio y posteriormente conforme al avance de las obras", VigenciaDePago="Hasta el cumplimiento del objetivo", NormatividadAplicable="Federal", Contraparte="" }


            );




           context.ModalidadesFinanciamiento.AddOrUpdate(

                new ModalidadFinanciamiento { Id = 1, Clave = "MF001", Nombre = "Actual", Orden = 1 },
                new ModalidadFinanciamiento { Id = 2, Clave = "MF002", Nombre = "Remanente", Orden = 2 },
                new ModalidadFinanciamiento { Id = 3, Clave = "MF003", Nombre = "Intereses", Orden = 3 },
                new ModalidadFinanciamiento { Id = 4, Clave = "MF004", Nombre = "Prestamo", Orden = 4 }
            );

           context.Años.AddOrUpdate(

              new Año { Id = 1, Anio = 2008 },
              new Año { Id = 2, Anio = 2009 },
              new Año { Id = 3 ,Anio = 2010 },
              new Año { Id = 4 ,Anio = 2011 },
              new Año { Id = 5 ,Anio = 2012 },
              new Año { Id = 6 ,Anio = 2013 },
              new Año { Id = 7 ,Anio = 2014 },
              new Año { Id = 8 ,Anio = 2015 }
             
          );
          
           context.Financiamientos.AddOrUpdate(
             new Financiamiento { Id = 1,  AñoId = 6, FondoId = 1, ModalidadFinanciamientoId = 1 }, 
             new Financiamiento { Id = 2,  AñoId = 6, FondoId = 1, ModalidadFinanciamientoId = 2 },   
             new Financiamiento { Id = 3,  AñoId = 6, FondoId = 1, ModalidadFinanciamientoId = 3 },   
             new Financiamiento { Id = 4,  AñoId = 6, FondoId = 1, ModalidadFinanciamientoId = 4 },
             new Financiamiento { Id = 5,  AñoId = 6, FondoId = 5, ModalidadFinanciamientoId = 1 },
             new Financiamiento { Id = 6,  AñoId = 6, FondoId = 5, ModalidadFinanciamientoId = 2 },
             new Financiamiento { Id = 7,  AñoId = 6, FondoId = 5, ModalidadFinanciamientoId = 3 },
             new Financiamiento { Id = 8,  AñoId = 6, FondoId = 5, ModalidadFinanciamientoId = 4 },
             new Financiamiento { Id = 9,  AñoId = 7, FondoId = 1, ModalidadFinanciamientoId = 1 },
             new Financiamiento { Id = 10, AñoId = 7, FondoId = 1, ModalidadFinanciamientoId = 2 },
             new Financiamiento { Id = 11, AñoId = 7, FondoId = 1, ModalidadFinanciamientoId = 3 },
             new Financiamiento { Id = 12, AñoId = 7, FondoId = 1, ModalidadFinanciamientoId = 4 },  
             new Financiamiento { Id = 13, AñoId = 7, FondoId = 5, ModalidadFinanciamientoId = 1 },
             new Financiamiento { Id = 14, AñoId = 7, FondoId = 5, ModalidadFinanciamientoId = 2 },
             new Financiamiento { Id = 15, AñoId = 7, FondoId = 5, ModalidadFinanciamientoId = 3 },
             new Financiamiento { Id = 16, AñoId = 7, FondoId = 5, ModalidadFinanciamientoId = 4 }  
           );

           context.AperturaProgramaticaTipo.AddOrUpdate(
              new AperturaProgramaticaTipo { Id = 1, Clave = "APT001", Nombre = "OBRA", Orden = 1,EsObra=true,Identificador="0" },
              new AperturaProgramaticaTipo { Id = 2, Clave = "APT002", Nombre = "ACCIONES DE APOYO A LA SUPERVISION", Orden = 2,EsObra=false,Identificador="2" },
              new AperturaProgramaticaTipo { Id = 3, Clave = "APT003", Nombre = "ACCIONES DE INFRAESTRUCTURA", Orden = 3,EsObra=false,Identificador="3" },
              new AperturaProgramaticaTipo { Id = 4, Clave = "APT004", Nombre = "ESTUDIOS Y PROYECTOS", Orden = 4,EsObra=false,Identificador="1" } 
          );


           context.AperturaProgramaticaUnidades.AddOrUpdate(
               new AperturaProgramaticaUnidad { Id = 1, Clave = "APU001", Nombre = "Planta", Orden = 1 },
               new AperturaProgramaticaUnidad { Id = 2, Clave = "APU002", Nombre = "Pozo", Orden = 2 },
               new AperturaProgramaticaUnidad { Id = 3, Clave = "APU003", Nombre = "Tanque", Orden = 3 },
               new AperturaProgramaticaUnidad { Id = 4, Clave = "APU004", Nombre = "Metro lineal", Orden = 4 },
               new AperturaProgramaticaUnidad { Id = 5, Clave = "APU005", Nombre = "Sistema", Orden = 5 },
               new AperturaProgramaticaUnidad { Id = 6, Clave = "APU006", Nombre = "Obra", Orden = 6 },
               new AperturaProgramaticaUnidad { Id = 7, Clave = "APU007", Nombre = "Pozo", Orden = 7 },
               new AperturaProgramaticaUnidad { Id = 8, Clave = "APU008", Nombre = "Olla", Orden = 8}

           );

           context.AperturaProgramaticaBeneficiarios.AddOrUpdate(
             new AperturaProgramaticaBeneficiario { Id = 1, Clave = "APB001", Nombre = "Persona", Orden = 1 },
             new AperturaProgramaticaBeneficiario { Id = 2, Clave = "APB002", Nombre = "Productor", Orden = 2 },
             new AperturaProgramaticaBeneficiario { Id = 3, Clave = "APB003", Nombre = "Familia", Orden = 3 },
             new AperturaProgramaticaBeneficiario { Id = 4, Clave = "APB004", Nombre = "Alumno", Orden = 4 }

           );

           context.AperturaProgramatica.AddOrUpdate(
               new AperturaProgramatica { Id = 1, Clave = "SC", Nombre = "Agua y saneamiento (Agua potable)", Orden = 1, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 2, Clave = "SD", Nombre = "Agua y saneamiento (Drenaje)", Orden = 2, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 3, Clave = "SE", Nombre = "Urbanización municipal", Orden = 3, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 4, Clave = "SG", Nombre = "Electrificación", Orden = 4, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 5, Clave = "SO", Nombre = "Salud", Orden = 5, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 6, Clave = "SJ", Nombre = "Educación", Orden = 6, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 7, Clave = "SH", Nombre = "Vivienda", Orden = 7, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 8, Clave = "UB", Nombre = "Caminos rurales", Orden = 8, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 9, Clave = "IR", Nombre = "Infraestructura productiva rural", Orden = 9, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 10, Clave = "EP", Nombre = "Estudios", Orden = 10, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 11, Clave = "DI", Nombre = "Programa de desarrollo institucional municipal y de las demarcaciones territoriales del distrito federal", Orden = 11, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 12, Clave = "U9", Nombre = "Gastos indirectos", Orden = 12, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 13, Clave = "PP", Nombre = "Prevención presupuestaria", Orden = 13, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 14, Clave = "DP", Nombre = "Deuda pública", Orden = 14, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 15, Clave = "PA", Nombre = "Auditoría", Orden = 15, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 16, Clave = "SP", Nombre = "Seguridad pública municipal", Orden = 16, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 17, Clave = "FM", Nombre = "Fortalecimiento municipal", Orden = 17, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 18, Clave = "UM", Nombre = "Equipamiento urbano", Orden = 18, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 19, Clave = "PE", Nombre = "Protección y preservación ecológica", Orden = 19, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 20, Clave = "BM", Nombre = "Bienes muebles", Orden = 20, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 21, Clave = "BI", Nombre = "Bienes inmuebles", Orden = 21, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 22, Clave = "PM", Nombre = "Planeación municipal", Orden = 22, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 23, Clave = "SB", Nombre = "Estímulos a la educación", Orden = 23, EjercicioId = 2, Nivel = 1 }
            );

           AperturaProgramatica sc = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SC");

           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 24, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 25, Clave = "02", Nombre = "Ampliación", Orden = 2, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 26, Clave = "03", Nombre = "Construcción", Orden = 3, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 27, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 28, Clave = "05", Nombre = "Equipamiento", Orden = 5, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 29, Clave = "06", Nombre = "Sustitución", Orden = 6, EjercicioId = 2, Nivel = 2 });


           AperturaProgramatica sd = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SD");

           sd.DetalleSubElementos.Add(new AperturaProgramatica { Id = 30, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           sd.DetalleSubElementos.Add(new AperturaProgramatica { Id = 31, Clave = "02", Nombre = "Ampliación", Orden = 2, EjercicioId = 2, Nivel = 2 });
           sd.DetalleSubElementos.Add(new AperturaProgramatica { Id = 32, Clave = "03", Nombre = "Construcción", Orden = 3, EjercicioId = 2, Nivel = 2 });
           sd.DetalleSubElementos.Add(new AperturaProgramatica { Id = 33, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica se = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SE");

           se.DetalleSubElementos.Add(new AperturaProgramatica { Id = 34, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           se.DetalleSubElementos.Add(new AperturaProgramatica { Id = 35, Clave = "02", Nombre = "Construcción", Orden = 2, EjercicioId = 2, Nivel = 2 });
           se.DetalleSubElementos.Add(new AperturaProgramatica { Id = 36, Clave = "03", Nombre = "Ampliación", Orden = 3, EjercicioId = 2, Nivel = 2 });
           se.DetalleSubElementos.Add(new AperturaProgramatica { Id = 37, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica sg = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SG");

           sg.DetalleSubElementos.Add(new AperturaProgramatica { Id = 38, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           sg.DetalleSubElementos.Add(new AperturaProgramatica { Id = 39, Clave = "02", Nombre = "Ampliación", Orden = 2, EjercicioId = 2, Nivel = 2 });
           sg.DetalleSubElementos.Add(new AperturaProgramatica { Id = 40, Clave = "03", Nombre = "Construcción", Orden = 3, EjercicioId = 2, Nivel = 2 });
           sg.DetalleSubElementos.Add(new AperturaProgramatica { Id = 41, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });
           sg.DetalleSubElementos.Add(new AperturaProgramatica { Id = 42, Clave = "05", Nombre = "Equipamiento", Orden = 5, EjercicioId = 2, Nivel = 2 });


           AperturaProgramatica so = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SO");

           so.DetalleSubElementos.Add(new AperturaProgramatica { Id = 43, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           so.DetalleSubElementos.Add(new AperturaProgramatica { Id = 44, Clave = "02", Nombre = "Ampliación", Orden = 2, EjercicioId = 2, Nivel = 2 });
           so.DetalleSubElementos.Add(new AperturaProgramatica { Id = 45, Clave = "03", Nombre = "Construcción", Orden = 3, EjercicioId = 2, Nivel = 2 });
           so.DetalleSubElementos.Add(new AperturaProgramatica { Id = 46, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });
           so.DetalleSubElementos.Add(new AperturaProgramatica { Id = 47, Clave = "05", Nombre = "Equipamiento", Orden = 5, EjercicioId = 2, Nivel = 2 });


           AperturaProgramatica sj = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SJ");

           sj.DetalleSubElementos.Add(new AperturaProgramatica { Id = 48, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           sj.DetalleSubElementos.Add(new AperturaProgramatica { Id = 49, Clave = "02", Nombre = "Construcción", Orden = 2, EjercicioId = 2, Nivel = 2 });
           sj.DetalleSubElementos.Add(new AperturaProgramatica { Id = 50, Clave = "03", Nombre = "Equipamiento", Orden = 3, EjercicioId = 2, Nivel = 2 });
           sj.DetalleSubElementos.Add(new AperturaProgramatica { Id = 51, Clave = "04", Nombre = "Ampliación", Orden = 4, EjercicioId = 2, Nivel = 2 });
           sj.DetalleSubElementos.Add(new AperturaProgramatica { Id = 52, Clave = "05", Nombre = "Mantenimiento", Orden = 5, EjercicioId = 2, Nivel = 2 });


           AperturaProgramatica sh = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SH");

           sh.DetalleSubElementos.Add(new AperturaProgramatica { Id = 53, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           sh.DetalleSubElementos.Add(new AperturaProgramatica { Id = 54, Clave = "02", Nombre = "Construcción", Orden = 2, EjercicioId = 2, Nivel = 2 });
           sh.DetalleSubElementos.Add(new AperturaProgramatica { Id = 55, Clave = "03", Nombre = "Equipamiento", Orden = 3, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica ub = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "UB");

           ub.DetalleSubElementos.Add(new AperturaProgramatica { Id = 56, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           ub.DetalleSubElementos.Add(new AperturaProgramatica { Id = 57, Clave = "02", Nombre = "Construcción", Orden = 2, EjercicioId = 2, Nivel = 2 });
           ub.DetalleSubElementos.Add(new AperturaProgramatica { Id = 58, Clave = "03", Nombre = "Ampliación", Orden = 3, EjercicioId = 2, Nivel = 2 });
           ub.DetalleSubElementos.Add(new AperturaProgramatica { Id = 59, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });


           AperturaProgramatica ir = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "IR");

           ir.DetalleSubElementos.Add(new AperturaProgramatica { Id = 60, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           ir.DetalleSubElementos.Add(new AperturaProgramatica { Id = 61, Clave = "02", Nombre = "Ampliación", Orden = 2, EjercicioId = 2, Nivel = 2 });
           ir.DetalleSubElementos.Add(new AperturaProgramatica { Id = 62, Clave = "03", Nombre = "Construcción", Orden = 3, EjercicioId = 2, Nivel = 2 });
           ir.DetalleSubElementos.Add(new AperturaProgramatica { Id = 63, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });
           ir.DetalleSubElementos.Add(new AperturaProgramatica { Id = 64, Clave = "05", Nombre = "Equipamiento", Orden = 5, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica ep = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "EP");

           ep.DetalleSubElementos.Add(new AperturaProgramatica { Id = 65, Clave = "01", Nombre = "Estudios", Orden = 1, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica di = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "DI");

           di.DetalleSubElementos.Add(new AperturaProgramatica { Id = 66, Clave = "01", Nombre = "Programa de desarrollo institucional municipal y de las demarcaciones territoriales del distrito federal", Orden = 1, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica u9 = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "U9");

           u9.DetalleSubElementos.Add(new AperturaProgramatica { Id = 67, Clave = "01", Nombre = "Realización de estudios asociados a los proyectos", Orden = 1, EjercicioId = 2, Nivel = 2 });
           u9.DetalleSubElementos.Add(new AperturaProgramatica { Id = 68, Clave = "02", Nombre = "Seguimiento de obra", Orden = 2, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica pp = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "PP");

           pp.DetalleSubElementos.Add(new AperturaProgramatica { Id = 69, Clave = "01", Nombre = "Prevención presupuestaria", Orden = 1, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica dp = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "DP");

           dp.DetalleSubElementos.Add(new AperturaProgramatica { Id = 70, Clave = "01", Nombre = "Deuda pública", Orden = 1, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica pa = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "PA");

           pa.DetalleSubElementos.Add(new AperturaProgramatica { Id = 71, Clave = "01", Nombre = "Auditoría", Orden = 1, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica sp = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SP");

           sp.DetalleSubElementos.Add(new AperturaProgramatica { Id = 72, Clave = "01", Nombre = "Recursos humanos", Orden = 1, EjercicioId = 2, Nivel = 2 });
           sp.DetalleSubElementos.Add(new AperturaProgramatica { Id = 73, Clave = "02", Nombre = "Equipos y accesorios", Orden = 2, EjercicioId = 2, Nivel = 2 });


           AperturaProgramatica fm = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "FM");

           fm.DetalleSubElementos.Add(new AperturaProgramatica { Id = 74, Clave = "01", Nombre = "Capacitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           fm.DetalleSubElementos.Add(new AperturaProgramatica { Id = 75, Clave = "02", Nombre = "Pago de servicios municipales", Orden = 2, EjercicioId = 2, Nivel = 2 });
           fm.DetalleSubElementos.Add(new AperturaProgramatica { Id = 76, Clave = "03", Nombre = "Vehículos terrestres", Orden = 3, EjercicioId = 2, Nivel = 2 });
           fm.DetalleSubElementos.Add(new AperturaProgramatica { Id = 77, Clave = "04", Nombre = "Sistematización de procesos", Orden = 4, EjercicioId = 2, Nivel = 2 });
           fm.DetalleSubElementos.Add(new AperturaProgramatica { Id = 78, Clave = "05", Nombre = "Protección civil municipal", Orden = 5, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica um = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "UM");

           um.DetalleSubElementos.Add(new AperturaProgramatica { Id = 79, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           um.DetalleSubElementos.Add(new AperturaProgramatica { Id = 80, Clave = "02", Nombre = "Construcción", Orden = 2, EjercicioId = 2, Nivel = 2 });
           um.DetalleSubElementos.Add(new AperturaProgramatica { Id = 81, Clave = "03", Nombre = "Ampliación", Orden = 3, EjercicioId = 2, Nivel = 2 });
           um.DetalleSubElementos.Add(new AperturaProgramatica { Id = 82, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });
           um.DetalleSubElementos.Add(new AperturaProgramatica { Id = 83, Clave = "05", Nombre = "Equipamiento", Orden = 5, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica pe = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "PE");

           pe.DetalleSubElementos.Add(new AperturaProgramatica { Id = 84, Clave = "01", Nombre = "Manejo de residuos solidos", Orden = 1, EjercicioId = 2, Nivel = 2 });
           pe.DetalleSubElementos.Add(new AperturaProgramatica { Id = 85, Clave = "02", Nombre = "Reforestación", Orden = 2, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica be = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "BM");

           be.DetalleSubElementos.Add(new AperturaProgramatica { Id = 86, Clave = "01", Nombre = "Adquisiciones", Orden = 1, EjercicioId = 2, Nivel = 2 });
           be.DetalleSubElementos.Add(new AperturaProgramatica { Id = 87, Clave = "02", Nombre = "Otros", Orden = 2, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica bi = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "BI");

           bi.DetalleSubElementos.Add(new AperturaProgramatica { Id = 88, Clave = "01", Nombre = "Adquisiciones", Orden = 1, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica pm = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "PM");

           pm.DetalleSubElementos.Add(new AperturaProgramatica { Id = 89, Clave = "01", Nombre = "Estudios", Orden = 1, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica sb = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SB");

           sb.DetalleSubElementos.Add(new AperturaProgramatica { Id = 90, Clave = "01", Nombre = "Becas y despensas", Orden = 1, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica sc_rehabilitacion = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Id == 24);

           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 91, Clave = "a", Nombre = "Planta potabilizadora", Orden = 1, EjercicioId = 2, Nivel = 3,AperturaProgramaticaTipoId=1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 92, Clave = "b", Nombre = "Pozo profundo de agua potable", Orden = 2, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 93, Clave = "c", Nombre = "Deposito o tanque de agua potable", Orden = 3, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 94, Clave = "d", Nombre = "Linea de conducción", Orden = 4, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 95, Clave = "e", Nombre = "Red de agua potable", Orden = 5, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 96, Clave = "f", Nombre = "Sistema integral de agua potable", Orden = 6, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 97, Clave = "g", Nombre = "Carcamo", Orden = 7, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 98, Clave = "h", Nombre = "Norias", Orden = 8, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 99, Clave = "i", Nombre = "Pozo artesiano", Orden = 9, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 100, Clave = "j", Nombre = "Olla de captación de agua pluvial", Orden = 10, EjercicioId = 2, Nivel = 3, AperturaProgramaticaTipoId = 1 });


           context.AperturaProgramaticaMetas.AddOrUpdate(
               new AperturaProgramaticaMeta { Id = 1, AperturaProgramaticaId = 91, AperturaProgramaticaUnidadId = 1, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 2, AperturaProgramaticaId = 92, AperturaProgramaticaUnidadId = 2, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 3, AperturaProgramaticaId = 93, AperturaProgramaticaUnidadId = 3, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 4, AperturaProgramaticaId = 94, AperturaProgramaticaUnidadId = 4, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 5, AperturaProgramaticaId = 95, AperturaProgramaticaUnidadId = 4, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 6, AperturaProgramaticaId = 96, AperturaProgramaticaUnidadId = 5, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 7, AperturaProgramaticaId = 97, AperturaProgramaticaUnidadId = 6, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 8, AperturaProgramaticaId = 98, AperturaProgramaticaUnidadId = 6, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 9, AperturaProgramaticaId = 99, AperturaProgramaticaUnidadId = 7, AperturaProgramaticaBeneficiarioId = 1 },
               new AperturaProgramaticaMeta { Id = 10, AperturaProgramaticaId = 100, AperturaProgramaticaUnidadId = 8, AperturaProgramaticaBeneficiarioId = 1 }
           );

           context.Funcionalidad.AddOrUpdate(
            new Funcionalidad { Id = 1, Clave = "F001", Descripcion = "Gobierno", Orden = 1,Nivel=1 },
            new Funcionalidad { Id = 2, Clave = "F002", Descripcion = "Desarrollo Social", Orden = 2, Nivel = 1 },
            new Funcionalidad { Id = 3, Clave = "F003", Descripcion = "Desarrollo Económico", Orden = 3, Nivel = 1 }           
            );

           Funcionalidad fgobierno = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F001");
           Funcionalidad fdesarrollosocial = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F002");
           Funcionalidad fdesarrolloeconomico = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F003");

           fgobierno.DetalleSubElementos.Add(new Funcionalidad { Id = 4, Clave = "F004", Descripcion = "Legislación", Orden = 1, Nivel = 2 });
           fgobierno.DetalleSubElementos.Add(new Funcionalidad { Id = 5, Clave = "F005", Descripcion = "Fiscalización", Orden = 2, Nivel = 2 });
           fgobierno.DetalleSubElementos.Add(new Funcionalidad { Id = 6, Clave = "F006", Descripcion = "Justicia", Orden = 3, Nivel = 2 });

           fdesarrollosocial.DetalleSubElementos.Add(new Funcionalidad { Id = 7, Clave = "F007", Descripcion = "Protección ambiental", Orden = 1, Nivel = 2 });
           fdesarrollosocial.DetalleSubElementos.Add(new Funcionalidad { Id = 8, Clave = "F008", Descripcion = "Vivienda y servicios a la comunidad", Orden = 2, Nivel = 2 });
           fdesarrollosocial.DetalleSubElementos.Add(new Funcionalidad { Id = 9, Clave = "F009", Descripcion = "Salud", Orden = 3, Nivel = 2 });

           fdesarrolloeconomico.DetalleSubElementos.Add(new Funcionalidad { Id = 10, Clave = "F010", Descripcion = "Asuntos económicos, comerciales y laborales en general", Orden = 1, Nivel = 2 });
           fdesarrolloeconomico.DetalleSubElementos.Add(new Funcionalidad { Id = 11, Clave = "F011", Descripcion = "Agropecuaria, silvicultura, pesca y caza", Orden = 2, Nivel = 2 });
           fdesarrolloeconomico.DetalleSubElementos.Add(new Funcionalidad { Id = 12, Clave = "F012", Descripcion = "Combustible y energía", Orden = 3, Nivel = 2 });

           Funcionalidad flegislacion = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F004");
           Funcionalidad ffiscalizacion = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F005");
           Funcionalidad fjusticia = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F006");

           flegislacion.DetalleSubElementos.Add(new Funcionalidad { Id = 13, Clave = "F013", Descripcion = "Legislación", Orden = 1, Nivel = 3 });
           ffiscalizacion.DetalleSubElementos.Add(new Funcionalidad { Id = 14, Clave = "F014", Descripcion = "Fiscalización", Orden = 1, Nivel = 3 });

           fjusticia.DetalleSubElementos.Add(new Funcionalidad { Id = 15, Clave = "F015", Descripcion = "Impartición de Justicia", Orden = 1, Nivel = 3 });
           fjusticia.DetalleSubElementos.Add(new Funcionalidad { Id = 16, Clave = "F016", Descripcion = "Procuración de Justicia", Orden = 2, Nivel = 3 });
           fjusticia.DetalleSubElementos.Add(new Funcionalidad { Id = 17, Clave = "F017", Descripcion = "Reclusión y readaptación social", Orden = 3, Nivel = 3 });
           fjusticia.DetalleSubElementos.Add(new Funcionalidad { Id = 18, Clave = "F018", Descripcion = "Derechos humanos", Orden = 4, Nivel = 3 });


          context.Eje.AddOrUpdate(
                new Eje { Id = 1, Clave = "A", Descripcion = "Construir el presente: Un mejor futuro para todos", Orden = 1, Nivel = 1 },
                new Eje { Id = 2, Clave = "B", Descripcion = "Economía fuerte para el progreso de la gente", Orden = 2, Nivel = 1 },
                new Eje { Id = 3, Clave = "C", Descripcion = "Un Veracruz sustentable", Orden = 3, Nivel = 1 },
                new Eje { Id = 4, Clave = "D", Descripcion = "Gobierno y administración eficientes y transparentes", Orden = 4, Nivel = 1 }
        
          );

          Eje ejeA = context.Eje.Local.FirstOrDefault(e => e.Clave == "A");

          ejeA.DetalleSubElementos.Add(new Eje { Id = 5, Clave = "A005", Descripcion = "Combatir rezagos para salir adelante", Orden = 1,Nivel=2 });
          ejeA.DetalleSubElementos.Add(new Eje { Id = 6, Clave = "A006", Descripcion = "El valor de la civilización indígena", Orden = 2, Nivel = 2 });
          ejeA.DetalleSubElementos.Add(new Eje { Id = 7, Clave = "A007", Descripcion = "La familia veracruzana", Orden = 3, Nivel = 2 });
          ejeA.DetalleSubElementos.Add(new Eje { Id = 8, Clave = "A008", Descripcion = "Igualdad de género", Orden = 4, Nivel = 2 });
          ejeA.DetalleSubElementos.Add(new Eje { Id = 9, Clave = "A009", Descripcion = "Juventud: oportunidad y compromiso", Orden = 5, Nivel = 2 });


          context.PlanSectorial.AddOrUpdate(
              new PlanSectorial { Id = 1, Clave = "A", Descripcion = "Programa Veracruzano de Desarrollo Agropecuario, Rural, Forestal y Pesca.", Orden = 1,Nivel=1 },
              new PlanSectorial { Id = 2, Clave = "B", Descripcion = "Programa Veracruzano de Salud.", Orden = 2, Nivel = 1 },
              new PlanSectorial { Id = 3, Clave = "C", Descripcion = "Programa Veracruzano de Asistencia Social.", Orden = 3, Nivel = 1 },
              new PlanSectorial { Id = 4, Clave = "D", Descripcion = "Programa Veracruzano de Educación.", Orden = 4, Nivel = 1 }           

          );

          context.Modalidad.AddOrUpdate(
            new Modalidad { Id = 1, Clave = "M001", Descripcion = "Subsidios: Sector Social y Privado o Entidades Federativas y Municipios", Orden = 1,Nivel=1 },
            new Modalidad { Id = 2, Clave = "M002", Descripcion = "Desempeño de las Funciones", Orden = 2,Nivel=1 },
            new Modalidad { Id = 3, Clave = "M003", Descripcion = "Administrativos y de Apoyo", Orden = 3,Nivel=1 },
            new Modalidad { Id = 4, Clave = "M004", Descripcion = "Programas de Gasto Federalizado (Gobierno Federal)", Orden = 4,Nivel=1 }
          );

          Modalidad mSubsidios = context.Modalidad.Local.FirstOrDefault(m => m.Clave == "M001");

          mSubsidios.DetalleSubElementos.Add(new Modalidad { Id = 5, Clave = "S", Descripcion = "Sujetos a Reglas de Operación", Orden = 1, Nivel = 2 });
          mSubsidios.DetalleSubElementos.Add(new Modalidad { Id = 6, Clave = "U", Descripcion = "Otros Subsidios", Orden = 2, Nivel = 2 });


          context.Programa.AddOrUpdate(
             new Programa { Id = 1, Clave = "010", Descripcion = "Formación y Orientación Educativa", Tipo = "A.I.", Objetivo = "Contribuir al desarrollo de las tareas de los alumnos, padres y profesores dentro del ámbito específico de los centros escolares.", Orden = 1 },
             new Programa { Id = 2, Clave = "011", Descripcion = "Centros de Desarrollo Infantil", Tipo = "A.I.", Objetivo = "Brindar servicios de cuidado, salud, alimentación y estimulación a los hijos de las trabajadoras de la Secretaría de Educación de Veracruz de edades comprendidas entre 45 días y 5 años 11 meses.", Orden = 2 },
             new Programa { Id = 3, Clave = "012", Descripcion = "Educación Básica Nivel Preescolar", Tipo = "A.I.", Objetivo = "Atender y apoyar desde edades tempranas a los menores para favorecer el desarrollo de sus potencialidades y capacidades, lo que permitirá un mejordesarrollo personal y social.", Orden = 3 }            

          );

          context.GrupoBeneficiario.AddOrUpdate(
              new GrupoBeneficiario { Id = 1, Clave = "A", Nombre = "Adulto Mayor", Orden = 1 },
              new GrupoBeneficiario { Id = 2, Clave = "B", Nombre = "Alumno", Orden = 2 },
              new GrupoBeneficiario { Id = 3, Clave = "C", Nombre = "Artesano", Orden = 3 },
              new GrupoBeneficiario { Id = 4, Clave = "D", Nombre = "Artista", Orden = 4 },
              new GrupoBeneficiario { Id = 5, Clave = "E", Nombre = "Contribuyente", Orden = 5 },
              new GrupoBeneficiario { Id = 6, Clave = "F", Nombre = "Damnificado", Orden = 6 }

          );

          context.CriterioPriorizacion.AddOrUpdate(
            new CriterioPriorizacion { Id = 1, Clave = "CP001", Nombre = "Terminación de obra", Orden = 1 } ,
            new CriterioPriorizacion { Id = 2, Clave = "CP002", Nombre = "Obras y acciones en PARIPASSU", Orden = 2 } , 
            new CriterioPriorizacion { Id = 3, Clave = "CP003", Nombre = "Obras y acciones nuevas", Orden = 3 } , 
            new CriterioPriorizacion { Id = 4, Clave = "CP004", Nombre = "Estudios y proyectos", Orden = 4 } , 
            new CriterioPriorizacion { Id = 5, Clave = "CP005", Nombre = "Obras y acciones nuevas que en el mismo ejercicio contemplen los proyectos", Orden = 5 } 
          );


          context.Plantilla.AddOrUpdate(

            new Plantilla { Id = 1, Clave = "P01000000", Descripcion = "Plantilla inicial", Orden = 1 },
            new Plantilla { Id = 2, Clave = "P02000000", Descripcion = "Planeación", Orden = 2 },
            new Plantilla { Id = 3, Clave = "P03000000", Descripcion = "Ejecución", Orden = 3 },

            new Plantilla { Id = 4, Clave = "P02010000", Descripcion = "Plan de Desarrollo Estatal Urbano", Orden = 1, DependeDeId = 2 },
            new Plantilla { Id = 5, Clave = "P02020000", Descripcion = "Anteproyecto y normatividad", Orden = 2, DependeDeId = 2 },
            new Plantilla { Id = 6, Clave = "P02030000", Descripcion = "Fondo y programa", Orden = 3, DependeDeId = 2 },
            new Plantilla { Id = 7, Clave = "P02040000", Descripcion = "Proyecto Ejecutivo y Proyecto Base", Orden = 4, DependeDeId = 2 },
            new Plantilla { Id = 8, Clave = "P02050000", Descripcion = "Tipo de Adjudicación", Orden = 5, DependeDeId = 2 },
            new Plantilla { Id = 9, Clave = "P02060000", Descripcion = "Presupuesto Autorizado Contrato", Orden = 6, DependeDeId = 2 },
            new Plantilla { Id = 10, Clave = "P02070000", Descripcion = "Administración Directa", Orden = 7, DependeDeId = 2 },

            new Plantilla { Id = 11, Clave = "P03010000", Descripcion = "Control técnico financiero", Orden = 1, DependeDeId = 3 },
            new Plantilla { Id = 12, Clave = "P03020000", Descripcion = "Bitácora electrónica - convencional", Orden = 2, DependeDeId = 3 },
            new Plantilla { Id = 13, Clave = "P03030000", Descripcion = "Supervisión y estimaciones", Orden = 3, DependeDeId = 3 },
            new Plantilla { Id = 14, Clave = "P03040000", Descripcion = "Convenios prefiniquitos", Orden = 4, DependeDeId = 3 },
            new Plantilla { Id = 15, Clave = "P03050000", Descripcion = "Finiquito", Orden = 5, DependeDeId = 3 },
            new Plantilla { Id = 16, Clave = "P03060000", Descripcion = "Acta entrega recepción", Orden = 6, DependeDeId = 3 },           
            new Plantilla { Id = 17, Clave = "P03070000", Descripcion = "Documentación de gestión de recursos", Orden = 7, DependeDeId = 3 },

            new Plantilla { Id = 19, Clave = "P02050100", Descripcion = "Adjudicación directa", Orden = 1, DependeDeId = 8 },
            new Plantilla { Id = 20, Clave = "P02050200", Descripcion = "Licitación pública", Orden = 2, DependeDeId = 8 },

            new Plantilla { Id = 21, Clave = "P02050201", Descripcion = "Adjudicación por excepción de ley", Orden = 1, DependeDeId = 19 },
            new Plantilla { Id = 22, Clave = "P02050202", Descripcion = "Invitación a cuando menos tres personas", Orden = 2, DependeDeId = 19 }

          );

          context.PlantillaDetalle.AddOrUpdate(
             //Plantilla inicial
             new PlantillaDetalle { Id = 1, PlantillaId = 1, Clave = "Q001", Pregunta = "¿El importe total de la inversión se ajusta a la asignación presupuestal autorizada?", Orden = 1 },
             new PlantillaDetalle { Id = 2, PlantillaId = 1, Clave = "Q002", Pregunta = "¿Las obras o acciones corresponden al capitulo 6000 \"Infraestructura para el Desarrollo, Obra Pública y Servicios Relacionados con la Misma\"?", Orden = 2 },
             new PlantillaDetalle { Id = 3, PlantillaId = 1, Clave = "Q003", Pregunta = "¿La descripción de la obra o accón hace referencia clara de los trabajos a realizar?", Orden = 3 },
             new PlantillaDetalle { Id = 4, PlantillaId = 1, Clave = "Q004", Pregunta = "¿Las claves de los programas, subprogramas y subsubsubprogramas corresponden a la apertura programática y están de acuerdo con la descripción de la obra o acción?", Orden = 4 },
             new PlantillaDetalle { Id = 5, PlantillaId = 1, Clave = "Q005", Pregunta = "¿Las metas de la obra o acción son congruentes con el Subprograma asignado asignado y son susceptibles de medición?", Orden = 5 },
             new PlantillaDetalle { Id = 6, PlantillaId = 1, Clave = "Q006", Pregunta = "¿Los beneficiarios corresponden a la unidad de medida \"personas\"?", Orden = 6 },
             new PlantillaDetalle { Id = 7, PlantillaId = 1, Clave = "Q007", Pregunta = "¿Se especifica el nombre completo de la unidad o subunidad presupuestal?", Orden = 7 },
             new PlantillaDetalle { Id = 8, PlantillaId = 1, Clave = "Q008", Pregunta = "¿Se especifica el número progresivo y el total de hojas utilizadas?", Orden = 8 },
             new PlantillaDetalle { Id = 9, PlantillaId = 1, Clave = "Q009", Pregunta = "¿Se especifica el municipio y localidad(es) donde se realizará la obra o acción, omitiendo los términos \"varios\" y \"cobertura estatal\"?", Orden = 9 },
             new PlantillaDetalle { Id = 10, PlantillaId = 1, Clave = "Q0010", Pregunta = "¿Se especifica la modalidad de esjecución de la obra o acción?", Orden = 10 },
             new PlantillaDetalle { Id = 11, PlantillaId = 1, Clave = "Q0011", Pregunta = "¿Se especifica la situación de la obra o acción? ", Orden = 11 },
             new PlantillaDetalle { Id = 12, PlantillaId = 1, Clave = "Q0012", Pregunta = "Si la situación de la obra o acción es en \"proceso\", ¿ se especifica el número de obra asignado en el ejercicio anterior y presenta la misma modalidad de ejecución?", Orden = 12 },
             new PlantillaDetalle { Id = 13, PlantillaId = 1, Clave = "Q0013", Pregunta = "¿El importe que se registra de las acciones en proceso, es coincidente con el saldo del ejercicio anterior?", Orden = 13 },
             new PlantillaDetalle { Id = 14, PlantillaId = 1, Clave = "Q0014", Pregunta = "¿La programación de los gastos indirectos corresponden a obras por contrato que se están considerando en el POA y además el cálculo es de acuerdo al financiamiento, sin especificar beneficiarios y jornales?", Orden = 14 },
             new PlantillaDetalle { Id = 15, PlantillaId = 1, Clave = "Q0015", Pregunta = "La meta anual y el número de beneficiarios , ¿son indicativos de la descripción de la obra o acción que se registra?", Orden = 15 },
             new PlantillaDetalle { Id = 16, PlantillaId = 1, Clave = "Q0016", Pregunta = "¿El documento presenta en cada una de las hojas la antefirma del Titular de la Unidad Presupuestal, así como, su nombre, cargo y firma en la última hoja?", Orden = 16 },
             new PlantillaDetalle { Id = 17, PlantillaId = 1, Clave = "Q0017", Pregunta = "¿Se especifica la fecha de Firma?", Orden = 17 },
             new PlantillaDetalle { Id = 18, PlantillaId = 1, Clave = "Q0018", Pregunta = "Los estudios y proyectos no deben especificar beneficiarios y jornales", Orden = 18 },
             new PlantillaDetalle { Id = 19, PlantillaId = 1, Clave = "Q0019", Pregunta = "Las obras deben especificar los empleos y jornales a generar", Orden = 19 },
             new PlantillaDetalle { Id = 20, PlantillaId = 1, Clave = "Q0020", Pregunta = "El Capítulo 6000 no incluye el financiamiento para construcción de viviendas", Orden = 20 },
             new PlantillaDetalle { Id = 21, PlantillaId = 1, Clave = "Q0021", Pregunta = "En cuanto sean asignadas las obras a los fondos correspondientes, checar la normatividad (revisar los gastos indirectos, estudios y proyectos, etc.7)", Orden = 21 },

             //Plan de desarrollo estatal urbano

             new PlantillaDetalle { Id = 22, PlantillaId = 4, Clave = "Q001", Pregunta = "Plan de Desarrollo  Municipal", Orden = 1 },
             new PlantillaDetalle { Id = 23, PlantillaId = 4, Clave = "Q002", Pregunta = "Plan de Desarrollo  Estatal", Orden = 2 },
             new PlantillaDetalle { Id = 24, PlantillaId = 4, Clave = "Q003", Pregunta = "Plan de Desarrollo Nacional", Orden = 3 },
             new PlantillaDetalle { Id = 25, PlantillaId = 4, Clave = "Q004", Pregunta = "Plan de Desarrollo Urbano  Municipal", Orden = 4 },
             new PlantillaDetalle { Id = 26, PlantillaId = 4, Clave = "Q005", Pregunta = "Plan de Desarrollo Urbano  Estatal", Orden = 5 },
             new PlantillaDetalle { Id = 27, PlantillaId = 4, Clave = "Q006", Pregunta = "Plan de Desarrollo Urbano  Nacional", Orden = 6 },
             new PlantillaDetalle { Id = 28, PlantillaId = 4, Clave = "Q007", Pregunta = "Costo del Plan de Desarrollo Urbano", Orden = 7 },
             new PlantillaDetalle { Id = 29, PlantillaId = 4, Clave = "Q008", Pregunta = "Costo del Plan de Desarrollo", Orden = 8 },

             //Anteproyecto y normatividad

             new PlantillaDetalle { Id = 30, PlantillaId = 5, Clave = "Q001", Pregunta = "Anteproyecto de propuesta de inversión", Orden = 1 },
             new PlantillaDetalle { Id = 31, PlantillaId = 5, Clave = "Q002", Pregunta = "Presupuesto Estimado", Orden = 2 },
             new PlantillaDetalle { Id = 32, PlantillaId = 5, Clave = "Q003", Pregunta = "Prefactibilidad del anteproyecto por la Dependencia Normativa", Orden = 3 },
             new PlantillaDetalle { Id = 33, PlantillaId = 5, Clave = "Q004", Pregunta = "Estudio de Impacto Ambiental", Orden = 4 },
             new PlantillaDetalle { Id = 34, PlantillaId = 5, Clave = "Q005", Pregunta = "Permisos. Licencias y Afectaciones (según sea el caso)", Orden = 5 },
             new PlantillaDetalle { Id = 35, PlantillaId = 5, Clave = "Q006", Pregunta = "Acreditación de Propiedad", Orden = 6 },             
             

             //Fondo y programa

             new PlantillaDetalle { Id = 36, PlantillaId = 6, Clave = "Q001", Pregunta = "Propuesta de Fondo", Orden = 1 },            
             
             //Proyecto Ejecutivo y Proyecto Base

             new PlantillaDetalle { Id = 37, PlantillaId = 7, Clave = "Q001", Pregunta = "Proyecto Ejecutivo y/o Planos de las distintas especialidades de la Obra (según el caso)", Orden = 1 },
             new PlantillaDetalle { Id = 38, PlantillaId = 7, Clave = "Q002", Pregunta = "Costos del proyecto acordes con Aranceles del Colegio", Orden = 2 },
             new PlantillaDetalle { Id = 39, PlantillaId = 7, Clave = "Q003", Pregunta = "Validación del Proyecto por la Dependencia Normativa (según sea el caso)", Orden = 3 },
             new PlantillaDetalle { Id = 40, PlantillaId = 7, Clave = "Q004", Pregunta = "Catálogo de conceptos del proyecto ejecutivo (Aplica para contratos de modalidad a precio unitario)", Orden = 4 },
             new PlantillaDetalle { Id = 41, PlantillaId = 7, Clave = "Q005", Pregunta = "Lista de Partidas (Aplica para contratos de modalidad a precio alzado)", Orden = 5 },
             new PlantillaDetalle { Id = 42, PlantillaId = 7, Clave = "Q006", Pregunta = "Presupuesto base (Obras por contrato y administración directa)", Orden = 6 },
             new PlantillaDetalle { Id = 43, PlantillaId = 7, Clave = "Q007", Pregunta = "Análisis de precios unitarios del presupuesto base (obras por contrato y administración directa)", Orden = 7 },
             new PlantillaDetalle { Id = 43, PlantillaId = 7, Clave = "Q008", Pregunta = "Programa de ejecución de obra  (Obras por Contrato y Administración Directa).", Orden = 7 },
             

             //Tipo de Adjudicación - Adjudicación directa

             new PlantillaDetalle { Id = 44, PlantillaId = 19, Clave = "Q001", Pregunta = "Evidencia de la calificación del presupuesto contratado", Orden = 1 },
             new PlantillaDetalle { Id = 45, PlantillaId = 19, Clave = "Q002", Pregunta = "Acta de adjudicación", Orden = 2 },
             new PlantillaDetalle { Id = 46, PlantillaId = 19, Clave = "Q003", Pregunta = "Descripcion de la planeacion integral del licitante para realizar los trabajos", Orden = 3 },

             //Tipo de Adjudicación - Adjudicación directa - Adjudicacion por excepción de Ley

             new PlantillaDetalle { Id = 47, PlantillaId = 21, Clave = "Q004", Pregunta = "Dictamen de excepción de Ley firmado por el títular del área responsable de la ejecución de los trabajos debidamente fundamentado y motivado", Orden = 4 },

             //Tipo de Adjudicación - Adjudicación directa - Invitación a cuando menos tres personas

             new PlantillaDetalle { Id = 48, PlantillaId = 22, Clave = "Q005", Pregunta = "Bases del concurso", Orden = 5 },
             new PlantillaDetalle { Id = 49, PlantillaId = 22, Clave = "Q006", Pregunta = "Oficio de invitación a participar a cuando menos tres contratistas", Orden = 6 },
             new PlantillaDetalle { Id = 50, PlantillaId = 22, Clave = "Q007", Pregunta = "Oficio de aceptación del contratista a participar a la invitación", Orden = 7 },
             new PlantillaDetalle { Id = 51, PlantillaId = 22, Clave = "Q008", Pregunta = "Acta de visita al sitio de la obra", Orden = 8 },
             new PlantillaDetalle { Id = 52, PlantillaId = 22, Clave = "Q009", Pregunta = "Acta de Junta de Aclaraciones", Orden = 9 },
             new PlantillaDetalle { Id = 53, PlantillaId = 22, Clave = "Q0010", Pregunta = "Acta de presentación y apertura de propuestas (Técnica y Económica)", Orden = 10 },
             new PlantillaDetalle { Id = 54, PlantillaId = 22, Clave = "Q0011", Pregunta = "Descripcion de la planeacion integral del licitante para realizar los trabajos", Orden = 11 },
             new PlantillaDetalle { Id = 55, PlantillaId = 22, Clave = "Q0012", Pregunta = "Análisis de las propuestas (Cuadro comparativo)", Orden = 12 },
             new PlantillaDetalle { Id = 56, PlantillaId = 22, Clave = "Q0013", Pregunta = "Dictámen Técnico", Orden = 13 },
             new PlantillaDetalle { Id = 57, PlantillaId = 22, Clave = "Q0014", Pregunta = "Acta de Adjudicación o fallo", Orden = 14 },
             new PlantillaDetalle { Id = 58, PlantillaId = 22, Clave = "Q0015", Pregunta = "Oficio de notificación del fallo", Orden = 15 },


             //Tipo de Adjudicación - Licitación pública

             new PlantillaDetalle { Id = 59, PlantillaId = 20, Clave = "Q001", Pregunta = "Bases del concurso", Orden = 1 },
             new PlantillaDetalle { Id = 60, PlantillaId = 20, Clave = "Q002", Pregunta = "Convocatoria Pública", Orden = 2 },
             new PlantillaDetalle { Id = 61, PlantillaId = 20, Clave = "Q003", Pregunta = "Acta de visita al sitio de la obra", Orden = 3 },
             new PlantillaDetalle { Id = 62, PlantillaId = 20, Clave = "Q004", Pregunta = "Acta de Junta de Aclaraciones", Orden = 4 },
             new PlantillaDetalle { Id = 63, PlantillaId = 20, Clave = "Q005", Pregunta = "Acta de presentación y apertura de propuestas (Técnica y Económica)", Orden = 5 },
             new PlantillaDetalle { Id = 64, PlantillaId = 20, Clave = "Q006", Pregunta = "Descripcion de la planeacion integral del licitante para realizar los trabajos", Orden = 6 },
             new PlantillaDetalle { Id = 65, PlantillaId = 20, Clave = "Q007", Pregunta = "Análisis de las propuestas (Cuadro comparativo)", Orden = 7 },
             new PlantillaDetalle { Id = 66, PlantillaId = 20, Clave = "Q008", Pregunta = "Dictamen técnico", Orden = 8 },
             new PlantillaDetalle { Id = 67, PlantillaId = 20, Clave = "Q009", Pregunta = "Acta de adjudicación o fallo", Orden = 9 },
             new PlantillaDetalle { Id = 68, PlantillaId = 20, Clave = "Q010", Pregunta = "Oficio de Notificación del fallo", Orden = 10 },


             //Presupuesto autorizado contrato
             new PlantillaDetalle { Id = 69, PlantillaId = 9, Clave = "Q001", Pregunta = "Presupuesto autorizado", Orden = 1 },
             new PlantillaDetalle { Id = 70, PlantillaId = 9, Clave = "Q002", Pregunta = "Contrato de Presupuesto Autorizado (según sea el caso)", Orden = 2 },
             new PlantillaDetalle { Id = 71, PlantillaId = 9, Clave = "Q003", Pregunta = "Análisis de precios unitarios del presupuesto contratado", Orden = 3 },
             new PlantillaDetalle { Id = 72, PlantillaId = 9, Clave = "Q004", Pregunta = "Analisis de cálculo de gastos indirectos del presupuesto contratado", Orden = 4 },
             new PlantillaDetalle { Id = 73, PlantillaId = 9, Clave = "Q005", Pregunta = "Programacion de pago de acuerdo a avances en tiempo definido", Orden = 5 },
             new PlantillaDetalle { Id = 74, PlantillaId = 9, Clave = "Q006", Pregunta = "Fianzas", Orden = 6 },
             new PlantillaDetalle { Id = 75, PlantillaId = 9, Clave = "Q007", Pregunta = "Veracidad de las Fianzas", Orden = 7 },
             new PlantillaDetalle { Id = 76, PlantillaId = 9, Clave = "Q008", Pregunta = "Especificaciones generales y partículares, normas, validados por la dependencia ejecutora", Orden = 8 },
             new PlantillaDetalle { Id = 77, PlantillaId = 9, Clave = "Q009", Pregunta = "Importe de Sanción por incumplimiento al contrato y/o al programa de obra", Orden = 9 },
             new PlantillaDetalle { Id = 78, PlantillaId = 9, Clave = "Q010", Pregunta = "Aviso de inicio del proceso de rescisión administrativa del contrato (según sea el caso)", Orden = 10 },
             new PlantillaDetalle { Id = 79, PlantillaId = 9, Clave = "Q010", Pregunta = "Sanciones por incumplimiento o vicios ocultos", Orden = 11 },
             

             //Administración Directa
             new PlantillaDetalle { Id = 80, PlantillaId = 10, Clave = "Q001", Pregunta = "Acuerdo de ejecución autorizado (obra por administración directa)", Orden = 1 },
             new PlantillaDetalle { Id = 81, PlantillaId = 10, Clave = "Q002", Pregunta = "Registros IMSS", Orden = 2 },
             new PlantillaDetalle { Id = 82, PlantillaId = 10, Clave = "Q003", Pregunta = "Explosión de insumos de obra", Orden = 3 },
             new PlantillaDetalle { Id = 83, PlantillaId = 10, Clave = "Q004", Pregunta = "Licitación de compra de materiales", Orden = 4 },
             new PlantillaDetalle { Id = 84, PlantillaId = 10, Clave = "Q005", Pregunta = "Licitación de Arrendamiento de maquinaria", Orden = 5 },
             new PlantillaDetalle { Id = 85, PlantillaId = 10, Clave = "Q006", Pregunta = "Listas de raya", Orden = 6 },
             new PlantillaDetalle { Id = 86, PlantillaId = 10, Clave = "Q007", Pregunta = "Comprobación de gastos", Orden = 7 },

              //Control técnico financiero
             new PlantillaDetalle { Id = 87, PlantillaId = 11, Clave = "Q001", Pregunta = "Oficio de inicio de los trabajos", Orden = 1 },
             new PlantillaDetalle { Id = 88, PlantillaId = 11, Clave = "Q002", Pregunta = "Factura de Anticipo", Orden = 2 },
             new PlantillaDetalle { Id = 89, PlantillaId = 11, Clave = "Q003", Pregunta = "Cuenta por Liquidar (CL)", Orden = 3 },
             new PlantillaDetalle { Id = 90, PlantillaId = 11, Clave = "Q004", Pregunta = "Reporte de pago a proovedores y contratistas (transferencias electrónicas)", Orden = 4 },

             //Bitácora electrónica - convencional
             new PlantillaDetalle { Id = 91, PlantillaId = 12, Clave = "Q001", Pregunta = "Bitácora de Obra Electrónica o Convencional. (según sea el caso)", Orden = 1 },
             new PlantillaDetalle { Id = 92, PlantillaId = 12, Clave = "Q002", Pregunta = "Oficio de Excepción de Bitacora Electrónica", Orden = 2 },

             //Supervisión y estimaciones
             new PlantillaDetalle { Id = 93, PlantillaId = 13, Clave = "Q001", Pregunta = "Estimaciones de Obra con soporte de la supervision de obra", Orden = 1 },
             new PlantillaDetalle { Id = 94, PlantillaId = 13, Clave = "Q002", Pregunta = "Numero Generador, Reporte Fotografico, Pruebas de laboratorio, Etc", Orden = 2 },
             new PlantillaDetalle { Id = 95, PlantillaId = 13, Clave = "Q002", Pregunta = "Dictamen Técnico de estimaciones (verifica previamente los conceptos ejecutados)", Orden = 3 },
             new PlantillaDetalle { Id = 96, PlantillaId = 13, Clave = "Q002", Pregunta = "Amortizaciones", Orden = 4 },
           
              //Convenios prefiniquitos
             new PlantillaDetalle { Id = 97, PlantillaId = 14, Clave = "Q001", Pregunta = "Convenios modificatorios a solicitud del contratista por medio de oficio y Bitácora Electrónica con 30 días de anticipación con fecha a la terminación contractual fundados y motivados.", Orden = 1 },
             new PlantillaDetalle { Id = 98, PlantillaId = 14, Clave = "Q002", Pregunta = "Documentación justificatoria del convenio: (Dictámen)", Orden = 2 },
             new PlantillaDetalle { Id = 99, PlantillaId = 14, Clave = "Q003", Pregunta = "Por tiempo de ejecución", Orden = 3 },
             new PlantillaDetalle { Id = 100, PlantillaId = 14, Clave = "Q004", Pregunta = "Convenio por Precios Unitarios.\nConvenio por cambio de Especificaciones.\nConvenio por Volúmenes con los mismos Precios Unitarios y Especificaciones.\nPor cambio de Volúmenes, Precios Unitarios y Especificaciones.\nProyecto Ejecutivo.\nPor asuntos sociales.", Orden = 4 },
             new PlantillaDetalle { Id = 101, PlantillaId = 14, Clave = "Q005", Pregunta = "Importe de Presupuesto de Convenios", Orden = 5 },
             new PlantillaDetalle { Id = 102, PlantillaId = 14, Clave = "Q006", Pregunta = "Reprogramación en funcion de los convenios", Orden = 6 },
             new PlantillaDetalle { Id = 103, PlantillaId = 14, Clave = "Q006", Pregunta = "Autorización de Precios Unitarios extraordinarios", Orden = 6 },
             new PlantillaDetalle { Id = 104, PlantillaId = 14, Clave = "Q006", Pregunta = "Solicitud de terminación anticipada de la obra por la dependencia ejecutora, o por parte de la contratista o ambas. (según sea el caso)", Orden = 7 },
             new PlantillaDetalle { Id = 105, PlantillaId = 14, Clave = "Q006", Pregunta = "Acta circunstanciada de terminación anticipada de obra (según sea el caso) con prefiniquito", Orden = 8 },

              //Finiquito
             new PlantillaDetalle { Id = 106, PlantillaId = 15, Clave = "Q001", Pregunta = "Finiquito de obra", Orden = 1 },
             new PlantillaDetalle { Id = 107, PlantillaId = 15, Clave = "Q002", Pregunta = "Resumen de Reportes de Supervision", Orden = 2 },

              //Acta entrega recepción
             new PlantillaDetalle { Id = 108, PlantillaId = 16, Clave = "Q001", Pregunta = "Aviso de Terminación de la Obra, emitido por el Contratista", Orden = 1 },
             new PlantillaDetalle { Id = 109, PlantillaId = 16, Clave = "Q002", Pregunta = "Acta de Entrega Recepción a la Dependencia Normativa", Orden = 2 },
             new PlantillaDetalle { Id = 110, PlantillaId = 16, Clave = "Q002", Pregunta = "Acta de Entrega Recepcion de la dependencia al Usuario Final", Orden = 3 },
             new PlantillaDetalle { Id = 111, PlantillaId = 16, Clave = "Q002", Pregunta = "Planos Definitivos y Manual operativo de la obra", Orden = 4 },

             //Documentación de gestión de recursos
             new PlantillaDetalle { Id = 112, PlantillaId = 17, Clave = "Q001", Pregunta = "Registro de POA", Orden = 1 },
             new PlantillaDetalle { Id = 113, PlantillaId = 17, Clave = "Q002", Pregunta = "Registro de aprobacion y liberacion de recursos", Orden = 2 },
             new PlantillaDetalle { Id = 114, PlantillaId = 17, Clave = "Q003", Pregunta = "Presupuesto Aprobado por el Congreso del Estado", Orden = 3 },
             new PlantillaDetalle { Id = 115, PlantillaId = 17, Clave = "Q004", Pregunta = "Registro de POA ajustado", Orden = 4 },
             new PlantillaDetalle { Id = 116, PlantillaId = 17, Clave = "Q005", Pregunta = "Acuerdos y Convenios o recibos con otras fuentes de financiamiento", Orden = 5 },
             new PlantillaDetalle { Id = 117, PlantillaId = 17, Clave = "Q006", Pregunta = "Oficio de Asignacion Presupuestal", Orden = 6 },
             new PlantillaDetalle { Id = 118, PlantillaId = 17, Clave = "Q007", Pregunta = "Oficio de Suficiencia Presupuestal", Orden = 7 },
             new PlantillaDetalle { Id = 119, PlantillaId = 17, Clave = "Q008", Pregunta = "Cedula Tecnica Programatica ( PROG )", Orden = 8 },
             new PlantillaDetalle { Id = 120, PlantillaId = 17, Clave = "Q009", Pregunta = "Solicitud de Modificacion de POA con Soporte", Orden = 9 },
             new PlantillaDetalle { Id = 121, PlantillaId = 17, Clave = "Q010", Pregunta = "Tramitacion de Pagos Cuentas por Liquidar ( CL ) con Soporte", Orden = 10 },
             new PlantillaDetalle { Id = 122, PlantillaId = 17, Clave = "Q011", Pregunta = "Informe Trimestrales de Avances Fisicos Financieros (AVAN)", Orden = 11 },
             new PlantillaDetalle { Id = 123, PlantillaId = 17, Clave = "Q012", Pregunta = "Oficios todo relacionado con la gestion, planeacion y ejecucion de la obra", Orden = 12 }

         );

          context.TechoFinancieroStatus.AddOrUpdate(
            new TechoFinancieroStatus { Id = 1, EjercicioId = 1, Status = 2 } ,
            new TechoFinancieroStatus { Id = 2, EjercicioId = 2, Status = 1 }   
          );                      
                    

          context.SaveChanges();

          CrearTriggers(context);      
           
        }


        private void CrearTriggers(Contexto contexto)
        {

            string sp001 = @" CREATE TRIGGER trgAsignarNumeroObra_POADetalle ON [dbo].[POADetalle] 
                                FOR INSERT
                                AS
	                               
									 declare @consecutivo int;
						             declare @UnidadPresupuestalClave varchar(9);
						             declare @anio int;
						             declare @poadetalleId int;
						             declare @poaId int;
						             declare @numeroObra varchar(100);

						             select @poaId=POAId,@poadetalleId=Id from inserted; 

                                     select

                                         @consecutivo=MAX(POADetalle.Consecutivo),							  
							             @UnidadPresupuestalClave=UnidadPresupuestal.Clave,
							             @anio=Ejercicio.Año							   

                                     from POADetalle 
                                     inner join POA
                                     on POA.Id=POADetalle.POAId
                                     inner join UnidadPresupuestal
                                     on UnidadPresupuestal.Id=POA.UnidadPresupuestalId
                                     inner join Ejercicio
                                     on Ejercicio.Id=POA.EjercicioId
                                     where POA.Id=@poaId
							         group by POA.Id,UnidadPresupuestal.Clave,Ejercicio.Año
                            
                            set @consecutivo=@consecutivo+1;                                     
							
							set @numeroObra= CAST(@UnidadPresupuestalClave AS varchar(9))  + CAST(@anio AS varchar(4)) + REPLACE(STR(@consecutivo, 3),SPACE(1),'0');

                            update POADetalle set Consecutivo=@consecutivo,Numero=@numeroObra where Id=@poadetalleId";



            contexto.Database.ExecuteSqlCommand(sp001);


            sp001 = @"CREATE TRIGGER trgAsignarNumeroObra_Obra ON [dbo].[Obra] 
                                FOR INSERT
                                AS	                               									 
						             declare @consecutivo int;
						             declare @obraId int;
						             declare @poaDetalleId int;
						             declare @numeroObra varchar(100);

						             select @poaDetalleId=POADetalleId,@obraId=Id from inserted; 

                                     select @consecutivo=Consecutivo,@numeroObra=Numero from POADetalle where Id=@poaDetalleId     
                        
                                update Obra set Consecutivo=@consecutivo,Numero=@numeroObra where Id=@obraId";



            contexto.Database.ExecuteSqlCommand(sp001);           




        } // Triggers






    }
}

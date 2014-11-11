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
                new Localidad { Id = 1, Nombre = "Primer localidad de Acajete", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 1, MunicipioId = 1 },
                new Localidad { Id = 2, Nombre = "Segunda localidad de Acajete", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 2, MunicipioId = 1 },
                new Localidad { Id = 3, Nombre = "Tercera localidad de Acajete", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 3, MunicipioId = 1 },
                new Localidad { Id = 4, Nombre = "Primer localidad de Acatlán", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 1, MunicipioId = 2 },
                new Localidad { Id = 5, Nombre = "Segunda localidad de Acatlán", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 2, MunicipioId = 2 },
                new Localidad { Id = 6, Nombre = "Primer localidad de Acayucan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 1, MunicipioId = 3 },
                new Localidad { Id = 7, Nombre = "Primer localidad de Actopan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 1, MunicipioId = 4 },
                new Localidad { Id = 8, Nombre = "Segunda localidad de Actopan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 2, MunicipioId = 4 },
                new Localidad { Id = 9, Nombre = "Tercera localidad de Actopan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 3, MunicipioId = 4 },
                new Localidad { Id = 10, Nombre = "Cuarta localidad de Actopan", Latitud = 19.54M, Longitud = 96.9275M, PoblacionFemenina = 100, PoblacionMasculina = 100, PoblacionTotal = 200, Orden = 4, MunicipioId = 4 }

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


           context.Fondos.AddOrUpdate(
             new Fondo { Id = 1, Clave = "F010", Abreviatura = "FORTAMUNDF", Nombre = "Fondo de Aportaciones para el Fortalecimiento de los Municipios y Demarcaciones Territoriales del Distrito Federal", Orden = 1 },
             new Fondo { Id = 2, Clave = "F020", Abreviatura = "FAIS", Nombre = "Fondo para la Infraestructura Social", Orden = 2 },
             new Fondo { Id = 3, Clave = "F030", Abreviatura = "Otros", Nombre = "Otros fondos", Orden = 3 }
           );

           Fondo fais = context.Fondos.Local.FirstOrDefault(f => f.Clave == "F010");
           fais.DetalleSubFondos.Add(new Fondo { Id = 4, Clave = "F011", Abreviatura = "FISE", Nombre = "Fondo para la Infraestructura Social Estatal", Orden = 1 });
           fais.DetalleSubFondos.Add(new Fondo { Id = 5, Clave = "F012", Abreviatura = "FISM", Nombre = "Fondo para la Infraestructura Social Municipal ", Orden = 2 });

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
               new AperturaProgramatica { Id = 1, Clave = "SC", Nombre = "Agua y saneamiento (Agua potable)", Orden = 1, EjercicioId = 2,Nivel=1 },
               new AperturaProgramatica { Id = 2, Clave = "SD", Nombre = "Agua y saneamiento (Drenaje)", Orden = 2, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 3, Clave = "SE", Nombre = "Urbanización municipal", Orden = 3, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 4, Clave = "SG", Nombre = "Electrificación", Orden = 4, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 5, Clave = "SO", Nombre = "Salud", Orden = 5, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 6, Clave = "SJ", Nombre = "Educación", Orden = 6, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 7, Clave = "SH", Nombre = "Vivienda", Orden = 7, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 8, Clave = "UB", Nombre = "Caminos rurales", Orden = 8, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 9, Clave = "IR", Nombre = "Infraestructura productiva rural", Orden = 9, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 10, Clave = "UM", Nombre = "Equipamiento urbano", Orden = 10, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 11, Clave = "PE", Nombre = "Protección y preservación ecológica", Orden = 11, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 12, Clave = "BE", Nombre = "Bienes muebles", Orden = 12, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 13, Clave = "BI", Nombre = "Bienes inmuebles", Orden = 13, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 14, Clave = "PM", Nombre = "Planeación municipal", Orden = 14, EjercicioId = 2, Nivel = 1 },
               new AperturaProgramatica { Id = 15, Clave = "SB", Nombre = "Estímulos a la educación", Orden = 15, EjercicioId = 2, Nivel = 1 }
            );

           AperturaProgramatica sc = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SC");

           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 16, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 17, Clave = "02", Nombre = "Ampliación", Orden = 2, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 18, Clave = "03", Nombre = "Construcción", Orden = 3, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 19, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 20, Clave = "05", Nombre = "Equipamiento", Orden = 5, EjercicioId = 2, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 21, Clave = "06", Nombre = "Sustitución", Orden = 6, EjercicioId = 2, Nivel = 2 });

           AperturaProgramatica sc_rehabilitacion = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Id == 16);

           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 22, Clave = "a", Nombre = "Planta potabilizadora", Orden = 1, EjercicioId = 2, Nivel = 3,EsObraOAccion=enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 23, Clave = "b", Nombre = "Pozo profundo de agua potable", Orden = 2, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 24, Clave = "c", Nombre = "Deposito o tanque de agua potable", Orden = 3, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 25, Clave = "d", Nombre = "Linea de conducción", Orden = 4, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 26, Clave = "e", Nombre = "Red de agua potable", Orden = 5, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 27, Clave = "f", Nombre = "Sistema integral de agua potable", Orden = 6, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 28, Clave = "g", Nombre = "Carcamo", Orden = 7, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 29, Clave = "h", Nombre = "Norias", Orden = 8, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 30, Clave = "i", Nombre = "Pozo artesiano", Orden = 9, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 31, Clave = "j", Nombre = "Olla de captación de agua pluvial", Orden = 10, EjercicioId = 2, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
                   

          context.AperturaProgramaticaMetas.AddOrUpdate(
              new AperturaProgramaticaMeta { Id = 1,AperturaProgramaticaId = 22, AperturaProgramaticaUnidadId = 1, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 2, AperturaProgramaticaId = 23, AperturaProgramaticaUnidadId = 2, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 3, AperturaProgramaticaId = 24, AperturaProgramaticaUnidadId = 3, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 4, AperturaProgramaticaId = 25, AperturaProgramaticaUnidadId = 4, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 5, AperturaProgramaticaId = 26, AperturaProgramaticaUnidadId = 4, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 6, AperturaProgramaticaId = 27, AperturaProgramaticaUnidadId = 5, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 7, AperturaProgramaticaId = 28, AperturaProgramaticaUnidadId = 6, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 8, AperturaProgramaticaId = 29, AperturaProgramaticaUnidadId = 6, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 9, AperturaProgramaticaId = 30, AperturaProgramaticaUnidadId = 7, AperturaProgramaticaBeneficiarioId = 1 },
              new AperturaProgramaticaMeta { Id = 10, AperturaProgramaticaId = 31, AperturaProgramaticaUnidadId = 8, AperturaProgramaticaBeneficiarioId = 1 }
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
            new Plantilla { Id = 1, Clave = "P001", Descripcion="Plantilla inicial",Orden=1 },
            new Plantilla { Id = 2, Clave = "P002", Descripcion="Plan de Desarrollo Urbano",Orden=2 },

            new Plantilla { Id = 3, Clave = "P003", Descripcion = "Normatividad Fondo-Programa", Orden = 3 },
            new Plantilla { Id = 4, Clave = "P004", Descripcion = "Anteproyecto y Costo Estimado", Orden = 4 },
            new Plantilla { Id = 5, Clave = "P005", Descripcion = "Proyecto Ejecutivo y PB", Orden = 5 },
            new Plantilla { Id = 6, Clave = "P006", Descripcion = "Tipo de Adjudicación", Orden = 6 },
            new Plantilla { Id = 7, Clave = "P007", Descripcion = "Presupuesto Autorizado Contrato", Orden = 7 },
            new Plantilla { Id = 8, Clave = "P008", Descripcion = "Administración Directa", Orden = 8 }
           



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

             //Plan de desarrollo urbano

             new PlantillaDetalle { Id = 22, PlantillaId = 2, Clave = "Q001", Pregunta = "¿Se cuenta con el Plan de Desarrollo Urbano?", Orden = 1 },
             new PlantillaDetalle { Id = 23, PlantillaId = 2, Clave = "Q002", Pregunta = "Costo del Plan de Desarrollo", Orden = 2 },
             new PlantillaDetalle { Id = 24, PlantillaId = 2, Clave = "Q003", Pregunta = "Factibilidad del proyecto por la Dependencia Normativa (según sea el caso)", Orden = 3 },
             new PlantillaDetalle { Id = 25, PlantillaId = 2, Clave = "Q004", Pregunta = "Validación del Proyecto por la Dependencia Normativa (según sea el caso)", Orden = 4 },
             new PlantillaDetalle { Id = 26, PlantillaId = 2, Clave = "Q005", Pregunta = "Estudio de Impacto Ambiental Validado", Orden = 5 },
             new PlantillaDetalle { Id = 27, PlantillaId = 2, Clave = "Q006", Pregunta = "Permisos. Licencias y Afectaciones (según sea el caso)", Orden = 6 },
             new PlantillaDetalle { Id = 28, PlantillaId = 2, Clave = "Q007", Pregunta = "Descripción de la planeacón integral del licitante para realizar los trabajos (Aplica para contratos de modalidad a precio unitario)", Orden = 7 },
             new PlantillaDetalle { Id = 29, PlantillaId = 1, Clave = "Q008", Pregunta = "Términos de referencia (Aplica para contratos de modalidad a precio alzado)", Orden = 8 },

             //Normatividad Fondo-Programa
             new PlantillaDetalle { Id = 30, PlantillaId = 3, Clave = "Q001", Pregunta = "Dictamen que dé corroboración del Fenómeno Natural Perturbador", Orden = 1 },
             new PlantillaDetalle { Id = 31, PlantillaId = 3, Clave = "Q002", Pregunta = "Solicitud de Declaración de Desastre Natural", Orden = 2 },
             new PlantillaDetalle { Id = 32, PlantillaId = 3, Clave = "Q003", Pregunta = "Publicación del Desastre Natural en el Diario Oficial de la Federación", Orden = 3 },
             new PlantillaDetalle { Id = 33, PlantillaId = 3, Clave = "Q004", Pregunta = "Acta de Entrega de Resultados del Comité de Evaluación de Daños", Orden = 4 },
             new PlantillaDetalle { Id = 34, PlantillaId = 3, Clave = "Q005", Pregunta = "Acta de Instalacón del Comité de Evaluación de Resultados", Orden = 5 },
             new PlantillaDetalle { Id = 35, PlantillaId = 3, Clave = "Q006", Pregunta = "Solicitud de los Recursos con cargo al FONDEN", Orden = 6 },
             new PlantillaDetalle { Id = 36, PlantillaId = 3, Clave = "Q007", Pregunta = "Acuerdo de la Comisión donde se recomienda a la Secretaría autorice recursos con cargo al FONDEN", Orden = 7 },
             new PlantillaDetalle { Id = 37, PlantillaId = 3, Clave = "Q008", Pregunta = "Oficio de Disponibilidad Presupuestal de la Transferencia de Recursos", Orden = 8 },
             new PlantillaDetalle { Id = 38, PlantillaId = 3, Clave = "Q009", Pregunta = "Solicitud de Excepción de impacto ambiental para OBRA FONDEN (Oficio de respuesta sobre la Exceptuación Técnica)", Orden = 9 },
             new PlantillaDetalle { Id = 39, PlantillaId = 3, Clave = "Q0010", Pregunta = "Normatividad del Fondo o Programa (Lineamientos del Recurso)", Orden = 10 },


             //Anteproyecto y Costo Estimado
              new PlantillaDetalle { Id = 40, PlantillaId = 4, Clave = "Q001", Pregunta = "Anteproyecto de propuesta de inversión", Orden = 1 },
             new PlantillaDetalle { Id = 41, PlantillaId = 4, Clave = "Q002", Pregunta = "Oficio de aprobación de Inversión", Orden = 2 },
             new PlantillaDetalle { Id = 42, PlantillaId = 4, Clave = "Q003", Pregunta = "Cédula Técnica Programática-Presupuestal", Orden = 3 },
             
             //Proyecto Ejecutivo y PB
             new PlantillaDetalle { Id = 43, PlantillaId = 5, Clave = "Q001", Pregunta = "Proyecto Ejecutivo y/o Planos actualizados (según sea el caso)", Orden = 1 },
             new PlantillaDetalle { Id = 44, PlantillaId = 5, Clave = "Q002", Pregunta = "Costos del proyecto acordes con Aranceles Colegiales", Orden = 2 },
             new PlantillaDetalle { Id = 45, PlantillaId = 5, Clave = "Q003", Pregunta = "Acreditación de Propiedad", Orden = 3 },
             new PlantillaDetalle { Id = 46, PlantillaId = 5, Clave = "Q004", Pregunta = "Catálogo de conceptos de la ejecutora (Aplica para contratos de modalidad a precio unitario)", Orden = 4 },
             new PlantillaDetalle { Id = 47, PlantillaId = 5, Clave = "Q005", Pregunta = "Lista de Partidas (Aplica para contratos de modalidad a precio alzado)", Orden = 5 },
             new PlantillaDetalle { Id = 48, PlantillaId = 5, Clave = "Q006", Pregunta = "Presupuesto base de la Ejecutora (Obras por contrato y administración directa)", Orden = 6 },
             new PlantillaDetalle { Id = 49, PlantillaId = 5, Clave = "Q007", Pregunta = "Análisis de precios unitarios del presupuesto base (obras por contrato y administración directa)", Orden = 7 },

             //Tipo de Adjudicación
             new PlantillaDetalle { Id = 50, PlantillaId = 6, Clave = "Q001", Pregunta = "Evidencia de la calificación del presupuesto contratado", Orden = 1 },
             new PlantillaDetalle { Id = 51, PlantillaId = 6, Clave = "Q002", Pregunta = "Escrito de fundamentación y motivación de las excepciones de ley firmado por el titular del área responsable de la ejecución de los trabajos", Orden = 2 },
             new PlantillaDetalle { Id = 52, PlantillaId = 6, Clave = "Q003", Pregunta = "Oficio de invitación a cuando menos tres contratistas", Orden = 3 },
             new PlantillaDetalle { Id = 53, PlantillaId = 6, Clave = "Q004", Pregunta = "Oficio de aceptación del contratista  a participar en la invitación", Orden = 4 },
             new PlantillaDetalle { Id = 54, PlantillaId = 6, Clave = "Q005", Pregunta = "Bases del concurso", Orden = 5 },
             new PlantillaDetalle { Id = 55, PlantillaId = 6, Clave = "Q006", Pregunta = "Acta de visita al sitio de la obra", Orden = 6 },
             new PlantillaDetalle { Id = 56, PlantillaId = 6, Clave = "Q007", Pregunta = "Análisis de precios unitarios del presupuesto base (obras por contrato y administración directa)", Orden = 7 },
             new PlantillaDetalle { Id = 57, PlantillaId = 6, Clave = "Q008", Pregunta = "Acta de Junta de Aclaraciones ", Orden = 8 },
             new PlantillaDetalle { Id = 58, PlantillaId = 6, Clave = "Q009", Pregunta = "Invitación al Órgano de Control para el Acto de presentación y apertura de proposiciones en caso de Invitación a cuando menos tres personas", Orden = 9 },
             new PlantillaDetalle { Id = 59, PlantillaId = 6, Clave = "Q0010", Pregunta = "Acta de presentación y apertura de propuestas (Técnica y Económica)", Orden = 10 },
             new PlantillaDetalle { Id = 60, PlantillaId = 6, Clave = "Q0011", Pregunta = "Análisis de las propuestas (Cuadro comparativo)", Orden = 11 },
             new PlantillaDetalle { Id = 61, PlantillaId = 6, Clave = "Q0012", Pregunta = "Si la situación de la obra o acción es en \"proceso\", ¿ se especifica el número de obra asignado en el ejercicio anterior y presenta la misma modalidad de ejecución?", Orden = 12 },
             new PlantillaDetalle { Id = 62, PlantillaId = 6, Clave = "Q0013", Pregunta = "Dictámen Técnico", Orden = 13 },
             new PlantillaDetalle { Id = 63, PlantillaId = 6, Clave = "Q0014", Pregunta = "Acta de Adjudicación o fallo", Orden = 14 },
             new PlantillaDetalle { Id = 64, PlantillaId = 6, Clave = "Q0015", Pregunta = "Oficio de notificación del fallo", Orden = 15 },
             new PlantillaDetalle { Id = 65, PlantillaId = 6, Clave = "Q0016", Pregunta = "Convocatoria pública", Orden = 16 },

             //Presupuesto Autorizado Contrato
             new PlantillaDetalle { Id = 66, PlantillaId = 7, Clave = "Q001", Pregunta = "Presupuesto contratado", Orden = 1 },
             new PlantillaDetalle { Id = 67, PlantillaId = 7, Clave = "Q002", Pregunta = "Análisis de precios unitarios del presupuesto contratado", Orden = 2 },
             new PlantillaDetalle { Id = 68, PlantillaId = 7, Clave = "Q003", Pregunta = "Analisis de cálculo de gastos indirectos del presupuesto contratado", Orden = 3 },

             //Administración Directa
             new PlantillaDetalle { Id = 69, PlantillaId = 8, Clave = "Q001", Pregunta = "Acuerdo de ejecución autorizado (obra por administración directa)", Orden = 1 },
             new PlantillaDetalle { Id = 70, PlantillaId = 8, Clave = "Q002", Pregunta = "Registros IMSS", Orden = 2 },
             new PlantillaDetalle { Id = 71, PlantillaId = 8, Clave = "Q003", Pregunta = "Explosión de insumos de obra", Orden = 3 },
             new PlantillaDetalle { Id = 72, PlantillaId = 8, Clave = "Q004", Pregunta = "Licitación de compra de materiales", Orden = 4 },
             new PlantillaDetalle { Id = 73, PlantillaId = 8, Clave = "Q005", Pregunta = "Licitación de Arrendamiento de maquinaria", Orden = 5 },
             new PlantillaDetalle { Id = 74, PlantillaId = 8, Clave = "Q006", Pregunta = "Listas de raya", Orden = 6 },
             new PlantillaDetalle { Id = 75, PlantillaId = 8, Clave = "Q007", Pregunta = "Comprobación de gastos", Orden = 7 }

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

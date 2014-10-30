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
                new Rol { Id = 4 , Clave="R004", Nombre="Capturista", Orden=4}              
            );
                        

            context.Usuarios.AddOrUpdate(

               new Usuario { Id = 1, Login = "desarrollador", Password = "desarrollador", Nombre = "Usuario Desarrollador", Activo = true },
               new Usuario { Id = 2, Login = "ejecutivo", Password = "ejecutivo", Nombre = "Usuario Ejecutivo", Activo = true },
               new Usuario { Id = 3, Login = "admin", Password = "admin", Nombre = "Usuario Administrador", Activo = true },
               new Usuario { Id = 4, Login = "sedarpa", Password = "sedarpa", Nombre = "Usuario de SEDARPA", Activo = true },
               new Usuario { Id = 5, Login = "iiev", Password = "iiev", Nombre = "Usuario de IIEV", Activo = true },
               new Usuario { Id = 6, Login = "inverbio", Password = "inverbio", Nombre = "Usuario de INVERBIO", Activo = true }               
            );

            context.UsuarioRoles.AddOrUpdate(

                new UsuarioRol { Id = 1, UsuarioId = 1, RolId = 1 },
                new UsuarioRol { Id = 2, UsuarioId = 2, RolId = 2 },
                new UsuarioRol { Id = 3, UsuarioId = 3, RolId = 3 },
                new UsuarioRol { Id = 4, UsuarioId = 4, RolId = 4 },
                new UsuarioRol { Id = 5, UsuarioId = 5, RolId = 4 },
                new UsuarioRol { Id = 6, UsuarioId = 6, RolId = 4 }
            );

            context.OpcionesSistema.AddOrUpdate(
            
                new OpcionSistema { Id = 1, Clave = "OS001", Descripcion = "Captura del proyecto de POA",Activo=true,Orden=1},
                new OpcionSistema { Id = 2, Clave = "OS002", Descripcion = "Ajuste del POA",Activo=true,Orden=2},
                new OpcionSistema { Id = 3, Clave = "OS003", Descripcion = "Cat�logos",Activo=true,Orden=3},
                new OpcionSistema { Id = 4, Clave = "OS004", Descripcion = "Cat�logo de Unidades presupuestales", Activo = true, Orden = 1,ParentId=3 },
                new OpcionSistema { Id = 5, Clave = "OS005", Descripcion = "Cat�logo de Fondos", Activo = true, Orden = 2, ParentId = 3 },
                new OpcionSistema { Id = 6, Clave = "OS006", Descripcion = "Cat�logo de Apertura programatica", Activo = true, Orden = 3, ParentId = 3 },
                new OpcionSistema { Id = 7, Clave = "OS007", Descripcion = "Cat�logo de Municipios", Activo = true, Orden = 4, ParentId = 3 },
                new OpcionSistema { Id = 8, Clave = "OS008", Descripcion = "Cat�logo de Ejercicios", Activo = true, Orden = 5, ParentId = 3 },
                new OpcionSistema { Id = 9, Clave = "OS009", Descripcion = "Cat�logo de Usuarios", Activo = true, Orden = 6, ParentId = 3 }

            );

            context.Permisos.AddOrUpdate(

                new Permiso { Id = 1, RolId = 3, OpcionSistemaId = 4, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
                new Permiso { Id = 2, RolId = 3, OpcionSistemaId = 5, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
                new Permiso { Id = 3, RolId = 3, OpcionSistemaId = 6, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
                new Permiso { Id = 4, RolId = 4, OpcionSistemaId = 1, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle | enumOperaciones.Borrar },
                new Permiso { Id = 5, RolId = 4, OpcionSistemaId = 2, Operaciones = enumOperaciones.Agregar | enumOperaciones.Editar | enumOperaciones.Detalle }
                
            );            
            

            context.Ejercicios.AddOrUpdate(

               new Ejercicio { Id = 1, A�o = 2009, FactorIva = 1.5M, Estatus = enumEstatusEjercicio.Cerrado },
               new Ejercicio { Id = 2, A�o = 2010, FactorIva = 1.5M, Estatus = enumEstatusEjercicio.Cerrado },
               new Ejercicio { Id = 3, A�o = 2011, FactorIva = 1.5M, Estatus = enumEstatusEjercicio.Cerrado },
               new Ejercicio { Id = 4, A�o = 2012, FactorIva = 1.5M, Estatus = enumEstatusEjercicio.Cerrado },
               new Ejercicio { Id = 5, A�o = 2013, FactorIva = 1.5M, Estatus = enumEstatusEjercicio.Cerrado },
               new Ejercicio { Id = 6, A�o = 2014, FactorIva = 1.6M, Estatus = enumEstatusEjercicio.Activo  },
               new Ejercicio { Id = 7, A�o = 2015, FactorIva = 1.6M, Estatus = enumEstatusEjercicio.Nuevo   }
               
            );

            context.UnidadesPresupuestales.AddOrUpdate(

              new UnidadPresupuestal { Id = 1, Clave = "102S11001", Abreviatura = "SEDARPA", Nombre = "Secretaria de Desarrollo Agropecuario, Rural y Pesca", Orden = 1 },
              new UnidadPresupuestal { Id = 2, Clave = "104C80803", Abreviatura = "IIEV", Nombre = "Instituto de Espacios Educativos", Orden = 2 },
              new UnidadPresupuestal { Id = 3, Clave = "104S80801", Abreviatura = "COBAEV", Nombre = "Colegio de Bachilleres del Estado de Veracruz", Orden = 3 }
              
           );

            UnidadPresupuestal sedarpa = context.UnidadesPresupuestales.Local.FirstOrDefault(u => u.Clave == "102S11001");

            sedarpa.DetalleSubUnidadesPresupuestales.Add(new UnidadPresupuestal { Id = 4, Clave = "102S80808", Abreviatura = "CODEPAP", Nombre = "Consejo de desarrollo del Papaloapan", Orden = 1 });
            sedarpa.DetalleSubUnidadesPresupuestales.Add(new UnidadPresupuestal { Id = 5, Clave = "102S80809", Abreviatura = "INVERBIO", Nombre = "Instituto Veracruzano de Bioenerg�ticos", Orden = 2 });



            Usuario usedarpa = context.Usuarios.Local.FirstOrDefault(u => u.Login == "sedarpa");
            Usuario uiiev = context.Usuarios.Local.FirstOrDefault(u => u.Login == "iiev");
            Usuario uinverbio = context.Usuarios.Local.FirstOrDefault(u => u.Login == "inverbio");

            usedarpa.DetalleUnidadesPresupuestales.Add(new UsuarioUnidadPresupuestal { UnidadPresupuestalId=1});
            uiiev.DetalleUnidadesPresupuestales.Add(new UsuarioUnidadPresupuestal { UnidadPresupuestalId = 2 });
            uinverbio.DetalleUnidadesPresupuestales.Add(new UsuarioUnidadPresupuestal { UnidadPresupuestalId = 5 });




            context.Municipios.AddOrUpdate(              
              new Municipio {Id=1,Clave="M001",Nombre="Acajete",Orden=1 },
              new Municipio {Id=2,Clave="M002",Nombre="Acatl�n",Orden=2 },
              new Municipio {Id=3,Clave="M003",Nombre="Acayucan",Orden=3 },
              new Municipio {Id=4,Clave="M004",Nombre="Actopan",Orden=4 }              
            );

            context.TiposLocalidad.AddOrUpdate(
             new TipoLocalidad { Id = 1,Clave="TL001",Nombre="Poblado urbano",Orden=1},
             new TipoLocalidad { Id = 2,Clave="TL002",Nombre="Poblado rural",Orden=2},
             new TipoLocalidad { Id = 3,Clave="TL003",Nombre="Colonia popular",Orden=3},
             new TipoLocalidad { Id = 4,Clave="TL004",Nombre="Poblado ind�gena",Orden=4}
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

           context.A�os.AddOrUpdate(

              new A�o { Id = 1, Anio = 2008 },
              new A�o { Id = 2, Anio = 2009 },
              new A�o { Id = 3 ,Anio = 2010 },
              new A�o { Id = 4 ,Anio = 2011 },
              new A�o { Id = 5 ,Anio = 2012 },
              new A�o { Id = 6 ,Anio = 2013 },
              new A�o { Id = 7 ,Anio = 2014 },
              new A�o { Id = 8 ,Anio = 2015 }
             
          );

           //var list = from a�o in context.A�os.Local
           //           from mf in context.ModalidadesFinanciamiento.Local
           //           from f in context.Fondos.Local
           //           select new { a�o, mf, f };

           //foreach (var item in list)
           //{
           //    context.Financiamientos.Local.Add(new Financiamiento { A�o = item.a�o, ModalidadFinanciamiento = item.mf, Fondo = item.f });
           //}


           context.Financiamientos.AddOrUpdate(
             new Financiamiento { Id = 1,  A�oId = 6, FondoId = 1, ModalidadFinanciamientoId = 1 }, 
             new Financiamiento { Id = 2,  A�oId = 6, FondoId = 1, ModalidadFinanciamientoId = 2 },   
             new Financiamiento { Id = 3,  A�oId = 6, FondoId = 1, ModalidadFinanciamientoId = 3 },   
             new Financiamiento { Id = 4,  A�oId = 6, FondoId = 1, ModalidadFinanciamientoId = 4 },
             new Financiamiento { Id = 5,  A�oId = 6, FondoId = 5, ModalidadFinanciamientoId = 1 },
             new Financiamiento { Id = 6,  A�oId = 6, FondoId = 5, ModalidadFinanciamientoId = 2 },
             new Financiamiento { Id = 7,  A�oId = 6, FondoId = 5, ModalidadFinanciamientoId = 3 },
             new Financiamiento { Id = 8,  A�oId = 6, FondoId = 5, ModalidadFinanciamientoId = 4 },
             new Financiamiento { Id = 9,  A�oId = 7, FondoId = 1, ModalidadFinanciamientoId = 1 },
             new Financiamiento { Id = 10, A�oId = 7, FondoId = 1, ModalidadFinanciamientoId = 2 },
             new Financiamiento { Id = 11, A�oId = 7, FondoId = 1, ModalidadFinanciamientoId = 3 },
             new Financiamiento { Id = 12, A�oId = 7, FondoId = 1, ModalidadFinanciamientoId = 4 },  
             new Financiamiento { Id = 13, A�oId = 7, FondoId = 5, ModalidadFinanciamientoId = 1 },
             new Financiamiento { Id = 14, A�oId = 7, FondoId = 5, ModalidadFinanciamientoId = 2 },
             new Financiamiento { Id = 15, A�oId = 7, FondoId = 5, ModalidadFinanciamientoId = 3 },
             new Financiamiento { Id = 16, A�oId = 7, FondoId = 5, ModalidadFinanciamientoId = 4 }  
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
               new AperturaProgramatica { Id = 1, Clave = "SC", Nombre = "Agua y saneamiento (Agua potable)", Orden = 1, EjercicioId = 7,Nivel=1 },
               new AperturaProgramatica { Id = 2, Clave = "SD", Nombre = "Agua y saneamiento (Drenaje)", Orden = 2, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 3, Clave = "SE", Nombre = "Urbanizaci�n municipal", Orden = 3, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 4, Clave = "SG", Nombre = "Electrificaci�n", Orden = 4, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 5, Clave = "SO", Nombre = "Salud", Orden = 5, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 6, Clave = "SJ", Nombre = "Educaci�n", Orden = 6, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 7, Clave = "SH", Nombre = "Vivienda", Orden = 7, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 8, Clave = "UB", Nombre = "Caminos rurales", Orden = 8, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 9, Clave = "IR", Nombre = "Infraestructura productiva rural", Orden = 9, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 10, Clave = "UM", Nombre = "Equipamiento urbano", Orden = 10, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 11, Clave = "PE", Nombre = "Protecci�n y preservaci�n ecol�gica", Orden = 11, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 12, Clave = "BE", Nombre = "Bienes muebles", Orden = 12, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 13, Clave = "BI", Nombre = "Bienes inmuebles", Orden = 13, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 14, Clave = "PM", Nombre = "Planeaci�n municipal", Orden = 14, EjercicioId = 7, Nivel = 1 },
               new AperturaProgramatica { Id = 15, Clave = "SB", Nombre = "Est�mulos a la educaci�n", Orden = 15, EjercicioId = 7, Nivel = 1 }
            );

           AperturaProgramatica sc = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SC");

           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 16, Clave = "01", Nombre = "Rehabilitaci�n", Orden = 1, EjercicioId = 7, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 17, Clave = "02", Nombre = "Ampliaci�n", Orden = 2, EjercicioId = 7, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 18, Clave = "03", Nombre = "Construcci�n", Orden = 3, EjercicioId = 7, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 19, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 7, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 20, Clave = "05", Nombre = "Equipamiento", Orden = 5, EjercicioId = 7, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 21, Clave = "06", Nombre = "Sustituci�n", Orden = 6, EjercicioId = 7, Nivel = 2 });

           AperturaProgramatica sc_rehabilitacion = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Id == 16);

           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 22, Clave = "a", Nombre = "Planta potabilizadora", Orden = 1, EjercicioId = 7, Nivel = 3,EsObraOAccion=enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 23, Clave = "b", Nombre = "Pozo profundo de agua potable", Orden = 2, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 24, Clave = "c", Nombre = "Deposito o tanque de agua potable", Orden = 3, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 25, Clave = "d", Nombre = "Linea de conducci�n", Orden = 4, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 26, Clave = "e", Nombre = "Red de agua potable", Orden = 5, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 27, Clave = "f", Nombre = "Sistema integral de agua potable", Orden = 6, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 28, Clave = "g", Nombre = "Carcamo", Orden = 7, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 29, Clave = "h", Nombre = "Norias", Orden = 8, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 30, Clave = "i", Nombre = "Pozo artesiano", Orden = 9, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 31, Clave = "j", Nombre = "Olla de captaci�n de agua pluvial", Orden = 10, EjercicioId = 7, Nivel = 3, EsObraOAccion = enumObraAccion.Obra });
                   

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
            new Funcionalidad { Id = 3, Clave = "F003", Descripcion = "Desarrollo Econ�mico", Orden = 3, Nivel = 1 }           
            );

           Funcionalidad fgobierno = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F001");
           Funcionalidad fdesarrollosocial = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F002");
           Funcionalidad fdesarrolloeconomico = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F003");

           fgobierno.DetalleSubElementos.Add(new Funcionalidad { Id = 4, Clave = "F004", Descripcion = "Legislaci�n", Orden = 1, Nivel = 2 });
           fgobierno.DetalleSubElementos.Add(new Funcionalidad { Id = 5, Clave = "F005", Descripcion = "Fiscalizaci�n", Orden = 2, Nivel = 2 });
           fgobierno.DetalleSubElementos.Add(new Funcionalidad { Id = 6, Clave = "F006", Descripcion = "Justicia", Orden = 3, Nivel = 2 });

           fdesarrollosocial.DetalleSubElementos.Add(new Funcionalidad { Id = 7, Clave = "F007", Descripcion = "Protecci�n ambiental", Orden = 1, Nivel = 2 });
           fdesarrollosocial.DetalleSubElementos.Add(new Funcionalidad { Id = 8, Clave = "F008", Descripcion = "Vivienda y servicios a la comunidad", Orden = 2, Nivel = 2 });
           fdesarrollosocial.DetalleSubElementos.Add(new Funcionalidad { Id = 9, Clave = "F009", Descripcion = "Salud", Orden = 3, Nivel = 2 });

           fdesarrolloeconomico.DetalleSubElementos.Add(new Funcionalidad { Id = 10, Clave = "F010", Descripcion = "Asuntos econ�micos, comerciales y laborales en general", Orden = 1, Nivel = 2 });
           fdesarrolloeconomico.DetalleSubElementos.Add(new Funcionalidad { Id = 11, Clave = "F011", Descripcion = "Agropecuaria, silvicultura, pesca y caza", Orden = 2, Nivel = 2 });
           fdesarrolloeconomico.DetalleSubElementos.Add(new Funcionalidad { Id = 12, Clave = "F012", Descripcion = "Combustible y energ�a", Orden = 3, Nivel = 2 });

           Funcionalidad flegislacion = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F004");
           Funcionalidad ffiscalizacion = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F005");
           Funcionalidad fjusticia = context.Funcionalidad.Local.FirstOrDefault(f => f.Clave == "F006");

           flegislacion.DetalleSubElementos.Add(new Funcionalidad { Id = 13, Clave = "F013", Descripcion = "Legislaci�n", Orden = 1, Nivel = 3 });
           ffiscalizacion.DetalleSubElementos.Add(new Funcionalidad { Id = 14, Clave = "F014", Descripcion = "Fiscalizaci�n", Orden = 1, Nivel = 3 });

           fjusticia.DetalleSubElementos.Add(new Funcionalidad { Id = 15, Clave = "F015", Descripcion = "Impartici�n de Justicia", Orden = 1, Nivel = 3 });
           fjusticia.DetalleSubElementos.Add(new Funcionalidad { Id = 16, Clave = "F016", Descripcion = "Procuraci�n de Justicia", Orden = 2, Nivel = 3 });
           fjusticia.DetalleSubElementos.Add(new Funcionalidad { Id = 17, Clave = "F017", Descripcion = "Reclusi�n y readaptaci�n social", Orden = 3, Nivel = 3 });
           fjusticia.DetalleSubElementos.Add(new Funcionalidad { Id = 18, Clave = "F018", Descripcion = "Derechos humanos", Orden = 4, Nivel = 3 });


          context.Eje.AddOrUpdate(
                new Eje { Id = 1, Clave = "A", Descripcion = "Construir el presente: Un mejor futuro para todos", Orden = 1, Nivel = 1 },
                new Eje { Id = 2, Clave = "B", Descripcion = "Econom�a fuerte para el progreso de la gente", Orden = 2, Nivel = 1 },
                new Eje { Id = 3, Clave = "C", Descripcion = "Un Veracruz sustentable", Orden = 3, Nivel = 1 },
                new Eje { Id = 4, Clave = "D", Descripcion = "Gobierno y administraci�n eficientes y transparentes", Orden = 4, Nivel = 1 }
        
          );

          Eje ejeA = context.Eje.Local.FirstOrDefault(e => e.Clave == "A");

          ejeA.DetalleSubElementos.Add(new Eje { Id = 5, Clave = "A005", Descripcion = "Combatir rezagos para salir adelante", Orden = 1,Nivel=2 });
          ejeA.DetalleSubElementos.Add(new Eje { Id = 6, Clave = "A006", Descripcion = "El valor de la civilizaci�n ind�gena", Orden = 2, Nivel = 2 });
          ejeA.DetalleSubElementos.Add(new Eje { Id = 7, Clave = "A007", Descripcion = "La familia veracruzana", Orden = 3, Nivel = 2 });
          ejeA.DetalleSubElementos.Add(new Eje { Id = 8, Clave = "A008", Descripcion = "Igualdad de g�nero", Orden = 4, Nivel = 2 });
          ejeA.DetalleSubElementos.Add(new Eje { Id = 9, Clave = "A009", Descripcion = "Juventud: oportunidad y compromiso", Orden = 5, Nivel = 2 });


          context.PlanSectorial.AddOrUpdate(
              new PlanSectorial { Id = 1, Clave = "A", Descripcion = "Programa Veracruzano de Desarrollo Agropecuario, Rural, Forestal y Pesca.", Orden = 1,Nivel=1 },
              new PlanSectorial { Id = 2, Clave = "B", Descripcion = "Programa Veracruzano de Salud.", Orden = 2, Nivel = 1 },
              new PlanSectorial { Id = 3, Clave = "C", Descripcion = "Programa Veracruzano de Asistencia Social.", Orden = 3, Nivel = 1 },
              new PlanSectorial { Id = 4, Clave = "D", Descripcion = "Programa Veracruzano de Educaci�n.", Orden = 4, Nivel = 1 }           

          );

          context.Modalidad.AddOrUpdate(
            new Modalidad { Id = 1, Clave = "M001", Descripcion = "Subsidios: Sector Social y Privado o Entidades Federativas y Municipios", Orden = 1,Nivel=1 },
            new Modalidad { Id = 2, Clave = "M002", Descripcion = "Desempe�o de las Funciones", Orden = 2,Nivel=1 },
            new Modalidad { Id = 3, Clave = "M003", Descripcion = "Administrativos y de Apoyo", Orden = 3,Nivel=1 },
            new Modalidad { Id = 4, Clave = "M004", Descripcion = "Programas de Gasto Federalizado (Gobierno Federal)", Orden = 4,Nivel=1 }
          );

          Modalidad mSubsidios = context.Modalidad.Local.FirstOrDefault(m => m.Clave == "M001");

          mSubsidios.DetalleSubElementos.Add(new Modalidad { Id = 5, Clave = "S", Descripcion = "Sujetos a Reglas de Operaci�n", Orden = 1, Nivel = 2 });
          mSubsidios.DetalleSubElementos.Add(new Modalidad { Id = 6, Clave = "U", Descripcion = "Otros Subsidios", Orden = 2, Nivel = 2 });


          context.Programa.AddOrUpdate(
             new Programa { Id = 1, Clave = "010", Descripcion = "Formaci�n y Orientaci�n Educativa", Tipo = "A.I.", Objetivo = "Contribuir al desarrollo de las tareas de los alumnos, padres y profesores dentro del �mbito espec�fico de los centros escolares.", Orden = 1 },
             new Programa { Id = 2, Clave = "011", Descripcion = "Centros de Desarrollo Infantil", Tipo = "A.I.", Objetivo = "Brindar servicios de cuidado, salud, alimentaci�n y estimulaci�n a los hijos de las trabajadoras de la Secretar�a de Educaci�n de Veracruz de edades comprendidas entre 45 d�as y 5 a�os 11 meses.", Orden = 2 },
             new Programa { Id = 3, Clave = "012", Descripcion = "Educaci�n B�sica Nivel Preescolar", Tipo = "A.I.", Objetivo = "Atender y apoyar desde edades tempranas a los menores para favorecer el desarrollo de sus potencialidades y capacidades, lo que permitir� un mejordesarrollo personal y social.", Orden = 3 }            

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
            new CriterioPriorizacion { Id = 1, Clave = "CP001", Nombre = "Terminaci�n de obra", Orden = 1 } ,
            new CriterioPriorizacion { Id = 2, Clave = "CP002", Nombre = "Obras y acciones en PARIPASSU", Orden = 2 } , 
            new CriterioPriorizacion { Id = 3, Clave = "CP003", Nombre = "Obras y acciones nuevas", Orden = 3 } , 
            new CriterioPriorizacion { Id = 4, Clave = "CP004", Nombre = "Estudios y proyectos", Orden = 4 } , 
            new CriterioPriorizacion { Id = 5, Clave = "CP005", Nombre = "Obras y acciones nuevas que en el mismo ejercicio contemplen los proyectos", Orden = 5 } 
          );


          context.Plantilla.AddOrUpdate(
            new Plantilla { Id = 1, Clave = "P001", Descripcion="Plantilla inicial",Orden=1 }
          );

          context.PlantillaDetalle.AddOrUpdate(
             new PlantillaDetalle { Id = 1, PlantillaId = 1, Clave = "Q001", Pregunta = "�El importe total de la inversi�n se ajusta a la asignaci�n presupuestal autorizada?", Orden = 1 },
             new PlantillaDetalle { Id = 2, PlantillaId = 1, Clave = "Q002", Pregunta = "�Las obras o acciones corresponden al capitulo 6000 \"Infraestructura para el Desarrollo, Obra P�blica y Servicios Relacionados con la Misma\"?", Orden = 2 },
             new PlantillaDetalle { Id = 3, PlantillaId = 1, Clave = "Q003", Pregunta = "�La descripci�n de la obra o acc�n hace referencia clara de los trabajos a realizar?", Orden = 3 },
             new PlantillaDetalle { Id = 4, PlantillaId = 1, Clave = "Q004", Pregunta = "�Las claves de los programas, subprogramas y subsubsubprogramas corresponden a la apertura program�tica y est�n de acuerdo con la descripci�n de la obra o acci�n?", Orden = 4 },

             new PlantillaDetalle { Id = 5, PlantillaId = 1, Clave = "Q005", Pregunta = "�Las metas de la obra o acci�n son congruentes con el Subprograma asignado asignado y son susceptibles de medici�n?", Orden = 5 },
             new PlantillaDetalle { Id = 6, PlantillaId = 1, Clave = "Q006", Pregunta = "�Los beneficiarios corresponden a la unidad de medida \"personas\"?", Orden = 6 },
             new PlantillaDetalle { Id = 7, PlantillaId = 1, Clave = "Q007", Pregunta = "�Se especifica el nombre completo de la unidad o subunidad presupuestal?", Orden = 7 },
             new PlantillaDetalle { Id = 8, PlantillaId = 1, Clave = "Q008", Pregunta = "�Se especifica el n�mero progresivo y el total de hojas utilizadas?", Orden = 8 },
             new PlantillaDetalle { Id = 9, PlantillaId = 1, Clave = "Q009", Pregunta = "�Se especifica el municipio y localidad(es) donde se realizar� la obra o acci�n, omitiendo los t�rminos \"varios\" y \"cobertura estatal\"?", Orden = 9 },
             new PlantillaDetalle { Id = 10, PlantillaId = 1, Clave = "Q0010", Pregunta = "�Se especifica la modalidad de esjecuci�n de la obra o acci�n?", Orden = 10 },
             new PlantillaDetalle { Id = 11, PlantillaId = 1, Clave = "Q0011", Pregunta = "�Se especifica la situaci�n de la obra o acci�n? ", Orden = 11 },
             new PlantillaDetalle { Id = 12, PlantillaId = 1, Clave = "Q0012", Pregunta = "Si la situaci�n de la obra o acci�n es en \"proceso\", � se especifica el n�mero de obra asignado en el ejercicio anterior y presenta la misma modalidad de ejecuci�n?", Orden = 12 },
             new PlantillaDetalle { Id = 13, PlantillaId = 1, Clave = "Q0013", Pregunta = "�El importe que se registra de las acciones en proceso, es coincidente con el saldo del ejercicio anterior?", Orden = 13 },
             new PlantillaDetalle { Id = 14, PlantillaId = 1, Clave = "Q0014", Pregunta = "�La programaci�n de los gastos indirectos corresponden a obras por contrato que se est�n considerando en el POA y adem�s el c�lculo es de acuerdo al financiamiento, sin especificar beneficiarios y jornales?", Orden = 14 },
             new PlantillaDetalle { Id = 15, PlantillaId = 1, Clave = "Q0015", Pregunta = "La meta anual y el n�mero de beneficiarios , �son indicativos de la descripci�n de la obra o acci�n que se registra?", Orden = 15 },
             new PlantillaDetalle { Id = 16, PlantillaId = 1, Clave = "Q0016", Pregunta = "�El documento presenta en cada una de las hojas la antefirma del Titular de la Unidad Presupuestal, as� como, su nombre, cargo y firma en la �ltima hoja?", Orden = 16 },
             new PlantillaDetalle { Id = 17, PlantillaId = 1, Clave = "Q0017", Pregunta = "�Se especifica la fecha de Firma?", Orden = 17 },
             new PlantillaDetalle { Id = 18, PlantillaId = 1, Clave = "Q0018", Pregunta = "Los estudios y proyectos no deben especificar beneficiarios y jornales", Orden = 18 },
             new PlantillaDetalle { Id = 19, PlantillaId = 1, Clave = "Q0019", Pregunta = "Las obras deben especificar los empleos y jornales a generar", Orden = 19 },
             new PlantillaDetalle { Id = 20, PlantillaId = 1, Clave = "Q0020", Pregunta = "El Cap�tulo 6000 no incluye el financiamiento para construcci�n de viviendas", Orden = 20 },
             new PlantillaDetalle { Id = 21, PlantillaId = 1, Clave = "Q0021", Pregunta = "En cuanto sean asignadas las obras a los fondos correspondientes, checar la normatividad (revisar los gastos indirectos, estudios y proyectos, etc.7)", Orden = 21 }

         );

          context.TechoFinancieroStatus.AddOrUpdate(
            new TechoFinancieroStatus { Id = 1, EjercicioId = 7, Status=1 }          
          );
                      

         // context.TechoFinanciero.AddOrUpdate(
         //   new TechoFinanciero { Id = 1, EjercicioId = 7, FinanciamientoId = 2, Importe = 10000, tmpImporteAsignado = 0, tmpImporteEjecutado = 0 } ,
         //   new TechoFinanciero { Id = 2, EjercicioId = 7, FinanciamientoId = 9, Importe = 12000, tmpImporteAsignado = 0, tmpImporteEjecutado = 0 } 
         // );

         //context.TechoFinancieroUnidadPresupuestal.AddOrUpdate(
         //  new TechoFinancieroUnidadPresupuestal { Id = 1, TechoFinancieroId = 1, UnidadPresupuestalId = 1, Importe = 3000, tmpImporteAsignado = 0, tmpImporteEjecutado = 0 },
         //  new TechoFinancieroUnidadPresupuestal { Id = 2, TechoFinancieroId = 2, UnidadPresupuestalId = 1, Importe = 4500, tmpImporteAsignado = 0, tmpImporteEjecutado = 0 },
         //  new TechoFinancieroUnidadPresupuestal { Id = 3, TechoFinancieroId = 1, UnidadPresupuestalId = 2, Importe = 2000, tmpImporteAsignado = 0, tmpImporteEjecutado = 0 },
         //  new TechoFinancieroUnidadPresupuestal { Id = 4, TechoFinancieroId = 1, UnidadPresupuestalId = 3, Importe = 2000, tmpImporteAsignado = 0, tmpImporteEjecutado = 0 },
         //  new TechoFinancieroUnidadPresupuestal { Id = 5, TechoFinancieroId = 1, UnidadPresupuestalId = 4, Importe = 2000, tmpImporteAsignado = 0, tmpImporteEjecutado = 0 },
         //  new TechoFinancieroUnidadPresupuestal { Id = 6, TechoFinancieroId = 1, UnidadPresupuestalId = 5, Importe = 1000, tmpImporteAsignado = 0, tmpImporteEjecutado = 0 }
         //); 

         // POA poa2013 = new POA { Id = 1, UnidadPresupuestalId = 1, EjercicioId = 5 };  
         // POA poa2014 = new POA { Id = 2, UnidadPresupuestalId = 1, EjercicioId = 7 };

         // POADetalle poadetalle2013_001 = new POADetalle();
         // poadetalle2013_001.Consecutivo = 1;
         // poadetalle2013_001.Numero = "102S110012013001";
         // poadetalle2013_001.Descripcion = "Demolicion manual de cimentaci�n de concreto armado con varilla de acero. Incluye: retiro de material a zona de acopio a 1ra estaci�n de 20m.";
         // poadetalle2013_001.MunicipioId = 1;
         // poadetalle2013_001.Localidad = "Alguna localidad en Acajete";
         // poadetalle2013_001.TipoLocalidadId = 1;
         // poadetalle2013_001.SituacionObraId = 1;
         // poadetalle2013_001.ModalidadObra = enumModalidadObra.Contrato;          
         // poadetalle2013_001.ImporteTotal = 3000;
         // poadetalle2013_001.ImporteLiberadoEjerciciosAnteriores = 0;
         // poadetalle2013_001.ImportePresupuesto = 4000;
         // poadetalle2013_001.AperturaProgramaticaId = 22;
         // poadetalle2013_001.AperturaProgramaticaMetaId = 1;
         // poadetalle2013_001.NumeroBeneficiarios = 25;
         // poadetalle2013_001.CantidadUnidades = 17;
         // poadetalle2013_001.Empleos = 10;
         // poadetalle2013_001.Jornales = 15;
         // poadetalle2013_001.FuncionalidadId = 16;
         // poadetalle2013_001.EjeId = 5;
         // poadetalle2013_001.PlanSectorialId = 1;
         // poadetalle2013_001.ModalidadId = 5;
         // poadetalle2013_001.ProgramaId = 1;
         // poadetalle2013_001.GrupoBeneficiarioId = 2;
         // poadetalle2013_001.CriterioPriorizacionId = 1;
         // poadetalle2013_001.Observaciones = "Observaciones del proyecto de obra cuyo n�mero es 102S110012013001";

         // poa2013.Detalles.Add(poadetalle2013_001);


         // Obra obra2013_001 = new Obra();
         // obra2013_001.Consecutivo = 1;
         // obra2013_001.Numero = "102S110012013001";
         // obra2013_001.Descripcion = "Demolicion manual de cimentaci�n de concreto armado con varilla de acero. Incluye: retiro de material a zona de acopio a 1ra estaci�n de 20m.";
         // obra2013_001.MunicipioId = 1;
         // obra2013_001.Localidad = "Alguna localidad en Acajete";
         // obra2013_001.TipoLocalidadId = 1;
         // obra2013_001.SituacionObraId = 1;
         // obra2013_001.ModalidadObra = enumModalidadObra.Contrato;
         // obra2013_001.FechaInicio = new DateTime(2013, 01, 30);
         // obra2013_001.FechaTermino = new DateTime(2013, 09, 16);        
         // obra2013_001.ImporteTotal = 12348700;
         // obra2013_001.ImporteLiberadoEjerciciosAnteriores = 10200000;
         // obra2013_001.ImportePresupuesto = 15000000;
         // obra2013_001.AperturaProgramaticaId = 22;
         // obra2013_001.AperturaProgramaticaMetaId = 1;
         // obra2013_001.NumeroBeneficiarios = 25;
         // obra2013_001.CantidadUnidades = 18;
         // obra2013_001.Empleos = 10;
         // obra2013_001.Jornales = 15;
         // obra2013_001.FuncionalidadId = 16;
         // obra2013_001.EjeId = 5;
         // obra2013_001.PlanSectorialId = 1;
         // obra2013_001.ModalidadId = 5;
         // obra2013_001.ProgramaId = 1;
         // obra2013_001.GrupoBeneficiarioId = 2;
         // obra2013_001.CriterioPriorizacionId = 1;
         // obra2013_001.Observaciones = "Estas son las observaciones de la obra cuyo n�mero es 102S110012013001";

         // obra2013_001.POA = poa2013;
         // obra2013_001.POADetalle = poadetalle2013_001;

         // context.Obras.Add(obra2013_001); 
              

         // POADetalle poadetalle2014_001 = new POADetalle();
         // poadetalle2014_001.Consecutivo = 1;
         // poadetalle2014_001.Numero = "102S110012014001";
         // poadetalle2014_001.Descripcion = "CONSTRUCCION DEL CAMINO JALACINGO-OCAMPO-COLIHUI (3A. ETAPA) DEL KM. 2+300 AL KM. 4+800.";
         // poadetalle2014_001.MunicipioId = 1;
         // poadetalle2014_001.Localidad = "Alguna localidad en Acajete";
         // poadetalle2014_001.TipoLocalidadId = 1;
         // poadetalle2014_001.SituacionObraId = 1;
         // poadetalle2014_001.ModalidadObra = enumModalidadObra.Contrato;         
         // poadetalle2014_001.ImporteTotal = 3000;
         // poadetalle2014_001.ImporteLiberadoEjerciciosAnteriores = 0;
         // poadetalle2014_001.ImportePresupuesto = 4000;
         // poadetalle2014_001.AperturaProgramaticaId = 22;
         // poadetalle2014_001.AperturaProgramaticaMetaId = 1;
         // poadetalle2014_001.NumeroBeneficiarios = 25;
         // poadetalle2014_001.CantidadUnidades = 17;
         // poadetalle2014_001.Empleos = 10;
         // poadetalle2014_001.Jornales = 15;
         // poadetalle2014_001.FuncionalidadId = 16;
         // poadetalle2014_001.EjeId = 5;
         // poadetalle2014_001.PlanSectorialId = 1;
         // poadetalle2014_001.ModalidadId = 5;
         // poadetalle2014_001.ProgramaId = 1;
         // poadetalle2014_001.GrupoBeneficiarioId = 2;
         // poadetalle2014_001.CriterioPriorizacionId = 1;
         // poadetalle2014_001.Observaciones = "Observaciones del proyecto de obra cuyo n�mero es 102S110012014001";

         // poa2014.Detalles.Add(poadetalle2014_001);

         // context.POA.Add(poa2014);

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
							             @anio=Ejercicio.A�o							   

                                     from POADetalle 
                                     inner join POA
                                     on POA.Id=POADetalle.POAId
                                     inner join UnidadPresupuestal
                                     on UnidadPresupuestal.Id=POA.UnidadPresupuestalId
                                     inner join Ejercicio
                                     on Ejercicio.Id=POA.EjercicioId
                                     where POA.Id=@poaId
							         group by POA.Id,UnidadPresupuestal.Clave,Ejercicio.A�o
                            
                            set @consecutivo=@consecutivo+1;                                     
							
							set @numeroObra= concat(@UnidadPresupuestalClave,@anio,REPLACE(STR(@consecutivo, 3),SPACE(1),'0'));

                            update POADetalle set Consecutivo=@consecutivo,Numero=@numeroObra where Id=@poadetalleId";



            contexto.Database.ExecuteSqlCommand(sp001);


            sp001 = @"CREATE TRIGGER trgAsignarNumeroObra_Obra ON [dbo].[Obra] 
                                FOR INSERT
                                AS
	                               
									 declare @consecutivo int;
						             declare @UnidadPresupuestalClave varchar(9);
						             declare @anio int;
						             declare @obraId int;
						             declare @poaId int;
						             declare @numeroObra varchar(100);

						             select @poaId=POAId,@obraId=Id from inserted; 

                                     select

                                         @consecutivo=MAX(obra.Consecutivo),							  
							             @UnidadPresupuestalClave=UnidadPresupuestal.Clave,
							             @anio=Ejercicio.A�o							   

                                     from Obra 
                                     inner join POA
                                     on POA.Id=Obra.POAId
                                     inner join UnidadPresupuestal
                                     on UnidadPresupuestal.Id=POA.UnidadPresupuestalId
                                     inner join Ejercicio
                                     on Ejercicio.Id=POA.EjercicioId
                                     where POA.Id=@poaId
							         group by POA.Id,UnidadPresupuestal.Clave,Ejercicio.A�o
                            
                            set @consecutivo=@consecutivo+1;                                     
							
							set @numeroObra= concat(@UnidadPresupuestalClave,@anio,REPLACE(STR(@consecutivo, 3),SPACE(1),'0'));

                            update Obra set Consecutivo=@consecutivo,Numero=@numeroObra where Id=@obraId";



            contexto.Database.ExecuteSqlCommand(sp001);           




        } // Triggers






    }
}

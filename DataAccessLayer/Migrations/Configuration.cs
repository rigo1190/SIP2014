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

            context.Usuarios.AddOrUpdate(

               new Usuario { Id = 1, Login = "sedarpa", Password = "sedarpa", Nombre = "Usuario de SEDARPA", Activo = true },
               new Usuario { Id = 2, Login = "iiev", Password = "iiev", Nombre = "Usuario de IIEV", Activo = true },
               new Usuario { Id = 3, Login = "inverbio", Password = "inverbio", Nombre = "Usuario de INVERBIO", Activo = true }               
            );



            context.Ejercicios.AddOrUpdate(

               new Ejercicio { Id = 1, Año =2009,Activo=false },
               new Ejercicio { Id = 2, Año =2010,Activo=false },
               new Ejercicio { Id = 3, Año =2011,Activo=false },
               new Ejercicio { Id = 4, Año =2012,Activo=false },
               new Ejercicio { Id = 5, Año =2013,Activo=false },
               new Ejercicio { Id = 6, Año =2014,Activo=true },
               new Ejercicio { Id = 7, Año =2015,Activo=false },
               new Ejercicio { Id = 8, Año =2016,Activo=false }
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
               new AperturaProgramatica { Id = 1, Clave = "SC", Nombre = "Agua y saneamiento (Agua potable)", Orden = 1, EjercicioId = 6,Nivel=1 },
               new AperturaProgramatica { Id = 2, Clave = "SD", Nombre = "Agua y saneamiento (Drenaje)", Orden = 2, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 3, Clave = "SE", Nombre = "Urbanización municipal", Orden = 3, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 4, Clave = "SG", Nombre = "Electrificación", Orden = 4, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 5, Clave = "SO", Nombre = "Salud", Orden = 5, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 6, Clave = "SJ", Nombre = "Educación", Orden = 6, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 7, Clave = "SH", Nombre = "Vivienda", Orden = 7, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 8, Clave = "UB", Nombre = "Caminos rurales", Orden = 8, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 9, Clave = "IR", Nombre = "Infraestructura productiva rural", Orden = 9, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 10, Clave = "UM", Nombre = "Equipamiento urbano", Orden = 10, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 11, Clave = "PE", Nombre = "Protección y preservación ecológica", Orden = 11, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 12, Clave = "BE", Nombre = "Bienes muebles", Orden = 12, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 13, Clave = "BI", Nombre = "Bienes inmuebles", Orden = 13, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 14, Clave = "PM", Nombre = "Planeación municipal", Orden = 14, EjercicioId = 6, Nivel = 1 },
               new AperturaProgramatica { Id = 15, Clave = "SB", Nombre = "Estímulos a la educación", Orden = 15, EjercicioId = 6, Nivel = 1 }
            );

           AperturaProgramatica sc = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Clave == "SC");

           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 16, Clave = "01", Nombre = "Rehabilitación", Orden = 1, EjercicioId = 6, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 17, Clave = "02", Nombre = "Ampliación", Orden = 2, EjercicioId = 6, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 18, Clave = "03", Nombre = "Construcción", Orden = 3, EjercicioId = 6, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 19, Clave = "04", Nombre = "Mantenimiento", Orden = 4, EjercicioId = 6, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 20, Clave = "05", Nombre = "Equipamiento", Orden = 5, EjercicioId = 6, Nivel = 2 });
           sc.DetalleSubElementos.Add(new AperturaProgramatica { Id = 21, Clave = "06", Nombre = "Sustitución", Orden = 6, EjercicioId = 6, Nivel = 2 });

           AperturaProgramatica sc_rehabilitacion = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Id == 16);

           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 22, Clave = "a", Nombre = "Planta potabilizadora", Orden = 1, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 23, Clave = "b", Nombre = "Pozo profundo de agua potable", Orden = 2, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 24, Clave = "c", Nombre = "Deposito o tanque de agua potable", Orden = 3, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 25, Clave = "d", Nombre = "Linea de conducción", Orden = 4, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 26, Clave = "e", Nombre = "Red de agua potable", Orden = 5, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 27, Clave = "f", Nombre = "Sistema integral de agua potable", Orden = 6, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 28, Clave = "g", Nombre = "Carcamo", Orden = 7, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 29, Clave = "h", Nombre = "Norias", Orden = 8, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 30, Clave = "i", Nombre = "Pozo artesiano", Orden = 9, EjercicioId = 6, Nivel = 3 });
           sc_rehabilitacion.DetalleSubElementos.Add(new AperturaProgramatica { Id = 31, Clave = "j", Nombre = "Olla de captación de agua pluvial", Orden = 10, EjercicioId = 6, Nivel = 3 });

           AperturaProgramatica sc_rehabilitacion_plantapotabilizadora = context.AperturaProgramatica.Local.FirstOrDefault(ap => ap.Id == 22);

           sc_rehabilitacion_plantapotabilizadora.DetalleMetas.Add(new AperturaProgramaticaMeta { AperturaProgramaticaUnidadId = 8, AperturaProgramaticaBeneficiarioId = 1 });



           var list = from año in context.Años.Local
                      from mf in context.ModalidadesFinanciamiento.Local
                      from f in context.Fondos.Local
                      select new { año, mf, f };

           foreach (var item in list)
           {
               context.Financiamientos.Add(new Financiamiento { Año = item.año, ModalidadFinanciamiento = item.mf, Fondo = item.f });
           }


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

          POA poa = new POA { Id = 1, UnidadPresupuestalId = 1, EjercicioId = 6 };

          POADetalle poadetalle = new POADetalle();
          poadetalle.Consecutivo = 1;
          poadetalle.Numero = "102S110012014001";
          poadetalle.Descripcion = "Demolicion manual de cimentación de concreto armado con varilla de acero. Incluye: retiro de material a zona de acopio a 1ra estación de 20m.";
          poadetalle.MunicipioId = 1;
          poadetalle.Localidad = "Alguna localidad en Acajete";
          poadetalle.TipoLocalidadId = 1;
          poadetalle.SituacionObraId = 1;
          poadetalle.ModalidadObra = enumModalidadObra.Contrato;
          poadetalle.EsAccion = false;
          poadetalle.ImporteTotal = 12348700;
          poadetalle.ImporteLiberadoEjerciciosAnteriores = 10200000;
          poadetalle.ImportePresupuesto = 15000000;
          poadetalle.AperturaProgramaticaId = 22;
          poadetalle.AperturaProgramaticaMetaId = 1;
          poadetalle.NumeroBeneficiarios = 25;
          poadetalle.CantidadUnidades = 17;
          poadetalle.Empleos = 10;
          poadetalle.Jornales = 15;
          poadetalle.FuncionalidadId = 16;
          poadetalle.EjeId= 5;
          poadetalle.PlanSectorialId = 1;
          poadetalle.ModalidadId = 5;
          poadetalle.ProgramaId = 1;
          poadetalle.GrupoBeneficiarioId = 2;
          poadetalle.CriterioPriorizacionId = 1;

          poa.Detalles.Add(poadetalle);

          context.POA.Add(poa);


          Obra obra = new Obra();
          obra.Numero = "102S110012014001";
          obra.Descripcion = "Demolicion manual de cimentación de concreto armado con varilla de acero. Incluye: retiro de material a zona de acopio a 1ra estación de 20m.";
          obra.MunicipioId = 1;
          obra.Localidad = "Alguna localidad en Acajete";
          obra.TipoLocalidadId = 1;
          obra.SituacionObraId = 1;
          obra.ModalidadObra = enumModalidadObra.Contrato;
          obra.FechaInicio = new DateTime(2014, 01, 30);
          obra.FechaTermino = new DateTime(2014, 09, 16);
          obra.EsAccion = false;
          obra.ImporteTotal = 12348700;
          obra.AperturaProgramaticaId = 22;
          obra.AperturaProgramaticaMetaId = 1;
          obra.NumeroBeneficiarios = 25;
          obra.CantidadUnidades = 18;
          obra.Empleos = 10;
          obra.Jornales = 15;
          obra.FuncionalidadId = 16;
          obra.EjeId = 5;
          obra.PlanSectorialId = 1;
          obra.ModalidadId = 5;
          obra.ProgramaId = 1;
          obra.GrupoBeneficiarioId = 2;
          obra.CriterioPriorizacionId = 1;

          obra.POADetalle = poadetalle;

          context.Obras.Add(obra);

           context.SaveChanges();

           CrearTriggers(context);
      
           
        }


        private void CrearTriggers(Contexto contexto)
        {

            string sp001 = @" CREATE TRIGGER trgAsignarNumeroObra ON [dbo].[POADetalle] 
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
							
							set @numeroObra= concat(@UnidadPresupuestalClave,@anio,REPLACE(STR(@consecutivo, 3),SPACE(1),'0'));

                            update POADetalle set Consecutivo=@consecutivo,Numero=@numeroObra where Id=@poadetalleId";



            contexto.Database.ExecuteSqlCommand(sp001);           


        } // Triggers






    }
}

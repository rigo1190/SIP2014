using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class frmPOAAjustado : System.Web.UI.Page
    {   
       

        [WebMethod(EnableSession = true)]
        public static object GetTituloPagina()
        {

            UnitOfWork uow = new UnitOfWork();

            int unidadpresupuestalId = Utilerias.StrToInt(HttpContext.Current.Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(HttpContext.Current.Session["EjercicioId"].ToString());

            UnidadPresupuestal unidadpresupuestal = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);
            Ejercicio ejercicio = uow.EjercicioBusinessLogic.GetByID(ejercicioId);

            return String.Format("{0}<br />Proyecto de POA ajustado en el ejercicio {1}", unidadpresupuestal.Nombre, ejercicio.Año); ;

        }

       

        [WebMethod]
        public static object GetRecord(int id)
        {

            UnitOfWork uow = new UnitOfWork();

            Obra obra = uow.ObraBusinessLogic.GetByID(id);

            return new { Id=obra.Id, Numero=obra.Numero,Descripcion=obra.Descripcion,MunicipioId=obra.MunicipioId,LocalidadId=obra.LocalidadId,CriterioPriorizacionId=obra.CriterioPriorizacionId,Convenio=obra.Convenio,ProgramaId=obra.AperturaProgramatica.Parent.ParentId,SubprogramaId=obra.AperturaProgramatica.ParentId,SubsubprogramaId=obra.AperturaProgramaticaId,UnidadMedidaId=obra.AperturaProgramaticaUnidadId,CantidadUnidades=obra.CantidadUnidades,NumeroBeneficiarios=obra.NumeroBeneficiarios,Empleos=obra.Empleos,Jornales=obra.Jornales,SituacionObraId=obra.SituacionObraId,CostoTotal=obra.GetCostoTotal(),PresupuestoEjercicio=obra.GetImporteAsignado(),NumeroAnterior=obra.NumeroAnterior,ImporteLiberadoEjerciciosAnteriores=obra.ImporteLiberadoEjerciciosAnteriores,ModalidadId=obra.ModalidadObra,FuncionalidadId=obra.FuncionalidadId,EjeId=obra.EjeId,PlanSectorialId=obra.PlanSectorialId,ModalidadPVDId=obra.ModalidadId,ProgramaPVDId=obra.ProgramaId,GrupoBeneficiarioId=obra.GrupoBeneficiarioId };

        }

        [WebMethod]
        public static IQueryable GetUnidadesPresupuestales()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.UnidadPresupuestalBusinessLogic.Get().Select(e => new { Id = e.Id, Nombre = e.Nombre, ParentId = e.ParentId });
            return list;
        }

        [WebMethod]
        public static IQueryable GetMunicipios()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.MunicipioBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetLocalidades()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.LocalidadBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden,MunicipioId=e.MunicipioId });
            return list;
        }

        [WebMethod]
        public static IQueryable GetCriteriosPriorizacion()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.CriterioPriorizacionBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetAperturaProgramatica()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.AperturaProgramaticaBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden,ParentId=e.ParentId,Nivel=e.Nivel });
            return list;
        }

        [WebMethod]
        public static IQueryable GetUnidadesMedida()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.AperturaProgramaticaUnidadBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetSituacionesObra()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.SituacionObraBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static object GetModalidadesEjecucion()
        {          
            List<object> list = new List<object>();

            list.Add(new { Id = 1, Nombre="Contrato"});
            list.Add(new { Id = 2, Nombre = "Administración directa" });
            list.Add(new { Id = 3, Nombre = "Mixta" });

            return list;

        }

        [WebMethod]
        public static IQueryable GetFondos()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.FondoBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Abreviatura = e.Abreviatura, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetFuncionalidades()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.FuncionalidadBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Nivel=e.Nivel,Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetEjes()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.EjeBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Nivel = e.Nivel, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetPlanesSectoriales()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.PlanSectorialBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Nivel = e.Nivel, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetModalidades()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.ModalidadBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Nivel = e.Nivel, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetProgramasPVD()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.ProgramaBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetGrupoBeneficiarios()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.GrupoBeneficiarioBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }


          [WebMethod(EnableSession = true)]
        public static object GetListadoObras(string WhereNumero, string WhereDescripcion, string WhereMunicipio, string WhereLocalidad, string WhereUnidadPresupuestal, string WhereContratista, string WhereFondos, string WherePresupuesto)
        {
            UnitOfWork uow = new UnitOfWork();
            SqlDataReader reader =null;

            int unidadpresupuestalId = Utilerias.StrToInt(HttpContext.Current.Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(HttpContext.Current.Session["EjercicioId"].ToString());

            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();


            List<object> list = new List<object>();

            using (SqlConnection con = new SqlConnection(uow.Contexto.Database.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_MulticriterioPOAAjustado", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@POAId", SqlDbType.Int).Value = poa.Id;
                    cmd.Parameters.Add("@WhereNumero", SqlDbType.VarChar).Value = WhereNumero;
                    cmd.Parameters.Add("@WhereDescripcion", SqlDbType.VarChar).Value = WhereDescripcion;
                    cmd.Parameters.Add("@WhereMunicipio", SqlDbType.VarChar).Value = WhereMunicipio;
                    cmd.Parameters.Add("@WhereLocalidad", SqlDbType.VarChar).Value = WhereLocalidad;
                    cmd.Parameters.Add("@WhereUnidadPresupuestal", SqlDbType.VarChar).Value = WhereUnidadPresupuestal;                    
                    cmd.Parameters.Add("@WhereContratista", SqlDbType.VarChar).Value = WhereContratista;
                    cmd.Parameters.Add("@WhereFondos", SqlDbType.VarChar).Value = WhereFondos;
                    cmd.Parameters.Add("@WherePresupuesto", SqlDbType.VarChar).Value = WherePresupuesto;
                    

                    con.Open();

                    reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {


                            int ObraId = Convert.ToInt32(reader["ObraId"]);
                            string Numero = reader["Numero"].ToString();
                            string Descripcion = reader["Descripcion"].ToString();
                            string Municipio = reader["Municipio"].ToString();
                            string Localidad = reader["Localidad"].ToString();
                            string UnidadPresupuestal = reader["UnidadPresupuestal"].ToString();
                            string SubUnidadPresupuestal = reader["SubUnidadPresupuestal"].ToString();
                            string Contratista = reader["Contratista"].ToString();
                            string Fondos = reader["Fondos"].ToString();
                            decimal Presupuesto = Convert.ToDecimal(reader["Presupuesto"]);

                            list.Add(new { ObraId = ObraId, Numero = Numero, Descripcion = Descripcion, Municipio = Municipio, Localidad = Localidad, UnidadPresupuestal = UnidadPresupuestal,SubUnidadPresupuestal=SubUnidadPresupuestal, Contratista = Contratista, Fondos = Fondos, Presupuesto = Presupuesto });

                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();

                } //using command

            }//using connection

            return list;

        }


        [WebMethod(EnableSession = true)]
        public static object UpdateRecord(RegistroObra registro)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());
           
            Obra obra = uow.ObraBusinessLogic.GetByID(registro.Id);

            obra.Numero = registro.Numero;
            obra.Descripcion = registro.Descripcion;
            obra.MunicipioId = registro.MunicipioId;
            obra.LocalidadId = registro.LocalidadId;
            obra.CriterioPriorizacionId = registro.CriterioPriorizacionId;
            obra.Convenio = registro.Convenio;
            obra.AperturaProgramaticaId = registro.AperturaProgramaticaId;
            obra.AperturaProgramaticaUnidadId = registro.UnidadMedidaId;
            obra.CantidadUnidades = registro.CantidadUnidades;
            obra.NumeroBeneficiarios = registro.NumeroBeneficiarios;
            obra.Empleos = registro.Empleos;
            obra.Jornales = registro.Jornales;
            obra.SituacionObraId = registro.SituacionObraId;
            obra.NumeroAnterior = registro.NumeroAnterior;
            obra.ImporteLiberadoEjerciciosAnteriores = registro.ImporteLiberadoEjerciciosAnteriores;
            obra.ModalidadObra = (enumModalidadObra)registro.ModalidadEjecucionId;           

            if (registro.FuncionalidadId == 0)
            {
                obra.FuncionalidadId = null;
            }
            else
            {
                obra.FuncionalidadId = registro.FuncionalidadId;
            };

            if (registro.EjeId == 0)
            {
                obra.EjeId = null;
            }
            else
            {
                obra.EjeId = registro.EjeId;
            };

            if (registro.PlanSectorialId == 0)
            {
                obra.PlanSectorialId = null;
            }
            else
            {
                obra.PlanSectorialId = registro.PlanSectorialId;
            };

            if (registro.ModalidadPVDId == 0)
            {
                obra.ModalidadId = null;
            }
            else
            {
                obra.ModalidadId = registro.ModalidadPVDId;
            };

            if (registro.ProgramaPVDId == 0)
            {
                obra.ProgramaId = null;
            }
            else
            {
                obra.ProgramaId = registro.ProgramaPVDId;
            };

            if (registro.GrupoBeneficiarioId == 0)
            {
                obra.GrupoBeneficiarioId = null;
            }
            else
            {
                obra.GrupoBeneficiarioId = registro.GrupoBeneficiarioId;
            };

            uow.ObraBusinessLogic.Update(obra);                       

            uow.SaveChanges();

            return uow.GetResult();
        }


        [WebMethod(EnableSession = true)]
        public static object AddRecord(RegistroObra registro)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());

            int unidadpresupuestalId = Utilerias.StrToInt(HttpContext.Current.Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(HttpContext.Current.Session["EjercicioId"].ToString());
          
            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();
            POADetalle poadetalle = null;          

            if (poa == null)
            {
                poa = new DataAccessLayer.Models.POA();
                poa.UnidadPresupuestalId = unidadpresupuestalId;
                poa.EjercicioId = ejercicioId;
            }


            Obra obra = new Obra();

            obra.Numero = registro.Numero;
            obra.Descripcion = registro.Descripcion;
            obra.MunicipioId = registro.MunicipioId;
            obra.LocalidadId = registro.LocalidadId;
            obra.CriterioPriorizacionId = registro.CriterioPriorizacionId;
            obra.Convenio = registro.Convenio;
            obra.AperturaProgramaticaId = registro.AperturaProgramaticaId;
            obra.AperturaProgramaticaUnidadId = registro.UnidadMedidaId;
            obra.CantidadUnidades = registro.CantidadUnidades;
            obra.NumeroBeneficiarios = registro.NumeroBeneficiarios;
            obra.Empleos = registro.Empleos;
            obra.Jornales = registro.Jornales;
            obra.SituacionObraId = registro.SituacionObraId;
            obra.NumeroAnterior = registro.NumeroAnterior;
            obra.ImporteLiberadoEjerciciosAnteriores = registro.ImporteLiberadoEjerciciosAnteriores;
            obra.ModalidadObra = (enumModalidadObra)registro.ModalidadEjecucionId;

            if(registro.FuncionalidadId==0)
            {
                obra.FuncionalidadId =null;
            }else
            {
                obra.FuncionalidadId =registro.FuncionalidadId;
            };

            if (registro.EjeId == 0)
            {
                obra.EjeId = null;
            }
            else
            {
                obra.EjeId = registro.EjeId;
            };

            if (registro.PlanSectorialId == 0)
            {
                obra.PlanSectorialId = null;
            }
            else
            {
                obra.PlanSectorialId = registro.PlanSectorialId;
            };

            if (registro.ModalidadPVDId == 0)
            {
                obra.ModalidadId = null;
            }
            else
            {
                obra.ModalidadId = registro.ModalidadPVDId;
            };

            if (registro.ProgramaPVDId == 0)
            {
                obra.ProgramaId = null;
            }
            else
            {
                obra.ProgramaId = registro.ProgramaPVDId;
            };

            if (registro.GrupoBeneficiarioId == 0)
            {
                obra.GrupoBeneficiarioId = null;
            }
            else
            {
                obra.GrupoBeneficiarioId = registro.GrupoBeneficiarioId;
            };
      

            //Crear un poadetalle para una nueva obra

                    poadetalle = new POADetalle();
                    poadetalle.Numero = obra.Numero;
                    poadetalle.Descripcion = obra.Descripcion;
                    poadetalle.MunicipioId = obra.MunicipioId;
                    poadetalle.LocalidadId = obra.LocalidadId;                
                    poadetalle.CriterioPriorizacionId = obra.CriterioPriorizacionId;
                    poadetalle.Convenio = obra.Convenio;
                    poadetalle.AperturaProgramaticaId = obra.AperturaProgramaticaId;
                    poadetalle.AperturaProgramaticaMetaId = obra.AperturaProgramaticaMetaId;
                    poadetalle.AperturaProgramaticaUnidadId = obra.AperturaProgramaticaUnidadId;
                    poadetalle.NumeroBeneficiarios = obra.NumeroBeneficiarios;
                    poadetalle.CantidadUnidades = obra.CantidadUnidades;
                    poadetalle.Empleos = obra.Empleos;
                    poadetalle.Jornales = obra.Jornales;

                    poadetalle.FuncionalidadId = obra.FuncionalidadId;
                    poadetalle.EjeId = obra.EjeId;
                    poadetalle.PlanSectorialId = obra.PlanSectorialId;
                    poadetalle.ModalidadId = obra.ModalidadId;
                    poadetalle.ProgramaId = obra.ProgramaId;
                    poadetalle.GrupoBeneficiarioId = obra.GrupoBeneficiarioId;


                    poadetalle.SituacionObraId = obra.SituacionObraId;
                    poadetalle.NumeroAnterior = obra.NumeroAnterior;
                    poadetalle.ImporteLiberadoEjerciciosAnteriores = obra.ImporteLiberadoEjerciciosAnteriores;
                    poadetalle.ModalidadObra = obra.ModalidadObra;               
                    poadetalle.Observaciones = obra.Observaciones;
                    poadetalle.Extemporanea = true;
                    poadetalle.POA = poa;                

                    obra.POA = poa;
                    obra.POADetalle = poadetalle;                  
                        
            
            uow.ObraBusinessLogic.Insert(obra);

            uow.SaveChanges();

            return uow.GetResult();

        }

        [WebMethod(EnableSession = true)]
        public static object DeleteRecord(int id)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());

            Obra obra = uow.ObraBusinessLogic.GetByID(id);

            uow.ObraBusinessLogic.Delete(obra);

            uow.SaveChanges();

            return uow.GetResult();
        }




    } //partial


          

    public class RegistroObra 
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public int MunicipioId { get; set; }
        public int LocalidadId { get; set; }
        public int CriterioPriorizacionId { get; set; }
        public string Convenio { get; set; }
        public int AperturaProgramaticaId { get; set; }        
        public int UnidadMedidaId { get; set; }
        public int CantidadUnidades { get; set; }
        public int NumeroBeneficiarios { get; set; }
        public int Empleos { get; set; }
        public int Jornales { get; set; }
        public int SituacionObraId { get; set; }
        public string NumeroAnterior { get; set; }
        public decimal ImporteLiberadoEjerciciosAnteriores { get; set; }       
        public int ModalidadEjecucionId { get; set; }
        public int FuncionalidadId { get; set; }
        public int EjeId { get; set; }
        public int PlanSectorialId { get; set; }
        public int ModalidadPVDId { get; set; }
        public int ProgramaPVDId { get; set; }
        public int GrupoBeneficiarioId { get; set; }

    }





} //namespace
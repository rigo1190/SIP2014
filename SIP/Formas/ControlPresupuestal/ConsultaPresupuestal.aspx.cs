using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.ControlPresupuestal
{
    public partial class ConsultaPresupuestal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static object GetListadoObras(string WhereNumero, string WhereDescripcion, string WhereMunicipio, string WhereLocalidad, string WhereUnidadPresupuestal, string WhereContratista, string WhereFondos, string WherePresupuesto)
        {
            UnitOfWork uow = new UnitOfWork();
            SqlDataReader reader = null;

            int unidadpresupuestalId = Utilerias.StrToInt(HttpContext.Current.Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(HttpContext.Current.Session["EjercicioId"].ToString());

            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();


            List<object> list = new List<object>();

            using (SqlConnection con = new SqlConnection(uow.Contexto.Database.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ConsultaPresupuestal", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
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
                            decimal Total = Convert.ToDecimal(reader["Total"]);
                            decimal SinExpedienteTecnico = Convert.ToDecimal(reader["SinExpedienteTecnico"]);
                            decimal ConExpedienteTecnico = Convert.ToDecimal(reader["ConExpedienteTecnico"]);
                            decimal Tramitado = Convert.ToDecimal(reader["Tramitado"]);
                            decimal Saldo = Convert.ToDecimal(reader["Saldo"]);

                            list.Add(new { ObraId = ObraId, Numero = Numero, Descripcion = Descripcion, Municipio = Municipio, Localidad = Localidad, UnidadPresupuestal = UnidadPresupuestal, SubUnidadPresupuestal = SubUnidadPresupuestal, Contratista = Contratista, Fondos = Fondos, Total = Total, SinExpedienteTecnico = SinExpedienteTecnico, ConExpedienteTecnico = ConExpedienteTecnico, Tramitado = Tramitado ,Saldo=Saldo});

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


    }
}
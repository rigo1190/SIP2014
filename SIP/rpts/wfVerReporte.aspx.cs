using BusinessLogicLayer;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace SIP.rpts
{
    public partial class wfVerReporte : System.Web.UI.Page
    {

        public string nombreReporte;
        protected void Page_Load(object sender, EventArgs e)
        {
            int caller = Utilerias.StrToInt(Request.Params["c"].ToString());
            string parametros = Request.Params["p"].ToString();
            string nomReporte = GetNombreReporte(caller);
            
            ReportDocument rdc = new ReportDocument();

            rdc.FileName = Server.MapPath("~/rpts/" + nomReporte);

            if (!parametros.Equals(string.Empty))
                CargarParametros(caller, parametros, ref rdc);

            CargarReporte(rdc);


        }


        private void CargarParametros(int caller, string parametros, ref ReportDocument rdc)
        {
            string[] primerArray = parametros.Split('-');

            switch (caller)
            {
                case 1: //REPORTE DE EVALUACION DE PLANEACION
                    rdc.SetParameterValue("POADetalleID", primerArray[0]);
                    break;
                case 2: //REPORTE DE DSP
                    rdc.SetParameterValue("@UnidadPresupuestalID", primerArray[0]);
                    rdc.RecordSelectionFormula = "{pa_DSP;1.POADetalleID} in ["+primerArray[1]+"]";
                    break;
                case 3: //REPORTE DE DETALLE DE OBRAS APROBADAS DSP
                    rdc.SetParameterValue("@UnidadPresupuestalID", primerArray[0]);
                    rdc.RecordSelectionFormula = "{sp_DetalleObrasDSP;1.POADetalleID} in ["+primerArray[1]+"]";
                    break;
                case 4: //REPORTE DE DETALLE DE OBRAS RECHAZADAS DSP
                     rdc.SetParameterValue("@UnidadPresupuestalID", primerArray[0]);
                    break;
                case 5: //REPORTE DE RPAI
                     rdc.SetParameterValue("@UnidadPresupuestalID", primerArray[0]);
                     rdc.RecordSelectionFormula = "{sp_RPAI;1.POADetalleID} in [" + primerArray[1] + "]";
                    break;
                case 6: //REPORTE DE DETALLE DE OBRAS DE RPAI
                    rdc.SetParameterValue("@UnidadPresupuestalID", primerArray[0]);
                    rdc.RecordSelectionFormula = "{sp_DetalleObrasRPAI;1.POADetalleID} in [" + primerArray[1] + "]";
                    break;
                    //20's REPORTES DE TECHO FINANCIERO
                case 21:
                    rdc.SetParameterValue("up", primerArray[0]);
                    rdc.SetParameterValue("ejercicio",primerArray[1]);
                    break;


                case 22:
                    rdc.SetParameterValue("techofinanciero", primerArray[0]);
                    break;
                    
                    //30'S REPORTES DE CONTROL FINANCIERO
                case 31:
                    rdc.SetParameterValue("obra", primerArray[0]);
                    break;

                case 32:
                    rdc.SetParameterValue("obra", primerArray[0]);
                    rdc.SetParameterValue("usuario", primerArray[1]);
                    break;
                case 33:
                    rdc.SetParameterValue("contrato", primerArray[0]);                    
                    break;
                case 34:
                    rdc.SetParameterValue("contrato", primerArray[0]);                    
                    break;
            }
        }




        private void CargarReporte(ReportDocument rdc)
        {
            CrystalReportViewer1.ReportSource = rdc;
            string user = System.Configuration.ConfigurationManager.AppSettings["user"];
            string pass = System.Configuration.ConfigurationManager.AppSettings["pass"];
            string server = @System.Configuration.ConfigurationManager.AppSettings["server"];
            string db = System.Configuration.ConfigurationManager.AppSettings["db"];


            TableLogOnInfo Logon = new TableLogOnInfo();

            foreach (CrystalDecisions.CrystalReports.Engine.Table t in rdc.Database.Tables)
            {
                Logon = t.LogOnInfo;
                Logon.ConnectionInfo.ServerName = server;
                Logon.ConnectionInfo.DatabaseName = db;
                Logon.ConnectionInfo.UserID = user;
                Logon.ConnectionInfo.Password = pass;
                t.ApplyLogOnInfo(Logon);
            }



            foreach (ReportDocument subreport in rdc.Subreports)
            {
                foreach (CrystalDecisions.CrystalReports.Engine.Table t in rdc.Database.Tables)
                {
                    Logon = t.LogOnInfo;
                    Logon.ConnectionInfo.ServerName = server;
                    Logon.ConnectionInfo.DatabaseName = db;
                    Logon.ConnectionInfo.UserID = user;
                    Logon.ConnectionInfo.Password = pass;
                    t.ApplyLogOnInfo(Logon);
                }
            }

            CrystalReportViewer1.DataBind();
        }


        private string GetNombreReporte(int caller)
        {
            string nombreReporte = string.Empty;

            switch (caller)
            {
                case 1:
                    nombreReporte = "rptEvaluacionPOA.rpt";
                    break;
                case 2:
                    nombreReporte = "rptDSP.rpt";
                    break;
                case 3:
                    nombreReporte = "rtpDetalleObrasDSP.rpt";
                    break;
                case 4:
                    nombreReporte = "rptDetalleObrasRechazadasDSP.rpt";
                    break;
                case 5:
                    nombreReporte = "rptRPAI.rpt";
                    break;
                case 6:
                    nombreReporte = "rptDetalleObrasRPAI.rpt";
                    break;
                //20's REPORTES DE TECHO FINANCIERO
                case 21:
                    nombreReporte = "FuentesDeFinanciamientoDisponibles.rpt";
                    break;
                case 22:
                    nombreReporte = "TechosFinancierosXup.rpt";
                    break;

                //30'S REPORTES DE CONTROL FINANCIERO
                case 31:
                    nombreReporte = "rptControlFinancieroAnticiposOrdenDePago.rpt";
                    break;
                case 32:
                    nombreReporte = "rptControlFinancieroConceptosDeLaEstimacion.rpt";
                    break;
                case 33:
                    nombreReporte = "rptControlFinancieroCantidadesEstimadas.rpt";
                    break;
                case 34:
                    nombreReporte = "rptControlFinancieroConcentradoDeEstimaciones.rpt";
                    break;

            }


            return nombreReporte;

        }




    }
}
using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.IO;
using ClosedXML.Excel;
using System.Data;
using System.Data.SqlClient;

namespace SIP.Formas.ControlFinanciero
{
    public partial class wfEstimacionesNew : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            uow = new UnitOfWork(Session["IdUser"].ToString());
            this.idObra = int.Parse(Session["XidObra"].ToString());
            int usuario = int.Parse(Session["IdUser"].ToString());

            if (!IsPostBack)
            {
                divTMP.Style.Add("display", "none");
                divBtnImprimir.Style.Add("display","none");


                _URLVisor.Value = ResolveClientUrl("~/rpts/wfVerReporte.aspx");
                _idObra.Value = idObra.ToString();
                _idUsuario.Value = usuario.ToString();    

            }
        }

        protected void btnCargarConceptos_Click(object sender, EventArgs e)
        {
             
            string R;
            int usuario = int.Parse(Session["IdUser"].ToString());

            if (fileUpload.PostedFile.FileName.Equals(string.Empty))//vacio
                return;            
            
            if (fileUpload.FileBytes.Length > 10485296)//Validar el tamaño del archivo
                return;
            
            R = CargarArchivoExcel(fileUpload.PostedFile); //Se guarda el archivo

            if (R == "error")
                return;

            
            _R.Value = R;
            var wb = new XLWorkbook(R);
            var ws = wb.Worksheet(1);
            var firstRowUsed = ws.FirstRow();
            var lastRowUsed = ws.LastRowUsed();
            var rows = ws.FirstRow();

            int nregistros = lastRowUsed.RowNumber() - firstRowUsed.RowNumber();

            BorrarTMPbyUsuario();

            this.idObra = int.Parse(Session["XidObra"].ToString());

            ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).FirstOrDefault();

                    nregistros++;
                    int i = 0;

                    decimal cantidad;
                    while (nregistros > 0)
                    {
                        if (i > 9)
                        {
                            EstimacionesConceptosTMP obj = new EstimacionesConceptosTMP();

                            try
                            {
                                cantidad = decimal.Parse(rows.Cell("E").GetString());
                            }
                            catch
                            {
                                cantidad = 0;
                            }

                            obj.Usuario = usuario;
                            obj.ContratoDeObraId = contrato.Id;

                            obj.Numero = double.Parse(rows.Cell("A").GetString());
                            obj.Inciso = rows.Cell("B").GetString();
                            obj.Descripcion = rows.Cell("C").GetString();
                            obj.UnidadDeMedida = rows.Cell("D").GetString();
                            obj.Cantidad = double.Parse(cantidad.ToString());

                            obj.StatusFechas = 1;
                            obj.StatusFechasNombre = " ";
                            uow.EstimacionesConceptosTMPBL.Insert(obj);
                        }
                        

                        rows = rows.RowBelow();
                        nregistros--;
                        i++;
                    }

                    uow.SaveChanges();


                    //evaluar los registros

                    SqlConnection sqlConnection1 = new SqlConnection(uow.Contexto.Database.Connection.ConnectionString.ToString());
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
             
                    cmd.CommandText = "sp_evaluarConceptosDeLaNuevaEstimacion";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection1;

                    cmd.Parameters.Add("@usuario", usuario);
                    cmd.Parameters.Add("@contrato", contrato.Id);
                    sqlConnection1.Open();

                    reader = cmd.ExecuteReader();            
                    sqlConnection1.Close();

                    uow = new UnitOfWork(Session["IdUser"].ToString());
                    

                    this.grid.DataSource = uow.EstimacionesConceptosTMPBL.Get(p=>p.Usuario == usuario).ToList ();
                    this.grid.DataBind();


                    List<EstimacionesConceptosTMP> listaConceptos = uow.EstimacionesConceptosTMPBL.Get(p => p.Usuario == usuario).ToList();

                    decimal importeEstimado, iva, total;
                    decimal amortizacion, ret5, ret2, ret2bis,sancion, importefinal;

                    importeEstimado=0;
                    iva=0;
                    total = 0;
                    amortizacion=0;
                    ret5=0;
                    ret2=0;
                    ret2bis = 0;
                    sancion = 0;
                    importefinal = 0;

                    foreach (EstimacionesConceptosTMP item in listaConceptos)
                    {
                        importeEstimado = importeEstimado + item.subtotal;
                        iva = iva + item.iva;
                        total = total + item.total;
                        amortizacion = amortizacion + item.amortizacion;
                        ret5 = ret5 + item.retencion5;
                        ret2 = ret2 + item.retencion2;
                        ret2bis = ret2bis + item.retencion2SyV;
                        sancion = sancion + item.sancion;
                        importefinal = importefinal + item.importeFinal;

                    }


                    txtImporteEstimado.Text = importeEstimado.ToString("C2");
                    txtIVA.Text = iva.ToString("C2");
                    txtImporteTotal.Text = total.ToString("C2");
                    txtAmortizacion.Text = amortizacion.ToString("C2");
                    txtRetencion5.Text = ret5.ToString("C2");
                    txtRetencion2.Text = ret2.ToString("C2");
                    txtRet2Bis.Text = ret2bis.ToString("C2"); 
                    txtImporteApagar.Text = importefinal.ToString("C0");


                    List<Estimaciones> listaEstimaciones = uow.EstimacionesBL.Get(p=>p.ContratoDeObra.ObraId == this.idObra ).ToList();

                    txtNumEstimacion.Text = (listaEstimaciones.Max(p => p.NumeroDeEstimacion) + 1).ToString();

                    this.divTMP.Style.Add("display", "block");
                    this.divCargarArchivo.Style.Add("display", "none");
                    this.divBtnImprimir.Style.Add("display","block");


                



            
        }


        private string CargarArchivoExcel(HttpPostedFile postedFile)
        {

            //string M = string.Empty;
            //List<string> R = new List<string>();

            string ruta = string.Empty;


            try
            {

                ruta = System.Configuration.ConfigurationManager.AppSettings["ArchivosControlFinanciero"]; //Se recupera nombre de la carpeta del archivo WEB.CONFIG

                if (!ruta.EndsWith("/"))
                    ruta += "/";

                //ruta += idPregunta.ToString() + "/"; //Se asigna el ID de la Pregunta

                if (ruta.StartsWith("~") || ruta.StartsWith("/"))   //Es una ruta relativa al sitio
                    ruta = Server.MapPath(ruta);


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta); //Se crea la carpeta

                ruta += postedFile.FileName;

                postedFile.SaveAs(ruta); //Se guarda el archivo

            }
            catch (Exception ex)
            {
                //M = ex.Message;
                ruta = "error";
            }

            //R.Add(M);
            //R.Add(ruta);
            //return R;

            return ruta;

        }

        private void BorrarTMPbyUsuario()
        {
            int usuario = int.Parse(Session["IdUser"].ToString());
            List<EstimacionesConceptosTMP> lista = uow.EstimacionesConceptosTMPBL.Get(p => p.Usuario == usuario).ToList();

            foreach (EstimacionesConceptosTMP elemento in lista)
                uow.EstimacionesConceptosTMPBL.Delete(elemento);

            uow.SaveChanges();


        }

        protected void btnGuardarEstimacion_Click(object sender, EventArgs e)
        {
            int usuario = int.Parse(Session["IdUser"].ToString());
            this.idObra = int.Parse(Session["XidObra"].ToString());

            Estimaciones estimacion = new Estimaciones();
            ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p=>p.ObraId == this.idObra ).FirstOrDefault();



            decimal importeEstimado, iva, total;
            decimal amortizacion, ret5, ret2, ret2Bis, importefinal;

            importeEstimado = 0;
            iva = 0;
            total = 0;
            amortizacion = 0;
            ret5 = 0;
            ret2 = 0;
            ret2Bis = 0;
            importefinal = 0;

            List<EstimacionesConceptosTMP> listaConceptos = uow.EstimacionesConceptosTMPBL.Get(p => p.Usuario == usuario && p.Status == 1).ToList();

            foreach (EstimacionesConceptosTMP item in listaConceptos)
            {
                importeEstimado = importeEstimado + item.subtotal;
                iva = iva + item.iva;
                total = total + item.total;
                amortizacion = amortizacion + item.amortizacion;
                ret5 = ret5 + item.retencion5;
                ret2 = ret2 + item.retencion2;
                ret2Bis = ret2Bis + item.retencion2SyV;
                importefinal = importefinal + item.importeFinal;

            }

            List<Estimaciones> listaEstimaciones = uow.EstimacionesBL.Get(p=>p.ContratoDeObra.ObraId == this.idObra ).ToList(); 




            estimacion.ContratoDeObraId = contrato.Id;
            estimacion.NumeroDeEstimacion = listaEstimaciones.Max(p=>p.NumeroDeEstimacion) + 1;
            estimacion.FechaDeEstimacion = DateTime.Parse(dtpFecha.Value.ToString());

            estimacion.ImporteEstimado = importeEstimado;
            estimacion.IVA = iva;
            estimacion.Total = total ;
            estimacion.AmortizacionAnticipo = amortizacion;
            estimacion.Retencion2AlMillar = ret2;
            estimacion.Retencion5AlMillar = ret5;
            estimacion.Retencion2AlMillarSV = ret2Bis;
            estimacion.Sanciones = 0;
            estimacion.ISR = 0;
            estimacion.Otros = 0;
            estimacion.PeriodoInicio = DateTime.Parse(dtpFecha.Value.ToString());
            estimacion.PeriodoTermino = DateTime.Parse(dtpFecha.Value.ToString());
            estimacion.ImporteNetoACobrar = importefinal;
            estimacion.ConceptoDePago = "est" + txtNumEstimacion.Text; 
            estimacion.Status = 1;
            estimacion.FolioCL = "123";

            uow.EstimacionesBL.Insert(estimacion);

            

            foreach (EstimacionesConceptosTMP item in listaConceptos)
            {
                EstimacionesConceptos concepto = new EstimacionesConceptos();

                concepto.Estimacion = estimacion;
                concepto.PresupuestoContratadoId = item.PresupuestoContratadoId;
                concepto.Cantidad = item.Cantidad;
                concepto.Subtotal = item.subtotal;

                uow.EstimacionesConceptosBL.Insert(concepto);
            }

            Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);
                obra.StatusControlFinanciero = 6;
            uow.ObraBusinessLogic.Update(obra);




            //temporal mientras no esta el modulo de registrar pagos
            Estimaciones lastEstimacion = uow.EstimacionesBL.Get(p => p.NumeroDeEstimacion < estimacion.NumeroDeEstimacion).OrderByDescending(q => q.NumeroDeEstimacion).First();
                lastEstimacion.Status = 2;
            uow.EstimacionesBL.Update(lastEstimacion);

            uow.SaveChanges();

            if(uow.Errors.Count == 0)
                Response.Redirect("wfEstimaciones.aspx");
        }


    }
}
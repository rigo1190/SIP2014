using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.OleDb;
using System.Data;

using System.IO;
using ClosedXML.Excel;



namespace SIP.Formas.ControlFinanciero
{
    public partial class wfSeguimiento : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;
        private int idEjercicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            idObra = int.Parse(Request.QueryString["id"].ToString());
            idEjercicio = int.Parse(Session["EjercicioId"].ToString());

            if (!IsPostBack)
            {
                BindX();
            }
        }

        private void BindX()
        {
            //datos generales de la obra
            Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);

            txtNoObra.Value = obra.Numero;
            txtDescripcionObra.Value = obra.Descripcion;


            if (obra.StatusControlFinanciero > 0)//datos del contrato
            {
                ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).First();

                txtNumContrato.Value = contrato.NumeroDeContrato;

                txtRFC.Value = contrato.RFCcontratista;
                txtRazonSocial.Value = contrato.RazonSocialContratista;
                txtImporteTotal.Value = contrato.Total.ToString();

                dtpContrato.Value = String.Format("{0:d}", contrato.FechaDelContrato);
                dtpInicio.Value = String.Format("{0:d}", contrato.FechaDeInicio);
                dtpTermino.Value = String.Format("{0:d}", contrato.FechaDeTermino);

                txtPorcentajeAnticipo.Value = contrato.PorcentajeDeAnticipo.ToString();
                chk5almillar.Checked = contrato.Descontar5AlMillar;
                chk2almillar.Checked = contrato.Descontar2AlMillar;
                
            }


            if (obra.StatusControlFinanciero > 3)
            {
                divBtnGuardarContrato.Style.Add("display","none");
            }


        }

        protected void btnGuardarContrato_Click(object sender, EventArgs e)
        {
            List<ContratosDeObra> listaContrato = uow.ContratosDeObraBL.Get(p=>p.ObraId == this.idObra).ToList();



            ContratosDeObra contrato;

            if (listaContrato.Count == 0)
            {
                contrato = new ContratosDeObra();

                contrato.ObraId = this.idObra;
                contrato.NumeroDeContrato = txtNumContrato.Value;
                contrato.RFCcontratista = txtRFC.Value;
                contrato.RazonSocialContratista = txtRazonSocial.Value;
                contrato.Total = decimal.Parse( txtImporteTotal.Value.ToString());

                contrato.FechaDelContrato = DateTime.Parse(dtpContrato.Value.ToString());
                contrato.FechaDeInicio = DateTime.Parse( dtpInicio.Value.ToString());
                contrato.FechaDeTermino = DateTime.Parse(dtpTermino.Value.ToString());

                contrato.PorcentajeDeAnticipo = double.Parse(txtPorcentajeAnticipo.Value.ToString());
                contrato.Descontar5AlMillar = bool.Parse(chk5almillar.Checked.ToString());
                contrato.Descontar2AlMillar = bool.Parse(chk2almillar.Checked.ToString());

               // uow.ContratosDeObraBL.Insert(contrato);
                

            }
            else
            {
                contrato = uow.ContratosDeObraBL.GetByID(this.idObra);

                contrato.NumeroDeContrato = txtNumContrato.Value;
                contrato.RFCcontratista = txtRFC.Value;
                contrato.RazonSocialContratista = txtRazonSocial.Value;
                contrato.Total = decimal.Parse(txtImporteTotal.Value.ToString());

                contrato.FechaDelContrato = DateTime.Parse(dtpContrato.Value.ToString());
                contrato.FechaDeInicio = DateTime.Parse(dtpInicio.Value.ToString());
                contrato.FechaDeTermino = DateTime.Parse(dtpTermino.Value.ToString());

                contrato.PorcentajeDeAnticipo = double.Parse(txtPorcentajeAnticipo.Value.ToString());
                contrato.Descontar5AlMillar = bool.Parse(chk5almillar.Value.ToString());
                contrato.Descontar2AlMillar = bool.Parse(chk2almillar.Value.ToString());

               // uow.ContratosDeObraBL.Update(contrato);
            
            }

            //Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);

            //if (obra.StatusControlFinanciero == 0)
            //{
            //    obra.StatusControlFinanciero = 1;
            //    uow.ObraBusinessLogic.Update(obra);
            //}



          //  uow.SaveChanges();

            



        }







        public static DataTable selectExcel(string Arch, string Hoja)
        {

            OleDbConnection Conex = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Arch + ";Extended Properties=Excel 12.0;");

            OleDbCommand CmdOle = new OleDbCommand();

            CmdOle.Connection = Conex;
            CmdOle.CommandType = CommandType.Text;
            CmdOle.CommandText = "SELECT * FROM [" + Hoja + "$A6:DF100]";

            OleDbDataAdapter AdaptadorOle = new OleDbDataAdapter(CmdOle.CommandText, Conex);

            DataTable dt = new DataTable();

            AdaptadorOle.Fill(dt);

            return dt;
        }

        protected void btnCargarConceptosDeObra_Click(object sender, EventArgs e)
        {
            
            //List<string> R = new List<string>();
            string R;
            
            if (!fileUpload.PostedFile.FileName.Equals(string.Empty))//Se tiene que almacenar el archivo adjunto, si es que se cargo uno
            {
                if (fileUpload.FileBytes.Length > 10485296)//Validar el tamaño del archivo
                {
                                 return;
                }



                R = GuardarArchivo(fileUpload.PostedFile); //Se guarda el archivo



                if (R != "error")
                {


                    var wb = new XLWorkbook(R);
                    var ws = wb.Worksheet(1);
                    var firstRowUsed = ws.FirstRowUsed();
                    var lastRowUsed = ws.LastRowUsed();
                    var rows = firstRowUsed.RowUsed();


                    string cad = "";

                    int nregistros = lastRowUsed.RowNumber() - firstRowUsed.RowNumber();

                    nregistros++;

                    while (nregistros > 0)
                    {

                        cad += rows.Cell("A").GetString();

                        rows = rows.RowBelow();
                        nregistros--;
                    }

                    txtcontenido.Value = cad;

                }

                
            }





            
        }





        private string GuardarArchivo(HttpPostedFile postedFile)
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






    }
}
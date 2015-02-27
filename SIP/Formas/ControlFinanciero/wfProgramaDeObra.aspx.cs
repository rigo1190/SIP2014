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
    public partial class wfProgramaDeObra : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork(Session["IdUser"].ToString());
            idObra = int.Parse(Session["XidObra"].ToString());

            Obra obra = uow.ObraBusinessLogic.GetByID(idObra);

            if (!IsPostBack)
            {

                divErrorEnLosRegistros.Style.Add("display", "none");

                if (obra.StatusControlFinanciero < 2)//no hay presupuestoContratado registrado
                {
                    //divMSGnoHayPresupuesto.Style.Add("display", "none");  --> Mostrar
                    divCargarArchivo.Style.Add("display", "none");
                    divGuardarProgramaDeObra.Style.Add("display", "none");
                    divTMP.Style.Add("display", "none");
                    divProgramaDeObra.Style.Add("display", "none");
                }


                if (obra.StatusControlFinanciero == 2)// ya esta cargado el presupuesto contratado
                {
                    divMSGnoHayPresupuesto.Style.Add("display", "none");  
                    //divCargarArchivo.Style.Add("display", "none");--> mostrar
                    divGuardarProgramaDeObra.Style.Add("display", "none");
                    divTMP.Style.Add("display", "none");
                    divProgramaDeObra.Style.Add("display", "none");
                }


                if (obra.StatusControlFinanciero > 2)// ya esta registrado el programa de obra
                {
                    divMSGnoHayPresupuesto.Style.Add("display", "none");
                    divCargarArchivo.Style.Add("display", "none");
                    divGuardarProgramaDeObra.Style.Add("display", "none");
                    divTMP.Style.Add("display", "none");
                    //divProgramaDeObra.Style.Add("display", "none");--> mostrar
                    cargarProgramaDeObra();
                }
            }


        }

        protected void btnCargarConceptosDeObra_Click(object sender, EventArgs e)
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
            DateTime? inicio, termino;
            while (nregistros > 0)
            {
                if (i > 9)
                {
                    ProgramasDeObrasTMP obj = new ProgramasDeObrasTMP();

                    try
                    {
                        cantidad = decimal.Parse(rows.Cell("E").GetString());
                        inicio = DateTime.Parse(rows.Cell("F").GetString());
                        termino = DateTime.Parse(rows.Cell("G").GetString());
                    }
                    catch
                    {
                        cantidad = 0;
                        inicio = null;
                        termino = null;
                    }

                    obj.Usuario = usuario;
                    obj.ContratoDeObraId = contrato.Id;

                    obj.Numero = double.Parse(rows.Cell("A").GetString());
                    obj.Inciso = rows.Cell("B").GetString();
                    obj.Descripcion = rows.Cell("C").GetString();
                    obj.UnidadDeMedida = rows.Cell("D").GetString();
                    obj.Cantidad = double.Parse(cantidad.ToString());
                    obj.Inicio = inicio;
                    obj.Termino  = termino;

                    uow.ProgramasDeObraTMPBL.Insert(obj);
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

            cmd.CommandText = "sp_evaluarProgramDeObra";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = sqlConnection1;

            cmd.Parameters.Add("@usuario", usuario);
            cmd.Parameters.Add("@contrato", contrato.Id);
            sqlConnection1.Open();

            reader = cmd.ExecuteReader();
            sqlConnection1.Close();


            uow = new UnitOfWork(Session["IdUser"].ToString());



            this.grid.DataSource = uow.ProgramasDeObraTMPBL.Get(p => p.Usuario == usuario).ToList();
            this.grid.DataBind();

            this.divTMP.Style.Add("display", "block");

            List<ProgramasDeObrasTMP> listaFechas = uow.ProgramasDeObraTMPBL.Get(p=>p.Usuario == usuario && p.Status != 1).ToList();

            if (listaFechas.Count == 0)
            {
                this.divCargarArchivo.Style.Add("display", "none");
                this.divGuardarProgramaDeObra.Style.Add("display", "block");
                this.divErrorEnLosRegistros.Style.Add("display", "none"); 
            }
            else
            {
                this.divErrorEnLosRegistros.Style.Add("display","block"); 

            }
            
            
                

        }


        private void BorrarTMPbyUsuario()
        {
            int usuario = int.Parse(Session["IdUser"].ToString());
            List<ProgramasDeObrasTMP> lista = uow.ProgramasDeObraTMPBL.Get(p => p.Usuario == usuario).ToList();

            foreach (ProgramasDeObrasTMP elemento in lista)
                uow.ProgramasDeObraTMPBL.Delete(elemento);

            uow.SaveChanges();


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
        
        protected void btnGuardarProgramaDeObra_Click(object sender, EventArgs e)
        {
            int usuario = int.Parse(Session["IdUser"].ToString());
            this.idObra = int.Parse(Session["XidObra"].ToString());

            
            ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).FirstOrDefault();
            


            List<ProgramasDeObrasTMP> listaFechas = uow.ProgramasDeObraTMPBL.Get(p => p.Usuario == usuario && p.Status == 1 && p.Cantidad > 0).ToList();

            foreach (ProgramasDeObrasTMP item in listaFechas )
            {
                ProgramasDeObras programacion = new ProgramasDeObras();

                programacion.ContratoDeObra = contrato;
                programacion.PresupuestoContratadoId = item.PresupuestoContratadoId;
                programacion.Inicio = item.Inicio.Value;
                programacion.Termino = item.Termino.Value;
                programacion.Status = 1;
                uow.ProgramasDeObrasBL.Insert(programacion);
            }

    
            Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);
                obra.StatusControlFinanciero = 3;
            uow.ObraBusinessLogic.Update(obra);


            uow.SaveChanges();

            if (uow.Errors.Count == 0){

                divMSGnoHayPresupuesto.Style.Add("display", "none");
                divCargarArchivo.Style.Add("display", "none");
                divGuardarProgramaDeObra.Style.Add("display","none");
                divTMP.Style.Add("display", "none");                
                divProgramaDeObra.Style.Add("display","block");

                cargarProgramaDeObra();

            }

                 
        }
















        private void cargarProgramaDeObra()
        {
            idObra = int.Parse(Session["XidObra"].ToString());

            List<PresupuestosContratados> listaPadres = uow.PresupuestosContratadosBL.Get(p => p.ContratoDeObra.Obra.Id == idObra && p.Nivel == 1).ToList();


            int i = 0;
            foreach (PresupuestosContratados padre in listaPadres)
            {
                i++;

                System.Web.UI.HtmlControls.HtmlGenericControl divPanel = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelHeading = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelCollapse = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelBody = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                System.Web.UI.HtmlControls.HtmlGenericControl h4 = new System.Web.UI.HtmlControls.HtmlGenericControl("H4");
                System.Web.UI.HtmlControls.HtmlGenericControl a = new System.Web.UI.HtmlControls.HtmlGenericControl("A");

                System.Web.UI.HtmlControls.HtmlGenericControl p = new System.Web.UI.HtmlControls.HtmlGenericControl("P");

                System.Web.UI.HtmlControls.HtmlGenericControl tabla = new System.Web.UI.HtmlControls.HtmlGenericControl("TABLE");


                //heading
                divPanelHeading.Attributes.Add("class", "panel-heading");

                h4.Attributes.Add("class", "panel-title");

                a.Attributes.Add("data-toggle", "collapse");
                a.Attributes.Add("data-parent", "#accordion");
                a.Attributes.Add("href", "#collapse" + i.ToString());
                a.InnerText = padre.Numero + " : " + padre.Descripcion;

                h4.Controls.Add(a);
                divPanelHeading.Controls.Add(h4);


                //Collapse
                divPanelCollapse.Attributes.Add("id", "collapse" + i.ToString());
                divPanelCollapse.Attributes.Add("class", "panel-collapse collapse");


                divPanelBody.Attributes.Add("class", "panel-body");



                List<PresupuestosContratados> detalle = uow.PresupuestosContratadosBL.Get(q => q.ParentId == padre.Id).ToList();

                tabla.Attributes.Add("class", "table");
                tabla.Attributes.Add("cellspacing", "0");

                System.Web.UI.HtmlControls.HtmlGenericControl trHead = new System.Web.UI.HtmlControls.HtmlGenericControl("TR");
                System.Web.UI.HtmlControls.HtmlGenericControl thOne = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thTwo = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thThree = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thFour = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thFive = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thSix = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thSeven = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");

                trHead.Attributes.Add("align", "center");


                thOne.InnerText = "No";
                thTwo.InnerText = "Inciso";
                thThree.InnerText = "Concepto";
                thFour.InnerText = "U.M.";
                thFive.InnerText = "Cantidad";
                thSix.InnerText = "Inicio";
                thSeven.InnerText = "Termino";

                trHead.Controls.Add(thOne);
                trHead.Controls.Add(thTwo);
                trHead.Controls.Add(thThree);
                trHead.Controls.Add(thFour);
                trHead.Controls.Add(thFive);
                trHead.Controls.Add(thSix);
                trHead.Controls.Add(thSeven);

                tabla.Controls.Add(trHead);


                foreach (PresupuestosContratados item in detalle)
                {

                    System.Web.UI.HtmlControls.HtmlGenericControl tr = new System.Web.UI.HtmlControls.HtmlGenericControl("TR");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdOne = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdTwo = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdThree = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdFour = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdFive = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdSix = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdSeven = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");

                    tdOne.Attributes.Add("align", "left");
                    tdOne.InnerText = item.Numero.ToString();
                    tdTwo.InnerText = item.Inciso;


                    tdThree.InnerText = item.Descripcion;
                    tdFour.InnerText = item.UnidadDeMedida;
                    tdFive.InnerText = item.Cantidad.ToString();

                    ProgramasDeObras programa = uow.ProgramasDeObrasBL.Get(q => q.PresupuestoContratadoId == item.Id).FirstOrDefault();
                    
                    tdSix.InnerText =  programa.Inicio.ToString("d");
                    tdSix.Attributes.Add("align", "right");

                    tdSeven.InnerText = programa.Termino.ToString("d");
                    tdSeven.Attributes.Add("align", "right");

                    tr.Controls.Add(tdOne);
                    tr.Controls.Add(tdTwo);
                    tr.Controls.Add(tdThree);
                    tr.Controls.Add(tdFour);
                    tr.Controls.Add(tdFive);
                    tr.Controls.Add(tdSix);
                    tr.Controls.Add(tdSeven);


                    tabla.Controls.Add(tr);
                }





                divPanelBody.Controls.Add(tabla);
                divPanelCollapse.Controls.Add(divPanelBody);


                //Agregar Elemento
                divPanel.Attributes.Add("class", "panel panel-default");
                divPanel.Controls.Add(divPanelHeading);
                divPanel.Controls.Add(divPanelCollapse);

                this.accordion.Controls.Add(divPanel);

            }


        }




    }
}
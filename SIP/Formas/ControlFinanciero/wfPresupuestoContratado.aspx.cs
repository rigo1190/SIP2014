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

namespace SIP.Formas.ControlFinanciero
{
    public partial class wfPresupuestoContratado : System.Web.UI.Page
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

                if (obra.StatusControlFinanciero == 0)//no hay contrato registrado
                {
                    //divMSGnoHayContrato.Style.Add("display", "none");     --> mostrar
                    divCargarArchivo.Style.Add("display","none");
                    divGuardarPresupuesto.Style.Add("display", "none");
                    divTMP.Style.Add("display", "none");
                    divPresupuesto.Style.Add("display", "none");
                }

                if (obra.StatusControlFinanciero == 1)// ya esta cargado el contrato
                {
                    divMSGnoHayContrato.Style.Add("display", "none");
                    //divCargarArchivo.Style.Add("display", "none");    --> mostrar
                    divGuardarPresupuesto.Style.Add("display", "none");
                    divTMP.Style.Add("display", "none");
                    divPresupuesto.Style.Add("display", "none");
                }


                if (obra.StatusControlFinanciero > 1)// ya esta registrado el presupuesto contratado
                {
                    divMSGnoHayContrato.Style.Add("display", "none");
                    divCargarArchivo.Style.Add("display", "none");    
                    divGuardarPresupuesto.Style.Add("display", "none");
                    divTMP.Style.Add("display", "none");
                    //divPresupuesto.Style.Add("display", "none");      --> mostrar
                    
                    cargarPresupuestoContratado();
                }


                
            }

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



                R = CargarArchivoExcel(fileUpload.PostedFile); //Se guarda el archivo



                if (R != "error")
                {
                    _R.Value = R;

                    var wb = new XLWorkbook(R);
                    var ws = wb.Worksheet(1);
                    var firstRowUsed = ws.FirstRow();
                    var lastRowUsed = ws.LastRowUsed();
                    var rows = ws.FirstRow();  

                    int nregistros = lastRowUsed.RowNumber() - firstRowUsed.RowNumber();
                    

                    DataTable table = new DataTable();
                    table.Columns.Add("id");
                    table.Columns.Add("No");
                    table.Columns.Add("Inciso");
                    table.Columns.Add("Concepto");
                    table.Columns.Add("UM");
                    table.Columns.Add("Cantidad");
                    table.Columns.Add("Precio");
                    table.Columns.Add("Subtotal");


                    

                    nregistros++;
                    int i = 0;

                    decimal importe;
                    decimal importeTotal=0;
                    decimal cantidad;
                    while (nregistros > 0)
                    {
                        DataRow row = table.NewRow();

                        try {
                            cantidad = decimal.Parse(rows.Cell("E").GetString());
                            importe = decimal.Parse(rows.Cell("F").GetString());
                        }
                        catch {
                            cantidad = 0;
                            importe = 0;
                        }
                        

                        row["id"] = i;
                        row["No"] = rows.Cell("A").GetString();
                        row["Inciso"] = rows.Cell("B").GetString();
                        row["Concepto"] = rows.Cell("C").GetString();
                        row["UM"] = rows.Cell("D").GetString();

                        if (cantidad == 0 && importe == 0)
                        {
                            row["Cantidad"] = null;
                            row["Precio"] = null;
                            row["Subtotal"] = null;
                        }
                        else
                        {                        
                            row["Cantidad"] = cantidad;
                            row["Precio"] = importe.ToString("C2");
                            row["Subtotal"] = (cantidad * importe).ToString("C2");

                            importeTotal = importeTotal + (cantidad * importe);
                        }

                        if (i > 9)
                            table.Rows.Add(row);

                        rows = rows.RowBelow();
                        nregistros--;
                        i++;
                    }

                    this.grid.DataSource = table;
                    this.grid.DataBind();

                    this.divCargarArchivo.Style.Add("display", "none");
                    this.divGuardarPresupuesto.Style.Add("display", "block");
                    this.divTMP.Style.Add("display","block");

                    this.btnGuardarPresupuesto.Text = "Guardar Presupuesto Contratado por: " + importeTotal.ToString("C0") ;


                    if (importeTotal == 0) {
                        this.divGuardarPresupuesto.Style.Add("display", "none");
                    }

                }

                

            }
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

        protected void btnGuardarPresupuestoContratado_Click(object sender, EventArgs e)
        {

            idObra = int.Parse(Session["XidObra"].ToString());

            Obra obra = uow.ObraBusinessLogic.GetByID(idObra);
            ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p=>p.ObraId == idObra).FirstOrDefault();
            
            int idContratoDeObra = contrato.Id;

            var wb = new XLWorkbook(_R.Value);
            var ws = wb.Worksheet(1);
            var firstRowUsed = ws.FirstRow();
            var lastRowUsed = ws.LastRowUsed();
            var rows = ws.FirstRow(); 

            int nregistros = lastRowUsed.RowNumber() - firstRowUsed.RowNumber();

            
            nregistros++;
            int i = 0;

            decimal importe;
            decimal cantidad;
             
            PresupuestosContratados padre = null;

            while (nregistros > 0)
            {
                if (i > 9)
                {

                
                    PresupuestosContratados presupuesto = new PresupuestosContratados();
                
                    try
                    {
                        cantidad = decimal.Parse(rows.Cell("E").GetString());
                        importe = decimal.Parse(rows.Cell("F").GetString());
                    }
                    catch
                    {
                        cantidad = 0;
                        importe = 0;
                    }

                    



                    if (cantidad == 0 && importe == 0)
                    {
                        presupuesto.ContratoDeObraId = idContratoDeObra;
                        presupuesto.Nivel = 1;
                        presupuesto.Orden = i;
                        presupuesto.Numero = double.Parse(rows.Cell("A").GetString());
                        presupuesto.Descripcion = rows.Cell("C").GetString();
                        
                        padre = presupuesto;

                        uow.PresupuestosContratadosBL.Insert(presupuesto);

                        
                    }
                    else
                    {
                        presupuesto.ContratoDeObraId = idContratoDeObra;
                        presupuesto.Nivel = 2;
                        presupuesto.Orden = i;
                        presupuesto.Numero = double.Parse(rows.Cell("A").GetString());
                        presupuesto.Inciso = rows.Cell("B").GetString();
                        presupuesto.Descripcion = rows.Cell("C").GetString();
                        presupuesto.UnidadDeMedida = rows.Cell("D").GetString();
                        presupuesto.Cantidad = double.Parse(cantidad.ToString());
                        presupuesto.PrecioUnitario = importe;
                        presupuesto.Subtotal = (cantidad * importe);
                        presupuesto.Parent = padre;
                        uow.PresupuestosContratadosBL.Insert(presupuesto);
                    }                                
                
                }
                rows = rows.RowBelow();
                nregistros--;
                i++;
            }

            

            contrato.Subtotal = contrato.detallePresupuesto.Sum(p => p.Subtotal);
            contrato.IVA = (contrato.Subtotal * decimal.Parse("0.16"));
            contrato.Total = (contrato.Subtotal * decimal.Parse("1.16"));
            uow.ContratosDeObraBL.Update(contrato);

            obra.StatusControlFinanciero = 2;
            uow.ObraBusinessLogic.Update(obra);

            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {
                divMSGnoHayContrato.Style.Add("display", "none");
                divCargarArchivo.Style.Add("display", "none");
                divGuardarPresupuesto.Style.Add("display", "none");
                divTMP.Style.Add("display", "none");
                
                divPresupuesto.Style.Add("display", "block");      

                cargarPresupuestoContratado();

            }

        }




        private void cargarPresupuestoContratado()
        {
            idObra = int.Parse(Session["XidObra"].ToString());

            List<PresupuestosContratados> listaPadres = uow.PresupuestosContratadosBL.Get(p=> p.ContratoDeObra.Obra.Id == idObra && p.Nivel == 1).ToList();

            
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


                    
                List<PresupuestosContratados> detalle = uow.PresupuestosContratadosBL.Get(q=>q.ParentId == padre.Id).ToList();

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
                thSix.InnerText = "Precio";
                thSeven.InnerText = "Subtotal";

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
                    
                    tdSix.InnerText = item.PrecioUnitario.ToString("C2");
                    tdSix.Attributes.Add("align", "right");
                    
                    tdSeven.InnerText = item.Subtotal.ToString("C2");
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
using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class POAAjustadoEvaluacion : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                int unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
                int ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());
                int columnsInicial = grid.Columns.Count;

                UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);

                InsertarTitulosNuevasColumnas(); //Se insertan nuevas columnas, realativas a la evaluacion de plantillas

                BindGrid();

                InsertarNuevasCeldas(columnsInicial); //Se insertan los botnes necesarios para ir a la EVALUACION de la obra
            }
        }

        private void BindGrid()
        {

            int unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            this.grid.DataSource = uow.ObraBusinessLogic.Get(o => o.POA.UnidadPresupuestalId == unidadpresupuestalId & o.POA.EjercicioId == ejercicioId, orderBy: r => r.OrderBy(ro => ro.Numero)).ToList();
            this.grid.DataBind();

        }


        /// <summary>
        /// Metodo encargado de crear nuevos encabezados de columnas, realtivos a la evaluacion de plantillas por cada obra
        /// Creado por Rigoberto TS
        /// 03/11/2014
        /// </summary>
        private void InsertarTitulosNuevasColumnas()
        {
            List<Plantilla> list = GetPlantillas(); //Se obtienen las plantillas exitentes en el catalogo

            if (list.Count > 0)
            {
                foreach (Plantilla p in list)
                {
                    TemplateField colNew = new TemplateField(); //Se agrega la columna, una por cada plantilla existente
                    colNew.HeaderText = p.Descripcion;
                    grid.Columns.Add(colNew);
                }


            }
        }


        /// <summary>
        /// Metodo encargado de agregar los botones necesarios por cada fila del grid, se construye la URL de la pagina de Evaluacion
        /// Creado por Rigoberto TS
        /// 03/11/2014
        /// </summary>
        /// <param name="columnsInicial">A partir de donde se empieza a agregar la celda a la fila</param>
        private void InsertarNuevasCeldas(int columnsInicial)
        {
            List<Plantilla> list = GetPlantillas();

            int inicio;

            foreach (GridViewRow row in grid.Rows) //Se lee cada fila del GRID de OBraS
            {
                string id = grid.DataKeys[row.RowIndex].Values["Id"].ToString(); //Se obtiene el ID

                if (list.Count > 0)
                {
                    inicio = columnsInicial;
                    foreach (Plantilla p in list)
                    {
                        TableCell cell1 = new TableCell();
                        string url = "EvaluacionPOA.aspx?ob=0&pd=" + id + "&o=";

                        HtmlButton button = new HtmlButton();
                        HtmlGenericControl spanButton = new HtmlGenericControl("span");

                        //Se construye el BOTON
                        button.ID = "btn" + p.Orden;
                        button.Attributes.Add("class", "btn btn-default");
                        button.Attributes.Add("data-tipo-operacion", "evaluar");
                        button.Attributes.Add("runat", "server");
                        url += p.Orden.ToString(); //SE AGREGA PARAMETRO DE ORDEN DE LA PLANTILLA a la URL
                        button.Attributes.Add("data-url-poa", url);

                        spanButton.Attributes.Add("class", "glyphicon glyphicon-ok");
                        button.Controls.Add(spanButton);

                        cell1.Controls.Add(button);

                        row.Cells.AddAt(inicio, cell1); //Se agrega la celda a la fila

                        inicio++;

                        row.Cells.RemoveAt(row.Cells.Count - 1);
                    }


                }

                

            }
        }

        /// <summary>
        /// Metodo encargado de obtener las PLANTILLAS PADDRE del catalogo de plantillas
        /// Creado por Rigoberto TS
        /// 03/11/2014
        /// </summary>
        /// <returns></returns>
        private List<Plantilla> GetPlantillas()
        {
            List<Plantilla> list = uow.PlantillaBusinessLogic.Get(e => e.DependeDeId == null).ToList();
            return list;
        }
    }
}
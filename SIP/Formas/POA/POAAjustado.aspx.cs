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
    public partial class POAAjustado : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                int unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
                int ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());
                //int columnsInicial = grid.Columns.Count;

                UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);


                BindGrid();
            }
        }

        private void BindGrid()
        {

            int unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

          
            var list = (from o in uow.ObraBusinessLogic.Get(o => o.POA.UnidadPresupuestalId == unidadpresupuestalId & o.POA.EjercicioId == ejercicioId)
                        join pd in uow.POADetalleBusinessLogic.Get(e=>e.Extemporanea==false)
                        on o.POADetalleId equals pd.Id
                        select pd).ToList();

            this.grid.DataSource = list;//uow.POADetalleBusinessLogic.Get(o => o.POA.UnidadPresupuestalId == unidadpresupuestalId & o.POA.EjercicioId == ejercicioId, orderBy: r => r.OrderBy(ro => ro.Numero)).ToList();
            this.grid.DataBind();

        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int id = Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                string url = "EvaluacionPOA_.aspx?ob=0&pd=" + id;
                HtmlButton button = (HtmlButton)e.Row.FindControl("btnEvaluar");
                button.Attributes.Add("data-tipo-operacion", "evaluar");
                button.Attributes.Add("data-url-poa", url);


                HtmlGenericControl spanAvanceP = (HtmlGenericControl)e.Row.FindControl("spanAvanceP");
                HtmlGenericControl progresoP = (HtmlGenericControl)e.Row.FindControl("progresoP");
                HtmlGenericControl spanAvanceE = (HtmlGenericControl)e.Row.FindControl("spanAvanceE");
                HtmlGenericControl progresoE = (HtmlGenericControl)e.Row.FindControl("progresoE");

                decimal avance = BuscarPorcentajesAvance(id,2); //PLANEACION

                progresoP.Style.Add("width", avance.ToString() + "%");
                spanAvanceP.InnerText = avance.ToString() + "%";


                avance = BuscarPorcentajesAvance(id, 3); //EJECUCION

                progresoE.Style.Add("width", avance.ToString() + "%");
                spanAvanceE.InnerText = avance.ToString() + "%";


            }
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView grid = sender as GridView;
            grid.PageIndex = e.NewPageIndex;
            BindGrid();          
        }


        public decimal BuscarPorcentajesAvance(int idPOADetalle, int orden)
        {

            decimal avance;
            int porcentaje;

            var listPOAPlantillasPlaneacion = (from poapl in uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == idPOADetalle && e.Detalles.Count > 0)
                                               join pl in uow.PlantillaBusinessLogic.Get()
                                               on poapl.PlantillaId equals pl.Id
                                               select new { poaPlantillaID = poapl.Id, Aprobado = poapl.Aprobado, Padre = pl.Padre }).Where(e=>e.Padre.Orden==orden);


            decimal totalPlantillas = listPOAPlantillasPlaneacion.Count();
            decimal totalEvaluadas = listPOAPlantillasPlaneacion.Where(e => e.Aprobado == true).Count();

            if (totalPlantillas > 0)
            {
                avance = Math.Round((totalEvaluadas / (totalPlantillas)) * 100);
                porcentaje = Utilerias.StrToInt(avance.ToString());
            }
            else
                avance = 0;

            return avance;

        }


    }
}
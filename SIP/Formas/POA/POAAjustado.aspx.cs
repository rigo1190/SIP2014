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
                int columnsInicial = grid.Columns.Count;

                UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);


                BindGrid();
            }
        }

        private void BindGrid()
        {

            int unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            this.grid.DataSource = uow.ObraBusinessLogic.Get(o => o.POA.UnidadPresupuestalId == unidadpresupuestalId & o.POA.EjercicioId == ejercicioId, orderBy: r => r.OrderBy(ro => ro.Numero)).ToList();
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

            }
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView grid = sender as GridView;
            grid.PageIndex = e.NewPageIndex;
            BindGrid();          
        }

    }
}
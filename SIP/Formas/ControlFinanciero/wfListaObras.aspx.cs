using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;



namespace SIP.Formas.ControlFinanciero
{
    public partial class wfListaObras : System.Web.UI.Page
    {

        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            this.grid.DataSource = uow.ObraBusinessLogic.Get().ToList();
            this.grid.DataBind();
        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int consecutivo = Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());


                //aqui va el calculo del porcentaje
                consecutivo = consecutivo * 10;

                HtmlGenericControl barra = (HtmlGenericControl)e.Row.FindControl("divBarra");


                System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl span = new System.Web.UI.HtmlControls.HtmlGenericControl("SPAN");

                div.Attributes.Add("class", "progress-bar progress-bar-success progress-bar-striped");
                div.Attributes.Add("role", "progressbar");
                div.Style.Add("width", consecutivo.ToString() + "%");

                span.Attributes.Add("class", "sr-only");
                span.InnerText = "...";

                div.Controls.Add(span);
                barra.Controls.Add(div);





            }
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            int idObra;
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            idObra = int.Parse(grid.DataKeys[row.RowIndex].Values["Id"].ToString());

            Response.Redirect("wfSeguimiento.aspx?id=" + idObra);


        }
    }
}
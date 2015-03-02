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
    public partial class wfContratosDeObra : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork(Session["IdUser"].ToString());
            if (!IsPostBack)
            {
                BindGrid();
            }
        }


        private void BindGrid()
        {
            int idEjercicio = int.Parse(Session["EjercicioId"].ToString());

            this.grid.DataSource = uow.ObraBusinessLogic.Get(p => p.POA.EjercicioId == idEjercicio).ToList();
            this.grid.DataBind();
        }

         
        

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idObra = Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                
                Obra obra = uow.ObraBusinessLogic.GetByID(idObra);


                LinkButton linkC = (LinkButton)e.Row.FindControl("linkContrato");
                LinkButton linkP = (LinkButton)e.Row.FindControl("LinkPresupuesto");
                LinkButton linkPdO = (LinkButton)e.Row.FindControl("LinkProgramacion");
                LinkButton linkPdE = (LinkButton)e.Row.FindControl("LinkProgramacionEstimaciones");
                
                linkC.Text = "Pendiente";
                linkP.Text = "Pendiente";
                linkPdO.Text = "Pendiente";
                linkPdE.Text = "Pendiente";

                if (obra.StatusControlFinanciero > 0)
                {
                    ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == idObra).First();
                    linkC.Text = contrato.NumeroDeContrato;
                    
                    if (obra.StatusControlFinanciero > 1)
                        linkP.Text = contrato.Total.ToString("C0");

                    if (obra.StatusControlFinanciero > 2)
                        linkPdO.Text = "Cargado...";
                        //linkPdO.CssClass = "alert-success";

                    if (obra.StatusControlFinanciero > 3)
                        linkPdE.Text = "Cargadas...";

                }

              


            }

        }

        protected void linkContrato_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
            
            Session["XidObra"] = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
                        
            Response.Redirect("wfContrato.aspx");
        }

        protected void LinkPresupuesto_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Session["XidObra"] = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Response.Redirect("wfPresupuestoContratado.aspx");
        }

        protected void LinkProgramacion_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Session["XidObra"] = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Response.Redirect("wfProgramaDeObra.aspx");
        }

        protected void LinkProgramacionEstimaciones_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Session["XidObra"] = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Response.Redirect("wfProgramacionEstimaciones.aspx");
        }



    }
}
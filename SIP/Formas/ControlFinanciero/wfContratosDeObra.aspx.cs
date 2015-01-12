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
                int idObra = Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                
                Obra obra = uow.ObraBusinessLogic.GetByID(idObra);


                LinkButton linkC = (LinkButton)e.Row.FindControl("linkContrato");
                LinkButton linkP = (LinkButton)e.Row.FindControl("LinkPresupuesto");
                
                linkC.Text = "Pendiente";
                linkP.Text = "Pendiente";

                if (obra.StatusControlFinanciero > 0)
                {
                    ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == idObra).First();
                    linkC.Text = contrato.NumeroDeContrato;
                    
                    if (obra.StatusControlFinanciero > 1)
                        linkP.Text = contrato.Total.ToString("C2");
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



    }
}
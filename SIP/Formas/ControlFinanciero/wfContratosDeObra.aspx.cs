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



                HtmlGenericControl divContrato = (HtmlGenericControl)e.Row.FindControl("DIVcontrato");

                HtmlGenericControl divPresupuesto = (HtmlGenericControl)e.Row.FindControl("DIVPresupuestoContratado");



                System.Web.UI.HtmlControls.HtmlGenericControl linkContrato = new System.Web.UI.HtmlControls.HtmlGenericControl("A");
                System.Web.UI.HtmlControls.HtmlGenericControl linkPresupuesto = new System.Web.UI.HtmlControls.HtmlGenericControl("A");


                 

                linkContrato.Attributes.Add("href", "wfContrato.aspx?id=" + idObra);                
                linkPresupuesto.Attributes.Add("href", "wfPresupuestoContratado.aspx?id=" + idObra);


                if (obra.StatusControlFinanciero > 0)
                {
                    ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == idObra).First();
                    linkContrato.InnerText = contrato.NumeroDeContrato;

                    if (obra.StatusControlFinanciero > 1)
                        linkPresupuesto.InnerText = contrato.Total.ToString("C2");
                    else
                        linkPresupuesto.InnerText = "Pendiente";
                }
                else
                {
                    linkContrato.InnerText = "Pendiente";
                    linkPresupuesto.InnerText = "Pendiente";
                }


                


                divContrato.Controls.Add(linkContrato);
                divPresupuesto.Controls.Add(linkPresupuesto);





            }

        }


    }
}
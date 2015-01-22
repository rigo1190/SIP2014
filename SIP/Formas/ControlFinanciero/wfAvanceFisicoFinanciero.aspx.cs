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
    public partial class wfAvanceFisicoFinanciero : System.Web.UI.Page
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
            int idEjercicio = int.Parse(Session["EjercicioId"].ToString());
            this.grid.DataSource = uow.ObraBusinessLogic.Get(p => p.POA.EjercicioId == idEjercicio &&  p.StatusControlFinanciero > 3).ToList();
            this.grid.DataBind();
        }


        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idObra = Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                Obra obra = uow.ObraBusinessLogic.GetByID(idObra);



                
                HtmlGenericControl divAvFisico = (HtmlGenericControl)e.Row.FindControl("DIVAvanceFisico");
                HtmlGenericControl divAvFinanciero = (HtmlGenericControl)e.Row.FindControl("DIVAvanceFinanciero");
                
                System.Web.UI.HtmlControls.HtmlGenericControl spanAvanceFisico = new System.Web.UI.HtmlControls.HtmlGenericControl("SPAN");
                System.Web.UI.HtmlControls.HtmlGenericControl spanAvanceFinanciero = new System.Web.UI.HtmlControls.HtmlGenericControl("SPAN");


                LinkButton linkAnt = (LinkButton)e.Row.FindControl("linkAnticipo");
                LinkButton linkEst = (LinkButton)e.Row.FindControl("linkEstimaciones");

                linkAnt.Text = "Pendiente";
                linkEst.Text = "Pendiente";




                HtmlGenericControl progresoA = (HtmlGenericControl)e.Row.FindControl("ProgresoA");
                HtmlGenericControl progresoB = (HtmlGenericControl)e.Row.FindControl("ProgresoB");
                

                if (obra.StatusControlFinanciero > 4)
                {
                    ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == idObra).First();
                    Estimaciones anticipo = uow.EstimacionesBL.Get(p=>p.ContratoDeObra.ObraId == idObra && p.NumeroDeEstimacion == 0).First();
                    List<Estimaciones> listaEstimaciones;

                    linkAnt.Text = anticipo.Total.ToString("C2");





                    if (obra.StatusControlFinanciero > 5)
                    {
                        listaEstimaciones = uow.EstimacionesBL.Get(p=>p.ContratoDeObra.ObraId == idObra && p.NumeroDeEstimacion > 0).ToList();
                        string cad = " ";
                        foreach (Estimaciones item in listaEstimaciones)
                        {
                            cad = cad + item.NumeroDeEstimacion + "-";
                        }
                        cad = cad.Substring(0, cad.Length - 1);

                        linkEst.Text = "EST: " + cad;
                    }

                    

                    decimal suma;
                    decimal avance;
                    int porcentaje;

                    //Avance Fisico
                    listaEstimaciones = uow.EstimacionesBL.Get(p => p.ContratoDeObra.ObraId == idObra && p.NumeroDeEstimacion > 0).ToList();
                    suma = listaEstimaciones.Sum(p => p.Total);
                    avance = Math.Round(suma / contrato.Total, 2) * 100;
                    porcentaje =  Convert.ToInt16 ( avance);

                    spanAvanceFisico.InnerText = porcentaje.ToString() + "%";
                    progresoA.Style.Add("width", porcentaje.ToString() + "%");
                


                    //Avance Financiero
                    listaEstimaciones = uow.EstimacionesBL.Get(p => p.ContratoDeObra.ObraId == idObra && p.Status == 2).ToList();
                    suma = listaEstimaciones.Sum(p => p.Total - p.AmortizacionAnticipo);
                    avance = Math.Round( suma / contrato.Total,2) * 100;
                    porcentaje = Convert.ToInt16(avance);
                    
                    spanAvanceFinanciero.InnerText = porcentaje.ToString() + "%";
                    progresoB.Style.Add("width", porcentaje.ToString() + "%");
                        

                    



                }
                else
                {
                    spanAvanceFisico.InnerText = "0%";
                    spanAvanceFinanciero.InnerText = "0%";

                    progresoA.Style.Add("width", 0.ToString() + "%");
                    progresoB.Style.Add("width", 0.ToString() + "%");

                }


                divAvFisico.Controls.Add(spanAvanceFisico);
                divAvFinanciero.Controls.Add(spanAvanceFinanciero);

             


            }
        }

        protected void linkAnticipo_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Session["XidObra"] = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Response.Redirect("wfAnticipos.aspx");
        }

        protected void linkEstimaciones_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;

            Session["XidObra"] = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Response.Redirect("wfEstimaciones.aspx");
        }



    }
}
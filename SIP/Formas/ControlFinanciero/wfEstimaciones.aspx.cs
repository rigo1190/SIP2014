using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace SIP.Formas.ControlFinanciero
{
    public partial class wfEstimaciones : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            uow = new UnitOfWork(Session["IdUser"].ToString());

            this.idObra = int.Parse(Session["XidObra"].ToString());
            Obra obra = uow.ObraBusinessLogic.GetByID(idObra);

            if (!IsPostBack)
            {
                BindGrids();

                _URLVisor.Value = ResolveClientUrl("~/rpts/wfVerReporte.aspx");               
                _idContrato.Value = "0";

                if (obra.StatusControlFinanciero < 5)//no hay anticipo registrado
                {
                    DIVmostrarEstimaciones.Style.Add("display", "none");
                }
                else
                {
                    divMSGnoHayAnticipo.Style.Add("display", "none");
                    ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).First();
                    _idContrato.Value = contrato.Id.ToString();
                }


            }
        }

        private void BindGrids()
        {
            this.idObra = int.Parse(Session["XidObra"].ToString());

            this.GridObra.DataSource = uow.ObraBusinessLogic.Get(p => p.Id == idObra).ToList();
            this.GridObra.DataBind();

            this.GridContrato.DataSource = uow.ContratosDeObraBL.Get(p=>p.ObraId == idObra).ToList();
            this.GridContrato.DataBind();

            this.gridEstimaciones.DataSource = uow.EstimacionesBL.Get(p=>p.ContratoDeObra.ObraId == idObra && p.NumeroDeEstimacion > 0).ToList();
            this.gridEstimaciones.DataBind();

        }



    }
}
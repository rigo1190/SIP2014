using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SIP.Formas.ControlFinanciero
{
    public partial class wfAnticipos : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            this.idObra = int.Parse(Session["XidObra"].ToString());
            if (!IsPostBack)
            {
                bindDatos();
                _URLVisor.Value = ResolveClientUrl("~/rpts/wfVerReporte.aspx");
                _idObra.Value = idObra.ToString(); 

            }
        }


        private void bindDatos()
        {
            this.idObra = int.Parse(Session["XidObra"].ToString());
            Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);

            ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).FirstOrDefault();
            Estimaciones anticipo = uow.EstimacionesBL.Get(p => p.ContratoDeObra.ObraId == this.idObra && p.NumeroDeEstimacion == 0).FirstOrDefault();

            txtNumObra.Value = obra.Numero;
            txtDescripcionObra.Value = obra.Descripcion;
            txtRFC.Value = contrato.RFCcontratista;
            txtRazonSocial.Value = contrato.RazonSocialContratista;

            txtImporteContratado.Value = contrato.Total.ToString();
            txtPorcentaje.Value = contrato.PorcentajeDeAnticipo.ToString() + " %";
            txtImporteAnticipo.Value = (double.Parse(contrato.Total.ToString()) * (contrato.PorcentajeDeAnticipo / 100)).ToString();

            divBtnImprimir.Style.Add("display", "none");

            if (obra.StatusControlFinanciero > 2)//datos del anticipo
            {
                

                txtFolio.Value = anticipo.FolioDePago;
                dtpFecha.Value = String.Format("{0:d}", anticipo.FechaDeEstimacion);
                txtImporteAnticipo.Value = anticipo.Total.ToString();

                divBtnGuardarAnticipo.Style.Add("display","none");
                divBtnImprimir.Style.Add("display","block");
             

            }



        }

        protected void btnGuardarAnticipo_Click(object sender, EventArgs e)
        {
            this.idObra = int.Parse(Session["XidObra"].ToString());

            List<Estimaciones>listaAnticipos = uow.EstimacionesBL.Get(p=>p.ContratoDeObra.ObraId == this.idObra && p.NumeroDeEstimacion==0).ToList();
            ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).First();

            Estimaciones anticipo;

            if (listaAnticipos.Count == 0){
                
                anticipo = new Estimaciones();

                anticipo.ContratoDeObraId = contrato.Id;
                anticipo.NumeroDeEstimacion = 0;
                anticipo.FolioDePago = txtFolio.Value;
                anticipo.FechaDeEstimacion = DateTime.Parse(dtpFecha.Value.ToString());
                anticipo.ImporteEstimado = 0;
                anticipo.IVA = 0;
                anticipo.Total = Convert.ToDecimal(txtImporteAnticipo.Value.ToString()); 
                anticipo.AmortizacionAnticipo = 0;
                anticipo.Retencion2AlMillar = 0;
                anticipo.Retencion5AlMillar = 0;
                anticipo.ImporteNetoACobrar = Convert.ToDecimal(txtImporteAnticipo.Value.ToString());
                anticipo.Status = 2;

                uow.EstimacionesBL.Insert(anticipo);

                Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);

                if (obra.StatusControlFinanciero == 2)
                {
                    obra.StatusControlFinanciero = 3;
                    uow.ObraBusinessLogic.Update(obra);
                }



            }else{

                anticipo = uow.EstimacionesBL.Get(p=>p.ContratoDeObraId == contrato.Id && p.NumeroDeEstimacion == 0).First();

                anticipo.FolioDePago = txtFolio.Value;
                anticipo.FechaDeEstimacion = DateTime.Parse(dtpFecha.Value.ToString());
                anticipo.Total = Convert.ToDecimal(txtImporteAnticipo.Value.ToString());
                anticipo.ImporteNetoACobrar = Convert.ToDecimal(txtImporteAnticipo.Value.ToString());
                anticipo.Status = 1;

                uow.EstimacionesBL.Update(anticipo);
            }


            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {
                divBtnImprimir.Style.Add("display", "block");
                divBtnGuardarAnticipo.Style.Add("display", "none");
            }

        }
    }
}
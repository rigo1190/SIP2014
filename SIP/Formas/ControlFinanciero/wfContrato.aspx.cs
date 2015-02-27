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
    public partial class wfContratoPresupuestoContratado : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;


        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork(Session["IdUser"].ToString());
            idObra = int.Parse(Session["XidObra"].ToString());
            
            if (!IsPostBack)
            {
                bindDatos();
            }
        }

        private void bindDatos()
        {
            this.idObra = int.Parse(Session["XidObra"].ToString());
            Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);

            
            if (obra.StatusControlFinanciero > 0)//datos del contrato
            {
                ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).First();

                txtNumContrato.Value = contrato.NumeroDeContrato;

                txtClaveContratista.Value = contrato.ClaveContratista; 
                txtRFC.Value = contrato.RFCcontratista;
                txtRazonSocial.Value = contrato.RazonSocialContratista;
                txtImporteTotal.Value = contrato.Total.ToString("C0");

                dtpContrato.Value = String.Format("{0:d}", contrato.FechaDelContrato);
                dtpInicio.Value = String.Format("{0:d}", contrato.FechaDeInicio);
                dtpTermino.Value = String.Format("{0:d}", contrato.FechaDeTermino);
                txtClavePresupuestal.Value = contrato.ClavePresupuestal;
                
                txtPorcentajeAnticipo.Value = contrato.PorcentajeDeAnticipo.ToString();
                chk5almillar.Checked = contrato.Descontar5AlMillar;
                chk2almillar.Checked = contrato.Descontar2AlMillar;
                chk2almillarSV.Checked = contrato.Descontar2AlMillarSV; 

                txtNombreAfianzadora.Value = contrato.NombreAfianzadora;
                txtFianza.Value = contrato.NumeroFianza;
                txtFianzaCumplimiento.Value = contrato.NumeroFianzaCumplimiento;
                
            }

            if (obra.StatusControlFinanciero > 3)
            {
                divBtnGuardarContrato.Style.Add("display", "none");
            }


        }

        protected void btnGuardarContrato_Click(object sender, EventArgs e)
        {
            this.idObra = int.Parse(Session["XidObra"].ToString());
            List<ContratosDeObra> listaContrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).ToList();
            
            ContratosDeObra contrato;

            if (listaContrato.Count == 0)
            {
                contrato = new ContratosDeObra();

                contrato.ObraId = this.idObra;

                contrato.NumeroDeContrato = txtNumContrato.Value;
                contrato.ClaveContratista = txtClaveContratista.Value;  
                contrato.RFCcontratista = txtRFC.Value;
                contrato.RazonSocialContratista = txtRazonSocial.Value;
                //contrato.Total = decimal.Parse(txtImporteTotal.Value.ToString());

                contrato.FechaDelContrato = DateTime.Parse(dtpContrato.Value.ToString());
                contrato.FechaDeInicio = DateTime.Parse(dtpInicio.Value.ToString());
                contrato.FechaDeTermino = DateTime.Parse(dtpTermino.Value.ToString());
                contrato.ClavePresupuestal = txtClavePresupuestal.Value;
                contrato.PorcentajeDeAnticipo = double.Parse(txtPorcentajeAnticipo.Value.ToString());
                contrato.Descontar5AlMillar = bool.Parse(chk5almillar.Checked.ToString());
                contrato.Descontar2AlMillar = bool.Parse(chk2almillar.Checked.ToString());
                contrato.Descontar2AlMillarSV = bool.Parse(chk2almillarSV.Checked.ToString());

                contrato.NombreAfianzadora = txtNombreAfianzadora.Value;
                contrato.NumeroFianza = txtFianza.Value;
                contrato.NumeroFianzaCumplimiento = txtFianzaCumplimiento.Value;


                contrato.CreatedById = int.Parse(Session["IdUser"].ToString());
                

                uow.ContratosDeObraBL.Insert(contrato);

                Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);

                if (obra.StatusControlFinanciero == 0)
                {
                    obra.StatusControlFinanciero = 1;
                    uow.ObraBusinessLogic.Update(obra);
                }
                

                
            }
            else
            {
                contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).FirstOrDefault();

                contrato.NumeroDeContrato = txtNumContrato.Value;
                contrato.ClaveContratista = txtClaveContratista.Value;  
                contrato.RFCcontratista = txtRFC.Value;
                contrato.RazonSocialContratista = txtRazonSocial.Value;
                //contrato.Total = decimal.Parse(txtImporteTotal.Value.ToString());

                contrato.FechaDelContrato = DateTime.Parse(dtpContrato.Value.ToString());
                contrato.FechaDeInicio = DateTime.Parse(dtpInicio.Value.ToString());
                contrato.FechaDeTermino = DateTime.Parse(dtpTermino.Value.ToString());
                contrato.ClavePresupuestal = txtClavePresupuestal.Value;
                contrato.PorcentajeDeAnticipo = double.Parse(txtPorcentajeAnticipo.Value.ToString());
                contrato.Descontar5AlMillar = bool.Parse(chk5almillar.Checked.ToString());
                contrato.Descontar2AlMillar = bool.Parse(chk2almillar.Checked.ToString());
                contrato.Descontar2AlMillarSV = bool.Parse(chk2almillarSV.Checked.ToString());

                contrato.NombreAfianzadora = txtNombreAfianzadora.Value;
                contrato.NumeroFianza = txtFianza.Value;
                contrato.NumeroFianzaCumplimiento = txtFianzaCumplimiento.Value;
                contrato.EditedById = int.Parse(Session["IdUser"].ToString());

                uow.ContratosDeObraBL.Update(contrato);
                

            }

            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {
                Response.Redirect("wfPresupuestoContratado.aspx");
            }
            

            
        }
    }
}
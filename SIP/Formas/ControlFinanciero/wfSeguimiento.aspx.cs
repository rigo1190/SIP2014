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
    public partial class wfSeguimiento : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;
        private int idEjercicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            idObra = int.Parse(Request.QueryString["id"].ToString());
            idEjercicio = int.Parse(Session["EjercicioId"].ToString());

            if (!IsPostBack)
            {
                BindX();
            }
        }

        private void BindX()
        {
            //datos generales de la obra
            Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);

            txtNoObra.Value = obra.Numero;
            txtDescripcionObra.Value = obra.Descripcion;


            if (obra.StatusControlFinanciero > 0)//datos del contrato
            {
                ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).First();

                txtNumContrato.Value = contrato.NumeroDeContrato;

                txtRFC.Value = contrato.RFCcontratista;
                txtRazonSocial.Value = contrato.RazonSocialContratista;
                txtImporteTotal.Value = contrato.Total.ToString();

                dtpContrato.Value = String.Format("{0:d}", contrato.FechaDelContrato);
                dtpInicio.Value = String.Format("{0:d}", contrato.FechaDeInicio);
                dtpTermino.Value = String.Format("{0:d}", contrato.FechaDeTermino);

                txtPorcentajeAnticipo.Value = contrato.PorcentajeDeAnticipo.ToString();
                chk5almillar.Checked = contrato.Descontar5AlMillar;
                chk2almillar.Checked = contrato.Descontar2AlMillar;
                
            }


            


        }

        protected void btnGuardarContrato_Click(object sender, EventArgs e)
        {
            List<ContratosDeObra> listaContrato = uow.ContratosDeObraBL.Get(p=>p.ObraId == this.idObra).ToList();



            ContratosDeObra contrato;

            if (listaContrato.Count == 0)
            {
                contrato = new ContratosDeObra();

                contrato.ObraId = this.idObra;
                contrato.NumeroDeContrato = txtNumContrato.Value;
                contrato.RFCcontratista = txtRFC.Value;
                contrato.RazonSocialContratista = txtRazonSocial.Value;
                contrato.Total = decimal.Parse( txtImporteTotal.Value.ToString());

                contrato.FechaDelContrato = DateTime.Parse(dtpContrato.Value.ToString());
                contrato.FechaDeInicio = DateTime.Parse( dtpInicio.Value.ToString());
                contrato.FechaDeTermino = DateTime.Parse(dtpTermino.Value.ToString());

                contrato.PorcentajeDeAnticipo = double.Parse(txtPorcentajeAnticipo.Value.ToString());
                contrato.Descontar5AlMillar = bool.Parse(chk5almillar.Checked.ToString());
                contrato.Descontar2AlMillar = bool.Parse(chk2almillar.Checked.ToString());

               // uow.ContratosDeObraBL.Insert(contrato);
                

            }
            else
            {
                contrato = uow.ContratosDeObraBL.GetByID(this.idObra);

                contrato.NumeroDeContrato = txtNumContrato.Value;
                contrato.RFCcontratista = txtRFC.Value;
                contrato.RazonSocialContratista = txtRazonSocial.Value;
                contrato.Total = decimal.Parse(txtImporteTotal.Value.ToString());

                contrato.FechaDelContrato = DateTime.Parse(dtpContrato.Value.ToString());
                contrato.FechaDeInicio = DateTime.Parse(dtpInicio.Value.ToString());
                contrato.FechaDeTermino = DateTime.Parse(dtpTermino.Value.ToString());

                contrato.PorcentajeDeAnticipo = double.Parse(txtPorcentajeAnticipo.Value.ToString());
                contrato.Descontar5AlMillar = bool.Parse(chk5almillar.Value.ToString());
                contrato.Descontar2AlMillar = bool.Parse(chk2almillar.Value.ToString());

               // uow.ContratosDeObraBL.Update(contrato);
            
            }

            //Obra obra = uow.ObraBusinessLogic.GetByID(this.idObra);

            //if (obra.StatusControlFinanciero == 0)
            //{
            //    obra.StatusControlFinanciero = 1;
            //    uow.ObraBusinessLogic.Update(obra);
            //}



          //  uow.SaveChanges();

            



        }







        

        protected void btnCargarConceptosDeObra_Click(object sender, EventArgs e)
        {
            
        





            
        }





        






    }
}
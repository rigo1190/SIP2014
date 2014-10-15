using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.Catalogos
{
    public partial class catAperturaProgramaticaBeneficiario : System.Web.UI.Page
    {
        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "none");
            }
        }

        private void BindGrid()
        {
            this.grid.DataSource = uow.AperturaProgramaticaBeneficiarioBusinessLogic.Get().ToList();
            this.grid.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            _Accion.Text = "Nuevo";

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "update";

            AperturaProgramaticaBeneficiario beneficiario = uow.AperturaProgramaticaBeneficiarioBusinessLogic.GetByID(int.Parse(_ElId.Text));
            BindCatalogo(beneficiario);


            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            AperturaProgramaticaBeneficiario beneficiario = uow.AperturaProgramaticaBeneficiarioBusinessLogic.GetByID(int.Parse(_ElId.Text));


            uow.Errors.Clear();
            List<AperturaProgramaticaMeta> lista;
            lista = uow.AperturaProgramaticaMetaBusinessLogic.Get(p => p.AperturaProgramaticaBeneficiarioId == beneficiario.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");



            if (uow.Errors.Count == 0)
            {
                uow.AperturaProgramaticaBeneficiarioBusinessLogic.Delete(beneficiario);
                uow.SaveChanges();
            }


            if (uow.Errors.Count == 0)
            {
                BindGrid();
                lblMensajeSuccess.Text = "El registro se ha eliminado correctamente";

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");

            }

            else
            {
                string mensaje;

                divMsg.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");

                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMensajes.Text = mensaje;
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            AperturaProgramaticaBeneficiario beneficiario;
            List<AperturaProgramaticaBeneficiario> lista;
            string mensaje = "";
            int orden;

            if (_Accion.Text == "Nuevo")
                beneficiario = new AperturaProgramaticaBeneficiario();
            else
                beneficiario = uow.AperturaProgramaticaBeneficiarioBusinessLogic.GetByID(int.Parse(_ElId.Text));


            beneficiario.Clave = txtClave.Value;
            beneficiario.Nombre = txtNombre.Value;

            if (_Accion.Text == "Nuevo")
            {
                lista = uow.AperturaProgramaticaBeneficiarioBusinessLogic.Get().ToList();

                orden = lista.Max(p => p.Orden);
                orden++;
                beneficiario.Orden = orden;
            }


            //Validaciones
            uow.Errors.Clear();
            if (_Accion.Text == "Nuevo")
            {

                lista = uow.AperturaProgramaticaBeneficiarioBusinessLogic.Get(p => p.Clave == beneficiario.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.AperturaProgramaticaBeneficiarioBusinessLogic.Get(p => p.Nombre == beneficiario.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                uow.AperturaProgramaticaBeneficiarioBusinessLogic.Insert(beneficiario);
                mensaje = "El nuevo beneficiario ha sido registrado correctamente";




            }
            else//Update
            {

                int xid;

                xid = int.Parse(_ElId.Text);

                lista = uow.AperturaProgramaticaBeneficiarioBusinessLogic.Get(p => p.Id != xid && p.Clave == beneficiario.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.AperturaProgramaticaBeneficiarioBusinessLogic.Get(p => p.Id != xid && p.Nombre == beneficiario.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.AperturaProgramaticaBeneficiarioBusinessLogic.Update(beneficiario);
                mensaje = "Los cambios se registraron satisfactoriamente";
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_EjecutarMensaje('" + mensaje + "')", true);
                txtClave.Value = string.Empty;
                txtNombre.Value = string.Empty;

                BindGrid();

                lblMensajeSuccess.Text = mensaje;

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");
                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");

            }
            else
            {
                divMsg.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");

                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";



                lblMensajes.Text = mensaje;

            }
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            BindGrid();

            divBtnNuevo.Style.Add("display", "block");

            divEdicion.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        public void BindCatalogo(AperturaProgramaticaBeneficiario beneficiario)
        {
            txtClave.Value = beneficiario.Clave;
            txtNombre.Value = beneficiario.Nombre;
            _ElId.Text = beneficiario.Id.ToString();
        }

        

        
       


    }
}
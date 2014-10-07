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
    public partial class catGrupoBeneficiarios : System.Web.UI.Page
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
            this.grid.DataSource = uow.GrupoBeneficiarioBusinessLogic.Get().ToList();
            this.grid.DataBind();
        }



        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            int orden;
            _Accion.Text = "Nuevo";

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");

            List<GrupoBeneficiario> lista = uow.GrupoBeneficiarioBusinessLogic.Get().ToList();

            orden = lista.Max(p => p.Orden);
            orden++;
            txtOrden.Value = orden.ToString();
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "update";

            GrupoBeneficiario beneficiario = uow.GrupoBeneficiarioBusinessLogic.GetByID(int.Parse(_ElId.Text));
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

            GrupoBeneficiario beneficiario = uow.GrupoBeneficiarioBusinessLogic.GetByID(int.Parse(_ElId.Text));
            

            uow.Errors.Clear();
            List<POADetalle> lista;
            lista = uow.POADetalleBusinessLogic.Get(p => p.GrupoBeneficiarioId == beneficiario.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");



            if (uow.Errors.Count == 0)
            {
                uow.GrupoBeneficiarioBusinessLogic.Delete(beneficiario);
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
            GrupoBeneficiario beneficiario;
            List<GrupoBeneficiario> lista;
            string mensaje = "";

            if (_Accion.Text == "Nuevo")
                beneficiario = new GrupoBeneficiario();
            else
                beneficiario = uow.GrupoBeneficiarioBusinessLogic.GetByID(int.Parse(_ElId.Text));


            beneficiario.Clave = txtClave.Value;
            beneficiario.Nombre = txtNombre.Value;
            beneficiario.Orden = int.Parse(txtOrden.Value);


            //Validaciones
            uow.Errors.Clear();
            if (_Accion.Text == "Nuevo")
            {

                lista = uow.GrupoBeneficiarioBusinessLogic.Get(p => p.Clave == beneficiario.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.GrupoBeneficiarioBusinessLogic.Get(p => p.Nombre == beneficiario.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.GrupoBeneficiarioBusinessLogic.Get(p => p.Orden == beneficiario.Orden).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El número de orden que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.GrupoBeneficiarioBusinessLogic.Insert(beneficiario);
                mensaje = "El nuevo beneficiario ha sido registrado correctamente";




            }
            else//Update
            {

                int xid;

                xid = int.Parse(_ElId.Text);

                lista = uow.GrupoBeneficiarioBusinessLogic.Get(p => p.Id != xid && p.Clave == beneficiario.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.GrupoBeneficiarioBusinessLogic.Get(p => p.Id != xid && p.Nombre == beneficiario.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.GrupoBeneficiarioBusinessLogic.Get(p => p.Id != xid && p.Orden == beneficiario.Orden).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El número de orden que capturo ya ha sido registrada anteriormente, verifique su información");




                uow.GrupoBeneficiarioBusinessLogic.Update(beneficiario);
                mensaje = "Los cambios se registraron satisfactoriamente";
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_EjecutarMensaje('" + mensaje + "')", true);
                txtClave.Value = string.Empty;
                txtNombre.Value = string.Empty;
                txtOrden.Value = string.Empty;

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


        public void BindCatalogo(GrupoBeneficiario beneficiario)
        {
            txtClave.Value = beneficiario.Clave;
            txtNombre.Value = beneficiario.Nombre;
            txtOrden.Value = beneficiario.Orden.ToString();
            _ElId.Text = beneficiario.Id.ToString();
        }
       


    }
}
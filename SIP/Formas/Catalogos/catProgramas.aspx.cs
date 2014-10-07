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
    public partial class catProgramas : System.Web.UI.Page
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
            this.grid.DataSource = uow.ProgramaBusinessLogic.Get().ToList();
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

            List<Programa> lista = uow.ProgramaBusinessLogic.Get().ToList();

            orden = lista.Max(p => p.Orden);
            orden++;
            txtOrden.Value = orden.ToString();
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idPROG.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "update";

            Programa prog = uow.ProgramaBusinessLogic.GetByID(int.Parse(_idPROG.Text));
            BindCatalogo(prog);


            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idPROG.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            Programa prog = uow.ProgramaBusinessLogic.GetByID(int.Parse(_idPROG.Text));
            


            uow.Errors.Clear();
            List<POADetalle> lista;
            lista = uow.POADetalleBusinessLogic.Get(p => p.ProgramaId == prog.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");



            if (uow.Errors.Count == 0)
            {
                uow.ProgramaBusinessLogic.Delete(prog);
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
            Programa prog;
            List<Programa> lista;
            string mensaje = "";

            if (_Accion.Text == "Nuevo")
                prog = new Programa();
            else
                prog = uow.ProgramaBusinessLogic.GetByID(int.Parse(_idPROG.Text));

            prog.Clave = txtClave.Value;
            prog.Descripcion = txtDescripcion.Value; 
            prog.Orden = int.Parse(txtOrden.Value);


            //Validaciones
            uow.Errors.Clear();
            if (_Accion.Text == "Nuevo")
            {

                lista = uow.ProgramaBusinessLogic.Get(p => p.Clave == prog.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.ProgramaBusinessLogic.Get(p => p.Descripcion == prog.Descripcion).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.ProgramaBusinessLogic.Get(p => p.Orden == prog.Orden).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El número de orden que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.ProgramaBusinessLogic.Insert(prog);
                mensaje = "El nuevo programa ha sido registrado correctamente";

            }
            else//Update
            {

                int xid;

                xid = int.Parse(_idPROG.Text);

                lista = uow.ProgramaBusinessLogic.Get(p => p.Id != xid && p.Clave == prog.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.ProgramaBusinessLogic.Get(p => p.Id != xid && p.Descripcion == prog.Descripcion).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.ProgramaBusinessLogic.Get(p => p.Id != xid && p.Orden == prog.Orden).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El número de orden que capturo ya ha sido registrada anteriormente, verifique su información");




                uow.ProgramaBusinessLogic.Update(prog);
                mensaje = "Los cambios se registraron satisfactoriamente";
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_EjecutarMensaje('" + mensaje + "')", true);
                txtClave.Value = string.Empty;
                txtDescripcion.Value = string.Empty;
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


        

        

        public void BindCatalogo(Programa prog)
        {
            txtClave.Value = prog.Clave;
            txtDescripcion.Value = prog.Descripcion;
            txtOrden.Value = prog.Orden.ToString();
            _idPROG.Text = prog.Id.ToString();
        }
       

    }
}
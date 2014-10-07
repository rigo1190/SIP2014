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
    public partial class catUnidadesPresupuestales : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindDataGrid();

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "none");
            }
        }



        private void BindDataGrid()
        {
            this.grid.DataSource = uow.UnidadPresupuestalBusinessLogic.Get().ToList().OrderBy(p=>p.Orden).ToList();
            this.grid.DataBind();
        }


        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            int orden;
            _Accion.Text = "Nuevo";

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add ("display", "none");
            divMsgSuccess.Style.Add("display", "none");



            List<UnidadPresupuestal> lista = uow.UnidadPresupuestalBusinessLogic.Get().ToList();

            orden = lista.Max(p => p.Orden);
            orden++;
            txtOrden.Value = orden.ToString();

        }


        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idUP.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "update";

            UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(int.Parse(_idUP.Text));
            BindCatalogo(up);


            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");


        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idUP.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(int.Parse(_idUP.Text));



            uow.Errors.Clear();
            List<DataAccessLayer.Models.POA> lista;
            lista = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == up.Id).ToList();
                        
            if (lista.Count > 0)
                uow.Errors.Add("La unidad presupuestal no puede ser eliminada porque ya fue utilizada dentro del sistema");
            


            if (uow.Errors.Count == 0)            {
                uow.UnidadPresupuestalBusinessLogic.Delete(up);
                uow.SaveChanges();
            }


            if (uow.Errors.Count == 0)
            {
                BindDataGrid();
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
            UnidadPresupuestal up;
            List<UnidadPresupuestal> lista;
            string mensaje="";
                       
            if (_Accion.Text == "Nuevo")
                up = new UnidadPresupuestal();
            else
                up = uow.UnidadPresupuestalBusinessLogic.GetByID(int.Parse(_idUP.Text));

            up.Clave = txtClave.Value;
            up.Abreviatura = txtAbreviatura.Value;
            up.Nombre = txtNombre.Value;
            up.Orden = int.Parse(txtOrden.Value);


            //Validaciones
            uow.Errors.Clear();            
            if (_Accion.Text == "Nuevo"){
            
                lista = uow.UnidadPresupuestalBusinessLogic.Get(p => p.Clave == up.Clave).ToList(); 
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");

                lista = uow.UnidadPresupuestalBusinessLogic.Get(p => p.Abreviatura == up.Abreviatura).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Abreviatura que capturo ya ha sido registrada anteriormente, verifique su información");

                lista = uow.UnidadPresupuestalBusinessLogic.Get(p => p.Nombre == up.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.UnidadPresupuestalBusinessLogic.Get(p => p.Orden == up.Orden).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El número de orden que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.UnidadPresupuestalBusinessLogic.Insert(up);
                mensaje = "La nueva unidad presupuestal se ha almacenado correctamente";

                

    
            }
            else//Update
            {

                int xid;

                xid = int.Parse(_idUP.Text);

                lista = uow.UnidadPresupuestalBusinessLogic.Get(p => p.Id != xid && p.Clave == up.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");

                lista = uow.UnidadPresupuestalBusinessLogic.Get(p => p.Id != xid && p.Abreviatura == up.Abreviatura).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Abreviatura que capturo ya ha sido registrada anteriormente, verifique su información");

                lista = uow.UnidadPresupuestalBusinessLogic.Get(p => p.Id != xid &&  p.Nombre == up.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.UnidadPresupuestalBusinessLogic.Get(p => p.Id != xid && p.Orden == up.Orden).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El número de orden que capturo ya ha sido registrada anteriormente, verifique su información");
                



                uow.UnidadPresupuestalBusinessLogic.Update(up);
                mensaje = "La unidad presupuestal se ha actualizado correctamente";
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();
            

            if (uow.Errors.Count == 0)
            {   
                //ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_EjecutarMensaje('" + mensaje + "')", true);
                txtClave.Value = string.Empty;
                txtAbreviatura.Value = string.Empty;
                txtNombre.Value = string.Empty;
                txtOrden.Value = string.Empty;

                BindDataGrid();

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







         
        public void BindCatalogo(UnidadPresupuestal UP)
        {
            txtClave.Value = UP.Clave;
            txtAbreviatura.Value = UP.Abreviatura;
            txtNombre.Value = UP.Nombre;
            txtOrden.Value = UP.Orden.ToString();
            _idUP.Text = UP.Id.ToString();
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            BindDataGrid();

            divBtnNuevo.Style.Add("display", "block");

            divEdicion.Style.Add("display", "none");            
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");

        }



    }
}
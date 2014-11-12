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
    public partial class catFondosLineamientos : System.Web.UI.Page
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


                ddlFondo.DataSource = uow.FondoBusinessLogic.Get().ToList();
                ddlFondo.DataValueField = "Id";
                ddlFondo.DataTextField = "Abreviatura";
                ddlFondo.DataBind();


            }

        }


        private void BindGrid()
        {
            uow = new UnitOfWork();
            this.grid.DataSource = uow.FondoLineamientosBL.Get().ToList();
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

            FondoLineamientos fondoLin = uow.FondoLineamientosBL.GetByID(int.Parse(_ElId.Text));
            BindCatalogo(fondoLin);


            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            
            FondoLineamientos fondoLin = uow.FondoLineamientosBL.GetByID(int.Parse(_ElId.Text));


            uow.FondoLineamientosBL.Delete(fondoLin);
            uow.SaveChanges();
            


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
            FondoLineamientos fondoLin;
            
            List<FondoLineamientos> lista;
            string mensaje = "";
            int fondo = int.Parse(ddlFondo.SelectedValue);

            if (_Accion.Text == "Nuevo")
                fondoLin = new FondoLineamientos();               
            else
                fondoLin  = uow.FondoLineamientosBL.GetByID(int.Parse(_ElId.Text));


            
            fondoLin.TipoDeObrasYAcciones = txtTipo.Value;
            fondoLin.CalendarioDeIngresos = txtCalendario.Value;
            fondoLin.VigenciaDePago = txtVigencia.Value;
            fondoLin.NormatividadAplicable = txtNormatividad.Value;
            fondoLin.Contraparte = txtContraparte.Value;


            




            //Validaciones
            uow.Errors.Clear();
            if (_Accion.Text == "Nuevo")
            {

                

                lista = uow.FondoLineamientosBL.Get(p=>p.FondoId == fondo).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El Fondo que indico ya se encuentra registrado en el sistema, verifique su información");



                fondoLin.FondoId = fondo;
                uow.FondoLineamientosBL.Insert(fondoLin);
                
                mensaje = "El lineamiento ha sido registrado correctamente";




            }
            else//Update
            {

                
                uow.FondoLineamientosBL.Update(fondoLin); 
                mensaje = "Los cambios se registraron satisfactoriamente";
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {
                
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



        public void BindCatalogo(FondoLineamientos fl)
        {
            txtTipo.Value = fl.TipoDeObrasYAcciones;
            txtCalendario.Value = fl.CalendarioDeIngresos;
            txtVigencia.Value = fl.VigenciaDePago;
            txtNormatividad.Value = fl.NormatividadAplicable;
            txtContraparte.Value = fl.Contraparte;

            _ElId.Text = fl.Id.ToString();
        }


    }
}
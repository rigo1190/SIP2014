using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.Catalogos
{
    public partial class Ejercicios : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                divEdicion.Style.Add("display", "none");
                BindGrid();

            }
        }

        /// <summary>
        /// Metodo encargado de recuperar todos los objetos de tipo Ejercicio y colocarlos como source del
        /// grid en cuestion
        /// Creado por Rigoberto TS
        /// 12/09/2014
        /// </summary>
        private void BindGrid()
        {
            grid.DataSource = uow.EjercicioBusinessLogic.Get().ToList();
            grid.DataBind();
        }

        private void BindControles(Ejercicio obj)
        {
            txtAnio.Value = obj.Año.ToString();
            chkActivo.Checked = obj.Activo;
        }


        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            Utilerias.LimpiarCampos(this);
            chkActivo.Checked = false;
            _Accion.Text = "N"; //Se cambia el estado de la forma a crear un NUEVo registro
        }


        protected void btnCrear_Click(object sender, EventArgs e)
        {
            string msg = "Se ha guardado correctamente";

            Ejercicio obj;

            if (_Accion.Text.Equals("N")) //Si el estado de la forma es crear un NUEVo registro
               obj=new Ejercicio();
           else
            {  //Se va a ACTUALIZAR un registro existente
               obj = uow.EjercicioBusinessLogic.GetByID(Utilerias.StrToInt(_Anio.Text));
               msg = "Se ha actualizado correctamente";
           }

            obj.Año = Utilerias.StrToInt(txtAnio.Value);
            obj.Activo = Convert.ToBoolean(chkActivo.Checked);

            //Se almacena el objeto
            if (_Accion.Text.Equals("N")) //Si el estado de la forma es crear un NUEVo registro
                uow.EjercicioBusinessLogic.Insert(obj);
            else
                uow.EjercicioBusinessLogic.Update(obj); //Se va a ACTUALIZAR un registro existente

            uow.SaveChanges();

            if (uow.Errors.Count > 0)
            {
                msg=string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
                return;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;
            //Se limpian los controles
            txtAnio.Value = string.Empty;
            chkActivo.Checked = false;

            BindGrid();  //Se bindean los datos 
            divEdicion.Style.Add("display", "none");
            divMsg.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "block");
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;

            int id = Convert.ToInt32(grid.DataKeys[row.RowIndex].Values["Id"].ToString());

            Ejercicio ejercicio = uow.EjercicioBusinessLogic.GetByID(id);

            BindControles(ejercicio);

            _Anio.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "A"; //Se cambia el estado de la forma a ACTUALIZAR un registro existente

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            //Se busca l ID de la fila seleccionada
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            string msg = "Se ha eliminado correctamente";

            int id = Utilerias.StrToInt(grid.DataKeys[row.RowIndex].Value.ToString());

            Ejercicio obj=uow.EjercicioBusinessLogic.GetByID(id);

            //Se elimina el objeto
            uow.EjercicioBusinessLogic.Delete(obj);
            uow.SaveChanges();

            if (uow.Errors.Count > 0) //Si hubo errores
            {
                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
                divMsg.Style.Add("display", "block");

                return;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;

            BindGrid();
            divEdicion.Style.Add("display", "none");
            divBtnNuevo.Style.Add("display", "block");
            divMsg.Style.Add("display", "block");
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            divEdicion.Style.Add("display", "none");
            divBtnNuevo.Style.Add("display", "block");
            divMsg.Style.Add("display", "none");
            BindGrid();
        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ctrl = (Label)e.Row.FindControl("lblActivo");
                Ejercicio obj;

                if (ctrl != null)
                {
                    if (grid.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                    {
                        obj = uow.EjercicioBusinessLogic.GetByID(Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString()));
                        ctrl.Text = obj.Activo ? "SI" : "NO";
                    }
                }
            }
        }

       

    }
}
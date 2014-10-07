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
    public partial class Plantillas : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
                BindDrops();
                
                
                
            }
        }

        #region EVENTOS
        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlButton btnPP = (HtmlButton)e.Row.FindControl("btnPP");
                if (grid.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    string url = string.Empty;
                    if (btnPP != null)
                    {
                        url = "PlantillaPreguntas.aspx?p=" + grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
                        btnPP.Attributes.Add("onclick", "fnc_IrDesdeGrid('" + url + "')");
                    }
                }
            }
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;

            int id = Utilerias.StrToInt(grid.DataKeys[row.RowIndex].Values["Id"].ToString());

            Plantilla obj = uow.PlantillaBusinessLogic.GetByID(id);

            BindControles(obj);

            _IDPlantilla.Value = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Value = "A"; //Se cambia el estado de la forma a ACTUALIZAR un registro existente

            divCaptura.Style.Add("display", "block");
            divGuardar.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            //Se busca l ID de la fila seleccionada
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            string msg = "Se ha eliminado correctamente";

            int id = Utilerias.StrToInt(grid.DataKeys[row.RowIndex].Value.ToString());

            Plantilla obj = uow.PlantillaBusinessLogic.GetByID(id);

            //Se elimina el objeto
            uow.PlantillaBusinessLogic.Delete(obj);
            uow.SaveChanges();

            if (uow.Errors.Count > 0) //Si hubo errores
            {
                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
                lblMensajes.ForeColor = System.Drawing.Color.Red;


                return;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;

            BindGrid();
            divCaptura.Style.Add("display", "none");
            divBtnNuevo.Style.Add("display", "block");
            divMsg.Style.Add("display", "block");
        }

        protected void btnPP_ServerClick(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((HtmlButton)sender).NamingContainer;

            HttpContext.Current.Items["p"] = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            
            Server.Transfer("PlantillaPreguntas.aspx");
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            divCaptura.Style.Add("display", "block");
            divGuardar.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            Utilerias.LimpiarCampos(this);
            _Accion.Value = "N";
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Plantilla obj;
            string msg = "Se ha guardado correctamente";

            if (_Accion.Value.Equals("N"))
                obj = new Plantilla();
            else
            {
                obj = uow.PlantillaBusinessLogic.GetByID(Utilerias.StrToInt(_IDPlantilla.Value));
                msg = "Se ha actualizado correctamente";
            }
               


            obj.Clave = txtClave.Text;
            obj.Descripcion = txtDescripcion.Text;
            obj.EjercicioId = Utilerias.StrToInt(ddlEjercicio.SelectedValue);
            obj.Orden = Utilerias.StrToInt(txtOrden.Value);

            switch (_Accion.Value) 
            { 
                case "N":
                    //Agregar campos de bitacora


                    uow.PlantillaBusinessLogic.Insert(obj); //Se guarda el nuevo objeto creado

                    break;
                case "A":
                    //Agregar campos de bitacora

                    uow.PlantillaBusinessLogic.Update(obj); //Se actualiza el objeto


                    break;
            }

            uow.SaveChanges();

            if (uow.Errors.Count > 0)
            {
                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
                lblMensajes.ForeColor = System.Drawing.Color.Red;
               
                //lblMensajes.Attributes.Add("class", "alert alert-danger");
                divMsg.Style.Add("display", "block");
                return;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;

            //lblMensajes.Attributes.Add("title", msg);
            //lblMensajes.Attributes.Add("class", "alert alert-success");

            BindGrid();  //Se bindean los datos 
            divCaptura.Style.Add("display", "none");
            divGuardar.Style.Add("display", "none");
            divMsg.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "block");
                    

        }

        #endregion


        #region METODOS

        private void BindGrid() 
        {
            grid.DataSource = uow.PlantillaBusinessLogic.Get().ToList();
            grid.DataBind();

        }

        private void BindControles(Plantilla obj)
        {
            txtClave.Text = obj.Clave;
            txtDescripcion.Text = obj.Descripcion;
            ddlEjercicio.SelectedValue = obj.EjercicioId.ToString();
            txtOrden.Value = obj.Orden.ToString();
        }

        private void BindDrops()
        {
            Utilerias.ConstruyeCatalogos<Ejercicio>(uow.EjercicioBusinessLogic.Get().ToList<Ejercicio>(), this.ddlEjercicio, "Id", "Año");
        }



        #endregion


    }
}
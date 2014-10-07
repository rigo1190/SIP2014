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
    public partial class PlantillaPreguntas : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack) 
            {
                //_IDPlantilla.Value = HttpContext.Current.Items["p"].ToString(); //Se recupera el id del objeto padre PLANTILLA
                _IDPlantilla.Value = Request.QueryString["p"].ToString(); //Se recupera el id del objeto padre PLANTILLA
                Plantilla obj = uow.PlantillaBusinessLogic.GetByID(Utilerias.StrToInt(_IDPlantilla.Value));

                BindGrid();

                if (obj != null)
                    titulo.InnerText += " de la Plantilla " + obj.Descripcion; 

                divCaptura.Style.Add("display", "none");
                divGuardar.Style.Add("display", "none");
                divMsg.Style.Add("display", "none");
            }
        }


        #region EVENTOS

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;

            int id = Utilerias.StrToInt(grid.DataKeys[row.RowIndex].Values["Id"].ToString());

            PlantillaDetalle obj = uow.PlantillaDetalleBusinessLogic.GetByID(id);

            BindControles(obj);

            _IDPregunta.Value = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
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

            PlantillaDetalle obj = uow.PlantillaDetalleBusinessLogic.GetByID(id);

            //Se elimina el objeto
            uow.PlantillaDetalleBusinessLogic.Delete(obj);
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


        protected void btnDel_Click(object sender, EventArgs e)
        {
            string msg = "Se ha eliminado correctamente";

            int id = Utilerias.StrToInt(_IDPregunta.Value);

            PlantillaDetalle obj = uow.PlantillaDetalleBusinessLogic.GetByID(id);

            //Se elimina el objeto
            uow.PlantillaDetalleBusinessLogic.Delete(obj);
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
            PlantillaDetalle obj;
            string msg = "Se ha guardado correctamente";

            if (_Accion.Value.Equals("N"))
                obj = new PlantillaDetalle();
            else
            {
                obj = uow.PlantillaDetalleBusinessLogic.GetByID(Utilerias.StrToInt(_IDPregunta.Value));
                msg = "Se ha actualizado correctamente";
            }



            obj.Clave = txtClave.Text;
            obj.Pregunta = txtPregunta.Text;
            //obj.Orden = 1;

            switch (_Accion.Value)
            {
                case "N":
                    //Agregar campos de bitacora
                    obj.PlantillaId = Utilerias.StrToInt(_IDPlantilla.Value);

                    uow.PlantillaDetalleBusinessLogic.Insert(obj); //Se guarda el nuevo objeto creado

                    break;
                case "A":
                    //Agregar campos de bitacora

                    uow.PlantillaDetalleBusinessLogic.Update(obj); //Se actualiza el objeto


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
                divMsg.Style.Add("display", "block");
                return;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;

            BindGrid();  //Se bindean los datos 
            divCaptura.Style.Add("display", "none");
            divGuardar.Style.Add("display", "none");
            divMsg.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "block");
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlantillasPadre.aspx");
        }


        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ctrl = (ImageButton)e.Row.FindControl("imgBtnEliminar");

                if (ctrl != null)
                {
                    if (grid.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                    {
                        ctrl.Attributes.Add("onclick", "fnc_ColocarIDPregunta(" + grid.DataKeys[e.Row.RowIndex].Values["Id"] + ")");
                    }
                }
            }
        }
        #endregion


        #region METODOS

        public void BindGrid()
        {
            int IdPlantilla = Utilerias.StrToInt(_IDPlantilla.Value);

            grid.DataSource = uow.PlantillaDetalleBusinessLogic.Get(e => e.PlantillaId == IdPlantilla).ToList();

            grid.DataBind();

        }


        private void BindControles(PlantillaDetalle obj)
        {
            txtClave.Text = obj.Clave;
            txtPregunta.Text = obj.Pregunta;
            
        }

        #endregion

        

        



    }
}
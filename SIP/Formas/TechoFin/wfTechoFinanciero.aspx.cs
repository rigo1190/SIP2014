using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.TechoFin
{
    public partial class wfTechoFinanciero : System.Web.UI.Page
    {
        private UnitOfWork uow;
         
        private int idEjercicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            idEjercicio = int.Parse(Session["EjercicioId"].ToString());

            

            if (!IsPostBack){
                BindGrid();
                BindCombos();

                _URLVisor.Value = ResolveClientUrl("~/rpts/wfVerReporte.aspx");

                ModoLista();

                verificarStatusDelEjercicio();

                
            }

            

        }

        #region Metodos


        private void verificarStatusDelEjercicio()
        {
            List<TechoFinancieroStatus> listaStatus;

            _StatusEjercicio.Text = "0";

            listaStatus = uow.TechoFinancieroStatusBusinessLogic.Get(p => p.EjercicioId == idEjercicio).ToList();
            
            foreach (TechoFinancieroStatus tfs in listaStatus)
                _StatusEjercicio.Text = tfs.Status.ToString();

            if (_StatusEjercicio.Text == "2")
            {
                divBtnNuevo.Style.Add("display", "none");
                lblStatus.Text = "Status: Cerrado";
                idLinkClose.Visible = false;
            }
            else
            {
                lblStatus.Text = "Status: Abierto para captura";
                idLinkClose.Visible = true;
            }
                
                
        } 

        private void BindGrid()
        {
            uow = new UnitOfWork();
            var lista = from tf in uow.TechoFinancieroBusinessLogic.Get()
                        where tf.EjercicioId == idEjercicio
                        select new { tf.Id, tf.Ejercicio, tf.EjercicioId, tf.Financiamiento, tf.FinanciamientoId, tf.Importe, ImporteAsignadoUp = tf.detalleUnidadesPresupuestales.Sum(p => p.Importe), ImporteAsignadoObras = tf.getImporteAsignadoObras()};


            //this.grid.DataSource = uow.TechoFinancieroBusinessLogic.Get(p=> p.EjercicioId == idEjercicio ,includeProperties:"Financiamiento").ToList();
            this.grid.DataSource = lista;
            this.grid.DataBind();
        }

        private void BindCombos()
        {
            ddlFondo.DataSource = uow.FondoBusinessLogic.Get().ToList().OrderBy(p=>p.Abreviatura);
            ddlFondo.DataValueField = "Id";
            ddlFondo.DataTextField = "Abreviatura";
            ddlFondo.DataBind();

            ddlAño.DataSource = uow.AñoBusinessLogic.Get().OrderByDescending(p=>p.Anio).ToList();
            ddlAño.DataValueField = "Id";
            ddlAño.DataTextField = "Anio";
            ddlAño.DataBind();

            ddlModalidad.DataSource = uow.ModalidadFinanciamientoBusinessLogic.Get().ToList();
            ddlModalidad.DataValueField = "Id";
            ddlModalidad.DataTextField = "Nombre";
            ddlModalidad.DataBind();
        }

        private void ModoLista()
        {
            divMsgSuccess.Style.Add("display", "none");
            divMsgFail.Style.Add("display", "none");

            divBtnNuevo.Style.Add("display", "block");
            divNuevoRegistro.Style.Add("display", "none");
            divEditar.Style.Add("display", "none");
        }
        private void ModoNuevo()
        {
            divMsgSuccess.Style.Add("display", "none");
            divMsgFail.Style.Add("display", "none");

            divBtnNuevo.Style.Add("display", "none");
            divNuevoRegistro.Style.Add("display", "block");
        }
        private void ModoEditar()
        {
            divMsgSuccess.Style.Add("display", "none");
            divMsgFail.Style.Add("display", "none");

            divBtnNuevo.Style.Add("display", "none");
            divNuevoRegistro.Style.Add("display", "none");
            divEditar.Style.Add("display","block");
        }


        


        #endregion
        
        #region Eventos


        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if (_StatusEjercicio.Text == "2")
                return;

            ModoNuevo();            
        }
        
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
            TechoFinanciero obj;
            Financiamiento fin;
            List<Financiamiento> listaFinanciamientos;
            List<TechoFinanciero> listaTechosFin;

            int fondo = int.Parse(ddlFondo.SelectedValue);
            int modalidad = int.Parse(ddlModalidad.SelectedValue);
            int año = int.Parse(ddlAño.SelectedValue);
            int idFinanciamiento = 0;
            
            listaFinanciamientos = uow.FinanciamientoBusinessLogic.Get(p => p.FondoId == fondo && p.ModalidadFinanciamientoId == modalidad && p.AñoId == año).ToList();
            
            //buscamos el financiamiento seleccionado y si no existe lo creamos
            if (listaFinanciamientos.Count == 0)
            {
                fin = new Financiamiento();

                fin.FondoId = fondo;
                fin.ModalidadFinanciamientoId = modalidad;
                fin.AñoId = año;

                uow.FinanciamientoBusinessLogic.Insert(fin);
                uow.SaveChanges();

                idFinanciamiento = fin.Id;

            }
            else
            {
                foreach (Financiamiento aux in listaFinanciamientos)
                {
                    idFinanciamiento = aux.Id;
                }
            }




            uow.Errors.Clear();
            listaTechosFin = uow.TechoFinancieroBusinessLogic.Get(p => p.EjercicioId == idEjercicio && p.FinanciamientoId == idFinanciamiento).ToList();

            if (listaTechosFin.Count == 0)//Nuevo Techo Financiero
            {
                obj = new TechoFinanciero();

                obj.EjercicioId = this.idEjercicio;
                obj.FinanciamientoId = idFinanciamiento;
                obj.Importe = decimal.Parse(txtImporte.Value);

                uow.TechoFinancieroBusinessLogic.Insert(obj);
            }
            else
            {
                uow.Errors.Add("El financiamiento que quiere registrar ya ha sido capturado anteriormente");
            }

            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {

                BindGrid();
                txtImporte.Value = "0";
                
                ModoLista();
                
                divMsgSuccess.Style.Add("display", "block");
                lblMensajeSuccess.Text = "El Techo Financiero ha sido registrado correctamente";

            }
            else
            {
                divMsgSuccess.Style.Add("display", "none");
                divMsgFail.Style.Add("display", "block");

                string mensaje;
                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMensajeFail.Text = mensaje;

            }


        }
        
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ModoLista();
        }
        



        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
            {
                if (_StatusEjercicio.Text == "2")
                    return;

                GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
                _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
        
                TechoFinanciero tf = uow.TechoFinancieroBusinessLogic.GetByID(int.Parse(_ElId.Text));
            
        
                txtImporteEdicion.Value = tf.Importe.ToString();
                ModoEditar();
             
            }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            TechoFinanciero tf = uow.TechoFinancieroBusinessLogic.GetByID(int.Parse(_ElId.Text));
            decimal importeAsignado;
            decimal nuevoImporte;

            importeAsignado = tf.detalleUnidadesPresupuestales.Sum(p => p.Importe);
            nuevoImporte = decimal.Parse(txtImporteEdicion.Value);
            uow.Errors.Clear();

            if (importeAsignado > nuevoImporte)
                uow.Errors.Add("El nuevo importe es menor al que ya ha sido asignado a las entidades, verifique y corrija su información");


            tf.Importe = nuevoImporte;
            uow.TechoFinancieroBusinessLogic.Update(tf);

            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {
                BindGrid();
                txtImporteEdicion.Value = "0";

                ModoLista();

                divMsgSuccess.Style.Add("display", "block");
                lblMensajeSuccess.Text = "La modificación ha sido registrada correctamente";

            }
            else
            {
                divMsgSuccess.Style.Add("display", "none");
                divMsgFail.Style.Add("display", "block");

                string mensaje;
                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMensajeFail.Text = mensaje;

            }





        }
        protected void btnCancelarModificacion_Click(object sender, EventArgs e)
        {
            ModoLista();
        }



        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            if (_StatusEjercicio.Text == "2")
                return;

            int id;
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;

            id = int.Parse(grid.DataKeys[row.RowIndex].Values["Id"].ToString());

            TechoFinanciero tf = uow.TechoFinancieroBusinessLogic.GetByID(id);


            uow.Errors.Clear();
            List<TechoFinancieroUnidadPresupuestal> lista;
            lista = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == tf.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");
                
                


            if (uow.Errors.Count == 0)
            {
                uow.TechoFinancieroBusinessLogic.Delete(tf);
                uow.SaveChanges();
                ModoLista();
                divMsgSuccess.Style.Add("display", "block");
                lblMensajeSuccess.Text = "El registro se ha eliminado correctamente";
            }
                

            if (uow.Errors.Count == 0)
            {
                BindGrid();

            }
            else
            {
                ModoLista();
                divMsgSuccess.Style.Add("display", "none");
                divMsgFail.Style.Add("display", "block");

                string mensaje;
                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMensajeFail.Text = mensaje;
            }


        }

        protected void imgSubdetalle_Click(object sender, ImageClickEventArgs e)
        {
            int id;
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;

            id = int.Parse(grid.DataKeys[row.RowIndex].Values["Id"].ToString());

            Response.Redirect("wfTechoFinancieroUnidadPresupuestal.aspx?id=" + id);
        }
        

        protected void imgVerRPT_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int idTF = Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                ImageButton imgBut = (ImageButton)e.Row.FindControl("imgVerRPT");
                if (imgBut != null)
                    imgBut.Attributes["onclick"] = "fnc_AbrirReporte(" + idTF + ");return false;";
            }
        }

        #endregion


    }
}
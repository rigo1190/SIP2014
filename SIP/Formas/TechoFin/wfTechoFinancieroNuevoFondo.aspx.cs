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
    public partial class wfTechoFinancieroNuevoFondo : System.Web.UI.Page
    {

        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            divMsgFail.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");

            int idEjercicio = 0;

            if (!IsPostBack)
            {
                BorrarTMPbyUsuario();
                BindCombos();
                bindComboUP();

                divRegistrarNuevoTecho.Style.Add("display", "none");



                idEjercicio = int.Parse(Session["EjercicioId"].ToString());
                List<TechoFinancieroStatus> lista = uow.TechoFinancieroStatusBusinessLogic.Get(p => p.EjercicioId == idEjercicio &&  p.Status == 2).ToList();


                if (lista.Count == 0)
                {
                    divAdd.Style.Add("display","none");
                    divNoSePuede.Style.Add("display", "none");
                }
                else
                {
                    divAdd.Style.Add("display", "block");
                    divNoSePuede.Style.Add("display", "none");
                }



            }
            
        }





        #region Metodos
        private void bindComboUP()
        {
            int usuario = int.Parse(Session["IdUser"].ToString());

            var lista = from up in uow.UnidadPresupuestalBusinessLogic.Get()
                        join techo in uow.TechoFinancieroTMPtransferenciasBL.Get(p => p.Usuario == usuario).ToList()
                        on up.Id equals techo.OrigenId into abc
                        from x in abc.DefaultIfEmpty()
                        select new { Id = up.Id, Nombre = up.Nombre, techo = (x == null ? 0 : x.Id) };



            ddlOrigen.DataSource = lista.Where(p => p.techo == 0).ToList();
            ddlOrigen.DataValueField = "Id";
            ddlOrigen.DataTextField = "Nombre";
            ddlOrigen.DataBind();

            if (ddlOrigen.Items.Count == 0) { btnAdd.Enabled = false; } else { btnAdd.Enabled = true; }

        }

        private void BindCombos()
        {




            ddlFondo.DataSource = uow.FondoBusinessLogic.Get(p => p.ParentId != null).ToList();
            ddlFondo.DataValueField = "Id";
            ddlFondo.DataTextField = "Abreviatura";
            ddlFondo.DataBind();

            ddlAño.DataSource = uow.AñoBusinessLogic.Get().OrderByDescending(p => p.Anio).ToList();
            ddlAño.DataValueField = "Id";
            ddlAño.DataTextField = "Anio";
            ddlAño.DataBind();

            ddlModalidad.DataSource = uow.ModalidadFinanciamientoBusinessLogic.Get().ToList();
            ddlModalidad.DataValueField = "Id";
            ddlModalidad.DataTextField = "Nombre";
            ddlModalidad.DataBind();
        }


        private void BorrarTMPbyUsuario()
        {
            int usuario = int.Parse(Session["IdUser"].ToString());
            List<TechoFinancieroTMPtransferencias> lista = uow.TechoFinancieroTMPtransferenciasBL.Get(p => p.Usuario == usuario).ToList();

            foreach (TechoFinancieroTMPtransferencias elemento in lista)
                uow.TechoFinancieroTMPtransferenciasBL.Delete(elemento);

            uow.SaveChanges();

        }

        private void MostrarDetalle()
        {
            uow = new UnitOfWork();
            int usuario = int.Parse(Session["IdUser"].ToString());
            this.gridDetalle.DataSource = uow.TechoFinancieroTMPtransferenciasBL.Get(p => p.Usuario == usuario).ToList();
            this.gridDetalle.DataBind();

            if (gridDetalle.Rows.Count == 0)
            {
                divRegistrarNuevoTecho.Style.Add("display", "none");
            }
            else
            {
                divRegistrarNuevoTecho.Style.Add("display", "block");
            }

            bindComboUP();

            List<TechoFinancieroTMPtransferencias> lista = uow.TechoFinancieroTMPtransferenciasBL.Get(p => p.Usuario == usuario).ToList();

            if (lista.Count == 0)
            {
                lblTotal.Text = "$0.00";
            }
            else
            {
                lblTotal.Text = lista.Sum(q => q.Importe).ToString("C");
            }

        }
        #endregion




        #region Eventos

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int origen = int.Parse(ddlOrigen.SelectedValue);
            int destino = origen;
            int usuario = int.Parse(Session["IdUser"].ToString());
            int financiamiento = 0;


            TechoFinancieroTMPtransferencias obj = new TechoFinancieroTMPtransferencias();



            List<TechoFinancieroTMPtransferencias> lista = uow.TechoFinancieroTMPtransferenciasBL.Get(p => p.Usuario == usuario && p.Financiamiento == financiamiento && p.OrigenId == origen && p.DestinoId == destino).ToList();
            if (lista.Count > 0)
                uow.Errors.Add("Las unidades presupuestales Origen y Destino que indico ya están registradas");




            obj.Usuario = usuario;
            obj.Financiamiento = 0;
            obj.OrigenId = origen;
            obj.DestinoId = destino;
            obj.Importe = decimal.Parse(txtImporte.Value);

            uow.TechoFinancieroTMPtransferenciasBL.Insert(obj);





            if (uow.Errors.Count == 0)
                uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {                
                MostrarDetalle();
                txtImporte.Value = string.Empty;

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

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            int id = int.Parse(gridDetalle.DataKeys[row.RowIndex].Values["Id"].ToString());

            TechoFinancieroTMPtransferencias obj = uow.TechoFinancieroTMPtransferenciasBL.GetByID(id);

            uow.TechoFinancieroTMPtransferenciasBL.Delete(obj);
            uow.SaveChanges();
            MostrarDetalle();
        }



        #endregion

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Financiamiento fin;
            List<Financiamiento> listaFinanciamientos;
            List<TechoFinanciero> listaTechosFin;

            int fondo = int.Parse(ddlFondo.SelectedValue);
            int modalidad = int.Parse(ddlModalidad.SelectedValue);
            int año = int.Parse(ddlAño.SelectedValue);
            int idFinanciamiento = 0;
            
            int idEjercicio = int.Parse(Session["EjercicioId"].ToString());
            int usuario = int.Parse(Session["IdUser"].ToString());

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

            if (txtOficio.Value.Trim() == string.Empty)           
                uow.Errors.Add("No ha capturado el número de oficio correspondiente a este registro");

            if (txtObservaciones.Value.Trim() == string.Empty)
                uow.Errors.Add("No ha capturado las observaciones correspondientes");


            listaTechosFin = uow.TechoFinancieroBusinessLogic.Get(p => p.EjercicioId == idEjercicio && p.FinanciamientoId == idFinanciamiento).ToList();

            if (listaTechosFin.Count == 0)//Nuevo Techo Financiero
            {

                List<TechoFinancieroTMPtransferencias> listaTMP = uow.TechoFinancieroTMPtransferenciasBL.Get(p => p.Usuario == usuario).ToList();

                //insertar el techo financiero
                TechoFinanciero obj  = new TechoFinanciero();
                    obj.EjercicioId = idEjercicio;
                    obj.FinanciamientoId = idFinanciamiento;
                    obj.Importe = listaTMP.Sum(q=>q.Importe);
                uow.TechoFinancieroBusinessLogic.Insert(obj);

                //insertar la bitacora
                TechoFinancieroBitacora bitacora = new TechoFinancieroBitacora();
                    bitacora.EjercicioId = idEjercicio;
                    bitacora.Movimiento = 3;
                    bitacora.Tipo = EnumTipoMovimientoTechoFinanciero.NuevoFondo;
                    bitacora.Fecha = DateTime.Now;
                    bitacora.Oficio = txtOficio.Value;
                    bitacora.Observaciones = txtObservaciones.Value;
                uow.TechoFinancieroBitacoraBL.Insert(bitacora);


                
                int i = 0;
                foreach (TechoFinancieroTMPtransferencias item in listaTMP)
                {
                    i++;

                    TechoFinancieroUnidadPresupuestal tfup = new TechoFinancieroUnidadPresupuestal();
                        tfup.UnidadPresupuestalId = item.OrigenId;
                        tfup.Importe = item.Importe;
                    obj.detalleUnidadesPresupuestales.Add(tfup);



                    TechoFinancieroBitacoraMovimientos bitacoraMovimientos = new TechoFinancieroBitacoraMovimientos();
                        bitacoraMovimientos.TechoFinancieroBitacora = bitacora;                        
                        bitacoraMovimientos.Submovimiento = i;
                        bitacoraMovimientos.UnidadPresupuestalId = item.OrigenId;
                        bitacoraMovimientos.Decremento = 0;
                        bitacoraMovimientos.Incremento = item.Importe;
                    obj.detalleBitacoraMovimientos.Add(bitacoraMovimientos);

                    
                }


                
                

            }
            else
            {
                uow.Errors.Add("El financiamiento que quiere registrar ya ha sido capturado anteriormente");
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();





            if (uow.Errors.Count == 0)
            {
                Response.Redirect("wfTechoFinanciero.aspx");
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

        protected void btnRegresar2_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfTechoFinanciero.aspx");
        }

        
        



    }
}
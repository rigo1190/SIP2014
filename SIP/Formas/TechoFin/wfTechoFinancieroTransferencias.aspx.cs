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
    public partial class wfTechoFinancieroTransferencias : System.Web.UI.Page
    {
        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            divMsgFail.Style.Add("display","none");
            divMsgSuccess.Style.Add("display", "none");
            

            if (!IsPostBack)
            {
                BindCombos();
                divTransferencia.Style.Add("display", "none");
                divNuevo.Style.Add("display", "none");
            }


        }


        #region Metodos
        private void BindCombos()
        {
            int idEjercicio = int.Parse(Session["EjercicioId"].ToString());
            


            ddlFinanciamiento.DataSource = uow.TechoFinancieroBusinessLogic.Get(p => p.EjercicioId == idEjercicio).ToList();
            ddlFinanciamiento.DataValueField = "Id";
            ddlFinanciamiento.DataTextField = "Descripcion";
            ddlFinanciamiento.DataBind();

            ddlFinanciamiento.Items.Insert(0, new ListItem("Seleccione el financiamiento", "0"));

            ddlDestino.DataSource = uow.UnidadPresupuestalBusinessLogic.Get().ToList();
            ddlDestino.DataValueField = "Id";
            ddlDestino.DataTextField = "Nombre";
            ddlDestino.DataBind();

        }

        private void MostrarAsignaciones()
        {

            int financiamiento = int.Parse(ddlFinanciamiento.SelectedValue.ToString());
            _financiamiento.Text = financiamiento.ToString();

            divTransferencia.Style.Add("display", "none");
            divNuevo.Style.Add("display", "none");
            

            if (financiamiento == 0)
                return;


            uow = new UnitOfWork();

            var lista = from tfup in uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get() 
                        where tfup.TechoFinancieroId == financiamiento
                        select new { tfup.Id, tfup.UnidadPresupuestal, tfup.UnidadPresupuestalId, tfup.Importe, ImporteEjecutado = tfup.DetalleObraFinanciamiento.Sum(p => p.Importe), ImporteDisponible = tfup.Importe - tfup.DetalleObraFinanciamiento.Sum(p => p.Importe) };

            //this.grid.DataSource = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == financiamiento).ToList();
            this.grid.DataSource = lista;
            this.grid.DataBind();

            TechoFinanciero tf = uow.TechoFinancieroBusinessLogic.GetByID(financiamiento);
            lblTotal.Text = "Techo Financiero :" + tf.Importe.ToString("C");

            
            divNuevo.Style.Add("display", "block");



            this.grid.DataSource = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == financiamiento).ToList();



            var ListaUPs = from up in uow.UnidadPresupuestalBusinessLogic.Get().ToList()
                           join tfup in uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == financiamiento).ToList()
                           on up.Id equals tfup.UnidadPresupuestalId
                           select up;


            ddlOrigen.DataSource = ListaUPs.ToList();
            ddlOrigen.DataValueField = "Id";
            ddlOrigen.DataTextField = "Nombre";
            ddlOrigen.DataBind();

        }

        private void MostrarDetalleTransferencia()
        {
            uow = new UnitOfWork();
            int usuario = int.Parse(Session["IdUser"].ToString());
            this.gridDetalle.DataSource = uow.TechoFinancieroTMPtransferenciasBL.Get(p=>p.Usuario == usuario).ToList();
            this.gridDetalle.DataBind();
        }


        #endregion
        

        protected void ddlFinanciamiento_SelectedIndexChanged(object sender, EventArgs e)
        {
                       

            MostrarAsignaciones();


            BorrarTMPbyUsuario();

        }

        private void BorrarTMPbyUsuario()
        {
            int usuario = int.Parse(Session["IdUser"].ToString());
            List<TechoFinancieroTMPtransferencias> lista = uow.TechoFinancieroTMPtransferenciasBL.Get(p => p.Usuario == usuario).ToList();

            foreach (TechoFinancieroTMPtransferencias elemento in lista)
                uow.TechoFinancieroTMPtransferenciasBL.Delete(elemento);
            
            uow.SaveChanges();


        }

        #region Eventos
        


        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            divTransferencia.Style.Add("display", "block");
            divNuevo.Style.Add("display", "none");
            MostrarDetalleTransferencia();
        }



        protected void btnAdd_Click(object sender, EventArgs e)
        {   
            int origen = int.Parse(ddlOrigen.SelectedValue);
            int destino = int.Parse(ddlDestino.SelectedValue);
            int usuario = int.Parse(Session["IdUser"].ToString());
            int financiamiento = int.Parse(_financiamiento.Text);

            decimal importeDisponible;
            decimal importeTransferencias;
            decimal importe;

            TechoFinancieroTMPtransferencias obj = new TechoFinancieroTMPtransferencias();

            uow.Errors.Clear();
            if (origen == destino)
                uow.Errors.Add("La unidad presupuestal ORIGEN no puede ser la misma que la unidad presupuestal DESTINO");


            List<TechoFinancieroTMPtransferencias> lista = uow.TechoFinancieroTMPtransferenciasBL.Get(p=> p.Usuario == usuario && p.Financiamiento == financiamiento && p.OrigenId == origen && p.DestinoId == destino).ToList();
            if (lista.Count > 0)
                uow.Errors.Add("Las unidades presupuestales Origen y Destino que indico ya están registradas");

            //techo financiero disponible de la unidad presupuestal Origen
            TechoFinancieroUnidadPresupuestal tfup = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == financiamiento && p.UnidadPresupuestalId == origen).First();
            importeDisponible = (tfup.Importe - tfup.DetalleObraFinanciamiento.Sum(p => p.Importe));
            importeTransferencias = uow.TechoFinancieroTMPtransferenciasBL.Get(p => p.Usuario == usuario && p.Financiamiento == financiamiento && p.OrigenId == origen).ToList().Sum(q => q.Importe);
            importe = decimal.Parse(txtImporte.Value);

            if (importeDisponible < (importeTransferencias + importe))
                uow.Errors.Add("El importe que desea registrar dejaría sobregiraria el disponible de la Unidad Presupuestal Origen, verifique y corrija sus datos ");



            obj.Usuario = usuario;
            obj.Financiamiento = int.Parse(_financiamiento.Text);
            obj.OrigenId = origen;
            obj.DestinoId = destino;
            obj.Importe = decimal.Parse(txtImporte.Value);
                
            uow.TechoFinancieroTMPtransferenciasBL.Insert(obj);
            






            if (uow.Errors.Count == 0)
                uow.SaveChanges();
                
            if (uow.Errors.Count == 0)
            {

                MostrarDetalleTransferencia();
                txtImporte.Value = string.Empty;

                //divMsgSuccess.Style.Add("display", "block");
                //lblMensajeSuccess.Text = "El Techo Financiero ha sido registrado correctamente";

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
            MostrarDetalleTransferencia();


        }







        protected void btnOk_Click(object sender, EventArgs e)
        {
            int usuario = int.Parse(Session["IdUser"].ToString());
            int idEjercicio = int.Parse(Session["EjercicioId"].ToString());
            int financiamiento = int.Parse(_financiamiento.Text);
            int next = uow.TechoFinancieroBitacoraBL.Get(p => p.EjercicioId == idEjercicio).ToList().Max(q => q.Movimiento);
            next++;


            TechoFinancieroBitacora bitacora = new TechoFinancieroBitacora();
            bitacora.EjercicioId = idEjercicio;
            bitacora.Movimiento = next;
            bitacora.Tipo = EnumTipoMovimientoTechoFinanciero.Transferencia;
            bitacora.Fecha = DateTime.Now;
            bitacora.Oficio = txtOficio.Value;
            bitacora.Observaciones = txtObservaciones.Value;

            uow.TechoFinancieroBitacoraBL.Insert(bitacora);

            List<TechoFinancieroTMPtransferencias> lista; 
            lista = uow.TechoFinancieroTMPtransferenciasBL.Get(p=>p.Usuario == usuario).ToList();


            TechoFinancieroBitacoraMovimientos bitmov;
            TechoFinancieroUnidadPresupuestal tfup;
            
            int i = 0;
            foreach (TechoFinancieroTMPtransferencias elemento in lista)
            {
                i++;

                //Decrementamos

                
                bitmov = new TechoFinancieroBitacoraMovimientos();                
                bitmov.TechoFinancieroId = financiamiento;
                bitmov.Submovimiento = i;
                bitmov.UnidadPresupuestalId = elemento.OrigenId;
                bitmov.Incremento = 0;
                bitmov.Decremento = elemento.Importe;
                bitacora.detalleMovimientos.Add(bitmov);
                


                
                tfup = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == financiamiento && p.UnidadPresupuestalId == elemento.OrigenId).First();
                tfup.Importe = tfup.Importe - elemento.Importe;
                uow.TechoFinancieroUnidadPresuestalBusinessLogic.Update(tfup);

                //incrementamos
                bitmov = new TechoFinancieroBitacoraMovimientos();
                bitmov.TechoFinancieroId = financiamiento;
                bitmov.Submovimiento = i;
                bitmov.UnidadPresupuestalId = elemento.DestinoId;
                bitmov.Incremento = elemento.Importe;
                bitmov.Decremento = 0;
                bitacora.detalleMovimientos.Add(bitmov);
                


                List<TechoFinancieroUnidadPresupuestal> listaUP = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p=>p.TechoFinancieroId == financiamiento && p.UnidadPresupuestalId == elemento.DestinoId).ToList();

                if (listaUP.Count == 0)
                {
                    tfup = new TechoFinancieroUnidadPresupuestal();
                    tfup.TechoFinancieroId = financiamiento;
                    tfup.UnidadPresupuestalId = elemento.DestinoId;
                    tfup.Importe = elemento.Importe;
                    uow.TechoFinancieroUnidadPresuestalBusinessLogic.Insert(tfup);
                }
                else
                {
                    tfup = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == financiamiento && p.UnidadPresupuestalId == elemento.DestinoId).First();
                    tfup.Importe = tfup.Importe + elemento.Importe;
                    uow.TechoFinancieroUnidadPresuestalBusinessLogic.Update(tfup);
                }

                

            }

            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {
                divTransferencia.Style.Add("display", "none");
                divNuevo.Style.Add("display", "block");

                txtOficio.Value = string.Empty;
                txtObservaciones.Value = string.Empty;

                MostrarAsignaciones();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            BorrarTMPbyUsuario();

            divTransferencia.Style.Add("display", "none");
            divNuevo.Style.Add("display", "block");
        }








        #endregion

        

        

    }
}
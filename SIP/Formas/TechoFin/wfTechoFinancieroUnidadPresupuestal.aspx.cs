using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Linq;

namespace SIP.Formas.TechoFin
{
    public partial class wfTechoFinancieroUnidadPresupuestal : System.Web.UI.Page
    {
        private UnitOfWork uow;
         
        private int idTechoFinanciero;
        private int idEjercicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            idTechoFinanciero = int.Parse(Request.QueryString["id"].ToString());
            idEjercicio = int.Parse(Session["EjercicioId"].ToString());
            
            if (!IsPostBack)
            {

                TechoFinanciero tf = uow.TechoFinancieroBusinessLogic.GetByID(idTechoFinanciero);

                lblUPnombre.Text = tf.Financiamiento.Fondo.Abreviatura.ToString() + " (" + tf.Financiamiento.ModalidadFinanciamiento.Nombre.ToString() + ":" + tf.Financiamiento.Año.Anio.ToString() + ")";

                divMsgSuccess.Style.Add("display", "none");
                divMsgFail.Style.Add("display", "none");
                
                BindGrid();

                verificarStatusDelEjercicio();
                
            }            
        }



        #region Metodos

        private void BindGrid()
        {
            TechoFinanciero tf = uow.TechoFinancieroBusinessLogic.GetByID(idTechoFinanciero);

            decimal importetotal = tf.Importe;
            decimal importeasignado = tf.detalleUnidadesPresupuestales.Sum(p => p.Importe);

            this.grid.DataSource = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == idTechoFinanciero, includeProperties: "UnidadPresupuestal").ToList();  
            this.grid.DataBind();

            lblTechoFinanciero.Text = importetotal.ToString("C");
            lblImporteAsignado.Text = importeasignado.ToString("C");
            lblImporteDisponible.Text = (importetotal - importeasignado).ToString("C");

            BindCombos();
        }

        private void BindCombos()
        {


            var lista = from up in uow.UnidadPresupuestalBusinessLogic.Get()
                         join techo in uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == idTechoFinanciero).ToList()
                         on up.Id equals techo.UnidadPresupuestalId into abc
                         from x in abc.DefaultIfEmpty()
                         select new { Id = up.Id, Nombre= up.Nombre , techo=(x==null? 0 : x.Id)  };

            ddlUP.DataSource = lista.Where(p=>p.techo == 0).ToList();

            //ddlUP.DataSource = uow.UnidadPresupuestalBusinessLogic.Get().ToList();            
            ddlUP.DataValueField = "Id";
            ddlUP.DataTextField = "Nombre";
            ddlUP.DataBind();

            if (ddlUP.Items.Count == 0) { btnAdd.Enabled = false; } else { btnAdd.Enabled = true; }



        }


        private void verificarStatusDelEjercicio()
        {
            List<TechoFinancieroStatus> listaStatus;

            _StatusEjercicio.Text = "0";

            listaStatus = uow.TechoFinancieroStatusBusinessLogic.Get(p => p.EjercicioId == idEjercicio).ToList();

            foreach (TechoFinancieroStatus tfs in listaStatus)
                _StatusEjercicio.Text = tfs.Status.ToString();

            if (_StatusEjercicio.Text == "2")
            {
                divEjercicioCerrado.Style.Add("display", "block");
                divAdd.Style.Add("display", "none");
                lblStatus.Text = "Status: Cerrado";
            }
            else
            {
                divEjercicioCerrado.Style.Add("display", "none");
                divAdd.Style.Add("display", "block");
                lblStatus.Text = "Status: Abierto para captura";
            }


        }
        #endregion


        #region Eventos
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (_StatusEjercicio.Text == "2")
                return;

            TechoFinanciero techoFin;
            TechoFinancieroUnidadPresupuestal obj;
            List<TechoFinancieroUnidadPresupuestal> listaTFUP;

            int idUnidadPresupuestal = int.Parse(ddlUP.SelectedValue);
            int auxId = 0;

            decimal sumaImportes = 0;

            techoFin = uow.TechoFinancieroBusinessLogic.GetByID(idTechoFinanciero);

            listaTFUP = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == idTechoFinanciero && p.UnidadPresupuestalId == idUnidadPresupuestal).ToList();

            uow.Errors.Clear();

            if (listaTFUP.Count == 0)
            {

                sumaImportes = techoFin.detalleUnidadesPresupuestales.Sum(p => p.Importe);
                sumaImportes = sumaImportes + decimal.Parse(txtImporte.Value);

                if (sumaImportes > techoFin.Importe)
                {
                    uow.Errors.Add("Este importe no puede registrarse porque sobregiraria el techo financiero de este Fondo");
                }
                else
                {
                    obj = new TechoFinancieroUnidadPresupuestal();
                    obj.TechoFinancieroId = idTechoFinanciero;
                    obj.UnidadPresupuestalId = idUnidadPresupuestal;
                    obj.Importe = decimal.Parse(txtImporte.Value);
                    uow.TechoFinancieroUnidadPresuestalBusinessLogic.Insert(obj);
                }
                    

            }
            else
            {
                
                foreach (TechoFinancieroUnidadPresupuestal elemento in listaTFUP)                
                    auxId = elemento.Id;

                sumaImportes = techoFin.detalleUnidadesPresupuestales.Where(p => p.TechoFinancieroId == idTechoFinanciero && p.Id != auxId).Sum(p => p.Importe);
                sumaImportes = sumaImportes + decimal.Parse(txtImporte.Value);

                if (sumaImportes > techoFin.Importe)
                {
                    uow.Errors.Add("Este importe no puede registrarse porque sobregiraria el techo financiero de este Fondo");
                }
                else
                {
                    obj = uow.TechoFinancieroUnidadPresuestalBusinessLogic.GetByID(auxId);
                    obj.Importe = decimal.Parse(txtImporte.Value);
                    uow.TechoFinancieroUnidadPresuestalBusinessLogic.Update(obj);
                }
                
            }


            divMsgSuccess.Style.Add("display", "none");
            divMsgFail.Style.Add("display", "none");

            if (uow.Errors.Count == 0)
            {
                uow.SaveChanges();
                //divMsgFail.Style.Add("display", "none");
                //divMsgSuccess.Style.Add("display", "block");
                //lblMensajeSuccess.Text = "El registro se ha agregado correctamente";
            }
            else
            {
                
                divMsgFail.Style.Add("display", "block");
                lblMensajeFail.Text = "Este importe no puede registrarse porque sobregiraria el techo financiero de este Fondo";
            }
                

        


            BindGrid();
            txtImporte.Value = string.Empty;


        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            if (_StatusEjercicio.Text == "2")
                return;

            divMsgSuccess.Style.Add("display", "none");
            divMsgFail.Style.Add("display", "none");

            TechoFinancieroUnidadPresupuestal obj; 
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            int id;

            id = int.Parse( grid.DataKeys[row.RowIndex].Values["Id"].ToString());
            
            obj = uow.TechoFinancieroUnidadPresuestalBusinessLogic.GetByID(id);

            uow.TechoFinancieroUnidadPresuestalBusinessLogic.Delete(obj);
            uow.SaveChanges();

            BindGrid();



            //
            //uow.Errors.Clear();
            //List<TechoFinancieroUnidadPresupuestal> lista;
            //lista = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinancieroId == tf.Id).ToList();


            //if (lista.Count > 0)
            //    uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");




            //if (uow.Errors.Count == 0)
            //{
            //    uow.TechoFinancieroBusinessLogic.Delete(tf);
            //    uow.SaveChanges();
                
            //    divMsgSuccess.Style.Add("display", "block");
            //    lblMensajeSuccess.Text = "El registro se ha eliminado correctamente";
            //}


            //if (uow.Errors.Count == 0)
            //{
            //    BindGrid();

            //}
            //else
            //{
                
            //    divMsgSuccess.Style.Add("display", "none");
            //    divMsgFail.Style.Add("display", "block");

            //    string mensaje;
            //    mensaje = string.Empty;
            //    foreach (string cad in uow.Errors)
            //        mensaje = mensaje + cad + "<br>";

            //    lblMensajeFail.Text = mensaje;
            //}




        }

        #endregion


        
        


    }
}
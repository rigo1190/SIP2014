using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class AsignarFinanciamientoPOA : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int currentId;     
        private int unidadpresupuestalId;
        private int ejercicioId;
        private int poadetalleId;
        protected string obraNumero;
        protected string obraDescripcion;
        protected bool enproyectopoa;
        protected void Page_Load(object sender, EventArgs e)
        {            
            uow = new UnitOfWork(Session["IdUser"].ToString());      

            poadetalleId = Utilerias.StrToInt(Request.QueryString["poadetalleId"].ToString());
            enproyectopoa = Convert.ToBoolean(Request.QueryString["EnProyecto"]);

            if (enproyectopoa)
            {
                divlinkPOAFinanciamiento.Style.Add("display", "block");
                divlinkPOAAjustadoFinanciamiento.Style.Add("display", "none");
            }
            else 
            {
                divlinkPOAFinanciamiento.Style.Add("display", "none");
                divlinkPOAAjustadoFinanciamiento.Style.Add("display", "block");
            }

            POADetalle poadetalle = uow.POADetalleBusinessLogic.GetByID(poadetalleId);
            obraNumero = poadetalle.Numero;
            obraDescripcion = poadetalle.Descripcion;

           

            if (!IsPostBack) 
            {
                BindearDropDownList();
                BindGrid();
            }
            
        }

        private void BindGrid()
        {

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());
            poadetalleId = Utilerias.StrToInt(Request.QueryString["poadetalleId"].ToString());

            this.GridViewObraFinanciamiento.DataSource = uow.ObraFinanciamientoBusinessLogic.Get(of => of.TechoFinancieroUnidadPresupuestal.UnidadPresupuestalId == unidadpresupuestalId & of.TechoFinancieroUnidadPresupuestal.TechoFinanciero.EjercicioId == ejercicioId & of.Obra.POADetalleId==poadetalleId, includeProperties: "TechoFinancieroUnidadPresupuestal").ToList();
            this.GridViewObraFinanciamiento.DataBind();

            this.GridViewTechoFinanciero.DataBind();
        }

        private void BindearDropDownList()
        {
            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            if (_Accion.Text.Equals("N"))
            {

                List<int> list_tfupIds = new List<int>();

                Obra obra = uow.ObraBusinessLogic.Get(o => o.POADetalleId == poadetalleId).FirstOrDefault();

                if (obra != null)
                {
                    foreach (var item in obra.DetalleFinanciamientos)
                    {
                        list_tfupIds.Add(item.TechoFinancieroUnidadPresupuestalId);
                    }
                }

                ddlTechoFinancieroUnidadPresupuestal.DataSource = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(tfup => tfup.UnidadPresupuestalId == unidadpresupuestalId & tfup.TechoFinanciero.EjercicioId == ejercicioId & !list_tfupIds.Contains(tfup.Id));

            }
            else 
            {

                ddlTechoFinancieroUnidadPresupuestal.DataSource = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(tfup => tfup.UnidadPresupuestalId == unidadpresupuestalId & tfup.TechoFinanciero.EjercicioId == ejercicioId);

            }
           
            //ddlTechoFinancieroUnidadPresupuestal.DataSource = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(tfup => tfup.UnidadPresupuestalId == unidadpresupuestalId & tfup.TechoFinanciero.EjercicioId == ejercicioId & !list_tfupIds.Contains(tfup.Id));
            ddlTechoFinancieroUnidadPresupuestal.DataValueField = "Id";
            ddlTechoFinancieroUnidadPresupuestal.DataTextField = "Descripcion";
            ddlTechoFinancieroUnidadPresupuestal.DataBind();

            ddlTechoFinancieroUnidadPresupuestal.Items.Insert(0, new ListItem("Seleccione...", "0"));

        }

        public void BindControles(ObraFinanciamiento obrafinanciamiento)
        {
            ddlTechoFinancieroUnidadPresupuestal.SelectedValue = obrafinanciamiento.TechoFinancieroUnidadPresupuestalId.ToString();
            txtImporte.Text = obrafinanciamiento.Importe.ToString();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            //Se limpian los controles

            _Accion.Text = "N";

            ddlTechoFinancieroUnidadPresupuestal.SelectedIndex = -1;
            txtImporte.Text = String.Empty;                       

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            _Accion.Text = "A";

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ID.Text = GridViewObraFinanciamiento.DataKeys[row.RowIndex].Values["Id"].ToString();

            currentId = Convert.ToInt32(GridViewObraFinanciamiento.DataKeys[row.RowIndex].Values["Id"].ToString());

            ObraFinanciamiento obrafinanciamiento = uow.ObraFinanciamientoBusinessLogic.GetByID(currentId);

            BindearDropDownList();
            BindControles(obrafinanciamiento);

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
           
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            string msg = "Se ha eliminado correctamente";

            int currentId = Utilerias.StrToInt(GridViewObraFinanciamiento.DataKeys[row.RowIndex].Value.ToString());

            ObraFinanciamiento obrafinanciamiento = uow.ObraFinanciamientoBusinessLogic.GetByID(currentId);


            uow.ObraFinanciamientoBusinessLogic.Delete(obrafinanciamiento);
            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");
                BindGrid();

            }
            else
            {

                divMsg.Style.Add("display", "block");

                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string msg = String.Empty;
                               
            poadetalleId = Utilerias.StrToInt(Request.QueryString["poadetalleId"].ToString());

            POADetalle poadetalle = uow.POADetalleBusinessLogic.GetByID(poadetalleId);

            Obra obra = uow.ObraBusinessLogic.Get(o=>o.POADetalleId==poadetalleId).FirstOrDefault();

            if (obra == null) 
            {
                obra = new Obra();
                obra.Numero = poadetalle.Numero;
                obra.Descripcion = poadetalle.Descripcion;
                obra.MunicipioId = poadetalle.MunicipioId;
                obra.Localidad = poadetalle.Localidad;
                obra.TipoLocalidadId = poadetalle.TipoLocalidadId;
                obra.CriterioPriorizacionId = poadetalle.CriterioPriorizacionId;            
                obra.AperturaProgramaticaId = poadetalle.AperturaProgramaticaId;
                obra.AperturaProgramaticaMetaId = poadetalle.AperturaProgramaticaMetaId;
                obra.NumeroBeneficiarios = poadetalle.NumeroBeneficiarios;
                obra.CantidadUnidades = poadetalle.CantidadUnidades;
                obra.Empleos = poadetalle.Empleos;
                obra.Jornales = poadetalle.Jornales;

                obra.FuncionalidadId = poadetalle.FuncionalidadId;
                obra.EjeId = poadetalle.EjeId;
                obra.PlanSectorialId = poadetalle.PlanSectorialId;
                obra.ModalidadId = poadetalle.ModalidadId;
                obra.ProgramaId = poadetalle.ProgramaId;
                obra.GrupoBeneficiarioId = poadetalle.GrupoBeneficiarioId;

                obra.SituacionObraId = poadetalle.SituacionObraId;
                obra.ModalidadObra = poadetalle.ModalidadObra;                
                obra.ImporteLiberadoEjerciciosAnteriores = 0;                
                obra.Observaciones = poadetalle.Observaciones;               

                obra.POAId = poadetalle.POAId;
                obra.POADetalleId = poadetalle.Id;

                uow.ObraBusinessLogic.Insert(obra);
            }


            //Superamos el techo financiero?

            int tfupId = Utilerias.StrToInt(ddlTechoFinancieroUnidadPresupuestal.SelectedValue);

            TechoFinancieroUnidadPresupuestal tfup = uow.TechoFinancieroUnidadPresuestalBusinessLogic.GetByID(tfupId);
            

            if (_Accion.Text.Equals("N"))
            {
                var ofinanciamiento = uow.ObraFinanciamientoBusinessLogic.Get(of => of.ObraId == obra.Id & of.TechoFinancieroUnidadPresupuestalId == tfupId).FirstOrDefault();

                if (ofinanciamiento != null)
                {
                    uow.Errors.Add("El importe para este fondo ya fue asignado, intente modificarlo");
                }
                else if (tfup.GetImporteAsignado() + Convert.ToDecimal(txtImporte.Text) > tfup.Importe)
                {
                    uow.Errors.Add("El importe que intenta asignar más el importe asignado (" + tfup.GetImporteAsignado(currentId).ToString("C2") + "),superan el techo financiero (" + tfup.Importe.ToString("c2") + ") para el fondo seleccionado.");
                }
            }
            else 
            {
                currentId = Convert.ToInt32(_ID.Text);

                if (tfup.GetImporteAsignado(currentId) + Convert.ToDecimal(txtImporte.Text) > tfup.Importe)
                {
                    uow.Errors.Add("El importe que intenta asignar más el importe asignado (" + tfup.GetImporteAsignado(currentId).ToString("C2") + "),superan el techo financiero (" + tfup.Importe.ToString("c2") + ") para el fondo seleccionado.");
                }
            }
            
    
           

            if (uow.Errors.Count > 0)
            {

                divMsg.Style.Add("display", "block");

                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;

                return;

            }





            ObraFinanciamiento obrafinanciamiento;


            if (_Accion.Text.Equals("N"))
            {
                obrafinanciamiento = new ObraFinanciamiento();
                obrafinanciamiento.TechoFinancieroUnidadPresupuestalId = Utilerias.StrToInt(ddlTechoFinancieroUnidadPresupuestal.SelectedValue);
                obrafinanciamiento.Importe = Convert.ToDecimal(txtImporte.Text);             

                obra.DetalleFinanciamientos.Add(obrafinanciamiento);
            }
            else
            {
                currentId = Convert.ToInt32(_ID.Text);
                obrafinanciamiento = uow.ObraFinanciamientoBusinessLogic.GetByID(currentId);
                obrafinanciamiento.TechoFinancieroUnidadPresupuestalId = Utilerias.StrToInt(ddlTechoFinancieroUnidadPresupuestal.SelectedValue);
                obrafinanciamiento.Importe = Convert.ToDecimal(txtImporte.Text); 
            }
               
            
            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {
                BindearDropDownList();
                BindGrid();
                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");
                divMsg.Style.Add("display", "none");
            }
            else 
            {
                divMsg.Style.Add("display", "block");

                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
            }            

        }

        // El tipo devuelto puede ser modificado a IEnumerable, sin embargo, para ser compatible con paginación y ordenación 
        // , se deben agregar los siguientes parametros:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<DataAccessLayer.Models.TechoFinancieroUnidadPresupuestal> GridViewTechoFinanciero_GetData()
        {

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            List<TechoFinanciero> listTechofinanciero = uow.TechoFinancieroBusinessLogic.Get(tf => tf.EjercicioId == ejercicioId).ToList();
            List<int> colIdsTechoFinanciero = new List<int>();

            listTechofinanciero.ForEach(item => colIdsTechoFinanciero.Add(item.Id));

            IQueryable<TechoFinancieroUnidadPresupuestal> source = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(tf => colIdsTechoFinanciero.Contains(tf.TechoFinancieroId) & tf.UnidadPresupuestalId == unidadpresupuestalId);

            return source;

        }

        
    }
}
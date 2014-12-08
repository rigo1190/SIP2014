using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class POA : System.Web.UI.Page
    {
        private UnitOfWork uow;      
        private int currentId;        
        private int unidadpresupuestalId;
        private int ejercicioId;        
        protected void Page_Load(object sender, EventArgs e)
        {
          
            uow = new UnitOfWork(Session["IdUser"].ToString());
            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());
               
            if (!IsPostBack)
            {  
                UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);
                
                lblTituloPOA.Text = String.Format("{0} <br /> Anteproyecto de POA para el ejercicio {1}",up.Nombre,uow.EjercicioBusinessLogic.GetByID(ejercicioId).Año);
                BindearDropDownList();               
            }
                        
        }      

        private void BindearDropDownList()
        {           

            //ddlTipoLocalidad.DataSource = uow.TipoLocalidadBusinessLogic.Get().ToList();
            //ddlTipoLocalidad.DataValueField = "Id";
            //ddlTipoLocalidad.DataTextField = "Nombre";
            //ddlTipoLocalidad.DataBind();

            //ddlTipoLocalidad.Items.Insert(0, new ListItem("Seleccione...", "0"));

            ddlUnidadMedida.DataSource = uow.AperturaProgramaticaUnidadBusinessLogic.Get().ToList();
            ddlUnidadMedida.DataValueField = "Id";
            ddlUnidadMedida.DataTextField = "Nombre";
            ddlUnidadMedida.DataBind();

            ddlUnidadMedida.Items.Insert(0,new ListItem("Seleccione...","0"));


            ddlCriterioPriorizacion.DataSource = uow.CriterioPriorizacionBusinessLogic.Get().OrderBy(cp => cp.Orden);
            ddlCriterioPriorizacion.DataValueField = "Id";
            ddlCriterioPriorizacion.DataTextField = "Nombre";
            ddlCriterioPriorizacion.DataBind();

            ddlCriterioPriorizacion.Items.Insert(0, new ListItem("Seleccione...", "0"));


            ddlSituacionObra.DataSource = uow.SituacionObraBusinessLogic.Get().ToList();
            ddlSituacionObra.DataValueField = "Id";
            ddlSituacionObra.DataTextField = "Nombre";
            ddlSituacionObra.DataBind();

            ddlSituacionObra.Items.Insert(0, new ListItem("Seleccione...", "0"));

            Utilerias.BindDropDownToEnum(ddlModalidad, typeof(enumModalidadObra));

            ddlEje.DataSource = uow.EjeBusinessLogic.Get(f => f.ParentId == null, orderBy: ap => ap.OrderBy(r => r.Orden));
            ddlEje.DataValueField = "Id";
            ddlEje.DataTextField = "Descripcion";
            ddlEje.DataBind();

            ddlEje.Items.Insert(0, new ListItem("Seleccione...", "0"));  

            ddlPlanSectorial.DataSource = uow.PlanSectorialBusinessLogic.Get(orderBy: ps => ps.OrderBy(o => o.Orden)).ToList();
            ddlPlanSectorial.DataValueField = "Id";
            ddlPlanSectorial.DataTextField = "Descripcion";
            ddlPlanSectorial.DataBind();

            ddlPlanSectorial.Items.Insert(0, new ListItem("Seleccione...", "0"));           

            ddlProgramaPresupuesto.DataSource = uow.ProgramaBusinessLogic.Get();
            ddlProgramaPresupuesto.DataValueField = "Id";
            ddlProgramaPresupuesto.DataTextField = "Descripcion";
            ddlProgramaPresupuesto.DataBind();

            ddlProgramaPresupuesto.Items.Insert(0, new ListItem("Seleccione...", "0"));

            ddlGrupoBeneficiario.DataSource = uow.GrupoBeneficiarioBusinessLogic.Get();
            ddlGrupoBeneficiario.DataValueField = "Id";
            ddlGrupoBeneficiario.DataTextField = "Nombre";
            ddlGrupoBeneficiario.DataBind();

            ddlGrupoBeneficiario.Items.Insert(0, new ListItem("Seleccione...", "0"));

        }              
        
        public void BindControles(POADetalle poadetalle)
        {
            txtNumero.Value = poadetalle.Numero;
            txtDescripcion.Value = poadetalle.Descripcion;
            cddlMunicipio.SelectedValue = poadetalle.MunicipioId.ToString();
            cddlLocalidad.SelectedValue = poadetalle.LocalidadId.ToString();           

            if (poadetalle.AperturaProgramaticaUnidadId != null) 
            {
                ddlUnidadMedida.SelectedValue = poadetalle.AperturaProgramaticaUnidadId.ToString();
            }
                       
            ddlCriterioPriorizacion.SelectedValue = poadetalle.CriterioPriorizacionId.ToString();
            txtNombreConvenio.Value = poadetalle.Convenio;

            cddlPrograma.SelectedValue = poadetalle.AperturaProgramatica.Parent.ParentId.ToString();
            cddlSubprograma.SelectedValue = poadetalle.AperturaProgramatica.ParentId.ToString();
            cddlSubsubprograma.SelectedValue = poadetalle.AperturaProgramaticaId.ToString();
            cddlMeta.SelectedValue = poadetalle.AperturaProgramaticaMetaId.ToString();
                                 
                    
            txtNumeroBeneficiarios.Value = poadetalle.NumeroBeneficiarios.ToString();
            txtCantidadUnidades.Value = poadetalle.CantidadUnidades.ToString();
            txtEmpleos.Value = poadetalle.Empleos.ToString();
            txtJornales.Value = poadetalle.Jornales.ToString();
            ddlSituacionObra.SelectedValue = poadetalle.SituacionObraId.ToString();

            ddlModalidad.SelectedIndex = 0;

            if (poadetalle.ModalidadObra != null) 
            {
                ddlModalidad.SelectedValue = ((int)poadetalle.ModalidadObra).ToString();
            }
            
            txtNumeroAnterior.Value = poadetalle.NumeroAnterior;
            txtImporteLiberadoEjerciciosAnteriores.Value= poadetalle.ImporteLiberadoEjerciciosAnteriores.ToString();
            txtImporteTotal.Value = poadetalle.ImporteTotal.ToString();                    
            txtObservaciones.Value = poadetalle.Observaciones;

            if (poadetalle.FuncionalidadId != null) 
            {
                cddlFuncionalidadNivel1.SelectedValue = poadetalle.Funcionalidad.Parent.ParentId.ToString();
                cddlFuncionalidadNivel2.SelectedValue = poadetalle.Funcionalidad.ParentId.ToString();
                cddlFuncionalidadNivel3.SelectedValue = poadetalle.FuncionalidadId.ToString();
            }

            if (poadetalle.EjeId != null) 
            {
                //cddlEjePVD1.SelectedValue = poadetalle.Eje.ParentId.ToString();
                //cddlEjePVD2.SelectedValue = poadetalle.EjeId.ToString();
                ddlEje.SelectedValue = poadetalle.EjeId.ToString();
            }

            if (poadetalle.ModalidadId != null) 
            {
                cddlModalidadAgrupador.SelectedValue = poadetalle.Modalidad.ParentId.ToString();
                cddlModalidadElemento.SelectedValue = poadetalle.ModalidadId.ToString();
            }

            if (poadetalle.PlanSectorialId != null) 
            {
                ddlPlanSectorial.SelectedValue = poadetalle.PlanSectorialId.ToString();
            }

            if (poadetalle.ProgramaId != null) 
            {
                ddlProgramaPresupuesto.SelectedValue = poadetalle.ProgramaId.ToString();
            }

            if (poadetalle.GrupoBeneficiarioId != null) 
            {
                ddlGrupoBeneficiario.SelectedValue = poadetalle.GrupoBeneficiarioId.ToString();
            }          
            
        }
     
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            //Se limpian los controles

            txtNumero.Value = String.Empty;
            txtDescripcion.Value = String.Empty;
            cddlMunicipio.SelectedValue = String.Empty;

            ddlCriterioPriorizacion.SelectedIndex = -1;
            txtNombreConvenio.Value = String.Empty;

            cddlLocalidad.SelectedValue = String.Empty;                 
         
            cddlPrograma.SelectedValue = String.Empty;
            cddlSubprograma.SelectedValue = String.Empty;
            cddlSubsubprograma.SelectedValue = String.Empty;
            cddlMeta.SelectedValue = String.Empty;

            txtNumeroBeneficiarios.Value = String.Empty;
            txtCantidadUnidades.Value = String.Empty;
            txtEmpleos.Value = String.Empty;
            txtJornales.Value = String.Empty;
            ddlSituacionObra.SelectedIndex = -1;
            ddlModalidad.SelectedIndex = -1;
            txtImporteTotal.Value = String.Empty;
            txtNumeroAnterior.Value = String.Empty;
            txtImporteLiberadoEjerciciosAnteriores.Value = String.Empty;
           

            txtObservaciones.Value = String.Empty;                      

            cddlFuncionalidadNivel1.SelectedValue = String.Empty;
            cddlFuncionalidadNivel2.SelectedValue = String.Empty;
            cddlFuncionalidadNivel3.SelectedValue = String.Empty;

            //cddlEjePVD1.SelectedValue = String.Empty;
            //cddlEjePVD2.SelectedValue = String.Empty; 
            ddlEje.SelectedIndex = -1;          

            ddlPlanSectorial.SelectedIndex = -1;

            cddlModalidadAgrupador.SelectedValue = String.Empty;
            cddlModalidadElemento.SelectedValue = String.Empty;           

            ddlProgramaPresupuesto.SelectedIndex = -1;
            ddlGrupoBeneficiario.SelectedIndex = -1;

            HabilitarDesHabilitarControles(true);

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            _Accion.Text = "N";

        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ID.Text = GridViewObras.DataKeys[row.RowIndex].Values["Id"].ToString();

            currentId = Convert.ToInt32(GridViewObras.DataKeys[row.RowIndex].Values["Id"].ToString());

            POADetalle poadetalle = uow.POADetalleBusinessLogic.GetByID(currentId);
            
            BindControles(poadetalle);

            Obra obra = uow.ObraBusinessLogic.Get(o => o.POADetalleId == poadetalle.Id).FirstOrDefault();

            if (obra == null)
            {
                HabilitarDesHabilitarControles(true);
            }
            else 
            {
                HabilitarDesHabilitarControles(false);
            }


            divEdicion.Style.Add("display", "block");           
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            _Accion.Text = "A";
           
            ClientScript.RegisterStartupScript(this.GetType(), "script01", "fnc_ocultarDivObraAnterior();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "script02", "fnc_ocultarDivDatosConvenio();", true);
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            //Se busca l ID de la fila seleccionada
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            string msg = "Se ha eliminado correctamente";

            currentId = Utilerias.StrToInt(GridViewObras.DataKeys[row.RowIndex].Value.ToString());

            POADetalle poadetalle = uow.POADetalleBusinessLogic.GetByID(currentId);

            
            uow.POADetalleBusinessLogic.Delete(poadetalle);
            uow.SaveChanges();

            if (uow.Errors.Count==0) 
            {                

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");
                //BindGrid();
               
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
            string msg = "Se ha guardado correctamente";          
            

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();
            POADetalle poadetalle = null;

            if (poa == null) 
            {
                poa = new DataAccessLayer.Models.POA();
                poa.UnidadPresupuestalId = unidadpresupuestalId;
                poa.EjercicioId = ejercicioId;
            }
      
                       

            if (_Accion.Text.Equals("N"))
                poadetalle = new POADetalle();
            else
            {
                currentId = Convert.ToInt32(_ID.Text);
                poadetalle = uow.POADetalleBusinessLogic.GetByID(currentId);
                msg = "Se ha actualizado correctamente";
            }
                       
            poadetalle.Numero = txtNumero.Value;
            poadetalle.Descripcion = txtDescripcion.Value;
            poadetalle.MunicipioId = Utilerias.StrToInt(ddlMunicipio.SelectedValue);
            poadetalle.LocalidadId = Utilerias.StrToInt(ddlLocalidad.SelectedValue);            

            poadetalle.CriterioPriorizacionId = Utilerias.StrToInt(ddlCriterioPriorizacion.SelectedValue);
            poadetalle.Convenio = txtNombreConvenio.Value;

            poadetalle.AperturaProgramaticaId = Utilerias.StrToInt(ddlSubsubprograma.SelectedValue);
            poadetalle.AperturaProgramaticaMetaId = null;
            poadetalle.NumeroBeneficiarios =Utilerias.StrToInt(txtNumeroBeneficiarios.Value.ToString());
            poadetalle.CantidadUnidades = Utilerias.StrToInt(txtCantidadUnidades.Value.ToString());
            poadetalle.Empleos = Utilerias.StrToInt(txtEmpleos.Value.ToString());
            poadetalle.Jornales = Utilerias.StrToInt(txtJornales.Value.ToString());

            poadetalle.FuncionalidadId = Utilerias.StrToInt(ddlSubFuncion.SelectedValue);
            //poadetalle.EjeId = Utilerias.StrToInt(ddlEjeElemento.SelectedValue);
            poadetalle.EjeId = Utilerias.StrToInt(ddlEje.SelectedValue);
            poadetalle.PlanSectorialId = Utilerias.StrToInt(ddlPlanSectorial.SelectedValue);
            poadetalle.ModalidadId = Utilerias.StrToInt(ddlModalidadElemento.SelectedValue);
            poadetalle.ProgramaId = Utilerias.StrToInt(ddlProgramaPresupuesto.SelectedValue);
            poadetalle.GrupoBeneficiarioId = Utilerias.StrToInt(ddlGrupoBeneficiario.SelectedValue);


            poadetalle.SituacionObraId = Utilerias.StrToInt(ddlSituacionObra.SelectedValue);
            poadetalle.ModalidadObra = (enumModalidadObra)Convert.ToInt32(ddlModalidad.SelectedValue);
            poadetalle.ImporteTotal = Convert.ToDecimal(txtImporteTotal.Value.ToString());
            poadetalle.NumeroAnterior = txtNumeroAnterior.Value;
            poadetalle.ImporteLiberadoEjerciciosAnteriores = Utilerias.StrToDecimal(txtImporteLiberadoEjerciciosAnteriores.Value.ToString());
            poadetalle.Observaciones = txtObservaciones.InnerText;
            poadetalle.Extemporanea = false;

            if (_Accion.Text.Equals("N")) 
            {
                poadetalle.POA = poa;
                uow.POADetalleBusinessLogic.Insert(poadetalle);
            }               
            else
            {
                uow.POADetalleBusinessLogic.Update(poadetalle);
            }

            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {

                // Esto solo es necesario para recargar en memoria
                // los cambios que se realizan mediante un trigger
                uow = null;
                uow = new UnitOfWork();

                //Este gridview usa un enlace de Modelo, por esto No requiere 
                //asignar nuevamente su propiedad Datasource
                //vease método GridViewObras_GetData
                this.GridViewObras.DataBind();


                divEdicion.Style.Add("display", "none");                
                divBtnNuevo.Style.Add("display", "block");       
                              
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

		protected void GridViewObras_RowDataBound(object sender, GridViewRowEventArgs e)
        {          

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlButton btnE = (HtmlButton)e.Row.FindControl("btnFinanciamiento");
                if (btnE != null)
                {
                    if (GridViewObras.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                    {
                        string url = string.Empty;
                        url = "AsignarFinanciamientoPOA.aspx?poadetalleId=" + GridViewObras.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
                        //btnE.Attributes.Add("onclick", "fnc_IrDesdeGrid('" + url + "')");
                        btnE.Attributes.Add("data-url-poa", url);

                        int poadetalleId = Utilerias.StrToInt(GridViewObras.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                        Obra obra = uow.ObraBusinessLogic.Get(o => o.POADetalleId == poadetalleId).FirstOrDefault();

                        if (obra != null) 
                        {
                            btnE.Attributes.Add("disabled", "disabled");
                        }
                        
                    }
                    
                }
               
            }
        }

        private void HabilitarDesHabilitarControles(bool habilitar)
        {

            if (habilitar)
            {
                txtDescripcion.Disabled = false;
                ddlMunicipio.Enabled = true;
                ddlLocalidad.Enabled = true;                
                ddlCriterioPriorizacion.Enabled = true;
                txtNombreConvenio.Disabled = false;

                ddlPrograma.Enabled = true;
                ddlSubprograma.Enabled = true;
                ddlSubsubprograma.Enabled = true;
                ddlMeta.Enabled = true;
                txtNumeroBeneficiarios.Disabled = false;
                txtCantidadUnidades.Disabled = false;
                txtEmpleos.Disabled = false;
                txtJornales.Disabled = false;

                ddlSituacionObra.Enabled = true;
                ddlModalidad.Enabled = true;
                txtImporteTotal.Disabled = false;
                txtImporteLiberadoEjerciciosAnteriores.Disabled = false;
                txtNumeroAnterior.Disabled = false;
                txtObservaciones.Disabled = false;

                ddlFinalidad.Enabled = true;
                ddlFuncion.Enabled = true;
                ddlSubFuncion.Enabled = true;

                //ddlEjeAgrupador.Enabled = true;
                //ddlEjeElemento.Enabled = true;
                ddlEje.Enabled = true;

                ddlPlanSectorial.Enabled = true;
                ddlModalidadAgrupador.Enabled = true;
                ddlModalidadElemento.Enabled = true;

                ddlProgramaPresupuesto.Enabled = true;
                ddlGrupoBeneficiario.Enabled = true;

                btnGuardar.Enabled = true;
            }
            else 
            {
                txtDescripcion.Disabled = true;
                ddlMunicipio.Enabled = false;
                ddlLocalidad.Enabled = false;                
                ddlCriterioPriorizacion.Enabled = false;
                txtNombreConvenio.Disabled = true;

                ddlPrograma.Enabled = false;
                ddlSubprograma.Enabled = false;
                ddlSubsubprograma.Enabled = false;
                ddlMeta.Enabled = false;
                txtNumeroBeneficiarios.Disabled = true;
                txtCantidadUnidades.Disabled = true;
                txtEmpleos.Disabled = true;
                txtJornales.Disabled = true;

                ddlSituacionObra.Enabled = false;
                ddlModalidad.Enabled = false;
                txtImporteTotal.Disabled = true;
                txtImporteLiberadoEjerciciosAnteriores.Disabled = true;
                txtNumeroAnterior.Disabled = true;
                txtObservaciones.Disabled = true;

                ddlFinalidad.Enabled = false;
                ddlFuncion.Enabled = false;
                ddlSubFuncion.Enabled = false;

                //ddlEjeAgrupador.Enabled = false;
                //ddlEjeElemento.Enabled = false;
                ddlEje.Enabled = false;

                ddlPlanSectorial.Enabled = false;
                ddlModalidadAgrupador.Enabled = false;
                ddlModalidadElemento.Enabled = false;

                ddlProgramaPresupuesto.Enabled = false;
                ddlGrupoBeneficiario.Enabled = false;

                btnGuardar.Enabled = false;
            }
           
        }

        protected void GridViewObras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView grid = sender as GridView;
            grid.PageIndex = e.NewPageIndex;           
           
            divMsg.Style.Add("display", "none");
            divEdicion.Style.Add("display", "none");
            divBtnNuevo.Style.Add("display", "block");

        }
      
        public IQueryable<POADetalle> GridViewObras_GetData()
        {
            IQueryable<POADetalle> list = null;          

            list = uow.POADetalleBusinessLogic.Get(pd => pd.POA.UnidadPresupuestalId == unidadpresupuestalId & pd.POA.EjercicioId == ejercicioId & pd.Extemporanea == false, orderBy: r => r.OrderBy(ro => ro.Consecutivo));
            
            return list;
        }

        

    }      
    

}
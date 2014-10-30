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
               
            if (!IsPostBack)
            {
                unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
                ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

                UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);
                
                lblTituloPOA.Text = String.Format("{0} <br /> Anteproyecto de POA para el ejercicio {1}",up.Nombre,uow.EjercicioBusinessLogic.GetByID(ejercicioId).Año);
                BindGrid();
                BindearDropDownList();
               
            }
            
        }
       
        private void BindGrid()
        {            

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            this.GridViewObras.DataSource = uow.POADetalleBusinessLogic.Get(pd => pd.POA.UnidadPresupuestalId == unidadpresupuestalId & pd.POA.EjercicioId == ejercicioId & pd.Extemporanea==false,orderBy:r=>r.OrderBy(ro=>ro.Numero)).ToList();
            this.GridViewObras.DataBind();            
                        
        }

        private void BindearDropDownList()
        {

            ddlMunicipio.DataSource = uow.MunicipioBusinessLogic.Get().ToList();
            ddlMunicipio.DataValueField = "Id";
            ddlMunicipio.DataTextField = "Nombre";
            ddlMunicipio.DataBind();

            ddlMunicipio.Items.Insert(0, new ListItem("Seleccione...", "0"));

            ddlTipoLocalidad.DataSource = uow.TipoLocalidadBusinessLogic.Get().ToList();
            ddlTipoLocalidad.DataValueField = "Id";
            ddlTipoLocalidad.DataTextField = "Nombre";
            ddlTipoLocalidad.DataBind();

            ddlTipoLocalidad.Items.Insert(0, new ListItem("Seleccione...", "0"));

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

            ddlPlanSectorial.DataSource = uow.PlanSectorialBusinessLogic.Get(orderBy: ps => ps.OrderBy(o => o.Orden)).ToList();
            ddlPlanSectorial.DataValueField = "Id";
            ddlPlanSectorial.DataTextField = "Descripcion";
            ddlPlanSectorial.DataBind();

            ddlPlanSectorial.Items.Insert(0, new ListItem("Seleccione...", "0"));

            //ddlClasificacionProgramaticaCONAC.DataSource = uow.ModalidadBusinessLogic.Get(m => m.ParentId == null).ToList();
            //ddlClasificacionProgramaticaCONAC.DataValueField = "Id";
            //ddlClasificacionProgramaticaCONAC.DataTextField = "Descripcion";
            //ddlClasificacionProgramaticaCONAC.DataBind();

            //ddlClasificacionProgramaticaCONAC.Items.Insert(0, new ListItem("Seleccione...", "0"));

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
            ddlMunicipio.SelectedValue = poadetalle.MunicipioId.ToString();
            ddlTipoLocalidad.SelectedValue = poadetalle.TipoLocalidadId.ToString();
            ddlCriterioPriorizacion.SelectedValue = poadetalle.CriterioPriorizacionId.ToString();

            cddlPrograma.SelectedValue = poadetalle.AperturaProgramatica.Parent.ParentId.ToString();
            cddlSubprograma.SelectedValue = poadetalle.AperturaProgramatica.ParentId.ToString();
            cddlSubsubprograma.SelectedValue = poadetalle.AperturaProgramaticaId.ToString();
            cddlMeta.SelectedValue = poadetalle.AperturaProgramaticaMetaId.ToString();
                                 
            txtLocalidad.Value = poadetalle.Localidad;            
            txtNumeroBeneficiarios.Value = poadetalle.NumeroBeneficiarios.ToString();
            txtCantidadUnidades.Value = poadetalle.CantidadUnidades.ToString();
            txtEmpleos.Value = poadetalle.Empleos.ToString();
            txtJornales.Value = poadetalle.Jornales.ToString();
            ddlSituacionObra.SelectedValue = poadetalle.SituacionObraId.ToString();
            ddlModalidad.SelectedValue = ((int)poadetalle.ModalidadObra).ToString();
            txtImporteTotal.Value = poadetalle.ImporteTotal.ToString();
            txtCostoLiberadoEjerciciosAnteriores.Value = poadetalle.ImporteLiberadoEjerciciosAnteriores.ToString();
            txtPresupuestoEjercicio.Value = poadetalle.ImportePresupuesto.ToString();
            txtObservaciones.Value = poadetalle.Observaciones;
           
            cddlFuncionalidadNivel1.SelectedValue = poadetalle.Funcionalidad.Parent.ParentId.ToString();
            cddlFuncionalidadNivel2.SelectedValue = poadetalle.Funcionalidad.ParentId.ToString();
            cddlFuncionalidadNivel3.SelectedValue = poadetalle.FuncionalidadId.ToString();

            cddlEjePVD1.SelectedValue = poadetalle.Eje.ParentId.ToString();
            cddlEjePVD2.SelectedValue = poadetalle.EjeId.ToString();

            cddlModalidadAgrupador.SelectedValue = poadetalle.Modalidad.ParentId.ToString();
            cddlModalidadElemento.SelectedValue = poadetalle.ModalidadId.ToString();
            
            ddlPlanSectorial.SelectedValue = poadetalle.PlanSectorialId.ToString();
            ddlProgramaPresupuesto.SelectedValue = poadetalle.ProgramaId.ToString();
            ddlGrupoBeneficiario.SelectedValue = poadetalle.GrupoBeneficiarioId.ToString();
            
            
        }
     
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            //Se limpian los controles

            txtNumero.Value = String.Empty;
            txtDescripcion.Value = String.Empty;
            ddlMunicipio.SelectedIndex = -1;
            ddlCriterioPriorizacion.SelectedIndex = -1;
            txtLocalidad.Value = String.Empty;
            ddlTipoLocalidad.SelectedIndex = -1;          
         
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
            txtCostoLiberadoEjerciciosAnteriores.Value = String.Empty;
            txtPresupuestoEjercicio.Value = String.Empty;

            txtObservaciones.Value = String.Empty;                      

            cddlFuncionalidadNivel1.SelectedValue = String.Empty;
            cddlFuncionalidadNivel2.SelectedValue = String.Empty;
            cddlFuncionalidadNivel3.SelectedValue = String.Empty;

            cddlEjePVD1.SelectedValue = String.Empty;
            cddlEjePVD2.SelectedValue = String.Empty;                       

            ddlPlanSectorial.SelectedIndex = -1;

            cddlModalidadAgrupador.SelectedValue = String.Empty;
            cddlModalidadElemento.SelectedValue = String.Empty;           

            ddlProgramaPresupuesto.SelectedIndex = -1;
            ddlGrupoBeneficiario.SelectedIndex = -1;

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

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            _Accion.Text = "A";
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
            poadetalle.Localidad = txtLocalidad.Value;
            poadetalle.TipoLocalidadId = Utilerias.StrToInt(ddlTipoLocalidad.SelectedValue);
            poadetalle.CriterioPriorizacionId = Utilerias.StrToInt(ddlCriterioPriorizacion.SelectedValue);            
            poadetalle.AperturaProgramaticaId = Utilerias.StrToInt(ddlSubsubprograma.SelectedValue);
            poadetalle.AperturaProgramaticaMetaId = Utilerias.StrToInt(ddlMeta.SelectedValue);
            poadetalle.NumeroBeneficiarios =Utilerias.StrToInt(txtNumeroBeneficiarios.Value.ToString());
            poadetalle.CantidadUnidades = Utilerias.StrToInt(txtCantidadUnidades.Value.ToString());
            poadetalle.Empleos = Utilerias.StrToInt(txtEmpleos.Value.ToString());
            poadetalle.Jornales = Utilerias.StrToInt(txtJornales.Value.ToString());

            poadetalle.FuncionalidadId = Utilerias.StrToInt(ddlSubFuncion.SelectedValue);
            poadetalle.EjeId = Utilerias.StrToInt(ddlEjeElemento.SelectedValue);
            poadetalle.PlanSectorialId = Utilerias.StrToInt(ddlPlanSectorial.SelectedValue);
            poadetalle.ModalidadId = Utilerias.StrToInt(ddlModalidadElemento.SelectedValue);
            poadetalle.ProgramaId = Utilerias.StrToInt(ddlProgramaPresupuesto.SelectedValue);
            poadetalle.GrupoBeneficiarioId = Utilerias.StrToInt(ddlGrupoBeneficiario.SelectedValue);


            poadetalle.SituacionObraId = Utilerias.StrToInt(ddlSituacionObra.SelectedValue);
            poadetalle.ModalidadObra = (enumModalidadObra)Convert.ToInt32(ddlModalidad.SelectedValue);
            poadetalle.ImporteTotal = Convert.ToDecimal(txtImporteTotal.Value.ToString());   
            poadetalle.ImporteLiberadoEjerciciosAnteriores = Convert.ToDecimal(txtCostoLiberadoEjerciciosAnteriores.Value.ToString());
            poadetalle.ImportePresupuesto = Convert.ToDecimal(txtPresupuestoEjercicio.Value.ToString());            
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

                BindGrid();  
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
                    }
                    
                }
               
            }
        }

        private void InsertarInformacionEvaluaciones()         
        {
            string url = string.Empty;

            List<Plantilla> list = uow.PlantillaBusinessLogic.Get(e => e.DependeDeId == null).ToList();

                                  
            foreach (GridViewRow row in GridViewObras.Rows) 
            {
                string id = GridViewObras.DataKeys[row.RowIndex].Values["Id"].ToString(); 

                if (list.Count > 0)
                {
                    TableCell cell1 = new TableCell();

                    foreach (Plantilla p in list)
                    {
                        
                        url = "EvaluacionPOA.aspx?p=" + id + "&o=";

                        HtmlButton button = new HtmlButton();
                        HtmlGenericControl spanButton = new HtmlGenericControl("span");

                        //Se construye el BOTON
                        button.ID = "btn" + p.Orden;
                        button.Attributes.Add("class", "btn btn-default");
                        button.Attributes.Add("data-tipo-operacion", "evaluar");
                        button.Attributes.Add("runat", "server");
                        url += p.Orden.ToString(); //SE AGREGA PARAMETRO DE ORDEN a la URL
                        button.Attributes.Add("data-url-poa", url);

                        spanButton.Attributes.Add("class", "glyphicon glyphicon-ok");
                        button.Controls.Add(spanButton);

                        cell1.Controls.Add(button);                        
                    }

                    row.Cells.AddAt(3, cell1);

                }

                row.Cells.RemoveAt(row.Cells.Count - 1);
      

                TableCell celdafinanciamiento = new TableCell();
                LinkButton btnfinanciamiento = new LinkButton();
                btnfinanciamiento.Attributes.Add("class", "btn btn-default glyphicon glyphicon-usd");
                btnfinanciamiento.Attributes.Add("data-tipo-operacion", "asignarfinanciamiento");                
                //btnfinanciamiento.Attributes.Add("runat", "server");
                url = "AsignarFinanciamientoPOA.aspx?poadetalleId=" + GridViewObras.DataKeys[row.RowIndex].Values["Id"].ToString();
                btnfinanciamiento.Attributes.Add("data-url-poa", url);               

                celdafinanciamiento.Controls.Add(btnfinanciamiento);

                row.Cells.AddAt(4, celdafinanciamiento);
                
                row.Cells.RemoveAt(row.Cells.Count - 1);
            }                       


        }
                              
    }      
    

}
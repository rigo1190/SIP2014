using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class frmPOAAjustado : System.Web.UI.Page
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
                
                int columnsInicial = GridViewObras.Columns.Count;

                UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);
                Ejercicio ejercicio = uow.EjercicioBusinessLogic.GetByID(ejercicioId); 

                lblTituloPOA.Text = String.Format("{0}<br />Proyecto de POA ajustado en el ejercicio {1}",up.Nombre,ejercicio.Año);
                
                BindearDropDownList();
              
            }

        }


        public void BindControles(Obra obra)
        {
            //restablecer previamente controles dropdownlist

            cddlFuncionalidadNivel1.SelectedValue = String.Empty;
            ddlEje.SelectedIndex = -1;



            txtNumero.Value = obra.Numero;
            txtDescripcion.Value = obra.Descripcion;
            cddlMunicipio.SelectedValue = obra.MunicipioId.ToString();
            cddlLocalidad.SelectedValue = obra.LocalidadId.ToString();


            if (obra.AperturaProgramaticaUnidadId != null)
            {
                ddlUnidadMedida.SelectedValue = obra.AperturaProgramaticaUnidadId.ToString();
            }

            ddlCriterioPriorizacion.SelectedValue = obra.CriterioPriorizacionId.ToString();
            txtNombreConvenio.Value = obra.Convenio;

            cddlPrograma.SelectedValue = obra.AperturaProgramatica.Parent.ParentId.ToString();
            cddlSubprograma.SelectedValue = obra.AperturaProgramatica.ParentId.ToString();
            cddlSubsubprograma.SelectedValue = obra.AperturaProgramaticaId.ToString();
            cddlMeta.SelectedValue = obra.AperturaProgramaticaMetaId.ToString();
                       

            txtFechaInicio.Value =String.Format("{0:d}",obra.FechaInicio);
            txtFechaTermino.Value = String.Format("{0:d}", obra.FechaTermino);

            txtNumeroBeneficiarios.Value = obra.NumeroBeneficiarios.ToString();
            txtCantidadUnidades.Value = obra.CantidadUnidades.ToString();
            txtEmpleos.Value = obra.Empleos.ToString();
            txtJornales.Value = obra.Jornales.ToString();
            ddlSituacionObra.SelectedValue = obra.SituacionObraId.ToString();

            ddlModalidad.SelectedIndex = 0;

            if (obra.ModalidadObra != null)
            {
                ddlModalidad.SelectedValue = ((int)obra.ModalidadObra).ToString();
            }
                        
            txtImporteTotal.Value = obra.GetCostoTotal().ToString();
            txtNumeroAnterior.Value = obra.NumeroAnterior;
            txtImporteLiberadoEjerciciosAnteriores.Value = obra.GetImporteLiberadoEjerciciosAnteriores().ToString();
            txtPresupuestoEjercicio.Value = obra.GetImporteAsignado().ToString();
            txtObservaciones.Value = obra.Observaciones;

            if (obra.FuncionalidadId != null) 
            {
                cddlFuncionalidadNivel1.SelectedValue = obra.Funcionalidad.Parent.ParentId.ToString();
                cddlFuncionalidadNivel2.SelectedValue = obra.Funcionalidad.ParentId.ToString();
                cddlFuncionalidadNivel3.SelectedValue = obra.FuncionalidadId.ToString();
            }

            if (obra.EjeId != null)
            {
                ddlEje.SelectedValue = obra.EjeId.ToString();
            }

            if (obra.ModalidadId != null) 
            {
                cddlModalidadAgrupador.SelectedValue = obra.Modalidad.ParentId.ToString();
                cddlModalidadElemento.SelectedValue = obra.ModalidadId.ToString();
            }

            if (obra.PlanSectorialId != null) 
            {
                ddlPlanSectorial.SelectedValue = obra.PlanSectorialId.ToString();
            }

            if (obra.ProgramaId != null) 
            {
                ddlProgramaPresupuesto.SelectedValue = obra.ProgramaId.ToString();
            }

            if (obra.GrupoBeneficiarioId != null) 
            {
                ddlGrupoBeneficiario.SelectedValue = obra.GrupoBeneficiarioId.ToString();
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

            txtFechaInicio.Value = String.Empty;
            txtFechaTermino.Value = String.Empty;

            cddlPrograma.SelectedValue = String.Empty;
            cddlSubprograma.SelectedValue = String.Empty;
            cddlSubsubprograma.SelectedValue = String.Empty;
            cddlMeta.SelectedValue = String.Empty;

            ddlUnidadMedida.SelectedIndex = -1;

            txtNumeroBeneficiarios.Value = String.Empty;
            txtCantidadUnidades.Value = String.Empty;
            txtEmpleos.Value = String.Empty;
            txtJornales.Value = String.Empty;
            ddlSituacionObra.SelectedIndex = -1;
            ddlModalidad.SelectedIndex = -1;
            txtImporteTotal.Value = String.Empty;
            txtImporteLiberadoEjerciciosAnteriores.Value = String.Empty;
            txtPresupuestoEjercicio.Value = String.Empty;

            txtObservaciones.Value = String.Empty;

            cddlFuncionalidadNivel1.SelectedValue = String.Empty;
            cddlFuncionalidadNivel2.SelectedValue = String.Empty;
            cddlFuncionalidadNivel3.SelectedValue = String.Empty;


            ddlPlanSectorial.SelectedIndex = -1;

            cddlModalidadAgrupador.SelectedValue = String.Empty;
            cddlModalidadElemento.SelectedValue = String.Empty;

            ddlProgramaPresupuesto.SelectedIndex = -1;
            ddlGrupoBeneficiario.SelectedIndex = -1;

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            _Accion.Text = "N";

            ClientScript.RegisterStartupScript(this.GetType(), "script04", "fnc_DeshabilitarSituacionObra(true);", true);

        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ID.Text = GridViewObras.DataKeys[row.RowIndex].Values["Id"].ToString();

            currentId = Convert.ToInt32(GridViewObras.DataKeys[row.RowIndex].Values["Id"].ToString());

            Obra obra = uow.ObraBusinessLogic.GetByID(currentId);

            BindControles(obra);

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            _Accion.Text = "A";

            ClientScript.RegisterStartupScript(this.GetType(), "script01", "fnc_ocultarDivDatosConvenio();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "script02", "fnc_ocultarDivObraAnterior();", true);
            ClientScript.RegisterStartupScript(this.GetType(), "script03", "fnc_RecuperarValoresEnCamposDeshabilitados();", true);

        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            //Se busca l ID de la fila seleccionada
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            string msg = "Se ha eliminado correctamente";

            currentId = Utilerias.StrToInt(GridViewObras.DataKeys[row.RowIndex].Value.ToString());

            Obra obra = uow.ObraBusinessLogic.GetByID(currentId);


            uow.ObraBusinessLogic.Delete(obra);
            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

                this.GridViewObras.DataBind();
            }
            else
            {

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");
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
            Obra obra = null;

            if (poa == null)
            {
                poa = new DataAccessLayer.Models.POA();
                poa.UnidadPresupuestalId = unidadpresupuestalId;
                poa.EjercicioId = ejercicioId;
            }



            if (_Accion.Text.Equals("N"))
            {                
                obra = new Obra();
            }
            else
            {
                currentId = Convert.ToInt32(_ID.Text);
                obra = uow.ObraBusinessLogic.GetByID(currentId);
                msg = "Se ha actualizado correctamente";
            }

            obra.Numero = txtNumero.Value;
            obra.Descripcion = txtDescripcion.Value;
            obra.MunicipioId = Utilerias.StrToInt(ddlMunicipio.SelectedValue);
            obra.LocalidadId = Utilerias.StrToInt(ddlLocalidad.SelectedValue);            
            obra.CriterioPriorizacionId = Utilerias.StrToInt(ddlCriterioPriorizacion.SelectedValue);

            //Garantizar que se limpie correctamente el campo Nombre del Convenio

            switch (obra.CriterioPriorizacionId)
            {
                case 2:

                    break;

                default:

                    txtNombreConvenio.Value = String.Empty;
                    break;
            }


            obra.Convenio = txtNombreConvenio.Value;
            obra.AperturaProgramaticaId = Utilerias.StrToInt(ddlSubsubprograma.SelectedValue);
            obra.AperturaProgramaticaMetaId = null;
            obra.AperturaProgramaticaUnidadId = Utilerias.StrToInt(ddlUnidadMedida.SelectedValue);
            obra.NumeroBeneficiarios = Utilerias.StrToInt(txtNumeroBeneficiarios.Value.ToString().Replace(",",null));
            obra.CantidadUnidades = Utilerias.StrToInt(txtCantidadUnidades.Value.ToString().Replace(",", null));
            obra.Empleos = Utilerias.StrToInt(txtEmpleos.Value.ToString().Replace(",", null));
            obra.Jornales = Utilerias.StrToInt(txtJornales.Value.ToString().Replace(",", null));

            obra.FuncionalidadId = null;
            obra.EjeId = null;
            obra.PlanSectorialId = null;
            obra.ModalidadId = null;
            obra.ProgramaId = null;
            obra.GrupoBeneficiarioId = null;


            if (ddlSubFuncion.SelectedIndex > 0) 
            {
                obra.FuncionalidadId = Utilerias.StrToInt(ddlSubFuncion.SelectedValue);
            }

            if (ddlEje.SelectedIndex > 0) 
            {
                obra.EjeId = Utilerias.StrToInt(ddlEje.SelectedValue);
            }

            if (ddlPlanSectorial.SelectedIndex > 0) 
            {
                obra.PlanSectorialId = Utilerias.StrToInt(ddlPlanSectorial.SelectedValue);
            }

            if (ddlModalidadElemento.SelectedIndex > 0) 
            {
                obra.ModalidadId = Utilerias.StrToInt(ddlModalidadElemento.SelectedValue);
            }

            if (ddlProgramaPresupuesto.SelectedIndex > 0) 
            {
                obra.ProgramaId = Utilerias.StrToInt(ddlProgramaPresupuesto.SelectedValue);
            }

            if (ddlGrupoBeneficiario.SelectedIndex > 0) 
            {
                obra.GrupoBeneficiarioId = Utilerias.StrToInt(ddlGrupoBeneficiario.SelectedValue);
            }
            

            //Tomamos el valor de un campo oculto, esto fue necesario porque deshabilitamos el campo <Situacion de la obra>
            obra.SituacionObraId = Utilerias.StrToInt(hiddenSituacionObraId.Value);
            obra.NumeroAnterior = txtNumeroAnterior.Value;
            obra.ImporteLiberadoEjerciciosAnteriores = Utilerias.StrToDecimal(txtImporteLiberadoEjerciciosAnteriores.Value.ToString());
           
            obra.ModalidadObra = (enumModalidadObra)Convert.ToInt32(ddlModalidad.SelectedValue);  
            obra.Observaciones = txtObservaciones.InnerText;
           
            if (txtFechaInicio.Value != String.Empty) 
            {
                obra.FechaInicio=Utilerias.StrToDate(txtFechaInicio.Value);
            }

            if (txtFechaTermino.Value != String.Empty)
            {
                obra.FechaTermino = Utilerias.StrToDate(txtFechaTermino.Value);
            }
            

            if (_Accion.Text.Equals("N"))
            {

                //Crear un poadetalle para una nueva obra

                poadetalle = new POADetalle();
                poadetalle.Numero = obra.Numero;
                poadetalle.Descripcion = obra.Descripcion;
                poadetalle.MunicipioId = obra.MunicipioId;
                poadetalle.LocalidadId = obra.LocalidadId;                
                poadetalle.CriterioPriorizacionId = obra.CriterioPriorizacionId;
                poadetalle.Convenio = obra.Convenio;
                poadetalle.AperturaProgramaticaId = obra.AperturaProgramaticaId;
                poadetalle.AperturaProgramaticaMetaId = obra.AperturaProgramaticaMetaId;
                poadetalle.AperturaProgramaticaUnidadId = obra.AperturaProgramaticaUnidadId;
                poadetalle.NumeroBeneficiarios = obra.NumeroBeneficiarios;
                poadetalle.CantidadUnidades = obra.CantidadUnidades;
                poadetalle.Empleos = obra.Empleos;
                poadetalle.Jornales = obra.Jornales;

                poadetalle.FuncionalidadId = obra.FuncionalidadId;
                poadetalle.EjeId = obra.EjeId;
                poadetalle.PlanSectorialId = obra.PlanSectorialId;
                poadetalle.ModalidadId = obra.ModalidadId;
                poadetalle.ProgramaId = obra.ProgramaId;
                poadetalle.GrupoBeneficiarioId = obra.GrupoBeneficiarioId;


                poadetalle.SituacionObraId = obra.SituacionObraId;
                poadetalle.NumeroAnterior = obra.NumeroAnterior;
                poadetalle.ImporteLiberadoEjerciciosAnteriores = obra.ImporteLiberadoEjerciciosAnteriores;
                poadetalle.ModalidadObra = obra.ModalidadObra;               
                poadetalle.Observaciones = obra.Observaciones;
                poadetalle.Extemporanea = true;
                poadetalle.POA = poa;                
                
                obra.POA = poa;
                obra.POADetalle = poadetalle;
                uow.ObraBusinessLogic.Insert(obra);

            }
            else
            {
                uow.ObraBusinessLogic.Update(obra);
            } 

            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {

                // Esto solo es necesario para recargar en memoria
                // los cambios que se realizan automaticamente en la base de datos 
                // mediante un trigger de inserción
                uow = null;
                uow = new UnitOfWork(Session["IdUser"].ToString());

                this.GridViewObras.DataBind();

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

            }
            else
            {

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");
                divMsg.Style.Add("display", "block");

                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;

            }

        }

        private void BindearDropDownList()
        {

            ddlUnidadMedida.DataSource = uow.AperturaProgramaticaUnidadBusinessLogic.Get().ToList();
            ddlUnidadMedida.DataValueField = "Id";
            ddlUnidadMedida.DataTextField = "Nombre";
            ddlUnidadMedida.DataBind();

            ddlUnidadMedida.Items.Insert(0, new ListItem("Seleccione...", "0"));

            ddlMunicipio.DataSource = uow.MunicipioBusinessLogic.Get().ToList();
            ddlMunicipio.DataValueField = "Id";
            ddlMunicipio.DataTextField = "Nombre";
            ddlMunicipio.DataBind();

            ddlMunicipio.Items.Insert(0, new ListItem("Seleccione...", "0"));
            

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

                        int obraId = Utilerias.StrToInt(GridViewObras.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                        Obra obra = uow.ObraBusinessLogic.GetByID(obraId);

                        url = "AsignarFinanciamientoPOA.aspx?poadetalleId=" + obra.POADetalleId.ToString();
                        btnE.Attributes.Add("data-url-financiamiento", url);                      
                                              

                    }

                }

            }
        }

        
        public IQueryable<DataAccessLayer.Models.Obra> GridViewObras_GetData()
        {
            IQueryable<Obra> list = null;

            list = uow.ObraBusinessLogic.Get(o => o.POA.UnidadPresupuestalId == unidadpresupuestalId & o.POA.EjercicioId == ejercicioId, orderBy: r => r.OrderBy(ro => ro.Consecutivo));
            lblResumen.Text = String.Format("Total de obras y acciones : {0}", list.Count());
            return list;            
        }

        protected void GridViewObras_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView grid = sender as GridView;
            grid.PageIndex = e.NewPageIndex;
            
            divEdicion.Style.Add("display", "none");
            divBtnNuevo.Style.Add("display", "block");
            divMsg.Style.Add("display", "none");
        }


    }
}
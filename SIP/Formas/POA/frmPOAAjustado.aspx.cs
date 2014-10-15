using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
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

            uow = new UnitOfWork();
             
            if (!IsPostBack)
            {

                unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
                ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

                lblTituloPOA.Text = String.Format("Proyecto de POA ajustado en el ejercicio {0}", uow.EjercicioBusinessLogic.GetByID(ejercicioId).Año);
                BindGrid();
                BindearDropDownList();
            }

        }


        private void BindGrid()
        {

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            this.GridViewObras.DataSource = uow.ObraBusinessLogic.Get(o => o.POA.UnidadPresupuestalId == unidadpresupuestalId & o.POA.EjercicioId == ejercicioId).ToList();
            this.GridViewObras.DataBind();
        }


        public void BindControles(Obra obra)
        {

            txtNumero.Value = obra.Numero;
            txtDescripcion.Value = obra.Descripcion;
            ddlMunicipio.SelectedValue = obra.MunicipioId.ToString();
            ddlTipoLocalidad.SelectedValue = obra.TipoLocalidadId.ToString();
            ddlCriterioPriorizacion.SelectedValue = obra.CriterioPriorizacionId.ToString();

            cddlPrograma.SelectedValue = obra.AperturaProgramatica.Parent.ParentId.ToString();
            cddlSubprograma.SelectedValue = obra.AperturaProgramatica.ParentId.ToString();
            cddlSubsubprograma.SelectedValue = obra.AperturaProgramaticaId.ToString();
            cddlMeta.SelectedValue = obra.AperturaProgramaticaMetaId.ToString();

            txtLocalidad.Value = obra.Localidad;
            txtNumeroBeneficiarios.Value = obra.NumeroBeneficiarios.ToString();
            txtCantidadUnidades.Value = obra.CantidadUnidades.ToString();
            txtEmpleos.Value = obra.Empleos.ToString();
            txtJornales.Value = obra.Jornales.ToString();
            ddlSituacionObra.SelectedValue = obra.SituacionObraId.ToString();
            ddlModalidad.SelectedValue = ((int)obra.ModalidadObra).ToString();
            txtImporteTotal.Value = obra.ImporteTotal.ToString();
            txtCostoLiberadoEjerciciosAnteriores.Value = obra.ImporteLiberadoEjerciciosAnteriores.ToString();
            txtPresupuestoEjercicio.Value = obra.ImportePresupuesto.ToString();
            txtObservaciones.Value = obra.Observaciones;

            cddlFuncionalidadNivel1.SelectedValue = obra.Funcionalidad.Parent.ParentId.ToString();
            cddlFuncionalidadNivel2.SelectedValue = obra.Funcionalidad.ParentId.ToString();
            cddlFuncionalidadNivel3.SelectedValue = obra.FuncionalidadId.ToString();

            cddlEjePVD1.SelectedValue = obra.Eje.ParentId.ToString();
            cddlEjePVD2.SelectedValue = obra.EjeId.ToString();

            cddlModalidadAgrupador.SelectedValue = obra.Modalidad.ParentId.ToString();
            cddlModalidadElemento.SelectedValue = obra.ModalidadId.ToString();

            ddlPlanSectorial.SelectedValue = obra.PlanSectorialId.ToString();
            ddlProgramaPresupuesto.SelectedValue = obra.ProgramaId.ToString();
            ddlGrupoBeneficiario.SelectedValue = obra.GrupoBeneficiarioId.ToString();


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
            txtEsAccion.Checked = false;

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

            Obra obra = uow.ObraBusinessLogic.GetByID(currentId);

            BindControles(obra);

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

            Obra obra = uow.ObraBusinessLogic.GetByID(currentId);


            uow.ObraBusinessLogic.Delete(obra);
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
            string msg = "Se ha guardado correctamente";


            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();
            Obra obra = null;

            if (poa == null)
            {
                poa = new DataAccessLayer.Models.POA();
                poa.UnidadPresupuestalId = unidadpresupuestalId;
                poa.EjercicioId = ejercicioId;
            }



            if (_Accion.Text.Equals("N"))
                obra = new Obra();
            else
            {
                currentId = Convert.ToInt32(_ID.Text);
                obra = uow.ObraBusinessLogic.GetByID(currentId);
                msg = "Se ha actualizado correctamente";
            }

            obra.Numero = txtNumero.Value;
            obra.Descripcion = txtDescripcion.Value;
            obra.MunicipioId = Utilerias.StrToInt(ddlMunicipio.SelectedValue);
            obra.Localidad = txtLocalidad.Value;
            obra.TipoLocalidadId = Utilerias.StrToInt(ddlTipoLocalidad.SelectedValue);
            obra.CriterioPriorizacionId = Utilerias.StrToInt(ddlCriterioPriorizacion.SelectedValue);
            obra.EsAccion = txtEsAccion.Checked;
            obra.AperturaProgramaticaId = Utilerias.StrToInt(ddlSubsubprograma.SelectedValue);
            obra.AperturaProgramaticaMetaId = Utilerias.StrToInt(ddlMeta.SelectedValue);
            obra.NumeroBeneficiarios = Utilerias.StrToInt(txtNumeroBeneficiarios.Value.ToString());
            obra.CantidadUnidades = Utilerias.StrToInt(txtCantidadUnidades.Value.ToString());
            obra.Empleos = Utilerias.StrToInt(txtEmpleos.Value.ToString());
            obra.Jornales = Utilerias.StrToInt(txtJornales.Value.ToString());

            obra.FuncionalidadId = Utilerias.StrToInt(ddlSubFuncion.SelectedValue);
            obra.EjeId = Utilerias.StrToInt(ddlEjeElemento.SelectedValue);
            obra.PlanSectorialId = Utilerias.StrToInt(ddlPlanSectorial.SelectedValue);
            obra.ModalidadId = Utilerias.StrToInt(ddlModalidadElemento.SelectedValue);
            obra.ProgramaId = Utilerias.StrToInt(ddlProgramaPresupuesto.SelectedValue);
            obra.GrupoBeneficiarioId = Utilerias.StrToInt(ddlGrupoBeneficiario.SelectedValue);

            ///
            obra.SituacionObraId = Utilerias.StrToInt(ddlSituacionObra.SelectedValue);
            obra.ModalidadObra = (enumModalidadObra)Convert.ToInt32(ddlModalidad.SelectedValue);
            obra.ImporteTotal = Convert.ToDecimal(txtImporteTotal.Value.ToString());
            obra.ImporteLiberadoEjerciciosAnteriores = Convert.ToDecimal(txtCostoLiberadoEjerciciosAnteriores.Value.ToString());
            obra.ImportePresupuesto = Convert.ToDecimal(txtPresupuestoEjercicio.Value.ToString());
            obra.Observaciones = txtObservaciones.InnerText;

            if (_Accion.Text.Equals("N"))
            {
                obra.POA = poa;
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
                HtmlButton btnE = (HtmlButton)e.Row.FindControl("btnE");
                if (btnE != null)
                {
                    if (GridViewObras.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                    {
                        string url = string.Empty;
                        url = "EvaluacionPOA.aspx?p=" + GridViewObras.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
                        //btnE.Attributes.Add("onclick", "fnc_IrDesdeGrid('" + url + "')");
                        btnE.Attributes.Add("data-url-poa", url);
                    }

                }

            }
        }



    }
}
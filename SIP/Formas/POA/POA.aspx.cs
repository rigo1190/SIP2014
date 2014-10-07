using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web;

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

            uow = new UnitOfWork();
               
            if (!IsPostBack)
            {

                unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
                ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());
                
                lblTituloPOA.Text = String.Format("POA proyectado ejercicio {0}", uow.EjercicioBusinessLogic.GetByID(ejercicioId).Año);
                BindGrid();
                BindearDropDownList();
            }
            
        }
       
        private void BindGrid()
        {

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            this.GridViewObras.DataSource = uow.POADetalleBusinessLogic.Get(o=>o.POA.UnidadPresupuestalId==unidadpresupuestalId & o.POA.EjercicioId==ejercicioId).ToList();
            this.GridViewObras.DataBind();
        }
              
        
        public void BindControles(POADetalle poadetalle)
        {

            txtNumero.Value = poadetalle.Numero;
            txtDescripcion.Value = poadetalle.Descripcion;
            ddlMunicipio.SelectedValue = poadetalle.MunicipioId.ToString();
            ddlTipoLocalidad.SelectedValue = poadetalle.TipoLocalidadId.ToString();
            ddlCriterioPriorizacion.SelectedValue = poadetalle.CriterioPriorizacionId.ToString();

            BindearDropDownListPrograma(ddlPrograma, poadetalle);
            BindearDropDownListSubPrograma(ddlSubprograma, poadetalle);
            BindearDropDownListSubSubPrograma(ddlTipologia, poadetalle);
            BindearDropDownListMeta(ddlMeta, poadetalle);            
           
            txtLocalidad.Value = poadetalle.Localidad;            
            txtNumeroBeneficiarios.Value = poadetalle.NumeroBeneficiarios.ToString();
            txtCantidadUnidades.Value = poadetalle.CantidadUnidades.ToString();
            txtEmpleos.Value = poadetalle.Empleos.ToString();
            txtJornales.Value = poadetalle.Jornales.ToString();
            ddlSituacionObra.SelectedValue = poadetalle.SituacionObraId.ToString();
            ddlModalidad.SelectedValue = ((int)poadetalle.ModalidadObra).ToString();
            txtImporteTotal.Value = poadetalle.ImporteTotal.ToString();
            txtObservaciones.Value = poadetalle.Observaciones;

            BindearDropDownListFinalidad(ddlFinalidad, poadetalle);
            BindearDropDownListFuncion(ddlFuncion,poadetalle);
            BindearDropDownListSubFuncion(ddlSubFuncion, poadetalle);

            BindearDropDownListEjeAgrupador(ddlEjeAgrupador, poadetalle);
            BindearDropDownListEjeElemento(ddlEjeElemento, poadetalle);

            BindearDropDownListModalidadAgrupador(ddlModalidadAgrupador,poadetalle);
            BindearDropDownListModalidadElemento(ddlModalidadElemento, poadetalle);

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
            txtLocalidad.Value = String.Empty;
            ddlTipoLocalidad.SelectedIndex = -1;
            txtEsAccion.Checked = false;
            ddlPrograma.SelectedIndex = -1;
            ddlSubprograma.Items.Clear();
            ddlTipologia.Items.Clear();
            ddlMeta.Items.Clear();
            txtNumeroBeneficiarios.Value = String.Empty;
            txtCantidadUnidades.Value = String.Empty;
            txtEmpleos.Value = String.Empty;
            txtJornales.Value = String.Empty;
            ddlSituacionObra.SelectedIndex = -1;
            ddlModalidad.SelectedIndex = -1;
            txtImporteTotal.Value = String.Empty;
            txtObservaciones.Value = String.Empty;

            ddlFinalidad.SelectedIndex = -1;
            ddlFuncion.Items.Clear();
            ddlSubFuncion.Items.Clear();

            ddlEjeAgrupador.SelectedIndex = -1;
            ddlEjeElemento.Items.Clear();

            ddlPlanSectorial.SelectedIndex = -1;

            ddlModalidadAgrupador.SelectedIndex = -1;
            ddlModalidadElemento.Items.Clear();

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
            poadetalle.EsAccion = txtEsAccion.Checked;
            poadetalle.AperturaProgramaticaId = Utilerias.StrToInt(ddlTipologia.SelectedValue);
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
            
            poadetalle.Observaciones = txtObservaciones.InnerText;

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

        private void BindearDropDownList()
        {          

            //Se carga informnacion en el drop de Municipios

            ddlPrograma.DataSource = uow.AperturaProgramaticaBusinessLogic.Get(ap=> ap.ParentId==null && ap.EjercicioId==ejercicioId).OrderBy(ap=>ap.Orden);
            ddlPrograma.DataValueField = "Id";
            ddlPrograma.DataTextField = "Nombre";
            ddlPrograma.DataBind();            
                
            ddlPrograma.Items.Insert(0, new ListItem("Seleccione...", "0"));          


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

            ddlFinalidad.DataSource = uow.FuncionalidadBusinessLogic.Get(f=>f.ParentId==null).ToList();
            ddlFinalidad.DataValueField = "Id";
            ddlFinalidad.DataTextField = "Descripcion";
            ddlFinalidad.DataBind();

            ddlFinalidad.Items.Insert(0, new ListItem("Seleccione...", "0"));
           
            ddlEjeAgrupador.DataSource = uow.EjeBusinessLogic.Get(f => f.ParentId == null).ToList();
            ddlEjeAgrupador.DataValueField = "Id";
            ddlEjeAgrupador.DataTextField = "Descripcion";
            ddlEjeAgrupador.DataBind();

            ddlEjeAgrupador.Items.Insert(0, new ListItem("Seleccione...", "0"));

            
            ddlPlanSectorial.DataSource = uow.PlanSectorialBusinessLogic.Get(orderBy:ps=>ps.OrderBy(o=>o.Orden)).ToList();
            ddlPlanSectorial.DataValueField = "Id";
            ddlPlanSectorial.DataTextField = "Descripcion";
            ddlPlanSectorial.DataBind();

            ddlPlanSectorial.Items.Insert(0, new ListItem("Seleccione...", "0"));
                        
            ddlModalidadAgrupador.DataSource = uow.ModalidadBusinessLogic.Get(m=>m.ParentId==null).ToList();
            ddlModalidadAgrupador.DataValueField = "Id";
            ddlModalidadAgrupador.DataTextField = "Descripcion";
            ddlModalidadAgrupador.DataBind();

            ddlModalidadAgrupador.Items.Insert(0, new ListItem("Seleccione...", "0"));
            
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
              
        protected void ddlPrograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ctrol = sender as DropDownList;

            int programaId = Convert.ToInt32(ctrol.SelectedValue);

            ddlSubprograma.Items.Clear();
            ddlTipologia.Items.Clear();
            ddlMeta.Items.Clear();

            ddlSubprograma.DataSource = uow.AperturaProgramaticaBusinessLogic.Get(ap => ap.ParentId == programaId).OrderBy(r=>r.Orden);
            ddlSubprograma.DataValueField = "Id";
            ddlSubprograma.DataTextField = "Nombre";
            ddlSubprograma.DataBind();

            ddlSubprograma.Items.Insert(0, new ListItem("Seleccione ...", "0"));

        }
             
        protected void ddlSubprograma_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ctrol = sender as DropDownList;

            int subprogramaId = Convert.ToInt32(ctrol.SelectedValue);
           
            ddlTipologia.DataSource = uow.AperturaProgramaticaBusinessLogic.Get(ap => ap.ParentId == subprogramaId).OrderBy(ap => ap.Orden);
            ddlTipologia.DataValueField = "Id";
            ddlTipologia.DataTextField = "Nombre";
            ddlTipologia.DataBind();

            ddlTipologia.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }

        protected void ddlTipologia_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ctrol = sender as DropDownList;

            int subsubprogramaId = Convert.ToInt32(ctrol.SelectedValue);

            ddlMeta.DataSource = uow.AperturaProgramaticaMetaBusinessLogic.Get(ap => ap.AperturaProgramaticaId == subsubprogramaId);
            ddlMeta.DataValueField = "Id";
            ddlMeta.DataTextField = "Descripcion";
            ddlMeta.DataBind();

            ddlMeta.Items.Insert(0, new ListItem("Seleccione...", "0"));

        }
        
        private void BindearDropDownListPrograma(DropDownList ddl, POADetalle poadetalle)
        {

            int programaId = poadetalle.AperturaProgramatica.Parent.Parent.Id;

            ddl.DataSource = uow.AperturaProgramaticaBusinessLogic.Get(ap => ap.ParentId == null).OrderBy(ap => ap.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Nombre";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione", "0"));

            ddl.SelectedValue = programaId.ToString();

        }

        private void BindearDropDownListSubPrograma(DropDownList ddl, POADetalle poadetalle)
        {

            int programaId = poadetalle.AperturaProgramatica.Parent.Parent.Id;
            int subprogramaId = poadetalle.AperturaProgramatica.Parent.Id;

            ddl.DataSource = uow.AperturaProgramaticaBusinessLogic.Get(ap => ap.ParentId == programaId).OrderBy(ap => ap.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Nombre";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione ...", "0"));

            ddl.SelectedValue = subprogramaId.ToString();

        }

        private void BindearDropDownListSubSubPrograma(DropDownList ddl, POADetalle poadetalle)
        {

            int subprogramaId = poadetalle.AperturaProgramatica.Parent.Id;

            ddl.DataSource = uow.AperturaProgramaticaBusinessLogic.Get(ap => ap.ParentId == subprogramaId).OrderBy(ap => ap.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Nombre";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione ...", "0"));

            ddl.SelectedValue = poadetalle.AperturaProgramaticaId.ToString();

        }

        private void BindearDropDownListMeta(DropDownList ddl, POADetalle poadetalle)
        {

            ddl.DataSource = uow.AperturaProgramaticaMetaBusinessLogic.Get(m => m.AperturaProgramaticaId == poadetalle.AperturaProgramaticaId);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Descripcion";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione ...", "0"));

            ddl.SelectedValue = poadetalle.AperturaProgramaticaMetaId.ToString();

        }

        protected void ddlFinalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ctrol = sender as DropDownList;

            int finalidadId = Convert.ToInt32(ctrol.SelectedValue);

            ddlFuncion.Items.Clear();
            ddlSubFuncion.Items.Clear();

            ddlFuncion.DataSource = uow.FuncionalidadBusinessLogic.Get(sf => sf.ParentId == finalidadId).OrderBy(r => r.Orden);
            ddlFuncion.DataValueField = "Id";
            ddlFuncion.DataTextField = "Descripcion";
            ddlFuncion.DataBind();

            ddlFuncion.Items.Insert(0, new ListItem("Seleccione ...", "0"));
        }

        protected void ddlFuncion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ctrol = sender as DropDownList;

            int funcionId = Convert.ToInt32(ctrol.SelectedValue);
           
            ddlSubFuncion.Items.Clear();

            ddlSubFuncion.DataSource = uow.FuncionalidadBusinessLogic.Get(sf => sf.ParentId == funcionId).OrderBy(r => r.Orden);
            ddlSubFuncion.DataValueField = "Id";
            ddlSubFuncion.DataTextField = "Descripcion";
            ddlSubFuncion.DataBind();

            ddlSubFuncion.Items.Insert(0, new ListItem("Seleccione ...", "0"));
        }

        protected void ddlSubFuncion_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        protected void ddlEje_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }
        protected void ddlPlanSectorial_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }
        protected void ddlEjeAgrupador_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ctrol = sender as DropDownList;

            int agrupadorId = Convert.ToInt32(ctrol.SelectedValue);

            ddlEjeElemento.Items.Clear();

            ddlEjeElemento.DataSource = uow.EjeBusinessLogic.Get(sf => sf.ParentId == agrupadorId).OrderBy(r => r.Orden);
            ddlEjeElemento.DataValueField = "Id";
            ddlEjeElemento.DataTextField = "Descripcion";
            ddlEjeElemento.DataBind();

            ddlEjeElemento.Items.Insert(0, new ListItem("Seleccione ...", "0"));
        }
        protected void ddlModalidadAgrupador_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ctrol = sender as DropDownList;

            int agrupadorId = Convert.ToInt32(ctrol.SelectedValue);

            ddlModalidadElemento.Items.Clear();

            ddlModalidadElemento.DataSource = uow.ModalidadBusinessLogic.Get(m => m.ParentId == agrupadorId).OrderBy(r => r.Orden);
            ddlModalidadElemento.DataValueField = "Id";
            ddlModalidadElemento.DataTextField = "Descripcion";
            ddlModalidadElemento.DataBind();

            ddlModalidadElemento.Items.Insert(0, new ListItem("Seleccione ...", "0"));
        }
        protected void ddlModalidadElemento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

        private void BindearDropDownListFinalidad(DropDownList ddl, POADetalle poadetalle)
        {

            int finalidadId = poadetalle.Funcionalidad.Parent.Parent.Id;

            ddl.DataSource = uow.FuncionalidadBusinessLogic.Get(f => f.ParentId == null).OrderBy(f => f.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Descripcion";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione", "0"));

            ddl.SelectedValue = finalidadId.ToString();

        }

        private void BindearDropDownListFuncion(DropDownList ddl, POADetalle poadetalle)
        {

            int finalidadId = poadetalle.Funcionalidad.Parent.Parent.Id;
            int funcionId = poadetalle.Funcionalidad.Parent.Id;

            ddl.DataSource = uow.FuncionalidadBusinessLogic.Get(f => f.ParentId == finalidadId).OrderBy(f => f.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Descripcion";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione ...", "0"));

            ddl.SelectedValue = funcionId.ToString();

        }

        private void BindearDropDownListSubFuncion(DropDownList ddl, POADetalle poadetalle)
        {
           
            int funcionId = poadetalle.Funcionalidad.Parent.Id;

            ddl.DataSource = uow.FuncionalidadBusinessLogic.Get(f => f.ParentId == funcionId).OrderBy(f => f.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Descripcion";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione ...", "0"));

            ddl.SelectedValue = poadetalle.FuncionalidadId.ToString();

        }

        private void BindearDropDownListEjeAgrupador(DropDownList ddl, POADetalle poadetalle)
        {

            int agrupadorId = poadetalle.Eje.Parent.Id;

            ddl.DataSource = uow.EjeBusinessLogic.Get(f => f.ParentId == null).OrderBy(f => f.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Descripcion";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione", "0"));

            ddl.SelectedValue = agrupadorId.ToString();

        }

        private void BindearDropDownListEjeElemento(DropDownList ddl, POADetalle poadetalle)
        {

            int agrupadorId = poadetalle.Eje.Parent.Id;

            ddl.DataSource = uow.EjeBusinessLogic.Get(f => f.ParentId == agrupadorId).OrderBy(f => f.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Descripcion";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione", "0"));

            ddl.SelectedValue = poadetalle.EjeId.ToString();

        }

        private void BindearDropDownListModalidadAgrupador(DropDownList ddl, POADetalle poadetalle)
        {

            int agrupadorId = poadetalle.Modalidad.Parent.Id;

            ddl.DataSource = uow.ModalidadBusinessLogic.Get(f => f.ParentId == null).OrderBy(f => f.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Descripcion";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione", "0"));

            ddl.SelectedValue = agrupadorId.ToString();

        }

        private void BindearDropDownListModalidadElemento(DropDownList ddl, POADetalle poadetalle)
        {

            int agrupadorId = poadetalle.Modalidad.Parent.Id;

            ddl.DataSource = uow.ModalidadBusinessLogic.Get(f => f.ParentId == agrupadorId).OrderBy(f => f.Orden);
            ddl.DataValueField = "Id";
            ddl.DataTextField = "Descripcion";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("Seleccione", "0"));

            ddl.SelectedValue = poadetalle.ModalidadId.ToString();

        }

        protected void ddlEjeElemento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
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
                        btnE.Attributes.Add("onclick", "fnc_IrDesdeGrid('" + url + "')");
                    }
                    
                }

            }
        }
                           
    }
}
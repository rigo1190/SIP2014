using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class frmPOAAjustado : System.Web.UI.Page
    {
            
        //private UnitOfWork uow;
        //private int currentId;
        //private int unidadpresupuestalId;
        //private int ejercicioId;       

        protected void Page_Load(object sender, EventArgs e)
        {

            //uow = new UnitOfWork(Session["IdUser"].ToString());
            //unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            //ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());
             
            if (!IsPostBack)
            {               
                
                //int columnsInicial = GridViewObras.Columns.Count;

                //UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);
                //Ejercicio ejercicio = uow.EjercicioBusinessLogic.GetByID(ejercicioId); 

                //lblTituloPOA.Text = String.Format("{0}<br />Proyecto de POA ajustado en el ejercicio {1}",up.Nombre,ejercicio.Año);
                
                //BindearDropDownList();
              
            }

        }


        //public void BindControles(Obra obra)
        //{
        //    //restablecer previamente controles dropdownlist

        //    cddlFuncionalidadNivel1.SelectedValue = String.Empty;
        //    ddlEje.SelectedIndex = -1;



        //    txtNumero.Value = obra.Numero;
        //    txtDescripcion.Value = obra.Descripcion;
        //    cddlMunicipio.SelectedValue = obra.MunicipioId.ToString();
        //    cddlLocalidad.SelectedValue = obra.LocalidadId.ToString();


        //    if (obra.AperturaProgramaticaUnidadId != null)
        //    {
        //        ddlUnidadMedida.SelectedValue = obra.AperturaProgramaticaUnidadId.ToString();
        //    }

        //    ddlCriterioPriorizacion.SelectedValue = obra.CriterioPriorizacionId.ToString();
        //    txtNombreConvenio.Value = obra.Convenio;

        //    cddlPrograma.SelectedValue = obra.AperturaProgramatica.Parent.ParentId.ToString();
        //    cddlSubprograma.SelectedValue = obra.AperturaProgramatica.ParentId.ToString();
        //    cddlSubsubprograma.SelectedValue = obra.AperturaProgramaticaId.ToString();
        //    cddlMeta.SelectedValue = obra.AperturaProgramaticaMetaId.ToString();
                       

        //    txtFechaInicio.Value =String.Format("{0:d}",obra.FechaInicio);
        //    txtFechaTermino.Value = String.Format("{0:d}", obra.FechaTermino);

        //    txtNumeroBeneficiarios.Value = obra.NumeroBeneficiarios.ToString();
        //    txtCantidadUnidades.Value = obra.CantidadUnidades.ToString();
        //    txtEmpleos.Value = obra.Empleos.ToString();
        //    txtJornales.Value = obra.Jornales.ToString();
        //    ddlSituacionObra.SelectedValue = obra.SituacionObraId.ToString();

        //    ddlModalidad.SelectedIndex = 0;

        //    if (obra.ModalidadObra != null)
        //    {
        //        ddlModalidad.SelectedValue = ((int)obra.ModalidadObra).ToString();
        //    }
                        
        //    txtImporteTotal.Value = obra.GetCostoTotal().ToString();
        //    txtNumeroAnterior.Value = obra.NumeroAnterior;
        //    txtImporteLiberadoEjerciciosAnteriores.Value = obra.GetImporteLiberadoEjerciciosAnteriores().ToString();
        //    txtPresupuestoEjercicio.Value = obra.GetImporteAsignado().ToString();
        //    txtObservaciones.Value = obra.Observaciones;

        //    if (obra.FuncionalidadId != null) 
        //    {
        //        cddlFuncionalidadNivel1.SelectedValue = obra.Funcionalidad.Parent.ParentId.ToString();
        //        cddlFuncionalidadNivel2.SelectedValue = obra.Funcionalidad.ParentId.ToString();
        //        cddlFuncionalidadNivel3.SelectedValue = obra.FuncionalidadId.ToString();
        //    }

        //    if (obra.EjeId != null)
        //    {
        //        ddlEje.SelectedValue = obra.EjeId.ToString();
        //    }

        //    if (obra.ModalidadId != null) 
        //    {
        //        cddlModalidadAgrupador.SelectedValue = obra.Modalidad.ParentId.ToString();
        //        cddlModalidadElemento.SelectedValue = obra.ModalidadId.ToString();
        //    }

        //    if (obra.PlanSectorialId != null) 
        //    {
        //        ddlPlanSectorial.SelectedValue = obra.PlanSectorialId.ToString();
        //    }

        //    if (obra.ProgramaId != null) 
        //    {
        //        ddlProgramaPresupuesto.SelectedValue = obra.ProgramaId.ToString();
        //    }

        //    if (obra.GrupoBeneficiarioId != null) 
        //    {
        //        ddlGrupoBeneficiario.SelectedValue = obra.GrupoBeneficiarioId.ToString();
        //    }
            

        //}
        
        //protected void btnNuevo_Click(object sender, EventArgs e)
        //{
        //    //Se limpian los controles

        //    txtNumero.Value = String.Empty;
        //    txtDescripcion.Value = String.Empty;
        //    cddlMunicipio.SelectedValue = String.Empty;
        //    ddlCriterioPriorizacion.SelectedIndex = -1;
        //    txtNombreConvenio.Value = String.Empty;
        //    cddlLocalidad.SelectedValue = String.Empty;            

        //    txtFechaInicio.Value = String.Empty;
        //    txtFechaTermino.Value = String.Empty;

        //    cddlPrograma.SelectedValue = String.Empty;
        //    cddlSubprograma.SelectedValue = String.Empty;
        //    cddlSubsubprograma.SelectedValue = String.Empty;
        //    cddlMeta.SelectedValue = String.Empty;

        //    ddlUnidadMedida.SelectedIndex = -1;

        //    txtNumeroBeneficiarios.Value = String.Empty;
        //    txtCantidadUnidades.Value = String.Empty;
        //    txtEmpleos.Value = String.Empty;
        //    txtJornales.Value = String.Empty;
        //    ddlSituacionObra.SelectedIndex = -1;
        //    ddlModalidad.SelectedIndex = -1;
        //    txtImporteTotal.Value = String.Empty;
        //    txtImporteLiberadoEjerciciosAnteriores.Value = String.Empty;
        //    txtPresupuestoEjercicio.Value = String.Empty;

        //    txtObservaciones.Value = String.Empty;

        //    cddlFuncionalidadNivel1.SelectedValue = String.Empty;
        //    cddlFuncionalidadNivel2.SelectedValue = String.Empty;
        //    cddlFuncionalidadNivel3.SelectedValue = String.Empty;


        //    ddlPlanSectorial.SelectedIndex = -1;

        //    cddlModalidadAgrupador.SelectedValue = String.Empty;
        //    cddlModalidadElemento.SelectedValue = String.Empty;

        //    ddlProgramaPresupuesto.SelectedIndex = -1;
        //    ddlGrupoBeneficiario.SelectedIndex = -1;

        //    divEdicion.Style.Add("display", "block");
        //    divBtnNuevo.Style.Add("display", "none");
        //    divMsg.Style.Add("display", "none");
        //    _Accion.Text = "N";

        //    ClientScript.RegisterStartupScript(this.GetType(), "script04", "fnc_DeshabilitarSituacionObra(true);", true);

        //}

        //protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        //{

        //    GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
        //    _ID.Text = GridViewObras.DataKeys[row.RowIndex].Values["Id"].ToString();

        //    currentId = Convert.ToInt32(GridViewObras.DataKeys[row.RowIndex].Values["Id"].ToString());

        //    Obra obra = uow.ObraBusinessLogic.GetByID(currentId);

        //    BindControles(obra);

        //    divEdicion.Style.Add("display", "block");
        //    divBtnNuevo.Style.Add("display", "none");
        //    divMsg.Style.Add("display", "none");
        //    _Accion.Text = "A";

        //    ClientScript.RegisterStartupScript(this.GetType(), "script01", "fnc_ocultarDivDatosConvenio();", true);
        //    ClientScript.RegisterStartupScript(this.GetType(), "script02", "fnc_ocultarDivObraAnterior();", true);
        //    ClientScript.RegisterStartupScript(this.GetType(), "script03", "fnc_RecuperarValoresEnCamposDeshabilitados();", true);

        //}

        //protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        //{
        //    //Se busca l ID de la fila seleccionada
        //    GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
        //    string msg = "Se ha eliminado correctamente";

        //    currentId = Utilerias.StrToInt(GridViewObras.DataKeys[row.RowIndex].Value.ToString());

        //    Obra obra = uow.ObraBusinessLogic.GetByID(currentId);


        //    uow.ObraBusinessLogic.Delete(obra);
        //    uow.SaveChanges();

        //    if (uow.Errors.Count == 0)
        //    {

        //        divEdicion.Style.Add("display", "none");
        //        divBtnNuevo.Style.Add("display", "block");

        //        this.GridViewObras.DataBind();
        //    }
        //    else
        //    {

        //        divEdicion.Style.Add("display", "none");
        //        divBtnNuevo.Style.Add("display", "block");
        //        divMsg.Style.Add("display", "block");

        //        msg = string.Empty;
        //        foreach (string cad in uow.Errors)
        //            msg += cad;

        //        lblMensajes.Text = msg;
        //    }


        //}

        //protected void btnGuardar_Click(object sender, EventArgs e)
        //{
        //    string msg = "Se ha guardado correctamente";


        //    unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
        //    ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

        //    DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();
        //    POADetalle poadetalle = null;
        //    Obra obra = null;

        //    if (poa == null)
        //    {
        //        poa = new DataAccessLayer.Models.POA();
        //        poa.UnidadPresupuestalId = unidadpresupuestalId;
        //        poa.EjercicioId = ejercicioId;
        //    }



        //    if (_Accion.Text.Equals("N"))
        //    {                
        //        obra = new Obra();
        //    }
        //    else
        //    {
        //        currentId = Convert.ToInt32(_ID.Text);
        //        obra = uow.ObraBusinessLogic.GetByID(currentId);
        //        msg = "Se ha actualizado correctamente";
        //    }

        //    obra.Numero = txtNumero.Value;
        //    obra.Descripcion = txtDescripcion.Value;
        //    obra.MunicipioId = Utilerias.StrToInt(ddlMunicipio.SelectedValue);
        //    obra.LocalidadId = Utilerias.StrToInt(ddlLocalidad.SelectedValue);            
        //    obra.CriterioPriorizacionId = Utilerias.StrToInt(ddlCriterioPriorizacion.SelectedValue);

        //    //Garantizar que se limpie correctamente el campo Nombre del Convenio

        //    switch (obra.CriterioPriorizacionId)
        //    {
        //        case 2:

        //            break;

        //        default:

        //            txtNombreConvenio.Value = String.Empty;
        //            break;
        //    }


        //    obra.Convenio = txtNombreConvenio.Value;
        //    obra.AperturaProgramaticaId = Utilerias.StrToInt(ddlSubsubprograma.SelectedValue);
        //    obra.AperturaProgramaticaMetaId = null;
        //    obra.AperturaProgramaticaUnidadId = Utilerias.StrToInt(ddlUnidadMedida.SelectedValue);
        //    obra.NumeroBeneficiarios = Utilerias.StrToInt(txtNumeroBeneficiarios.Value.ToString().Replace(",",null));
        //    obra.CantidadUnidades = Utilerias.StrToInt(txtCantidadUnidades.Value.ToString().Replace(",", null));
        //    obra.Empleos = Utilerias.StrToInt(txtEmpleos.Value.ToString().Replace(",", null));
        //    obra.Jornales = Utilerias.StrToInt(txtJornales.Value.ToString().Replace(",", null));

        //    obra.FuncionalidadId = null;
        //    obra.EjeId = null;
        //    obra.PlanSectorialId = null;
        //    obra.ModalidadId = null;
        //    obra.ProgramaId = null;
        //    obra.GrupoBeneficiarioId = null;


        //    if (ddlSubFuncion.SelectedIndex > 0) 
        //    {
        //        obra.FuncionalidadId = Utilerias.StrToInt(ddlSubFuncion.SelectedValue);
        //    }

        //    if (ddlEje.SelectedIndex > 0) 
        //    {
        //        obra.EjeId = Utilerias.StrToInt(ddlEje.SelectedValue);
        //    }

        //    if (ddlPlanSectorial.SelectedIndex > 0) 
        //    {
        //        obra.PlanSectorialId = Utilerias.StrToInt(ddlPlanSectorial.SelectedValue);
        //    }

        //    if (ddlModalidadElemento.SelectedIndex > 0) 
        //    {
        //        obra.ModalidadId = Utilerias.StrToInt(ddlModalidadElemento.SelectedValue);
        //    }

        //    if (ddlProgramaPresupuesto.SelectedIndex > 0) 
        //    {
        //        obra.ProgramaId = Utilerias.StrToInt(ddlProgramaPresupuesto.SelectedValue);
        //    }

        //    if (ddlGrupoBeneficiario.SelectedIndex > 0) 
        //    {
        //        obra.GrupoBeneficiarioId = Utilerias.StrToInt(ddlGrupoBeneficiario.SelectedValue);
        //    }
            

        //    //Tomamos el valor de un campo oculto, esto fue necesario porque deshabilitamos el campo <Situacion de la obra>
        //    obra.SituacionObraId = Utilerias.StrToInt(hiddenSituacionObraId.Value);
        //    obra.NumeroAnterior = txtNumeroAnterior.Value;
        //    obra.ImporteLiberadoEjerciciosAnteriores = Utilerias.StrToDecimal(txtImporteLiberadoEjerciciosAnteriores.Value.ToString());
           
        //    obra.ModalidadObra = (enumModalidadObra)Convert.ToInt32(ddlModalidad.SelectedValue);  
        //    obra.Observaciones = txtObservaciones.InnerText;
           
        //    if (txtFechaInicio.Value != String.Empty) 
        //    {
        //        obra.FechaInicio=Utilerias.StrToDate(txtFechaInicio.Value);
        //    }

        //    if (txtFechaTermino.Value != String.Empty)
        //    {
        //        obra.FechaTermino = Utilerias.StrToDate(txtFechaTermino.Value);
        //    }
            

        //    if (_Accion.Text.Equals("N"))
        //    {

        //        //Crear un poadetalle para una nueva obra

        //        poadetalle = new POADetalle();
        //        poadetalle.Numero = obra.Numero;
        //        poadetalle.Descripcion = obra.Descripcion;
        //        poadetalle.MunicipioId = obra.MunicipioId;
        //        poadetalle.LocalidadId = obra.LocalidadId;                
        //        poadetalle.CriterioPriorizacionId = obra.CriterioPriorizacionId;
        //        poadetalle.Convenio = obra.Convenio;
        //        poadetalle.AperturaProgramaticaId = obra.AperturaProgramaticaId;
        //        poadetalle.AperturaProgramaticaMetaId = obra.AperturaProgramaticaMetaId;
        //        poadetalle.AperturaProgramaticaUnidadId = obra.AperturaProgramaticaUnidadId;
        //        poadetalle.NumeroBeneficiarios = obra.NumeroBeneficiarios;
        //        poadetalle.CantidadUnidades = obra.CantidadUnidades;
        //        poadetalle.Empleos = obra.Empleos;
        //        poadetalle.Jornales = obra.Jornales;

        //        poadetalle.FuncionalidadId = obra.FuncionalidadId;
        //        poadetalle.EjeId = obra.EjeId;
        //        poadetalle.PlanSectorialId = obra.PlanSectorialId;
        //        poadetalle.ModalidadId = obra.ModalidadId;
        //        poadetalle.ProgramaId = obra.ProgramaId;
        //        poadetalle.GrupoBeneficiarioId = obra.GrupoBeneficiarioId;


        //        poadetalle.SituacionObraId = obra.SituacionObraId;
        //        poadetalle.NumeroAnterior = obra.NumeroAnterior;
        //        poadetalle.ImporteLiberadoEjerciciosAnteriores = obra.ImporteLiberadoEjerciciosAnteriores;
        //        poadetalle.ModalidadObra = obra.ModalidadObra;               
        //        poadetalle.Observaciones = obra.Observaciones;
        //        poadetalle.Extemporanea = true;
        //        poadetalle.POA = poa;                
                
        //        obra.POA = poa;
        //        obra.POADetalle = poadetalle;
        //        uow.ObraBusinessLogic.Insert(obra);

        //    }
        //    else
        //    {
        //        uow.ObraBusinessLogic.Update(obra);
        //    } 

        //    uow.SaveChanges();

        //    if (uow.Errors.Count == 0)
        //    {

        //        // Esto solo es necesario para recargar en memoria
        //        // los cambios que se realizan automaticamente en la base de datos 
        //        // mediante un trigger de inserción
        //        uow = null;
        //        uow = new UnitOfWork(Session["IdUser"].ToString());

        //        this.GridViewObras.DataBind();

        //        divEdicion.Style.Add("display", "none");
        //        divBtnNuevo.Style.Add("display", "block");

        //    }
        //    else
        //    {

        //        divEdicion.Style.Add("display", "none");
        //        divBtnNuevo.Style.Add("display", "block");
        //        divMsg.Style.Add("display", "block");

        //        msg = string.Empty;
        //        foreach (string cad in uow.Errors)
        //            msg += cad;

        //        lblMensajes.Text = msg;

        //    }

        //}

        [WebMethod(EnableSession = true)]
        public static object GetTituloPagina()
        {

            UnitOfWork uow = new UnitOfWork();

            int unidadpresupuestalId = Utilerias.StrToInt(HttpContext.Current.Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(HttpContext.Current.Session["EjercicioId"].ToString());

            UnidadPresupuestal unidadpresupuestal = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);
            Ejercicio ejercicio = uow.EjercicioBusinessLogic.GetByID(ejercicioId);

            return String.Format("{0}<br />Proyecto de POA ajustado en el ejercicio {1}", unidadpresupuestal.Nombre, ejercicio.Año); ;

        }

       

        [WebMethod]
        public static object GetRecord(int id)
        {

            UnitOfWork uow = new UnitOfWork();

            Obra obra = uow.ObraBusinessLogic.GetByID(id);

            return new { Id=obra.Id, Numero=obra.Numero,Descripcion=obra.Descripcion,MunicipioId=obra.MunicipioId,LocalidadId=obra.LocalidadId,CriterioPriorizacionId=obra.CriterioPriorizacionId,Convenio=obra.Convenio,ProgramaId=obra.AperturaProgramatica.Parent.ParentId,SubprogramaId=obra.AperturaProgramatica.ParentId,SubsubprogramaId=obra.AperturaProgramaticaId,UnidadMedidaId=obra.AperturaProgramaticaUnidadId,CantidadUnidades=obra.CantidadUnidades,NumeroBeneficiarios=obra.NumeroBeneficiarios,Empleos=obra.Empleos,Jornales=obra.Jornales,SituacionObraId=obra.SituacionObraId,CostoTotal=obra.GetCostoTotal(),PresupuestoEjercicio=obra.GetImporteAsignado(),NumeroAnterior=obra.NumeroAnterior,ImporteLiberadoEjerciciosAnteriores=obra.ImporteLiberadoEjerciciosAnteriores,ModalidadId=obra.ModalidadObra,FuncionalidadId=obra.FuncionalidadId,EjeId=obra.EjeId,PlanSectorialId=obra.PlanSectorialId,ModalidadPVDId=obra.ModalidadId,ProgramaPVDId=obra.ProgramaId,GrupoBeneficiarioId=obra.GrupoBeneficiarioId };

        }

        [WebMethod]
        public static IQueryable GetUnidadesPresupuestales()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.UnidadPresupuestalBusinessLogic.Get().Select(e => new { Id = e.Id, Nombre = e.Nombre, ParentId = e.ParentId });
            return list;
        }

        [WebMethod]
        public static IQueryable GetMunicipios()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.MunicipioBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetLocalidades()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.LocalidadBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden,MunicipioId=e.MunicipioId });
            return list;
        }

        [WebMethod]
        public static IQueryable GetCriteriosPriorizacion()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.CriterioPriorizacionBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetAperturaProgramatica()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.AperturaProgramaticaBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden,ParentId=e.ParentId,Nivel=e.Nivel });
            return list;
        }

        [WebMethod]
        public static IQueryable GetUnidadesMedida()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.AperturaProgramaticaUnidadBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetSituacionesObra()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.SituacionObraBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static object GetModalidadesEjecucion()
        {          
            List<object> list = new List<object>();

            list.Add(new { Id = 1, Nombre="Contrato"});
            list.Add(new { Id = 2, Nombre = "Administración directa" });
            list.Add(new { Id = 3, Nombre = "Mixta" });

            return list;

        }

        [WebMethod]
        public static IQueryable GetFondos()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.FondoBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Abreviatura = e.Abreviatura, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetFuncionalidades()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.FuncionalidadBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Nivel=e.Nivel,Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetEjes()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.EjeBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Nivel = e.Nivel, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetPlanesSectoriales()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.PlanSectorialBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Nivel = e.Nivel, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetModalidades()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.ModalidadBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Nivel = e.Nivel, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetProgramasPVD()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.ProgramaBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Descripcion = e.Descripcion, Orden = e.Orden });
            return list;
        }

        [WebMethod]
        public static IQueryable GetGrupoBeneficiarios()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.GrupoBeneficiarioBusinessLogic.Get().Select(e => new { Id = e.Id, Clave = e.Clave, Nombre = e.Nombre, Orden = e.Orden });
            return list;
        }


          [WebMethod(EnableSession = true)]
        public static object GetListadoObras(string WhereNumero, string WhereDescripcion, string WhereMunicipio, string WhereLocalidad, string WhereUnidadPresupuestal, string WhereContratista, string WhereFondos, string WherePresupuesto)
        {
            UnitOfWork uow = new UnitOfWork();
            SqlDataReader reader =null;

            int unidadpresupuestalId = Utilerias.StrToInt(HttpContext.Current.Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(HttpContext.Current.Session["EjercicioId"].ToString());

            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();


            List<object> list = new List<object>();

            using (SqlConnection con = new SqlConnection(uow.Contexto.Database.Connection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_MulticriterioPOAAjustado", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@POAId", SqlDbType.Int).Value = poa.Id;
                    cmd.Parameters.Add("@WhereNumero", SqlDbType.VarChar).Value = WhereNumero;
                    cmd.Parameters.Add("@WhereDescripcion", SqlDbType.VarChar).Value = WhereDescripcion;
                    cmd.Parameters.Add("@WhereMunicipio", SqlDbType.VarChar).Value = WhereMunicipio;
                    cmd.Parameters.Add("@WhereLocalidad", SqlDbType.VarChar).Value = WhereLocalidad;
                    cmd.Parameters.Add("@WhereUnidadPresupuestal", SqlDbType.VarChar).Value = WhereUnidadPresupuestal;                    
                    cmd.Parameters.Add("@WhereContratista", SqlDbType.VarChar).Value = WhereContratista;
                    cmd.Parameters.Add("@WhereFondos", SqlDbType.VarChar).Value = WhereFondos;
                    cmd.Parameters.Add("@WherePresupuesto", SqlDbType.VarChar).Value = WherePresupuesto;
                    

                    con.Open();

                    reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {


                            int ObraId = Convert.ToInt32(reader["ObraId"]);
                            string Numero = reader["Numero"].ToString();
                            string Descripcion = reader["Descripcion"].ToString();
                            string Municipio = reader["Municipio"].ToString();
                            string Localidad = reader["Localidad"].ToString();
                            string UnidadPresupuestal = reader["UnidadPresupuestal"].ToString();
                            string SubUnidadPresupuestal = reader["SubUnidadPresupuestal"].ToString();
                            string Contratista = reader["Contratista"].ToString();
                            string Fondos = reader["Fondos"].ToString();
                            decimal Presupuesto = Convert.ToDecimal(reader["Presupuesto"]);

                            list.Add(new { ObraId = ObraId, Numero = Numero, Descripcion = Descripcion, Municipio = Municipio, Localidad = Localidad, UnidadPresupuestal = UnidadPresupuestal,SubUnidadPresupuestal=SubUnidadPresupuestal, Contratista = Contratista, Fondos = Fondos, Presupuesto = Presupuesto });

                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                    }
                    reader.Close();

                } //using command

            }//using connection

            return list;

        }


        [WebMethod(EnableSession = true)]
        public static object UpdateRecord(RegistroObra registro)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());
           
            Obra obra = uow.ObraBusinessLogic.GetByID(registro.Id);

            obra.Numero = registro.Numero;
            obra.Descripcion = registro.Descripcion;
            obra.MunicipioId = registro.MunicipioId;
            obra.LocalidadId = registro.LocalidadId;
            obra.CriterioPriorizacionId = registro.CriterioPriorizacionId;
            obra.Convenio = registro.Convenio;
            obra.AperturaProgramaticaId = registro.AperturaProgramaticaId;
            obra.AperturaProgramaticaUnidadId = registro.UnidadMedidaId;
            obra.CantidadUnidades = registro.CantidadUnidades;
            obra.NumeroBeneficiarios = registro.NumeroBeneficiarios;
            obra.Empleos = registro.Empleos;
            obra.Jornales = registro.Jornales;
            obra.SituacionObraId = registro.SituacionObraId;
            obra.NumeroAnterior = registro.NumeroAnterior;
            obra.ImporteLiberadoEjerciciosAnteriores = registro.ImporteLiberadoEjerciciosAnteriores;
            obra.ModalidadObra = (enumModalidadObra)registro.ModalidadEjecucionId;           

            if (registro.FuncionalidadId == 0)
            {
                obra.FuncionalidadId = null;
            }
            else
            {
                obra.FuncionalidadId = registro.FuncionalidadId;
            };

            if (registro.EjeId == 0)
            {
                obra.EjeId = null;
            }
            else
            {
                obra.EjeId = registro.EjeId;
            };

            if (registro.PlanSectorialId == 0)
            {
                obra.PlanSectorialId = null;
            }
            else
            {
                obra.PlanSectorialId = registro.PlanSectorialId;
            };

            if (registro.ModalidadPVDId == 0)
            {
                obra.ModalidadId = null;
            }
            else
            {
                obra.ModalidadId = registro.ModalidadPVDId;
            };

            if (registro.ProgramaPVDId == 0)
            {
                obra.ProgramaId = null;
            }
            else
            {
                obra.ProgramaId = registro.ProgramaPVDId;
            };

            if (registro.GrupoBeneficiarioId == 0)
            {
                obra.GrupoBeneficiarioId = null;
            }
            else
            {
                obra.GrupoBeneficiarioId = registro.GrupoBeneficiarioId;
            };

            uow.ObraBusinessLogic.Update(obra);                       

            uow.SaveChanges();

            return uow.GetResult();
        }


        [WebMethod(EnableSession = true)]
        public static object AddRecord(RegistroObra registro)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());

            int unidadpresupuestalId = Utilerias.StrToInt(HttpContext.Current.Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(HttpContext.Current.Session["EjercicioId"].ToString());
          
            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();
            POADetalle poadetalle = null;          

            if (poa == null)
            {
                poa = new DataAccessLayer.Models.POA();
                poa.UnidadPresupuestalId = unidadpresupuestalId;
                poa.EjercicioId = ejercicioId;
            }


            Obra obra = new Obra();

            obra.Numero = registro.Numero;
            obra.Descripcion = registro.Descripcion;
            obra.MunicipioId = registro.MunicipioId;
            obra.LocalidadId = registro.LocalidadId;
            obra.CriterioPriorizacionId = registro.CriterioPriorizacionId;
            obra.Convenio = registro.Convenio;
            obra.AperturaProgramaticaId = registro.AperturaProgramaticaId;
            obra.AperturaProgramaticaUnidadId = registro.UnidadMedidaId;
            obra.CantidadUnidades = registro.CantidadUnidades;
            obra.NumeroBeneficiarios = registro.NumeroBeneficiarios;
            obra.Empleos = registro.Empleos;
            obra.Jornales = registro.Jornales;
            obra.SituacionObraId = registro.SituacionObraId;
            obra.NumeroAnterior = registro.NumeroAnterior;
            obra.ImporteLiberadoEjerciciosAnteriores = registro.ImporteLiberadoEjerciciosAnteriores;
            obra.ModalidadObra = (enumModalidadObra)registro.ModalidadEjecucionId;

            if(registro.FuncionalidadId==0)
            {
                obra.FuncionalidadId =null;
            }else
            {
                obra.FuncionalidadId =registro.FuncionalidadId;
            };

            if (registro.EjeId == 0)
            {
                obra.EjeId = null;
            }
            else
            {
                obra.EjeId = registro.EjeId;
            };

            if (registro.PlanSectorialId == 0)
            {
                obra.PlanSectorialId = null;
            }
            else
            {
                obra.PlanSectorialId = registro.PlanSectorialId;
            };

            if (registro.ModalidadPVDId == 0)
            {
                obra.ModalidadId = null;
            }
            else
            {
                obra.ModalidadId = registro.ModalidadPVDId;
            };

            if (registro.ProgramaPVDId == 0)
            {
                obra.ProgramaId = null;
            }
            else
            {
                obra.ProgramaId = registro.ProgramaPVDId;
            };

            if (registro.GrupoBeneficiarioId == 0)
            {
                obra.GrupoBeneficiarioId = null;
            }
            else
            {
                obra.GrupoBeneficiarioId = registro.GrupoBeneficiarioId;
            };
      

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

            uow.SaveChanges();

            return uow.GetResult();

        }

        [WebMethod(EnableSession = true)]
        public static object DeleteRecord(int id)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());

            Obra obra = uow.ObraBusinessLogic.GetByID(id);

            uow.ObraBusinessLogic.Delete(obra);

            uow.SaveChanges();

            return uow.GetResult();
        }




    } //partial


          

    public class RegistroObra 
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public int MunicipioId { get; set; }
        public int LocalidadId { get; set; }
        public int CriterioPriorizacionId { get; set; }
        public string Convenio { get; set; }
        public int AperturaProgramaticaId { get; set; }        
        public int UnidadMedidaId { get; set; }
        public int CantidadUnidades { get; set; }
        public int NumeroBeneficiarios { get; set; }
        public int Empleos { get; set; }
        public int Jornales { get; set; }
        public int SituacionObraId { get; set; }
        public string NumeroAnterior { get; set; }
        public decimal ImporteLiberadoEjerciciosAnteriores { get; set; }       
        public int ModalidadEjecucionId { get; set; }
        public int FuncionalidadId { get; set; }
        public int EjeId { get; set; }
        public int PlanSectorialId { get; set; }
        public int ModalidadPVDId { get; set; }
        public int ProgramaPVDId { get; set; }
        public int GrupoBeneficiarioId { get; set; }

    }





} //namespace
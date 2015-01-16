using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class EvaluacionPOA_ : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork(Session["IdUser"].ToString());

            if (!IsPostBack)
            {
                string M = string.Empty;
                int POADetalleID = Request.QueryString["pd"] != null ? Utilerias.StrToInt(Request.QueryString["pd"].ToString()) : 0;
                int idObra = Request.QueryString["ob"] != null ? Utilerias.StrToInt(Request.QueryString["ob"].ToString()) : 0; //Se recupera el id del objeto OBRA            
                _IDPOADetalle.Value = POADetalleID.ToString();
                _URLVisor.Value = ResolveClientUrl("~/rpts/wfVerReporte.aspx");

                BindControlesObra(idObra, POADetalleID);

                CargarPreguntasPlantillas(POADetalleID);

                BindGridsEvaluacion();

                CargarPreguntasPlantillasEjecucion(POADetalleID);

                BindGridsEvaluacionEjecucion();

            }

        }

        private void GetPlantillasEjecucion(int idPOADetalle)
        {
           var list = (from pl1 in uow.PlantillaBusinessLogic.Get()
                        join pl2 in uow.PlantillaBusinessLogic.Get()
                        on pl1.Id equals pl2.Id
                        select new { PlantillaID = pl1.Id, Padre = pl1.Padre, Descripcion=pl1.Descripcion }).Where(e => e.Padre.Orden == 3);

            foreach (var item in list)
            {

            } 
        }


        #region METODOS

        private void BindGridIndividual(int numGrid, POAPlantillaDetalle obj)
        {
            POAPlantilla poaPlantilla = uow.POAPlantillaBusinessLogic.GetByID(obj.POAPlantillaId);

            switch (numGrid)
            {
                //ETAPA DE PLANEACION

                case 1: //Plan de Desarrollo Estatal Urbano
                    gridPlanDesarrollo.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridPlanDesarrollo.DataBind();
                    break;

                case 2: //Anteproyecto y normatividad
                    gridAnteproyecto.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridAnteproyecto.DataBind();
                    break;

                case 3: //Fondo y programa
                    gridFondoPrograma.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridFondoPrograma.DataBind();
                    break;

                case 4: //Proyecto Ejecutivo y Proyecto Base
                    gridProyectoEjecutivo.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridProyectoEjecutivo.DataBind();
                    break;

                case 5: //Adjudicacion Directa
                    gridAdjuDirecta.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridAdjuDirecta.DataBind();
                    break;

                case 6: //Adjudicacion por excepcion de ley
                    gridExcepcion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridExcepcion.DataBind();
                    break;

                case 7: //Invitacion a cuando menos tres personas
                    gridInvitacion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridInvitacion.DataBind();
                    break;

                case 8: //Licitacion Pública
                    gridLicitacion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridLicitacion.DataBind();
                    break;

                case 9: //Presupuesto Autorizado Contrato
                    gridPresupuesto.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridPresupuesto.DataBind();
                    break;

                case 10: //Administración Directa
                    gridAdmin.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridAdmin.DataBind();
                    break;

                //ETAPA DE EJECUCION

                case 11: //Control técnico financiero
                    gridTecnicoFinanciero.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridTecnicoFinanciero.DataBind();
                    break;
                case 12: // Bitácora electrónica - convencional
                    gridBitacora.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridBitacora.DataBind();
                    break;
                case 13: // Supervisión y estimaciones
                    gridEstimaciones.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridEstimaciones.DataBind();
                    break;
                case 14: // Convenios prefiniquitos
                    gridConvenios.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridConvenios.DataBind();
                    break;
                case 15: // Finiquito
                    gridFiniquito.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridFiniquito.DataBind();
                    break;
                case 16: // Acta entrega recepción
                    gridEntrega.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridEntrega.DataBind();
                    break;
                case 17: // Documentación de gestión de recursos
                    gridGestion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                    gridGestion.DataBind();
                    break;

            }

        }
        private void BindGridsEvaluacion()
        {
            List<Plantilla> listPlantillas = uow.PlantillaBusinessLogic.Get(e => e.DependeDe.Orden == 2).ToList(); //PLANTILLAS DE LA ETAPA DE PLANEACION
            POAPlantilla poaPlantilla;
            int POADetalleID = Utilerias.StrToInt(_IDPOADetalle.Value);

            foreach (Plantilla p in listPlantillas)
            {
                poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == POADetalleID && e.PlantillaId == p.Id).FirstOrDefault();


                switch (p.Orden)
                {
                    case 1: //Plan de Desarrollo Estatal Urbano
                        gridPlanDesarrollo.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridPlanDesarrollo.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoPD.Checked = poaPlantilla.Aprobado;
                        break;

                    case 2: //Anteproyecto y normatividad
                        gridAnteproyecto.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridAnteproyecto.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoAnteproyecto.Checked = poaPlantilla.Aprobado;
                        break;

                    case 3: //Fondo y programa
                        gridFondoPrograma.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridFondoPrograma.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoFondoPrograma.Checked = poaPlantilla.Aprobado;
                        break;

                    case 4: //Proyecto Ejecutivo y Proyecto Base
                        gridProyectoEjecutivo.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridProyectoEjecutivo.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoProyectoEjecutivo.Checked = poaPlantilla.Aprobado;
                        break;

                    case 5: //TIpo de Adjudicacion

                        #region TIPO DE ADJUDICACION

                        if (p.Detalles.Count > 0)
                        {
                            foreach (Plantilla pd in p.Detalles)
                            {
                                poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == POADetalleID && e.PlantillaId == pd.Id).FirstOrDefault();

                                switch (pd.Orden)
                                {
                                    case 1: //Adjudicacion Directa
                                        gridAdjuDirecta.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                                        gridAdjuDirecta.DataBind();

                                        //Se Bindea el check de APROBADO
                                        chkAprobadoAdjuDirecta.Checked = poaPlantilla.Aprobado;

                                        if (pd.Detalles.Count > 0)
                                        {
                                            Plantilla temp = pd.Detalles.ElementAt(0);

                                            //Adjudicacion por excepcion de ley
                                            poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == POADetalleID && e.PlantillaId == temp.Id).FirstOrDefault();
                                            gridExcepcion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                                            gridExcepcion.DataBind();

                                            //Se Bindea el check de APROBADO
                                            chkAprobadoExcepcion.Checked = poaPlantilla.Aprobado;

                                            poaPlantilla = null;
                                            temp = null;
                                            temp = pd.Detalles.ElementAt(1);

                                            //Invitacion a cuando menos tres personas
                                            poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == POADetalleID && e.PlantillaId == temp.Id).FirstOrDefault();
                                            gridInvitacion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                                            gridInvitacion.DataBind();

                                            //Se Bindea el check de APROBADO
                                            chkAprobadoInvitacion.Checked = poaPlantilla.Aprobado;
 
                                        }

                                        break;
                                    case 2: //Licitacion Pública
                                        gridLicitacion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                                        gridLicitacion.DataBind();

                                        //Se Bindea el check de APROBADO
                                        chkAprobadoLicitacion.Checked = poaPlantilla.Aprobado;
                                        break;
                                }
                            }

                        }

                        #endregion

                        break;

                    case 6: //Presupuesto Autorizado Contrato
                        gridPresupuesto.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridPresupuesto.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoPresupuesto.Checked = poaPlantilla.Aprobado;
                        
                        break;

                    case 7: //Administración Directa
                        gridAdmin.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridAdmin.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoAdmin.Checked = poaPlantilla.Aprobado;
                        break;
                }

            }




        }

        private void BindGridsEvaluacionEjecucion()
        {
            POAPlantilla poaPlantilla;
            int POADetalleID = Utilerias.StrToInt(_IDPOADetalle.Value);

            var list = (from pl1 in uow.PlantillaBusinessLogic.Get()
                        join pl2 in uow.PlantillaBusinessLogic.Get()
                        on pl1.Id equals pl2.Id
                        select new { PlantillaID = pl1.Id, Padre = pl1.Padre, Descripcion = pl1.Descripcion, Orden=pl1.Orden }).Where(e => e.Padre.Orden == 3);

            foreach (var item in list)
            {
                poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == POADetalleID && e.PlantillaId == item.PlantillaID).FirstOrDefault();

                if (poaPlantilla.Detalles == null)
                    continue;
                else if (poaPlantilla.Detalles.Count==0)
                    continue;

                switch (item.Orden)
                {
                    case 1: //Control técnico financiero
                        gridTecnicoFinanciero.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridTecnicoFinanciero.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoTecnicoFinanciero.Checked = poaPlantilla.Aprobado;
                        break;
                    case 2: // Bitácora electrónica - convencional
                        gridBitacora.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridBitacora.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoBitacora.Checked = poaPlantilla.Aprobado;
                        break;
                    case 3: // Supervisión y estimaciones
                        gridEstimaciones.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridEstimaciones.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoEstimaciones.Checked = poaPlantilla.Aprobado;
                        break;
                    case 4: // Convenios prefiniquitos
                        gridConvenios.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridConvenios.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoConvenios.Checked = poaPlantilla.Aprobado;
                        break;
                    case 5: // Finiquito
                        gridFiniquito.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridFiniquito.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoFiniquito.Checked = poaPlantilla.Aprobado;
                        break;
                    case 6: // Acta entrega recepción
                        gridEntrega.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridEntrega.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoEntrega.Checked = poaPlantilla.Aprobado;
                        break;
                    case 7: // Documentación de gestión de recursos
                        gridGestion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridGestion.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoGestion.Checked = poaPlantilla.Aprobado;
                        break;
                }
            }

        }

        /// <summary>
        /// Metodo que se encarga de clonar las plantillas de la etapa de Planeacion
        /// </summary>
        /// <param name="POADetalleID"></param>
        private void CargarPreguntasPlantillas(int POADetalleID)
        {

            //List<Plantilla> listPlantillas = uow.PlantillaBusinessLogic.Get(e => e.DependeDe.Orden == 2).ToList(); //PLANTILLAS DE LA ETAPA DE PLANEACION
            var list = (from pl1 in uow.PlantillaBusinessLogic.Get()
                        join pl2 in uow.PlantillaBusinessLogic.Get()
                        on pl1.Id equals pl2.Id
                        select new { PlantillaID = pl1.Id, Padre = pl1.Padre, Descripcion = pl1.Descripcion }).Where(e => e.Padre.Orden == 2);
            POAPlantilla poaPlantilla = null;
            string M;

            foreach (var item in list)
            {
                Plantilla p = uow.PlantillaBusinessLogic.GetByID(item.PlantillaID);

                poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.PlantillaId == p.Id && e.POADetalleId == POADetalleID).FirstOrDefault(); //Se recupera POAPlantilla

                if (poaPlantilla == null) //Si no existe ningun objeto con la plantilla creada, entonces se procede a clonar la plantilla
                    M = CopiarPlantilla(p);
                else
                    M = CrearPreguntasInexistentes(poaPlantilla.Id, p.Id);
            }

        }


        private void CargarPreguntasPlantillasEjecucion(int POADetalleID)
        {

            var list = (from pl1 in uow.PlantillaBusinessLogic.Get()
                        join pl2 in uow.PlantillaBusinessLogic.Get()
                        on pl1.Id equals pl2.Id
                        select new { PlantillaID = pl1.Id, Padre = pl1.Padre, Descripcion = pl1.Descripcion }).Where(e => e.Padre.Orden == 3);


            POAPlantilla poaPlantilla = null;
            string M;

            foreach (var item in list)
            {
                Plantilla p = uow.PlantillaBusinessLogic.GetByID(item.PlantillaID);
                poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.PlantillaId == p.Id && e.POADetalleId == POADetalleID).FirstOrDefault(); //Se recupera POAPlantilla

                if (poaPlantilla == null) //Si no existe ningun objeto con la plantilla creada, entonces se procede a clonar la plantilla
                    M = CopiarPlantilla(p);

            }

        }



        private string CrearPreguntasInexistentes(int POAPlantillaID, int plantillaID)
        {
            //int ordenPlantilla = Utilerias.StrToInt(Request.QueryString["o"].ToString());
            string M = string.Empty;

            //Consulta que hace la diferencia de preguntas entre POAPLANTILLADETALLE y PLANTILLADETALLE
            var list = (from p in uow.PlantillaDetalleBusinessLogic.Get()
                        join pp in uow.PlantillaBusinessLogic.Get(e => e.Id == plantillaID).ToList()
                        on p.PlantillaId equals pp.Id
                        join po in uow.POAPlantillaDetalleBusinessLogic.Get()
                        on p.Id equals po.PlantillaDetalleId into temp
                        from pi in temp.DefaultIfEmpty()
                        select new { p.Id, pp.Orden, pregunta = (pi == null ? 0 : pi.PlantillaDetalleId) });


            foreach (var obj in list)
            {
                if (obj.pregunta == 0)
                {
                    POAPlantillaDetalle objPOAPlantillaDetalle = new POAPlantillaDetalle();
                    objPOAPlantillaDetalle.POAPlantillaId = POAPlantillaID; //ID del obj POAPlantilla
                    objPOAPlantillaDetalle.PlantillaDetalleId = obj.Id; //Id del obj Plantilla detalle


                    uow.POAPlantillaDetalleBusinessLogic.Insert(objPOAPlantillaDetalle);
                    uow.SaveChanges();

                    //Si hubo errores
                    if (uow.Errors.Count > 0)
                    {
                        M = string.Empty;
                        foreach (string cad in uow.Errors)
                            M += cad;

                        return M;
                    }
                }
            }


            Plantilla objPlantilla = uow.PlantillaBusinessLogic.GetByID(plantillaID);

            if (objPlantilla.Detalles.Count > 0)
            {
                foreach (Plantilla p in objPlantilla.Detalles)
                {
                    CrearPreguntasInexistentes(POAPlantillaID, p.Id);
                }
            }



            return M;

        }
        private string CopiarPlantilla(Plantilla p)
        {

            string M = string.Empty;

            M = ImportarPlantilla(p); //SE CREAN LOS POAPLANTILLAS

            if (!M.Equals(string.Empty))
                return M;


            return M;
        }
        private string ImportarPlantilla(Plantilla p)
        {
            string M = string.Empty;
            int IDPoaDetalle = Utilerias.StrToInt(_IDPOADetalle.Value);
            //Se obtiene el OBJ de la PLANTILLA PADRE QUE SE ELIGIO
            Plantilla objPlantilla = p;

            #region CREAR EN PRIMERA INSTANCIA POAPLANTILLA CON DATOS DE LA PLANTILLA PADRE QUE SE ELIGIO
            //Se crea el OBJ de POAPlantilla, con los datos del la plantilla PADRE que se obtuvo
            POAPlantilla obj = new POAPlantilla();
            obj.PlantillaId = objPlantilla.Id;
            obj.POADetalleId = IDPoaDetalle;



            //SE ALMACENA EL POAPLANTILLA
            uow.POAPlantillaBusinessLogic.Insert(obj);
            uow.SaveChanges();

            //Si hubo errores
            if (uow.Errors.Count > 0)
            {
                M = string.Empty;
                foreach (string cad in uow.Errors)
                    M += cad;

                return M;
            }

            //Se crea detalle de preguntas en POAPLANTILLADETALLE 
            M = ImportarDetallePlantilla(objPlantilla.DetallePreguntas.ToList(), obj.Id);

            //Si hubo errores
            if (!M.Equals(string.Empty))
                return M;


            if (objPlantilla.Detalles.Count > 0)
            {
                foreach (Plantilla pl in objPlantilla.Detalles)
                {
                    ImportarPlantilla(pl);
                }
            }

            #endregion

            return M;
        }
        private string ImportarDetallePlantilla(List<PlantillaDetalle> DetallePreguntas, int IDPOAPlantilla)
        {

            string M = string.Empty;

            //Se crean OBJs de POAPlantillaDetalle, que es la que almacenara todas las preguntas de la plantilladetalle
            //Se recorre el detalle de preguntas que trae la plantilladetalle
            foreach (PlantillaDetalle pregunta in DetallePreguntas)
            {
                POAPlantillaDetalle objPOAPlantillaDetalle = new POAPlantillaDetalle();
                objPOAPlantillaDetalle.POAPlantillaId = IDPOAPlantilla; //ID del obj POAPlantilla
                objPOAPlantillaDetalle.PlantillaDetalleId = pregunta.Id; //Id del obj Plantilla detalle

                //Se agregan mas datos, los de bitacora....pendiente

                uow.POAPlantillaDetalleBusinessLogic.Insert(objPOAPlantillaDetalle);
                uow.SaveChanges();

                //Si hubo errores
                if (uow.Errors.Count > 0)
                {
                    M = string.Empty;
                    foreach (string cad in uow.Errors)
                        M += cad;

                    return M;
                }

            }

            return M;
        }

        /// <summary>
        /// Metodo encargado de bindear los datos de un POADetalle
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// <param name="idPOADetalle"></param>
        private void BindControlesObra(int idObra, int idPoaDetalle)
        {
            Obra obra;
            POADetalle poaDetalle;

            if (idObra != 0)
            {

                obra = uow.ObraBusinessLogic.GetByID(idObra);

                //Se bindean con los datos de la OBRA, es decir, viene de POA AJUSTADO
                txtDescripcion.Value = obra.Descripcion;
                txtMunicipio.Value = obra.Municipio.Nombre;
                txtNumero.Value = obra.Numero;
                txtLocalidad.Value = obra.Localidad.Nombre;
                //txtObservacion.Value = obra.Observaciones;
                _IDPOADetalle.Value = obra.POADetalleId.ToString(); //Se coloca el id de POADETALLE, por que sobre ese obj es donde se realizara la EVALUACION DE PLANTILLAS

            }
            else if (idPoaDetalle != 0)
            {  //Quiere decir que se esta abriendo desde ANTEPROYECTO DE POA

                poaDetalle = uow.POADetalleBusinessLogic.GetByID(idPoaDetalle);
                txtFecha.Value = DateTime.Now.ToShortDateString();
                //Se bindean los datos directamente de POADETALLE
                txtDescripcion.Value = poaDetalle.Descripcion;
                txtMunicipio.Value = poaDetalle.Municipio.Nombre;
                txtNumero.Value = poaDetalle.Numero;
                txtLocalidad.Value = poaDetalle.Localidad.Nombre;
                //txtObservacion.Value = poaDetalle.Observaciones;
                _IDPOADetalle.Value = idPoaDetalle.ToString();

                 ColocarEncabezadoObra(idPoaDetalle);

            }

        }



        private void ColocarEncabezadoObra(int PoaDetalleID)
        {
            int idUnidad = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(idUnidad);

            var objApertura = (from pd in uow.POADetalleBusinessLogic.Get(e => e.Id == PoaDetalleID)
                               join ap3 in uow.AperturaProgramaticaBusinessLogic.Get()
                               on pd.AperturaProgramaticaId equals ap3.Id
                               join ap2 in uow.AperturaProgramaticaBusinessLogic.Get()
                               on ap3.ParentId equals ap2.Id
                               join ap1 in uow.AperturaProgramaticaBusinessLogic.Get()
                               on ap2.ParentId equals ap1.Id
                               select new { programa = ap1.Nombre, subprograma = ap2.Nombre });

            txtEntidad.Value = up.Nombre;

            foreach (var item in objApertura)
            {
                txtPrograma.Value = item.programa;
                txtSubprograma.Value = item.subprograma;
            }


            Obra obra = uow.ObraBusinessLogic.Get(e => e.POADetalleId == PoaDetalleID).FirstOrDefault();
            POADetalle poaDetalle = uow.POADetalleBusinessLogic.Get(e => e.Id == PoaDetalleID).FirstOrDefault();

            if (obra != null)
            {
                ObraFinanciamiento obraFinan = uow.ObraFinanciamientoBusinessLogic.Get(e => e.ObraId == obra.Id).FirstOrDefault();

                TechoFinancieroUnidadPresupuestal tfu = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(e => e.Id == obraFinan.TechoFinancieroUnidadPresupuestalId).FirstOrDefault();

                TechoFinanciero tf = uow.TechoFinancieroBusinessLogic.Get(e => e.Id == tfu.TechoFinancieroId).FirstOrDefault();

                Financiamiento fin = uow.FinanciamientoBusinessLogic.Get(e => e.Id == tf.FinanciamientoId).FirstOrDefault();

                ModalidadFinanciamiento mf = uow.ModalidadFinanciamientoBusinessLogic.Get(e => e.Id == fin.ModalidadFinanciamientoId).FirstOrDefault();

                Fondo fondo = uow.FondoBusinessLogic.Get(e => e.Id == fin.FondoId).FirstOrDefault();

                FondoLineamientos fl = uow.FondoLineamientosBL.Get(e => e.FondoId == fondo.Id).FirstOrDefault();

                Año año = uow.AñoBusinessLogic.Get(e => e.Id == fin.AñoId).FirstOrDefault();

                txtFondo.Value = fondo != null && mf != null && año != null ? fondo.Abreviatura + "-" + mf.Nombre + " (" + año.Anio + ")" : string.Empty;
                txtTecho.Value = tfu != null ? tfu.Importe.ToString("c") : string.Empty;
                txtLineamiento.Value = fl != null ? fl.TipoDeObrasYAcciones : string.Empty;
                txtNormatividad.Value = fl != null ? fl.NormatividadAplicable : string.Empty;
                txtImporte.Value = obraFinan != null ? obraFinan.Importe.ToString("c") : string.Empty;

            }
        }

        private void RowDataBound(int id, int numGrid, GridViewRowEventArgs e)
        {
            HtmlInputCheckBox chkRequiere = (HtmlInputCheckBox)e.Row.FindControl("chkRequiere");
            HtmlInputCheckBox chkPresento = (HtmlInputCheckBox)e.Row.FindControl("chkPresento");
            ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtnEdit");
            Label lblArchivo = (Label)e.Row.FindControl("lblArchivo");

            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

            if (obj.Respuesta != 0)
            {
                switch (obj.Respuesta) //Segun sea la respuesta que traiga el objeto, se marca el radiobutton correspondiente
                {
                    case enumRespuesta.Si:
                        chkRequiere.Value = "true";
                        chkRequiere.Checked = true;
                        break;
                    case enumRespuesta.No:
                        chkRequiere.Value = "false";
                        chkRequiere.Checked = false;
                        break;

                }
            }

            if (obj.Presento != 0)
            {
                switch (obj.Presento) //Segun sea la respuesta que traiga el objeto, se marca el radiobutton correspondiente
                {
                    case enumPresento.Si:
                        chkPresento.Value = "true";
                        chkPresento.Checked = true;
                        break;
                    case enumPresento.No:
                        chkPresento.Value = "false";
                        chkPresento.Checked = false;
                        break;

                }
            }


            if (obj.NombreArchivo != null)
                if (!obj.NombreArchivo.Equals(string.Empty))
                    lblArchivo.Text = obj.NombreArchivo;
            //    else
            //        lblArchivo.Text = "No existe archivo adjunto";
            //else
            //    lblArchivo.Text = "No existe archivo adjunto";


            if (imgBtnEdit != null)
                imgBtnEdit.Attributes["onclick"] = "fnc_Edicion(" + id + "," + numGrid + ");return false;";



            //Se coloca la fucnion a corespondiente para visualizar el DOCUMENTO ADJUNTO A LA PREGUNTA
            HtmlButton btnVer = (HtmlButton)e.Row.FindControl("btnVer");
            string ruta = ResolveClientUrl("~/AbrirDocto.aspx");
            btnVer.Attributes["onclick"] = "fnc_AbrirArchivo('" + ruta + "'," + id + ")";
        }

        [WebMethod]
        public static List<string> GuardarChecks(string cadValores, bool checkAprobado)
        {
            string M = string.Empty;
            List<string> R = new List<string>();
            string[] primerArray = cadValores.Split('|'); //Se separa la cadena en un arreglo quedando solamente el formato ID:RES:PRES
            UnitOfWork uow = new UnitOfWork();
            POAPlantillaDetalle pregu=null;

            foreach (string pa in primerArray) //Se recorre el primer arreglo
            {
                string[] segundoArrary = pa.Split(':'); //Se separa el primer resultado, ahora por el caracter de = 

                int id = Utilerias.StrToInt(segundoArrary[0]); //la primera posicion del segundo array contiene el ID de la PREGUNTA
                pregu = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

                if (pregu != null)
                {
                    switch (segundoArrary[1]) //Segun la respuesta, se asigna el valor. Esta viene en la segunda posicion del segundo array
                    {
                        case "1":
                            pregu.Respuesta = enumRespuesta.Si;
                            break;
                        case "2":
                            pregu.Respuesta = enumRespuesta.No;
                            break;
                        case "3":
                            pregu.Respuesta = enumRespuesta.NoAplica;
                            break;
                    }


                    switch (segundoArrary[2]) //Segun si PRESENTO o no PRESENTO archivo, se coloca. Viene contenida en la tercera posicion del segundo array
                    {
                        case "1":
                            pregu.Presento = enumPresento.Si;
                            break;
                        case "2":
                            pregu.Presento = enumPresento.No;
                            break;
                    }

                    uow.POAPlantillaDetalleBusinessLogic.Update(pregu);
                    uow.SaveChanges();

                    //Si hubo errores
                    if (uow.Errors.Count > 0)
                    {
                        M = string.Empty;
                        foreach (string cad in uow.Errors)
                            M += cad;
                    }
                }

                //Si hubo errores
                if (!M.Equals(string.Empty))
                {
                    R.Add(string.Empty);
                    R.Add(M);
                    return R;
                }

            }

            //EL uULTIMO OBJETO DE POAPLANTILLADETALLE RECUPERADO, NOS DARA CUAL ES LA PLANTILLA PADRE (POAPLANTILLA)
            //A LA CUAL SE ACTUALIZARA SU CAMPO DE "Aprobado", EL CUAL INDICA SI LA PLANTILLA DE EVALUACION FUE APROBADA POR EL ANALISTA DE SEFIPLAN

            POAPlantilla plantilla = uow.POAPlantillaBusinessLogic.GetByID(pregu.POAPlantillaId);
            plantilla.Aprobado = checkAprobado;

            uow.POAPlantillaBusinessLogic.Update(plantilla);
            uow.SaveChanges();

            //Si hubo errores
            if (uow.Errors.Count > 0)
            {
                M = string.Empty;
                foreach (string cad in uow.Errors)
                    M += cad;
            }


            //Si hubo errores
            if (!M.Equals(string.Empty))
            {
                R.Add(string.Empty);
                R.Add(M);
                return R;
            }

            M = "Se han guardado correctamente los cambios";
            R.Add(M);

            return R;
        }
        private List<string> GuardarChecks()
        {
            string M = string.Empty;
            string cadValores = _CadValoresChecks.Value;
            List<string> R = new List<string>();
            string[] primerArray = cadValores.Split('|'); //Se separa la cadena en un arreglo quedando solamente el formato ID:RES:PRES

            foreach (string pa in primerArray) //Se recorre el primer arreglo
            {
                string[] segundoArrary = pa.Split(':'); //Se separa el primer resultado, ahora por el caracter de = 

                int id = Utilerias.StrToInt(segundoArrary[0]); //la primera posicion del segundo array contiene el ID de la PREGUNTA
                POAPlantillaDetalle pregu = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

                if (pregu != null)
                {
                    switch (segundoArrary[1]) //Segun la respuesta, se asigna el valor. Esta viene en la segunda posicion del segundo array
                    {
                        case "1":
                            pregu.Respuesta = enumRespuesta.Si;
                            break;
                        case "2":
                            pregu.Respuesta = enumRespuesta.No;
                            break;
                        case "3":
                            pregu.Respuesta = enumRespuesta.NoAplica;
                            break;
                    }


                    switch (segundoArrary[2]) //Segun si PRESENTO o no PRESENTO archivo, se coloca. Viene contenida en la tercera posicion del segundo array
                    {
                        case "1":
                            pregu.Presento = enumPresento.Si;
                            break;
                        case "2":
                            pregu.Presento = enumPresento.No;
                            break;
                    }

                    uow.POAPlantillaDetalleBusinessLogic.Update(pregu);
                    uow.SaveChanges();

                    //Si hubo errores
                    if (uow.Errors.Count > 0)
                    {
                        M = string.Empty;
                        foreach (string cad in uow.Errors)
                            M += cad;
                    }
                }

                //Si hubo errores
                if (!M.Equals(string.Empty))
                {
                    R.Add(string.Empty);
                    R.Add(M);
                    return R;
                }

            }

            M = "Se han guardado correctamente los cambios";
            R.Add(M);

            return R;
        }

        /// <summary>
        /// Metodo encargado de almacenar un archivo de soporte en el directorio de ARCHIVOSADJUNTOS para la pregunta de alguna plantilla
        /// Creado por Rigoberto TS
        /// 05/10/2014
        /// </summary>
        /// <param name="postedFile">Archivo que selecciono el usuario</param>
        /// <param name="idPregunta">ID de la pregunta a la que se le va a asociar el archivo</param>
        /// <returns></returns>
        private List<string> GuardarArchivo(HttpPostedFile postedFile, int idPregunta)
        {

            string M = string.Empty;
            string ruta = string.Empty;
            List<string> R = new List<string>();
            try
            {

                ruta = System.Configuration.ConfigurationManager.AppSettings["ArchivosPlantilla"]; //Se recupera nombre de la carpeta del archivo WEB.CONFIG

                if (!ruta.EndsWith("/"))
                    ruta += "/";

                ruta += idPregunta.ToString() + "/"; //Se asigna el ID de la Pregunta

                if (ruta.StartsWith("~") || ruta.StartsWith("/"))   //Es una ruta relativa al sitio
                    ruta = Server.MapPath(ruta);


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta); //Se crea la carpeta

                ruta += postedFile.FileName;

                postedFile.SaveAs(ruta); //Se guarda el archivo

            }
            catch (Exception ex)
            {
                M = ex.Message;
            }

            R.Add(M);
            R.Add(ruta);

            return R;

        }
        private string EliminarArchivo(int id, string nombreArchivo)
        {
            string M = string.Empty;
            try
            {
                string ruta = string.Empty;

                //eliminar archivo
                ruta = System.Configuration.ConfigurationManager.AppSettings["ArchivosPlantilla"];

                if (!ruta.EndsWith("/"))
                    ruta += "/";

                ruta += id.ToString() + "/";

                if (ruta.StartsWith("~") || ruta.StartsWith("/"))   //Es una ruta relativa al sitio
                    ruta = Server.MapPath(ruta);

                File.Delete(ruta + "\\" + nombreArchivo);
                Directory.Delete(ruta);

            }
            catch (Exception ex)
            {
                M = ex.Message;
            }


            return M;
        }

        /// <summary>
        /// WEB METHOD encargado de recuperar informacion de la pregunta cuando se da clic sobre las filas del grid
        /// y colocarla en los controles correspondientes en el CLIENTE
        /// </summary>
        /// <param name="idPregunta">ID de la pregunta que se selecciono del grid</param>
        /// <returns>Retorna una lista con los valores de la pregunta hacia el CLIENTE (JAVASCRIPT)</returns>
        [WebMethod]
        public static List<string> GetValoresPregunta(string idPregunta)
        {
            List<string> R = new List<string>();

            UnitOfWork uow = new UnitOfWork();

            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(Utilerias.StrToInt(idPregunta));

            if (obj != null)
            {
                R.Add(obj.Observaciones != null && obj.Observaciones != string.Empty ? obj.Observaciones : string.Empty);
                R.Add(obj.NombreArchivo != null && obj.NombreArchivo != string.Empty ? obj.NombreArchivo : string.Empty);
                R.Add(obj.PlantillaDetalle.Pregunta);
            }

            return R;

        }

        #endregion


        #region EVENTOS

        protected void gridPlanDesarrollo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridPlanDesarrollo.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridPlanDesarrollo.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 1, e);
                }
            }


        }
        protected void gridAnteproyecto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridAnteproyecto.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridAnteproyecto.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 2, e);
                }
            }
        }
        protected void gridFondoPrograma_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridFondoPrograma.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridFondoPrograma.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 3, e);
                }
            }
        }
        protected void gridProyectoEjecutivo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridProyectoEjecutivo.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridProyectoEjecutivo.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 4, e);
                }
            }
        }
        protected void gridAdjuDirecta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridAdjuDirecta.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridAdjuDirecta.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 5, e);
                }
            }
        }
        protected void gridExcepcion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridExcepcion.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridExcepcion.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 6, e);
                }
            }
        }
        protected void gridInvitacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridInvitacion.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridInvitacion.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 7, e);
                }
            }
        }
        protected void gridLicitacion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridLicitacion.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridLicitacion.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 8, e);
                }
            }
        }
        protected void gridPresupuesto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridPresupuesto.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridPresupuesto.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 9, e);
                }
            }
        }
        protected void gridAdmin_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridAdmin.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridAdmin.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 10, e);
                }
            }
        }
        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            int idPregunta = Utilerias.StrToInt(_IDPregunta.Value);
            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(idPregunta);
            string nomAnterior = string.Empty;
            string M;
            List<string> R = new List<string>();

            nomAnterior = obj.NombreArchivo;

            //Se tiene que almacenar el archivo adjunto, si es que se cargo uno
            if (!fileUpload.PostedFile.FileName.Equals(string.Empty))
            {
                //Validar el tamaño del archivo
                if (fileUpload.FileBytes.Length > 10485296)
                {
                    //divMsgError.Style.Add("display", "block");
                    //divMsgSuccess.Style.Add("display", "none");
                    //lblMsgError.Text = "Se ha excedido en el tamaño del archivo, el máximo permitido es de 10 Mb";

                    return;
                }


                if (nomAnterior != null)
                {
                    if (!nomAnterior.Equals(Path.GetFileName(fileUpload.FileName)))  //Se elimina el archivo anterior
                        if (!nomAnterior.Equals(string.Empty))
                            M = EliminarArchivo(obj.Id, nomAnterior);

                }


                R = GuardarArchivo(fileUpload.PostedFile, idPregunta); //Se guarda el archivo

                ////Si hubo errores
                //if (!R[0].Equals(string.Empty))
                //{
                //    divMsgError.Style.Add("display", "block");
                //    divMsgSuccess.Style.Add("display", "none");
                //    lblMsgError.Text = R[0];
                //    return;
                //}

                //Se guarda el objeto con la informacion del archivo y sus datos correspondientes
                obj.NombreArchivo = Path.GetFileName(fileUpload.FileName);
                obj.TipoArchivo = fileUpload.PostedFile.ContentType;
            }



            obj.Observaciones = txtObservacionesPregunta.Value;
            uow.POAPlantillaDetalleBusinessLogic.Update(obj);
            uow.SaveChanges();

            //Si hubo errores
            if (uow.Errors.Count > 0)
            {
                M = string.Empty;
                foreach (string cad in uow.Errors)
                    M += cad;
                //divMsgError.Style.Add("display", "block");
                //divMsgSuccess.Style.Add("display", "none");
                //lblMsgError.Text = M;
                return;

            }


            //Se tienen que almacenar los checks de cada una de las preguntas que se pudieran
            //haber marcado

            R = null;

            R = GuardarChecks();

            //Si hubo errores
            if (R.Count > 1)
            {

                //divMsgError.Style.Add("display", "block");
                //divMsgSuccess.Style.Add("display", "none");
                //lblMsgError.Text = R[1];

                return;
            }

            M = GuardarCheckAprobadoIndividual(Utilerias.StrToInt(_NumGrid.Value), obj);

            if (!M.Equals(string.Empty))
            {
                return;
            }


            BindGridIndividual(Utilerias.StrToInt(_NumGrid.Value), obj);

            
            ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_AbrirCollapse()", true);
        }

        private string GuardarCheckAprobadoIndividual(int numGrid, POAPlantillaDetalle obj )
        {
            string M = string.Empty;
            bool check = false;
            POAPlantilla poaPlantilla = uow.POAPlantillaBusinessLogic.GetByID(obj.POAPlantillaId);

            switch (numGrid)
            {
                case 1: //Plan de Desarrollo Estatal Urbano
                    check = chkAprobadoPD.Checked;
                    break;

                case 2: //Anteproyecto y normatividad
                    check = chkAprobadoAnteproyecto.Checked;
                    break;

                case 3: //Fondo y programa
                    check = chkAprobadoFondoPrograma.Checked;
                    break;

                case 4: //Proyecto Ejecutivo y Proyecto Base
                    check = chkAprobadoProyectoEjecutivo.Checked;
                    break;

                case 5: //Adjudicacion Directa
                    check = chkAprobadoAdjuDirecta.Checked;
                    break;

                case 6: //Adjudicacion por excepcion de ley
                    check = chkAprobadoExcepcion.Checked;
                    break;

                case 7: //Invitacion a cuando menos tres personas
                    check = chkAprobadoInvitacion.Checked;
                    break;

                case 8: //Licitacion Pública
                    check = chkAprobadoLicitacion.Checked;
                    break;

                case 9: //Presupuesto Autorizado Contrato
                    check = chkAprobadoPresupuesto.Checked;
                    break;

                case 10: //Administración Directa
                    check = chkAprobadoAdmin.Checked;
                    break;

                //ETAPA DE EJECUCION

                case 11: //Control técnico financiero
                    check = chkAprobadoTecnicoFinanciero.Checked;
                    break;
                case 12: // Bitácora electrónica - convencional
                    check = chkAprobadoBitacora.Checked;
                    break;
                case 13: // Supervisión y estimaciones
                    check = chkAprobadoEstimaciones.Checked;
                    break;
                case 14: // Convenios prefiniquitos
                    check = chkAprobadoConvenios.Checked;
                    break;
                case 15: // Finiquito
                    check = chkAprobadoFiniquito.Checked;
                    break;
                case 16: // Acta entrega recepción
                    check = chkAprobadoEntrega.Checked;
                    break;
                case 17: // Documentación de gestión de recursos
                    check = chkAprobadoGestion.Checked;
                    break;
            }

            poaPlantilla.Aprobado = check;
            uow.POAPlantillaBusinessLogic.Update(poaPlantilla);
            uow.SaveChanges();

            //Si hubo errores
            if (uow.Errors.Count > 0)
            {
                M = string.Empty;
                foreach (string cad in uow.Errors)
                    M += cad;  
            }


            return M;
        }


        protected void gridTecnicoFinanciero_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridTecnicoFinanciero.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridTecnicoFinanciero.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 11, e);
                }
            }
        }

        protected void gridBitacora_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridBitacora.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridBitacora.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 12, e);
                }
            }
        }

        protected void gridEstimaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridEstimaciones.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridEstimaciones.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 13, e);
                }
            }
        }

        protected void gridConvenios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridConvenios.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridConvenios.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 14, e);
                }
            }
        }
        protected void gridFiniquito_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridFiniquito.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridFiniquito.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 15, e);
                }
            }
        }

        protected void gridEntrega_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridEntrega.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridEntrega.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 16, e);
                }
            }
        }

        protected void gridGestion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridGestion.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridGestion.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 17, e);
                }
            }
        }




        #endregion

        
        


        

    }
}
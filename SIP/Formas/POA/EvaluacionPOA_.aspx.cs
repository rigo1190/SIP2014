using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
                //DisplayArea.Cursor = Cursors.Wait;

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

            //for (int i = 1; i <= 17; i++)
            //{
            //    if (i!=numGrid)
            //        DibujarEncabezado(i);
            //}


        }
        private void BindGridsEvaluacion()
        {
            List<Plantilla> listPlantillas = uow.PlantillaBusinessLogic.Get(e => e.DependeDe.Orden == 2).ToList(); //PLANTILLAS DE LA ETAPA DE PLANEACION
            POAPlantilla poaPlantilla;
            int POADetalleID = Utilerias.StrToInt(_IDPOADetalle.Value);


            //var listPOAPlantillasPlaneacion = (from poapl in uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == POADetalleID && e.Detalles.Count > 0)
            //                                   join pl in uow.PlantillaBusinessLogic.Get()
            //                                   on poapl.PlantillaId equals pl.Id
            //                                   select new { poaPlantillaID = poapl.Id, Padre = pl.Padre, Orden=pl.Orden }).Where(e => e.Padre.Orden == 2);
            
            
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
                        txtAprobadoPD.Value = poaPlantilla.ObservacionGeneral;
                        break;

                    case 2: //Anteproyecto y normatividad
                        gridAnteproyecto.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridAnteproyecto.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoAnteproyecto.Checked = poaPlantilla.Aprobado;
                        txtAnteproyecto.Value = poaPlantilla.ObservacionGeneral;
                        break;

                    case 3: //Fondo y programa
                        gridFondoPrograma.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridFondoPrograma.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoFondoPrograma.Checked = poaPlantilla.Aprobado;
                        txtFondoPrograma.Value = poaPlantilla.ObservacionGeneral;
                        break;

                    case 4: //Proyecto Ejecutivo y Proyecto Base
                        gridProyectoEjecutivo.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridProyectoEjecutivo.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoProyectoEjecutivo.Checked = poaPlantilla.Aprobado;
                        txtProyectoEjecutivo.Value = poaPlantilla.ObservacionGeneral;
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
                                        txtAdjuDirecta.Value = poaPlantilla.ObservacionGeneral;
                                        if (pd.Detalles.Count > 0)
                                        {
                                            Plantilla temp = pd.Detalles.ElementAt(0);

                                            //Adjudicacion por excepcion de ley
                                            poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == POADetalleID && e.PlantillaId == temp.Id).FirstOrDefault();
                                            gridExcepcion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                                            gridExcepcion.DataBind();

                                            //Se Bindea el check de APROBADO
                                            chkAprobadoExcepcion.Checked = poaPlantilla.Aprobado;
                                            txtExcepcion.Value = poaPlantilla.ObservacionGeneral;

                                            poaPlantilla = null;
                                            temp = null;
                                            temp = pd.Detalles.ElementAt(1);

                                            //Invitacion a cuando menos tres personas
                                            poaPlantilla = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == POADetalleID && e.PlantillaId == temp.Id).FirstOrDefault();
                                            gridInvitacion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                                            gridInvitacion.DataBind();

                                            //Se Bindea el check de APROBADO
                                            chkAprobadoInvitacion.Checked = poaPlantilla.Aprobado;
                                            txtInvitacion.Value = poaPlantilla.ObservacionGeneral;
 
                                        }

                                        break;
                                    case 2: //Licitacion Pública
                                        gridLicitacion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                                        gridLicitacion.DataBind();

                                        //Se Bindea el check de APROBADO
                                        chkAprobadoLicitacion.Checked = poaPlantilla.Aprobado;
                                        txtLicitacion.Value = poaPlantilla.ObservacionGeneral;
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
                        txtPresupuesto.Value = poaPlantilla.ObservacionGeneral;
                        break;

                    case 7: //Administración Directa
                        gridAdmin.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridAdmin.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoAdmin.Checked = poaPlantilla.Aprobado;
                        txtAdmin.Value = poaPlantilla.ObservacionGeneral;
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
                        txtTecnicoFinanciero.Value = poaPlantilla.ObservacionGeneral;
                        break;
                    case 2: // Bitácora electrónica - convencional
                        gridBitacora.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridBitacora.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoBitacora.Checked = poaPlantilla.Aprobado;
                        txtBitacora.Value = poaPlantilla.ObservacionGeneral;
                        break;
                    case 3: // Supervisión y estimaciones
                        gridEstimaciones.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridEstimaciones.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoEstimaciones.Checked = poaPlantilla.Aprobado;
                        txtEstimaciones.Value = poaPlantilla.ObservacionGeneral;
                        break;
                    case 4: // Convenios prefiniquitos
                        gridConvenios.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridConvenios.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoConvenios.Checked = poaPlantilla.Aprobado;
                        txtConvenios.Value = poaPlantilla.ObservacionGeneral;
                        break;
                    case 5: // Finiquito
                        gridFiniquito.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridFiniquito.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoFiniquito.Checked = poaPlantilla.Aprobado;
                        txtFiniquito.Value = poaPlantilla.ObservacionGeneral;
                        break;
                    case 6: // Acta entrega recepción
                        gridEntrega.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridEntrega.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoEntrega.Checked = poaPlantilla.Aprobado;
                        txtEntrega.Value = poaPlantilla.ObservacionGeneral;
                        break;
                    case 7: // Documentación de gestión de recursos
                        gridGestion.DataSource = poaPlantilla.Detalles.OrderBy(e => e.PlantillaDetalle.Orden).ToList();
                        gridGestion.DataBind();

                        //Se Bindea el check de APROBADO
                        chkAprobadoGestion.Checked = poaPlantilla.Aprobado;
                        txtGestion.Value = poaPlantilla.ObservacionGeneral;
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
            var list = (from pl1 in uow.PlantillaBusinessLogic.Get(e=> e.DetallePreguntas.Count > 0)
                        join pl2 in uow.PlantillaBusinessLogic.Get(e => e.DetallePreguntas.Count > 0)
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
                    M = CrearPreguntasInexistentes(poaPlantilla.Id, p.Id, p.DetallePreguntas.ToList());//CrearPreguntasInexistentes(poaPlantilla.Id, p.Id);
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
                else
                    M = CrearPreguntasInexistentes(poaPlantilla.Id, p.Id, p.DetallePreguntas.ToList());//CrearPreguntasInexistentes(poaPlantilla.Id, p.Id);

            }

        }

        private string CrearPreguntasInexistentes(int idPOAPlantilla,int idPlantilla, List<PlantillaDetalle> DetallePreguntas)
        {
            string M = string.Empty;
            POAPlantillaDetalle objPOAPlantillaDetalle;

            foreach (PlantillaDetalle pd in DetallePreguntas)
            {

                objPOAPlantillaDetalle = uow.POAPlantillaDetalleBusinessLogic.Get(e => e.POAPlantillaId == idPOAPlantilla && e.PlantillaDetalleId == pd.Id).FirstOrDefault();

                if (objPOAPlantillaDetalle == null)
                {
                    objPOAPlantillaDetalle = new POAPlantillaDetalle();
                    objPOAPlantillaDetalle.POAPlantillaId = idPOAPlantilla; //ID del obj POAPlantilla
                    objPOAPlantillaDetalle.PlantillaDetalleId = pd.Id; //Id del obj Plantilla detalle

                    uow.POAPlantillaDetalleBusinessLogic.Insert(objPOAPlantillaDetalle);
                    uow.SaveChanges();

                    if (uow.Errors.Count > 0)
                    {
                        M = string.Empty;
                        foreach (string cad in uow.Errors)
                            M += cad;

                        return M;
                    }
                }

                

            }

            Plantilla objPlantilla = uow.PlantillaBusinessLogic.GetByID(idPlantilla);

            if (objPlantilla.Detalles.Count > 0)
            {
                foreach (Plantilla p in objPlantilla.Detalles)
                {
                    CrearPreguntasInexistentes(idPOAPlantilla, p.Id, objPlantilla.DetallePreguntas.ToList());
                }
            }



            return M;
        }

        //private string CrearPreguntasInexistentes(int POAPlantillaID, int plantillaID)
        //{
        //    //int ordenPlantilla = Utilerias.StrToInt(Request.QueryString["o"].ToString());
        //    string M = string.Empty;

        //    //Consulta que hace la diferencia de preguntas entre POAPLANTILLADETALLE y PLANTILLADETALLE
        //    var list = (from p in uow.PlantillaDetalleBusinessLogic.Get()
        //                join pp in uow.PlantillaBusinessLogic.Get(e => e.Id == plantillaID).ToList()
        //                on p.PlantillaId equals pp.Id
        //                join po in uow.POAPlantillaDetalleBusinessLogic.Get(e=>e.POAPlantillaId==POAPlantillaID)
        //                on p.Id equals po.PlantillaDetalleId into temp
        //                from pi in temp.DefaultIfEmpty()
        //                select new { p.Id, pp.Orden, pregunta = (pi == null ? 0 : pi.PlantillaDetalleId) });


        //    //var tem=(from pp in uow.PlantillaBusinessLogic.Get(e=>e.Id==plantillaID)
        //    //         join pd in uow.PlantillaDetalleBusinessLogic.Get()
        //    //         on pp.Id equals pd.PlantillaId
        //    //         join poap in uow.POAPlantillaBusinessLogic.Get(e=>e.Id==POAPlantillaID)
        //    //         on pp.Id equals poap.PlantillaId
        //    //         join poapd in uow.POAPlantillaDetalleBusinessLogic.Get()
        //    //         on poap.Id equals poapd.POAPlantillaId into temp
        //    //         from pi in temp.DefaultIfEmpty()
        //    //         select new { pd.Id, pp.Orden, pregunta = (pi == null ? 0 : pi.PlantillaDetalleId) });

        //    foreach (var obj in list)
        //    {
        //        if (obj.pregunta == 0)
        //        {
        //            POAPlantillaDetalle objPOAPlantillaDetalle;

        //            objPOAPlantillaDetalle = uow.POAPlantillaDetalleBusinessLogic.Get(e => e.POAPlantillaId == POAPlantillaID && e.PlantillaDetalleId == obj.Id).FirstOrDefault();

        //            if (objPOAPlantillaDetalle == null)
        //            {
        //                objPOAPlantillaDetalle = new POAPlantillaDetalle();
        //                objPOAPlantillaDetalle.POAPlantillaId = POAPlantillaID; //ID del obj POAPlantilla
        //                objPOAPlantillaDetalle.PlantillaDetalleId = obj.Id; //Id del obj Plantilla detalle
                        
        //                uow.POAPlantillaDetalleBusinessLogic.Insert(objPOAPlantillaDetalle);
        //                uow.SaveChanges();
        //            }

        //            //Si hubo errores
        //            if (uow.Errors.Count > 0)
        //            {
        //                M = string.Empty;
        //                foreach (string cad in uow.Errors)
        //                    M += cad;

        //                return M;
        //            }
        //        }
        //    }


        //    Plantilla objPlantilla = uow.PlantillaBusinessLogic.GetByID(plantillaID);

        //    if (objPlantilla.Detalles.Count > 0)
        //    {
        //        foreach (Plantilla p in objPlantilla.Detalles)
        //        {
        //            CrearPreguntasInexistentes(POAPlantillaID, p.Id);
        //        }
        //    }



        //    return M;

        //}



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
            
            //Label lblArchivo = (Label)e.Row.FindControl("lblArchivo");

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


            //if (obj.NombreArchivo != null)
            //    if (!obj.NombreArchivo.Equals(string.Empty))
                    //lblArchivo.Text = obj.NombreArchivo;
            //    else
            //        lblArchivo.Text = "No existe archivo adjunto";
            //else
            //    lblArchivo.Text = "No existe archivo adjunto";


            if (imgBtnEdit != null)
                imgBtnEdit.Attributes["onclick"] = "fnc_Edicion(" + id + "," + numGrid + ");return false;";



            //Se coloca la fucnion a corespondiente para visualizar el DOCUMENTO ADJUNTO A LA PREGUNTA
            //HtmlButton btnVer = (HtmlButton)e.Row.FindControl("btnVer");
            //string ruta = ResolveClientUrl("~/AbrirDocto.aspx");
            //btnVer.Attributes["onclick"] = "fnc_AbrirArchivo('" + ruta + "'," + id + ")";


            //Tooltip para la fundamentacion
            List<string> fp;


            for (int i = 1; i <= 4; i++)
            {
                fp = GetFundamentacion(i, obj);

                HtmlButton btnA = (HtmlButton)e.Row.FindControl("btnA"+i.ToString());
                btnA.InnerText = fp[0] != string.Empty ? "Art:" : "N/A";
                btnA.Attributes.Add("title",fp[0] != string.Empty ? fp[0] : "N/A");
                btnA.Attributes.Add("data-tooltip", "tooltip");
                btnA.Attributes.Add("data-content",fp[1] != string.Empty ? fp[1] : string.Empty); //"Este es un ejemplo de articulada para cada uno de los puntos correspondientes, Este es un ejemplo de articulada para cada uno de los puntos correspondientes");// fp[1];
            }
            
            
        }


        public List<string> GetFundamentacion(int rubro,POAPlantillaDetalle obj)
        {
            List<string> fundamentacion = new List<string>();

            PlantillaDetalle pd = uow.PlantillaDetalleBusinessLogic.GetByID(obj.PlantillaDetalleId);
            List<FundamentacionPlantilla> listFP = uow.FundamentacionPlantillaBL.Get(e => e.PlantillaDetalleId == pd.Id).ToList();
            FundamentacionPlantilla fp = null;

            switch (rubro)
            {
                case 1: //LOPySRM
                    fp = listFP.Where(e => e.RubroFundamentacionId == 1).FirstOrDefault();
                    break;

                case 2: //RLOPSRM
                    fp = listFP.Where(e => e.RubroFundamentacionId == 2).FirstOrDefault();
                    break;

                case 3: //Contrato
                    fp = listFP.Where(e => e.RubroFundamentacionId == 3).FirstOrDefault();
                    break;

                case 4: //Admón. Directa
                    fp = listFP.Where(e => e.RubroFundamentacionId == 4).FirstOrDefault();
                    break;
            }

            if (fp != null)
            {
                fundamentacion.Add(fp.DescripcionCorta);
                fundamentacion.Add(fp.Fundamentacion);
            }
            else
            {
                fundamentacion.Add(string.Empty);
                fundamentacion.Add(string.Empty);
            }
            

            return fundamentacion;
        }



        [WebMethod]
        public static List<string> GuardarChecks(string cadValores, bool checkAprobado, string obsGeneral)
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
            plantilla.ObservacionGeneral = obsGeneral;

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
        private List<string> GuardarArchivo(FileUpload fileUpload, int idPregunta)
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

                ruta += fileUpload.FileName;

                fileUpload.PostedFile.SaveAs(ruta); //Se guarda el archivo

            }
            catch (Exception ex)
            {
                M = ex.Message;
            }

            R.Add(M);
            R.Add(ruta);

            return R;

        }


        [WebMethod]
        public static string EliminarArchivo(int id)
        {
            string M = string.Empty;
            try
            {
                UnitOfWork uow = new UnitOfWork();
                POAPlantillaDetalleDoctos docto = uow.POAPlantillaDetalleDoctosBL.GetByID(id);
                string ruta = string.Empty;
                
                //eliminar archivo
                ruta = System.Configuration.ConfigurationManager.AppSettings["ArchivosPlantilla"];

                if (!ruta.EndsWith("/"))
                    ruta += "/";

                ruta += docto.POAPlantillaDetalleId.ToString() + "/";

                if (ruta.StartsWith("~") || ruta.StartsWith("/"))   //Es una ruta relativa al sitio
                    ruta = HttpContext.Current.Server.MapPath(ruta);//Server.MapPath(ruta);

                File.Delete(ruta + "\\" + docto.NombreArchivo);

                uow.POAPlantillaDetalleDoctosBL.Delete(docto);
                uow.SaveChanges();

                if (uow.Errors.Count > 0)
                {
                    foreach (string e in uow.Errors)
                        M += e;
                }

                //Directory.Delete(ruta);

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
        public static List<object> GetValoresPregunta(string idPregunta)
        {
            List<object> R = new List<object>();

            UnitOfWork uow = new UnitOfWork();
            Dictionary<string, int> test = new Dictionary<string, int>();

            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(Utilerias.StrToInt(idPregunta));

            if (obj != null)
            {
                R.Add(obj.Observaciones != null && obj.Observaciones != string.Empty ? obj.Observaciones : string.Empty);
                R.Add(obj.NombreArchivo != null && obj.NombreArchivo != string.Empty ? obj.NombreArchivo : string.Empty);
                R.Add(obj.PlantillaDetalle.Pregunta);

                //Se recupera la Lista de documentos qu existan para la pregunta

                if (obj.DetalleDoctos.Count > 0)
                {

                    var list = (from d in obj.DetalleDoctos
                                select new { d.Id, d.NombreArchivo });

                    R.Add(list);
  
                }
            }

            return R;

        }


        

        #endregion


        #region EVENTOS

        protected void gridPlanDesarrollo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(1);
            
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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(2);
            
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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(3);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(4);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(5);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(6);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(7);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(8);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(9);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(10);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (gridAdmin.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    int id = Utilerias.StrToInt(gridAdmin.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    RowDataBound(id, 10, e);
                }
            }
        }


        [WebMethod]
        public static string GuardarEdicion(string nombreArchivo)
        {
            string M = "Funciona";

            FileInfo fi = new FileInfo(nombreArchivo);

            FileUpload fu = new FileUpload();
            
            

            
            return M;
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

                POAPlantillaDetalleDoctos doctonew;
                
                doctonew = obj.DetalleDoctos.Where(d => d.NombreArchivo == fileUpload.FileName && d.TipoArchivo == fileUpload.PostedFile.ContentType).FirstOrDefault();

                if (doctonew == null)
                {
                    doctonew = new POAPlantillaDetalleDoctos();
                    doctonew.POAPlantillaDetalleId = obj.Id;
                    doctonew.NombreArchivo = Path.GetFileName(fileUpload.FileName);
                    doctonew.TipoArchivo = fileUpload.PostedFile.ContentType;

                    obj.DetalleDoctos.Add(doctonew);
                }

                //R = GuardarArchivo(fileUpload.PostedFile, idPregunta); ///Se guarda el archivo fisicamente en el servidor
                R = GuardarArchivo(fileUpload, idPregunta); //Se guarda el archivo fisicamente en el servidor


                ////Si hubo errores
                //if (!R[0].Equals(string.Empty))
                //{
                //    divMsgError.Style.Add("display", "block");
                //    divMsgSuccess.Style.Add("display", "none");
                //    lblMsgError.Text = R[0];
                //    return;
                //}

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

            M = GuardarCheckAprobadoObsIndividual(Utilerias.StrToInt(_NumGrid.Value), obj);

            if (!M.Equals(string.Empty))
            {
                return;
            }


            BindGridIndividual(Utilerias.StrToInt(_NumGrid.Value), obj);

            
            ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_AbrirCollapse()", true);
        }

        private string GuardarCheckAprobadoObsIndividual(int numGrid, POAPlantillaDetalle obj )
        {
            string M = string.Empty;
            bool check = false;
            string observacionesGen = string.Empty;
            POAPlantilla poaPlantilla = uow.POAPlantillaBusinessLogic.GetByID(obj.POAPlantillaId);

            switch (numGrid)
            {
                case 1: //Plan de Desarrollo Estatal Urbano
                    check = chkAprobadoPD.Checked;
                    observacionesGen = txtAprobadoPD.Value;
                    break;

                case 2: //Anteproyecto y normatividad
                    check = chkAprobadoAnteproyecto.Checked;
                    observacionesGen = txtAnteproyecto.Value;
                    break;

                case 3: //Fondo y programa
                    check = chkAprobadoFondoPrograma.Checked;
                    observacionesGen = txtFondoPrograma.Value;
                    break;

                case 4: //Proyecto Ejecutivo y Proyecto Base
                    check = chkAprobadoProyectoEjecutivo.Checked;
                    observacionesGen = txtProyectoEjecutivo.Value;
                    break;

                case 5: //Adjudicacion Directa
                    check = chkAprobadoAdjuDirecta.Checked;
                    observacionesGen = txtAdjuDirecta.Value;
                    break;

                case 6: //Adjudicacion por excepcion de ley
                    check = chkAprobadoExcepcion.Checked;
                    observacionesGen = txtExcepcion.Value;
                    break;

                case 7: //Invitacion a cuando menos tres personas
                    check = chkAprobadoInvitacion.Checked;
                    observacionesGen = txtInvitacion.Value;
                    break;

                case 8: //Licitacion Pública
                    check = chkAprobadoLicitacion.Checked;
                    observacionesGen = txtLicitacion.Value;
                    break;

                case 9: //Presupuesto Autorizado Contrato
                    check = chkAprobadoPresupuesto.Checked;
                    observacionesGen = txtPresupuesto.Value;
                    break;

                case 10: //Administración Directa
                    check = chkAprobadoAdmin.Checked;
                    observacionesGen = txtAdmin.Value;
                    break;

                //ETAPA DE EJECUCION

                case 11: //Control técnico financiero
                    check = chkAprobadoTecnicoFinanciero.Checked;
                    observacionesGen = txtTecnicoFinanciero.Value;
                    break;
                case 12: // Bitácora electrónica - convencional
                    check = chkAprobadoBitacora.Checked;
                    observacionesGen = txtBitacora.Value;
                    break;
                case 13: // Supervisión y estimaciones
                    check = chkAprobadoEstimaciones.Checked;
                    observacionesGen = txtEstimaciones.Value;
                    break;
                case 14: // Convenios prefiniquitos
                    check = chkAprobadoConvenios.Checked;
                    observacionesGen = txtConvenios.Value;
                    break;
                case 15: // Finiquito
                    check = chkAprobadoFiniquito.Checked;
                    observacionesGen = txtFiniquito.Value;
                    break;
                case 16: // Acta entrega recepción
                    check = chkAprobadoEntrega.Checked;
                    observacionesGen = txtEntrega.Value;
                    break;
                case 17: // Documentación de gestión de recursos
                    check = chkAprobadoGestion.Checked;
                    observacionesGen = txtGestion.Value;
                    break;
            }

            poaPlantilla.Aprobado = check;
            poaPlantilla.ObservacionGeneral = observacionesGen;
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

        private void DibujarEncabezado(int numGrid)
        {
            GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell cell0 = new TableCell();
            cell0.Text = string.Empty;
            cell0.HorizontalAlign = HorizontalAlign.Center;
            cell0.ColumnSpan = 5;

            TableCell cell1 = new TableCell();
            cell1.Text = "FEDERAL";
            cell1.ForeColor = System.Drawing.Color.Black;

            cell1.HorizontalAlign = HorizontalAlign.Center;
            cell1.ColumnSpan = 2;

            TableCell cell2 = new TableCell();
            cell2.Text = "LEY ESTATAL 825";
            cell2.ForeColor = System.Drawing.Color.Black;
            cell2.HorizontalAlign = HorizontalAlign.Center;
            cell2.ColumnSpan = 2;


            gvrow.Cells.Add(cell0);
            gvrow.Cells.Add(cell1);
            gvrow.Cells.Add(cell2);

            //grid.Controls[0].Controls.AddAt(0, gvrow);

            switch (numGrid)
            {
                case 1: //Plan de Desarrollo Estatal Urbano
                    gridPlanDesarrollo.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 2: //Anteproyecto y normatividad
                    gridAnteproyecto.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 3: //Fondo y programa
                    gridFondoPrograma.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 4: //Proyecto Ejecutivo y Proyecto Base
                    gridProyectoEjecutivo.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 5: //Adjudicacion Directa
                    gridAdjuDirecta.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 6: //Adjudicacion por excepcion de ley
                    gridExcepcion.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 7: //Invitacion a cuando menos tres personas
                    gridInvitacion.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 8: //Licitacion Pública
                    gridLicitacion.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 9: //Presupuesto Autorizado Contrato
                    gridPresupuesto.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                case 10: //Administración Directa
                    gridAdmin.Controls[0].Controls.AddAt(0, gvrow);
                    break;

                //ETAPA DE EJECUCION

                case 11: //Control técnico financiero
                    gridTecnicoFinanciero.Controls[0].Controls.AddAt(0, gvrow);
                    break;
                case 12: // Bitácora electrónica - convencional
                    gridBitacora.Controls[0].Controls.AddAt(0, gvrow);
                    break;
                case 13: // Supervisión y estimaciones
                    gridEstimaciones.Controls[0].Controls.AddAt(0, gvrow);
                    break;
                case 14: // Convenios prefiniquitos
                    gridConvenios.Controls[0].Controls.AddAt(0, gvrow);
                    break;
                case 15: // Finiquito
                    gridFiniquito.Controls[0].Controls.AddAt(0, gvrow);
                    break;
                case 16: // Acta entrega recepción
                    gridEntrega.Controls[0].Controls.AddAt(0, gvrow);
                    break;
                case 17: // Documentación de gestión de recursos
                    gridGestion.Controls[0].Controls.AddAt(0, gvrow);
                    break;
            }

        }


        protected void gridTecnicoFinanciero_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(11);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(12);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(13);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(14);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(15);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(16);

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
            //if (e.Row.RowType == DataControlRowType.Header)
                //DibujarEncabezado(17);

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
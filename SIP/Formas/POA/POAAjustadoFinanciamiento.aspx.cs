﻿using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class POAAjustadoFinanciamiento : System.Web.UI.Page
    {
        UnitOfWork uow;
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

                lblTitulo.Text = String.Format("{0} <br /> Asignar financiamiento en POA ajustado, para el ejercicio {1}", up.Nombre, uow.EjercicioBusinessLogic.GetByID(ejercicioId).Año);
                              
            }

        }              

        protected void GridViewObra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlButton btnE = (HtmlButton)e.Row.FindControl("btnFinanciamiento");
                if (btnE != null)
                {
                    if (GridViewObra.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                    {
                        string url = string.Empty;
                       
                        Obra obra = (Obra)e.Row.DataItem;

                        url = "AsignarFinanciamientoPOA.aspx?EnProyecto=false&poadetalleId=" + obra.POADetalleId.ToString();
                     
                        btnE.Attributes.Add("data-url-poa", url);
                       

                    }

                }

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

        // El tipo devuelto puede ser modificado a IEnumerable, sin embargo, para ser compatible con
        //paginación y ordenación // , se deben agregar los siguientes parametros:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<DataAccessLayer.Models.Obra> GridViewObra_GetData()
        {

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();

            if (poa == null) return null;

            return uow.ObraBusinessLogic.Get(obra => obra.POAId == poa.Id).OrderBy(r=>r.Consecutivo);

        }

        protected void GridViewTechoFinanciero_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gridView = sender as GridView;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlButton btn = (HtmlButton)e.Row.FindControl("btnlineamientos");

                if (btn != null)
                {
                    if (gridView.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                    {
                        TechoFinancieroUnidadPresupuestal tfup = (TechoFinancieroUnidadPresupuestal)e.Row.DataItem;

                        String fondoId = String.Empty;
                        fondoId = gridView.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
                        btn.Attributes.Add("onclick", "fnc_MostrarLineamientos(this,'" + tfup.TechoFinanciero.Financiamiento.FondoId + "');");

                    }

                }

            }
        }

        [WebMethod]
        public static List<string> GetLineamientosFondo(int fondoId)
        {
            List<string> result = new List<string>();

            try
            {


                UnitOfWork uow = new UnitOfWork();
                FondoLineamientos fondolineamientos = uow.FondoLineamientosBL.Get(fl => fl.FondoId == fondoId).FirstOrDefault();

                if (fondolineamientos == null) return result;

                result.Add(fondolineamientos.Fondo.Abreviatura);
                result.Add(fondolineamientos.Fondo.Nombre);
                result.Add(fondolineamientos.TipoDeObrasYAcciones);
                result.Add(fondolineamientos.CalendarioDeIngresos);
                result.Add(fondolineamientos.VigenciaDePago);
                result.Add(fondolineamientos.NormatividadAplicable);
                result.Add(fondolineamientos.Contraparte);
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.Print(ex.Message);
            }

            return result;
        }

    }
}
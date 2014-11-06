﻿using BusinessLogicLayer;
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
    public partial class POAFinanciamiento : System.Web.UI.Page
    {
        UnitOfWork uow;
        private int unidadpresupuestalId;
        private int ejercicioId;
        private bool techofinancierocerrado;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork(Session["IdUser"].ToString());

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            TechoFinancieroStatus tfestatus = uow.TechoFinancieroStatusBusinessLogic.Get(tfe => tfe.EjercicioId == ejercicioId).FirstOrDefault();

            if (tfestatus==null || tfestatus.Status == 1)
            {
                divTechoFinancieroError.Style.Add("display", "block");
                divTechoFinancieroEstatus.Style.Add("display", "none");
                techofinancierocerrado = false;
            }
            else
            {
                divTechoFinancieroError.Style.Add("display", "none");
                divTechoFinancieroEstatus.Style.Add("display", "block");
                techofinancierocerrado = true;
            }


            if (!IsPostBack) 
            {

                UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(unidadpresupuestalId);

                lblTitulo.Text = String.Format("{0} <br /> Asignar financiamiento de POA para el ejercicio {1}", up.Nombre, uow.EjercicioBusinessLogic.GetByID(ejercicioId).Año);
                                
                BindGrid();
            }
            
        }

        private void BindGrid()
        {           

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();

            if (poa == null) return;

            List<Obra> obras = uow.ObraBusinessLogic.Get(ob => ob.POAId == poa.Id).ToList();
          
            List<int> colObrasId = new List<int>();

            foreach (var item in obras)
            {
                colObrasId.Add(item.POADetalleId.Value);
            }                     
           
            this.GridViewPOADetalle.DataSource = uow.POADetalleBusinessLogic.Get(pd => !colObrasId.Contains(pd.Id) & pd.Extemporanea == false & pd.POAId==poa.Id, orderBy: r => r.OrderBy(ro => ro.Numero)).ToList();
            this.GridViewPOADetalle.DataBind();

        }

        protected void GridViewPOADetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlButton btnE = (HtmlButton)e.Row.FindControl("btnFinanciamiento");
                if (btnE != null)
                {
                    if (GridViewPOADetalle.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                    {
                        string url = string.Empty;
                        url = "AsignarFinanciamientoPOA.aspx?EnProyecto=true&poadetalleId=" + GridViewPOADetalle.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
                       
                        btnE.Attributes.Add("data-url-poa", url);

                        if (techofinancierocerrado==false)
                        {
                            btnE.Attributes.Add("disabled", "disabled");
                        }

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

        protected void GridViewTechoFinanciero_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

    }
}
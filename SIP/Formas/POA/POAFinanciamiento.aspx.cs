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
    public partial class POAFinanciamiento : System.Web.UI.Page
    {
        UnitOfWork uow;
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

                lblTitulo.Text = String.Format("{0} <br /> Asignar financiamiento de POA para el ejercicio {1}", up.Nombre, uow.EjercicioBusinessLogic.GetByID(ejercicioId).Año);
                
                BindGrid();
            }
            
        }

        private void BindGrid()
        {           

            unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());

            DataAccessLayer.Models.POA poa = uow.POABusinessLogic.Get(p => p.UnidadPresupuestalId == unidadpresupuestalId & p.EjercicioId == ejercicioId).FirstOrDefault();

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
                        url = "AsignarFinanciamientoPOA.aspx?poadetalleId=" + GridViewPOADetalle.DataKeys[e.Row.RowIndex].Values["Id"].ToString();
                       
                        btnE.Attributes.Add("data-url-poa", url);

                        int poadetalleId = Utilerias.StrToInt(GridViewPOADetalle.DataKeys[e.Row.RowIndex].Values["Id"].ToString());

                        Obra obra = uow.ObraBusinessLogic.Get(o => o.POADetalleId == poadetalleId).FirstOrDefault();

                        if (obra != null)
                        {
                            btnE.Attributes.Add("disabled", "disabled");
                        }

                    }

                }

            }
        }
    }
}
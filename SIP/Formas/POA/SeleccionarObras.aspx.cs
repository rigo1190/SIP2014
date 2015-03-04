using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class SeleccionarObras : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGridDSP();
                BindGridRPAI();
            }
        }


        private void BindGridDSP()
        {

            int unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());


            var list = (from o in uow.ObraBusinessLogic.Get(o => o.POA.UnidadPresupuestalId == unidadpresupuestalId & o.POA.EjercicioId == ejercicioId)
                        join pd in uow.POADetalleBusinessLogic.Get(e => e.Extemporanea == false)
                        on o.POADetalleId equals pd.Id
                        join pp in uow.POAPlantillaBusinessLogic.Get(e=>e.PlantillaId==5 && e.Aprobado==true)
                        on pd.Id equals pp.POADetalleId
                        select pd).ToList();

            gridDSP.DataSource = list;
            gridDSP.DataBind();

        }

        private void BindGridRPAI()
        {

            int unidadpresupuestalId = Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString());
            int ejercicioId = Utilerias.StrToInt(Session["EjercicioId"].ToString());


            var list = (from o in uow.ObraBusinessLogic.Get(o => o.POA.UnidadPresupuestalId == unidadpresupuestalId & o.POA.EjercicioId == ejercicioId)
                        join pd in uow.POADetalleBusinessLogic.Get(e => e.Extemporanea == false)
                        on o.POADetalleId equals pd.Id
                        join pp in uow.POAPlantillaBusinessLogic.Get(e =>e.Aprobado==true && e.PlantillaId == 7)
                        on pd.Id equals pp.POADetalleId
                        select pd).ToList();

            gridRPAI.DataSource = list;
            gridRPAI.DataBind();

        }

        protected void gridDSP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridDSP.PageIndex = e.NewPageIndex;
            BindGridDSP();
        }



        protected void gridRPAI_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridRPAI.PageIndex = e.NewPageIndex;
            BindGridRPAI();
        }


    }
}
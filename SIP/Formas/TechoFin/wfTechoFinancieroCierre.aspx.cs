using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.TechoFin
{
    public partial class wfTechoFinancieroCierre : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            if (!IsPostBack)
            {

            }
        }




        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            int ejercicio;

            ejercicio = int.Parse(Session["EjercicioId"].ToString());

            TechoFinancieroStatus obj = uow.TechoFinancieroStatusBusinessLogic.Get(p=>p.EjercicioId == ejercicio).First();

            obj.Status = 2;
            uow.TechoFinancieroStatusBusinessLogic.Update(obj);
            uow.SaveChanges();

            Response.Redirect("wfTechoFinanciero.aspx");

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfTechoFinanciero.aspx");
        }
    }
}
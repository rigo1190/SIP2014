using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP
{
    public partial class NavegadorDependencia : System.Web.UI.MasterPage
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            uow = new UnitOfWork();


            Ejercicio ejercicio = uow.EjercicioBusinessLogic.GetByID(Utilerias.StrToInt(Session["EjercicioId"].ToString()));
            UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString()));

            lblUsuario.Text = Session["NombreUsuario"].ToString();
            lblEjercicio.Text = ejercicio.Año.ToString();
            lblDependencia.Text = up.Abreviatura;

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}
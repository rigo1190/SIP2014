using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.ControlFinanciero
{
    public partial class wfSeguimiento : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;
        private int idEjercicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            idObra = int.Parse(Request.QueryString["id"].ToString());
            idEjercicio = int.Parse(Session["EjercicioId"].ToString());

            if (!IsPostBack)
            {
                BindX();
            }
        }

        private void BindX()
        {

        }


    }
}
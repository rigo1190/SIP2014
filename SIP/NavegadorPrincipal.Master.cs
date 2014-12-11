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
    public partial class NavegadorPrincipal : System.Web.UI.MasterPage
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

            MostrarOpciones();
        }


        private void MostrarOpciones()
        {
            int idUser = Utilerias.StrToInt(Session["IdUser"].ToString());

            Usuario user = uow.UsuarioBusinessLogic.GetByID(idUser);
            
            //SEGUN EL ROL, SE MOSTRARAN OPCIONES

            switch (user.RolId)
            {
                case 1: //Desarrollador
                    mnCatalogos.Visible = true;
                    mnPOA.Visible = true;
                    mnTechosFinancieros.Visible = true;
                    break;
                case 2: //Ejecutivo
                    break;
                case 3: //Administrador
                    mnCatalogos.Visible = true;
                    break;
                case 4: //Capturista de Dependencia
                    mnPOA.Visible = true;
                    break;
                case 5: //Analista
                    mnPOAAnalista.Visible = true;
                    mnTechosFinancieros.Visible = true;
                    break;     
            }




        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}
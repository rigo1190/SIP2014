﻿using BusinessLogicLayer;
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

            lblUsuario.Text = Session["NombreUsuario"].ToString();

            MostrarOpciones();
        }


        private void MostrarOpciones()
        {
            int idUser = Utilerias.StrToInt(Session["IdUser"].ToString());

            Usuario user = uow.UsuarioBusinessLogic.GetByID(idUser);
            
            //SEGUN EL ROL, SE MOSTRARAN OPCIONES

            if (user.RolId != 3 & user.RolId != 6)
            {
                UnidadPresupuestal up = uow.UnidadPresupuestalBusinessLogic.GetByID(Utilerias.StrToInt(Session["UnidadPresupuestalId"].ToString()));
                Ejercicio objEjercicio = uow.EjercicioBusinessLogic.GetByID(Utilerias.StrToInt(Session["EjercicioId"].ToString())); 
                lblEjercicio.Text = objEjercicio!=null ? objEjercicio.Año.ToString():string.Empty;
                lblDependencia.Text = up.Abreviatura;
            }



            switch (user.RolId)
            {
                case 1: //Desarrollador

                    mnInicioSefiplan.Visible = true;
                    mnCatalogos.Visible = true;
                    mnPOA.Visible = true;
                    mnTechosFinancieros.Visible = true;
                    mnControlFinanciero.Visible = true;
                    break;

                case 2: //Ejecutivo
                    break;
                case 3: //Administrador

                    mnCatalogos.Visible = true;
                    break;

                case 4: //Capturista de Dependencia

                    mnInicioDependencia.Visible = true;
                    mnPOA.Visible = true;
                    break;

                case 5: //Analista

                    mnInicioSefiplan.Visible = true;
                    mnPOAAnalista.Visible = true;
                    mnTechosFinancieros.Visible = true;
                    break;

                case 6: //Subdirector de Control financiero

                    mnInicioDependencia.Visible = true;
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
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas
{
    public partial class SelectorEjercicioDependencia : System.Web.UI.Page
    {
        UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindDropDownEjercicios();
                BinDropDownUnidades();
            }
        }


        private void BindDropDownEjercicios()
        {
            ddlEjercicios.DataSource = uow.EjercicioBusinessLogic.Get().OrderBy(up => up.Año);
            ddlEjercicios.DataValueField = "Id";
            ddlEjercicios.DataTextField = "Año";
            ddlEjercicios.DataBind();

            ddlEjercicios.Items.Insert(0, new ListItem("Seleccione...", "0"));
        }



        private void BinDropDownUnidades()
        {
            
            int idUser=Utilerias.StrToInt(Session["IdUser"].ToString());

            var list = (from up in uow.UnidadPresupuestalBusinessLogic.Get()
                        join uu in uow.UsuarioUnidadPresupuestalBusinessLogic.Get(e => e.UsuarioId == idUser)
                        on up.Id equals uu.UnidadPresupuestalId
                        select new { up.Nombre, up.Id }).ToList();


            ddlUnidadPresupuestal.DataSource = list;
            ddlUnidadPresupuestal.DataValueField = "Id";
            ddlUnidadPresupuestal.DataTextField = "Nombre";
            ddlUnidadPresupuestal.DataBind();

            if (list.Count() > 1)
            {
                ddlUnidadPresupuestal.Items.Insert(0, new ListItem("Seleccione...", "0"));
                divUnidades.Style.Add("display", "block");
            }
            else
            {
                Session["UnidadPresupuestalId"] = list[0].Id;
                divUnidades.Style.Add("display", "none");
            }



        }

        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (ddlUnidadPresupuestal.Items.Count>1)
                Session["UnidadPresupuestalId"] = ddlUnidadPresupuestal.SelectedValue;
            
            Session["EjercicioId"] = ddlEjercicios.SelectedValue;
            Response.Redirect("~/Formas/Catalogos/Inicio.aspx");
        }



    }
}
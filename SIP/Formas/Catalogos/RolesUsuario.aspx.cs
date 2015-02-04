using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.Catalogos
{
    public partial class RolesUsuario : System.Web.UI.Page
    {
        UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork(Session["IdUser"].ToString());     
        }

        public IQueryable<Rol> GridViewRoles_GetData()
        {
            IQueryable<Rol> roles = uow.RolBusinessLogic.Get(orderBy:r=>r.OrderBy(ro=>ro.Orden));
            return roles;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ViewState["tituloModal"] = "Agregando registro";
            ClientScript.RegisterStartupScript(this.GetType(), "script01", "BeforeAddRecord();", true);            
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["tituloModal"] = "Modificando registro";          

            //GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            //ViewState["currentId"] = GridViewRoles.DataKeys[row.RowIndex].Values["Id"].ToString();

            //ClientScript.RegisterStartupScript(this.GetType(), "script01", "BeforeUpdateRecord('" + ViewState["currentId"].ToString() +  "');", true);


        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            //GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            //ViewState["currentId"] = GridViewRoles.DataKeys[row.RowIndex].Values["Id"].ToString();

            //ClientScript.RegisterStartupScript(this.GetType(), "script01", "BeforeDeleteRecord('" + ViewState["currentId"].ToString() + "');", true);

        }

        protected void GridViewRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView grid = sender as GridView;
            grid.PageIndex = e.NewPageIndex;     
        }

        [WebMethod]
        public static IQueryable<Rol> GetRoles()
        {
            UnitOfWork uow = new UnitOfWork();
            IQueryable<Rol> list = uow.RolBusinessLogic.Get();

            //List<poadetails> result = new List<poadetails>();

            //foreach (var item in list)
            //{
            //    result.Add(new poadetails { Id = item.Id, Numero = item.Numero, Descripcion = item.Descripcion });
            //}

            return list;

        }

        [WebMethod]
        public static Rol GetRecord(int id)
        {

            UnitOfWork uow = new UnitOfWork();

            Rol rol = uow.RolBusinessLogic.GetByID(id);

            return rol;
           
        }

        [WebMethod]
        public static result AddRecord(string clave, string nombre, int orden,bool dependencia,bool sefiplan) 
        {

            UnitOfWork uow = new UnitOfWork();

            Rol rol = new Rol { Clave = clave, Nombre = nombre, Orden = orden,EsDependencia=dependencia,EsSefiplan=sefiplan };

            uow.RolBusinessLogic.Insert(rol);

            uow.SaveChanges();

            if (uow.Errors.Count > 0) 
            {
                return new result { OK=false,Errors=uow.Errors};
            }
            
            return new result { OK = true };
        }

        [WebMethod]
        public static result UpdateRecord(int id,string clave, string nombre, int orden,bool dependencia,bool sefiplan)
        {

            UnitOfWork uow = new UnitOfWork();

            Rol rol = uow.RolBusinessLogic.GetByID(id);
            rol.Clave = clave;
            rol.Nombre = nombre;
            rol.Orden = orden;
            rol.EsDependencia = dependencia;
            rol.EsSefiplan = sefiplan;

            uow.RolBusinessLogic.Update(rol);

            uow.SaveChanges();

            if (uow.Errors.Count > 0)
            {
                return new result { OK = false, Errors = uow.Errors };
            }


            return new result { OK = true };
        }

        [WebMethod]
        public static result DeleteRecord(int id)
        {

            UnitOfWork uow = new UnitOfWork();

            Rol rol = uow.RolBusinessLogic.GetByID(id);

            uow.RolBusinessLogic.Delete(rol);

            uow.SaveChanges();

            if (uow.Errors.Count > 0)
            {
                return new result { OK = false, Errors = uow.Errors };
            }


            return new result { OK = true };
        }

        public class result 
        {
            public bool OK { get; set; }          
            public List<string> Errors { get; set; }
        }


    }
}
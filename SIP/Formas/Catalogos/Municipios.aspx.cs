using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SIP.Formas.Catalogos
{
    public partial class Municipios : System.Web.UI.Page
    {      

        protected void Page_Load(object sender, EventArgs e)
        {
            //
        }


        [WebMethod]
        public static IQueryable GetList()
        {
            UnitOfWork uow = new UnitOfWork();
            var list = uow.MunicipioBusinessLogic.Get().Select(e => new { Id=e.Id,Clave=e.Clave,Nombre=e.Nombre,Orden=e.Orden});
            return list;
        }

        [WebMethod]
        public static object GetRecord(int id)
        {

            UnitOfWork uow = new UnitOfWork();

            Municipio municipio = uow.MunicipioBusinessLogic.GetByID(id);

            return new {Id=municipio.Id,Clave=municipio.Clave,Nombre=municipio.Nombre,Orden=municipio.Orden };
           
        }

        [WebMethod(EnableSession=true)]
        public static object AddRecord(string clave, string nombre, int orden)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());

            Municipio municipio = new Municipio { Clave = clave, Nombre = nombre, Orden = orden };  

            //Validar clave y número de orden

            var list = uow.MunicipioBusinessLogic.Get(m=>m.Clave==clave);

            if (list.Count() > 0) uow.Errors.Add("La clave que intenta ingresar ya existe");

            list = uow.MunicipioBusinessLogic.Get(m => m.Orden == orden);

            if (list.Count() > 0) uow.Errors.Add("El número de orden que intenta ingresar ya existe");

            if (uow.Errors.Count > 0) return uow.GetResult();
                        

            uow.MunicipioBusinessLogic.Insert(municipio);                            

            uow.SaveChanges();

            return uow.GetResult();

        }

        [WebMethod(EnableSession=true)]
        public static object UpdateRecord(int id, string clave, string nombre, int orden)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());

            //Validar duplicidad en clave y número de orden

            var list = uow.MunicipioBusinessLogic.Get(m => m.Clave == clave & m.Id != id);

            if (list.Count() > 0) uow.Errors.Add("La clave que intenta ingresar ya existe");

            list = uow.MunicipioBusinessLogic.Get(m => m.Orden == orden & m.Id!=id);

            if (list.Count() > 0) uow.Errors.Add("El número de orden que intenta ingresar ya existe");

            if (uow.Errors.Count > 0) return uow.GetResult();
                        

            Municipio municipio = uow.MunicipioBusinessLogic.GetByID(id);
            municipio.Clave = clave;
            municipio.Nombre = nombre;
            municipio.Orden = orden;               
          
            uow.MunicipioBusinessLogic.Update(municipio);

            uow.SaveChanges();                      

            return uow.GetResult();
        }

        [WebMethod(EnableSession=true)]
        public static object DeleteRecord(int id)
        {

            UnitOfWork uow = new UnitOfWork(HttpContext.Current.Session["IdUser"].ToString());

            Municipio municipio = uow.MunicipioBusinessLogic.GetByID(id);

            uow.MunicipioBusinessLogic.Delete(municipio);

            uow.SaveChanges();
         
            return uow.GetResult();
        }

      
    }
}
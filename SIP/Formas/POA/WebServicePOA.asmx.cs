using AjaxControlToolkit;
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SIP.Formas.POA
{
    /// <summary>
    /// Descripción breve de WebServicePOA
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class WebServicePOA : System.Web.Services.WebService
    {        

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetProgramas(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();

            var programas = uow.AperturaProgramaticaBusinessLogic.Get(ap=>ap.ParentId==null,orderBy:ap=>ap.OrderBy(r=>r.Orden));

            foreach (var item in programas)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Nombre, value = item.Id.ToString() });           
            }           
           
            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetSubProgramas(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();

            int programaId =Utilerias.StrToInt(CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["programaId"]);

            var subprogramas = uow.AperturaProgramaticaBusinessLogic.Get(ap => ap.ParentId == programaId, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in subprogramas)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Nombre, value = item.Id.ToString() });
            }

            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetSubSubProgramas(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();

            int subprogramaId = Utilerias.StrToInt(CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["subprogramaId"]);

            var subsubprogramas = uow.AperturaProgramaticaBusinessLogic.Get(ap => ap.ParentId == subprogramaId, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in subsubprogramas)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Nombre, value = item.Id.ToString() });
            }

            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetMetas(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();

            int subsubprogramaId = Utilerias.StrToInt(CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["subsubprogramaId"]);

            var metas = uow.AperturaProgramaticaMetaBusinessLogic.Get(apm => apm.AperturaProgramaticaId == subsubprogramaId, orderBy: ap => ap.OrderBy(r => r.AperturaProgramaticaUnidad.Orden));

            foreach (var item in metas)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Descripcion, value = item.Id.ToString() });
            }

            return list.ToArray();

        }


        [WebMethod]
        public CascadingDropDownNameValue[] GetFuncionalidadNivel1(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();                        

            var finalidades = uow.FuncionalidadBusinessLogic.Get(f => f.ParentId == null, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in finalidades)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Descripcion, value = item.Id.ToString() });
            }

            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetFuncionalidadNivel2(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();

            int finalidadId = Utilerias.StrToInt(CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["finalidadId"]);

            var funciones = uow.FuncionalidadBusinessLogic.Get(f => f.ParentId == finalidadId, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in funciones)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Descripcion, value = item.Id.ToString() });
            }

            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetFuncionalidadNivel3(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();

            int funcionId = Utilerias.StrToInt(CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["funcionId"]);

            var funciones = uow.FuncionalidadBusinessLogic.Get(f => f.ParentId == funcionId, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in funciones)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Descripcion, value = item.Id.ToString() });
            }

            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetEjePVDNivel1(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();
           

            var ejesAgrupadores = uow.EjeBusinessLogic.Get(f => f.ParentId == null, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in ejesAgrupadores)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Descripcion, value = item.Id.ToString() });
            }

            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetEjePVDNivel2(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();

            int ejeAgrupadorId = Utilerias.StrToInt(CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["ejeAgrupadorId"]);

            var ejes = uow.EjeBusinessLogic.Get(f => f.ParentId == ejeAgrupadorId, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in ejes)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Descripcion, value = item.Id.ToString() });
            }

            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetModalidadNivel1(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();


            var modalidadAgrupadores = uow.ModalidadBusinessLogic.Get(f => f.ParentId == null, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in modalidadAgrupadores)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Descripcion, value = item.Id.ToString() });
            }

            return list.ToArray();

        }

        [WebMethod]
        public CascadingDropDownNameValue[] GetModalidadNivel2(string knownCategoryValues, string category)
        {
            UnitOfWork uow = new UnitOfWork();

            List<CascadingDropDownNameValue> list = new List<CascadingDropDownNameValue>();

            int modalidadAgrupadorId = Utilerias.StrToInt(CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["modalidadAgrupadorId"]);

            var modalidades = uow.ModalidadBusinessLogic.Get(f => f.ParentId == modalidadAgrupadorId, orderBy: ap => ap.OrderBy(r => r.Orden));

            foreach (var item in modalidades)
            {
                list.Add(new CascadingDropDownNameValue { name = item.Descripcion, value = item.Id.ToString() });
            }

            return list.ToArray();

        }






    }
}

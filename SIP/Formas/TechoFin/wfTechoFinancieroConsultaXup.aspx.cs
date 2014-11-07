using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace SIP.Formas.TechoFin
{
    public partial class wfTechoFinancieroConsultaXup : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            int idEjercicio = int.Parse(Session["EjercicioId"].ToString());

            var lista = from tfup in uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get (p=>p.TechoFinanciero.EjercicioId == idEjercicio).ToList()
                        select new { tfup.Id, tfup.UnidadPresupuestal, tfup.UnidadPresupuestalId, Financiamiento = tfup.TechoFinanciero.Descripcion , tfup.Importe, ImporteAsignado = tfup.GetImporteAsignado() };

         
            this.grid.DataSource = lista.OrderBy(p=>p.UnidadPresupuestal.Nombre).ToList();
            this.grid.DataBind();




            
            var query = from item in uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinanciero.EjercicioId == idEjercicio).ToList()
                        group item by new { up = item.UnidadPresupuestal } into g
                        select new
                        {
                            Key = g.Key,
                            Suma = g.Sum(p => p.Importe)
                        };

            List<TechoFinancieroUnidadPresupuestal> listaUP;

            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("Nombre");
            table.Columns.Add("Importe", typeof(decimal));
            table.Columns.Add("ImporteAsignado", typeof(decimal));

            query = query.OrderBy(p => p.Key.up.Nombre).ToList();

            foreach (var item in query)
            {
                DataRow row = table.NewRow();
            
    
                row["id"] = item.Key.up.Id;
                row["Nombre"] = item.Key.up.Nombre;
                row["Importe"] = item.Suma;

                listaUP = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinanciero.EjercicioId == idEjercicio && p.UnidadPresupuestalId == item.Key.up.Id).ToList();
                row["ImporteAsignado"] = listaUP.Sum(p => p.GetImporteAsignado());

                table.Rows.Add(row);
            }





            this.grid2.DataSource = table;
            this.grid2.DataBind();
        
        
        }


    }
}
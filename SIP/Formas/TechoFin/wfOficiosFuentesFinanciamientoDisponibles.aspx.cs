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
using SIP.rpts;

namespace SIP.Formas.TechoFin
{
    public partial class wfOficiosFuentesFinanciamientoDisponibles : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                bindGrid();
                divEdicion.Style.Add("display","none");
                _URLVisor.Value = ResolveClientUrl("~/rpts/wfVerReporte.aspx");
            }
        }

        private void bindGrid()
        {
            int idEjercicio = int.Parse(Session["EjercicioId"].ToString());
    
            //List<TechoFinancieroUnidadPresupuestal> lista = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p=>p.TechoFinanciero.EjercicioId == idEjercicio).ToList();


            var query = from item in uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinanciero.EjercicioId == idEjercicio).ToList()
                        group item by new { up = item.UnidadPresupuestal, oficioA = item.NumOficioAsignacionPresupuestal, oficioB = item.NumOficioAlcance, obs = item.ObservacionesAlcance } into g
                        select new
                        {
                            Key = g.Key,
                            Suma = g.Sum(p => p.ImporteInicial)
                        };

            DataTable table = new DataTable();
            table.Columns.Add("id");
            table.Columns.Add("Nombre");
            table.Columns.Add("Importe", typeof(decimal));
            table.Columns.Add("OficioA");
            table.Columns.Add("OficioB");
            table.Columns.Add("Observaciones");

            query = query.OrderBy(p => p.Key.up.Nombre).ToList();

            foreach (var item in query)
            {
                DataRow row = table.NewRow();


                row["id"] = item.Key.up.Id;
                row["Nombre"] = item.Key.up.Nombre;
                row["Importe"] = item.Suma;
                row["OficioA"] = item.Key.oficioA;
                row["OficioB"] = item.Key.oficioB;
                row["Observaciones"] = item.Key.obs;
                
                table.Rows.Add(row);
            }


            this.grid.DataSource = table;
            this.grid.DataBind();

        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idUP.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            int id = int.Parse(_idUP.Text);

            //TechoFinancieroUnidadPresupuestal tfup = uow.TechoFinancieroUnidadPresuestalBusinessLogic.GetByID(id);

            TechoFinancieroUnidadPresupuestal tfup = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p=>p.UnidadPresupuestalId == id).First();

            txtOficioA.Value = tfup.NumOficioAsignacionPresupuestal;
            txtOficioB.Value = tfup.NumOficioAlcance;
            txtObservaciones.Value = tfup.ObservacionesAlcance;
            dtpFechaA.Value = tfup.FechaOficioAsignacionPresupuestal.ToString();
            dtpFechaB.Value = tfup.FechaOficioAlcance.ToString();

            divEdicion.Style.Add("display", "block");
            divUPS.Style.Add("display", "none");
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            int id = int.Parse(_idUP.Text);
            
            
            List<TechoFinancieroUnidadPresupuestal> lista = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p=>p.UnidadPresupuestalId == id).ToList();

            foreach (TechoFinancieroUnidadPresupuestal item in lista)
            {
                item.NumOficioAsignacionPresupuestal = txtOficioA.Value;
                item.NumOficioAlcance = txtOficioB.Value;
                item.ObservacionesAlcance = txtObservaciones.Value;
                item.FechaOficioAsignacionPresupuestal = DateTime.Parse(dtpFechaA.Value);
                item.FechaOficioAlcance = DateTime.Parse(dtpFechaB.Value);
                uow.TechoFinancieroUnidadPresuestalBusinessLogic.Update(item);
            }
            


            
            uow.SaveChanges();

            uow = new UnitOfWork();
            bindGrid();


            divEdicion.Style.Add("display", "none");
            divUPS.Style.Add("display", "block");

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            divEdicion.Style.Add("display", "none");
            divUPS.Style.Add("display", "block");
        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                int idUP = Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                int idEjercicio = int.Parse(Session["EjercicioId"].ToString());
                ImageButton imgSubdetalle = (ImageButton)e.Row.FindControl("imgSubdetalle");
                if (imgSubdetalle != null)
                    imgSubdetalle.Attributes["onclick"] = "fnc_AbrirReporte(" + idUP + "," + idEjercicio + ");return false;";
            }
            
        }

      

    }
}
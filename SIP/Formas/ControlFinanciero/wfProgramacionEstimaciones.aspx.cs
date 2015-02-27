using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using ClosedXML.Excel;
using System.Data;
using System.Data.SqlClient;


namespace SIP.Formas.ControlFinanciero
{
    public partial class wfProgramacionEstimaciones : System.Web.UI.Page
    {
        private UnitOfWork uow;
        private int idObra;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork(Session["IdUser"].ToString());
            idObra = int.Parse(Session["XidObra"].ToString());

            Obra obra = uow.ObraBusinessLogic.GetByID(idObra);


            
            

            


            if (!IsPostBack)
            {

                divMSGnoHayProgramaDeObra.Style.Add("display", "none");
                divTMP.Style.Add("display", "none");
                divAgregarEstimacion.Style.Add("display", "none");
                divGuardarEstimacionesProgramadas.Style.Add("display", "none");                
                divEstimacionesProgramadas.Style.Add("display", "none");
    

                if (obra.StatusControlFinanciero < 3)//no hay programadeobra registrado                
                    divMSGnoHayProgramaDeObra.Style.Add("display", "block");   
                    
                
                if (obra.StatusControlFinanciero == 3)//ya hay programa de obra
                {
                    
                    divTMP.Style.Add("display", "block");
                    divEstimacionesProgramadas.Style.Add("display", "block");
                    
                    cargarConceptosPendientes();
                    cargarEstimacionesProgramadas();

                    NextNumeroEstimacion();
                    
                }

                if (obra.StatusControlFinanciero > 3)//ya estan registradas las estimaciones programadas                                    
                    cargarEstimacionesProgramadas();
                    divEstimacionesProgramadas.Style.Add("display", "block");
                

                
            }
        }



        #region Eventos

        protected void btnAddEstimacion_Click(object sender, EventArgs e)
        {
            DateTime fechaEstimacion;

            idObra = int.Parse(Session["XidObra"].ToString());

            ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == idObra).First();

            fechaEstimacion =  DateTime.Parse( dtpFecha.Value);

            List<ProgramasDeObras> listaConceptos = uow.ProgramasDeObrasBL.Get(p => p.ContratoDeObra.ObraId == idObra && p.Status == 1 && p.Termino <= fechaEstimacion).ToList();

            if (listaConceptos.Count == 0)
                return;



            EstimacionesProgramadas estimacion = new EstimacionesProgramadas();
            PresupuestosContratados concepto;

            decimal importeEstimado, iva, total;
            decimal amortizacion, ret5, ret2, ret2bis;

            importeEstimado = 0;
            
         

            foreach (ProgramasDeObras item in listaConceptos)
            {
                concepto = uow.PresupuestosContratadosBL.GetByID(item.PresupuestoContratadoId);
                importeEstimado = importeEstimado + concepto.Subtotal;                
            }

            iva = importeEstimado * decimal.Parse("0.16");
            total = importeEstimado * decimal.Parse("1.16");

            amortizacion = decimal.Parse(contrato.PorcentajeDeAnticipo.ToString()) / decimal.Parse("100");
            if (contrato.Descontar5AlMillar) { ret5 = decimal.Parse("0.005"); } else { ret5 = 0; }
            if (contrato.Descontar2AlMillar) { ret2 = decimal.Parse("0.002"); } else { ret2 = 0; }
            if (contrato.Descontar2AlMillarSV) { ret2bis = decimal.Parse("0.002"); } else { ret2bis = 0; }
                        
            amortizacion = total * amortizacion;
            ret5 = total * ret5;
            ret2 = total * ret2;
            ret2bis = total * ret2bis;

            List<EstimacionesProgramadas> listaEstimacionesProgramadas = uow.EstimacionesProgramadasBL.Get(p => p.ContratoDeObra.ObraId == this.idObra).ToList();
            int NumEstimacion = 0;

            if (listaEstimacionesProgramadas.Count > 0)
                NumEstimacion = listaEstimacionesProgramadas.Max(p => p.NumeroDeEstimacion);

            NumEstimacion++;


            estimacion.ContratoDeObraId = contrato.Id;
            estimacion.NumeroDeEstimacion = NumEstimacion;
            estimacion.FechaDeEstimacion = fechaEstimacion;
            estimacion.ImporteEstimado = importeEstimado;
            estimacion.IVA = iva;
            estimacion.Total = total;
            estimacion.AmortizacionAnticipo = amortizacion;
            estimacion.Retencion5AlMillar = ret5;
            estimacion.Retencion2AlMillar = ret2;
            estimacion.Retencion2AlMillarSyV = ret2bis;
            estimacion.ImporteAPagar = total - (amortizacion + ret5 + ret2 +  ret2bis);

            uow.EstimacionesProgramadasBL.Insert(estimacion);


            foreach (ProgramasDeObras item in listaConceptos)
            {
                EstimacionesProgramadasConceptos conceptoProgramado = new EstimacionesProgramadasConceptos();

                conceptoProgramado.EstimacionProgramada = estimacion;
                conceptoProgramado.PresupuestoContratadoId = item.PresupuestoContratadoId;
                conceptoProgramado.Cantidad = item.PresupuestoContratado.Cantidad;
                conceptoProgramado.Subtotal = item.PresupuestoContratado.Subtotal;

                uow.EstimacionesProgramadasConceptosBL.Insert(conceptoProgramado);

                item.Status = 2;
                uow.ProgramasDeObrasBL.Update(item);
            }

            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {

                cargarConceptosPendientes();
                cargarEstimacionesProgramadas();
                NextNumeroEstimacion();
                dtpFecha.Value = string.Empty;
            }





        }
        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {

            idObra = int.Parse(Session["XidObra"].ToString());
            Obra obra = uow.ObraBusinessLogic.GetByID(idObra);

            if (obra.StatusControlFinanciero < 4)
            {
                GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;

                EstimacionesProgramadas estimacion = uow.EstimacionesProgramadasBL.GetByID(int.Parse(gridEstimaciones.DataKeys[row.RowIndex].Values["Id"].ToString()));

                List<EstimacionesProgramadasConceptos> listaConceptos = uow.EstimacionesProgramadasConceptosBL.Get(p => p.EstimacionProgramadaId == estimacion.Id).ToList();

                ProgramasDeObras conceptoProgramado;
                foreach (EstimacionesProgramadasConceptos item in listaConceptos)
                {
                    uow.EstimacionesProgramadasConceptosBL.Delete(item);

                    conceptoProgramado = uow.ProgramasDeObrasBL.Get(p => p.PresupuestoContratadoId == item.PresupuestoContratadoId).First();
                    conceptoProgramado.Status = 1;
                    uow.ProgramasDeObrasBL.Update(conceptoProgramado);
                }


                uow.EstimacionesProgramadasBL.Delete(estimacion);


                uow.SaveChanges();

                if (uow.Errors.Count == 0)
                {

                    cargarConceptosPendientes();
                    cargarEstimacionesProgramadas();
                }


            }



        }

        protected void btnGuardarProgramaDeEstimaciones_Click(object sender, EventArgs e)
        {
            idObra = int.Parse(Session["XidObra"].ToString());

            Obra obra = uow.ObraBusinessLogic.GetByID(idObra);
            obra.StatusControlFinanciero = 4;
            uow.ObraBusinessLogic.Update(obra);
            uow.SaveChanges();

            divMSGnoHayProgramaDeObra.Style.Add("display", "none");
            divTMP.Style.Add("display", "none");
            divAgregarEstimacion.Style.Add("display", "none");
            divGuardarEstimacionesProgramadas.Style.Add("display", "none");
            divEstimacionesProgramadas.Style.Add("display", "block");

        }

        protected void gridEstimaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int idEstimacion = Utilerias.StrToInt(gridEstimaciones.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                ImageButton imgBtnEliminar = (ImageButton)e.Row.FindControl("imgBtnEliminar");


                EstimacionesProgramadas estimacion = uow.EstimacionesProgramadasBL.GetByID(idEstimacion);
                List<EstimacionesProgramadas> lista = uow.EstimacionesProgramadasBL.Get(p => p.ContratoDeObraId == estimacion.ContratoDeObraId && p.NumeroDeEstimacion > estimacion.NumeroDeEstimacion).ToList();

                if (lista.Count > 0 || estimacion.ContratoDeObra.Obra.StatusControlFinanciero > 3)
                    imgBtnEliminar.Visible = false;

            }
        }

        #endregion


        #region metodos
        
        private void cargarEstimacionesProgramadas()
        {
            this.idObra = int.Parse(Session["XidObra"].ToString());

            this.gridEstimaciones.DataSource = uow.EstimacionesProgramadasBL.Get(p => p.ContratoDeObra.ObraId == this.idObra).ToList();
            this.gridEstimaciones.DataBind();
        }

        private void cargarConceptosPendientes()
        {
            this.idObra = int.Parse(Session["XidObra"].ToString());



            var query = from item in uow.ProgramasDeObrasBL.Get(p => p.ContratoDeObra.ObraId == this.idObra && p.Status == 1).ToList()
                        group item by new { item.Termino } into g
                        select new
                        {
                            Key = g.Key,
                            Suma = g.Sum(p => p.PresupuestoContratado.Subtotal)
                        };
            query = query.OrderBy(p => p.Key.Termino).ToList();

            DataTable table = new DataTable();
            table.Columns.Add("Id");
            table.Columns.Add("Fecha", typeof(DateTime));
            table.Columns.Add("NConceptos");
            table.Columns.Add("Subtotal", typeof(decimal));
            table.Columns.Add("IVA", typeof(decimal));
            table.Columns.Add("Total", typeof(decimal));
            table.Columns.Add("Anticipo", typeof(decimal));
            table.Columns.Add("Ret5", typeof(decimal));
            table.Columns.Add("Ret2", typeof(decimal));
            table.Columns.Add("Ret2Bis", typeof(decimal));
            table.Columns.Add("ImporteFinal", typeof(decimal));

            int i = 0;


            ContratosDeObra contrato = uow.ContratosDeObraBL.Get(p => p.ObraId == this.idObra).First();
            decimal anticipo, ret5, ret2, ret2bis, total;

            List<ProgramasDeObras> listaConceptos;

            foreach (var item in query)
            {
                DataRow row = table.NewRow();

                anticipo = decimal.Parse(contrato.PorcentajeDeAnticipo.ToString()) / decimal.Parse("100");
                if (contrato.Descontar5AlMillar) { ret5 = decimal.Parse("0.005"); } else { ret5 = 0; }
                if (contrato.Descontar2AlMillar) { ret2 = decimal.Parse("0.002"); } else { ret2 = 0; }
                if (contrato.Descontar2AlMillarSV ) { ret2bis = decimal.Parse("0.002"); } else { ret2bis = 0; }

                i++;

                total = item.Suma * decimal.Parse("1.16");
                anticipo = total * anticipo;
                ret5 = total * ret5;
                ret2 = total * ret2;
                ret2bis = total * ret2bis;

                listaConceptos = uow.ProgramasDeObrasBL.Get(p => p.ContratoDeObra.ObraId == this.idObra && p.Status == 1 && p.Termino == item.Key.Termino).ToList();

                row["Id"] = i;
                row["Fecha"] = item.Key.Termino;
                row["NConceptos"] = listaConceptos.Count;
                row["Subtotal"] = item.Suma;
                row["IVA"] = item.Suma * decimal.Parse("0.16");
                row["Total"] = total;
                row["Anticipo"] = anticipo;
                row["Ret5"] = ret5;
                row["Ret2"] = ret2;
                row["Ret2Bis"] = ret2bis;
                row["ImporteFinal"] = total - (anticipo + ret5 + ret2 + ret2bis);

                table.Rows.Add(row);
            }


            this.grid.DataSource = table;
            this.grid.DataBind();

            if (table.Rows.Count == 0)
            {
                this.divAgregarEstimacion.Style.Add("display", "none");
                this.divGuardarEstimacionesProgramadas.Style.Add("display", "block");
            }
            else
            {
                this.divAgregarEstimacion.Style.Add("display", "block");
                this.divGuardarEstimacionesProgramadas.Style.Add("display", "none");
            }

        }

        private void NextNumeroEstimacion()
        {
            List<EstimacionesProgramadas> listaEstimacionesProgramadas = uow.EstimacionesProgramadasBL.Get(p => p.ContratoDeObra.ObraId == this.idObra).ToList();


            int NumEstimacion = 0;

            if (listaEstimacionesProgramadas.Count > 0)
                NumEstimacion = listaEstimacionesProgramadas.Max(p => p.NumeroDeEstimacion);

            NumEstimacion++;

            txtNoEstimacion.Value = NumEstimacion.ToString();
        }
        #endregion

        

    }
}
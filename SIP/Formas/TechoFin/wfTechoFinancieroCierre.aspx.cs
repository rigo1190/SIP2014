using DataAccessLayer;
using DataAccessLayer.Models;
using BusinessLogicLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.TechoFin
{
    public partial class wfTechoFinancieroCierre : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            int idEjercicio = 0;

            if (!IsPostBack)
            {
                idEjercicio = int.Parse(Session["EjercicioId"].ToString());



                List<TechoFinanciero> lista = uow.TechoFinancieroBusinessLogic.Get(p => p.EjercicioId == idEjercicio
                                                                         && p.Importe >  ((p.detalleUnidadesPresupuestales.Count > 0 ) ?p.detalleUnidadesPresupuestales.Sum(q => q.Importe):0)).ToList();


                if (lista.Count == 0)
                {
                    divClose.Style.Add("display", "block");
                    divNoSePuedeCerrar.Style.Add("display", "none");
                }
                else
                {
                    divClose.Style.Add("display", "none");
                    divNoSePuedeCerrar.Style.Add("display", "block");
                }

            }
        }




        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            int ejercicio;

            ejercicio = int.Parse(Session["EjercicioId"].ToString());

            



            TechoFinancieroBitacora tfBit = new TechoFinancieroBitacora();
            tfBit.EjercicioId = ejercicio;
            tfBit.Movimiento = 1;
            tfBit.Tipo = EnumTipoMovimientoTechoFinanciero.CargaInicial;
            tfBit.Fecha = DateTime.Now;
            uow.TechoFinancieroBitacoraBL.Insert(tfBit);


            List<TechoFinancieroUnidadPresupuestal> lista = uow.TechoFinancieroUnidadPresuestalBusinessLogic.Get(p => p.TechoFinanciero.EjercicioId == ejercicio).ToList();

            foreach (TechoFinancieroUnidadPresupuestal elemento in lista) {
                TechoFinancieroBitacoraMovimientos tfBitMov = new TechoFinancieroBitacoraMovimientos();

                tfBitMov.TechoFinancieroBitacoraId = tfBit.Id;
                tfBitMov.TechoFinancieroId = elemento.TechoFinancieroId;
                tfBitMov.UnidadPresupuestalId = elemento.UnidadPresupuestalId;
                tfBitMov.Incremento = elemento.Importe;
                tfBitMov.Decremento = 0;

                uow.TechoFinancieroBitacoraMovimientosBL.Insert(tfBitMov);

            }



            TechoFinancieroStatus obj = uow.TechoFinancieroStatusBusinessLogic.Get(p => p.EjercicioId == ejercicio).First();
            obj.Status = 2;
            uow.TechoFinancieroStatusBusinessLogic.Update(obj);


            uow.SaveChanges();

            //if (uow.Errors.Count > 0)
            //    uow = new UnitOfWork();



            Response.Redirect("wfTechoFinanciero.aspx");

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("wfTechoFinanciero.aspx");
        }
    }
}
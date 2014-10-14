using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.Catalogos
{
    public partial class catPlanesSectoriales : System.Web.UI.Page
    {
        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                BindGrid();
                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "none");
            }           
        }

        private void BindGrid()
        {
            this.grid.DataSource = uow.PlanSectorialBusinessLogic.Get().ToList();
            this.grid.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            _Accion.Text = "Nuevo";

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "update";

            PlanSectorial ps = uow.PlanSectorialBusinessLogic.GetByID(int.Parse(_ElId.Text));
            BindCatalogo(ps);


            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _ElId.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            PlanSectorial ps = uow.PlanSectorialBusinessLogic.GetByID(int.Parse(_ElId.Text));



            uow.Errors.Clear();
            List<POADetalle> lista;
            lista = uow.POADetalleBusinessLogic.Get(p => p.PlanSectorialId == ps.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");



            if (uow.Errors.Count == 0)
            {
                uow.PlanSectorialBusinessLogic.Delete(ps);
                uow.SaveChanges();
            }


            if (uow.Errors.Count == 0)
            {
                BindGrid();
                lblMensajeSuccess.Text = "El registro se ha eliminado correctamente";

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");

            }

            else
            {
                string mensaje;

                divMsg.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");

                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMensajes.Text = mensaje;
            }
        }


        protected void btnCrear_Click(object sender, EventArgs e)
        {
            PlanSectorial ps;
            List<PlanSectorial> lista;
            string mensaje = "";
            int orden;

            if (_Accion.Text == "Nuevo")
                ps = new PlanSectorial();
            else
                ps = uow.PlanSectorialBusinessLogic.GetByID(int.Parse(_ElId.Text));

            ps.Clave = txtClave.Value;
            ps.Descripcion = txtDescripcion.Value;


            if (_Accion.Text == "Nuevo")
            {
                lista = uow.PlanSectorialBusinessLogic.Get().ToList();

                orden = lista.Max(p => p.Orden);
                orden++;
                ps.Orden = orden;
            }


            


            //Validaciones
            uow.Errors.Clear();
            if (_Accion.Text == "Nuevo")
            {

                lista = uow.PlanSectorialBusinessLogic.Get(p => p.Clave == ps.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.PlanSectorialBusinessLogic.Get(p => p.Descripcion == ps.Descripcion).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.PlanSectorialBusinessLogic.Insert(ps);
                mensaje = "El nuevo plan sectorial ha sido registrado correctamente";

            }
            else//Update
            {

                int xid;

                xid = int.Parse(_ElId.Text);

                lista = uow.PlanSectorialBusinessLogic.Get(p => p.Id != xid && p.Clave == ps.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.PlanSectorialBusinessLogic.Get(p => p.Id != xid && p.Descripcion == ps.Descripcion).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.PlanSectorialBusinessLogic.Update(ps);
                mensaje = "Los cambios se registraron satisfactoriamente";
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_EjecutarMensaje('" + mensaje + "')", true);
                txtClave.Value = string.Empty;
                txtDescripcion.Value = string.Empty;

                BindGrid();

                lblMensajeSuccess.Text = mensaje;

                divEdicion.Style.Add("display", "none");
                divBtnNuevo.Style.Add("display", "block");
                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");

            }
            else
            {
                divMsg.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");

                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";



                lblMensajes.Text = mensaje;

            }
        }

        public void BindCatalogo(PlanSectorial ps)
        {
            txtClave.Value = ps.Clave;
            txtDescripcion.Value = ps.Descripcion;
            _ElId.Text = ps.Id.ToString();
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            BindGrid();

            divBtnNuevo.Style.Add("display", "block");

            divEdicion.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        

    }
}
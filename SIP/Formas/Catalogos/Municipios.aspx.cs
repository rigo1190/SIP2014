﻿using BusinessLogicLayer;
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
    public partial class Municipios : System.Web.UI.Page
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
            this.grid.DataSource = uow.MunicipioBusinessLogic.Get().ToList();
            this.grid.DataBind();
        }


               

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            int orden;
            _Accion.Text = "Nuevo";

            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");

            List<Municipio> lista = uow.MunicipioBusinessLogic.Get().ToList();

            orden = lista.Max(p => p.Orden);
            orden++;
            txtOrden.Value = orden.ToString();
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idMUN.Text= grid.DataKeys[row.RowIndex].Values["Id"].ToString();
            _Accion.Text = "update";

            Municipio mun = uow.MunicipioBusinessLogic.GetByID(int.Parse(_idMUN.Text));
            BindCatalogo(mun);


            divEdicion.Style.Add("display", "block");
            divBtnNuevo.Style.Add("display", "none");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void imgBtnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _idMUN.Text = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            Municipio mun = uow.MunicipioBusinessLogic.GetByID(int.Parse(_idMUN.Text));



            uow.Errors.Clear();
            List<POADetalle> lista;
            lista = uow.POADetalleBusinessLogic.Get(p => p.MunicipioId == mun.Id).ToList();
                

            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");



            if (uow.Errors.Count == 0)
            {
                uow.MunicipioBusinessLogic.Delete(mun);
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
            
            Municipio mun;
            List<Municipio> lista;
            string mensaje = "";

            if (_Accion.Text == "Nuevo")
                mun = new Municipio();
            else
                mun = uow.MunicipioBusinessLogic.GetByID(int.Parse(_idMUN.Text));
                

            mun.Clave = txtClave.Value;
            mun.Nombre = txtNombre.Value;
            mun.Orden = int.Parse(txtOrden.Value);


            //Validaciones
            uow.Errors.Clear();
            if (_Accion.Text == "Nuevo")
            {

                lista = uow.MunicipioBusinessLogic.Get(p => p.Clave == mun.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.MunicipioBusinessLogic.Get(p => p.Nombre == mun.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.MunicipioBusinessLogic.Get(p => p.Orden == mun.Orden).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El número de orden que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.MunicipioBusinessLogic.Insert(mun);
                mensaje = "El nuevo municipio ha sido registrado correctamente";




            }
            else//Update
            {

                int xid;

                xid = int.Parse(_idMUN.Text);

                lista = uow.MunicipioBusinessLogic.Get(p => p.Id != xid && p.Clave == mun.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.MunicipioBusinessLogic.Get(p => p.Id != xid && p.Nombre == mun.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El nombre que capturo ya ha sido registrada anteriormente, verifique su información");


                lista = uow.MunicipioBusinessLogic.Get(p => p.Id != xid && p.Orden == mun.Orden).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("El número de orden que capturo ya ha sido registrada anteriormente, verifique su información");




                uow.MunicipioBusinessLogic.Update(mun);
                mensaje = "Los cambios se registraron satisfactoriamente";
            }





            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_EjecutarMensaje('" + mensaje + "')", true);
                txtClave.Value = string.Empty;
                txtNombre.Value = string.Empty;
                txtOrden.Value = string.Empty;

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




        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            BindGrid();

            divBtnNuevo.Style.Add("display", "block");

            divEdicion.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }




        public void BindCatalogo(Municipio UP)
        {
            txtClave.Value = UP.Clave;
            txtNombre.Value = UP.Nombre;
            txtOrden.Value = UP.Orden.ToString();
            _idMUN.Text = UP.Id.ToString();
        }
       

       


    }
}
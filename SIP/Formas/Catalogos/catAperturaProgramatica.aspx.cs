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
    public partial class catAperturaProgramatica : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
                       

            if (!IsPostBack) {
                CargarArbol();

                divcaptura.Style.Add("display", "none");
                divMetas.Style.Add("display", "none");
                btnMenuCancelar.Style.Add("display", "none");
                btnMenuGuardar.Style.Add("display", "none");

                if (treeMain.Nodes.Count > 0)
                {
                    BindControles(treeMain.Nodes[0]);
                    treeMain.Nodes[0].Select();
                    treeMain.ExpandAll();
                }

                ddlBeneficiario.DataSource = uow.AperturaProgramaticaBeneficiarioBusinessLogic.Get().ToList();
                ddlBeneficiario.DataValueField = "Id";
                ddlBeneficiario.DataTextField = "Nombre";
                ddlBeneficiario.DataBind();


                ddlUnidad.DataSource = uow.AperturaProgramaticaUnidadBusinessLogic.Get().ToList();
                ddlUnidad.DataValueField = "Id";
                ddlUnidad.DataTextField = "Nombre";
                ddlUnidad.DataBind();

                BindGrid();

                btnMetaGuardar.Style.Add("display", "block");
                btnMetaUpdate.Style.Add("display", "none");
                btnMetaCancelarCambios.Style.Add("display", "none");
            }


        }


        


        #region Eventos
        protected void treeMain_SelectedNodeChanged(object sender, EventArgs e)
        {
            BindControles(treeMain.SelectedNode);
            divMsg.Style.Add("display", "none");
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AperturaProgramatica obj;
            AperturaProgramatica objOrigen;

            List<AperturaProgramatica> lista;
            String mensaje = "";
            int orden;

            if (_Accion.Value == "Nuevo")
                obj = new AperturaProgramatica();
            else
                obj = uow.AperturaProgramaticaBusinessLogic.GetByID(int.Parse(_ElId.Value));


            obj.EjercicioId = 6;
            obj.Clave = txtClave.Text;
            obj.Nombre = txtNombre.Text;

            obj.EsObraOAccion = enumObraAccion.Obra;

            lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == null).ToList();


            if (_ElId.Value == "")
            {
                obj.ParentId = null;
                obj.Nivel = 1;
                obj.Orden = 1;

            }
            else
            {


                if (_Accion.Value == "Nuevo")
                {
                    objOrigen = uow.AperturaProgramaticaBusinessLogic.GetByID(int.Parse(_ElId.Value));

                    if (objOrigen.ParentId == null)
                    { //estoy en la primera rama

                        if (_Tipo.Value == "Grupo")
                        {//agregando un registro del mismo nivel
                            obj.ParentId = null;
                            obj.Nivel = 1;

                            lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == null).ToList();

                        }
                        else
                        {//agregando un subnivel

                            obj.ParentId = objOrigen.Id;
                            obj.Nivel = 2;

                            lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == objOrigen.Id).ToList();
                        }


                    }
                    else
                    {// estoy en una segunda o tercera rama

                        if (_Tipo.Value == "Grupo")
                        {//agregando un registro del mismo nivel
                            obj.ParentId = objOrigen.ParentId;
                            obj.Nivel = objOrigen.Nivel;

                            lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == objOrigen.ParentId).ToList();
                        }
                        else
                        {//agregando un subnivel

                            if (_Nivel.Value == "2")
                            {
                                obj.ParentId = objOrigen.Id;
                                obj.Nivel = 3;

                                lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == objOrigen.Id).ToList();
                            }

                            if (_Nivel.Value == "3")
                            {
                                obj.ParentId = objOrigen.ParentId;
                                obj.Nivel = 3;

                                lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == objOrigen.ParentId).ToList();
                            }

                        }


                    }

                    if (lista.Count == 0)
                        orden = 0;
                    else
                        orden = lista.Max(p => p.Orden);


                    orden++;
                    obj.Orden = orden;

                }//es nuevo

            }//elId





            //validaciones
            uow.Errors.Clear();

            if (_Accion.Value == "Nuevo")
            {
                lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.Clave == obj.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");

                lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.Nombre == obj.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Descripción que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.AperturaProgramaticaBusinessLogic.Insert(obj);
                mensaje = "El registro se ha  almacenado correctamente";

            }
            else
            { //update

                int xid;

                xid = int.Parse(_ElId.Value);

                lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.Id != xid && p.Clave == obj.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");



                lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.Id != xid && p.Nombre == obj.Nombre).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Descripción que capturo ya ha sido registrada anteriormente, verifique su información");


                uow.AperturaProgramaticaBusinessLogic.Update(obj);
                mensaje = "Los cambios se registraron satisfactoriamente";

            }


            if (uow.Errors.Count == 0)
                uow.SaveChanges();


            if (uow.Errors.Count == 0)//Integrando el nuevo nodo en el arbol
            {
                TreeNode node = null;
                switch (_Accion.Value)
                {
                    case "Nuevo":


                        if (_ElId.Value == "")
                        {
                            node = new TreeNode();
                            node.Value = obj.Id.ToString();
                            node.Text = obj.Clave + " " + obj.Nombre;
                            treeMain.Nodes.Add(node);
                        }
                        else
                        {
                            objOrigen = uow.AperturaProgramaticaBusinessLogic.GetByID(int.Parse(_ElId.Value));

                            node = new TreeNode();
                            node.Value = obj.Id.ToString();
                            node.Text = obj.Clave + " " + obj.Nombre;

                            if (_Tipo.Value == "Grupo")//registro
                            {
                                if (objOrigen.Nivel == 1)
                                    treeMain.Nodes.Add(node);
                                else
                                    treeMain.SelectedNode.Parent.ChildNodes.Add(node);
                            }
                            else//Subnivel
                            {
                                if (objOrigen.Nivel < 3)//nivel 1 y 2
                                    treeMain.SelectedNode.ChildNodes.Add(node);
                                else//nivel 3                                    
                                    treeMain.SelectedNode.Parent.ChildNodes.Add(node);

                            }

                        }//elIde


                        BindControles(node);



                        break;
                    case "Modificar":
                        node = treeMain.FindNode(_rutaNodoSeleccionado.Value);

                        node.Value = obj.Id.ToString();
                        node.Text = obj.Clave + " " + obj.Nombre;
                        BindControles(node);

                        break;
                }

                //Se ocultan los botones de GUARDAR Y CANCELAR del menu y Normalizar pantallas de datos y captura
                divcaptura.Style.Add("display", "none");
                divMetas.Style.Add("display", "block");
                divArbol.Style.Add("display", "block");

                btnMenuCancelar.Style.Add("display", "none");
                btnMenuGuardar.Style.Add("display", "none");


                //Se habiltan las opciones del menu contextual
                addp.Enabled = true;
                addsp.Enabled = true;
                edit.Enabled = true;

                //Se habilita el arbol
                treeMain.Enabled = true;


                //Mensaje de Se actualizo correctamente
                lblMensajeSuccess.Text = mensaje;
                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");


            }
            else
            {
                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMensajes.Text = mensaje;
                divMsg.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");

                divArbol.Style.Add("display", "none");
                divcaptura.Style.Add("display", "block");
                divMetas.Style.Add("display", "none");

                SpanModificar.Style.Add("display", "none");
                SpanGrupo.Style.Add("display", "none");
                SpanFondo.Style.Add("display", "none");

                if (_Accion.Value == "Nuevo")
                {

                    if (_Tipo.Value == "Grupo")
                        SpanGrupo.Style.Add("display", "block");
                    else
                        SpanFondo.Style.Add("display", "block");
                }
                else
                {
                    SpanModificar.Style.Add("display", "block");

                }

            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            if (_ElId.Value == "")
            {
                txtClave.Text = "";
                txtNombre.Text = "";
                return;
            }

            AperturaProgramatica obj = uow.AperturaProgramaticaBusinessLogic.GetByID(int.Parse(_ElId.Value));

            if (obj != null)
            {
                txtClave.Text = obj.Clave;
                txtNombre.Text = obj.Nombre;

                //Se busca el nodo del arbol de fondos para colocarlo como seleccionado
                treeMain.FindNode(_rutaNodoSeleccionado.Value).Select();
            }

            divArbol.Style.Add("display", "block");
            divcaptura.Style.Add("display", "none");
            divMetas.Style.Add("display", "block");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (_ElId.Value == "")
                return;
            AperturaProgramatica obj = uow.AperturaProgramaticaBusinessLogic.GetByID(int.Parse(_ElId.Value));



            uow.Errors.Clear();
            List<POADetalle> lista;
            lista = uow.POADetalleBusinessLogic.Get(p => p.AperturaProgramaticaId == obj.Id).ToList();

            List<Obra> listaObra;
            listaObra = uow.ObraBusinessLogic.Get(p => p.AperturaProgramaticaId == obj.Id).ToList();

            List<AperturaProgramatica> listaParent;
            listaParent = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == obj.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");

            if (listaObra.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");

            if (listaParent.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");

            //Se elimina el objeto
            if (uow.Errors.Count == 0)
            {
                uow.AperturaProgramaticaBusinessLogic.Delete(obj);
                uow.SaveChanges();
            }



            if (uow.Errors.Count == 0)
            {
                //Se vuelve a RECONSTRUIR el ARBOL
                CargarArbol();

                //Se bindea el primer fondo, si existe
                if (treeMain.Nodes.Count > 0)
                    BindControles(treeMain.Nodes[0]);
                else
                {
                    BindControles(null);
                    ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_CargaInicial()", true);
                }

                lblMensajeSuccess.Text = "El registro se ha eliminado correctamente";
                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");

            }

            else
            {
                string mensaje;

                mensaje = string.Empty;
                foreach (string cad in uow.Errors)
                    mensaje = mensaje + cad + "<br>";

                lblMensajes.Text = mensaje;
                divMsg.Style.Add("display", "block");
                divMsgSuccess.Style.Add("display", "none");
            }
        }


        //metas
        protected void btnMetaGuardar_Click(object sender, EventArgs e)
        {
            AperturaProgramaticaMeta obj = new AperturaProgramaticaMeta();

            obj.AperturaProgramaticaId = int.Parse(_ElId.Value);
            obj.AperturaProgramaticaBeneficiarioId = int.Parse(ddlBeneficiario.SelectedValue);
            obj.AperturaProgramaticaUnidadId = int.Parse(ddlUnidad.SelectedValue);

            uow.AperturaProgramaticaMetaBusinessLogic.Insert(obj);
            uow.SaveChanges();

            if (uow.Errors.Count == 0) {
                BindGrid();

                lblMensajeSuccess.Text = "La Meta se ha registrado";

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");
            }

        }




        protected void imgBtnEditMeta_Click(object sender, ImageClickEventArgs e)
        {
            btnMetaGuardar.Style.Add("display", "none");
            btnMetaUpdate.Style.Add("display", "block");
            btnMetaCancelarCambios.Style.Add("display", "block");

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            _IdMeta.Value = grid.DataKeys[row.RowIndex].Values["Id"].ToString();

            AperturaProgramaticaMeta meta = uow.AperturaProgramaticaMetaBusinessLogic.GetByID(int.Parse(_IdMeta.Value));

            ddlBeneficiario.SelectedValue = meta.AperturaProgramaticaBeneficiarioId.ToString();
            ddlUnidad.SelectedValue = meta.AperturaProgramaticaUnidadId.ToString();

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void btnMetaUpdate_Click(object sender, EventArgs e)
        {
            AperturaProgramaticaMeta meta = uow.AperturaProgramaticaMetaBusinessLogic.GetByID(int.Parse(_IdMeta.Value));
            meta.AperturaProgramaticaBeneficiarioId = int.Parse(ddlBeneficiario.SelectedValue);
            meta.AperturaProgramaticaUnidadId = int.Parse(ddlUnidad.SelectedValue);

            uow.AperturaProgramaticaMetaBusinessLogic.Update(meta);
            uow.SaveChanges();

            if (uow.Errors.Count == 0)
            {
                BindGrid();
                btnMetaGuardar.Style.Add("display", "block");
                btnMetaUpdate.Style.Add("display", "none");
                btnMetaCancelarCambios.Style.Add("display", "none");

                lblMensajeSuccess.Text = "La Meta se ha actualizado";

                divMsg.Style.Add("display", "none");
                divMsgSuccess.Style.Add("display", "block");
            }

        }

        protected void btnMetaCancelarCambios_Click(object sender, EventArgs e)
        {
            BindGrid();
            btnMetaGuardar.Style.Add("display", "block");
            btnMetaUpdate.Style.Add("display", "none");
            btnMetaCancelarCambios.Style.Add("display", "none");
        }

        protected void imgBtnEliminarMeta_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
            int idMeta;
            idMeta = int.Parse(grid.DataKeys[row.RowIndex].Values["Id"].ToString());

            AperturaProgramaticaMeta meta = uow.AperturaProgramaticaMetaBusinessLogic.GetByID(idMeta);

            uow.Errors.Clear();
            List<POADetalle> lista;
            lista = uow.POADetalleBusinessLogic.Get(p => p.AperturaProgramaticaMetaId == meta.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");



            if (uow.Errors.Count == 0)
            {
                uow.AperturaProgramaticaMetaBusinessLogic.Delete(meta);
                uow.SaveChanges();
            }


            if (uow.Errors.Count == 0)
            {
                BindGrid();
                lblMensajeSuccess.Text = "La Meta se ha eliminado correctamente";

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



        #endregion


        #region Metodos

        private void BindGrid()
        {
            int id;
            if (_ElId.Value == "")
                return;

            id = int.Parse(_ElId.Value);

            this.grid.DataSource = uow.AperturaProgramaticaMetaBusinessLogic.Get(p=> p.AperturaProgramaticaId == id,includeProperties:"AperturaProgramaticaUnidad,AperturaProgramaticaBeneficiario").ToList();
            this.grid.DataBind();
        }


        private void CargarArbol()
        {
            if (treeMain.Nodes.Count > 0)
            {
                treeMain.Nodes.Clear();
            }


            List<AperturaProgramatica> lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == null).ToList();

            foreach (AperturaProgramatica obj in lista)
            {
                //Se crea el nodo padre
                TreeNode nodeNew = new TreeNode();
                nodeNew.Text = obj.Clave + " " + obj.Nombre;
                nodeNew.Value = obj.Id.ToString();

                treeMain.Nodes.Add(nodeNew);

                if (obj.DetalleSubElementos.Count > 0)
                {
                    GenerarRamas(nodeNew, obj.DetalleSubElementos.ToList());
                }


            }

        }

        private void GenerarRamas(TreeNode nodeParent, List<AperturaProgramatica> lista)
        {
            foreach (AperturaProgramatica obj in lista)
            {
                //Se crea el nodo hijo
                TreeNode nodeChild = new TreeNode();
                nodeChild.Text = obj.Clave + " " + obj.Nombre;
                nodeChild.Value = obj.Id.ToString();
                nodeChild.Collapse();

                //Se agrega al nodo padre 
                nodeParent.ChildNodes.Add(nodeChild);

                if (obj.DetalleSubElementos.Count > 0)
                    GenerarRamas(nodeChild, obj.DetalleSubElementos.ToList());
            }
        }

        private void BindControles(TreeNode node)
        {
            AperturaProgramatica obj = null;

            if (node != null)
            {
                obj = uow.AperturaProgramaticaBusinessLogic.GetByID(int.Parse(node.Value));

                txtClave.Text = obj.Clave;
                txtNombre.Text = obj.Nombre;

                _ElId.Value = obj.Id.ToString();
                _Nivel.Value = obj.Nivel.ToString();
                _rutaNodoSeleccionado.Value = node.ValuePath;

                treeMain.FindNode(node.ValuePath).Select();

                List<AperturaProgramaticaMeta> listaMetas = uow.AperturaProgramaticaMetaBusinessLogic.Get(p=> p.AperturaProgramaticaId == obj.Id).ToList();

                if (listaMetas.Count > 0)
                {                    
                    BindGrid();
                    btnMetaGuardar.Style.Add("display", "block");
                    btnMetaUpdate.Style.Add("display", "none");
                    btnMetaCancelarCambios.Style.Add("display", "none");
                }
                    

            }
            else
            {
                txtClave.Text = string.Empty;
                txtNombre.Text = string.Empty;

                _ElId.Value = string.Empty;
                _Nivel.Value = string.Empty;
                _rutaNodoSeleccionado.Value = string.Empty;
            }


            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");


        }

        #endregion

       

        

        

        

        

        

       

    }
}
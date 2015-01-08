using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace SIP.Formas.Catalogos
{
    public partial class catFuncionalidad : System.Web.UI.Page
    {
        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                CargarArbol();

                divcaptura.Style.Add("display", "none");
                btnMenuCancelar.Style.Add("display", "none");
                btnMenuGuardar.Style.Add("display", "none");

                if (treeMain.Nodes.Count > 0)
                {
                    BindControles(treeMain.Nodes[0]);
                    treeMain.Nodes[0].Select();
                    treeMain.ExpandAll();
                }
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
            Funcionalidad obj;
            Funcionalidad objOrigen;

            List<Funcionalidad> lista;
            String mensaje = "";
            int orden;

            if (_Accion.Value == "Nuevo")
                obj = new Funcionalidad();
            else
                obj = uow.FuncionalidadBusinessLogic.GetByID(int.Parse(_ElId.Value));



            obj.Clave = txtClave.Text;
            obj.Descripcion = txtNombre.Text;
            

            lista = uow.FuncionalidadBusinessLogic.Get(p => p.ParentId == null).ToList();


            if (_ElId.Value == "")
            {
                obj.ParentId = null;
                obj.Nivel = 1;
                obj.Orden = 1;

            }else
            {

            
                if (_Accion.Value == "Nuevo")
                {
                    objOrigen = uow.FuncionalidadBusinessLogic.GetByID(int.Parse(_ElId.Value));

                    if (objOrigen.ParentId == null) { //estoy en la primera rama

                        if (_Tipo.Value == "Grupo"){//agregando un registro del mismo nivel
                            obj.ParentId = null;
                            obj.Nivel = 1;

                            lista = uow.FuncionalidadBusinessLogic.Get(p => p.ParentId == null).ToList();

                        }
                        else {//agregando un subnivel

                            obj.ParentId = objOrigen.Id;
                            obj.Nivel = 2;

                            lista = uow.FuncionalidadBusinessLogic.Get(p => p.ParentId == objOrigen.Id).ToList();
                        }


                    } else {// estoy en una segunda o tercera rama
                    
                        if (_Tipo.Value == "Grupo")
                        {//agregando un registro del mismo nivel
                            obj.ParentId = objOrigen.ParentId;
                            obj.Nivel = objOrigen.Nivel;

                            lista = uow.FuncionalidadBusinessLogic.Get(p => p.ParentId == objOrigen.ParentId).ToList();
                        }
                        else
                        {//agregando un subnivel

                            if (_Nivel.Value == "2")
                            {
                                obj.ParentId = objOrigen.Id;
                                obj.Nivel = 3;

                                lista = uow.FuncionalidadBusinessLogic.Get(p => p.ParentId == objOrigen.Id).ToList();
                            }

                            if (_Nivel.Value == "3")
                            {
                                obj.ParentId = objOrigen.ParentId;
                                obj.Nivel = 3;

                                lista = uow.FuncionalidadBusinessLogic.Get(p => p.ParentId == objOrigen.ParentId).ToList();
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
                lista = uow.FuncionalidadBusinessLogic.Get(p => p.Clave == obj.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");

                // En el catálogo de funcionalidades si es válido la existencia de descripciones repetidas
                
                //lista = uow.FuncionalidadBusinessLogic.Get(p => p.Descripcion == obj.Descripcion).ToList();
                //if (lista.Count > 0)
                //    uow.Errors.Add("La Descripción que capturo ya ha sido registrada anteriormente, verifique su información");
                
                uow.FuncionalidadBusinessLogic.Insert(obj);
                mensaje = "El registro se ha  almacenado correctamente";
                
            }
            else
            { //update

                int xid;

                xid = int.Parse(_ElId.Value);

                lista = uow.FuncionalidadBusinessLogic.Get(p => p.Id != xid && p.Clave == obj.Clave).ToList();
                if (lista.Count > 0)
                    uow.Errors.Add("La Clave que capturo ya ha sido registrada anteriormente, verifique su información");


                // En el catálogo de funcionalidades si es válido la existencia de descripciones repetidas

                //lista = uow.FuncionalidadBusinessLogic.Get(p => p.Id != xid && p.Descripcion == obj.Descripcion).ToList();
                //if (lista.Count > 0)
                //    uow.Errors.Add("La Descripción que capturo ya ha sido registrada anteriormente, verifique su información");

                uow.FuncionalidadBusinessLogic.Update(obj);
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
                            node.Text = obj.Clave + " " + obj.Descripcion;
                            treeMain.Nodes.Add(node);
                        }
                        else
                        {
                            objOrigen = uow.FuncionalidadBusinessLogic.GetByID(int.Parse(_ElId.Value));
                            
                            node = new TreeNode();
                            node.Value = obj.Id.ToString();
                            node.Text = obj.Clave + " " + obj.Descripcion;
                            
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
                        node.Text = obj.Clave + " " + obj.Descripcion;
                        BindControles(node);

                        break;
                }
                
                //Se ocultan los botones de GUARDAR Y CANCELAR del menu y Normalizar pantallas de datos y captura
                divcaptura.Style.Add("display", "none");
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

            Funcionalidad obj = uow.FuncionalidadBusinessLogic.GetByID(int.Parse(_ElId.Value));

            if (obj != null)
            {
                txtClave.Text = obj.Clave;
                txtNombre.Text = obj.Descripcion;
                
                //Se busca el nodo del arbol de fondos para colocarlo como seleccionado
                treeMain.FindNode(_rutaNodoSeleccionado.Value).Select();
            }

            divArbol.Style.Add("display", "block");
            divcaptura.Style.Add("display", "none");

            divMsg.Style.Add("display", "none");
            divMsgSuccess.Style.Add("display", "none");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {

            if (_ElId.Value == "")
                return;

            Funcionalidad obj = uow.FuncionalidadBusinessLogic.GetByID(int.Parse(_ElId.Value));



            uow.Errors.Clear();
            List<POADetalle> lista;
            lista = uow.POADetalleBusinessLogic.Get(p => p.FuncionalidadId == obj.Id).ToList();

            List<Obra> listaObra;
            listaObra = uow.ObraBusinessLogic.Get(p => p.FuncionalidadId == obj.Id).ToList();

            List<Funcionalidad> listaParent;
            listaParent = uow.FuncionalidadBusinessLogic.Get(p => p.ParentId == obj.Id).ToList();


            if (lista.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");

            if (listaObra.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");

            if (listaParent.Count > 0)
                uow.Errors.Add("El registro no puede eliminarse porque ya ha sido usado en el sistema");

            //Se elimina el objeto
            if (uow.Errors.Count == 0)
            {
                uow.FuncionalidadBusinessLogic.Delete(obj);
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
        #endregion


        #region Metodos

        private void CargarArbol()
        {
            if (treeMain.Nodes.Count > 0)
            {
                treeMain.Nodes.Clear();
            }


            List<Funcionalidad> lista = uow.FuncionalidadBusinessLogic.Get(p => p.ParentId == null).ToList();

            foreach (Funcionalidad obj in lista)
            {
                //Se crea el nodo padre
                TreeNode nodeNew = new TreeNode();
                nodeNew.Text = obj.Clave + " " + obj.Descripcion;
                nodeNew.Value = obj.Id.ToString();

                treeMain.Nodes.Add(nodeNew);

                if (obj.DetalleSubElementos.Count > 0)
                {
                    GenerarRamas(nodeNew, obj.DetalleSubElementos.ToList());
                }


            }

        }

        private void GenerarRamas(TreeNode nodeParent, List<Funcionalidad> lista)
        {
            foreach (Funcionalidad obj in lista)
            {
                //Se crea el nodo hijo
                TreeNode nodeChild = new TreeNode();
                nodeChild.Text = obj.Clave + " " + obj.Descripcion;
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
            Funcionalidad obj = null;
            

            if (node != null)
            {
                obj = uow.FuncionalidadBusinessLogic.GetByID(int.Parse(node.Value));

                txtClave.Text = obj.Clave;
                txtNombre.Text = obj.Descripcion;
                
                _ElId.Value = obj.Id.ToString();
                _Nivel.Value = obj.Nivel.ToString();
                _rutaNodoSeleccionado.Value = node.ValuePath;

                treeMain.FindNode(node.ValuePath).Select();
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
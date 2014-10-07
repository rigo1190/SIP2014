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

                _Padre.Value = "N";
                _Nivel.Value = "1";

                divguardar.Style.Add("display", "none");
                btnCancelar.Style.Add("display", "none");  //Se oculta opcion del menu contextual
                btnGuardar2.Style.Add("display", "none");  //Se oculta opcion del menu contextual


                txtClave.Enabled = false;
                txtNombre.Enabled = false;
                txtOrden.Disabled = true;


                //Se bindea los datos para el primer plantilla, si existe
                if (treeApertura.Nodes.Count > 0)
                {
                    BindControles(treeApertura.Nodes[0]);
                    treeApertura.Nodes[0].Select();
                    treeApertura.ExpandAll();
                }


            }

            //Evento que se ejecuta en JAVASCRIPT para evitar que se 'RESCROLLEE' el arbol al seleccionar un NODO y no se pierda el nodo seleccionado
            ClientScript.RegisterStartupScript(this.GetType(), "script", "SetSelectedTreeNodeVisible('<%= TreeViewName.ClientID %>_SelectedNode')", true);

        }


        #region eventos
        


        protected void treeApertura_SelectedNodeChanged(object sender, EventArgs e)
        {
            divmsg.Style.Add("display", "none");
            BindControles(treeApertura.SelectedNode);
            
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            string estado = _Accion.Value;
            string agregarPadre = _Padre.Value;

            AperturaProgramatica obj = null;
            AperturaProgramatica obj2 = new AperturaProgramatica();

            TreeNode node = null;
            string msg = "Se ha guardado correctamente";

            int idApertura = Utilerias.StrToInt(_IdApertura.Value);


            obj2 = uow.AperturaProgramaticaBusinessLogic.GetByID(idApertura);


            List<AperturaProgramatica> lista = new List<AperturaProgramatica>();
            lista = uow.AperturaProgramaticaBusinessLogic.Get().Where(p => p.ParentId == obj2.ParentId && p.Clave == txtClave.Text).ToList();

            if (lista.Count > 0) {

                lblMensajes.Text = "Esa clave ya ha sido asignada anteriormente";
                lblMensajes.ForeColor = System.Drawing.Color.Red;
                
                divmsg.Style.Add("display", "block");
                return;
            
            }



            if (estado.Equals("N"))
                obj = new AperturaProgramatica();
            else
            {
                msg = "Se ha actualizado correctamente";
                obj = uow.AperturaProgramaticaBusinessLogic.GetByID(idApertura);
            }

            obj.Clave = txtClave.Text;
            obj.Nombre = txtNombre.Text;
            obj.Orden = Utilerias.StrToInt(txtOrden.Value);

            int nivel;
            nivel = int.Parse(_Nivel.Value);




            switch (estado)
            {
                case "N":  //Se esta agregando un nuevo registro
                    //Agregar campos de bitacora

                    if (agregarPadre.Equals("S")){
                        node = new TreeNode(); //Si estan agregando un nuevo Plantilla PADRE, se crea un nuevo nodo PADRE
                    }                        
                    else
                    {
                        node = treeApertura.FindNode(_rutaNodoSeleccionado.Value); //Si se esta agregando un SUBPLANTILLA, se recupera el nodo padre
                        nivel++;
                    }

                    if (idApertura != null)
                    {                        
                        if (agregarPadre.Equals("S")) {
                            obj.ParentId = obj2.ParentId;
                        } else {
                            obj.ParentId = idApertura; 
                        }
                    }


                    obj.Nivel = nivel;
                    obj.EjercicioId = 6;

                    uow.AperturaProgramaticaBusinessLogic.Insert(obj); //Se guarda el nuevo objeto creado

                    break;
                case "A": //Se esta actualizando un registro
                    //Agregar campos de bitacora

                    uow.AperturaProgramaticaBusinessLogic.Update(obj); //Se actualiza el objeto

                    node = treeApertura.FindNode(_rutaNodoSeleccionado.Value);  //Si se esta actualizando, se recupera el nodo

                    break;
            }

            uow.SaveChanges();

            if (uow.Errors.Count > 0)
            {
                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
                lblMensajes.ForeColor = System.Drawing.Color.Red;

                //lblMensajes.Attributes.Add("class", "alert alert-danger");
                divmsg.Style.Add("display", "block");
                return;
            }




            //Se agrega el NODO, ya sea direcatamente al ARBOL o a un NODO PADRE, si se agrego uno nuevo
            switch (estado)
            {
                case "N":  //SE ESTA AGREGANDO UN NUEVA PLANTILLA

                    if (agregarPadre.Equals("S")) //SE ESTA AGREGANDO UNA PLANTILLA PADRE
                    {

                        if (obj.ParentId == null)
                        {
                            //Se colocan los valores al nodo recien creado como padre, y se agrega direcatamente en la raiz del arbol
                            node.Value = obj.Id.ToString();
                            node.Text = obj.Clave + " " + obj.Nombre;
                            treeApertura.Nodes.Add(node);
                        }
                        else {
                            //Se colocan los valores al nodo recien creado como padre, y se agrega direcatamente en la raiz del arbol
                            node.Value = obj.Id.ToString();
                            node.Text = obj.Clave + " " + obj.Nombre;
                            treeApertura.SelectedNode.Parent.ChildNodes.Add(node);
                        }
                        
                         



                        //Se bindean los datos con el nuevo NODO agregado
                        BindControles(node);
                    }
                    else  //SE ESTA AGREGANDO UNA PLANTILLA HIJO
                    {
                        //Se crea un nodo hijo y se agrega al NODO PADRE
                        TreeNode nodeChild = new TreeNode();
                        nodeChild.Value = obj.Id.ToString();
                        nodeChild.Text = obj.Clave + " " + obj.Nombre;
                        node.ChildNodes.Add(nodeChild);

                        //Se bindean los datos con el nuevo NODO HIJO agregado
                        BindControles(nodeChild);
                    }

                    break;

                case "A":  //SE ESTA ACTUALIZANDO UN FONDO
                    //Se bindean los datos con el nodo recuperado y con los datos recien actualizados
                    node.Value = obj.Id.ToString();
                    node.Text = obj.Nombre;
                    BindControles(node);
                    break;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;

            //Se ocultan los botones de GUARDAR Y CANCELAR
            divguardar.Style.Add("display", "none");
            btnCancelar.Style.Add("display", "none"); //Se oculta opcion del menu contextual
            btnGuardar2.Style.Add("display", "none");//Se oculta opcion del menu contextual
            divmsg.Style.Add("display", "block");

            //Se habiltan las opciones del menu contextual

            addp.Enabled = true;
            addsp.Enabled = true;
            
            edit.Enabled = true;

            //Se habilita el arbol
            treeApertura.Enabled = true;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            string msg = "Se ha eliminado correctamente";

            int id = Utilerias.StrToInt(_IdApertura.Value);

            AperturaProgramatica obj = uow.AperturaProgramaticaBusinessLogic.GetByID(id);

            //Se elimina el objeto
            uow.AperturaProgramaticaBusinessLogic.Delete(obj);
            uow.SaveChanges();

            if (uow.Errors.Count > 0) //Si hubo errores
            {
                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
                lblMensajes.ForeColor = System.Drawing.Color.Red;

                divmsg.Style.Add("display", "block");

                return;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;

            //Se vuelve a RECONSTRUIR el ARBOL
            CargarArbol();

            //Se bindea el primer fondo, si existe
            if  (treeApertura.Nodes.Count > 0)
                BindControles(treeApertura.Nodes[0]);
            else
            {
                BindControles(null);
                ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_CargaInicial()", true);
            }


            divmsg.Style.Add("display", "block");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Se vuelven a colocar los valores y a bloquear los controles

            int idAperturaActual = Utilerias.StrToInt(_IdApertura.Value);
            AperturaProgramatica obj = uow.AperturaProgramaticaBusinessLogic.GetByID(idAperturaActual);

            if (obj != null)
            {
                txtClave.Text = obj.Clave;
                txtNombre.Text = obj.Nombre;
                txtOrden.Value = obj.Orden.ToString();

                //Se busca el nodo del arbol de fondos para colocarlo como seleccionado
                treeApertura.FindNode(_rutaNodoSeleccionado.Value).Select();

            }
        }


        #endregion

        #region Metodos

        private void CargarArbol()
        {
            if (treeApertura.Nodes.Count > 0)
            {
                treeApertura.Nodes.Clear();
            }
                     
            List<AperturaProgramatica> lista = uow.AperturaProgramaticaBusinessLogic.Get(p => p.ParentId == null).ToList();

            foreach (AperturaProgramatica obj in lista)
            {
                //Se crea el nodo padre
                TreeNode nodeNew = new TreeNode();
                nodeNew.Text = obj.Clave + " " + obj.Nombre;
                nodeNew.Value = obj.Id.ToString();

                treeApertura.Nodes.Add(nodeNew);

                if (obj.DetalleSubElementos.Count > 0) {
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
                txtOrden.Value = obj.Orden.ToString();
                _IdApertura.Value = obj.Id.ToString();
                _rutaNodoSeleccionado.Value = node.ValuePath;
                _Nivel.Value = obj.Nivel.ToString();
                treeApertura.FindNode(node.ValuePath).Select();
            }
            else
            {
                txtClave.Text = string.Empty;
                txtNombre.Text = string.Empty;
                txtOrden.Value = string.Empty;
                _IdApertura.Value = string.Empty;
                _rutaNodoSeleccionado.Value = string.Empty;
                _Nivel.Value = "0";

            }



        }

        #endregion

       


        

        


       

    }
}
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
    public partial class PlantillasPadre : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            bool hayPlantillas = true;
            if (!IsPostBack)
            {
                BindArbol();
                BindDrops();

                _Padre.Value = "N";  //Se pone la bandera de que si se va a agregar un fondo padre en N

                divGuardar.Style.Add("display", "none");
                btnCancelar.Style.Add("display", "none");  //Se oculta opcion del menu contextual
                btnGuardar2.Style.Add("display", "none");  //Se oculta opcion del menu contextual


                txtClave.Enabled = false;
                txtDescripcion.Enabled = false;
                txtOrden.Disabled = true;
                ddlEjercicio.Enabled = false;


                //Se bindea los datos para el primer plantilla, si existe
                if (treePlantilla.Nodes.Count > 0)
                {
                    BindControles(treePlantilla.Nodes[0]);
                    treePlantilla.Nodes[0].Select();
                    treePlantilla.ExpandAll();
                }
                else
                {

                    ////Se ocultan opciones del menu contextul
                    //adds.Enabled = false;
                    //edit.Enabled = false;
                    hayPlantillas = false;
                }
            }

            //Evento que se ejecuta en JAVASCRIPT para evitar que se 'RESCROLLEE' el arbol al seleccionar un NODO y no se pierda el nodo seleccionado
            ClientScript.RegisterStartupScript(this.GetType(), "script", "SetSelectedTreeNodeVisible('<%= TreeViewName.ClientID %>_SelectedNode')", true);

        }


        #region EVENTOS
        protected void treePlantilla_SelectedNodeChanged(object sender, EventArgs e)
        {
            divMsg.Style.Add("display", "none");
            BindControles(treePlantilla.SelectedNode); //Se bindean los datos
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string estado = _Accion.Value;
            string agregarPadre = _Padre.Value;
            Plantilla obj = null;
            TreeNode node = null;
            string msg = "Se ha guardado correctamente";

            int idPlantilla = Utilerias.StrToInt(_IDPlantilla.Value);

            if (estado.Equals("N"))
                obj = new Plantilla();
            else
            {
                msg = "Se ha actualizado correctamente";
                obj = uow.PlantillaBusinessLogic.GetByID(idPlantilla);
            }

            obj.Clave = txtClave.Text;
            obj.Descripcion = txtDescripcion.Text;
            obj.EjercicioId = Utilerias.StrToInt(ddlEjercicio.SelectedValue);
            obj.Orden = Utilerias.StrToInt(txtOrden.Value);

            switch (estado)
            {
                case "N":  //Se esta agregando un nuevo registro
                    //Agregar campos de bitacora

                    if (agregarPadre.Equals("S"))
                        node = new TreeNode(); //Si estan agregando un nuevo Plantilla PADRE, se crea un nuevo nodo PADRE
                    else
                    {
                        node = treePlantilla.FindNode(_rutaNodoSeleccionado.Value); //Si se esta agregando un SUBPLANTILLA, se recupera el nodo padre
                        obj.DependeDeId = idPlantilla; //Se coloca el DependeId ala subplantilla recien creada
                    }

                    uow.PlantillaBusinessLogic.Insert(obj); //Se guarda el nuevo objeto creado

                    break;
                case "A": //Se esta actualizando un registro
                    //Agregar campos de bitacora

                    uow.PlantillaBusinessLogic.Update(obj); //Se actualiza el objeto

                    node = treePlantilla.FindNode(_rutaNodoSeleccionado.Value);  //Si se esta actualizando, se recupera el nodo

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
                divMsg.Style.Add("display", "block");
                return;
            }


            //Se agrega el NODO, ya sea direcatamente al ARBOL o a un NODO PADRE, si se agrego uno nuevo
            switch (estado)
            {
                case "N":  //SE ESTA AGREGANDO UN NUEVA PLANTILLA

                    if (agregarPadre.Equals("S")) //SE ESTA AGREGANDO UNA PLANTILLA PADRE
                    {
                        //Se colocan los valores al nodo recien creado como padre, y se agrega direcatamente en la raiz del arbol
                        node.Value = obj.Id.ToString();
                        node.Text = obj.Descripcion;
                        treePlantilla.Nodes.Add(node);

                        //Se bindean los datos con el nuevo NODO agregado

                        BindControles(node);
                    }
                    else  //SE ESTA AGREGANDO UNA PLANTILLA HIJO
                    {
                        //Se crea un nodo hijo y se agrega al NODO PADRE
                        TreeNode nodeChild = new TreeNode();
                        nodeChild.Value = obj.Id.ToString();
                        nodeChild.Text = obj.Descripcion;
                        node.ChildNodes.Add(nodeChild);

                        //Se bindean los datos con el nuevo NODO HIJO agregado
                        BindControles(nodeChild);
                    }

                    break;

                case "A":  //SE ESTA ACTUALIZANDO UN FONDO
                    //Se bindean los datos con el nodo recuperado y con los datos recien actualizados
                    node.Value = obj.Id.ToString();
                    node.Text = obj.Descripcion;
                    BindControles(node);
                    break;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;

            //Se ocultan los botones de GUARDAR Y CANCELAR
            divGuardar.Style.Add("display", "none");
            btnCancelar.Style.Add("display", "none"); //Se oculta opcion del menu contextual
            btnGuardar2.Style.Add("display", "none");//Se oculta opcion del menu contextual
            divMsg.Style.Add("display", "block");
            
            //Se habiltan las opciones del menu contextual
           
            add.Enabled = true;
            adds.Enabled = true;
            edit.Enabled = true;

            //Se habilita el arbol
            treePlantilla.Enabled = true;
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            string msg = "Se ha eliminado correctamente";

            int id = Utilerias.StrToInt(_IDPlantilla.Value);

            Plantilla obj = uow.PlantillaBusinessLogic.GetByID(id);

            //Se elimina el objeto
            uow.PlantillaBusinessLogic.Delete(obj);
            uow.SaveChanges();

            if (uow.Errors.Count > 0) //Si hubo errores
            {
                msg = string.Empty;
                foreach (string cad in uow.Errors)
                    msg += cad;

                lblMensajes.Text = msg;
                lblMensajes.ForeColor = System.Drawing.Color.Red;

                divMsg.Style.Add("display", "block");

                return;
            }

            lblMensajes.Text = msg;
            lblMensajes.ForeColor = System.Drawing.Color.Black;

            //Se vuelve a RECONSTRUIR el ARBOL
            BindArbol();

            //Se bindea el primer fondo, si existe
            if (treePlantilla.Nodes.Count > 0)
                BindControles(treePlantilla.Nodes[0]);
            else
            {
                BindControles(null);
                ClientScript.RegisterStartupScript(this.GetType(), "script", "fnc_CargaInicial()", true);
            }
                

            divMsg.Style.Add("display", "block");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            //Se vuelven a colocar los valores y a bloquear los controles

            int idPlantillaActual = Utilerias.StrToInt(_IDPlantilla.Value);
            Plantilla obj = uow.PlantillaBusinessLogic.GetByID(idPlantillaActual);

            if (obj != null)
            {
                txtDescripcion.Text = obj.Descripcion;
                txtClave.Text = obj.Clave;
                ddlEjercicio.SelectedValue = obj.EjercicioId.ToString();
                txtOrden.Value = obj.Orden.ToString();

                //Se busca el nodo del arbol de fondos para colocarlo como seleccionado
                treePlantilla.FindNode(_rutaNodoSeleccionado.Value).Select();
            }

           
        }


        #endregion


        #region METODOS

        private void BindArbol()
        {
            if (treePlantilla.Nodes.Count > 0)
            {
                treePlantilla.Nodes.Clear();
            }

            List<Plantilla> list = uow.PlantillaBusinessLogic.Get(e => e.DependeDeId == null).ToList();

            foreach (Plantilla obj in list)
            {
                //Se crea el nodo padre
                TreeNode nodeNew = new TreeNode();
                nodeNew.Text = obj.Descripcion;
                nodeNew.Value = obj.Id.ToString();


                treePlantilla.Nodes.Add(nodeNew); //Se agrega el nodo al arbol

                if (obj.Detalles.Count > 0) //Si se tienen plantillas hijos, se anidan mas nodos al nodo padre
                    ColocarFondosHijos(nodeNew, obj.Detalles.ToList());
            }

        }


        private void ColocarFondosHijos(TreeNode nodeParent, List<Plantilla> list)
        {
            foreach (Plantilla obj in list)
            {
                //Se crea el nodo hijo
                TreeNode nodeChild = new TreeNode();
                nodeChild.Text = obj.Descripcion;
                nodeChild.Value = obj.Id.ToString();
                nodeChild.Collapse();

                //Se agrega al nodo padre 
                nodeParent.ChildNodes.Add(nodeChild);

                if (obj.Detalles.Count > 0) //Si se tienen plantillas hijos, se anidan mas nodos al nodo padre
                    ColocarFondosHijos(nodeChild, obj.Detalles.ToList()); //Se manda a llamar la misma fucnion
            }
        }


        private void BindControles(TreeNode node)
        {
            Plantilla obj = null;

            if (node != null) 
            {
                obj = uow.PlantillaBusinessLogic.GetByID(Utilerias.StrToInt(node.Value));
                
                txtClave.Text = obj.Clave;
                txtDescripcion.Text = obj.Descripcion;
                ddlEjercicio.SelectedValue = obj.EjercicioId.ToString();
                txtOrden.Value = obj.Orden.ToString();
                _IDPlantilla.Value = obj.Id.ToString();
                _rutaNodoSeleccionado.Value = node.ValuePath;
                treePlantilla.FindNode(node.ValuePath).Select();
            }
            else
            {
                txtClave.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                
                if (ddlEjercicio.Items.Count > 0)
                    ddlEjercicio.SelectedIndex = 0;
                
                txtOrden.Value = string.Empty;
                _IDPlantilla.Value = string.Empty;
                _rutaNodoSeleccionado.Value = string.Empty;
                
            }
            


        }

        private void BindDrops()
        {
            Utilerias.ConstruyeCatalogos<Ejercicio>(uow.EjercicioBusinessLogic.Get().ToList<Ejercicio>(), this.ddlEjercicio, "Id", "Año");
        }


       


        #endregion

       

    }
}
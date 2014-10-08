using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.POA
{
    public partial class EvaluacionPOA : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            if (!IsPostBack)
            {

                int idPOADetalle =Utilerias.StrToInt(Request.QueryString["p"].ToString()); //Se recupera el id del objeto POADETALLE                
                BindControlesObra(idPOADetalle);
                BindArbol(treePlantilla);
                BindArbol(treePOAPlantilla,false,false);

                if (treePOAPlantilla.Nodes.Count > 0) 
                {
                    BindGrid(Utilerias.StrToInt(treePOAPlantilla.Nodes[0].Value));
                    _IDPlantilla.Value = treePOAPlantilla.Nodes[0].Value;
                    treePOAPlantilla.Nodes[0].Selected = true;
                }

                _SoloChecks.Value = "true";
            }

            //Evento que se ejecuta en JAVASCRIPT para evitar que se 'RESCROLLEE' el arbol al seleccionar un NODO y no se pierda el nodo seleccionado
            ClientScript.RegisterStartupScript(this.GetType(), "script", "SetSelectedTreeNodeVisible('<%= TreeViewName.ClientID %>_SelectedNode')", true);
        }

        #region METODOS

        /// <summary>
        /// Metodo encargado de cargar los arboles de PLANTILLAS, aquel donde el usuario va a seleccionar una plantilla
        /// y el arbol donde van a aparecer todas aquellas plantillas seleccionadas unicamente
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// <param name="tree">Nombre del arbol a cargar</param>
        /// <param name="catalogoCompleto">Indica si se va a cargar todo el catalogo completo de plantilla o no, true=todo el catalogo, false=las que eligio el usuario</param>
        /// <param name="habilitarSubNodos">Indica si se tienen que habilitar los subnodos de la plantilla, true=se habilitan, false=no se habilitan</param>
        private void BindArbol(TreeView tree, bool catalogoCompleto=true, bool habilitarSubNodos=true)
        {
            if (tree.Nodes.Count > 0)
            {
                tree.Nodes.Clear();
            }
            List<Plantilla> list;

            if (catalogoCompleto)
                list = uow.PlantillaBusinessLogic.Get(e => e.DependeDeId == null).ToList(); //Se obtiene todo el catalogo
            else
                list = GetPlantillasPadre(Utilerias.StrToInt(_IDPOADetalle.Value)); //Se buscan solo las plantillas que eligio el usuario

            foreach (Plantilla obj in list)
            {
                //Se crea el nodo padre
                TreeNode nodeNew = new TreeNode();
                nodeNew.Text = obj.Descripcion;
                nodeNew.Value = obj.Id.ToString();
                

                tree.Nodes.Add(nodeNew); //Se agrega el nodo al arbol

                if (obj.Detalles.Count > 0) //Si se tienen plantillas hijos, se anidan mas nodos al nodo padre
                    ColocarPlantillasHijos(nodeNew, obj.Detalles.ToList(), habilitarSubNodos);
            }

        }
        private void ColocarPlantillasHijos(TreeNode nodeParent, List<Plantilla> list,bool habilitarSubNodos)
        {
            foreach (Plantilla obj in list)
            {
                //Se crea el nodo hijo
                TreeNode nodeChild = new TreeNode();
                nodeChild.Text = obj.Descripcion;
                nodeChild.Value = obj.Id.ToString();
                nodeChild.Collapse();

                if (habilitarSubNodos)
                    nodeChild.SelectAction = TreeNodeSelectAction.None;
                
                //Se agrega al nodo padre 
                nodeParent.ChildNodes.Add(nodeChild);

                if (obj.Detalles.Count > 0) //Si se tienen plantillas hijos, se anidan mas nodos al nodo padre
                    ColocarPlantillasHijos(nodeChild, obj.Detalles.ToList(), habilitarSubNodos); //Se manda a llamar la misma fucnion
            }
        }
        public void BindControlesObra(int idPOADetalle)
        {
            POADetalle obj = uow.POADetalleBusinessLogic.GetByID(idPOADetalle);
            if (obj != null)
            {
                txtDescripcion.Value = obj.Descripcion;
                txtMunicipio.Value = obj.Municipio.Nombre;
                txtNumero.Value = obj.Numero;
                txtObservacion.Value = obj.Observaciones;
                _IDPOADetalle.Value = idPOADetalle.ToString();
            }
            else
            {
                txtDescripcion.Value = string.Empty;
                txtMunicipio.Value = string.Empty;
                txtNumero.Value = string.Empty;
                txtObservacion.Value = string.Empty;
            }

        }
        public string ImportarPlantilla(int IDPlantilla)
        {
            string M = string.Empty;
            int IDPoaDetalle = Utilerias.StrToInt(_IDPOADetalle.Value);
            //Se obtiene el OBJ de la PLANTILLA PADRE QUE SE ELIGIO
            Plantilla objPlantilla = uow.PlantillaBusinessLogic.GetByID(IDPlantilla);

            #region CREAR EN PRIMERA INSTANCIA POAPLANTILLA CON DATOS DE LA PLANTILLA PADRE QUE SE ELIGIO
            //Se crea el OBJ de POAPlantilla, con los datos del la plantilla PADRE que se obtuvo
            POAPlantilla obj = new POAPlantilla();
            obj.PlantillaId = IDPlantilla;
            obj.POADetalleId = IDPoaDetalle;
            //Se agregan mas datos, los de bitacora....pendiente


            //SE ALMACENA EL POAPLANTILLA
            uow.POAPlantillaBusinessLogic.Insert(obj);
            uow.SaveChanges();

            //Si hubo errores
            if (uow.Errors.Count > 0)
            {
                M = string.Empty;
                foreach (string cad in uow.Errors)
                    M += cad;

                return M;
            }

            //Se crea detalle de preguntas en POAPLANTILLADETALLE 
            M = ImportarDetallePlantilla(objPlantilla.DetallePreguntas.ToList(), obj.Id);

            //Si hubo errores
            if (!M.Equals(string.Empty))
                return M;

            #endregion

            #region CREAR POAPLANTILLASDETALLE TANTAS COMO SUBPLANTILLAS EXISTAN

            //Se Crean tantos POAPlantilla como subplantillas tenga el  OBJ de la PLANTILLA PADRE QUE SE ELIGIO
            if (objPlantilla.Detalles.Count > 0)
            {
                foreach (Plantilla p in objPlantilla.Detalles)
                {
                    obj = null;
                    //Se crea el OBJ de POAPlantilla, con los datos del la plantilla PADRE que se obtuvo
                    obj = new POAPlantilla();
                    obj.PlantillaId = p.Id;
                    obj.POADetalleId = IDPoaDetalle;
                    //Se agregan mas datos, los de bitacora....pendiente


                    //SE ALMACENA EL POAPLANTILLA
                    uow.POAPlantillaBusinessLogic.Insert(obj);
                    uow.SaveChanges();

                    //Si hubo errores
                    if (uow.Errors.Count > 0)
                    {
                        M = string.Empty;
                        foreach (string cad in uow.Errors)
                            M += cad;

                        return M;
                    }

                    //Se crea detalle de preguntas en POAPLANTILLADETALLE 
                    M = ImportarDetallePlantilla(p.DetallePreguntas.ToList(), obj.Id);

                    //Si hubo errores
                    if (!M.Equals(string.Empty))
                        return M;
                }

            }


            #endregion
            
            return M;
        }
        public string ImportarDetallePlantilla(List<PlantillaDetalle> DetallePreguntas, int IDPOAPlantilla)
        {
           
            string M = string.Empty;

            //Se crean OBJs de POAPlantillaDetalle, que es la que almacenara todas las preguntas de la plantilladetalle
            //Se recorre el detalle de preguntas que trae la plantilladetalle
            foreach (PlantillaDetalle pregunta in DetallePreguntas)
            {
                POAPlantillaDetalle objPOAPlantillaDetalle = new POAPlantillaDetalle();
                objPOAPlantillaDetalle.POAPlantillaId = IDPOAPlantilla; //ID del obj POAPlantilla
                objPOAPlantillaDetalle.PlantillaDetalleId = pregunta.Id; //Id del obj Plantilla detalle
                //Se agregan mas datos, los de bitacora....pendiente

                uow.POAPlantillaDetalleBusinessLogic.Insert(objPOAPlantillaDetalle);
                uow.SaveChanges();

                //Si hubo errores
                if (uow.Errors.Count > 0)
                {
                    M = string.Empty;
                    foreach (string cad in uow.Errors)
                        M += cad;
                   
                    return M;
                }

            }

            return M;
        }
        public List<Plantilla> GetPlantillasPadre(int idPOADetalle) 
        {
            List<Plantilla> list = null;

            list=(from p in uow.PlantillaBusinessLogic.Get(e=>e.DependeDeId==null)
                 join pp in uow.POAPlantillaBusinessLogic.Get(e=>e.POADetalleId==idPOADetalle) on p.Id equals pp.PlantillaId
                 select p).ToList();

            return list;

        }
        private void BindGrid(int idPlantilla)
        {
            int IDPOADetalle = Utilerias.StrToInt(_IDPOADetalle.Value);

            POAPlantilla obj = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == IDPOADetalle && e.PlantillaId == idPlantilla).FirstOrDefault();

            if (obj!=null)
            {
                grid.DataSource = obj.Detalles.ToList();
                grid.DataBind();

                if (obj.Detalles.Count > 0)
                {
                    txtPregunta.Value = obj.Detalles.First().PlantillaDetalle.Pregunta;
                    txtObservacionesPregunta.Value = obj.Detalles.First().Observaciones;
                    txtRutaArchivo.Value = obj.Detalles.First().RutaArchivo != null && obj.Detalles.First().RutaArchivo!=string.Empty?Path.GetFileName(obj.Detalles.First().RutaArchivo):string.Empty;
                }
            }
            

        }


        [WebMethod]
        public static List<string> GuardarPlantillas(string cadValores)
        {

            string M = string.Empty;
            List<string> R = new List<string>();
            string[] primerArray = cadValores.Split('|');

            foreach (string pa in primerArray)
            {
                string[] segundoArrary = pa.Split('=');

                M = GuardarPlantillaChecks(segundoArrary[0], segundoArrary[1]);

                if (!M.Equals(string.Empty))
                {
                    R.Add(string.Empty);
                    R.Add(M);
                    return R;
                }
                    
            }


            M = "Se han guardado correctamente los cambios";
            R.Add(M);

            return R;
        }

        public static string GuardarPlantillaChecks(string idPregunta, string respuesta)
        {
            string M = string.Empty;
            UnitOfWork uow = new UnitOfWork();

            int id = Utilerias.StrToInt(idPregunta);
            POAPlantillaDetalle pregu = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

            if (pregu != null)
            {
                switch (respuesta)
                {
                    case "1":
                        pregu.Respuesta = enumRespuesta.Si;
                        break;
                    case "2":
                        pregu.Respuesta = enumRespuesta.No;
                        break;
                    case "3":
                        pregu.Respuesta = enumRespuesta.NoAplica;
                        break;
                }

                uow.POAPlantillaDetalleBusinessLogic.Update(pregu);
                uow.SaveChanges();

                //Si hubo errores
                if (uow.Errors.Count > 0)
                {
                    M = string.Empty;
                    foreach (string cad in uow.Errors)
                        M += cad;

                    
                    return M;
                }

            }

           
            return M;

        }

         [WebMethod]
        public static List<string> GuardarDatosPlantilla(string idPregunta, string observacion, string nombreArchivo)
        {
            string M = string.Empty;
            List<string> R = new List<string>();

            UnitOfWork uow = new UnitOfWork();

            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(Utilerias.StrToInt(idPregunta));

            if (obj != null)
            {
                obj.Observaciones = observacion;

                string fullPath = Path.GetFullPath(nombreArchivo);

                FileInfo fileInfo = new FileInfo(nombreArchivo);

                List<string> R2 = null;// GuardarArchivo(fullPath, nombreArchivo, idPregunta);

                if (!R2[0].Equals(string.Empty))
                {
                    R.Add(R2[0]);
                    return R;
                }

                obj.RutaArchivo = nombreArchivo;

                uow.POAPlantillaDetalleBusinessLogic.Update(obj);
                uow.SaveChanges();

                //Si hubo errores
                if (uow.Errors.Count > 0)
                {
                    M = string.Empty;
                    foreach (string cad in uow.Errors)
                        M += cad;

                    R.Add(M);
                    return R;
                }

            }

            R.Add(M);
            return R;
        }

        public List<string> GuardarArchivo(HttpPostedFile postedFile,int idPregunta)
        {

            string M = string.Empty;
            string ruta = string.Empty;
            List<string> R = new List<string>();
            try
            {

                ruta = System.Configuration.ConfigurationManager.AppSettings["ArchivosPlantilla"];

                if (!ruta.EndsWith("/"))
                    ruta += "/";

                ruta += idPregunta.ToString() + "/";

                if (ruta.StartsWith("~") || ruta.StartsWith("/"))   //Es una ruta relativa al sitio
                    ruta = Server.MapPath(ruta);


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);

                ruta += postedFile.FileName;
                
                postedFile.SaveAs(ruta);

                //R.Add(M);
                //R.Add(ruta);
            }
            catch (Exception ex)
            {
                M = ex.Message;
            }

            R.Add(M);
            R.Add(ruta);

            return R;
           
        }


        [WebMethod]
        public static List<string> GetValoresPregunta(string idPregunta)
        {
            List<string> R = new List<string>();

            UnitOfWork uow = new UnitOfWork();

            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(Utilerias.StrToInt(idPregunta));

            if (obj != null)
            {
                R.Add(obj.Observaciones);
                R.Add(obj.RutaArchivo!=null && obj.RutaArchivo!=string.Empty?Path.GetFileName(obj.RutaArchivo):string.Empty);
                R.Add(obj.PlantillaDetalle.Pregunta);
            }

            return R;

        }

       

        #endregion
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //string M = "Se ha importado correctamente la plantilla";
            string M=string.Empty;

            int IDPlantilla=Utilerias.StrToInt(_IDPlantillaSeleccionada.Value); //ID de la plantilla padre
            
            M=ImportarPlantilla(IDPlantilla); //SE CREAN LOS POAPLANTILLAS

            //Si hubo errores
            if(!M.Equals(string.Empty))
            {
                lblMsgImportarPlantilla.Text = M;
                lblMsgImportarPlantilla.ForeColor = System.Drawing.Color.Red;
                divMsgImportarPlantilla.Style.Add("display", "block");
                return;
            }
            //Se recarga el arbol de las plantillas recien importadas
            BindArbol(treePOAPlantilla,false);

            M = "La plantilla se ha importado correctamente";
            lblMsgImportarPlantilla.Text = M;
            lblMsgImportarPlantilla.ForeColor = System.Drawing.Color.Black;
            divMsgImportarPlantilla.Style.Add("display", "block");


            divGuardarPlantilla.Style.Add("display", "none");
            divPlantillaImportar.Style.Add("display", "none");
            
        }

       
        protected void treePOAPlantilla_SelectedNodeChanged(object sender, EventArgs e)
        {
            int plantillaSeleccionada = Utilerias.StrToInt(treePOAPlantilla.SelectedNode.Value);
            BindGrid(plantillaSeleccionada);
            _IDPlantilla.Value = plantillaSeleccionada.ToString();
            divEvaluacion.Style.Add("display", "block");
            divDatos.Style.Add("display", "none");
        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                //GridViewRow gvrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                //TableCell cell0 = new TableCell();
                //cell0.Text = string.Empty;
                //cell0.HorizontalAlign = HorizontalAlign.Center;
                //cell0.ColumnSpan = 2;

                //TableCell cell1 = new TableCell();
                //cell1.Text = "RESPUESTA";
                //cell1.ForeColor = System.Drawing.Color.Black;
                
                //cell1.HorizontalAlign = HorizontalAlign.Center;
                //cell1.ColumnSpan = 3;

                //gvrow.Cells.Add(cell0);
                //gvrow.Cells.Add(cell1);

                //grid.Controls[0].Controls.AddAt(0, gvrow);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (grid.DataKeys[e.Row.RowIndex].Values["Id"] != null)
                {
                    
 
                    int id=Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

                    HtmlInputRadioButton chkSI = (HtmlInputRadioButton)e.Row.FindControl("chkSI");
                    HtmlInputRadioButton chkNO = (HtmlInputRadioButton)e.Row.FindControl("chkNO");
                    HtmlInputRadioButton chkNOAplica = (HtmlInputRadioButton)e.Row.FindControl("chkNOAplica");

                    chkSI.Attributes["onchange"] = "fnc_DesmarcarChecks(this," + e.Row.RowIndex + ",'" + grid.ID + "','" + chkNO.ID + "','" + chkNOAplica.ID + "')";
                    chkNO.Attributes["onchange"] = "fnc_DesmarcarChecks(this," + e.Row.RowIndex + ",'" + grid.ID + "','" + chkSI.ID + "','" + chkNOAplica.ID + "')";
                    chkNOAplica.Attributes["onchange"] = "fnc_DesmarcarChecks(this," + e.Row.RowIndex + ",'" + grid.ID + "','" + chkSI.ID + "','" + chkNO.ID + "')";
                    e.Row.Cells[1].Attributes.Add("onclick", "fnc_ClickRow(" + e.Row.RowIndex + ")");
                    
                    if (obj.Respuesta != 0)
                    {
                        switch (obj.Respuesta)
                        {
                            case enumRespuesta.Si:
                                chkSI.Value = "true";
                                chkSI.Checked = true;
                                break;
                            case enumRespuesta.No:
                                chkNO.Value = "true";
                                chkNO.Checked = true;
                                break;
                            case enumRespuesta.NoAplica:
                                chkNOAplica.Value = "true";
                                chkNOAplica.Checked = true;
                                break;
                        }
                    } 
                }
            }


        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            txtObservacionesPregunta.Disabled = false;
            btnBuscarArchivo.Enabled = true;

            GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;

            int id = Convert.ToInt32(grid.DataKeys[row.RowIndex].Values["Id"].ToString());
            _IDPregunta.Value = id.ToString();

            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

            txtPregunta.Value = obj.PlantillaDetalle.Pregunta;
            txtObservacionesPregunta.Value = obj.Observaciones;
            txtRutaArchivo.Value = obj.RutaArchivo != null && obj.RutaArchivo != string.Empty ? Path.GetFileName(obj.RutaArchivo) : string.Empty;

            divEvaluacion.Style.Add("display", "block");
            divDatos.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            _SoloChecks.Value = "false";

            //divBtnGuardarPreguntas.Style.Add("display", "none");
            divBtnGuardarDatosPreguntas.Style.Add("display", "block");

            btnCancelar.Disabled = false;
        }

        protected void btnGuardarDatosPreguntas_ServerClick(object sender, EventArgs e)
        {
            string M = string.Empty;
            List<string> R = new List<string>();
            
            if (!Convert.ToBoolean(_SoloChecks.Value))
            {
                //Siempre se van a actualizar datos de la pregunta (Observaciones y el archivo de soporte)
                int idPregunta = Utilerias.StrToInt(_IDPregunta.Value);
                _SoloChecks.Value = "true";
                POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(idPregunta);


                //Se tiene que almacenar el archivo adjunto, si es que se cargo uno
                if (!btnBuscarArchivo.PostedFile.FileName.Equals(string.Empty))
                {
                    R = GuardarArchivo(btnBuscarArchivo.PostedFile, idPregunta);

                    //Si hubo errores
                    if (!R[0].Equals(string.Empty))
                    {
                        lblMensajes.Text = R[0];
                        lblMensajes.ForeColor = System.Drawing.Color.Red;
                        divMsg.Style.Add("display", "block");
                        return;
                    }
                }

                //Se guarda el objeto con la informacion del archivo y sus datos correspondientes
                obj.Observaciones = txtObservacionesPregunta.Value;
                obj.RutaArchivo = R.Count > 0 ? R[1] : obj.RutaArchivo;

                uow.POAPlantillaDetalleBusinessLogic.Update(obj);
                uow.SaveChanges();

                //Si hubo errores
                if (uow.Errors.Count > 0)
                {
                    M = string.Empty;
                    foreach (string cad in uow.Errors)
                        M += cad;

                    lblMensajes.Text = M;
                    lblMensajes.ForeColor = System.Drawing.Color.Red;
                    divMsg.Style.Add("display", "block");
                    return;

                }


            }

            //Se tienen que almacenar los checks de cada una de las preguntas que se pudieran
            //haber marcado

            R = null;

            R = GuardarPlantillas(_CadValoresChecks.Value);

            //Si hubo errores
            if (R.Count > 1)
            {
                lblMensajes.Text = R[1];
                lblMensajes.ForeColor = System.Drawing.Color.Red;
                divMsg.Style.Add("display", "block");
                
                return;
            }

            BindGrid(Utilerias.StrToInt(_IDPlantilla.Value));

            btnCancelar.Disabled = true;
            btnBuscarArchivo.Enabled = false;
            txtObservacionesPregunta.Disabled = true;


            lblMensajes.Text = R[0];
            lblMensajes.ForeColor = System.Drawing.Color.Black;
            divMsg.Style.Add("display", "block");
            divEvaluacion.Style.Add("display", "block");
            divDatos.Style.Add("display", "none");
            
            
        }

        



    }


}
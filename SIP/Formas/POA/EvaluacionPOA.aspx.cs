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
                string M = string.Empty;

                int idObra =Utilerias.StrToInt(Request.QueryString["ob"].ToString()); //Se recupera el id del objeto OBRA            

                int ordenPlantilla = Utilerias.StrToInt(Request.QueryString["o"].ToString()); //Se recupera el orden de la plantilla que sera evaluada          

                BindControlesObra(idObra); //Se bindean los datos del poadetalle, se pasa como argumento el ID, recuperado de la sesion

                Plantilla plantilla = uow.PlantillaBusinessLogic.Get(p => p.Orden == ordenPlantilla).FirstOrDefault();

                POAPlantilla poaPlantilla = uow.POAPlantillaBusinessLogic.Get(pp => pp.PlantillaId == plantilla.Id && pp.POADetalleId == Utilerias.StrToInt(_IDPOADetalle.Value)).FirstOrDefault(); //Se recupera POAPlantilla

                if (poaPlantilla == null) //Si no existe ningun objeto con la plantilla creada, entonces se procede a clonar la plantilla
                    M = CopiarPlantilla(plantilla.Id);
                else
                    M= CrearPreguntasInexistentes(poaPlantilla.Id); //Se obtienen todas aquellas preguntas que se pudieron haber creado en las plantillas despues de HABER creado la clonada de plantillas, 


                //Si hubo errores
                if (!M.Equals(string.Empty))
                {
                    lblMsgImportarPlantilla.Text = M;
                    lblMsgImportarPlantilla.ForeColor = System.Drawing.Color.Red;
                    divMsgImportarPlantilla.Style.Add("display", "block");
                    return;
                }

                //Se carga la informacion de la plantilla clonada

                BindArbol(treePOAPlantilla); //Se bindea el arbol de las plantillas que se seleccionaron, las que se van a usar

                if (treePOAPlantilla.Nodes.Count > 0) //Si existen plantillas cargadas, se bindea el grid de preguntas para la primera plantilla
                {
                    BindGrid(Utilerias.StrToInt(treePOAPlantilla.Nodes[0].Value));

                    _IDPlantilla.Value = treePOAPlantilla.Nodes[0].Value;
                    treePOAPlantilla.Nodes[0].Selected = true;
                }

                _SoloChecks.Value = "true"; //Hidden que indica cuando solo se van a guardar las respuetas de las preguntas (NO, SI, NO APLICA)
            }

            //Evento que se ejecuta en JAVASCRIPT para evitar que se 'RESCROLLEE' el arbol al seleccionar un NODO y no se pierda el nodo seleccionado
            ClientScript.RegisterStartupScript(this.GetType(), "script", "SetSelectedTreeNodeVisible('<%= TreeViewName.ClientID %>_SelectedNode')", true);
        }

        #region METODOS

        /// <summary>
        /// Metodo encargado de obtner las nuevas preguntas de una plantilla que se hayan creado
        /// posterior o despues de haber clonado la plantilla
        /// Se obtienen y se agregan a POAPLANTILLADETALLE
        /// Creado por Rigoberto TS
        /// 20/10/2014
        /// </summary>
        /// <param name="POAPlantillaID"></param>
        /// <returns></returns>
        private string CrearPreguntasInexistentes(int POAPlantillaID)
        {
            int ordenPlantilla = Utilerias.StrToInt(Request.QueryString["o"].ToString());
            string M=string.Empty;

            //Consulta que hace la diferencia de preguntas entre POAPLANTILLADETALLE y PLANTILLADETALLE
            var list=(from p in uow.PlantillaDetalleBusinessLogic.Get()
                      join pp in uow.PlantillaBusinessLogic.Get(e=>e.Orden==ordenPlantilla).ToList()
                      on p.PlantillaId equals pp.Id
                      join po in uow.POAPlantillaDetalleBusinessLogic.Get()
                      on p.Id equals po.PlantillaDetalleId into temp
                      from pi in temp.DefaultIfEmpty()
                      select new {p.Id,pp.Orden, pregunta=(pi==null ? 0 : pi.PlantillaDetalleId) });


            foreach (var obj in list)
            {
                if (obj.pregunta == 0)
                {
                    POAPlantillaDetalle objPOAPlantillaDetalle = new POAPlantillaDetalle();
                    objPOAPlantillaDetalle.POAPlantillaId = POAPlantillaID; //ID del obj POAPlantilla
                    objPOAPlantillaDetalle.PlantillaDetalleId = obj.Id; //Id del obj Plantilla detalle
                    objPOAPlantillaDetalle.CreatedById = Utilerias.StrToInt(Session["IdUser"].ToString());

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
            }

            return M;

        }

        private string CopiarPlantilla(int idPlantilla)
        {
            
            string M = string.Empty;

            M = ImportarPlantilla(idPlantilla); //SE CREAN LOS POAPLANTILLAS

            if (!M.Equals(string.Empty))
                return M;

            //Se recarga el arbol de las plantillas recien importadas
            BindArbol(treePOAPlantilla);

            return M;
        }

        /// <summary>
        /// Metodo encargado de cargar los arboles de PLANTILLAS, aquel donde el usuario va a seleccionar una plantilla
        /// y el arbol donde van a aparecer todas aquellas plantillas seleccionadas unicamente
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// <param name="tree">Nombre del arbol a cargar</param>
        
        private void BindArbol(TreeView tree)
        {
            if (tree.Nodes.Count > 0)
            {
                tree.Nodes.Clear();
            }
            List<Plantilla> list;


            list = GetPlantillasPadre(Utilerias.StrToInt(_IDPOADetalle.Value)); //Se buscan solo las plantillas que eligio el usuario

            foreach (Plantilla obj in list)
            {
                //Se crea el nodo padre
                TreeNode nodeNew = new TreeNode();
                nodeNew.Text = obj.Descripcion;
                nodeNew.Value = obj.Id.ToString();
                

                tree.Nodes.Add(nodeNew); //Se agrega el nodo al arbol

                if (obj.Detalles.Count > 0) //Si se tienen plantillas hijos, se anidan mas nodos al nodo padre
                    ColocarPlantillasHijos(nodeNew, obj.Detalles.ToList());
            }

        }

        /// <summary>
        /// Metodo encargado de crear los nodos hijos.cuando se tienen detalles de plantillas
        /// Credo por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// </summary>
        /// <param name="nodeParent">Nodo Padre</param>
        /// <param name="list">Lista de elmentos</param>
        private void ColocarPlantillasHijos(TreeNode nodeParent, List<Plantilla> list)
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
                    ColocarPlantillasHijos(nodeChild, obj.Detalles.ToList()); //Se manda a llamar la misma fucnion
            }
        }
        
        /// <summary>
        /// Metodo encargado de bindear los datos de un POADetalle
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// <param name="idPOADetalle"></param>
        public void BindControlesObra(int id)
        {
            Obra obra = uow.ObraBusinessLogic.GetByID(id);

            if (obra == null) //Quiere decir que se esta abriendo desde ANTEPROYECTO DE POA
            {
                POADetalle obj = uow.POADetalleBusinessLogic.GetByID(id); //Se recupera el objeto

                if (obj != null) //Se bindean los datos directamente de POADETALLE
                {
                    txtDescripcion.Value = obj.Descripcion;
                    txtMunicipio.Value = obj.Municipio.Nombre;
                    txtNumero.Value = obj.Numero;
                    txtObservacion.Value = obj.Observaciones;
                    _IDPOADetalle.Value = id.ToString();
                }
                else
                {
                    txtDescripcion.Value = string.Empty;
                    txtMunicipio.Value = string.Empty;
                    txtNumero.Value = string.Empty;
                    txtObservacion.Value = string.Empty;
                }
            }
            else
            {
                //Se bindean con los datos de la OBRA, es decir, viene de POA AJUSTADO
                txtDescripcion.Value = obra.Descripcion;
                txtMunicipio.Value = obra.Municipio.Nombre;
                txtNumero.Value = obra.Numero;
                txtObservacion.Value = obra.Observaciones;

                _IDPOADetalle.Value = obra.POADetalleId.ToString(); //Se coloca el id de POADETALLE, por que sobre ese obj es donde se realizara la EVALUACION DE PLANTILLAS
                
            }
                


            
            

        }

        /// <summary>
        /// Metodo encargado de Importar la plantilla del catalogo de plantillas que haya elegido el usuario en el show modal
        /// Se crean objetos en POAPlantilla
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// </summary>
        /// <param name="IDPlantilla">ID de la plantilla que eligio el usuario</param>
        /// <returns>Regresa una cadena. Vacia si no hay errores</returns>
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
            obj.CreatedById = Utilerias.StrToInt(Session["IdUser"].ToString());
            

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
                    obj.CreatedById = Utilerias.StrToInt(Session["IdUser"].ToString());

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

        /// <summary>
        /// Metodo encargado de crear la lista de preguntas que contiene la Plantilla que haya elegido el usuario en el show modal
        /// Se crean objetos en POAPLANTILLADETALLE
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// </summary>
        /// <param name="DetallePreguntas">Lista de Preguntas (objeto PlantillaDetalle)</param>
        /// <param name="IDPOAPlantilla">ID del objeto POAPlantilla que se creo con la plantilla que eligio el usuario</param>
        /// <returns>Regresa una cadena. vacia si no hay errores</returns>
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
                objPOAPlantillaDetalle.CreatedById = Utilerias.StrToInt(Session["IdUser"].ToString());
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

        /// <summary>
        /// Metodo encargdo de devolver todas las plantillas padre de la tabla Plantilla
        /// Se hace join con la tabla de POAPlantilla, que es donde se almacena la plantila que eligio el usuario
        /// Utilizado para cargar el arbol de plantillas elegidas por el usuario
        /// </summary>
        /// <param name="idPOADetalle">ID del objeto POADetalle</param>
        /// <returns></returns>
        public List<Plantilla> GetPlantillasPadre(int idPOADetalle) 
        {
            List<Plantilla> list = null;
            int ordenPlantilla = Utilerias.StrToInt(Request.Params["o"].ToString());

            list=(from p in uow.PlantillaBusinessLogic.Get(e=>e.DependeDeId==null && e.Orden==ordenPlantilla)
                 join pp in uow.POAPlantillaBusinessLogic.Get(e=>e.POADetalleId==idPOADetalle) on p.Id equals pp.PlantillaId
                 select p).ToList();

            return list;

        }
        
        /// <summary>
        /// metodo encargado de bindear el grid de preguntas que marcara el usuario como NO, SI, NO APLICA
        /// Creado por Rigberto TS
        /// 29/09/2014
        /// </summary>
        /// </summary>
        /// <param name="idPlantilla">ID de la plantilla que haya seleccionado el usuario</param>
        private void BindGrid(int idPlantilla)
        {
            int IDPOADetalle = Utilerias.StrToInt(_IDPOADetalle.Value);

            POAPlantilla obj = uow.POAPlantillaBusinessLogic.Get(e => e.POADetalleId == IDPOADetalle && e.PlantillaId == idPlantilla).FirstOrDefault();

            if (obj!=null)
            {
                
                grid.PageIndex = Utilerias.StrToInt(_PageIndex.Value);
                grid.DataSource = obj.Detalles.OrderBy(e=>e.PlantillaDetalle.Orden).ToList();
                grid.DataBind();

            }
            

        }

        /// <summary>
        /// WEB METHOD encargado de guardar los datos de la Plantilla SI, NO , NO APLICA de la lista de preguntas que conforman la plantilla
        /// Creado por Rigobero TS
        /// 05/10/2014
        /// </summary>
        /// <param name="cadValores">cadena que se recibe desde el cliente(JAVASCRIPT) con el formato de ID=VALOR separado por |</param>
        /// <returns>Regresa una lista de cadenas hacia JAVASCRIPT</returns>
        [WebMethod]
        public static List<string> GuardarPlantillas(string cadValores,UnitOfWork _uow,int idUser)
        {
            //cadValores viene con el formato de ID=Valor|ID=Valor|ID=Valor|ID=Valor...........

            string M = string.Empty;
            List<string> R = new List<string>();
            string[] primerArray = cadValores.Split('|'); //Se separa la cadena en un arreglo quedando solamente el formato ID=Valor

            foreach (string pa in primerArray) //Se recorre el primer arreglo
            {
                string[] segundoArrary = pa.Split('='); //Se separa el primer resultado, ahora por el caracter de = 

                M = GuardarPlantillaChecks(segundoArrary[0], segundoArrary[1], _uow, idUser); //Se guarda el objeto con los valores recien sacados de la separacion

                //Si hubo errores
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

        /// <summary>
        /// Metodo encagado ya de modificar el objeto de POAPlantillaDetalle, colocando ya en la RESPUESTA, un valor segun sea lo que se haya marcado SI, NO, NO APLICA
        /// Creado por Rigoberto TS
        /// 05/10/2014
        /// </summary>
        /// <param name="idPregunta">ID de la Pregunta (POAPlantillaDetalle)</param>
        /// <param name="respuesta">RESPUESTA que marco el usuario (1=SI, 2=NO, 3=NO APLICA)</param>
        /// <returns></returns>
        public static string GuardarPlantillaChecks(string idPregunta, string respuesta,UnitOfWork _uow, int idUser)
        {
            string M = string.Empty;
            UnitOfWork uow = _uow;

            int id = Utilerias.StrToInt(idPregunta);
            POAPlantillaDetalle pregu = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

            if (pregu != null)
            {
                switch (respuesta) //Segun la respuesta, se asigna el valor
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
                pregu.EditedById = idUser;
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

        /// <summary>
        /// Metodo encargado de almacenar un archivo de soporte en el directorio de ARCHIVOSADJUNTOS para la pregunta de alguna plantilla
        /// Creado por Rigoberto TS
        /// 05/10/2014
        /// </summary>
        /// <param name="postedFile">Archivo que selecciono el usuario</param>
        /// <param name="idPregunta">ID de la pregunta a la que se le va a asociar el archivo</param>
        /// <returns></returns>
        public List<string> GuardarArchivo(HttpPostedFile postedFile,int idPregunta)
        {

            string M = string.Empty;
            string ruta = string.Empty;
            List<string> R = new List<string>();
            try
            {

                ruta = System.Configuration.ConfigurationManager.AppSettings["ArchivosPlantilla"]; //Se recupera nombre de la carpeta del archivo WEB.CONFIG

                if (!ruta.EndsWith("/"))
                    ruta += "/";

                ruta += idPregunta.ToString() + "/"; //Se asigna el ID de la Pregunta

                if (ruta.StartsWith("~") || ruta.StartsWith("/"))   //Es una ruta relativa al sitio
                    ruta = Server.MapPath(ruta);


                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta); //Se crea la carpeta

                ruta += postedFile.FileName;

                postedFile.SaveAs(ruta); //Se guarda el archivo

            }
            catch (Exception ex)
            {
                M = ex.Message;
            }

            R.Add(M);
            R.Add(ruta);

            return R;
           
        }

        /// <summary>
        /// WEB METHOD encargado de recuperar informacion de la pregunta cuando se da clic sobre las filas del grid
        /// y colocarla en los controles correspondientes en el CLIENTE
        /// </summary>
        /// <param name="idPregunta">ID de la pregunta que se selecciono del grid</param>
        /// <returns>Retorna una lista con los valores de la pregunta hacia el CLIENTE (JAVASCRIPT)</returns>
        [WebMethod]
        public static List<string> GetValoresPregunta(string idPregunta)
        {
            List<string> R = new List<string>();

            UnitOfWork uow = new UnitOfWork();

            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(Utilerias.StrToInt(idPregunta));

            if (obj != null)
            {
                R.Add(obj.Observaciones != null && obj.Observaciones != string.Empty ? obj.Observaciones : string.Empty);
                R.Add(obj.RutaArchivo!=null && obj.RutaArchivo!=string.Empty?Path.GetFileName(obj.RutaArchivo):string.Empty);
                R.Add(obj.PlantillaDetalle.Pregunta);
            }

            return R;

        }


        #endregion

        #region EVENTOS

        /// <summary>
        /// Evento que se encarga de guardar las plantillas seleccionadas por el usuario
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //string M = "Se ha importado correctamente la plantilla";
            string M = string.Empty;

            //int IDPlantilla = Utilerias.StrToInt(_IDPlantillaSeleccionada.Value); //ID de la plantilla padre

            //M = ImportarPlantilla(IDPlantilla); //SE CREAN LOS POAPLANTILLAS

            //Si hubo errores
            if (!M.Equals(string.Empty))
            {
                lblMsgImportarPlantilla.Text = M;
                lblMsgImportarPlantilla.ForeColor = System.Drawing.Color.Red;
                divMsgImportarPlantilla.Style.Add("display", "block");
                return;
            }
            //Se recarga el arbol de las plantillas recien importadas
            BindArbol(treePOAPlantilla);

            M = "La plantilla se ha importado correctamente";
            lblMsgImportarPlantilla.Text = M;
            lblMsgImportarPlantilla.ForeColor = System.Drawing.Color.Black;
            divMsgImportarPlantilla.Style.Add("display", "block");


            //divGuardarPlantilla.Style.Add("display", "none");
            //divPlantillaImportar.Style.Add("display", "none");

        }

        /// <summary>
        /// Evento que se dispara al seleccionar un nodo del arbol de plantillas
        /// Se tiene que llenar el grid de preguntas con las que cuenta dicha plantilla seleccionada por el usuario
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void treePOAPlantilla_SelectedNodeChanged(object sender, EventArgs e)
        {
            int plantillaSeleccionada = Utilerias.StrToInt(treePOAPlantilla.SelectedNode.Value); //Se recupera el ID de la plantilla del nodo elegido
            BindGrid(plantillaSeleccionada); //Se carga el grid de preguntas
            _IDPlantilla.Value = plantillaSeleccionada.ToString();
            divEvaluacion.Style.Add("display", "block");
            divDatos.Style.Add("display", "none");
        }

        /// <summary>
        /// Evento del grid al momento de llenarse, coloca las funciones de JAVASCRIPT sobre los radio buttons
        /// Ademas de marcar o desmarcar segun la RESPUESTA que traiga el objeto POAPLANTILLADETALLE
        /// Creado por Rigoberto TS
        /// 29/09/2014
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


                    int id = Utilerias.StrToInt(grid.DataKeys[e.Row.RowIndex].Values["Id"].ToString());
                    POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

                    //Se recuperan los controles de RADIOBUTTONS
                    HtmlInputRadioButton chkSI = (HtmlInputRadioButton)e.Row.FindControl("chkSI");
                    HtmlInputRadioButton chkNO = (HtmlInputRadioButton)e.Row.FindControl("chkNO");
                    HtmlInputRadioButton chkNOAplica = (HtmlInputRadioButton)e.Row.FindControl("chkNOAplica");

                    //Se coloca la FUNCION de fnc_DesmarcarChecks en JAVASCRIPT
                    chkSI.Attributes["onchange"] = "fnc_DesmarcarChecks(this," + e.Row.RowIndex + ",'" + grid.ID + "','" + chkNO.ID + "','" + chkNOAplica.ID + "')"; 
                    chkNO.Attributes["onchange"] = "fnc_DesmarcarChecks(this," + e.Row.RowIndex + ",'" + grid.ID + "','" + chkSI.ID + "','" + chkNOAplica.ID + "')";
                    chkNOAplica.Attributes["onchange"] = "fnc_DesmarcarChecks(this," + e.Row.RowIndex + ",'" + grid.ID + "','" + chkSI.ID + "','" + chkNO.ID + "')";
                    e.Row.Cells[1].Attributes.Add("onclick", "fnc_ClickRow(" + e.Row.RowIndex + ")");
                    e.Row.Cells[0].Attributes.Add("onclick", "fnc_ClickRow(" + e.Row.RowIndex + ")");
                    
                    if (obj.Respuesta != 0)
                    {
                        switch (obj.Respuesta) //Segun sea la respuesta que traiga el objeto, se marca el radiobutton correspondiente
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

                    //Se coloca la fucnion a corespondiente para visualizar el DOCUMENTO ADJUNTO A LA PREGUNTA
                    HtmlButton btnVer = (HtmlButton)e.Row.FindControl("btnVer");
                    string ruta = ResolveClientUrl("~/AbrirDocto.aspx");
                    btnVer.Attributes["onclick"] = "fnc_AbrirArchivo('" + ruta + "'," + id + ")";
                }
            }


        }

        /// <summary>
        /// Evento que se dispara cuando se da clic en el boton de editar, se preparan los controles para que el usuario ueda modificar los datos
        /// Credo por Rigoberto TS
        /// 10/10/2014
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //txtRutaArchivo.Value = obj.RutaArchivo != null && obj.RutaArchivo != string.Empty ? Path.GetFileName(obj.RutaArchivo) : string.Empty;

            divEvaluacion.Style.Add("display", "block");
            divDatos.Style.Add("display", "none");
            divMsg.Style.Add("display", "none");
            divCapturaPreguntas.Style.Add("display", "block");

            _SoloChecks.Value = "false"; //Se indica que tambien se almacenaran datos de la PREGUNTA, no solo las respuestas SI, NO, NO APLICA

            divBtnGuardarDatosPreguntas.Style.Add("display", "block");

            btnCancelar.Disabled = false;
        }


        /// <summary>
        /// Evento encargado de almacenar los datos de la pregunta que se este editando, ademas de las respuestas que haya marcado el usuario
        /// Creado por Rigoberto TS
        /// 10/10/2014
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    //Validar el tamaño del archivo
                    if (btnBuscarArchivo.FileBytes.Length > 10485296)
                    {
                        lblMensajes.Text = "Se ha excedido en el tamaño del archivo, el máximo permitido es de 10 Mb";
                        lblMensajes.ForeColor = System.Drawing.Color.Red;
                        divMsg.Style.Add("display", "block");

                        return;
                    }

                    R = GuardarArchivo(btnBuscarArchivo.PostedFile, idPregunta); //Se guarda el archivo

                    //Si hubo errores
                    if (!R[0].Equals(string.Empty))
                    {
                        lblMensajes.Text = R[0];
                        lblMensajes.ForeColor = System.Drawing.Color.Red;
                        divMsg.Style.Add("display", "block");
                        return;
                    }

                    //Se guarda el objeto con la informacion del archivo y sus datos correspondientes
                    obj.NombreArchivo = Path.GetFileName(btnBuscarArchivo.FileName);
                    obj.TipoArchivo = btnBuscarArchivo.PostedFile.ContentType;
                }


                obj.Observaciones = txtObservacionesPregunta.Value;
                obj.EditedById =Utilerias.StrToInt(Session["IdUser"].ToString());

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

            R = GuardarPlantillas(_CadValoresChecks.Value, uow, Utilerias.StrToInt(Session["IdUser"].ToString())); //Se guarda las respuestas que se hayan marcado en el grid SI, NO, NO APLICA

            //Si hubo errores
            if (R.Count > 1)
            {
                lblMensajes.Text = R[1];
                lblMensajes.ForeColor = System.Drawing.Color.Red;
                divMsg.Style.Add("display", "block");

                return;
            }

            BindGrid(Utilerias.StrToInt(_IDPlantilla.Value)); //Se bindea nuevamente el grid de preguntas

            btnCancelar.Disabled = true;
            btnBuscarArchivo.Enabled = false;
            txtObservacionesPregunta.Disabled = true;


            lblMensajes.Text = R[0];
            lblMensajes.ForeColor = System.Drawing.Color.Black;
            divMsg.Style.Add("display", "block");
            divEvaluacion.Style.Add("display", "block");
            divDatos.Style.Add("display", "none");
            divCapturaPreguntas.Style.Add("display", "none");

        }
        
        /// <summary>
        /// Evento que se encarga de bindear el grid de preguntas cada vez que se vanza o rtrocede de pagina en el grid
        /// Creado por Rigoberto TS
        /// 16/10/2014
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;

            _PageIndex.Value = e.NewPageIndex.ToString();

            List<string> R = null;

            //Se tiene que guardar los checks que haya marcado el usuario
            R = GuardarPlantillas(_CadValoresChecks.Value, uow, Utilerias.StrToInt(Session["IdUser"].ToString()));

            if (R.Count > 1)
            {
                lblMensajes.Text = R[1];
                lblMensajes.ForeColor = System.Drawing.Color.Red;
                divMsg.Style.Add("display", "block");
                return;
            }
            else
                divMsg.Style.Add("display", "none");

            BindGrid(Utilerias.StrToInt(_IDPlantilla.Value)); //Se bindea el grid
            divEvaluacion.Style.Add("display", "block");
            divCapturaPreguntas.Style.Add("display", "none");
            divDatos.Style.Add("display", "none");
            
            
        }

        #endregion
        

    }


}
<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorCatalogos.Master" AutoEventWireup="true" CodeBehind="catAperturaProgramatica.aspx.cs" Inherits="SIP.Formas.Catalogos.catAperturaProgramatica" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var id = 0;
    $(document).ready(function () {
        try {
            //
            $("#<%= contenedor.ClientID %>").bind("contextmenu", function (e) {
                     e.preventDefault();
                     $("#custom-menu").css({ top: e.pageY + "px", left: e.pageX + "px" }).show(100);
                 });

                 $("#<%= contenedor.ClientID %>").mouseup(function (e) {
                     var container = $("#custom-menu");
                     if (container.has(e.target).length == 0) {
                         container.hide();
                     }
                 });
            
                 $(document).bind(function () {
                     $(document).bind("contextmenu", function (e) {
                         return false;
                     });
                 });

                 $(document).mouseup(function (e) {
                     var container = $("#custom-menu");
                     if (container.has(e.target).length == 0) {
                         container.hide();
                     }
                 });

                 //Se ejecuta el evento de las opciones del menu contextual
                 $('.evento').click(function () {
                     var control = $(this).attr("id");
                     $("#<%= treeApertura.ClientID %>").attr("disabled", true)
                    fnc_ClickMenu(control);
                });
            }
             catch (err) {
                 alert(err);
             }

         });

        //Funcion que evita que se "RESCROLEE" el arbol al seleccionar un NODO
        function SetSelectedTreeNodeVisible(controlID, boolHayPlantillas) {

            var elem = document.getElementById("<%=  treeApertura.ClientID%>" + "_SelectedNode");
             if (elem != null) {
                 var node = document.getElementById(elem.value);
                 if (node != null) {
                     node.scrollIntoView(true);
                     $("#<%= divArbol.ClientID %>").scrollLeft = 0;
                }
            }


            fnc_CargaInicial();

        }

        //funcion que permite habiltar o inhabilitar las opciones del menu contextual
        //de acuerdo al parametro recibido boolhabilitar
        //Creado por Rigoberto TS
        //22/09/2014
        function fnc_HabilitarInHabilitarOpciones(boolHabilitar) {

            var blockNone;

            if (boolHabilitar)
                blockNone = "block";
            else
                blockNone = "none";

             $("#<%= divguardar.ClientID%>").css("display", blockNone);
             $("#<%= btnCancelar.ClientID%>").css("display", blockNone);
             $("#<%= btnGuardar2.ClientID%>").css("display", blockNone);
             $("#<%= divmsg.ClientID%>").css("display", "none");



             $("#<%= addp.ClientID %>").attr("disabled", boolHabilitar);
             $("#<%= addsp.ClientID %>").attr("disabled", boolHabilitar);
             $("#<%= btnDel.ClientID %>").attr("disabled", boolHabilitar);
             $("#btnBorrar").attr("disabled", boolHabilitar);
             $("#btnPD").attr("disabled", boolHabilitar);
             $("#btnPD2").attr("disabled", boolHabilitar);
             $("#<%= edit.ClientID %>").attr("disabled", boolHabilitar)
             $("#<%= divArbol.ClientID %>").attr("disabled", boolHabilitar)

             $("#custom-menu").hide();

         }

         //funcion que permite habiltar los controles de la edicion de una plantilla
         //Creado por Rigoberto TS
         //22/09/2014
         function fnc_HabilitarControlesAperturaProgramatica() {
             $("#<%= txtNombre.ClientID%>").removeAttr("disabled");
             $("#<%= txtClave.ClientID%>").removeAttr("disabled");
             $("#<%= txtOrden.ClientID%>").removeAttr("disabled");
         }

         //funcion que permite habiltar limpiar los controles de edicion de una plantilla, si es nueva
         //Creado por Rigoberto TS
         //22/09/2014
         function fnc_LimpiarControles() {
             //Se limpian los controles
             $("#<%=  txtNombre.ClientID%>").val("");
             $("#<%= txtClave.ClientID%>").val("");
             $("#<%= txtOrden.ClientID%>").val("");
         }


         function fnc_ClickMenu(sender) {
             var limpiar = true;
             var procede = true;

             switch (sender) {

                 case "<%= addp.ClientID %>": //AGREGAR UNA PLANTILLA PADRE

                    $("#<%= _Padre.ClientID%>").val("S"); //Se indica que se va a agregar una plantilla padre, se coloca una  S en la caja de texto oculto _PAdre
                    $("#<%= _Accion.ClientID%>").val("N");  //Se indica que el estado de la forma es NUEVO, a traves del campo oculto Estado

                    break;

                case "<%= addsp.ClientID %>": //AGREGAR SUBP

                    var idPadre = parseInt($("#<%= _IdApertura.ClientID%>").val());

                    var nivel = parseInt($("#<%= _Nivel.ClientID%>").val());

                    if (nivel == 3) {
                        procede = false;
                    }

                    if (idPadre != 0) {
                        $("#<%= _Padre.ClientID%>").val("N"); //Se indica que se va a agregar una subplantilla, se coloca una N en la caja de texto oculto_PAdre
                        $("#<%= _Accion.ClientID%>").val("N"); //Se indica que el estado de la forma es NUEVO, a traves del campo oculto Estado
                    }
                    break;

                 case "<%= edit.ClientID %>": //EDITAR UN REGISTRO

                    limpiar = false;  //Se desactiva bandera para limpiar controles
                    $("#<%= _Accion.ClientID%>").val("A"); //Se indica que el estado de la forma es EDICION, a traves del campo oculto Estado

                    break;

            }

             if (procede) {
                 fnc_HabilitarControlesAperturaProgramatica();  //SE HABILITAN LOS CONTROLES PARA AGREGAR UN NUEVA PLNATILLA PADRE O UNA NUEVA SUPLANTILLA

                 if (limpiar) //SI SE TIENEN QUE LIMPIAR LOS CONTROLES
                     fnc_LimpiarControles();

                 fnc_HabilitarInHabilitarOpciones(true); //Se inhabilitan los botnes de agregar PLANTILLA, SUBPLANTILLA y ELIMINAR, y el propio arbol
             }

            

            $("#custom-menu").hide(); //Se oculta el menu contextual


        }


        //Funcion que permite ocultar el menu contextual cuando se da en la opcion de BORRAR
        //Ademas, cambia el mensaje del dialogo modal de confirmacion
        //Creada por rigoberto ts
        //22/09/2014
        function fnc_OcultarMC() {
            var container = $("#custom-menu");
            container.hide();

            $("#msgContenido").text("¿Está seguro que desea eliminar el registro?"); //Se cambia el mensaje del dialogo modal de confirmacion
        }

        //Funcion que permite validar si la descripcion de la plantilla va vacia
        //Ademas, cambia el mensaje del dialogo modal de confirmacion
        //Creada por rigoberto ts
        //22/09/2014
        function fnc_Validar() {

            var desc = $("#<%= txtNombre.ClientID%>").val();
            if (desc == null || desc.length == 0 || desc == undefined) {
                $("#custom-menu").hide(); //Se oculta el menu contextual 
                $("#msgContenido").text("El campo de nombre no puede ir vacío"); //Se cambia el mensaje del dialogo modal de confirmacion
                $("#myModal").modal('show') //Se muestra el modal
                return false;
            }
            return true;
        }


        //Funcion que permite ir hacia el detalle de preguntas de cada plantilla
        //Creada por rigoberto ts
        //22/09/2014
        //function fnc_IrPreguntasDetalle() {

        //    var url = "PlantillaPreguntas.aspx?p=" + $("#<%=_IdApertura.ClientID%>").val();
        //     $(location).attr('href', url);
        // }

         //Funcion que se ejecuta al cargar la pagina, verifica si existen plantillas en el arbol, sino hay niguna
         //se ocultan todas las opciones del menu contextual, excepto la de AGREGAR PLANTILLA
         //Creado por la cachorra Rigoberto TS
         //23/09/2014
         function fnc_CargaInicial() {

             nodes = document.getElementById("<%= txtNombre.ClientID%>").childNodes;

             if (nodes.length == 0) {
                 $("#<%= addsp.ClientID %>").attr("disabled", true);
                 $("#<%= edit.ClientID %>").attr("disabled", true);
                 $("#<%= btnDel.ClientID %>").attr("disabled", true);
                 $("#btnBorrar").attr("disabled", true);
                 $("#btnPD").attr("disabled", true);
                 $("#btnPD2").attr("disabled", true);
             }

         }

    </script>
     <style type="text/css">
        
        
        #custom-menu
        {
        z-index: 1000;
        position: absolute;
        border: solid 2px black;
        background-color: white;
        padding: 5px 0;
        display: none;
        }
        #custom-menu ol
        {
        padding: 0;
        margin: 0;
        list-style-type: none;
        min-width: 130px;
        width: auto;
        max-width: 200px;
        font-family:Verdana;
        font-size:12px;
        }
        #custom-menu ol li
        {
        margin: 0;
        display: block;
        list-style: none;
        padding: 5px 5px;
        }
        #custom-menu ol li:hover
        {
        background-color: #efefef;
        }

        #custom-menu ol li:active
        {
        color: White;
        background-color: #000;
        }

        #custom-menu ol .list-devider
        {
        padding: 0px;
        margin: 0px;
        }

        #custom-menu ol .list-devider hr
        {
        margin: 2px 0px;
        }

        #custom-menu ol li a
        {
        color: Black;
        text-decoration: none;
        display: block;
        padding: 0px 5px;
        }
        #custom-menu ol li a:active
        {
        color: White;
        }
</style>
</asp:Content>






<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="contenedor" runat="server" class="container">
        
        <div class="row"> 
            <div id="divApertura" >
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h3 class="panel-title">Catálogo de Apertura Programática</h3>
                    </div>
                    <div class="panel-body" id="divArbol" style="height:250px; overflow:scroll" runat="server"> 
                        <asp:TreeView runat="server" SelectedNodeStyle-ForeColor="Black" AutoPostBack="false" ID="treeApertura"  ShowLines="True"  NodeIndent="25"  AutoGenerateDataBindings="False" Width="117px" OnSelectedNodeChanged="treeApertura_SelectedNodeChanged" >
                            <ParentNodeStyle Font-Bold="true" />  
                            <SelectedNodeStyle Font-Bold="true" BackColor="LightGray" ForeColor="Green" />  
                        </asp:TreeView>
                         <div id="custom-menu">
                                <ol>
                                    <li>
                                        <asp:Button ID="addp" Width="160px" AutoPostBack="false" CssClass="btn btn-default evento" OnClientClick="return false" runat="server" Text="Agregar Registro" /> 

                                    </li>
                                    <li>
                                        <asp:Button ID="addsp" Width="160px" AutoPostBack="false" CssClass="btn btn-default evento" OnClientClick="return false" runat="server" Text="Agregar Subnivel" /> 
                                    </li>
                                    
                                    <li class="list-devider">
                                        <hr />
                                    </li>
                                    <li>
                                        <asp:Button ID="edit" Width="160px" AutoPostBack="false" OnClientClick="return false" CssClass="btn btn-default evento" runat="server" Text="Editar" /> 
                                    </li>
                                    <li>
                                        <input type="button" id="btnBorrar" style="width:160px" onclick="fnc_OcultarMC()" data-toggle="modal" data-target="#myModal" class="btn btn-default" value="Borrar" />
                                    </li>
                                     
                                    <li>
                                        <asp:Button ID="btnGuardar2"  Width="160px" runat="server" Text="Guardar" OnClick="btnguardar_Click" OnClientClick="return fnc_Validar()" CssClass="btn btn-default" />
                                    </li>
                                    <li>
                                        <asp:Button ID="btnCancelar" Width="160px" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" OnClientClick="fnc_HabilitarInHabilitarOpciones(false)" CssClass="btn btn-default" AutoPostBack="false" />
                                    </li>
                                    <li class="list-devider">
                                    <hr />
                                    </li>
                                </ol>
                            </div> 
                    </div>
                </div>
             </div>
        </div>
        
        <div class="row">
            <div id="divcaptura" runat="server" class="panel panel-success">
                    <div class="panel-heading">
                        <h3 class="panel-title">Datos de la apertura programática</h3>
                    </div>
                   <div class="row top-buffer">
                       <div class="col-md-2"> 
                            <p class="text-right"><strong>clave</strong></p>
                        </div>
                        <div class="col-md-6">
                            <asp:textbox width="180px" id="txtClave" text="0" cssclass="form-control" runat="server"></asp:textbox>
                        </div>
                    </div>
                   <div class="row top-buffer">
                       <div class="col-md-2"> 
                            <p class="text-right"><strong>Nombre</strong></p>
                        </div>
                        <div class="col-md-10">
                            <asp:textbox width="800px" height="60px" textmode="multiline" id="txtNombre" cssclass="form-control" runat="server"></asp:textbox>
                        </div>
                    </div>

                   <div class="row top-buffer">
                       <div class="col-md-2"> 
                            <p class="text-right"><strong>orden</strong></p>
                        </div>
                        <div class="col-md-6">
                            <input type="number" class="form-control" id="txtOrden" runat="server" />
                        </div>
                    </div>
                     

                
                <div class="panel-footer" id="divguardar" runat="server">
                    
                    <asp:button id="btnGuardar" runat="server" text="guardar" onclientclick="return fnc_Validar()" onclick="btnguardar_Click" cssclass="btn btn-default" />
                    <asp:button id="btnCancelar2" runat="server" text="cancelar" cssclass="btn btn-default" onclick="btnCancelar_Click" autopostback="false" onclientclick="fnc_habilitarinhabilitaropciones(false)" />
                </div> 

                 <div class="panel-footer" id="divmsg" style="display:none" runat="server">
                    <asp:label  id="lblMensajes" runat="server"></asp:label>
                </div>

            </div>
        </div>

        <div runat="server" style="display:none">
            <input type="hidden" runat="server" id="_Accion" />
            <input type="hidden" runat="server" id="_IdApertura" />
            <input type="hidden" runat="server" id="_Padre" />
            <input type="hidden" runat="server" id="_rutaNodoSeleccionado" />
            <input type="hidden" runat="server" id="_PageIndex" />

            <input type="hidden" runat="server" id="_Nivel" />
        </div>
    </div>


<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
        <h4 class="modal-title" id="myModalLabel">Confirmación</h4>
      </div>
      <div class="modal-body">
        <h3 id="msgContenido"></h3>
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnDel" runat="server" CssClass="btn btn-default" Text="Aceptar" OnClick="btnDel_Click"  />
        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
      </div>
        
    </div>
  </div>
</div>
</asp:Content>
    
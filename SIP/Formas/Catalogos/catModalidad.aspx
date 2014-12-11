<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="catModalidad.aspx.cs" Inherits="SIP.Formas.Catalogos.catModalidad" %>


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
                    $("#<%=  treeMain.ClientID %>").attr("disabled", true)
                fnc_ClickMenu(control);
            });
        }
            catch (err) {
                alert(err);
            }

        });

    //Funcion que evita que se "RESCROLEE" el arbol al seleccionar un NODO
    function SetSelectedTreeNodeVisible(controlID, boolHayPlantillas) {

        var elem = document.getElementById("<%=  treeMain.ClientID%>" + "_SelectedNode");
        if (elem != null) {
            var node = document.getElementById(elem.value);
            if (node != null) {
                node.scrollIntoView(true);
                $("#<%= divArbol.ClientID %>").scrollLeft = 0;
                 }
             }

             fnc_CargaInicial();
         }

         function fnc_CargaInicial() {

             nodes = document.getElementById("<%= txtNombre.ClientID%>").childNodes;

            if (nodes.length == 0) {
                $("#<%= addsp.ClientID %>").attr("disabled", true);
                $("#<%= edit.ClientID %>").attr("disabled", true);
                $("#<%= btnDel.ClientID %>").attr("disabled", true);
                $("#btnBorrar").attr("disabled", true);

            }

        }


        //funcion que permite habiltar o inhabilitar las opciones del menu contextual
        //de acuerdo al parametro recibido boolhabilitar
        //Creado por Rigoberto TS
        //22/09/2014
        function fnc_HabilitarInHabilitarOpciones(boolHabilitar) {

            var blockNone;

            if (boolHabilitar) {
                blockNone = "block";
                $("#<%= divArbol.ClientID %>").css("display", "none")
            }
            else {
                blockNone = "none";
                $("#<%= divArbol.ClientID %>").css("display", "block")
             }


             $("#<%= divcaptura.ClientID %>").css("display", blockNone)
            $("#<%= btnMenuCancelar.ClientID%>").css("display", blockNone);
            $("#<%= btnMenuGuardar.ClientID%>").css("display", blockNone);


            $("#<%= addp.ClientID %>").attr("disabled", boolHabilitar);
            $("#<%= addsp.ClientID %>").attr("disabled", boolHabilitar);
            $("#<%= btnDel.ClientID %>").attr("disabled", boolHabilitar);
            $("#btnBorrar").attr("disabled", boolHabilitar);

            $("#<%= edit.ClientID %>").attr("disabled", boolHabilitar)



             $("#custom-menu").hide();

         }





         function fnc_LimpiarCampos() {
             $("#<%= txtClave.ClientID%>").val("");
            $("#<%= txtNombre.ClientID%>").val("");
        }


        function fnc_ClickMenu(sender) {
            var limpiar = true;
            var procede = true;

            switch (sender) {

                case "<%= addp.ClientID %>": //AGREGAR Nivel 1

                     $("#<%= _Accion.ClientID%>").val("Nuevo");
                     $("#<%= _Tipo.ClientID%>").val("Grupo");

                     $("#<%= SpanGrupo.ClientID %>").css("display", "block")
                     $("#<%= SpanFondo.ClientID %>").css("display", "none")
                     $("#<%= SpanModificar.ClientID %>").css("display", "none")
                     break;

                 case "<%= addsp.ClientID %>": //AGREGAR SubNivel

                     var idPadre = parseInt($("#<%= _ElId.ClientID%>").val());

                     if (idPadre != 0) {
                         $("#<%= _Accion.ClientID%>").val("Nuevo");
                         $("#<%= _Tipo.ClientID%>").val("Fondo");

                         $("#<%= SpanGrupo.ClientID %>").css("display", "none")
                         $("#<%= SpanFondo.ClientID %>").css("display", "block")
                         $("#<%= SpanModificar.ClientID %>").css("display", "none")
                     } else {
                         procede = false;
                     }

                     break;

                 case "<%= edit.ClientID %>": //EDITAR UN REGISTRO

                     $("#<%= _Accion.ClientID%>").val("Modificar");
                     limpiar = false;

                     $("#<%= SpanGrupo.ClientID %>").css("display", "none")
                    $("#<%= SpanFondo.ClientID %>").css("display", "none")
                     $("#<%= SpanModificar.ClientID %>").css("display", "block")

                     break;

             }

             if (procede) {


                 if (limpiar)
                     fnc_LimpiarCampos();

                 fnc_HabilitarInHabilitarOpciones(true);
             }



             $("#custom-menu").hide(); //Se oculta el menu contextual


         }



         function fnc_OcultarMC() {
             var container = $("#custom-menu");
             container.hide();

             $("#msgContenido").text("¿Está seguro que desea eliminar el registro?"); //Se cambia el mensaje del dialogo modal de confirmacion
         }


         function fnc_Validar() {

             var desc = $("#<%= txtClave.ClientID%>").val();
            if (desc == null || desc.length == 0 || desc == undefined) {
                $("#custom-menu").hide(); //Se oculta el menu contextual 
                $("#msgContenido").text("El campo CLAVE no puede ir vacío"); //Se cambia el mensaje del dialogo modal de confirmacion
                $("#myModal").modal('show') //Se muestra el modal
                return false;
            }



            desc = $("#<%= txtNombre.ClientID%>").val();
            if (desc == null || desc.length == 0 || desc == undefined) {
                $("#custom-menu").hide(); //Se oculta el menu contextual 
                $("#msgContenido").text("El campo NOMBRE no puede ir vacío"); //Se cambia el mensaje del dialogo modal de confirmacion
                $("#myModal").modal('show') //Se muestra el modal
                return false;
            }

            return true;
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
            <div id="divDatos" >
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h3 class="panel-title">Clasificación Programática CONAC</h3>
                    </div>
                    <div class="panel-body" id="divArbol" style="height:250px; overflow:scroll" runat="server"> 
                        <asp:TreeView runat="server" SelectedNodeStyle-ForeColor="Black" AutoPostBack="false" ID="treeMain"  ShowLines="True"  NodeIndent="25"  AutoGenerateDataBindings="False" Width="117px" OnSelectedNodeChanged="treeMain_SelectedNodeChanged">
                            <ParentNodeStyle Font-Bold="true" />  
                            <SelectedNodeStyle Font-Bold="true" BackColor="LightGray" ForeColor="Green" />  
                        </asp:TreeView>
                         <div id="custom-menu">
                                <ol>
                                    <li>
                                        <asp:Button ID="addp" Width="160px" AutoPostBack="false" CssClass="btn btn-default evento" OnClientClick="return false"  runat="server" Text="Agregar Grupo" /> 

                                    </li>
                                    <li>
                                        <asp:Button ID="addsp" Width="160px" AutoPostBack="false" CssClass="btn btn-default evento" OnClientClick="return false"  runat="server" Text="Agregar Registro" /> 
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
                                        <asp:Button ID="btnMenuGuardar"  Width="160px" runat="server" Text="Guardar" OnClick="btnGuardar_Click"  CssClass="btn btn-default" />
                                    </li>
                                    <li>
                                        <asp:Button ID="btnMenuCancelar" Width="160px" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" OnClientClick="fnc_HabilitarInHabilitarOpciones(false)" CssClass="btn btn-default" AutoPostBack="false" />
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
                        <h3 class="panel-title">Indique los datos del registro</h3>
                    </div>

                
                    <span runat="server" id="SpanModificar" class="label label-primary">Modificando ...</span>
                    <span runat="server" id="SpanGrupo" class="label label-primary">Agregando Grupo ...</span>
                    <span runat="server" id="SpanFondo" class="label label-primary">Agregando Registro ...</span>

                     



                   <div class="row top-buffer">
                       <div class="col-md-2"> 
                            <p class="text-right"><strong>Clave</strong></p>
                        </div>
                        <div class="col-md-6">
                            <asp:textbox width="180px" id="txtClave" text="0" cssclass="form-control" runat="server"></asp:textbox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClave" ErrorMessage="El campo Clave es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                    </div>


                   <div class="row top-buffer">
                       <div class="col-md-2"> 
                            <p class="text-right"><strong>Nombre</strong></p>
                        </div>
                        <div class="col-md-10">
                            <asp:textbox width="1000px" height="60px" textmode="multiline" id="txtNombre" cssclass="form-control" runat="server"></asp:textbox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNombre" ErrorMessage="El campo Nombre es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                    </div>

               


                <div class="form-group" id="divguardar" runat="server">
                    <asp:Button  CssClass="btn btn-default" Text="Guardar" Id="btnGuardar" runat="server"   OnClick="btnGuardar_Click" AutoPostBack="false" ValidationGroup="validateX" />
                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" Id="btnCancelar" runat="server" OnClientClick="fnc_habilitarinhabilitaropciones(false)" OnClick ="btnCancelar_Click" AutoPostBack="false" />
                </div>
                

            </div>
        </div>



        <div class="panel-footer alert alert-success" id="divMsgSuccess" style="display:none" runat="server">
        <asp:Label ID="lblMensajeSuccess" runat="server" Text=""></asp:Label>
        </div>
        <div class="panel-footer alert alert-danger" id="divMsg" style="display:none" runat="server">
        <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
        </div>


        <div runat="server" style="display:none">
            <input type="hidden" runat="server" id="_Accion" />
            <input type="hidden" runat="server" id="_Tipo" />
            
            <input type="hidden" runat="server" id="_ElId" />
            <input type="hidden" runat="server" id="_rutaNodoSeleccionado" />            
        </div>

        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />
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


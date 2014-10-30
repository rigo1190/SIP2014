<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="EvaluacionPOA.aspx.cs" Inherits="SIP.Formas.POA.EvaluacionPOA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {

            $("#linkDatos").click(function () {
                $("#<%= divDatos.ClientID %>").css("display", "block");
                $("#<%= divEvaluacion.ClientID %>").css("display", "none");
            });

            $("#linkEvaluacion").click(function () {
                $("#<%= divDatos.ClientID %>").css("display", "none");
                $("#<%= divEvaluacion.ClientID %>").css("display", "block");
            });
            
            //Evento que dispara la llamada a un WEB METHOD que se encarga de guardar las respuestas que se hayan cheqeado en el grid
            $("#btnGuardarPreguntas").click(function () {
                var cadenaValores = fnc_ObtenerRadioChecks();
                PageMethods.GuardarPlantillas(cadenaValores, fnc_ResponseGuardarPlantillas);
            });

            //Evento que se dispara al momento de dar clic en la paginacion del grid. Se tiene que
            //guardar los checks marcaddos en el grid por parte del usuario
            $('.pager a').click(function () {
                
                var cadenaValores = fnc_ObtenerRadioChecks(); //Se recupera la cadena con los checks
                $("#<%= _CadValoresChecks.ClientID %>").val("");
                $("#<%= _CadValoresChecks.ClientID %>").val(cadenaValores);
            });


            $("[id*=grid] td").bind("click", function () {
                var row = $(this).parent();
                $("[id*=grid] tr").each(function () {
                    if ($(this)[0] != row[0]) {
                        $("td", this).removeClass("selected_row");
                    }
                });
                $("td", row).each(function () {
                    if (!$(this).hasClass("selected_row")) {
                        if (!$(this).hasClass("pager"))
                            $(this).addClass("selected_row");
                    } else {
                        $(this).removeClass("selected_row");
                    }
                });
                $('.pager a').removeClass("selected_row");
            });

        });

        //Funcion que se encarga de recuperar todas las FILAS del GRID de PREGUNTAS
        //Para armar la cadena que se enviara al codigo de c# con el 
        //formato de ID=VALOR|ID=VALOR|ID=VALOR|ID=VALOR|ID=VALOR|.................
        //Creada por Rigoberto TS
        //07/10/2014
        function fnc_ObtenerRadioChecks(edicion) {
            var cadena = "";
            var index=0;
            var grid = document.getElementById('<%=grid.ClientID %>'); //Se recupera el grid
            var primera=true;

            for (i = 1; i < grid.rows.length; i++) { //Se recorren las filas
                var idPregunta="";
                var respuesta = 0;

                idPregunta=$("input#ContentPlaceHolder1_grid"+ "_idPregunta_" + index).val();
                
                if (idPregunta != null && idPregunta != "" && idPregunta != undefined) {

                    if ($("input#ContentPlaceHolder1_grid" + "_chkSI_" + index).is(':checked'))
                        respuesta = 1;
                    else if ($("input#ContentPlaceHolder1_grid" + "_chkNO_" + index).is(':checked'))
                        respuesta = 2;
                    else if ($("input#ContentPlaceHolder1_grid" + "_chkNOAplica_" + index).is(':checked'))
                        respuesta = 3;

                    if (primera) {
                        cadena += idPregunta + "=" + respuesta;
                        primera = false;
                    } else
                        cadena += "|" + idPregunta + "=" + respuesta;
                }

                index++;
            }

            if (edicion)
                $("#<%= _CadValoresChecks.ClientID %>").val(cadena);
            else
                return cadena;
        }

        function fnc_InhabilitarCampos(){
            $("#<%= txtObservacionesPregunta.ClientID %>").prop("disabled",true);
            <%-- $("#<%= txtRutaArchivo.ClientID %>").prop("disabled",true);--%>
            $("#<%= divCapturaPreguntas.ClientID %>").css("display", "none");
            $("#<%= btnCancelar.ClientID %>").prop("disabled", true);
            $("#<%= btnBuscarArchivo.ClientID %>").prop("disabled", true);
            $("#<%= _IDPregunta.ClientID %>").val("");
            $("#<%= _SoloChecks.ClientID %>").val("true");
            
        }
        

        //Funcion que dispara la llamda a un WEB METHOD en C#
        //para traer informacion de la pregunta que se haya seleccionado o "cliqueado" en el grid
        //Creada por Rigoberto TS
        //07/10/2014
        function fnc_ClickRow(index) {
            var idPregunta = "";

            idPregunta = $("input#ContentPlaceHolder1_grid" + "_idPregunta_" + index).val();

            if (idPregunta != "" || idPregunta != null || idPregunta != undefined) {
                $("#<%= _IDPregunta.ClientID %>").val(idPregunta);
                PageMethods.GetValoresPregunta(idPregunta, fnc_ResponseGetValoresPregunta); //Se manda a llamar el metodo a C#
            }

            $("#<%= divMsgError.ClientID %>").css("display", "none");
            $("#<%= divMsgSuccess.ClientID %>").css("display", "none");
    
        }

        //Funcion a la que se regresa despuess de la llamada al WEB METHOD GetValoresPregunta
        //Se traen los valores de la pregunta, y se colocan en los controles correspondientes
        //Creada por Rigoberto TS
        //07/10/2014
        function fnc_ResponseGetValoresPregunta(response) {

            $("#<%= txtObservacionesPregunta.ClientID %>").text(response[0]);
            $("#<%= txtArchivoAdjunto.ClientID %>").val(response[1]);
            $("#<%= txtPregunta.ClientID %>").text(response[2]);
            

        }

        function fnc_ResponseGuardarPlantillas(response)
        {
            fnc_InhabilitarCampos();
            alert(response[0]);
            
        }

      //Funcion que evita que se "RESCROLEE" el arbol al seleccionar un NODO
        function SetSelectedTreeNodeVisible(controlID, boolHayPlantillas) {

            var elem = document.getElementById("<%=treePlantilla.ClientID%>" + "_SelectedNode");
             if (elem != null) {
                 var node = document.getElementById(elem.value);
                 if (node != null) {
                     node.scrollIntoView(true);
                     $("#<%= divArbol.ClientID %>").scrollLeft = 0;
                }
            }

        }

        function fnc_OcultarDivs() {
            $("#<%= divMsgImportarPlantilla.ClientID %>").css("display", "none");
        }

        
        function fnc_DesmarcarChecks(sender, index, grid,radio1,radio2) {

            if ($(sender).is(':checked')) {
                $(sender).val("true");
                $(sender).prop("checked", true);
                $("input#ContentPlaceHolder1_" + grid + "_" + radio1 + "_" + index).val(false);
                $("input#ContentPlaceHolder1_" + grid + "_" + radio2 + "_" + index).val(false);

                fnc_ClickRow(index)
            }

        }

        //Functin que se encarga de abrir una nueva ventana con el contenido del archivo que se haya adjuntado en la pregunta
        //Creado por Rigoberto TS
        //15/10/2014
        function fnc_AbrirArchivo(ruta, id) {
            window.open(ruta + '?i=' + id, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=750,height=600,top=0');
        }


        function fnc_Edicion() {

            $("#<%= txtObservacionesPregunta.ClientID %>").prop("disabled", false);
            $("#<%= divCapturaPreguntas.ClientID %>").css("display", "block");
            $("#<%= btnCancelar.ClientID %>").prop("disabled", false);
            $("#<%= btnBuscarArchivo.ClientID %>").prop("disabled", false);
            $("#<%= _IDPregunta.ClientID %>").val("");
            $("#<%= _SoloChecks.ClientID %>").val("false");
            return false;
        }

        <%--function fnc_Test() {
            //PageMethods.GuardarPlantillas("cadena", fnc_Success);
            var cad = "cadena";

            $.ajax({
                    type: 'POST',
                    url: "<%=ResolveClientUrl("~/Ajax.aspx") %>",
                    data: {
                         'accion': 'abrirPeriodo',
                         'mes': 1,
                         'anio': 2014
                          },
                    dataType: "json",
                    success: function (data, textStatus, XMLHttpRequest) {

                        if (data.g == "1") {
                            alert(data.g);
                       }


                    },
                    error: function (error) {
                       alert("---[ Error ]---" + error + "\n" + errorThrown);
                    }
               });
        }--%>

        
       
    </script>

    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .selected_row
        {
            background-color: #c2c2c2;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container">
       
      
        <div class="page-header"">
            <h3>Evaluación de POA</h3>
        </div>


           <div class="row">
            <div class="container-fluid">
                <div id="divMenu" runat="server">
                    <ul class="nav nav-tabs nav-justified panel-success">
                        <li class="active"><a id="linkDatos" href="#">Datos Generales Obra</a></li>
                        <li class="active"><a id="linkEvaluacion" href="#">Plantillas de Evaluación Importadas</a></li>
                    </ul>
                </div>
            </div>
        </div>

           <div id="divDatos" runat="server">
            <div class="container-fluid">
                <div class="row">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                                <h3 class="panel-title">
                                Obra de POA
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-6">
                                
                                        <fieldset disabled="disabled">
                                        <div class="form-group">
                                            <label for="disabledSelect">Número de Obra:</label>
                                            <textarea type="text" class="form-control"  id="txtNumero"  runat="server"/>
                                        </div>

                                        <div class="form-group">
                                            <label>Descripción:</label>
                                            <textarea type="text"  name="prueba" runat="server" class="form-control" id="txtDescripcion" />
                                        </div>

                                        <div class="form-group">
                                            <label>Municipio:</label>
                                            <textarea type="text" class="form-control"  id="txtMunicipio" runat="server"/>
                                        </div>
                                        
                                    </fieldset>
                                
                            </div>
                            <div class="col-lg-6">
                                    <fieldset disabled="disabled">
                                        <div class="form-group">
                                            <label for="disabledSelect">Localidad:</label>
                                            <textarea type="text" runat="server" class="form-control" id="txtLocalidad"/>
                                        </div>
                                        <div class="form-group">
                                            <label for="disabledSelect">Observaciones:</label>
                                            <textarea type="text" name="prueba" runat="server" class="form-control" id="txtObservacion" />
                                        </div>
                                    </fieldset>
                            </div>
                        </div>
                        <div class="panel-footer" id="divMsgImportarPlantilla" style="display:none" runat="server">
                            <asp:Label  ID="lblMsgImportarPlantilla" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>

           <div class="row">
            <div id="divEvaluacion" style="display:none" runat="server">
                <div class="container-fluid">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Plantillas de Evaluación para Obra de POA
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-6">
                                <asp:TreeView runat="server" SelectedNodeStyle-ForeColor="Black" AutoPostBack="false" ID="treePOAPlantilla" OnSelectedNodeChanged="treePOAPlantilla_SelectedNodeChanged"  ShowLines="True"  NodeIndent="25"  AutoGenerateDataBindings="False" Width="117px">
                                    <ParentNodeStyle Font-Bold="true" />  
                                    <SelectedNodeStyle Font-Bold="true" BackColor="LightGray" ForeColor="Green" />  
                                </asp:TreeView>       
                            </div>
                        </div>

                        <div class="panel-footer" id="divPreguntas" runat="server">
                             <div class="panel panel-success">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        Contenido de la plantilla
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <asp:GridView Height="25px" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" OnPageIndexChanging="grid_PageIndexChanging" CssClass="table" ID="grid" OnRowDataBound="grid_RowDataBound" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Acciones">
                                                <ItemTemplate>
                                                    <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" OnClientClick="fnc_Edicion();return false;" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                </ItemTemplate>
                                                <HeaderStyle BackColor="#EEEEEE" />
                                                <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Pregunta de Evaluación" SortExpression="Orden">
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                    <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                           <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="SI" SortExpression="SI">
                                                <ItemTemplate>
                                                    <input type="radio" value="false" runat="server" id="chkSI" />
                                                </ItemTemplate>
                                                
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="NO" SortExpression="NO">
                                                <ItemTemplate>
                                                    <input type="radio" value="false"  runat="server" id="chkNO" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="NO APLICA" SortExpression="NOAplica">
                                                <ItemTemplate>
                                                    <input type="radio" value="false"  runat="server" id="chkNOAplica" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                <ItemTemplate>
                                                    <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="pager" />
                                        <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                                        
                                    </asp:GridView>
                                    
                                </div>
                                <div class="panel-footer" id="divEdicionPreguntas" runat="server">
                                    <div class="container-fluid" id="divCapturaPreguntas" style="display:none" runat="server">
                                        
                                            <div class="col-lg-6">
                                                 
                                                    <div class="form-group">
                                                        <label for="disabledSelect">Pregunta de Evaluación</label>
                                                        <textarea  type="text"  disabled="disabled" name="prueba" runat="server" class="form-control" id="txtPregunta" />
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="disabledSelect">Observaciones:</label>
                                                        <textarea  type="text" disabled="disabled" name="prueba" runat="server" class="form-control" id="txtObservacionesPregunta" />
                                                    </div>
                                                   
                                               
                                            </div>
                                            <div class="col-lg-6">
                                                
                                                    <div class="form-group">
                                                        <label>Documento Soporte actual:</label>
                                                        <input type="text" disabled="disabled" name="prueba" id="txtArchivoAdjunto" runat="server" class="form-control"  />
                                                    </div>
                                                    <div class="form-group">
                                                        <label for="disabledSelect">Documento Soporte:</label>
                                                        <asp:FileUpload ID="btnBuscarArchivo" Enabled="false" runat="server" />
                                                    </div>
                                                    
                                                
                                            </div>

                                    </div>
                                    <div class="row top-buffer">
                                        <div class="col-md-6"  id="divBtnGuardarDatosPreguntas" runat="server">
                                            <input id="btnGuardarDatosPreguntas" class="btn btn-default" runat="server" onclick="fnc_ObtenerRadioChecks(true);" onserverclick="btnGuardarDatosPreguntas_ServerClick" type="button" value="Guardar" />
                                            <input id="btnCancelar" disabled="disabled" class="btn btn-default"  runat="server" type="button" onclick="fnc_InhabilitarCampos();" value="Cancelar" />
                                        </div>
                                    </div>
                                    <div class="row top-buffer">
                                         <div class="alert alert-danger" runat="server" id="divMsgError" style="display:none">
                                            <asp:Label ID="lblMsgError" EnableViewState="false" runat="server" Text="" CssClass="font-weight:bold"></asp:Label>
                                        </div>
                                        <div class="alert alert-success" runat="server" id="divMsgSuccess" style="display:none">
                                            <asp:Label ID="lblMsgSuccess" EnableViewState="false" runat="server" Text="" CssClass="font-weight:bold"></asp:Label>
                                        </div>  
                                    </div> 
                                       
                                </div>

                            </div>
                        </div> 

                    </div>
                </div> 
            </div>
        </div>

           <div runat="server" style="display:none">
            <input type="hidden" runat="server" id="_IDPlantilla" />
            <input type="hidden" runat="server" id="_IDPOADetalle" />
            <input type="hidden" runat="server" id="_IDPregunta" />
            <input type="hidden" runat="server" id="_CadValoresChecks" />
            <input type="hidden" runat="server" id="_SoloChecks" />
            <input type="hidden" runat="server" id="_PageIndex" />
          
       </div>


       

       

   </div>
    
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
      <div class="modal-dialog modal-sm">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
            <h4 class="modal-title" id="myModalLabel">Catálogo de plantillas</h4>
          </div>
          <div class="modal-body">
                <div class="divCatalogo">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h3 class="panel-title">Plantillas</h3>
                    </div>
                    <div class="panel-body" id="divArbol" style="height:300px; overflow:scroll" runat="server"> 
                        <asp:TreeView runat="server" SelectedNodeStyle-ForeColor="Black"  AutoPostBack="false" ID="treePlantilla"  ShowLines="True"  NodeIndent="25"  AutoGenerateDataBindings="False" Width="117px">
                            <ParentNodeStyle Font-Bold="true" />  
                            <SelectedNodeStyle Font-Bold="true" BackColor="LightGray" ForeColor="Green" />  
                        </asp:TreeView>
                    </div>
                </div>
         </div>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Aceptar</button>
            <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
          </div>
        
        </div>
      </div>
    </div>

</asp:Content>

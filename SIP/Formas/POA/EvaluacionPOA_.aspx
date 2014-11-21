<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="EvaluacionPOA_.aspx.cs" Inherits="SIP.Formas.POA.EvaluacionPOA_" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function fnc_AbrirReporte() {

            var izq = (screen.width - 750) / 2
            var sup = (screen.height - 600) / 2
            var param = "";
            //param = fnc_ArmarParamentros();
            url = $("#<%= _URLVisor.ClientID %>").val();
            var argumentos = "?c=" + 1;
            url += argumentos;
            window.open(url, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=750,height=500,top=' + sup + ',left=' + izq);
        }


        function fnc_Edicion(idPregunta,numGrid) {
            var cadenaValores;
            var nombreGrid;

            cadenaValores = fnc_ObtenerRadioChecks(numGrid);

            $("#<%= _CadValoresChecks.ClientID %>").val(cadenaValores);
            $("#<%= _IDPregunta.ClientID %>").val(idPregunta);
            $("#<%= _NumGrid.ClientID %>").val(numGrid);

            PageMethods.GetValoresPregunta(idPregunta, fnc_ResponseGetValoresPregunta); //Se manda a llamar el metodo a C#

        }


        //Funcion a la que se regresa despuess de la llamada al WEB METHOD GetValoresPregunta
        //Se traen los valores de la pregunta, y se colocan en los controles correspondientes
        //Creada por Rigoberto TS
        //07/10/2014
        function fnc_ResponseGetValoresPregunta(response) {

            $("#<%= txtObservacionesPregunta.ClientID %>").text(response[0]);
            $("#<%= txtArchivoAdjunto.ClientID %>").val(response[1]);
            $("#<%= txtPregunta.ClientID %>").val(response[2]);

            $("#modalDatos").modal('show') //Se muestra el modal
        }



        function fnc_GuardarChecks(numGrid) {
            var cadenaValores;
            cadenaValores = fnc_ObtenerRadioChecks(numGrid);
            PageMethods.GuardarChecks(cadenaValores, fnc_ResponseGuardarChecks);
        }


        function fnc_ResponseGuardarChecks(response) {
            alert(response[0]);
        }

        //Funcion que se encarga de recuperar todas las FILAS del GRID de PREGUNTAS
        //Para armar la cadena que se enviara al codigo de c# con el 
        //formato de ID:RES:PRES|ID:RES:PRES|ID:RES:PRES|ID:RES:PRES|ID:RES:PRES|.................
        //Creada por Rigoberto TS
        //07/10/2014
        function fnc_ObtenerRadioChecks(numGrid) {
            var cadena = "";
            var index = 0;
            var grid;
            var nomGrid = fnc_GetNombreGrid(numGrid);

            switch (numGrid) {
                case 1: //PLAN DE DESARROLLO VERCRUZANO
                    grid = document.getElementById('<%=gridPlanDesarrollo.ClientID %>'); //Se recupera el grid
                    break;
                case 2: //ANTEPROYECTO
                    grid = document.getElementById('<%=gridAnteproyecto.ClientID %>'); //Se recupera el grid
                    break;
                case 3: //Fondo y programa
                    grid = document.getElementById('<%=gridFondoPrograma.ClientID %>'); //Se recupera el grid
                    break;
                case 4: //Proyecto Ejecutivo y Proyecto Base
                    grid = document.getElementById('<%=gridProyectoEjecutivo.ClientID %>'); //Se recupera el grid
                    break;
                case 5: //Adjudicacion Directa
                    grid = document.getElementById('<%=gridAdjuDirecta.ClientID %>'); //Se recupera el grid
                    break;
                case 6: //Adjudicacion por excepcion de ley
                    grid = document.getElementById('<%=gridExcepcion.ClientID %>'); //Se recupera el grid
                    break;
                case 7: //Invitacion a cuando menos tres personas
                    grid = document.getElementById('<%=gridInvitacion.ClientID %>'); //Se recupera el grid
                    break;
                case 8: //Licitacion Pública
                    grid = document.getElementById('<%=gridLicitacion.ClientID %>'); //Se recupera el grid
                    break;
                case 9: //Presupuesto Autorizado Contrato
                    grid = document.getElementById('<%=gridPresupuesto.ClientID %>'); //Se recupera el grid
                    break;
                case 10: //Administración Directa
                    grid = document.getElementById('<%=gridAdmin.ClientID %>'); //Se recupera el grid
                    break;

            }

            
            var primera = true;

            for (i = 1; i < grid.rows.length; i++) { //Se recorren las filas
                var idPregunta = "";
                var respuesta = 0;
                var presento = 0;

                idPregunta = $("input#ContentPlaceHolder1_" + nomGrid + "_idPregunta_" + index).val();

                if (idPregunta != null && idPregunta != "" && idPregunta != undefined) {

                    if ($("input#ContentPlaceHolder1_" + nomGrid + "_chkRequiere_" + index).is(':checked'))
                        respuesta = 1;
                    else 
                        respuesta = 2;

                    if ($("input#ContentPlaceHolder1_" + nomGrid + "_chkPresento_" + index).is(':checked'))
                        presento = 1;
                    else
                        presento = 2;

                    if (primera) {
                        cadena += idPregunta + ":" + respuesta+":"+presento;
                        primera = false;
                    } else
                        cadena += "|" + idPregunta + ":" + respuesta + ":" + presento;
                }

                index++;
            }

            return cadena;
        }


        function fnc_GetNombreGrid(numGrid) {

            var nombre = "";

            switch (numGrid) {
                case 1:
                    nombre = "gridPlanDesarrollo";
                    break;
                case 2:
                    nombre = "gridAnteproyecto";
                    break;
                case 3:
                    nombre = "gridFondoPrograma";
                    break;
                case 4:
                    nombre = "gridProyectoEjecutivo";
                    break;
                case 5:
                    nombre = "gridAdjuDirecta";
                    break;
                case 6:
                    nombre = "gridExcepcion";
                    break;
                case 7:
                    nombre = "gridInvitacion";
                    break;
                case 8:
                    nombre = "gridLicitacion";
                    break;
                case 9:
                    nombre = "gridPresupuesto";
                    break;
                case 10:
                    nombre = "gridAdmin";
                    break;
            }

            return nombre;

        }



        //Functin que se encarga de abrir una nueva ventana con el contenido del archivo que se haya adjuntado en la pregunta
        //Creado por Rigoberto TS
        //15/10/2014
        function fnc_AbrirArchivo(ruta, id) {
            var izq = (screen.width - 750) / 2
            var sup = (screen.height - 600) / 2
            window.open(ruta + '?i=' + id, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=750,height=500,top=' + sup + ',left=' + izq);
        }

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container">
        <div class="row" runat="server" id="divDatosGenerales">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa"></i>Datos de la Obra</h3>
                </div>
                <div class="panel-body">
                    
                    <div class="row top-buffer">
                        <div class="col-md-2">
                            <label for="disabledSelect">Entidad Ejecutora:</label>
                        </div>
                        <div class="col-md-4">
                            <input type="text" id="txtEntidad" runat="server" class="form-control" disabled="disabled" />
                        </div>
                        <div class="col-md-2">
                            <label for="disabledSelect">Fecha:</label>
                        </div>
                        
                        <div class="col-md-4">
                            <input type="text" id="txtFecha" runat="server" class="form-control" disabled="disabled" />
                        </div>

                    </div>
                    <div><p></p></div>

                    <div class="row top-buffer">
                        <div class="col-md-4">
                            <label for="disabledSelect">Número de Obra:</label>
                            <input type="text" class="form-control" disabled="disabled"  id="txtNumero"  runat="server"/>
                        </div>

                        <div class="col-md-4">
                            <label for="disabledSelect">Fondo:</label>
                            <input type="text" class="form-control" disabled="disabled"  id="txtFondo"  runat="server"/>
                        </div>
                           
                        <div class="col-md-4">
                            <label for="disabledSelect">Techo Financiero:</label>
                            <input type="text" class="form-control" disabled="disabled"  id="txtTecho"  runat="server"/>
                        </div>

                    </div>
                    <div><p></p></div>
                    <div class="row top-buffer">
                        <div class="col-md-4">
                            <label>Descripción:</label>
                            <textarea type="text" disabled="disabled"  name="prueba" runat="server" class="form-control" id="txtDescripcion" />
                        </div>
                        <div class="col-md-4">
                            <label>Lineamiento:</label>
                            <input type="text" class="form-control" disabled="disabled"  id="txtLineamiento"  runat="server"/>
                        </div>
                        <div class="col-md-4">
                            <label>Normatividad:</label>
                            <input type="text" class="form-control" disabled="disabled"  id="txtNormatividad"  runat="server"/>
                        </div>

                    </div>
                    <div><p></p></div>
                    <div class="row top-buffer">
                        <div class="col-md-4">
                            <label for="disabledSelect">Localidad:</label>
                            <input disabled="disabled" type="text" runat="server" class="form-control" id="txtLocalidad"/>
                        </div>
                        <div class="col-md-4">
                            <label>Municipio:</label>
                            <input type="text" disabled="disabled" class="form-control"  id="txtMunicipio" runat="server"/>
                        </div>

                        <div class="col-md-4">
                            <label>Programa:</label>
                            <input type="text" disabled="disabled" class="form-control"  id="txtPrograma" runat="server"/>
                        </div>
                        
                        

                    </div>
                    <div><p></p></div>
                    <div class="row top-buffer">
                        <div class="col-md-4">
                            <label>Contratista:</label>
                            <input type="text" disabled="disabled" class="form-control"  id="txtContratista" runat="server"/>
                        </div>
                        <div class="col-md-4">
                            <label>Importe Contratado:</label>
                            <input type="text" disabled="disabled" class="form-control"  id="txtImporte" runat="server"/>
                        </div>

                        <div class="col-md-4">
                            <label>Subprograma:</label>
                            <input type="text" disabled="disabled" class="form-control"  id="txtSubprograma" runat="server"/>
                        </div>
                    </div>
                </div>  
            </div>
        </div>
        
        <div class="row" id="divAcordeon">
            <div class="col-md-12">

                <div class="panel panel-success">
                    <div class="panel-heading">
                        <div class="col-md-10">
                            <h3 class="panel-title">
                                Etapas de evaluación
                            </h3>
                        </div>
                        <div >
                            <label>Reporte</label>
                            <button type="button" style="width:80px" runat="server" onclick="fnc_AbrirReporte()" id="btnVer"><span class="glyphicon glyphicon-print"></span></button>
                        </div>
                        
                    </div>
                    <div class="panel-body">
                        
                         <div class="panel-group" id="accordion">
                             <div class="panel panel-default">
                                 
                                 <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Plan de Desarrollo Estatal Urbano</a>
                                    </h4>
                                </div>
                                 <div id="collapse1" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:GridView OnRowDataBound="gridPlanDesarrollo_RowDataBound" PageSize="1000" Height="25px" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridPlanDesarrollo" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                    </ItemTemplate>
                                                
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            
                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                    </ItemTemplate>
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

                                        </asp:GridView>

                                        <div>
                                            <input value="Guardar" type="button" id="btnPlanDesarrollo" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(1);" />
                                        </div>

                                    </div>
                                </div>

                                 <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse2">Anteproyecto y normatividad</a>
                                    </h4>
                                </div>
                                 <div id="collapse2" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridAnteproyecto_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridAnteproyecto" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                    </ItemTemplate>
                                                
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                    </ItemTemplate>
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
                                            
                                        
                                        </asp:GridView>

                                        <div>
                                            <input value="Guardar" type="button" id="btnAnteproyecto" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(2);" />
                                        </div>

                                    </div>
                                </div>

                                 <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse3">Fondo y programa</a>
                                    </h4>
                                </div>
                                 <div id="collapse3" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridFondoPrograma_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridFondoPrograma" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                    </ItemTemplate>
                                                
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                    </ItemTemplate>
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
                                            
                                        
                                        </asp:GridView>

                                        <div>
                                            <input value="Guardar" type="button" id="btnFondoPrograma" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(3);" />
                                        </div>

                                    </div>
                                </div>

                                 <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse4">Proyecto Ejecutivo y Proyecto Base</a>
                                    </h4>
                                </div>
                                 <div id="collapse4" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridProyectoEjecutivo_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridProyectoEjecutivo" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                    </ItemTemplate>
                                                
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                    </ItemTemplate>
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

                                        </asp:GridView>

                                        <div>
                                            <input value="Guardar" type="button" id="btnProyecto" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(4);" />
                                        </div>

                                    </div>
                                </div>

                                 <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse5">Tipo de Adjudicación</a>
                                    </h4>
                                </div>
                                 <div id="collapse5" class="panel-collapse collapse">
                                    <div class="panel-body">

                                        <div class="panel-group" id="subaccordion">

                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#subaccordion" href="#collapse6">Adjudicación Directa</a>
                                                </h4>
                                            </div>
                                            <div id="collapse6" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                       <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridAdjuDirecta_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridAdjuDirecta" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acciones">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                                    </ItemTemplate>
                                                
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                                    </ItemTemplate>
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
                                                           
                                        
                                                       </asp:GridView>
                                                       <div>
                                                            <input value="Guardar" type="button" id="btnTipoAdju" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(5);" />
                                                       </div>
                                                </div>
                                            </div>

                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#subaccordion" href="#collapse7">Adjudicación por Excepción de Ley</a>
                                                </h4>
                                             </div>
                                            <div id="collapse7" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                       <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridExcepcion_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridExcepcion" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acciones">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                                    </ItemTemplate>
                                                
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                                    </ItemTemplate>
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
                                                            
                                        
                                                       </asp:GridView>
                                                       <div>
                                                            <input value="Guardar" type="button" id="btnExcepcion" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(6);" />
                                                       </div>
                                                </div>
                                            </div>

                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#subaccordion" href="#collapse8">Invitación a cuando menos tres personas</a>
                                                </h4>
                                            </div>
                                            <div id="collapse8" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                       <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridInvitacion_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridInvitacion" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acciones">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                                    </ItemTemplate>
                                                
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                                    </ItemTemplate>
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
                                                            
                                        
                                                       </asp:GridView>
                                                       <div>
                                                            <input value="Guardar" type="button" id="btnInvitacion" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(7);" />
                                                       </div>
                                                </div>
                                            </div>

                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <a data-toggle="collapse" data-parent="#subaccordion" href="#collapse9">Licitación Pública</a>
                                                </h4>
                                             </div>
                                            <div id="collapse9" class="panel-collapse collapse">
                                                <div class="panel-body">
                                                       <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridLicitacion_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridLicitacion" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acciones">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                                    </ItemTemplate>
                                                
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                                    </ItemTemplate>
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
                                                            
                                        
                                                       </asp:GridView>
                                                       <div>
                                                            <input value="Guardar" type="button" id="btnLicitacion" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(8);" />
                                                       </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>

                                 <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse10">Presupuesto Autorizado Contrato</a>
                                    </h4>
                                </div>
                                 <div id="collapse10" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridPresupuesto_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridPresupuesto" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                    </ItemTemplate>
                                                
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                    </ItemTemplate>
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
                                            
                                        
                                        </asp:GridView>

                                        <div>
                                            <input value="Guardar" type="button" id="btnPresupuesto" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(9);" />
                                        </div>

                                    </div>
                                </div>

                                 <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse11">Administración Directa</a>
                                    </h4>
                                </div>
                                 <div id="collapse11" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:GridView  Height="25px" OnRowDataBound="gridAdmin_RowDataBound" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridAdmin" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" PageSize="1000">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" />
                                                    </ItemTemplate>
                                                    <HeaderStyle BackColor="#EEEEEE" />
                                                    <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "PlantillaDetalle.Pregunta") %>
                                                        <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPregunta" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="REQUIERE" SortExpression="SI">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false" runat="server" id="chkRequiere" />
                                                    </ItemTemplate>
                                                
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="PRESENTÓ" SortExpression="NO">
                                                    <ItemTemplate>
                                                        <input type="checkbox" value="false"  runat="server" id="chkPresento" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Observaciones" SortExpression="Orden">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Observaciones") %>
                                                    </ItemTemplate>
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
                                            
                                        
                                        </asp:GridView>

                                        <div>
                                            <input value="Guardar" type="button" id="btnAdmin" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(10);" />
                                        </div>

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
            <input type="hidden" runat="server" id="_NumGrid" />
            <input type="hidden" runat="server" id="_URLVisor" />
          
    </div>
    <div class="modal fade" id="modalDatos" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
        
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                  <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalTitle">Datos del Documento de Evaluación</h4>
                  </div>
                    <div class="modal-body">
                      <div class="container">
                        <div class="container-fluid">
                             <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>Documento de Evaluación</label>
                                         <%--<input class="form-control" disabled="disabled" runat="server" id="txtPregunta"/>--%>
                                        <textarea style="height:120px"  type="text" disabled="disabled" name="prueba" runat="server" class="form-control" id="txtPregunta" />
                                    </div>
                                    <div class="form-group">
                                        <label for="disabledSelect">Observaciones:</label>
                                        <textarea style="height:120px"  type="text" name="prueba" runat="server" class="form-control" id="txtObservacionesPregunta" />
                                    </div>
                                    <div class="form-group">
                                        <label>Documento Soporte actual:</label>
                                        <%--<input type="text" disabled="disabled" name="prueba" id="txtArchivoAdjunto" runat="server" class="form-control"  />--%>
                                        <textarea style="height:120px"  type="text" disabled="disabled" name="prueba" runat="server" class="form-control" id="txtArchivoAdjunto" />
                                    </div>
                                    <div class="form-group">
                                        <label for="disabledSelect">Documento Soporte:</label>
                                        <asp:FileUpload ID="fileUpload" runat="server" />
                                    </div>
                                </div>

                                <div>
                                    <asp:Button OnClick="btnGuardarEdicion_Click" ID="btnGuardarEdicion" runat="server" CssClass="btn btn-default" Text="Guardar" />  
                                    <input value="Cancelar" data-dismiss="modal" aria-hidden="true" type="button" id="btnCancelar" runat="server" class="btn btn-default" />
                                </div>
                            </div>
                      </div>
                    </div>
                </div>   
              </div>
              <div class="modal-footer">
                  
              </div>
        </div>
</asp:Content>

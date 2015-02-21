<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="EvaluacionPOA_.aspx.cs" Inherits="SIP.Formas.POA.EvaluacionPOA_" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Scripts/jquery-browser.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#linkPlaneacion").click(function () {
                $("#<%= divPlaneacion.ClientID %>").css("display", "block");
                $("#<%= divEjecucion.ClientID %>").css("display", "none");
                
                $('#linkEjecucion').removeClass('active');
                $('#linkPlaneacion').addClass('active');

            });


            $("#linkEjecucion").click(function () {
                $("#<%= divPlaneacion.ClientID %>").css("display", "none");
                $("#<%= divEjecucion.ClientID %>").css("display", "block");

                $('#linkPlaneacion').removeClass('active');
                $('#linkEjecucion').addClass('active');
            });

            $(function () {
                $('[data-toggle="popover"]').popover();
                $('[data-tooltip="tooltip"]').tooltip();
                
            })

            $('.popover').css("background-color", "red")

        });

        function fnc_GetFile() {
            try {
                var file="";
                //for IE
                if ($.browser.msie) {
                    var objFSO = new ActiveXObject("Scripting.FileSystemObject");
                    file = $("#<%= fileUpload.ClientID %>")[0].value;
                }
                //for FF, Safari, Opeara and Others
                else {
                    file = $("#<%= fileUpload.ClientID %>")[0].value;
                }

                return file;
               
            }
            catch (e) {
                
            }
        }

        function fnc_GuardarEdicion() {
            var nombreArchivo = fnc_GetFile();
            PageMethods.GuardarEdicion(nombreArchivo, fnc_ResponseGuardarEdicion)
        }


        function fnc_ResponseGuardarEdicion(response) {
            alert(response);
        }


        function fnc_AbrirCollapse() {

            var numeroCollapse=$("#<%= _numCollapse.ClientID %>").val();

            $("#collapse" + numeroCollapse).addClass("panel-collapse collapse in");

            switch (numeroCollapse) { //para el caso del acordeon de TIPO DE ADJUDICACION
                case "6":
                case "7":
                case "8":
                case "9":
                    $("#collapse5").addClass("panel-collapse collapse in");
                
            }

            //Mostrar la pestaña correspondiente de evaluacion, PLANEACION o EJECUCION
            var numGrid = $("#<%= _NumGrid.ClientID %>").val();
            if (numGrid > 10) {
                $("#<%= divPlaneacion.ClientID %>").css("display", "none");
                $("#<%= divEjecucion.ClientID %>").css("display", "block");
            }

        }


        function fnc_AbrirReporte() {

            var izq = (screen.width - 750) / 2
            var sup = (screen.height - 600) / 2
            var param = "";
            param = fnc_ArmarParamentros();
            url = $("#<%= _URLVisor.ClientID %>").val();
            var argumentos = "?c=" + 1 + param;
            url += argumentos;
            window.open(url, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=750,height=500,top=' + sup + ',left=' + izq);
        }


        function fnc_ArmarParamentros() {
            var p = "";

            var IDPoaDetalle = $("#<%= _IDPOADetalle.ClientID %>").val();

            p += "&p=" + IDPoaDetalle;
            return p;

        }


        function fnc_VerDetalleDoctos(idPregunta) {
            PageMethods.GetDetalleDocumentos(idPregunta, fnc_ResponseVerDetalleDoctos); //Se manda a llamar el metodo a C#
        }


        function fnc_ResponseVerDetalleDoctos(response) {

            var divDoctos = document.getElementById("divDoctosDetalle");
            var totalNodes = divDoctos.childNodes.length;

            if (totalNodes >= 1) {
                for (index = totalNodes - 1; index >= 0; index--) {
                    divDoctos.removeChild(divDoctos.childNodes[index]);
                }
            }

            if (response[0] == "") {

                if (response[1] != "N") {

                    for (index = 0; index < response[1].length; index++) {

                        var funcionDoc = "fnc_AbrirArchivo(" + response[1][index]["Id"] + ")";
                        var inputTag = document.createElement("div");
                        inputTag.id = "div" + index;

                        var html = "<table>";
                        html += "<tr>";

                        html += "<td>";
                        html += "<a href='#'>"
                        html += "<span id='doc" + index + "' onClick = " + funcionDoc + ">" + response[1][index]["NombreArchivo"] + "</span>";
                        html += "</a>";
                        html += "</td>";

                        //html += "<td>";
                        //html += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        //html += "</td>";

                        //html += "<td colspan=2>";
                        //html += "<a href='#'>"
                        //html += "<span id='btnE" + index + "' onClick = " + funcionEliminar + ">Eliminar</span>";
                        //html += "</a>";
                        //html += "</td>";

                        html += "</tr>";
                        html += "</table>";

                        inputTag.innerHTML = html; 
                        divDoctos.appendChild(inputTag);

                    }


                }
            }

            $("#modalDoctos").modal('show') //Se muestra el modal
        }


        function fnc_Edicion(idPregunta,numGrid) {
            var cadenaValores;
            var nombreGrid;
            var numeroCollapse;

            cadenaValores = fnc_ObtenerRadioChecks(numGrid);
            
            switch (numGrid) {
                //ETAPA DE PLANEACION
                case 1:
                    numeroCollapse = 1;
                    break;
                case 2:
                    numeroCollapse = 2;
                    break;
                case 3:
                    numeroCollapse = 3;
                    break;
                case 4:
                    numeroCollapse = 4;
                    break;
                case 5:
                    numeroCollapse = 6;
                    break;
                case 6:
                    numeroCollapse = 7;
                    break;
                case 7:
                    numeroCollapse = 8;
                    break;
                case 8:
                    numeroCollapse = 9;
                    break;
                case 9:
                    numeroCollapse = 10;
                    break;
                case 10:
                    numeroCollapse = 11;
                    break;

                //ETAPA DE EJECUCION
                case 11:
                    numeroCollapse = "1_2";
                    break;
                case 12:
                    numeroCollapse = "2_2";
                    break;
                case 13:
                    numeroCollapse = "3_2";
                    break;
                case 14:
                    numeroCollapse = "4_2";
                    break;
                case 15:
                    numeroCollapse = "5_2";
                    break;
                case 16:
                    numeroCollapse = "6_2";
                    break;
                case 17:
                    numeroCollapse = "7_2";
                    break;
                

            }

            $("#<%= _numCollapse.ClientID %>").val(numeroCollapse);

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
            <%--<%--$("#<%= txtArchivoAdjunto.ClientID %>").val(response[1]);--%>
            $("#<%= txtPregunta.ClientID %>").val(response[2]);

            var divDoctos = document.getElementById("divDoctos");
            var totalNodes = divDoctos.childNodes.length;


            if (totalNodes >= 1) {
                for (index = totalNodes - 1; index >= 0; index--) {
                    divDoctos.removeChild(divDoctos.childNodes[index]);
                }
            }

            if (response.length > 3) {

                for (index = 0; index < response[3].length; index++) {

                    var funcionDoc = "fnc_AbrirArchivo("+response[3][index]["Id"] + ")";

                    var funcionEliminar = "fnc_EliminarArchivo(" + response[3][index]["Id"] + "," + index + ")";

                    var inputTag = document.createElement("div");
                    inputTag.id = "div" + index;

                    var html = "<table>";
                    html += "<tr>";

                    html += "<td>";
                    html += "<a href='#'>"
                    html += "<span id='doc" + index + "' onClick = " + funcionDoc + ">" + response[3][index]["NombreArchivo"] + "</span>";
                    html += "</a>";
                    html += "</td>";
                    
                    html += "<td>";
                    html += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    html += "</td>";

                    html += "<td colspan=2>";
                    html += "<a href='#'>"
                    html += "<span id='btnE" + index + "' onClick = " + funcionEliminar + ">Eliminar</span>";
                    html += "</a>";
                    html += "</td>";

                    html += "</tr>";
                    html += "</table>";

                    inputTag.innerHTML = html; //"<span onClick = " + funcion + ">" + response[3][index]+"</span>";
                    divDoctos.appendChild(inputTag);

                }
            }


            $("#modalDatos").modal('show') //Se muestra el modal
        }

        function fnc_EliminarArchivo(idDocto, index) {

            $("#<%= _index.ClientID %>").val(index);

            PageMethods.EliminarArchivo(idDocto,fnc_ResponseEliminarArchivo)

        }

        function fnc_ResponseEliminarArchivo(response) {

            if (response == "")
            {
                var divDoctos = document.getElementById("divDoctos");
                var index = $("#<%= _index.ClientID %>").val();
                var divIndex = document.getElementById("div" + index);

                divDoctos.removeChild(divIndex);
            }
        }


        function fnc_GuardarChecks(numGrid) {
            var cadenaValores;
            var checkAprobado;
            var obsGeneral;
            cadenaValores = fnc_ObtenerRadioChecks(numGrid);
            checkAprobado = fnc_GetCheckAprobado(numGrid);
            obsGeneral = fnc_GetObservacionGeneral(numGrid);

            PageMethods.GuardarChecks(cadenaValores,checkAprobado,obsGeneral, fnc_ResponseGuardarChecks);
        }


        function fnc_ResponseGuardarChecks(response) {
            alert(response[0]);
            fnc_AbrirCollapse();
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
                //GRIDS DE LA ETAPA DE EVALUACION

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

                //GRIDS DE LA ETAPA DE EJECUCION
                case 11: //Control técnico financiero
                    grid = document.getElementById('<%=gridTecnicoFinanciero.ClientID %>'); //Se recupera el grid
                    break;
                case 12: //Bitácora electrónica - convencional
                    grid = document.getElementById('<%=gridBitacora.ClientID %>'); //Se recupera el grid
                    break;
                case 13: //Supervisión y estimaciones
                    grid = document.getElementById('<%=gridEstimaciones.ClientID %>'); //Se recupera el grid
                    break;
                case 14: //Convenios prefiniquitos
                    grid = document.getElementById('<%=gridConvenios.ClientID %>'); //Se recupera el grid
                    break;
                case 15: //Finiquito
                    grid = document.getElementById('<%=gridFiniquito.ClientID %>'); //Se recupera el grid
                    break;
                case 16: //Acta entrega recepción
                    grid = document.getElementById('<%=gridEntrega.ClientID %>'); //Se recupera el grid
                    break;
                case 17: //Documentación de gestión de recursos
                    grid = document.getElementById('<%=gridGestion.ClientID %>'); //Se recupera el grid
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
                //GRIDS DE LA ETAPA DE PLANEACION
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

                //GRIDS DE LA ETAPA DE EJECUCION
                case 11:
                    nombre = "gridTecnicoFinanciero";
                    break;
                case 12:
                    nombre = "gridBitacora";
                    break;
                case 13:
                    nombre = "gridEstimaciones";
                    break;
                case 14:
                    nombre = "gridConvenios";
                    break;
                case 15:
                    nombre = "gridFiniquito";
                    break;
                case 16:
                    nombre = "gridEntrega";
                    break;
                case 17:
                    nombre = "gridGestion";
                    break;
            }

            return nombre;

        }

        function fnc_GetCheckAprobado(numGrid) {

            var checked=false;

            switch (numGrid) {
                //GRIDS DE LA ETAPA DE PLANEACION

                case 1: //PLAN DE DESARROLLO VERCRUZANO
                    checked=$("#<%= chkAprobadoPD.ClientID %>").is(':checked');
                    break;
                case 2: //ANTEPROYECTO
                    checked = $("#<%= chkAprobadoAnteproyecto.ClientID %>").is(':checked');
                    break;
                case 3: //Fondo y programa
                    checked = $("#<%= chkAprobadoFondoPrograma.ClientID %>").is(':checked');
                    break;
                case 4: //Proyecto Ejecutivo y Proyecto Base
                    checked = $("#<%= chkAprobadoProyectoEjecutivo.ClientID %>").is(':checked');
                    break;
                case 5: //Adjudicacion Directa
                    checked = $("#<%= chkAprobadoAdjuDirecta.ClientID %>").is(':checked');
                    break;
                case 6: //Adjudicacion por excepcion de ley
                    checked = $("#<%= chkAprobadoExcepcion.ClientID %>").is(':checked');
                    break;
                case 7: //Invitacion a cuando menos tres personas
                    checked = $("#<%= chkAprobadoInvitacion.ClientID %>").is(':checked');
                    break;
                case 8: //Licitacion Pública
                    checked = $("#<%= chkAprobadoLicitacion.ClientID %>").is(':checked');
                    break;
                case 9: //Presupuesto Autorizado Contrato
                    checked = $("#<%= chkAprobadoPresupuesto.ClientID %>").is(':checked');
                    break;
                case 10: //Administración Directa
                    checked = $("#<%= chkAprobadoAdmin.ClientID %>").is(':checked');
                    break;

                //GRIDS DE LA ETAPA DE EJECUCION
                case 11: //Control técnico financiero
                    checked = $("#<%= chkAprobadoTecnicoFinanciero.ClientID %>").is(':checked');
                    break;
                case 12: //Bitácora electrónica - convencional
                    checked = $("#<%= chkAprobadoBitacora.ClientID %>").is(':checked');
                    break;
                case 13: //Supervisión y estimaciones
                    checked = $("#<%= chkAprobadoEstimaciones.ClientID %>").is(':checked');
                    break;
                case 14: //Convenios prefiniquitos
                    checked = $("#<%= chkAprobadoConvenios.ClientID %>").is(':checked');
                    break;
                case 15: //Finiquito
                    checked = $("#<%= chkAprobadoFiniquito.ClientID %>").is(':checked');
                    break;
                case 16: //Acta entrega recepción
                    checked = $("#<%= chkAprobadoEntrega.ClientID %>").is(':checked');
                    break;
                case 17: //Documentación de gestión de recursos
                    checked = $("#<%= chkAprobadoGestion.ClientID %>").is(':checked');
                    break;

            }

            return checked;
        }


        function fnc_GetObservacionGeneral(numGrid) {

            var observacion = "";

            switch (numGrid) {
                //GRIDS DE LA ETAPA DE PLANEACION

                case 1: //PLAN DE DESARROLLO VERCRUZANO
                    observacion = $("#<%= txtAprobadoPD.ClientID %>").val();
                    break;
                case 2: //ANTEPROYECTO
                    observacion = $("#<%= txtAnteproyecto.ClientID %>").val();
                    break;
                case 3: //Fondo y programa
                    observacion = $("#<%= txtFondoPrograma.ClientID %>").val();
                    break;
                case 4: //Proyecto Ejecutivo y Proyecto Base
                    observacion = $("#<%= txtProyectoEjecutivo.ClientID %>").val();
                    break;
                case 5: //Adjudicacion Directa
                    observacion = $("#<%= txtAdjuDirecta.ClientID %>").val();
                    break;
                case 6: //Adjudicacion por excepcion de ley
                    observacion = $("#<%= txtExcepcion.ClientID %>").val();
                    break;
                case 7: //Invitacion a cuando menos tres personas
                    observacion = $("#<%= txtInvitacion.ClientID %>").val();
                    break;
                case 8: //Licitacion Pública
                    observacion = $("#<%= txtLicitacion.ClientID %>").val();
                    break;
                case 9: //Presupuesto Autorizado Contrato
                    observacion = $("#<%= txtPresupuesto.ClientID %>").val();
                    break;
                case 10: //Administración Directa
                    observacion = $("#<%= txtAdmin.ClientID %>").val();
                    break;

                    //GRIDS DE LA ETAPA DE EJECUCION
                case 11: //Control técnico financiero
                    observacion = $("#<%= txtTecnicoFinanciero.ClientID %>").val();
                    break;
                case 12: //Bitácora electrónica - convencional
                    observacion = $("#<%= txtBitacora.ClientID %>").val();
                    break;
                case 13: //Supervisión y estimaciones
                    observacion = $("#<%= txtEstimaciones.ClientID %>").val();
                    break;
                case 14: //Convenios prefiniquitos
                    observacion = $("#<%= txtConvenios.ClientID %>").val();
                    break;
                case 15: //Finiquito
                    observacion = $("#<%= txtFiniquito.ClientID %>").val();
                    break;
                case 16: //Acta entrega recepción
                    observacion = $("#<%= txtEntrega.ClientID %>").val();
                    break;
                case 17: //Documentación de gestión de recursos
                    observacion = $("#<%= txtGestion.ClientID %>").val();
                    break;

            }

            return observacion;
        }


        //Functin que se encarga de abrir una nueva ventana con el contenido del archivo que se haya adjuntado en la pregunta
        //Creado por Rigoberto TS
        //15/10/2014
        function fnc_AbrirArchivo(id) {

            var ruta = '<%= ResolveClientUrl("~/AbrirDocto.aspx") %>';
            var izq = (screen.width - 750) / 2
            var sup = (screen.height - 600) / 2
            window.open(ruta + '?i=' + id, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=750,height=500,top=' + sup + ',left=' + izq);
        }

        

    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div class="container">
         <div class="page-header"">
            <h3>Evaluación de la OBRA (Planeación y Ejecución)</h3>
             <a href="<%=ResolveClientUrl("~/Formas/POA/POAAjustado.aspx") %>" ><span class="glyphicon glyphicon-arrow-left"></span> <strong>regresar listado de POA</strong></a>
        </div>

        <%--<div class="row">
            <div class="col-md-8"></div>
            <div class="col-md-4 text-right">
                
            </div>
        </div>   --%>

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
                        
                        <div class="row">
                            <div id="divMenu" runat="server">
                                <ul class="nav nav-tabs nav-justified panel-success">
                                    <li class="active"><a id="linkPlaneacion">Etapa Planeación</a></li>
                                    <li class="active"><a id="linkEjecucion">Etapa Ejecución</a></li>
                                </ul>
                            </div>
                        </div>


                        <div id="divPlaneacion" runat="server">
                             <div class="panel-group" id="accordion">
                                 <div class="panel panel-default">
                                 
                                 <div class="panel-heading">
                                    <h4 class="panel-title">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#collapse1">Plan de Desarrollo Estatal Urbano</a>
                                    </h4>
                                </div>
                                 <div id="collapse1" class="panel-collapse collapse">
                                    <div class="panel-body">
                                        <asp:GridView OnRowDataBound="gridPlanDesarrollo_RowDataBound" PageSize="1000" Height="25px" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridPlanDesarrollo" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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

                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                        </Columns>

                                        </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnPlanDesarrollo" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(1);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoPD" />
                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px"  type="text" name="prueba" runat="server" id="txtAprobadoPD" />

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
                                        <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridAnteproyecto_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridAnteproyecto" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                                
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                            </Columns>
                                            
                                        
                                        </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnAnteproyecto" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(2);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoAnteproyecto" />
                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtAnteproyecto" />
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
                                        <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridFondoPrograma_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridFondoPrograma" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                                
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                            </Columns>
                                            
                                        
                                        </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnFondoPrograma" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(3);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoFondoPrograma" />
                                                 <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtFondoPrograma" />
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
                                        <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridProyectoEjecutivo_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridProyectoEjecutivo" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                                
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>


                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                            </Columns>

                                        </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnProyecto" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(4);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoProyectoEjecutivo" />
                                                 <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtProyectoEjecutivo" />
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
                                                       <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridAdjuDirecta_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridAdjuDirecta" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acciones">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>

                                                                
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>


                                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                           
                                        
                                                           </asp:GridView>
                                                           <div>
                                                                <input value="Guardar" type="button" id="btnTipoAdju" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(5);" />
                                                                <label>&nbsp;Evaluación Aprobada</label>
                                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoAdjuDirecta" />
                                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                                <textarea style="height:100px; width:580px" type="text" name="prueba" runat="server" id="txtAdjuDirecta" />
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
                                                       <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridExcepcion_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridExcepcion" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acciones">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>

                                                                
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>


                                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                            
                                        
                                                           </asp:GridView>
                                                           <div>
                                                                <input value="Guardar" type="button" id="btnExcepcion" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(6);" />
                                                                <label>&nbsp;Evaluación Aprobada</label>
                                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoExcepcion" />
                                                               <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                                <textarea style="height:100px; width:570px" type="text" name="prueba" runat="server" id="txtExcepcion" />
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
                                                       <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridInvitacion_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridInvitacion" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acciones">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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

                                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                            
                                        
                                                           </asp:GridView>
                                                           <div>
                                                                <input value="Guardar" type="button" id="btnInvitacion" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(7);" />
                                                                <label>&nbsp;Evaluación Aprobada</label>
                                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoInvitacion" />
                                                               <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                                <textarea style="height:100px; width:580px" type="text" name="prueba" runat="server" id="txtInvitacion" />
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
                                                       <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridLicitacion_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridLicitacion" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Acciones">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>

                                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                                    <ItemTemplate>
                                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                            
                                        
                                                           </asp:GridView>
                                                           <div>
                                                                <input value="Guardar" type="button" id="btnLicitacion" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(8);" />
                                                                <label>&nbsp;Evaluación Aprobada</label>
                                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoLicitacion" />
                                                               <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                                <textarea style="height:100px; width:580px" type="text" name="prueba" runat="server" id="txtLicitacion" />
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
                                        <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridPresupuesto_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridPresupuesto" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                            </Columns>
                                            
                                        
                                        </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnPresupuesto" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(9);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoPresupuesto" />
                                                 <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtPresupuesto" />
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
                                        <asp:GridView  Height="25px" OnRowDataBound="gridAdmin_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridAdmin" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" PageSize="1000">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Acciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                    <ItemTemplate>
                                                        <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>--%>

                                            </Columns>
                                            
                                        
                                        </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnAdmin" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(10);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoAdmin" />
                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtAdmin" />
                                            </div>

                                    </div>
                                </div>

                                 </div>   
                             </div>
                        </div>

                        <div id="divEjecucion" runat="server" style="display:none">
                            <div class="panel-group" id="accordion2">
                                 <div class="panel panel-default">
                                 
                                     <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapse1_2">Control técnico financiero</a>
                                        </h4>
                                    </div>
                                     <div id="collapse1_2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView OnRowDataBound="gridTecnicoFinanciero_RowDataBound" PageSize="1000" Height="25px" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridTecnicoFinanciero" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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

                                                   <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                   <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                            </Columns>

                                            </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnTecnicoFinanciero" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(11);" />
                                            
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoTecnicoFinanciero" />
                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtTecnicoFinanciero" />
                                            </div>

                                        </div>
                                    </div>

                                     <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapse2_2">Bitácora electrónica - convencional</a>
                                        </h4>
                                    </div>
                                     <div id="collapse2_2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridBitacora_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridBitacora" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                </Columns>
                                            
                                        
                                            </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnBitacora" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(12);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoBitacora" />
                                                 <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtBitacora" />
                                            </div>

                                        </div>
                                    </div>

                                     <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapse3_2">Supervisión y estimaciones</a>
                                        </h4>
                                    </div>
                                     <div id="collapse3_2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridEstimaciones_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridEstimaciones" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                </Columns>
                                            
                                        
                                            </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnEstimaciones" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(13);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoEstimaciones" />
                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtEstimaciones" />
                                            </div>

                                        </div>
                                    </div>

                                     <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapse4_2">Convenios prefiniquitos</a>
                                        </h4>
                                    </div>
                                     <div id="collapse4_2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView Height="25px" PageSize="1000" OnRowDataBound="gridConvenios_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridConvenios" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                   <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                </Columns>

                                            </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnConvenios" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(14);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoConvenios" />
                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtConvenios" />
                                            </div>

                                        </div>
                                    </div>

                                     <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapse5_2">Finiquito</a>
                                        </h4>
                                    </div>
                                     <div id="collapse5_2" class="panel-collapse collapse">
                                        <div class="panel-body">

                                           <div class="panel-body">
                                                <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridFiniquito_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridFiniquito" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Acciones">
                                                            <ItemTemplate>
                                                                <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                       <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>--%>

                                                         <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                            <ItemTemplate>
                                                                <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                            <ItemTemplate>
                                                                <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                            <ItemTemplate>
                                                                <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                            <ItemTemplate>
                                                                <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>

                                                       <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                            <ItemTemplate>
                                                                <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                           
                                        
                                                </asp:GridView>
                                                <div>
                                                    <input value="Guardar" type="button" id="btnFiniquito" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(15);" />
                                                    <label>&nbsp;Evaluación Aprobada</label>
                                                    <input type="checkbox" value="false" runat="server" id="chkAprobadoFiniquito" />
                                                    <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                    <textarea style="height:100px; width:570px" type="text" name="prueba" runat="server" id="txtFiniquito" />
                                                </div>
                                        </div>

                                        </div>
                                    </div>

                                     <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapse6_2">Acta entrega recepción</a>
                                        </h4>
                                    </div>
                                     <div id="collapse6_2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView PageSize="1000" Height="25px" OnRowDataBound="gridEntrega_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridEntrega" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                </Columns>
                                            
                                        
                                            </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnEntrega" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(16);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoEntrega" />
                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtEntrega" />
                                            </div>

                                        </div>
                                    </div>

                                     <div class="panel-heading">
                                        <h4 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#accordion2" href="#collapse7_2">Documentación de gestión de recursos</a>
                                        </h4>
                                    </div>
                                     <div id="collapse7_2" class="panel-collapse collapse">
                                        <div class="panel-body">
                                            <asp:GridView  Height="25px" OnRowDataBound="gridGestion_RowDataBound" EnablePersistedSelection="true" CssClass="table" ShowHeaderWhenEmpty="true" ID="gridGestion" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" PageSize="1000">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Acciones">
                                                        <ItemTemplate>
                                                            <asp:ImageButton AutoPostBack="false" ID="imgDoctos" ToolTip="Ver doctos. adjuntos" runat="server" ImageUrl="~/img/Sub.png" />
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
                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Documento soporte" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArchivo" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="LOPySRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA1" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="RLOPSRM" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA2" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Contrato" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA3" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Admón. Directa" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" class="btn btn-default" id="btnA4" data-container="body" runat="server" data-toggle="popover" data-placement="right"></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Ver" SortExpression="NOAplica">
                                                        <ItemTemplate>
                                                            <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>--%>

                                                </Columns>
                                            
                                        
                                            </asp:GridView>

                                            <div>
                                                <input value="Guardar" type="button" id="btnGestion" runat="server" class="btn btn-default" onclick="fnc_GuardarChecks(17);" />
                                                <label>&nbsp;Evaluación Aprobada</label>
                                                <input type="checkbox" value="false" runat="server" id="chkAprobadoGestion" />
                                                <label for="disabledSelect">&nbsp;&nbsp;&nbsp;&nbsp;Observaciones Generales:</label>
                                                <textarea style="height:100px; width:610px" type="text" name="prueba" runat="server" id="txtGestion" />
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
        
    </div>
    <div runat="server" style="display:none">
            <input type="hidden" runat="server" id="_IDPlantilla" />
            <input type="hidden" runat="server" id="_IDPOADetalle" />
            <input type="hidden" runat="server" id="_IDPregunta" />
            <input type="hidden" runat="server" id="_CadValoresChecks" />
            <input type="hidden" runat="server" id="_NumGrid" />
            <input type="hidden" runat="server" id="_URLVisor" />
            <input type="hidden" runat="server" id="_numCollapse" />
            <input type="hidden" runat="server" id="_index" />
          
    </div>
    <div class="modal fade" id="modalDatos" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
         <div class="modal-dialog modal-sm">
                <div class="modal-content">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="modalTitle">Datos del Documento de Evaluación</h4>
                      </div>
                       <div class="modal-body">
                           <div class="form-group">
                                    <label>Documento de Evaluación</label>
                                    <textarea style="height:120px"  type="text" disabled="disabled" name="prueba" runat="server" class="form-control" id="txtPregunta" />
                                </div>
                                <div class="form-group">
                                    <label for="disabledSelect">Observaciones:</label>
                                    <textarea style="height:120px"  type="text" name="prueba" runat="server" class="form-control" id="txtObservacionesPregunta" />
                                </div>

                                <div class="form-group" >
                                    <label for="disabledSelect">Adjuntar Documento Soporte:</label>
                                    <asp:FileUpload ID="fileUpload" runat="server" />
                                </div>

                                <div class="form-group">
                                    <label>Documentos Soporte actuales:</label>
                                    <div id="divDoctos">
                                    </div>
                                </div>
                                 <div class="form-group">
                                     <%--<input type="button" value="Get File Size" onclick="GetFileSize('fileUpload');" />--%>
                                    <asp:Button ID="btnGuardarEdicion" OnClick="btnGuardarEdicion_Click" runat="server" CssClass="btn btn-default" Text="Guardar" />  
                                    <input value="Cancelar" data-dismiss="modal" aria-hidden="true" type="button" id="btnCancelar" runat="server" class="btn btn-default" />
                                </div>
                            </div>

                           

                       </div>
                </div>
    </div>
        

    <div class="modal fade" id="modalDoctos" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
         <div class="modal-dialog modal-sm">
                <div class="modal-content">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" id="modalTitleDoctos">Documentos adjuntos</h4>
                      </div>
                       <div class="modal-body">
                            <div class="form-group">
                                <div id="divDoctosDetalle">
                                </div>
                            </div>
                            <div class="form-group">
                                <input value="Aceptar" data-dismiss="modal" aria-hidden="true" type="button" id="btnAceptar" runat="server" class="btn btn-default" />
                            </div>
                        </div>
                    </div>
            </div>
    </div>
</asp:Content>

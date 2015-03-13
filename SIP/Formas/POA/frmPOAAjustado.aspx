<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPOAAjustado.aspx.cs" 

Inherits="SIP.Formas.POA.frmPOAAjustado" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   

      <style type="text/css">

        .edit-control 
        {
                 background: url('../../img/edit1.png') no-repeat center center;
                 cursor: pointer;
        }

        .delete-control 
        {
                 background: url('../../img/close.png') no-repeat center center;
                 cursor: pointer;
        }

    </style>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

    <div class="container">
       
        <div class="page-header"><h3><span id="titulopagina"></span></h3></div>
        
        <div id="divRecorrido">

            <div class="row">           
            <div class="col-md-1">
                <button type="button" class="btn btn-default" aria-label="Left Align" id="btnFiltro">
                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                </button>
            </div>
            <div class="col-md-7"></div>
            <div class="col-md-4 text-right">
                <a href="<%=ResolveClientUrl("~/Formas/POA/POAAjustadoFinanciamiento.aspx") %>" ><span class="glyphicon glyphicon-arrow-right"></span> <strong>Modificar financiamientos</strong></a>
            </div>
            </div>        
            <br />

            <div class="panel-footer alert alert-success" id="divResumen" style="display:block">
                <strong><span id="tituloresumen"></span></strong>
            </div>

            <div class="table-responsive" id="list"></div><!-- contenedor de lista -->

            <input type="button"  class="btn btn-default" id="btnNuevo" title="Agregar registro" value="Nuevo" />


        </div><!-- divRecorrido -->
                              
    
        <div id="divEdicion" class="panel-footer" style="display:none">
                   

            <div class="alert alert-danger" id="divErroresEdit" style="display:none">
                      <ul></ul>
            </div>
            
            <ul class="nav nav-tabs" role="tablist">
              <li class="active"><a href="#datosgenerales" role="tab" data-toggle="tab"><strong>Datos generales</strong></a></li>
              <li><a href="#planveracruzanodesarrollo" role="tab" data-toggle="tab"><strong>Plan Veracruzano de Desarrollo</strong></a></li>        
            </ul>

            <div class="tab-content">
                               
                
                <div class="tab-pane active" id="datosgenerales">

                     <div class="row">

                     <br /> 

                     <div class="col-md-4">

                          <div class="form-group">
                               <label for="Numero">Número</label>
                             <div>
                                <input type="text" class="input-sm required form-control" id="txtNumero" style="text-align: left; align-items:flex-start" autocomplete="off" disabled="disabled"/>                           
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="Descripcion">Descripción</label>
                             <div>
                                <textarea id="txtDescripcion" class="input-sm required form-control" style="text-align: left; align-items:flex-start" rows="3" autofocus></textarea>
                            </div>
                          </div>

                         <div class="form-group">
                             <label for="ddlMunicipio">Municipio</label>
                             <div>
                                <select class="form-control" id="ddlMunicipio"></select>
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="ddlLocalidad">Localidad</label>
                             <div>
                                <select class="form-control" id="ddlLocalidad"></select>
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="ddlCriterioPriorizacion">Criterio de priorización</label>
                             <div>
                                <select class="form-control" id="ddlCriterioPriorizacion"></select>
                             </div>
                         </div>
                     

                          <div style="display:none" id="divDatosConvenio">

                              <div class="form-group">
                                 <label for="NombreConvenio">Nombre del convenio</label>
                                 <div>
                                    <textarea id="txtNombreConvenio" class="input-sm required form-control" style="text-align: left; align-items:flex-start" rows="2" ></textarea>
                                 </div>
                              </div>    

                          </div><!--divDatosConvenio-->

                       

                     </div>

                     <div class="col-md-4">


                         <div class="form-group">
                             <label for="ddlPrograma">Programa</label>
                             <div>
                                <select class="form-control" id="ddlPrograma"></select>
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="ddlSubprograma">SubPrograma</label>
                             <div>
                                <select class="form-control" id="ddlSubprograma"></select>
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="ddlSubsubprograma">SubSubPrograma</label>
                             <div>
                                <select class="form-control" id="ddlSubsubprograma"></select>
                             </div>
                         </div>

                         <div class="form-group">
                             <label for="ddlUnidadMedida">Unidad de medida</label>
                             <div>
                                <select class="form-control" id="ddlUnidadMedida"></select>
                             </div>
                         </div>


                         <div class="form-group">
                               <label for="CantidadUnidades">Cantidad de unidades</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtCantidadUnidades" style="text-align: left; align-items:flex-start" data-v-min="0" data-m-dec="0"/>
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="NumeroBeneficiarios">Número de beneficiarios</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtNumeroBeneficiarios" style="text-align: left; align-items:flex-start" data-v-min="0" data-m-dec="0"/>
                            </div>
                          </div>                                                   

                         <div class="form-group">
                               <label for="Empleos">Empleos</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtEmpleos" style="text-align: left; align-items:flex-start" data-v-min="0" data-m-dec="0"/>
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="Jornales">Jornales</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtJornales" style="text-align: left; align-items:flex-start" data-v-min="0" data-m-dec="0"/>
                            </div>
                          </div>


                      </div>

                     <div class="col-md-4">
                                                

                         <div class="form-group">
                             <label for="ddlSituacionObra">Situación</label>
                             <div>
                                <select class="form-control" id="ddlSituacionObra" disabled="disabled"></select>
                             </div>
                         </div>

                     

                         <div class="form-group">
                               <label for="txtImporteTotal">Costo total</label>
                             <div class="input-group">
                                <span class="input-group-addon">$</span>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtImporteTotal" style="text-align: left; align-items:flex-start" disabled="disabled" />
                            </div>
                          </div>

                         <div style="display:block" id="divDatosObraAnterior">

                              <div class="form-group">
                                <label for="Numero">Número anterior</label>
                                <div>
                                    <input type="text" class="input-sm required form-control" id="txtNumeroAnterior" style="text-align: left; align-items:flex-start" autocomplete="off" disabled="disabled" />                           
                                </div>
                              </div>

                              <div class="form-group">
                                   <label for="txtImporteLiberado">Costo liberado en ejercicios anteriores</label>
                                 <div class="input-group">
                                    <span class="input-group-addon">$</span>
                                    <input type="text" class="input-sm required form-control campoNumerico" id="txtImporteLiberadoEjerciciosAnteriores" style="text-align: left; align-items:flex-start" disabled="disabled" autocomplete="off" />
                                </div>
                              </div>

                          </div><!--divDatosObraAnterior-->


                         <div class="form-group">
                               <label for="txtPresupuestoEjercicio">Presupuesto del ejercicio</label>
                             <div class="input-group">
                                <span class="input-group-addon">$</span>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtPresupuestoEjercicio" style="text-align: left; align-items:flex-start" disabled="disabled" />
                            </div>
                          </div>

                         <div class="form-group">
                             <label for="ddlModalidad">Modalidad de ejecución</label>
                             <div>
                                <select class="form-control" id="ddlModalidad"></select>
                             </div>
                         </div>



                         <div class="form-group">
                               <label for="Observaciones">Observaciones</label>
                             <div>
                                <textarea id="txtObservaciones" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="4"></textarea>
                            </div>
                          </div>

                     </div>
                

                    </div>

                </div><!--tab datosgenerales-->

                <div class="tab-pane" id="planveracruzanodesarrollo">
                    
                     <div class="row">

                        <br />

                        <div class="col-md-8">

                            <div class="form-group">
                               <label for="ddlFuncionalidad">Funcionalidad</label>
                                <div>
                                    <div class="col-md-2">
                                        <input type="text" class="form-control" id="txtClaveFuncionalidad" disabled="disabled"/>                           
                                    </div> 
                                   <div>                                  
                                      <select class="form-control" id="ddlFuncionalidad"></select>
                                   </div>
                                </div>                               
                            </div>

                            <div class="form-group">
                               <label for="ddlEje">Eje</label>
                                <div>
                                    <div class="col-md-2">
                                        <input type="text" class="form-control" id="txtClaveEje" disabled="disabled"/>                           
                                    </div> 
                                   <div>                                  
                                      <select class="form-control" id="ddlEje"></select>
                                   </div>
                                </div>                               
                            </div>

                            <div class="form-group">
                               <label for="ddlPlanSectorial">Plan sectorial</label>
                                <div>
                                    <div class="col-md-2">
                                        <input type="text" class="form-control" id="txtClavePlanSectorial" disabled="disabled"/>                           
                                    </div> 
                                   <div>                                  
                                      <select class="form-control" id="ddlPlanSectorial"></select>
                                   </div>
                                </div>                               
                            </div>

                            <div class="form-group">
                               <label for="ddlModalidadPVD">Modalidad</label>
                                <div>
                                    <div class="col-md-2">
                                        <input type="text" class="form-control" id="txtClaveModalidad" disabled="disabled"/>                           
                                    </div> 
                                   <div>                                  
                                      <select class="form-control" id="ddlModalidadPVD"></select>
                                   </div>
                                </div>                               
                            </div>

                            <div class="form-group">
                               <label for="ddlProgramaPVD">Programa</label>
                                <div>
                                    <div class="col-md-2">
                                        <input type="text" class="form-control" id="txtClaveProgramaPVD" disabled="disabled"/>                           
                                    </div> 
                                   <div>                                  
                                      <select class="form-control" id="ddlProgramaPVD"></select>
                                   </div>
                                </div>                               
                            </div>

                            <div class="form-group">
                               <label for="ddlGrupoBeneficiario">Grupo de beneficiarios</label>
                                <div>
                                    <div class="col-md-2">
                                        <input type="text" class="form-control" id="txtClaveGrupoBeneficiario" disabled="disabled"/>                           
                                    </div> 
                                   <div>                                  
                                      <select class="form-control" id="ddlGrupoBeneficiario"></select>
                                   </div>
                                </div>                               
                            </div>
                         
                                                            
                        </div><!--col-md-8-->
                         
                        <div class="col-md-4"></div>                                            
                
                    </div><!--row-->


                </div><!--planveracruzanodesarrollo--> 
                              
            </div>

            <hr />
            <button type="button" class="btn btn-default" id="btnSaveToAdd" style="display:none">Guardar</button>
            <button type="button" class="btn btn-default" id="btnSaveToUpdate" style="display:none">Guardar</button>                         
            <button type="button" class="btn btn-default" id="btnCancelar">Cancelar</button>                           
                     

       </div><!--divEdicion-->

    </div><!--div class="container"-->

    <div class="modal fade" id="modalBorrar">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header alert alert-success">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title">Confirmar operación</h4>
          </div>
          <div class="modal-body">

              <div class="alert alert-danger" id="divErroresDelete" style="display:none">
                  <ul></ul>
              </div>

              ¿Desea borrar realmente este registro?
            
          </div>
          <div class="modal-footer">
                            
              <button type="button" class="btn btn-default" id="btnDelete">Borrar</button>                                
              <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                          
          </div>
        </div><!-- modal-content -->
      </div><!-- modal-dialog -->
    </div>  <!-- modal borrar -->
   

    <div class="modal fade" id="modalFiltro">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">

              <div class="modal-header alert alert-success">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Filtrar información</h4>
              </div>
              <div class="modal-body">


                         <div class="form-group">
                             <label for="Numero">Número de obra o acción</label>
                             <div>                                
                                 <input type="text" class="input-sm required form-control" id="txtFiltroNumeroObraAccion"  autofocus="autofocus" autocomplete="off" />                           
                             </div>                           
                          </div>


                          <div class="form-group">
                             <label for="Descripcion">Descripción</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroDescripcion"  autocomplete="off" />                           
                             </div>
                          </div>

                          <div class="form-group">
                             <label for="UnidadPresupuestal">Unidad presupuestal</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroUnidadPresupuestal"  autocomplete="off" />                           
                             </div>
                          </div>
                 
                          <div class="form-group">
                             <label for="Municipio">Municipio</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroMunicipio"  autocomplete="off" />                           
                             </div>
                          </div>
                  
                         <div class="form-group">
                             <label for="Localidad">Localidad</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroLocalidad"  autocomplete="off" />                           
                             </div>
                         </div>
                  
                         <div class="form-group">
                             <label for="Contratista">Contratista</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroContratista"  autocomplete="off" />                           
                             </div>
                         </div>
                  
                        <div class="form-group">
                             <label for="Fondos">Fondos</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroFondos"  autocomplete="off" />                           
                             </div>
                        </div> 
                     


                  </div><!-- modal-body -->
              <div class="modal-footer">
                            
                  <button type="button" class="btn btn-default" id="btnAplicarFiltro" >Filtrar</button>                                           
                  <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                          
              </div>

            </div><!-- modal-content -->
          </div><!-- modal-dialog -->
    </div> <!-- modal filtro -->

     
    <script type="text/javascript">


        $(document).ready(function ()
        {             

            try
            {
                $('.campoNumerico').autoNumeric('init');
            }
            catch (err)
            {           
                alert(err.message);
            }

            GetTituloPagina();
            GetUnidadesPresupuestales();
            GetMunicipios();
            GetLocalidades();
            GetCriteriosPriorizacion();
            GetAperturaProgramatica();
            GetUnidadesMedida();
            GetSituacionesObra();
            GetModalidadesEjecucion();
            GetFondos();
            GetFuncionalidades();
            GetEjes();
            GetPlanesSectoriales();
            GetModalidades();
            GetProgramasPVD();
            GetGrupoBeneficiarios();


            var WhereNumero = '%';
            var WhereDescripcion = '%';
            var WhereMunicipio = '%';
            var WhereLocalidad = '%';
            var WhereUnidadPresupuestal = '%'            
            var WhereContratista = '%'
            var WhereFondos = '%'
            var WherePresupuesto='%'


            GetList(WhereNumero, WhereDescripcion, WhereMunicipio, WhereLocalidad, WhereUnidadPresupuestal, WhereContratista, WhereFondos, WherePresupuesto);
            
            $("#ddlMunicipio").change(function (e)
            {               

                $("#ddlLocalidad").empty();
                $("#ddlLocalidad").prop("disabled", true);

                var valorseleccionado = $("#ddlMunicipio option:selected").val();

                var listlocalidades = JSON.parse(sessionStorage.getItem("catalogoLocalidades"));

                var listlocalidadesmunicipio = $linq(listlocalidades).where(function (x) { return x.MunicipioId == parseInt(valorseleccionado); }).toArray();

                if (listlocalidadesmunicipio.length > 0)
                {
                    $('#ddlLocalidad').append($("<option>").val(0).text('Seleccione...'));

                    $.map(listlocalidadesmunicipio, function (n) {
                        $('#ddlLocalidad').append($("<option>").val(n.Id).text(n.Nombre));
                    });

                    $("#ddlLocalidad").prop("disabled", false);

                }
            });
            
            $("#ddlCriterioPriorizacion").change(function (e) {


                var valorseleccionado = $("#ddlCriterioPriorizacion option:selected").val();

                 switch (valorseleccionado)
                 {
                    case "2":
                        
                        $("#divDatosConvenio").css("display", "block");
                        break;

                    default:

                        $("#divDatosConvenio").css("display", "none");
                        break;
                }


             });

            $("#ddlSituacionObra").change(function (e) {

                var valorseleccionado = $("#ddlSituacionObra option:selected").val();
              
                switch (valorseleccionado)
                {
                     case "0":
                         $("#divDatosObraAnterior").css("display", "none");
                         $("#txtNumeroAnterior").val("");
                         $("#txtImporteLiberadoEjerciciosAnteriores").val("");
                         break;
                     case "1":
                         $("#divDatosObraAnterior").css("display", "none");
                         $("#txtNumeroAnterior").val("");
                         $("#txtImporteLiberadoEjerciciosAnteriores").val("");
                         break;
                     default:
                         $("#divDatosObraAnterior").css("display", "block");
                         break;
                 }


            });

            $('#btnFiltro').click(function () {
                $('#modalFiltro').modal({ backdrop: 'static', show: true });
            });

            $('#btnAplicarFiltro').click(function () {

                var WhereNumero = '%';
                var WhereDescripcion = '%';
                var WhereMunicipio = '%';
                var WhereLocalidad = '%';
                var WhereUnidadPresupuestal = '%';
                var WhereContratista = '%';
                var WhereFondos = '%';
                var WherePresupuesto = '%';
                
                
                var WhereNumero = (!$('#txtFiltroNumeroObraAcion').val()) ? '%' : '%' + $('#txtFiltroNumeroObraAcion').val() + '%';
                var WhereDescripcion = (!$('#txtFiltroDescripcion').val()) ? '%' : '%' + $('#txtFiltroDescripcion').val() + '%';
                var WhereMunicipio = (!$('#txtFiltroMunicipio').val()) ? '%' : '%' + $('#txtFiltroMunicipio').val() + '%';
                var WhereLocalidad = (!$('#txtFiltroLocalidad').val()) ? '%' : '%' + $('#txtFiltroLocalidad').val() + '%';
                var WhereUnidadPresupuestal = (!$('#txtFiltroUnidadPresupuestal').val()) ? '%' : '%' + $('#txtFiltroUnidadPresupuestal').val() + '%';                
                var WhereContratista = (!$('#txtFiltroContratista').val()) ? '%' : '%' + $('#txtFiltroContratista').val() + '%';
                var WhereFondos = (!$('#txtFiltroFondos').val()) ? '%' : '%' + $('#txtFiltroFondos').val() + '%';
              

                GetList(WhereNumero, WhereDescripcion, WhereMunicipio, WhereLocalidad, WhereUnidadPresupuestal, WhereContratista, WhereFondos, WherePresupuesto);
                
                $('#modalFiltro').modal('hide');


            });//$(#btnAplicarFiltro)
            
            $('#list').on('click', '.edit-control.btn', function () {

                var rowId = $(this).data("rowid"); 
                BeforeEditRecord(rowId);

            });

            $('#list').on('click', '.delete-control.btn', function () {

                var rowId = $(this).data("rowid");
                BeforeDeleteRecord(rowId);

            });

            $('#btnCancelar').click(function () {
                
                $('#divEdicion').hide();
                $('#divRecorrido').show();
                
            });

            $("#ddlFiltroUnidadPresupuestal").change(function (e) {

                var unidadpresupuestalId = $("#ddlFiltroUnidadPresupuestal option:selected").val();
                
                var listadeptos = JSON.parse(sessionStorage.getItem("catalogoUnidadesPresupuestales"));

                var listsubdeptos = $linq(listadeptos).where(function (x) { return x.ParentId == parseInt(unidadpresupuestalId); }).toArray();

                if (listsubdeptos.length > 0) {
                   
                    $("#ddlFiltroSubUnidadPresupuestal").empty();

                    $('#ddlFiltroSubUnidadPresupuestal').append($("<option>").val(-1).text("Seleccione un elemento..."));

                    $.map(listsubdeptos, function (n) {
                        $('#ddlFiltroSubUnidadPresupuestal').append($("<option>").val(n.Id).text(n.Nombre));
                    });

                    $("#divFiltroSubUnidadPresupuestal").show();

                }
                else
                {
                    $("#divFiltroSubUnidadPresupuestal").hide();
                }                
                
            });
          
            $("#ddlPrograma").change(SincronizarCombosAperturaProgramatica);

            $("#ddlSubprograma").change(SincronizarCombosAperturaProgramatica);

            $("#ddlFuncionalidad").change(function () {             
                $("#txtClaveFuncionalidad").val($(this).find('option:selected').data('clave'));
            });

            $('#btnNuevo').click(function () {
                BeforeAddRecord();
            });

            $('#btnSaveToAdd').click(function () {

                if (fnc_Validar())
                {
                    AddRecord();
                }
            });

            $('#btnSaveToUpdate').click(function () {

                if (fnc_Validar())
                {
                    UpdateRecord();
                }                
            });

            $('#btnDelete').click(function () {
                DeleteRecord();
            });
            

        }); //$(document).ready


        function BeforeAddRecord() {
                                        

            $('#txtNumero').val('');
            $('#txtDescripcion').val('');
            $("#ddlMunicipio option[value=0]").prop("selected", true);
            $("#ddlLocalidad").empty();
            $("#ddlLocalidad").prop("disabled", true);
            $('#ddlLocalidad').append($("<option>").val(0).text("Seleccione..."));

            $("#ddlCriterioPriorizacion option[value=0]").prop("selected", true);            
            $('#txtNombreConvenio').val('');
            $("#divDatosConvenio").hide();

            $("#ddlPrograma option[value=0]").prop("selected", true);

            $("#ddlSubprograma").empty();
            $("#ddlSubprograma").prop("disabled", true);
            $('#ddlSubprograma').append($("<option>").val(0).text("Seleccione..."));

            $("#ddlSubsubprograma").empty();
            $("#ddlSubsubprograma").prop("disabled", true);
            $('#ddlSubsubprograma').append($("<option>").val(0).text("Seleccione..."));
                       
            $("#ddlUnidadMedida option[value=0]").prop("selected", true);
          
            $('#txtCantidadUnidades').val('');
            $('#txtNumeroBeneficiarios').val('');
            $('#txtEmpleos').val('');
            $('#txtJornales').val('');

            $("#ddlSituacionObra option[value=0]").prop("selected", true);
            $("#ddlSituacionObra").prop("disabled", false); //se permiten otras situaciones, además de nueva
            $("#divDatosObraAnterior").hide();

            $('#txtImporteTotal').val('');
            $('#txtNumeroAnterior').val('');
            $('#txtImporteLiberadoEjerciciosAnteriores').val('');
            $('#txtPresupuestoEjercicio').val('');

            $("#ddlModalidad option[value=0]").prop("selected", true);           

            $("#ddlFuncionalidad option[value=0]").prop("selected", true);
            $("#ddlEje option[value=0]").prop("selected", true);
            $("#ddlPlanSectorial option[value=0]").prop("selected", true);
            $("#ddlModalidadPVD option[value=0]").prop("selected", true);
            $("#ddlProgramaPVD option[value=0]").prop("selected", true);
            $("#ddlGrupoBeneficiario option[value=0]").prop("selected", true);

            $('#btnSaveToUpdate').hide();
            $('#btnSaveToAdd').show();            

            $('#divErroresEdit').hide();
            $('#divRecorrido').hide();
            $('#divEdicion').show();
            
        }
        
        function BeforeEditRecord(rowId)
        {                      

            sessionStorage.setItem("rowId", rowId);

            GetRecord(rowId);
           
            $('#btnSaveToAdd').hide();
            $('#btnSaveToUpdate').show();
           
            $('#divErroresEdit').hide();
            $('#divRecorrido').hide();
            $('#divEdicion').show();           

        }

        function BeforeDeleteRecord(rowId)
        {
            sessionStorage.setItem("rowId", rowId);
            $('#divErroresDelete').hide();
            $('#modalBorrar').modal({ backdrop: 'static', show: true });
        }

        function GetRecord(rowId) {

           
            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetRecord",
                    data: JSON.stringify({ id: rowId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                       
                        $('#txtNumero').val(response.d.Numero);
                        $('#txtDescripcion').val(response.d.Descripcion);
                        $("#ddlMunicipio option[value=" + response.d.MunicipioId + "]").prop("selected", true);                                             

                        //Sincronizar combo de localidades
                        //--------------------------------------------------------
                        var listlocalidades = JSON.parse(sessionStorage.getItem("catalogoLocalidades"));
                        var listlocalidadesmunicipio = $linq(listlocalidades).where(function (x) { return x.MunicipioId == parseInt(response.d.MunicipioId); }).toArray();

                        $('#ddlLocalidad').empty();
                        $('#ddlLocalidad').append($("<option>").val(0).text('Seleccione...'));

                        if (listlocalidadesmunicipio.length == 0)
                        {
                            $("#ddlLocalidad").prop("disabled", true);
                        }
                        else
                        {                          

                            $.map(listlocalidadesmunicipio, function (n) {
                                $('#ddlLocalidad').append($("<option>").val(n.Id).text(n.Nombre));
                            });

                            $("#ddlLocalidad").prop("disabled", false);

                        }

                        $("#ddlLocalidad option[value=" + response.d.LocalidadId + "]").prop("selected", true);

                        //Sincronizar combo de localidades
                        

                        $("#ddlCriterioPriorizacion option[value=" + response.d.CriterioPriorizacionId + "]").prop("selected", true);
                        $('#txtNombreConvenio').val(response.d.Convenio);
                        
                        fnc_ocultarDivDatosConvenio();                        

                        SincronizarCombosAperturaProgramaticaGetRecord(response.d.ProgramaId, response.d.SubprogramaId, response.d.SubsubprogramaId);
                     
                        $("#ddlUnidadMedida option[value=" + response.d.UnidadMedidaId + "]").prop("selected", true);
                                              
                       
                        $('#txtCantidadUnidades').autoNumeric('set', response.d.CantidadUnidades);
                        $('#txtNumeroBeneficiarios').autoNumeric('set', response.d.NumeroBeneficiarios);
                        $('#txtEmpleos').autoNumeric('set', response.d.Empleos);
                        $('#txtJornales').autoNumeric('set', response.d.Jornales);
                       
                        $("#ddlSituacionObra option[value=" + response.d.SituacionObraId + "]").prop("selected", true);
                                             
                        $('#txtImporteTotal').autoNumeric('set', response.d.CostoTotal);
                        $('#txtNumeroAnterior').val(response.d.NumeroAnterior);                       
                        $('#txtImporteLiberadoEjerciciosAnteriores').autoNumeric('set', response.d.ImporteLiberadoEjerciciosAnteriores);
                        $('#txtPresupuestoEjercicio').autoNumeric('set', response.d.PresupuestoEjercicio);

                        $("#ddlModalidad option[value=" + response.d.ModalidadId + "]").prop("selected", true);

                        fnc_ocultarDivObraAnterior();

                        $("#ddlFuncionalidad option[value=" + response.d.FuncionalidadId + "]").prop("selected", true);

                        $("#txtClaveFuncionalidad").val($("#ddlFuncionalidad").find('option:selected').data('clave'));


                        $("#ddlEje option[value=" + response.d.EjeId + "]").prop("selected", true);
                        $("#ddlPlanSectorial option[value=" + response.d.PlanSectorialId + "]").prop("selected", true);
                        $("#ddlModalidadPVD option[value=" + response.d.ModalidadPVDId + "]").prop("selected", true);
                        $("#ddlProgramaPVD option[value=" + response.d.ProgramaPVDId + "]").prop("selected", true);
                        $("#ddlGrupoBeneficiario option[value=" + response.d.GrupoBeneficiarioId + "]").prop("selected", true);
                       
                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function AddRecord() {

            alert("Ingresamos a AddRecord");

            var numero = $('#txtNumero').val();
            var descripcion = $('#txtDescripcion').val();
            var municipioId = $("#ddlMunicipio option:selected").val();
            var localidadId = $("#ddlLocalidad option:selected").val();
            var criterioPriorizacionId = $("#ddlCriterioPriorizacion option:selected").val();
            var convenio = $('#txtNombreConvenio').val();

            var programaId = $("#ddlPrograma option:selected").val();
            var subprogramaId = $("#ddlSubprograma option:selected").val();
            var subsubprogramaId = $("#ddlSubsubprograma option:selected").val();
            var unidadmedidaId = $("#ddlUnidadMedida option:selected").val();

            var cantidadunidades = $('#txtCantidadUnidades').val();
            var numerobeneficiarios = $('#txtNumeroBeneficiarios').val();
            var empleos = $('#txtEmpleos').val();
            var jornales = $('#txtJornales').val();

            var situacionobraId = $("#ddlSituacionObra option:selected").val();

            var importetotal = $('#txtImporteTotal').val();
            var numeroanterior = $('#txtNumeroAnterior').val();
            var importeliberadoejerciciosanteriores = $('#txtImporteLiberadoEjerciciosAnteriores').val();
            var presupuestoejercicio = $('#txtPresupuestoEjercicio').val();

            var modalidadId = $("#ddlModalidad option:selected").val();

            var funcionalidadId = $("#ddlFuncionalidad option:selected").val();
            var ejeId = $("#ddlEje option:selected").val();
            var plansectorialId = $("#ddlPlanSectorial option:selected").val();
            var modalidadpvdId = $("#ddlModalidadPVD option:selected").val();
            var programapvdId = $("#ddlProgramaPVD option:selected").val();
            var grupobeneficiarioId = $("#ddlGrupoBeneficiario option:selected").val();

            var datos = {
                Id: -1,
                Numero: numero,
                Descripcion: descripcion,
                MunicipioId: municipioId,
                LocalidadId: localidadId,
                CriterioPriorizacionId: criterioPriorizacionId,
                Convenio: convenio,
                AperturaProgramaticaId: subsubprogramaId,
                UnidadMedidaId: unidadmedidaId,
                CantidadUnidades: cantidadunidades.replace(/,/g, ''),
                NumeroBeneficiarios: numerobeneficiarios.replace(/,/g, ''),
                Empleos: empleos.replace(/,/g, ''),
                Jornales: jornales.replace(/,/g, ''),
                SituacionObraId: situacionobraId,
                NumeroAnterior: numeroanterior,
                ImporteLiberadoEjerciciosAnteriores: ($.isNumeric(importeliberadoejerciciosanteriores))?importeliberadoejerciciosanteriores:0,
                ModalidadEjecucionId: modalidadId,
                FuncionalidadId: funcionalidadId,
                EjeId: ejeId,
                PlanSectorialId: plansectorialId,
                ModalidadPVDId: modalidadpvdId,
                ProgramaPVDId: programapvdId,
                GrupoBeneficiarioId: grupobeneficiarioId
            };
           

            $.ajax({
                type: "POST",
                url: "frmPOAAjustado.aspx/AddRecord",                
                data: JSON.stringify({ registro: datos }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.OK)
                    {
                        $('#divEdicion').hide();
                        $('#divRecorrido').show();
                    }
                    else
                    {

                        $('#divErroresEdit ul:first').empty();

                        $.map(response.d.Errors, function (n) {
                            $('#divErroresEdit ul:first').append($("<li>").text(n));
                        });

                        $('#divErroresEdit').show();
                    }

                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresEdit ul:first').empty();
                    $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                    $('#divErroresEdit').show();

                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresEdit ul:first').empty();
                    $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                    $('#divErroresEdit').show();
                }
            });
        }

        function UpdateRecord() {
                       

            var rowId = sessionStorage.getItem("rowId");
          
            var numero=$('#txtNumero').val();
            var descripcion=$('#txtDescripcion').val();
            var municipioId = $("#ddlMunicipio option:selected").val();
            var localidadId = $("#ddlLocalidad option:selected").val();
            var criterioPriorizacionId = $("#ddlCriterioPriorizacion option:selected").val();
            var convenio=$('#txtNombreConvenio').val();
           
            var programaId = $("#ddlPrograma option:selected").val();
            var subprogramaId = $("#ddlSubprograma option:selected").val();
            var subsubprogramaId = $("#ddlSubsubprograma option:selected").val();
            var unidadmedidaId = $("#ddlUnidadMedida option:selected").val();

            var cantidadunidades=$('#txtCantidadUnidades').val();
            var numerobeneficiarios=$('#txtNumeroBeneficiarios').val();
            var empleos=$('#txtEmpleos').val();
            var jornales=$('#txtJornales').val();

            var situacionobraId=$("#ddlSituacionObra option:selected").val();

            var importetotal=$('#txtImporteTotal').val();
            var numeroanterior=$('#txtNumeroAnterior').val();
            var importeliberadoejerciciosanteriores=$('#txtImporteLiberadoEjerciciosAnteriores').val();
            var presupuestoejercicio=$('#txtPresupuestoEjercicio').val();

            var modalidadId = $("#ddlModalidad option:selected").val();                        

            var funcionalidadId = $("#ddlFuncionalidad option:selected").val();
            var ejeId = $("#ddlEje option:selected").val();
            var plansectorialId = $("#ddlPlanSectorial option:selected").val();
            var modalidadpvdId = $("#ddlModalidadPVD option:selected").val();
            var programapvdId = $("#ddlProgramaPVD option:selected").val();
            var grupobeneficiarioId = $("#ddlGrupoBeneficiario option:selected").val();

            var datos = {
                Id: rowId,
                Numero: numero,
                Descripcion: descripcion,
                MunicipioId: municipioId,
                LocalidadId: localidadId,
                CriterioPriorizacionId: criterioPriorizacionId,
                Convenio: convenio,
                AperturaProgramaticaId: subsubprogramaId,
                UnidadMedidaId: unidadmedidaId,
                CantidadUnidades: cantidadunidades.replace(/,/g, ''),
                NumeroBeneficiarios: numerobeneficiarios.replace(/,/g, ''),
                Empleos: empleos.replace(/,/g, ''),
                Jornales: jornales.replace(/,/g, ''),
                SituacionObraId: situacionobraId,
                NumeroAnterior: numeroanterior,
                ImporteLiberadoEjerciciosAnteriores: importeliberadoejerciciosanteriores,
                ModalidadEjecucionId: modalidadId,
                FuncionalidadId: funcionalidadId,
                EjeId: ejeId,
                PlanSectorialId: plansectorialId,
                ModalidadPVDId: modalidadpvdId,
                ProgramaPVDId: programapvdId,
                GrupoBeneficiarioId: grupobeneficiarioId
            };


            $.ajax({
                type: "POST",
                url: "frmPOAAjustado.aspx/UpdateRecord",
                data: JSON.stringify({ registro: datos }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.OK) {

                        GetList('%', '%', '%', '%', '%', '%', '%', '%', '%');
                        
                        $('#divEdicion').hide();
                        $('#divRecorrido').show();

                    }
                    else
                    {

                        $('#divErroresEdit ul:first').empty();

                        $.map(response.d.Errors, function (n) {
                            $('#divErroresEdit ul:first').append($("<li>").text(n));
                        });

                        $('#divErroresEdit').show();
                    }

                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresEdit ul:first').empty();
                    $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                    $('#divErroresEdit').show();

                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresEdit ul:first').empty();
                    $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                    $('#divErroresEdit').show();
                }
            });
        }

        function DeleteRecord() {


            var rowId = sessionStorage.getItem("rowId");


            $.ajax({
                type: "POST",
                url: "frmPOAAjustado.aspx/DeleteRecord",
                data: JSON.stringify({ id: rowId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.OK)
                    {
                        $('#modalBorrar').modal('hide');

                        GetList('%', '%', '%', '%', '%', '%', '%', '%', '%');

                    }
                    else
                    {
                        $('#divErroresDelete ul:first').empty();

                        $.map(response.d.Errors, function (n) {
                            $('#divErroresDelete ul:first').append($("<li>").text(n));
                        });

                        $('#divErroresDelete').show();
                    }

                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresDelete ul:first').empty();
                    $('#divErroresDelete ul:first').append($("<li>").text(r.Message));
                    $('#divErroresDelete').show();

                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresDelete ul:first').empty();
                    $('#divErroresDelete ul:first').append($("<li>").text(r.Message));
                    $('#divErroresDelete').show();
                }

            });
        }

        function GetTituloPagina() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetTituloPagina",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                     
                        $('#titulopagina').html(data.d);

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }
        
        function GetMunicipios() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetMunicipios",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        $('#ddlMunicipio').append($("<option>").val(0).text('Seleccione...'));

                        $.map(data.d, function (n) {
                            $('#ddlMunicipio').append($("<option>").val(n.Id).text(n.Nombre));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }
        
        function GetLocalidades() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetLocalidades",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        sessionStorage.setItem("catalogoLocalidades", JSON.stringify(data.d));
                        
                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }
        
        function GetCriteriosPriorizacion() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetCriteriosPriorizacion",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        $('#ddlCriterioPriorizacion').append($("<option>").val(0).text('Seleccione...'));

                        $.map(data.d, function (n) {
                            $('#ddlCriterioPriorizacion').append($("<option>").val(n.Id).text(n.Nombre));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetAperturaProgramatica() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetAperturaProgramatica",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        sessionStorage.setItem("listaperturaprogramatica", JSON.stringify(data.d));

                        var listprogramas = $linq(data.d).where(function (x) { return x.Nivel == 1; }).toArray();                      

                        $('#ddlPrograma').append($("<option>").val(0).text('Seleccione...'));

                        $.map(listprogramas, function (n) {
                            $('#ddlPrograma').append($("<option>").val(n.Id).text(n.Nombre));
                        });
                     

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function SincronizarCombosAperturaProgramatica(event)
        {           

            var listaperturaprogramatica = JSON.parse(sessionStorage.getItem("listaperturaprogramatica"));
            var valorseleccionado = $('#' + event.target.id + ' option:selected').val();
           
            switch (event.target.id)
            {
                case "ddlPrograma":

                    var sublist = $linq(listaperturaprogramatica).where(function (x) { return x.ParentId == parseInt(valorseleccionado); }).toArray();
                                      
                    $('#ddlSubsubprograma').empty();
                    $('#ddlSubsubprograma').prop("disabled", true);
                    $('#ddlSubsubprograma').append($('<option/>', { value: 0, text: "Seleccione..." }));
                                      
                    var select = $('#ddlSubprograma'); 
                    select.empty();
                    select.append($('<option/>', { value: 0, text: "Seleccione..." }));

                    if (sublist.length == 0) {
                        select.prop("disabled", true);
                    }
                    else
                    {
                        select.prop("disabled", false);
                        $.each(sublist, function (index, itemData) {

                            select.append($('<option/>', { value: itemData.Id, text: itemData.Nombre }));

                        });
                    }
                                      

                    break;

                case "ddlSubprograma":

                    var sublist = $linq(listaperturaprogramatica).where(function (x) { return x.ParentId == parseInt(valorseleccionado); }).toArray();

                    var select = $('#ddlSubsubprograma');
                    select.empty();
                    select.append($('<option/>', { value: 0, text: "Seleccione..." }));

                    if (sublist.length == 0) {
                        select.prop("disabled", true);
                    }
                    else
                    {
                        select.prop("disabled", false);

                        $.each(sublist, function (index, itemData) {

                            select.append($('<option/>', { value: itemData.Id, text: itemData.Nombre }));

                        });
                    }

                    break;

                default:

                    break;
            }


        }

        function SincronizarCombosAperturaProgramaticaGetRecord(programaId,sprogramaId,ssprogramaId)
        {

            var listaperturaprogramatica = JSON.parse(sessionStorage.getItem("listaperturaprogramatica"));
            var subprogramas = $linq(listaperturaprogramatica).where(function (x) { return x.ParentId == parseInt(programaId); }).toArray();
            var subsubprogramas = $linq(listaperturaprogramatica).where(function (x) { return x.ParentId == parseInt(sprogramaId); }).toArray();
            
            var select = $('#ddlSubprograma');
            select.empty();
            select.append($('<option/>', { value: 0, text: "Seleccione..." }));

            if (subprogramas.length == 0)
            {
                select.prop("disabled", true);
            }
            else
            {
                select.prop("disabled", false);
                $.each(subprogramas, function (index, itemData) {

                    select.append($('<option/>', { value: itemData.Id, text: itemData.Nombre }));

                });
            }


            select = $('#ddlSubsubprograma');
            select.empty();
            select.append($('<option/>', { value: 0, text: "Seleccione..." }));

            if (subsubprogramas.length == 0)
            {
                select.prop("disabled", true);
            }
            else
            {
                select.prop("disabled", false);
                $.each(subsubprogramas, function (index, itemData) {

                    select.append($('<option/>', { value: itemData.Id, text: itemData.Nombre }));

                });
            }

            $("#ddlPrograma option[value=" + programaId + "]").prop("selected", true);
            $("#ddlSubprograma option[value=" + sprogramaId + "]").prop("selected", true);
            $("#ddlSubsubprograma option[value=" + ssprogramaId + "]").prop("selected", true);
            


        }

        function GetUnidadesMedida() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetUnidadesMedida",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        $('#ddlUnidadMedida').append($("<option>").val(0).text('Seleccione...'));

                        $.map(data.d, function (n) {
                            $('#ddlUnidadMedida').append($("<option>").val(n.Id).text(n.Nombre));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetSituacionesObra() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetSituacionesObra",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        $('#ddlSituacionObra').append($("<option>").val(0).text('Seleccione...'));

                        $.map(data.d, function (n) {
                            $('#ddlSituacionObra').append($("<option>").val(n.Id).text(n.Nombre));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetModalidadesEjecucion() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetModalidadesEjecucion",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        $('#ddlModalidad').append($("<option>").val(0).text('Seleccione...'));

                        $.map(data.d, function (n) {
                            $('#ddlModalidad').append($("<option>").val(n.Id).text(n.Nombre));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }
        
        function GetUnidadesPresupuestales() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetUnidadesPresupuestales",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        sessionStorage.setItem("catalogoUnidadesPresupuestales", JSON.stringify(data.d));

                        $.map(data.d, function (n) {
                            $('#ddlFiltroUnidadPresupuestal').append($("<option>").val(n.Id).text(n.Nombre));
                        });

                    },
                    error: function (response)
                    {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response)
                    {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetFondos() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetFondos",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        $.map(data.d, function (n) {
                            $('#ddlFiltroFondo').append($("<option>").val(n.Id).text(n.Abreviatura));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }
        
        function GetFuncionalidades() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetFuncionalidades",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        sessionStorage.setItem("catalogoFuncionalidad", JSON.stringify(data.d));

                        var list = $linq(data.d).where(function (x) { return x.Nivel == 3; }).toArray();

                        $('#ddlFuncionalidad').append($("<option>").val(0).text('Seleccione...'));

                        $.map(list, function (n)
                        {
                           $('#ddlFuncionalidad').append($("<option>").val(n.Id).text(n.Descripcion.toUpperCase()).attr("data-clave",n.Clave));                           
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetEjes() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetEjes",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {                                                

                        var list = $linq(data.d).where(function (x) { return x.Nivel == 1; }).toArray();

                        $('#ddlEje').append($("<option>").val(0).text('Seleccione...'));

                        $.map(list, function (n) {
                            $('#ddlEje').append($("<option>").val(n.Id).text(n.Descripcion.toUpperCase()));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetPlanesSectoriales() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetPlanesSectoriales",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {                                               

                        $('#ddlPlanSectorial').append($("<option>").val(0).text('Seleccione...'));

                        $.map(data.d, function (n) {
                            $('#ddlPlanSectorial').append($("<option>").val(n.Id).text(n.Descripcion.toUpperCase()));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetModalidades() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetModalidades",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        var list = $linq(data.d).where(function (x) { return x.Nivel == 2; }).toArray();

                        $('#ddlModalidadPVD').append($("<option>").val(0).text('Seleccione...'));

                        $.map(list, function (n) {
                            $('#ddlModalidadPVD').append($("<option>").val(n.Id).text(n.Descripcion.toUpperCase()));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetProgramasPVD() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetProgramasPVD",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                       
                        $('#ddlProgramaPVD').append($("<option>").val(0).text('Seleccione...'));

                        $.map(data.d, function (n) {
                            $('#ddlProgramaPVD').append($("<option>").val(n.Id).text(n.Descripcion.toUpperCase()));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function GetGrupoBeneficiarios() {

            $.ajax(
                {

                    type: "POST",
                    url: "frmPOAAjustado.aspx/GetGrupoBeneficiarios",
                    data: {},
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {

                        $('#ddlGrupoBeneficiario').append($("<option>").val(0).text('Seleccione...'));

                        $.map(data.d, function (n) {
                            $('#ddlGrupoBeneficiario').append($("<option>").val(n.Id).text(n.Nombre.toUpperCase()));
                        });

                    },
                    error: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response) {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function fnc_Validar()
        {


            var desc = $("#txtDescripcion").val();
             if (desc == null || desc.length == 0 || desc == undefined) {
                 alert("El campo descripción no puede estar vacio");
                 return false;
             }

             var municipio = $("#ddlMunicipio").val();
             if (municipio == null || municipio.length == 0 || municipio == undefined || municipio == 0) {
                 alert("El campo Municipio no puede estar vacio");
                 return false;
             }

            var localidad = $("#ddlLocalidad").val();
            if (localidad == null || localidad.length == 0 || localidad == undefined || localidad == 0) {
                alert("El campo Localidad no puede estar vacio");
                return false;
            }

            var criteriopriorizacion = $("#ddlCriterioPriorizacion").val();
             if (criteriopriorizacion == null || criteriopriorizacion.length == 0 || criteriopriorizacion == undefined || criteriopriorizacion == 0) {
                 alert("Debe indicar el criterio de priorización");
                 return false;
             }

             var valorseleccionado = $("#ddlCriterioPriorizacion option:selected").val();

            switch (valorseleccionado)
            {
                 case "2":

                     var nombreconvenio = $("#txtNombreConvenio").val();
                     if (nombreconvenio == null || nombreconvenio.length == 0 || nombreconvenio == undefined) {
                         alert("Debe indicar el nombre del convenio");
                         return false;
                     }

                     break;

                 default:

                     break;
             }




             var subsubprograma = $("#ddlSubsubprograma").val();
             if (subsubprograma == null || subsubprograma.length == 0 || subsubprograma == undefined || subsubprograma == 0) {
                 alert("Debe indicar el tipo de la apertura programatica");
                 return false;
             }

             var unidadmedida = $("#ddlUnidadMedida").val();
             if (unidadmedida == null || unidadmedida.length == 0 || unidadmedida == undefined || unidadmedida == 0) {
                 alert("Debe indicar la unidad de medida");
                 return false;
             }

             var numeroBeneficiarios = $("#txtNumeroBeneficiarios").val();
             if (numeroBeneficiarios == null || numeroBeneficiarios.length == 0 || numeroBeneficiarios == undefined) {
                 alert("El campo Número de beneficiarios no puede estar vacio");
                 return false;
             }

             var cantidadUnidades = $("#txtCantidadUnidades").val();
             if (cantidadUnidades == null || cantidadUnidades.length == 0 || cantidadUnidades == undefined) {
                 alert("El campo Cantidad de Unidades no puede estar vacio");
                 return false;
             }

             var situacionObra = $("#ddlSituacionObra").val();
             if (situacionObra == null || situacionObra.length == 0 || situacionObra == undefined || situacionObra == 0) {
                 alert("Debe indicar la situación de la obra");
                 return false;
             }

             var situacionobraId = $("#ddlSituacionObra option:selected").val();

             switch (situacionobraId) {
                 case "2":
                 case "3":

                     var numeroobraanterior = $("#txtNumeroAnterior").val();
                     if (numeroobraanterior == null || numeroobraanterior.length == 0 || numeroobraanterior == undefined) {
                         alert("Debe indicar el número de obra anterior");
                         return false;
                     }

                     break;

                 default:

                     break;
             }
         

             return true;

        }
     

        function fnc_ocultarDivDatosConvenio()
        {
            var valorseleccionado = $("#ddlCriterioPriorizacion option:selected").val();

            switch (valorseleccionado)
            {
                case "2":

                    $("#divDatosConvenio").css("display", "block");
                    break;

                default:

                    $("#divDatosConvenio").css("display", "none");
                    break;
            }
        }

        function fnc_ocultarDivObraAnterior()
        {

            var valorseleccionado = $("#ddlSituacionObra option:selected").val();
          
            switch (valorseleccionado)
            {
                case "0":
                    $("#divDatosObraAnterior").css("display", "none");
                    break;
                case "1":
                    $("#divDatosObraAnterior").css("display", "none");
                    break;
                default:
                    $("#divDatosObraAnterior").css("display", "block");
                    break;
            }
        }
        
        
        function GetList(WhereNumero, WhereDescripcion, WhereMunicipio, WhereLocalidad, WhereUnidadPresupuestal, WhereContratista, WhereFondos, WherePresupuesto) {
                       

            $.ajax({

                type: "POST",
                url: "frmPOAAjustado.aspx/GetListadoObras",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ WhereNumero: WhereNumero, WhereDescripcion: WhereDescripcion,WhereMunicipio:WhereMunicipio,WhereLocalidad:WhereLocalidad,WhereUnidadPresupuestal:WhereUnidadPresupuestal,WhereContratista:WhereContratista,WhereFondos:WhereFondos,WherePresupuesto:WherePresupuesto }),
                dataType: "json",
                success: function (data) {

                    //On success
                                        

                    sessionStorage.setItem("listapoaajustado", JSON.stringify(data.d));

                    $('.table-responsive').empty();

                    $('.table-responsive').append($("<table>").attr('id','tblPOA'));
                    var table = $('.table-responsive table:first').addClass('table table-condensed table-bordered');
                    table.append($("<thead>"));
                    table.append($("<tbody>"));

                    var thead = $('.table-responsive thead:first');
                    var tbody = $('.table-responsive tbody:first');

                    var tr = $("<tr>").addClass('panel-footer');
                    tr.append($("<th>").text('Acción').addClass('col-md-1'));
                    tr.append($("<th>").text('Número'));
                    tr.append($("<th>").text('Descripción'));
                    tr.append($("<th>").text('Municipio'));
                    tr.append($("<th>").text('Localidad'));                 
                    tr.append($("<th>").text('Contratista'));                   
                    tr.append($("<th>").text('Presupuesto'));

                    thead.append(tr);

                    var dependencias = $linq(data.d).groupBy("x => x.SubUnidadPresupuestal",null)  
                               .select("x => x.key")
                               .toArray();

                    $.map(dependencias, function (dep) {

                        var tr = $("<tr>").addClass('panel-footer text-center');
                        tr.append($("<td>").text(dep).attr('colspan', '7')).css("font-weight", "bold");
                        tbody.append(tr);

                        var listobrasdepto = $linq(data.d).where(function (x) { return x.SubUnidadPresupuestal == dep; }).toArray();
                                               
                      

                        var fondos = $linq(listobrasdepto).groupBy("x => x.Fondos",null)  
                                        .select("x => x.key")
                                        .toArray();


                        $.map(fondos, function (f) {

                            var tr = $("<tr>").addClass('panel-footer');
                            tr.append($("<td>").text(f).attr('colspan', '7')).css("font-weight", "bold");
                            tbody.append(tr);

                            var listobrasfondo = $linq(data.d).where(function (x) { return x.SubUnidadPresupuestal == dep & x.Fondos == f; }).toArray();


                            $.map(listobrasfondo, function (n) {

                                var tr = $("<tr>");
                                tr.append($("<td>").html("<button type='button' class='edit-control btn' data-rowid='" + n.ObraId + "'>&nbsp;</button><button type='button' class='delete-control btn' data-rowid='" + n.ObraId + "'>&nbsp;</button>"));
                                tr.append($("<td>").text(n.Numero));
                                tr.append($("<td>").text(n.Descripcion));
                                tr.append($("<td>").text(n.Municipio));
                                tr.append($("<td>").text(n.Localidad));                               
                                tr.append($("<td>").text(n.Contratista.replace('empty',' ')));                                
                                tr.append($("<td>").text('$' + parseFloat(n.Presupuesto, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                
                                tbody.append(tr);

                            });

                            if (listobrasfondo.length > 0)
                            {

                                var stotalFondo = $linq(listobrasfondo).sum("x => x.Presupuesto");
                                total += stotalFondo;

                                var tr = $("<tr>");
                                tr.addClass('panel-footer text-right');
                                tr.css("font-weight", "bold");
                                tr.append($("<td>").text('Subtotal').attr('colspan', '6'));
                                tr.append($("<td>").text('$' + parseFloat(stotalFondo, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                tbody.append(tr);
                                
                            }


                        });//$.map-fondos


                    });//$.map-dependencias

                    var total = $linq(data.d).sum("x => x.Presupuesto");

                    var tr = $("<tr>");
                    tr.addClass('panel-footer text-right');
                    tr.css("font-weight", "bold");
                    tr.append($("<td>").text('Total').attr('colspan', '6'));
                    tr.append($("<td>").text('$' + parseFloat(total, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                    tbody.append(tr);                

                    $('#tituloresumen').text('Total de obras y acciones: ' + data.d.length);


                },//success
                error: function (result) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresListado ul:first').empty();
                    $('#divErroresListado ul:first').append($("<li>").text(r.Message));
                    $('#divErroresListado').show();
                }

            });
        }




    </script>
    

</asp:Content>

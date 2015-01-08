<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="frmPOAAjustado.aspx.cs" 

Inherits="SIP.Formas.POA.frmPOAAjustado" EnableEventValidation = "false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <ajaxToolkit:ToolkitScriptManager runat="server" />

    <div class="container">
       
        <div class="page-header"><h3><asp:Label ID="lblTituloPOA" runat="server" Text=""></asp:Label></h3></div>

        <div class="row">
            <div class="col-md-8"></div>
            <div class="col-md-4 text-right">
                <a href="<%=ResolveClientUrl("~/Formas/POA/POAAjustadoFinanciamiento.aspx") %>" ><span class="glyphicon glyphicon-arrow-right"></span> <strong>Modificar financiamientos</strong></a>
            </div>
        </div>        
        <br />

        <div class="panel-footer alert alert-danger" id="divMsg" style="display:none" runat="server">
          <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
        </div>

        <asp:GridView 
            ID="GridViewObras" runat="server"
            ItemType="DataAccessLayer.Models.Obra" DataKeyNames="Id"
            SelectMethod="GridViewObras_GetData"
            ShowHeaderWhenEmpty="true" CssClass="table" AutoGenerateColumns="False" 
            OnRowDataBound="GridViewObras_RowDataBound" AllowPaging="True"
            OnPageIndexChanging="GridViewObras_PageIndexChanging">
            <Columns>

                       <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">
                            <ItemTemplate>
                                                    
                                <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" data-tipo-operacion="editar"/>
                                <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick="imgBtnEliminar_Click" data-tipo-operacion="borrar"/>

                            </ItemTemplate>                         
                        </asp:TemplateField>     
                                         
                       <asp:DynamicField DataField="Numero" HeaderText="Número" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer"/>
                       <asp:DynamicField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="panel-footer"/> 
                       <asp:TemplateField HeaderText="Tipo" ItemStyle-CssClass="col-md-2" HeaderStyle-CssClass="panel-footer">
                           <ItemTemplate>
                                <asp:Label Text='<%# Item.AperturaProgramatica.AperturaProgramaticaTipo.Nombre %>' runat="server" />
                           </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Importe Asignado" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">
                           <ItemTemplate>
                              <asp:Label Text='<%# String.Format("{0:C2}",Item.GetImporteAsignado()) %>' runat="server" />
                           </ItemTemplate>
                       </asp:TemplateField>                   
                
            </Columns>
                    
            <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                    
        </asp:GridView>

        <div id="divBtnNuevo" runat="server" style="display:block">
              <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default" OnClick="btnNuevo_Click" OnClientClick="fnc_RecuperarValoresEnCamposDeshabilitados();" AutoPostBack="false" />
         </div>
    
        <div id="divEdicion" runat="server" class="panel-footer" style="display:none">
            
            <ul class="nav nav-tabs" role="tablist">
              <li class="active"><a href="#datosgenerales" role="tab" data-toggle="tab">Datos generales</a></li>
              <li><a href="#planveracruzanodesarrollo" role="tab" data-toggle="tab">Plan Veracruzano de Desarrollo</a></li>        
            </ul>

            <div class="tab-content">
                               
                
                <div class="tab-pane active" id="datosgenerales">

                     <div class="row">

                     <br /> 

                     <div class="col-md-4">

                          <div class="form-group">
                               <label for="Numero">Número</label>
                             <div>
                                <input type="text" class="input-sm required form-control" id="txtNumero" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off" disabled="disabled"/>                           
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="Descripcion">Descripción</label>
                             <div>
                                <textarea id="txtDescripcion" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="3" autofocus></textarea>
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="Municipio">Municipio</label>
                             <div>
                                 <asp:DropDownList ID="ddlMunicipio" CssClass="form-control" runat="server"></asp:DropDownList>
                                 <ajaxToolkit:CascadingDropDown ID="cddlMunicipio" runat="server" 
                                     ServicePath="WebServicePOA.asmx" ServiceMethod="GetMunicipios" 
                                     TargetControlID="ddlMunicipio" Category="municipioId"
                                     PromptText="Seleccione el Municipio..." LoadingText="Loading..."/>                           
                            
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="Localidad">Localidad</label>
                             <div>
                                 <asp:DropDownList ID="ddlLocalidad" CssClass="form-control" runat="server" ></asp:DropDownList>
                                 <ajaxToolkit:CascadingDropDown ID="cddlLocalidad" runat="server" 
                                     ServicePath="WebServicePOA.asmx" ServiceMethod="GetLocalidades" 
                                     TargetControlID="ddlLocalidad" ParentControlID="ddlMunicipio" Category="localidadId"
                                     PromptText="Seleccione la localidad..." LoadingText="Loading..."/>                 
                                                     
                            </div>
                          </div>                  
                     
                         <div class="form-group">
                                   <label for="ddlCriterioPriorizacion">Criterio de priorización</label>
                                 <div>
                                     <asp:DropDownList ID="ddlCriterioPriorizacion" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                             </div>

                             <div style="display:none" id="divDatosConvenio">

                                    <div class="form-group">
                                        <label for="NombreConvenio">Nombre del convenio</label>
                                        <div>
                                            <textarea id="txtNombreConvenio" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="2" ></textarea>
                                        </div>
                                    </div>    

                              </div><!--divDatosConvenio-->

                          <div class="form-group">
                               <label for="FechaInicio">Fecha de inicio</label>
                             <div>
                                <input type="text" class="required form-control date-picker" id="txtFechaInicio" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="FechaTermino">Fecha de Término</label>
                             <div>
                                <input type="text" class="input-sm required form-control date-picker" id="txtFechaTermino" runat="server" data-date-format = "dd/mm/yyyy" autocomplete="off" />
                            </div>
                          </div>

                     </div>

                     <div class="col-md-4">

                          <div class="form-group">
                               <label for="Programa">Programa</label>
                             <div>
                                 <asp:DropDownList ID="ddlPrograma" CssClass="form-control" runat="server"></asp:DropDownList>
                                 <ajaxToolkit:CascadingDropDown ID="cddlPrograma" runat="server" 
                                     ServicePath="WebServicePOA.asmx" ServiceMethod="GetProgramas" 
                                     TargetControlID="ddlPrograma" Category="programaId"
                                     PromptText="Seleccione el programa..." LoadingText="Loading..."/>                           
                            
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="SubPrograma">SubPrograma</label>
                             <div>
                                 <asp:DropDownList ID="ddlSubprograma" CssClass="form-control" runat="server" ></asp:DropDownList>
                                 <ajaxToolkit:CascadingDropDown ID="cddlSubprograma" runat="server" 
                                     ServicePath="WebServicePOA.asmx" ServiceMethod="GetSubProgramas" 
                                     TargetControlID="ddlSubprograma" ParentControlID="ddlPrograma" Category="subprogramaId"
                                     PromptText="Seleccione el subprograma..." LoadingText="Loading..."/>                 
                                                     
                            </div>
                          </div>

                           <div class="form-group">
                               <label for="SubSubPrograma">SubSubPrograma</label>
                             <div>
                                 <asp:DropDownList ID="ddlSubsubprograma" CssClass="form-control" runat="server" ></asp:DropDownList>
                                 <ajaxToolkit:CascadingDropDown ID="cddlSubsubprograma" runat="server" 
                                     ServicePath="WebServicePOA.asmx" ServiceMethod="GetSubSubProgramas" 
                                     TargetControlID="ddlSubsubprograma" ParentControlID="ddlSubprograma" Category="subsubprogramaId"
                                     PromptText="Seleccione el subsubprograma..." LoadingText="Loading..."/>              
                             
                            </div>
                          </div>

                           <div class="form-group" style="display:none">
                               <label for="Metas">Metas</label>
                             <div>
                                 <asp:DropDownList ID="ddlMeta" CssClass="form-control" runat="server"></asp:DropDownList>
                                  <ajaxToolkit:CascadingDropDown ID="cddlMeta" runat="server" 
                                     ServicePath="WebServicePOA.asmx" ServiceMethod="GetMetas" 
                                     TargetControlID="ddlMeta" ParentControlID="ddlSubsubprograma" Category="metaId"
                                     PromptText="Seleccione la meta..." LoadingText="Loading..."/>
                           
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="UnidadMedida">Unidad de medida</label>
                             <div>
                                  <asp:DropDownList ID="ddlUnidadMedida" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                          </div>

                          <div class="form-group">
                               <label for="NumeroBeneficiarios">Número de beneficiarios</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtNumeroBeneficiarios" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-m-dec="0"/>
                            </div>
                          </div>

                           <div class="form-group">
                               <label for="CantidadUnidades">Cantidad de unidades</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtCantidadUnidades" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-m-dec="0"/>
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="Empleos">Empleos</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtEmpleos" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-m-dec="0"/>
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="Jornales">Jornales</label>
                             <div>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtJornales" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-m-dec="0"/>
                            </div>
                          </div>


                      </div>

                     <div class="col-md-4">

                          <div class="form-group">
                               <label for="SituacionObra">Situación</label>
                             <div>
                                  <asp:DropDownList ID="ddlSituacionObra" CssClass="form-control" runat="server" disabled="disabled"></asp:DropDownList>
                            </div>
                          </div>

                     

                         <div class="form-group">
                               <label for="txtImporteTotal">Costo total</label>
                             <div class="input-group">
                                <span class="input-group-addon">$</span>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtImporteTotal" runat="server" style="text-align: left; align-items:flex-start" disabled="disabled" />
                            </div>
                          </div>

                         <div style="display:none" id="divDatosObraAnterior">

                              <div class="form-group">
                                <label for="Numero">Número anterior</label>
                                <div>
                                    <input type="text" class="input-sm required form-control" id="txtNumeroAnterior" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off" disabled="disabled" />                           
                                </div>
                              </div>

                              <div class="form-group">
                                   <label for="txtImporteLiberado">Costo liberado en ejercicios anteriores</label>
                                 <div class="input-group">
                                    <span class="input-group-addon">$</span>
                                    <input type="text" class="input-sm required form-control campoNumerico" id="txtImporteLiberadoEjerciciosAnteriores" runat="server" style="text-align: left; align-items:flex-start" disabled="disabled" autocomplete="off" />
                                </div>
                              </div>

                          </div><!--divDatosObraAnterior-->


                         <div class="form-group">
                               <label for="txtPresupuestoEjercicio">Presupuesto del ejercicio</label>
                             <div class="input-group">
                                <span class="input-group-addon">$</span>
                                <input type="text" class="input-sm required form-control campoNumerico" id="txtPresupuestoEjercicio" runat="server" style="text-align: left; align-items:flex-start" disabled="disabled" />
                            </div>
                          </div>

                         <div class="form-group">
                               <label for="ModalidadObra">Modalidad de ejecución</label>
                             <div>
                                  <asp:DropDownList ID="ddlModalidad" CssClass="form-control" runat="server"></asp:DropDownList>
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

                </div><!--datosgenerales-->

                <div class="tab-pane" id="planveracruzanodesarrollo">
                    
                     <div class="row">

                        <br />

                        <div class="col-md-4">

                            <div class="panel panel-default">
                              <div class="panel-heading">
                                <h3 class="panel-title">Clasificación funcional</h3>
                              </div>
                              <div class="panel-body">

                                  <div class="form-group">
                                       <label for="Finalidad">Finalidad</label>
                                     <div>
                                         <asp:DropDownList ID="ddlFinalidad" CssClass="form-control" runat="server" ></asp:DropDownList>
                                         <ajaxToolkit:CascadingDropDown ID="cddlFuncionalidadNivel1" runat="server" 
                                             ServicePath="WebServicePOA.asmx" ServiceMethod="GetFuncionalidadNivel1" 
                                             TargetControlID="ddlFinalidad" Category="finalidadId"
                                             PromptText="Seleccione la finalidad..." LoadingText="Loading..."/>  
                                    </div>
                                  </div>

                                  <div class="form-group">
                                       <label for="Funcion">Funcion</label>
                                     <div>
                                         <asp:DropDownList ID="ddlFuncion" CssClass="form-control" runat="server" ></asp:DropDownList>
                                          <ajaxToolkit:CascadingDropDown ID="cddlFuncionalidadNivel2" runat="server" 
                                             ServicePath="WebServicePOA.asmx" ServiceMethod="GetFuncionalidadNivel2" 
                                             TargetControlID="ddlFuncion" ParentControlID="ddlFinalidad" Category="funcionId"
                                             PromptText="Seleccione la función..." LoadingText="Loading..."/>  
                                    </div>
                                  </div>

                                  <div class="form-group">
                                     <label for="SubFuncion">SubFuncion</label>
                                     <div>
                                         <asp:DropDownList ID="ddlSubFuncion" CssClass="form-control" runat="server" ></asp:DropDownList>
                                          <ajaxToolkit:CascadingDropDown ID="cddlFuncionalidadNivel3" runat="server" 
                                             ServicePath="WebServicePOA.asmx" ServiceMethod="GetFuncionalidadNivel3" 
                                             TargetControlID="ddlSubFuncion" ParentControlID="ddlFuncion" Category="subfuncionId"
                                             PromptText="Seleccione la sub función..." LoadingText="Loading..."/>  
                                    </div>
                                  </div>
                                                                  
                              </div>

                            </div><!--Funcionalidad panel panel-default-->

                            <%--<div class="panel panel-default">
                              <div class="panel-heading">
                                <h3 class="panel-title">Eje del PVD</h3>
                              </div>
                              <div class="panel-body">

                                  <div class="form-group">
                                       <label for="EjeAgrupador">Agrupador</label>
                                     <div>
                                         <asp:DropDownList ID="ddlEjeAgrupador" CssClass="form-control" runat="server" ></asp:DropDownList>
                                         <ajaxToolkit:CascadingDropDown ID="cddlEjePVD1" runat="server" 
                                             ServicePath="WebServicePOA.asmx" ServiceMethod="GetEjePVDNivel1" 
                                             TargetControlID="ddlEjeAgrupador" Category="ejeAgrupadorId"
                                             PromptText="Seleccione el eje agrupador..." LoadingText="Loading..."/>  
                                    </div>
                                  </div>

                                  <div class="form-group">
                                       <label for="EjeElemento">Elemento</label>
                                     <div>
                                         <asp:DropDownList ID="ddlEjeElemento" CssClass="form-control" runat="server" ></asp:DropDownList>
                                         <ajaxToolkit:CascadingDropDown ID="cddlEjePVD2" runat="server" 
                                             ServicePath="WebServicePOA.asmx" ServiceMethod="GetEjePVDNivel2" 
                                             TargetControlID="ddlEjeElemento" ParentControlID="ddlEjeAgrupador" Category="ejeElementoId"
                                             PromptText="Seleccione el eje..." LoadingText="Loading..."/>
                                    </div>
                                  </div>
                                                                                                    
                              </div>

                            </div><!--Eje panel panel-default-->     --%>  
                            
                            <div class="form-group">
                                 <label for="Eje">Eje</label>
                                 <div>
                                     <asp:DropDownList ID="ddlEje" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>                                                
                                                        
                                                            
                        </div><!--col-md-4-->
                         
                        <div class="col-md-4">

                               <div class="form-group">
                                   <label for="PlanSectorial">Plan Sectorial</label>
                                 <div>
                                     <asp:DropDownList ID="ddlPlanSectorial" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                              </div>
                            
                            
                            
                              <div class="panel panel-default">
                              <div class="panel-heading">
                                <h3 class="panel-title">Clasificación Programática CONAC</h3>
                              </div>
                              <div class="panel-body">

                                  <div class="form-group">
                                       <label for="ddlModalidadAgrupador">Agrupador</label>
                                     <div>
                                         <asp:DropDownList ID="ddlModalidadAgrupador" CssClass="form-control" runat="server" ></asp:DropDownList>
                                         <ajaxToolkit:CascadingDropDown ID="cddlModalidadAgrupador" runat="server" 
                                             ServicePath="WebServicePOA.asmx" ServiceMethod="GetModalidadNivel1" 
                                             TargetControlID="ddlModalidadAgrupador" Category="modalidadAgrupadorId"
                                             PromptText="Seleccione el agrupador..." LoadingText="Loading..."/> 
                                    </div>
                                  </div>

                                  <div class="form-group">
                                       <label for="ddlModalidadElemento">Elemento</label>
                                     <div>
                                         <asp:DropDownList ID="ddlModalidadElemento" CssClass="form-control" runat="server"></asp:DropDownList>
                                         <ajaxToolkit:CascadingDropDown ID="cddlModalidadElemento" runat="server" 
                                             ServicePath="WebServicePOA.asmx" ServiceMethod="GetModalidadNivel2" 
                                             TargetControlID="ddlModalidadElemento" ParentControlID="ddlModalidadAgrupador" Category="modalidadElementoId"
                                             PromptText="Seleccione el elemento..." LoadingText="Loading..."/>
                                    </div>
                                  </div>
                                                                                                    
                              </div>

                            </div><!--Clasificación Programática panel panel-default-->   
                                 
                            <div class="form-group">
                                   <label for="Programa">Programa</label>
                                 <div>
                                      <asp:DropDownList ID="ddlProgramaPresupuesto" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                              </div>

                              <div class="form-group">
                                   <label for="GrupoBeneficiarios">Grupo de beneficiarios</label>
                                 <div>
                                      <asp:DropDownList ID="ddlGrupoBeneficiario" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                              </div>

                        </div><!--col-md-4-->                                                
                
                    </div><!--row-->


                </div><!--planveracruzanodesarrollo--> 
                
                 <div class="form-group header">
                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnGuardar" runat="server" OnClientClick="return fnc_Validar()" OnClick="btnGuardar_Click" AutoPostBack="false" />
                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" OnClientClick="return fnc_OcultarDivs()" AutoPostBack="false" />
                </div>        

            </div>                                        
                               

             <div style="display:none" runat="server">
                <asp:TextBox ID="_ID" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                <asp:TextBox ID="_Accion" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>                                                           
             </div>
            
             <asp:HiddenField ID="hiddenSituacionObraId" runat="server" />                       
                     

       </div><!--divEdicion-->

    </div><!--div class="container"-->



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


            $('*[data-tipo-operacion]').click(function () {

                if ($("#<%= divEdicion.ClientID %>").is(':visible')) {
                    return false;
                }



                var strOperacion = $(this).data("tipo-operacion").toUpperCase();

                switch (strOperacion) {

                    case "EDITAR":
                        return true;
                        break;
                    case "BORRAR":
                        return confirm("¿Está seguro de eliminar el registro?");
                        break;
                    case "ASIGNARFINANCIAMIENTO":
                        var url = $(this).data("url-financiamiento");
                        $(location).attr('href', url);
                        break;
                    case "EVALUAR":
                        var url = $(this).data("url-poa");
                        $(location).attr('href', url);
                        break;
                    default:
                        break;
                }

                return false;

            });


             $("#<%= ddlCriterioPriorizacion.ClientID %>").change(function (e) {


                var valorseleccionado = $("#<%= ddlCriterioPriorizacion.ClientID   %> option:selected").val();

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


            $("#<%= ddlSituacionObra.ClientID   %>").change(function (e) {

                var valorseleccionado = $("#<%= ddlSituacionObra.ClientID   %> option:selected").val();

                $("#<%= hiddenSituacionObraId.ClientID %>").val(valorseleccionado);

                 switch (valorseleccionado) {
                     case "0":
                         $("#divDatosObraAnterior").css("display", "none");
                         $("#<%= txtNumeroAnterior.ClientID %>").val("");
                         $("#<%= txtImporteLiberadoEjerciciosAnteriores.ClientID %>").val("");
                         break;
                     case "1":
                         $("#divDatosObraAnterior").css("display", "none");
                         $("#<%= txtNumeroAnterior.ClientID %>").val("");
                         $("#<%= txtImporteLiberadoEjerciciosAnteriores.ClientID %>").val("");
                         break;
                     default:
                         $("#divDatosObraAnterior").css("display", "block");
                         break;
                 }


             });


        }); //$(document).ready



        function fnc_Validar()
        {


            var desc = $("#<%=txtDescripcion.ClientID%>").val();
             if (desc == null || desc.length == 0 || desc == undefined) {
                 alert("El campo descripción no puede estar vacio");
                 return false;
             }

             var municipio = $("#<%=ddlMunicipio.ClientID%>").val();
             if (municipio == null || municipio.length == 0 || municipio == undefined || municipio == 0) {
                 alert("El campo Municipio no puede estar vacio");
                 return false;
             }

             var localidad = $("#<%= ddlLocalidad.ClientID %>").val();
            if (localidad == null || localidad.length == 0 || localidad == undefined || localidad == 0) {
                alert("El campo Localidad no puede estar vacio");
                return false;
            }

            var criteriopriorizacion = $("#<%= ddlCriterioPriorizacion.ClientID %>").val();
             if (criteriopriorizacion == null || criteriopriorizacion.length == 0 || criteriopriorizacion == undefined || criteriopriorizacion == 0) {
                 alert("Debe indicar el criterio de priorización");
                 return false;
             }

             var valorseleccionado = $("#<%= ddlCriterioPriorizacion.ClientID   %> option:selected").val();

            switch (valorseleccionado)
            {
                 case "2":

                     var nombreconvenio = $("#<%= txtNombreConvenio.ClientID %>").val();
                     if (nombreconvenio == null || nombreconvenio.length == 0 || nombreconvenio == undefined) {
                         alert("Debe indicar el nombre del convenio");
                         return false;
                     }

                     break;

                 default:

                     break;
             }




             var subsubprograma = $("#<%= ddlSubsubprograma.ClientID %>").val();
             if (subsubprograma == null || subsubprograma.length == 0 || subsubprograma == undefined || subsubprograma == 0) {
                 alert("Debe indicar el tipo de la apertura programatica");
                 return false;
             }

             var unidadmedida = $("#<%= ddlUnidadMedida.ClientID %>").val();
             if (unidadmedida == null || unidadmedida.length == 0 || unidadmedida == undefined || unidadmedida == 0) {
                 alert("Debe indicar la unidad de medida");
                 return false;
             }

             var numeroBeneficiarios = $("#<%=txtNumeroBeneficiarios.ClientID%>").val();
             if (numeroBeneficiarios == null || numeroBeneficiarios.length == 0 || numeroBeneficiarios == undefined) {
                 alert("El campo Número de beneficiarios no puede estar vacio");
                 return false;
             }

             var cantidadUnidades = $("#<%=txtCantidadUnidades.ClientID%>").val();
             if (cantidadUnidades == null || cantidadUnidades.length == 0 || cantidadUnidades == undefined) {
                 alert("El campo Cantidad de Unidades no puede estar vacio");
                 return false;
             }

             var situacionObra = $("#<%=ddlSituacionObra.ClientID%>").val();
             if (situacionObra == null || situacionObra.length == 0 || situacionObra == undefined || situacionObra == 0) {
                 alert("Debe indicar la situación de la obra");
                 return false;
             }

             var situacionobraId = $("#<%= ddlSituacionObra.ClientID   %> option:selected").val();

             switch (situacionobraId) {
                 case "2":
                 case "3":

                     var numeroobraanterior = $("#<%= txtNumeroAnterior.ClientID %>").val();
                     if (numeroobraanterior == null || numeroobraanterior.length == 0 || numeroobraanterior == undefined) {
                         alert("Debe indicar el número de obra anterior");
                         return false;
                     }

                     break;

                 default:

                     break;
             }

            <%-- var modalidad = $("#<%= ddlModalidad.ClientID %>").val();
             if (modalidad == null || modalidad.length == 0 || modalidad == undefined || modalidad == 0) {
                 alert("Debe indicar la modalidad de ejecución de la obra");
                 return false;
             }--%>

           <%--  var importetotal = $("#<%=txtImporteTotal.ClientID%>").val();
             if (importetotal == null || importetotal.length == 0 || importetotal == undefined) {
                 alert("El campo Importe total no puede estar vacio");
                 return false;
             }--%>
      

             var importeLiberado = $("#<%= txtImporteLiberadoEjerciciosAnteriores.ClientID %>").val();
             if (importeLiberado == null || importeLiberado.length == 0 || importeLiberado == undefined) {
                 alert("El campo <Costo liberado en ejercicios anteriores> no puede estar vacio");
                 return false;
             }

            <%-- var importePresupuesto = $("#<%= txtPresupuestoEjercicio.ClientID %>").val();
             if (importePresupuesto == null || importePresupuesto.length == 0 || importePresupuesto == undefined) {
                 alert("El campo <Presupuesto del ejercicio> no puede estar vacio");
                 return false;
             }--%>

             <%-- var subfuncion = $("#<%= ddlSubFuncion.ClientID %>").val();
             if (subfuncion == null || subfuncion.length == 0 || subfuncion == undefined || subfuncion == 0) {
                 alert("Debe indicar la funcionalidad de la obra");
                 return false;
             }--%>

             <%-- var eje = $("#<%= ddlEje.ClientID %>").val();
             if (eje == null || eje.length == 0 || eje == undefined || eje == 0) {
                 alert("Debe indicar el <Eje del Plan de Desarrollo Veracruzano>");
                 return false;
             }--%>

             <%--  var plansectorial = $("#<%= ddlPlanSectorial.ClientID %>").val();
             if (plansectorial == null || plansectorial.length == 0 || plansectorial == undefined || plansectorial == 0) {
                 alert("Debe indicar el <Plan sectorial> correspondiente");
                 return false;
             }--%>

             <%-- var clasificacionCONAC = $("#<%= ddlModalidadElemento.ClientID %>").val();
             if (clasificacionCONAC == null || clasificacionCONAC.length == 0 || clasificacionCONAC == undefined || clasificacionCONAC == 0) {
                 alert("Debe indicar la <Clasificación programática del CONAC> correspondiente");
                 return false;
             }--%>

             <%-- var programaPresupuestal = $("#<%= ddlProgramaPresupuesto.ClientID %>").val();
             if (programaPresupuestal == null || programaPresupuestal.length == 0 || programaPresupuestal == undefined || programaPresupuestal == 0) {
                 alert("Debe indicar el <Programa presupuestal> correspondiente");
                 return false;
             }--%>

            <%-- var grupoBeneficiario = $("#<%= ddlGrupoBeneficiario.ClientID %>").val();
             if (grupoBeneficiario == null || grupoBeneficiario.length == 0 || grupoBeneficiario == undefined || grupoBeneficiario == 0) {
                 alert("Debe indicar el <Grupo de Beneficiarios> correspondiente");
                 return false;
             }--%>


            var fechaInicio = $("#<%= txtFechaInicio.ClientID %>").val();
            var fechaTermino = $("#<%= txtFechaTermino.ClientID %>").val();

            if (IsDate(fechaInicio) & IsDate(fechaTermino))
            {
                if (fechaTermino <= fechaInicio)
                {
                     alert("La fecha de término debe ser posterior a la fecha de inicio");
                     return false;
                }
             }

             return true;

         }

        function fnc_OcultarDivs(sender)
        {
             $("#<%= divBtnNuevo.ClientID %>").css("display", "block");
             $("#<%= divEdicion.ClientID %>").css("display", "none");
             return false;
         }

        function fnc_ocultarDivDatosConvenio()
        {
             var valorseleccionado = $("#<%= ddlCriterioPriorizacion.ClientID   %> option:selected").val();

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

            var valorseleccionado = $("#<%= ddlSituacionObra.ClientID   %> option:selected").val();

            $("#<%= hiddenSituacionObraId.ClientID %>").val(valorseleccionado);

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


        function fnc_EjecutarMensaje(mensaje) {
            alert(mensaje);
        }

        function fnc_IrDesdeGrid(url) {
            $(location).attr('href', url);
        }

        function IsDate(dateString) {
            date = new Date(dateString);
            return date instanceof Date && !isNaN(date.valueOf());
        }

        function fnc_RecuperarValoresEnCamposDeshabilitados()
        {           
            $("#<%= hiddenSituacionObraId.ClientID %>").val($("#<%= ddlSituacionObra.ClientID   %> option:selected").val());
        }

        function fnc_DeshabilitarSituacionObra(enable)
        {
            if (enable)
            {
                $("#<%= ddlSituacionObra.ClientID %>").prop("disabled", false);
                $("#<%= txtNumeroAnterior.ClientID %>").prop("disabled", false); 
                $("#<%= txtImporteLiberadoEjerciciosAnteriores.ClientID %>").prop("disabled", false); 
            }
            else
            {
                $("#<%= ddlSituacionObra.ClientID %>").prop("disabled", true);
                $("#<%= txtNumeroAnterior.ClientID %>").prop("disabled", true);
                $("#<%= txtImporteLiberadoEjerciciosAnteriores.ClientID %>").prop("disabled", true); 
            }          
        }


    </script>







</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfContrato.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfContratoPresupuestoContratado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script type="text/javascript">

            $(document).ready(function () {
                $('.campoNumerico').autoNumeric('init');
            });
            
</script>


</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">
    
    <div class="panel panel-success">
        <div class="panel-heading">
            <div class="row">
                <div class="col-md-2"><h3 class="panel-title">Datos del Contrato</h3></div>
                
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfContrato.aspx") %>" >A) Contrato</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfPresupuestoContratado.aspx") %>" >B) Presupuesto Contratado</a> </div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfProgramaDeObra.aspx") %>" >C) Programa de Obra</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfProgramacionEstimaciones.aspx") %>" >D) Programación de Estimaciones</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfContratosDeObra.aspx") %>" >Regresar</a></div>

             </div>   
        </div>
    </div>


    <div class="row">

                    
                 <div class="col-md-6">

                      <div class="form-group">
                           <label for="Numero">Número de Contrato</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtNumContrato" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNumContrato" ErrorMessage="El número de contrato es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>
                     
                     <div class="form-group">
                         <label for="ClaveContratista">Clave contratista</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtClaveContratista" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtClaveContratista" ErrorMessage="La clave del contratista es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>
                     

                     <div class="form-group">
                         <label for="RFC">RFC contratista</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtRFC" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtRFC" ErrorMessage="El RFC del contratista es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>


                     <div class="form-group">
                         <label for="RazonSocial">Razón Social contratista</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtRazonSocial" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRazonSocial" ErrorMessage="La razón social del contratista es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>

                      <div class="form-group">
                           <label for="ClavePtal">Clave Presupuestal a Afectar</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtClavePresupuestal" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="50"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtClavePresupuestal" ErrorMessage="La clave presupuestal es obligatoria" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>                     

                     
                     


                     

                 </div>

                 <div class="col-md-3">


                     <div class="form-group">
                           <label for="FechaContrato">Fecha de Contrato</label>
                         <div>
                            <input type="text" class="required form-control date-picker" id="dtpContrato" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpContrato" ErrorMessage="La fecha del contrato es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>



                      <div class="form-group">
                           <label for="FechaInicio">Fecha de inicio</label>
                         <div>
                            <input type="text" class="required form-control date-picker" id="dtpInicio" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtpInicio" ErrorMessage="La fecha de inicio de obra es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>

                      <div class="form-group">
                           <label for="FechaTermino">Fecha de Término</label>                          
                         <div>
                            <input type="text" class="input-sm required form-control date-picker" id="dtpTermino" runat="server" data-date-format = "dd/mm/yyyy" autocomplete="off" />
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="dtpTermino" ErrorMessage="La fecha de término de obra es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>

                     
                      <div class="form-group">
                           <label for="nombreAfianzadora">Nombre de Afianzadora</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtNombreAfianzadora" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="50"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtNombreAfianzadora" ErrorMessage="El nombre de la afianzadora es obligatoria" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>

                     
                      <div class="form-group">
                           <label for="fianza">Número de Fianza</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtFianza" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="50"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtFianza" ErrorMessage="El número de fianza es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>


                     <div class="form-group">
                           <label for="fianzaCumplimiento">Número de Fianza de Cumplimiento</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtFianzaCumplimiento" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="50"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtFianzaCumplimiento" ErrorMessage="El número de fianza de cumplimiento es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>


                     



                  </div>


                <div class="col-md-3">      
                    
                     

                      <div class="form-group">
                           <label for="PorcentajeAnticipo">Porcentaje de Anticipo</label>
                         <div>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtPorcentajeAnticipo" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="50"/>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPorcentajeAnticipo" ErrorMessage="El porcentaje de anticipo es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>
                     
                     <div class="form-group">
                           <label for="desc5almillar">Aplicar 5 al millar</label>
                         <div>
                            <input type="checkbox" class="input-sm required form-control campoNumerico" id="chk5almillar" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="100"/>
                        </div>
                      </div>


                     <div class="form-group">
                           <label for="desc2almillar">Aplicar 2 al millar</label>
                         <div>
                            <input type="checkbox" class="input-sm required form-control campoNumerico" id="chk2almillar" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="100"/>
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="desc2almillarSV">Aplicar 2 al millar (Sup. y Vig.)</label>
                         <div>
                            <input type="checkbox" class="input-sm required form-control campoNumerico" id="chk2almillarSV" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="100"/>
                        </div>
                      </div>                  



                     <div class="form-group">
                           <label for="txtImporteTotal">Importe Contratado</label>
                         <div class="input-group">
                            <input type="text" disabled="disabled" class="input-sm required form-control campoNumerico" id="txtImporteTotal" runat="server" style="text-align: left; align-items:flex-start"  />
                        </div>
                      </div>
                    
                    
                                                   
                    <div class="form-group"  id="divBtnGuardarContrato" runat="server">
                        <asp:Button  CssClass="btn btn-primary" Text="Guardar Datos del Contrato" ID="btnGuardarContrato" runat="server" AutoPostBack="false" OnClick ="btnGuardarContrato_Click" ValidationGroup="validateX" />                        
                    </div>
                </div>

            </div>            

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />


    </div>
</asp:Content>

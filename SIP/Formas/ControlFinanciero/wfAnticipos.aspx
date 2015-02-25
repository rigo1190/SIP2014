<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfAnticipos.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfAnticipos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 



    <script type="text/javascript">

        $(document).ready(function () {
            $('.campoNumerico').autoNumeric('init');
        });

        function fnc_Abrir() {

        }

        function fnc_AbrirReporte() {

            var izq = (screen.width - 1000) / 2
            var sup = (screen.height - 600) / 2
            var param = $("#<%= _idObra.ClientID %>").val(); 

            var argumentos = "?c=" + 31 + "&p=" + param;

            url = $("#<%= _URLVisor.ClientID %>").val();
                
        url += argumentos;
        window.open(url, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=1000,height=500,top=' + sup + ',left=' + izq);
    }



</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container">
   <div class="panel panel-success">
        <div class="panel-heading">

            <div class="row">
                <div class="col-md-4"><h3 class="panel-title">Anticipo</h3></div>
                <div class="col-md-6"> . </div>
                <div class="col-md-2"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfAvanceFisicoFinanciero.aspx") %>">Regresar</a></div>
             </div>
        </div>
    </div>




    <div class="row">

                    
                 <div class="col-md-4">

                      <div class="form-group">
                           <label for="Numero">Número de Obra</label>
                         <div>
                            <input type="text" disabled="disabled"  class="input-sm required form-control" id="txtNumObra" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                         </div>
                      </div>
                     
                     


                      <div class="form-group">
                           <label for="NombreObra">Descripción de Obra</label>
                         <div>
                            <textarea id="txtDescripcionObra" disabled="disabled" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="3" ></textarea>
                         </div>
                      </div>
                     

                     <div class="form-group">
                         <label for="RFC">RFC contratista</label>
                         <div>
                            <input type="text" disabled="disabled" class="input-sm required form-control" id="txtRFC" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                         </div>
                      </div>


                     <div class="form-group">
                         <label for="RazonSocial">Razón Social Contratista</label>
                         <div>
                            <input type="text" disabled="disabled" class="input-sm required form-control" id="txtRazonSocial" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                         </div>
                      </div>


                    

                 </div>

                <div class="col-md-4">
                     <div class="form-group">
                           <label for="ImporteContratado">Importe Contratado</label>
                         <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <input type="text" disabled="disabled"  class="input-sm required form-control campoNumerico" id="txtImporteContratado" runat="server" style="text-align: left; align-items:flex-start" data-m-dec="0"   />
                         </div>
                     </div>


                     <div class="form-group">
                         <label for="Porcentaje">Porcentaje de Anticipo</label>
                         <div>
                            <input type="text" disabled="disabled" class="input-sm required form-control" id="txtPorcentaje" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                         </div>
                      </div>

                    <div class="form-group">
                           <label for="Anticipo">Anticipo</label>
                         <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <input type="text" disabled="disabled" class="input-sm required form-control campoNumerico" id="txtImporteAnticipo" runat="server" style="text-align: left; align-items:flex-start" data-m-dec="0"  />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtImporteAnticipo" ErrorMessage="El Importe del anticipo es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>

                </div>
                
                 <div class="col-md-4">


                     <div class="form-group">
                           <label for="Folio">Número de Folio</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtFolio" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFolio" ErrorMessage="El número de Folio es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>



                     <div class="form-group">
                           <label for="Fecha">Fecha</label>
                         <div>
                            <input type="text" class="required form-control date-picker" id="dtpFecha" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpFecha" ErrorMessage="La fecha es obligatoria" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>


 

                       

                     
                     <div class="form-group"  id="divBtnGuardarAnticipo" runat="server">
                        <asp:Button   CssClass="btn btn-primary"  Text="Guardar Datos del Anticipo" ID="btnGuardarAnticipo" runat="server" AutoPostBack="false" OnClick ="btnGuardarAnticipo_Click" ValidationGroup="validateX" />                        
                     </div>

                     <div class="form-group"  id="divBtnImprimir" runat="server">
                            <asp:Button   CssClass="btn btn-success"  Text="Imprimir Orden de pago" ID="btnImprimir" runat="server" AutoPostBack="false" OnClientClick="fnc_AbrirReporte()"  />                        
                     </div>


                  </div>

                    
                                                   
                    
                

            </div>            

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />


                <div style="display:none" runat="server">
                    <input type="hidden" runat="server" id="_URLVisor" />                    
                    <input type="hidden" runat="server" id="_idObra" />                    
                </div>


</div>
    </asp:Content>

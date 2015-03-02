<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfEstimacionesNew.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfEstimacionesNew" %>

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

            //var pa1ram = idUnidad + "-" + idEjercicio;

            param = param + "-" + $("#<%= _idUsuario.ClientID %>").val();

            var argumentos = "?c=" + 32 + "&p=" + param;

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
                <div class="col-md-4"><h3 class="panel-title">Registrar Nueva Estimación</h3></div>
                <div class="col-md-6"> . </div>
                <div class="col-md-2"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfAvanceFisicoFinanciero.aspx") %>">Regresar</a></div>
             </div>
        </div>
    </div>




    <div class="row">
        
        <div class="col-md-12">

            <div id="divCargarArchivo" class="row" runat="server">


                <div class="col-md-4">
                    <label>Archivo</label>
                    <asp:FileUpload ID="fileUpload" runat="server" />
                </div>


                                                
                <div class="col-md-4" runat="server"  id="divBtncargarConceptos">
                    <asp:Button  CssClass="btn btn-default" Text="Mostrar Datos" ID="btnCargarConceptos" runat="server" AutoPostBack="false" OnClick="btnCargarConceptos_Click" />
                </div>
            

             </div>

            <div class="form-group"  id="divBtnImprimir" runat="server">
                    <asp:Button   CssClass="btn btn-success"  Text="Ver listado de conceptos a registrar" ID="btnImprimir" runat="server" AutoPostBack="false" OnClientClick="fnc_AbrirReporte()"  />                        
            </div>


             <div id="divTMP" runat ="server" class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">Estimación a registrar...</h3>
                </div> 
            


                <div class="row">
  

                    <div class="col-md-4">
                        
                        <div class="row">
                            <div class="col-md-6">
                               <a>Número de Estimación</a> <asp:textbox width="180px" id="txtNumEstimacion" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>
                               <a>Amortización del Anticipo</a> <asp:textbox width="180px" id="txtAmortizacion" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>
                            </div>

                            <div class="col-md-6">    
                               <a>Importe Estimado</a> <asp:textbox width="180px" id="txtImporteEstimado" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>
                               <a>Retención del 5 al Millar</a> <asp:textbox width="180px" id="txtRetencion5" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>
                            </div>
                        </div>
          
                    </div>
                
                    <div class="col-md-4">
                        <div class="row">
                            <div class="col-md-6">
                                <a>IVA</a><asp:textbox width="180px" id="txtIVA" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>
                                <a>Retención del 2 al Millar</a> <asp:textbox width="180px" id="txtRetencion2" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>
                            </div>
                            <div class="col-md-6">
                                <a>Importe Total</a> <asp:textbox width="180px" id="txtImporteTotal" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>
                                <a>Ret. 2 al Millar Sup. y Vig.</a> <asp:textbox width="180px" id="txtRet2Bis" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>
                            </div>
                        </div>
                    </div>


                    <div class="col-md-4">
                    


                        <div class="row">
                            <div class="col-md-6">

                                <a>Fecha</a> <input type="text" class="required form-control date-picker" id="dtpFecha" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpFecha" ErrorMessage="La fecha es obligatoria" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                            
                                <a>Importe a pagar</a> <asp:textbox width="180px" id="txtImporteApagar" text="0" cssclass="form-control" runat="server" disabled="disabled"></asp:textbox>                                                                                          
                                    
                              </div>


                            
                            
                            <div class="col-md-6">
                                

                                 

                     
                             <div class="form-group"  id="divBtnGuardarAnticipo" runat="server">
                                <asp:Button   CssClass="btn btn-primary"  Text="Guardar Estimación" ID="btnGuardarEstimacion" runat="server" AutoPostBack="false"  ValidationGroup="validateX" OnClick ="btnGuardarEstimacion_Click" />                        
                             </div>



                            </div>
                        </div>




                     

 

                  </div>
                    
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />

                </div>          

                 <br />

                 

                 
                 <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server"  >
                    <Columns>
                              
                        <asp:TemplateField HeaderText="No" SortExpression="No" >                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblNo" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  Width="100px"  />
                        </asp:TemplateField>
                                        
                        <asp:TemplateField HeaderText="Inciso" SortExpression="Inciso">                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblInciso" runat="server" Text='<%# Bind("Inciso") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Concepto" SortExpression="Concepto">                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="U.M." SortExpression="U.M.">                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblUM" runat="server" Text='<%# Bind("UnidadDeMedida") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cantidad Contratada">                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("CantidadContratada") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cantidad Estimaciones Anteriores">                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblCantidad2" runat="server" Text='<%# Bind("CantidadEstimadaAcumulada") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Cantidad a Registrar">                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblCantidad3" runat="server" Text='<%# Bind("Cantidad") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("StatusNombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="N Dias de Desfase">                    
                            <ItemTemplate>                            
                                    <asp:Label ID="lblStatusFechas" runat="server" Text='<%# Bind("StatusFechasNombre") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    
                
                    
            </asp:GridView>
        

            </div>


            <div runat="server" style="display:none">
                <input type="hidden" runat="server" id="_R" />        
            
                <input type="hidden" runat="server" id="_URLVisor" />                    
                <input type="hidden" runat="server" id="_idObra" />                    
                <input type="hidden" runat="server" id="_idUsuario" />

            </div>




        </div>
    </div>

</div>
</asp:Content>

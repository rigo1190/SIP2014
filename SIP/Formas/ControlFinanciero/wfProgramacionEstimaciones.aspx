<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfProgramacionEstimaciones.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfProgramacionEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">



        function fnc_ConfirmarEliminarEstimacion() {
            return confirm("¿Está seguro de eliminar la última estimación programada?");
        }


        function fnc_ConfirmarGuardarEstimaciones() {
            return confirm("¿Está seguro de concluir la fase de programar las estimaciones de la obra?");
        }


    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container">

    <div class="panel panel-success">
        <div class="panel-heading">
            <div class="row">
                <div class="col-md-2"><h3 class="panel-title">Programa de Obra</h3></div>
                
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfContrato.aspx") %>" >A) Contrato</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfPresupuestoContratado.aspx") %>" >B) Presupuesto Contratado</a> </div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfProgramaDeObra.aspx") %>" >C) Programa de Obra</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfProgramacionEstimaciones.aspx") %>" >D) Programación de Estimaciones</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfContratosDeObra.aspx") %>" >Regresar</a></div>
             </div>   
        </div>
    </div>


    <div class="panel panel-warning" runat="server" id="divMSGnoHayProgramaDeObra">
        <div class="panel-heading">
            <div class="row">
                <div class="col-md-10"><h3 class="panel-title">Antes de programar las estimaciones es necesario cargar el programa de obra, para tomarlo como base.</h3></div>
            </div>
        </div>
    </div>


    <div id="divTMP" runat ="server" class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title">Fechas de términos del programa de obra</h3>
            </div>
                
            <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server"  >
                <Columns>
                              
                    <asp:TemplateField HeaderText="No">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="100px"  />
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Fecha">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("Fecha","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cantidad de Conceptos">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblNConceptos" runat="server" Text='<%# Bind("NConceptos") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subtotal">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblSubtotal" runat="server" Text='<%#  Bind("Subtotal","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IVA">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblIVA" runat="server" Text='<%# Bind("IVA","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Amortización Anticipo">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblAnticipo" runat="server" Text='<%# Bind("Anticipo","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Retención 5 al millar">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblRet5" runat="server" Text='<%# Bind("Ret5","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Retención 2 al millar">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblRet2" runat="server" Text='<%# Bind("Ret2","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Retención 2 al millar Vig y Sup">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblRet2Bis" runat="server" Text='<%# Bind("Ret2Bis","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Importe Final">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblImporteFinal" runat="server" Text='<%# Bind("ImporteFinal","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                    
                
                    
        </asp:GridView>

        </div>

    <div id="divGuardarEstimacionesProgramadas" class="row" runat="server">
            <div class="col-md-4" >.</div>
            <div class="col-md-4" >
                <asp:Button  CssClass="btn btn-primary" Text="Guardar Programación de Estimaciones" ID="btnGuardarProgramaDeEstimaciones" runat="server" AutoPostBack="false" OnClick="btnGuardarProgramaDeEstimaciones_Click" OnClientClick="return fnc_ConfirmarGuardarEstimaciones()" />
            </div>
            <div class="col-md-4" >.</div>
        </div>


    <div id="divAgregarEstimacion" class="row" runat ="server">
              <label>Agregar</label>

             <div class="row">
                <div class="col-md-4">
                     <div class="form-group">
                           <label for="Numero">Número de Estimación</label>
                         <div>
                            <input type="text" disabled="disabled"  class="input-sm required form-control" id="txtNoEstimacion" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                         </div>
                      </div>
                </div>

                <div class="col-md-4">
                    <div class="form-group">
                           <label for="Fecha">Fecha</label>
                         <div>
                            <input type="text" class="required form-control date-picker" id="dtpFecha" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtpFecha" ErrorMessage="La fecha es obligatoria" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        </div>
                      </div>
                </div>                


                 <div class="col-md-4">
                     <div class="form-group">
                          <label for="Numero">.</label>
                         <div>
                        <asp:Button   CssClass="btn btn-primary"  Text="Agregar Estimación" ID="btnAddEstimacion" runat="server" AutoPostBack="false" OnClick ="btnAddEstimacion_Click" ValidationGroup="validateX" />                        
                             </div>
                     </div>
                </div>
            
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />
             </div>   


                      


 

                       

                     
                     



    </div>
            


    <div id="divEstimacionesProgramadas" runat ="server" class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title">Estimaciones Programadas ...</h3>
            </div>
                
            <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridEstimaciones" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" OnRowDataBound="gridEstimaciones_RowDataBound"  >
                <Columns>
                              
                    <asp:TemplateField HeaderText="No">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NumeroDeEstimacion") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="100px"  />
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Fecha">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblFecha" runat="server" Text='<%# Bind("FechaDeEstimacion","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

    
                    <asp:TemplateField HeaderText="Subtotal">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblSubtotal" runat="server" Text='<%#  Bind("ImporteEstimado","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="IVA">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblIVA" runat="server" Text='<%# Bind("IVA","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("Total","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Amortización Anticipo">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblAnticipo" runat="server" Text='<%# Bind("AmortizacionAnticipo","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Retención 5 al millar">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblRet5" runat="server" Text='<%# Bind("Retencion5AlMillar","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Retención 2 al millar">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblRet2" runat="server" Text='<%# Bind("Retencion2AlMillar","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Retención 2 al millar Sup y Vig">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblRet2bis" runat="server" Text='<%# Bind("Retencion2AlMillarSyV","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Importe Final">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblImporteFinal" runat="server" Text='<%# Bind("ImporteAPagar","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>                                           
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClientClick="return fnc_ConfirmarEliminarEstimacion()" OnClick="imgBtnEliminar_Click"/>                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="70px" BackColor="#EEEEEE" />
                    </asp:TemplateField>  

                </Columns>
                    
                
                    
        </asp:GridView>

        </div>


</div>

</asp:Content>

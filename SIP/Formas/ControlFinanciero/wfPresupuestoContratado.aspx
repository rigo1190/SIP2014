<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfPresupuestoContratado.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfPresupuestoContratado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="panel panel-success">
        <div class="panel-heading">
            <div class="row">
                <div class="col-md-2"><h3 class="panel-title">Presupuesto Contratado</h3></div>
                
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfContrato.aspx") %>" >A) Contrato</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfPresupuestoContratado.aspx") %>" >B) Presupuesto Contratado</a> </div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfProgramaDeObra.aspx") %>" >C) Programa de Obra</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfProgramacionEstimaciones.aspx") %>" >D) Programación de Estimaciones</a></div>
                <div class="col-md-2 text-center"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfContratosDeObra.aspx") %>" >Regresar</a></div>

             </div>   
        </div>
    </div>


        <div class="panel panel-warning" runat="server" id="divMSGnoHayContrato">
        <div class="panel-heading">
            <div class="row">
                <div class="col-md-10"><h3 class="panel-title">Antes de registrar el presupuesto contratado es necesario registrar los datos del contrato </h3></div>
            </div>
        </div>
    </div>


        <div id="divCargarArchivo" class="row" runat="server">


            <div class="col-md-4">
                <label>Archivo</label>
                <asp:FileUpload ID="fileUpload" runat="server" />
            </div>


                                                
            <div class="col-md-4" runat="server"  id="divBtnMostrarDatosExcel">
                <asp:Button  CssClass="btn btn-default" Text="Mostrar Datos" ID="btnCargarConceptosDeObra" runat="server" AutoPostBack="false" OnClick="btnCargarConceptosDeObra_Click" />
            </div>


            


         </div>


        <div id="divGuardarPresupuesto" class="row" runat="server">
            <div class="col-md-4" >.</div>
            <div class="col-md-4" >
                <asp:Button  CssClass="btn btn-primary" Text="Guardar Presupuesto Contratado" ID="btnGuardarPresupuesto" runat="server" AutoPostBack="false" OnClick="btnGuardarPresupuestoContratado_Click" />
            </div>
            <div class="col-md-4" >.</div>
        </div>

    
        <div id="divTMP" runat ="server" class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title">Presupuesto Contratado a Registrar</h3>
            </div>
                
            <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" >
                <Columns>
                              
                    <asp:TemplateField HeaderText="No" SortExpression="No" >                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("No") %>'></asp:Label>
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
                                <asp:Label ID="lblConcepto" runat="server" Text='<%# Bind("Concepto") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="U.M." SortExpression="U.M.">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblUM" runat="server" Text='<%# Bind("UM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Cantidad" SortExpression="Cantidad">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblCantidad" runat="server" Text='<%# Bind("Cantidad") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Precio Unitario" SortExpression="Precio Unitario">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblPrecio" runat="server" Text='<%# Bind("Precio") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Subtotal" SortExpression="Subtotal">                    
                        <ItemTemplate>                            
                                <asp:Label ID="lblSubtotal" runat="server" Text='<%# Bind("Subtotal") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
                    
                
                    
        </asp:GridView>

        </div>
    
        
        <div id="divPresupuesto" class="row" runat ="server">
            <label>presupuesto contratado</label>




            <div class="bs-example">
            <div class="panel-group" id="accordion" runat="server">

                

            </div>
            </div>


        </div>


        <div runat="server" style="display:none">
            <input type="hidden" runat="server" id="_R" />        
        </div>


</div>

</asp:Content>

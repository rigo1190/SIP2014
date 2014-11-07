<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfTechoFinancieroConsultaXup.aspx.cs" Inherits="SIP.Formas.TechoFin.wfTechoFinancieroConsultaXup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    
     <div class="row panel-footer alert alert-success">
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Techos Financieros X Unidad Presupuestal"></asp:Label>
        </div>
        <div class="col-md-4">
            
        </div>

        <div class="col-md-4">
            
        </div>
    </div>


    <ul class="nav nav-tabs" role="tablist">
              <li class="active"><a href="#divGrupo" role="tab" data-toggle="tab">Mostrar Concentrado</a></li>
              <li><a href="#divDetalle" role="tab" data-toggle="tab">Mostrar Detalle</a></li>        
            </ul>

    <div class="tab-content">

        <div class="tab-pane active" id="divGrupo">
            <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid2" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                    
                    

                    <asp:TemplateField HeaderText="Unidad Presupuestal" SortExpression="Fondo">
                        <ItemTemplate>
                            <asp:Label ID="lblUP" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="400px"  />
                    </asp:TemplateField>
                            
                     
                    <asp:TemplateField HeaderText="Techo Financiero">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Importe")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Asignado a Obras">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ImporteAsignado")).ToString("c") %>                               
                        </ItemTemplate>                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ejecutado">
                        <ItemTemplate>
                            $000.00
                        </ItemTemplate>                        
                    </asp:TemplateField>


                      

                </Columns>
                    
                
                    
        </asp:GridView>
        </div>
        <div class="tab-pane" id="divDetalle">
            <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                    
                                                    


                    <asp:TemplateField HeaderText="Unidad Presupuestal" SortExpression="Fondo">
                        <ItemTemplate>
                            <asp:Label ID="lblUP" runat="server" Text='<%# Bind("UnidadPresupuestal.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="400px"  />
                    </asp:TemplateField>
                            
                    <asp:TemplateField HeaderText="Unidad Presupuestal" SortExpression="Fondo">
                        <ItemTemplate>
                            <asp:Label ID="lblUP" runat="server" Text='<%# Bind("Financiamiento") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="200px"  />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Techo Financiero">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Importe")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Asignado a Obras">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ImporteAsignado")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Ejecutado">
                        <ItemTemplate>
                            $000.00
                        </ItemTemplate>                        
                    </asp:TemplateField>


                      

                </Columns>
                    
                
                    
        </asp:GridView>
        </div>
    </div>

    
</asp:Content>


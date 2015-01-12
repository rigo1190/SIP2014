<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfContratosDeObra.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfContratosDeObra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True" OnRowDataBound="grid_RowDataBound" >
                <Columns>
                                  
                    <asp:TemplateField HeaderText="Numero">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="100px"  />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Descripcion" SortExpression="Descripcion">
                        <ItemTemplate>
                            <asp:Label ID="labelNombre" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Número de Contrato" SortExpression="Número de Contrato">
                        <ItemTemplate>                           

                        <div id = "DIVcontrato" runat="server">
                            <asp:LinkButton ID="linkContrato" runat="server" PostBackUrl="#" OnClick ="linkContrato_Click">Pendiente</asp:LinkButton>  
                        </div>
                        </ItemTemplate>
                    </asp:TemplateField>    
                    
                    
                    <asp:TemplateField HeaderText="Presupuesto Contratado" SortExpression="Presupuesto Contratado">
                        <ItemTemplate>                           

                        <div id = "DIVPresupuestoContratado" runat="server">
                        <asp:LinkButton ID="LinkPresupuesto" runat="server" PostBackUrl="#" OnClick ="LinkPresupuesto_Click">Pendiente</asp:LinkButton>  
                        </div>
                        </ItemTemplate>
                    </asp:TemplateField>    
                     

                     
                     

                </Columns>
                    
                
                    
        </asp:GridView>

     

</asp:Content>


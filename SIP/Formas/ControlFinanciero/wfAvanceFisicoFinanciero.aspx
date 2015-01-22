<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfAvanceFisicoFinanciero.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfAvanceFisicoFinanciero" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
<div class="container">


       <div class="panel panel-success">
        <div class="panel-heading">

            <div class="row">
                <div class="col-md-4"><h3 class="panel-title">Avance Físico - Financiero</h3></div>
                <div class="col-md-4"> . </div>
                <div class="col-md-4"> . </div>
             </div>
        </div>
    </div>

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


                    
                    
                    
                    <asp:TemplateField HeaderText="Avance Físico" SortExpression="Avance Físico">
                        <ItemTemplate>                           

                        <div id = "DIVAvanceFisico" runat="server">
                        
                        </div>
                        

                        <div class="progress">
                            <div id="ProgresoA" runat="server" class="progress-bar progress-bar-success progress-bar-striped" role="progressbar" style="width:0%;"><span class="sr-only">.s..</span></div>
                        </div>

                        
                        </ItemTemplate>
                    </asp:TemplateField>    
                     
                    <asp:TemplateField HeaderText="Avance Financiero" SortExpression="Avance Financiero">
                        <ItemTemplate>                           

                        <div id = "DIVAvanceFinanciero" runat="server">
                        
                        </div>
                        
                        <div class="progress">
                            <div id="ProgresoB" runat="server" class="progress-bar progress-bar-success progress-bar-striped" role="progressbar" style="width:0%;"><span class="sr-only">.s..</span></div>
                        </div>

                        </ItemTemplate>
                    </asp:TemplateField>  
                     


                    <asp:TemplateField HeaderText="Anticipo" SortExpression="Anticipo">
                        <ItemTemplate>                           

                        <div id = "DIVanticipo" runat="server">
                            <asp:LinkButton ID="linkAnticipo" runat="server" PostBackUrl="#" OnClick="linkAnticipo_Click" >Pendiente</asp:LinkButton>  
                        </div>
                        </ItemTemplate>
                    </asp:TemplateField>    

                    <asp:TemplateField HeaderText="Estimaciones" SortExpression="Estimaciones">
                        <ItemTemplate>                           

                        <div id = "DIVestimaciones" runat="server">
                            <asp:LinkButton ID="linkEstimaciones" runat="server" PostBackUrl="#"  OnClick ="linkEstimaciones_Click">Pendiente</asp:LinkButton>  
                        </div>
                        </ItemTemplate>
                    </asp:TemplateField>    


                </Columns>
                    
                
                    
        </asp:GridView>
</div>
</asp:Content>

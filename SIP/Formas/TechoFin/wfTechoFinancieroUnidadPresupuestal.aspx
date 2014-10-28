﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfTechoFinancieroUnidadPresupuestal.aspx.cs" Inherits="SIP.Formas.TechoFin.wfTechoFinancieroUnidadPresupuestal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<div id="divEjercicioCerrado" class="panel panel-success" runat="server">
        <div class="panel-heading">
            <h3 class="panel-title">El Techo Financiero esta cerrado a su captura</h3>
        </div>
    </div>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    



    <div class="row panel-footer alert alert-success">
        <div class="col-md-4">
            <asp:Label ID="lblUPnombre" runat="server" Text=""></asp:Label>
            <br />
            Techo Financiero : <asp:Label ID="lblTechoFinanciero" runat="server" Text=""></asp:Label>
            <br />
            Importe Asignado : <asp:Label ID="lblImporteAsignado" runat="server" Text=""></asp:Label>
            <br />
            Importe Disponible : <asp:Label ID="lblImporteDisponible" runat="server" Text=""></asp:Label>    
        </div>

        <div class="col-md-4 text-center">.<br />
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-md-4 text-right">.<br />
            
                <a href="<%=ResolveClientUrl("~/Formas/TechoFin/wfTechoFinanciero.aspx") %>">Regresar a los Techos Financieros</a>                                    
             
            
        </div>
    </div>





    <div id="divMsgSuccess" class="panel-footer alert alert-success"  runat="server">
    <asp:Label ID="lblMensajeSuccess" runat="server" Text=""></asp:Label>
    </div>
    <div id="divMsgFail" class="panel-footer alert alert-danger" runat="server">
    <asp:Label ID="lblMensajeFail" runat="server" Text=""></asp:Label>
    </div>
    




    <div class ="row" id="divAdd" runat="server">
        <div class="col-md-6">
            <label>Unidad Presupuestal</label>
            <asp:DropDownList ID="ddlUP" CssClass="form-control" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label>Importe</label><br />
            <input type="text" class="input-sm required" id="txtImporte" runat="server" style="text-align: left; width:300px;  align-items:flex-start" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImporte" ErrorMessage="El campo Importe es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtImporte" ErrorMessage="El campo Importe debe ser un valor númerico" ValidationGroup="validateX" MaximumValue="999999999999" MinimumValue="0" Type="Double">*</asp:RangeValidator>


        </div>
        <div class="col-md-2">
            <asp:Button  CssClass="btn btn-default" Text="Agregar Registro" ID="btnAdd" runat="server" AutoPostBack="false" OnClick ="btnAdd_Click" ValidationGroup="validateX" />
        </div>


        <div style="display:none" runat="server">
            <asp:TextBox ID="_ElId" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
            <asp:TextBox ID="_StatusEjercicio" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
        </div>


    </div>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" />




    <div class="panel panel-success">
        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick="imgBtnEliminar_Click" />                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>                                  


                    <asp:TemplateField HeaderText="UnidadPresupuestal" SortExpression="UnidadPresupuestal">
                        <EditItemTemplate>
                            <asp:TextBox CssClass="input-sm" ID="txtUnidadPresupuestal" runat="server" Text='<%# Bind("UnidadPresupuestal.Nombre") %> '></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="labelUnidadPresupuestal" runat="server" Text='<%# Bind("UnidadPresupuestal.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="900px"  />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Importe" SortExpression="Importe">
                        <EditItemTemplate>
                            <asp:TextBox CssClass="input-sm" ID="txtImporte" runat="server" Text='<%# Bind("Importe") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="labelImporte" runat="server" Text='<%# Bind("Importe") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    

                    
                    

                </Columns>
                    
                
                    
        </asp:GridView>
    </div>





</asp:Content>

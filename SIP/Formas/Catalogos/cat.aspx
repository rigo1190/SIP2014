<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="cat.aspx.cs" Inherits="SIP.Formas.Catalogos.cat" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="txtClave" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCantidad" ErrorMessage="Este campo es obligatorio" ValidationGroup="snoopy">*</asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre" ErrorMessage="El campo nombre es obligatorio" ValidationGroup="snoopy">*</asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ok" ValidationGroup="snoopy" />
    <br />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="snoopy" />
</asp:Content>

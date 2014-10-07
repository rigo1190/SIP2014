<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorCatalogos.Master" AutoEventWireup="true" CodeBehind="Ejercicios.aspx.cs" Inherits="SIP.Formas.Catalogos.Ejercicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function fnc_OcultarDivs(sender) {
            $("#<%= divBtnNuevo.ClientID %>").css("display", "block");
            $("#<%= divEdicion.ClientID %>").css("display", "none");
            return false;
        }


        function fnc_Confirmar() {
            return confirm("¿Está seguro de eliminar el registro?");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="divCatalogo">

                <div class="panel panel-success">

                    <div class="panel-heading">
                        <h3 class="panel-title">Catálogo de Ejercicios</h3>
                    </div>

                    <div class="panel-body"> 
                        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True" OnRowDataBound="grid_RowDataBound" OnPageIndexChanging="grid_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate>
                                                    
                                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" />
                                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClientClick="return fnc_Confirmar()" OnClick="imgBtnEliminar_Click"/>

                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#EEEEEE" />
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Año" SortExpression="Año">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Año") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activo" SortExpression="Orden">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivo" runat="server" Text='<%# Bind("Activo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                </Columns>
                    
                                <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                    
                        </asp:GridView>
                        <div id="divBtnNuevo" runat="server">
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default" OnClick="btnNuevo_Click" AutoPostBack="false" />
                        </div>
                         <div class="row"> 
                            <div id="divEdicion" runat="server" class="panel-footer">
                                <div class="row top-buffer">
                                    <div class="col-md-2">
                                        <label for="Año">Año</label>
                                    </div>
                                    <div class="col-md-6">
                                        <input type="text" class="form-control input-sm required numeric" id="txtAnio" runat="server" style="text-align: left; width:500px; align-items:flex-start" />
                                    </div>
                                </div>
                                <div class="row top-buffer">
                                     <div class="col-md-2">
                                        <p class="text-right"><strong>Activo</strong></p>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:CheckBox ID="chkActivo" runat="server" Font-Bold="True"/>
                                    </div>
                                </div>
                                 <div class="form-group">
                                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnCrear" runat="server" OnClick="btnCrear_Click" AutoPostBack="false" />
                                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" OnClientClick="return fnc_OcultarDivs()" AutoPostBack="false" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div style="display:none" runat="server">
                        <asp:TextBox ID="_Anio" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                        <asp:TextBox ID="_Accion" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                                    
                    </div>

                    <div class="panel-footer" id="divMsg" style="display:none" runat="server">
                        <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
                    </div>

                </div>


            </div>
        </div>

        
    </div>
</asp:Content>

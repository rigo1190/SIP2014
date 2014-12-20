<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="Ejercicios.aspx.cs" Inherits="SIP.Formas.Catalogos.Ejercicios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">


        $(document).ready(function ()
        {
            $('.campoNumerico').autoNumeric('init');
        });

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
                        <h3 class="panel-title">Ejercicios</h3>
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
                                    <asp:TemplateField HeaderText="Estatus" SortExpression="Orden">
                                        <ItemTemplate>
                                            <asp:Label ID="lblActivo" runat="server" Text='<%# Bind("Estatus") %>'></asp:Label>
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


                                <div class="row">

                                    <div class="col-md-6">

                                         <div class="form-group">
                                         <label for="Anio">Año</label>
                                         <div>
                                            <input type="text" class="input-sm required form-control campoNumerico" id="txtAnio" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="9999"  data-m-dec="0" data-a-sep="" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnio" ErrorMessage="El campo AÑO no puede quedar en blanco." ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAnio" ErrorMessage="El campo AÑO debe estar en un rango de 2015-N" MaximumValue="2030" MinimumValue="2015" Type="Integer" ValidationGroup="validateX">*</asp:RangeValidator>                         
                                        </div>
                                    </div>

                                  <div class="form-group">
                                     <label for="Estatus">Estatus</label>
                                     <div>
                                        <asp:DropDownList ID="ddlEstatus" CssClass="form-control" runat="server"></asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlEstatus" ErrorMessage="Especifique el estatus del ejercicio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                                         <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ddlEstatus" ErrorMessage="Especifique el estatus del ejercicio" MaximumValue="3" MinimumValue="1" Type="Integer" ValidationGroup="validateX">*</asp:RangeValidator>                         
                                    </div>
                                  </div>




                                    </div>
                                    
                                   

                                </div><!-- row -->


                                <%--<div class="row top-buffer">
                                    <div class="col-md-2">
                                        <label for="Año">Año</label>
                                    </div>
                                    <div class="col-md-6">
                                        <input type="text" class="form-control input-sm required numeric" id="txtAnio" runat="server" style="text-align: left; width:500px; align-items:flex-start" />
                                        <input type="text" class="input-sm required form-control campoNumerico" id="txtAnio" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="9999"  data-m-dec="0" data-a-sep="" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAnio" ErrorMessage="El campo AÑO no puede quedar en blanco." ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAnio" ErrorMessage="El campo AÑO debe estar en un rango de 2015-N" MaximumValue="2030" MinimumValue="2015" Type="Integer" ValidationGroup="validateX">*</asp:RangeValidator>
                                    </div>
                                </div>--%>

                                <%--<div class="row top-buffer">
                                     <div class="col-md-2">
                                        <p class="text-right"><strong>Estatus</strong></p>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:CheckBox ID="chkActivo" runat="server" Font-Bold="True"/>
                                        <div>
                                            <asp:DropDownList ID="ddlEstatus" CssClass="form-control" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>--%>

                                 <div class="form-group">
                                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnCrear" runat="server" OnClick="btnCrear_Click" AutoPostBack="false" ValidationGroup="validateX" />
                                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" OnClientClick="return fnc_OcultarDivs()" AutoPostBack="false" />
                                     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" />
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

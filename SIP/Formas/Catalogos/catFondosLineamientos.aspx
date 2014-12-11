<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="catFondosLineamientos.aspx.cs" Inherits="SIP.Formas.Catalogos.catFondosLineamientos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">




       function fnc_limpiarCampos() {
           $("#<%= txtTipo.ClientID%>").val("");
           $("#<%= txtCalendario.ClientID%>").val("");
           $("#<%= txtVigencia.ClientID%>").val("");
           $("#<%= txtNormatividad.ClientID%>").val("");
           $("#<%= txtContraparte.ClientID%>").val("");

            return true;
        }

        function fnc_OcultarDivs(sender) {
            $("#<%= divBtnNuevo.ClientID %>").css("display", "block");
              $("#<%= divEdicion.ClientID %>").css("display", "none");
              $("#<%= divMsg.ClientID %>").css("display", "none");
              $("#<%= divMsgSuccess.ClientID %>").css("display", "none");

              fnc_limpiarCampos();


              return false;
          }

          function fnc_Confirmar() {
              return confirm("¿Está seguro de eliminar el registro?");
          }


          function fnc_EjecutarMensaje(mensaje) {
              alert(mensaje);
          }

    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


  <div class="panel-footer alert alert-success" id="divMsgSuccess" style="display:none" runat="server">
                <asp:Label ID="lblMensajeSuccess" runat="server" Text=""></asp:Label>
    </div>
    <div class="panel-footer alert alert-danger" id="divMsg" style="display:none" runat="server">
                <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
    </div>



    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title">Lineamientos de los Fondos</h3>
        </div>

        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True" OnPageIndexChanging="grid_PageIndexChanging"
        >
                <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                                                    
                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click"/>
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClientClick="return fnc_Confirmar()" OnClick="imgBtnEliminar_Click"/>
                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>                                  


                    

                    <asp:TemplateField HeaderText="Fondo" SortExpression="Fondo">
                        <ItemTemplate>
                            <asp:Label ID="labelFondo" runat="server" Text='<%# Bind("Fondo.Abreviatura") %>'></asp:Label>
                        </ItemTemplate>                    
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Tipo de Obras y Acciones">
                        <ItemTemplate>
                            <asp:Label ID="labelTipo" runat="server" Text='<%# Bind("TipoDeObrasYAcciones") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Calendario de Ingresos">
                        <ItemTemplate>
                            <asp:Label ID="labelCalendario" runat="server" Text='<%# Bind("CalendarioDeIngresos") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Normatividad Aplicable">
                        <ItemTemplate>
                            <asp:Label ID="labelNormatividad" runat="server" Text='<%# Bind("NormatividadAplicable") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Contraparte">
                        <ItemTemplate>
                            <asp:Label ID="labelContraparte" runat="server" Text='<%# Bind("Contraparte") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>




                </Columns>
                    
                <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                    
        </asp:GridView>

    </div>


    


    <div id="divBtnNuevo" runat="server">
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default" OnClientClick="return fnc_limpiarCampos()" OnClick="btnNuevo_Click" AutoPostBack="false" />
    </div>


    <div class="row " > 
        <div id="divEdicion" runat="server" class="panel-footer">

                <div class="panel panel-success">                
                    <div class="panel-heading">
                            <h3 class="panel-title">Indique los datos del registro</h3>
                    </div>
                </div>


                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="Fondo">Fondo</label>
                    </div>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlFondo" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                </div>


                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="Tipo">Tipo de Obras y Acciones</label>
                    </div>
                    <div class="col-md-8">
                        <textarea id="txtTipo" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" ></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTipo" ErrorMessage="El campo 'Tipo de Obras y Acciones' es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                    </div>
                </div>


                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="Calendario">Calendario de Ingresos</label>
                    </div>
                    <div class="col-md-8">
                        <textarea id="txtCalendario" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" ></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCalendario" ErrorMessage="El campo 'Calendario de Ingresos' es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="Vigencia">Vigencia de Pagos</label>
                    </div>
                    <div class="col-md-8">
                        <textarea id="txtVigencia" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" ></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtVigencia" ErrorMessage="El campo 'Vigencia de Pagos' es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                    </div>
                </div>


                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="Normatividad">Normatividad Aplicable</label>
                    </div>
                    <div class="col-md-8">
                        <textarea id="txtNormatividad" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" ></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNormatividad" ErrorMessage="El campo 'Normatividad Aplicable' es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-2">
                        <label for="Contraparte">Contraparte</label>
                    </div>
                    <div class="col-md-8">
                        <textarea id="txtContraparte" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" ></textarea>                        
                    </div>
                </div>


            

                                
                <div class="form-group">
                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnCrear" runat="server" OnClick="btnCrear_Click" AutoPostBack="false" ValidationGroup="validateX" />
                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" OnClientClick="return fnc_OcultarDivs()" AutoPostBack="false" />
                </div>

                <div style="display:none" runat="server">
                    <asp:TextBox ID="_ElId" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                    <asp:TextBox ID="_Accion" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                                    
                </div>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" ViewStateMode="Disabled" />

            </div>
    </div>


</asp:Content>

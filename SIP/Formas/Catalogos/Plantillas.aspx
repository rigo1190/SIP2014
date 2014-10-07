<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorCatalogos.Master" AutoEventWireup="true" CodeBehind="Plantillas.aspx.cs" Inherits="SIP.Formas.Catalogos.Plantillas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function fnc_OcultarDivs(sender) {
            $("#<%= divBtnNuevo.ClientID %>").css("display", "block");
            $("#<%= divCaptura.ClientID %>").css("display", "none");
            $("#<%= divGuardar.ClientID %>").css("display", "none");
            return false;
        }


        function fnc_Confirmar() {
            return confirm("¿Está seguro de eliminar el registro?");
        }

        function fnc_IrDesdeGrid(url) {
            //$(location).attr('href', 'PlantillaPreguntas.aspx?2');
            $(location).attr('href', url);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="container">
        <div class="row">
            <div id="divPlantilla" runat="server">
                <div class="panel panel-success">
                    <div class="panel-heading">
                            <h3 id="titulo" runat="server" class="panel-title">Plantillas</h3>
                    </div>
                     <div class="panel-body">
                    <div class="row">
                       <asp:GridView ShowHeaderWhenEmpty="true" CssClass="table" Width="100%" OnRowDataBound="grid_RowDataBound" ShowFooter="true" AutoGenerateColumns="false" ID="grid" DataKeyNames="Id"  runat="server">
                           <Columns>
                               <asp:TemplateField HeaderText="Acciones">
                                        <ItemTemplate> 
                                                    
                                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" />
                                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClientClick="return fnc_Confirmar()" OnClick="imgBtnEliminar_Click"/>
                                            
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#EEEEEE" />
                                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                                        <FooterTemplate>
                                            <asp:Label ID="label" Font-Bold="true" runat="server" Text="Total" />
                                        </FooterTemplate>
                                </asp:TemplateField>
                               
                               <asp:TemplateField HeaderText="Ejercicio">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Ejercicio.Año") %>
                                    </ItemTemplate>                    
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Clave">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "Clave") %>
                                    </ItemTemplate>                    
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                             <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                        </ItemTemplate>          
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Descripción">
                                        <ItemTemplate>
                                             <%# DataBinder.Eval(Container.DataItem, "Orden") %>
                                        </ItemTemplate>          
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Preguntas Plantilla">
                                        <ItemTemplate>
                                            <button type="button" id="btnPP" onclick="fnc_IrDesdeGrid()" runat="server" class="btn btn-default"> <span class="glyphicon glyphicon-book"></span></button>
                                        </ItemTemplate>   
                                        <HeaderStyle HorizontalAlign="Center" BackColor="#EEEEEE" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />                       
                                </asp:TemplateField>

                           </Columns>
                            <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                       </asp:GridView>
                    </div>
                    <div id="divBtnNuevo" runat="server">
                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default" OnClick="btnNuevo_Click" AutoPostBack="false" />
                        
                    </div>
                    <div id="divCaptura" style="display:none" runat="server">
                        <div class="row top-buffer">
                            <div class="col-md-2">
                                <p class="text-right"><strong>Clave</strong></p>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox Width="180px" ID="txtClave" Text="0" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row top-buffer">
                            <div class="col-md-2">
                                <p class="text-right"><strong>Descripción</strong></p>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox Width="180px" ID="txtDescripcion" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row top-buffer" runat="server" id="div1">
                            <div class="col-md-2">
                                <p class="text-right"><strong>Ejercicio</strong></p>
                            </div>
                            <div class="col-md-6">
                                <asp:DropDownList Width="180px" ID="ddlEjercicio" CssClass="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="row top-buffer">
                            <div class="col-md-2">
                                <p class="text-right"><strong>Orden</strong></p>
                            </div>
                            <div class="col-md-6">
                                <input type="text" class="input-sm digits" id="txtOrden" runat="server" />
                            </div>
                        </div>

                    </div>
                </div>
                    <div class="panel-footer" id="divGuardar" style="display:none" runat="server">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-default" />
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" AutoPostBack="false" OnClientClick="return fnc_OcultarDivs()" />
                    
                </div> 

                <div class="panel-footer" id="divMsg" style="display:none" runat="server">
                    <asp:Label  ID="lblMensajes" runat="server"></asp:Label>
                </div>

                <div runat="server" style="display:none">
                    <input type="hidden" runat="server" id="_Accion" />
                    <input type="hidden" runat="server" id="_IDPlantilla" />
                    <input type="hidden" runat="server" id="_PageIndex" />
                </div>




                </div>
               
            </div>
        </div>
    </div>


</asp:Content>

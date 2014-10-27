<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="AsignarFinanciamientoPOA.aspx.cs" Inherits="SIP.Formas.POA.AsignarFinanciamientoPOA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {


            $('.campoNumerico').autoNumeric('init');


            $('*[data-tipo-operacion]').click(function () {

                if ($("#<%= divEdicion.ClientID %>").is(':visible')) {
                    return false;
                }



                var strOperacion = $(this).data("tipo-operacion").toUpperCase();

                switch (strOperacion) {

                    case "EDITAR":
                        return true;
                        break;
                    case "BORRAR":
                        return confirm("¿Está seguro de eliminar el registro?");
                        break;                   
                    default:
                        break;
                }

                return false;

            });



        }); //$(document).ready



        function fnc_Validar()
        {

            var financiamiento = $("#<%= ddlTechoFinancieroUnidadPresupuestal.ClientID %>").val();
            if (financiamiento == null || financiamiento.length == 0 || financiamiento == undefined || financiamiento == 0) {
                alert("El valor del campo <Financiamiento> es requerido");
                return false;
            }

            var importe = $("#<%= txtImporte.ClientID %>").val();
            if (importe == null || importe.length == 0 || importe == undefined || importe == 0) {
                alert("El campo <Importe> es requerido");
                return false;
            }
                       
            return true;
         }

        function fnc_OcultarDivs(sender) {        
             $("#<%= divBtnNuevo.ClientID %>").css("display", "block");
             $("#<%= divEdicion.ClientID %>").css("display", "none");
             $("#<%= divMsg.ClientID %>").css("display", "none");
             return false;
         }

                


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="panel-footer alert alert-danger" id="divMsg" style="display:none" runat="server">
           <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
        </div>

        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="GridViewObraFinanciamiento" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True">
            <Columns>

                       <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">
                            <ItemTemplate>
                                                    
                                <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" data-tipo-operacion="editar"/>
                                <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick="imgBtnEliminar_Click" data-tipo-operacion="borrar"/>

                            </ItemTemplate>                         
                        </asp:TemplateField>     
                                         
                       <asp:TemplateField HeaderText="Financiamiento" ItemStyle-CssClass="col-md-9" HeaderStyle-CssClass="panel-footer">                          
                            <ItemTemplate>
                                <asp:Label ID="LabelFinanciamiento" runat="server" Text='<%# Bind("TechoFinancieroUnidadPresupuestal.Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Importe" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">                            
                            <ItemTemplate>
                                <asp:Label ID="labelImporte" runat="server" Text='<%# Eval("Importe", "{0:C}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       

            </Columns>
                    
            <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                    
        </asp:GridView>

        <div id="divBtnNuevo" runat="server" style="display:block">
              <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default" OnClick="btnNuevo_Click" AutoPostBack="false" />
              <hr />
              <a href="<%=ResolveClientUrl("~/Formas/POA/POA.aspx") %>"><span class="glyphicon glyphicon-arrow-left"></span> Anteproyecto de POA</a>
         </div>

        <div id="divEdicion" runat="server" class="panel-footer" style="display:none">

            
           <div class="form-group">
              <label for="ddlTechoFinancieroUnidadPresupuestal">Financiamiento</label>
              <div>
                <asp:DropDownList ID="ddlTechoFinancieroUnidadPresupuestal" CssClass="form-control" runat="server"></asp:DropDownList>
              </div>
          </div>

           <div class="form-group">
              <label for="txtImporte">Importe</label>
              <div>
                  <asp:TextBox ID="txtImporte" CssClass="form-control campoNumerico" runat="server"></asp:TextBox>
              </div>
           </div>


           <div class="form-group header">
                <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnGuardar" runat="server" OnClientClick="return fnc_Validar()" OnClick="btnGuardar_Click" AutoPostBack="false" />
                <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" OnClientClick="return fnc_OcultarDivs()" AutoPostBack="false" />
           </div>      

        </div>

        <div style="display:none" runat="server">
           <asp:TextBox ID="_ID" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
           <asp:TextBox ID="_Accion" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>                                                           
        </div>      

    </div><!--div class="container"-->

</asp:Content>

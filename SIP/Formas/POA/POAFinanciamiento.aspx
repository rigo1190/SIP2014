<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="POAFinanciamiento.aspx.cs" Inherits="SIP.Formas.POA.POAFinanciamiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {


            $('.campoNumerico').autoNumeric('init');


            $('*[data-tipo-operacion]').click(function ()
            {
                 var strOperacion = $(this).data("tipo-operacion").toUpperCase();

                 switch (strOperacion) {

                     case "EDITAR":                    
                         break;
                     case "BORRAR":                    
                         break;
                     case "ASIGNARFINANCIAMIENTO":
                         var url = $(this).data("url-poa");
                         $(location).attr('href', url);
                         break;
                     default:
                         break;
                 }

                 return false;

             });



         }); //$(document).ready



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="page-header"><h3><asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label></h3></div>

        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="GridViewPOADetalle" DataKeyNames="Id" AutoGenerateColumns="False" OnRowDataBound="GridViewPOADetalle_RowDataBound" runat="server" AllowPaging="True">
            <Columns>                                   
                       <asp:TemplateField HeaderText="Número" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">                          
                            <ItemTemplate>
                                <asp:Label ID="LabelNumero" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Descripción" ItemStyle-CssClass="col-md-10" HeaderStyle-CssClass="panel-footer">                            
                            <ItemTemplate>
                                <asp:Label ID="labelDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                        <asp:TemplateField HeaderText="Financiamiento" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">
                            <ItemTemplate>
                                <button type="button" id="btnFinanciamiento" data-tipo-operacion="asignarfinanciamiento" runat="server" class="btn btn-default"> <span class="glyphicon glyphicon-usd"></span></button> 
                            </ItemTemplate>                          
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />                                            
                        </asp:TemplateField>                       
            </Columns>
                    
            <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                    
        </asp:GridView>


    </div>



</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="POAAjustado.aspx.cs" Inherits="SIP.Formas.POA.POAAjustado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            $('*[data-tipo-operacion]').click(function () {

                var strOperacion = $(this).data("tipo-operacion").toUpperCase();

                switch (strOperacion) {
                    case "EVALUAR":
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
        <div class="page-header"">
            <h3>Evaluación de POA Ajustado</h3>
        </div>
        <div id="divEncabezado" runat="server">
            <div class="row">
                <div class="panel panel-success">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Lista de Obras
                        </h3>
                    </div>
                    <div class="panel-body">
                        <asp:GridView OnRowDataBound="grid_RowDataBound"  ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Dependencia" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">                          
                                    <ItemTemplate>
                                        <asp:Label ID="lblDependencia" runat="server" Text='<%# Bind("POA.UnidadPresupuestal.Nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Numero" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">                          
                                    <ItemTemplate>
                                        <asp:Label ID="LabelNumero" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Descripcion" ItemStyle-CssClass="col-md-10" HeaderStyle-CssClass="panel-footer">                            
                                    <ItemTemplate>
                                        <asp:Label ID="labelDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Evaluar POA" SortExpression="NOAplica">
                                    <ItemTemplate>
                                        <button type="button" runat="server" id="btnEvaluar"><span class="glyphicon glyphicon-ok"></span></button>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                 
                            </Columns>
                            <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

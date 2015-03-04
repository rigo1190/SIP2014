<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="SeleccionarObras.aspx.cs" Inherits="SIP.Formas.POA.SeleccionarObras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //$(document).ready(function () {
           
        //});

        function fnc_AbrirReporte(caller) {
            var izq = (screen.width - 750) / 2
            var sup = (screen.height - 600) / 2
            var listObras = "";
            
            switch (caller) {
                case 2:
                case 3:
                    listObras = fnc_ObtenerSeleccionados(1);
                    break;
                case 4:
                    break;
                case 5:
                case 6:
                    listObras = fnc_ObtenerSeleccionados(2);
                    break;
            }

            if (caller != 4) {
                if (listObras == "") {
                    alert("Debe seleccionar al menos una obra");
                    return;
                }
            }

            url = "<%= ResolveClientUrl("~/rpts/wfVerReporte.aspx") %>";
            var idUnidad = '<%=Session["UnidadPresupuestalId"].ToString()%>';
            var argumentos;

            if (caller != 4) {
                argumentos = "?c=" + caller + "&p=" + idUnidad + "-" + listObras;
            } else {
                argumentos = "?c=" + caller + "&p=" + idUnidad
            }

            url += argumentos;
            window.open(url, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=750,height=500,top=' + sup + ',left=' + izq);
        }

        function fnc_ObtenerSeleccionados(numGrid) {
            var index = 0;
            var grid;
            var listObras="";
            var nomGrid = "";

            switch (numGrid) {
                case 1:
                    grid = document.getElementById('<%=gridDSP.ClientID %>'); //Se recupera el grid
                    nomGrid = "gridDSP";
                    break;
                case 2:
                    grid = document.getElementById('<%=gridRPAI.ClientID %>'); //Se recupera el grid
                    nomGrid = "gridRPAI";
                    break;
            }

            var primera = true;
            var idPOA;

            for (i = 1; i < grid.rows.length; i++) { //Se recorren las filas
                
                idPOA = "";
                
                if ($("input#ContentPlaceHolder1_" + nomGrid + "_chkSeleccionar_" + index).is(':checked')) {

                    idPOA = $("input#ContentPlaceHolder1_" + nomGrid + "_idPOADetalle_" + index).val();

                    if (idPOA != null && idPOA != "" && idPOA != undefined) {

                        if (primera) {
                            listObras += idPOA;
                            primera = false;
                        } else
                            listObras += "," + idPOA;
                    }
                }

                index++;
            }

            return listObras;
        }


    </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="page-header"">
             <h3>Imprimir Oficios DSP y RPAI</h3>
        </div>
        <div class="row" runat="server" id="divGrids">
            <div class="panel panel-success">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        Obras aprobadas
                    </h3>
                </div>

                <div class="panel-body">
                   
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active"><a href="#DSP" role="tab" data-toggle="tab">Obras para DSP</a></li>
                        <li><a href="#RPAI" role="tab" data-toggle="tab">Obras para RPAI</a></li>        
                    </ul>

                    <div class="tab-content">
                         <div class="tab-pane active" id="DSP">
                            <div class="row">
                                <asp:GridView ID="gridDSP" OnPageIndexChanging="gridDSP_PageIndexChanging" CssClass="table" ShowHeaderWhenEmpty="true" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <input type="checkbox" value="false" runat="server" id="chkSeleccionar" />
                                                <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPOADetalle" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Obra" SortExpression="Orden">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Numero") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descripcion" SortExpression="Orden">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Municipio" SortExpression="Orden">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Municipio.Nombre") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Loalidad" SortExpression="Orden">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Localidad.Nombre") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                                </asp:GridView>
                            </div>

                            <div class="row">

                                <table>
                                    <tr>
                                        <td>
                                            <label>DSP</label>
                                            <button type="button" style="width:80px" runat="server" onclick="fnc_AbrirReporte(2)" id="btnDSP"><span class="glyphicon glyphicon-print"></span></button>
                                        </td>
                                         <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <label>Detalle Obras aprobadas DSP</label>
                                            <button type="button" style="width:80px" runat="server" onclick="fnc_AbrirReporte(3)" id="Button1"><span class="glyphicon glyphicon-print"></span></button>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <label>Detalle Obras rechazadas DSP</label>
                                            <button type="button" style="width:80px" runat="server" onclick="fnc_AbrirReporte(4)" id="Button2"><span class="glyphicon glyphicon-print"></span></button>
                                        </td>
                                    </tr>
                                </table>

                            </div>


                        </div>

                         <div class="tab-pane" id="RPAI">
                            <div class="row">
                                <asp:GridView ID="gridRPAI" OnPageIndexChanging="gridRPAI_PageIndexChanging" CssClass="table" ShowHeaderWhenEmpty="true" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Seleccionar">
                                            <ItemTemplate>
                                                <input type="checkbox" value="false" runat="server" id="chkSeleccionar" />
                                                <input type="hidden" value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' runat="server" id="idPOADetalle" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No. Obra" SortExpression="Orden">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Numero") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Descripcion" SortExpression="Orden">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Descripcion") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Municipio" SortExpression="Orden">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Municipio.Nombre") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Loalidad" SortExpression="Orden">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Localidad.Nombre") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                                </asp:GridView>
                            </div>

                              <div class="row">

                                <table>
                                    <tr>
                                        <td>
                                            <label>RPAI</label>
                                            <button type="button" style="width:80px" runat="server" onclick="fnc_AbrirReporte(5)" id="Button3"><span class="glyphicon glyphicon-print"></span></button>
                                        </td>
                                         <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td>
                                            <label>Detalle Obras aprobadas RPAI</label>
                                            <button type="button" style="width:80px" runat="server" onclick="fnc_AbrirReporte(6)" id="Button4"><span class="glyphicon glyphicon-print"></span></button>
                                        </td>
                                        
                                    </tr>
                                </table>

                            </div>



                        </div>


                        

                    </div>

                </div>

            </div>



        </div>

    </div>
</asp:Content>

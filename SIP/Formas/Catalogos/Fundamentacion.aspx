<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="Fundamentacion.aspx.cs" Inherits="SIP.Formas.Catalogos.Fundamentacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        <%--//Funcion que evita que se "RESCROLEE" el arbol al seleccionar un NODO
        function SetSelectedTreeNodeVisible(controlID, boolHayPlantillas) {

            var elem = document.getElementById("<%=treePreguntas.ClientID%>" + "_SelectedNode");
             if (elem != null) {
                 var node = document.getElementById(elem.value);
                 if (node != null) {
                     node.scrollIntoView(true);
                     $("#<%= divArbol.ClientID %>").scrollLeft = 0;
                }
            }
        }--%>


        <%--function OnLoad() {
            var links = document.getElementById("<%=treePreguntas.ClientID %>").getElementsByTagName("a");
            for (var i = 0; i < links.length; i++) {
                links[i].setAttribute("href", "javascript:NodeClick(\"" + links[i].id + "\", \"" + links[i].getAttribute("href") + "\")");
            }
        }
        //window.onload = OnLoad;
        function NodeClick(id, attribute) {
            //Do Something
            var nodeLink = document.getElementById(id);
            alert(nodeLink.innerHTML + " clicked");

            //Execute the server side event.
            eval(attribute);
        }--%>

        function fnc_Test(id) {
            
        }

        

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>

    <div class="container">
        <div class="page-header"">
            <h3>Fundamentación</h3>
        </div>

        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title"><i class="fa"></i>Plantillas</h3>
            </div>
            <div class="panel-body">
                <asp:GridView OnRowDataBound="gridPlanDesarrollo_RowDataBound" PageSize="1000" Height="25px" EnablePersistedSelection="true" ShowHeaderWhenEmpty="true" ID="gridPlanDesarrollo" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                     <Columns>
                        <asp:TemplateField HeaderText="Documento de Evaluación" SortExpression="Orden">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Pregunta") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Fundamentación" SortExpression="SI">
                            <ItemTemplate>
                                <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderText="Agregar Fundamentación" SortExpression="NO">
                           <ItemTemplate>
                                <button type="button" runat="server" id="btnVer"><span class="glyphicon glyphicon-search"></span></button>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modalCaptura" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
        
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" id="modalTitle">Datos de la fundamentación</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Ley aplicable:</label>
                        <asp:DropDownList Width="120px" AutoPostBack="false" runat="server" ID="ddlLey"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Descripción Corta:</label>
                        <input type="text" style="width:350px" runat="server" id="txtDescripcionCorta" />
                    </div>
                    <div class="form-group">
                        <label>Fundamentación:</label>
                        <textarea runat="server" style="height:100px; width:350px" ></textarea>
                    </div>
                            
                </div>
            </div>
        </div>
                
    </div>
      
    <div class="modal fade" id="modalFundamentacion" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
        
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" id="modalTit">Datos del Documento de Evaluación</h4>
                </div>
                <div class="modal-body">
                    <asp:GridView Caption="Fundamentación" DataKeyNames="Id" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" runat="server" ID="gridFundamentacion">
                        <Columns>
                            <asp:TemplateField HeaderText="Ley Aplicable">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "RubroFundamentacion.Nombre") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción Corta">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "DescripcionCorta") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Fundamentación">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Fundamentacion") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
               
    </div>
      

</asp:Content>

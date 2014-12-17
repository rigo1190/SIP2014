﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="POAAjustadoFinanciamiento.aspx.cs" Inherits="SIP.Formas.POA.POAAjustadoFinanciamiento" %>
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

        <div class="row">
            <div class="col-md-8"></div>
            <div class="col-md-4 text-right">
                <a href="<%=ResolveClientUrl("~/Formas/POA/frmPOAAjustado.aspx") %>" ><span class="glyphicon glyphicon-arrow-left"></span> <strong>regresar al proyecto de POA ajustado</strong></a>
            </div>
        </div>        
        <br />
      
        <div class="alert alert-danger" id="divTechoFinancieroError" style="display:none" runat="server">
            <p><strong>Aún no se ha cerrado la apertura de Techos financieros para este ejercicio.</strong></p>
        </div>

        <div class="panel panel-success" id="divTechoFinancieroEstatus" style="display:block" runat="server">
            <div class="panel-heading"><strong>Techo financiero de la unidad presupuestal</strong></div>
            <div class="panel-body">

                      <asp:GridView ID="GridViewTechoFinanciero" runat="server" CssClass="table"
                        ItemType="DataAccessLayer.Models.TechoFinancieroUnidadPresupuestal" DataKeyNames="Id"
                        SelectMethod="GridViewTechoFinanciero_GetData"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:DynamicField DataField="Id" Visible="false"/>
                            <asp:DynamicField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="panel-footer"/>
                            <asp:DynamicField DataField="Importe" HeaderText="Techo financiero"  HeaderStyle-CssClass="panel-footer" DataFormatString="{0:C}"/>                                                                    
                            <asp:TemplateField HeaderText="Asignado" HeaderStyle-CssClass="panel-footer">
                              <ItemTemplate>
                                <asp:Label Text='<%# String.Format("{0:C2}",Item.GetImporteAsignado()) %>' runat="server" />
                              </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disponible" HeaderStyle-CssClass="panel-footer">
                              <ItemTemplate>
                                <asp:Label Text='<%# String.Format("{0:C2}",Item.GetImporteDisponible()) %>' runat="server" />
                              </ItemTemplate>
                            </asp:TemplateField>                               
                        </Columns>
                      </asp:GridView>

            </div>
            <div class="panel-footer"></div>
        </div>

        <asp:GridView ID="GridViewObra" runat="server" CssClass="table" ShowHeaderWhenEmpty="true" AllowPaging="true"
                        ItemType="DataAccessLayer.Models.Obra" DataKeyNames="Id"
                        SelectMethod="GridViewObra_GetData"
                        OnRowDataBound="GridViewObra_RowDataBound"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:DynamicField DataField="POADetalleId" Visible="false"/> 
                            <asp:DynamicField DataField="Numero" HeaderText="Número" HeaderStyle-CssClass="panel-footer"/>                          
                            <asp:DynamicField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="panel-footer"/>                                                                                              
                            <asp:TemplateField HeaderText="Importe Asignado" HeaderStyle-CssClass="panel-footer">
                              <ItemTemplate>
                                <asp:Label Text='<%# String.Format("{0:C2}",Item.GetImporteAsignado()) %>' runat="server" />
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

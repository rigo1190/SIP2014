<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="POAAjustadoFinanciamiento.aspx.cs" Inherits="SIP.Formas.POA.POAAjustadoFinanciamiento" %>
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

        function OnRequestComplete(result, userContext, methodName) {

            $("#<%= txtFondoAbreviatura.ClientID %>").val(result[0]);
            $("#<%= txtFondoNombre.ClientID %>").val(result[1]);
            $("#<%= txtFondoTiposObrasAcciones.ClientID %>").val(result[2]);
            $("#<%= txtFondoCalendarioDeIngresos.ClientID %>").val(result[3]);
            $("#<%= txtFondoVigenciaDePago.ClientID %>").val(result[4]);
            $("#<%= txtFondoNormatividadAplicable.ClientID %>").val(result[5]);
            $("#<%= txtFondoContraparte.ClientID %>").val(result[6]);

            $('#modallineamientosfondos').modal('show');

        }

        function OnRequestError(error, userContext, methodName) {

            if (error != null) {

                alert(error.get_message());

            }

        }

        function fnc_MostrarLineamientos(sender, valor) {
            //alert("El fondo tiene el id=" + valor);
            PageMethods.GetLineamientosFondo(valor, OnRequestComplete, OnRequestError);
        }




        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

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
                        AutoGenerateColumns="false"
                          OnRowDataBound="GridViewTechoFinanciero_RowDataBound">
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
                            <asp:TemplateField HeaderText="Lineamientos del fondo" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer text-center">
                              <ItemTemplate>
                                  <button type="button" class="btn btn-default btn-sm" id="btnlineamientos" runat="server">Mostrar lineamientos</button>                     
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

        

    </div><!-- container-->

     <div class="modal fade" id="modallineamientosfondos" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
              <div class="modal-header alert alert-success">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><strong>&times;</strong></button>
                <h4 class="modal-title" id="myModalLabel">Lineamientos de fondos</h4>
              </div>
              <div class="modal-body">

                      <div class="row">

                          <div class="col-md-6">

                              <div class="form-group">
                                     <label for="Abreviatura">Abreviatura</label>
                                     <div>
                                        <input type="text" class="input-sm required form-control" id="txtFondoAbreviatura" runat="server" disabled="disabled"/>                           
                                    </div>
                              </div>

                              <div class="form-group">
                                 <label for="Nombre">Nombre</label>
                                 <div>
                                    <textarea id="txtFondoNombre" class="input-sm required form-control" runat="server"  rows="3" disabled="disabled" ></textarea>
                                </div>
                              </div>

                              <div class="form-group">
                                     <label for="TiposObrasYAcciones">Tipos de obras y acciones</label>
                                     <div>
                                        <textarea id="txtFondoTiposObrasAcciones" class="input-sm required form-control" runat="server"  rows="3" disabled="disabled"></textarea>
                                    </div>
                               </div>

                               <div class="form-group">
                                     <label for="CalendarioDeIngresos">Calendario de ingresos</label>
                                     <div>
                                        <textarea id="txtFondoCalendarioDeIngresos" class="input-sm required form-control" runat="server"  rows="3" disabled="disabled"></textarea>
                                    </div>
                               </div>


                          </div>                       
                          <div class="col-md-6">

                                 <div class="form-group">
                                     <label for="VigenciaDePago">Vigencia de pago</label>
                                     <div>
                                        <textarea id="txtFondoVigenciaDePago" class="input-sm required form-control" runat="server"  rows="3" disabled="disabled" ></textarea>
                                     </div>
                                 </div>

                               <div class="form-group">
                                   <label for="NormatividadAplicable">Normatividad aplicable</label>
                                    <div>
                                       <input type="text" class="input-sm required form-control" id="txtFondoNormatividadAplicable" runat="server" disabled="disabled"/>                           
                                    </div>
                               </div>

                               <div class="form-group">
                                     <label for="Contraparte">Contraparte</label>
                                     <div>
                                        <textarea id="txtFondoContraparte" class="input-sm required form-control" runat="server"  rows="3" disabled="disabled" ></textarea>
                                     </div>
                               </div>


                          </div>

                      </div><!-- row --> 

              </div><!-- modal-body -->                 
            </div>
        </div>
    </div>






</asp:Content>

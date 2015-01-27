<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="AsignarFinanciamientoPOA.aspx.cs" Inherits="SIP.Formas.POA.AsignarFinanciamientoPOA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function ()
        {


            $('.campoNumerico').autoNumeric('init');
                     

        }); 



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
             $("#<%= divMsg.ClientID %>").css("display", "none");
             return false;
        }


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

        function fnc_MostrarLineamientos(sender,valor)
        {            
            PageMethods.GetLineamientosFondo(valor, OnRequestComplete, OnRequestError);
        }

        function fnc_MostrarPanelEditar()
        {
            $('#modalEdicion').modal('show');
        }

        function fnc_OcultarPanelEditar() {
            $('#modalEdicion').modal('hide');
        }

        function fnc_MostrarPanelBorrar() {
            $('#modalBorrar').modal('show');
        }

        function fnc_OcultarPanelBorrar() {
            $('#modalBorrar').modal('hide');
        }

        function BindEvents() {
            $(document).ready(function () {      
                $('.campoNumerico').autoNumeric('init');
            });
        }


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

    <div class="container">

        <div class="row">
            <div class="col-md-6"></div>
            <div class="col-md-6 text-right">
                <div id="divlinkPOAFinanciamiento" style="display:none" runat="server">
                  <a href="<%=ResolveClientUrl("~/Formas/POA/POAFinanciamiento.aspx") %>" ><span class="glyphicon glyphicon-arrow-left"></span> <strong>regresar a la asignación de financiamiento para el Anteproyecto de POA</strong></a>
                </div>
                <div id="divlinkPOAAjustadoFinanciamiento" style="display:none" runat="server">
                  <a href="<%=ResolveClientUrl("~/Formas/POA/POAAjustadoFinanciamiento.aspx") %>" ><span class="glyphicon glyphicon-arrow-left"></span> <strong>regresar a la asignación de financiamiento para el proyecto de POA ajustado</strong></a>
                </div>              
            </div>
        </div>   
                             
        <br />

        <div class="panel panel-default">
          <div class="panel-heading"><strong>Número de obra o acción: </strong><% Response.Write(obraNumero); %></div>
          <div class="panel-body">
            <p><% Response.Write(obraDescripcion); %></p>             
          </div>
        </div>

        <div class="panel panel-success" id="divTechoFinancieroEstatus" style="display:block" runat="server">

            <div class="panel-heading">

                <div class="row">
                    <div class="col-md-8"><strong>Financiamientos de la unidad presupuestal</strong></div> 
                </div>               

            </div>
            <div class="panel-body">

                      <asp:GridView ID="GridViewTechoFinanciero" runat="server" CssClass="table"
                        ItemType="DataAccessLayer.Models.TechoFinancieroUnidadPresupuestal" DataKeyNames="Id"
                        SelectMethod="GridViewTechoFinanciero_GetData" 
                        OnRowDataBound="GridViewTechoFinanciero_RowDataBound"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:DynamicField DataField="Id" Visible="false"/>
                            <asp:DynamicField DataField="Descripcion" HeaderText="Descripción" ItemStyle-CssClass="col-md-4" HeaderStyle-CssClass="panel-footer"/>
                            <asp:DynamicField DataField="Importe" ItemStyle-CssClass="col-md-2" HeaderText="Techo financiero" HeaderStyle-CssClass="panel-footer" DataFormatString="{0:C}"/>                                                                    
                            <asp:TemplateField HeaderText="Asignado" ItemStyle-CssClass="col-md-2" HeaderStyle-CssClass="panel-footer">
                              <ItemTemplate>
                                <asp:Label Text='<%# String.Format("{0:C2}",Item.GetImporteAsignado()) %>' runat="server" />
                              </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disponible" ItemStyle-CssClass="col-md-2" HeaderStyle-CssClass="panel-footer">
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
                       
        </div>        

    

        <div class="panel panel-success">

          <div class="panel-heading"><strong>Financiamientos de la obra</strong></div>
          <div class="panel-body">


                    <%--<asp:UpdatePanel ID="pnlFinanciamientosObra" UpdateMode="Always">
                        <ContentTemplate>--%>

                            <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="GridViewObraFinanciamiento" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True">
                        <Columns>

                               <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">
                                    <ItemTemplate>                                                    
                                      
                                        <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" />
                                        <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick="imgBtnEliminar_Click" />

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

                  <%--      </ContentTemplate>
                        <Triggers></Triggers>
                    </asp:UpdatePanel>--%>


                    

                <div id="divBtnNuevo" runat="server" style="display:block">
                     <asp:Button ID="btnNuevo" runat="server" Text="Agregar nuevo financiamiento" CssClass="btn btn-default" OnClick="btnNuevo_Click" />             
                </div>

                         
          </div><!-- panel-body -->

        </div><!-- panel financiamientos obra -->
           

    </div><!--div class="container"-->


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
    </div><!-- modal lineamientos -->

     
     <div class="modal fade" id="modalEdicion" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">

        <div class="modal-dialog modal-lg">

            <div class="modal-content">

              <div class="modal-header alert alert-success">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><strong>&times;</strong></button>
                <h4 class="modal-title"><% Response.Write(ViewState["titulo"]); %></h4>
              </div><!-- modal header -->

              <div class="modal-body">

                                <asp:UpdatePanel ID="pnlEdicion" runat="server">

                                    <ContentTemplate>


                                         <script type="text/javascript">

                                             Sys.Application.add_load(BindEvents);

                                        </script>


                                        <div class="panel-footer alert alert-danger strong" id="divMsg" style="display:none" runat="server">
                                    <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
                                </div>  


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


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                                    </Triggers>

                                </asp:UpdatePanel>

                               
                    

             </div><!-- modal-body -->  
                
              <div class="modal-footer">  
                  <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-default" OnClientClick="return fnc_Validar()" OnClick="btnGuardar_Click" />
                  <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancelar</button>
              </div><!-- modal-footer -->  
                                
            </div><!-- modal-content -->

        </div><!-- modal-dialog -->

    </div><!-- modal Edicion -->

         
     <div class="modal fade" id="modalBorrar">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header alert alert-warning">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Confirmar operación</h4>
              </div>
              <div class="modal-body">
                <p><strong>¿Desea borrar realmente este registro?</strong></p>
              </div>
               <div class="modal-footer">  
                  <asp:Button ID="btnBorrar" runat="server" Text="Borrar" CssClass="btn btn-default" OnClick="btnBorrar_Click" />
                  <button class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancelar</button>
              </div><!-- modal-footer -->  
            </div><!-- /.modal-content -->
          </div><!-- /.modal-dialog -->
    </div><!-- modal Borrar -->
       

</asp:Content>

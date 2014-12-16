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


            $('#modallineamientosfondos').on('shown.bs.modal', function (e)
            {                
                alert("Se dispara el evento shown.bs.modal");
                var strOperacion = $(this).data("fondo-id").toUpperCase();
                alert("Estos son los datos: " + strOperacion);
            })
           

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

        function fnc_MostrarLineamientos(sender,valor)
        {
            alert("Ingresamos al método fnc_MostrarLineamientos, con parametro= " + valor);
            //var strOperacion = this.data("fondo-id").toUpperCase();
            //var id = sender.get_element().id;
            //var strOperacion = $get(id).data("tipo-operacion").toUpperCase();
            //alert("Ingresamos al método fnc_MostrarLineamientos, con parametro= " + strOperacion);
            PageMethod.
            $('#modallineamientosfondos').modal('show');
            return true;
        }

                


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container">

        <div class="row">
            <div class="col-md-8"></div>
            <div class="col-md-4 text-right">
                <a href="<%=ResolveClientUrl("~/Formas/POA/POA.aspx") %>" ><span class="glyphicon glyphicon-arrow-left"></span> <strong>regresar al anteproyecto de POA</strong></a>
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
                    <%--<div class="col-md-4 text-right">
                        <button type="button" class="btn btn-default btn-sm" data-toggle="modal" data-target="#modallineamientosfondos">
                            Consultar lineamientos del fondo
                        </button>                       
                    </div>--%>
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
                            <asp:DynamicField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="panel-footer"/>
                            <asp:DynamicField DataField="Importe" HeaderText="Techo financiero" HeaderStyle-CssClass="panel-footer" DataFormatString="{0:C}"/>                                                                    
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
                            <asp:TemplateField HeaderText="Lineamientos del fondoX" HeaderStyle-CssClass="panel-footer">
                              <ItemTemplate>                               
                                <%--<asp:Button  runat="server" ID="btnLineamientos" Text="Mostrar lineamientos" OnClick="btnLineamientos_Click" OnClientClick="fnc_MostrarLineamientos();"/>  --%> 
                                  <button type="button" class="btn btn-default btn-sm" id="btnlineamientos" onclick="fnc_MostrarLineamientos(this,'<%# Item.TechoFinanciero.Financiamiento.Fondo.Id %>');">
                            Consultar lineamientos del fondo
                        </button>                     
                              </ItemTemplate>
                            </asp:TemplateField>                                     
                        </Columns>
                      </asp:GridView>

            </div>
            <div class="panel-footer"></div>
        </div>





        <div class="panel-footer alert alert-danger strong" id="divMsg" style="display:none" runat="server">
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
              <asp:Button ID="btnNuevo" runat="server" Text="Agregar nuevo financiamiento" CssClass="btn btn-default" OnClick="btnNuevo_Click" AutoPostBack="false" />
              <hr />
              <div id="divlinkPOAFinanciamiento" style="display:none" runat="server">
                  <a href="<%=ResolveClientUrl("~/Formas/POA/POAFinanciamiento.aspx") %>" ><span class="glyphicon glyphicon-arrow-left"></span> <strong>regresar a la asignación de financiamiento para el Anteproyecto de POA</strong></a>
              </div>
              <div id="divlinkPOAAjustadoFinanciamiento" style="display:none" runat="server">
                  <a href="<%=ResolveClientUrl("~/Formas/POA/POAAjustadoFinanciamiento.aspx") %>" ><span class="glyphicon glyphicon-arrow-left"></span> <strong>regresar a la asignación de financiamiento para el proyecto de POA ajustado</strong></a>
              </div>              
              
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


     <div class="modal fade" id="modallineamientosfondos" tabindex="-1" role="dialog" aria-labelledby="smallModal" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="width:800px">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" id="myModalLabel">Lineamientos de fondos</h4>
              </div>
              <div class="modal-body">

                  <asp:GridView ID="GridViewFondoLineamientos" runat="server" CssClass="table"
                        ItemType="DataAccessLayer.Models.FondoLineamientos" DataKeyNames="Id"
                        SelectMethod="GridViewFondoLineamiento_GetData"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:DynamicField DataField="Id" Visible="false"/>
                            <asp:TemplateField HeaderText="Siglas" HeaderStyle-CssClass="panel-footer">
                              <ItemTemplate>
                                <asp:Label Text='<%# Item.Fondo.Abreviatura %>' runat="server" />
                              </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Nombre" HeaderStyle-CssClass="panel-footer">
                              <ItemTemplate>
                                <asp:Label Text='<%# Item.Fondo.Nombre %>' runat="server" />
                              </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:DynamicField DataField="TipoDeObrasYAcciones" HeaderText="Tipos de obras y acciones" HeaderStyle-CssClass="panel-footer" />                                                                    
                            <asp:DynamicField DataField="CalendarioDeIngresos" HeaderText="Calendario de ingresos" HeaderStyle-CssClass="panel-footer" />
                            <asp:DynamicField DataField="VigenciaDePago" HeaderText="Vigencia de pago" HeaderStyle-CssClass="panel-footer" />
                            <asp:DynamicField DataField="NormatividadAplicable" HeaderText="Normatividad aplicable" HeaderStyle-CssClass="panel-footer" />
                           <asp:DynamicField DataField="Contraparte" HeaderText="Contraparte" HeaderStyle-CssClass="panel-footer" />                      
                        </Columns>
                      </asp:GridView>
                
              </div>
              <%--<div class="modal-footer">
                <asp:Button ID="btnDel" runat="server" CssClass="btn btn-default" Text="Aceptar"  />
                <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
              </div>     --%>   
            </div>
        </div>
    </div>
       

</asp:Content>

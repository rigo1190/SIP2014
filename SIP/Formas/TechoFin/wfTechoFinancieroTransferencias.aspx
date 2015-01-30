<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfTechoFinancieroTransferencias.aspx.cs" Inherits="SIP.Formas.TechoFin.wfTechoFinancieroTransferencias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

    <script type="text/javascript">

        $(document).ready(function () {


            $('.campoNumerico').autoNumeric('init');



        });

</script>




</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">
    
    <div class="row panel-footer alert alert-success">
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Techos Financieros"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:Label ID="lblStatus" runat="server" Text="Transferencia de recursos"></asp:Label>
        </div>

        <div class="col-md-4">
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </div>
    </div>


|   <div id="divDatos" runat="server" class="row ">

        
            <div class="col-md-4">
                <label>Financiamiento</label>
                <asp:DropDownList ID="ddlFinanciamiento" CssClass="form-control" runat="server" Height="39px" OnSelectedIndexChanged="ddlFinanciamiento_SelectedIndexChanged"  AutoPostBack="True"></asp:DropDownList>
            </div>

            
            <div class="col-md-8">
                <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Unidad Presupuestal" SortExpression="Unidad Presupuestal">
                        <ItemTemplate>
                            <asp:Label ID="labelUP" runat="server" Text='<%# Bind("UnidadPresupuestal.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="400px"  />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Techo Financiero">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Importe")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Importe Ejecutado">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ImporteEjecutado")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Importe Disponible">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ImporteDisponible")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>

                </Columns>
                </asp:GridView>                
    
                
                
                <div class="row">
                <div class="col-md-4">
                    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-4">.</div>
                <div class="col-md-4">
                    <div class="col-md-2" id="divNuevo" runat="server">              
                            <asp:Button  CssClass="btn btn-default" Text="Realizar Nueva Transferencia" ID="btnNuevo" runat="server" AutoPostBack="false" OnClick ="btnNuevo_Click"  />                    
                            
                    </div>
                </div>
    </div>
                            
            </div>

    </div>

    <div id="divMsgSuccess" class="panel-footer alert alert-success"  runat="server">
    <asp:Label ID="lblMensajeSuccess" runat="server" Text=""></asp:Label>
    </div>
    <div id="divMsgFail" class="panel-footer alert alert-danger" runat="server">
    <asp:Label ID="lblMensajeFail" runat="server" Text=""></asp:Label>
    </div>

    <br />

    <div id="divTransferencia" runat="server" >

        <div class="row panel-footer alert alert-success">
        <div class="col-md-12">
            <asp:Label ID="Label3" runat="server" Text="Configurando la Transferencia de Recursos"></asp:Label>
        </div> 
    </div>

        <div class="row" id="divAddMovimiento">

        
            <div class="col-md-4">
                    <label>Transferir de la Unidad Presupuestal:</label>
                    <asp:DropDownList ID="ddlOrigen" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label>a la Unidad Presupuestal:</label>
                    <asp:DropDownList ID="ddlDestino" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <div class="col-md-2" >
                    <label>Importe</label>
                    <input type="text" class="input-sm required form-control campoNumerico" id="txtImporte" runat="server" style="text-align: left;  align-items:flex-start" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImporte" ErrorMessage="El campo Importe es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        
                </div>
            
                <div class="col-md-2">              
                    <div class="form-group" >.<br />
                        <asp:Button  CssClass="btn btn-default" Text="Agregar Registro" ID="btnAdd" runat="server" AutoPostBack="false" OnClick ="btnAdd_Click" ValidationGroup="validateX" />                        
                    </div>
                </div>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" />

         </div>

        <div class="row" id ="divGridview">
            <div class="col-md-12" >
                <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridDetalle" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                    


                    <asp:TemplateField HeaderText="Origen" SortExpression="Origen">
                        <ItemTemplate>
                            <asp:Label ID="labelOrigen" runat="server" Text='<%# Bind("Origen.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="500px"  />
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Destino" SortExpression="Destino">
                        <ItemTemplate>
                            <asp:Label ID="labelDestino" runat="server" Text='<%# Bind("Destino.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="500px"  />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Importe">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Importe")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>                            
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick ="imgBtnEliminar_Click"/>                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="20px" BackColor="#EEEEEE" />
                    </asp:TemplateField>                                  


                </Columns>
                    
                
                    
                </asp:GridView>                
            </div>
        </div>





          <div class="row" >

                <div class="col-md-2" >
                    <label>Oficio</label>
                    <input type="text" class="input-sm required form-control" id="txtOficio" runat="server" style="text-align: left; align-items:flex-start" />
                </div>
            
              <div class="col-md-10" >
                    <label>Observaciones</label><br />
                    <input type="text" class="input-sm required form-control" id="txtObservaciones" runat="server" style="text-align: left; align-items:flex-start; " />
                </div>
            

         </div>



        <div class="form-group" >.<br />
            <asp:Button  CssClass="btn btn-primary" Text="Registrar Transferencia" ID="btnOk" runat="server" AutoPostBack="false" OnClick ="btnOk_Click"  />                        
            <asp:Button  CssClass="btn btn-default" Text="Cancelar Transferencia" ID="btnCancel" runat="server" AutoPostBack="false" OnClick ="btnCancel_Click"  />                        
        </div>
        
    </div>

    
    





    






    <div style="display:none" runat="server">
        <asp:TextBox ID="_financiamiento" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
    </div>


</div>
</asp:Content>

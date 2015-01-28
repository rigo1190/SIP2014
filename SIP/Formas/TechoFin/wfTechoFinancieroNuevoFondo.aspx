<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfTechoFinancieroNuevoFondo.aspx.cs" Inherits="SIP.Formas.TechoFin.wfTechoFinancieroNuevoFondo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {


            $('.campoNumerico').autoNumeric('init');



        });



        function fnc_Confirmar() {
            return confirm("¿Está seguro de que los datos del nuevo financiamiento a registrar son los correctos?");
        }
        

    </script>




</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container">


    <div class="row panel-footer alert alert-success">
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Techos Financieros"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:Label ID="lblStatus" runat="server" Text="Agregar Nuevo Fondo"></asp:Label>
        </div>

        <div class="col-md-4">
            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
        </div>
    </div>

    <div id="divAdd" runat="server" >
        
        <div class="row" id="divAddMovimiento">

        
            <div class="col-md-4">
                    <label>Unidad Presupuestal:</label>
                    <asp:DropDownList ID="ddlOrigen" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>

                <div class="col-md-2" >
                    <label>Importe</label>
                    <input type="text" class="input-sm required form-control campoNumerico" id="txtImporte" runat="server" style="text-align: left;  align-items:flex-start" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImporte" ErrorMessage="El campo Importe es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                        
                </div>
            
                <div class="col-md-2">              
                    <div class="form-group" >.<br />
                        <asp:Button  CssClass="btn btn-primary" Text="Agregar Registro" ID="btnAdd" runat="server" AutoPostBack="false" OnClick ="btnAdd_Click" ValidationGroup="validateX" />                        
                    </div>
                </div>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" />

         </div>

        <div id="divMsgSuccess" class="panel-footer alert alert-success"  runat="server">
        <asp:Label ID="lblMensajeSuccess" runat="server" Text=""></asp:Label>
        </div>
        <div id="divMsgFail" class="panel-footer alert alert-danger" runat="server">
        <asp:Label ID="lblMensajeFail" runat="server" Text=""></asp:Label>
        </div>


        <div class="row" id ="divGridview">
            <div class="col-md-12" >
                <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridDetalle" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                    


                    <asp:TemplateField HeaderText="Origen" SortExpression="Origen">
                        <ItemTemplate>
                            <asp:Label ID="labelOrigen" runat="server" Text='<%# Bind("Origen.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="700px"  />
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
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>                                  


                </Columns>
                    
                
                    
                </asp:GridView>                
        </div>
            
           



        </div>

         <div class="row">
                <div class="col-md-4 text-center">.</div>
                <div class="col-md-4 text-right">.</div>   
                <div class="col-md-4">Total : <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></div>
                
          </div>




        
    </div>


    <div id="divRegistrarNuevoTecho" runat="server" >
            <div class="row ">

            <div class="panel panel-success">                
                <div class="panel-heading">
                    <h3 class="panel-title">Indique el financiamiento a registrar</h3>
                </div>
            </div>


            <div class="col-md-2">
                <label>Fondo</label>
                <asp:DropDownList ID="ddlFondo" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                <label>Modalidad</label>
                <asp:DropDownList ID="ddlModalidad" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-2">
                <label>Año</label>
                <asp:DropDownList ID="ddlAño" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>


            </div>

        <div class="row" >

                <div class="col-md-2" >
                    <label>Oficio</label>
                    <input type="text" class="input-sm required form-control" id="txtOficio" runat="server" style="text-align: left;  align-items:flex-start" />
                </div>
            
              <div class="col-md-6" >
                    <label>Observaciones</label><br />
                    <input type="text" class="input-sm required form-control" id="txtObservaciones" runat="server" style="text-align: left; align-items:flex-start; " />
                </div>
            
            <div class="col-md-4">              
                <div class="form-group" >.<br />
                    <asp:Button  CssClass="btn btn-primary" Text="Guardar" ID="btnGuardar" runat="server" AutoPostBack="false" OnClientClick="return fnc_Confirmar()" OnClick ="btnGuardar_Click" />                    
                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-default" OnClick="btnRegresar2_Click" ></asp:Button>
                </div>
            </div>    


         </div>


            
        
    
        </div>

    


    <div id="divNoSePuede" class="panel panel-warning" runat="server">

            <div class="panel-heading">
                <h3 class="panel-title">Generar nuevo techo financiero</h3>
            </div>
        
            <div class="panel-body">
                Para usar esta opción es necesario que el ejercicio se encuentre abierto y la carga inicial de techos financieros se encuentre cerrada;                
            </div>

            <div class="panel-footer clearfix">

                <div class="pull-right">                                
                    <asp:Button ID="btnRegresar2" runat="server" Text="Regresar" CssClass="btn btn-primary" OnClick="btnRegresar2_Click" ></asp:Button>
                </div>

            </div>
           
    </div>

</div>
</asp:Content>

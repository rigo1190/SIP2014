<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfTechoFinanciero.aspx.cs" Inherits="SIP.Formas.TechoFin.wfTechoFinanciero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">

    $(document).ready(function () {


        $('.campoNumerico').autoNumeric('init');



    });

</script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



     <div class="row panel-footer alert alert-success">
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Techos Financieros"></asp:Label>
        </div>
        <div class="col-md-4">
            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
        </div>

        <div class="col-md-4">
            <asp:LinkButton ID="idLinkClose" runat="server" PostBackUrl="~/Formas/TechoFin/wfTechoFinancieroCierre.aspx">Cerrar Apertura de Techos Financieros</asp:LinkButton>
        </div>
    </div>



    <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                        <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            
                            <asp:ImageButton ID="imgSubdetalle" ToolTip="Editar" runat="server" ImageUrl="~/img/Sub.png" OnClick="imgSubdetalle_Click" />                        
                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" />
                            <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick ="imgBtnEliminar_Click"/>
                            
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>                                  


                    <asp:TemplateField HeaderText="Fondo" SortExpression="Fondo">
                        <ItemTemplate>
                            <asp:Label ID="labelFondo" runat="server" Text='<%# Bind("Financiamiento.Fondo.Abreviatura") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="200px"  />
                    </asp:TemplateField>
                                        
                    <asp:TemplateField HeaderText="Modalidad" SortExpression="Modalidad">
                        <ItemTemplate>
                            <asp:Label ID="labelModalidad" runat="server" Text='<%# Bind("Financiamiento.ModalidadFinanciamiento.Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="200px"  />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Año" SortExpression="Año">
                        <ItemTemplate>
                            <asp:Label ID="labelAño" runat="server" Text='<%# Bind("Financiamiento.Año.Anio") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="200px"  />
                    </asp:TemplateField>




                    <asp:TemplateField HeaderText="Techo Financiero">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Importe")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Asignado a U.P.">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ImporteAsignadoUP")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Asignado a Obras">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "ImporteAsignadoObras")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>



                      

                </Columns>
                    
                
                    
        </asp:GridView>

    
        
    <div id="divBtnNuevo" runat="server">
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default"  AutoPostBack="false" OnClick="btnNuevo_Click" />
    </div>
    <br />
    

    <div id="divEditar" runat="server" class="row ">
        <div class="panel panel-success">                
            <div class="panel-heading">
                <h3 class="panel-title">Modifique el Importe</h3>
            </div>
        </div>

        <div class="col-md-2" >
                <label>Importe</label>
                <input type="text" class="input-sm required form-control campoNumerico" id="txtImporteEdicion" runat="server" style="text-align: left; align-items:flex-start" />                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtImporteEdicion" ErrorMessage="El campo Importe es obligatorio" ValidationGroup="validateY">*</asp:RequiredFieldValidator>
                    
        </div>


        <div class="col-md-2">              
            <div class="form-group" >.<br />
                <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnModificar" runat="server" AutoPostBack="false" OnClick ="btnModificar_Click" ValidationGroup="validateY" />
                <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelarModificacion" runat="server" AutoPostBack="false" onClick ="btnCancelarModificacion_Click" />                        
            </div>
        </div>

    </div>

    <div id="divNuevoRegistro" runat="server" class="row ">

        <div class="panel panel-success">                
            <div class="panel-heading">
                <h3 class="panel-title">Indique los datos del nuevo registro</h3>
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

            <div class="col-md-2" >
                <label>Importe</label>
                <input type="text" class="input-sm required form-control campoNumerico" id="txtImporte" runat="server" style="text-align: left; align-items:flex-start" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtImporte" ErrorMessage="El campo Importe es obligatorio" ValidationGroup="validateX">*</asp:RequiredFieldValidator>
                    
            </div>

            
            <div class="col-md-2">              
                <div class="form-group" >.<br />
                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnGuardar" runat="server" AutoPostBack="false" OnClick ="btnGuardar_Click" ValidationGroup="validateX" />
                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" AutoPostBack="false" OnClick="btnCancelar_Click" />                        
                </div>
            </div>


                                
                

            <div style="display:none" runat="server">
                <asp:TextBox ID="_ElId" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                <asp:TextBox ID="_StatusEjercicio" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
            </div>
    
        </div>




        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validateX" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="validateY" />




        <div id="divMsgSuccess" class="panel-footer alert alert-success"  runat="server">
        <asp:Label ID="lblMensajeSuccess" runat="server" Text=""></asp:Label>
        </div>
        <div id="divMsgFail" class="panel-footer alert alert-danger" runat="server">
        <asp:Label ID="lblMensajeFail" runat="server" Text=""></asp:Label>
        </div>



</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfOficiosFuentesFinanciamientoDisponibles.aspx.cs" Inherits="SIP.Formas.TechoFin.wfOficiosFuentesFinanciamientoDisponibles" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

    function fnc_Abrir() {

    }

    function fnc_AbrirReporte(idUnidad, idEjercicio) {

        var izq = (screen.width - 750) / 2
        var sup = (screen.height - 600) / 2
        var param = idUnidad + "-" + idEjercicio;
        
        url = $("#<%= _URLVisor.ClientID %>").val();
            var argumentos = "?c=" + 2 + "&p="+ param;
            url += argumentos;
            window.open(url, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=750,height=500,top=' + sup + ',left=' + izq);
    }



</script>
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title">Oficios: Fuentes de Financiamiento Disponibles</h3>
        </div>

        <div class="tab-pane" id="divUPS" runat="server">
            <asp:GridView Height="25px" OnRowDataBound="grid_RowDataBound" ShowHeaderWhenEmpty="true" CssClass="table" ID="grid" DataKeyNames="Id" AutoGenerateColumns="False" runat="server">
                <Columns>
                    
                                                    


                    <asp:TemplateField HeaderText="Unidad Presupuestal" SortExpression="Unidad Presupuestal">
                        <ItemTemplate>
                            <asp:Label ID="lblUP" runat="server" Text='<%# Bind("Nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="500px"  />
                    </asp:TemplateField>
                            
                    <asp:TemplateField HeaderText="Techo Financiero">
                        <ItemTemplate>
                            <%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Importe")).ToString("c") %>   
                        </ItemTemplate>                        
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Oficio: Asignación Presupuestal ">
                        <ItemTemplate>
                            <asp:Label ID="lbloficioA" runat="server" Text='<%# Bind("OficioA") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Oficio: Alcance">
                        <ItemTemplate>
                            <asp:Label ID="lbloficioB" runat="server" Text='<%# Bind("OficioB") %>'></asp:Label>
                        </ItemTemplate>                        
                    </asp:TemplateField>


                      
                        <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>                                                    
                            <asp:ImageButton ID="imgSubdetalle" ToolTip="Editar" runat="server" ImageUrl="~/img/Sub.png"  /> 
                            <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click"/>                                                        
                        </ItemTemplate>
                        <HeaderStyle BackColor="#EEEEEE" />
                        <ItemStyle HorizontalAlign="right" VerticalAlign="Middle" Width="50px" BackColor="#EEEEEE" />
                    </asp:TemplateField>                                 

                </Columns>
                    
                
                    
        </asp:GridView>
        </div>
    </div>

<div class="row"> 
        <div id="divEdicion" runat="server" class="panel-footer">

                <div class="panel panel-success">                
                    <div class="panel-heading">
                            <h3 class="panel-title">Indique los datos solicitados</h3>
                    </div>
                </div>

                <div class="row top-buffer">
                    <div class="col-md-3">
                        <label for="oficioA">Oficio: Asignación Presupuestal</label>
                    </div>
                    <div class="col-md-8">
                        <input type="text" class="input-sm required" id="txtOficioA" runat="server" style="text-align: left;  align-items:flex-start" />
                    </div>
                </div>


                <div class="row top-buffer">
                    <div class="col-md-3">
                        <label for="oficioB">Oficio: Alcance</label>
                    </div>
                    <div class="col-md-8">
                        <input type="text" class="input-sm required" id="txtOficioB" runat="server" style="text-align: left;  align-items:flex-start" />
                    </div>
                </div>


                <div class="row top-buffer">
                    <div class="col-md-3">
                        <label for="Obs">Observaciones</label>
                    </div>
                    <div class="col-md-8">
                        <textarea id="txtObservaciones" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="5" ></textarea>
                    </div>
                </div>


            

                                
                <div class="form-group">
                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnCrear" runat="server" AutoPostBack="false" OnClick="btnCrear_Click"/>
                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" AutoPostBack="false" OnClick="btnCancelar_Click" />
                </div>

                <div style="display:none" runat="server">
                    <input type="hidden" runat="server" id="_URLVisor" />
                    <asp:TextBox ID="_idUP" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                </div>

            
            </div>
    </div>



</asp:Content>

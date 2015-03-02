<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfEstimaciones.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfEstimaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script type="text/javascript">

            $(document).ready(function () {
                $('.campoNumerico').autoNumeric('init');
            });

            function fnc_Abrir() {

            }

            function fnc_AbrirReporteCantidadesEstimadas() {

                var izq = (screen.width - 1000) / 2
                var sup = (screen.height - 600) / 2
                var param = $("#<%= _idContrato.ClientID %>").val();
                var argumentos = "?c=" + 33 + "&p=" + param;

                url = $("#<%= _URLVisor.ClientID %>").val();

                url += argumentos;
                window.open(url, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=1000,height=500,top=' + sup + ',left=' + izq);
            }

            function fnc_AbrirReporteConcentradoEstimaciones() {

                var izq = (screen.width - 1000) / 2
                var sup = (screen.height - 600) / 2
                var param = $("#<%= _idContrato.ClientID %>").val();
                var argumentos = "?c=" + 34 + "&p=" + param;

                url = $("#<%= _URLVisor.ClientID %>").val();

                url += argumentos;
                window.open(url, 'pmgw', 'toolbar=no,status=no,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,width=1000,height=500,top=' + sup + ',left=' + izq);
            }


</script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">

   <div class="panel panel-success">
        <div class="panel-heading">

            <div class="row">
                <div class="col-md-4"><h3 class="panel-title">Estimaciones</h3></div>
                <div class="col-md-6"> . </div>
                <div class="col-md-2"><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfAvanceFisicoFinanciero.aspx") %>">Regresar</a></div>
             </div>
    </div>

    
    <ul class="nav nav-tabs" role="tablist">
              <li class="active"><a href="#divDatosObra" role="tab" data-toggle="tab">Datos de la Obra</a></li>
              <li><a href="#divDatosContrato" role="tab" data-toggle="tab">Datos del Contrato</a></li>        
            </ul>

    <div class="tab-content">
        <div class="tab-pane active" id="divDatosObra">            
             <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="GridObra" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True"  >
                <Columns>
                                  
                    <asp:TemplateField HeaderText="No. Obra">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="100px"  />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Descripción de la Obra">
                        <ItemTemplate>
                            <asp:Label ID="labelNombre" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                </Columns>                    
        </asp:GridView>
        </div>
        <div class="tab-pane" id="divDatosContrato">
            <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="GridContrato" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True"  >
                <Columns>
                                  
                    <asp:TemplateField HeaderText="Contrato">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("NumeroDeContrato") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="100px"  />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Contratista">
                        <ItemTemplate>
                            <asp:Label ID="label2" runat="server" Text='<%# Bind("RFCcontratista") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    
                    <asp:TemplateField HeaderText="Importe Contratado">
                        <ItemTemplate>
                            <asp:Label ID="label3" runat="server" Text='<%# Bind("Total","{0:C0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    

                    <asp:TemplateField HeaderText="% Anticipo">
                        <ItemTemplate>
                            <asp:Label ID="label4" runat="server" Text='<%# Bind("PorcentajeDeAnticipo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    

                    <asp:TemplateField HeaderText="Ret. 5 al millar">
                        <ItemTemplate>
                            <asp:Label ID="label5" runat="server" Text='<%# Bind("Descontar5AlMillar") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    

                    <asp:TemplateField HeaderText="Ret. 2 al millar">
                        <ItemTemplate>
                            <asp:Label ID="label6" runat="server" Text='<%# Bind("Descontar2AlMillar") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>         

                    <asp:TemplateField HeaderText="Ret. 2 al millar Sup y Vig">
                        <ItemTemplate>
                            <asp:Label ID="label6" runat="server" Text='<%# Bind("Descontar2AlMillarSV") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>         

                    <asp:TemplateField HeaderText="Fecha del Contrato">
                        <ItemTemplate>
                            <asp:Label ID="label7" runat="server" Text='<%# Bind("FechaDelContrato","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Fecha de Inicio">
                        <ItemTemplate>
                            <asp:Label ID="label8" runat="server" Text='<%# Bind("FechaDeInicio","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 

                    <asp:TemplateField HeaderText="Fecha de Término">
                        <ItemTemplate>
                            <asp:Label ID="label9" runat="server" Text='<%# Bind("FechaDeTermino","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 


                </Columns>                    
        </asp:GridView>
        </div>
    </div>


    <div class="panel panel-warning" runat="server" id="divMSGnoHayAnticipo">
        <div class="panel-heading">
            <div class="row">
                <div class="col-md-10"><h3 class="panel-title">Antes de registrar las estimaciones debe registrar el anticipo</h3></div>
            </div>
        </div>
    </div>



    <div id="DIVmostrarEstimaciones" runat="server" >
    


        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="gridEstimaciones" DataKeyNames="Id" AutoGenerateColumns="False" runat="server" AllowPaging="True"  >
                <Columns>

                    <asp:TemplateField HeaderText="No. Estimación">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("NumeroDeEstimacion") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle  Width="100px"  />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Fecha">
                        <ItemTemplate>
                            <asp:Label ID="label2" runat="server" Text='<%# Bind("FechaDeEstimacion","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    

                    <asp:TemplateField HeaderText="Subtotal">
                        <ItemTemplate>
                            <asp:Label ID="label3" runat="server" Text='<%# Bind("ImporteEstimado","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    

                    <asp:TemplateField HeaderText="IVA">
                        <ItemTemplate>
                            <asp:Label ID="label4" runat="server" Text='<%# Bind("IVA","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    

                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label ID="label5" runat="server" Text='<%# Bind("Total","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                
                    
                    <asp:TemplateField HeaderText="Amortización de Anticipos">
                        <ItemTemplate>
                            <asp:Label ID="label6" runat="server" Text='<%# Bind("AmortizacionAnticipo","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>       

                    <asp:TemplateField HeaderText="Retención del 5 al millar">
                        <ItemTemplate>
                            <asp:Label ID="label7" runat="server" Text='<%# Bind("Retencion5AlMillar","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>       
                    
                    <asp:TemplateField HeaderText="Retención del 2 al millar">
                        <ItemTemplate>
                            <asp:Label ID="label8" runat="server" Text='<%# Bind("Retencion2AlMillar","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>          

                    <asp:TemplateField HeaderText="Retención del 2 al millar sup y vig">
                        <ItemTemplate>
                            <asp:Label ID="label81" runat="server" Text='<%# Bind("Retencion2AlMillarSV","{0:C2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>          
                    
                    <asp:TemplateField HeaderText="Importe a Pagar">
                        <ItemTemplate>
                            <asp:Label ID="label9" runat="server" Text='<%# Bind("ImporteNetoACobrar","{0:C0}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>       
                                                                                              

                </Columns>                    
        </asp:GridView>

        
        <div style="display:none" runat="server">
            <input type="hidden" runat="server" id="_URLVisor" />                    
            <input type="hidden" runat="server" id="_idContrato" />                                         
        </div>


        <ul> operaciones:
            <li><a href="<%=ResolveClientUrl("~/Formas/ControlFinanciero/wfEstimacionesNew.aspx?idObra=") %>"> Agregar Estimación</a></li>                                  
            <li>-.-</li>        
            <li><asp:LinkButton ID="linkCantidadesEstimadas" runat="server" PostBackUrl="#" OnClientClick="fnc_AbrirReporteCantidadesEstimadas()">Ver reporte de cantidades estimadas</asp:LinkButton>  </li>
            <li><asp:LinkButton ID="linkConcentradoDeEstimaciones" runat="server" PostBackUrl="#" OnClientClick="fnc_AbrirReporteConcentradoEstimaciones()">Ver reporte concentrado de estimaciones</asp:LinkButton>  </li>
            
        </ul>

    </div>

</div>
</asp:Content>

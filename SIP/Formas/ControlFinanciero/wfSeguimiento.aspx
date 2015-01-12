<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfSeguimiento.aspx.cs" Inherits="SIP.Formas.ControlFinanciero.wfSeguimiento" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {


            $('.campoNumerico').autoNumeric('init');



        });



    
            

</script>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="accordion" class="panel-group">
        
        <%--Datos generales de la obra--%>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseOne">Datos Generales de la obra</a>
                </h4>
            </div>
            <div id="collapseOne" class="panel-collapse collapse">
                <div class="panel-body">
                    



                <div class="row"> 
                <div id="divDatosGenerales" class="panel-footer">
                    
                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="ObraNo">Número de Obra</label>
                    </div>
                    <div class="col-md-2">
                        <input type="text" class="input-sm required" id="txtNoObra" runat="server" style="text-align: left;  align-items:flex-start" />
                    </div>
                </div>


            
                <div class="row top-buffer">
                    <div class="col-md-2">
                        <label for="ObraNombre">Descripción de la Obra</label>
                    </div>
                    <div class="col-md-10">
                        <textarea id="txtDescripcionObra" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="3" ></textarea>


                    </div>
                </div>

            

            </div>
    </div>









                </div>
            </div>
        </div>
        
        <%--Contrato--%>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">Contrato</a>
                </h4>
            </div>
            <div id="collapseTwo" class="panel-collapse collapse">
                <div class="panel-body">
                    





                <div class="row">

                    
                 <div class="col-md-4">

                      <div class="form-group">
                           <label for="Numero">Número de Contrato</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtNumContrato" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                        </div>
                      </div>
                     
                     

                     <div class="form-group">
                         <label for="RFC">RFC contratista</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtRFC" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                        </div>
                      </div>


                     <div class="form-group">
                         <label for="RazonSocial">Razón Social contratista</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtRazonSocial" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off"/>                                                        
                        </div>
                      </div>

                     

                     
                     <div class="form-group">
                           <label for="txtImporteTotal">Importe Contratado</label>
                         <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtImporteTotal" runat="server" style="text-align: left; align-items:flex-start"  />
                        </div>
                      </div>


                     

                 </div>

                 <div class="col-md-4">


                     <div class="form-group">
                           <label for="FechaContrato">Fecha de Contrato</label>
                         <div>
                            <input type="text" class="required form-control date-picker" id="dtpContrato" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                        </div>
                      </div>



                      <div class="form-group">
                           <label for="FechaInicio">Fecha de inicio</label>
                         <div>
                            <input type="text" class="required form-control date-picker" id="dtpInicio" runat="server" data-date-format = "dd/mm/yyyy"  autocomplete="off" />
                        </div>
                      </div>

                      <div class="form-group">
                           <label for="FechaTermino">Fecha de Término</label>                          
                         <div>
                            <input type="text" class="input-sm required form-control date-picker" id="dtpTermino" runat="server" data-date-format = "dd/mm/yyyy" autocomplete="off" />
                        </div>
                      </div>

                     



                     



                  </div>


                <div class="col-md-4">      
                    
                    <div class="form-group">
                           <label for="PorcentajeAnticipo">Porcentaje de Anticipo</label>
                         <div>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtPorcentajeAnticipo" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="50"/>
                        </div>
                      </div>

                       

                     <div class="form-group">
                           <label for="desc5almillar">Aplicar 5 al millar</label>
                         <div>
                            <input type="checkbox" class="input-sm required form-control campoNumerico" id="chk5almillar" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="100"/>
                        </div>
                      </div>


                     <div class="form-group">
                           <label for="desc2almillar">Aplicar 2 al millar</label>
                         <div>
                            <input type="checkbox" class="input-sm required form-control campoNumerico" id="chk2almillar" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="100"/>
                        </div>
                      </div>
                    
                    
                                                   
                    
                </div>

                 

            </div>            










                </div>
            </div>
        </div>


        <%--Presupuesto Contratado--%>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFour">Presupuesto Contratado</a>
                </h4>
            </div>
            <div id="collapseFour" class="panel-collapse collapse">
                <div class="panel-body">
                    <p>Aqui importaremos desde excel el presupuesto contratado</p>
                </div>
            </div>
        </div>


        <%--Anticipo--%>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseFive">Anticipo</a>
                </h4>
            </div>
            <div id="collapseFive" class="panel-collapse collapse">
                <div class="panel-body">
                    <p>Aqui se cargara los datos del anticipo</p>
                </div>
            </div>
        </div>

        <%--Estimaciones--%>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSix">Estimaciones</a>
                </h4>
            </div>
            <div id="collapseSix" class="panel-collapse collapse">
                <div class="panel-body">
                    <p>Estimaciones</p>
                </div>
            </div>
        </div>

        
        <%--Finiquito--%>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a data-toggle="collapse" data-parent="#accordion" href="#collapseSeven">Finiquito</a>
                </h4>
            </div>
            <div id="collapseSeven" class="panel-collapse collapse">
                <div class="panel-body">
                    <p>Finiquito</p>
                </div>
            </div>
        </div>


    </div>


</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="ConsultaPresupuestal.aspx.cs" Inherits="SIP.Formas.ControlPresupuestal.ConsultaPresupuestal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <style type="text/css">

        .details-open 
        {
                 background: url('../../img/details_open.png') no-repeat scroll center center transparent;                 
                 cursor: pointer;
        }

        .details-close 
        {
                 background: url('../../img/details_close.png') no-repeat center center;
                 cursor: pointer;
        }

        table 
        {
            font-size:smaller;
        }

    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            
            GetList('%', '%', '%', '%', '%', '%', '%', '%', '%');

            $('#btnFiltro').click(function ()
            {
                $('#modalFiltro').modal({ backdrop: 'static', show: true });
            });

            $('#btnAplicarFiltro').click(function () {
                
                var WhereNumero = (!$('#txtFiltroNumeroObraAccion').val()) ? '%' : '%' + $('#txtFiltroNumeroObraAccion').val() + '%';
                var WhereDescripcion = (!$('#txtFiltroDescripcion').val()) ? '%' : '%' + $('#txtFiltroDescripcion').val() + '%';
                var WhereMunicipio = (!$('#txtFiltroMunicipio').val()) ? '%' : '%' + $('#txtFiltroMunicipio').val() + '%';
                var WhereLocalidad = (!$('#txtFiltroLocalidad').val()) ? '%' : '%' + $('#txtFiltroLocalidad').val() + '%';
                var WhereUnidadPresupuestal = (!$('#txtFiltroUnidadPresupuestal').val()) ? '%' : '%' + $('#txtFiltroUnidadPresupuestal').val() + '%';
                var WhereContratista = (!$('#txtFiltroContratista').val()) ? '%' : '%' + $('#txtFiltroContratista').val() + '%';
                var WhereFondos = (!$('#txtFiltroFondos').val()) ? '%' : '%' + $('#txtFiltroFondos').val() + '%';
                var WherePresupuesto = '%';
               

                try
                {
                    GetList(WhereNumero, WhereDescripcion, WhereMunicipio, WhereLocalidad, WhereUnidadPresupuestal, WhereContratista, WhereFondos, WherePresupuesto);
                }
                catch(error)
                {
                    alert(error.message);
                }       

                $('#modalFiltro').modal('hide');


            });//$(#btnAplicarFiltro)


        });



        function GetList(WhereNumero, WhereDescripcion, WhereMunicipio, WhereLocalidad, WhereUnidadPresupuestal, WhereContratista, WhereFondos, WherePresupuesto) {


            $.ajax({

                type: "POST",
                url: "ConsultaPresupuestal.aspx/GetListadoObras",
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify({ WhereNumero: WhereNumero, WhereDescripcion: WhereDescripcion, WhereMunicipio: WhereMunicipio, WhereLocalidad: WhereLocalidad, WhereUnidadPresupuestal: WhereUnidadPresupuestal, WhereContratista: WhereContratista, WhereFondos: WhereFondos, WherePresupuesto: WherePresupuesto }),
                dataType: "json",
                success: function (data) {

                    //On success
                    
                  
                    var tbody = $('#tblConsultaPresupuestal tbody:first');
                    tbody.empty();

                    var unidadespresupuestales = $linq(data.d)                                                
                                                 .groupBy('x=> x.UnidadPresupuestal', null)
                                                 .orderBy('x=> x.key')
                                                 .select('x=> x.key')                                                    
                                                 .toArray();

                    


                    $.each(unidadespresupuestales, function (index, up) {
                                                
                       
                        //encabezado unidad presupuestal

                        var tr = $("<tr>").addClass('panel-footer text-center');
                        tr.append($("<td>").text(up).attr('colspan', '11')).css("font-weight", "bold");
                        tbody.append(tr);

                        //detalle subunidades presupuestales

                        var subunidadespresupuestales = $linq(data.d)
                                                        .where("x=> x.UnidadPresupuestal== '" + up + "'")
                                                        .groupBy("x => x.SubUnidadPresupuestal", null)
                                                        .select("x => x.key")
                                                        .toArray();

                 

                        $.each(subunidadespresupuestales, function (index, sup) {

                            //encabezado para la sub unidad presupuestal

                            if (sup != up)
                            {
                                var tr = $("<tr>").addClass('panel-footer');
                                tr.append($("<td>").text(sup).attr('colspan', '11')).css("font-weight", "bold");
                                tbody.append(tr);
                            }                          
                            
                            //detalle fondos subunidades presupuestales

                            var fondos = $linq(data.d)
                                        .where("x=> x.UnidadPresupuestal== '" + up + "'")
                                        .where("x=> x.SubUnidadPresupuestal== '" + sup + "'")
                                        .groupBy("x => x.Fondos", null)
                                        .select("x => x.key")
                                        .toArray();

                            $.each(fondos, function (index, f) {


                                //encabezado para el fondo

                                var tr = $("<tr>").addClass('panel-footer');
                                tr.append($("<td>").text(f).attr('colspan', '11').css('padding-left',25)).css("font-weight", "bold");
                                tbody.append(tr);

                                //detalle de obras del fondo

                                var obras = $linq(data.d).where(function (x) { return x.UnidadPresupuestal == up & x.SubUnidadPresupuestal == sup & x.Fondos == f; }).toArray();

                                $.each(obras, function (index, n) {

                                        var tr = $("<tr>");
                                        tr.append($("<td>").addClass('details-open'));
                                        tr.append($("<td>").text(n.Numero));
                                        tr.append($("<td>").text(n.Descripcion));
                                        tr.append($("<td>").text(n.Municipio));
                                        tr.append($("<td>").text(n.Localidad));
                                        tr.append($("<td>").text(n.Contratista.replace('empty', ' ')));
                                        tr.append($("<td>").text('$' + parseFloat(n.Total).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                        tr.append($("<td>").text('$' + parseFloat(n.SinExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                        tr.append($("<td>").text('$' + parseFloat(n.ConExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                        tr.append($("<td>").text('$' + parseFloat(n.Tramitado).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                        tr.append($("<td>").text('$' + parseFloat(n.Saldo).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));

                                        tbody.append(tr);

                                }); // each obras


                                //subtotal por fondo

                                if (obras.length > 0) {

                                    var stotalTotal = $linq(obras).sum("x => x.Total");
                                    var stotalSinExpedienteTecnico = $linq(obras).sum("x => x.SinExpedienteTecnico");
                                    var stotalConExpedienteTecnico = $linq(obras).sum("x => x.ConExpedienteTecnico");
                                    var stotalTramitado = $linq(obras).sum("x => x.Tramitado");
                                    var stotalSaldo = $linq(obras).sum("x => x.Saldo");  
                              
                                    var tr = $("<tr>");
                                    tr.addClass('panel-footer text-right');
                                    tr.css("font-weight", "bold");
                                    tr.append($("<td>").text('SUBTOTAL > ' + f).attr('colspan', '6'));
                                    tr.append($("<td>").text('$' + parseFloat(stotalTotal).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                    tr.append($("<td>").text('$' + parseFloat(stotalSinExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                    tr.append($("<td>").text('$' + parseFloat(stotalConExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                    tr.append($("<td>").text('$' + parseFloat(stotalTramitado).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                    tr.append($("<td>").text('$' + parseFloat(stotalSaldo).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                    tbody.append(tr);

                                }


                            });// each fondos



                            //subtotal por sub unidad presupuestal

                            var obras = $linq(data.d).where(function (x) { return x.UnidadPresupuestal == up & x.SubUnidadPresupuestal == sup }).toArray();

                            if (sup != up)
                            {

                                var stotalTotal = $linq(obras).sum("x => x.Total");
                                var stotalSinExpedienteTecnico = $linq(obras).sum("x => x.SinExpedienteTecnico");
                                var stotalConExpedienteTecnico = $linq(obras).sum("x => x.ConExpedienteTecnico");
                                var stotalTramitado = $linq(obras).sum("x => x.Tramitado");
                                var stotalSaldo = $linq(obras).sum("x => x.Saldo");
                                
                               
                                var tr = $("<tr>");
                                tr.addClass('panel-footer text-right');
                                tr.css("font-weight", "bold");
                                tr.append($("<td>").text('SUBTOTAL > ' + sup).attr('colspan', '6'));
                                tr.append($("<td>").text('$' + parseFloat(stotalTotal).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                tr.append($("<td>").text('$' + parseFloat(stotalSinExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                tr.append($("<td>").text('$' + parseFloat(stotalConExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                tr.append($("<td>").text('$' + parseFloat(stotalTramitado).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                tr.append($("<td>").text('$' + parseFloat(stotalSaldo).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                                tbody.append(tr);                            
                                
                            }

                            

                        });// each sub unidades presupuestales


                        //subtotal por unidad presupuestal

                        var obras = $linq(data.d).where(function (x) { return x.UnidadPresupuestal == up }).toArray();                       

                            var stotalTotal = $linq(obras).sum("x => x.Total");
                            var stotalSinExpedienteTecnico = $linq(obras).sum("x => x.SinExpedienteTecnico");
                            var stotalConExpedienteTecnico = $linq(obras).sum("x => x.ConExpedienteTecnico");
                            var stotalTramitado = $linq(obras).sum("x => x.Tramitado");
                            var stotalSaldo = $linq(obras).sum("x => x.Saldo");
                           
                            var tr = $("<tr>");
                            tr.addClass('panel-footer text-right');
                            tr.css("font-weight", "bold");
                            tr.append($("<td>").text('SUBTOTAL > ' + up).attr('colspan', '6'));
                            tr.append($("<td>").text('$' + parseFloat(stotalTotal).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                            tr.append($("<td>").text('$' + parseFloat(stotalSinExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                            tr.append($("<td>").text('$' + parseFloat(stotalConExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                            tr.append($("<td>").text('$' + parseFloat(stotalTramitado).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                            tr.append($("<td>").text('$' + parseFloat(stotalSaldo).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                            tbody.append(tr); 
                        

                    }); // each unidades presupuestales

                    
                    //sumatoria general
                   
                    if (data.d.length > 0) {

                        var stotalTotal = $linq(data.d).sum("x => x.Total");
                        var stotalSinExpedienteTecnico = $linq(data.d).sum("x => x.SinExpedienteTecnico");
                        var stotalConExpedienteTecnico = $linq(data.d).sum("x => x.ConExpedienteTecnico");
                        var stotalTramitado = $linq(data.d).sum("x => x.Tramitado");
                        var stotalSaldo = $linq(data.d).sum("x => x.Saldo");

                        var tr = $("<tr>");
                        tr.addClass('panel-footer text-right');
                        tr.css("font-weight", "bold");
                        tr.append($("<td>").text('TOTAL GENERAL').attr('colspan', '6'));
                        tr.append($("<td>").text('$' + parseFloat(stotalTotal).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                        tr.append($("<td>").text('$' + parseFloat(stotalSinExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                        tr.append($("<td>").text('$' + parseFloat(stotalConExpedienteTecnico).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                        tr.append($("<td>").text('$' + parseFloat(stotalTramitado).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                        tr.append($("<td>").text('$' + parseFloat(stotalSaldo).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString()));
                        tbody.append(tr);

                    }
                    else
                    {
                        var tr = $("<tr>");
                        tr.addClass('text-center');
                        tr.append($("<td>").text('--- Sin registros ----').attr('colspan', '11'));
                        tbody.append(tr);
                    }

                    $('#tituloresumen').text('Total de obras y acciones: ' + data.d.length);


                },//success
                error: function (result) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresListado ul:first').empty();
                    $('#divErroresListado ul:first').append($("<li>").text(r.Message));
                    $('#divErroresListado').show();
                },
                failure: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresListado ul:first').empty();
                    $('#divErroresListado ul:first').append($("<li>").text(r.Message));
                    $('#divErroresListado').show();
                }

            });
        }





    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

     <div class="container">
                  

          <div class="alert alert-danger" id="divErroresListado" style="display:none">
            <ul></ul>
          </div>

         <div class="row">
            <div class="col-md-12 page-header">
                <div style="float:left"><h2>Consulta presupuestal</h2></div>
                <div style="float:right">
                    <button type="button" class="btn btn-default" aria-label="Left Align" id="btnFiltro">
                        <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                </button>        
                </div>
            </div> 
          </div>        
         


         <div class="table-responsive" id="divTabla">


            <table id="tblConsultaPresupuestal" class="table table-bordered">
            <thead>
                <tr>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th></th>
                    <th colspan="3" class="text-center">Autorizado</th>                   
                    <th></th>
                    <th></th>                                   
                </tr>
                <tr>
                    <th></th>
                    <th>No.</th>
                    <th>Descripción</th>
                    <th>Municipio</th>
                    <th>Localidad</th>
                    <th>Contratista</th>
                    <th>Total</th>
                    <th><center>Sin<br />expediente<br />técnico</center></th>
                    <th><center>Con<br />expediente<br />técnico</center></th>
                    <th><center>Tramitado<br />para su<br />Liberación</center></th>
                    <th>Saldo</th>                    
                </tr>
            </thead>
            <tbody>
                <tr><td colspan="11" class="text-center">-- Sin registros ---</td></tr>
            </tbody>
        </table>



         </div><!-- contenedor de lista -->          


     </div><!-- container -->   


     <div class="modal fade" id="modalFiltro">
          <div class="modal-dialog modal-lg">
            <div class="modal-content">

              <div class="modal-header alert alert-success">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Filtrar información</h4>
              </div>
              <div class="modal-body">


                         <div class="form-group">
                             <label for="Numero">Número de obra o acción</label>
                             <div>                                
                                 <input type="text" class="input-sm required form-control" id="txtFiltroNumeroObraAccion"  autofocus="autofocus" autocomplete="off" />                           
                             </div>                           
                          </div>


                          <div class="form-group">
                             <label for="Descripcion">Descripción</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroDescripcion"  autocomplete="off" />                           
                             </div>
                          </div>

                          <div class="form-group">
                             <label for="UnidadPresupuestal">Unidad o subunidad presupuestal</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroUnidadPresupuestal"  placeholder='ingrese texto descriptivo de la unidad o subunidad' autocomplete="off" />                           
                             </div>
                          </div>
                 
                          <div class="form-group">
                             <label for="Municipio">Municipio</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroMunicipio"  autocomplete="off" />                           
                             </div>
                          </div>
                  
                         <div class="form-group">
                             <label for="Localidad">Localidad</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroLocalidad"  autocomplete="off" />                           
                             </div>
                         </div>
                  
                         <div class="form-group">
                             <label for="Contratista">Contratista</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroContratista"  autocomplete="off" />                           
                             </div>
                         </div>
                  
                        <div class="form-group">
                             <label for="Fondos">Fondos</label>
                             <div>
                                 <input type="text" class="input-sm required form-control" id="txtFiltroFondos"  autocomplete="off" />                           
                             </div>
                        </div> 
                     


                  </div><!-- modal-body -->
              <div class="modal-footer">
                            
                  <button type="button" class="btn btn-primary" id="btnAplicarFiltro" >Filtrar</button>                                           
                  <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                          
              </div>

            </div><!-- modal-content -->
          </div><!-- modal-dialog -->
    </div> <!-- modal filtro -->


</asp:Content>

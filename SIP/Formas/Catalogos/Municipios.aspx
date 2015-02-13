<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="Municipios.aspx.cs" Inherits="SIP.Formas.Catalogos.Municipios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">

        .edit-control 
        {
                 background: url('../../img/edit1.png') no-repeat center center;
                 cursor: pointer;
        }

        .delete-control 
        {
                 background: url('../../img/close.png') no-repeat center center;
                 cursor: pointer;
        }

    </style>

    <script type="text/javascript">

        $(document).ready(function ()
        {


            $('.campoNumerico').autoNumeric('init');

            $('#tblMunicipios').dataTable();

            GetList();

            $('#btnAdd').click(function () {
                AddRecord();
            });

            $('#btnUpdate').click(function () {
                UpdateRecord();
            });

            $('#btnDelete').click(function () {
                DeleteRecord();
            });

            $('#btnNuevo').click(function () {
                BeforeAddRecord();
            });


            $('#tblMunicipios tbody').on('click', '.edit-control.btn', function () {

                var rowId = $(this).data("rowid");
                $(this).closest('tr').addClass('selected');
                BeforeEditRecord(rowId);
               
            });

            $('#tblMunicipios tbody').on('click', '.delete-control.btn', function () {

                var rowId = $(this).data("rowid");
                $(this).closest('tr').addClass('selected');
                BeforeDeleteRecord(rowId);

            });

            $(document).on('shown.bs.modal', function (e) {
                $('[autofocus]', e.target).focus();
            });

            $(document).on('hidden.bs.modal', function (e) {
                $('#tblMunicipios .selected').removeClass('selected');
            });
          

        }); //$(document).ready




        function GetList()
        {           
           
            $.ajax({

                type: "POST",
                url: "Municipios.aspx/GetList",
                contentType: "application/json;charset=utf-8",
                data: { },
                dataType: "json",
                success: function (data) {

                    //On success

                    $('#tblMunicipios').dataTable({
                        "stateSave": true,
                        "destroy": true,
                        "data": data.d,
                        "language": {
                            "lengthMenu": "Mostrando _MENU_ registros por página",
                            "zeroRecords": "- Sin resultados -",
                            "info": "Mostrando página _PAGE_ de _PAGES_",
                            "infoEmpty": "No se encontraron registros",
                            "infoFiltered": "(filtrado de _MAX_ registros en total)",
                            "search": "<span class='glyphicon glyphicon-search'></span>",
                            "paginate":
                                {
                                    "next": "siguiente",
                                    "previous": "anterior"
                                }
                        },
                        "columns": [
                            {
                                 "orderable": false,
                                 "data": null,
                                 "defaultContent": "<button type='button' class='edit-control btn'>&nbsp;</button><button type='button' class='delete-control btn'>&nbsp;</button>"
                            },
                            { "data": "Clave" },
                            { "data": "Nombre" },                           
                            { "data": "Orden" }

                        ],
                        "createdRow": function (row, data, index)
                        {                           
                            $('.edit-control.btn', row).attr('data-rowid', data.Id);
                            $('.delete-control.btn', row).attr('data-rowid', data.Id);                           
                        }

                    });


                },
                error: function (result)
                {    
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresListado ul:first').empty();
                    $('#divErroresListado ul:first').append($("<li>").text(r.Message));
                    $('#divErroresListado').show();
                }

            });
        }


        function AddRecord()
        {

            
            var clave = $('#txtClave').val();
            var nombre = $('#txtNombre').val();
            var orden = $('#txtOrden').val();
            

            $.ajax({
                type: "POST",
                url: "Municipios.aspx/AddRecord",
                data: JSON.stringify({ clave: clave, nombre: nombre, orden: orden }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.OK)
                    {

                        $('#modalEdicion').modal('hide');

                        GetList();

                    }
                    else
                    {
                        $('#divErroresEdit ul:first').empty();

                        $.map(response.d.Errors, function (n) {
                            $('#divErroresEdit ul:first').append($("<li>").text(n));
                        });

                        $('#divErroresEdit').show();
                    }

                },
                error: function (response)
                {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresEdit ul:first').empty();
                    $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                    $('#divErroresEdit').show();

                },
                failure: function (response)
                {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresEdit ul:first').empty();
                    $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                    $('#divErroresEdit').show();
                }
            });
        }

        function UpdateRecord()
        {
                        
            var rowId = sessionStorage.getItem("rowId");
            var clave = $('#txtClave').val();
            var nombre = $('#txtNombre').val();
            var orden = $('#txtOrden').val();

            $.ajax({
                type: "POST",
                url: "Municipios.aspx/UpdateRecord",
                data: JSON.stringify({ id: rowId, clave: clave, nombre: nombre, orden: orden }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.OK)
                    {
                        $('#modalEdicion').modal('hide');
                        GetList();
                    }
                    else
                    {

                        $('#divErroresEdit ul:first').empty();

                        $.map(response.d.Errors, function (n) {
                            $('#divErroresEdit ul:first').append($("<li>").text(n));
                        });

                        $('#divErroresEdit').show();
                    }

                },
                error: function (response)
                {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresEdit ul:first').empty();
                    $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                    $('#divErroresEdit').show();

                },
                failure: function (response)
                {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresEdit ul:first').empty();
                    $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                    $('#divErroresEdit').show();
                }
            });
        }

        function DeleteRecord()
        {

            
            var rowId = sessionStorage.getItem("rowId");
            

            $.ajax({
                type: "POST",
                url: "Municipios.aspx/DeleteRecord",
                data: JSON.stringify({ id: rowId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response)
                {

                    if (response.d.OK)
                    {
                        $('#modalBorrar').modal('hide');
                        GetList();
                    }
                    else
                    {
                        $('#divErroresDelete ul:first').empty();

                        $.map(response.d.Errors, function (n) {
                            $('#divErroresDelete ul:first').append($("<li>").text(n));
                        });

                        $('#divErroresDelete').show();
                    }

                },
                error: function (response)
                {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresDelete ul:first').empty();
                    $('#divErroresDelete ul:first').append($("<li>").text(r.Message));
                    $('#divErroresDelete').show();

                },
                failure: function (response)
                {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresDelete ul:first').empty();
                    $('#divErroresDelete ul:first').append($("<li>").text(r.Message));
                    $('#divErroresDelete').show();
                }

            });
        }

        function GetRecord(rowId)
        {           

            $.ajax(
                {

                    type: "POST",
                    url: "Municipios.aspx/GetRecord",
                    data: JSON.stringify({ id: rowId }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response)
                    {                                               

                        $('#txtClave').val(response.d.Clave);
                        $('#txtNombre').val(response.d.Nombre);
                        $('#txtOrden').val(response.d.Orden);                                             

                    },
                    error: function (response)
                    {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();

                    },
                    failure: function (response)
                    {
                        var r = jQuery.parseJSON(response.responseText);

                        $('#divErroresEdit ul:first').empty();
                        $('#divErroresEdit ul:first').append($("<li>").text(r.Message));
                        $('#divErroresEdit').show();
                    }

                });
        }

        function BeforeAddRecord()
        {

            $('#txtClave').val('');
            $('#txtNombre').val('');
            $('#txtOrden').val('');           

            $('#btnUpdate').hide();
            $('#btnAdd').show();

            $('#divErroresEdit').hide();
            $('#modalEdicion .modal-title').html('Agregando registro');
            $('#modalEdicion').modal({ backdrop: 'static', show: true });
            
        }

        function BeforeEditRecord(rowId)
        {

            sessionStorage.setItem("rowId", rowId);
            
            GetRecord(rowId);

            $('#btnAdd').hide();
            $('#btnUpdate').show();

            $('#divErroresEdit').hide();
            $('#modalEdicion .modal-title').html('Modificando registro');
            $('#modalEdicion').modal({ backdrop: 'static', show: true });

        }

        function BeforeDeleteRecord(rowId)
        {           
            sessionStorage.setItem("rowId", rowId);
            $('#divErroresDelete').hide();
            $('#modalBorrar').modal({ backdrop: 'static', show: true });            
        }

        function ShowErrors(data)
        {

        }


    </script>

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

    <div class="container">
             
        <div class="panel panel-success">
          <div class="panel-heading">
            <h3 class="panel-title"><strong>Municipios</strong></h3>
          </div>
          <div class="panel-body">
             
                <div class="alert alert-danger" id="divErroresListado" style="display:none">
                      <ul></ul>
                </div>

                <table id="tblMunicipios" class="table table-condensed table-bordered">
                    <thead>
                        <tr>
                          <th class="col-md-1 panel-footer">Acciones</th>
                          <th class="col-md-1 panel-footer">Clave</th>
                          <th class="panel-footer">Nombre</th>                         
                          <th class="col-md-1 panel-footer">Orden</th>
                        </tr>                        
                    </thead>
                </table>                          
         
            <input type="button"  class="btn btn-default" id="btnNuevo" title="Agregar registro" value="Nuevo" />

          </div><!-- panel-body -->
        </div><!-- panel -->        
    </div><!-- container -->



    <div class="modal fade" id="modalEdicion">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header alert alert-success">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Modificando registro</h4>
              </div>
              <div class="modal-body">

                  <div class="alert alert-danger" id="divErroresEdit" style="display:none">
                      <ul></ul>
                  </div>


                  <div class="form-group">
                     <label for="Clave">Clave</label>
                     <div>
                         <input type="text" class="input-sm required form-control" id="txtClave"  autofocus="autofocus" autocomplete="off" />                           
                     </div>
                  </div>

                  <div class="form-group">
                     <label for="Nombre">Nombre</label>
                     <div>
                         <input type="text" class="input-sm required form-control" id="txtNombre" autocomplete="off" />                           
                     </div>
                  </div>

                  <div class="form-group">
                     <label for="Orden">Orden</label>
                     <div>
                         <input type="text" class="input-sm required form-control campoNumerico" id="txtOrden"  data-v-max="999" data-m-dec="0" autocomplete="off" />                           
                     </div>
                  </div>  
                                 
              </div>
              <div class="modal-footer">
                            
                  <button type="button" class="btn btn-default" id="btnAdd" style="display:none">Guardar</button>
                  <button type="button" class="btn btn-default" id="btnUpdate" style="display:none">Guardar</button>                         
                  <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                          
              </div>
            </div><!-- modal-content -->
          </div><!-- modal-dialog -->
        </div> <!-- modal edicion -->

    <div class="modal fade" id="modalBorrar">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header alert alert-success">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title">Confirmar operación</h4>
          </div>
          <div class="modal-body">

              <div class="alert alert-danger" id="divErroresDelete" style="display:none">
                  <ul></ul>
              </div>

              ¿Desea borrar realmente este registro?
            
          </div>
          <div class="modal-footer">
                            
              <button type="button" class="btn btn-default" id="btnDelete">Borrar</button>                                
              <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                          
          </div>
        </div><!-- modal-content -->
      </div><!-- modal-dialog -->
    </div>  <!-- modal borrar -->
    
  
    
</asp:Content>

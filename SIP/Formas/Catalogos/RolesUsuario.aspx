<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="RolesUsuario.aspx.cs" Inherits="SIP.Formas.Catalogos.RolesUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
                                  
            $('.campoNumerico').autoNumeric('init');

            $('#btnAdd').click(function () 
            {
                AddRecord();
            });

            $('#btnUpdate').click(function () 
            {
                UpdateRecord();
            });

            $('#btnDelete').click(function ()
            {
                DeleteRecord();
            });

        });
     

        function AddRecord()
        {           

            var clave=$('#txtClave').val();
            var nombre=$('#txtNombre').val();
            var orden = $('#txtOrden').val();
            var dependencia = $('#rbDependencia').is(":checked");
            var sefiplan = $('#rbSefiplan').is(":checked");
            
            $.ajax({
                type: "POST",
                url: "RolesUsuario.aspx/AddRecord",
                data: JSON.stringify({ clave: clave , nombre: nombre , orden: orden, dependencia: dependencia, sefiplan: sefiplan }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response)
                {

                    if (response.d.OK) 
                    {
                        $('#modalEdicion').modal('hide');                       

                    }
                    else 
                    {
                        $('#divErrores ul:first').empty();

                        $.map(response.d.Errors, function (n) {
                            $('#divErrores ul:first').append($("<li>").text(n));
                        });

                        $('#divErrores').show();
                    }

                },
                error: function (response)
                {
                    var r = jQuery.parseJSON(response.responseText);
                    
                    $('#divErrores ul:first').empty();
                    $('#divErrores ul:first').append($("<li>").text(r.Message));
                    $('#divErrores').show();


                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        
        function UpdateRecord() 
        {
          
            var rowId = $('#rowId').val();
            var clave = $('#txtClave').val();
            var nombre = $('#txtNombre').val();
            var orden = $('#txtOrden').val();
            var dependencia = $('#rbDependencia').is(":checked");
            var sefiplan = $('#rbSefiplan').is(":checked");
                     

            $.ajax({
                type: "POST",
                url: "RolesUsuario.aspx/UpdateRecord",
                data: JSON.stringify({ id: rowId, clave: clave, nombre: nombre, orden: orden, dependencia: dependencia, sefiplan: sefiplan }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.OK)
                    {
                        $('#modalEdicion').modal('hide');                       
                    }
                    else
                    {
                        $('#divErrores ul:first').empty();

                        $.map(response.d.Errors, function (n) {
                            $('#divErrores ul:first').append($("<li>").text(n));
                        });

                        $('#divErrores').show();
                    }

                },
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErrores ul:first').empty();
                    $('#divErrores ul:first').append($("<li>").text(r.Message));
                    $('#divErrores').show();


                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function DeleteRecord()
        {           
            
            var rowId = $('#rowId').val();
            var clave = $('#txtClave').val();
            var nombre = $('#txtNombre').val();
            var orden = $('#txtOrden').val();

            $.ajax({
                type: "POST",
                url: "RolesUsuario.aspx/DeleteRecord",
                data: JSON.stringify({ id: rowId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.d.OK)
                    {
                        $('#modalBorrar').modal('hide');                       
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
                error: function (response) {
                    var r = jQuery.parseJSON(response.responseText);

                    $('#divErroresDelete ul:first').empty();
                    $('#divErroresDelete ul:first').append($("<li>").text(r.Message));
                    $('#divErroresDelete').show();


                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        function GetRecord(recordId) 
        {            

            $.ajax(
                {

                type: "POST",
                url: "RolesUsuario.aspx/GetRecord",
                data: JSON.stringify({ id: recordId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    $('#txtClave').val(response.d.Clave);
                    $('#txtNombre').val(response.d.Nombre);
                    $('#txtOrden').val(response.d.Orden);
                                     
                    $('#rbDependencia').prop('checked', response.d.EsDependencia);
                    $('#rbSefiplan').prop('checked', response.d.EsSefiplan);
                    
                }
               
            });
        }
        
        function BeforeAddRecord()
        {

            $('#txtClave').val('');
            $('#txtNombre').val('');
            $('#txtOrden').val('');
            $('#rbDependencia').prop("checked",true);           

            $('#btnUpdate').hide();
            $('#btnAdd').show();
            
            $('#modalEdicion').modal('show');
        }

        function BeforeUpdateRecord(rowId) 
        {

            $('#rowId').val(rowId);         

            GetRecord(rowId);

            $('#btnAdd').hide();
            $('#btnUpdate').show();

            $('#modalEdicion').modal('show');

        }

        function BeforeDeleteRecord(rowId)
        {           
            $('#rowId').val(rowId);
            $('#modalBorrar').modal('show');
        }
        

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server"></asp:ScriptManager>

    <div class="container">
             
        <div class="panel panel-success">
          <div class="panel-heading">
            <h3 class="panel-title"><strong>Roles de usuario</strong></h3>
          </div>
          <div class="panel-body">

              <asp:GridView ID="GridViewRoles" runat="server"
                ItemType="DataAccessLayer.Models.Rol" DataKeyNames="Id"
                SelectMethod="GridViewRoles_GetData"
                ShowHeaderWhenEmpty="true" CssClass="table" AutoGenerateColumns="False" 
                AllowPaging="True" 
                OnPageIndexChanging="GridViewRoles_PageIndexChanging">
                <Columns>

                           <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">
                                <ItemTemplate>                               
                                    <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" />
                                    <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick="imgBtnEliminar_Click" />
                                </ItemTemplate>                         
                            </asp:TemplateField>

                           <asp:DynamicField DataField="Clave" HeaderText="Clave" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer"/>
                           <asp:DynamicField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="panel-footer"/> 
                           <asp:DynamicField DataField="Orden" HeaderText="Orden" HeaderStyle-CssClass="panel-footer"/>
                           <asp:DynamicField DataField="EsDependencia" HeaderText="Dependencia" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer"/>
                           <asp:DynamicField DataField="EsSefiplan" HeaderText="Sefiplan" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer"/> 
                                         
                       
                </Columns>
                    
                <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                    
            </asp:GridView>

            <asp:Button ID="btnNuevo" runat="server" Text="Agregar rol" CssClass="btn btn-default" OnClick="btnNuevo_Click" />             
            

          </div><!-- panel-body -->
        </div><!-- panel -->
        
    </div><!-- container -->



    <div class="modal fade" id="modalEdicion">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header alert alert-success">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title"><%= ViewState["tituloModal"] %></h4>
              </div>
              <div class="modal-body">

                  <div class="alert alert-danger" id="divErrores" style="display:none">
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
                         <input type="text" class="input-sm required form-control campoNumerico" id="txtOrden" data-m-dec="0" autocomplete="off" />                           
                     </div>
                  </div>                   
                         
                  <div class="form-group">
                      <label class="radio-inline"><input type="radio" name="optradio" id="rbDependencia"/>Dependencias</label>
                      <label class="radio-inline"><input type="radio" name="optradio" id="rbSefiplan"/>Sefiplan</label>                                
                  </div>          
                         
            
              </div>
              <div class="modal-footer">
                            
                  <button type="button" class="btn btn-primary" id="btnAdd" style="display:none">Guardar</button>
                  <button type="button" class="btn btn-primary" id="btnUpdate" style="display:none">Guardar</button>                         
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

              <strong>¿Desea borrar realmente este registro?</strong>
            
          </div>
          <div class="modal-footer">
                            
              <button type="button" class="btn btn-primary" id="btnDelete">Borrar</button>                                
              <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                          
          </div>
        </div><!-- modal-content -->
      </div><!-- modal-dialog -->
    </div>  <!-- modal borrar -->
    
    <input type="hidden" id="rowId" />

</asp:Content>

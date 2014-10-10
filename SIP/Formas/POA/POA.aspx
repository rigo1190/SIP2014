<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="POA.aspx.cs" Inherits="SIP.Formas.POA.POA" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script type="text/javascript">

         $(document).ready(function () {


             alert("document ready");             
                                                  
             $('.campoNumerico').autoNumeric('init');             


             $("#<%= ddlPrograma.ClientID %>").change(function ()
             {
             
                 var id = $(this).children(":selected").attr("value");
                 fnc_fillddlSubPrograma(id);

             });

             $("#ddlSubprograma").change(function ()
             {
                
                 var id = $(this).children(":selected").attr("value");
                 fnc_fillddlSubSubPrograma(id);
             });

             $("#ddlTipologia").change(function ()
             {                 
                 var id = $(this).children(":selected").attr("value");
                 fnc_fillddlMeta(id);

             });
             

             $('*[data-tipo-operacion]').click(function ()
             {                                 
                 
                 if ($("#<%= divEdicion.ClientID %>").is(':visible'))
                 {
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
                        case "EVALUAR":
                            var url = $(this).data("url-poa");
                            $(location).attr('href', url);                            
                           break;
                       default:
                           break;
                   }

                   return false;

             });
             


         });

         function fnc_fillddlSubPrograma(parentid)
         {
             PageMethods.GetAperturaProgramaticaNivel2(parentid,fnc_OnSuccessA, fnc_OnErrorA);
         }

         function fnc_OnSuccessA(response) {
             

             if (response != null) {                                
                      

                 var optionssubprograma = $("#ddlSubprograma");
                 var optionstipologia = $("#ddlTipologia");
                 var optionsmeta = $("#ddlMeta");


                 if (response.length == 0)
                 {
                     optionssubprograma.find('option').remove().end();
                     optionstipologia.find('option').remove().end();
                     optionsmeta.find('option').remove().end();
                 }
                 else
                 {
                     optionssubprograma.find('option').remove().end().append('<option value="0">Seleccione...</option>');
                     optionstipologia.find('option').remove().end();
                     optionsmeta.find('option').remove().end();

                     $.each(response, function () {
                         optionssubprograma.append($("<option />").val(this.Value).text(this.Text));
                     });

                 }
                 
             }
             else {
                 alert("Response es null");
             }

         }

         function fnc_OnErrorA(error) {
             alert("El error es " + error.get_message());
         }

       

         function fnc_fillddlSubSubPrograma(parentid) {
             PageMethods.GetAperturaProgramaticaNivel3(parentid, fnc_OnSuccessB, fnc_OnErrorB);
         }

         function fnc_OnSuccessB(response)
         {           

             if (response != null) {

                
                 var optionstipologia = $("#ddlTipologia");
                 var optionsmeta = $("#ddlMeta");
                 

                 if (response.length == 0)
                 {
                     optionstipologia.find('option').remove().end();
                     optionsmeta.find('option').remove().end();
                 }
                 else
                 {
                     optionstipologia.find('option').remove().end().append('<option value="0">Seleccione...</option>');
                     optionsmeta.find('option').remove().end();

                     $.each(response, function () {
                         optionstipologia.append($("<option />").val(this.Value).text(this.Text));
                     });

                 }
                 

             }
             else {
                 alert("Response es null");
             }

         }

         function fnc_OnErrorB(error) {
             alert("El error es " + error.get_message());
         }


         function fnc_fillddlMeta(tipologiaid) {
             PageMethods.GetMetasAperturaProgramatica(tipologiaid, fnc_OnSuccessC, fnc_OnErrorC);
         }

         function fnc_OnSuccessC(response)
         {
            
             if (response != null) {                
                
                 var optionsmetas = $("#ddlMeta");               
                 
                 if (response.length == 0)
                 {                    
                     optionsmetas.find('option').remove().end();
                 }
                 else
                 {
                     optionsmetas.find('option').remove().end().append('<option value="0">Seleccione...</option>');
                     
                     $.each(response, function () {
                         optionsmetas.append($("<option />").val(this.Value).text(this.Text));
                     });

                 }
                 
             }
             else {
                 alert("Response es null");
             }

         }

         function fnc_OnErrorC(error) {
             alert("El error es " + error.get_message());
         }







      
                        

         function fnc_Validar() {


             <%--var numero = $("#<%=txtNumero.ClientID%>").val();
             if (numero == null || numero.length == 0 || numero == undefined) {
                 alert("El campo Número no puede estar vacio");
                 return false;
             }--%>

            var desc = $("#<%=txtDescripcion.ClientID%>").val();
            if (desc == null || desc.length == 0 || desc == undefined) {
                alert("El campo descripción no puede estar vacio");
                return false;
            }

            var municipio = $("#<%=ddlMunicipio.ClientID%>").val();
             if (municipio == null || municipio.length == 0 || municipio == undefined || municipio == 0) {
                alert("El campo Municipio no puede estar vacio");
                return false;
            }

             var localidad = $("#<%=txtLocalidad.ClientID%>").val();
             if (localidad == null || localidad.length == 0 || localidad == undefined) {
                 alert("El campo Localidad no puede estar vacio");
                 return false;
             }

             var tipolocalidad = $("#<%=ddlTipoLocalidad.ClientID%>").val();
             if (tipolocalidad == null || tipolocalidad.length == 0 || tipolocalidad == undefined || tipolocalidad == 0) {
                 alert("Debe indicar el tipo de localidad");
                 return false;
             }

             var subsubprograma = $("#ddlTipologia").val();
             if (subsubprograma == null || subsubprograma.length == 0 || subsubprograma == undefined || subsubprograma == 0) {
                 alert("Debe indicar el tipo de la apertura programatica");
                 return false;
             }

             var meta = $("#ddlMeta").val();
             if (meta == null || meta.length == 0 || meta == undefined || meta == 0) {
                 alert("Debe indicar la unidad y beneficiario de la meta");
                 return false;
             }

             var numeroBeneficiarios = $("#<%=txtNumeroBeneficiarios.ClientID%>").val();
             if (numeroBeneficiarios == null || numeroBeneficiarios.length == 0 || numeroBeneficiarios == undefined) {
                 alert("El campo Número de beneficiarios no puede estar vacio");
                 return false;
             }

             var cantidadUnidades = $("#<%=txtCantidadUnidades.ClientID%>").val();
             if (cantidadUnidades == null || cantidadUnidades.length == 0 || cantidadUnidades == undefined) {
                 alert("El campo Cantidad de Unidades no puede estar vacio");
                 return false;
             }

             var situacionObra = $("#<%=ddlSituacionObra.ClientID%>").val();
             if (situacionObra == null || situacionObra.length == 0 || situacionObra == undefined || situacionObra == 0) {
                 alert("Debe indicar la situación de la obra");
                 return false;
             }

             var modalidad = $("#<%=ddlModalidad.ClientID%>").val();
             if (modalidad == null || modalidad.length == 0 || modalidad == undefined || modalidad == 0) {
                 alert("Debe indicar la modalidad de la obra");
                 return false;
             }

             var importetotal = $("#<%=txtImporteTotal.ClientID%>").val();
             if (importetotal == null || importetotal.length == 0 || importetotal == undefined) {
                 alert("El campo Importe total no puede estar vacio");
                 return false;
             }



            return true;
        }

         function fnc_OcultarDivs(sender) {
             $("#<%= divBtnNuevo.ClientID %>").css("display", "block");
             $("#<%= divEdicion.ClientID %>").css("display", "none");
              return false;
          }


        function fnc_EjecutarMensaje(mensaje) {
            alert(mensaje);
        }

        function fnc_IrDesdeGrid(url) {
            $(location).attr('href', url);
        }
             

        window.onload = function () {
            return "Se dispara evento window.onload";
        }


    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       
    
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>--%>

    <div class="container">
       
        <div class="page-header"><h3><asp:Label ID="lblTituloPOA" runat="server" Text=""></asp:Label></h3></div>

        <asp:GridView Height="25px" ShowHeaderWhenEmpty="true" CssClass="table" ID="GridViewObras" DataKeyNames="Id" AutoGenerateColumns="False" OnRowDataBound="GridViewObras_RowDataBound" runat="server" AllowPaging="True">
            <Columns>

                       <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">
                            <ItemTemplate>
                                                    
                                <asp:ImageButton ID="imgBtnEdit" ToolTip="Editar" runat="server" ImageUrl="~/img/Edit1.png" OnClick="imgBtnEdit_Click" data-tipo-operacion="editar"/>
                                <asp:ImageButton ID="imgBtnEliminar" ToolTip="Borrar" runat="server" ImageUrl="~/img/close.png" OnClick="imgBtnEliminar_Click" data-tipo-operacion="borrar"/>

                            </ItemTemplate>                         
                        </asp:TemplateField>     
                                         
                       <asp:TemplateField HeaderText="Numero" ItemStyle-CssClass="col-md-1" HeaderStyle-CssClass="panel-footer">                          
                            <ItemTemplate>
                                <asp:Label ID="LabelNumero" runat="server" Text='<%# Bind("Numero") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                       <asp:TemplateField HeaderText="Descripcion" ItemStyle-CssClass="col-md-8" HeaderStyle-CssClass="panel-footer">                            
                            <ItemTemplate>
                                <asp:Label ID="labelDescripcion" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Evaluación de Obra" ItemStyle-CssClass="col-md-2" HeaderStyle-CssClass="panel-footer">
                            <ItemTemplate>
                                    <button type="button" id="btnE" data-tipo-operacion="evaluar" runat="server" class="btn btn-default"> <span class="glyphicon glyphicon-ok"></span></button> 
                            </ItemTemplate>                          
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />                                            
                        </asp:TemplateField>

            </Columns>
                    
            <PagerSettings FirstPageText="Primera" LastPageText="Ultima" Mode="NextPreviousFirstLast" NextPageText="Siguiente" PreviousPageText="Anterior" />
                    
        </asp:GridView>

        <div id="divBtnNuevo" runat="server" style="display:block">
              <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn btn-default" OnClick="btnNuevo_Click" AutoPostBack="false" />
         </div>
    
        <div id="divEdicion" runat="server" class="panel-footer" style="display:none">
            
            <ul class="nav nav-tabs" role="tablist">
              <li class="active"><a href="#datosgenerales" role="tab" data-toggle="tab">Datos generales</a></li>
              <li><a href="#planveracruzanodesarrollo" role="tab" data-toggle="tab">Plan Veracruzano de Desarrollo</a></li>        
            </ul>

            <div class="tab-content">

                <div class="panel-footer alert alert-danger" id="divMsg" style="display:none" runat="server">
                    <asp:Label ID="lblMensajes" runat="server" Text=""></asp:Label>
                </div>
                
                <div class="tab-pane active" id="datosgenerales">

                     <div class="row">

                     <br />   


                 <div class="col-md-4">

                      <div class="form-group">
                           <label for="Numero">Numero</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtNumero" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off" disabled="disabled"/>                           
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="Descripcion">Descripcion</label>
                         <div>
                            <textarea id="txtDescripcion" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="3" autofocus></textarea>
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="Municipio">Municipio</label>
                         <div>
                             <asp:DropDownList ID="ddlMunicipio" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                      </div>

                      <div class="form-group">
                           <label for="Localidad">Localidad</label>
                         <div>
                            <input type="text" class="input-sm required form-control" id="txtLocalidad" runat="server" style="text-align: left; align-items:flex-start" autocomplete="off" />
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="TipoLocalidad">Tipo de localidad</label>
                         <div>
                             <asp:DropDownList ID="ddlTipoLocalidad" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="ddlCriterioPriorizacion">Criterio de priorización</label>
                         <div>
                             <asp:DropDownList ID="ddlCriterioPriorizacion" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="EsAccion">Es acción</label>
                         <div>
                             <input type="checkbox" class="input-sm required form-control" id="txtEsAccion" runat="server" style="text-align: right; align-items:flex-start" />
                        </div>
                      </div>

                 </div>

                 <div class="col-md-4">

                      <div class="form-group">
                           <label for="Programa">Programa</label>
                         <div>
                             <asp:DropDownList ID="ddlPrograma" CssClass="form-control" runat="server"></asp:DropDownList>
                             <%--<select id="ddlPrograma" class="form-control"></select>--%>
                        </div>
                      </div>

                      <div class="form-group">
                           <label for="SubPrograma">SubPrograma</label>
                         <div>
                             <%--<asp:DropDownList ID="ddlSubprograma" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubprograma_SelectedIndexChanged"></asp:DropDownList>--%>
                             <select id="ddlSubprograma" class="form-control"></select>                          
                        </div>
                      </div>

                       <div class="form-group">
                           <label for="SubSubPrograma">SubSubPrograma</label>
                         <div>
                             <%--<asp:DropDownList ID="ddlTipologia" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTipologia_SelectedIndexChanged"></asp:DropDownList>--%>
                             <select id="ddlTipologia" class="form-control"></select>    
                        </div>
                      </div>

                       <div class="form-group">
                           <label for="Metas">Metas</label>
                         <div>
                             <%--<asp:DropDownList ID="ddlMeta" CssClass="form-control" runat="server"></asp:DropDownList>--%>
                             <select id="ddlMeta" class="form-control"></select> 
                        </div>
                      </div>

                      <div class="form-group">
                           <label for="NumeroBeneficiarios">Número de beneficiarios</label>
                         <div>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtNumeroBeneficiarios" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="9999"/>
                        </div>
                      </div>

                       <div class="form-group">
                           <label for="CantidadUnidades">Cantidad de unidades</label>
                         <div>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtCantidadUnidades" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="9999"/>
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="Empleos">Empleos</label>
                         <div>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtEmpleos" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="9999"/>
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="Jornales">Jornales</label>
                         <div>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtJornales" runat="server" style="text-align: left; align-items:flex-start" data-v-min="0" data-v-max="9999"/>
                        </div>
                      </div>


                  </div>

                 <div class="col-md-4">

                      <div class="form-group">
                           <label for="SituacionObra">Situación</label>
                         <div>
                              <asp:DropDownList ID="ddlSituacionObra" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                      </div>

                      <div class="form-group">
                           <label for="ModalidadObra">Modalidad</label>
                         <div>
                              <asp:DropDownList ID="ddlModalidad" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="txtImporteTotal">Costo total</label>
                         <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtImporteTotal" runat="server" style="text-align: left; align-items:flex-start" />
                        </div>
                      </div>

                      <div class="form-group">
                           <label for="txtCostoLiberadoEjerciciosAnteriores">Costo liberado en ejercicios anteriores</label>
                         <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtCostoLiberadoEjerciciosAnteriores" runat="server" style="text-align: left; align-items:flex-start" />
                        </div>
                      </div>

                     <div class="form-group">
                           <label for="txtPresupuestoEjercicio">Presupuesto del ejercicio</label>
                         <div class="input-group">
                            <span class="input-group-addon">$</span>
                            <input type="text" class="input-sm required form-control campoNumerico" id="txtPresupuestoEjercicio" runat="server" style="text-align: left; align-items:flex-start" />
                        </div>
                      </div>



                     <div class="form-group">
                           <label for="Observaciones">Observaciones</label>
                         <div>
                            <textarea id="txtObservaciones" class="input-sm required form-control" runat="server" style="text-align: left; align-items:flex-start" rows="4"></textarea>
                        </div>
                      </div>

                 </div>
                

            </div>

                </div><!--datosgenerales-->

                <div class="tab-pane" id="planveracruzanodesarrollo">
                    
                     <div class="row">

                        <br />

                        <div class="col-md-4">

                            <div class="panel panel-default">
                              <div class="panel-heading">
                                <h3 class="panel-title">Funcionalidad</h3>
                              </div>
                              <div class="panel-body">

                                  <div class="form-group">
                                       <label for="Finalidad">Finalidad</label>
                                     <div>
                                         <asp:DropDownList ID="ddlFinalidad" CssClass="form-control" runat="server" ></asp:DropDownList>
                                    </div>
                                  </div>

                                  <div class="form-group">
                                       <label for="Funcion">Funcion</label>
                                     <div>
                                         <asp:DropDownList ID="ddlFuncion" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFuncion_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                  </div>

                                  <div class="form-group">
                                     <label for="SubFuncion">SubFuncion</label>
                                     <div>
                                         <asp:DropDownList ID="ddlSubFuncion" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSubFuncion_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                  </div>
                                                                  
                              </div>

                            </div><!--Funcionalidad panel panel-default-->

                            <div class="panel panel-default">
                              <div class="panel-heading">
                                <h3 class="panel-title">Eje</h3>
                              </div>
                              <div class="panel-body">

                                  <div class="form-group">
                                       <label for="EjeAgrupador">Agrupador</label>
                                     <div>
                                         <asp:DropDownList OnSelectedIndexChanged="ddlEjeAgrupador_SelectedIndexChanged" ID="ddlEjeAgrupador" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                  </div>

                                  <div class="form-group">
                                       <label for="EjeElemento">Elemento</label>
                                     <div>
                                         <asp:DropDownList OnSelectedIndexChanged="ddlEjeElemento_SelectedIndexChanged" ID="ddlEjeElemento" CssClass="form-control" runat="server" ></asp:DropDownList>
                                    </div>
                                  </div>
                                                                                                    
                              </div>

                            </div><!--Eje panel panel-default-->                                                       
                                                        
                                                            
                        </div><!--col-md-4-->
                         
                        <div class="col-md-4">

                               <div class="form-group">
                                   <label for="PlanSectorial">Plan Sectorial</label>
                                 <div>
                                     <asp:DropDownList OnSelectedIndexChanged="ddlPlanSectorial_SelectedIndexChanged" ID="ddlPlanSectorial" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                              </div> 
                            
                              <div class="panel panel-default">
                              <div class="panel-heading">
                                <h3 class="panel-title">Clasificación Programática CONAC</h3>
                              </div>
                              <div class="panel-body">

                                  <div class="form-group">
                                       <label for="ddlModalidadAgrupador">Agrupador</label>
                                     <div>
                                         <asp:DropDownList OnSelectedIndexChanged="ddlModalidadAgrupador_SelectedIndexChanged" ID="ddlModalidadAgrupador" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                  </div>

                                  <div class="form-group">
                                       <label for="ddlModalidadElemento">Elemento</label>
                                     <div>
                                         <asp:DropDownList OnSelectedIndexChanged="ddlModalidadElemento_SelectedIndexChanged" ID="ddlModalidadElemento" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                  </div>
                                                                                                    
                              </div>

                            </div><!--Clasificación Programática panel panel-default-->   
                                 
                            <div class="form-group">
                                   <label for="Programa">Programa</label>
                                 <div>
                                      <asp:DropDownList ID="ddlProgramaPresupuesto" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                              </div>

                              <div class="form-group">
                                   <label for="GrupoBeneficiarios">Grupo de beneficiarios</label>
                                 <div>
                                      <asp:DropDownList ID="ddlGrupoBeneficiario" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                              </div>

                        </div><!--col-md-4-->                                                
                
                    </div><!--row-->


                </div><!--planveracruzanodesarrollo--> 
                
                 <div class="form-group header">
                    <asp:Button  CssClass="btn btn-default" Text="Guardar" ID="btnGuardar" runat="server" OnClientClick="return fnc_Validar()" OnClick="btnGuardar_Click" AutoPostBack="false" />
                    <asp:Button  CssClass="btn btn-default" Text="Cancelar" ID="btnCancelar" runat="server" OnClientClick="return fnc_OcultarDivs()" AutoPostBack="false" />
                </div>        

            </div>                                        
                               

             <div style="display:none" runat="server">
                <asp:TextBox ID="_ID" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>
                <asp:TextBox ID="_Accion" runat="server" Enable="false" BorderColor="White" BorderStyle="None" ForeColor="White"></asp:TextBox>                                    
             </div>                       
                     

       </div><!--divEdicion-->

    </div>
          

</asp:Content>

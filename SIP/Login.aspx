﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MLogin.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SIP.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 

    <script type="text/javascript">


        $(document).ready(function () {

            $('#txtLogin').keyup(function () { $("#divMensaje").css("display", "none"); });
            $('#txtContrasena').keyup(function () { $("#divMensaje").css("display", "none"); });

            $('#txtLogin').change(function () { $('#<%= hiddenLogin.ClientID %>').val($('#txtLogin').val()) });
            $('#txtContrasena').change(function () { $('#<%= hiddenContrasena.ClientID %>').val($('#txtContrasena').val()); });

        });


        function fnc_ShowMensaje() {         
            $("#divMensaje").css("display", "block");
        }

        function fnc_HideMensaje() {         
            $("#divMensaje").css("display", "none");
        }       


    </script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       
        <br />
        <br />
        <div class="container">

             <div class="well text-center"><h3>Sistema de Inversión Pública</h3></div>     
            
            <div class="form-group">
                 <img class="img-circle center-block" src="<%= ResolveClientUrl("~/img/photo.png")%>" alt="" />
            </div>          
            
                                                             
            <div class="row">
                                
               
                 <div class="col-md-offset-4 col-md-4">  
                                            
                   
                    <p>Por favor proporcione los datos requeridos.</p>

                    <div class="alert alert-danger" id="divMensaje" style="display:none">
                        <asp:Label ID="lblMensajes" EnableViewState="false" runat="server" Text="" CssClass="font-weight:bold"></asp:Label>
                    </div>
                   
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                            <input type="text" class="form-control" id="txtLogin" placeholder="Usuario" required autofocus autocomplete="off" />

                        </div>
                    </div>

                    <div class="form-group">
                        <div class="input-group">
                             <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                            <input type="password" class="form-control" id="txtContrasena" placeholder="Password" required  />
                           
                        </div>
                    </div>
                    
                    <div class="form-group pull-right">
                        <div class="input-group">
                            <asp:Button ID="btnEntrar" runat="server" Text="Entrar" CssClass="btn btn-default" OnClick="btnEntrar_Click"   />
                        </div>
                    </div>

                     <asp:HiddenField ID="hiddenLogin" runat="server" />
                     <asp:HiddenField ID="hiddenContrasena" runat="server" />

                  </div>
                               
                
            </div>                      
                        
            
        </div>
   
</asp:Content>

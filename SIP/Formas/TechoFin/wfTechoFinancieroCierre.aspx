<%@ Page Title="" Language="C#" MasterPageFile="~/NavegadorPrincipal.Master" AutoEventWireup="true" CodeBehind="wfTechoFinancieroCierre.aspx.cs" Inherits="SIP.Formas.TechoFin.wfTechoFinancieroCierre" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">

        

          function fnc_Confirmar() {
              return confirm("¿Está seguro de eliminar el registro?");
          }


          

    </script>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <div class="panel panel-success">

            <div class="panel-heading">
                <h3 class="panel-title">Cerrar etapa de carga inicial</h3>
            </div>
        
            <div class="panel-body">
                Una vez que se cierre esta etapa, el comportamiento del sistema sera el siguiente:
                <ul>
                    <li>Una vez cerrado ya no podrá abrirlo nuevamente</li>
                    <li>Ya no podrá modificar los techos financieros de manera directa</li>
                    <li>La afectación de los techos financieros será mediante la opción de transferencia de Recursos</li>
                    <li>:</li>
                    <li>Una vez cerrado, el sistema permitira la asignación de fuente de financiamientos a las obras</li>
                </ul>                                 
            </div>

            <div class="panel-footer clearfix">

                <div class="pull-right">                
                    <asp:Button ID="btnCerrar" runat="server" Text="Cerrar Carga Inicial" CssClass="btn btn-primary" OnClick="btnCerrar_Click"  OnClientClick="return fnc_Confirmar()"></asp:Button>
                    <asp:Button ID="btnCancelar" runat="server" Text="Regresar" CssClass="btn btn-primary" OnClick="btnCancelar_Click" ></asp:Button>
                </div>

            </div>
           
    </div>

</asp:Content>

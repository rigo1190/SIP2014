using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIP.Formas.TechoFin
{
    public partial class wfTechoFinancieroBitacora : System.Web.UI.Page
    {
        private UnitOfWork uow;

        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();

            if (!IsPostBack)
            {
                cargarBitacora();
            }
            
            

         

            

        }

        private void cargarBitacora()
        {
            int ejercicio = int.Parse(Session["EjercicioId"].ToString());

            List<TechoFinancieroBitacora> lista = uow.TechoFinancieroBitacoraBL.Get(p=>p.EjercicioId == ejercicio).ToList();

            int i = 0;
            foreach (TechoFinancieroBitacora bit in lista)
            {
                i++;

                System.Web.UI.HtmlControls.HtmlGenericControl divPanel = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelHeading = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelCollapse = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                System.Web.UI.HtmlControls.HtmlGenericControl divPanelBody = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");

                System.Web.UI.HtmlControls.HtmlGenericControl h4 = new System.Web.UI.HtmlControls.HtmlGenericControl("H4");
                System.Web.UI.HtmlControls.HtmlGenericControl a = new System.Web.UI.HtmlControls.HtmlGenericControl("A");

                System.Web.UI.HtmlControls.HtmlGenericControl p = new System.Web.UI.HtmlControls.HtmlGenericControl("P");


                System.Web.UI.HtmlControls.HtmlGenericControl tabla = new System.Web.UI.HtmlControls.HtmlGenericControl("TABLE");


                //heading
                divPanelHeading.Attributes.Add("class", "panel-heading");
                
                h4.Attributes.Add("class", "panel-title");

                a.Attributes.Add("data-toggle","collapse");
                a.Attributes.Add("data-parent", "#accordion");
                a.Attributes.Add("href","#collapse" + i.ToString());
                a.InnerText = bit.Movimiento + " : " + bit.Observaciones;

                h4.Controls.Add(a);
                divPanelHeading.Controls.Add(h4);


                //Collapse
                divPanelCollapse.Attributes.Add("id", "collapse" + i.ToString());
                divPanelCollapse.Attributes.Add("class", "panel-collapse collapse");
                

                divPanelBody.Attributes.Add("class", "panel-body");



                


                List<TechoFinancieroBitacoraMovimientos> detalle = uow.TechoFinancieroBitacoraMovimientosBL.Get(q => q.TechoFinancieroBitacoraId == bit.Id).ToList();
                

                tabla.Attributes.Add("class","table");
                tabla.Attributes.Add("cellspacing","0");

                System.Web.UI.HtmlControls.HtmlGenericControl trHead = new System.Web.UI.HtmlControls.HtmlGenericControl("TR");
                System.Web.UI.HtmlControls.HtmlGenericControl thOne = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thTwo = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");
                System.Web.UI.HtmlControls.HtmlGenericControl thThree = new System.Web.UI.HtmlControls.HtmlGenericControl("TH");

                trHead.Attributes.Add("align", "center");

                
                thOne.InnerText = "Unidad Presupuestal";
                thTwo.InnerText = "Decremento";
                thThree.InnerText = "Incremento";

                trHead.Controls.Add(thOne);
                trHead.Controls.Add(thTwo);
                trHead.Controls.Add(thThree);

                tabla.Controls.Add(trHead);


                foreach (TechoFinancieroBitacoraMovimientos item in detalle)
                {

                    System.Web.UI.HtmlControls.HtmlGenericControl tr = new System.Web.UI.HtmlControls.HtmlGenericControl("TR");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdOne = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdTwo = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");
                    System.Web.UI.HtmlControls.HtmlGenericControl tdThree = new System.Web.UI.HtmlControls.HtmlGenericControl("TD");

                    tdOne.Attributes.Add("align", "left");
                    tdOne.InnerText = item.UnidadPresupuestal.Nombre;

                    tdTwo.Attributes.Add("align","right");
                    tdTwo.InnerText = item.Decremento.ToString("C");

                    tdThree.Attributes.Add("align", "right");
                    tdThree.InnerText = item.Incremento.ToString("C"); 


                    tr.Controls.Add(tdOne);
                    tr.Controls.Add(tdTwo);
                    tr.Controls.Add(tdThree);



                    tabla.Controls.Add(tr);
                }





                divPanelBody.Controls.Add(tabla);
                divPanelCollapse.Controls.Add(divPanelBody);


                //Agregar Elemento
                divPanel.Attributes.Add("class", "panel panel-default");
                divPanel.Controls.Add(divPanelHeading);
                divPanel.Controls.Add(divPanelCollapse);

                this.accordion.Controls.Add(divPanel);

            }


        }


    }
}
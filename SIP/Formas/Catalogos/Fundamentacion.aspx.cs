using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP.Formas.Catalogos
{
    public partial class Fundamentacion : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork(Session["IdUser"].ToString());

            if (!IsPostBack)
            {
                //BindArbolPreguntas();
            }

            //Evento que se ejecuta en JAVASCRIPT para evitar que se 'RESCROLLEE' el arbol al seleccionar un NODO y no se pierda el nodo seleccionado
            ClientScript.RegisterStartupScript(this.GetType(), "script", "SetSelectedTreeNodeVisible('<%= treePreguntas.ClientID %>_SelectedNode')", true);
        }


        //private void BindArbolPreguntas()
        //{
        //    treePreguntas.Nodes.Clear();
        //    Plantilla p = uow.PlantillaBusinessLogic.GetByID(4);

        //    foreach (PlantillaDetalle pd in p.DetallePreguntas)
        //    {
        //        TreeNode nodeNew = new TreeNode();
        //        nodeNew.Text = pd.Pregunta;
        //        nodeNew.Value = pd.Id.ToString();
        //        //nodeNew.NavigateUrl = "javascript:fnc_Test(" + pd.Id + ")";
        //        treePreguntas.Nodes.Add(nodeNew);
        //    }

        //}


        private void BindGridFundamentacion(int idPregunta)
        {
            gridFundamentacion.DataSource = uow.FundamentacionPlantillaBL.Get(e => e.PlantillaDetalleId == idPregunta);
            gridFundamentacion.DataBind();
        }

        protected void treePreguntas_SelectedNodeChanged(object sender, EventArgs e)
        {
            //BindGridFundamentacion(Utilerias.StrToInt(treePreguntas.SelectedNode.Value));
        }



    }
}
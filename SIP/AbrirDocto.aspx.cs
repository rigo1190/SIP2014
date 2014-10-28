using BusinessLogicLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIP
{
    public partial class AbrirDocto : System.Web.UI.Page
    {
        private UnitOfWork uow;
        protected void Page_Load(object sender, EventArgs e)
        {
            uow = new UnitOfWork();
            int id = Utilerias.StrToInt(Request.Params["i"].ToString());
            POAPlantillaDetalle obj = uow.POAPlantillaDetalleBusinessLogic.GetByID(id);

            if (obj.NombreArchivo == null)
            {
                lblMsgError.Text = "No existe ningun archivo adjunto";
                divMsgError.Style.Add("display", "block");
            }else if (obj.NombreArchivo.Equals(string.Empty))
            {
                lblMsgError.Text = "No existe ningun archivo adjunto";
                divMsgError.Style.Add("display", "block");
            }
            else
                Response.Redirect("~/ArchivosAdjuntos/" + obj.Id + "/" + obj.NombreArchivo);

        }
    }
}
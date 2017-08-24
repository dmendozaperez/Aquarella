using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Configuration;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class ConsultaVentaTk : System.Web.UI.Page
    {
        Users _user;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }
        protected void sbconsultar()
        {
            string cultureName = "es-ES";
            CultureInfo culture = new CultureInfo(cultureName);

            String vnumero = txtbuscar.Text;
            String VFormatoTK = invoice.get_formatoTickets(vnumero);

            if (VFormatoTK == "0")
            {
                string vmensaje = "El Numero de tickets : " + vnumero + " no se encuenta registrado en el sistema";
                ScriptManager.RegisterStartupScript(Page, GetType(), "mensaje", "alert('" + vmensaje + "');", true);
                return;
            }
            VFormatoTK = VFormatoTK.Replace("\r\n", "\r\n");
            VFormatoTK = VFormatoTK.Replace("|", " ");
            VFormatoTK = VFormatoTK.Replace("&nbsp", " ");

            System.Text.StringBuilder str = new System.Text.StringBuilder();
            str.Append(VFormatoTK);
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/plain";
            Response.AddHeader("Content-Disposition", "attachment;filename=Tk" + vnumero + " .txt");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.Default;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            Response.Write(str.ToString());
            Response.End();
        }
    }
}
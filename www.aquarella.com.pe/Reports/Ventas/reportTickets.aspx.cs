using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;

namespace www.aquarella.com.pe.Reports.Ventas
{
    public partial class reportTickets : System.Web.UI.Page
    {
        Users _user;
        string _noventa;
        string _nombreSession = "ArticlesReturnedValues";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            ///
            if (!IsPostBack)
            {
                Session[_nombreSession] = null;
            }

            //
            if (Request.Params["noventa"] != null)
                _noventa = Request.Params["noventa"].ToString();

            Reimp_tickets(_noventa);


        }
        private void Reimp_tickets(string _numero)
        {
            try
            {
                string cultureName = "es-ES";
                CultureInfo culture = new CultureInfo(cultureName);

                String vnumero = _numero;
                String VFormatoTK = invoice.get_formatoTickets(vnumero, 3);

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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
using System;
using www.aquarella.com.pe.bll.Util;


namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class panelLiqReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);            
        }
    }
}
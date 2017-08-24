using System;

namespace www.aquarella.com.pe.UserControl
{
    public partial class ucShippingForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dwDptos.DataSourceID = odsDeptos.ID;
                dwDptos.DataBind();
            }
        }
    }
}
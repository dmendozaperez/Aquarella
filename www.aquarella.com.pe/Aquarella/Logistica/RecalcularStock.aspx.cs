using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class RecalcularStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
      

        protected void ibrecalcular_Click(object sender, ImageClickEventArgs e)
        {
            msnMessage.Visible = false;
            string _mensaje = Stock.recalcular_stock();
            if (_mensaje.Length == 0)
            {
                msnMessage.LoadMessage("Se Recalculo el stock correctamente...  ", UserControl.ucMessage.MessageType.Information);
            }
            else
                msnMessage.LoadMessage(_mensaje, UserControl.ucMessage.MessageType.Error);
        }
    }
}
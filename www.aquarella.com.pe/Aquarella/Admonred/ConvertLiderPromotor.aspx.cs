using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class ConvertLiderPromotor : System.Web.UI.Page
    {
        Users _user;
       // string _nameSessionData = "_ReturnData";
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
               cargarlider();
               //btnaceptar.Attributes.Add("onclick", "javascript:return " +
               //"confirm('¿Esta seguro de APLICAR el Lider/Promotor A. " +
               // "- ?')");
              // btnaceptar.Attributes["Onclick"] = "return confirm('Do you really want to save?')";  
                //if (_user._usv_employee == true && _user._usv_area == "%%")
                //    formForEmployee();
                //else if (_user._usv_employee == true && _user._usv_area != "%%")
                //    formForCustomer();
            }
        }
        private void cargarlider()
        {
            ds = new DataSet();
            ds = Lider.Lider.getlider();
            dwlider.Items.Clear();
            ListItem valor=new ListItem();
            valor.Text="-- Seleccionar un Lider --";
            valor.Value = "-1";
            dwlider.Items.Add(valor);
            dwlider.DataSource = ds;
            dwlider.DataTextField = "Are_Descripcion";
            dwlider.DataValueField = "Are_Id";
            dwlider.DataBind();
        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {

            //if (Page.IsValid)
            //{
            //    msnMessage.LoadMessage("Se convirtio Lider/Promotor...", UserControl.ucMessage.MessageType.Information);
            //}
            try
            {
                msnMessage.Visible = false;
                string vid = dwlider.SelectedValue;
                Lider.Lider.sbliderpromotor(vid, false);
                cargarlider();
                msnMessage.LoadMessage("Se convirtio Lider/Promotor...", UserControl.ucMessage.MessageType.Information);
                dwlider.SelectedValue = "-1";
            }
            catch (Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
    }
}
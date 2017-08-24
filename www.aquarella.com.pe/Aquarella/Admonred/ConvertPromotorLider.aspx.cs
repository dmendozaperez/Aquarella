using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;

using System.Data;
namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class ConvertPromotorLider : System.Web.UI.Page
    {
        Users _user;
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                cargarpromotor();
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

        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            try
            {
                msnMessage.Visible = false;
                string vid = dwpromotor.SelectedValue;
                Lider.Lider.sbpromotorlider(vid,true);
                cargarpromotor();
                msnMessage.LoadMessage("Se convirtio Promotor/Lider...", UserControl.ucMessage.MessageType.Information);
                dwpromotor.SelectedValue = "-1";
            }
            catch (Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }

        }

        private void cargarpromotor()
        {
            ds = new DataSet();
            ds = Lider.Lider.getpromotor();
            dwpromotor.Items.Clear();
            ListItem valor = new ListItem();
            valor.Text = "-- Seleccionar un Promotor --";
            valor.Value = "-1";
            dwpromotor.Items.Add(valor);
            dwpromotor.DataSource = ds;
            dwpromotor.DataTextField = "Nombre";
            dwpromotor.DataValueField = "Bas_Id";
            dwpromotor.DataBind();
        }
    }
}
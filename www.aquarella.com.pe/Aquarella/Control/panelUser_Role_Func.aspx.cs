using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;

namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class panelUser_Role_Func : System.Web.UI.Page
    {
        private string DSUsers = "DataSetUsers";
        
        DataSet data;

        private Users _user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session.Remove(DSUsers);
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();

            data = Users.GetUsersByName(txtNombre.Text.Trim());
            if (data.Tables[0].Rows.Count > 0)
            {
                GridUsers.DataSource = data;

                Session[DSUsers] = data;
                GridUsers.DataBind();
            }
            else
            {
                msnMessage.LoadMessage("Nombe no encontrado", UserControl.ucMessage.MessageType.Information);
                GridUsers.DataSource = null;
                GridUsers.DataBind();
            }
        }

        protected void GridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridUsers.PageIndex = e.NewPageIndex;
            GridUsers.DataSource = (DataSet)Session[DSUsers];
            GridUsers.DataBind();
        }

        
    }
}
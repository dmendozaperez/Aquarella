using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;

namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class panelUser_Role : System.Web.UI.Page
    {       
        protected decimal _URN_USERID;
        protected decimal _URN_ROLEID;
        private bool respuesta;

        private Users _user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
                       
            lblInfo.Text = Request.QueryString["NAME"];
            _URN_USERID = Convert.ToDecimal(Request.QueryString["USN_USERID"]);

            if (!IsPostBack)
            {
                fillDropDown();
                fillGrid();
            }
        }

        protected void fillDropDown() { 
            DataSet data = Roles.getRoles();
            data.Tables[0].DefaultView.Sort = "rol_nombre";
            DDListRoles.DataSource = data.Tables[0];
            DDListRoles.DataValueField = "rol_id";
            DDListRoles.DataTextField = "rol_nombre";
            DDListRoles.DataBind();
        }

        protected void fillGrid() {

           
                GridRolesUsers.DataSource = Roles.GetRolesByUser(_URN_USERID);
                GridRolesUsers.DataBind();
           
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            _URN_ROLEID = Convert.ToDecimal(DDListRoles.SelectedValue);

            respuesta = Roles.insertUserRole(_URN_ROLEID, _URN_USERID);
            if (respuesta)
            {
                lblInfo.Text = "Adicion correcta";
                fillGrid();
            }
            else
                lblInfo.Text = "Ocurrio un problema";
        }

        protected void GridRolesUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                _URN_ROLEID = Convert.ToDecimal(e.CommandArgument);

                respuesta = Roles.deleteUserRole(_URN_ROLEID, _URN_USERID);

                if (respuesta)
                    fillGrid();
            }
        }

        protected void GridRolesUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridRolesUsers.PageIndex = e.NewPageIndex;
            fillGrid();
        }
    }
}
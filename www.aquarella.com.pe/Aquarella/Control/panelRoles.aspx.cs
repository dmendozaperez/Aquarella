using System;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;


namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class panelRoles : System.Web.UI.Page
    {
        public bool estado = false;
        string DSdata = "DataSetRoles";

        protected Users _user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session.Remove(DSdata);
                fillGridRoles();    
            }
        }

        protected void BtnSaveRol_Click(object sender, EventArgs e)
        {
            
            string _ROV_NAME = txbNombreRol.Text.Trim();
            string _ROV_DESCRIPTION = txbDescRol.Text.ToUpper();

            estado =  Roles.insertRole(_ROV_NAME, _ROV_DESCRIPTION);

            if (estado)
            {
                lblMsg.Text = "Rol Insertado satisfactoriamente";
                fillGridRoles();
                eraseFields();
            }
            else
                lblMsg.Text = "Error al crear Rol";
        }

        public void eraseFields(){
            txbDescRol.Text = "";
            txbNombreRol.Text = "";
        }

        public void fillGridRoles() {
            DataSet data = Roles.getRoles();
            data.Tables[0].DefaultView.Sort = "rol_nombre";
            GridRoles.DataSource = data.Tables[0];
            GridRoles.DataBind();
            Session[DSdata] = data;
        }

        protected void GridRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet data = (DataSet)Session[DSdata];
            data.Tables[0].DefaultView.Sort = "rol_nombre";
            GridRoles.PageIndex = e.NewPageIndex;
            GridRoles.DataSource = data.Tables[0];
            GridRoles.DataBind();
        }
        
        [WebMethod()]
        public static string ajaxUpdateRole(int RON_ID, string ROV_NAME, string ROV_DESCRIPTION)
        {   
            try
            {
                bool respuesta = Roles.updateRole(RON_ID, ROV_NAME, ROV_DESCRIPTION);

                if (respuesta)
                    return "1";
                else
                    return "-1";
            }
            catch (Exception)
            {
                return "-1";
            }
        }
    }
}
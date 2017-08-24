using System;
using System.Data;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;


namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class panelRole_Func : System.Web.UI.Page
    {
        protected Users _user;
        
        public string _ROV_CO { get; set; }
        public decimal _RON_ID { get; set; }
        public decimal _FUN_ID { get; set; }

        public bool respuesta;

        protected void Page_Load(object sender, EventArgs e)
        {           
            lblInfo.Text = Request.QueryString["ROV_NAME"];
            _RON_ID = Convert.ToDecimal(Request.QueryString["RON_ID"]);

            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                llenarDrop();
                llenarGrilla();
            }
        }

        public void llenarGrilla() {            
                GridFunctions.DataSource = Functions.GetFunctionsByRol(_RON_ID);
                GridFunctions.DataBind();            
        }

        public void llenarDrop() {
            DataTable datos = Functions.GetAllFunctions();
            datos.DefaultView.Sort = "fun_nombre";
            DDListFunctions.DataSource = datos;
            DDListFunctions.DataTextField = "fun_nombre";
            DDListFunctions.DataValueField = "fun_id";
            DDListFunctions.DataBind();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            _FUN_ID = Convert.ToDecimal(DDListFunctions.SelectedValue);

            respuesta = Functions.InsertRoleFunction(_FUN_ID, _RON_ID);

            if (respuesta)
            {
                lblInfo.Text = "Adicion correcta";
                llenarGrilla();
            }
            else
            {
                lblInfo.Text = "Ocurrio un problema";
            }
        }

        protected void GridFunctions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                _FUN_ID = Convert.ToDecimal(e.CommandArgument);

                respuesta = Functions.RemoveRoleFunction(_FUN_ID, _RON_ID);

                if (respuesta)
                    llenarGrilla();
            }
        }

        protected void GridFunctions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridFunctions.PageIndex = e.NewPageIndex;
            llenarGrilla();
        }

        
    }
}
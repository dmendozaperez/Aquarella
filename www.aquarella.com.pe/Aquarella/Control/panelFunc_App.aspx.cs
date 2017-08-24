using System;
using System.Data;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;

namespace www.bata.aquarella.com.pe.Aquarella.Control
{
    public partial class panelFunc_App : System.Web.UI.Page
    {
        private Users _user;

        public string _FUV_CO { get; set; }
        public decimal _FUN_ID { get; set; }
        public decimal _APN_ID { get; set; }

        public bool respuesta;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
            
            _FUN_ID = Convert.ToDecimal(Request.QueryString["FUN_ID"]);
            lblInfo.Text = Request.QueryString["FUN_NOMBRE"];

            if (!IsPostBack)
            {
                llenarDropDawn();
                llenarGrilla();
            }
        }

        public void llenarGrilla() {
            
                GridAplications.DataSource = ApplicationClass.ApplicationByFunc(_FUN_ID);
                GridAplications.DataBind();
            
        }

        public void llenarDropDawn() {
            DataTable datos = ApplicationClass.GetAllAplications();
            datos.DefaultView.Sort = "apl_nombre";
            DDAplications.DataSource = datos;
            DDAplications.DataTextField = "apl_nombre";
            DDAplications.DataValueField = "apl_id";
            DDAplications.DataBind();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            _APN_ID = Convert.ToDecimal(DDAplications.SelectedValue);

            respuesta = ApplicationClass.insertAppFunction(_APN_ID, _FUN_ID);

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

        protected void GridAplications_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                _APN_ID = Convert.ToDecimal(e.CommandArgument);

                respuesta = ApplicationClass.deleteAppFunction(_APN_ID, _FUN_ID);

                if (respuesta)
                    llenarGrilla();
            }
        }
    }
}
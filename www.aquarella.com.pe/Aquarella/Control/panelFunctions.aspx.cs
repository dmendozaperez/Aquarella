using System;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class panelFunctions : System.Web.UI.Page
    {
        private Users _user;

        private bool respuesta;

        string DSFunctions = "DataSetFunctions";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session.Remove(DSFunctions);
                fillDropDawn();
                fill();    
            }
        }

        protected void fill() {
            DataSet data = Functions.GetAllFunctionsDS();
            data.Tables[0].DefaultView.Sort = "fun_nombre";
            gridFunctions.DataSource = data.Tables[0];
            gridFunctions.DataBind();
            Session[DSFunctions] = data;
        }

        protected void fillDropDawn(){
                
                DataTable datos = Functions.GetAllFunctions();
                datos.DefaultView.Sort = "fun_nombre";
                DDPadre.DataSource = datos;
                DDPadre.DataTextField = "fun_nombre";
                DDPadre.DataValueField = "fun_id";
                DDPadre.DataBind();
                DDPadre.Items.Insert(0, new ListItem("(vacio)",""));

                datos.DefaultView.Sort = "fun_nombre";
                DDPadre2.DataSource = datos;
                DDPadre2.DataTextField = "fun_nombre";
                DDPadre2.DataValueField = "fun_id";
                DDPadre2.DataBind();
                DDPadre2.Items.Insert(0, new ListItem("(vacio)",""));
        }

        protected void gridFunctions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridFunctions.PageIndex = e.NewPageIndex;
            DataSet data = (DataSet)Session[DSFunctions];
            data.Tables[0].DefaultView.Sort = "FUN_NOMBRE";
            gridFunctions.DataSource = data.Tables[0];
            gridFunctions.DataBind();

        }

        protected void btnSaveFuction_Click(object sender, EventArgs e)
        {
            Functions fnc = new Functions();
            msnMessage.HideMessage();

            try
            {                
                fnc._FUN_ID = 0;
                fnc._FUV_NAME = txtNombre.Text.Trim();
                if (DDPadre.SelectedValue != "")
                    fnc._FUN_FATHER = Convert.ToDecimal(DDPadre.SelectedValue);
                if (txtOrden.Text != String.Empty)
                    fnc._FUN_ORDER = Convert.ToDecimal(txtOrden.Text);
                fnc._FUV_DESCRIPTION = txtDesc.Text.Trim();
                fnc._FUN_SYSTEM = Convert.ToDecimal(DDFUN_System.SelectedValue);
                if (fnc._FUN_FATHER == null) fnc._FUN_FATHER = 0;
                respuesta = fnc.InsertFunction();

                if (respuesta){
                    msnMessage.LoadMessage("Funcion insertAQUARELLA satisfactoriamente", UserControl.ucMessage.MessageType.Information);
                    eraseFields();
                    fill();
                }
                else
                    msnMessage.LoadMessage("Ocurrio un problema durante la insercion", UserControl.ucMessage.MessageType.Error);

            }
            catch (Exception){ msnMessage.LoadMessage("Ocurrio un problema durante la insercion", UserControl.ucMessage.MessageType.Error); }
        }

        public void eraseFields() {
            txtDesc.Text = "";
            txtNombre.Text = "";
            txtOrden.Text = "";
            DDPadre.SelectedIndex = 0;
        }

        [WebMethod()]
        public static string ajaxUpdateFunction(decimal FUN_ID, string FUV_NAME, string FUV_DESCRIPTION, string _FUN_ORDER, string _FUN_FATHER)
        {
            decimal? FUN_ORDER;
            decimal? FUN_FATHER;

            // Convierte la seleccion del orden en nulo si no hay seleccion
            if (_FUN_ORDER == "")
                FUN_ORDER = null;
            else
                FUN_ORDER = Convert.ToDecimal(_FUN_ORDER);
            // Convierte la seleccion del padre en nulo si no hay seleccion
            if (_FUN_FATHER == "")
                FUN_FATHER = null;
            else
                FUN_FATHER = Convert.ToDecimal(_FUN_FATHER);


            if (FUN_FATHER == null)  FUN_FATHER = 0;
            bool respuesta = Functions.updateFunction(FUN_ID, FUV_NAME, FUV_DESCRIPTION, FUN_ORDER, FUN_FATHER);

            if (respuesta)
                return "1";
            else
                return "-1";
        }
    }
}
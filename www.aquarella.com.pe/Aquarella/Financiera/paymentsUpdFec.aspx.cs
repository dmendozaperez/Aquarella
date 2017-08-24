using System;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.BLL.Control;

namespace  www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class paymentsUpdFec : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "Info_Monto";

        #region < Eventos del Load >

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                fillDropDawn();
                Session[_nameSessionData] = null;
                txtDateStart.Text= DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");
                setParamsDataSource(txtDateStart.Text, txtDateEnd.Text);
                refreshGrid();
            }
        }


        protected void fillDropDawn()
        {

            DataTable datos = Functions.Get_PAYMENT_STATUS();

            datos.DefaultView.Sort = "Est_Descripcion";
            DDPadre2.DataSource = datos;
            DDPadre2.DataTextField = "Est_Descripcion";
            DDPadre2.DataValueField = "Est_Id";
            DDPadre2.DataBind();
            //DDPadre2.Items.Insert(0, new ListItem("(vacio)", ""));
        }

        #endregion

      
        /// <summary>
        /// Refrescar datos
        /// </summary>
     
        #region < Eventos sobre data source >

        protected void setParamsDataSource(string date_start, string date_end)
        {    
            odsPays.SelectParameters[0].DefaultValue = date_start;
            odsPays.SelectParameters[1].DefaultValue = date_end;
        }
     
        protected void btConsult_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            setParamsDataSource(txtDateStart.Text, txtDateEnd.Text);
            refreshGrid();
            
        }


        protected void refreshGrid()
        {
            gvPays.DataSourceID = odsPays.ID;
            gvPays.DataBind();
        }

        protected void odsPays_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessionData] = dt;
            }
            catch
            { }
        }

        protected void btRefresh_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            refreshGrid();
        }


        [WebMethod()]
        public static string ajaxUpdateFunction(string FUN_ID, string FUV_NAME, string FUV_DESCRIPTION, string _FUN_ORDER, string _FUN_FATHER)
        {
            decimal? FUN_ORDER;

            // Convierte la seleccion del orden en nulo si no hay seleccion
            if (_FUN_ORDER == "")
                FUN_ORDER = null;
            else
                FUN_ORDER = Convert.ToDecimal(_FUN_ORDER);

            //bool respuesta = Functions.updateFunction_UPD(FUV_CO, FUN_ID, FUV_NAME, FUV_DESCRIPTION, FUN_ORDER, _FUN_FATHER);
            //if (respuesta)
            //    return "1";
            //else
            //    return "-1";

            string respuesta = Functions.updateFunction_UPD(FUN_ID, FUV_NAME, FUV_DESCRIPTION, FUN_ORDER, _FUN_FATHER);

            if (respuesta == "bien")
                return "1";
            else
                return respuesta;

        }

        #endregion

    }
}
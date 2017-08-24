using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.Bll.Admonred;

namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class ConvertAsesorLider : System.Web.UI.Page
    {
        Users _user;
        // string _nameSessionData = "_ReturnData";
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
            if (!IsPostBack)
            {
                cargarusuario();               
            }
        }
        private void cargarusuario()
        {
            dt = new DataTable();
            dt = Asesor.getlider_usuarios();
            dwusuario.Items.Clear();
            ListItem valor = new ListItem();
            valor.Text = "-- Seleccionar un Usuario --";
            valor.Value = "-1";
            dwusuario.Items.Add(valor);
            dwusuario.DataSource = dt;
            dwusuario.DataTextField = "Nombres";
            dwusuario.DataValueField = "Bas_Id";
            dwusuario.DataBind();
        }
        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            try
            { 
                msnMessage.Visible = false;
                string vid = dwusuario.SelectedValue;
                Asesor.convert_lider_asesor(vid);
                cargarusuario();
                msnMessage.LoadMessage("Se convirtio Lider/Promotor...", UserControl.ucMessage.MessageType.Information);
                dwusuario.SelectedValue = "-1";
            }
            catch (Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
    }
}
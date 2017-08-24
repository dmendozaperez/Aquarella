using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;


namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class changePassword : System.Web.UI.Page
    {
        Users _user;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion 
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                txtusername.Text = _user._usu_nombre;
                if(Request.QueryString["expiration"]!=null)
                    if (Request.QueryString["expiration"].Equals("1"))
                    { 
                     TreeView treeMenu = new TreeView();
                     treeMenu = (TreeView)this.Master.FindControl("MenuPrin");
                     treeMenu.Visible = false;
                     lblExpiration.Text = "Su contraseña ha caducado. <br /><br />";
                     lblExpiration.ForeColor = System.Drawing.Color.Red;  
                    }
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (Cryptographic.decrypt(_user._usu_contraseña).Equals(txtPassAnterior.Text.Trim())
                         &
                        (!string.IsNullOrEmpty(txtPassNew.Text.Trim()))

                        )
                    {
                        _user._usv_status = "A";
                        _user._usv_password = Cryptographic.encrypt(txtPassNew.Text.Trim());
                        _user.Update();

                        //History_Password.addPassword(_user._usv_co, _user._usn_userid, _user._usv_password, DateTime.Now);
                        msnMessage.LoadMessage("Su contraseña se ha cambiado satisfactoriamente.", UserControl.ucMessage.MessageType.Information);
                        //Guardar en el historial de passwords
                        /*HISTORY_PASSWORD hp = new HISTORY_PASSWORD();
                        hp.HPN_USERID = _user.USN_USERID;
                        hp.HPV_CO = _user.USV_CO;
                        hp.HPV_PASSWORD = _user.USV_PASSWORD;
                        hp.HPD_DATE = DateTime.Now;
                        hp.IsNew = true;
                        hp.Save(_id, _machine);
                        lblMsg.Text = "Su contraseña se ha cambiado satisfactoriamente.";*/
                    }
                    else
                    {
                        throw new InvalidCastException();
                    }
                }
            }
            catch (InvalidCastException)
            {
                msnMessage.LoadMessage("Hubo un error. No se realizaron los cambios por que la contraseña anterior no es valida o la nueva contraseña ya la habia utilizado.", UserControl.ucMessage.MessageType.Error);
                ///lblMsg.Text = "Hubo un error. No se realizaron los cambios por que la contraseña anterior no es valida o la nueva contraseña ya la habia utilizado.";
            }
        }
    }
}
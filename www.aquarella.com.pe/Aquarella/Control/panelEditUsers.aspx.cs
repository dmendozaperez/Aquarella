using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;


namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class panelEditUsers : System.Web.UI.Page
    {
        Users _user;
        Users _user2;
        DataSet data;
        string consulta = "DataSetConsulta";
        string usr2 = "User2";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null)
                Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
             
            if (!IsPostBack)
            {
                Session.Remove(consulta);
            }

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();

            data = Users.GetUsersByName(txtNombre.Text.Trim());
            if (data.Tables[0].Rows.Count > 0)
            {
                GridUsers.DataSource = data;

                Session[consulta] = data;
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
            GridUsers.DataSource = (DataSet)Session[consulta];
            GridUsers.DataBind();
        }

        [WebMethod()]
        public static string ajaxUpdateFunction(decimal USN_USERID, string USV_USERNAME, string USV_PASSWORD,string USD_CREATION, string USV_STATUS){

            Users _usr2 = new Users();
            string ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
            _usr2._logTx = new Log_Transaction(ip);
            
            _usr2._usn_userid = USN_USERID;
            _usr2._usv_username = USV_USERNAME;
            _usr2._usv_password = Cryptographic.encrypt(USV_PASSWORD);            
            _usr2._usd_creation = Convert.ToDateTime(USD_CREATION);
            _usr2._usv_status = USV_STATUS;
            
            

            try
            {
                if (_usr2.Update())
                {
                    History_Password.addPassword(USN_USERID, Cryptographic.encrypt(USV_PASSWORD), DateTime.Now);
                    return "1";
                }
                else { return "-1"; }
                
            }
            catch (Exception)
            {
                return "-1";
            }
        }

        protected void btnConsultarCedula_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            initFields();

            if (txtCedula.Text != String.Empty)
            {
                string txtCedulaString = txtCedula.Text.Trim();
                txtCedulaString = txtCedulaString.Replace(".","");

                DataSet usDS = Users.GetUserByDocument(txtCedulaString);

                if (usDS.Tables[0].Rows.Count > 0)
                {
                    lblCedula.Text = usDS.Tables[0].Rows[0]["bas_documento"].ToString();
                    lblNombre.Text = usDS.Tables[0].Rows[0]["nombre"].ToString();
                    decimal _user2id = Convert.ToDecimal(usDS.Tables[0].Rows[0]["usu_id"]);

                    DataSet usControl = Users.GetUserControlById(_user2id);

                    if (usControl.Tables[0].Rows.Count > 0)
                    {
                        DataRow info = usControl.Tables[0].Rows[0];

                        pnlInforUser.Visible = true;

                        _user2 = new Users();

                       _user2._usn_userid = Convert.ToDecimal(info["usu_id"]);

                        _user2._usd_creation = Convert.ToDateTime(info["usu_fecha_cre"].ToString());                        

                        Session[usr2] = _user2; // se almacena el usuario2 en session para no perder la informacion despues del postback

                        txtUSVUserName.Text = info["usu_nombre"].ToString();                        
                        DDUSVStatus.SelectedValue = info["usu_est_id"].ToString();
                    }
                }
                else
                    msnMessage.LoadMessage("Numero de cedula no se encuentra", UserControl.ucMessage.MessageType.Information);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _user2 = (Users)Session[usr2];
            _user2._logTx = new Log_Transaction(Request.ServerVariables["remote_addr"]);
            _user2._usv_username = txtUSVUserName.Text.Trim();            
            _user2._usv_status = DDUSVStatus.SelectedValue;
            _user2._usv_password = Cryptographic.encrypt(txtUSVPassword.Text.Trim());

            try
            {
                if (_user2.Update())
                {
                    initFields(); // Inicializan campos de cedula y nombre
                    msnMessage.LoadMessage("Usuario Actualizado",UserControl.ucMessage.MessageType.Information);
                    History_Password.addPassword(_user2._usn_userid, _user2._usv_password, DateTime.Now);
                }
                else
                    msnMessage.LoadMessage("Ocurrio un problema durante la actualizacion", UserControl.ucMessage.MessageType.Error);
            }
            catch (Exception){msnMessage.LoadMessage("Ocurrio un problema durante la actualizacion", UserControl.ucMessage.MessageType.Error);}
        }

        protected void initFields()
        {
            lblCedula.Text = "";
            lblNombre.Text = "";
            pnlInforUser.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlInforUser.Visible = false;
            initFields();
            txtCedula.Text = "";
        }

        protected void btnConstulatUSR_Click(object sender, EventArgs e)
        {
            //msnMessage.HideMessage();

            ////Primero se realiza la verificacion de la cedula 
            ////que no se encuentre registrado como usuario. 
            //if (txtCedulaUSR.Text != String.Empty)
            //{
            //    string txtCedulaString = txtCedulaUSR.Text.Trim();
            //    txtCedulaString = txtCedulaString.Replace(".","");

            //    DataSet usDS = Users.GetUserByDocument(txtCedulaString);

            //    if (usDS.Tables[0].Rows.Count > 0)
            //        msnMessage.LoadMessage("El Usuario ya se encuntra registrado en el sistema", UserControl.ucMessage.MessageType.Information);
            //    else
            //    {
            //        DataSet userBasicData = Users.GetUserBasicDataByDocument(_user._usv_co, txtCedulaString); // Si no lo encuentra puede continuar en la busqueda de la informacion del usuario. 

            //        if (userBasicData.Tables[0].Rows.Count > 0)
            //        {           
            //            bool estado = false;

            //            string cordinador = userBasicData.Tables[0].Rows[0]["cordinador"].ToString();
            //            string empleado = userBasicData.Tables[0].Rows[0]["empleado"].ToString();

            //            // Si el Usuario consultado es cordinador o empleado se puede realizar la creacion de usuario
            //            if (cordinador == "cordinador") estado = true;
            //            if (empleado == "empleado") estado = true;

            //            if (estado){
            //                GridUsersBasicData.DataSource = userBasicData;
            //                GridUsersBasicData.DataBind();
            //            }
            //            else
            //                msnMessage.LoadMessage("El Usuario no se puede adicionar por que no es Cordinador o Empleado", UserControl.ucMessage.MessageType.Information);
            //        }
            //        else
            //            msnMessage.LoadMessage("No se encuentra el usuario", UserControl.ucMessage.MessageType.Information);

            //    }
            //}
        }

        [WebMethod()]
        public static string ajaxInsertUserToControl(string BD_CO, decimal BDN_ID, string BDV_EMAIL, string BDV_DOCUMENT_NO)
        {
            BDV_DOCUMENT_NO = Cryptographic.encrypt(BDV_DOCUMENT_NO);

            if (Users.insertUserToControl(BD_CO, BDN_ID, BDV_EMAIL, BDV_DOCUMENT_NO))
                return "1";
            else
                return "-1";
        }
    }
}
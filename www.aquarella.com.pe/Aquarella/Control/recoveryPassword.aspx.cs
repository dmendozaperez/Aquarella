using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.Services;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Control;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
using System.Linq;

namespace www.aquarella.com.pe.Aquarella.Control
{
    public partial class recoveryPassword : System.Web.UI.Page
    {
        public static string _nSlogTx = "_nSlogTx";

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginUser.Focus();
        }

        /// <summary>
        /// Autenticacion de usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
       
        private  string GetUserIPAddress()
        {
            //var context = System.Web.HttpContext.Current;
            string ip = String.Empty;
            try
            {

                ip = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                //    ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                //else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                //    ip = context.Request.UserHostAddress;

                //if (ip == "::1")
                //    ip = "127.0.0.1";
            }
            catch
            {

            }
            return ip;
        }
        private  void GetIpAddress(ref string _host,ref string _ip)
        {
            try
            {

                //The X-Forwarded-For (XFF) HTTP header field is a de facto standard for identifying the originating IP address of a 
                //client connecting to a web server through an HTTP proxy or load balancer
                _ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(_ip))
                {
                    _ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                //_host = System.Net.Dns.GetHostName();
                //_ip = System.Net.Dns.GetHostAddresses(_host).GetValue(1).ToString();
            }
            catch
            {

            }
        }       
        protected void LoginUser_Authenticate(object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
        {
            try
            {

               
                    //obtine el nombre del usuario que desea autenticarse
                    string name = LoginUser.UserName;
                    //Obtine el password
                    string password = LoginUser.Password;
                    //obtiene si el usuario desea o no almacenar una cookie
                    bool checkcookie = LoginUser.RememberMeSet;
                    Users user = new Users();
                    user = loadUser(name);

                    if (user != null)
                    {
                        if (user._usu_est_id.Equals(Constants.IdStatusActive))
                        {
                            //Desencripta la contraseña del usuario
                            string passUser = Cryptographic.decrypt(user._usu_contraseña);
                            //valida la contraseña contraseña que ingreso contra lad del usuario
                            if (password.Equals(passUser))
                            {                        
                                //userSign(ref user, name, password, Request.UserHostAddress);

                                Session[Constants.NameSessionUser] = user;
                                loadMenu(user._bas_id);


                            //String ip = GetUserIPAddress(); // o se puede utiliar lo siguiente que es lo mismo                                                         
                            string _host = ""; string _ip = "";
                            GetIpAddress(ref _host,ref _ip);

                            //insertar log de acceso al sistema
                            Log_Transaction._auditoria_acceso(user._bas_id, user._nombre, _ip, _host);

                                //Autenticación
                                try
                                {
                                    FormsAuthentication.RedirectFromLoginPage(user._bas_id.ToString(), checkcookie);
                                }
                                catch (Exception ex) { LoginUser.FailureText = "Error de conexión: " + ex.Message.ToString(); }
                            }
                            else
                            {
                                InvalidCredentialsLog.insertInvalidCredentialsLog(user._usv_co, name, password, "F", "F", Request.UserHostAddress);
                                System.Diagnostics.Trace.WriteLine("[ValidateUser] Usuario y/o contraseña invalidos.");
                            } 
                        }
                        else if (user._usv_status.Equals(Constants.IdStatusPasswordExpiration))
                        {
                             //Desencripta la contraseña del usuario
                            string passUser = Cryptographic.decrypt(user._usv_password);
                            //valida la contraseña contraseña que ingreso contra lad del usuario
                            if (password.Equals(passUser))
                            {
                                userSign(ref user, name, password, Request.UserHostAddress);
                        
                                loadMenu(user._bas_id);
                                FormsAuthentication.SetAuthCookie(user._usn_userid.ToString(), checkcookie);
                                Server.Transfer("changePassword.aspx?expiration=1");
                            }
                            else
                            {
                                InvalidCredentialsLog.insertInvalidCredentialsLog(user._usv_co, name, password, "F", "F", Request.UserHostAddress);
                                System.Diagnostics.Trace.WriteLine("[ValidateUser] Usuario y/o contraseña invalidos.");
                            }
                        }
                        else
                        {                    
                            LoginUser.FailureText = "Error de conexión: El usuario no esta Activo";
                        }
                    }
                    else
                        System.Diagnostics.Trace.WriteLine("[ValidateUser] La validacion del usuario fallo.");

            }
            catch 
            { }
        }


        private void userSign(ref Users user, string name, string password, string userHosAddress)
        {
            InvalidCredentialsLog.insertInvalidCredentialsLog(user._usv_co, name, password, "T", "F", userHosAddress);

            try
            {
                user._logTx = new Log_Transaction();
                user._logTx = (Log_Transaction)Session[_nSlogTx];
                if (string.IsNullOrEmpty(user._logTx._ip))
                    user._logTx._ip = userHosAddress;
            }
            catch
            {
                user._logTx = new Log_Transaction(userHosAddress);                
            }
            finally { Session.Clear(); }

            user._usd_lastaccess = DateTime.Now;

            user._isnew = false;
            user._usv_username = name;
            Session[Constants.NameSessionUser] = user;

            // Async 
            Log_Transaction.registerUserInfo(user, "SIGNIN:" + user._usv_username);
        }

        /// <summary>
        /// Carga de usuario
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private Users loadUser(string userName)
        {
            DataTable dtUser = Users.F_LeerUsuario(userName);

            if (dtUser == null || dtUser.Rows.Count <= 0)
            {
                return null;
            }
            
            //string idUserEmployee = dtUser.Rows[0]["emn_employee_id"].ToString();
            //bool isEmployee = false;
            //string idUserCustomer = dtUser.Rows[0]["con_coordinator_id"].ToString();
            //bool isCustomer = false;
            //string wareId,wareDesc,areaId,areaDesc, regionId,nivel;
            DataRow dr = dtUser.Rows[0];

            // 1. Verificar si es empleado
            //if (string.IsNullOrEmpty(idUserEmployee))
            //    // 2. Verificar si es cliente
            //    if (string.IsNullOrEmpty(idUserCustomer))
            //        return null;
            //    else
            //    {                    
            //        isCustomer = true;
            //        wareId = dr["cov_warehouseid"].ToString();
            //        wareDesc = dr["wav_descriptionc"].ToString();
            //        areaDesc = dr["arv_descriptionc"].ToString();
            //        areaId = dr["bdv_area_id"].ToString();
            //        regionId = dr["dpv_region"].ToString();
            //        nivel = dr["nivel"].ToString();
            //    }
            //else
            //{
            Int32 v_bas_id; string v_usu_nombre; string v_usu_contraseña; string v_usu_est_id; string v_nombre; string v_usu_tip_id; string v_usu_tip_nombre;

            v_bas_id = Convert.ToInt32(dr["bas_id"].ToString());
            v_usu_nombre = dr["usu_nombre"].ToString();
            v_usu_contraseña = dr["usu_contraseña"].ToString();
            v_usu_est_id = dr["usu_est_id"].ToString();
            v_nombre = dr["nombre"].ToString();
            v_usu_tip_id = dr["usu_tip_id"].ToString();
            v_usu_tip_nombre = dr["usu_tip_nombre"].ToString();
                //isEmployee = true;
                //wareId = dr["dpv_warehouse"].ToString();
                //wareDesc = dr["wav_description"].ToString();
                //areaDesc = dr["arv_descriptione"].ToString();
                //areaId = dr["dpv_area"].ToString();
                //regionId = dr["dpv_region"].ToString();
                //nivel = dr["nivel"].ToString();
            //}

            Users u = new Users
            {
                 _bas_id = Convert.ToInt32(dr["bas_id"].ToString()),
                 _usu_nombre = dr["usu_nombre"].ToString(),
                 _usu_contraseña = dr["usu_contraseña"].ToString(),
                 _usu_est_id = dr["usu_est_id"].ToString(),
                _nombre = dr["nombre"].ToString(),
                _usu_tip_id = dr["usu_tip_id"].ToString(),
                _usu_tip_nombre = dr["usu_tip_nombre"].ToString(),
                _usu_flete = dr["usu_flete"].ToString(),
                _usv_area = dr["bas_Are_id"].ToString(),
                 _usn_userid = Convert.ToInt32(dr["bas_id"].ToString()),
                 _usv_username = dr["usu_nombre"].ToString(),
                 _asesor= dr["asesor"].ToString(),
                //_usn_userid = Convert.ToDecimal(dr["usn_userid"]),
                //_usv_co = dr["usv_co"].ToString(),
                //_usv_employee = isEmployee,
                //_usv_customer = isCustomer,
                //_usv_answer = dr["usv_answer"].ToString(),
                //_usv_question = dr["usv_question"].ToString(),
                //_usv_status = dr["usv_status"].ToString(),
                //_usv_username = dr["usv_username"].ToString(),
                //_usv_name = dr["name"].ToString(),
                _usd_creation = System.DateTime.Parse(dr["usu_fecha_cre"].ToString()),
                //_usv_warehouse = wareId,
                //_usv_warehouse_name = wareDesc,
                //_usv_area = areaId,
                //_usv_area_name = areaDesc,
                //_usv_region = regionId,
                //_usv_nivel = nivel,
                //_usv_password = dr["usv_password"].ToString(), 
                //_attempts = int.Parse(dr["usn_failedattemptcount"].ToString()),
                //_usv_postpago = dr["postpago"].ToString()
                 _usv_postpago = dr["postpago"].ToString()
            };

            return u;
        }

        private void loadMenu(decimal _bas_id)
        {
            List<ApplicationFunctions> colappFunctions = new List<ApplicationFunctions>();
            colappFunctions = ApplicationFunctions.getFunctions_tree(_bas_id);//  .getCoordinators(_user._usv_co, _user._usv_warehouse, _user._usv_area);
            Session["_MENU"] = colappFunctions;
            //dwCustomers.Focus();
            //// Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            //dwCustomers.DataSource = dsCustomers;
            //dwCustomers.DataBind();
        }

        #region < Ajax for user data info >

        /// <summary>
        /// Set de informacion de visitante
        /// </summary>
        /// <param name="userIp"></param>
        /// <param name="userCountry"></param>
        /// <param name="userRegion"></param>
        /// <param name="userCity"></param>
        /// <param name="userName"></param>
        [WebMethod()]
        public static void setUserInfo(string userIp, string userCountry, string userRegion, string userCity, string userName)
        {
            Log_Transaction lTLogin = new Log_Transaction(userIp, userName, userCountry, userRegion, userCity);

            HttpContext.Current.Session[_nSlogTx] = lTLogin;
        }

        #endregion

    }
}
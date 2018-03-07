using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using informativa.aquarella.com.oe.Models;
using informativa.aquarella.com.oe.Models.Util;
using informativa.aquarella.com.oe.Data;
using System.Security.Claims;


namespace informativa.aquarella.com.oe.Controllers
{
    public class AdminController : Controller
    {
        //AuthenticationManager Authentication
        //{
        //    get { return HttpContext.GetOwinContext().Authentication; }
        //}

        private LoginBL loginBl = new LoginBL();
        //public ActionResult Index()
        //{
        //    LoginModel view = new LoginModel();
        //    return View(view);
        //}

        // GET: Control
        [AllowAnonymous]
        public ActionResult Index(string returnUrl = null)
        {
            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];
            if (_usuario == null)
            {
                //Authentication.SignOut();
                Session.Clear();
            }

            ////ViewBag.returnUrl = returnUrl;

            LoginModel view = new LoginModel();
            view.returnUrl = returnUrl;
            ////return View(new LoginViewModel());
            //return View(view);
            return View(view);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(LoginModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string _error_con = "";
            Boolean _acceso = IsValid(model.Usuario, model.Password, ref _error_con);

            string return_action = ""; string return_controller = "";

            if (_acceso)
            {
                if (returnUrl != null)
                {
                    if (returnUrl.Length > 0)
                    {
                        string[] controller_action = returnUrl.Split('|');
                        return_action = controller_action[0].ToString();
                        return_controller = controller_action[1].ToString();
                    }
                }


                Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];
             
                if (return_action.Length == 0)
                {
                    return RedirectToAction("Inicio", "Admin");
                    //return RedirectToAction("Index", "Pasarela");
                }
                else
                {
                 
                    return RedirectToAction(return_action, return_controller);
                    /*************************************/
                }

            }
            else
            {
                if (_error_con == "1")
                {
                    ModelState.AddModelError("", "El intento de conexión no fue correcto. Inténtelo de nuevo");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError("", "Usuario y/o Password con incorrectos.");
                    return View(model);
                }
            }
        }

        public ActionResult Inicio()
        {

            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                return RedirectToAction("Index", "Admin", "");

            }
            else
            {
            
                return View();
            }
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Index", "Admin");
        }

        private bool IsValid(string usuario, string password, ref string _error_con)
        {
            bool _valida = false;
          
            Ent_Usuario _data_user = loginBl.get_login(usuario);


            if (_data_user == null) _valida = false;

            if (_data_user != null)
            {
                if (usuario.ToUpper() == _data_user.usu_login.ToUpper() && password == _data_user.usu_contraseña)
                {
                    if (_data_user.usu_est_id == "A")
                    {

                        string strIp = GetIPAddress(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"],
                                        System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                                        System.Web.HttpContext.Current.Request.UserHostAddress);

                        _data_user.usu_ip = strIp;

                        Session[Ent_Constantes.NameSessionUser] = _data_user;

                        _valida = true;
                    }
                    else
                    {
                        _valida = false;
                    }
                }
                else
                {
                    _valida = false;
                }
            }

            return _valida;
        }

        public static string GetIPAddress(string HttpVia, string HttpXForwardedFor, string RemoteAddr)
        {
            // Use a default address if all else fails.
            string result = "127.0.0.1";

            // Web user - if using proxy
            string tempIP = string.Empty;
            if (HttpVia != null)
                tempIP = HttpXForwardedFor;
            else // Web user - not using proxy or can't get the Client IP
                tempIP = RemoteAddr;

            // If we can't get a V4 IP from the above, try host address list for internal users.
            if (!IsIPV4(tempIP) || tempIP == "127.0.0.1 ")
            {
                try
                {
                    string hostName = System.Net.Dns.GetHostName();
                    foreach (System.Net.IPAddress ip in System.Net.Dns.GetHostAddresses(hostName))
                    {
                        if (IsIPV4(ip))
                        {
                            result = ip.ToString();
                            break;
                        }
                    }
                }
                catch { }
            }
            else
            {
                result = tempIP;
            }

            return result;
        }
             
        private static bool IsIPV4(string input)
        {
            bool result = false;
            System.Net.IPAddress address = null;

            if (System.Net.IPAddress.TryParse(input, out address))
                result = IsIPV4(address);

            return result;
        }

        private static bool IsIPV4(System.Net.IPAddress address)
        {
            bool result = false;

            switch (address.AddressFamily)
            {
                case System.Net.Sockets.AddressFamily.InterNetwork:   // we have IPv4
                    result = true;
                    break;
                case System.Net.Sockets.AddressFamily.InterNetworkV6: // we have IPv6
                    break;
                default:
                    break;
            }

            return result;
        }

    }
}
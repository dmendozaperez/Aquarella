using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using www.aquarella.pe.bll.util;
using www.aquarella.pe.Data.Control;
using www.aquarella.pe.Models.Cuenta;
namespace www.aquarella.pe.Controllers
{
    [Authorize]
    public class CuentaController : Controller
    {
        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        // GET: Cuenta
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = null)
        {
            Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];
            if (_usuario == null)
            {
                Authentication.SignOut();
                Session.Clear();
            }

            //ViewBag.returnUrl = returnUrl;

            LoginViewModel view = new LoginViewModel();
            view.returnUrl = returnUrl;
            //return View(new LoginViewModel());
            return View(view);
        }
        [HttpPost]
        [AllowAnonymous]        
        public ActionResult Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string _error_con = "";
            Boolean _acceso = IsValid(model.Usuario, model.Password, ref _error_con);

            string return_action = "";string return_controller = "";

            if (_acceso)
            {
                if (returnUrl!=null)
                {
                    if (returnUrl.Length>0)
                    { 
                        string[] controller_action = returnUrl.Split('|');
                        return_action = controller_action[0].ToString();
                        return_controller = controller_action[1].ToString();
                    }
                }


                Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, _usuario._usu_nom_ape), }, DefaultAuthenticationTypes.ApplicationCookie);
                Authentication.SignIn(new AuthenticationProperties
                {
                    IsPersistent = model.Recordar
                }, identity);


                if (return_action.Length==0)
                { 
                    return RedirectToAction("Index", "Home");
                }
                else
                {                    
                    /*validamos las opciones del menu de acceso*/
                    var data = new Data_Menu();                                      
                    var items = data.navbarItems(_usuario._usu_id).ToList();
                    Session[Global._session_menu_user] = items;
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
        private bool IsValid(string usuario, string password, ref string _error_con)
        {
            bool _valida = false;
            Usuario _usuario = new Usuario(usuario);

            if (_usuario._error_con == null) _error_con = "1";

            if (_usuario._error_con != null && _usuario._error_con == "0") _valida = false;

            if (_usuario._error_con != null && _usuario._error_con == "1")
            {
                if (usuario.ToUpper() == _usuario._usu_nombre.ToUpper() && password == _usuario._usu_password)
                {
                    if (_usuario._usu_est_id == "A")
                    {
                        Session[Constantes.NameSessionUser] = _usuario;

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
        public ActionResult LogOff()
        {

            Authentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Cuenta");
        }
    }
}
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
    public class CuentaController : Controller
    {
        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        // GET: Cuenta
        [AllowAnonymous]
        public ActionResult Login()
        {
            Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];
            if (_usuario == null)
            {
                Authentication.SignOut();
                Session.Clear();
            }
            return View(new LoginViewModel());
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string _error_con = "";
            Boolean _acceso = IsValid(model.Usuario, model.Password, ref _error_con);

            if (_acceso)
            {
                Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, _usuario._usu_nom_ape), }, DefaultAuthenticationTypes.ApplicationCookie);
                Authentication.SignIn(new AuthenticationProperties
                {
                    IsPersistent = model.Recordar
                }, identity);
                return RedirectToAction("Index", "Home");
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
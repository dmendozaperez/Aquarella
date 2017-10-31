using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace MultiAuthorize
{
    /// Verificación de inicio de sesión personalizada, se pueden aplicar múltiples inicios de sesión
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MultiAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Verificar el nombre del reconocimiento
        /// </summary>
        public string AuthorizeName { set; get; }
        /// <summary>
        /// Error de verificación El controlador de autenticación que se convertirá a
        /// </summary>
        public string AuthorizeController { set; get; }
        /// <summary>
        /// la validación no pudo verificar la conversión a Acción
        /// </summary>
        public string AuthorizeAction { set; get; }
        /// <summary>
        /// Verifica que el área de autenticación a convertirse en error
        /// </summary>
        public string AuthorizeArea { set; get; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var skipAuthorization =
                filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute),
                    inherit: true);

            if (skipAuthorization)
            {
                return;
            }

            var authCookie = AuthenticationHelper.GetAuthCookie(AuthorizeName ?? "User");

            // Verificar la URL a convertir
            RedirectToRouteResult authorizeUrl = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = AuthorizeController ?? "Account",
                            action = AuthorizeAction ?? "Login",
                            area = AuthorizeArea,
                            returnUrl = filterContext.HttpContext.Request.RawUrl
                        }));

            if (!AuthenticationHelper.CheckAuthorization(authCookie, Roles, Users))
                filterContext.Result = authorizeUrl;
        }
    }
}

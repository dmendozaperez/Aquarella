using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace MultiAuthorize
{
    public class AuthenticationHelper
    {
        /// <summary>
        /// Verificar inicio de sesión
        /// </summary>
        /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
        /// <param name = "name"> nombre de usuario </ param>
        /// <param name = "roles"> roles (múltiples roles separados por comas) </ param>
        /// <param name = "Timeout"> establece el tiempo de espera (el valor predeterminado es de 20 minutos) </ param>
        public static void SignIn(string authorizeName, string name, string roles, int Timeout = 20)
        {
            var authTicket = new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddMinutes(Timeout), false, roles);
            WriteAuthentication(authTicket, authorizeName);
        }

        /// <summary>
        /// Verificar cierre de sesión
        /// </summary>
        /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
        public static void SignOut(string authorizeName)
        {
            // Escribe una cookie vencida
            HttpContext.Current.Response.Headers.Add("Set-Cookie", Uri.EscapeDataString(authorizeName) + "=; path=/; expires=Thu, 01-Jan-1970 00:00:00 GMT");
        }

        /// <summary>
        /// Escribe el ticket de verificación en la cookie
                /// </ summary>
                /// <param name = "authTicket"> ticket de verificación </ param>
                /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
        private static void WriteAuthentication(FormsAuthenticationTicket authTicket, string authorizeName)
        {
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(authorizeName, encryptedTicket);
            authCookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// Obtenga la AuthCookie con el nombre especificado
                 /// </ summary>
                 /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
        /// <returns></returns>
        public static HttpCookie GetAuthCookie(string authorizeName)
        {
            return HttpContext.Current.Request.Cookies[authorizeName];
        }

        /// <summary>
        /// Toma un ticket de la cookie
        /// </summary>
        /// <param name="authCookie">Cookie</param>
        /// <returns></returns>
        public static FormsAuthenticationTicket GetAuthTicket(HttpCookie authCookie)
        {
            try
            {
                return FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return new FormsAuthenticationTicket(1, "", DateTime.Now, DateTime.Now.AddMinutes(-20), false, "");
            }
        }

        /// <summary>
        /// Se puede usar un ticket de verificación directa para obtener la información de inicio de sesión en el estado de inicio de sesión
                 /// </ summary>
                 /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
        /// <returns></returns>
        public static FormsAuthenticationTicket GetAuthTicket(string authorizeName)
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[authorizeName];
            FormsAuthenticationTicket authTicket = GetAuthTicket(authCookie);

            return authTicket;
        }

        /// <summary>
        /// determinar si el rol existe
                 /// </ summary>
                 /// <param name = "Roles"> rol del sistema </ param>
                 /// <param name = "UserRoles"> rol de usuario </ param>
                 /// <returns> </ returns>
        public static bool CheckRoleExists(string Roles, string UserRoles)
        {
            List<string> rolesList = rolesList = Roles.ToLower().Split(',').ToList();
            List<string> userRolesList = UserRoles.ToLower().Split(',').ToList();

            // tomar la intersección, el número de 0 que no cumple con el rol
            var intersectRoles = rolesList.Intersect(userRolesList);

            if (intersectRoles.Count() == 0)
            {
                return false;
            }

            return true;
        }

        #region Confirmar validación
        /// <resumen>
                 /// Verifica que authCookie sea válido
                 /// </ summary>
                 /// <param name = "authCookie"> Guarde la información de autorización para la cookie </ param>
        /// <returns></returns>
        public static bool CheckAuthorization(HttpCookie authCookie)
        {
            return CheckAuthorization(authCookie, null, null);
        }

        /// <summary>
        /// Verifica que authCookie sea válida y contenga el usuario especificado
                 /// </ summary>
                 /// <param name = "Users"> users </ param>
                 /// <param name = "authCookie"> Guarde la información de autorización para la cookie </ param>
        /// <returns></returns>
        public static bool CheckAuthorization(string Users, HttpCookie authCookie)
        {
            return CheckAuthorization(authCookie, null, Users);
        }

        /// <summary>
        /// Verificar que authCookie sea válida y contenga el rol especificado
                 /// </ summary>
                 /// <param name = "authCookie"> Guarde la información de autorización para la cookie </ param>
                 /// <param name = "Roles"> rol </ param>
        /// <returns></returns>
        public static bool CheckAuthorization(HttpCookie authCookie, string Roles)
        {
            return CheckAuthorization(authCookie, Roles, null);
        }

        /// <summary>
        /// Verifica que authCookie sea válida y contenga el rol o usuario especificado
                 /// </ summary>
                 /// <param name = "authCookie"> Guarde la información de autorización para la cookie </ param>
                 /// <param name = "Roles"> rol </ param>
                 /// <param name = "Users"> users </ param>
        /// <returns></returns>
        public static bool CheckAuthorization(HttpCookie authCookie, string Roles, string Users)
        {
            // Verificar que la cookie no existe para verificar el error
            if (authCookie == null || authCookie.Value == "")
            {
                return false;
            }

            FormsAuthenticationTicket authTicket = GetAuthTicket(authCookie);

            // Verificar que el ticket expire y falla
            if (authTicket.Expired || authTicket.Expiration <= DateTime.Now)
            {

                return false;
            }

            // Tener información del usuario para verificar que el usuario sea elegible
            if (!string.IsNullOrEmpty(Users))
            {
                // Verificar que el usuario sea coherente, no cumplir también falló
                List<string> usersList = Users.ToLower().Split(',').ToList();
                if (!usersList.Contains(authTicket.Name.ToLower()))
                {
                    return false;
                }
            }

            // tenemos información de roles para verificar que el rol sea consistente
            if (!string.IsNullOrEmpty(Roles))
            {
                // No cumplió el rol de fracaso
                if (!CheckRoleExists(Roles, authTicket.UserData))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}

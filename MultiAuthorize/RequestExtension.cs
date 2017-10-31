using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MultiAuthorize
{
    /// Solicitar método de extensión
    /// </summary>
    public static class RequestExtension
    {
        /// <summary>
        /// para determinar si hay un inicio de sesión y permiso
                 /// Aviso: no incluye caracteres de juicio y usuarios
                 /// </ summary>
                 /// <param name = "request"> </ param>
                 /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
        /// <returns></returns>
        public static bool IsAuthenticated(this HttpRequestBase request, string authorizeName)
        {
            HttpCookie authCookie = AuthenticationHelper.GetAuthCookie(authorizeName);

            return AuthenticationHelper.CheckAuthorization(authCookie);
        }

        /// <summary>
        /// para determinar si hay un inicio de sesión y permiso
                 /// Aviso: no incluye caracteres de juicio y usuarios
                 /// </ summary>
                 /// <param name = "request"> </ param>
                 /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
                 /// <param name = "roles"> role </ param>
                 /// <param name = "users"> usuario </ param>
        /// <returns></returns>
        public static bool IsAuthenticated(this HttpRequestBase request, string authorizeName, string roles, string users)
        {
            HttpCookie authCookie = AuthenticationHelper.GetAuthCookie(authorizeName);

            return AuthenticationHelper.CheckAuthorization(authCookie, roles, users);
        }

        /// <summary>
        /// Determina si la función especificada existe bajo el nombre de autenticación especificado y tiene permiso
                 /// </ summary>
                 /// <param name = "request"> </ param>
                 /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
                 /// <param name = "roles"> role </ param>
        /// <returns></returns>
        public static bool IsAuthenticatedRole(this HttpRequestBase request, string authorizeName, string roles)
        {
            HttpCookie authCookie = AuthenticationHelper.GetAuthCookie(authorizeName);

            return AuthenticationHelper.CheckAuthorization(authCookie, roles);
        }

        /// <summary>
        /// Determina si el usuario especificado existe bajo el nombre de autenticación especificado y tiene permiso
                 /// </ summary>
                 /// <param name = "request"> </ param>
                 /// <param name = "authorizeName"> utilizado para nombrar la autenticación de inicio de sesión diferente </ param>
                 /// <param name = "users"> usuario </ param>
        /// <returns></returns>
        public static bool IsAuthenticatedUser(this HttpRequestBase request, string authorizeName, string users)
        {
            HttpCookie authCookie = AuthenticationHelper.GetAuthCookie(authorizeName);

            return AuthenticationHelper.CheckAuthorization(users, authCookie);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using www.aquarella.pe.Data.Control;

namespace www.aquarella.pe.bll.util
{
    public class Global: Controller
    {
        public static string titulo_header { get { return "Aquarella - Perú"; } }
        public static string titulo_footer { get { return "Aquarella La vida en colores ! . All Rights Reserved"; } }

        public static string _session_menu_user { get { return "_session_menu_user"; } }

        public  Boolean AccesoMenu(List<Menu_Items> menu,Controller cont)
        {
             Boolean valida = false;
            try
            {               
                string actionName = cont.ControllerContext.RouteData.GetRequiredString("action");
                string controllerName = cont.ControllerContext.RouteData.GetRequiredString("controller");

                var existe = menu.Where(t => t.action == actionName && t.controller == controllerName).ToList();

                if (existe.Count > 0) valida = true;

            }
            catch 
            {

                valida=false;
            }
            return valida;
        }
    }
}
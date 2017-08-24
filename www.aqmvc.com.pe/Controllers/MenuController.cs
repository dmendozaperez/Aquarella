using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.aqmvc.com.pe.bll.util;
using www.aqmvc.com.pe.Data.Control;
using www.aqmvc.com.pe.Models.Cuenta;

namespace www.aqmvc.com.pe.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        [Authorize]
        public ActionResult Menu()
        {
            Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];
            var data = new Data_Menu();
            if (data == null) return View(new LoginViewModel());

            if (_usuario == null) return View(new LoginViewModel());
            var items = data.navbarItems(_usuario._usu_id).ToList();

            return PartialView("_Menu",items);
        }
    }
}
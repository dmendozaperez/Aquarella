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
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];
            if (_usuario == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }
            else
            {
                return View();
            }           

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
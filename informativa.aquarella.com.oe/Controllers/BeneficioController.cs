using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using informativa.aquarella.com.oe.Data;
using informativa.aquarella.com.oe.Models;
using informativa.aquarella.com.oe.Models.Util;

namespace informativa.aquarella.com.oe.Controllers
{
    public class BeneficioController : Controller
    {
        // GET: Beneficio
        public ActionResult Index()
        {

            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                return RedirectToAction("Index", "Admin", "");

            }
            else
            {
                return View(listar());
            }
        }

        public List<Ent_Pasarela> listar()
        {
            PasarelaDA pasarela = new PasarelaDA();
            List<Ent_Pasarela> listPasarela = new List<Ent_Pasarela>();
            listPasarela = pasarela.get_listaPasarela();

            return listPasarela;
        }
    }
}
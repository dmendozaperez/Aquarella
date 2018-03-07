using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using informativa.aquarella.com.oe.Data;
using informativa.aquarella.com.oe.Models;
using informativa.aquarella.com.oe.Models.Util;
using System.Web;

namespace informativa.aquarella.com.oe.Controllers
{
    public class PasarelaController : Controller
    {
        // GET: Pasarela
        private PasarelaBL pasarelaBl = new PasarelaBL();
        public ActionResult Index()
        {
            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                return RedirectToAction("Index", "Admin", "");

            }
            else
            {
                return RedirectToAction("ListarPasarela", "Pasarela", "");
            }
        }

        public ActionResult ListarPasarela()
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

        [HttpGet]
        public ActionResult Editar(string strId)
        {

            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                return RedirectToAction("Index", "Admin", "");

            }
            else
            {
                Ent_Pasarela pasarela = new Ent_Pasarela();
                PasarelaDA pasarelaDa = new PasarelaDA();
                pasarela = pasarelaDa.GetPasarela(strId);

                return View("Editar", pasarela);
            }

        }

        [HttpGet]
        public ActionResult Nuevo()
        {

            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                return RedirectToAction("Index", "Admin", "");

            }
            else
            {
                Ent_Pasarela pasarela = new Ent_Pasarela();

                return View("Nuevo", pasarela);
            }
        }
       
       
        [HttpPost]
        public ActionResult GuardarPasarela(Ent_Pasarela pasarela)
        {

            var oJRespuesta = new JsonResponse();
          
            Ent_Usuario _usuario = (Ent_Usuario)Session[Ent_Constantes.NameSessionUser];

            if (_usuario == null)
            {
                oJRespuesta.Data = -1;
                oJRespuesta.Message = "Debe Iniciar sessión.";


            }
            else
            {
                pasarela.Pasarela_UsuCrea = _usuario.usu_login;
                pasarela.Pasarela_Ip = _usuario.usu_ip;
                oJRespuesta.Data = pasarelaBl.InsertarPasarela(pasarela);
                oJRespuesta.Message = "Coleccion ha sido guardada.";
            }   

            return Json(oJRespuesta, JsonRequestBehavior.AllowGet);
        }
      

        [HttpPost]
        public ActionResult GuardarPasarelaArchivo(Ent_PasarelaArchivo pasarelaArchivo)
        {
            //int nIdRegistro = 0;
            var oJRespuesta = new JsonResponse();
            Boolean valido = true;
            valido = pasarelaBl.GuardarPasarelaArchivo(pasarelaArchivo.PasarelaDeta_Nombre1, pasarelaArchivo.PasarelaDet_archivo1);
            valido = pasarelaBl.GuardarPasarelaArchivo(pasarelaArchivo.PasarelaDeta_Nombre2, pasarelaArchivo.PasarelaDet_archivo2);
            valido = pasarelaBl.GuardarPasarelaArchivo(pasarelaArchivo.PasarelaDeta_Nombre3, pasarelaArchivo.PasarelaDet_archivo3);
            valido = pasarelaBl.GuardarPasarelaArchivo(pasarelaArchivo.PasarelaDeta_Nombre4, pasarelaArchivo.PasarelaDet_archivo4);
            valido = pasarelaBl.GuardarPasarelaArchivo(pasarelaArchivo.PasarelaDeta_Nombre5, pasarelaArchivo.PasarelaDet_archivo5);
            valido = pasarelaBl.GuardarPasarelaArchivo(pasarelaArchivo.PasarelaDeta_Nombre6, pasarelaArchivo.PasarelaDet_archivo6);

            return Json(oJRespuesta, JsonRequestBehavior.AllowGet);
        }



    }
}
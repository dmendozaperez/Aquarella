using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using informativa.aquarella.com.oe.Data;
using informativa.aquarella.com.oe.Models;
using informativa.aquarella.com.oe.Models.Util;
//using System.Web;

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

        [OutputCache(CacheProfile = "OneMinuteValidate")]
        public ActionResult GuardarPasarela()
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

                foreach (string fileName in Request.Files)
                {

                    HttpPostedFileBase file = Request.Files[fileName];
                    string nombrelbl = fileName.Remove(0, 19);
                    string nombre = Post("PasarelaDeta_Nombre" + nombrelbl);

                    Boolean valido = true;
                    valido = pasarelaBl.GuardarPasarelaArchivo(nombre, file);

                }

                Ent_Pasarela pasarela = new Ent_Pasarela();
                pasarela.Pasarela_id = Convert.ToInt32(Post("Pasarela_id"));
                pasarela.Pasarela_Titulo = Post("Pasarela_Titulo");
                pasarela.Pasarela_Descripcion = Post("Pasarela_Descripcion");
                pasarela.Pasarela_Estado = Post("Pasarela_Estado");
                pasarela.Pasarela_strDetalle = Post("Pasarela_strDetalle");
             
                pasarela.Pasarela_UsuCrea = _usuario.usu_login;
                pasarela.Pasarela_Ip = _usuario.usu_ip;
                oJRespuesta.Data = pasarelaBl.InsertarPasarela(pasarela);
                oJRespuesta.Message = "Coleccion ha sido guardada.";
            }   

            return Json(oJRespuesta, JsonRequestBehavior.AllowGet);
        }
      
        
        public static string Post(string campo)
        {

            bool existeParametro = System.Web.HttpContext.Current.Request.Form[campo] != null;
            string parametro = existeParametro ? System.Web.HttpContext.Current.Request.Form[campo].ToString().Trim() : string.Empty;
            return parametro;
        }



    }
}
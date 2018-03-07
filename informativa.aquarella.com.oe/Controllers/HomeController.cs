using informativa.aquarella.com.oe.Data;
using informativa.aquarella.com.oe.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace informativa.aquarella.com.oe.Controllers
{
    public class HomeController : Controller
    {
        private CatalogoBL catalogoBL = new CatalogoBL();

        public ActionResult Index()
        {

            //List<Imagen> imagen = new List<Imagen>();
            //List<string> listafotos = new List<string>();

            //string ruta = Server.MapPath("~/Slider");


            //string[] _archivos_foto = Directory.GetFiles(@ruta, "*.png");

            //for (Int32 i = 0; i < _archivos_foto.Length; ++i)
            //{
            //    Imagen img = new Imagen();
            //    img.name = Path.GetFileNameWithoutExtension(@_archivos_foto[i].ToString());
            //    img.ruta1 = ".." + "/assets/img/slider/" + img.name + ".png";
            //    img.ruta2 = "~/assets/img/slider/" + img.name + ".png";
            //    imagen.Add(img);
            //}

            //return View(imagen);
            return View(lista());
        }

        public List<Ent_PasarelaDetalle> lista()
        {
            PasarelaDA pasarela = new PasarelaDA();
            List<Ent_PasarelaDetalle> listPasarela = new List<Ent_PasarelaDetalle>();
            listPasarela = pasarela.get_listaPasarelaDetalle();

            return listPasarela;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        private LugarBL lugarbl = new LugarBL();

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            List<Lugar> list = lugarbl.Listar();
            ViewBag.lugar = list;
            return View();
        }
        public ActionResult Catalogo()
        {
            return View(listaCatalogo());
        }

        public List<Ent_Catalogo> listaCatalogo()
        {
            List<Ent_Catalogo> lista = new List<Ent_Catalogo>();
            lista = catalogoBL.get_listaCatalogo();


            return lista;
        }

        [HttpPost]
        public ActionResult EnviaCorreo(string nombres,string apellidos,string telefono,string email, string comentario, string direccion)
        {
            Correo envia = new Correo();
            envia._nombre = nombres;
            envia._apellidos = apellidos;
            envia._telefono = telefono;
            envia._email = email;
            envia._comentario = comentario;
            envia._direccion = direccion;

            Boolean valida = envia._enviar_contactenos();

            return Json(new { estado = (valida) ? "1" : "-1", desmsg = (valida) ? "Se envio el correo correctamente" : "Hubo un error al momento de enviar , por favor intente de nuevo." });
        }
    }
}
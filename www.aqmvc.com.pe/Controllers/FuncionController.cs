using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.aqmvc.com.pe.Data.Control;

namespace www.aqmvc.com.pe.Controllers
{
    public class FuncionController : Controller
    {
        private Funcion funcion = new Funcion();
        private string _session_listfuncion_private = "session_listfun_private";
        // GET: Funcion
        public ActionResult Index()
        {
            return View(lista());
            //return View();
        }

        public ActionResult Nuevo()
        {
            Funcion funcion = new Funcion();
            ViewBag.funcion = funcion.get_lista();
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(string nombre,string descripcion,string orden,string padre)
        {
            Funcion _funcion = new Funcion();

            Int32 ord = 0;
             Int32.TryParse(orden,out ord);

            _funcion.fun_id = "0";
            _funcion.fun_nombre = nombre;
            _funcion.fun_descripcion = descripcion;
            _funcion.fun_orden = ord.ToString();
            _funcion.fun_padre = padre;
            _funcion.fun_system = "0";

            Boolean _valida_nuevo = _funcion.InsertarFuncion();

            return Json(new { estado = (_valida_nuevo) ? "1" : "-1", desmsg = (_valida_nuevo) ? "Se actualizo satisfactoriamente." : "Hubo un error al actualizar." });
        }
        //public PartialViewResult ListaFuncion()
        //{
        //    return PartialView(lista());
        //}
        public List<Funcion> lista()
        {
            List<Funcion> listfuncion = funcion.get_lista(true);
            Session[_session_listfuncion_private] = listfuncion;
            return listfuncion;
        }
    }
}
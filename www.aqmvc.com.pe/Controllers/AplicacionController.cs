using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using www.aqmvc.com.pe.bll.util;
using www.aqmvc.com.pe.Data.Control;

namespace www.aqmvc.com.pe.Controllers
{
    public class AplicacionController : Controller
    {
        private Aplicacion aplicacion = new Aplicacion();
        private string _session_listaplicacion_private = "session_listapl_private";
        // GET: Aplicacion
        public ActionResult Index()
        {
            Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];
            if (_usuario == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }
            { 
                return View(lista());
            }
        }
        public List<Aplicacion> lista()
        {
            List<Aplicacion> listaplicacion = aplicacion.get_lista();
            Session[_session_listaplicacion_private] = listaplicacion;
            return listaplicacion;
        }
        public ActionResult Nuevo()
        {
            Estado drop_estado = new Estado();
            List<Estado_Select> estado_list_select  = drop_estado._LeerEstado(0);
            estado_list_select.RemoveAll((x) => x._est_id != "I" && x._est_id!="A");           
            ViewBag.estado = estado_list_select;
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(string apl_nombre,string apl_url,string apl_orden,string apl_est_id,
                                  string apl_controller,string apl_action)
        {
            Aplicacion _aplicacion = new Aplicacion();
            Int32 ord = 0;
            Int32.TryParse(apl_orden, out ord);

            _aplicacion.apl_id = "0";
            _aplicacion.apl_nombre = apl_nombre;
            _aplicacion.apl_tip_id = "F";
            _aplicacion.apl_url = apl_url;
            _aplicacion.apl_orden = ord.ToString();
            _aplicacion.apl_est_id = apl_est_id;
            _aplicacion.apl_controller = apl_controller;
            _aplicacion.apl_action = apl_action;

            Boolean _valida_nuevo = _aplicacion.InsertarAplicacion();
            return Json(new { estado = (_valida_nuevo) ? "1" : "-1", desmsg = (_valida_nuevo) ? "Se actualizo satisfactoriamente." : "Hubo un error al actualizar." });
        }
        public PartialViewResult ListaAplicacion()
        {
            return PartialView(lista());
        }
        public ActionResult Edit(int? id)
        {
            List<Aplicacion> listaplicacion = (List<Aplicacion>)Session[_session_listaplicacion_private];
            if (id == null || listaplicacion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplicacion filaaplicacion = listaplicacion.Find(x => x.apl_id == id.ToString());

            Funcion funcion = new Funcion();

            Estado drop_estado = new Estado();
            List<Estado_Select> estado_list_select = drop_estado._LeerEstado(0);
            estado_list_select.RemoveAll((x) => x._est_id != "I" && x._est_id != "A");
            ViewBag.estado = estado_list_select;
                        
            return View(filaaplicacion);
        }
        [HttpPost]
        public ActionResult Edit(string apl_id, string apl_nombre, string apl_url, string apl_orden, string apl_est_id,
                                  string apl_controller, string apl_action)
        {
            Aplicacion _aplicacion = new Aplicacion();
            Int32 ord = 0;
            Int32.TryParse(apl_orden, out ord);

            _aplicacion.apl_id = apl_id;
            _aplicacion.apl_nombre = apl_nombre;
            _aplicacion.apl_tip_id = "F";
            _aplicacion.apl_url = apl_url;
            _aplicacion.apl_orden = ord.ToString();
            _aplicacion.apl_est_id = apl_est_id;
            _aplicacion.apl_controller = apl_controller;
            _aplicacion.apl_action = apl_action;

            Boolean _valida_editar = _aplicacion.UpdateAplicacion();
            return Json(new { estado = (_valida_editar) ? "1" : "-1", desmsg = (_valida_editar) ? "Se actualizo satisfactoriamente." : "Hubo un error al actualizar." });
        }
    }
}
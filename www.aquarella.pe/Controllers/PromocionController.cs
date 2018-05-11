using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using www.aquarella.pe.bll.util;
using www.aquarella.pe.Data.Maestros;
using www.aquarella.pe.Data.Control;

namespace www.aquarella.pe.Controllers
{
    public class PromocionController : Controller
    {
        // GET: Funcion
        private Promocion promocion = new Promocion();
        private string _session_listpromocion_private = "session_listProm_private";
        private string _session_listfuncion_private = "";
        // GET: Funcion
        [Authorize]
        public ActionResult Index()
        {
            Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];

            string actionName = this.ControllerContext.RouteData.GetRequiredString("action");
            string controllerName = this.ControllerContext.RouteData.GetRequiredString("controller");
            string return_view = actionName + "|" + controllerName;

            if (_usuario == null)
            {
                return RedirectToAction("Login", "Cuenta", new { returnUrl = return_view });
            }
            else
            {
                #region<VALIDACION DE ROLES DE USUARIO>
                Boolean valida_rol = true;
                Global valida_controller = new Global();
                List<Menu_Items> menu = (List<Menu_Items>)Session[Global._session_menu_user];
                valida_rol = valida_controller.AccesoMenu(menu, this);
                #endregion
                if (valida_rol)
                { 
                    return View(lista());
                }
                else
                {
                    return RedirectToAction("Login", "Cuenta", new { returnUrl = return_view });
                }
            }

        }
        /*agreanfo controler*/
        public ActionResult Aplicacion(Decimal id)
        {
            List<Funcion> listfuncion = (List<Funcion>)Session[_session_listfuncion_private];
            if (listfuncion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcion filafuncion = listfuncion.Find(x => x.fun_id == id.ToString());
            ViewBag.funid = id.ToString();
            ViewBag.funnombre = filafuncion.fun_nombre.ToString();

            Aplicacion aplicacion = new Aplicacion();
            ViewBag.aplicacion = aplicacion.get_lista();

            return View(lista_fun_apl(id));
        }

        [HttpPost]
        public ActionResult Borrar_Apl(Decimal apl_id, Decimal fun_id)
        {
            FuncionAplicacion _funcion_apl = new FuncionAplicacion();

            Boolean _valida_borrar = _funcion_apl.Eliminar_App_Funcion(apl_id, fun_id);

            return Json(new { estado = (_valida_borrar) ? "1" : "-1", desmsg = (_valida_borrar) ? "Se borro correctamente." : "Hubo un error al borrar." });
        }

        [HttpPost]
        public ActionResult Agregar_Apl(Decimal apl_id, Decimal fun_id)
        {

            FuncionAplicacion _funcion_apl = new FuncionAplicacion();

            Boolean _valida_agregar = _funcion_apl.Insertar_App_Funcion(apl_id, fun_id);

            return Json(new { estado = (_valida_agregar) ? "1" : "-1", desmsg = (_valida_agregar) ? "Se agrego correctamente." : "Hubo un error al agregar." });
        }

        public ActionResult Edit(int? id)
        {

            List<Funcion> listfuncion = (List<Funcion>)Session[_session_listfuncion_private];
            if (id == null || listfuncion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcion filafuncion = listfuncion.Find(x => x.fun_id == id.ToString());

            Funcion funcion = new Funcion();
            ViewBag.funcion = funcion.get_lista();
            return View(filafuncion);
        }
        [HttpPost]
        public ActionResult Edit(string id, string nombre, string descripcion, string orden, string padre)
        {
            Funcion _funcion = new Funcion();

            Int32 ord = 0;
            Int32.TryParse(orden, out ord);

            _funcion.fun_id = id;
            _funcion.fun_nombre = nombre;
            _funcion.fun_descripcion = descripcion;
            _funcion.fun_orden = ord.ToString();
            _funcion.fun_padre = padre;
            _funcion.fun_system = "3";

            Boolean _valida_editar = _funcion.EditarFuncion();

            return Json(new { estado = (_valida_editar) ? "1" : "-1", desmsg = (_valida_editar) ? "Se actualizo satisfactoriamente." : "Hubo un error al actualizar." });
        }
        public ActionResult Nuevo()
        {
            Funcion funcion = new Funcion();
            ViewBag.funcion = funcion.get_lista();
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(string nombre, string descripcion, string orden, string padre)
        {
            Funcion _funcion = new Funcion();

            Int32 ord = 0;
            Int32.TryParse(orden, out ord);

            _funcion.fun_id = "0";
            _funcion.fun_nombre = nombre;
            _funcion.fun_descripcion = descripcion;
            _funcion.fun_orden = ord.ToString();
            _funcion.fun_padre = padre;
            _funcion.fun_system = "3";

            Boolean _valida_nuevo = _funcion.InsertarFuncion();

            return Json(new { estado = (_valida_nuevo) ? "1" : "-1", desmsg = (_valida_nuevo) ? "Se actualizo satisfactoriamente." : "Hubo un error al actualizar." });
        }
        public PartialViewResult ListaAplFun(Decimal funid)
        {
            ViewBag.funid = funid.ToString();
            return PartialView(lista_fun_apl(funid));
        }
        public PartialViewResult ListaFuncion()
        {
            return PartialView(lista());
        }
        public List<ent_Promocion> lista()
        {
            List<ent_Promocion> listPromocion = promocion.get_lista();
            Session[_session_listpromocion_private] = listPromocion;
            return listPromocion;
        }
        public List<FuncionAplicacion> lista_fun_apl(Decimal id)
        {
            FuncionAplicacion lista = new FuncionAplicacion();

            List<FuncionAplicacion> list_fun_apl = lista.get_lista(id);

            return list_fun_apl;

        }
    }
}
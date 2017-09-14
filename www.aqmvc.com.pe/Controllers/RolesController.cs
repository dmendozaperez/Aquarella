using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using www.aqmvc.com.pe.Data.Control;

namespace www.aqmvc.com.pe.Controllers
{
    public class RolesController : Controller
    {
        private string _session_listroles_private = "session_listroles_private";
        private Roles roles = new Roles();
        // GET: Roles
        public ActionResult Index()
        {
            return View(lista());
        }
        public List<Roles> lista()
        {
            List<Roles> listroles = roles.get_lista();
            Session[_session_listroles_private] = listroles;
            return listroles;
        }
        public PartialViewResult ListaRoles()
        {
            return PartialView(lista());
        }
        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nuevo(string nombre, string descripcion)
        {
            Roles _roles = new Roles();
            _roles.rol_nombre = nombre;
            _roles.rol_descripcion = descripcion;
            Boolean _valida_nuevo =_roles.InsertarRoles();

            return Json(new { estado = (_valida_nuevo) ? "1" : "-1", desmsg = (_valida_nuevo) ? "Se actualizo satisfactoriamente." : "Hubo un error al actualizar." });
        }
        public ActionResult Edit(int? id)
        {
            List<Roles> listroles = (List<Roles>)Session[_session_listroles_private];
            if (id == null || listroles == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles filaroles = listroles.Find(x => x.rol_id == id.ToString());

            return View(filaroles);
        }
        [HttpPost]
        public ActionResult Edit(string id, string nombre, string descripcion)
        {
            Roles _roles = new Roles();

            _roles.rol_id = id;
            _roles.rol_nombre = nombre;
            _roles.rol_descripcion= descripcion;
           

            Boolean _valida_editar = _roles.EditarRoles();

            return Json(new { estado = (_valida_editar) ? "1" : "-1", desmsg = (_valida_editar) ? "Se actualizo satisfactoriamente." : "Hubo un error al actualizar." });
        }
        public ActionResult Funcion(Decimal id)
        {
            List<Roles> listroles = (List<Roles>)Session[_session_listroles_private];
            if (listroles == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Roles filaroles = listroles.Find(x => x.rol_id == id.ToString());
            ViewBag.rolid = id.ToString();
            ViewBag.rolnombre = filaroles.rol_nombre.ToString();

            Funcion funciones = new Funcion();
            ViewBag.funciones = funciones.get_lista(true);

            return View(lista_rol_fun(id));
        }
        public List<RolesFuncion> lista_rol_fun(Decimal id)
        {
            RolesFuncion lista = new RolesFuncion();

            List<RolesFuncion> list_rol_fun = lista.get_lista(id);

            return list_rol_fun;

        }
        public PartialViewResult ListaFunRol(Decimal rolid)
        {
            ViewBag.rolid = rolid.ToString();
            return PartialView(lista_rol_fun(rolid));
        }

        [HttpPost]
        public ActionResult Borrar_Fun(Decimal fun_id, Decimal rol_id)
        {
            RolesFuncion _roles_fun = new RolesFuncion();

            Boolean _valida_borrar = _roles_fun.Eliminar_Fun_Roles(fun_id,rol_id);

            return Json(new { estado = (_valida_borrar) ? "1" : "-1", desmsg = (_valida_borrar) ? "Se borro correctamente." : "Hubo un error al borrar." });
        }

        [HttpPost]
        public ActionResult Agregar_Fun(Decimal fun_id, Decimal rol_id)
        {

            RolesFuncion _roles_fun = new RolesFuncion();

            Boolean _valida_agregar = _roles_fun.Insertar_Fun_Roles(fun_id, rol_id);

            return Json(new { estado = (_valida_agregar) ? "1" : "-1", desmsg = (_valida_agregar) ? "Se agrego correctamente." : "Hubo un error al agregar." });
        }
    }
}
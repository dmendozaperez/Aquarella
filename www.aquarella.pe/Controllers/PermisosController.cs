using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using www.aquarella.pe.bll.util;
using www.aquarella.pe.Data.Control;

namespace www.aquarella.pe.Controllers
{
    public class PermisosController : Controller
    {
        // GET: Permisos
        private Usuario usuario = new Usuario();
        private string _session_list_user_per_private = "_session_list_user_per_private";
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
            {
                #region<VALIDACION DE ROLES DE USUARIO>
                Boolean valida_rol = true;
                Global valida_controller = new Global();
                List<Menu_Items> menu = (List<Menu_Items>)Session[Global._session_menu_user];
                valida_rol = valida_controller.AccesoMenu(menu, this);
                #endregion
                if (valida_rol)
                { 
                    return View(Buscar(""));
                }
                else
                {
                    return RedirectToAction("Login", "Cuenta", new { returnUrl = return_view });
                }
            }
        }
        public List<UsuarioModel> Buscar(string _nombre)
        {
            _nombre = (_nombre == null) ? "" : _nombre;
            List<UsuarioModel> listuser = usuario.GetUserByName(_nombre);
            if (_nombre.Trim().Length == 0)
            {
                listuser = new List<UsuarioModel>();
            }
            else
            {
                listuser = usuario.GetUserByName(_nombre);
            }
            Session[_session_list_user_per_private] = listuser;
            return listuser;
        }
        public PartialViewResult ListaUsuario(string buscarnom)
        {
            return PartialView(Buscar(buscarnom));
        }
        public ActionResult Roles(Decimal id)
        {
            List<UsuarioModel> listusuarios = (List<UsuarioModel>)Session[_session_list_user_per_private];
            if (listusuarios == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsuarioModel filausuario = listusuarios.Find(x => x.usu_id == id);
            ViewBag.usuid = id.ToString();
            ViewBag.usunombre = filausuario.nombre.ToString();

            Roles roles = new Roles();
            ViewBag.roles = roles.get_lista();

            return View(lista_usu_rol(id));

        }
        [HttpPost]
        public ActionResult Borrar_Rol(Decimal usu_id, Decimal rol_id)
        {
            UsuarioRoles _usuario_rol = new UsuarioRoles();

            Boolean _valida_borrar = _usuario_rol.Eliminar_Rol_Usuario(usu_id,rol_id);

            return Json(new { estado = (_valida_borrar) ? "1" : "-1", desmsg = (_valida_borrar) ? "Se borro correctamente." : "Hubo un error al borrar." });
        }
        [HttpPost]
        public ActionResult Agregar_Rol(Decimal usu_id, Decimal rol_id)
        {

            UsuarioRoles _usuario_rol = new UsuarioRoles();

            Boolean _valida_agregar = _usuario_rol.Insertar_Rol_Usuario(usu_id, rol_id);

            return Json(new { estado = (_valida_agregar) ? "1" : "-1", desmsg = (_valida_agregar) ? "Se agrego correctamente." : "Hubo un error al agregar." });
        }
        public PartialViewResult ListaRolUsu(Decimal usuid)
        {
            ViewBag.usuid = usuid.ToString();
            return PartialView(lista_usu_rol(usuid));
        }
        public List<UsuarioRoles> lista_usu_rol(Decimal id)
        {
            UsuarioRoles lista = new UsuarioRoles();

            List<UsuarioRoles> list_usu_rol = lista.get_lista(id);

            return list_usu_rol;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
            return View(Buscar(""));
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
        public List<UsuarioRoles> lista_usu_rol(Decimal id)
        {
            UsuarioRoles lista = new UsuarioRoles();

            List<UsuarioRoles> list_usu_rol = lista.get_lista(id);

            return list_usu_rol;

        }
    }
}
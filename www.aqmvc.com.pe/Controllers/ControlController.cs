using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using www.aqmvc.com.pe.bll.util;
using www.aqmvc.com.pe.Data.Control;
using www.aqmvc.com.pe.Data.Util;

namespace www.aqmvc.com.pe.Controllers
{
    public class ControlController : Controller
    {
        // GET: Control
        private Usuario usuario = new Usuario();
        private string _session_listuser_private = "session_listuser_private";
        public  ActionResult Usuarios()
        {
            Usuario _usuario = (Usuario)Session[Constantes.NameSessionUser];
            if (_usuario == null)
            {
                return RedirectToAction("Login", "Cuenta");
            }
            else
            {
                return View(Buscar(""));
            }

        }
        public List<UsuarioModel> Buscar(string _nombre)
        {
            _nombre = (_nombre==null)?"":_nombre;
            List<UsuarioModel> listuser = usuario.GetUserByName(_nombre);
            if (_nombre.Trim().Length == 0)
            {
                listuser = new List<UsuarioModel>();
            }
            else
            {
                listuser = usuario.GetUserByName(_nombre);
            }
            Session[_session_listuser_private]=listuser;        
            return listuser;
        }
        public PartialViewResult ListaUsuario(string buscarnom)
        {            
            return PartialView(Buscar(buscarnom));
        }
        public ActionResult Edit(int? id)
        {
            List<UsuarioModel> listuser =  (List<UsuarioModel>)Session[_session_listuser_private];
            if (id == null || listuser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsuarioModel filauser =  listuser.Find(x => x.usu_id == id);
            filauser.usu_password = Cryptographic.decrypt(filauser.usu_password);

            Estado drop_estado = new Estado();
            ViewBag.estado =  drop_estado._LeerEstado(0);
            return View(filauser);
            //return View();
        }       
        [HttpPost]       
        public  ActionResult Edit(int _id,string _pass,string _estado)
        {          
                List<UsuarioModel> listuser = (List<UsuarioModel>)Session[_session_listuser_private];
                UsuarioModel filauser = listuser.Find(x => x.usu_id == _id);
                Usuario _usuario = new Usuario();
                _usuario._usu_id = _id;
                _usuario._usu_nombre = filauser.usu_nombre;
                _usuario._usu_est_id = _estado;
                _usuario._usu_fec_cre = filauser.usu_fecha_cre;
                _usuario._usu_password = Cryptographic.encrypt(_pass);
                Boolean _valida_update = _usuario.UpdateUsuario();
           // _valida_update = false;
            //string url = Url.Action("ListaUsuario", "Control", new { buscarnom = _usuario._usu_nombre });

            return Json(new { estado = (_valida_update) ? "1" : "-1", desmsg = (_valida_update) ? "Se actualizo satisfactoriamente." : "Hubo un error al actualizar." });            
             

        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using www.aqmvc.com.pe.bll.util;
using www.aqmvc.com.pe.Data.Cliente;
using www.aqmvc.com.pe.Data.Control;

namespace www.aqmvc.com.pe.Controllers
{
    public class RRHHController : Controller
    {
        List<Estado_Select> _select_estado = new List<Estado_Select>();
        List<Lider_Select> _select_lider = new List<Lider_Select>();
        Estado drop_estado = new Estado();
        Lider drop_lider = new Lider();
        // GET: RRHH
        [Authorize]
        public ActionResult UpdateCliente()
        {
            if (Session[Constantes.NameSessionUser] == null)
                return RedirectToAction("Login", "Cuenta");

            if (Request.IsAuthenticated)
            {
                ViewBag.estado = drop_estado._LeerEstado(0);
                ViewBag.lider = drop_lider._leer_lider();

                return View();
            }
            //return View();
            else
                return RedirectToAction("Login", "Cuenta");
        }

        [HttpPost]
        public ActionResult UpdateCliente(string _id,string _area,string _estado)
        {
            string _upd = Promotor.updateCoord(_id, _area, _estado);
            if (_upd.Length==0)
            {
                return Json(new { estado = "1", desmsg = "El cliente se actualizo correctamente" });
            }
            else
            {
                return Json(new { estado = "-1", desmsg = _upd });
            }
        }

        [HttpPost]
        public ActionResult GetCliente(string _documento)
        {
            DataTable dtgetcliente = Promotor.get_dtcliente(_documento);
            if (dtgetcliente != null)
            {
                if (dtgetcliente.Rows.Count > 0)
                {     
                    string _id= dtgetcliente.Rows[0]["Bas_Id"].ToString();
                    string _doc = dtgetcliente.Rows[0]["Bas_Documento"].ToString();
                    string _nomcli = dtgetcliente.Rows[0]["NombreCompleto"].ToString();
                    string _ciudad = dtgetcliente.Rows[0]["Ubicacion"].ToString();
                    string _fecha = String.Format("{0:D}", Convert.ToDateTime(dtgetcliente.Rows[0]["Bas_Fecha_Cre"]));
                    string _direccion = dtgetcliente.Rows[0]["Bas_direccion"].ToString();
                    string _telefono = dtgetcliente.Rows[0]["Bas_Telefono"].ToString();
                    string _estadocli = dtgetcliente.Rows[0]["Bas_Est_Id"].ToString();
                    string _lider= dtgetcliente.Rows[0]["Bas_are_Id"].ToString();
                    return Json(new { desmsg="Cliente encontrado", estado = "1", id=_id,doc = _doc, nomcli = _nomcli, ciudad = _ciudad, fecha = _fecha, direccion = _direccion, telefono = _telefono, estadocli = _estadocli,lider= _lider });
                }
            }
            return Json(new { estado = "-1", desmsg = "Cliente no encontrado" });
        }
    }
}
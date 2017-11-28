using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using www.aquarella.pe.Data.Cliente;
using www.aquarella.pe.Data.Maestros;
using www.aquarella.pe.Models.General;

namespace www.aquarella.pe.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        private Personal personal = new Personal();
        private string _session_listpersonal_private = "session_listper_private";
        public ActionResult Index()
        {
            return View(lista());
        }
        public PartialViewResult ListaPersonal()
        {
            return PartialView(lista());
        }
        public ActionResult Edit(int? id)
        {
            List<Personal> listpersonal = (List<Personal>)Session[_session_listpersonal_private];
            if (id==null || listpersonal==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Personal filapersonal = listpersonal.Find(x => x.bas_id == id);

            DataBasico data_maestros = new DataBasico();
            data_maestros.ejecuta();
            /*get maestros*/
            ViewBag.tipopersona = data_maestros.tipo_persona;
            ViewBag.tipousuario = data_maestros.tipo_usuario;
            ViewBag.tipodocumento = data_maestros.tipo_documento;
            ViewBag.departamento = data_maestros.departamento;
            /****FILTRAR****/
            ViewBag.provincia=data_maestros.filtrar_prov(filapersonal.depar);
            ViewBag.distrito = data_maestros.filtrar_dis(filapersonal.prv_id);

            /*******/

            return View(filapersonal);

        }
        public List<Personal> lista()
        {
            List<Personal> listpersonal = personal.get_lista();
            Session[_session_listpersonal_private] = listpersonal;
            return listpersonal;
        }
        public ActionResult getPersonal(jQueryDataTableParams param)
        {
            //Traer registros
            IQueryable<Personal> membercol = ((List<Personal>)(Session[_session_listpersonal_private])).AsQueryable();  //lista().AsQueryable();

            //Manejador de filtros
            int totalCount = membercol.Count();
            IEnumerable<Personal> filteredMembers = membercol;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                filteredMembers = membercol
                    .Where(m => m.nombres.ToUpper().Contains(param.sSearch.ToUpper()) ||
                     m.dni_ruc.ToUpper().Contains(param.sSearch.ToUpper()) ||
                     m.telefono.ToUpper().Contains(param.sSearch.ToUpper()) ||
                     m.celular.ToUpper().Contains(param.sSearch.ToUpper()) ||
                     m.correo.ToUpper().Contains(param.sSearch.ToUpper()) ||
                     m.tipo_usuario.ToUpper().Contains(param.sSearch.ToUpper()) ||
                     m.estado.ToUpper().Contains(param.sSearch.ToUpper()));
            }
            //Manejador de orden
            var sortIdx = Convert.ToInt32(Request["iSortCol_0"]);
            Func<Personal, string> orderingFunction =
            (
            m => sortIdx == 0 ? m.nombres :
            sortIdx == 1 ? m.dni_ruc :
            sortIdx == 2 ? m.telefono :
            sortIdx == 3 ? m.celular :
            sortIdx == 4 ? m.correo :
            sortIdx == 5 ? m.tipo_usuario :
            m.estado
            );
            var sortDirection = Request["sSortDir_0"];
            if (sortDirection == "asc")
                filteredMembers = filteredMembers.OrderBy(orderingFunction);
            else
                filteredMembers = filteredMembers.OrderByDescending(orderingFunction);
            var displayMembers = filteredMembers
                .Skip(param.iDisplayStart)
                .Take(param.iDisplayLength);
            var result = from a in displayMembers
                         select new
                         {
                             a.bas_id,
                             a.nombres,
                             a.dni_ruc,
                             a.telefono,
                             a.celular,
                             a.correo,
                             a.tipo_usuario,
                             a.estado
                         };
            //Se devuelven los resultados por json
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = totalCount,
                iTotalDisplayRecords = filteredMembers.Count(),
                aaData = result
            }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult Edit()
        //{
        //    return View();
        //}
    }
}
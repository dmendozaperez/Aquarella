using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad.Bll.Util
{
    public class Ent_Global
    {
        public static Int32 _bas_id_codigo { set; get; }
        public static string _nom_modulo { set; get; }

        public static string _modulo_activo { set; get; }

        public static Ent_Usuario _usuario_var { set; get; }

        public static Boolean _session_activa { set; get; }


        #region<CREACION DE VARIABLES PARA FACTURACION CON ALMACEN>
        public static string _pvt_nombre { get; set; }
        public static string _pvt_entorno { get; set; }
        public static string _pvt_almaid { get; set; }
        public static decimal _pvt_id { get; set; }
        public static string _alm_descripcion { get; set; }
        public static Boolean _pvt_directo { get; set; }
        public static Decimal _igv { get; set; }
        public static decimal _percepcion { get; set; }
        public static decimal _comision_porc { get; set; }

        public static Boolean _inicio_caja { get; set; }

        public static DateTime _fecha_server { get; set; }

        public static Boolean _fecha_cierre_valida { get; set; }
        public static DateTime _fecha_cierre_ult { get; set; }

        public static string _serie_imp { get; set; }
        public static string _impresora { get; set; }

        #endregion

    }
}

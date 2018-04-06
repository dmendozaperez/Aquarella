using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.Bll.Ventas
{
    public class ReporteComisionDetXDoc
    {
        #region<REGION DE ATRIBUTOS>
        public string _lider { set; get; }
        public string _cliente { set; get; }
        public string _nrodoc { set; get; }
        public DateTime _fechadoc { set; get; }
        public string _articulo { set; get; }
        public string _talla { set; get; }
        public Int32 _tpares { set; get; }
        public Decimal _spares { set; get; }
        public Decimal _comision { set; get; }
        public DateTime _fechaini { set; get; }
        public DateTime _fechafin { set; get; }
        public String _tipodoc { set; get; }
        public decimal _comsion_sum { set; get; }
        public String _asesor { set; get; }
        #endregion
        public ReporteComisionDetXDoc(string lider,string cliente,string nrodoc,DateTime fechadoc,string articulo,string talla,Int32 tpares,
                                      Decimal spares,Decimal comision,DateTime fechaini,DateTime fechafin,string tipodoc,decimal comision_sum,
                                      string asesor)
        {
            _lider = lider;
            _cliente = cliente;
            _nrodoc = nrodoc;
            _fechadoc = fechadoc;
            _articulo = articulo;
            _talla = talla;
            _tpares = tpares;
            _spares = spares;
            _comision = comision;
            _fechaini = fechaini;
            _fechafin = fechafin;
            _tipodoc = tipodoc;
            _comsion_sum = comision_sum;
            _asesor = asesor;
        }
        public static DataSet _reportecomision_XDoc(string _idlider,DateTime _fechaini,DateTime _fechafin)
        {
            string sqlquery = "USP_Leer_ComisionPersona_Detalle_XDoc";            
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@are_id", _idlider);
                cmd.Parameters.AddWithValue("@fecha_ini", _fechaini);
                cmd.Parameters.AddWithValue("@fecha_fin", _fechafin);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }

        public static DataSet _reportecomisionbono(DateTime _fechaini, DateTime _fechafin)
        {
            string sqlquery = "USP_Reporte_Comision";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@are_id", _idlider);
                cmd.Parameters.AddWithValue("@fecha_ini", _fechaini);
                cmd.Parameters.AddWithValue("@fecha_fin", _fechafin);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }
    }
}
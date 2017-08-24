using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.aquarella.com.pe.bll.Control;
using System.Data;
using System.Data.SqlClient;

namespace www.aquarella.com.pe.bll.Logistica
{
    public class ManifiestoBll
    {
        public static DataTable consulta_manifiesto(Decimal _manid,DateTime _fecha_desde,DateTime _fecha_hasta)
        {
            string sqlquery = "USP_Consultar_Manifiesto";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idmanifiesto", _manid);
                cmd.Parameters.AddWithValue("@fecha_desde", _fecha_desde);
                cmd.Parameters.AddWithValue("@fecha_hasta", _fecha_hasta);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch
            {
                dt = null;
                throw;
            }
            return dt;
        }
        public static DataSet Manifiesto_AgregarXDoc(string _doc)
        {
            string sqlquery = "USP_Manifiesto_AgregarXDoc";
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
                cmd.Parameters.AddWithValue("@doc", _doc);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch
            {
                ds = null;
                throw;
            }
            return ds;
        }
        public static Boolean actualizar_manifiesto(Int32 _estado,Decimal _idman,Decimal _usu,DataTable _dt,ref Decimal _idmanifiesto)
        {
            string sqlquery = "USP_Insertar_Modifica_Manifiesto";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            Boolean _valida = false;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estado", _estado);
                //cmd.Parameters.AddWithValue("@idmanifiesto", _idman);
                cmd.Parameters.AddWithValue("@usuario", _usu);
                cmd.Parameters.AddWithValue("@Tmp_Manifiesto_Detalle", _dt);
                cmd.Parameters.Add("@idmanifiesto",SqlDbType.VarChar,10);
                cmd.Parameters["@idmanifiesto"].Value = _idman.ToString();
                cmd.Parameters["@idmanifiesto"].Direction = ParameterDirection.InputOutput;
                cmd.ExecuteNonQuery();
                _idmanifiesto =Convert.ToDecimal(cmd.Parameters["@idmanifiesto"].Value);
                _valida = true;
            }
            catch
            {
                _valida = false;
                throw;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return _valida;
        }
        public static Decimal _correlativo_manifiesto()
        {
            string sqlquery = "USP_Correlativo_Manifiesto";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            decimal _correlativo = 0;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idmanifiesto", SqlDbType.Decimal);
                cmd.Parameters["@idmanifiesto"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                _correlativo =Convert.ToDecimal(cmd.Parameters["@idmanifiesto"].Value);
            }
            catch
            {
                 _correlativo=0;
                throw;
            }
            return _correlativo;
        }
        public static DataTable _consulta_modifica_manifiesto(decimal _idmanifiesto)
        {
            string sqlquery = "USP_Consulta_Manifiesto";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idmanifiesto", _idmanifiesto);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch
            {
                dt = null;
                throw;

            }
            return dt;
        }
        public static Boolean _anular_manifiesto(Decimal _usu,Decimal _idman)
        {
            string sqlquery = "USP_Anular_Manifiesto";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            Boolean _valida = false;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usuario", _usu);
                cmd.Parameters.AddWithValue("@idmanifiesto", _idman);
                cmd.ExecuteNonQuery();
                _valida = true;
            }
            catch
            {
                _valida = false;
                throw;
            }
            return _valida;
        }
        public static DataSet _reporte_manifiesto(decimal _idmanifiesto)
        {
            string sqlquery = "USP_Reporte_Manifiesto";
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
                cmd.Parameters.AddWithValue("@idmanifiesto", _idmanifiesto);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch
            {
                ds = null;
                throw;

            }
            return ds;
        }
        public static Decimal _valida_manifiesto(DataTable dt,ref string _des)
        {
            string sqlquery = "USP_Valida_Manifiesto";
            SqlConnection cn = null;
            SqlCommand cmd = null;           
            decimal _estado = -1;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tmp_Manifiesto_Detalle", dt);
                cmd.Parameters.Add("@estado", SqlDbType.Decimal);
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar, 100);

                cmd.Parameters["@estado"].Direction = ParameterDirection.Output;
                cmd.Parameters["@descripcion"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                _estado =(decimal) cmd.Parameters["@estado"].Value;
                _des = (string)cmd.Parameters["@descripcion"].Value;
            }
            catch
            {
                throw;
            }
            return _estado;
        }

    }
}
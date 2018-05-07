using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.Bll.Ventas
{
    public class DespachoAlmacen

    {
        
        public static DataSet getDespacho(int idDespacho)
        {

            string sqlquery = "USP_obtener_Despacho";
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
                cmd.Parameters.AddWithValue("@id_despacho", idDespacho);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getRotulo(string idLider, string descripcion)
        {

            string sqlquery = "USP_Despacho_listarRotulo";
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
                cmd.Parameters.AddWithValue("@idLider", idLider);
                cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getLiquidacionDespacho(DateTime _date_start, DateTime _date_end)
        {

            string sqlquery = "USP_Buscar_Despacho_Almacen";
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
                cmd.Parameters.AddWithValue("@fecha_inicio", _date_start);
                cmd.Parameters.AddWithValue("@fecha_final", _date_end);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getDespachos(DateTime _date_start, DateTime _date_end,string  strNroDocumento)
        {

            string sqlquery = "USP_Listar_Despacho_almacen";
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
                cmd.Parameters.AddWithValue("@Nro_documento", strNroDocumento);
                cmd.Parameters.AddWithValue("@fecha_inicio", _date_start);
                cmd.Parameters.AddWithValue("@fecha_final", _date_end);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet InsertarDespacho(Decimal _usu, int IdDespacho, string strListDetalle, string strListLiderDespachoLiquidacion, string Descripcion)
        {

            string sqlquery = "USP_Insertar_Despacho";
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
                cmd.Parameters.AddWithValue("@IdDespacho", IdDespacho);
                cmd.Parameters.AddWithValue("@strListDetalle", strListDetalle);
                cmd.Parameters.AddWithValue("@strListLiqui", strListLiderDespachoLiquidacion); 
                cmd.Parameters.AddWithValue("@strDescripcion", Descripcion);
                cmd.Parameters.AddWithValue("@UsuCrea", _usu);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        public static Boolean ActualizarDespacho(int IdDespacho, string strListDetalle, string strDescripcion)
        {
            string sqlquery = "USP_Actualizar_Despacho";
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
                cmd.Parameters.AddWithValue("@IdDespacho", IdDespacho);
                cmd.Parameters.AddWithValue("@strDescripcion", strDescripcion);
                cmd.Parameters.AddWithValue("@strListDetalle", strListDetalle);
                cmd.Parameters.AddWithValue("@UsuCrea", 0);
                cmd.ExecuteNonQuery();
                _valida = true;
            }
            catch (Exception ex)
            {
                _valida = false;
                throw;
            }
            return _valida;
        }
                
        public static Boolean ActualizarSalidaDespacho(Decimal _usu, int IdDespacho, string strEstadoDespacho, string strListDetalle, string strFlgAtendido)
        {
            string sqlquery = "USP_Actualizar_Salida_Despacho";
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
                cmd.Parameters.AddWithValue("@IdDespacho", IdDespacho);
                cmd.Parameters.AddWithValue("@strEstadoDespacho", strEstadoDespacho);
                cmd.Parameters.AddWithValue("@strListDetalle", strListDetalle);
                cmd.Parameters.AddWithValue("@strFlgAtendido", strFlgAtendido);
                cmd.Parameters.AddWithValue("@UsuCrea", _usu);
                cmd.ExecuteNonQuery();
                _valida = true;
            }
            catch (Exception ex)
            {
                _valida = false;
                throw;
            }
            return _valida;
        }
        public static Boolean Anular_DetalleDespacho(Decimal _usu,int IdDetalle)
        {
            string sqlquery = "USP_Anular_DespachoDetalle";
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
                cmd.Parameters.AddWithValue("@IdDespachoDetalle", IdDetalle);
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

    }


}
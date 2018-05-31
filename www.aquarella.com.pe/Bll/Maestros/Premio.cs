using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.bll
{
    public class Premio
    {
        public int promo_Id { get; set; }
        public string promo_Descripcion { get; set; }
        public int promo_Max_pares { get; set; }
        public Decimal promo_Porcentaje { get; set; }
        public string promo_FechaIni { get; set; }
        public string promo_FechaFin { get; set; }
        public string promo_strDetalle { get; set; }
        public int promo_usuarioId { get; set; }
        public decimal Premio_cantidad { get; set; }
        public string Premio_Articulo { get; set; }
        public string Premio_talla { get; set; }

        public static DataSet GetAllPremiosDS()
        {
            string sqlquery = "USP_Leer_Premios";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;   
                      
        }

        public static DataSet getPremiosArticuloStock(string _art, string _usu_tip)
        {
            string sqlquery = "USP_Leer_Premio_Articulo_Stock";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                ///
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@art_id", _art);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet getPremiosArticulo(string strIdPremio)
        {
            string sqlquery = "USP_Leer_Premio_Articulo";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                ///
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPremio", strIdPremio);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
               

        public static DataSet ConsultarPremiosArticulo(string strIdPremio)
        {
            string sqlquery = "USP_Consulta_Premio_Articulo";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                ///
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPremio", strIdPremio);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static Boolean InsertarArticuloPremio(Decimal _usu, string IdPremio, string strListDetalle)
        {

            string sqlquery = "USP_Insertar_ArticuloPremio";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            Boolean _valida = false;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPremio", IdPremio);
                cmd.Parameters.AddWithValue("@strListDetalle", strListDetalle);
                cmd.Parameters.AddWithValue("@UsuCrea", _usu);
                cmd.ExecuteNonQuery();
                cn.Close();
                _valida = true;

            }
            catch (Exception e)
            {
                cn.Close();
                throw new Exception(e.Message, e.InnerException);
                _valida = false;
            }

            return _valida;
        }

        public static Boolean EliminarArticuloPremio(Decimal _usu, string strIdDetalle)
        {

            string sqlquery = "USP_Eliminar_ArticuloPremio";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            Boolean _valida = false;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@strIdArticuloDetalle", strIdDetalle);
                cmd.Parameters.AddWithValue("@Usu", _usu);
                cmd.ExecuteNonQuery();
                cn.Close();
                _valida = true;

            }
            catch (Exception e)
            {
                cn.Close();
                throw new Exception(e.Message, e.InnerException);
                _valida = false;
            }

            return _valida;
        }

        public static Boolean Eliminar(Decimal _usu, string strListDetalle)
        {

            string sqlquery = "USP_Eliminar_ArticuloPremio";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            Boolean _valida = false;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@strListDetalleEliminar", strListDetalle);
                cmd.Parameters.AddWithValue("@Usu", _usu);
                cmd.ExecuteNonQuery();
                cn.Close();
                _valida = true;

            }
            catch (Exception e)
            {
                cn.Close();
                throw new Exception(e.Message, e.InnerException);
                _valida = false;
            }

            return _valida;
        }


    }
}
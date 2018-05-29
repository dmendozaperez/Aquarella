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


        public static DataTable GetAllMarcaDt()
        {
          
            string sqlquery = "USP_Leer_Marca";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;

        }

        public static DataTable GetAllArticuloDt(string MarcaId)
        {

            string sqlquery = "USP_Leer_ArticuloXMarca";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Parameters.AddWithValue("@marcaId", MarcaId);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;

        }

        public static bool deleteAppArticulo(int promoId, string @marcaId, string @articuloId)
        {
            string sqlquery = "USP_EliminarArticuloOferta";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@promoId", promoId);
                cmd.Parameters.AddWithValue("@marcaId", @marcaId);
                cmd.Parameters.AddWithValue("@articuloId", @articuloId);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataSet ArticulosXPromocion(int idPromo)
        {
            string sqlquery = "USP_Leer_ArticuloXMarca_Premio";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@promoId", idPromo);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }


        public static bool insertarMarcaArticulo(int idOferta, string idMarca, string idArticulo)
        {
            string sqlquery = "USP_Insertar_OfertaMarcaArticulo";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prom_id", idOferta);
                cmd.Parameters.AddWithValue("@prom_idmarca", idMarca);
                cmd.Parameters.AddWithValue("@prom_idArticulo", idArticulo);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool BuscarArticuloMarca(int idOferta,string idMarca, string idArticulo)
        {
           
            bool valido = true;
            string sqlquery = "USP_BuscarArticuloMarca";
            try
            {
                SqlConnection cn = new SqlConnection(Conexion.myconexion());
                SqlCommand cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PromoId", idOferta);
                cmd.Parameters.AddWithValue("@marcaId", idMarca);
                cmd.Parameters.AddWithValue("@articuloId", idArticulo);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                    valido = false;
            }
            catch (Exception ex) {
                valido = false;
            }

            return valido;

        }

        public bool InsertarPromocion()
        {
            string sqlquery = "USP_Insertar_Promocion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prom_id", promo_Id);
                cmd.Parameters.AddWithValue("@prom_descripcion", promo_Descripcion);
                cmd.Parameters.AddWithValue("@prom_porcentaje", promo_Porcentaje);
                cmd.Parameters.AddWithValue("@prom_pares", promo_Max_pares);
                cmd.Parameters.AddWithValue("@prom_FecIni", promo_FechaIni);
                cmd.Parameters.AddWithValue("@prom_FecFin", promo_FechaFin);
                cmd.Parameters.AddWithValue("@prom_usuario", promo_usuarioId);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool updatePromocion(int promo_id, string Ofe_Descripcion, string Ofe_MaxPares, string Ofe_Porc, string FechaIni, string FechaFin, int IdUser)
        {
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string sqlquery = "USP_Modificar_Promocion";
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@prom_id", promo_id);
                cmd.Parameters.AddWithValue("@prom_descripcion", Ofe_Descripcion);
                cmd.Parameters.AddWithValue("@prom_porcentaje", Ofe_Porc);
                cmd.Parameters.AddWithValue("@prom_pares", Ofe_MaxPares);
                cmd.Parameters.AddWithValue("@prom_FecIni", FechaIni);
                cmd.Parameters.AddWithValue("@prom_FecFin", FechaFin); 
                cmd.Parameters.AddWithValue("@prom_usuario", IdUser);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.Bll.Control
{
    public class CatalogoClass
    {
        #region<ATRIBUTO>
        public Decimal idcatalogo { get; set; }
        public string descripcion { get; set; }
        public string header_title { get; set; }
        public decimal nropagina { get; set; }
        public string est_id { get; set; }
        #endregion

        #region<metodo antiguo>
        public bool InsertCatalogo()
        {
            string sqlquery = "[USP_Insertar_Catalogo]";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcatalogo",idcatalogo);
                cmd.Parameters.AddWithValue("@descripcion",descripcion);
                cmd.Parameters.AddWithValue("@header_title",header_title);
                cmd.Parameters.AddWithValue("@nropagina",nropagina);
                cmd.Parameters.AddWithValue("@est_id",est_id);                
                cmd.ExecuteNonQuery();


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static DataTable GetAllCatalogo()
        {
            string sqlquery = "[USP_Leer_Catalogo]";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
        public static bool UpdateCatalogo(decimal CAT_IDCATALOGO, string CAT_DESCRIPCION, string CAT_HEADERTITLE, decimal CAT_NROPAGINA, string CAT_ESTID)
        {
        //    CAT_IDCATALOGO,CAT_DESCRIPCION,CAT_HEADERTITLE,CAT_NROPAGINA, CAT_ESTID
            string sqlquery = "[USP_Modificar_Catalogo]";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcatalogo", CAT_IDCATALOGO);
                cmd.Parameters.AddWithValue("@descripcion", CAT_DESCRIPCION);
                cmd.Parameters.AddWithValue("@Header_Title", CAT_HEADERTITLE);
                cmd.Parameters.AddWithValue("@NroPagina", CAT_NROPAGINA);
                cmd.Parameters.AddWithValue("@Est_Id", CAT_ESTID);                
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception) { return false; }

        }
        #endregion

        #region<PROPIEDADES ESTATICAS>
        public static Boolean actualizar_catalogo(Int32 _estado, Decimal _idcat,string _descripcion,string _header_title,decimal _nropagina, ref Decimal _idcatalogo)
        {
            string sqlquery = "[USP_Insertar_Modifica_Catalogo]";
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
                cmd.Parameters.AddWithValue("@descripcion", _descripcion);
                cmd.Parameters.AddWithValue("@header_title", _header_title);
                cmd.Parameters.AddWithValue("@nropagina", _nropagina);

                //cmd.Parameters.AddWithValue("@idmanifiesto", _idman);

                cmd.Parameters.Add("@idcatalogo", SqlDbType.VarChar,10);
                cmd.Parameters["@idcatalogo"].Value = _idcat.ToString();
                cmd.Parameters["@idcatalogo"].Direction = ParameterDirection.InputOutput;
                cmd.ExecuteNonQuery();
                _idcatalogo = Convert.ToDecimal(cmd.Parameters["@idcatalogo"].Value);
                _valida = true;
            }
            catch(Exception exc)
            {
                _valida = false;
                throw;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return _valida;
        }
        public static Decimal _correlativo_catalogo()
        {
            string sqlquery = "[USP_Correlativo_Catalogo]";
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
                cmd.Parameters.Add("@idcatalogo", SqlDbType.Decimal);
                cmd.Parameters["@idcatalogo"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                _correlativo = Convert.ToDecimal(cmd.Parameters["@idcatalogo"].Value);
            }
            catch
            {
                _correlativo = 0;
                throw;
            }
            return _correlativo;
        }
        public static Boolean _anular_catalogo(Decimal _idcatalogo)
        {
            string sqlquery = "[USP_Anular_Catalogo]";
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
                cmd.Parameters.AddWithValue("@idcatalogo", _idcatalogo);
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
        public static DataTable consulta_catalogo(String _decripcion)
        {
            string sqlquery = "[USP_Consultar_Catalogo]";
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
                cmd.Parameters.AddWithValue("@descripcion", _decripcion);                
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
        #endregion
    }
}
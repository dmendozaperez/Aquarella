using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.Bll.Admonred
{
    public class Asesor
    {
        #region<PROPIEDADES ESTATICOS>
        public static void convert_lider_asesor(string id)
        {
            string sqlquery = "USP_Convert_Lider_Asesor";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", id);
                cmd.ExecuteNonQuery();

            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
        public static DataTable getlider_usuarios(Boolean _solo_lider=false)
        {
            string sqlquery = "[USP_Leer_LiderUsuarios]";
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
                cmd.Parameters.AddWithValue("@vsolo_lider", _solo_lider);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
        public static Boolean update_lider_asesor(Int32 _estado,Decimal _idasesor,Decimal _idlider)
        {
            string sqlquery = "USP_Update_LiderAsesor";
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
                cmd.Parameters.AddWithValue("@bas_idasesor", _idasesor);
                cmd.Parameters.AddWithValue("@bas_idlider", _idlider);
                cmd.ExecuteNonQuery();
                _valida = true;
                return _valida;
            }
            catch(Exception e)
            {
                //_valida = false;
                if (cn != null)
                    if (cn.State == ConnectionState.Open) cn.Close();
                throw new Exception(e.Message, e.InnerException);
            }
        }
        public static DataTable dtleerlider_asesor(decimal _bas_id_asesor)
        {
            string sqlquery = "USP_LeerLiderAsesor";
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
                cmd.Parameters.AddWithValue("@bas_idasesor", _bas_id_asesor);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
        public static DataTable getasesor()
        {
            string sqlquery = "[USP_Leer_AsesorComercial]";
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
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
        #endregion

        #region<PRIMER CAMBIO HIT>
        #endregion
    }
}
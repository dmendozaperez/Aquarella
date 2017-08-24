using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aqmvc.com.pe.Data.Util;

namespace www.aqmvc.com.pe.Data.Cliente
{
    public class Promotor
    {
        public static string updateCoord(string _customerId, string _area, string _status)
        {
            string sqlquery = "USP_Modificar_Promotor_EstLid";
            string _error = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_id", _customerId);
                        cmd.Parameters.AddWithValue("@bas_est_id", _status);
                        cmd.Parameters.AddWithValue("@bas_are_id", _area);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception exc)
            {
                _error = exc.Message;
            }
            return _error;
        }
        public static DataTable get_dtcliente(string _documento)
        {
            string sqlquery = "USP_Leer_Persona_Usuario";
            DataTable dt = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bas_documento", _documento);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch
            {
                dt = null;
            }
            return dt;
        }
    }
}
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.Bll.Ventas
{
    public class ReporteMarcaxCategoria

    {
        
        public static DataSet getObtenerComboMarcaLider()
        {
            string sqlquery = "USP_ObtenerCombo_Marca_Lider";
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
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getReporteMarcaCategoria(string strMarca, string strLider, DateTime _date_start, DateTime _date_end)
        {

            string sqlquery = "USP_Rep_CategoriaXMarca";
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
                cmd.Parameters.AddWithValue("@marca", strMarca);
                cmd.Parameters.AddWithValue("@lider", strLider);
                cmd.Parameters.AddWithValue("@fecha_inicio", _date_start);
                cmd.Parameters.AddWithValue("@fecha_final", _date_end);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

   

    }


}
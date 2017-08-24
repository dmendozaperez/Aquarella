using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Variables;
namespace Sistema_Aquarella
{
    public class NotaCredito_Negocio
    {

        public static DataTable dt_consulta_notacredito(Boolean _tipo, DateTime _fecha_ini, DateTime _fecha_fin, string _doc)
        {
            string sqlquery = "USP_Consultar_NCredito_Anular";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipo", _tipo);
                cmd.Parameters.AddWithValue("@fechaini", _fecha_ini);
                cmd.Parameters.AddWithValue("@fechafin", _fecha_fin);
                cmd.Parameters.AddWithValue("@doc", _doc);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }
        public static DataTable searchArticleInvoice(String _noInvoice, String _article, String _size, String _customer, string _calidad)
        {
            //DataTable dt = new DataTable();
            //return dt;
            ///
            //DataTable dtResult = new DataTable();
            string sqlquery = "USP_Buscar_ArticuloXVenta";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {

                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", _noInvoice);
                cmd.Parameters.AddWithValue("@art_id", _article);
                cmd.Parameters.AddWithValue("@tal_id", _size);
                cmd.Parameters.AddWithValue("@bas_id", _customer);
                cmd.Parameters.AddWithValue("@calidad", _calidad);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static DataSet getCoordinatorByPk(decimal _idCoord)
        {
            string sqlquery = "USP_Leer_Persona_Usuario";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", _idCoord);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataSet getCoordinators(string _areaId)
        {
            string sqlquery = "USP_Leer_Promotor_Lider";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_are_id", _areaId);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
    }
}

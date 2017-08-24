using System;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace www.aquarella.com.pe.bll
{
    public class Picking
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Adicionar registro de inicio de marcacion para una liquidacion
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_noLiq"></param>
        /// <param name="_idEmp"></param>
        /// <returns></returns>
        /// 

        public static DataSet liquidacion_vs_despacho(DateTime _fechaini, DateTime _fechafin)
        {
            string sqlquery = "USP_Leer_PedYLiq_Despacho";
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
                cmd.Parameters.AddWithValue("@fecha_inicio", _fechaini);
                cmd.Parameters.AddWithValue("@fecha_final", _fechafin);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }


        public static string anular_liquidacion_marcacion(string _noliq)
        {
            string sqlquery = "USP_Anular_Liquidacion_Marcacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _noliq);
                cmd.ExecuteNonQuery();
                return "1";
            }
            catch(Exception exc)
            {
                throw new Exception(exc.Message, exc.InnerException);
            }
        }

        public static string addOrderToPicking(string _noLiq, string _idEmp)
        {
            //return "";
            string sqlquery = "USP_Insertar_EmpaqLiq";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _noLiq);
                cmd.Parameters.AddWithValue("@bas_id", _idEmp);
                cmd.Parameters.AddWithValue("@emp_liq_fechafin", DBNull.Value);
                cmd.ExecuteNonQuery();


                //Database db = DatabaseFactory.CreateDatabase(_conn);
                ////
                //string sqlCommand = "logistica.sp_add_picking";
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _noLiq, _idEmp, DBNull.Value);
                ////
                //db.ExecuteNonQuery(dbCommandWrapper);
                //
                return "1";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Finalizar la marcacion de una liquidacion
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_noLiq"></param>
        /// <param name="_status"></param>
        /// <returns></returns>
        public static string finalizePicking(string _noLiq)
        {
            //return "";
            string sqlquery = "USP_Finalizar_Empaque";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _noLiq);
                cmd.ExecuteNonQuery();
                return "1";

                //Database db = DatabaseFactory.CreateDatabase(_conn);
                ////
                //string sqlCommand = "logistica.sp_finalize_picking";
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _noLiq);
                ////
                //db.ExecuteNonQuery(dbCommandWrapper);
                ////
                //return "1";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }

        /// <summary>
        /// Consultar informacion de marcado de una liquidacion
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_noLiq"></param>
        /// <returns></returns>
        public static DataSet getInfoLiqPicking(string _noLiq)
        {
            string sqlquery = "USP_Leer_Info_Empaque";
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
                cmd.Parameters.AddWithValue("@liq_id", _noLiq);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_noLiq"></param>
        /// <returns></returns>
        public static DataSet getDtlPicking(string _noLiq)
        {
            string sqlquery = "USP_Leer_Empaque";
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
                cmd.Parameters.AddWithValue("@liq_id", _noLiq);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        public static DataSet getInfoPickingEmp()
        {
            string sqlquery = "USP_Leer_Info_EmpaqueUsu";
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

                //string sqlCommand = "logistica.sp_get_info_pickingEmpl";
                //// CURSOR REF
                //object results = new object[1];
                //// Create the Database object, using the default database service. The
                //// default database service is determined through configuration.
                //Database db = DatabaseFactory.CreateDatabase(_conn);
                /////                
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ware, results);
                //// DataSet that will hold the returned results		
                //// Note: connection closed by ExecuteDataSet method call 
                //return db.ExecuteDataSet(dbCommandWrapper);
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion
    }
}
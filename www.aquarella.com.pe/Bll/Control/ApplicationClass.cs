using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.aquarella.com.pe.bll.Util;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
namespace www.aquarella.com.pe.bll.Control
{
    public class ApplicationClass
    {
        public string _APV_CO { get; set; }
        public decimal _APN_ID { get; set; }
        public string _APV_NAME { get; set; }
        public string _APV_TYPE { get; set; }
        public string _APV_URL { get; set; }
        public decimal _APN_ORDER { get; set; }
        public string _APV_IMAGE { get; set; }
        public string _APV_STATUS { get; set; }
        public string _APV_HELP { get; set; }
        public string _APV_COMMENTS { get; set; }

       // public static string _conn = Constants.OrcleStringConn;


        #region <Metodos Publicos>

        public bool InsertApplication()
        {           
            string sqlquery = "USP_Insertar_Aplicacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@apl_id", _APN_ID);
                cmd.Parameters.AddWithValue("@apl_nombre", _APV_NAME);
                cmd.Parameters.AddWithValue("@apl_tip_id", _APV_TYPE);
                cmd.Parameters.AddWithValue("@apl_url", _APV_URL);
                cmd.Parameters.AddWithValue("@apl_orden", _APN_ORDER);
                cmd.Parameters.AddWithValue("@apl_est_id", _APV_STATUS);
                cmd.Parameters.AddWithValue("@apl_ayuda", _APV_HELP);
                cmd.Parameters.AddWithValue("@apl_comentario", _APV_COMMENTS);
                cmd.ExecuteNonQuery();

                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region <Metodos Estaticos>

        public static DataSet ApplicationByFunc(decimal FUN_ID)
        {
            string sqlquery = "USP_Leer_Apl_Fun";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Fun_Id", FUN_ID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);            
            return ds;            
        }

        public static DataTable GetAllAplications()
        {
            string sqlquery = "USP_Leer_Aplicacion";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static bool insertAppFunction(decimal _AFN_APLIID, decimal _AFN_FUNCTIONID)
        {            
            string sqlquery = "USP_Insertar_Apl_Fun";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@apl_fun_aplid", _AFN_APLIID);
                cmd.Parameters.AddWithValue("@apl_fun_funid", _AFN_FUNCTIONID);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool deleteAppFunction(decimal _AFN_APLIID, decimal _AFN_FUNCTIONID)
        {            
            string sqlquery = "USP_Borrar_Apl_Fun";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@apl_fun_aplid", _AFN_APLIID);
                cmd.Parameters.AddWithValue("@apl_fun_funid", _AFN_FUNCTIONID);
                cmd.ExecuteNonQuery();                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataTable GetApplicationType()
        {
            string sqlquery = "USP_Leer_Aplicacion_Tipo";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;

        }

        public static bool UpdateApplication( decimal APN_ID, string APV_NAME, string APV_TYPE, string APV_URL, decimal APN_ORDER, string APV_STATUS, string APV_HELP, string APV_COMMENTS)
        {           
            string sqlquery = "USP_Modificar_Aplicacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn =new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@apl_id", APN_ID);
                cmd.Parameters.AddWithValue("@apl_nombre", APV_NAME);
                cmd.Parameters.AddWithValue("@apl_tip_id", APV_TYPE);
                cmd.Parameters.AddWithValue("@apl_url", APV_URL);
                cmd.Parameters.AddWithValue("@apl_orden", APN_ORDER);
                cmd.Parameters.AddWithValue("@apl_est_id", APV_STATUS);
                cmd.Parameters.AddWithValue("@apl_ayuda", APV_HELP);
                cmd.Parameters.AddWithValue("@apl_comentario", APV_COMMENTS);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception) { return false; }

        }

        public static DataSet GetUserAppliFunction(string UAV_CO, decimal UAN_USERID)
        {
            DataSet ds = new DataSet();
            return ds;
            //object result = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "control.SP_USERLOAD_USER_APPLIFUNC2";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, UAV_CO, UAN_USERID, "A", result);

            //return db.ExecuteDataSet(dbCommandWrapper);
        }

        public static bool insetUserAppliFunction(string _UAV_CO, decimal _UAN_USERID, decimal _UAN_APLIID, decimal _UAN_FUN_ID)
        {
            return false;
            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "control.SP_ADD_USERS_APPLIFUNCTIONS";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _UAV_CO, _UAN_USERID, _UAN_APLIID, "A", _UAN_FUN_ID, "N");

            //try
            //{
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static bool deleteUserAppliFunction(decimal UAN_APPLIID, decimal UAN_FUN_ID, decimal UAN_USERID, string UAV_CO)
        {
            return false;
            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "control.SP_DELETE_USERS_APPLIFUNCTIONS";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, UAN_APPLIID, UAN_FUN_ID, UAN_USERID, UAV_CO);

            //try
            //{
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        public static decimal GetIDFunctionByApp(decimal _AFN_APLIID)
        {
            return 1;
            //object result = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "control.SP_GETFUNCTION_APP";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _AFN_APLIID, result);

            //DataSet data = db.ExecuteDataSet(dbCommandWrapper);

            //// Retorna el primer Identificador de funcion que encuentre. 
            //if (data.Tables[0].Rows.Count > 0)
            //    return Convert.ToDecimal(data.Tables[0].Rows[0][0]);
            //else
            //    return -1;

        }
        #endregion


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;


namespace www.aquarella.com.pe.bll.Control
{
    public class Functions
    {
        public string _FUV_CO { get; set; }
        public decimal _FUN_ID { get; set; }
        public string _FUV_NAME { get; set; }
        public string _FUV_DESCRIPTION { get; set; }
        public decimal? _FUN_ORDER { get; set; }
        public decimal? _FUN_FATHER { get; set; }
        public decimal _FUN_SYSTEM { get; set; }
        public string _FUN_URL { get; set; }

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        public bool InsertFunction()
        {
            string sqlquery = "USP_Insertar_Funcion";
            SqlConnection cn = null;
            SqlCommand cmd = null;          
            try
            {
                 cn = new SqlConnection(Conexion.myconexion());
                 if (cn.State == 0) cn.Open();
                 cmd = new SqlCommand(sqlquery, cn);
                 cmd.CommandTimeout = 0;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@fun_id", _FUN_ID);
                 cmd.Parameters.AddWithValue("@fun_nombre", _FUV_NAME);
                 cmd.Parameters.AddWithValue("@fun_descripcion", _FUV_DESCRIPTION);
                 cmd.Parameters.AddWithValue("@fun_orden", _FUN_ORDER);
                 cmd.Parameters.AddWithValue("@fun_padre", _FUN_FATHER);
                 cmd.Parameters.AddWithValue("@fun_sisid", _FUN_SYSTEM);
                 cmd.ExecuteNonQuery();               
                return true;
                
            }
            catch (Exception)
            {
                return false;
            }
            
        }


        #region <Metodos estaticos>
        public static DataTable GetAllFunctions()
        {
            string sqlquery="USP_Leer_Funcion_Sistema";
            SqlConnection cn=new SqlConnection(Conexion.myconexion());
            SqlCommand cmd=new SqlCommand(sqlquery,cn);
            cmd.CommandTimeout=0;
            cmd.CommandType=CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            da.Fill(dt);                     
            return dt;            
        }

        public static DataTable Get_PAYMENT_STATUS()
        {
            string sqlquery = "USP_Leer_Pago_Estado";
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
            catch (Exception)
            {
                throw;
            }

            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "CONTROL.SP_PAYMENT_STATUS";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);
            //return (db.ExecuteDataSet(dbCommandWrapper)).Tables[0];

            
        }



        public static DataSet GetAllFunctionsDS()
        {
            string sqlquery = "USP_Leer_Funcion_Sistema";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;   

            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "control.SP_LOAQUARELLALL_FUNCTIONS_SYS3";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);
            //return db.ExecuteDataSet(dbCommandWrapper);
            //DataSet ds = new DataSet();
            //return ds;
        }

        public static DataSet GetFunctionsByRol(decimal RON_ID)
        {           
            string sqlquery = "USP_Leer_Funcion_Roles";
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
                cmd.Parameters.AddWithValue("@rol_id", RON_ID);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception)
            {
                throw;
            }
           

        }

        public static bool updateFunction(decimal FUN_ID, string FUV_NAME, string FUV_DESCRIPTION, decimal? FUN_ORDER, decimal? FUN_FATHER)
        {          
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string sqlquery="USP_Modificar_Funcion";
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fun_id", FUN_ID);
                cmd.Parameters.AddWithValue("@fun_nombre", FUV_NAME);
                cmd.Parameters.AddWithValue("@fun_descripcion", FUV_DESCRIPTION);
                cmd.Parameters.AddWithValue("@fun_orden", FUN_ORDER);
                cmd.Parameters.AddWithValue("@fun_padre", FUN_FATHER);

                cmd.ExecuteNonQuery();                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string ValidarTarjetaPOS_UPD(string P_numTarjetaPos)
        {
            string sqlquery = "USP_Tarjeta_Visa_Valida";
            SqlConnection cn = null;
            SqlCommand cmd = null;          
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero", P_numTarjetaPos);
                cmd.Parameters.AddWithValue("@valida", DbType.Int32);
                cmd.Parameters["@valida"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                int rpta = Convert.ToInt32(cmd.Parameters["@valida"].Value);
                return Convert.ToString(rpta);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }            
        }
        public static void eliminar_pago(string _pag_op)
        {
            string sqlquery = "USP_Eliminar_Consignacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@op", _pag_op);
                cmd.ExecuteNonQuery();                
            }
            catch
            {
                throw;
            }
        }

        public static string updateFunction_UPD(string P_PAV_PAYMENT_ID, string P_PAV_NUM_CONSIGNACION, string P_PAD_PAY_DATE, decimal? P_PAN_AMOUNT, string stv_description)
        {
            string sqlquery = "USP_Modificar_Pago";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@pag_id", P_PAV_PAYMENT_ID);
                cmd.Parameters.AddWithValue("@pag_num_consfecha", P_PAD_PAY_DATE);
                cmd.Parameters.AddWithValue("@Pag_Monto", P_PAN_AMOUNT);
                cmd.Parameters.AddWithValue("@pag_estid", stv_description);
                cmd.Parameters.AddWithValue("@pag_op", P_PAV_NUM_CONSIGNACION);
                cmd.ExecuteNonQuery();
                return "bien";
            }
            catch (Exception exc)
            {
                return exc.Message;
            }
                       
        }

        //public static bool updateFunction_UPD(string P_PAV_CO, string P_PAV_PAYMENT_ID, string P_PAV_NUM_CONSIGNACION, string P_PAD_PAY_DATE, decimal? P_PAN_AMOUNT, string stv_description)
        //{
        //    Database db = DatabaseFactory.CreateDatabase(_conn);

        //    string sqlComand = "financiera.SP_ADD_PAYMENTS_UPDATE";

        //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlComand);

        //    db.AddInParameter(dbCommandWrapper, "P_PAV_CO", DbType.String, P_PAV_CO);
        //    db.AddInParameter(dbCommandWrapper, "P_PAV_PAYMENT_ID", DbType.String, P_PAV_PAYMENT_ID);
        //    db.AddInParameter(dbCommandWrapper, "P_PAV_NUM_CONSIGNACION", DbType.String, P_PAV_NUM_CONSIGNACION);
        //    db.AddInParameter(dbCommandWrapper, "P_PAD_PAY_DATE", DbType.Date, P_PAD_PAY_DATE);
        //    db.AddInParameter(dbCommandWrapper, "P_PAN_AMOUNT", DbType.Decimal, P_PAN_AMOUNT);
        //    db.AddInParameter(dbCommandWrapper, "P_PAV_STATUS", DbType.String, stv_description);


        //    try
        //    {
        //        db.ExecuteNonQuery(dbCommandWrapper);
        //        return true;

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        public static bool InsertRoleFunction(decimal _RFN_FUNCTIONID, decimal _RFN_ROLEID)
        {          
            string sqlquery = "USP_Insertar_Roles_Funcion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rol_fun_rolid", _RFN_ROLEID);
                cmd.Parameters.AddWithValue("@rol_fun_funid", _RFN_FUNCTIONID);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool RemoveRoleFunction(decimal _RFN_FUNCTIONID, decimal _RFN_ROLEID)
        {           
            string sqlquery = "USP_Borrar_Roles_Funcion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rol_fun_rolid", _RFN_ROLEID);
                cmd.Parameters.AddWithValue("@rol_fun_funid", _RFN_FUNCTIONID);
                cmd.ExecuteNonQuery();                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataTable Get_CARGAR_POS(string _co, string _postpago)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "Financiera.sp_concepto_mediopago";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _postpago, results);
            //    return (db.ExecuteDataSet(dbCommandWrapper)).Tables[0];
            //}
            //catch (Exception)
            //{
            //    return null;
            //}
        }



        #endregion
    }
}
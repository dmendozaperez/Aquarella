using System;
using System.Data;
using System.Data.Common;



namespace www.aquarella.com.pe.bll.Control
{
    public class InvalidCredentialsLog
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Consultar la configuración de seguridad
        /// </summary>
        /// <param name="usv_username"></param>
        /// <returns></returns>
        public static bool insertInvalidCredentialsLog(string icv_co, string icv_username, string icv_password, string icv_isapproved,
           string icv_islockedout, string icv_ipaddress)
        {
            try
            {
                return false;
                //string sqlCommand = "control.SP_INSERT_CREDENTIALSLOG";
                //// CURSOR REF
                //object results = new object[1];
                //// Create the Database object, using the default database service. The
                //// default database service is determined through configuration.
                //Database db = DatabaseFactory.CreateDatabase(_conn);
                /////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, icv_co, icv_username, icv_password, icv_isapproved, icv_islockedout, icv_ipaddress);
                //// DataSet that will hold the returned results		
                //// Note: connection closed by ExecuteDataSet method call 
                //db.ExecuteNonQuery(dbCommandWrapper);
                //return true;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }



        #endregion
    }
}
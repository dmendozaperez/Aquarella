using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
namespace www.aquarella.com.pe.bll
{
    public class Status
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Consultar estados por módulo
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_module"></param>
        /// <returns></returns>
        public static DataSet getStatusByModule(string _module)
        {            
            string sqlquery = "USP_Leer_EstadoModulo";
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
                cmd.Parameters.AddWithValue("@est_mod_id", _module);
                da=new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getStatusCedi(string _co)
        {
            DataSet ds = new DataSet();
            return ds;
            // CURSOR REF
            //object results = new object[1];
            //try
            //{
            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    string sqlCommand = "control.SP_GETSTATUS_CEDI";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        #endregion
    }
}
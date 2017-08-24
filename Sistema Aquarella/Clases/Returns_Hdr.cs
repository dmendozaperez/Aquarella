using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Variables;
namespace Sistema_Aquarella
{
    public class Returns_Hdr
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        //private String RHV_CO;
        //private String RHV_RETURN_NO;
        //private DateTime RHD_DATE;
        //private String RHV_TRANSACTION;
        //private Decimal RHN_PERSON;
        //private Decimal RHN_COORDINATOR;
        //private Decimal RHN_EMPLOYEE;

        #endregion


        #region < Metodos estaticos >


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_statusid"></param>
        /// <param name="_areaid"></param>
        /// <param name="_warehouse"></param>
        /// <returns></returns>
        public static DataSet getReturnsHdrByStatus(string _co, string _statusid, string _areaid, string _warehouse)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "ventas.sp_get_returns_bystatus";
            //    // CURSOR REF
            //    object results = new object[1];
            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///                
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _statusid, _areaid, _warehouse, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }



        /// <summary>
        /// Separar pedidos borrador y generar liquidacion
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_ordersChain">Pedidos en borrador: 1,2 etc</param>
        /// <returns></returns>
        public static object[] approvalReturnsList(string _co, string _return_no, decimal _employee)
        {
            String[] results = new String[3];
            return results;
            //object[] result = new object[2];
            //result[0] = false;
            //result[1] = "";
            //string sqlCommand = "ventas.sp_approval_returns_list";
            //try
            //{
            //    //
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //    // Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos 
            //    using (DbConnection connection = db.CreateConnection())
            //    {
            //        connection.Open();
            //        DbTransaction transaction = connection.BeginTransaction();
            //        try
            //        {
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _return_no, _employee);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            // Commit the transaction.
            //            transaction.Commit();
            //            result[0] = true;
            //            result[1] = "";
            //        }
            //        catch (Exception ex)
            //        {
            //            // Roll back the transaction. 
            //            transaction.Rollback();
            //            result[0] = false;
            //            result[1] = ex.Message;
            //        }
            //        finally
            //        {
            //            connection.Close();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result[0] = false;
            //    result[1] = ex.Message;
            //}
            //return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="RHV_CO"></param>
        /// <param name="RHV_RETURN_NO"></param>
        /// <param name="RHD_DATE"></param>
        /// <param name="RHV_TRANSACTION"></param>
        /// <param name="RHN_PERSON"></param>
        /// <param name="RHN_COORDINATOR"></param>
        /// <param name="RHN_EMPLOYEE"></param>
        /// <param name="listArticlesReturned"></param>
        /// <returns></returns>
        public static String[] saveReturnOrder(
            string RHN_COORDINATOR, string _ALMACEN, List<Returns_Dtl> listArticlesReturned, Int32 _usuing,string _codigo_estado)
        {
            //String[] results = new String[3];
            //return results;
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string resultDoc = "";
            //string storageDevol = "";
            //string noTransaccion = "";

            ///// Nombre del procedimiento
            //string sqlCommand = "ventas.SP_ADD_RETURNSHDR";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "P_RHV_CO", DbType.String, RHV_CO);
            /////
            /////db.AddInParameter(dbCommandWrapper, "P_RHN_PERSON", DbType.Decimal, RHN_PERSON);
            /////
            //db.AddInParameter(dbCommandWrapper, "P_RHN_COORDINATOR", DbType.String, RHN_COORDINATOR);
            /////
            //db.AddInParameter(dbCommandWrapper, "P_RHN_EMPLOYEE", DbType.Decimal, RHN_EMPLOYEE);
            /////
            //db.AddInParameter(dbCommandWrapper, "P_STV_WAREHOUSE", DbType.String, STV_WAREHOUSE);

            //// Output parameters specify the size of the return data.            
            //db.AddOutParameter(dbCommandWrapper, "P_RHV_RETURN_NO", DbType.String, 12);
            //// Storage a donde se enviaron los articulos devueltos
            //db.AddOutParameter(dbCommandWrapper, "P_STORAGE_DEVOL", DbType.String, 12);

            ////ahora agregamos la percepcion///

            //db.AddInParameter(dbCommandWrapper, "p_percepcion", DbType.Decimal, 0);
            //db.AddInParameter(dbCommandWrapper, "p_porc_percepcion", DbType.Decimal, 0);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //        /// Recuperar el parametro de salida
            //        resultDoc = (String)db.GetParameterValue(dbCommandWrapper, "P_RHV_RETURN_NO");
            //        storageDevol = (String)db.GetParameterValue(dbCommandWrapper, "P_STORAGE_DEVOL");

            //        // Recorrer todas las lineas adicionadas al detalle
            //        foreach (Returns_Dtl item in listArticlesReturned)
            //        {
            //            /// Procedimiento que adiciona las lineas de detalle
            //            sqlCommand = "ventas.SP_ADD_RETURNSDTL";

            //            ///
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                RHV_CO,
            //                resultDoc,
            //                item._RDV_INVOICE,
            //                item._RDV_ARTICLE,
            //                item._RDV_SIZE,
            //                item._RDN_QTY);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        }


            //        ///////////////////////////////////////////////////////////////////////////////////////////
            //        /// Procedimiento que adiciona las lineas de detalle
            //        sqlCommand = "ventas.SP_ADD_DOCTRANS_RETURNS";
            //        ///
            //        dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //            RHV_CO,
            //            resultDoc, 12);
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);///
            //        /// Recuperar el parametro de salida
            //        noTransaccion = (String)db.GetParameterValue(dbCommandWrapper, "P_DTV_TRANSDOC");
            //        ///////////////////////////////////////////////////////////////////////////////////////////

            //        // Commit the transaction.
            //        transaction.Commit();

            //        String[] results = new String[3];
            //        results[0] = resultDoc;
            //        results[1] = noTransaccion;
            //        results[2] = storageDevol;

            //        connection.Close();
            //        return results;

            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        resultDoc = "0";
            //        connection.Close();
            //        return null;
            //    }
            //}
            string sqlquery = "USP_Insertar_NotaCredito";
            //string sqlquery = "[USP_Insertar_NotaCredito_Tmp]";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            DataTable dt = null;
            String[] results = new String[3];
            try
            {
                dt = new DataTable();
                dt.Columns.Add("Not_Det_Id", typeof(string));
                dt.Columns.Add("Not_Det_Item", typeof(Int32));
                dt.Columns.Add("Not_Det_VenId", typeof(string));
                dt.Columns.Add("Not_Det_ArtId", typeof(string));
                dt.Columns.Add("Not_Det_TalId", typeof(string));
                dt.Columns.Add("Not_Det_Cantidad", typeof(Int32));
                dt.Columns.Add("Not_Det_Calidad", typeof(string));

                Int32 item_nc = 0;

                foreach (Returns_Dtl item in listArticlesReturned)
                {
                    item_nc += 1;
                    dt.Rows.Add("", item_nc, item._RDV_INVOICE, item._RDV_ARTICLE, item._RDV_SIZE, item._RDN_QTY, item._CALIDAD);

                    /// Procedimiento que adiciona las lineas de detalle
                    //sqlCommand = "ventas.SP_ADD_RETURNSDTL";

                    /////
                    //dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
                    //    RHV_CO,
                    //    resultDoc,
                    //    item._RDV_INVOICE,
                    //    item._RDV_ARTICLE,
                    //    item._RDV_SIZE,
                    //    item._RDN_QTY);
                    //db.ExecuteNonQuery(dbCommandWrapper, transaction);
                }

                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@alm_id", _ALMACEN);
                cmd.Parameters.AddWithValue("@bas_id", RHN_COORDINATOR);
                cmd.Parameters.AddWithValue("@not_id", DbType.String);
                cmd.Parameters.AddWithValue("@TmpNc", dt);
                cmd.Parameters.AddWithValue("@usu_ing", _usuing);
                cmd.Parameters.AddWithValue("@not_estado_nc", _codigo_estado);
                cmd.Parameters["@not_id"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                results[0] = Convert.ToString(cmd.Parameters["@not_id"].Value);
                return results;
            }
            catch
            {
                return null;
                //        connection.Close();
                //        return null;
            }
        }


        public static String[] addReturnOrder(String RHV_CO,
         String RHN_COORDINATOR, String RHN_EMPLOYEE, String STV_WAREHOUSE, List<Returns_Dtl> listArticlesReturned)
        {
            String[] results = new String[3];
            return results;
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //String resultDoc = "";
            //String storageDevol = "";
            //String noTransaccion = "";

            ///// Nombre del procedimiento
            //String sqlCommand = "ventas.SP_ADD_RETURNSHDR";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "P_RHV_CO", DbType.String, RHV_CO);
            /////
            /////db.AddInParameter(dbCommandWrapper, "P_RHN_PERSON", DbType.Decimal, RHN_PERSON);
            /////
            //db.AddInParameter(dbCommandWrapper, "P_RHN_COORDINATOR", DbType.String, RHN_COORDINATOR);
            /////
            //db.AddInParameter(dbCommandWrapper, "P_RHN_EMPLOYEE", DbType.Decimal, RHN_EMPLOYEE);
            /////
            //db.AddInParameter(dbCommandWrapper, "P_STV_WAREHOUSE", DbType.String, STV_WAREHOUSE);

            //// Output parameters specify the size of the return data.            
            //db.AddOutParameter(dbCommandWrapper, "P_RHV_RETURN_NO", DbType.String, 12);
            //// Storage a donde se enviaron los articulos devueltos
            //db.AddOutParameter(dbCommandWrapper, "P_STORAGE_DEVOL", DbType.String, 12);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //        /// Recuperar el parametro de salida
            //        resultDoc = (String)db.GetParameterValue(dbCommandWrapper, "P_RHV_RETURN_NO");
            //        storageDevol = (String)db.GetParameterValue(dbCommandWrapper, "P_STORAGE_DEVOL");

            //        // Recorrer todas las lineas adicionadas al detalle
            //        foreach (Returns_Dtl item in listArticlesReturned)
            //        {
            //            /// Procedimiento que adiciona las lineas de detalle
            //            sqlCommand = "ventas.sp_add_returnsdtl";

            //            ///
            //            if (item._RDV_STORAGE.Equals(""))
            //            {
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                    RHV_CO,
            //                    resultDoc,
            //                    item._RDV_INVOICE,
            //                    item._RDV_ARTICLE,
            //                    item._RDV_SIZE,
            //                    item._RDN_QTY);
            //            }
            //            else
            //            {
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                RHV_CO,
            //                resultDoc,
            //                item._RDV_INVOICE,
            //                item._RDV_ARTICLE,
            //                item._RDV_SIZE,
            //                item._RDN_QTY);
            //            }
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        }

            //        // Commit the transaction.
            //        transaction.Commit();

            //        String[] results = new String[3];
            //        results[0] = resultDoc;
            //        results[1] = noTransaccion;
            //        results[2] = storageDevol;

            //        connection.Close();
            //        return results;

            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        resultDoc = "0";
            //        connection.Close();
            //        return null;
            //    }
            //}
        }

        /// <summary>
        /// Consulta de cabecera de devolucion
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_noReturn"></param>
        /// <returns></returns>
        /// 
        public static string ejecuta_duplica_nc(decimal _bas_id, decimal _not_numero)
        {
            string sqlquery = "USP_Insertar_CopiaNC";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@not_usu_mod", _bas_id);
                cmd.Parameters.AddWithValue("@not_numero", _not_numero);
                cmd.Parameters.Add("@not_id", SqlDbType.VarChar, 12);
                cmd.Parameters["@not_id"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                return cmd.Parameters["@not_id"].Value.ToString();
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getRetunrHdr_electronica(string _noReturn)
        {
            string sqlquery = "USP_Leer_NotaCreditoReporte_Electronica";
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
                cmd.Parameters.AddWithValue("@not_id", _noReturn);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getRetunrHdr(string _noReturn)
        {
            string sqlquery = "USP_Leer_NotaCreditoReporte";
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
                cmd.Parameters.AddWithValue("@not_id", _noReturn);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataTable getRetunrHdr_Cancel(string _company, string _noReturn, string _noNota)
        {
            DataTable ds = new DataTable();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "ventas.sp_getreturnhdr_Cancel";

            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _noReturn, _noNota, results);

            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consultar cabecera de devolucion en espera de aprobacion
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_noReturn"></param>
        /// <returns></returns>
        public static DataTable getRetunrHdrDea(string _company, string _noReturn)
        {
            DataTable ds = new DataTable();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "ventas.sp_getreturnhdr_dea";

            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _noReturn, results);

            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_customer"></param>
        /// <returns></returns>
        public static DataTable getReturnsByCoord(String _company, String _customer)
        {
            DataTable ds = new DataTable();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "ventas.getreturnsbycoord";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _customer, results);
            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //}
            //catch
            //{
            //    ///
            //    return null;
            //}
        }

        /// <summary>
        /// Consulta de devoluciones por fecha
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <param name="_ware"></param>
        /// <param name="_area"></param>
        /// <returns></returns>
        public static DataSet getReturnsByDate(string _startDate, string _endDate)
        {
            string sqlquery = "USP_Leer_NotaCredito";
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
                cmd.Parameters.AddWithValue("@fechaini", _startDate);
                cmd.Parameters.AddWithValue("@fechafin", _endDate);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;

            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static void sbeanularnc(string varnumcred, Int32 _usu_mod)
        {
            string sqlquery = "USP_Anular_NotaCredito";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@not_numero", varnumcred);
                cmd.Parameters.AddWithValue("@not_usu_mod", _usu_mod);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }

        }
        public static int FValidaNC(string varnumcred)
        {
            //return 0;
            int _varretorno = 0;
            string sqlquery = "USP_Valica_NCFinanzas";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@not_numero", varnumcred);
                cmd.Parameters.AddWithValue("@valida", DbType.Int32);
                cmd.Parameters["@valida"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _varretorno = Convert.ToInt32(cmd.Parameters["@valida"].Value);
                return _varretorno;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        #endregion
    }
}

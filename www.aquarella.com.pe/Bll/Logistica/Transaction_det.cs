using System;
using System.Data.Common;
using System.Collections.Generic;
using www.aquarella.com.pe.bll.Util;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace www.aquarella.com.pe.bll.Logistica
{
    public class Transaction_det
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        /// <summary>
        /// Numero de transaccion tx
        /// </summary>
        public string _idTx { get; set; }
        /// <summary>
        /// Codigo de item
        /// </summary>
        public string _item { get; set; }
        /// <summary>
        /// Nombre de item
        /// </summary>
        public string _article { get; set; }
        /// <summary>
        /// Marca
        /// </summary>
        public string _brand { get; set; }
        /// <summary>
        /// Unidades
        /// </summary>
        public int _units { get; set; }
        /// <summary>
        /// Talla
        /// </summary>
        public string _size { get; set; }
        /// <summary>
        /// Fecha de la transaccion
        /// </summary>
        public DateTime _dateTx { get; set; }
        /// <summary>
        /// Bodega de origen
        /// </summary>
        public string _wareOrig { get; set; }

        #endregion

        #region < VARIABLES >
        private int _tdn_line_no = 0;
        private string _tdv_article = "";
        private string _tdv_size = "";
        private int _tdn_qty = 0;
        private decimal _tdn_odv = 0;
        private decimal _tdn_public_price = 0;
        #endregion

        #region < PROPIEDADES >
        public int tdn_Line_no
        {
            get { return _tdn_line_no; }
            set { _tdn_line_no = value; }
        }

        public string tdv_Article
        {
            get { return _tdv_article; }
            set { _tdv_article = value; }
        }

        public string tdv_Size
        {
            get { return _tdv_size; }
            set { _tdv_size = value; }
        }

        public int tdn_Qty
        {
            get { return _tdn_qty; }
            set { _tdn_qty = value; }
        }

        public decimal tdn_Odv
        {
            get { return _tdn_odv; }
            set { _tdn_odv = value; }
        }

        public decimal tdn_Public_Price
        {
            get { return _tdn_public_price; }
            set { _tdn_public_price = value; }
        }

        public decimal Total_lOdv
        {
            get { return _tdn_qty * _tdn_odv; }
        }

        public decimal Total_lPublic_Price
        {
            get { return _tdn_qty * _tdn_public_price; }
        }
        #endregion

        #region < CONSTRUCTORES >
        public Transaction_det()
        { }

        public Transaction_det(int tdn_line_no, string tdv_article, string tdv_size,
                int tdn_qty, decimal tdn_odv, decimal tdn_public_price)
        {
            _tdn_line_no = tdn_line_no;
            _tdv_article = tdv_article;
            _tdv_size = tdv_size;
            _tdn_qty = tdn_qty;
            _tdn_odv = tdn_odv;
            _tdn_public_price = tdn_public_price;
        }
        #endregion

        #region < METODOS STATICOS >

        /// <summary>
        /// Consultar los resumenes de movimientos CARDEX
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        //public static DataTable getResumeTransactions(String _company, String _startDate, String _endDate, String _warehouse)
        public static DataSet getResumeTransactions(String _company, DateTime _startDate, DateTime _endDate, String _warehouse)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "logistica.sp_getresumetransactions";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _startDate, _endDate, _warehouse, results);
            //    //DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_storageOut"></param>
        /// <param name="_storageIn"></param>
        /// <param name="lstAdjustOut"></param>
        /// <param name="lstAdjustIn"></param>
        /// <returns></returns>
        public static String[] saveAdjust(String _company, String _storageOut, String _storageIn,
            List<Transaction_det> lstAdjustOut, List<Transaction_det> lstAdjustIn)
        {
            String[] results = new String[2];
            return results;
            //Database db = DatabaseFactory.CreateDatabase(_conn);


            ///// Documento de salida
            //String idDocumentOut = "";
            ///// Documento de entrAQUARELLA
            //String idDocumentIn = "";


            ////String noTransaccion = "";

            ///// Nombre del procedimiento
            //String sqlCommand = "logistica.SP_ADD_ADJUSTMENT";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_trv_co", DbType.String, _company);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_storage_out", DbType.String, _storageOut);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_storage_in", DbType.Decimal, _storageIn);
            /////

            ///// Output parameters specify the size of the return data.            
            //db.AddOutParameter(dbCommandWrapper, "p_trv_document_out", DbType.String, 12);
            /////
            //db.AddOutParameter(dbCommandWrapper, "p_trv_document_in", DbType.String, 12);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    /// Inicio de transaccion
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        ///
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //        /// Recuperar el id documento de salida
            //        idDocumentOut = (String)db.GetParameterValue(dbCommandWrapper, "p_trv_document_out");

            //        /// Recuperar el id documento de entrAQUARELLA
            //        idDocumentIn = (String)db.GetParameterValue(dbCommandWrapper, "p_trv_document_in");

            //        // Recorrer todas las lineas adicionAQUARELLAs al detalle de la transaccion de salida
            //        foreach (Transaction_det item in lstAdjustOut)
            //        {
            //            /// Procedimiento que adiciona las lineas de detalle
            //            sqlCommand = "logistica.SP_ADD_TRANSDETAILS";

            //            ///
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                _company,
            //                idDocumentOut,
            //                item._tdv_article,
            //                item._tdv_size,
            //                item._tdn_qty);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        }


            //        ///////////////////////////////////////////////////////////////////////////////////////////
            //        /// Procedimiento que adiciona las lineas de detalle
            //        /*sqlCommand = "ventas.SP_ADD_DOCTRANS_RETURNS";
            //        ///
            //        dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //            RHV_CO,
            //            resultDoc,12);
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);///
            //        /// Recuperar el parametro de salida
            //        noTransaccion = (String)db.GetParameterValue(dbCommandWrapper, "P_DTV_TRANSDOC");*/
            //        // Recorrer todas las lineas adicionAQUARELLAs al detalle de la transaccion de salida
            //        foreach (Transaction_det itemIn in lstAdjustIn)
            //        {
            //            /// Procedimiento que adiciona las lineas de detalle
            //            sqlCommand = "logistica.SP_ADD_TRANSDETAILS";

            //            ///
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                _company,
            //                idDocumentIn,
            //                itemIn._tdv_article,
            //                itemIn._tdv_size,
            //                itemIn._tdn_qty);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        }
            //        ///////////////////////////////////////////////////////////////////////////////////////////

            //        // Commit the transaction.
            //        transaction.Commit();

            //        String[] results = new String[2];
            //        results[0] = idDocumentOut;
            //        results[1] = idDocumentIn;

            //        return results;

            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        connection.Close();
            //        return null;
            //    }
            //}
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_storage"></param>
        /// <param name="_concept"></param>
        /// <param name="_status"></param>
        /// <param name="lstTransacDetail"></param>
        /// <returns></returns>
        /// 

        public static string grabar_articulo(DataTable dt)
        {
            string sqlquery="USP_ActualizarArticulo_Bata";
            string valor = "";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Tabla_Articulo", dt);
                cmd.ExecuteNonQuery();                
            }
            catch (Exception exc)
            {
                valor = exc.Message;
            }
            return valor;
        }
        public static string actualza_stock(DataTable dt_stock)
        {
            string validar = "";
            string sqlquery = "USP_Cuadrar_Stock_Aquarella";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            //SqlDataAdapter da = null;
            //DataSet ds = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tmp_Stock_Bata", dt_stock);
                //da = new SqlDataAdapter(cmd);
                //ds = new DataSet();
                //da.Fill(ds);

                cmd.ExecuteNonQuery();

            }
            catch (Exception exc)
            {
                validar = exc.Message;
            }
            return validar;
        }

        public static string ejecuta_transferencia(DataTable dt_mov, ref DataTable dt_venta)
        {
            string validar = "";
            string sqlquery = "USP_ImporT_ExportAQBata";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                //if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tabla_mov", dt_mov);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                dt_venta = dt;
                //cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                validar = exc.Message;
                dt_venta = null;
            }
            return validar;
        }
        public static String saveTransaction(DataTable dt_mov)
        {
            string sqlquery = "USP_Actualizar_Archivo_Bata";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tabla_mov", dt_mov);
                cmd.ExecuteNonQuery();
                return "1";
            }
            catch
            {
                return null;
            }
            //return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);

            /////
            //String noTransaccion = "";

            ///// Nombre del procedimiento
            //String sqlCommand = "logistica.SP_ADD_TRANSACTIONS";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_trv_co", DbType.String, _company);
            ///// Output parameters specify the size of the return data.            
            //db.AddOutParameter(dbCommandWrapper, "p_trv_document", DbType.String, 12);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_trv_storage", DbType.String, _storage);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_trv_concept", DbType.String, _concept);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_trv_status", DbType.String, _status);

            /////
            //db.AddInParameter(dbCommandWrapper, "p_trv_docreferencia", DbType.String, 12);

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    /// Inicio de transaccion
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        ///
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        // Commit the transaction.
            //        // transaction.Commit();
            //        /// Recuperar el id documento de salida
            //        noTransaccion = (String)db.GetParameterValue(dbCommandWrapper, "p_trv_document");
            //        // transaction.Commit();
            //        // Recorrer todas las lineas adicionAQUARELLAs al detalle de la transaccion de salida
            //        foreach (Transaction_det item in lstTransacDetail)
            //        {

            //            //en esta transaccion verificamos que la talla este correcta

            //            sqlCommand = "MAESTROS.USP_ACTUALIZASIZE";
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //            item._tdv_article,
            //            item._tdv_size);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            //

            //            /// Procedimiento que adiciona las lineas de detalle
            //            sqlCommand = "logistica.SP_ADD_TRANSDETAILS";

            //            ///
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                _company,
            //                noTransaccion,
            //                item._tdv_article,
            //                item._tdv_size,
            //                item._tdn_qty);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        }


            //        // Commit the transaction.
            //        transaction.Commit();

            //        ///
            //        return noTransaccion;

            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        connection.Close();
            //        return null;
            //    }
            //}
        }

        /// <summary>
        /// Calculo de existencias del stock
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_warehouse"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataSet getStockCalculation(String _company, String _warehouse, DateTime _startDate, DateTime _endDate)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "LOGISTICA.sp_getStockCalculation";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _warehouse, _startDate, _endDate, results);
            //    //DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch
            //{
            //    return null;
            //}
        }


        #endregion

        //internal static string saveTransaction(string p, string p_2, string p_3, string p_4, Transaction_det newline)
        //{
        //    throw new NotImplementedException();
        //}

        internal static string saveTransaction(string p, string p_2, string p_3, string p_4, Transaction_det newline)
        {
            throw new NotImplementedException();
        }
    }
}
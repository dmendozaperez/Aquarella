using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;



namespace www.aquarella.com.pe.bll
{
    public class Liquidations_Hdr
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>


        #endregion

        #region < Metodos estaticos >

        public static DataSet getconsultapedidoven(int _area_id, String _asesor, DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_ConsultaPedidovencidos";
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
                cmd.Parameters.AddWithValue("@lider", _area_id);
                cmd.Parameters.AddWithValue("@fechaini", _date_start);
                cmd.Parameters.AddWithValue("@fechafin", _date_end);
                cmd.Parameters.AddWithValue("@asesor", _asesor);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                return ds;
                //throw new Exception(e.Message, e.InnerException);
            }
        }


        public static DataTable get_pedido_lidergrupo()
        {
            string sqlquery = "USP_Reporte_Pedido_Lider_Grupo";
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
            }
            catch
            {
                dt = null;
            }
            return dt;
        }
        public static DataSet getpedidosvencidos(string var_lhn_customer)
        {
            //DataSet ds = new DataSet();
            //return ds;
            string sqlquery = "USP_Leer_PedidosVencidos";
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
                cmd.Parameters.AddWithValue("@bas_id", Convert.ToDecimal(var_lhn_customer));
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }

            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static void sbresliqvenc(string var_liqui,ref Decimal _valor)
        {
            string sqlquery = "USP_Modificar_Liq_Vencidos";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", var_liqui);
                cmd.Parameters.Add("@valor", SqlDbType.Decimal);
                cmd.Parameters["@valor"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _valor = Convert.ToDecimal(cmd.Parameters["@valor"].Value);
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Separar pedidos borrador y generar liquidacion
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_ordersChain">Pedidos en borrador: 1,2 etc</param>
        /// <returns></returns>
        public static string[] createLiquidation(string _co, string _ordersChain, Transporters_Guides shipping, string _newStatus)
        {
            string[] resultDoc = new string[2];
            return resultDoc;
            //string sqlCommand = "logistica.sp_reserve_stock_order_list";
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
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ordersChain);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);


            //            if (!string.IsNullOrEmpty(_newStatus))
            //            {
            //                sqlCommand = "logistica.sp_liquidation_for_hall";//"logistica.sp_updateStatusLiquidation";
            //                //
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ordersChain, _newStatus, 12);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //                //
            //                resultDoc[0] = db.GetParameterValue(dbCommandWrapper, "p_lhv_liquidation_no").ToString();
            //            }
            //            else
            //            {
            //                // Crear Liquidacion
            //                sqlCommand = "logistica.sp_liquidation";

            //                //
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ordersChain, 12);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //                //
            //                resultDoc[0] = db.GetParameterValue(dbCommandWrapper, "p_lhv_liquidation_no").ToString();
            //            }

            //            /// Quitar cuando el sistema funcione con los cambios en la tabla transporters_guides
            //            if (shipping._configShipping)
            //            {
            //                /* Solo para cuando este activo la nueva creacion de guias*/
            //                // Registro info destinatario
            //                sqlCommand = "logistica.sp_addguide_shipping";
            //                //
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, 12, DBNull.Value, shipping._tgn_transport, shipping._tgv_name_cust, shipping._tgv_phone_cust,
            //                    shipping._tgv_movil_cust, shipping._tgv_shipp_add, shipping._tgv_shipp_block, shipping._tgv_city, shipping._tgv_depto, resultDoc[0]);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            }
            //            // Commit the transaction.
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            // Roll back the transaction. 
            //            transaction.Rollback();
            //            resultDoc[0] = "-1";
            //            resultDoc[1] = ex.Message;//"-1";
            //            //return ex.Message;
            //        }
            //        connection.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    resultDoc[0] = "-1";
            //    resultDoc[1] = ex.Message; ;
            //}
            //return resultDoc;
        }

        /// <summary>
        /// Consulta de liquidacion e informacion adicional
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_noLiq"></param>
        /// <returns></returns>
        public static DataSet getLiquidationHdrInfo(string _noLiq)
        {            
            string sqlquery = "USP_Leer_Liquidacion_Reporte";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da=null;
            DataSet ds= null;
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
        public static DataSet getpagoncreditoliqui(string _noliq)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "LOGISTICA.USP_LiquiPagoNC";
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _noliq, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataSet getpagonformaliqui(string _noliq)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "LOGISTICA.USP_LiquiPagoForma";
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _noliq, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consulta de liquidacion pra pagos Online
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idLiq"></param>
        /// <returns></returns>
        public static DataTable getInfoLiquiForPaysOnline(string _co, string _idLiq)
        {
            DataTable ds = new DataTable();
            return ds;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "logistica.sp_get_liquidation_info";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _idLiq, results);
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    return dtResult;
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Actualizar estado de la cabecera de liquidacion
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_noLiquidation"></param>
        /// <param name="_newStatus"></param>
        /// <returns></returns>
        public static string updateStatusLiquidation(string _co, string _noLiquidation, string _newStatus)
        {
            return "";
            //try
            //{
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "logistica.sp_updateStatusLiquidation";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _newStatus, _noLiquidation);
            //    ///
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    ///
            //    return "1";
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message, e.InnerException);
            //}
        }

        /// <summary>
        /// Consulta de liquidaciones para marcar
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_ware"></param>
        /// <param name="_area"></param>
        /// <returns></returns>
        /// 

        public static DataSet getLiquidationPicking_marcacion()
        {
            //DataSet ds = new DataSet();
            //return ds;
            string sqlquery = "USP_Leer_LiqEmp_Marcacion";
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

        public static DataSet getLiquidationPicking()
        {
            //DataSet ds = new DataSet();
            //return ds;
            string sqlquery = "USP_Leer_LiqEmp";
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

        /// <summary>
        /// Consultar liquidaciones en estado activo en bodega, osea liquidaciones separAQUARELLAs, en marcacion o para facturacion.
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_status"></param>
        /// <param name="_ware"></param>
        /// <param name="_area"></param>
        /// <returns></returns>
        public static DataSet getLiqActives(string _area)
        {
            string sqlquery = "USP_Leer_Liquidacion_Activos";
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
                cmd.Parameters.AddWithValue("@are_id", _area);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Liquidaciones con finalizacion de marcacion y para entregar en hall
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_ware"></param>
        /// <param name="_area"></param>
        /// <returns></returns>
        public static DataSet getLiquidationForHall(string _co, string _ware, string _area)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "logistica.sp_getliquid_forhall";
            //    // CURSOR REF
            //    object results = new object[1];
            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///                
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ware, _area, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /*
        public static void paralellism()
        {
            // Use an event to wait for the children
            using (var mre = new ManualResetEvent(false))
            {
                int count = 2;

                // Process the left child asynchronously
                ThreadPool.QueueUserWorkItem(delegate
                {
                    //Process(tree.Left, action);
                    insert(count.ToString() + " task1 Async" + DateTime.Now.ToLongTimeString());                    
                    if (Interlocked.Decrement(ref count) == 0)
                        mre.Set();
                });                
                // Process the right child asynchronously
                ThreadPool.QueueUserWorkItem(delegate
                {
                    //Process(tree.Right, action);
                    insert(count.ToString() + " task2 Async" + DateTime.Now.ToLongTimeString());
                    if (Interlocked.Decrement(ref count) == 0)
                        mre.Set();
                });

                // Process the current node synchronously
                //action(tree.Data);

                // Wait for the children
                mre.WaitOne();
            }

            insert("task1 Sync" + DateTime.Now.ToLongTimeString());
            insert("task2 Sync" + DateTime.Now.ToLongTimeString());

        }


        public static void insert(string i)
        {            
            try
            {
                Database db = DatabaseFactory.CreateDatabase(_conn);
                ///
                String sqlCommand = "ventas.sp_insert_curso";
                DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, i);
                db.ExecuteNonQuery(dbCommandWrapper);
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        */


        /// <summary>
        /// Consulta de liquidaciones separAQUARELLAs
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_idWarehouse"></param>
        /// <param name="_area"></param>
        /// <returns></returns>
        public static DataSet getSeparateLiquidations(string _area)
        {
            //DataSet ds = new DataSet();
            //return ds;
            string sqlquery = "USP_Leer_Liquidacion_Separados";
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
                cmd.Parameters.AddWithValue("@are_id", _area);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consultar estado de pedidos, por liquidacion o numero de guia
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_liqGuide"></param>
        /// <returns></returns>
        public static DataSet getLiquidations(string _liqGuide)
        {
            string sqlquery = "USP_Leer_Liq_Guia";
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
                cmd.Parameters.AddWithValue("@liq_id", _liqGuide);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public static string updateExpirationDateOnLiq(string _noLiquidation, decimal _idUser)
        {
            //return "";
            string sqlquery = "USP_Modificar_ExpiracionLiq";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_mod", _idUser);
                cmd.Parameters.AddWithValue("@lid_id", _noLiquidation);
                cmd.ExecuteNonQuery();
                return "1";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public static void  sbanularliquidacion(string varliq)
        {
            string sqlquery = "USP_Anular_Liquidacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Liq_Id", varliq);
                cmd.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public static void sbanularpedido(string varped,decimal user)
        {
            string sqlquery = "USP_Anular_Pedido";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ped_Id", varped);
                cmd.Parameters.AddWithValue("@Usu", user);
                cmd.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
        public static DataSet get_Liquidations_separated(string _company, string _search)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];
            //    string _status = Util.ValuesDB.acronymStatusOrdersSeparated;
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "logistica.sp_liquidations_separated";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _search, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Cambio de estado de una liquidacion, para recoleccion presencial
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_noLiquidation"></param>
        /// <param name="_newStatus"></param>
        /// <returns></returns>
        public static string updateStatusLiquidationInCedi(string _company, string _noLiquidation, string _newStatus)
        {
            return "";
            //try
            //{
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "logistica.sp_updatestatusliqui_inCEDI";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _noLiquidation, _newStatus);
            //    ///
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    ///
            //    return "1";
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message, e.InnerException);
            //}
        }

        public static DataSet get_liquidations_Hall(String _company, DateTime _startDate, DateTime _endDate)
        {
            DataSet ds = new DataSet();
            return ds;
            ///
            //DataSet dsResult = new DataSet();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];
            //    object results1 = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "logistica.sp_get_liquidations_hall";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _startDate, _endDate, results, results1);
            //    ///
            //    dsResult = db.ExecuteDataSet(dbCommandWrapper);
            //    ///
            //    return dsResult;
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static string cancel_Liquidation_Hall(String _company, String _noLiquidation)
        {
            return "";
            //try
            //{
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "logistica.sp_cancel_liquidation_inhall";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _noLiquidation);
            //    ///
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    ///
            //    return "1";
            //}
            //catch (Exception e)
            //{
            //    throw new Exception(e.Message, e.InnerException);
            //}
        }

        /// <summary>
        /// Crear una liquidacion Nueva version octubre 2012
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_ordersChain"></param>
        /// <param name="shipping"></param>
        /// <param name="_newStatus"></param>
        /// <returns></returns>
        /// 

        public static string[] Gua_Mod_Liquidacion(decimal _usu, decimal _idCust, string _reference, decimal _discCommPctg,
                                               decimal _discCommValue, string _shipTo, string _specialInstr, List<Order_Dtl> _itemsDetail,
                                               decimal _varpercepcion, Int32 _estado, string _ped_id = "",string _liq="",Int32 _liq_dir=0,
                                               Int32 _PagPos=0,string _PagoPostarjeta="",string _PagoNumConsignacion="",decimal _PagoTotal=0,DataTable dtpago=null,Boolean _pago_credito=false,Decimal _porc_percepcion=0,List<Order_Dtl_Temp> order_dtl_temp=null)
        {
            string[] resultDoc = new string[2];
            string sqlquery = "USP_Insertar_Modifica_Liquidacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Ped_Det_Id", typeof(string));
                dt.Columns.Add("Ped_Det_Items", typeof(Int32));
                dt.Columns.Add("Ped_Det_ArtId", typeof(string));
                dt.Columns.Add("Ped_Det_TalId", typeof(string));
                dt.Columns.Add("Ped_Det_Cantidad", typeof(Int32));
                dt.Columns.Add("Ped_Det_Costo", typeof(decimal));
                dt.Columns.Add("Ped_Det_Precio", typeof(decimal));
                dt.Columns.Add("Ped_Det_ComisionP", typeof(decimal));
                dt.Columns.Add("Ped_Det_ComisionM", typeof(decimal));

                dt.Columns.Add("Ped_Det_OfertaP", typeof(decimal));
                dt.Columns.Add("Ped_Det_OfertaM", typeof(decimal));
                dt.Columns.Add("Ped_Det_OfeID", typeof(decimal));

                int i = 1;
                // Recorrer todas las lineas adicionAQUARELLAs al detalle

                if (_itemsDetail!=null)
                {
                    foreach (Order_Dtl item in _itemsDetail)
                    {
                        dt.Rows.Add(_ped_id, i, item._code, item._size, item._qty, 0, item._price, item._commissionPctg, Math.Round(item._commission,2,MidpointRounding.AwayFromZero),item._ofe_porc,item._dscto,item._ofe_id);
                        i++;
                    }
                }

                /*pedido original*/
                DataTable dtordertmp = new DataTable();
                dtordertmp.Columns.Add("items", typeof(Int32));
                dtordertmp.Columns.Add("articulo", typeof(string));
                dtordertmp.Columns.Add("talla", typeof(string));
                dtordertmp.Columns.Add("cantidad", typeof(Int32));
                



                if (order_dtl_temp!=null)
                {
                    foreach(Order_Dtl_Temp item in order_dtl_temp)
                    {
                        dtordertmp.Rows.Add(item.items, item.articulo, item.talla, item.cantidad);
                    }
                }


                //grabar pedido
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Estado", _estado);
                cmd.Parameters.AddWithValue("@Ped_Id", _ped_id);
                //cmd.Parameters.AddWithValue("@LiqId", _liq);
                cmd.Parameters.Add("@LiqId", SqlDbType.VarChar, 12);
                cmd.Parameters["@LiqId"].Value = _liq;
                cmd.Parameters["@LiqId"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.AddWithValue("@Liq_BasId", _idCust);
                cmd.Parameters.AddWithValue("@Liq_ComisionP", _discCommPctg);
                cmd.Parameters.AddWithValue("@Liq_PercepcionM", _varpercepcion);
                cmd.Parameters.AddWithValue("@Liq_Usu", _usu);
                cmd.Parameters.AddWithValue("@Detalle_Pedido", dt);
                cmd.Parameters.AddWithValue("@Liquidacion_Directa", _liq_dir);

                /*PEDIDO ORIGINAL*/
                cmd.Parameters.AddWithValue("@pedido_original", dtordertmp);

                //opcional pago por pos liquidacion directa
                cmd.Parameters.AddWithValue("@Pago_Pos", _PagPos);
                cmd.Parameters.AddWithValue("@Pago_PosTarjeta", _PagoPostarjeta);
                cmd.Parameters.AddWithValue("@Pago_numconsigacion", _PagoNumConsignacion);
                cmd.Parameters.AddWithValue("@Pago_Total", _PagoTotal);

                
                //pago directo de la liquidacion
                cmd.Parameters.AddWithValue("@DetallePago", dtpago);
                cmd.Parameters.AddWithValue("@Pago_Credito", _pago_credito);

                //porcentaje percepcion
                cmd.Parameters.AddWithValue("@Ped_Por_Perc", _porc_percepcion);
                //da = new SqlDataAdapter(cmd);
                //da.Fill(ds);

                cmd.ExecuteNonQuery();
                resultDoc[0] =cmd.Parameters["@LiqId"].Value.ToString();
            }
            catch (Exception ex)
            {
                if (cn != null)
                    if (cn.State == ConnectionState.Open) cn.Close();
                resultDoc[0] = "-1";
                resultDoc[1] = ex.Message;
            }
            if (cn != null)
                if (cn.State == ConnectionState.Open) cn.Close();
            return resultDoc;
        }

        public static void enviar_correos(string _liq)
        {
            string sqlquery = "USP_Envia_Correo_Liquidacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _liq);
                cmd.ExecuteNonQuery();

            }
            catch
            {
            }
        }

        public static string[] liquidation(string _co, string _ordersChain, Transporters_Guides shipping, string _newStatus, Decimal _varpercepcion)
        {
            string[] resultDoc = new string[2];
            return resultDoc;
            //string sqlCommand = "logistica.sp_createliquidation";
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
            //            /*PARAMETERS*/
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ordersChain, 0, _newStatus, _varpercepcion);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            resultDoc[0] = db.GetParameterValue(dbCommandWrapper, "p_lhv_liquidation_no").ToString();

            //            //** Quitar cuando el sistema funcione con los cambios en la tabla transporters_guides
            //            if (shipping._configShipping)
            //            {
            //                /* Solo para cuando este activo la nueva creacion de guias*/
            //                // Registro info destinatario
            //                sqlCommand = "logistica.sp_addguide_shipping";
            //                //
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, 12, DBNull.Value, shipping._tgn_transport, shipping._tgv_name_cust, shipping._tgv_phone_cust,
            //                    shipping._tgv_movil_cust, shipping._tgv_shipp_add, shipping._tgv_shipp_block, shipping._tgv_city, shipping._tgv_depto, resultDoc[0]);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            }
            //            // Commit the transaction.
            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            // Roll back the transaction. 
            //            transaction.Rollback();
            //            resultDoc[0] = "-1";
            //            resultDoc[1] = ex.Message;//"-1";
            //            //return ex.Message;
            //        }
            //        connection.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    resultDoc[0] = "-1";
            //    resultDoc[1] = ex.Message;
            //}
            //return resultDoc;
        }
        public static string[] modyliquidation(string _co, string _ordersChain, Transporters_Guides shipping, string _newStatus, Decimal _varpercepcion, int pagopos = 0, string varNumTarjeta="",
            string varNumVoucher="", decimal varMonto=0, decimal usuCre=0, String TipoPago="", string listdoc="")
        {
            string[] resultDoc = new string[2];
            return resultDoc;
            //string sqlCommand = "LOGISTICA.USP_modifyliquidation";
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
            //            /*PARAMETERS*/
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ordersChain, 0, _newStatus, _varpercepcion);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            resultDoc[0] = db.GetParameterValue(dbCommandWrapper, "p_lhv_liquidation_no").ToString();

            //            //** Quitar cuando el sistema funcione con los cambios en la tabla transporters_guides
            //            if (shipping._configShipping)
            //            {
            //                /* Solo para cuando este activo la nueva creacion de guias*/
            //                // Registro info destinatario
            //                sqlCommand = "logistica.sp_addguide_shipping";
            //                //
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, 12, DBNull.Value, shipping._tgn_transport, shipping._tgv_name_cust, shipping._tgv_phone_cust,
            //                    shipping._tgv_movil_cust, shipping._tgv_shipp_add, shipping._tgv_shipp_block, shipping._tgv_city, shipping._tgv_depto, resultDoc[0]);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            }
            //            // Commit the transaction.


            //            if (pagopos > 0)
            //            {
            //                //-- liquidation
            //                sqlCommand = "FINANCIERA.USP_POS_clear_UNICAMOD";
            //                dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ordersChain, 0, _newStatus, varNumTarjeta, varNumVoucher, varMonto, TipoPago, usuCre, _varpercepcion, listdoc);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //                //resultDoc[1] = db.GetParameterValue(dbCommandWrapper, "p_lhv_liquidation_no").ToString();

            //                if (shipping._configShipping)
            //                {
            //                    /* Solo para cuando este activo la nueva creacion de guias*/
            //                    // Registro info destinatario
            //                    sqlCommand = "logistica.sp_addguide_shipping";
            //                    //
            //                    dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, 12, DBNull.Value, shipping._tgn_transport, shipping._tgv_name_cust, shipping._tgv_phone_cust,
            //                        shipping._tgv_movil_cust, shipping._tgv_shipp_add, shipping._tgv_shipp_block, shipping._tgv_city, shipping._tgv_depto, resultDoc[0]);
            //                    db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //                }
            //                transaction.Commit();
            //                //** Quitar cuando el sistema funcione con los cambios en la tabla transporters_guides
            //                //if (shipping._configShipping)
            //                //{
            //                //    /* Solo para cuando este activo la nueva creacion de guias*/
            //                //    // Registro info destinatario
            //                //    sqlCommand = "logistica.sp_addguide_shipping";
            //                //    //
            //                //    dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, 12, DBNull.Value, shipping._tgn_transport, shipping._tgv_name_cust, shipping._tgv_phone_cust,
            //                //        shipping._tgv_movil_cust, shipping._tgv_shipp_add, shipping._tgv_shipp_block, shipping._tgv_city, shipping._tgv_depto, resultDoc[1]);
            //                //    db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //                //}

            //            }
            //            else
            //            {
            //                transaction.Commit();
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            // Roll back the transaction. 
            //            transaction.Rollback();
            //            resultDoc[0] = "-1";
            //            resultDoc[1] = ex.Message;//"-1";
            //            //return ex.Message;
            //        }
            //        connection.Close();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    resultDoc[0] = "-1";
            //    resultDoc[1] = ex.Message;
            //}
            //return resultDoc;
        }

        public static Boolean fvalidastockpedido(string pedido, ref string articulo, ref string talla)
        {
            //return false;
            Boolean vdevolvercadena = true;
            string sqlquery = "USP_VerificaStockLiquidacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {

                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ped_Id", pedido);
                cmd.Parameters.Add("@Art_Id",SqlDbType.VarChar,12);
                cmd.Parameters.Add("@Tal_Id", SqlDbType.VarChar,3);
                cmd.Parameters["@Art_Id"].Direction = ParameterDirection.Output;               
                cmd.Parameters["@Tal_Id"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                articulo = Convert.ToString(cmd.Parameters["@Art_Id"].Value);
                talla = Convert.ToString(cmd.Parameters["@Tal_Id"].Value);
              
                if (articulo.Length > 0)
                {
                    vdevolvercadena = false;
                }

                return vdevolvercadena;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataTable get_montoliqnc(string _nroliq)
        {
            DataTable ds = new DataTable();
            return ds;
            //try
            //{
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "LOGISTICA.USP_GetMontoLiqNc";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _nroliq, results);
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];

            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion
    }
}
using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;


namespace www.aquarella.com.pe.bll.Ventas
{
    public class Facturacion
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < MÉTODOS ESTATICOS >

        /// <summary>
        /// Generacion de factura.
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_noLiquidation"></param>
        /// <param name="_pointOfSale"></param>
        /// <returns></returns>
        /// 
        public static DataTable _Consulta_Lider_N(DateTime _fechaini,DateTime _fechafin)
        {
            string sqlquery = "USP_Consulta_Venta_LiderN";
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
                cmd.Parameters.AddWithValue("@fecha_ini", _fechaini);
                cmd.Parameters.AddWithValue("@fecha_fin", _fechafin);
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
        public static string generarFactura(String _company, String _noLiquidation, Decimal _pointOfSale)
        {
            return "";
            ///
            //try
            //{
            //    String _idFactura = "";
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.SP_INVOICING";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);
            //    ///
            //    db.AddInParameter(dbCommandWrapper, "P_IHV_CO", DbType.String, _company);
            //    ///
            //    db.AddInParameter(dbCommandWrapper, "P_IHV_LIQUIDATION", DbType.String, _noLiquidation);
            //    ///
            //    db.AddInParameter(dbCommandWrapper, "P_IHN_POINTSALE", DbType.Decimal, _pointOfSale);

            //    /// Output parameters specify the size of the return data.            
            //    /// 
            //    db.AddOutParameter(dbCommandWrapper, "P_IHV_INVOICE_NO", DbType.String, 12);

            //    ///
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    ///
            //    _idFactura = (String)db.GetParameterValue(dbCommandWrapper, "P_IHV_INVOICE_NO");
            //    ///
            //    return _idFactura;
            //}
            //catch
            //{
            //    return "-1";
            //}
        }

        /// <summary>
        /// Consultar la cabecera de una factura
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_noInvoice"></param>
        /// <param name="_noLiquidation"></param>
        /// <returns></returns>
        public static DataSet  getInvoiceHdr(String _noInvoice)
        {
            string sqlquery = "USP_Leer_Venta_Reporte";
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
                cmd.Parameters.AddWithValue("@Ven_Id", _noInvoice);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                // CURSOR REF
                
                ///
                return ds;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Consultar detalles de la factura
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_noInvoice"></param>
        /// <returns></returns>
        public static DataTable getInvoiceDtl(String _company, String _noInvoice)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getinvoice_dtl";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _noInvoice, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar Ventas por fechas agrupadas por marca y tipo de articulo, muestra tambien el stock
        /// </summary>
        /// <param name="company">Codigo Compañia</param>
        /// <param name="warehouse">Codigo de la Bodega</param>
        /// <param name="fecStart">Fecha de Inicio de la Consulta</param>
        /// <param name="fecEnd">Fecha Final de la Consulta</param>
        /// <returns></returns>
        public static DataTable getSalesForDate(string company, string warehouse, string area, DateTime fecStart, DateTime fecEnd)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "VENTAS.sp_getsales_for_warebrandtype";///"ventas.SP_GETSALES_FOR_WAREBRANDTYPE";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, warehouse, area, fecStart, fecEnd, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_noInvoice"></param>
        /// <returns></returns>
        public static string updateNoPrintsInvoice(String _company, String _noInvoice)
        {
            return "";
            ///
            //try
            //{
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_updatenoprintsinvoice";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);
            //    ///
            //    db.AddInParameter(dbCommandWrapper, "p_ihv_co", DbType.String, _company);
            //    ///
            //    db.AddInParameter(dbCommandWrapper, "p_ihv_invoice_no", DbType.String, _noInvoice);

            //    ///
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    ///
            //    return "1";
            //}
            //catch
            //{
            //    return "-1";
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable getLinesOrdersVrsLiquiVrsInvoiced(String _company, String _noLiquidation)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_get_ordersvrsliquidvrsinvoi";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _noLiquidation, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }


        /// <summary>
        /// Consultar la informacion de la cabecera de la factura deacuerdo a un numero de liquidacion dado
        /// </summary>
        /// <returns></returns>
        public static DataTable getHdrInvoiceByLiquidation(String _company, String _noLiquidation)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_gethdrinvoicebyliquidation";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _noLiquidation, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_noInvoice"></param>
        /// <param name="_article"></param>
        /// <param name="_size"></param>
        /// <returns></returns>
        public static DataTable searchArticleInvoice(String _noInvoice, String _article, String _size, String _customer,string _calidad)
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

                cn = new SqlConnection(Conexion.myconexion());
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_noInvoice"></param>
        /// <returns></returns>
        public static DataTable getInvoiceHdrByNoInvoice(String _noInvoice)
        {
            string sqlquery = "USP_Leer_Venta_Liquidacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;           
            try
            {
                cn=new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", _noInvoice);
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_warehouseId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getSalesByMonthForBrand(String _company, String _warehouseId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            ///
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getsales_bymonthforbrand";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _warehouseId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
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
        /// <param name="_warehouseId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getSalesByWeekForBrand(String _company, String _warehouseId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            ///
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getsales_byweekforbrand";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _warehouseId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consulta de ventas por coordinador
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_warehouseId"></param>
        /// <param name="_date_start"></param>
        /// <param name="_date_end"></param>
        /// <returns></returns>
        public static DataTable getSalesByCoordinator(String _company, String _warehouseId, String _areaId, DateTime _date_start, DateTime _date_end)
        {
            DataTable dt = new DataTable();
            return dt;
            ///
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.SP_GETSALES_FOR_COORDINATOR";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _warehouseId, _areaId, _date_start, _date_end, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar ventas netas, ventas factura, margen y cantidades de los cedis, segmentado por semana.
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getAalesCediByWeek(String _company, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            ///
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getsalescedibyweek";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar articulos adquiridos por el cliente, filtrando por cliente, articulo etc.
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_noInvoice"></param>
        /// <param name="_idCustomer"></param>
        /// <param name="_article"></param>
        /// <param name="_size"></param>
        /// <returns></returns>
        public static DataTable getShippedDetail(String _company, String _noInvoice, String _idCustomer,
            String _article, String _size, String _noDocReturn)
        {
            DataTable dt = new DataTable();
            return dt;
            ///
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getshippeddetail";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _noInvoice, _idCustomer, _article, _size, _noDocReturn, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }


        /// <summary>
        /// Consultar Ventas por fechas agrupadas por marca y tipo de articulo, muestra tambien el stock
        /// </summary>
        /// <param name="company">Codigo Compañia</param>
        /// <param name="warehouse">Codigo de la Bodega</param>
        /// <param name="fecStart">Fecha de Inicio de la Consulta</param>
        /// <param name="fecEnd">Fecha Final de la Consulta</param>
        /// <returns></returns>
        public static DataTable getSalesByCategOrArticle(String company, String warehouse, DateTime fecStart, DateTime fecEnd, int _typeReport)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getsales_byopcions";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, warehouse, fecStart, fecEnd, _typeReport, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consulta de comportamiento de coordinadores por mes
        /// </summary>
        /// <param name="company"></param>
        /// <param name="warehouse"></param>
        /// <param name="fecStart"></param>
        /// <param name="fecEnd"></param>
        /// <returns></returns>
        public static DataTable getSalesCoordByMonth(String company, String warehouse, String areaId, DateTime fecStart, DateTime fecEnd)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getsalescoordbymonth";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, warehouse, areaId, fecStart, fecEnd, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public static DataTable getventaunmo(String company, String warehouse, String areaId, DateTime fecStart, DateTime fecEnd)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.USP_Consulta_UNMO";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, warehouse, areaId, fecStart, fecEnd, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Sir de aquarella
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_fecStart"></param>
        /// <returns></returns>
        public static DataTable loadSirAquarella(String _company, DateTime _fecStart)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_loadsiraquarella";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _fecStart, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar ventas y devoluciones por marca y tipo de articulo
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_warehouseId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getSalesWeekBrand(String _company, String _warehouseId, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            ///
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getsales_weekbrand";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _warehouseId, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consulta las compras y devoluciones realizadas por un coordinador, detallada por articulo y talla.
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_coordId"></param>
        /// <param name="_article"></param>
        /// <param name="_size"></param>
        /// <returns></returns>
        public static DataTable getSalesDevolByCoord(String _company, String _coordId, String _article, String _size)
        {
            DataTable dt = new DataTable();
            return dt;
            ///
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getsalesdevolbycoord";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _coordId, _article, _size, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
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
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <param name="_idCoord"></param>
        /// <param name="_idProm"></param>
        /// <returns></returns>
        public static DataTable getSalesPromoter(String _company, String _startDate, String _endDate, String _idCoord, String _idProm)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getsalespromoter";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _startDate, _endDate, _idCoord, _idProm, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consulta de performance de la red de coordinadores, nacional o por region
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_wareId"></param>
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getPerformNetworkCoord(String _company, String _wareId, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getperfomnetworkcoord";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _wareId, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getPerformNetworkPromoter(String _company, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getperfomnetworkpromoter";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_wareId"></param>
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getCoordAbstinence(String _company, String _wareId, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getcoordabstinence";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _wareId, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_wareId"></param>
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getCoordNewVinculations(String _company, String _wareId, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getcoordnewvinculations";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _wareId, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_wareId"></param>
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getCoordReentry(String _company, String _wareId, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getcoordReentry";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _wareId, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_wareId"></param>
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getCoordInactive(String _company, String _wareId, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getcoordinactive";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _wareId, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_wareId"></param>
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getCoordiPossibleDesertion(String _company, String _wareId, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getcoordipossibledesertion";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _wareId, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        public static DataTable getCoordiDesertion(String _company, String _wareId, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getcoorddesertion";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _wareId, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar desercion de promotores
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getPromDesertion(String _company, String _areaId, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getpromdesertion";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _areaId, _startDate, _endDate, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar unn promotor, su informacion y su coordinador
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_areaId"></param>
        /// <param name="_searchValue"></param>
        /// <returns></returns>
        public static DataTable getPromoter(String _company, String _areaId, String _searchValue)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getPromoter";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _areaId, _searchValue, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
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
        /// <param name="_areaId"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataTable getPromInfo(String _company, String _areaId, String _varConsult)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "ventas.sp_getprominfo";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _areaId, _varConsult, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    //
            //    return dtResult;
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Realizar una cosula de un datatable mediante linq y algunos campos especificos
        /// </summary>
        /// <param name="dtObj"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static DataTable getFilterLinq(Object dtObj, String filter)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    DataTable dt = (DataTable)dtObj;
            //    ///
            //    return (from x in dt.AsEnumerable()
            //            where
            //            x.Field<String>("nombrecompleto").ToUpper().Contains(filter.Trim().ToUpper()) ||
            //            x.Field<String>("nombrecompletoc").ToUpper().Contains(filter.Trim().ToUpper()) ||
            //            x.Field<String>("area").ToUpper().Contains(filter.Trim().ToUpper()) ||
            //            x.Field<String>("ubicationcustomer").ToUpper().Contains(filter.Trim().ToUpper()) ||
            //            x.Field<String>("ubicationcustomerc").ToUpper().Contains(filter.Trim().ToUpper()) ||
            //            x.Field<String>("cedulac").ToUpper().Contains(filter.Trim().ToUpper()) ||
            //            x.Field<String>("cedulap").ToUpper().Contains(filter.Trim().ToUpper())
            //            select x).CopyToDataTable();
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consulta de ventas y devoluciones por separadas.
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns>Varios cursores en un dataset</returns>
        public static DataSet getAuditSalesReturns(String _company, String _startDate, String _endDate)
        {
            DataSet dt = new DataSet();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "VENTAS.sp_getAuditSalesReturns";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _startDate, _endDate, results, results);
            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }


        /// <summary>
        /// Consulta de ventas por semana y categoria
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_ware"></param>
        /// <param name="_area"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataSet getSalesForCatByWeek(DateTime _startDate, DateTime _endDate, string _vcategoria)
        {            
            ///  
            string sqlquery = "USP_Leer_Venta_MajorCategoria";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn=new SqlConnection(Conexion.myconexion());
                cmd=new SqlCommand(sqlquery,cn);
                cmd.CommandTimeout=0;
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaini",_startDate);
                cmd.Parameters.AddWithValue("@fechafin",_endDate);
                cmd.Parameters.AddWithValue("@idtipoarticulo",_vcategoria);
                da=new SqlDataAdapter(cmd);
                ds=new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet getestadisticaventasneta(Int32 _anio,string vopcion)
        {
            string sqlquery = "USP_Venta_EstadisticaAnual";
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
                cmd.Parameters.AddWithValue("@var_opcion", vopcion);
                cmd.Parameters.AddWithValue("@var_anio", _anio);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consultar las ventas por gran-categoria y categoria, por semana y año.
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_ware"></param>
        /// <param name="_area"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataSet getSalesCategoWeekYear(string _co, DateTime _startDate)
        {
            DataSet dt = new DataSet();
            return dt;
            ///            
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "ventas.sp_getsalescatego_weekyear";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _startDate, results, results);
            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// Consulta de ventas generales o solo hall
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_ware"></param>
        /// <param name="_area"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <param name="type">1-> Solo ventas Hall, 2-> Ventas totales generales</param>
        /// <returns></returns>
        public static DataSet getSalesHallOrGral(string _co, string _ware, string _area, string _region, string _startDate, string _endDate, int type)
        {
            DataSet dt = new DataSet();
            return dt;
            //         
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    //
            //    string sqlCommand = "ventas.sp_getsales_hallorgral";
            //    //
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _ware, _area, _region, _startDate, _endDate, type, results);
            //    //
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        public static DataSet sbcomisiondetallada(int _are_id, DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_Leer_ComisionPersona_Detalle";
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
                cmd.Parameters.AddWithValue("@are_id", _are_id);
                cmd.Parameters.AddWithValue("@fecha_ini", _date_start);
                cmd.Parameters.AddWithValue("@fecha_fin", _date_end);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        public static DataSet getconsultaKPI(int _area_id, String _asesor, DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_ConsultaKPI";
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
                cmd.Parameters.AddWithValue("@are_id", _area_id);
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


        public static DataSet getconsultaventaperdida(int _area_id, String _asesor, DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_ConsultaVentaPerdida";
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

        public static DataSet getventaliderresumido(int _area_id,String _asesor, DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_Leer_VentasDevolucion";
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
                cmd.Parameters.AddWithValue("@are_id", _area_id);
                cmd.Parameters.AddWithValue("@fecha_ini",_date_start);
                cmd.Parameters.AddWithValue("@fecha_fin", _date_end);
                cmd.Parameters.AddWithValue("@asesor", _asesor);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataSet GetTipoArticulo()
        {
            string sqlquery = "USP_Leer_TipoArticulo";
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataSet Get_VentaSemanal(DateTime _fechaini, DateTime _fechafin)
        {
            string sqlquery = "USP_Leer_Venta_Semanal";
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
                cmd.Parameters.AddWithValue("@var_fechaini", _fechaini);
                cmd.Parameters.AddWithValue("@var_fechafin", _fechafin);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
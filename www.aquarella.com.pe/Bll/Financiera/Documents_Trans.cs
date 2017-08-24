using System;
using System.Data;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using www.aquarella.com.pe.be.Financiera;
//using Bata.Aquarella.Pe.BE.Financiera;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess;
//using Oracle.DataAccess.Types;
using System.Configuration;

namespace www.aquarella.com.pe.bll
{
    public class Documents_Trans
    {
        #region < Atributos >

        public bool _check { get; set; }
        public string _docNo { get; set; }
        public string _conceptid { get; set; }
        public string _date { get; set; }
        public decimal _value { get; set; }
        public decimal _increase { get; set; }
        public string _numeroid { get; set; }

        public string _fechadoc { get; set; }

        public Decimal _valuepago { get; set; }

        
        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < VARIABLES >
        private string _dtv_co;
        private string _dtv_transdoc_id;
        private int _dtn_coord_id;
        private string _dtv_concept_id;
        private string _dtv_document_no;
        private DateTime _dtd_document_date;
        private decimal _dtn_tax_base;
        private string _dtv_clear;
        private string _dtv_comments;
        private decimal _dtn_tax;
        private decimal _dtn_autorete;
        private string _dtv_warehouse;
        #endregion

        #region < CONSTRUCTORES >
        public Documents_Trans()
        {
            _dtv_co = string.Empty;
            _dtv_transdoc_id = string.Empty;
            _dtn_coord_id = 0;
            _dtv_concept_id = string.Empty;
            _dtv_document_no = string.Empty;
            _dtd_document_date = DateTime.Now;
            _dtn_tax_base = 0;
            _dtv_clear = string.Empty;
            _dtv_comments = string.Empty;
            _dtn_tax = 0;
            _dtn_autorete = 0;
            _dtv_warehouse = string.Empty;
        }

        public Documents_Trans(string dtv_co, string dtv_transdoc_id, int dtn_coord_id,
            string dtv_concept_id,
            string dtv_document_no,
            DateTime dtd_document_date,
            decimal dtn_tax_base,
            string dtv_clear, string dtv_comments, decimal dtn_tax, decimal dtn_autorete, string dtv_warehouse)
        {
            _dtv_co = dtv_co;
            _dtv_transdoc_id = dtv_transdoc_id;
            _dtn_coord_id = dtn_coord_id;
            _dtv_concept_id = dtv_concept_id;
            _dtv_document_no = dtv_document_no;
            _dtd_document_date = dtd_document_date;
            _dtn_tax_base = dtn_tax_base;
            _dtv_clear = dtv_clear;
            _dtv_comments = dtv_comments;
            _dtn_tax = dtn_tax;
            _dtn_autorete = dtn_autorete;
            _dtv_warehouse = dtv_warehouse;
        }
        #endregion

        #region < PROPIEDADES >
        public string dtv_Co
        {
            get { return _dtv_co; }
            set { _dtv_co = value; }
        }

        public string dtv_TransDoc_Id
        {
            get { return _dtv_transdoc_id; }
            set { _dtv_transdoc_id = value; }
        }

        public int dtn_Coord_Id
        {
            get { return _dtn_coord_id; }
            set { _dtn_coord_id = value; }
        }

        public string dtv_Concept_Id
        {
            get { return _dtv_concept_id; }
            set { _dtv_concept_id = value; }
        }

        public string dtv_Document_No
        {
            get { return _dtv_document_no; }
            set { _dtv_document_no = value; }
        }

        public DateTime dtd_Document_Date
        {
            get { return _dtd_document_date; }
            set { _dtd_document_date = value; }
        }

        public decimal dtn_Tax_Base
        {
            get { return _dtn_tax_base; }
            set { _dtn_tax_base = value; }
        }

        public string dtv_Clear
        {
            get { return _dtv_clear; }
            set { _dtv_clear = value; }
        }

        public string dtv_Comments
        {
            get { return _dtv_comments; }
            set { _dtv_comments = value; }
        }

        public decimal dtn_Tax
        {
            get { return _dtn_tax; }
            set { _dtn_tax = value; }
        }

        public decimal dtn_Autorete
        {
            get { return _dtn_autorete; }
            set { _dtn_autorete = value; }
        }

        public string dtv_Warehouse
        {
            get { return _dtv_warehouse; }
            set { _dtv_warehouse = value; }
        }
        #endregion

        #region < METODOS PUBLICOS >
        public bool Save()
        {
            return Documents_Trans.SaveDoctrans(this.dtv_Co,
                this.dtn_Coord_Id, this.dtv_Concept_Id, this.dtv_Document_No,
                this.dtn_Tax_Base, this.dtv_Comments, this.dtn_Tax, this.dtn_Autorete, this.dtv_Warehouse);
        }

        #endregion

        #region < METODOS PRIVADOS >

        #endregion

        #region < METODOS ESTATICOS >
        public static bool SaveDoctrans(string dtv_Co, int dtn_Coord_Id,
            string dtv_Concept_Id,
            string dtv_Document_No,
            decimal dtn_Tax_Base,
            string dtv_Comments,
            decimal dtn_Tax,
            decimal dtn_Autorete,
            string dtv_Warehouse
            )
        {
            return false;
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //bool result = false;
            //string sqlCommand = "SP_ADD_DOCTRANS";
            //string _DocTrans_Id = string.Empty;
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, dtv_Co,
            //     _DocTrans_Id, dtn_Coord_Id, dtv_Concept_Id, dtv_Document_No,
            //     dtn_Tax_Base, dtv_Comments, dtn_Tax, dtn_Autorete, dtv_Warehouse);
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        _DocTrans_Id = Convert.ToString(db.GetParameterValue(dbCommandWrapper, "p_dtv_transdoc_id"));

            //        // Commit the transaction.
            //        transaction.Commit();

            //        result = true;
            //    }
            //    catch (Exception)
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //    }
            //    connection.Close();
            //    return result;
            //}
        }

        /// <summary>
        /// Consultar cruce de pagos y liquidaciones de un cliente
        /// </summary>
        /// <param name="dtv_co"></param>
        /// <param name="dtn_coord_id"></param>
        /// <returns></returns>
        /// 
        static public DataSet get_DocTranLiquiByPedidoAgruparLider(int _idCust)
        {
            string sqlquery = "USP_Leer_Pago_GrupoLiqXLider";
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
                cmd.Parameters.AddWithValue("@bas_id", _idCust);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        static public DataSet get_DocTranLiquiByLider(int _idCust)
        {
            string sqlquery = "USP_Leer_PagoLiqXLider";
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
                cmd.Parameters.AddWithValue("@bas_id", _idCust);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        static public DataSet get_DocTranLiquiByCoordinator(int _idCust)
        {            
            string sqlquery="USP_Leer_PagoLiqXPersona";
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
                cmd.Parameters.AddWithValue("@bas_id", _idCust);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        static public DataSet get_PagoNcredito(string _bas_id, string _idliq)
        {          
            string sqlquery="USP_Leer_Pago_Liq";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                if (_idliq == null) _idliq = "";
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", Convert.ToDecimal(_bas_id));
                cmd.Parameters.AddWithValue("@liq_id", _idliq);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        static public DataSet get_DocTransByConcept(string dtv_co, string warehouse, DateTime dtd_date_start, DateTime dtd_date_end)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    string sqlCommand = "financiera.SP_LOADOCTRANS_X_CONCEPT";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, dtv_co, warehouse, dtd_date_start, dtd_date_end, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        static public DataSet get_Docn_TransAdonis(DateTime _varfechaini, DateTime _varfechafin, string var_cliente)
        {
            string sqlquery = "USP_Leer_Asientos_Adonis";
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
                cmd.Parameters.AddWithValue("@var_fechaini", _varfechaini);
                cmd.Parameters.AddWithValue("@var_fechafin", _varfechafin);
                cmd.Parameters.AddWithValue("@var_cliente", var_cliente);
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
        /// <param name="company"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        static public DataSet get_BalanceByCoordinator(string company, string warehouse, string areaId)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    string sqlCommand = "FINANCIERA.SP_GET_BALANCE_X_COORDINATOR";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, warehouse, areaId, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar saldos y montos en pedidos del cliente
        /// </summary>
        /// <param name="company"></param>
        /// <param name="warehouse"></param>
        /// <param name="areaId"></param>
        /// <returns></returns>
        static public DataSet getBalanceCoordById(string company, string customer)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    string sqlCommand = "financiera.sp_get_balance_coor_byid";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, customer, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
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
        /// <param name="_idCoord"></param>
        /// <param name="_noDoc"></param>
        /// <returns></returns>
        static public DataTable getClearDocTransByDoc(String _company, String _idCoord, String _noDoc)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    string sqlCommand = "financiera.sp_get_cleardoctransbydoc";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _idCoord, _noDoc, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
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
        /// <param name="_idCoord"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        static public DataTable getClearDocTransByDate(String _company, String _idCoord, String _startDate, String _endDate)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    string sqlCommand = "financiera.sp_get_cleardoctransbydate";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _idCoord, _startDate, _endDate, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //}
            //catch
            //{
            //    return null;
            //}
        }

        /// <summary>
        /// Consultar notas creditos y debitos cargAQUARELLAs a un cliente
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_startDate"></param>
        /// <param name="_endDate"></param>
        /// <returns></returns>
        public static DataSet getDocTransCust(String _company, String _startDate, String _endDate)
        {
            DataSet ds = new DataSet();
            return ds;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "financiera.sp_getdoctranscust";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _startDate, _endDate, results);
            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        /// <summary>
        /// Consulta clear de facturas
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_type">1->Clear solo de facturas de Pos, otro: cualquier factura</param>
        /// <param name="_document"></param>
        /// <param name="_invoice"></param>
        /// <param name="_dateStart"></param>
        /// <param name="_dateEnd"></param>
        /// <returns></returns>
        public static DataSet getClear(string _co, int _type, string _document, string _invoice, string _dateStart, string _dateEnd)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    string sqlCommand = "financiera.sp_get_clear";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _type, _document, _invoice, _dateStart, _dateEnd, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        static public Be_Documents_trans SaveValidateBank(Be_Documents_trans objArray, int var_usu,DataTable dt)
        {
            Be_Documents_trans resp = new Be_Documents_trans();
            string sqlquery = "USP_Valida_Archivo_Banco";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State==0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_validar", var_usu);
                cmd.Parameters.AddWithValue("@Pago_Valida", dt);
                cmd.ExecuteNonQuery();
                resp.Ok = true;
                resp.Mensaje = "El documento ya se encuentra registrado.";
            }
            catch (Exception ex)
            {
                resp.Ok = false;
                resp.Mensaje = ex.Message;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return resp;
            //Be_Documents_trans resp = new Be_Documents_trans();

            //string nameConnection = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            //OracleConnection con = new OracleConnection(nameConnection);
            //con.Open();

            //OracleCommand cmd = con.CreateCommand();
            //cmd.CommandText = "financiera.Array_PKG_ValidarBanco.INS_ValidarBanco";
            //cmd.CommandType = CommandType.StoredProcedure;

            //OracleParameter p_Fecha = new OracleParameter();
            //p_Fecha.OracleDbType = OracleDbType.Varchar2;
            //p_Fecha.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            //p_Fecha.Value = objArray.getSetFecha;
            //cmd.Parameters.Add(p_Fecha);

            //OracleParameter p_Descripcion = new OracleParameter();
            //p_Descripcion.OracleDbType = OracleDbType.Varchar2;
            //p_Descripcion.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            //p_Descripcion.Value = objArray.getSetDescripcion;
            //cmd.Parameters.Add(p_Descripcion);

            //OracleParameter p_Monto = new OracleParameter();
            //p_Monto.OracleDbType = OracleDbType.Decimal;
            //p_Monto.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            //p_Monto.Value = objArray.getSetMonto;
            //cmd.Parameters.Add(p_Monto);

            //OracleParameter p_Operacion = new OracleParameter();
            //p_Operacion.OracleDbType = OracleDbType.Varchar2;
            //p_Operacion.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            //p_Operacion.Value = objArray.getSetOperacion;
            //cmd.Parameters.Add(p_Operacion);

            //OracleParameter p_PAV_BANK_ID = new OracleParameter();
            //p_PAV_BANK_ID.OracleDbType = OracleDbType.Varchar2;
            //p_PAV_BANK_ID.CollectionType = OracleCollectionType.PLSQLAssociativeArray;
            //p_PAV_BANK_ID.Value = objArray.getSetBanco;
            //cmd.Parameters.Add(p_PAV_BANK_ID);

            //OracleParameter P_USU_CREACION = new OracleParameter();
            //P_USU_CREACION.OracleDbType = OracleDbType.Int32;
            //P_USU_CREACION.Direction = ParameterDirection.Input;
            //P_USU_CREACION.Value = var_usu;
            //cmd.Parameters.Add(P_USU_CREACION);

            //cmd.Parameters.Add("P_Secuencia", OracleDbType.Int32).Direction = ParameterDirection.Output;

            //try
            //{
            //    cmd.ExecuteNonQuery();
            //    resp.getSet_Num_Secuencia = cmd.Parameters["P_Secuencia"].Value;
            //    resp.Ok = true;
            //    //if (resp.getSet_Num_Secuencia is DBNull)
            //    //{
            //    //    resp.Mensaje = "El documento ya se encuentra registrado.";
            //    //}
            //}
            //catch (Exception ex)
            //{
            //    resp.Ok = false;
            //    resp.Mensaje = ex.Message;
            //}
            //finally
            //{
            //    cmd.Dispose();
            //    con.Close();
            //    con.Dispose();
            //}
            //return resp;
        }

        static public DataSet get_excelBank(string _co, int _idCust)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    string sqlCommand = "financiera.SP_LOADPAYLIQUI_X_COORDINATOR";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _idCust, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static void insertar_codigo_hash(string _ven_id, string _hash, string _estado)
        {
            string sqlquery = "USP_Insertar_Codigo_Hash";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", _ven_id);
                cmd.Parameters.AddWithValue("@codigo_hash", _hash);
                cmd.Parameters.AddWithValue("@Estado", _estado);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
            if (cn.State == ConnectionState.Open) cn.Close();
        }

        public static string _anular_saldo(string _dni_ruc,Decimal _usu_ing)
        {
            string _error="";
            string sqlquery = "USP_Anular_Saldos";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_ing", _usu_ing);
                cmd.Parameters.AddWithValue("@dniruc", _dni_ruc);
                cmd.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                _error = exc.Message;
            }
            if (cn.State==ConnectionState.Open) cn.Close();
            return _error;
        }
        static public string dt_ejecutar_provisiones(DataTable dt,decimal _usu_ingreso)
        {
            string sqlquery = "USP_Genera_Provisiones";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dtgenera = null;
            string _error = "";
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_ing", _usu_ingreso);
                cmd.Parameters.AddWithValue("@tmp_genera", dt);
                cmd.ExecuteNonQuery();
                //da = new SqlDataAdapter(cmd);
                //dtgenera = new DataTable();
                //da.Fill(dtgenera);                
            }
            catch(Exception exc)
            {
                //dtgenera = null;
                _error = exc.Message;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return _error;
        }
        static public DataTable leer_saldo_factura()
        {
            string sqlquery = "USP_Leer_Genera_Anticipos";
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
            catch
            {
                return null;
            }
        }
        public static DataSet get_movimiento_pago(DateTime _fechaini,DateTime _fechafin)
        {
            string sqlquery = "[USP_Movimiento_Pagos]";
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
                cmd.Parameters.AddWithValue("@fecha_ini", _fechaini);
                cmd.Parameters.AddWithValue("@fecha_fin", _fechafin);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch
            {
                ds = null;
            }
            return ds;
        }
        static public DataSet get_reportsemana(DateTime _fechaini, DateTime _fechafin)
        {
            string sqlquery = "USP_Leer_VentaFinanzas";
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
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion

        #region<PROPIEDADES ESTATICOS DE METODOS ANTICIPOS>

        public static string _correlativo_anticipo(string _doc, ref string _serie_ant_doc,ref string _serie_ant_nc,ref decimal _numero_ant_doc,ref decimal _numero_ant_nc)
        {
            string _error = "";
            string sqlquery = "USP_Correlativo_Anticipo";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State==0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_documento", _doc);
                cmd.Parameters.Add("@serie_ant_doc", SqlDbType.VarChar, 5);
                cmd.Parameters.Add("@serie_ant_nc", SqlDbType.VarChar, 5);
                cmd.Parameters.Add("@numero_ant_doc", SqlDbType.Decimal);
                cmd.Parameters.Add("@numero_ant_nc", SqlDbType.Decimal);

                cmd.Parameters["@serie_ant_doc"].Direction = ParameterDirection.Output;
                cmd.Parameters["@serie_ant_nc"].Direction = ParameterDirection.Output;
                cmd.Parameters["@numero_ant_doc"].Direction = ParameterDirection.Output;
                cmd.Parameters["@numero_ant_nc"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                _serie_ant_doc=cmd.Parameters["@serie_ant_doc"].Value.ToString();
                _serie_ant_nc=cmd.Parameters["@serie_ant_nc"].Value.ToString();
                _numero_ant_doc =Convert.ToDecimal(cmd.Parameters["@numero_ant_doc"].Value.ToString());
                _numero_ant_nc =Convert.ToDecimal(cmd.Parameters["@numero_ant_nc"].Value.ToString());

            }
            catch(Exception exc)
            {
                _error = exc.Message;
            }
            return _error;
        }

        #endregion

    }
}
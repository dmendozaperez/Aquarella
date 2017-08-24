using System;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace www.aquarella.com.pe.bll.Logistica
{
    public class Storages
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < VARIABLES >
        private string _stv_co;
        private string _stv_storagesid;
        private string _stv_descriptions;
        private string _stv_warehouse;
        private string _stv_typestorage;
        private string _stv_publicacces;
        private decimal _stn_order;
        #endregion

        #region < CONSTRUCTORES >
        public Storages()
        {
            _stv_co = string.Empty;
            _stv_storagesid = string.Empty;
            _stv_descriptions = string.Empty;
            _stv_warehouse = string.Empty;
            _stv_typestorage = string.Empty;
            _stv_publicacces = string.Empty;
            _stn_order = 0;
        }

        public Storages(string stv_co, string stv_storagesid, string stv_descriptions,
            string stv_warehouse, string stv_typestorage, string stv_publicacces, decimal stn_order)
        {
            _stv_co = stv_co;
            _stv_storagesid = stv_storagesid;
            _stv_descriptions = stv_descriptions;
            _stv_warehouse = stv_warehouse;
            _stv_typestorage = stv_typestorage;
            _stv_publicacces = stv_publicacces;
            _stn_order = stn_order;
        }
        #endregion

        #region < PROPIEDADES >
        public string stv_Co
        {
            get { return _stv_co; }
            set { _stv_co = value; }
        }

        public string stv_StoragesId
        {
            get { return _stv_storagesid; }
            set { _stv_storagesid = value; }
        }

        public string stv_Descriptions
        {
            get { return _stv_descriptions; }
            set { _stv_descriptions = value; }
        }

        public string stv_Warehouse
        {
            get { return _stv_warehouse; }
            set { _stv_warehouse = value; }
        }

        public string stv_TypeStorage
        {
            get { return _stv_typestorage; }
            set { _stv_typestorage = value; }
        }

        public string _stv_PublicAcces
        {
            get { return _stv_publicacces; }
            set { _stv_publicacces = value; }
        }

        public decimal _stn_Order
        {
            get { return _stn_order; }
            set { _stn_order = value; }
        }
        #endregion

        #region < METODOS PUBLICOS >
        /// <summary>
        /// Lee los datos del storage pasando la compañia y el codigo del storage.
        /// </summary>
        /// <param name="stv_co"></param>
        /// <param name="stv_storagesid"></param>
        public void Load(string stv_co, string stv_storagesid)
        {
            //DataSet that will hold the returned results		
            DataSet ds = this.LoadByPrimaryKey(stv_co, stv_storagesid);
            try
            {
                // Load member variables from datarow
                DataRow row = ds.Tables[0].Rows[0];
                _stv_co = (string)row["STV_CO"];
                _stv_storagesid = (string)row["STV_STORAGESID"];
                _stv_descriptions = row.IsNull("STV_DESCRIPTIONS") ? string.Empty : (string)row["STV_DESCRIPTIONS"];
                _stv_warehouse = row.IsNull("STV_WAREHOUSE") ? string.Empty : (string)row["STV_WAREHOUSE"];
                _stv_typestorage = row.IsNull("STV_TYPESTORAGE") ? string.Empty : (string)row["STV_TYPESTORAGE"];
                _stv_publicacces = row.IsNull("STV_PUBLICACCES") ? string.Empty : (string)row["STV_PUBLICACCES"];
                _stn_order = row.IsNull("STN_ORDER") ? 0 : (Decimal)row["STN_ORDER"];
            }
            catch
            {
                _stv_co = string.Empty;
                _stv_storagesid = string.Empty;
                _stv_descriptions = string.Empty;
                _stv_warehouse = string.Empty;
                _stv_typestorage = string.Empty;
                _stv_publicacces = string.Empty;
                _stn_order = 0;
            }
        }
        #endregion

        #region < METODOS PRIVADOS >
        /// <summary>
        /// Metodo privado para leer un storage
        /// </summary>
        /// <param name="stv_co"></param>
        /// <param name="stv_storage"></param>
        /// <returns>A DataSet containing the results of the query</returns>
        private DataSet LoadByPrimaryKey(string stv_co, string stv_storagesid)
        {
            DataSet ds = new DataSet();
            return ds;
            // CURSOR REF
            //object results = new object[1];

            //// Create the Database object, using the default database service. The
            //// default database service is determined through configuration.
            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "LOGISTICA.SP_LOADPK_STORAGE";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, stv_co, stv_storagesid, results);
            //// DataSet that will hold the returned results		
            //// Note: connection closed by ExecuteDataSet method call 
            //return db.ExecuteDataSet(dbCommandWrapper);
        }
        #endregion

        #region < METODOS ESTATICOS >

        /// <summary>
        /// Devuelve todos los registros existentes en la tabla de storages
        /// </summary>
        /// <param name="stv_co"></param>
        /// <returns></returns>
        public static DataSet getAllStorages(String stv_co)
        {
            DataSet ds = new DataSet();
            return ds;
            //object results = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //String sqlCommand = "sp_load_allstorages";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, stv_co, results);

            //return db.ExecuteDataSet(dbCommandWrapper);
        }
        /// <summary>
        /// Devuelve todos los registros existentes en la tabla de storages, pero filtrados por el acceso al publico
        /// y la compañia.
        /// </summary>
        /// <param name="stv_co"></param>
        /// <returns></returns>
        public static DataSet getStoragesWithPublicAcces(String stv_co, String isPublicAccess)
        {
            DataSet ds = new DataSet();
            return ds;
            //object results = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //String sqlCommand = "sp_get_StoragesWithPublicAcces";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, stv_co, isPublicAccess, results);

            //return db.ExecuteDataSet(dbCommandWrapper);
        }
        /// <summary>
        /// Obtiene los Storages que contiene el WareHouse especificado.
        /// </summary>
        /// <param name="STV_CO"></param>
        /// <param name="STV_WAREHOUSE"></param>
        /// <returns></returns>
        public static DataSet getStorageByWereHouse()
        {
            string sqlquery = "USP_Leer_Almacen";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
            //DataSet ds = new DataSet();
            //return ds;
            /////
            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "logistica.SP_GETSTORAGESBYWH_STOCKTRANS";
            /////
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, STV_CO, STV_WAREHOUSE, results);
            /////
            //DataSet ret = db.ExecuteDataSet(dbCommandWrapper);
            /////
            //return ret;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="STV_CO"></param>
        /// <param name="STV_WAREHOUSE"></param>
        /// <param name="isPublicAccess"></param>
        /// <returns></returns>
        public static DataSet getStorageByWHAndPA(String STV_CO, String STV_WAREHOUSE, String isPublicAccess)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    ///
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "logistica.sp_getstoragesbywhpastocktra";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, STV_CO, STV_WAREHOUSE, isPublicAccess, results);
            //    ///
            //    DataSet ret = db.ExecuteDataSet(dbCommandWrapper);
            //    ///
            //    return ret;
            //}
            //catch
            //{
            //    return null;
            //}

        }
        #endregion
    }
}
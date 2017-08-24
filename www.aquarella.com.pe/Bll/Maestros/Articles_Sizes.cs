using System;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace www.aquarella.com.pe.bll
{
    public class Articles_Sizes
    {

        #region < Atributos >

        public string _size_des { get; set; }
        public string _size { get; set; }
        public int _qty { get; set; }

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
       

        #endregion

        #region < VARIABLES >

        private string _asv_co;
        private string _asv_article;
        private string _asv_size_display;
        private bool _isNew;
        /// <summary>
        ///Variables de identificacion del usurio en el log.
        /// </summary>
        Decimal _USER;
        String _MACHINE;

        #endregion

        #region < CONSTRUCTORES >
        public Articles_Sizes()
        {
            _asv_article = string.Empty;
            _asv_co = string.Empty;
            _asv_size_display = string.Empty;
        }

        public Articles_Sizes(string ASV_ARTICLE, string ASV_CO, string ASV_SIZE_DISPLAY)
            : this()
        {
            _asv_article = ASV_ARTICLE;
            _asv_co = ASV_CO;
            _asv_size_display = ASV_SIZE_DISPLAY;
        }

        #endregion

        #region < PROPIEDADES >

        public string ASV_CO
        {
            get { return _asv_co; }
            set { _asv_co = value; }
        }

        public string ASV_ARTICLE
        {
            get { return _asv_article; }
            set { _asv_article = value; }
        }

        public string ASV_SIZE_DISPLAY
        {
            get { return _asv_size_display; }
            set { _asv_size_display = value; }
        }

        public bool IsNew
        {
            get
            {
                return _isNew;
            }
            set { _isNew = value; }
        }
        #endregion

        #region < METODOS PUBLICOS >

        /// <summary>
        /// Populates current instance of the object with info from the database, based on the requested primary key.
        /// </summary>
        /// <param name="ASV_ARTICLE"></param>
        /// <param name="ASV_CO"></param>
        /// <param name="ASV_SIZE_DISPLAY"></param>
        public void Load(string ASV_ARTICLE, string ASV_CO, string ASV_SIZE_DISPLAY)
        {
            // DataSet that will hold the returned results		
            DataSet ds = this.LoadByPrimaryKey(ASV_ARTICLE, ASV_CO, ASV_SIZE_DISPLAY);
            try
            {
                // Load member variables from datarow
                DataRow row = ds.Tables[0].Rows[0];
                _asv_co = (string)row["ASV_CO"];
                _asv_article = (string)row["ASV_ARTICLE"];
                _asv_size_display = (string)row["ASV_SIZE_DISPLAY"];
            }
            catch
            {
                _asv_article = string.Empty;
                _asv_co = string.Empty;
                _asv_size_display = string.Empty;
            }
        }
        /// <summary>
        /// Adds or updates information in the database depending on the primary key stored in the object instance.
        /// </summary>
        /// <returns>Returns True if saved successfully, False otherwise.</returns>
        public bool Save(Decimal user, String machine)
        {
            _USER = user;
            _MACHINE = machine;
            if (this.IsNew)
                return Insert();
            else
                return Update();
        }
        /// <summary>
        /// Serializes the current instance data to an Xml string.
        /// </summary>
        /// <returns>A string containing the Xml representation of the object.</returns>
        public string ToXml()
        {
            // DataSet that will hold the returned results		
            DataSet ds = this.LoadByPrimaryKey(_asv_article, _asv_co, _asv_size_display);
            StringWriter writer = new StringWriter();
            ds.WriteXml(writer);
            return writer.ToString();
        }

        #endregion

        #region < METODOS PRIVADOS >

        /// <summary>
        /// Populates a dataset with info from the database, based on the requested primary key.
        /// </summary>
        /// <param name="ASV_ARTICLE"></param>
        /// <param name="ASV_CO"></param>
        /// <param name="ASV_SIZE_DISPLAY"></param>
        /// <returns>A DataSet containing the results of the query</returns>
        private DataSet LoadByPrimaryKey(string ASV_ARTICLE, string ASV_CO, string ASV_SIZE_DISPLAY)
        {
            DataSet ds = new DataSet();
            return ds;
            // CURSOR REF
            //object results = new object[1];
            //// Create the Database object, using the default database service. The
            //// default database service is determined through configuration.
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "SP_LOADPK_ARTICLE_SIZES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, ASV_ARTICLE, ASV_CO, ASV_SIZE_DISPLAY, results);
            //// DataSet that will hold the returned results		
            //// Note: connection closed by ExecuteDataSet method call 
            //return db.ExecuteDataSet(dbCommandWrapper);
        }
        /// <summary>
        /// Inserts the current instance data into the database.
        /// </summary>
        /// <returns>Returns True if saved successfully, False otherwise.</returns>
        private bool Insert()
        {
            return false;
            //// Create the Database object, using the default database service. The
            //// default database service is determined through configuration.
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "SP_ADD_ARTICLE_SIZES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _asv_co, _asv_article, _asv_size_display);
            //try
            //{
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    return true;
            //    /////////////////////
            //    //Guardar en el log
            //    /////////////////////
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message, ex.InnerException);
            //}
        }
        /// <summary>
        /// Updates the current instance data in the database.
        /// </summary>
        /// <returns>Returns True if saved successfully, False otherwise.</returns>
        private bool Update()
        {
            return false;
            //// Create the Database object, using the default database service. The
            //// default database service is determined through configuration.
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "SP_ADD_ARTICLE_SIZES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _asv_co, _asv_article, _asv_size_display);
            //try
            //{
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    return true;
            //    //////////////////////
            //    //Guardar en el log
            //    //////////////////////
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message, ex.InnerException);
            //}
        }
        /// <summary>
        /// Utility function that returns a DBNull.Value if requested. The comparison is done inline
        /// in the Insert() and Update() functions.
        /// </summary>
        private object SetNullValue(bool isNullValue, object value)
        {
            if (isNullValue)
                return DBNull.Value;
            else
                return value;
        }

        #endregion

        #region < METODOS ESTATICOS >

        /// <summary>
        /// Verificacion de la existencia dela relacion especificAQUARELLA.
        /// </summary>
        /// <param name="ASV_ARTICLE"></param>
        /// <param name="ASV_CO"></param>
        /// <param name="ASV_SIZE_DISPLAY"></param>
        /// <returns></returns>
        public static bool Existe(string ASV_ARTICLE, string ASV_CO, string ASV_SIZE_DISPLAY)
        {
            Articles_Sizes X = new Articles_Sizes();
            ///
            X.Load(ASV_ARTICLE, ASV_CO, ASV_SIZE_DISPLAY);
            if (!(X.ASV_ARTICLE == string.Empty) && !(X.ASV_CO == string.Empty) && !(X.ASV_SIZE_DISPLAY == string.Empty))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Artcle Sizes:
        /// -> devolver las tallas de un calzado 
        /// </summary>
        /// <param name="ASV_ARTICLE"></param>
        /// <returns></returns> 
        public static DataTable GetAllSizesForOneShoe(string ASV_ARTICLE)
        {
            DataTable dt = new DataTable();
            return dt;
            //// CURSOR REF
            //object results = new object[1];
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string sqlCommand = "maestros.sp_getsizesfromarticle";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, ASV_ARTICLE, results);
            ////return db.ExecuteDataSet(dbCommandWrapper);
            /////string sqlCommand = "Select * From ARTICLE_SIZES Where ASV_ARTICLE = '" + ASV_ARTICLE + "'";
            /////DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
            ////db.AddInParameter(dbCommandWrapper, "PHV_CO", DbType.AnsiString, PHV_CO);
            //DataTable t = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //t.Columns["ASV_ARTICLE"].DefaultValue = ASV_ARTICLE;

            //return t;
        }
        /// <summary>
        /// Retorna las relaciones que posean el articulo especificAQUARELLAo.
        /// </summary>
        /// <param name="ASV_CO"></param>
        /// <param name="ASV_ARTICLE"></param>
        /// <returns></returns>
        public static DataTable GetByARTICLE(string ASV_CO, string ASV_ARTICLE)
        {
            DataTable dt = new DataTable();
            return dt;
            //// CURSOR REF JhoamMauricio el 29 de julio de 2008
            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "maestros.SP_LOADXARTICLE_ART_SIZES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, ASV_CO, ASV_ARTICLE, results);
            //return db.ExecuteDataSet(dbCommandWrapper).Tables[0];


            ////Database db = DatabaseFactory.CreateDatabase();
            ////string sqlCommand = "Select * From ARTICLE_SIZES Where ASV_CO = '" + ASV_CO + "' and " + "ASV_ARTICLE = '" + ASV_ARTICLE + "'";
            ////DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
            ///////
            ////DataTable t = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            ////t.Columns["ASV_CO"].DefaultValue = ASV_CO;
            ////t.Columns["ASV_ARTICLE"].DefaultValue = ASV_ARTICLE;
            ///////
            ////return t;
        }
        /// <summary>
        /// Retrorna las relaciones que posean la talla especificado.
        /// </summary>
        /// <param name="ASV_CO"></param>
        /// <param name="ASV_SIZE_DISPLAY"></param>
        /// <returns></returns>
        public static DataTable GetBySIZES(string ASV_CO, string ASV_SIZE_DISPLAY)
        {
            DataTable dt = new DataTable();
            return dt;
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string sqlCommand = "Select * From ARTICLE_SIZES Where ASV_CO = '" + ASV_CO + "' and " + "ASV_SIZE_DISPLAY = '" + ASV_SIZE_DISPLAY + "'";
            //DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
            //DataTable t = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //t.Columns["ASV_CO"].DefaultValue = ASV_CO;
            //t.Columns["ASV_SIZE_DISPLAY"].DefaultValue = ASV_SIZE_DISPLAY;
            /////
            //return t;
        }
        /// <summary>
        /// Retorna todas las relaciones entre articulos y tallas del Sistema de información.
        /// </summary>
        /// <returns></returns>
        public static DataSet GetAllARTICLE_SIZES()
        {
            DataSet ds = new DataSet();
            return ds;
            //// CURSOR REF
            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "SP_LOAQUARELLALL_ARTICLE_SIZES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);
            //return db.ExecuteDataSet(dbCommandWrapper);
        }
        /// <summary>
        /// Guarda una tabla con relaciones entre articulos y tallas.
        /// </summary>
        /// <param name="tabla"></param>
        public static void SaveAll(DataTable tabla)
        {
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //DbDataAQUARELLApter da = db.GetDataAQUARELLApter();

            //da.SelectCommand = db.GetStoredProcCommand("SP_LOAQUARELLAll_ARTICLE_SIZES");
            //da.InsertCommand = db.GetStoredProcCommand("SP_ADD_ARTICLE_SIZES");
            //da.UpdateCommand = db.GetStoredProcCommand("SP_ADD_ARTICLE_SIZES");
            //da.DeleteCommand = db.GetStoredProcCommand("SP_DELETE_ARTICLE_SIZES");

            //#region Parametros de InsertCommand
            //db.AddInParameter(da.InsertCommand, "ASV_CO", DbType.AnsiString, "ASV_CO", DataRowVersion.Default);
            //db.AddInParameter(da.InsertCommand, "ASV_ARTICLE", DbType.AnsiString, "ASV_ARTICLE", DataRowVersion.Default);
            //db.AddInParameter(da.InsertCommand, "ASV_SIZE_DISPLAY", DbType.AnsiString, "ASV_SIZE_DISPLAY", DataRowVersion.Default);

            //#endregion

            //#region Parametros de UpdateCommand
            //db.AddInParameter(da.UpdateCommand, "ASV_CO", DbType.AnsiString, "ASV_CO", DataRowVersion.Default);
            //db.AddInParameter(da.UpdateCommand, "ASV_ARTICLE", DbType.AnsiString, "ASV_ARTICLE", DataRowVersion.Default);
            //db.AddInParameter(da.UpdateCommand, "ASV_SIZE_DISPLAY", DbType.AnsiString, "ASV_SIZE_DISPLAY", DataRowVersion.Default);

            //#endregion

            //#region Parametros de DeleteCommand
            //db.AddInParameter(da.DeleteCommand, "ASV_ARTICLE", DbType.AnsiString, "ASV_ARTICLE", DataRowVersion.Default);
            //db.AddInParameter(da.DeleteCommand, "ASV_CO", DbType.AnsiString, "ASV_CO", DataRowVersion.Default);
            //db.AddInParameter(da.DeleteCommand, "ASV_SIZE_DISPLAY", DbType.AnsiString, "ASV_SIZE_DISPLAY", DataRowVersion.Default);

            //#endregion

            //db.UpdateDataSet(tabla.DataSet, tabla.TableName, da.InsertCommand, da.UpdateCommand, da.DeleteCommand, UpdateBehavior.Standard);
        }
        /// <summary>
        /// Removes info from the database, based on the requested primary key.
        /// </summary>
        /// <param name="ASV_ARTICLE"></param>
        /// <param name="ASV_CO"></param>
        /// <param name="ASV_SIZE_DISPLAY"></param>
        public static void Remove(string ASV_ARTICLE, string ASV_CO, string ASV_SIZE_DISPLAY, Decimal user, String machine)
        {
            //// Create the Database object, using the default database service. The
            //// default database service is determined through configuration.
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "SP_DELETE_ARTICLE_SIZES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);
            //// Add primary keys to command wrapper.
            //db.AddInParameter(dbCommandWrapper, "P_ASV_ARTICLE", DbType.AnsiString, ASV_ARTICLE);
            //db.AddInParameter(dbCommandWrapper, "P_ASV_CO", DbType.AnsiString, ASV_CO);
            //db.AddInParameter(dbCommandWrapper, "P_ASV_SIZE_DISPLAY", DbType.AnsiString, ASV_SIZE_DISPLAY);
            /////
            //db.ExecuteNonQuery(dbCommandWrapper);
        }
        /// <summary>
        /// Cargar todas las tallas para los articulos habilitados
        /// </summary>
        /// <returns></returns>
        public static DataSet cargarArticles_Sizes()
        {
            DataSet ds = new DataSet();
            return ds;
            //// CURSOR REF
            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //String sqlCommand = "SP_LOAD_ARTICLES_SIZES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);
            //return db.ExecuteDataSet(dbCommandWrapper);
        }

        /*
        /// <summary>
        /// Método que consulta las tallas para un articulo dado
        /// Este método maneja un dataAQUARELLApter debido a que carga la información devuelta
        /// desde la base de datos en un Dataset XSD, el cual es provisto desde el llamado.        
        /// </summary>
        /// <param name="dtsOrders"></param>
        /// <param name="_articulo"></param>
        /// <param name="_company"></param>
        /// <returns></returns>
        public static DataSetPurchaseOrder cargarArticles_Sizes(DataSetPurchaseOrder dtsOrders, String _articulo, String _company)
        {
            Database db = DatabaseFactory.CreateDatabase();
            object results = new object[1];

            String sqlCommand = "SP_LOAD_SIZES_VARTICLE";
            
          //String sqlCommand = "SELECT asv_co, asv_article, asv_size_display, arv_name " +
          //                          "  FROM maestros.article_sizes INNER JOIN maestros.v_articles " +
          //                                 " ON (asv_co = arv_co AND asv_article = arv_article) " +
          //                            " WHERE asv_article = '" + _articulo + "' AND asv_co = '" + _company + "' " +
          //                        " ORDER BY asv_size_display";
          //  //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);

          //  ///return db.ExecuteDataSet(dbCommandWrapper);
          //  ///
             
            DbDataAQUARELLApter custDA = db.GetDataAQUARELLApter();
            custDA.SelectCommand = db.GetStoredProcCommand(sqlCommand, _company, _articulo, results);//db.GetSqlStringCommand(sqlCommand);
            try
            {
                custDA.SelectCommand.Connection = db.CreateConnection();
                custDA.Fill(dtsOrders, "Articles_Sizes");
                //custDA.SelectCommand.Connection.Close();

            }
            catch (Exception e)
            { }
            return dtsOrders;
        }
        */

        /*
        /// <summary>
        /// Método que consulta las tallas para un articulo dado
        /// Este método maneja un dataAQUARELLApter debido a que carga la información devuelta
        /// desde la base de datos en un Dataset XSD, el cual es provisto desde el llamado.        
        /// </summary>
        /// <param name="dtsOrders"></param>
        /// <param name="_articulo"></param>
        /// <param name="_company"></param>
        /// <returns></returns>
        public static DataSetPurchaseOrder cargarArticles_Sizes_Orders(DataSetPurchaseOrder dtsOrders, String _articulo, String _company)
        {
            Database db = DatabaseFactory.CreateDatabase();
            object results = new object[1];

            String sqlCommand = "MAESTROS.SP_LOAD_SIZES_VARTICLE_ORDERS";
            //String sqlCommand = "SELECT asv_co, asv_article, asv_size_display, arv_name " +
            //                        "  FROM maestros.article_sizes INNER JOIN maestros.v_articles " +
            //                               " ON (asv_co = arv_co AND asv_article = arv_article) " +
            //                          " WHERE asv_article = '" + _articulo + "' AND asv_co = '" + _company + "' " +
            //                      " ORDER BY asv_size_display";
            ////DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);

            /////return db.ExecuteDataSet(dbCommandWrapper);
            /////
             
            DbDataAQUARELLApter custDA = db.GetDataAQUARELLApter();
            custDA.SelectCommand = db.GetStoredProcCommand(sqlCommand, _company, _articulo, results);//db.GetSqlStringCommand(sqlCommand);
            try
            {
                custDA.SelectCommand.Connection = db.CreateConnection();
                custDA.Fill(dtsOrders, "Articles_Sizes");
                //custDA.SelectCommand.Connection.Close();

            }
            catch (Exception e)
            { }
            return dtsOrders;
        }
        */

        /// <summary>
        /// Metodo Estatico para la Actualización de un registro
        /// Nota: No intente agragar un nuevo registro con esta metodo esta valídado.
        /// </summary>
        /// <param name="ASV_CO"></param>
        /// <param name="ASV_ARTICLE"></param>
        /// <param name="ASV_SIZE_DISPLAY"></param>
        /// <returns> actalizo(true) o no actualizo(false)</returns>
        static public bool actualizarARTICLE_SIZES(string ASV_CO, string ASV_ARTICLE, string ASV_SIZE_DISPLAY, Decimal user, String machine)
        {
            //Intanciación de ARTICLE_SIZES
            Articles_Sizes X = new Articles_Sizes(ASV_CO, ASV_ARTICLE, ASV_SIZE_DISPLAY);
            //Validadcion de existencia de ARTICLE_SIZES
            if (Articles_Sizes.Existe(X.ASV_ARTICLE, X.ASV_CO, X.ASV_SIZE_DISPLAY))
            {
                //Update
                X.IsNew = false;
                //Guardar
                if (X.Save(user, machine))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static List<Articles_Sizes> getObjectSizes(DataTable dtArtSizes, bool units)
        {
            List<Articles_Sizes> sizes = new List<Articles_Sizes>();

            if (dtArtSizes == null || dtArtSizes.Rows.Count == 0)
                return null;

            foreach (DataRow dr in dtArtSizes.Rows)
            {
                if (units)
                    sizes.Add(new Articles_Sizes
                    {
                        _size = dr["asv_size_display"].ToString(),
                        _qty = int.Parse(dr["son_qty"].ToString())
                    });
                else
                    sizes.Add(new Articles_Sizes
                    {
                        _size_des= dr["tall_Des"].ToString(),
                        _size = dr["Tal_Descripcion"].ToString()
                    });
            }

            return sizes;
        }


        /// <summary>
        /// Consulta las tallas un articulo, y el stock de cAQUARELLA una de ellas
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_code"></param>
        /// <param name="_ware"></param>
        /// <returns></returns>
        public static DataSet getItemSizes(string _co, string _code, string _ware)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    object obj = (object)new object[1];
            //    Database database = DatabaseFactory.CreateDatabase(_conn);
            //    string str2 = "maestros.sp_get_item_sizes";
            //    DbCommand storedProcCommand = database.GetStoredProcCommand(str2, _co, _code, _ware, obj);
            //    return database.ExecuteDataSet(storedProcCommand);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message, ex.InnerException);
            //}
        }


        #endregion
    }
}
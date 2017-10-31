using System;
using System.Data.Common;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.bll
{
    public class Article
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>


        #endregion

        #region < VARIABLES >
        private string _arv_co;
        private string _arv_article;
        private string _arv_name;
        private string _arv_brand_id;
        private string _arv_category;
        private string _arv_sub_category;
        private string _arv_major_category;
        private string _arv_material;
        private string _arv_origin;
        private string _arv_photo;
        private string _arv_type;
        private string _arv_group;
        private string _arv_color;
        private string _arv_design;
        private string _arv_status;
        private string _arv_packing;
        private string _arv_collection;
        private string _arv_sole;
        private string _arv_upper;
        private string _arv_heeled;
        private int _arn_sellout;
        private DateTime _ard_create_date;
        private string _arv_articlegroups;
        private int _arn_commission;

        private decimal _price;

        #endregion

        #region < CONSTRUCTORES >
        public Article()
        {
            _arv_co = string.Empty;
            _arv_article = string.Empty;
            _arv_name = string.Empty;
            _arv_brand_id = string.Empty;
            _arv_category = string.Empty;
            _arv_sub_category = string.Empty;
            _arv_major_category = string.Empty;
            _arv_material = string.Empty;
            _arv_origin = string.Empty;
            _arv_photo = string.Empty;
            _arv_type = string.Empty;
            _arv_group = string.Empty;
            _arv_color = string.Empty;
            _arv_design = string.Empty;
            _arv_status = string.Empty;
            _arv_packing = string.Empty;
            _arv_collection = string.Empty;
            _arv_sole = string.Empty;
            _arv_upper = string.Empty;
            _arv_heeled = string.Empty;
            _arn_sellout = 0;
            _ard_create_date = DateTime.Now;
            _arv_articlegroups = string.Empty;
            _price = 0;
            //_articlesizes = null;
            _arn_commission = 0;
        }

        public Article(string arv_co, string arv_article, string arv_name,
            string arv_brand_id, string arv_category, string arv_sub_category,
            string arv_major_category, string arv_material, string arv_origin,
            string arv_photo, string arv_type, string arv_group, string arv_color,
            string arv_design, string arv_status, string arv_packing,
            string arv_collection, string arv_sole, string arv_upper, string arv_heeled,
            int arn_sellout, DateTime ard_create_date, int arn_commission, Decimal price)
        {
            _arv_co = arv_co;
            _arv_article = arv_article;
            _arv_name = arv_name;
            _arv_brand_id = arv_brand_id;
            _arv_category = arv_category;
            _arv_sub_category = arv_sub_category;
            _arv_major_category = arv_major_category;
            _arv_material = arv_material;
            _arv_origin = arv_origin;
            _arv_photo = arv_photo;
            _arv_type = arv_type;
            _arv_group = arv_group;
            _arv_color = arv_color;
            _arv_design = arv_design;
            _arv_status = arv_status;
            _arv_packing = arv_packing;
            _arv_collection = arv_collection;
            _arv_sole = arv_sole;
            _arv_upper = arv_upper;
            _arv_heeled = arv_heeled;
            _arn_sellout = arn_sellout;
            _arv_articlegroups = arv_group + arv_design + arv_material;
            _ard_create_date = ard_create_date;
            //_articlesizes = articlesizes;
            _arn_commission = arn_commission;
            _price = price;
        }
        #endregion

        #region < PROPIEDADES >

        public decimal price
        {
            get { return _price; }
            set { _price = value; }
        }

        public string arv_Co
        {
            get { return _arv_co; }
            set { _arv_co = value; }
        }

        public string arv_Article
        {
            get { return _arv_article; }
            set { _arv_article = value; }
        }

        public string arv_Name
        {
            get { return _arv_name; }
            set { _arv_name = value; }
        }

        public string arv_Brand_Id
        {
            get { return _arv_brand_id; }
            set { _arv_brand_id = value; }
        }

        public string arv_Category
        {
            get { return _arv_category; }
            set { _arv_category = value; }
        }

        public string arv_Sub_Category
        {
            get { return _arv_sub_category; }
            set { _arv_sub_category = value; }
        }

        public string arv_Major_Category
        {
            get { return _arv_major_category; }
            set { _arv_major_category = value; }
        }

        public string arv_Material
        {
            get { return _arv_material; }
            set { _arv_material = value; }
        }

        public string arv_Origin
        {
            get { return _arv_origin; }
            set { _arv_origin = value; }
        }

        public string arv_Photo
        {
            get { return _arv_photo; }
            set { _arv_photo = value; }
        }

        public string arv_Type
        {
            get { return _arv_type; }
            set { _arv_type = value; }
        }

        public string arv_Group
        {
            get { return _arv_group; }
            set { _arv_group = value; }
        }

        public string arv_Color
        {
            get { return _arv_color; }
            set { _arv_color = value; }
        }

        public string arv_Design
        {
            get { return _arv_design; }
            set { _arv_design = value; }
        }

        public string arv_Status
        {
            get { return _arv_status; }
            set { _arv_status = value; }
        }

        public string arv_Packing
        {
            get { return _arv_packing; }
            set { _arv_packing = value; }
        }

        public string arv_Collection
        {
            get { return _arv_collection; }
            set { _arv_collection = value; }
        }

        public string arv_Sole
        {
            get { return _arv_sole; }
            set { _arv_sole = value; }
        }

        public string arv_Upper
        {
            get { return _arv_upper; }
            set { _arv_upper = value; }
        }

        public string arv_Heeled
        {
            get { return _arv_heeled; }
            set { _arv_heeled = value; }
        }

        public int arn_Sellout
        {
            get { return _arn_sellout; }
            set { _arn_sellout = value; }
        }

        public DateTime ard_Create_Date
        {
            get { return _ard_create_date; }
            set { _ard_create_date = value; }
        }

        public string arv_ArticleGroups
        {
            get
            {
                if (String.IsNullOrEmpty(_arv_articlegroups))
                    _arv_articlegroups = arv_Design + arv_Material;
                return _arv_articlegroups;
            }
        }

        public int arn_Commission
        {
            get { return _arn_commission; }
            set { _arn_commission = value; }
        }
        #endregion

        ///////////////////////////////////////////////////////////////////////////
        /// Estos metodos permiten validar la exixtencia de articulos en la tabla
        /////////////////////////////////////////////////////////////////////////////
        #region <Metodos Nuevos>
        /// <summary>
        /// Populates a dataset with info from the database, based on the requested primary key.
        /// </summary>
        /// <param name="APN_ID"></param>
        /// <param name="APV_CO"></param>
        /// <returns>A DataSet containing the results of the query</returns>
        private DataSet LoadByPrimaryKey(string ARV_ARTICLE, string ARV_CO)
        {
            DataSet ds = new DataSet();
            return ds;
            // CURSOR REF
            //object results = new object[1];

            //// Create the Database object, using the default database service. The
            //// default database service is determined through configuration.
            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "SP_LOADPK_ARTICLE";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, ARV_CO, ARV_ARTICLE, results);
            //// DataSet that will hold the returned results		
            //// Note: connection closed by ExecuteDataSet method call 
            //return db.ExecuteDataSet(dbCommandWrapper);
        }

        /// <summary>
        /// Populates a dataset with info from the database, based on the requested primary key.
        /// </summary>
        /// <param name="APN_ID"></param>
        /// <param name="APV_CO"></param>
        /// <returns>A DataSet containing the results of the query</returns>
        static public DataSet getV_ArticleByPk(string ARV_ARTICLE, string ARV_CO)
        {
            DataSet ds = new DataSet();
            return ds;
            //// CURSOR REF
            //object results = new object[1];

            //// Create the Database object, using the default database service. The
            //// default database service is determined through configuration.
            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "MAESTROS.SP_LOADPK_V_ARTICLES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, ARV_CO, ARV_ARTICLE, results);
            //// DataSet that will hold the returned results		
            //// Note: connection closed by ExecuteDataSet method call 
            //return db.ExecuteDataSet(dbCommandWrapper);
        }

        /// <summary>
        /// Populates a dataset with info from the database, based on the requested primary key.
        /// </summary>
        /// <param name="APN_ID"></param>
        /// <param name="APV_CO"></param>
        /// <returns>A DataSet containing the results of the query</returns>
        static public DataSet getV_ArticleByPkAll(string ARV_ARTICLE, ref DataTable dttalla)
        {

            string sqlquery = "USP_Leer_Articulo";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Art_Id", ARV_ARTICLE);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dttalla = ds.Tables[1];
            return ds;
            //// CURSOR REF
            //object results = new object[1];

            //// Create the Database object, using the default database service. The
            //// default database service is determined through configuration.
            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "MAESTROS.SP_LOADPK_V_ARTICLES";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, ARV_CO, ARV_ARTICLE, results);
            //// DataSet that will hold the returned results		
            //// Note: connection closed by ExecuteDataSet method call 
            //return db.ExecuteDataSet(dbCommandWrapper);
        }
        /// <summary>
        /// Populates current instance of the object with info from the database, based on the requested primary key.
        /// </summary>
        /// <param name="APN_ID"></param>
        /// <param name="APV_CO"></param>
        public virtual void Load(string ARV_ARTICLE, string ARV_CO)
        {
            //DataSet that will hold the returned results		
            DataSet ds = this.LoadByPrimaryKey(ARV_ARTICLE, ARV_CO);
            try
            {
                // Load member variables from datarow
                DataRow row = ds.Tables[0].Rows[0];
                _arv_co = (string)row["ARV_CO"];
                _arv_article = (string)row["ARV_ARTICLE"];
                _arv_name = row.IsNull("ARV_NAME") ? string.Empty : (string)row["ARV_NAME"];
                _arv_brand_id = row.IsNull("ARV_BRAND_ID") ? string.Empty : (string)row["ARV_BRAND_ID"];
                _arv_category = row.IsNull("ARV_CATEGORY") ? string.Empty : (string)row["ARV_CATEGORY"];
                _arv_sub_category = row.IsNull("ARV_SUB_CATEGORY") ? string.Empty : (String)row["ARV_SUB_CATEGORY"];
                _arv_major_category = row.IsNull("ARV_MAJOR_CATEGORY") ? string.Empty : (string)row["ARV_MAJOR_CATEGORY"];
                _arv_material = row.IsNull("ARV_MATERIAL") ? string.Empty : (string)row["ARV_MATERIAL"];
                _arv_origin = row.IsNull("ARV_ORIGIN") ? string.Empty : (string)row["ARV_ORIGIN"];
                _arv_photo = row.IsNull("ARV_PHOTO") ? string.Empty : (string)row["ARV_PHOTO"];
                _arv_type = row.IsNull("ARV_TYPE") ? string.Empty : (string)row["ARV_TYPE"];
                _arv_group = row.IsNull("ARV_GROUP") ? string.Empty : (string)row["ARV_GROUP"];
                _arv_color = row.IsNull("ARV_COLOR") ? string.Empty : (string)row["ARV_COLOR"];
                _arv_design = row.IsNull("ARV_DESING") ? string.Empty : (string)row["ARV_DESING"];
                _arv_status = row.IsNull("ARV_STATUS") ? string.Empty : (string)row["ARV_STATUS"];
                _arv_packing = row.IsNull("ARV_PACKING") ? string.Empty : (string)row["ARV_PACKING"];
                _arv_collection = row.IsNull("ARV_COLLECTION") ? string.Empty : (string)row["ARV_COLLECTION"];
                _arv_sole = row.IsNull("ARV_SOLE") ? string.Empty : (string)row["ARV_SOLE"];
                _arv_upper = row.IsNull("ARV_UPPER") ? string.Empty : (string)row["ARV_UPPER"];
                _arv_heeled = row.IsNull("ARV_HEELED") ? string.Empty : (string)row["ARV_HEELED"];
                _arn_sellout = row.IsNull("ARN_SELLOUT") ? 0 : Convert.ToInt32(row["ARN_SELLOUT"]);
                _ard_create_date = row.IsNull("ARD_CREATE_DATE") ? DateTime.Now : (DateTime)row["ARD_CREATE_DATE"];
                _arv_articlegroups = row.IsNull("ARV_ARTICLEGROUPS") ? string.Empty : (string)row["ARV_ARTICLEGROUPS"];
                _arn_commission = row.IsNull("ARN_COMMISSION") ? 0 : Convert.ToInt32(row["ARN_COMMISSION"]);

            }
            catch
            {
                _arv_co = string.Empty;
                _arv_article = string.Empty;
                _arv_name = "empty";
                _arv_brand_id = "empty";
                _arv_category = "empty";
                _arv_sub_category = "empty";
                _arv_major_category = "empty";
                _arv_material = "empty";
                _arv_origin = "empty";
                _arv_photo = "empty";
                _arv_type = "empty";
                _arv_group = "empty";
                _arv_color = "empty";
                _arv_design = "empty";
                _arv_status = "empty";
                _arv_packing = "empty";
                _arv_collection = "empty";
                _arv_sole = "empty";
                _arv_upper = "empty";
                _arv_heeled = "empty";
                _arn_sellout = 0;
                _ard_create_date = DateTime.Now;
                _arv_articlegroups = "empty";
                _arn_commission = 0;

            }
        }

        #endregion
        #region < METODOS >

        public bool Save()
        {
            return Article.SaveArticle(this.arv_Co,
                        this.arv_Article, this.arv_Name, this.arv_Brand_Id, this.arv_Category,
                        this.arv_Sub_Category, this.arv_Major_Category, this.arv_Material,
                        this.arv_Origin, this.arv_Photo, this.arv_Type, this.arv_Group,
                        this.arv_Color, this.arv_Design, this.arv_Status, this.arv_Packing,
                        this.arv_Collection, this.arv_Sole, this.arv_Upper, this.arv_Heeled,
                        this.arn_Sellout, this.ard_Create_Date, this.arv_ArticleGroups, this.arn_Commission);
        }

        public bool SaveTaxes() { return false; }

        public bool SaveProcess() { return false; }

        #endregion

        #region < METODOS ESTATICOS >

        public static DataSet GetAllARTICLE()
        {
            DataSet ds = new DataSet();
            return ds;
            //// CURSOR REF
            //object results = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "SP_LOAQUARELLALL_ARTICLE";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);
            //return db.ExecuteDataSet(dbCommandWrapper);
        }

        public static bool AddSizeToArticle(string asv_co, string asv_article,
                        string asv_size_display)
        {
            return false;
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //bool result = false;
            //string sqlCommand = "SP_ADD_ARTICLESIZE";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, asv_co,
            //    asv_article, asv_size_display);
            //return result = ((db.ExecuteNonQuery(dbCommandWrapper)) > 0);
        }

        public static bool SaveArticle(string arv_Co,
                        string arv_Article, string arv_Name, string arv_Brand_Id, string arv_Category,
                        string arv_Sub_Category, string arv_Major_Category, string arv_Material,
                        string arv_Origin, string arv_Photo, string arv_Type, string arv_Group,
                        string arv_Color, string arv_Design, string arv_Status, string arv_Packing,
                        string arv_Collection, string arv_Sole, string arv_Upper, string arv_Heeled,
                        int arn_Sellout, DateTime ard_Create_Date, string arv_ArticleGroups, int arv_Commission)
        {
            return false;
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //bool result = false;
            //string sqlCommand = "SP_ADD_ARTICLE";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, arv_Co,
            //    arv_Article, arv_Name, arv_Brand_Id, arv_Category,
            //    arv_Sub_Category, arv_Major_Category, arv_Material,
            //    arv_Origin, arv_Photo, arv_Type, arv_Group,
            //    arv_Color, arv_Design, arv_Status, arv_Packing,
            //    arv_Collection, arv_Sole, arv_Upper, arv_Heeled,
            //    arn_Sellout, ard_Create_Date, arv_Commission);
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //        //// Adiciona cAQUARELLA talla en batch
            //        //foreach (Size Sizes in ArticleSizes)
            //        //{
            //        //    sqlCommand = "SP_ADD_ARTICLESIZE";
            //        //    dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //        //        arv_Co, arv_Article, Sizes.siv_size_display_id);
            //        //    db.ExecuteNonQuery(dbCommandWrapper);
            //        //}

            //        // Commit the transaction.
            //        transaction.Commit();

            //        result = true;
            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //    }
            //    connection.Close();
            //    return result;
            //}
        }

        public static bool SaveArticleLista(List<Article> lstarticle)
        {
            return false;
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //bool result = false;
            //string sqlCommandMaestros = "";

            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {

            //        foreach (Article item in lstarticle)
            //        {

            //            if (item.arv_Color == null || item.arv_Color == "")
            //            {
            //                item.arv_Color = "SIN ESPECIFICAR";
            //            }

            //            //**ahora actualizamos color
            //            string idcolor = "";
            //            sqlCommandMaestros = "MAESTROS.USP_ADD_COLOURS";
            //            DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommandMaestros,
            //                 item.arv_Color, idcolor);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            idcolor = (String)db.GetParameterValue(dbCommandWrapper, "P_COV_COLOUR_ID");

            //            ////

            //            //**ahora actualizamos mayorcategoria
            //            string idsubcat = "";

            //            //sqlCommandMaestros = "MAESTROS.USP_ADD_COLOURS";
            //            //dbCommandWrapper = db.GetStoredProcCommand(sqlCommandMaestros,
            //            //     item.arv_Color, idmajcatsub);
            //            //db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            sqlCommandMaestros = "MAESTROS.USP_ADD_MAJCATESUB";
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommandMaestros,
            //            item.arv_Major_Category, item.arv_Category, item.arv_Sub_Category, idsubcat);


            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            idsubcat = (String)db.GetParameterValue(dbCommandWrapper, "var_idsub");

            //            //item.arv_Major_Category, item.arv_Category, item.arv_Sub_Category);
            //            ////

            //            //idsubcat = "xxx";
            //            ////ahora articulo
            //            string sqlCommand = "MAESTROS.SP_ADD_ARTICLE";
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, item.arv_Co,
            //                item.arv_Article, item.arv_Name, item.arv_Brand_Id, item.arv_Category,
            //                idsubcat, item.arv_Major_Category, item.arv_Material,
            //                item.arv_Origin, item.arv_Photo, item.arv_Type, item.arv_Group,
            //                idcolor, item.arv_Design, item.arv_Status, item.arv_Packing,
            //                item.arv_Collection, item.arv_Sole, item.arv_Upper, item.arv_Heeled,
            //                item.arn_Sellout, item.ard_Create_Date, 1);

            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);


            //            //ahora ODV DE ARTICULO

            //            sqlCommand = "MAESTROS.USP_ADD_ODV";
            //            dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //                item.arv_Article, item.price);

            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //            // Commit the transaction.

            //        }
            //        transaction.Commit();

            //        result = true;
            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        connection.Close();
            //        result = false;
            //    }
            //}
            //return result;
        }

        public static bool SaveArticleprice(List<Article> lstarticle)
        {
            return false;
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //bool result = false;
            //string sqlCommandMaestros = "";

            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {

            //        foreach (Article item in lstarticle)
            //        {



            //            //**ahora actualizamos color

            //            sqlCommandMaestros = "MAESTROS.USP_ADD_PRICES";
            //            DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommandMaestros,
            //                 item.arv_Article, item.price);
            //            db.ExecuteNonQuery(dbCommandWrapper, transaction);



            //        }
            //        transaction.Commit();

            //        result = true;
            //    }
            //    catch
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        connection.Close();
            //        result = false;
            //    }
            //}
            //return result;
        }

        /// <summary>
        /// Consultar articulo por llave primaria
        /// </summary>
        /// <param name="arv_Articulo"></param>
        /// 
        /// <returns></returns>
        public static DataSet GetOneArticle(String arv_Articulo, String arv_co, String P_SUV_ID)
        {
            DataSet ds = new DataSet();
            return ds;
            //// CURSOR REF
            //object results = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);
            ///*string sqlCommand = "SELECT * FROM ARTICLE ART INNER JOIN ARTICLES_SUPPLIERS ART_S ON (ART.ARV_CO = ART_S.ASV_CO AND ART.ARV_ARTICLE = ART_S.ASV_ARTICLE ) " +
            //                                    " LEFT OUTER JOIN SUPPLIERS SPL ON (SPL.SUV_ID = ART_S.ASV_SUPPLIERS) WHERE ARV_ARTICLE = '" + arv_Articulo + "' AND ARV_CO = '" + arv_co + "' AND SPL.SUV_ID = '"+P_SUV_ID+"'";
            // * */
            //string sqlCommand = "sp_load_article_and_supplier";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, arv_Articulo, arv_co, P_SUV_ID, results);

            //// DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
            ////db.AddInParameter(dbCommandWrapper, "P_ARV_ARTICLE", DbType.AnsiString, arv_Articulo);
            ////db.AddInParameter(dbCommandWrapper, "P_ARV_CO", DbType.AnsiString, arv_co);

            ////DataTable t = db.ExecuteDataSet(dbCommandWrapper).Tables[0];

            //return db.ExecuteDataSet(dbCommandWrapper);
        }


        /// <summary>
        /// Método para consultar los articulos habilitados.
        /// </summary>
        /// <returns></returns>
        public static DataSet cargarArticles()
        {
            DataSet ds = new DataSet();
            return ds;

            //// CURSOR REF
            //object results = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //String sqlCommand = "SP_LOAD_VARTICLES";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);

            //return db.ExecuteDataSet(dbCommandWrapper);
        }
        /*
        /// <summary>
        /// Metodo que consulta un articulo dado, y devuelve una informacion especifica.
        /// Este método maneja un dataAQUARELLApter debido a que carga la información devuelta
        /// desde la base de datos en un Dataset XSD, el cual es provisto desde el llamado.                
        /// </summary>
        /// <param name="dtsOrders"></param>
        /// <param name="_articulo"></param>
        /// <param name="_company"></param>
        /// <returns></returns>
        public static DataSetPurchaseOrder cargarArticles(DataSetPurchaseOrder dtsOrders, String _articulo, String _company)
        {
            // CURSOR REF
            object results = new object[1];

            Database db = DatabaseFactory.CreateDatabase();
            String sqlCommand = "SP_LOAD_ARTICLE_FROMVARTICLE";
            //String sqlCommand = "SELECT arv_co, arv_article, arv_name,prn_public_price " +
            //                        " FROM maestros.v_articles WHERE arv_article = " + _articulo + " and arv_co = '" + _company + "' ";
            //DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
             
            ///
            DbDataAQUARELLApter custDA = db.GetDataAQUARELLApter();
            custDA.SelectCommand = db.GetStoredProcCommand(sqlCommand, _company, _articulo, results);//db.GetSqlStringCommand(sqlCommand);
            try
            {
                custDA.SelectCommand.Connection = db.CreateConnection();
                custDA.Fill(dtsOrders, "Articles");
                //custDA.SelectCommand.Connection.Close();

            }
            catch 
            { }
            return dtsOrders;
        }
        */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtsOrders"></param>
        /// <param name="_articulo"></param>
        /// <param name="_company"></param>
        /// <param name="_idPromoter"></param>
        /// <returns></returns>
        /* public static DataSetPurchaseOrder cargarArticles(DataSetPurchaseOrder dtsOrders, String _articulo, String _company, String _idCoordinator)
         {
             // CURSOR REF
             object results = new object[1];
             ///
             Database db = DatabaseFactory.CreateDatabase();
             String sqlCommand = "maestros.sp_load_article_pricepromotor";
             ///
             DbDataAQUARELLApter custDA = db.GetDataAQUARELLApter();
             custDA.SelectCommand = db.GetStoredProcCommand(sqlCommand, _company, _articulo, _idCoordinator, results);
             try
             {
                 custDA.SelectCommand.Connection = db.CreateConnection();
                 custDA.Fill(dtsOrders, "Articles");
                 //custDA.SelectCommand.Connection.Close();

             }
             catch (Exception e)
             { }
             return dtsOrders;
         }
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ARV_ARTICLE"></param>
        /// <param name="ARV_CO"></param>
        /// <returns></returns>
        public DataSet getArticleByRef(string ARV_ARTICLE, string ARV_CO)
        {
            return this.LoadByPrimaryKey(ARV_ARTICLE, ARV_CO);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="articleName"></param>
        /// <returns></returns>
        public static DataTable getsEqualsArticlesByNom(String company, String articleName)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);

            //    ///
            //    String sqlCommand = "maestros.sp_getarticlebyname";/*" SELECT                                                         " +
            //                        "    distinct ARV_CO, ARV_ARTICLE, initcap(ARV_NAME) ARV_NAME    " +
            //                        " FROM MAESTROS.V_ARTICLES                                       " +
            //                        "    where ARV_CO = '" + company + "' and upper(ARV_NAME)        " +
            //                        "    like upper('" + articleName + "%')                          " +
            //                        "    order by ARV_NAME";*/
            //    //"SP_LOADCOORDINATORSCOMISSIONS";
            //    ///
            //    //DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);///db.GetStoredProcCommand(sqlCommand, results);
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, company, articleName, results);
            //    ///
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
        /// <param name="_article"></param>
        /// <param name="_sizeDisplay"></param>
        /// <param name="_column"></param>
        /// <returns></returns>
        public static DataTable getInfoDecodifyCodeBars(String _company, String _article, String _sizeDisplay, String _column)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "maestros.sp_getinfocodebars";
            //    ///
            //    //DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);///db.GetStoredProcCommand(sqlCommand, results);
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _article, _sizeDisplay, _column, results);
            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //}
            //catch
            //{
            //    return null;
            //}
        }



        /// <summary>
        /// Buscar articulo por diferentes atributos.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="brand_id"></param>
        /// <param name="majorCategory_id"></param>
        /// <param name="category_id"></param>
        /// <param name="subCategory_id"></param>
        /// <param name="origin_id"></param>
        /// <param name="type_id"></param>
        /// <param name="article_id"></param>
        /// <param name="_supplierId"></param>
        /// <returns></returns>
        public static DataSet searchArticle(String company, String brand_id, String majorCategory_id, string category_id,
           String subCategory_id, String origin_id, String type_id, string article_id, String _supplierId)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    ///
            //    String _article = article_id.Equals(string.Empty) ? "%%" : article_id;
            //    ///
            //    String _brand = !brand_id.Equals(string.Empty) ? " AND UPPER(ARV_BRAND_ID) = UPPER('" + brand_id + "')" : "";
            //    ///
            //    String _majorCategory = !majorCategory_id.Equals(string.Empty) ? " AND UPPER(ARV_MAJOR_CATEGORY) = UPPER('" + majorCategory_id + "')" : "";
            //    ///
            //    String _category = !category_id.Equals(string.Empty) ? " AND UPPER(ARV_CATEGORY) = UPPER('" + category_id + "')" : "";
            //    ///
            //    String _subCategory = !subCategory_id.Equals(string.Empty) ? " AND UPPER(ARV_SUB_CATEGORY) = UPPER('" + subCategory_id + "')" : "";
            //    ///
            //    String _origin = !origin_id.Equals(string.Empty) ? " AND UPPER(ARV_ORIGIN) = UPPER('" + origin_id + "')" : "";
            //    ///
            //    String _type = !type_id.Equals(string.Empty) ? " AND UPPER(ARV_TYPE) = UPPER('" + type_id + "')" : "";
            //    ///            
            //    /// Linea adicionAQUARELLA para habilitar la busqueda por un proveedor
            //    String _supplier = !_supplierId.Equals("-1") ? " AND ASV_SUPPLIERS = '" + _supplierId + "' and UPPER(ASV_CO) = UPPER(ARV_CO) AND UPPER(ASV_ARTICLE) = UPPER(SOV_ARTICLE)" : "";
            //    ///
            //    ///
            //    String sentence = " ARV_ARTICLE LIKE '" + _article + "'" +
            //    " AND UPPER(ARV_CO) = UPPER('" + company + "')" +
            //        //" AND UPPER(SOV_WAREHOUSE) = UPPER('" + wareHouse + "')" +
            //        //" AND UPPER(SOV_STORAGE) = UPPER('" + storage_id + "')" +
            //    _brand + _majorCategory + _category + _subCategory + _origin + _type + _supplier;
            //    ///
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "maestros.sp_search_article";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, sentence, results);
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

        /// <summary>
        /// Consulta de articulos con precio y costo, aun cuando no tengas estos atributos configurados.
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_article"></param>
        /// <param name="_typeSentence"> 1 - consulta LIKE - 2 - Consulta IN</param>
        /// <returns></returns>
        public static DataTable verifyArticle(String _company, String _article, String _typeSentence)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "maestros.sp_verifyarticle";
            //    ///
            //    String sentence = " AND UPPER(ARV_CO) = UPPER('" + _company + "') ";
            //    ///
            //    if (_typeSentence.Equals("1"))
            //        ///
            //        sentence = " ARV_ARTICLE LIKE '" + _article + "'" + sentence;
            //    else if (_typeSentence.Equals("2"))
            //        ///
            //        sentence = " ARV_ARTICLE IN (" + _article + ") " + sentence;
            //    //DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);///db.GetStoredProcCommand(sqlCommand, results);
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, sentence, results);
            //    ///
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
        /// <param name="_article"></param>
        /// <param name="_warehouse"></param>
        /// <returns></returns>
        public static DataTable getArticleStockAndSales(String _company, String _article, String _warehouse)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "logistica.sp_getstocksalesarticle";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _article, _warehouse, results);
            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //}
            //catch
            //{ }
            //return null;
        }

        /// <summary>
        /// Verficar que los articulos que llegan como transferencia existan en aquarella
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_article"></param>
        /// <param name="_warehouse"></param>
        /// <returns></returns>
        public static DataTable verifyArtsToTransferExistInAqua(String _noTransfer)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "lepalacio.veriartstotransexistinaqua";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _noTransfer, results);
            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //}
            //catch
            //{ }
            //return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_seccion"></param>
        /// <param name="_date"></param>
        /// <returns></returns>
        public static DataTable verifyArtsProductionExistInAqua(String _seccion)
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];
            //    ///
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    String sqlCommand = "LEPALACIO.veriartsproducexistinaqua";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _seccion, results);
            //    ///
            //    return db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //}
            //catch { return null; }

        }

        public static DataSet lista_articulo_precio(DataTable dt)
        {
            string sqlquery = "USP_Buscar_Lista_ArticuloPrecio";
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
                cmd.Parameters.AddWithValue("@tmp_precio", dt);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }

        public static DataTable gettipo_precio()
        {
            DataTable dt = null;
            string sqlquery = "USP_LeerTipoPrecio";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.myconexion()))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception)
            {
                dt = null;
                
            }
            return dt;
        }
        public static DataSet fgetarticleprecio(string _article,string _tipo)
        {
            string sqlquery = "USP_Buscar_ArticuloPrecio";
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
                cmd.Parameters.AddWithValue("@art_id", _article);
                cmd.Parameters.AddWithValue("@tip", _tipo);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch { return null; }
        }

        public static string[] fupdatearticuloprecio(DataTable dtactualiza,decimal _usu_id,string _usu_nombre)
        {
            string sqlquery = "USP_Modificar_ArticuloPrecio";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string[] resultDoc = new string[2];
            //return resultDoc;
            
            try
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("tipo", typeof(string));
                dt.Columns.Add("Art_Id", typeof(string));
                dt.Columns.Add("Art_Precio", typeof(Decimal));

                for (Int32 i = 0; i < dtactualiza.Rows.Count; ++i)
                {
                    string vtipo= dtactualiza.Rows[i]["tipo"].ToString();
                    string varticulo = dtactualiza.Rows[i]["articulo"].ToString();
                    Decimal vprecio = Convert.ToDecimal(dtactualiza.Rows[i]["precion"].ToString());
                    dt.Rows.Add(vtipo, varticulo, vprecio);
                }

                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_id", _usu_id);
                cmd.Parameters.AddWithValue("@usu_nombre", _usu_nombre);
                cmd.Parameters.AddWithValue("@tabla_precio",dt);
                cmd.ExecuteNonQuery();
                resultDoc[0] = "";

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(SqlCommand);
            //    using (DbConnection connection = db.CreateConnection())
            //    {
            //        connection.Open();
            //        DbTransaction transaction = connection.BeginTransaction();
            //        try
            //        {
            //            for (Int32 i = 0; i < dtactualiza.Rows.Count; ++i)
            //            {
            //                string varticulo=dtactualiza.Rows[i]["articulo"].ToString();
            //                Decimal vprecio=Convert.ToDecimal(dtactualiza.Rows[i]["precion"].ToString());
            //                dbCommandWrapper = db.GetStoredProcCommand(SqlCommand, varticulo, vprecio);
            //                db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //            }
            //            transaction.Commit();
            //            resultDoc[0] = "";
            //        }
            //        catch (Exception exc)
            //        {
            //            transaction.Rollback();
            //            resultDoc[0] = "-1";
            //            resultDoc[1] = exc.Message;//"-1";
            //        }
            //        connection.Close();
            //    }
            }
            catch (Exception ex)
            {
                resultDoc[0] = "-1";
                resultDoc[1] = ex.Message;
            }
            return resultDoc;
        }

        public static DataSet get_listaprecios()
        {
            string sqlquery = "USP_ConsultaListaPrecios";
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
        /// Consultar un articulo, informacion y tallas
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_code"></param>
        /// <returns>Dos Tablas, [0] -> Informacion articulo, [1] -> Tallas activas</returns>
        public static DataSet getArticle(string _code,decimal _bas_id)
        {
            string sqlquery = "USP_Leer_Articulo";
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
                cmd.Parameters.AddWithValue("@Art_Id", _code);
                cmd.Parameters.AddWithValue("@bas_id", _bas_id);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch { return null; }
        }
        public static DataTable getcategoria()
        {
            string sqlquery = "USP_LeerCategoria";
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

        #endregion

        #region < METODOS PROPIOS >
        /// <summary>
        /// Permite comprovar la exixtencia de un articulo.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="article"></param>
        /// <returns></returns>
        static public bool existArticle(string company, string article)
        {
            Article a = new Article();

            a.Load(article, company);
            if (!a.arv_Co.Equals(string.Empty) && !a.arv_Article.Equals(string.Empty))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
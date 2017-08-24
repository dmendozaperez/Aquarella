using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Control;
namespace www.aquarella.com.pe.bll
{
    public class Users
    {
        #region < Atributos >


        //modifcacion depuracion de codigo
        public Int32 _bas_id { set; get; }
        public string _usu_nombre { set; get; }
        public string _usu_contraseña { set; get; }
        public string _usu_est_id { set; get; }
        public string _nombre { set; get; }
        public string _usu_tip_id { set; get; }
        public string _usu_tip_nombre { set; get; }

        public string _asesor { set; get; }

        //*************************


        /// <summary>
        /// Identificador de compañia
        /// </summary>
        public string _usv_co { set; get; }
        /// <summary>
        /// Identificador de usuario
        /// </summary>
        public decimal _usn_userid { set; get; }
        /// <summary>
        /// Nick de usuario
        /// </summary>
        public string _usv_username { set; get; }
        /// <summary>
        /// Nombre completo de usuario
        /// </summary>
        public string _usv_name { set; get; }
        /// <summary>
        /// Pass encriptado
        /// </summary>
        public string _usv_password { set; get; }
        /// <summary>
        /// Pregunta de recuperacion de pass
        /// </summary>
        public string _usv_question { set; get; }
        /// <summary>
        /// Respuesta de recuperacion de pass
        /// </summary>
        public string _usv_answer { set; get; }
        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime _usd_creation { set; get; }
        /// <summary>
        /// Estado del usuario
        /// </summary>
        public string _usv_status { set; get; }
        /// <summary>
        /// Fecha ultimo acceso
        /// </summary>
        public DateTime _usd_lastaccess { set; get; }
        /// <summary>
        /// Bandera de es usuario nuevo
        /// </summary>
        public bool _isnew { set; get; }

        /// <summary>
        /// Bandera de es o no empleado
        /// </summary>
        public bool _usv_employee { set; get; }
        /// <summary>
        /// Bandera de es o no cliente
        /// </summary>
        public bool _usv_customer { set; get; }
        /// <summary>
        /// Bodega del usuario
        /// </summary>
        public string _usv_warehouse { set; get; }
        /// <summary>
        /// Descripcion de bodega del usuario
        /// </summary>
        public string _usv_warehouse_name { set; get; }
        /// <summary>
        /// Area del usuario
        /// </summary>
        public string _usv_area { set; get; }
        /// <summary>
        /// Region del usuario, una region puede contener varias areas.
        /// </summary>
        public string _usv_region { get; set; }
        /// <summary>
        /// Descripcion de nivel de usuario: cliente, lider , lider regional
        public string _usv_nivel { get; set; }
        /// Descripcion del area de usuario
        /// </summary>
        public string _usv_area_name { set; get; }
        /// <summary>
        /// Direccion de correo BasicData
        /// </summary>
        public string _usv_email { get; set; }

        /// <summary>
        /// Objeto de info de visitante
        /// </summary>
        public Log_Transaction _logTx { get; set; }

        public int _attempts { get; set; }

        public string _usv_postpago { get; set; }

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < Metodos publicos >

        /// <summary>
        /// Retorna todos los roles del Usurio especificado.
        /// </summary>
        /// <param name="USN_USERID"></param>
        /// <param name="USV_CO"></param>
        /// <returns></returns>
        public DataSet getAllRoles(Decimal USN_USERID, string USV_CO)
        {
            DataSet ds = new DataSet();
            return ds;
            // CURSOR REF
            //object results = new object[1];
            /////
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            /////
            //string sqlCommand = "SP_LOADROLES_US";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, USN_USERID, USV_CO, results);
            ////return ALL APPLICATIONS COMPANY AND FUCTION REQUIRED 
            //return db.ExecuteDataSet(dbCommandWrapper);
        }

        public bool Update()
        {           
            string sqlquery = "USP_Modificar_Usuario";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_id", _usn_userid);
                cmd.Parameters.AddWithValue("@usu_nombre", _usv_username);
                cmd.Parameters.AddWithValue("@contraseña", _usv_password);
                cmd.Parameters.AddWithValue("@usu_fecha_cre", _usd_creation);
                cmd.Parameters.AddWithValue("@usu_est_id", _usv_status);
                cmd.ExecuteNonQuery();
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Consultar y cargar un usuario de acuerdo al nombre de usuario digitado
        /// </summary>
        /// <param name="_usv_username"></param>
        /// <returns></returns>
        public static DataTable F_LeerUsuario(string _usv_username)
        {
            DataTable dt = null;
            SqlConnection cn = null;
            SqlCommand cmd=null;
            SqlDataAdapter da = null;
            string sqlcommand = "USP_Leer_Usuario";
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlcommand, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Usu_Nombre", _usv_username);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;            
        }

        /// <summary>
        /// Crear usuario para un nuevo cliente
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idCust"></param>
        /// <param name="_mail"></param>
        /// <param name="_pass"></param>
        /// <returns></returns>
        public static string createUserCustomer(string _co, decimal _idCust, string _mail, string _pass)
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string sqlCommand = "control.sp_add_user_customer";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_usv_co", DbType.String, _co);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_usn_userid", DbType.Decimal, _idCust);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_usv_username", DbType.String, _mail);
            ////
            //db.AddInParameter(dbCommandWrapper, "p_usv_password", DbType.String, _pass);

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        // Commit the transaction.
            //        transaction.Commit();
            //        return "1";
            //    }
            //    catch (Exception e)
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        throw new Exception(e.Message, e.InnerException);
            //    }
            //}

        }


        public static string createUserLider(string _co, decimal _idCust, string _mail, string _pass)
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string sqlCommand = "control.sp_add_user_Lider";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_usv_co", DbType.String, _co);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_usn_userid", DbType.Decimal, _idCust);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_usv_username", DbType.String, _mail);
            ////
            //db.AddInParameter(dbCommandWrapper, "p_usv_password", DbType.String, _pass);

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        // Commit the transaction.
            //        transaction.Commit();
            //        return "1";
            //    }
            //    catch (Exception e)
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        throw new Exception(e.Message, e.InnerException);
            //    }
            //}

        }

        public static DataSet GetAllUsers()
        {
            DataSet ds = new DataSet();
            return ds;
            //object result = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlComand = "control.SP_LOAQUARELLALL_USERS";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlComand, result);

            //return db.ExecuteDataSet(dbCommandWrapper);
        }

        public static DataSet GetUserByDocument(string _document)
        {
            string sqlquery = "USP_Leer_Usuario_Doc";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bas_documento", _document);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;            
        }

        /// <summary>
        /// Consulta el Usuario Control
        /// </summary>
        /// <param name="_usv_co">Identificador compañia</param>
        /// <param name="_usn_id">id basic data</param>
        /// <returns>Usuario Control</returns>
        public static DataSet GetUserControlById(decimal _usn_id)
        {
            string sqlquery = "USP_Leer_Data_Usuario";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bas_id", _usn_id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;            
        }

        public static DataSet GetUsersByName(string _username)
        {
            string sqlquery = "USP_Leer_Usuario_Nombre";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bas_primer_nombre", _username);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;            
        }

        public static decimal GetUserIdByUsername(string USV_USERNAME)
        {
            return 0;
            //object results = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlCommand = "control.SP_GETUSERID_US";
            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, USV_USERNAME, results);

            //decimal ret;
            //try
            //{
            //    DataRow user = db.ExecuteDataSet(dbCommandWrapper).Tables[0].Rows[0];
            //    ret = Convert.ToDecimal(user["USN_USERID"]);
            //}
            //catch { ret = 0; }

            //return ret;
        }

        /// <summary> Consulta el Usuario en Control
        /// </summary>
        /// <param name="_usn_id">Identificador del Usuario</param>
        /// <returns>Retorna una entidad Usuario con la informacion del usuario encontrado</returns>
        public static Users GetUser_Id(decimal _usn_id)
        {
            Users _USER = new Users();
            return _USER;
            //object result = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlComand = "control.SP_LOADPK_USERS";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlComand, _usn_id, result);

            //DataSet data = db.ExecuteDataSet(dbCommandWrapper);

            //Users _USER = new Users();

            //try
            //{
            //    DataRow row = data.Tables[0].Rows[0];
            //    _USER._usv_co = (string)row["USV_CO"];
            //    _USER._usn_userid = (decimal)row["USN_USERID"];
            //    _USER._usv_username = (string)row["USV_USERNAME"];
            //    _USER._usv_password = (string)row["USV_PASSWORD"];
            //    _USER._usv_question = row.IsNull("USV_QUESTION") ? string.Empty : (string)row["USV_QUESTION"];
            //    _USER._usv_answer = row.IsNull("USV_ANSWER") ? string.Empty : (string)row["USV_ANSWER"];
            //    _USER._usd_creation = (DateTime)row["USD_CREATION"];
            //    _USER._usv_status = (string)row["USV_STATUS"];
            //    _USER._usd_lastaccess = row.IsNull("USD_LASTACCESS") ? DateTime.Parse("01/01/1900") : (DateTime)row["USD_LASTACCESS"];
            //    _USER._usv_email = (string)row["BDV_EMAIL"];

            //    return _USER;
            //}
            //catch (Exception)
            //{
            //    return _USER;
            //}
        }

        public static DataSet GetUserBasicDataByDocument(string _bdv_co, string _bdv_document_co)
        {
            DataSet ds = new DataSet();
            return ds;
            //object result = new object[1];

            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlComand = "CONTROL.SP_GET_USER_BASICDATA";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlComand, _bdv_co, _bdv_document_co, result);

            //return db.ExecuteDataSet(dbCommandWrapper);
        }

        public static bool insertUserToControl(string _usv_co, decimal _usn_userid, string _usv_username, string _usv_password)
        {
            return false;
            //Database db = DatabaseFactory.CreateDatabase(_conn);

            //string sqlComand = "CONTROL.SP_ADD_USER";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlComand, _usv_co, _usn_userid, _usv_username, _usv_password);

            //try
            //{
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
        }

        #endregion

    }
}
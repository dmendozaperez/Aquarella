using System;
using System.Data;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.bll
{
    public class Coordinator
    {
        #region < Atributos >

        public string _vartipopago { get; set; }

        public string _co { get; set; }
        public decimal _idCust { get; set; }
        public decimal _commission { get; set; }
        public string _idWare { get; set; }
        public decimal _taxRate { get; set; }
        public decimal _commission_POS_visaUnica { get; set; }
        public decimal _percepcion { get; set; }

        public decimal _mtopercepcion { get; set; }

        public string _email { get; set; }

        public string _nombrecompleto { get; set; }

        public string _premio { get; set; }
        public string _ppremio { get; set; }
        public string _pTalla { get; set; }
        public Int32 _pCantidad { get; set; }
        public Decimal _pMonto { get; set; }

        public string _pPremID { get; set; }

        public Boolean _aplica_percepcion { get; set; }
        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        /// 

        //para ver el total de documento sin percepcion
        public decimal _mtoimporte { get; set; }
       

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Consultar coordinadores
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idWare"></param>
        /// <param name="_areaId"></param>
        /// <returns></returns>
        public static DataSet getCoordinators(string _areaId,string _asesor)
        {
            string sqlquery = "USP_Leer_Promotor_Lider";
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
                cmd.Parameters.AddWithValue("@bas_are_id", _areaId);
                cmd.Parameters.AddWithValue("@asesor", _asesor);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consultar coordinador por id
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idCoord"></param>
        /// <returns></returns>
        public static DataSet getCoordinatorByPk(decimal _idCoord)
        {
            string sqlquery = "USP_Leer_Persona_Usuario";
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
                cmd.Parameters.AddWithValue("@bas_id", _idCoord);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consulta de pedidos en borrador, historial de liquidaciones y devoluciones
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idCust"></param>
        /// <returns>3 datasets 0-> Pedidos borrador, 1-> Liquidaciones, 2-> Devoluciones</returns>
        public static DataSet getOrdLiqAnsRet(decimal _idCust)
        {
            string sqlquery = "USP_Leer_Pedido_Usuario";
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

        public static DataSet getOrdLiqLider(DateTime date_ini, DateTime date_fin, decimal _idCust)
        {
            string sqlquery = "USP_Leer_Liquidacion_Lider";
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
                cmd.Parameters.AddWithValue("@fecha_inicio", date_ini);
                cmd.Parameters.AddWithValue("@fecha_final", date_fin);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static string setPreCrucePago(string _list_liquidations, DataTable dt)
        {
            //return "";
            string clearId = string.Empty;
            string sqlquery = "USP_Pre_Cruce_Pago";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@liq_id", _list_liquidations);
                cmd.Parameters.AddWithValue("@gru_id_devolver", DbType.String);
                cmd.Parameters.AddWithValue("@Tmp_Pago", dt);
                cmd.Parameters["@gru_id_devolver"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                clearId = Convert.ToString(cmd.Parameters["@gru_id_devolver"].Value);
                return clearId;

                //Database db = DatabaseFactory.CreateDatabase(_conn);
                ////                
                //string sqlCommand = "financiera.sp_pre_clear";
                ////
                //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _list_liquidations, _list_documentrans, clearId);
                ////
                //db.ExecuteNonQuery(dbCommandWrapper);
                //clearId = db.GetParameterValue(dbCommandWrapper, "p_clv_clear_id").ToString();

                //return clearId;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Crear un nuevo cliente
        /// </summary>
        /// <param name="BDV_CO"></param>
        /// <param name="BDV_FIRST_NAME"></param>
        /// <param name="BDV_MIDDLE_NAME"></param>
        /// <param name="BDV_FIRST_SURNAME"></param>
        /// <param name="BDV_SECOND_SURNAME"></param>
        /// <param name="BDD_BIRTHDAY"></param>
        /// <param name="BDV_DOCUMENT_NO"></param>
        /// <param name="BDV_VERIF_DIGIT_NO"></param>
        /// <param name="BDV_DOCUMENT_TYPE_ID"></param>
        /// <param name="BDV_PERSON_TYPE_ID"></param>
        /// <param name="BDV_ADDRESS"></param>
        /// <param name="BDV_PHONE"></param>
        /// <param name="BDV_FAX"></param>
        /// <param name="BDV_MOVIL_PHONE"></param>
        /// <param name="BDV_EMAIL"></param>
        /// <param name="BDV_CITY_CD"></param>
        /// <param name="BDV_STATUS"></param>
        /// <param name="BDV_LANGUAGE_ID"></param>
        /// <param name="BDV_AREA_ID"></param>
        /// <param name="BDV_CREATE_USER"></param>
        /// <param name="BDV_UPDATE_USER"></param>
        /// <param name="BDV_SEX"></param>
        /// <param name="p_COV_COORDINATOR_TYPE"></param>
        /// <param name="p_CON_TERM_PAY_ID"></param>
        /// <param name="p_COV_DELIVERY_TEM_ID"></param>
        /// <param name="p_COV_CURRENCY_ID"></param>
        /// <param name="p_COV_WAREHOUSEID"></param>
        /// <param name="p_COV_BANK_ID"></param>
        /// <param name="p_COV_BANK_ACCOUNT_NO"></param>
        /// <param name="p_COV_APPROVAL_NAME"></param>
        /// <param name="p_COV_HANDLING_ID"></param>
        /// <param name="p_COV_CREDIT_FLAG"></param>
        /// <param name="p_CON_CREDIT_LIMIT"></param>
        /// <param name="p_COV_TAX_ID"></param>
        /// <param name="p_COV_AUTORETAINER_FLAG"></param>
        /// <param name="p_COV_GREAT_TAXPAYERS_FLAG"></param>
        /// <param name="p_con_perso_invoice"></param>
        /// <param name="con_regime"></param>
        /// <returns></returns>
        public static string addCoordinador
            (
                    string BDV_CO,
                    string BDV_FIRST_NAME,
                    string BDV_MIDDLE_NAME,
                    string BDV_FIRST_SURNAME,
                    string BDV_SECOND_SURNAME,
                    DateTime BDD_BIRTHDAY,
                    string BDV_DOCUMENT_NO,
                    string BDV_VERIF_DIGIT_NO,
                    string BDV_DOCUMENT_TYPE_ID,
                    string BDV_PERSON_TYPE_ID,
                    string BDV_ADDRESS,
                    string BDV_PHONE,
                    string BDV_FAX,
                    string BDV_MOVIL_PHONE,
                    string BDV_EMAIL,
                    string BDV_CITY_CD,
                    string BDV_STATUS,
                    string BDV_LANGUAGE_ID,
                    string BDV_AREA_ID,
                    string BDV_CREATE_USER,
                    string BDV_UPDATE_USER,
                    string BDV_SEX,
            // Atributos de coordinador
                    string p_COV_COORDINATOR_TYPE,
                    string p_CON_TERM_PAY_ID,
                    string p_COV_DELIVERY_TEM_ID,
                    string p_COV_CURRENCY_ID,
                    string p_COV_WAREHOUSEID,
                    string p_COV_BANK_ID,
                    string p_COV_BANK_ACCOUNT_NO,
                    string p_COV_APPROVAL_NAME,
                    string p_COV_HANDLING_ID,
                    string p_COV_CREDIT_FLAG,
                    string p_CON_CREDIT_LIMIT,
                    string p_COV_AUTORETAINER_FLAG,
                    string p_COV_GREAT_TAXPAYERS_FLAG)
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string resultDoc = "";
            //string sqlCommand = "maestros.SP_ADD_BASIC_DATA";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CO", DbType.String, BDV_CO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FIRST_NAME", DbType.String, BDV_FIRST_NAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_MIDDLE_NAME", DbType.String, BDV_MIDDLE_NAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FIRST_SURNAME", DbType.String, BDV_FIRST_SURNAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_SECOND_SURNAME", DbType.String, BDV_SECOND_SURNAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDD_BIRTHDAY", DbType.DateTime, BDD_BIRTHDAY);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_DOCUMENT_NO", DbType.String, BDV_DOCUMENT_NO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_VERIF_DIGIT_NO", DbType.String, BDV_VERIF_DIGIT_NO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_DOCUMENT_TYPE_ID", DbType.String, BDV_DOCUMENT_TYPE_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_PERSON_TYPE_ID", DbType.String, BDV_PERSON_TYPE_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_ADDRESS", DbType.String, BDV_ADDRESS);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_PHONE", DbType.String, BDV_PHONE);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FAX", DbType.String, BDV_FAX);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_MOVIL_PHONE", DbType.String, BDV_MOVIL_PHONE);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_EMAIL", DbType.String, BDV_EMAIL);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CITY_CD", DbType.String, BDV_CITY_CD);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_STATUS", DbType.String, BDV_STATUS);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_LANGUAGE_ID", DbType.String, BDV_LANGUAGE_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_AREA_ID", DbType.String, BDV_AREA_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CREATE_USER", DbType.String, BDV_CREATE_USER);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_UPDATE_USER", DbType.String, BDV_UPDATE_USER);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_SEX", DbType.String, BDV_SEX);
            /////

            ///// Output parameters specify the size of the return data.            
            ///// 
            //db.AddOutParameter(dbCommandWrapper, "p_BDN_ID", DbType.Decimal, 12);

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();
            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        ///
            //        resultDoc = (String)Convert.ToString(db.GetParameterValue(dbCommandWrapper, "p_BDN_ID"));

            //        sqlCommand = "admonred.SP_ADD_COORDINATOR";
            //        dbCommandWrapper = db.GetStoredProcCommand(sqlCommand,
            //            BDV_CO,
            //            resultDoc,
            //            p_COV_COORDINATOR_TYPE,
            //            p_CON_TERM_PAY_ID,
            //            p_COV_DELIVERY_TEM_ID,
            //            p_COV_CURRENCY_ID,
            //            p_COV_WAREHOUSEID,
            //            p_COV_BANK_ID,
            //            p_COV_BANK_ACCOUNT_NO,
            //            p_COV_APPROVAL_NAME,
            //            p_COV_HANDLING_ID,
            //            p_COV_CREDIT_FLAG,
            //            p_CON_CREDIT_LIMIT,
            //            p_COV_AUTORETAINER_FLAG,
            //            p_COV_GREAT_TAXPAYERS_FLAG);
            //        //
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);

            //        // Commit the transaction.
            //        transaction.Commit();
            //        //
            //        return resultDoc;
            //    }
            //    catch (Exception e)
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        throw new Exception(e.Message, e.InnerException);
            //    }
            //}
        }


        public static string addNewusers
       (
               string BDV_CO,
               string BDV_FIRST_NAME,
               string BDV_MIDDLE_NAME,
               string BDV_FIRST_SURNAME,
               string BDV_SECOND_SURNAME,
               DateTime BDD_BIRTHDAY,
               string BDV_DOCUMENT_NO,
               string BDV_VERIF_DIGIT_NO,
               string BDV_DOCUMENT_TYPE_ID,
               string BDV_PERSON_TYPE_ID,
               string BDV_ADDRESS,
               string BDV_PHONE,
               string BDV_FAX,
               string BDV_MOVIL_PHONE,
               string BDV_EMAIL,
               string BDV_CITY_CD,
               string BDV_STATUS,
               string BDV_LANGUAGE_ID,
               string BDV_AREA_ID,
               string BDV_CREATE_USER,
               string BDV_UPDATE_USER,
               string BDV_SEX,
            // Atributos de coordinador
               string p_COV_COORDINATOR_TYPE,
               string p_CON_TERM_PAY_ID,
               string p_COV_DELIVERY_TEM_ID,
               string p_COV_CURRENCY_ID,
               string p_COV_WAREHOUSEID,
               string p_COV_BANK_ID,
               string p_COV_BANK_ACCOUNT_NO,
               string p_COV_APPROVAL_NAME,
               string p_COV_HANDLING_ID,
               string p_COV_CREDIT_FLAG,
               string p_CON_CREDIT_LIMIT,
               string p_COV_AUTORETAINER_FLAG,
               string p_COV_GREAT_TAXPAYERS_FLAG)
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string resultDoc = "";
            //string sqlCommand = "Maestros.sp_add_basic_data_users_C";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CO", DbType.String, BDV_CO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FIRST_NAME", DbType.String, BDV_FIRST_NAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_MIDDLE_NAME", DbType.String, BDV_MIDDLE_NAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FIRST_SURNAME", DbType.String, BDV_FIRST_SURNAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_SECOND_SURNAME", DbType.String, BDV_SECOND_SURNAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDD_BIRTHDAY", DbType.DateTime, BDD_BIRTHDAY);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_DOCUMENT_NO", DbType.String, BDV_DOCUMENT_NO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_VERIF_DIGIT_NO", DbType.String, BDV_VERIF_DIGIT_NO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_DOCUMENT_TYPE_ID", DbType.String, BDV_DOCUMENT_TYPE_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_PERSON_TYPE_ID", DbType.String, BDV_PERSON_TYPE_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_ADDRESS", DbType.String, BDV_ADDRESS);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_PHONE", DbType.String, BDV_PHONE);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FAX", DbType.String, BDV_FAX);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_MOVIL_PHONE", DbType.String, BDV_MOVIL_PHONE);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_EMAIL", DbType.String, BDV_EMAIL);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CITY_CD", DbType.String, BDV_CITY_CD);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_STATUS", DbType.String, BDV_STATUS);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_LANGUAGE_ID", DbType.String, BDV_LANGUAGE_ID);
            /////
            //if (BDV_AREA_ID == "-1")
            //{
            //    BDV_AREA_ID = "0";
            //}
            //db.AddInParameter(dbCommandWrapper, "p_BDV_AREA_ID", DbType.String, BDV_AREA_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CREATE_USER", DbType.String, BDV_CREATE_USER);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_UPDATE_USER", DbType.String, BDV_UPDATE_USER);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_SEX", DbType.String, BDV_SEX);
            /////

            ///// Output parameters specify the size of the return data.            
            ///// 
            //db.AddOutParameter(dbCommandWrapper, "p_BDN_ID", DbType.Decimal, 12);

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();
            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        ///
            //        resultDoc = (String)Convert.ToString(db.GetParameterValue(dbCommandWrapper, "p_BDN_ID"));

            //        // Commit the transaction.
            //        transaction.Commit();
            //        //
            //        return resultDoc;
            //    }
            //    catch (Exception e)
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        throw new Exception(e.Message, e.InnerException);
            //    }
            //}
        }


        public static string addNewLider
        (
                string BDV_CO,
                string BDV_FIRST_NAME,
                string BDV_MIDDLE_NAME,
                string BDV_FIRST_SURNAME,
                string BDV_SECOND_SURNAME,
                DateTime BDD_BIRTHDAY,
                string BDV_DOCUMENT_NO,
                string BDV_VERIF_DIGIT_NO,
                string BDV_DOCUMENT_TYPE_ID,
                string BDV_PERSON_TYPE_ID,
                string BDV_ADDRESS,
                string BDV_PHONE,
                string BDV_FAX,
                string BDV_MOVIL_PHONE,
                string BDV_EMAIL,
                string BDV_CITY_CD,
                string BDV_STATUS,
                string BDV_LANGUAGE_ID,
                string BDV_AREA_ID,
                string BDV_CREATE_USER,
                string BDV_UPDATE_USER,
                string BDV_SEX,
            // Atributos de coordinador
                string p_COV_COORDINATOR_TYPE,
                string p_CON_TERM_PAY_ID,
                string p_COV_DELIVERY_TEM_ID,
                string p_COV_CURRENCY_ID,
                string p_COV_WAREHOUSEID,
                string p_COV_BANK_ID,
                string p_COV_BANK_ACCOUNT_NO,
                string p_COV_APPROVAL_NAME,
                string p_COV_HANDLING_ID,
                string p_COV_CREDIT_FLAG,
                string p_CON_CREDIT_LIMIT,
                string p_COV_AUTORETAINER_FLAG,
                string p_COV_GREAT_TAXPAYERS_FLAG)
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string resultDoc = "";
            //string sqlCommand = "maestros.sp_add_basic_data_lider";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

            //// Recoleccion de la informacion necesaria para crear el registro de la cabecera del pedido
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CO", DbType.String, BDV_CO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FIRST_NAME", DbType.String, BDV_FIRST_NAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_MIDDLE_NAME", DbType.String, BDV_MIDDLE_NAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FIRST_SURNAME", DbType.String, BDV_FIRST_SURNAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_SECOND_SURNAME", DbType.String, BDV_SECOND_SURNAME);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDD_BIRTHDAY", DbType.DateTime, BDD_BIRTHDAY);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_DOCUMENT_NO", DbType.String, BDV_DOCUMENT_NO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_VERIF_DIGIT_NO", DbType.String, BDV_VERIF_DIGIT_NO);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_DOCUMENT_TYPE_ID", DbType.String, BDV_DOCUMENT_TYPE_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_PERSON_TYPE_ID", DbType.String, BDV_PERSON_TYPE_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_ADDRESS", DbType.String, BDV_ADDRESS);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_PHONE", DbType.String, BDV_PHONE);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_FAX", DbType.String, BDV_FAX);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_MOVIL_PHONE", DbType.String, BDV_MOVIL_PHONE);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_EMAIL", DbType.String, BDV_EMAIL);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CITY_CD", DbType.String, BDV_CITY_CD);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_STATUS", DbType.String, BDV_STATUS);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_LANGUAGE_ID", DbType.String, BDV_LANGUAGE_ID);
            /////
            //if (BDV_AREA_ID == "-1")
            //{
            //    BDV_AREA_ID = "0";
            //}
            //db.AddInParameter(dbCommandWrapper, "p_BDV_AREA_ID", DbType.String, BDV_AREA_ID);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_CREATE_USER", DbType.String, BDV_CREATE_USER);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_UPDATE_USER", DbType.String, BDV_UPDATE_USER);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_SEX", DbType.String, BDV_SEX);
            /////

            ///// Output parameters specify the size of the return data.            
            ///// 
            //db.AddOutParameter(dbCommandWrapper, "p_BDN_ID", DbType.Decimal, 12);

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();
            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        ///
            //        resultDoc = (String)Convert.ToString(db.GetParameterValue(dbCommandWrapper, "p_BDN_ID"));

            //        // Commit the transaction.
            //        transaction.Commit();
            //        //
            //        return resultDoc;
            //    }
            //    catch (Exception e)
            //    {
            //        // Roll back the transaction. 
            //        transaction.Rollback();
            //        throw new Exception(e.Message, e.InnerException);
            //    }
            //}
        }

        /// <summary>
        /// Actualizacion de coordinador
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idPerson"></param>
        /// <param name="p_COV_COORDINATOR_TYPE"></param>
        /// <param name="p_COV_WAREHOUSEID"></param>
        /// <param name="p_COV_HANDLING_ID"></param>
        /// <param name="p_COV_TAX_ID"></param>
        /// <param name="p_con_perso_invoice"></param>
        /// <param name="p_con_regime"></param>
        /// <returns></returns>
        public static string updateCoordinator(string _company, string _idPerson,
                    string p_COV_COORDINATOR_TYPE,
                    string p_COV_WAREHOUSEID,
                    string p_COV_HANDLING_ID)
        {
            return "";
        //    try
        //    {
        //        // CURSOR REF
        //        object results = new object[1];

        //        Database db = DatabaseFactory.CreateDatabase(_conn);
        //        string sqlCommand = "admonred.sp_updatecoordinator";
        //        //
        //        DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _idPerson, p_COV_COORDINATOR_TYPE, p_COV_WAREHOUSEID,
        //            p_COV_HANDLING_ID);
        //        //
        //        db.ExecuteNonQuery(dbCommandWrapper);
        //        //
        //        return "1";
        //    }
        //    catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consultar atributos del coordinador
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_idCoord"></param>
        /// <returns></returns>
        public static DataSet getCoordinator(string _co, decimal _idCoord)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "admonred.sp_getcoordinator";
            //    // CURSOR REF
            //    object results = new object[1];
            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///                
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _idCoord, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consulta de venta de un cliente, entre rangos de fechas y por cedula
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_document"></param>
        /// <param name="_dateStart"></param>
        /// <param name="_dateEnd"></param>
        /// <returns></returns>
        public static DataSet getSalesByCustomer(string _co, string _document, string _dateStart, string _dateEnd)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "ventas.sp_getsales_bycustomer";
            //    // CURSOR REF
            //    object results = new object[1];
            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///                
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _document, _dateStart, _dateEnd, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consultar informacion del cliente por documento
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_document"></param>
        /// <returns></returns>
        public static DataSet getCustomerByDoc(string _document)
        {
            string sqlquery = "USP_Leer_Persona_Usuario";
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
                cmd.Parameters.AddWithValue("@bas_documento", _document);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Actualizar campos especiales de cliente
        /// </summary>
        /// <param name="_co">Company</param>
        /// <param name="_customerId">Customer id for update</param>
        /// <param name="_type">New Type customer</param>
        /// <param name="_area">New Area</param>
        /// <param name="_ware">New Warehouse</param>
        /// <param name="_status">New Status</param>
        /// <returns></returns>
        public static string updateCoord(string _customerId, string _area, string _status)
        {
            string sqlquery = "USP_Modificar_Promotor_EstLid";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {

                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", _customerId);
                cmd.Parameters.AddWithValue("@bas_est_id",_status);
                cmd.Parameters.AddWithValue("@bas_are_id", _area);
                cmd.ExecuteNonQuery();                                
                return "1";
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion
    }
}
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
namespace www.aquarella.com.pe.bll
{
    public class Basic_Data
    {

        #region < Atributos >

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
       

        #endregion

        /// <summary>
        /// Consultar una persona, y si es coordinador o promotor
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_noDoc"></param>
        /// <returns></returns>
        /// 
        public static DataTable getPromoter(String _areaId, String _searchValue)
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
        public static DataSet getPersonCoorProm(string _noDoc, decimal _idPers)
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
                cmd.Parameters.AddWithValue("@bas_documento", _noDoc);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getPersonLider(string _noDoc, decimal _idPers)
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
                cmd.Parameters.AddWithValue("@bas_documento", _noDoc);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getPersonusers(string _noDoc)
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
                cmd.Parameters.AddWithValue("@bas_documento", _noDoc);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consultar informacion para la creacion de un nuevo cliente
        /// </summary>
        /// <param name="_co"></param>
        /// <returns>dataset que posee varios resultados: pos[0]-> Tipo de coordinador,pos[1]-> Tipo de impuestos,
        /// pos[2]-> Regimenes,pos[3]-> Ciudades,pos[4]-> Areas,pos[5]-> Tipos de flete<returns>
        /// <returns>pos[6]-> Tipo de persona<returns>
        /// <returns>pos[7]-> Bodegas<returns>
        /// <returns>pos[8]-> Tipos de documento<returns>
        /// 
        public static DataSet getinfosemana(string var_id_anio)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlcommand = "MAESTROS.USP_GET_FiltrarSemana";
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlcommand, var_id_anio, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

       

        public static DataSet getInfoNewCust()
        {
            string sqlquery = "USP_Leer_Datos_Maestros";
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
        public static DataSet getinfoprovincia(string var_id_dpto)
        {          
            string sqlquery = "USP_Leer_FiltraProvincia";
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
                cmd.Parameters.AddWithValue("@prv_dep_id", var_id_dpto);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataSet getinfodistrito(string var_id_prov)
        {           
            string sqlquery = "USP_Leer_FiltraDistrito";
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
                cmd.Parameters.AddWithValue("@dis_prv_cod", var_id_prov);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getInfoNewLider()
        {
            //DataSet ds = new DataSet();
            //return ds;
            string sqlquery = "USP_Leer_Datos_Maestros";
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

        public static DataSet getInfoNewusers()
        {           
            string sqlquery = "USP_Leer_Datos_Maestros";
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

        public static DataSet getInfoNewLiderZonal(string _co)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    string sqlCommand = "maestros.sp_getaallareasLiderZonal";
            //    // CURSOR REF
            //    object results = new object[1];
            //    // Create the Database object, using the default database service. The
            //    // default database service is determined through configuration.
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///                
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, results);
            //    // DataSet that will hold the returned results		
            //    // Note: connection closed by ExecuteDataSet method call 
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getInfoNewuserszonal()
        {
            string sqlquery = "USP_Leer_Area_Usuario";
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

        public static DataSet getInfoNewLideres()
        {
            string sqlquery = "USP_Leer_Area";
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
        /// Agregar un registro de basic data
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
        /// <returns></returns>
        public static string addBasicData
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
                   string BDV_SEX
           )
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string resultDoc = "";
            //string sqlCommand = "maestros.sp_add_basic_data";

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

            //// Output parameters specify the size of the return data.
            //db.AddOutParameter(dbCommandWrapper, "p_BDN_ID", DbType.Decimal, 12);

            //// Comenzar una transaccion y si todo resulta bien cerrarla realizando los cambios en la base de datos
            //using (DbConnection connection = db.CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();
            //    try
            //    {
            //        db.ExecuteNonQuery(dbCommandWrapper, transaction);
            //        //
            //        resultDoc = (string)Convert.ToString(db.GetParameterValue(dbCommandWrapper, "p_BDN_ID"));
            //        //
            //        // Commit the transaction.
            //        transaction.Commit();
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
        /// Actualizar basic data
        /// </summary>
        /// <param name="BDV_CO"></param>
        /// <param name="BDN_ID"></param>
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
        /// <param name="BDV_UPDATE_USER"></param>
        /// <param name="BDV_SEX"></param>
        /// <returns></returns>
        /// 

        public static string crear_usuario(string Bas_Primer_Nombre,string Bas_Segundo_Nombre,string Bas_Primer_Apellido,string Bas_Segundo_Apellido,
	                                       DateTime Bas_Fec_nac,string Bas_Documento,string Bas_Doc_Tip_Id,string Bas_Per_Tip_Id,string  Bas_Direccion,string Bas_Telefono,
                                           string  Bas_Fax,string Bas_Celular,string Bas_Correo,string Bas_Are_Id,string Bas_Cre_Usuario,string Bas_Sex_Id,string Bas_Dis_Id,string Bas_Usu_TipId,
                                           string Bas_Contraseña,Int32 _acceso=0,Boolean lider=false,string agencia="",string destino="",string agencia_ruc="")
        {
            string sqlquery = "USP_Crear_Usuario";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Bas_Primer_Nombre", Bas_Primer_Nombre);
                cmd.Parameters.AddWithValue("@Bas_Segundo_Nombre", Bas_Segundo_Nombre);
                cmd.Parameters.AddWithValue("@Bas_Primer_Apellido", Bas_Primer_Apellido);
                cmd.Parameters.AddWithValue("@Bas_Segundo_Apellido", Bas_Segundo_Apellido);
                cmd.Parameters.AddWithValue("@Bas_Fec_nac", Bas_Fec_nac);
                cmd.Parameters.AddWithValue("@Bas_Documento", Bas_Documento);
                cmd.Parameters.AddWithValue("@Bas_Doc_Tip_Id", Bas_Doc_Tip_Id);
                cmd.Parameters.AddWithValue("@Bas_Per_Tip_Id", Bas_Per_Tip_Id);
                cmd.Parameters.AddWithValue("@Bas_Direccion", Bas_Direccion);
                cmd.Parameters.AddWithValue("@Bas_Telefono", Bas_Telefono);
                cmd.Parameters.AddWithValue("@Bas_Fax", Bas_Fax);
                cmd.Parameters.AddWithValue("@Bas_Celular", Bas_Celular);
                cmd.Parameters.AddWithValue("@Bas_Correo", Bas_Correo);
                cmd.Parameters.AddWithValue("@Bas_Are_Id", Bas_Are_Id);
                cmd.Parameters.AddWithValue("@Bas_Cre_Usuario", Bas_Cre_Usuario);
                cmd.Parameters.AddWithValue("@Bas_Sex_Id", Bas_Sex_Id);
                cmd.Parameters.AddWithValue("@Bas_Dis_Id", Bas_Dis_Id);
                cmd.Parameters.AddWithValue("@Bas_Usu_TipId", Bas_Usu_TipId);
                cmd.Parameters.AddWithValue("@Bas_Contraseña", Bas_Contraseña);
                cmd.Parameters.AddWithValue("@promotor_defecto", _acceso);
                cmd.Parameters.AddWithValue("@lider", lider);

                cmd.Parameters.AddWithValue("@bas_agencia", agencia);
                cmd.Parameters.AddWithValue("@bas_destino", destino);
                cmd.Parameters.AddWithValue("@bas_agencia_ruc", agencia_ruc);


                cmd.ExecuteNonQuery();
                return "1";
            }
            catch (Exception e)
            {
                

                throw new Exception(e.Message, e.InnerException);
            }
        }


        public static string updateBasicDatausers
          (
                  decimal bas_id,
                  string bas_primer_nombre,string bas_segundo_nombre,
                  string bas_primer_apellido, string bas_segundo_apellido,
                  DateTime bas_fec_nac,string bas_doc_tip_id,
                  string  bas_per_tip_id,string bas_direccion,
                  string  bas_telefono,string bas_fax,
                  string  bas_celular,string bas_correo,
                  string  bas_are_id,string	bas_mod_usuario,
                  string  bas_sex_id,string bas_dis_id,
                  string  bas_usu_tipid,
                  Boolean defectotipousu=false,
                  string agencia="",string destino="",
                  string agencia_ruc=""  
          )
        {
            string sqlquery = "USP_Modificar_Basico_Dato";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", bas_id);
                cmd.Parameters.AddWithValue("@bas_primer_nombre", bas_primer_nombre);
                cmd.Parameters.AddWithValue("@bas_segundo_nombre", bas_segundo_nombre);
                cmd.Parameters.AddWithValue("@bas_primer_apellido", bas_primer_apellido);
                cmd.Parameters.AddWithValue("@bas_segundo_apellido", bas_segundo_apellido);
                cmd.Parameters.AddWithValue("@bas_fec_nac", bas_fec_nac);
                cmd.Parameters.AddWithValue("@bas_doc_tip_id", bas_doc_tip_id);
                cmd.Parameters.AddWithValue("@bas_per_tip_id", bas_per_tip_id);
                cmd.Parameters.AddWithValue("@bas_direccion", bas_direccion);
                cmd.Parameters.AddWithValue("@bas_telefono", bas_telefono);
                cmd.Parameters.AddWithValue("@bas_fax", bas_fax);
                cmd.Parameters.AddWithValue("@bas_celular", bas_celular);
                cmd.Parameters.AddWithValue("@bas_correo", bas_correo);
                cmd.Parameters.AddWithValue("@bas_are_id", bas_are_id);
                cmd.Parameters.AddWithValue("@bas_mod_usuario", bas_mod_usuario);
                cmd.Parameters.AddWithValue("@bas_sex_id", bas_sex_id);
                cmd.Parameters.AddWithValue("@bas_dis_id", bas_dis_id);
                cmd.Parameters.AddWithValue("@bas_usu_tipid", bas_usu_tipid);
                cmd.Parameters.AddWithValue("@DefectoTipoUsu", defectotipousu);

                cmd.Parameters.AddWithValue("@bas_agencia", agencia);
                cmd.Parameters.AddWithValue("@bas_destino", destino);
                cmd.Parameters.AddWithValue("@bas_agencia_ruc", agencia_ruc);


                cmd.ExecuteNonQuery();

                return "1";

            }
            catch (Exception e)
            {
                // Roll back the transaction. 
               
                throw new Exception(e.Message, e.InnerException);
            }
            //}
        }



        public static string updateBasicData
            (
                    string BDV_CO, decimal BDN_ID,
                    string BDV_FIRST_NAME, string BDV_MIDDLE_NAME,
                    string BDV_FIRST_SURNAME,
                    string BDV_SECOND_SURNAME,
                    DateTime BDD_BIRTHDAY, string BDV_DOCUMENT_NO,
                    string BDV_VERIF_DIGIT_NO, string BDV_DOCUMENT_TYPE_ID,
                    string BDV_PERSON_TYPE_ID, string BDV_ADDRESS,
                    string BDV_PHONE, string BDV_FAX, string BDV_MOVIL_PHONE,
                    string BDV_EMAIL, string BDV_CITY_CD, string BDV_STATUS,
                    string BDV_LANGUAGE_ID, string BDV_AREA_ID,
                    string BDV_UPDATE_USER, string BDV_SEX
            )
        {
            return "";
            //Database db = DatabaseFactory.CreateDatabase(_conn);
            //string sqlCommand = "maestros.sp_update_basic_data";

            //DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand);

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

            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_UPDATE_USER", DbType.String, BDV_UPDATE_USER);
            /////
            //db.AddInParameter(dbCommandWrapper, "p_BDV_SEX", DbType.String, BDV_SEX);
            /////

            //db.AddInParameter(dbCommandWrapper, "p_BDN_ID", DbType.Decimal, BDN_ID);

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

        /// <summary>
        /// Busqueda de persona, si es coordinador o promotor
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_searchField">Campo de comodines %,_, busca por cedula o nombre</param>
        /// <returns></returns>
        public static DataSet getPerson(string _co, string _searchField)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    object obj = (object)new object[1];
            //    Database database = DatabaseFactory.CreateDatabase(_conn);
            //    string str2 = "maestros.sp_get_person";
            //    DbCommand storedProcCommand = database.GetStoredProcCommand(str2, _co, _searchField, obj);
            //    return database.ExecuteDataSet(storedProcCommand);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message, ex.InnerException);
            //}
        }

        /// <summary>
        /// Buscar una persona, por cedula, id, ciudad, depto o nombre.
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_country"></param>
        /// <param name="_depto"></param>
        /// <param name="_idPerson"></param>
        /// <param name="_cedula"></param>
        /// <param name="_namePerson"></param>
        /// <returns></returns>
        public static DataSet searchPerson(String _company, String _country, String _depto,
                                                String _idPerson, String _cedula, String _namePerson)
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
            //    String sqlCommand = "MAESTROS.sp_searchperson";
            //    ///                
            //    /// Verificar los filtros de búsqueda  de coordinadores
            //    ///
            //    String _sentenceCountry = !_country.Equals("-1") ? " and  DEV_COUNTRY_ID = '" + _country + "' " : "";
            //    /// 
            //    String _sentenceDepto = !_depto.Equals("-1") && !_depto.Equals(String.Empty) ? "and DEV_DEPT_ID = '" + _depto + "'" : "";
            //    ///
            //    String _sentenceCedula = !_cedula.Equals(String.Empty) ? " and BDV_DOCUMENT_NO like ('" + _cedula + "')" : "";
            //    ///
            //    String _sentenceIdPerson = !_idPerson.Equals(String.Empty) ? " and BDN_ID like ('" + _idPerson + "')" : "";
            //    ///
            //    String _sentenceNamePerson = !_namePerson.Equals(String.Empty) ? "   and (  UPPER(bdv_first_name)  " +
            //                             "        || ' '  " +
            //                             "        || UPPER(bdv_middle_name)  " +
            //                             "        || ' '  " +
            //                             "        || UPPER(bdv_first_surname)  " +
            //                             "        || ' '  " +
            //                             "        || UPPER(BDV_SECOND_SURNAME) ) like UPPER ('" + _namePerson + "')" : "";
            //    /// 
            //    /// Armar la sentencia WHERE
            //    /// 
            //    String sqlWhereCommand = "   BDV_CO = '" + _company + "' " +
            //                  _sentenceCountry + _sentenceDepto + _sentenceCedula + _sentenceIdPerson + _sentenceNamePerson;
            //    ///" order by CON_COORDINATOR_ID ";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, sqlWhereCommand, results);
            //    ///
            //    ///DbCommand dbCommandWrapper = db.GetSqlStringCommand(sqlCommand);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e)
            //{
            //    Console.Write("Error : " + e.ToString());
            //    return null;
            //}

        }

    }
}
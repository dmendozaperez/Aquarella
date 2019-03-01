using System;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;


namespace www.aquarella.com.pe.bll
{
    public class Clear
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Crear el pre clear, cruce de pagos y liquidaciones
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_list_liquidations"></param>
        /// <param name="_list_documentrans"></param>
        /// <returns></returns>
        /// 
        public static DataTable getvalida_inter(DataTable dt)
        {
            string sqlquery = "USP_ConsultaValidaInter";
            DataTable dtinter = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.myconexion()))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tmpinter", dt);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtinter = new DataTable();
                            da.Fill(dtinter);
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return dtinter;
        }
        public static string setvalidaclear(string _list_liquidations,ref string _ncredito, ref string _fecharef)
        {            
            String sqlquery = "USP_Valida_Finanzas_PagoNc";
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
                cmd.Parameters.Add("@ncredito", SqlDbType.VarChar, 20);
                cmd.Parameters.Add("@fecha_ref", SqlDbType.VarChar, 20);
                cmd.Parameters["@ncredito"].Direction = ParameterDirection.Output;
                cmd.Parameters["@fecha_ref"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                _ncredito = cmd.Parameters["@ncredito"].Value.ToString();
                _fecharef = cmd.Parameters["@fecha_ref"].Value.ToString();

                return _ncredito;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static string _ejecutar_agrupar_pedido(Decimal _bas_id, Decimal _usu_ing, DataTable dt,ref string _liq_id)
        {
            string mensaje = "";
            string sqlquery="USP_Insertar_Liquidacion_Grupo";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", _bas_id);
                cmd.Parameters.AddWithValue("@usu_cre", _usu_ing);
                cmd.Parameters.AddWithValue("@tmp_grupo", dt);
                cmd.Parameters.Add("@LiqId", SqlDbType.VarChar, 12);
                cmd.Parameters["@LiqId"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _liq_id = cmd.Parameters["@LiqId"].Value.ToString();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
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

        public static string setPreClear(Decimal _usuid, DataTable dt)
        {
            //return "";
            string clearId = string.Empty;
            string sqlquery = "USP_Pre_Grupo_CN";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_id", _usuid);
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

        public static string setPreClear(string _list_liquidations,DataTable dt)
        {
            //return "";
            string clearId = string.Empty;
            string sqlquery = "USP_Pre_Grupo";
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
                //cmd.Parameters.AddWithValue("@gru_id_devolver", DbType.String);
                cmd.Parameters.AddWithValue("@Tmp_Pago", dt);
                //cmd.Parameters["@gru_id_devolver"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@gru_id_devolver", SqlDbType.VarChar, 80).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                clearId =Convert.ToString(cmd.Parameters["@gru_id_devolver"].Value);
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

        public static string setCrearLiquidacionPremio(int basId, int premioId, string TipoPremio ="C")
        {
            //return "";
            string strLiqui = string.Empty;
            string sqlquery = "USP_Generar_LiquidacionPremio";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bas_id", basId);
                //cmd.Parameters.AddWithValue("@gru_id_devolver", DbType.String);
                cmd.Parameters.AddWithValue("@tipoRegalo", premioId);
                cmd.Parameters.AddWithValue("@tipoPremio", TipoPremio);
                //cmd.Parameters["@gru_id_devolver"].Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@gru_id_devolver", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                strLiqui = Convert.ToString(cmd.Parameters["@gru_id_devolver"].Value);
                return strLiqui;

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

        public static void setpagocredito(string _liquidacion)
        {
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlcommand = "FINANCIERA.USP_AddPagoCredito";
            //    DbCommand dbCommanWrapper = db.GetStoredProcCommand(sqlcommand, _liquidacion);
            //    db.ExecuteNonQuery(dbCommanWrapper);

            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static void sbupdateclearncredito(string _list_liquidations, string vidclear)
        {
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "FINANCIERA.USP_UpdateClearNcredito";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _list_liquidations, vidclear);
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataTable fgetcorreoenvio()
        {
            DataTable dt = new DataTable();
            return dt;
            //try
            //{
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "Control.USP_Get_CorreoEnvio";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, results);
            //    return (db.ExecuteDataSet(dbCommandWrapper)).Tables[0];
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static void sb_addliquidacionnc(string _list_liquidations, string _list_documentrans)
        {
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "LOGISTICA.USP_ADD_LIQUIDACIONNC";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _list_liquidations, _list_documentrans);
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static void sb_borraliqnc(string _idliq)
        {
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "LOGISTICA.USP_BorrarNCLiqPago";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _idliq);
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Cruce financiero de documentos
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_list_documentrans"></param>
        /// <returns></returns>
        public static string setClearingDoc(string _company, string _list_documentrans)
        {
            return "";
            //string clearId = string.Empty;
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    //
            //    string sqlCommand = "financiera.sp_clearing_doctrans";
            //    //
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _list_documentrans, clearId);
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    clearId = Convert.ToString(db.GetParameterValue(dbCommandWrapper, "p_clv_clear_id"));

            //    return clearId;
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion
    }
}
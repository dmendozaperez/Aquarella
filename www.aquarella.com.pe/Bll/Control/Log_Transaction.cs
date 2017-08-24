using System;
using System.Data.Common;
using System.Threading.Tasks;
using www.aquarella.com.pe.bll;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Runtime.CompilerServices;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using www.aquarella.com.pe.bll.Control;
namespace www.aquarella.com.pe.bll
{
    public class Log_Transaction
    {
        #region < Atributos >

        /// <summary>
        /// Client ip
        /// </summary>
        public string _ip { get; set; }

        /// <summary>
        /// Client pc name
        /// </summary>
        public string _pcName { get; set; }

        /// <summary>
        /// Client country
        /// </summary>
        public string _country { get; set; }

        /// <summary>
        /// Client region
        /// </summary>
        public string _region { get; set; }

        /// <summary>
        /// Client city
        /// </summary>
        public string _city { get; set; }


        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        public Log_Transaction(string iP, string pcName, string country, string region, string city)
        {
            this._ip = iP;
            this._pcName = pcName;
            this._country = country;
            this._region = region;
            this._city = city;
        }


        public Log_Transaction(string iP)
        {
            this._ip = iP;
        }

        public Log_Transaction()
        { }

        #region < Metodos estaticos >

        /// <summary>
        /// Insertar un log de transaccion de actividad del cliente
        /// </summary>
        /// <param name="co"></param>
        /// <param name="userId"></param>
        /// <param name="dateTx"></param>
        /// <param name="log"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        public static bool insertLogTransaction(string co, decimal userId, DateTime dateTx, string log, string machine)
        {
            return false;
            //try
            //{
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "control.sp_insert_log_transaction";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, co, 12, userId, dateTx, log, machine);
            //    //
            //    db.ExecuteNonQuery(dbCommandWrapper);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }


        /// <summary>
        /// Async Tasks
        /// </summary>
        public static void registerUserInfo(Users user, string log)
        {
            //TaskEx.RunEx(async () =>
            //{

            //    // Inicio de una tarea que se completara despues del tiempo de vencimiento especificado (mil)
            //    await TaskEx.Delay(1);   

            //    string logPlus = " GEOINFO-> USER:" + user._usv_username.ToUpper();

            //    if (!string.IsNullOrEmpty(user._logTx._country))
            //        logPlus += " COUNTRY:" + user._logTx._country.ToUpper() + " REGION:" + user._logTx._region.ToUpper() + " CITY:" + user._logTx._city.ToUpper();

            //    // Registrar el logueo
            //    Log_Transaction.insertLogTransaction(user._usv_co, user._usn_userid, DateTime.Now, log + logPlus, user._logTx._ip);
            //});
        }




        #endregion

        #region<REGION DE VALIDACION DE ACCESOS >
        public static void _auditoria_acceso(Int32 _usu_id, string _nombres,string _ip,string _host)
        {
            string sqlquery = "USP_Auditoria_Accesos";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery,cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Aud_BasId", _usu_id);
                cmd.Parameters.AddWithValue("@Aud_NombreUsuario", _nombres);
                cmd.Parameters.AddWithValue("@Aud_IP", _ip);
                cmd.Parameters.AddWithValue("@Aud_Host", _host);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
        }
        #endregion
    }
}
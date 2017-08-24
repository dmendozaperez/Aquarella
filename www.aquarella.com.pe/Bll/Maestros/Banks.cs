using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll
{
    public class Banks
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion


        #region < Metodos estaticos >

        /// <summary>
        /// Consultar todos los bancos
        /// </summary>
        /// <returns></returns>
        public static DataSet getAllBanks()
        {            
            string sqlquery = "USP_Leer_Banco";
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

        #endregion

    }
}
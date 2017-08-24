using System;
using System.Data;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;


namespace www.aquarella.com.pe.bll.Maestros
{
    public class Concepts
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        /// <summary>
        /// Retona todos los conceptos por modulo.
        /// </summary>
        /// <returns></returns>
        public static DataSet getConceptsByType()
        {
            string sqlquery = "USP_Leer_ConceptoTipo";
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public static DataSet getconcepto()
        {
            string sqlquery = "USP_Leer_Lista_Concepto";
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
    }
}
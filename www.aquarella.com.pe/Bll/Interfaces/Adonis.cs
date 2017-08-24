using System;
using System.Data;
using System.Data.Common;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
namespace www.aquarella.com.pe.bll.Interfaces
{
    public class Adonis
    {

        /// Nombre de conexion a bd
        /// </summary>
  //      public static string _conn = Constants.OrcleStringConn;

        #region < METODOS ESTATICOS >

        /// <summary>
        /// Devuelve en una columna y varios registros la facturacion, 
        /// pagos y demas conceptos utilizados en la interfaz 
        /// comercial con adonis.
        /// </summary>
        /// <param name="company">Numero de compania.</param>
        /// <param name="date_start">Fecha de comienzo de los movimientos.</param>
        /// <param name="date_end">Fecha final de los movimientos.</param>
        /// 
        /// <returns></returns>
        public static DataSet Get_Comercial_Interface( DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_Generar_Archivo_Adonis";
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
                cmd.Parameters.AddWithValue("@p_dtd_document_date_start", _date_start);
                cmd.Parameters.AddWithValue("@p_dtd_document_date_end", _date_end);
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



        #endregion
    }
}
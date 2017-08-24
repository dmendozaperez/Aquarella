using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;


namespace www.aquarella.com.pe.bll
{
    public class Area
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        

        #endregion

        /// <summary>
        /// Consultar todos las areas
        /// </summary>
        /// <param name="_co"></param>
        /// <returns></returns>
        /// 

        public static DataSet getAllclientes()
        {
            string sqlquery = "USP_Leer_Lista_Clientes";
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

        public static DataSet getAllAreas(string _asesor)
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
                cmd.Parameters.AddWithValue("@asesor", _asesor);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataSet getAllaño()
        {
            string sqlquery = "USP_Leer_Año";
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
        /// Consultar 
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_region"></param>
        /// <returns></returns>
        public static DataSet getAreasByRegion(string _co, string _region)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "maestros.sp_getareasByRegion";
            //    //
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _co, _region, results);
            //    //
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        public static DataSet getconsulta_concep_client(Int32 _param)
        {
            DataSet ds = new DataSet();
            return ds;
            //try
            //{
            //    // CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    string sqlCommand = "Ventas.sp_consulta_concep_client";
            //    //
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _param, results);
            //    //
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

    }
}
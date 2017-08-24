using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;

//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll
{
    public class Employees
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion

        #region <  Métodos estaticos >

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_profesion"></param>
        /// <returns></returns>
        public static DataTable getEmployeesByCharge(string _tipo)
        {
            string sqlquery = "USP_Leer_Usuario_Tipo";
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
                cmd.Parameters.AddWithValue("@usu_tip_id", _tipo);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
                /// CURSOR REF                                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Consultar la bodega a la cual pertenece un empleado
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_idUser"></param>
        /// <returns></returns>
        public static DataTable getEmployeeWarehouse(string _company, decimal _idUser)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    /// CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "maestros.sp_getemployeewarehouse";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _idUser, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_company"></param>
        /// <param name="_idUser"></param>
        /// <returns></returns>
        public static DataTable getEmployeeByPK(string _company, decimal _idUser)
        {
            DataTable dt = new DataTable();
            return dt;
            //DataTable dtResult = new DataTable();
            //try
            //{
            //    /// CURSOR REF
            //    object results = new object[1];

            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    ///
            //    string sqlCommand = "maestros.sp_getemployeebypk";
            //    ///
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _company, _idUser, results);
            //    ///
            //    dtResult = db.ExecuteDataSet(dbCommandWrapper).Tables[0];
            //    ///
            //    return dtResult;
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }



        #endregion
    }
}
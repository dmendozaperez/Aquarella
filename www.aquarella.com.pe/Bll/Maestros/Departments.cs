using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.bll
{
    public class Departments
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
       

        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Consultar todos los departamentos
        /// </summary>
        /// <returns></returns>
        public static DataSet getAllDepartmens()
        {
            string sqlquery = "USP_Leer_Departamento";
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
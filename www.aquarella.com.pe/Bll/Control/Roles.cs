using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.aquarella.com.pe.bll.Util;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
namespace www.aquarella.com.pe.bll.Control
{
    public class Roles
    {

        #region <Atributos>

        private string _ROV_CO { get; set; }
        private decimal _RON_ID { get; set; }
        private string _ROV_NAME { get; set; }
        private string _ROV_DESCRIPTION { get; set; }

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>
       
        #endregion
        #region <Metodos Estaticos>
        /// <summary> Insaertar un Rol
        /// </summary>
        /// <param name="ROV_CO">identificador de la compañia</param>
        /// <param name="ROV_NAME">Nombre del rol</param>
        /// <param name="ROV_DESCRIPTION">descripcion</param>
        /// <returns>bool</returns>
        public static bool insertRole(string ROV_NAME, string ROV_DESCRIPTION)
        {            
            string sqlquery = "USP_Insertar_Roles";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rol_id", 0);
                cmd.Parameters.AddWithValue("@rol_nombre", ROV_NAME);
                cmd.Parameters.AddWithValue("@rol_descripcion", ROV_DESCRIPTION);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary> Actualiza informacion del rol
        /// </summary>
        /// <param name="ROV_CO">company</param>
        /// <param name="RON_ID">id rol</param>
        /// <param name="ROV_NAME">nombre Rol</param>
        /// <param name="ROV_DESCRIPTION">Descripcion Rol</param>
        /// <returns>bool estado del update. </returns>
        public static bool updateRole(int RON_ID, string ROV_NAME, string ROV_DESCRIPTION)
        {            
            string sqlquery = "USP_Modificar_Roles";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rol_id", RON_ID);
                cmd.Parameters.AddWithValue("@rol_nombre", ROV_NAME);
                cmd.Parameters.AddWithValue("@rol_descripcion", ROV_DESCRIPTION);

                cmd.ExecuteNonQuery();
                
                return true;
            }
            catch (Exception) { return false; }
        }

        /// <summary>Obtener lista de roles
        /// </summary>
        /// <returns>Data Set con los roles</returns>
        public static DataSet getRoles()
        {
            string sqlquery = "USP_Leer_Roles";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd=new SqlCommand(sqlquery,cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);            
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
            
        }

        /// <summary> Obtener los roles por usuarios
        /// </summary>
        /// <param name="USN_USERID"></param>
        /// <param name="USV_CO"></param>
        /// <returns>Data Set con roles de usuario</returns>
        public static DataSet GetRolesByUser(decimal USN_USERID)
        {
            string sqlquery = "USP_Leer_Roles_Usuario";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usu_id", USN_USERID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;            
        }

        /// <summary> Insertar User role
        /// </summary>
        /// <param name="_URN_ROLEID"></param>
        /// <param name="_URN_USERID"></param>
        /// <param name="_URV_CO"></param>
        /// <returns>bool estado de la insercion</returns>
        public static bool insertUserRole(decimal _URN_ROLEID, decimal _URN_USERID)
        {         
            string sqlquery = "USP_Insertar_Usuario_Roles";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_rol_idusu", _URN_USERID);
                cmd.Parameters.AddWithValue("@usu_rol_idrol", _URN_ROLEID);
                cmd.ExecuteNonQuery();                
                return true;
            }
            catch (Exception) { return false; }

        }

        public static bool deleteUserRole(decimal _URN_ROLEID, decimal _URN_USERID)
        {           
            string sqlquery = "USP_Borrar_Usuario_Roles";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State==0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@usu_rol_idusu", _URN_USERID);
                cmd.Parameters.AddWithValue("@usu_rol_idrol", _URN_ROLEID);
                cmd.ExecuteNonQuery();                
                return true;
            }
            catch (Exception) { return false; }
        }
        #endregion


    }
}
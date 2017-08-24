using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.aquarella.com.pe.bll.Control;
using System.Data.SqlClient;
using System.Data;
namespace www.aquarella.com.pe.bll
{
    public class Contactenos_Data
    {
        public static DataTable departamento_contacto()
        {
            string sqlquery = "USP_Leer_Departamento";
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
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public static void _enviar_contactenos(string _nombre, string _apellidos, string _telefono, string _email, string _comentario, string _direccion)
        {
            string sqlquery = "USP_Envia_Correo_Contactenos";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", _nombre);
                cmd.Parameters.AddWithValue("@apellido", _apellidos);
                cmd.Parameters.AddWithValue("@telefono", _telefono);
                cmd.Parameters.AddWithValue("@email", _email);
                cmd.Parameters.AddWithValue("@Comentario", _comentario);
                cmd.Parameters.AddWithValue("@Direccion", _direccion);
                cmd.ExecuteNonQuery();
            }
            catch
            {

            }
        }
        public static DataSet leer_contactenos_data()
        {
            string sqlquery = "USP_Leer_Contactenos_Data";
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
            catch
            {
                return null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.aquarella.com.pe.bll.Control;
using System.Data.SqlClient;
using System.Data;
namespace www.aquarella.com.pe.bll.Interfaces
{
    public class Contactenos
    {

        public static void enviar_correo_contactenos(string _nombre, string _apellido, string _telefono, string _email, string _comentario)
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
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", _nombre);
                cmd.Parameters.AddWithValue("@apellido", _apellido);
                cmd.Parameters.AddWithValue("@telefono", _telefono);
                cmd.Parameters.AddWithValue("@email", _email);
                cmd.Parameters.AddWithValue("@Comentario", _comentario);
                cmd.ExecuteNonQuery();
            }
            catch
            {
            }
        }
    }
}
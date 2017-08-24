using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace informativa.aquarella.com.oe.Data
{
    public class Correo
    {
        public string _nombre { get; set; }
        public string _apellidos { get; set; }
        public string _telefono { get; set; }
        public string _email { get; set; }
        public string _comentario { get; set; }
        public string _direccion { get; set; }
       

        public Boolean _enviar_contactenos()
        {
            Boolean _valida = false;
            string sqlquery = "USP_Envia_Correo_Contactenos";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", _nombre);
                        cmd.Parameters.AddWithValue("@apellido", _apellidos);
                        cmd.Parameters.AddWithValue("@telefono", _telefono);
                        cmd.Parameters.AddWithValue("@email", _email);
                        cmd.Parameters.AddWithValue("@Comentario", _comentario);
                        cmd.Parameters.AddWithValue("@Direccion", _direccion);
                        cmd.ExecuteNonQuery();
                        _valida = true;
                    }

                }
            }
            catch
            {
                _valida = false;
            }
            return _valida;
        }

        
    }
}
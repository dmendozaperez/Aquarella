using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using informativa.aquarella.com.oe.Models;
using System.Web;

namespace informativa.aquarella.com.oe.Data
{
    public class LoginDA
    {

        public Ent_Usuario get_login(string _usuario)
        {
            string sqlquery = "[USP_OBTENER_USUARIO]";
            Ent_Usuario usuario = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usu_Login", _usuario);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            usuario = new Ent_Usuario();

                            while (dr.Read())
                            {
                                usuario.usu_id = (decimal)dr["usu_id"];
                                usuario.usu_nombre = dr["Usu_Nombre"].ToString();
                                usuario.usu_apPaterno = dr["Usu_Paterno"].ToString();
                                usuario.usu_apMaterno = dr["Usu_Materno"].ToString();
                                usuario.usu_contraseña = dr["password"].ToString();
                                usuario.usu_est_id = dr["usu_est_id"].ToString();
                                usuario.usu_login = dr["usu_login"].ToString();
                            }
                        }

                    }

                }
            }
            catch (Exception)
            {
                usuario = null;
            }
            return usuario;
        }

    }
}
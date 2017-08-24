using CapaEntidad.Bll.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaDato.Bll.Util
{
    public class Dat_Banco
    {
        public List<Ent_Banco> listar()
        {
            string sqlquery = "USP_Leer_Banco_Deposito";
            List<Ent_Banco> get_banco = null; 
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State ==0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            get_banco = new List<Ent_Banco>();

                            Ent_Banco ban = new Ent_Banco();
                            ban.ban_id = "-1";
                            ban.ban_des = "--NINGUNO--";
                            get_banco.Add(ban);

                            while(dr.Read())
                            {
                                ban = new Ent_Banco();
                                ban.ban_id = dr["Ban_Id"].ToString();
                                ban.ban_des = dr["Ban_Descripcion"].ToString();
                                get_banco.Add(ban);
                            }
                        }
                    }
                }
            }
            catch
            {
                get_banco = null;
            }
            return get_banco;
        } 
    }
}

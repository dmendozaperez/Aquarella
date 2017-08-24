using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aqmvc.com.pe.Data.Util;

namespace www.aqmvc.com.pe.Data.Control
{
    public class Estado
    {
        public List<Estado_Select> _LeerEstado(decimal _modulo)
        {
            string sqlquery = "USP_Leer_EstadoModulo";
            List<Estado_Select> data_select = new List<Estado_Select>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@est_mod_id", _modulo);
                        SqlDataReader sqlread = cmd.ExecuteReader();

                        if (sqlread.HasRows)
                        {
                            while (sqlread.Read())
                            {
                                Estado_Select item = new Estado_Select();
                                item._est_id = sqlread["Est_Id"].ToString();
                                item._est_des = sqlread["Est_Descripcion"].ToString();
                                data_select.Add(item);
                            }
                        }
                    }
                }

            }
            catch
            {
                data_select = null;
            }
            return data_select;
        }
    }
    public class Estado_Select
    {
        public string _est_id { set; get; }
        public string _est_des { set; get; }
    }
}
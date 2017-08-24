using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aqmvc.com.pe.Data.Util;

namespace www.aqmvc.com.pe.Data.Cliente
{
    public class Lider
    {
        public List<Lider_Select> _leer_lider()
        {
            string sqlquery = "USP_Leer_Area";
            List<Lider_Select> data_select = new List<Lider_Select>();
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader sqlread = cmd.ExecuteReader();

                        if (sqlread.HasRows)
                        {
                            while (sqlread.Read())
                            {
                                Lider_Select item = new Lider_Select();
                                item.are_id = sqlread["Are_Id"].ToString();
                                item.are_descripcion = sqlread["Are_Descripcion"].ToString();
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
    public class Lider_Select
    {
        public string are_id { set; get; }
        public string are_descripcion { set; get; }
    }
}
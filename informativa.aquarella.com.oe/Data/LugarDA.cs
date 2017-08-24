using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using informativa.aquarella.com.oe.Models;
using System.Data.SqlClient;
using System.Data;

namespace informativa.aquarella.com.oe.Data
{
    public class LugarDA
    {
        public List<Lugar> Listar()
        {
            string sqlquery = "USP_Lista_DistritoContactenos";
            List<Lugar> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_data))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            list = new List<Lugar>();
                            while (dr.Read())
                            {
                                Lugar row = new Lugar();
                                row.DIS_ID = dr["Dis_Id"].ToString();
                                row.DESRIPCION= dr["descripcion"].ToString();
                                list.Add(row);
                            }
                        }
                    }
                }
            }
            catch
            {
                list = null;
            }
            return list;
        }
    }
}
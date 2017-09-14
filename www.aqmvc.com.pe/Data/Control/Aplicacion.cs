using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aqmvc.com.pe.Data.Util;

namespace www.aqmvc.com.pe.Data.Control
{
    public class Aplicacion
    {
        public string apl_nombre { get; set; }
        public string apl_id { get; set; }

        public List<Aplicacion> get_lista()
        {
            string sqlquery = "USP_Leer_Aplicacion";
            List<Aplicacion> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            list = new List<Aplicacion>();

                            while(dr.Read())
                            {
                                Aplicacion apl = new Aplicacion();
                                apl.apl_id =dr["apl_id"].ToString();
                                apl.apl_nombre = dr["apl_nombre"].ToString();
                                list.Add(apl);
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
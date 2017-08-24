using CapaEntidad.Bll.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaDato.Bll.Util
{
    public class Dat_FormaPago
    {
        public List<Ent_FormaPago> select()
        {
            List<Ent_FormaPago> list = null;
            string sqlquery = "USP_LeerFormaPago";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery,cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            list = new List<Ent_FormaPago>();
                            while (dr.Read())
                            {
                                Ent_FormaPago pag = new Ent_FormaPago();
                                pag.con_id = dr["Con_Id"].ToString();
                                pag.con_descripcion = dr["Con_Descripcion"].ToString();
                                list.Add(pag);
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

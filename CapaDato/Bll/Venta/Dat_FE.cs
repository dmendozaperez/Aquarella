using CapaEntidad.Bll.Util;
using CapaEntidad.Bll.Venta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaDato.Bll.Venta
{
    public class Dat_FE
    {

        public void update_error_FE(string error_des)
        {
            string sqlquery = "USP_InsertarErrorFE";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@error_des", error_des);
                            cmd.ExecuteNonQuery();
                        }

                    }
                    catch (Exception)
                    {
                       
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {

                
            }
        }

        public List<Ent_FE> get_doc_fe_error()
        {
            List<Ent_FE> lista = null;
            string sqlquery = "USP_LeerErrorDocFacturacionE";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();

                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                lista = new List<Ent_FE>();
                                while (dr.Read())
                                {
                                    Ent_FE fe = new Ent_FE();
                                    fe.tipo = dr["tipo"].ToString();
                                    fe.numero = dr["numero"].ToString();
                                    fe.not_id= dr["not_id"].ToString();
                                    lista.Add(fe);
                                }
                            }

                        }

                    }
                    catch (Exception)
                    {
                        
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {
                lista = null;                
            }
            return lista;
        }
    }
}

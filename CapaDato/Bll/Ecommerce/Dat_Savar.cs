using System;
using System.Collections.Generic;
using System.Linq;
using CapaEntidad.Bll.Ecommerce;

using System.Data.SqlClient;
using System.Data;
using CapaEntidad.Bll.Util;
namespace CapaDato.Bll.Ecommerce
{
    public class Dat_Savar
    {
        public DataTable get_Ventas_por_Savar(string ven_id)
        {
            string sqlquery = "USP_ECOMMERCE_LISTA_SAVAR";
            DataTable dtSavar = new DataTable();

            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ven_id", ven_id);

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables.Count > 0)
                            {
                                dtSavar = ds.Tables[0];
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                dtSavar = null;
            }
            return dtSavar;
        }

        public DataTable Ecommerce_getConexionesAPI(string nombre, int tipo)
        {
            DataTable dt = null;
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            string sqlcommand = "USP_Lista_APICourier";
            try
            {
                cn = new SqlConnection(Ent_Conexion.conexion);
                cmd = new SqlCommand(sqlcommand, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch
            {
                dt = null;
                throw;
            }
            return dt;
        }

    }


}

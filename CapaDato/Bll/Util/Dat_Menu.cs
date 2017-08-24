using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CapaDato.Bll.Util
{
    public class Dat_Menu
    {
        public static DataTable _dt_opciones_menu(string _cod_usu,string _cod_mod,Int32 _item_mod)
        {
            string sqlquery = "USP_CONSULTAR_OPCMENU";
            DataTable dt = null;
            try
            {
                using(SqlConnection cn=new SqlConnection(Dat_Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USU_COD", _cod_usu);
                        cmd.Parameters.AddWithValue("@MOD_COD", _cod_mod);
                        cmd.Parameters.AddWithValue("@MDT_ITEM", _item_mod);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}

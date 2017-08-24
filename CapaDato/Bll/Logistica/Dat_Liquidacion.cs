using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CapaEntidad.Bll.Util;
namespace CapaDato.Bll.Logistica
{
    public class Dat_Liquidacion
    {
        public static DataTable liquidacionXfacturar()
        {
            DataTable dt = null;
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            string sqlcommand = "USP_Leer_LiquidacionXFacturar";
            try
            {
                cn = new SqlConnection(Ent_Conexion.conexion);
                cmd = new SqlCommand(sqlcommand, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Variables;
using System.Windows.Forms;
namespace Aquarella.bll
{
    public class Liquidacion
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
                cn = new SqlConnection(Global.conexion);
                cmd = new SqlCommand(sqlcommand, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
               
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dt = null;
            }
            return dt;
        }

        public static void insertar_guia(string _guino, Int32 _trans, string _liq,out  string _valida )
        {
            string sqlquery = "USP_Insertar_Guia";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Global.conexion);
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tra_gui_no", _guino);
                cmd.Parameters.AddWithValue("@tra_gui_traid", _trans);
                cmd.Parameters.AddWithValue("@liq_id", _liq);
                cmd.Parameters.Add("@valida_guia", SqlDbType.Int);
                cmd.Parameters["@valida_guia"].Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                _valida = cmd.Parameters["@valida_guia"].Value.ToString();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _valida = "-1";
            }
        }
    }
}

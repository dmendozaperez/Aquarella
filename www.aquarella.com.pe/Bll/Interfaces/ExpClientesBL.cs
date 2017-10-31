using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll.Interfaces
{
    public class ExpClientesBL
    {
        //public static string _conn = Constants.OrcleStringConn;

        #region < METODOS ESTATICOS >

        //public static DataSet Get_ClientesRel_Lider(string _company, DateTime _date_start, DateTime _date_end)
        public static DataSet Get_ClientesRel_Lider()
        {

            string sqlquery = "USP_Leer_Rel_ProLider";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;            
        }

        public static DataSet get_cliente_pedido_inter()
        {
            DataSet ds = null;
            string sqlquery = "[USP_ExportarClienteBanco]";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.myconexion()))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);
                        }
                    }
                }
            }
            catch (Exception)
            {

                ds = null;
            }
            return ds;
        }

        public static DataSet Get_Clientes_Banco()
        {
            string sqlquery = "USP_Leer_Cliente_Banco";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

            
        }

        public static DataSet Get_VentasAQ( DateTime _date_start, DateTime _date_end)
        {
            string sqlquery = "USP_Exportar_Ventas_Bata";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            cn = new SqlConnection(Conexion.myconexion());
            cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@p_IHD_DATE_start", _date_start);
            cmd.Parameters.AddWithValue("@p_IHD_DATE_end", _date_end);
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        #endregion
    }
}
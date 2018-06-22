using CapaEntidad.Bll.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaDato.Bll.Util
{
    public class Dat_Basico
    {
        public static Ent_Global entorno_global(string _entorno)
        {
            Ent_Global var_ent = null;
            try
            {

            }
            catch
            {
                var_ent = null;
            }
            return var_ent;
        }
        public Boolean update_cierre_venta(Int32 estado,DateTime fecha_cierre,Decimal monto,
                                           string banid,string nrooperacion,Decimal monto_opera)
        {
            Boolean _valida = false;
            string sqlquery = "USP_CierreVenta_Actualiza";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@estado", estado);
                        cmd.Parameters.AddWithValue("@fecha",fecha_cierre);
                        cmd.Parameters.AddWithValue("@monto_ini", monto);
                        cmd.Parameters.AddWithValue("@cierre_almid",Ent_Global._pvt_almaid);
                        cmd.Parameters.AddWithValue("@cierre_basid",Ent_Global._bas_id_codigo);

                        cmd.Parameters.AddWithValue("@cierre_ban_id", banid);
                        cmd.Parameters.AddWithValue("@cierre_operacion", nrooperacion);
                        cmd.Parameters.AddWithValue("@cierre_monto_ope", monto_opera);

                        cmd.ExecuteNonQuery();
                        _valida = true;
                    }
                }
            }
            catch
            {
                _valida = false;
            }
            return _valida;
        }
        public static void VerificaFechaServer_Cierre()
        {
            string sqlquery = "USP_VerificaVentaFechaCierre";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@almid", Ent_Global._pvt_almaid);
                        cmd.Parameters.Add("@valida", SqlDbType.Bit);
                        cmd.Parameters["@valida"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@ult_fec_cierre", SqlDbType.Date);
                        cmd.Parameters["@ult_fec_cierre"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        Ent_Global._fecha_cierre_valida =Convert.ToBoolean(cmd.Parameters["@valida"].Value);
                        
                        if (cmd.Parameters["@ult_fec_cierre"].Value.ToString()!="")
                        {
                            Ent_Global._fecha_cierre_ult = Convert.ToDateTime(cmd.Parameters["@ult_fec_cierre"].Value);
                        }
                    }
                }
            }
            catch
            {
                Ent_Global._fecha_cierre_valida = false;
               
            }
        }
        public static void VerificaCierreVenta()
        {
            string sqlquery = "USP_VerificaCierreVenta_Almacen";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idalmacen", Ent_Global._pvt_almaid);
                        cmd.Parameters.Add("@cierrealm", SqlDbType.Bit);
                        cmd.Parameters["@cierrealm"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        Ent_Global._inicio_caja =Convert.ToBoolean(cmd.Parameters["@cierrealm"].Value);
                    }
                }
            }
            catch
            {
                Ent_Global._inicio_caja = false;
            }
        }
        public static void GetFechaServer()
        {
            string sqlquery = "select fecha_server=dbo.Fecha(getdate())";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while(dr.Read())
                            {
                                Ent_Global._fecha_server =Convert.ToDateTime(dr["fecha_server"]);
                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

        #region<METODO ESTATICA PARA LA FACTURACION ELECTRONICA>
        /// <summary>
        /// configuracion de la facturacion electronica paperless
        /// </summary>
        public static void config_ws_fe()
        {
            string sqlquery = "USP_LeerConfig_FE";
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
                            cmd.Parameters.Add("@ws_ruc", SqlDbType.VarChar, 20);
                            cmd.Parameters.Add("@ws_login", SqlDbType.VarChar, 20);
                            cmd.Parameters.Add("@ws_password", SqlDbType.VarChar, 20);
                            cmd.Parameters.Add("@pr_factura", SqlDbType.VarChar, 1);

                            cmd.Parameters["@ws_ruc"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@ws_login"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@ws_password"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@pr_factura"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();

                            Ent_Global._ws_ruc = cmd.Parameters["@ws_ruc"].Value.ToString();
                            Ent_Global._ws_login = cmd.Parameters["@ws_login"].Value.ToString();
                            Ent_Global._ws_password = cmd.Parameters["@ws_password"].Value.ToString();
                            Ent_Global.pr_facturador = cmd.Parameters["@pr_factura"].Value.ToString();
                        }

                    }
                    catch(Exception exc)
                    {

                        
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch
            {

                
            }
        }

        #endregion
    }
}

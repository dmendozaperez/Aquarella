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
        public string apl_tip_id { get; set; }
        public string apl_url { get; set; }
        public string apl_orden { get; set; }
        public string apl_est_id { get; set; }
        public string apl_controller { get; set; }
        public string apl_action { get; set; }
        public Boolean InsertarAplicacion()
        {
            string sqlquery = "USP_Insertar_Aplicacion";
            Boolean valida = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@apl_id", apl_id);
                        cmd.Parameters.AddWithValue("@apl_nombre", apl_nombre);
                        cmd.Parameters.AddWithValue("@apl_tip_id", apl_tip_id);
                        cmd.Parameters.AddWithValue("@apl_url", apl_url);
                        cmd.Parameters.AddWithValue("@apl_orden", apl_orden);
                        cmd.Parameters.AddWithValue("@apl_est_id", apl_est_id);
                        cmd.Parameters.AddWithValue("@apl_ayuda", "");
                        cmd.Parameters.AddWithValue("@apl_comentario", "");
                        cmd.Parameters.AddWithValue("@apl_controller",apl_controller);
                        cmd.Parameters.AddWithValue("@apl_action", apl_action);
                        cmd.ExecuteNonQuery();
                        valida = true;
                    }
                }

            }
            catch (Exception exc)
            {
                valida = false;
            }
            return valida;
        }
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
                                apl.apl_tip_id= dr["apl_tip_id"].ToString();
                                apl.apl_orden= dr["apl_orden"].ToString();
                                apl.apl_est_id= dr["apl_est_id"].ToString();
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
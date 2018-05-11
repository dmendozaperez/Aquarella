using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aquarella.pe.Data.Util;

namespace www.aquarella.pe.Data.Maestros
{
    public class Promocion
    {

       
        public List<ent_Promocion> get_lista()
        {
            List<ent_Promocion> list = null;
            string sqlquery = "USP_Leer_Promociones";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        list = new List<ent_Promocion>();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            list = new List<ent_Promocion>();
                            list = (from DataRow dr in dt.Rows
                                      select new ent_Promocion()
                                      {
                                          ofe_id = dr["Ofe_Id"].ToString(),
                                          ofe_descripcion = dr["Ofe_Descripcion"].ToString(),
                                          ofe_maxPares = dr["Ofe_MaxPares"].ToString(),
                                          ofe_porc = (dr["Ofe_Porc"]).ToString(),
                                          ofe_fecIni = dr["FechaIni"].ToString(),
                                          ofe_fecFin = dr["FechaFin"].ToString(),
                                          ofe_Estado = dr["Estado"].ToString(),
                                          ofe_EstadoId = dr["estadoId"].ToString(),
                                         
                                      }).ToList();

                        }
                    }
                }
            }
            catch (Exception)
            {

                list = null;
            }
            return list;
        }
    }
    
    public class ent_Promocion
    {
        public string ofe_id { get; set; }
        public string ofe_descripcion { get; set; }
        public string ofe_maxPares { get; set; }
        public string ofe_porc { get; set; }
        public string ofe_fecIni { get; set; }
        public string ofe_fecFin { get; set; }
        public string ofe_Estado{ get; set; }
        public string ofe_EstadoId { get; set; }
    }
    
}
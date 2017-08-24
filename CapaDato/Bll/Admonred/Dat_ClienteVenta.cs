using CapaDato.Bll.Util;
using CapaEntidad.Bll.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaDato.Bll.Admonred
{
    public class Dat_ClienteVenta
    {
        public decimal bas_id { get; set; }
        public string dniruccli { get; set; }
        public string nomcli { get; set; }
        public string direccion { get; set; }
        public string tipocli { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string tipodoc { get; set; }
        public string sernum { get; set; }
        public Boolean existe_cli { get; set; }

        public Dat_ClienteVenta()
        {

        }

        public Dat_ClienteVenta(string dniruc)
        {
            string sqlquery = "USP_ConsultarClienteFD";
            existe_cli = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@dniruc", dniruc);
                        cmd.Parameters.AddWithValue("@alm_id", Ent_Global._pvt_almaid);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                bas_id =Convert.ToDecimal(dr["Bas_Id"]);
                                dniruccli = dr["dniruc"].ToString();
                                nomcli = dr["nomcli"].ToString();
                                direccion = dr["direccion"].ToString();
                                tipocli = dr["tipousu"].ToString();
                                telefono = dr["telefono"].ToString();
                                email = dr["email"].ToString();
                                tipodoc = dr["tipodoc"].ToString();
                                sernum = dr["sernum"].ToString();
                                existe_cli = true;
                            }

                        }
                    }
                }
            }
            catch
            {
                existe_cli = false;
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aquarella.pe.Data.Util;

namespace www.aquarella.pe.Data.Cliente
{
    public class Personal
    {
        public Int32 bas_id { get; set; }
        public string nombres { get; set; }
        public string primer_nombre { get; set; }
        public string segundo_nombre { get; set; }
        public string primer_apellido { get; set; }
        public string segundo_apellido { get; set; }
        public string dni_ruc { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string tipo_usuario { get; set; }
        public string estado { get; set; }
        public string fec_nac { get; set; }
        public string sexo { get; set; }
        public string tipo_doc { get; set; }
        public string tipo_persona { get; set; }
        public string depar { get; set; }
        public string tipo_usuid { get; set; }
        public string prv_id { get; set; }
        public string dis_id { get; set; }
        public List<Personal> get_lista()
        {
            string sqlquery = "USP_LeerLista_UsuariosAQ";
            List<Personal> listar = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<Personal>();
                            listar = (from DataRow dr in dt.Rows
                                      select new Personal()
                                      {
                                          bas_id=Convert.ToInt32(dr["bas_id"]),
                                          nombres= dr["nombres"].ToString(),
                                          primer_nombre = dr["primer_nombre"].ToString(),
                                          segundo_nombre = dr["segundo_nombre"].ToString(),
                                          primer_apellido = dr["primer_apellido"].ToString(),
                                          segundo_apellido = dr["segundo_apellido"].ToString(),
                                          dni_ruc = dr["dni_ruc"].ToString(),
                                          direccion = dr["direccion"].ToString(),
                                          telefono = dr["telefono"].ToString(),
                                          celular = dr["celular"].ToString(),
                                          correo = dr["correo"].ToString(),
                                          tipo_usuario = dr["tipo_usuario"].ToString(),
                                          estado = dr["estado"].ToString(), 
                                          fec_nac= dr["fec_nac"].ToString(),
                                          sexo=dr["sexo"].ToString(),
                                          tipo_doc= dr["tipo_doc"].ToString(),
                                          tipo_persona=dr["tipo_persona"].ToString(),
                                          depar=dr["depar"].ToString(),
                                          tipo_usuid = dr["tipo_usu"].ToString(),
                                          prv_id= dr["prv_id"].ToString(),
                                          dis_id = dr["dis_id"].ToString(),
                                      }).ToList();

                        }
                    }
                }
            }
            catch (Exception exc)
            {
                listar = null;
            }
            return listar;
        }
    }
}
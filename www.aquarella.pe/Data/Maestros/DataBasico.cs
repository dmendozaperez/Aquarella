using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aquarella.pe.Data.Util;

namespace www.aquarella.pe.Data.Maestros
{
    public class DataBasico
    {
        public List<TipoPersona> tipo_persona { get; set; }
        public List<TipoUsuario> tipo_usuario { get; set; }
        public List<TipoDocumento> tipo_documento { get; set; }
        public List<Departamento> departamento { get; set; }
        public List<Provincia> provincia { get; set; }
        public List<Distrito> distrito { get; set; }

        public List<Provincia> filtrar_prov(string dep_id)
        {
            List<Provincia> listar_prov = null;
            try
            {
                listar_prov = provincia.Where(p => p.prv_dep_id == dep_id).ToList();
            }
            catch (Exception)
            {
                listar_prov = null;
            }
            return listar_prov;
        }
        public List<Distrito> filtrar_dis(string prv_id)
        {
            List<Distrito> listar_dis = null;
            try
            {
                listar_dis = distrito.Where(p => p.dis_prv_id == prv_id).ToList();
            }
            catch (Exception)
            {
                listar_dis = null;                
            }
            return listar_dis;
        }
        public void ejecuta()
        {
            string sqlquery = "USP_Leer_Datos_Maestros";
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
                            DataSet ds = new DataSet();
                            da.Fill(ds, "Maestros");

                            if (ds.Tables.Count>0)
                            {
                                DataTable dt_tipopersona = ds.Tables[0];
                                DataTable dt_tipousuario = ds.Tables[1];
                                DataTable dt_tipodocumento = ds.Tables[2];

                                DataTable dt_departamento = ds.Tables[3];
                                DataTable dt_provincia = ds.Tables[5];
                                DataTable dt_distrito = ds.Tables[6];

                                tipo_persona = (from DataRow dr in dt_tipopersona.Rows
                                                select new TipoPersona()
                                                {
                                                    per_tip_id =dr["per_tip_id"].ToString(),
                                                    per_tip_des = dr["per_tip_descripcion"].ToString(),
                                                }).ToList();

                                tipo_usuario= (from DataRow dr in dt_tipousuario.Rows
                                               select new TipoUsuario()
                                               {
                                                   usu_tip_id = dr["usu_tip_id"].ToString(),
                                                   usu_tip_nom = dr["usu_tip_nombre"].ToString(),
                                               }).ToList();

                                tipo_documento = (from DataRow dr in dt_tipodocumento.Rows
                                                select new TipoDocumento()
                                                {
                                                    doc_tip_id= dr["doc_tip_id"].ToString(),
                                                    doc_tip_nom = dr["doc_tip_descripcion"].ToString(),
                                                }).ToList();

                                departamento = (from DataRow dr in dt_departamento.Rows
                                                  select new Departamento()
                                                  {
                                                      dep_id = dr["dep_id"].ToString(),
                                                      dep_nom= dr["dep_descripcion"].ToString(),
                                                  }).ToList();

                                provincia = (from DataRow dr in dt_provincia.Rows
                                                select new Provincia()
                                                {
                                                    prv_id = dr["prv_id"].ToString(),
                                                    prv_dep_id = dr["prv_dep_id"].ToString(),
                                                    prv_descripcion = dr["prv_descripcion"].ToString(),
                                                }).ToList();

                                distrito = (from DataRow dr in dt_distrito.Rows
                                                select new Distrito()
                                                {
                                                    dis_id = dr["dis_id"].ToString(),
                                                    dis_prv_id = dr["dis_prv_id"].ToString(),
                                                    dis_descripcion = dr["dis_descripcion"].ToString(),
                                                }).ToList();

                            }

                        }
                    }
                }
            }
            catch (Exception exc)
            {
                               
            }
        }
    }
    
    public class TipoPersona
    {
        public string per_tip_id { get; set; }
        public string per_tip_des { get; set; }
    }
    public class TipoUsuario
    {
        public string usu_tip_id { get; set; } 
        public string usu_tip_nom { get; set; }    
    }
    public class TipoDocumento
    {
        public string doc_tip_id { get; set; }
        public string doc_tip_nom { get; set; }
    }
    public class Departamento
    {
        public string dep_id { get; set; }
        public string dep_nom { get; set; }
    }
    public class Provincia
    {
        public string prv_id { get; set; }
        public string prv_dep_id { get; set; }
        public string prv_descripcion { get; set; }       
    }
    public class Distrito
    {
        public string dis_id { get; set; }
        public string dis_prv_id { get; set; }
        public string dis_descripcion { get; set; }       
    }
}
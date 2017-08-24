using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aqmvc.com.pe.Data.Util;

namespace www.aqmvc.com.pe.Data.Control
{
    public class Funcion
    {
        public string fun_id { get; set; }
        public string fun_nombre { get; set; }
        public string fun_descripcion { get; set; }
        public string fun_orden { get; set; }
        public string fun_padre { get; set; }
        public string fun_system { get; set; }

        public Boolean InsertarFuncion()
        {           
            string sqlquery = "USP_Insertar_Funcion";
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
                        cmd.Parameters.AddWithValue("@fun_id", fun_id);
                        cmd.Parameters.AddWithValue("@fun_nombre", fun_nombre);
                        cmd.Parameters.AddWithValue("@fun_descripcion", fun_descripcion);
                        cmd.Parameters.AddWithValue("@fun_orden", fun_orden);
                        cmd.Parameters.AddWithValue("@fun_padre", fun_padre);
                        cmd.Parameters.AddWithValue("@fun_sisid", fun_system);
                        cmd.ExecuteNonQuery();
                        valida = true;
                    }
                }
                
            }
            catch(Exception exc)
            {
                valida = false;
            }
            return valida;
        }

        public List<Funcion> get_lista(Boolean listar=false)
        {
            string sqlquery = "[USP_Leer_Funcion_Sistema]";
            List<Funcion> list = null;
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
                            list = new List<Funcion>();
                            Funcion fun = new Funcion();
                            if (!listar)
                            {                                 
                                fun.fun_id = "0";
                                fun.fun_nombre = "(Vacio)";
                                list.Add(fun);
                            }

                            while (dr.Read())
                            {
                                fun = new Funcion();
                                fun.fun_id = dr["Fun_Id"].ToString();
                                fun.fun_nombre= dr["Fun_Nombre"].ToString();
                                fun.fun_descripcion= dr["Fun_Descripcion"].ToString();
                                fun.fun_orden= dr["Fun_Orden"].ToString();
                                fun.fun_padre = dr["Fun_Padre"].ToString();
                                list.Add(fun);
                            }
                        }
                    }
                }
            }
            catch(Exception exc)
            {
                list = null;
            }
            return list;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aquarella.pe.Data.Util;

namespace www.aquarella.pe.Data.Control
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

        public Boolean EditarFuncion()
        {
            string sqlquery = "USP_Modificar_Funcion";
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
                        cmd.ExecuteNonQuery();
                        valida = true;
                    }
                }
            }
            catch
            {
                valida=false;
            }
            return valida;
        }

        public List<Funcion> get_lista(Boolean listar=false)
        {
            string sqlquery = "[USP_Leer_Funcion_Sistema]";
            List<Funcion> list = null;
            Boolean valida = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand("pruebaNuevo", cn))
                    {
                        cmd.CommandTimeout = 0; cmd.CommandType = CommandType.StoredProcedure;
                      
                        cmd.ExecuteNonQuery();
                        valida = true;
                    }
                }

                //using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                //{
                //    if (cn.State == 0) cn.Open();
                //    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                //    {
                //        cmd.CommandTimeout = 0;
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        SqlDataReader dr = cmd.ExecuteReader();

                //        if (dr.HasRows)
                //        {
                //            list = new List<Funcion>();
                //            Funcion fun = new Funcion();
                //            if (!listar)
                //            {                                 
                //                fun.fun_id = "0";
                //                fun.fun_nombre = "(Vacio)";
                //                list.Add(fun);
                //            }

                //            while (dr.Read())
                //            {
                //                fun = new Funcion();
                //                fun.fun_id = dr["Fun_Id"].ToString();
                //                fun.fun_nombre= dr["Fun_Nombre"].ToString();
                //                fun.fun_descripcion= dr["Fun_Descripcion"].ToString();
                //                fun.fun_orden= dr["Fun_Orden"].ToString();
                //                fun.fun_padre = dr["Fun_Padre"].ToString();
                //                list.Add(fun);
                //            }
                //        }
                //    }
                //}
            }
            catch(Exception exc)
            {
                list = null;
            }
            return list;
        }
    }

    public class FuncionAplicacion
    {
        //public string fun_id { get; set; }
        public string apl_id { get; set; }
        public string apl_nombre { get; set; }

        public Boolean Eliminar_App_Funcion(Decimal _apl_id, decimal _fun_id)
        {
            Boolean valida = false;
            string sqlquery = "USP_Borrar_Apl_Fun";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@apl_fun_aplid",_apl_id);
                        cmd.Parameters.AddWithValue("@apl_fun_funid", _fun_id);
                        cmd.ExecuteNonQuery();
                        valida = true;
                    }
                }
            }
            catch (Exception)
            {

                valida = false;
            }
            return valida;
        }

        public Boolean Insertar_App_Funcion(Decimal _apl_id,decimal _fun_id)
        {
            string sqlquery = "USP_Insertar_Apl_Fun";
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
                        cmd.Parameters.AddWithValue("@apl_fun_aplid", _apl_id);
                        cmd.Parameters.AddWithValue("@apl_fun_funid", _fun_id);
                        cmd.ExecuteNonQuery();
                        valida = true;
                    }
                }
            }
            catch (Exception)
            {

                valida=false;
            }
            return valida;
        }

        public List<FuncionAplicacion> get_lista(decimal fun_id)
        {
            string sqlquery = "USP_Leer_Apl_Fun";
            List<FuncionAplicacion> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Fun_Id", fun_id);
                        SqlDataReader dr = cmd.ExecuteReader();
                        list = new List<FuncionAplicacion>();
                        if (dr.HasRows)
                        {
                            
                            while(dr.Read())
                            {
                                FuncionAplicacion fila = new FuncionAplicacion();
                                fila.apl_id = dr["apl_id"].ToString();
                                fila.apl_nombre = dr["apl_nombre"].ToString();
                                list.Add(fila);
                            }
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
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using www.aquarella.pe.Data.Util;

namespace www.aquarella.pe.Data.Control
{
    public class Roles
    {
        public string rol_id { get; set; }
        public string rol_nombre { get; set; }
        public string rol_descripcion { get; set; }

        public Boolean InsertarRoles()
        {
            string sqlquery = "USP_Insertar_Roles";
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
                        cmd.Parameters.AddWithValue("@rol_id", 0);
                        cmd.Parameters.AddWithValue("@rol_nombre", rol_nombre);
                        cmd.Parameters.AddWithValue("@rol_descripcion", rol_descripcion);
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
        public List<Roles> get_lista()
        {
            List<Roles> list = null;
            string sqlquery = "USP_Leer_Roles";
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
                            list = new List<Roles>();

                            while(dr.Read())
                            {
                                Roles rol = new Roles();
                                rol.rol_id = dr["rol_id"].ToString();
                                rol.rol_nombre=dr["rol_nombre"].ToString();
                                rol.rol_descripcion = dr["rol_descripcion"].ToString();
                                list.Add(rol);
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
        public Boolean EditarRoles()
        {
            Boolean valida = false;
            string sqlquery = "USP_Modificar_Roles";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rol_id", rol_id);
                        cmd.Parameters.AddWithValue("@rol_nombre", rol_nombre);
                        cmd.Parameters.AddWithValue("@rol_descripcion", rol_descripcion);
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
    }
    public class RolesFuncion
    {
        public string fun_id { get; set; }
        public string fun_nombre { get; set; }

        public Boolean Eliminar_Fun_Roles(Decimal _fun_id, decimal _rol_id)
        {
            Boolean valida = false;
            string sqlquery = "USP_Borrar_Roles_Funcion";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0; cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rol_fun_rolid", _rol_id);
                        cmd.Parameters.AddWithValue("@rol_fun_funid", _fun_id);
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
        public Boolean Insertar_Fun_Roles(Decimal _fun_id, decimal _rol_id)
        {
            string sqlquery = "USP_Insertar_Roles_Funcion";
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
                        cmd.Parameters.AddWithValue("@rol_fun_rolid", _rol_id);
                        cmd.Parameters.AddWithValue("@rol_fun_funid", _fun_id);
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
        public List<RolesFuncion> get_lista(decimal rol_id)
        {
            string sqlquery = "USP_Leer_Funcion_Roles";
            List<RolesFuncion> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.conexion_sql))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rol_id", rol_id);
                        SqlDataReader dr = cmd.ExecuteReader();
                        list = new List<RolesFuncion>();
                        if (dr.HasRows)
                        {

                            while (dr.Read())
                            {
                                RolesFuncion fila = new RolesFuncion();
                                fila.fun_id= dr["fun_id"].ToString();
                                fila.fun_nombre = dr["fun_nombre"].ToString();
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
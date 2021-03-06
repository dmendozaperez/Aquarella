﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using CapaEntidad.Bll.Util;
namespace CapaDato.Bll.Control
{
    public class Dat_Acceso
    {
        public static DataTable F_LeerUsuario(string _usv_username)
        {
            DataTable dt = null;
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            string sqlcommand = "USP_Leer_Usuario";
            try
            {
                cn = new SqlConnection(Ent_Conexion.conexion);
                cmd = new SqlCommand(sqlcommand, cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Parameters.AddWithValue("@Usu_Nombre", _usv_username);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch 
            {                
                dt = null;
                throw;
            }
            return dt;
        }
        public static DataSet getSalesCoorByMonthPctg()
        {
            string sqlquery = "USP_Leer_ComisionPersona";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Ent_Conexion.conexion);
                cmd = new SqlCommand(sqlquery, cn);
                //cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@are_id", "-1");
                cmd.Parameters.AddWithValue("@fecha_ini",Convert.ToDateTime("01-01-2019"));
                cmd.Parameters.AddWithValue("@fecha_fin",Convert.ToDateTime("04-12-2019"));
                cmd.Parameters.AddWithValue("@asesor","");              
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static Boolean getpunto_vta(string _entorno)
        {
            Boolean _valida = false;
            string sqlquery = "USP_LEER_PUNTOVENTA";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd=new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@entorno", _entorno);
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                Ent_Global._pvt_id=Convert.ToDecimal(dr["Pvt_Id"]);
                                Ent_Global._pvt_nombre=dr["Pvt_Nombre"].ToString();
                                Ent_Global._pvt_almaid = dr["Pvt_AlmID"].ToString(); 
                                Ent_Global._pvt_directo=Convert.ToBoolean(dr["pvt_directo"]);
                                Ent_Global._igv= Convert.ToDecimal(dr["igv"]);
                                Ent_Global._percepcion = Convert.ToDecimal(dr["percepcion"]);
                                Ent_Global._comision_porc = Convert.ToDecimal(dr["comision_porc"]);
                                Ent_Global._serie_imp= dr["serie_imp"].ToString();
                                Ent_Global._impresora = dr["impresora"].ToString();

                                //if (Ent_Global._canal_venta=="BA") Ent_Global._impresora_etiquetas = dr["impresora_etiq"].ToString();
                                Ent_Global._impresora_etiquetas = dr["impresora_etiq"].ToString();
                            }
                            _valida = true;
                        }
                    }
                }
            }
            catch(Exception  ex)
            {
                _valida = false;
            }
            return _valida;
        }

        //public static DataTable Ecommerce_getConexionesAPI(string nombre, string tipo)
        //{
        //    DataTable dt = null;
        //    SqlConnection cn = null;
        //    SqlCommand cmd = null;
        //    SqlDataAdapter da = null;
        //    string sqlcommand = "USP_Lista_APICourier";
        //    try
        //    {
        //        cn = new SqlConnection(Ent_Conexion.conexion);
        //        cmd = new SqlCommand(sqlcommand, cn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandTimeout = 0;
        //        cmd.Parameters.AddWithValue("@Nombre", nombre);
        //        cmd.Parameters.AddWithValue("@Tipo", tipo);
        //        da = new SqlDataAdapter(cmd);
        //        dt = new DataTable();
        //        da.Fill(dt);
        //    }
        //    catch
        //    {
        //        dt = null;
        //        throw;
        //    }
        //    return dt;
        //}



    }
}

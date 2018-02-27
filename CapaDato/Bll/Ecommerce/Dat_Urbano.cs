﻿using CapaEntidad.Bll.Ecommerce;
using CapaEntidad.Bll.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaDato.Bll.Ecommerce
{
    public class Dat_Urbano
    {
        /// <summary>
        /// acceso de web service de urbano
        /// </summary>
        /// <returns></returns>
        public Ent_Urbano get_acceso()
        {
            string sqlquery = "USP_Urbano_AccesoWS";
            Ent_Urbano _acceso = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType =CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            _acceso = new Ent_Urbano();
                            while(dr.Read())
                            {
                                _acceso.url = dr["urb_url"].ToString();
                                _acceso.usuario = dr["urb_usu"].ToString();
                                _acceso.password = dr["urb_pass"].ToString();
                                _acceso.linea= dr["Urb_Linea"].ToString();
                                _acceso.contrato = dr["Urb_Contrato"].ToString();
                                break;
                            }                           
                        }

                    }
                }
            }
            catch (Exception)
            {
                _acceso=null;
            }
            return _acceso;
        }
        /// <summary>
        /// acceso a los datos a enviar urbano
        /// </summary>
        /// <returns></returns>
        public DataTable get_data()
        {
            DataTable dt = null;
            string sqlquery = "USP_Urbano_SendData";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception)
            {
                dt = null;                
            }
            return dt;
        }
        /// <summary>
        /// update venta guia urbano
        /// </summary>
        /// <param name="guia_prestasop"></param>
        /// <param name="guia_urbano"></param>
        /// <returns></returns>
       public Boolean update_guia(string guia_prestasop,string guia_urbano)
        {
            Boolean valida = false;
            string sqlquery = "USP_Urbano_UpdateGuia";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Ven_Pst_Ref", guia_prestasop);
                        cmd.Parameters.AddWithValue("@Ven_Guia_Urbano", guia_urbano);
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
    }
}

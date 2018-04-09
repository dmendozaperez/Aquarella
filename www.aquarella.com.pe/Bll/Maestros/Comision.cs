using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;

namespace www.aquarella.com.pe.bll
{
    public class Comision
    {
        public int comis_Id { get; set; }
        public string comis_Descripcion { get; set; }
        public Decimal comis_MontoMin { get; set; }
        public Decimal comis_MontoMax { get; set; }
        public Decimal comis_Porcentaje { get; set; }
        public string comis_FechaIni { get; set; }
        public string comis_FechaFin { get; set; }
        public string comis_Estado { get; set; }
        public int comis_usuarioId { get; set; }
        
        public static DataSet GetAllComisionesDS()
        {
            string sqlquery = "USP_Leer_Comisiones";
            SqlConnection cn = new SqlConnection(Conexion.myconexion());
            SqlCommand cmd = new SqlCommand(sqlquery, cn);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;   
                      
        }
        
        public bool InsertarComision()
        {
            string sqlquery = "USP_Insertar_Comision";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
              
                cmd.Parameters.AddWithValue("@comis_descripcion", comis_Descripcion);
                cmd.Parameters.AddWithValue("@comis_porcentaje", comis_Porcentaje);
                cmd.Parameters.AddWithValue("@comis_montoMin", comis_MontoMin);
                cmd.Parameters.AddWithValue("@comis_montoMax", comis_MontoMax);
                cmd.Parameters.AddWithValue("@comis_FecIni", comis_FechaIni);
                cmd.Parameters.AddWithValue("@comis_FecFin", comis_FechaFin);
                cmd.Parameters.AddWithValue("@comis_Estado", comis_Estado);
                cmd.Parameters.AddWithValue("@comis_usuario", comis_usuarioId);
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool updateComision(int comi_id, string comi_Descripcion, string comi_Porcentaje, string comi_MontoMin, string comi_MontoMax, string comi_FechaIni, string comi_FechaFin, string comi_Estado)
        {
            SqlConnection cn = null;
            SqlCommand cmd = null;
            string sqlquery = "USP_Modificar_Comision";
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                if (cn.State == 0) cn.Open();
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@comis_Id", comi_id);
                cmd.Parameters.AddWithValue("@comis_descripcion", comi_Descripcion);
                cmd.Parameters.AddWithValue("@comis_porcentaje", comi_Porcentaje);
                cmd.Parameters.AddWithValue("@comis_montoMin", comi_MontoMin);
                cmd.Parameters.AddWithValue("@comis_montoMax", comi_MontoMax);
                cmd.Parameters.AddWithValue("@comis_FecIni", comi_FechaIni);
                cmd.Parameters.AddWithValue("@comis_FecFin", comi_FechaFin);
                cmd.Parameters.AddWithValue("@comis_Estado", comi_Estado);
                cmd.Parameters.AddWithValue("@comis_usuario", "");

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
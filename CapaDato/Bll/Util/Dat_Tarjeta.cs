using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaEntidad.Bll.Util;
using System.Data.SqlClient;
using System.Data;

namespace CapaDato.Bll.Util
{
    public class Dat_Tarjeta
    {
        public List<Ent_Tarjeta> Leer()
        {
            List<Ent_Tarjeta> tar_list = null;
            string sqlquery = "USP_Leer_Tarjetas";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            tar_list = new List<Ent_Tarjeta>();

                            while(dr.Read())
                            {
                                Ent_Tarjeta add_tar = new Ent_Tarjeta();
                                add_tar.idtarjeta = dr["idtarjeta"].ToString();
                                add_tar.tarnom = dr["tarnom"].ToString();
                                add_tar.tarimagen =(Byte[])dr["tarimagen"];
                                tar_list.Add(add_tar);
                            }
                        }
                    }
                }
            }
            catch
            {
                tar_list=null;
            }
            return tar_list;
        }
        public Boolean insertar(Ent_Tarjeta tar)
        {
            string sqlquery = "USP_Insertar_Tarjeta";
            Boolean _valida = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idtarjeta", tar.idtarjeta);
                        cmd.Parameters.AddWithValue("@tarnom", tar.tarnom);
                        cmd.Parameters.AddWithValue("@tarimagen", tar.tarimagen);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                _valida = false;
            }
            return _valida;
        }
        public Ent_Pines_Tarjeta lista_pines(string bin_tar_cod)
        {
            Ent_Pines_Tarjeta lista = null;
            string sqlquery = "USP_LeerPines";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    lista = new Ent_Pines_Tarjeta();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        cmd.Parameters.AddWithValue("@bin_tar_cod", bin_tar_cod);

                        cmd.Parameters.Add("@existe_bines", SqlDbType.Bit);
                        cmd.Parameters["@existe_bines"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@bin_tar_ser", SqlDbType.VarChar, 50);
                        cmd.Parameters["@bin_tar_ser"].Direction = ParameterDirection.Output;

                        cmd.Parameters.Add("@bin_tar_des", SqlDbType.VarChar, 50);
                        cmd.Parameters["@bin_tar_des"].Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        
                        lista.bin_tar_cod = bin_tar_cod;

                        lista.bin_tar_des =(string)cmd.Parameters["@bin_tar_des"].Value;
                        lista.existe_bines=(Boolean)cmd.Parameters["@existe_bines"].Value;
                        lista.bin_tar_ser = (string)cmd.Parameters["@bin_tar_ser"].Value;
                    }
                }
            }
            catch
            {
                lista = null;
            }
            return lista;
        }
    }
}

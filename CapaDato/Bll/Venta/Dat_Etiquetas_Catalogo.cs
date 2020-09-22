using CapaEntidad.Bll.Util;
using CapaEntidad.Bll.Venta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaDato.Bll.Venta
{
    public class Dat_Etiquetas_Catalogo
    {
        public Ent_Etiquetas_Catalogo get_etiquetas(string ven_id)
        {
            string sqlquery = "USP_AquarellaImpEtiqueta";
            Ent_Etiquetas_Catalogo etiqueta = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ven_id", ven_id);
                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                etiqueta = new Ent_Etiquetas_Catalogo();
                                while (dr.Read())
                                {
                                    etiqueta.liq_tipo_prov = dr["liq_tipo_prov"].ToString();
                                    etiqueta.liq_tipodes = dr["liq_tipodes"].ToString();
                                    etiqueta.provincia = dr["provincia"].ToString();
                                    etiqueta.pedido = dr["pedido"].ToString();
                                    etiqueta.unidades = dr["unidades"].ToString();
                                    etiqueta.lider = dr["lider"].ToString();
                                    etiqueta.promotor = dr["promotor"].ToString();
                                    etiqueta.direccion = dr["direccion"].ToString();
                                    etiqueta.distrito = dr["distrito"].ToString();
                                    etiqueta.referencia = dr["referencia"].ToString();
                                    etiqueta.destino = dr["destino"].ToString();
                                    etiqueta.agencia = dr["agencia"].ToString();
                                    etiqueta.agencia_direccion = dr["agencia_direccion"].ToString();
                                    etiqueta.numdoc = dr["numdoc"].ToString();
                                    etiqueta.tipo_des= dr["tipo_despacho"].ToString();
                                }
                            }

                        }
                    }
                    catch
                    {
                        etiqueta = null;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch
            {
                etiqueta = null;
            }
            return etiqueta;
        }
    }
}

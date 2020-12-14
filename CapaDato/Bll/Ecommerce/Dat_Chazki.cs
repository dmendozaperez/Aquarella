using System;
using System.Collections.Generic;
using System.Linq;
using CapaEntidad.Bll.Ecommerce;

using System.Data.SqlClient;
using System.Data;
using CapaEntidad.Bll.Util;
namespace CapaDato.Bll.Ecommerce
{
    public class Dat_Chazki
    {
        public Ent_Ecommerce_Chazki get_Ventas_por_Chazki(string ven_id)
        {
            Ent_Ecommerce_Chazki ven = null;
            string sqlquery = "USP_ECOMMERCE_LISTA_CHAZKI";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    if (cn.State == 0) cn.Open();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ven_id", ven_id);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            //DataTable dtC = ds.Tables[0];
                            DataTable dtArticulos = ds.Tables[0]; //data articulos
                            DataTable dtChazki = ds.Tables[1]; //data chaski
                            DataTable dtCliente = ds.Tables[2]; //data direccion cliente 
                            ven = new Ent_Ecommerce_Chazki();
                            /*ARTICULOS*/
                            List<Ent_ListaArticulos> listaArticulos = new List<Ent_ListaArticulos>();
                            foreach (DataRow item in dtArticulos.Rows)
                            {
                                Ent_ListaArticulos venD = new Ent_ListaArticulos();
                                venD.codigoProducto = item["codigo_articulo"].ToString();
                                venD.nombreProducto = item["descripcion_articulo"].ToString();
                                venD.total = item["precio_total"].ToString();
                                venD.cantidad = Convert.ToInt32(Convert.ToDouble(item["cantidad"].ToString()));
                                listaArticulos.Add(venD);
                            }
                            ven.listaArticulos = listaArticulos;
                            /*TIENDA ORIGEN*/
                            if (dtChazki.Rows.Count > 0)
                            {
                                Ent_ChazkiApi Chazki = new Ent_ChazkiApi();
                                Chazki.storeId = dtChazki.Rows[0]["chazki_store_id"].ToString();
                                Chazki.branchId = dtChazki.Rows[0]["chazki_branch_id"].ToString();
                                Chazki.api_key = dtChazki.Rows[0]["chaski_api_key"].ToString();
                                Chazki.deliveryTrackCode = dtChazki.Rows[0]["deliveryTrack_Code"].ToString();
                                Chazki.mode = dtChazki.Rows[0]["mode"].ToString();
                                Chazki.time = dtChazki.Rows[0]["tiempo"].ToString();
                                Chazki.paymentMethod = dtChazki.Rows[0]["payment_method"].ToString();
                                ven.informacionTiendaEnvio = Chazki;
                            }
                            /*CLENTE DESTINO*/
                            if (dtCliente.Rows.Count > 0)
                            {
                                Ent_Cliente Cliente = new Ent_Cliente();

                                Cliente.nroDocumento = dtCliente.Rows[0]["dni_ruc"].ToString();
                                Cliente.email = dtCliente.Rows[0]["correo"].ToString();
                                Cliente.cliente = dtCliente.Rows[0]["cliente"].ToString();
                                Cliente.referencia = "";
                                Cliente.telefono = dtCliente.Rows[0]["telefono"].ToString();
                                Cliente.direccion_entrega = dtCliente.Rows[0]["direccion_cliente"].ToString();
                                Cliente.ubigeo = dtCliente.Rows[0]["ubigeo"].ToString();

                                ven.informacionTiendaDestinatario = Cliente;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ven = null;
            }
            return ven;
        }


        public string[] get_des_ubigeo(string ubigeo)
        {
            string sqlquery = "usp_ubigeo";
            string[] desUbigeo = null;
            try
            {
                //Ent_Conexion.conexion = "Server=192.168.1.242;Database=BDPOS;User ID=sa;Password=1;Trusted_Connection=False;";
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    DataTable dt = new DataTable();
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ubigeo", ubigeo);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count == 3)
                        {
                            string distrito = "";
                            if (dt.Rows[2]["des_ubigeo"].ToString() == "CERCADO")
                            {
                                distrito = "LIMA";
                            }
                            else if (dt.Rows[2]["des_ubigeo"].ToString() == "BREÑA")
                            {
                                distrito = "BRENA";
                            }
                            else
                            {
                                distrito = dt.Rows[2]["des_ubigeo"].ToString();
                            }
                            desUbigeo = new string[] { dt.Rows[0]["des_ubigeo"].ToString(), dt.Rows[1]["des_ubigeo"].ToString(), distrito };
                        }
                        else
                        {
                            desUbigeo = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                desUbigeo = null;
            }
            return desUbigeo;
        }

    }
}

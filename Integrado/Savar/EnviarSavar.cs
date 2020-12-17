using CapaDato.Bll.Ecommerce;
using CapaEntidad.Bll.Ecommerce;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Data;
using System.Text.RegularExpressions;

namespace Integrado.Savar
{
    public class EnviarSavar
    {
        /*metodo para savar - ecommerce*/
        public string Envia_Courier_Savar(string ven_id)
        {
            DataTable dtApi_savar = new DataTable();
            Dat_Savar objD_savar = new Dat_Savar();
            List<Ent_Savar> objList_savar = new List<Ent_Savar>();
            Ent_Savar objE_savar = new Ent_Savar();

            dtApi_savar = objD_savar.get_Ventas_por_Savar(ven_id);

            objE_savar.CodPaquete = dtApi_savar.Rows[0]["CodPaquete"].ToString();
            objE_savar.NomRemitente = dtApi_savar.Rows[0]["NomRemitente"].ToString();
            objE_savar.DireccionRemitente = dtApi_savar.Rows[0]["DireccionRemitente"].ToString();
            objE_savar.DistritoRemitente = dtApi_savar.Rows[0]["DistritoRemitente"].ToString();
            objE_savar.TelefonoRemitente = dtApi_savar.Rows[0]["TelefonoRemitente"].ToString();
            objE_savar.CodigoProducto = dtApi_savar.Rows[0]["CodigoProducto"].ToString();
            objE_savar.MarcaProducto = dtApi_savar.Rows[0]["MarcaProducto"].ToString();
            objE_savar.ModeloProducto = dtApi_savar.Rows[0]["ModeloProducto"].ToString();
            objE_savar.ColorProducto = dtApi_savar.Rows[0]["ColorProducto"].ToString();
            objE_savar.TipoProducto = dtApi_savar.Rows[0]["TipoProducto"].ToString();
            objE_savar.DescProducto = dtApi_savar.Rows[0]["DescProducto"].ToString();
            objE_savar.cantidad = Convert.ToInt32(dtApi_savar.Rows[0]["cantidad"].ToString());
            objE_savar.NomConsignado = dtApi_savar.Rows[0]["NomConsignado"].ToString();
            objE_savar.NumDocConsignado = dtApi_savar.Rows[0]["NumDocConsignado"].ToString();
            objE_savar.DireccionConsignado = dtApi_savar.Rows[0]["DireccionConsignado"].ToString();
            objE_savar.DistritoConsignado = dtApi_savar.Rows[0]["DistritoConsignado"].ToString();
            objE_savar.Referencia = dtApi_savar.Rows[0]["Referencia"].ToString();
            objE_savar.TelefonoConsignado = dtApi_savar.Rows[0]["TelefonoConsignado"].ToString();
            objE_savar.CorreoConsignado = dtApi_savar.Rows[0]["CorreoConsignado"].ToString();
            objE_savar.Subservicio = dtApi_savar.Rows[0]["Subservicio"].ToString();
            objE_savar.TipoPago = dtApi_savar.Rows[0]["TipoPago"].ToString();
            objE_savar.MetodoPago = dtApi_savar.Rows[0]["MetodoPago"].ToString();
            objE_savar.Monto = Convert.ToDecimal(dtApi_savar.Rows[0]["Monto"].ToString());
            objE_savar.Largo = Convert.ToDecimal(dtApi_savar.Rows[0]["Largo"].ToString());
            objE_savar.Ancho = Convert.ToDecimal(dtApi_savar.Rows[0]["Ancho"].ToString());
            objE_savar.Peso = Convert.ToDecimal(dtApi_savar.Rows[0]["Peso"].ToString());
            objE_savar.ValorComercial = dtApi_savar.Rows[0]["ValorComercial"].ToString();
            objE_savar.HoraIni1 = dtApi_savar.Rows[0]["HoraIni1"].ToString();
            objE_savar.HoraFin1 = dtApi_savar.Rows[0]["HoraFin1"].ToString();
            objE_savar.HoraIni2 = dtApi_savar.Rows[0]["HoraIni2"].ToString();
            objE_savar.HoraFin2 = dtApi_savar.Rows[0]["HoraFin2"].ToString();
            objE_savar.Comentario = dtApi_savar.Rows[0]["Comentario"].ToString();
            objE_savar.Comentario2 = dtApi_savar.Rows[0]["Comentario2"].ToString();

            objList_savar.Add(objE_savar);
            string jsonSavar = JsonConvert.SerializeObject(objList_savar);
 
            DataTable dtConexion = new DataTable();

            dtConexion = objD_savar.Ecommerce_getConexionesAPI("savar", 1); //conexion de savar
            string retorno = "";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", dtConexion.Rows[0]["Token"].ToString());
                    //client.DefaultRequestHeaders.Add("TOKEN", dtConexion.Rows[0]["Token"].ToString());

                    using (StringContent jsonContent = new StringContent(jsonSavar))
                    {
                        jsonContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var request = client.PostAsync(dtConexion.Rows[0]["Url"].ToString(), jsonContent);

                        string response = request.Result.Content.ReadAsStringAsync().Result;
                        string codseguimiento = Regex.Replace(response, @"[^A-Za-z0-9ñÑ ]+", "");

                        if (objE_savar.CodPaquete == codseguimiento)
                        {
                            retorno = codseguimiento;
                        }
                        else
                        {
                            retorno = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                retorno = "";
            }

            return retorno;
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad.Bll.Ecommerce;
using CapaDato.Bll.Ecommerce;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Integrado.Chazki
{
    public class EnviarChazki
    {
        public string Envia_Courier_chazki(string ven_id)
        {
            string retorno = "";
            try
            {
                /*delivery CHASKI*/
                Dat_Chazki objChazki = new Dat_Chazki();

                Ent_Ecommerce_Chazki cvCzk = objChazki.get_Ventas_por_Chazki(ven_id);

                List<Ent_Chazki> list_chazki = new List<Ent_Chazki>();

                if (cvCzk.listaArticulos != null)
                {
                    /* DATA CHASKI : PRODUCCION*/

                    Ent_Chazki chazki = new Ent_Chazki();
                    chazki.storeId = cvCzk.informacionTiendaEnvio.storeId; // "10411"; // proporcionado por chazki
                    chazki.branchId = cvCzk.informacionTiendaEnvio.branchId; // proporcionado por chazki
                    chazki.deliveryTrackCode = cvCzk.informacionTiendaEnvio.deliveryTrackCode;
                    chazki.proofPayment = "Ninguna"; // por definir la evindencia que será entregada al cliente
                    chazki.deliveryCost = 0;
                    chazki.mode = "Express"; //pendiente definir el modo con el que se va a trabajar el canal de venta.
                    chazki.time = "";
                    chazki.paymentMethod = "Pagado";
                    chazki.country = "PE";

                    /* DATA CHASKI : TEST*/

                    //Ent_Chazki chazki = new Ent_Chazki();
                    //chazki.storeId = "10411";
                    //chazki.branchId = "CCSC-B187";
                    //chazki.deliveryTrackCode = cvCzk.informacionTiendaEnvio.deliveryTrackCode;
                    //chazki.proofPayment = "Ninguna"; // por definir la evindencia que será entregada al cliente
                    //chazki.deliveryCost = 0;
                    //chazki.mode = "Express"; //pendiente definir el modo con el que se va a trabajar el canal de venta.
                    //chazki.time = "";
                    //chazki.paymentMethod = "Pagado";
                    //chazki.country = "PE";

                    /* DATA ARTICULO*/

                    List<Ent_ItemSold_E> listItemSold = new List<Ent_ItemSold_E>();
                    foreach (var producto in cvCzk.listaArticulos)
                    {
                        if (producto.codigoProducto != "9999997")
                        {
                            Ent_ItemSold_E _item = new Ent_ItemSold_E();
                            _item.name = producto.nombreProducto;
                            _item.currency = "PEN";
                            _item.price = Convert.ToDouble(producto.total);
                            _item.weight = 0.3;
                            _item.volumen = 0;
                            _item.quantity = producto.cantidad;
                            _item.unity = "Caja";
                            _item.size = "M";
                            listItemSold.Add(_item);
                        }
                    }

                    //CLIENTE
                    chazki.listItemSold = listItemSold;
                    chazki.notes = "Entregar a Cliente";
                    chazki.documentNumber = cvCzk.informacionTiendaDestinatario.nroDocumento;
                    //chazki.email = "servicio.clientes.peru@bata.com";
                    if (cvCzk.informacionTiendaDestinatario.email == "" || cvCzk.informacionTiendaDestinatario.email == null)
                    {
                        chazki.email = "servicio.clientes.peru @bata.com";
                    }
                    else
                    {
                        chazki.email = cvCzk.informacionTiendaDestinatario.email; //
                    }

                    chazki.phone = cvCzk.informacionTiendaDestinatario.telefono;
                    int CadRuc = cvCzk.informacionTiendaDestinatario.nroDocumento.Length;

                    if (CadRuc > 8)
                    {
                        chazki.documentType = "RUC";
                        chazki.lastName = "";
                        chazki.companyName = cvCzk.informacionTiendaDestinatario.cliente;
                        chazki.name_tmp = "";
                    }
                    else
                    {
                        chazki.documentType = "DNI";
                        chazki.companyName = "";
                        chazki.name_tmp = cvCzk.informacionTiendaDestinatario.cliente;
                        chazki.lastName = "";
                    }
                    /* DATA DIRECCION*/
                    List<Ent_AddressClient_E> listAdressClient = new List<Ent_AddressClient_E>();
                    Ent_AddressClient_E addressClient = new Ent_AddressClient_E();
                    Dat_Chazki datos = new Dat_Chazki();
                    string[] desUbigeo = null;

                    desUbigeo = datos.get_des_ubigeo(cvCzk.informacionTiendaDestinatario.ubigeo);
                    addressClient.nivel_2 = desUbigeo[0];
                    addressClient.nivel_3 = desUbigeo[1];
                    addressClient.nivel_4 = desUbigeo[2];
                    addressClient.name = cvCzk.informacionTiendaDestinatario.direccion_entrega;
                    addressClient.reference = cvCzk.informacionTiendaDestinatario.referencia;
                    addressClient.alias = "No Alias";
                    Ent_Position_E position = new Ent_Position_E();
                    position.latitude = 0;
                    position.longitude = 0;
                    addressClient.position = position;
                    listAdressClient.Add(addressClient);
                    chazki.addressClient = listAdressClient;

                    list_chazki.Add(chazki);

                    string jsonChazki = JsonConvert.SerializeObject(list_chazki);
                    Response_Registro_E rpta = new Response_Registro_E();

                    using (var http = new HttpClient())
                    {
                        http.DefaultRequestHeaders.Add("chazki-api-key", cvCzk.informacionTiendaEnvio.api_key); //PRODUCCION
                        //http.DefaultRequestHeaders.Add("chazki-api-key", "KfXfqgEBhfMK4T8Luw8ba91RynMtjzTY"); //TEST

                        HttpContent content = new StringContent(jsonChazki);
                        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var request = http.PostAsync("https://integracion.chazki.com:8443/chazkiServices/delivery/create/deliveryService", content); //PRODUCCION
                        //var request = http.PostAsync("https://sandboxintegracion.chazki.com:8443/chazkiServices/delivery/create/deliveryService", content); //TEST

                        var response = request.Result.Content.ReadAsStringAsync().Result;
                        rpta = JsonConvert.DeserializeObject<Response_Registro_E>(response);

                        if (rpta.descriptionResponse == "SUCCESS")
                        {
                            retorno = rpta.codeDelivery;
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

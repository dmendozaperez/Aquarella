using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad.Bll.Ecommerce
{
   public  class Ent_Ecommerce_Chazki
    {
        public string NroDocumento { get; set; }
        public string Ruc { get; set; }
        public string Cliente { get; set; }
        public string Tipo { get; set; }
        public string Importe { get; set; }
        public string Fecha { get; set; }
        public string CodSeguimiento { get; set; }
        public string Tienda { get; set; }
        public string CodInterno { get; set; }
        public string Ubigeo { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Referencia { get; set; }
        public string Estado { get; set; }
        public string FlagCourier { get; set; }


        public List<Ent_ListaArticulos> listaArticulos { get; set; }
        public Ent_ChazkiApi informacionTiendaEnvio { get; set; }
        public Ent_Cliente informacionTiendaDestinatario { get; set; }
    }
    public class Ent_ListaArticulos
    {
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public string precioUnitario { get; set; }
        public string descuento { get; set; }
        public string total { get; set; }
        public string talla { get; set; }
        public string fd_colo { get; set; }
    }
    public class Ent_ChazkiApi
    {
        public string storeId { get; set; }
        public string branchId { get; set; }
        public string deliveryTrackCode { get; set; }
        public string proofPayment { get; set; }
        public float deliveryCost { get; set; }
        public string mode { get; set; }
        public string time { get; set; }
        public string paymentMethod { get; set; }
        public string country { get; set; }
        public List<Ent_ItemSold_E> listItemSold { get; set; }
        public string notes { get; set; }
        public string documentNumber { get; set; }
        public string name_tmp { get; set; }
        public string lastName { get; set; }
        public string companyName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string documentType { get; set; }
        public string api_key { get; set; }
        public List<Ent_AddressClient_E> addressClient { get; set; }
    }
    public class Ent_Cliente
    {
        public int id { get; set; }
        public string cliente { get; set; }
        public string dni_ruc { get; set; }
        public string cod_entid { get; set; }
        public string direccion_entrega { get; set; }
        public string referencia { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string nroDocumento { get; set; }
        public string ubigeo { get; set; }



    }
}


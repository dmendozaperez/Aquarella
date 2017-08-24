
namespace www.aquarella.com.pe.bll
{
    public class Transporters_Guides
    {
        #region < Atributos >

        /// <summary>
        /// Id de la guia
        /// </summary>
        public string _tgv_id { get; set; }

        /// <summary>
        /// Numero de la guia
        /// </summary>
        public string _tgv_guide { get; set; }

        /// <summary>
        /// Id tipo de transportadora
        /// </summary>
        public decimal _tgn_transport { get; set; }

        /// <summary>
        /// Nombre del cliente destinatario
        /// </summary>
        public string _tgv_name_cust { get; set; }

        /// <summary>
        /// Telefono del cliente destinatario
        /// </summary>
        public string _tgv_phone_cust { get; set; }

        /// <summary>
        /// Movil del cliente destinatario
        /// </summary>
        public string _tgv_movil_cust { get; set; }

        /// <summary>
        /// Direccion del cliente destinatario
        /// </summary>
        public string _tgv_shipp_add { get; set; }

        /// <summary>
        /// Bloque, barrio, unidad o localidad del cliente destinatario
        /// </summary>
        public string _tgv_shipp_block { get; set; }

        /// <summary>
        /// Ciudad destino
        /// </summary>
        public string _tgv_city { get; set; }

        /// <summary>
        /// Departamento destino
        /// </summary>
        public string _tgv_depto { get; set; }

        /// <summary>
        /// True si configura una direccion de envio de tercero
        /// </summary>
        public bool _configShipping { get; set; }

        #endregion
    }
}
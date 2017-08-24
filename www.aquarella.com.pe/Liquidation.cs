using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace www.aquarella.com.pe.bll.Reports2
{
    public class Liquidation
    {
        #region < Atributos >

        /// <summary>
        /// Informacion de la bodega
        /// </summary>
        public string _wavId { get; set; }
        public string _wavDes { get; set; }
        public string _wavAdd { get; set; }
        public string _wavPhone { get; set; }
        public string _wavUbication { get; set; }

        /// <summary>
        /// Cliente
        /// </summary>
        public string _cusId { get; set; }
        public string _cusDoc { get; set; }
        public string _cusName { get; set; }
        public string _cusAdd { get; set; }
        public string _cusPhone { get; set; }
        public string _cusCellPhone { get; set; }
        public string _cusMail { get; set; }
        public string _cusUbication { get; set; }

        /// <summary>
        /// Liquidacion cabecera
        /// </summary>
        public string _liqNo { get; set; }
        public DateTime _liqDateCreate { get; set; }
        public DateTime _liqDateExp { get; set; }
        public string _liqStatus { get; set; }
        public decimal _liqDctogeneral { get; set; }
        public decimal _liqTaxRate { get; set; }
        public decimal _liqTaxValue { get; set; }
        public decimal _liqHandling { get; set; }
        public decimal _liqpercepcion { get; set; }
        public decimal _liqporcpercepcion { get; set; }
        public decimal _mtoncredito { get; set; }

        public string _idliquidacion { get; set; }
        /// <summary>
        /// Liquidacion detalle
        /// </summary>
        public string _artCode { get; set; }
        public string _artBrand { get; set; }
        public string _artColor { get; set; }
        public string _artName { get; set; }
        public string _artSize { get; set; }
        public decimal _artQty { get; set; }
        public decimal _artPrice { get; set; }
        public decimal _artComm { get; set; }
        public decimal _artDiss { get; set; }



        //subinforme
        public string _ncredito { get; set; }
        public DateTime _fecha { get; set; }
        public decimal _totalcredito { get; set; }

        //total op

        public decimal _totalop { get; set; }

        public Liquidation(string wavId)
        {
            _wavId = wavId;

        }

        #endregion

    }
}
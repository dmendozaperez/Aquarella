using System;
namespace www.aquarella.com.pe.bll.Reports
{
    public class VentaPagoSubInforme
    {
        #region < Atributos >
        public string _pago { get; set; }
        public string _documento { get; set; }
        public DateTime _fecha { get; set; }
        public decimal _total { get; set; }


        public VentaPagoSubInforme(string pago, string documento, DateTime fecha, Decimal total)
        {
            _pago = pago;
            _documento = documento;
            _fecha = fecha;
            _total = total;
        }

        #endregion
    }
}
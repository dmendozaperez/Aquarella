using System;
namespace www.aquarella.com.pe.bll.Reports
{
    public class LiqNcSubinforme
    {
        #region < Atributos >
        public string _liquidacion { get; set; }
        public string _ncredito { get; set; }
        public DateTime _fecha { get; set; }
        public decimal _total { get; set; }


        public LiqNcSubinforme(string liquidacion, string ncredito, DateTime fecha, Decimal total)
        {
            _liquidacion = liquidacion;
            _ncredito = ncredito;
            _fecha = fecha;
            _total = total;
        }

        #endregion

    }
}
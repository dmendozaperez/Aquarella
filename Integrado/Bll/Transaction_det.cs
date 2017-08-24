using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrado.Bll
{
    public class Transaction_det
    {
        #region < Atributos >

        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        /// <summary>
        /// Numero de transaccion tx
        /// </summary>
        public string _idTx { get; set; }
        /// <summary>
        /// Codigo de item
        /// </summary>
        public string _item { get; set; }
        /// <summary>
        /// Nombre de item
        /// </summary>
        public string _article { get; set; }
        /// <summary>
        /// Marca
        /// </summary>
        public string _brand { get; set; }
        /// <summary>
        /// Unidades
        /// </summary>
        public int _units { get; set; }
        /// <summary>
        /// Talla
        /// </summary>
        public string _size { get; set; }
        /// <summary>
        /// Fecha de la transaccion
        /// </summary>
        public DateTime _dateTx { get; set; }
        /// <summary>
        /// Bodega de origen
        /// </summary>
        public string _wareOrig { get; set; }

        #endregion

        #region < VARIABLES >
        private int _tdn_line_no = 0;
        private string _tdv_article = "";
        private string _tdv_size = "";
        private int _tdn_qty = 0;
        private decimal _tdn_odv = 0;
        private decimal _tdn_public_price = 0;
        #endregion

        #region < PROPIEDADES >
        public int tdn_Line_no
        {
            get { return _tdn_line_no; }
            set { _tdn_line_no = value; }
        }

        public string tdv_Article
        {
            get { return _tdv_article; }
            set { _tdv_article = value; }
        }

        public string tdv_Size
        {
            get { return _tdv_size; }
            set { _tdv_size = value; }
        }

        public int tdn_Qty
        {
            get { return _tdn_qty; }
            set { _tdn_qty = value; }
        }

        public decimal tdn_Odv
        {
            get { return _tdn_odv; }
            set { _tdn_odv = value; }
        }

        public decimal tdn_Public_Price
        {
            get { return _tdn_public_price; }
            set { _tdn_public_price = value; }
        }

        public decimal Total_lOdv
        {
            get { return _tdn_qty * _tdn_odv; }
        }

        public decimal Total_lPublic_Price
        {
            get { return _tdn_qty * _tdn_public_price; }
        }
        #endregion

        #region < CONSTRUCTORES >
        public Transaction_det()
        { }

        public Transaction_det(int tdn_line_no, string tdv_article, string tdv_size,
                int tdn_qty, decimal tdn_odv, decimal tdn_public_price)
        {
            _tdn_line_no = tdn_line_no;
            _tdv_article = tdv_article;
            _tdv_size = tdv_size;
            _tdn_qty = tdn_qty;
            _tdn_odv = tdn_odv;
            _tdn_public_price = tdn_public_price;
        }
        #endregion
    }
}

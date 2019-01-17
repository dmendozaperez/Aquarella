
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace www.aquarella.com.pe.bll
{
    public class Order_Dtl
    {
        #region < Atributos >

        /// <summary>
        /// Código de articulo
        /// </summary>
        public string _code { get; set; }
        /// <summary>
        /// Nombre de articulo
        /// </summary>
        public string _artName { get; set; }
        /// <summary>
        /// Marca
        /// </summary>
        public string _brand { get; set; }
        /// <summary>
        /// Imagen de la marca
        /// </summary>
        public string _brandImg { get; set; }
        /// <summary>
        /// Color
        /// </summary>
        public string _color { get; set; }
        /// <summary>
        /// Talla
        /// </summary>
        public string _size { get; set; }
        /// <summary>
        /// Unidades
        /// </summary>
        public int _qty { get; set; }
        /// <summary>
        /// Unidades cancelAQUARELLAs
        /// </summary>
        public int _qtyCancel { get; set; }
        /// <summary>
        /// Mayor categoria
        /// </summary>
        public string _majorCat { get; set; }
        /// <summary>
        /// Categoria
        /// </summary>
        public string _cat { get; set; }
        /// <summary>
        /// Sub-categoria
        /// </summary>
        public string _subcat { get; set; }
        /// <summary>
        /// Origen
        /// </summary>
        public string _origin { get; set; }
        /// <summary>
        /// Descripcion origen
        /// </summary>
        public string _originDesc { get; set; }
        /// <summary>
        /// Comision de articulo, 1-> si, 0-> No
        /// </summary>
        public int _comm { get; set; }
        /// <summary>
        /// Url de fotografia
        /// </summary>
        public string _uriPhoto { get; set; }
        /// <summary>
        /// Precio publico
        /// </summary>
        public decimal _price { get; set; }
        //varibale del precio de vanta al publico uncluido igv
        public decimal _priceigv { get; set; }

        /// <summary>
        /// Formato moneda del precio publico
        /// </summary>
        public string _priceDesc { get; set; }
        //variable de tipo string
        public string _priceigvDesc { get; set; }
        /// <summary>
        /// Comision valor
        /// </summary>
        public decimal _commission { get; set; }
        public decimal _commissionigv { get; set; }
        public Decimal _det_dcto_sigv { get; set; }
        /// <summary>
        /// % de comision
        /// </summary>
        public decimal _commissionPctg { get; set; }

        /// <summary>
        /// Formato de moneda del valor de la comision
        /// </summary>
        public string _commissionDesc { get; set; }
        public string _commissionigvDesc { get; set; }
        /// <summary>
        /// Valor descuento sobre el item
        /// </summary>
        public decimal _dscto { get; set; }
        /// <summary>
        /// Formato moneda del valor del descuento item
        /// </summary>
        public string _dsctoDesc { get; set; }
        /// <summary>
        /// % de descuento item
        /// </summary>
        public decimal _dsctoPerc { get; set; }
        /// <summary>
        /// Valor de descuento item
        /// </summary>
        public decimal _dsctoVale { get; set; }
        /// <summary>
        /// Formato moneda del valor del descuento
        /// </summary>
        public string _dsctoValeDesc { get; set; }
        /// <summary>
        /// Mensaje del descuento
        /// </summary>
        public string _dsctoMsg { get; set; }
        /// <summary>
        /// Total neto de la linea
        /// </summary>
        public decimal _lineTotal { get; set; }
        /// <summary>
        /// Formato moneda del total de la linea
        /// </summary>
        public string _lineTotDesc { get; set; }

        public string _ap_percepcion { get; set; }

        public string _ofe_Tipo { get; set; }
        public Decimal _ofe_PrecioPack { get; set; }

        public decimal _ofe_id { get; set; }
        public Decimal _ofe_maxpares { get; set; }
        public Decimal _ofe_porc { get; set; }

        /// <summary>
        /// Unidades
        /// </summary>
        public int _units { get; set; }

        public string _premio { get; set; }

        public string _premioDesc { get; set; }
        public string _premId { get; set; }

        /// <summary>
        /// Nombre de conexion a bd
        /// </summary>


        #endregion

        #region < Metodos estaticos >

        /// <summary>
        /// Crear una nueva linea de pedido
        /// </summary>
        /// <param name="dtArt"></param>
        /// <param name="dtDisscArt"></param>
        /// <returns></returns>
        public static Order_Dtl getNewLineOrder(DataTable dtArt)
        {
            Order_Dtl order = new Order_Dtl();

            string currency = System.Configuration.ConfigurationManager.AppSettings["kCurrency"];

            if (dtArt == null || dtArt.Rows.Count == 0)
                return null;

            DataRow dr = dtArt.Rows[0];

            order = new Order_Dtl
            {
                _code = dr["Art_Id"].ToString(),
                _artName = dr["Art_Descripcion"].ToString(),
                _brand = dr["Mar_Descripcion"].ToString(),
                //_brandImg = dr["brv_image"].ToString(),
                _color = dr["Col_Descripcion"].ToString(),
                _majorCat = dr["Cat_Pri_Descripcion"].ToString(),
                _cat = dr["Cat_Descripcion"].ToString(),
                _subcat = dr["Sca_Descripcion"].ToString(),
                //_origin = dr["arv_origin"].ToString(),
                //_originDesc = dr["arv_origin"].ToString().Equals(Constants.IdOriginImported) ? "Artículo importado" : "Artículo nacional",
                _comm = (int)Convert.ToInt16(dr["Art_Comision"]),
                _uriPhoto = dr["Art_Foto"].ToString(),
                _price = Convert.ToDecimal(dr["Art_Pre_Sin_Igv"]),
                _priceDesc = Convert.ToDecimal(dr["Art_Pre_Sin_Igv"]).ToString(currency),
                _dsctoDesc = order._dscto.ToString(currency),
                _priceigv = Convert.ToDecimal(dr["Art_Pre_Con_Igv"]),
                _priceigvDesc = Convert.ToDecimal(dr["Art_Pre_Con_Igv"]).ToString(currency),
                _ap_percepcion = dr["Afec_Percepcion"].ToString(),
                _ofe_Tipo = dr["Ofe_tipo"].ToString(),
                _ofe_PrecioPack = Convert.ToDecimal(dr["Ofe_ArtVenta"]),
                _ofe_id =Convert.ToDecimal(dr["Ofe_Id"]),
                _ofe_maxpares=Convert.ToDecimal(dr["Ofe_MaxPares"]),
                _ofe_porc=Convert.ToDecimal(dr["Ofe_Porc"])
            };

            //if (dtDisscArt == null || dtDisscArt.Rows.Count == 0)
            //    return order;

            //dr = dtDisscArt.Rows[0];

            //order._dscto = Convert.ToDecimal(dr["valdiscount"]);
            //order._dsctoDesc = order._dscto.ToString(currency);
            //order._dsctoPerc = Convert.ToDecimal(dr["porcentaje"]);
            //order._dsctoVale = order._price - order._dscto;
            //order._dsctoValeDesc = order._dsctoVale.ToString(currency);
            //order._dsctoMsg = dr["div_message"].ToString();

            return order;
        }

        public static Order_Dtl getNewLineOrderPremio(DataTable dtArt)
        {
            Order_Dtl order = new Order_Dtl();

            string currency = System.Configuration.ConfigurationManager.AppSettings["kCurrency"];

            if (dtArt == null || dtArt.Rows.Count == 0)
                return null;

            DataRow dr = dtArt.Rows[0];

            order = new Order_Dtl
            {
                _code = dr["Art_Id"].ToString(),
                _artName = dr["Art_Descripcion"].ToString(),
                _brand = dr["Mar_Descripcion"].ToString(),
                //_brandImg = dr["brv_image"].ToString(),
                _color = dr["Col_Descripcion"].ToString(),
                _majorCat = dr["Cat_Pri_Descripcion"].ToString(),
                _cat = dr["Cat_Descripcion"].ToString(),
                _subcat = dr["Sca_Descripcion"].ToString(),
                //_origin = dr["arv_origin"].ToString(),
                //_originDesc = dr["arv_origin"].ToString().Equals(Constants.IdOriginImported) ? "Artículo importado" : "Artículo nacional",
                _comm = (int)Convert.ToInt16(dr["Art_Comision"]),
                _uriPhoto = dr["Art_Foto"].ToString(),
                _price = Convert.ToDecimal(dr["Art_Costo"]),
                _priceDesc = Convert.ToDecimal(dr["Art_Costo"]).ToString(currency),
                _dsctoDesc = order._dscto.ToString(currency),
                _priceigv = Convert.ToDecimal(dr["Art_Costo"]),
                _priceigvDesc = Convert.ToDecimal(dr["Art_Costo"]).ToString(currency),
                _ap_percepcion = dr["Afec_Percepcion"].ToString(),
                _ofe_id = Convert.ToDecimal(dr["Ofe_Id"]),
                _ofe_maxpares = Convert.ToDecimal(dr["Ofe_MaxPares"]),
                _ofe_porc = Convert.ToDecimal(dr["Ofe_Porc"])
            };

            //if (dtDisscArt == null || dtDisscArt.Rows.Count == 0)
            //    return order;

            //dr = dtDisscArt.Rows[0];

            //order._dscto = Convert.ToDecimal(dr["valdiscount"]);
            //order._dsctoDesc = order._dscto.ToString(currency);
            //order._dsctoPerc = Convert.ToDecimal(dr["porcentaje"]);
            //order._dsctoVale = order._price - order._dscto;
            //order._dsctoValeDesc = order._dsctoVale.ToString(currency);
            //order._dsctoMsg = dr["div_message"].ToString();

            return order;
        }

        public static Order_Dtl getNewLineOrder(DataRow dr)
        {
            Order_Dtl order = new Order_Dtl();

            string currency = System.Configuration.ConfigurationManager.AppSettings["kCurrency"];

            order = new Order_Dtl
            {
                _code = dr["arv_article"].ToString(),
                _artName = dr["arv_name"].ToString(),
                _brand = dr["brv_description"].ToString(),
                _brandImg = dr["brv_image"].ToString(),
                _color = dr["cov_description"].ToString(),
                _majorCat = dr["mcv_description"].ToString(),
                _cat = dr["cav_description"].ToString(),
                _subcat = dr["scv_description"].ToString(),
                _origin = dr["arv_origin"].ToString(),
                _originDesc = dr["arv_origin"].ToString().Equals(Constants.IdOriginImported) ? "Artículo importado" : "Artículo nacional",
                _comm = (int)Convert.ToInt16(dr["arn_commission"]),
                _uriPhoto = dr["arv_photo"].ToString(),
                _price = Convert.ToDecimal(dr["prn_public_price"]),
                _priceDesc = Convert.ToDecimal(dr["prn_public_price"]).ToString(currency),
                _dsctoDesc = order._dscto.ToString(currency),
                _units = int.Parse(dr["son_qty"].ToString())
            };

            return order;
        }

        /// <summary>
        /// Consultar detalle de un pedido
        /// </summary>
        /// <param name="_co"></param>
        /// <param name="_noOrder"></param>
        /// <returns></returns>
        public static DataTable getDtlOrder(string _noOrder)
        {
            //DataTable dt = new DataTable();
            //return dt;
            //DataTable dtResult = new DataTable();
            string sqlquery = "USP_Leer_Pedido";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {

                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Ped_Id", _noOrder);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;                
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        public static DataTable getDtlliqui( string _noOrder)
        {
            string sqlquery = "USP_Leer_Liquidacion";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataTable dt = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Liq_Id", _noOrder);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                return dt;               
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion

    }
    public class Order_Dtl_Temp
    {
        /// <summary>
        /// numero de item de la fila
        /// </summary>
        public Int32 items { get; set; }
        /// <summary>
        /// codigo de articulo
        /// </summary>
        public string articulo { get; set; }
        /// <summary>
        /// talla del articulo
        /// </summary>
        public string talla { get; set; }
        /// <summary>
        /// cantidad del producto
        /// </summary>
        public Decimal cantidad { get; set; }

        public string  premio { get; set; } 

        public string premId { get; set; }
    }
}
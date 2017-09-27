using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.bll.Util;
using System.Data.SqlClient;
//using Bata.Aquarella.BLL.Util;
//using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Globalization;
using System.Collections.ObjectModel;
namespace www.aquarella.com.pe.bll
{
    public class Basic_Data_Valores
    {
        /// <summary>
        /// Código de compañia
        /// </summary>

        public string _bdvCo { set; get; }

        /// <summary>
        /// Código del cliente.
        /// </summary>

        public string _bdnId { set; get; }

        /// <summary>
        /// Nombre del cliente.
        /// </summary>

        public string _bdvFirstName { set; get; }

        /// <summary>
        /// Apellido del cliente.
        /// </summary>

        public string _bdvLastName { set; get; }

        /// <summary>
        /// Nombres y apellidos del cliente(Nombre Completo).
        /// </summary>

        public string _bdvFullName { set; get; }

        /// <summary>
        /// Documento de identificación del cliente.
        /// </summary>

        public string _bdvDocumentNo { set; get; }

        /// si es 1:DNI  o   2:RUC 

        public string _bdvDocumentTypeID { set; get; }

        //valor de IGV

        public decimal _bdvIGV { set; get; }


        /// <summary>
        /// Descripción de ubicación del cliente.
        /// </summary>

        public string _ubicationCustomer { set; get; }

        /// <summary>
        /// Telefono
        /// </summary>

        public string _bdvPhone { set; get; }

        /// <summary>
        /// Nombre de usuario
        /// </summary>

        public string _usvusername { set; get; }

        /// <summary>
        /// Nombre de area
        /// </summary>

        public string _areaDescription { get; set; }

        /// <summary>
        /// Area asociada
        /// </summary>

        public string _bdvArea { get; set; }

        /// <summary>
        /// Bodega asociada
        /// </summary>

        public string _usvWare { get; set; }

        /// <summary>
        /// Nombre de la bodega
        /// </summary>

        public string _usvWareDesc { get; set; }

        /// <summary>
        /// El usuario es empleado
        /// </summary>

        public bool _isEmployee { get; set; }

        /// <summary>
        /// Estado del usuario
        /// </summary>

        public string _usvStatus { get; set; }

        /// <summary>
        /// Password encriptado
        /// </summary>

        public string _usvPass { get; set; }
    }
    public class Invoice_hdr
    {
        /// <summary>
        /// Código de la compañia.
        /// </summary>
        /// 
        #region<Atributos>

        public string _ihv_co { set; get; }

        /// <summary>
        /// Código o número de la factura.
        /// </summary>

        public string _ihv_invoice_no { set; get; }


        /// <summary>
        /// Fecha de la factura.
        /// </summary>
        public System.DateTime _ihd_date { set; get; }


        /// <summary>
        /// Número o código de la liquidación.
        /// </summary>

        public string _ihv_liquidation { set; get; }

        /// <summary>
        /// Valor Envío.
        /// </summary>

        public decimal _ihn_handling { set; get; }

        /// <summary>
        ///
        /// </summary>

        public decimal _ihn_prt_cnt { set; get; }

        /// <summary>
        ///
        /// </summary>

        public string _ihv_transaction { set; get; }

        /// <summary>
        ///
        /// </summary>

        public decimal _ihn_pointsale { set; get; }

        /// <summary>
        ///
        /// </summary>

        public decimal _ihn_person { set; get; }

        /// <summary>
        ///
        /// </summary>

        public decimal _ihn_taxes { set; get; }

        ////percepcion

        public double _percepcion { set; get; }


        public decimal _porcpercepcion { set; get; }
        /// <summary>
        ///
        /// </summary>
        /// 

        public string _estadotk { set; get; }


        public decimal _ihn_numinvoice_id { set; get; }

        /// <summary>
        ///
        /// </summary>

        public string _wav_description { set; get; }

        /// <summary>
        ///
        /// </summary>

        public string _wav_address { set; get; }



        /// <summary>
        ///
        /// </summary>

        public string _telefonofijo { set; get; }


        /// <summary>
        ///
        /// </summary>

        public string _nuv_typeresolution { set; get; }

        /// <summary>
        ///
        /// </summary>

        public decimal _ion_disscount { set; get; }

        /// <summary>
        ///
        /// </summary>

        public string _nuv_resolution { set; get; }

        /// <summary>
        /// 
        /// </summary>

        public System.DateTime _nud_date { set; get; }

        /// <summary>
        ///
        /// </summary>

        public string _nuv_code { set; get; }

        /// <summary>
        ///
        /// </summary>

        public decimal _nun_invoice_start { set; get; }

        /// <summary>
        ///
        /// </summary>

        public decimal _nun_invoice_end { set; get; }


        /// <summary>
        /// 
        /// </summary>

        public www.aquarella.com.pe.bll.Basic_Data_Valores _datacustomer { set; get; }


        /// <summary>
        ///
        /// </summary>

        public string _trv_name { set; get; }


        public string _concepto { set; get; }


        public string _numTarCredito { set; get; }

        /// <summary>
        ///
        /// </summary>

        public string _tgv_guide { set; get; }

        /// <summary>
        ///
        /// </summary>

        public string _imv_text { set; get; }

        #endregion
    }
    public class InvoiceDtl
    {
        /// <summary>
        /// Código de la compañia.
        /// </summary>

        public string _idv_co { set; get; }

        /// <summary>
        /// Número o código de la factura.
        /// </summary>

        public string _idv_invoice { set; get; }

        /// <summary>
        /// Linea del articulo.
        /// </summary>

        public decimal _idn_line { set; get; }

        /// <summary>
        /// Código del artículo.
        /// </summary>

        public string _idv_article { set; get; }

        /// <summary>
        /// Tamaño del artículo.
        /// </summary>

        public string _idv_size { set; get; }

        /// <summary>
        /// Cantidad del artículo.
        /// </summary>

        public decimal _idn_qty { set; get; }

        /// <summary>
        /// Order.
        /// </summary>

        public string _idv_order { set; get; }

        /// <summary>
        /// 
        /// </summary>

        public decimal _idn_odv { set; get; }

        /// <summary>
        /// Precio.
        /// </summary>

        public decimal _idn_sellprice { set; get; }

        /// <summary>
        /// Descuento.
        /// </summary>

        public decimal _idn_disscount { set; get; }

        /// <summary>
        /// Comision.
        /// </summary>

        public decimal _idn_commission { set; get; }

        /// <summary>
        /// Comision.
        /// </summary>

        public decimal _articlevalue { set; get; }

        /// <summary>
        /// Order.
        /// </summary>

        public string _arv_name { set; get; }

        /// <summary>
        /// Order.
        /// </summary>

        public string _cov_description { set; get; }
    }

    public class invoice
    {
        #region <Variables de Formato de Tickets>
        static DataSet _data;
        static string _strInvoicePrint = "";
        static int _recLineChars = 0;
        static string _strDec = "";
        static CultureInfo _myCIintl = new CultureInfo("es-PE");
        static Invoice_hdr _InvoiceHdr = new Invoice_hdr();
        static ObservableCollection<InvoiceDtl> _InvoiceDtlCollection = new ObservableCollection<InvoiceDtl>();
        static string SetCentre = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' });
        static string SetNormal = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'N' });
        static string SetBold = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'b', (byte)'C' });
        static string SetDetalle = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'1', (byte)'C' });
        static string SetDoubleWide = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'C' });
        static Int32 VAnchoTicket = 39;
        #endregion

        #region < Atributos >
        /// <summary>
        /// Nombre de cadena de conexion en el web.config
        /// </summary>
        //public static string _conn = Constants.OrcleStringConn;

        #endregion
        #region <Metodos Estaticos>

        public static DataTable getventazonacategoria(DateTime _fecha_ini,DateTime _fecha_fin,string _asesor ,
	                                                String _lider,String _dep,String _prov ,String _categ,String _linea)
        {
            string sqlquery = "USP_Leer_VenZonCat";
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
                cmd.Parameters.AddWithValue("@fecha_ini", _fecha_ini);
                cmd.Parameters.AddWithValue("@fecha_fin", _fecha_fin);
                cmd.Parameters.AddWithValue("@asesor", _asesor);
                cmd.Parameters.AddWithValue("@lider", _lider);
                cmd.Parameters.AddWithValue("@dep",_dep);
                cmd.Parameters.AddWithValue("@prov", _prov);
                cmd.Parameters.AddWithValue("@linea", _linea);
                cmd.Parameters.AddWithValue("@categ", _categ);
                da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }
        public static DataSet getventaunmo(DateTime _date_start, DateTime _date_end)
        {
            DataSet ds = null;
            return ds;
            //try
            //{
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "VENTAS.USP_GetVentasResumido";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _date_start, _date_end, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        public static DataSet getventaresumido(Int16 _anio, Int16 _semana)
        {
            DataSet ds = null;
            return ds;
            //try
            //{
            //    object results = new object[1];
            //    Database db = DatabaseFactory.CreateDatabase(_conn);
            //    String sqlCommand = "VENTAS.USP_GETVENTA_SEMANAL";
            //    DbCommand dbCommandWrapper = db.GetStoredProcCommand(sqlCommand, _anio, _semana, results);
            //    return db.ExecuteDataSet(dbCommandWrapper);
            //}
            //catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        private static DataSet getallinvoice(string invoice_no,Int32 pvt_id=1)
        {
            string sqlquery = "USP_Leer_Venta_Imprimir";
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                cn = new SqlConnection(Conexion.myconexion());
                cmd = new SqlCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ven_id", invoice_no);
                cmd.Parameters.AddWithValue("@pvt_id", pvt_id);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        #endregion

        #region <Region de Formato de Tickets>

        public static string get_formatoTickets(string invoice_no, Int32 pvt_id = 1)
        {
            //consulta de venta invoice y invoice detalle
            DataSet dsinvoice = getallinvoice(invoice_no, pvt_id);

            if (dsinvoice.Tables[0].Rows.Count == 0)
            {
                return "0";
            }

            //formato del tickets de venta
            String FormatoTk = getPosPrinter(dsinvoice, "", _myCIintl, 39, "C2", "0", "FFGF100701", "FFGF100708", "9NE");
            //retorna el formato de tickets
            return FormatoTk;
        }

        public static DataTable get_ventadetcn(DateTime fecini, DateTime fecfin,string conid)
        {
            DataTable dt = null;
            string sqlquery = "USP_ConsultaVentaDetCN";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.myconexion()))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fecha_ini", fecini);
                        cmd.Parameters.AddWithValue("@fecha_fin", fecfin);
                        cmd.Parameters.AddWithValue("@con_id", conid);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch
            {
                dt = null;
            }
            return dt;
        }
        public static DataTable getconepto_ce()
        {
            string sqlquery = "USP_LeerFormaPagoCN";
            DataTable dt = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.myconexion()))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception)
            {
                dt = null;                
            }
            return dt;
        }

        private static string getPosPrinter(DataSet dsInvoice, string strFilePath, CultureInfo myCIintl, int recLineChars, string strDecimal, string strTipo, string StrSerieFactura, string StrSerieBoleta, string StrConceptoVisaUnica)
        {
            string imprimetk = "";
            try
            {

                _strInvoicePrint = "";
                _data = new DataSet();
                //_invoice = new DALInvoice();
                _myCIintl = myCIintl;
                _recLineChars = recLineChars;
                _strDec = strDecimal;
                Invoice_hdr InvoiceHdr = new Invoice_hdr();
                // Obtengo funciones
                _data = dsInvoice;

                DataTable dt_venta = dsInvoice.Tables[0];




                //declaracion de varibale de tipo ref para dar formato al tickets
                string VCadFc = "";
                if (dt_venta != null)
                {
                    if (dt_venta.Rows.Count > 0)
                    {
                        StrSerieBoleta = dt_venta.Rows[0]["serie_impresora"].ToString();
                        StrSerieFactura = dt_venta.Rows[0]["serie_impresora"].ToString();
                        PrintReceiptHeader(
                            dt_venta.Rows[0]["Alm_Descripcion"].ToString(),
                            dt_venta.Rows[0]["Alm_Direccion"].ToString(), " Telefono " + dt_venta.Rows[0]["Alm_Telefono"].ToString(),
                            dt_venta.Rows[0]["Emp_Razon"].ToString(),
                            dt_venta.Rows[0]["Emp_Ruc"].ToString(),
                            dt_venta.Rows[0]["nrodoc"].ToString(),
                            dt_venta.Rows[0]["ven_detalle"].ToString(),
                            dt_venta.Rows[0]["nombres"].ToString(),
                            dt_venta.Rows[0]["Bas_Doc_Tip_Id"].ToString(),
                            dt_venta.Rows[0]["Bas_Documento"].ToString(),
                            Convert.ToDateTime(dt_venta.Rows[0]["Ven_Fecha"].ToString()),
                            "VENTA",
                            "000000",
                            "Empleado Nombre", strFilePath, strTipo, StrSerieFactura, StrSerieBoleta, StrConceptoVisaUnica, ref VCadFc);
                       
                        decimal dSubtotal = 0;
                        decimal dDiscount = 0;

                        decimal _igv = Convert.ToDecimal(dt_venta.Rows[0]["Ven_Igv_Porc"].ToString());
                        decimal _igv_monto = Convert.ToDecimal(dt_venta.Rows[0]["Ven_Igv"].ToString());
                        string _tipodoc = dt_venta.Rows[0]["Bas_Doc_Tip_Id"].ToString();
                        decimal _percepcionp = Convert.ToDecimal(dt_venta.Rows[0]["Ven_PercepcionP"].ToString());
                        double _percepcionm = Convert.ToDouble(dt_venta.Rows[0]["Ven_PercepcionM"].ToString());
                        String _estadook = dt_venta.Rows[0]["EstadoOk"].ToString();
                        for (Int32 i = 0; i < dt_venta.Rows.Count; ++i)
                        {

                            string _iarticulo = dt_venta.Rows[i]["Art_Id"].ToString();
                            string _articulonombre = dt_venta.Rows[i]["Art_Descripcion"].ToString();
                            string _talla = dt_venta.Rows[i]["Ven_Det_TalId"].ToString();
                            string _cantidad = dt_venta.Rows[i]["Ven_Det_Cantidad"].ToString();
                            Decimal _articulo_total = Convert.ToDecimal(dt_venta.Rows[i]["articulo_total"].ToString());
                            decimal _comision = Convert.ToDecimal(dt_venta.Rows[i]["Ven_Det_ComisionM"].ToString());
                            PrintLineItem(_iarticulo, _articulonombre,
                            _talla, int.Parse(_cantidad),
                            Double.Parse(_articulo_total.ToString()),
                            _tipodoc, _igv, _comision,ref VCadFc);
                            PrintText(SetNormal + TruncateAt(_articulonombre.PadRight(24), 24) + "         ");
                            dSubtotal += _articulo_total;
                            dDiscount += _comision;
                        }

                        PrintReceiptFooter(Double.Parse(dSubtotal.ToString()), _igv,
                            Double.Parse(_igv_monto.ToString()), Double.Parse((dDiscount - 0).ToString()), "*** GRACIAS POR SU COMPRA ***",
                            "Facturacion " + "0" + " por Res. Dian ",
                            "1" + " De " + "" + " Del", "Pref " + "" + "-" + "" + " Al Pref " + "" + "-" + "",
                            _tipodoc, _percepcionm, _percepcionp, _estadook,ref VCadFc);

                    }
                }
                //   aca arma la cadena menos el foot (pie de pagina)
              

                imprimetk = VCadFc;

            }
            catch (Exception)
            {

                throw;
            }
            return imprimetk; //_strInvoicePrint;
        }

        private static void PrintReceiptHeader(string strDesAlmacen, string strAddress, string varTelefono, string strCompanyName, string strNitNumber,
           string strInvoiceNumber, string strTitle, string strCustomer, string strDocumentTypeID, string strCustomerId, DateTime dateTime, string concepto, string numTarjetaCred, string strEmployeeName, string strFilePath,
           string strTipo, string StrSerieFactura, string StrSerieBoleta, string StrConceptoVisaUnica, ref string VCadFc)
        {


            string VCadena = "";

            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, strDesAlmacen.Length, strDesAlmacen);
            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, strAddress.Length, strAddress);

            if (strAddress.Length > VAnchoTicket)
            {
                VCadFc += "\r\n";
                VCadena = strAddress.Substring(VAnchoTicket + 1, (strAddress.Length - VAnchoTicket) - 1);
                VCadFc += Alineacion("C", VAnchoTicket, VCadena.Length, VCadena);
            }
            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, varTelefono.Length, varTelefono);
            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, strCompanyName.Length, strCompanyName);
            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, strNitNumber.Length, strNitNumber);
            VCadFc += "\r\n";
            VCadFc += Alineacion("I", VAnchoTicket, String.Format("Autorizacion : 0011845040581").Length, String.Format("Autorizacion : 0011845040581"));

            // Para saber si es RUC y lo pinto en el ticket, 2 es ruc, 1 es DNI
            VCadFc += "\r\n";
            if (strDocumentTypeID == "2")
            {
                VCadFc += Alineacion("I", VAnchoTicket, String.Format("Impresora : {0}", StrSerieFactura).Length, String.Format("Impresora : {0}", StrSerieFactura));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", VAnchoTicket, String.Format("FACTURA: {0}", strInvoiceNumber).Length, String.Format("FACTURA: {0}", strInvoiceNumber));
                VCadFc += "\r\n";
            }

            else if (strDocumentTypeID == "1")
            {
                VCadFc += Alineacion("I", VAnchoTicket, String.Format("Impresora : {0}", StrSerieBoleta).Length, String.Format("Impresora : {0}", StrSerieBoleta));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", VAnchoTicket, String.Format("TICKET: {0}", strInvoiceNumber).Length, String.Format("TICKET: {0}", strInvoiceNumber));
                VCadFc += "\r\n";
            }

            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, strTitle.Length, strTitle);
            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, new string('=', _recLineChars).Length, new string('=', _recLineChars));
            VCadFc += "\r\n";
            VCadFc += Alineacion("I", VAnchoTicket, String.Format("Cliente : ").Length, String.Format("Cliente : "));
            VCadFc += "\r\n";
            VCadFc += Alineacion("I", VAnchoTicket, String.Format(strCustomer).Length, String.Format(strCustomer));
            VCadFc += "\r\n";


            // Para saber si es RUC y lo pinto en el ticket, 2 es ruc
            if (strDocumentTypeID == "2")
            {
                VCadFc += Alineacion("I", VAnchoTicket, String.Format("R.U.C. : {0}", strCustomerId).Length, String.Format("R.U.C. : {0}", strCustomerId));
            }
            else if (strDocumentTypeID == "1")
            {
                VCadFc += Alineacion("I", VAnchoTicket, String.Format("DNI : {0}", strCustomerId).Length, String.Format("DNI : {0}", strCustomerId));
            }
            VCadFc += "\r\n";
            VCadFc += Alineacion("I", VAnchoTicket, String.Format("Fecha : {0}", dateTime.ToString("dd/MM/yyyy ") + ", " + dateTime.ToString("HH:mm:ss")).Length, String.Format("Fecha : {0}", dateTime.ToString("dd/MM/yyyy ") + ", " + dateTime.ToString("HH:mm:ss")));

            if (concepto == StrConceptoVisaUnica)
            {
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", VAnchoTicket, String.Format("VISA UNICA : {0}", numTarjetaCred).Length, String.Format("VISA UNICA : {0}", numTarjetaCred));

            }
            VCadFc += "\r\n";
            VCadFc += "\r\n";
            VCadFc += Alineacion("I", 11, "Codigo".Length, "Codigo");
            VCadFc += Alineacion("I", 14, " Descr.".Length, " Descr.");
            VCadFc += Alineacion("C", 5, "Cnt".Length, "Cnt");
            VCadFc += Alineacion("D", 9, "Precio".Length, "Precio");
            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, new string('=', _recLineChars).Length, new string('=', _recLineChars));
            VCadFc += "\r\n";

        }

        public static void PrintTextLine(string text)
        {
            if (text.Length <= _recLineChars)
                _strInvoicePrint += text + System.Environment.NewLine;
            else if (text.Length > _recLineChars)
                _strInvoicePrint += TruncateAt(text, _recLineChars) + Environment.NewLine;
        }
        public static void PrintText(string text)
        {
            if (text.Length <= _recLineChars)
                _strInvoicePrint += text;
            else if (text.Length > _recLineChars)
                _strInvoicePrint += TruncateAt(text, _recLineChars);
        }
        public static void PrintTextLineCompleta(string text)
        {
            _strInvoicePrint += text + System.Environment.NewLine;
        }

        public static string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);
            return retVal;
        }
        private static void PrintLineItem(string itemCode, string item, string size, int quantity, double total, string varDocumentTypeID, decimal varIGV, decimal varIdn_commission, ref string VCadFc)
        {
            VCadFc += "\r\n";
            VCadFc += Alineacion("I", 11, itemCode.Length, itemCode);
            VCadFc += Alineacion("I", 10, item.Length, item);
            VCadFc += Alineacion("I", 4, size.Length, size);
            VCadFc += Alineacion("C", 5, quantity.ToString("#0").Length, quantity.ToString("#0"));

            if (varDocumentTypeID == "2")  // ruc es 2, DNI es 1
            {
                VCadFc += Alineacion("D", 9, total.ToString("#0.00").Length, total.ToString("#0.00"));
                VCadFc += "\r\n";
            }
            else
            {
                decimal vartotalTax = decimal.Parse(total.ToString());

                VCadFc += Alineacion("D", 9, vartotalTax.ToString("#0.00").Length, vartotalTax.ToString("#0.00"));
                VCadFc += "\r\n";
            }
        }
        public static void PrintReceiptFooter(double subTotal, decimal varIGV, double tax, double discount, string footerText, string resolucion, string resolucion1, string resolucion2, string varDocumentTypeID, double percepcion, decimal porcpercepcion, string estadotk, ref string VCadFc)
        {

            string offSetString = new string(' ', (_recLineChars / 2) - 14);

            offSetString = offSetString.Trim().PadLeft((_recLineChars / 2) - 14, '|');

            VCadFc += "\r\n";
            VCadFc += Alineacion("I", VAnchoTicket, new string('=', _recLineChars).Length, new string('=', _recLineChars));

            double mtoigv = (Double.Parse(subTotal.ToString()) - discount) * double.Parse(varIGV.ToString());
            Int32 porcigv = Convert.ToInt32((varIGV * 100));

            //double varDsctoTax = 0;
            if (varDocumentTypeID == "2") // 2 es RUC, 1 es DNI
            {
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + "SUB-TOTAL||||||||||").Length, offSetString + "SUB-TOTAL||||||||||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, (subTotal.ToString("#0.00")).Length, subTotal.ToString("#0.00"));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + "DESCUENTO||||||||||").Length, offSetString + "DESCUENTO||||||||||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, (discount.ToString("#0.00")).Length, discount.ToString("#0.00"));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + "IGV|" + porcigv + "%||||||||||||").Length, offSetString + "IGV|" + porcigv + "%||||||||||||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, (mtoigv.ToString("#0.00")).Length, mtoigv.ToString("#0.00"));


            }
            else
            {
                VCadFc += "\r\n";

                VCadFc += Alineacion("I", 25, (offSetString + "SUB-TOTAL||||||||||").Length, offSetString + "SUB-TOTAL||||||||||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, (subTotal.ToString("#0.00")).Length, subTotal.ToString("#0.00"));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + "DESCUENTO||||||||||").Length, offSetString + "DESCUENTO||||||||||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, (discount.ToString("#0.00")).Length, discount.ToString("#0.00"));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + "IGV|" + porcigv + "%||||||||||||").Length, offSetString + "IGV|" + porcigv + "%||||||||||||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, (mtoigv.ToString("#0.00")).Length, mtoigv.ToString("#0.00"));

            }

            double totalpagar = ((subTotal - discount) + mtoigv) + percepcion;

            VCadFc += "\r\n";
            VCadFc += Alineacion("I", VAnchoTicket, (offSetString + new string('-', (_recLineChars / 3))).Length, offSetString + new string('-', (_recLineChars / 3)));
            VCadFc += "\r\n";
            VCadFc += Alineacion("I", 25, (offSetString + "TOTAL||||||||||||||").Length, offSetString + "TOTAL||||||||||||||");
            VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
            VCadFc += Alineacion("D", 11, (((subTotal - discount) + mtoigv).ToString("#0.00")).Length, ((subTotal - discount) + mtoigv).ToString("#0.00"));

            //condiciones para el formato de tickets

            if (estadotk == "1")
            {
                //el valor 1 si es que el tickets es normal 
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", VAnchoTicket, (offSetString + new string('-', (_recLineChars / 3))).Length, offSetString + new string('-', (_recLineChars / 3)));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + "PERCEPCION|" + porcpercepcion + "%|||").Length, offSetString + "PERCEPCION|" + porcpercepcion + "%|||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, ((percepcion).ToString("#0.00")).Length, (percepcion).ToString("#0.00"));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", VAnchoTicket, (offSetString + new string('-', (_recLineChars / 3))).Length, offSetString + new string('-', (_recLineChars / 3)));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + "TOTAL A PAGAR|||").Length, offSetString + "TOTAL A PAGAR|||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, ((totalpagar).ToString("#0.00")).Length, (totalpagar).ToString("#0.00"));


            }
            if (estadotk == "2")
            {
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", VAnchoTicket, (offSetString + new string('-', (_recLineChars / 3))).Length, offSetString + new string('-', (_recLineChars / 3)));
                //el valor 2 si es que el tickets es que la nota d ecredito es mayor a la venta


                string espaciof = "||**";

                string vNotapercepcion = "(**) Este valor es referencial. Su valor real se consignara en su comprobante de percepcion respectivo.";

                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + espaciof + "PERCEPCION|" + porcpercepcion + "%|||").Length, offSetString + espaciof + "PERCEPCION|" + porcpercepcion + "%|||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, ((percepcion).ToString("#0.00")).Length, (percepcion).ToString("#0.00"));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", VAnchoTicket, (offSetString + new string('-', (_recLineChars / 3))).Length, offSetString + new string('-', (_recLineChars / 3)));
                VCadFc += "\r\n";
                VCadFc += Alineacion("I", 25, (offSetString + "TOTAL A PAGAR|||").Length, offSetString + "TOTAL A PAGAR|||");
                VCadFc += Alineacion("C", 3, (0.ToString(_strDec, _myCIintl).Replace("0.00", "|")).Length, 0.ToString(_strDec, _myCIintl).Replace("0.00", "|"));
                VCadFc += Alineacion("D", 11, ((totalpagar).ToString("#0.00")).Length, (totalpagar).ToString("#0.00"));
                VCadFc += "\r\n";
                VCadFc += "\r\n";
                VCadFc += Alineacion("C", VAnchoTicket, (vNotapercepcion).Length, vNotapercepcion);
            }
            VCadFc += "\r\n";
            VCadFc += "\r\n";
            VCadFc += Alineacion("C", VAnchoTicket, (footerText).Length, footerText);
            VCadFc += "\r\n";
        }
        public static void PrintTextLine_ZoneSubTot(string text)
        {

            _strInvoicePrint += text + System.Environment.NewLine;

        }

        public static String Alineacion(String Lugar, Int32 Tam_Campo, Int32 log_text, String cont_text)
        {
            string alineacionc = "";
            if (log_text > Tam_Campo)
            {
                log_text = Tam_Campo;
                cont_text = cont_text.Substring(0, Tam_Campo);
            }
            switch (Lugar)
            {
                case "D":
                    alineacionc = Space(Tam_Campo - log_text) + cont_text;
                    break;
                case "I":
                    alineacionc = cont_text + Space(Tam_Campo - log_text);
                    break;
                case "C":
                    alineacionc = Space((Tam_Campo - log_text) / 2) + cont_text + Space((Tam_Campo - log_text) / 2);
                    break;
            }
            return alineacionc;
        }
        public static string Space(int n)
        {
            string s = "";
            for (int i = 0; i < n; i++) s += "&nbsp";
            return s;
        }
        #endregion
    }
}
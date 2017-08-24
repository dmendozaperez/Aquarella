using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Variables;
using System.IO;
//using Microsoft.PointOfService;
using System.Globalization;
using CapaDato.Bll.Venta;

namespace Impresora_Epson
{
    public class Config_Imp_NC
    {
        private static POSPrinter _posprinter = new POSPrinter();
  //      private static PosPrinter _printer;
        private static int _recLineCharsnc = 39;
        static string _strInvoicePrint = "";
        static CultureInfo _myCIintl;
        static int _recLineChars = 0;
        static string _strDec = "";
        public static string SetBold = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'b', (byte)'C' });
        public static string SetUnderline = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'u', (byte)'C' });
        public static string SetItalic = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'i', (byte)'C' });
        public static string SetCentre = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' });
        public static string SetRight = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'r', (byte)'A' });
        public static string SetNormal = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'N' });
        public static string SetNormalSize = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'1', (byte)'C' });
        public static string SetDoubleWide = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'C' });
        public static string SetDetalle = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'1', (byte)'C' });
        public static string SetDoubleHigth = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'3', (byte)'C' });
        public static string SetDoubleWH = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'4', (byte)'C' });
        public static string SetCutPaper = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'f', (byte)'P' });
        //public static string GenerarTicketNC(string noInv, int varOrigina_Copia, string _codigo_hash)
        //{
        //    //string noInv = textBox1.Text;
        //    try
        //    {

        //        //Metodo que realiza la facturación, realiza el empaquetamiento, ingresa los pagos y registra la factura.
        //        string DirRaiz = Directory.GetCurrentDirectory();
        //        string strFilePath = string.Empty;
        //        int iRecLineChars = 36;//Tamaño o ancho del papel para la impresion, lo inicializo en 50.
               

        //        //AppSettingsReader lector = new AppSettingsReader();


        //        ////ConfigurationManager.AppSettings[""
        //        //string d = "";
        //        //d = (string)lector.GetValue("kdirlogoInvoce",typeof(string));
        //        //d = ConfigurationManager.ConnectionStrings["kdirlogoInvoce"].ConnectionString;


        //        strFilePath = DirRaiz + Global.kdirlogoInvoce;
        //        string varSerieFactura = Global.kSeriePosBoleta;
        //        string varSerieBoleta = Global.kSeriePosBoleta;
        //        string varConceptoVisaUnica = Global.kPosConcepts_VISA_UNICA;
        //        //ConfigurationManager.AppSettings[


        //        //ServiceVentas.VentasClient client = new ServiceVentas.VentasClient();
        //        //ObservableCollection<string> obsCadenaImpresion = new ObservableCollection<string>();
        //        string[] obsCadenaImpresion;

        //        // string varDocumentTypeID = client.getDocumentTypeID()

        //        //// arma la cadena, 
        //        ////ACA ME VOY A wcf EN ADA.bl/facade/controlers/ventas/CONTROLINVOICE.CS
        //        obsCadenaImpresion = getAllInvoice(Global.kCompany, noInv,
        //        string.Empty, "", _myCIintl, iRecLineChars, Global.KCountDecimal,
        //        varSerieFactura, varSerieBoleta, varConceptoVisaUnica, _codigo_hash);


        //        // es para enviar a la ticketera o a la ticketera_Factura
        //        bool strDocumentTypeID = obsCadenaImpresion[0].Contains("R.U.C.");
        //        if (strDocumentTypeID)
        //        {
        //            //ticketera Factura
        //            string varFactura = Global.kPosImpresoraFactura;
        //            _printer = _posprinter.GetReceiptPrinter(varFactura);  // prepara la impresora
        //            _posprinter.ConnectToPrinter(_printer);   // inicializa cin el getExplorer
        //            iRecLineChars = _printer.RecLineChars;//Tamaño o ancho del papel para la impresion. 
        //        }
        //        else
        //        {
        //            //ticketera boleta
        //            string varBoleta = Global.kPosImpresora;
        //            _printer = _posprinter.GetReceiptPrinter(varBoleta);  // prepara la impresora
        //            _posprinter.ConnectToPrinter(_printer);   // inicializa cin el getExplorer
        //            iRecLineChars = _printer.RecLineChars;//Tamaño o ancho del papel para la impresion. 
        //        }

        //        ////ticketera boleta esta al comienzo y solo se enviaba a una Ticketera
        //        //_printer = _posprinter.GetReceiptPrinter();  // prepara la impresora
        //        //_posprinter.ConnectToPrinter(_printer);   // inicializa cin el getExplorer
        //        //iRecLineChars = _printer.RecLineChars;//Tamaño o ancho del papel para la impresion. 

        //        ///// _posprinter.PrintContent(_printer, obsCadenaImpresion[0].Replace("@@", ""));  // manda a imprimir

        //        //// esta es paara q salgan las copias
        //        //_posprinter.PrintContent(_printer, obsCadenaImpresion[0].Replace("@@", "*** COPIA ***"));  // manda a imprimir con copia

        //        // si es uno Es tiket Original, si es 0 es Ticket copia
        //        if (varOrigina_Copia == 1)
        //        {
        //            _posprinter.PrintContent(_printer, obsCadenaImpresion[0].Replace("@@", ""));
        //        }
        //        else
        //        {
        //            _posprinter.PrintContent(_printer, obsCadenaImpresion[0].Replace("@@", "*** COPIA ***"));  // manda a imprimir con copia
        //        }

        //        ////Duplicado de factura 
        //        ////Corto el papel de la primera

        //        _posprinter.DisconnectFromPrinter(_printer); //CIERRA EL EXPLORADOR POS

        //        //_printer.Close();

        //        //_posprinterFactura.DisconnectFromPrinter(_printerFactura); //CIERRA EL EXPLORADOR POS

        //        return "Correcto";


        //    }
        //    catch (PosControlException errPosCotrl)
        //    {
        //        //bsyIndicadorPay.IsBusy = false;
        //        //lblMsg.Content = errPosCotrl.Message;
        //        _posprinter.ClearExclusiveAccess(_printer);
        //        return null;
        //    }
        //    catch (PosLibraryException errPosLib)
        //    {
        //        //bsyIndicadorPay.IsBusy = false;
        //        //lblMsg.Content = errPosLib.Message;
        //        _posprinter.ClearExclusiveAccess(_printer);
        //        return null;
        //    }
        //    catch (PosException errPos)
        //    {
        //        // bsyIndicadorPay.IsBusy = false;
        //        //lblMsg.Content = errPos.Message;
        //        _posprinter.ClearExclusiveAccess(_printer);
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        //bsyIndicadorPay.IsBusy = false;
        //        //lblMsg.Content = ex.Message;
        //        _posprinter.ClearExclusiveAccess(_printer);
        //        return null;
        //    }
        //    finally
        //    {
        //        //bsyIndicadorPay.IsBusy = false;                
        //    }
        //}

        

        public static string[] getAllInvoice(string strCompany, string strInvoice, string strLiquidation, string strFile, CultureInfo myCulture
          , int iRecLineChars, string strDec, string StrSerieFactura, string StrSerieBoleta, string StrConceptoVisaUnica, string _codigo_hash)
        {
            //DataSet ds = new DataSet();
            //_invoice = new DALInvoice();
            //ds = getAllInvoice(strCompany, strInvoice, strLiquidation);
            //string[] strPrint = new string[2];
            DataSet ds = Dat_NotaCredito.leer_NC_tk(strInvoice);
            string[] strPrint = new string[1];

            strPrint[0] = getPosPrinter(ds, strFile, myCulture, iRecLineChars, strDec, "O", StrSerieFactura, StrSerieBoleta, StrConceptoVisaUnica, _codigo_hash);

            //strPrint[0] = "Impresion aquarella";

            _strInvoicePrint = "";//Limpio cadena de impresion para generar la copia.
            //strPrint[1] = getPosPrinter(ds, strFile, myCulture, iRecLineChars, strDec,"C");//Original
            return strPrint;   // devuelve la cadena llena a  ImpresionPrueba.axml

        }
        private static string getPosPrinter(DataSet dsInvoice, string strFilePath, CultureInfo myCIintl, int recLineChars, string strDecimal, string strTipo, string StrSerieFactura, string StrSerieBoleta, string StrConceptoVisaUnica, string _codigo_hash)
        {
            try
            {
                _strInvoicePrint = "";
                _myCIintl = myCIintl;
                _recLineChars = recLineChars;
                _strDec = strDecimal;

                DataTable dt_venta = dsInvoice.Tables[0];
                string _referencia = "";
                string _observacion = "";
                if (dt_venta != null)
                {
                    if (dt_venta.Rows.Count > 0)
                    {
                        _referencia = dt_venta.Rows[0]["referencia"].ToString();
                        _observacion = dt_venta.Rows[0]["obs"].ToString();
                        StrSerieBoleta = dt_venta.Rows[0]["Con_Fig_SerieImpresora"].ToString();
                        StrSerieFactura = dt_venta.Rows[0]["Con_Fig_SerieImpresora"].ToString();
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
                            Convert.ToDateTime(dt_venta.Rows[0]["Not_Fecha"].ToString()),
                            "VENTA",
                            "000000",
                            "Empleado Nombre", strFilePath, strTipo, StrSerieFactura, StrSerieBoleta, StrConceptoVisaUnica, dt_venta.Rows[0]["emp_autorizacion"].ToString());

                        decimal dSubtotal = 0;
                        decimal dDiscount = 0;

                        decimal _igv = Convert.ToDecimal(dt_venta.Rows[0]["Ven_Igv_Porc"].ToString());
                        decimal _igv_monto = Convert.ToDecimal(dt_venta.Rows[0]["Not_Igv"].ToString());
                        string _tipodoc = dt_venta.Rows[0]["Bas_Doc_Tip_Id"].ToString();
                        decimal _percepcionp =0; //Convert.ToDecimal(dt_venta.Rows[0]["Ven_PercepcionP"].ToString());
                        double _percepcionm = 0; //Convert.ToDouble(dt_venta.Rows[0]["Ven_PercepcionM"].ToString());
                        String _estadook = dt_venta.Rows[0]["EstadoOk"].ToString();
                        for (Int32 i = 0; i < dt_venta.Rows.Count; ++i)
                        {

                            string _iarticulo = dt_venta.Rows[i]["Art_Id"].ToString();
                            string _articulonombre = dt_venta.Rows[i]["Art_Descripcion"].ToString();
                            string _talla = dt_venta.Rows[i]["Not_Det_TalId"].ToString();
                            string _cantidad = dt_venta.Rows[i]["Not_Det_Cantidad"].ToString();
                            Decimal _articulo_total = Convert.ToDecimal(dt_venta.Rows[i]["articulo_total"].ToString());
                            decimal _comision = Convert.ToDecimal(dt_venta.Rows[i]["Not_Det_ComisionM"].ToString());
                            PrintLineItem(_iarticulo, _articulonombre,
                            _talla, int.Parse(_cantidad),
                            Double.Parse(_articulo_total.ToString()),
                            _tipodoc, _igv, _comision);
                            PrintText(SetNormal + TruncateAt(_articulonombre.PadRight(24), 24) + "         ");
                            dSubtotal += _articulo_total;
                            dDiscount += _comision;
                        }

                        PrintReceiptFooter(Double.Parse(dSubtotal.ToString()), _igv,
                            Double.Parse(_igv_monto.ToString()), Double.Parse((dDiscount - 0).ToString()), "*** GRACIAS POR SU COMPRA ***",
                            "Facturacion " + "0" + " por Res. Dian ",
                            "1" + " De " + "" + " Del", "Pref " + "" + "-" + "" + " Al Pref " + "" + "-" + "",
                            _tipodoc, _percepcionm, _percepcionp, _estadook, _codigo_hash, _referencia,_observacion);

                    }
                }





                ////   aca arma la cadena menos el foot (pie de pagina)
                //PrintReceiptHeader(
                //        InvoiceHdr._wav_description,
                //        InvoiceHdr._wav_address, " Telefono " + InvoiceHdr._telefonofijo,
                //        "Bata-Empresas Comerciales S.A",
                //        "RUC 20101951872",
                //        InvoiceHdr._ihv_invoice_no,
                //        "DETALLE DE COMPRA",
                //        InvoiceHdr._datacustomer._bdvFullName,
                //        InvoiceHdr._datacustomer._bdvDocumentTypeID,
                //        InvoiceHdr._datacustomer._bdvDocumentNo,
                //        InvoiceHdr._ihd_date,
                //        InvoiceHdr._concepto,
                //        InvoiceHdr._numTarCredito,
                //        "Empleado Nombre", strFilePath, strTipo, StrSerieFactura, StrSerieBoleta, StrConceptoVisaUnica);

                //// arma el detalle DE ITEMS del ticket
                //decimal dSubtotal = 0;
                //decimal dDiscount = 0;
                //for (int j = 0; j < _InvoiceDtlCollection.Count; j++)
                //{
                //    PrintLineItem(_InvoiceDtlCollection[j]._idv_article, _InvoiceDtlCollection[j]._arv_name,
                //    _InvoiceDtlCollection[j]._idv_size, int.Parse(_InvoiceDtlCollection[j]._idn_qty.ToString()),
                //    Double.Parse(_InvoiceDtlCollection[j]._articlevalue.ToString()),
                //    InvoiceHdr._datacustomer._bdvDocumentTypeID, InvoiceHdr._datacustomer._bdvIGV, _InvoiceDtlCollection[j]._idn_commission);
                //    PrintText(SetNormal + TruncateAt(_InvoiceDtlCollection[j]._arv_name.PadRight(24), 24) + "         ");
                //    dSubtotal += _InvoiceDtlCollection[j]._articlevalue;
                //    dDiscount += _InvoiceDtlCollection[j]._idn_commission + _InvoiceDtlCollection[j]._idn_disscount;
                //}

                //PrintReceiptFooter(Double.Parse(dSubtotal.ToString()), InvoiceHdr._datacustomer._bdvIGV,
                //    Double.Parse(InvoiceHdr._ihn_taxes.ToString()), Double.Parse((dDiscount - InvoiceHdr._ihn_handling).ToString()), "*** GRACIAS POR SU COMPRA ***",
                //    "Facturacion " + InvoiceHdr._nuv_typeresolution + " por Res. Dian ",
                //    InvoiceHdr._nuv_resolution + " De " + InvoiceHdr._nud_date.ToString("MM/dd/yyyy") + " Del", "Pref " + InvoiceHdr._nuv_code + "-" + InvoiceHdr._nun_invoice_start + " Al Pref " + InvoiceHdr._nuv_code + "-" + InvoiceHdr._nun_invoice_end,
                //    InvoiceHdr._datacustomer._bdvDocumentTypeID, InvoiceHdr._percepcion, InvoiceHdr._porcpercepcion, InvoiceHdr._estadotk);
            }
            catch (Exception exc)
            {

            }
            return _strInvoicePrint;
        }

        private static void PrintReceiptFooter(double subTotal, decimal varIGV, double tax, double discount, string footerText, string resolucion, string resolucion1, string resolucion2, string varDocumentTypeID, double percepcion, decimal porcpercepcion, string estadotk, string _codigo_hash,string _referencia,string _obs)
        {
            //Genera un expacio en blanco de acuerdo para realizar posicionamiento.
            //string offSetString = new string(' ', (_recLineChars / 2) - 7); cambioRandy
            //string offSetString = new string(' ', (_recLineChars / 2) - 8);
            string offSetString = new string(' ', (_recLineChars / 2) - 14);
            PrintTextLine(SetNormal + new string('-', _recLineChars));
            //cambiorandy
            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "SUB-TOTAL " + subTotal.ToString(_strDec, _myCIintl).PadLeft(11));
            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "DESCUENTO " + discount.ToString(_strDec, _myCIintl).PadLeft(11));
            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "IGV       " + tax.ToString(_strDec, _myCIintl).PadLeft(11));

            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "SUB-TOTAL " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + subTotal.ToString("#0.00").PadLeft(1));
            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "DESCUENTO " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + discount.ToString("#0.00").PadLeft(1));
            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "IGV       " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + tax.ToString("#0.00").PadLeft(1));

            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "SUB-TOTAL " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + subTotal.ToString("#0.00").PadLeft(9));
            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "DESCUENTO " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + discount.ToString("#0.00").PadLeft(9));
            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "IGV       " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + tax.ToString("#0.00").PadLeft(9));
            // PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));

            double mtoigv = (Double.Parse(subTotal.ToString()) - discount) * double.Parse(varIGV.ToString());
            Int32 porcigv = Convert.ToInt32((varIGV * 100));

            double varDsctoTax = 0;
            if (varDocumentTypeID == "2") // 2 es RUC, 1 es DNI
            {
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "SUB-TOTAL       " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + subTotal.ToString("#0.00").PadLeft(9));
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "DESCUENTO       " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + discount.ToString("#0.00").PadLeft(9));
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "IGV " + porcigv + "%         " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + mtoigv.ToString("#0.00").PadLeft(9));

                //PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));
            }
            else
            {
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "SUB-TOTAL       " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + subTotal.ToString("#0.00").PadLeft(9));
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "DESCUENTO       " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + discount.ToString("#0.00").PadLeft(9));
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "IGV " + porcigv + "%         " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + mtoigv.ToString("#0.00").PadLeft(9));
                //varDsctoTax = Double.Parse(discount.ToString()) + (double.Parse(discount.ToString()) * double.Parse(varIGV.ToString()));
                //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "DESCUENTO       " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + varDsctoTax.ToString("#0.00").PadLeft(9));
            }

            double totalpagar = ((subTotal - discount) + mtoigv) + percepcion;

            PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));
            //  //cambiorandy:  PrintTextLine_ZoneSubTot(SetNormal + offSetString + "TOTAL     " + (subTotal + tax - discount).ToString(_strDec, _myCIintl).PadLeft(11));
            //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "TOTAL     " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + (subTotal + tax - discount).ToString("#0.00").PadLeft(1));
            PrintTextLine_ZoneSubTot(SetNormal + offSetString + "TOTAL           " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + ((subTotal - discount) + mtoigv).ToString("#0.00").PadLeft(9));

            //condiciones para el formato de tickets

            if (estadotk == "1")
            {
                //el valor 1 si es que el tickets es normal 


                PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "PERCEPCION " + porcpercepcion + "%   " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + (percepcion).ToString("#0.00").PadLeft(9));

                PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "TOTAL A PAGAR   " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + (totalpagar).ToString("#0.00").PadLeft(9));
            }
            if (estadotk == "2")
            {
                //el valor 2 si es que el tickets es que la nota d ecredito es mayor a la venta
                //PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));
                ////  //cambiorandy:  PrintTextLine_ZoneSubTot(SetNormal + offSetString + "TOTAL     " + (subTotal + tax - discount).ToString(_strDec, _myCIintl).PadLeft(11));
                ////PrintTextLine_ZoneSubTot(SetNormal + offSetString + "TOTAL     " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + (subTotal + tax - discount).ToString("#0.00").PadLeft(1));
                //PrintTextLine_ZoneSubTot(SetNormal + offSetString + "TOTAL           " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + ((subTotal - discount) + mtoigv).ToString("#0.00").PadLeft(9));

                string espaciof = "  **";

                PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));
                PrintTextLine_ZoneSubTot(SetNormal + espaciof + "PERCEPCION " + porcpercepcion + "%   " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + (percepcion).ToString("#0.00").PadLeft(9));

                PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));
                PrintTextLine_ZoneSubTot(SetNormal + offSetString + "TOTAL A PAGAR   " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + (totalpagar).ToString("#0.00").PadLeft(9));

                string vNotapercepcion = "(**) Este valor es referencial. Su valor real se consignara en su comprobante de percepcion respectivo.";

                PrintTextLine(SetNormal + String.Empty);
                PrintTextLineCompleta(SetCentre + vNotapercepcion);
                //PrintTextLine(SetNormal +  vNotapercepcion);
            }

            //if (estadotk == "3")
            //{
            //    PrintTextLine(SetNormal + offSetString + new string('-', (_recLineChars / 3)));
            //}
            //j         
            //PrintText(SetNormal + TruncateAt(itemCode.PadRight(9), 9));
            //PrintText(SetNormal + TruncateAt(item.PadRight(9), 10));
            //PrintText(SetNormal + TruncateAt(size.PadLeft(2), 2));
            //PrintText(SetNormal + TruncateAt(quantity.ToString("#0").PadLeft(3), 3));
            ////PrintTextLine(SetNormal + TruncateAt(total.ToString().PadLeft(8), 12)); cambioRandy
            ////PrintTextLine(SetNormal + TruncateAt(total.ToString("#0.00").PadLeft(9), 9));  

            //if (varDocumentTypeID == "2")  // ruc es 2, DNI es 1
            //{
            //    PrintTextLine(SetNormal + TruncateAt(total.ToString("#0.00").PadLeft(9), 9));
            //}
            PrintTextLine(SetNormal + new string('-', _recLineChars));
            PrintTextLine(SetNormal + String.Format("FIRMA: {0}", ""));
            //PrintText(SetNormal + TruncateAt("EFECTIVO : {0}".PadRight(9), 10));
            //PrintText(SetNormal + TruncateAt("".PadRight(9), 10));
            //PrintText(SetNormal + TruncateAt("".PadLeft(2), 2));
            //PrintText(SetNormal + TruncateAt("S/.".PadLeft(3), 3));
            //PrintTextLine(SetNormal + TruncateAt(totalpagar.ToString("#0.00").PadLeft(9), 9));
            PrintTextLine_ZoneSubTot(SetNormal + "EFECTIVO:           " + 0.ToString(_strDec, _myCIintl).Replace("0.00", "") + totalpagar.ToString("#0.00").PadLeft(9));

            PrintTextLine(SetNormal + new string('-', _recLineChars));
            PrintTextLine(SetNormal + String.Format("REFERENCIA DE N/C: {0}", _referencia));
            PrintTextLine(SetNormal + new string('-', _recLineChars));           
            PrintTextLine(SetNormal + SetCentre + _codigo_hash);
            PrintTextLine(SetNormal + new string('-', _recLineChars));            
            PrintTextLineCompleta(_obs);
            PrintTextLine(SetNormal + new string('-', _recLineChars));            
            PrintTextLine(SetNormal + SetCentre + footerText);
            PrintTextLine(SetNormal + String.Empty);
            //PrintTextLine(SetNormal + SetCentre + resolucion);
            //PrintTextLine(SetNormal + SetCentre + resolucion1);
            //PrintTextLine(SetNormal + SetCentre + resolucion2);
            PrintTextLine("\u001b|130uF");
        }
        private static void PrintTextLine_ZoneSubTot(string text)
        {

            _strInvoicePrint += text + System.Environment.NewLine;

        }
        private static void PrintReceiptHeader(string strDesAlmacen, string strAddress, string varTelefono, string strCompanyName, string strNitNumber,
           string strInvoiceNumber, string strTitle, string strCustomer, string strDocumentTypeID, string strCustomerId, DateTime dateTime, string concepto, string numTarjetaCred, string strEmployeeName, string strFilePath,
           string strTipo, string StrSerieFactura, string StrSerieBoleta, string StrConceptoVisaUnica, string strautorizacion)
        {

            PrintTextLine(String.Empty);
            PrintTextLine(SetCentre + strDesAlmacen);
            PrintTextLineCompleta(SetCentre + strAddress);
            PrintTextLine(SetCentre + varTelefono);
            PrintTextLine(SetCentre + strCompanyName);
            PrintTextLine(SetCentre + strNitNumber);
            PrintTextLine(SetNormal + String.Format("Autorizacion : " + strautorizacion));

            /// PrintTextLine(SetCentre + SetBold + String.Format("FACTURA DE VENTA No : {0}", strInvoiceNumber));
            // Para saber si es RUC y lo pinto en el ticket, 2 es ruc, 1 es DNI

            if (strDocumentTypeID == "2")
            {
                PrintTextLine(SetNormal + String.Format("Impresora : {0}", StrSerieFactura));
                PrintTextLinenc(SetNormal + SetBold + String.Format("NOTA DE CREDITO: {0}", strInvoiceNumber));
            }

            else if (strDocumentTypeID == "1")
            {
                PrintTextLine(SetNormal + String.Format("Impresora : {0}", StrSerieBoleta));
                PrintTextLinenc(SetNormal + SetBold + String.Format("NOTA DE CREDITO: {0}", strInvoiceNumber));
            }

            PrintTextLine(String.Empty);
            PrintTextLine(SetNormal + SetCentre + SetDetalle + SetBold + strTitle);
            // Comodin
            PrintTextLine(SetNormal + SetCentre + SetDoubleWide + SetBold + "@@");
            PrintTextLine(SetNormal + new string('-', _recLineChars));
            //PrintTextLine(SetNormal + String.Format("Cliente : {0}", strCustomer));
            PrintTextLine(SetNormal + String.Format("Cliente : "));
            PrintTextLine(SetNormal + String.Format(strCustomer));
            // Para saber si es RUC y lo pinto en el ticket, 2 es ruc
            if (strDocumentTypeID == "2")
            {
                PrintTextLine(SetNormal + String.Format("R.U.C. : {0}", strCustomerId));
            }
            else if (strDocumentTypeID == "1")
            {
                PrintTextLine(SetNormal + String.Format("DNI : {0}", strCustomerId));
            }
            //PrintTextLine(SetNormal + String.Format("Cliente No : {0}", strCustomerId));
            //PrintTextLine(SetNormal + String.Format("Fecha : {0}", dateTime.ToString("dd MMM yyyy ") + "Hora: " + dateTime.ToString("HH:mm:ss")));
            PrintTextLine(SetNormal + String.Format("Fecha : {0}", dateTime.ToString("dd/MM/yyyy ") + ", " + dateTime.ToString("HH:mm:ss")));
            if (concepto == StrConceptoVisaUnica)
            {
                PrintTextLine(SetNormal + String.Format("VISA UNICA : {0}", numTarjetaCred));
            }
            PrintTextLine(SetNormal + String.Empty);
            PrintText(SetNormal + "Codigo".PadRight(11));
            PrintText(SetNormal + " Descr.".PadRight(10));
            PrintText(SetNormal + "Cnt".PadRight(5));
            PrintText(SetNormal + "Precio".PadRight(7));
            PrintTextLine(SetNormal + new string('=', _recLineChars));
            //PrintTextLine(SetNormal + String.Empty);

        }
        public static void PrintTextLine(string text)
        {
            if (text.Length <= _recLineChars)
                _strInvoicePrint += text + System.Environment.NewLine;
            else if (text.Length > _recLineChars)
                _strInvoicePrint += TruncateAt(text, _recLineChars) + Environment.NewLine;
        }

        public static void PrintTextLinenc(string text)
        {
            if (text.Length <= _recLineCharsnc)
                _strInvoicePrint += text + System.Environment.NewLine;
            else if (text.Length > _recLineCharsnc)
                _strInvoicePrint += TruncateAt(text, _recLineCharsnc) + Environment.NewLine;
        }

        public static void PrintText(string text)
        {
            if (text.Length <= _recLineChars)
                _strInvoicePrint += text;
            else if (text.Length > _recLineChars)
                _strInvoicePrint += TruncateAt(text, _recLineChars);
        }
        public static string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);
            return retVal;
        }
        public static void PrintTextLineCompleta(string text)
        {
            _strInvoicePrint += text + System.Environment.NewLine;
        }
        //public static void Mainimp()
        //{
        //    // Crea la instancia del Explorador y de la impresora
        //    PosExplorer explorer;
        //    PosPrinter Printer;
        //    explorer = new PosExplorer();

        //    // El explorador busca todas las unidades y las pasa a una coleccion de unidades
        //    DeviceInfo device = explorer.GetDevice(DeviceType.PosPrinter, "PosTicket");

        //    try
        //    {
        //        Printer = (PosPrinter)explorer.CreateInstance(device);

        //        Printer.Open();
        //        Printer.Claim(1000);
        //        Printer.DeviceEnabled = true;
        //        Printer.PrintNormal(PrinterStation.Receipt, "******** Impresion de Prueba *******\n\n******** BATA CAMINA LA VIDA *******\n\n\n\n\n");
        //        Printer.CutPaper(95);
        //        Printer.Release();
        //        Printer.Close();

        //        Console.WriteLine("Impresion Correcta");
        //    }
        //    catch (PosException PosError)
        //    {
        //        Console.WriteLine("" + PosError.Message.ToString());
        //        Console.ReadKey();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("" + ex.Message.ToString());
        //        Console.ReadKey();
        //    }

        //    Console.ReadKey();
        //}

        private static void PrintLineItem(string itemCode, string item, string size, int quantity, double total, string varDocumentTypeID, decimal varIGV, decimal varIdn_commission)
        {
            PrintText(SetNormal + TruncateAt(itemCode.PadRight(9), 9));
            PrintText(SetNormal + TruncateAt(item.PadRight(9), 10));
            PrintText(SetNormal + TruncateAt(size.PadLeft(2), 2));
            PrintText(SetNormal + TruncateAt(quantity.ToString("#0").PadLeft(3), 3));
            //PrintTextLine(SetNormal + TruncateAt(total.ToString().PadLeft(8), 12)); cambioRandy
            //PrintTextLine(SetNormal + TruncateAt(total.ToString("#0.00").PadLeft(9), 9));  

            if (varDocumentTypeID == "2")  // ruc es 2, DNI es 1
            {
                PrintTextLine(SetNormal + TruncateAt(total.ToString("#0.00").PadLeft(9), 9));
            }
            else
            {
                // decimal varPrecioconDscto = decimal.Parse(total.ToString()) - varIdn_commission;
                // decimal vartotalTax = decimal.Parse(varPrecioconDscto.ToString()) + (decimal.Parse(varPrecioconDscto.ToString()) * varIGV);
                //PrintTextLine(SetNormal + TruncateAt(vartotalTax.ToString("#0.00").PadLeft(9), 9));

                //decimal vartotalTax = decimal.Parse(total.ToString()) + (decimal.Parse(total.ToString()) * varIGV);
                //PrintTextLine(SetNormal + TruncateAt(vartotalTax.ToString("#0.00").PadLeft(9), 9));
                decimal vartotalTax = decimal.Parse(total.ToString());
                PrintTextLine(SetNormal + TruncateAt(vartotalTax.ToString("#0.00").PadLeft(9), 9));
            }


        }
    }
}

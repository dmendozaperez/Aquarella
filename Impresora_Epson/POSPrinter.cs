using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.PointOfService;
using System.Configuration;
using System.Globalization;
using Variables;
namespace Impresora_Epson
{
    class POSPrinter
    {
        public string SetBold = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'b', (byte)'C' });
        public string SetUnderline = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'u', (byte)'C' });
        public string SetItalic = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'i', (byte)'C' });
        public string SetCentre = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'c', (byte)'A' });
        public string SetRight = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'r', (byte)'A' });
        public string SetNormal = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'N' });
        public string SetNormalSize = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'1', (byte)'C' });
        public string SetDoubleWide = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'2', (byte)'C' });
        public string SetDoubleHigth = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'3', (byte)'C' });
        public string SetDoubleWH = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'4', (byte)'C' });
        public string SetCutPaper = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { 27, (byte)'|', (byte)'f', (byte)'P' });

        /// <summary>
        /// Objeto para manejar la información de cultura de la aplicación.
        /// </summary>
        //private CultureInfo _myCIintl = new CultureInfo(ConfigurationSettings.AppSettings.Keys.Get(0) .GetConfig("kCulture").ToString());// ConfigurationManager.AppSettings["kCulture"].ToString());
        //private CultureInfo _myCIintl = new CultureInfo(ConfigurationManager.AppSettings["kCulture"].ToString());
        static private CultureInfo _myCIintl = new CultureInfo(Global.kCulture);


        /// <summary>
        /// Realiza la desconexion de la impresora.
        /// </summary>
        /// <param name="printer">Define la interfaz para el dispositivo de impresion.</param>
        /// 

        //public void DisconnectFromPrinter(PosPrinter printer)
        //{

        //    //string r=ConfigurationSettings.AppSettings.Get("c");
        //    printer.CutPaper(95);
        //    printer.Release();
        //    printer.Close();
        //}


        //public void ClearExclusiveAccess(PosPrinter printer)
        //{
        //    if (printer.State != ControlState.Closed)
        //    {
        //        if (printer.Claimed)
        //        {
        //            printer.Release();
        //        }
        //        printer.Close();
        //    }
        //}

        /// <summary>
        /// Realiza la conexión a la impresora y la prepara para 
        /// recibir los datos de impresión.
        /// </summary>
        /// <param name="printer">Define la interfaz para el dispositivo de impresion.</param>
        //public void ConnectToPrinter(PosPrinter printer)
        //{
        //    ClearExclusiveAccess(printer);
        //    printer.Open();
        //    printer.Claim(100);
        //    printer.DeviceEnabled = true;
        //    printer.RecLetterQuality = true;
        //    //Establece la compatibilidad de caracteres para la impresion 
        //    printer.CharacterSet = 1252;

        //}

        /// <summary>
        /// Metodo que obtiene el dispositivo de impresion conectada al pc.
        /// </summary>
        /// <returns></returns>
        //public PosPrinter GetReceiptPrinter(string varTicketara)
        //{
        //    PosExplorer posExplorer = new PosExplorer();
        //    //DeviceInfo receiptPrinterDevice = posExplorer.GetDevice(DeviceType.PosPrinter, ConfigurationManager.AppSettings["kPosImpresora"].ToString()); //May need to change this if you don't use a logicial name or use a different one.
        //    DeviceInfo receiptPrinterDevice = posExplorer.GetDevice(DeviceType.PosPrinter, varTicketara); //May need to change this if you don't use a logicial name or use a different one.
        //    return (PosPrinter)posExplorer.CreateInstance(receiptPrinterDevice);
        //}


        /// <summary>  
        /// Metódo para generar los datos inferiores de la factura.
        /// </summary>
        /// <param name="printer">Define la interfaz para el dispositivo de impresion.</param>
        /// <param name="subTotal">Valor subtotal de la compra.</param>
        /// <param name="tax">Valor Iva de la compra.</param>
        /// <param name="discount">Valor Descuento de la compra.</param>
        /// <param name="footerText">Texto para la parte inferior de la factura.</param>
        //public void PrintReceiptFooter(PosPrinter printer, double subTotal, double tax, double discount, string footerText)
        //{
        //    //Genera un expacio en blanco de acuerdo para realizar posicionamiento.
        //    //string offSetString = new string(' ', (printer.RecLineChars / 2) - 7);

        //    //PrintTextLine(printer, new string('-', printer.RecLineChars));
        //    //PrintTextLine(printer, offSetString + "SUB-TOTAL   " + subTotal.ToString(ConfigurationSettings.AppSettings.Get("KCountDecimal").ToString(), _myCIintl).PadLeft(12));
        //    //PrintTextLine(printer, offSetString + "DESCUENTO   " + discount.ToString(ConfigurationSettings.AppSettings.Get("KCountDecimal").ToString(), _myCIintl).PadLeft(12));
        //    //PrintTextLine(printer, offSetString + "IGV         " + tax.ToString(ConfigurationSettings.AppSettings.Get("KCountDecimal").ToString(), _myCIintl).PadLeft(12));
        //    //PrintTextLine(printer, offSetString + new string('-', (printer.RecLineChars / 3)));
        //    //PrintTextLine(printer, offSetString + SetBold + "TOTAL       " + (subTotal + tax - discount).ToString(ConfigurationSettings.AppSettings.Get("KCountDecimal").ToString(), _myCIintl).PadLeft(12));
        //    //PrintTextLine(printer, offSetString + new string('-', (printer.RecLineChars / 3)));
        //    //PrintTextLine(printer, String.Empty);
        //    //PrintTextLine(printer, SetCentre + footerText);
        //    //PrintTextLine(printer, "\u001b|130uF");
        //    // Console.Write(sali);
        //}

        /// <summary>
        /// Método para generar el detalle o lineas de la factura.
        /// </summary>
        /// <param name="printer">Define la interfaz para el dispositivo de impresion.</param>
        /// <param name="itemCode">Código del artículo.</param>
        /// <param name="item">Nombre del artículo.</param>
        /// <param name="size">Tamaño del artículo.</param>
        /// <param name="quantity">Cantidad del artículo.</param>
        /// <param name="total">Precio por cantidad de Artículos</param>
        //public void PrintLineItem(PosPrinter printer, string itemCode, string item, string size, int quantity, double total)
        //{
        //    //PrintText(printer, TruncateAt(itemCode.PadRight(9), 9));
        //    //PrintText(printer, TruncateAt(item.PadRight(8), 9));
        //    //PrintText(printer, TruncateAt(size.PadLeft(5), 7));
        //    //PrintText(printer, TruncateAt(quantity.ToString("#0").PadLeft(4), 4));
        //    //PrintTextLine(printer, TruncateAt(total.ToString(ConfigurationSettings.AppSettings.Get("KCountDecimal").ToString(), _myCIintl).PadLeft(12), 12));
        //}

        /// <summary>
        /// Metódo para generar los datos superiores o cabezera de la factura.
        /// </summary>
        /// <param name="printer">Define la interfaz para el dispositivo de impresion.</param>
        /// <param name="strAddress">Nombre de la bodega.</param>
        /// <param name="strAddressandTelefono">Dirección y teléfono de la bodega.</param>
        /// <param name="strCompanyName">Nombre del compañía.</param>
        /// <param name="strNitNumber">Nit de la compañía.</param>
        /// <param name="strInvoiceNumber">Número de la factura.</param>
        /// <param name="strTitle">Descripción o SubTítulo de la factura.</param>
        /// <param name="strCustomer">Nombre del cliente</param>
        /// <param name="strCustomerId">Identificación del cliente.</param>
        /// <param name="dateTime">Fecha</param>
        /// <param name="strEmployeeName">Nombre del empleado.</param>
        /// <param name="strFilePath">Dirección de ubicación de la imagen.</param>
        //public void PrintReceiptHeader(PosPrinter printer, string strAddress, string strAddressandTelefono, string strCompanyName, string strNitNumber,
        //    string strInvoiceNumber, string strTitle, string strCustomer, string strCustomerId, DateTime dateTime, string strEmployeeName, string strFilePath)
        //{
        //    //Se adiciona imagen o logo a la factura.
        //    if (!string.IsNullOrEmpty(strFilePath))
        //        printer.PrintBitmap(PrinterStation.Receipt, strFilePath, PosPrinter.PrinterBitmapAsIs, PosPrinter.PrinterBitmapCenter);
        //    PrintTextLine(printer, String.Empty);
        //    PrintTextLine(printer, SetCentre + strAddress);
        //    PrintTextLine(printer, SetCentre + strAddressandTelefono);
        //    PrintTextLine(printer, SetCentre + strCompanyName);
        //    PrintTextLine(printer, SetCentre + strNitNumber);
        //    PrintTextLine(printer, SetCentre + SetBold + String.Format("FACTURA DE VENTA N° : {0}", strInvoiceNumber));
        //    PrintTextLine(printer, String.Empty);
        //    PrintTextLine(printer, SetNormal + SetCentre + SetDoubleWide + SetBold + strTitle);
        //    PrintTextLine(printer, new string('-', printer.RecLineChars));
        //    PrintTextLine(printer, String.Format("Cliente : {0}", strCustomer));
        //    PrintTextLine(printer, String.Format("Cliente No : {0}", strCustomerId));
        //    PrintTextLine(printer, String.Format("Fecha : {0}", dateTime.ToString("dd MMM yyyy ") + "Hora: " + dateTime.ToString("HH:mm:ss")));
        //    PrintTextLine(printer, String.Empty);
        //    PrintText(printer, " Articulo".PadRight(12));
        //    PrintText(printer, " Talla".PadRight(8));
        //    PrintText(printer, "Cnt".PadRight(5));
        //    PrintText(printer, "Precio".PadRight(9));
        //    PrintTextLine(printer, new string('=', printer.RecLineChars));
        //    PrintTextLine(printer, String.Empty);

        //}

        /// <summary>
        /// Imprime texto sin salto de línea.
        /// </summary>
        /// <param name="printer"></param>
        /// <param name="text"></param>
        //public void PrintText(PosPrinter printer, string text)
        //{
        //    sali += text;
        //    if (text.Length <= printer.RecLineChars)
        //        printer.PrintNormal(PrinterStation.Receipt, text);
        //    else if (text.Length > printer.RecLineChars)
        //        printer.PrintNormal(PrinterStation.Receipt, TruncateAt(text, printer.RecLineChars));
        //}

        //public void PrintContent(PosPrinter printer, string text)
        //{
        //    printer.PrintNormal(PrinterStation.Receipt, text);
        //}

        public string sali = "";

        /// <summary>
        /// Imprime texto con salto de línea.
        /// </summary>
        /// <param name="printer"></param>
        /// <param name="text"></param>
        //public void PrintTextLine(PosPrinter printer, string text)
        //{
        //    sali += text + Environment.NewLine;
        //    if (text.Length <= printer.RecLineChars)
        //        printer.PrintNormal(PrinterStation.Receipt, text + Environment.NewLine);
        //    else if (text.Length > printer.RecLineChars)
        //        printer.PrintNormal(PrinterStation.Receipt, TruncateAt(text, printer.RecLineChars) + Environment.NewLine);
        //}

        /// <summary>
        /// Trunca el texto a mostrar de acuerdo al tamaño máximo permitido.
        /// </summary>
        /// <param name="text">Texto.</param>
        /// <param name="maxWidth">Tamaño.</param>
        /// <returns></returns>
        public string TruncateAt(string text, int maxWidth)
        {
            string retVal = text;
            if (text.Length > maxWidth)
                retVal = text.Substring(0, maxWidth);
            return retVal;
        }

    }
}

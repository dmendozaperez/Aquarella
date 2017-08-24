using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Aquarella.bll
{
    class Invoice_HdrViewModel
    {
        /// <summary>
        /// Recuperar el nombre de la conexion a la base de datos
        /// </summary>
       
        /// <returns></returns>
        public DataSet getInvoiceHdr(String ihv_invoice_no)
        {
            try
            {
                return Invoice_Hdr.getInvoiceHdr( ihv_invoice_no);
            }
            catch { return null; }
        }

        /// <summary>
        /// Realizar una factura
        /// </summary>
        /// <param name="ihv_co"></param>
        /// <param name="ihv_invoice_no"></param>
        /// <param name="ihv_liquidation"></param>
        /// <param name="ihn_pointsale"></param>
        /// <returns></returns>
        public String doInvoice(String ihv_liquidation, Decimal ihn_pointsale)
        {
            ///
            return Invoice_Hdr.doInvoice(ihv_liquidation, ihn_pointsale);
        }

        public String generarNumeroGuia(String idv_invoice, Decimal ihn_pointsale)
        {
            ///
            return Invoice_Hdr.generarNumeroGuiaDB(idv_invoice, ihn_pointsale);
        }

        public String updateNumeroGuia( String idv_invoice, Decimal ihn_pointsale)
        {
            ///
            return Invoice_Hdr.updateNumeroGuiaDB(idv_invoice, ihn_pointsale);
        }

        public GuiaTransportistaArray grabarTransportista( String varTransportista, Decimal ihn_pointsale, GuiaTransportistaArray _objBEArray)
        {
            ///
            return Invoice_Hdr.grabarTransportistaDB(varTransportista, ihn_pointsale, _objBEArray);
        }

        public GuiaTransportistaArray EditarTransportista(String varTransportista, Decimal ihn_pointsale, int SECUENCIALTRANSPORTISTA, GuiaTransportistaArray _objBEArray)
        {
            ///
            return Invoice_Hdr.EditarTransportistaDB(varTransportista, ihn_pointsale, SECUENCIALTRANSPORTISTA, _objBEArray);
        }

        public string AdicionarGuia(String pdv_co, String varINVOICE_NO, int varPREFIJO, int SECUENCIALTRANSPORTISTA, String varNombreTransportista, Decimal ihn_pointsale)
        {
            ///
            return Invoice_Hdr.AdicionarGuiaDB( varINVOICE_NO, varPREFIJO, SECUENCIALTRANSPORTISTA, varNombreTransportista, ihn_pointsale);
        }

        public String EliminarGuiaDeManifiesto( String IHV_INVOICE_NO, int PREFIJO, Decimal ihn_pointsale)
        {
            ///
            return Invoice_Hdr.EliminarGuiaDeManifiestoDB(IHV_INVOICE_NO, PREFIJO, ihn_pointsale);
        }



    }
}

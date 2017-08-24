using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Aquarella.bll
{
    class Invoice_Hdr
    {
        #region < METODOS PUBLICOS >

        /// <summary>
        /// Consultar informacion de la cabecera de la factura
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="idv_co"></param>
        /// <param name="idv_invoice_no"></param>
        /// <param name="idv_liquidation"></param>
        /// <returns></returns>
        public static DataSet getInvoiceHdr( String ihv_invoice_no)
        {
            try
            {
                // CURSOR REF
             
                ///
                return null;
            }
            catch { return null; }
        }
        public static GuiaTransportistaArray grabarTransportistaDB(String varTransportista, Decimal ihn_pointsale, GuiaTransportistaArray objBEArray)
        {
           
            try
            {
               
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
               
            }
            return null;
        }
        public static GuiaTransportistaArray EditarTransportistaDB(String varTransportista, Decimal ihn_pointsale, int SECUENCIALTRANSPORTISTA, GuiaTransportistaArray objBEArray)
        {
            ///
        

            try
            {
             
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
               
            }
            return null;
        }
        /// <summary>
        /// Generar factura
        /// </summary>
        /// <param name="_nameConn"></param>
        /// <param name="ihv_co"></param>
        /// <param name="_noLiquidation"></param>
        /// <param name="_pointOfSale"></param>
        /// <returns></returns>
        /// 
        public static String EliminarGuiaDeManifiestoDB(String IHV_INVOICE_NO, int PREFIJO, Decimal ihn_pointsale)
        {
            ///
            try
            {
              
                ///
                return "1";
            }
            catch
            {
                return "-1";
            }
        }


        public static String AdicionarGuiaDB(String varINVOICE_NO, int varPREFIJO, int SECUENCIALTRANSPORTISTA, String varNombreTransportista, decimal ihn_pointsale)
        {
            ///
            try
            {               
                ///
                return "1";
            }
            catch
            {
                return "-1";
            }
        }
            
        public static String doInvoice( String ihv_liquidation, Decimal ihn_pointsale)
        {
            ///
            try
            {
                String _idFactura = "";              
                ///
                return _idFactura;
            }
            catch
            {
                return "-1";
            }
        }



        public static String updateNumeroGuiaDB( String idv_invoice, Decimal ihn_pointsale)
        {
            ///
            try
            {
                String _NUMCOMPLETO_GUIA = "";
              
                return _NUMCOMPLETO_GUIA;
            }
            catch
            {
                return "-1";
            }
        }


        public static String generarNumeroGuiaDB(String idv_invoice, Decimal ihn_pointsale)
        {
            ///
            try
            {
               
                return "1";
            }
            catch
            {
                return "-1";
            }
        }

        #endregion
    }
}

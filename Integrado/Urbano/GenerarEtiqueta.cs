using CapaDato.Bll.Ecommerce;
using CapaEntidad.Bll.Ecommerce;
using CapaEntidad.Bll.Util;
using Epson_Ticket;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrado.Urbano
{
    public class GenerarEtiqueta
    {
        private string RemoverDiacriticos(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        private string str_etiqueta(string ven_id)
        {
            try
            {
                Dat_Urbano dat_etiqueta = new Dat_Urbano();
                Ent_Etiqueta etiqueta = dat_etiqueta.get_etiqueta(ven_id);

                string strNroGuia = etiqueta.strNroGuia;
                //GuiaUrbano oGuia = guiaUrbano;

                // Generar Formato de Información
                string cliente = etiqueta.cliente;;//RemoverDiacriticos(oGuia.nom_cliente);
                string empresa = etiqueta.empresa;//RemoverDiacriticos(oGuia.nom_empresa);
                string nro_pedido = etiqueta.nro_pedido; //oGuia.nro_o_compra;
                string direccion = etiqueta.direccion;//RemoverDiacriticos(oGuia.dir_entrega + " " + oGuia.nro_via + " " + oGuia.nro_int);
                string referencia = etiqueta.referencia;//; RemoverDiacriticos(oGuia.ref_direc);
                string ubigeo = etiqueta.ubigeo;// RemoverDiacriticos(GenerarNombreUbigeo(oGuia.ubi_direc) + oGuia.ubi_direc);

                // Generar Código ZPL
                StringBuilder strb = new StringBuilder();
                strb.Append("^XA\n");                       // - Inic. Etiqueta
                strb.Append("^CI27\n");                     // - Imprimir Caracteres Latinos
                strb.Append("^JMA\n");                      // - Resolución: A=8d/mm, B=8d/mm 
                strb.Append("^PRC\n");                      // - Velocidad impresion 4pulg/seg.
                strb.Append("^FWN\n");                      // - Sin Rotar
                strb.Append("^BY3,,70^FS\n");               // - Ancho y Alto de Código de Barras
                strb.Append("^LH 0,20\n");                  // - Set Coordenada Inicial
                strb.Append("^FO040,040^GB700,696,2^FS\n"); // - Formato de Tabla
                strb.Append("^FO040,040^GB700,060,2^FS\n");
                strb.Append("^FO040,098^GB700,190,2^FS\n");
                strb.Append("^FO040,286^GB700,050,2^FS\n");
                strb.Append("^FO040,334^GB700,070,2^FS\n");
                strb.Append("^FO040,402^GB700,070,2^FS\n");
                strb.Append("^FO040,470^GB700,070,2^FS\n");
                strb.Append("^FO040,538^GB700,070,2^FS\n");
                strb.Append("^FO040,606^GB700,070,2^FS\n");
                strb.Append("^FO060,055^A0,060,030^FDNro. Guia: " + strNroGuia + "^FS\n");
                strb.Append("^FO630,068^A0,040,030^FDCourier^FS\n");
                strb.Append("^FO270,300^A0,040,028^FDInformacion de Envio^FS\n");
                strb.Append("^FO270,300^A0,041,028^FDInformacion de Envio^FS\n");
                strb.Append("^FO060,365^A0,035,025^FDRemitente :^FS\n");
                strb.Append("^FO195,362^A0,042,035^FD" + empresa + "^FS\n");
                strb.Append("^FO060,430^A0,035,025^FDDestinatario :^FS");
                strb.Append("^FO195,428^A0,042,035^FD" + cliente + "^FS\n");
                strb.Append("^FO195,495^A0,040,028^FD" + direccion + "^FS\n");
                strb.Append("^FO195,562^A0,040,028^FD" + referencia + "^FS\n");
                strb.Append("^FO195,632^A0,040,028^FD" + ubigeo + "^FS\n");
                strb.Append("^FO060,687^A0,060,030^FDNro. Pedido: " + nro_pedido + "^FS\n");
                strb.Append("^FO455,697^A0,040,030^FDTrasladado por: Urbano^FS\n");
                strb.Append("^FO200,130^BCN,110,Y,N,N^FDWYB17868551^FS\n");
                strb.Append("^XZ\n");
                return strb.ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public  void imp_etiqueta(string ven_id)
        {
            try
            {
                string strGuia = str_etiqueta(ven_id);
                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings = new PrinterSettings();
                //doc.PrinterSettings.PrinterName = ConfigurationManager.AppSettings["Impresora"].ToString();
                // Impresión de Comandos
                RawPrinterHelper.SendStringToPrinter(Ent_Global._impresora_etiquetas, strGuia);
            }
            catch (Exception)
            {
                                    
            }
        }

    }
}

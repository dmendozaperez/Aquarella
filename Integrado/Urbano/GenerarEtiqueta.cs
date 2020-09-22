using CapaDato.Bll.Ecommerce;
using CapaDato.Bll.Venta;
using CapaEntidad.Bll.Ecommerce;
using CapaEntidad.Bll.Util;
using CapaEntidad.Bll.Venta;
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
        #region<REGION PARA LA IMPRESION DE ETIQUETAS ECOMMERCE>
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

                if (etiqueta.strNroGuia.Length == 0)
                    return "";

                string strNroGuia = etiqueta.strNroGuia;
                //GuiaUrbano oGuia = guiaUrbano;

                // Generar Formato de Información
                string cliente = etiqueta.cliente; ;//RemoverDiacriticos(oGuia.nom_cliente);
                string empresa = etiqueta.empresa;//RemoverDiacriticos(oGuia.nom_empresa);
                string nro_pedido = etiqueta.nro_pedido; //oGuia.nro_o_compra;
                string direccion = etiqueta.direccion;//RemoverDiacriticos(oGuia.dir_entrega + " " + oGuia.nro_via + " " + oGuia.nro_int);
                string referencia = etiqueta.referencia;//; RemoverDiacriticos(oGuia.ref_direc);
                string ubigeo = etiqueta.ubigeo;// RemoverDiacriticos(GenerarNombreUbigeo(oGuia.ubi_direc) + oGuia.ubi_direc);
                string cod_refer = etiqueta.cod_refer;

                // *** 2018-04-27
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
                strb.Append("^FO060,687^A0,060,030^FDNro. Pedido: " + cod_refer + "^FS\n");
                strb.Append("^FO200,130^BCN,110,Y,N,N^FD" + strNroGuia + "^FS\n");
                strb.Append("^PQ2^FS\n");
                strb.Append("^XZ\n");
                return strb.ToString();



            }
            catch (Exception)
            {

                throw;
            }
        }


        private string str_etiqueta2(string ven_id)
        {
            try
            {
                Dat_Urbano dat_etiqueta = new Dat_Urbano();
                Ent_Etiqueta etiqueta = dat_etiqueta.get_etiqueta(ven_id);

                if (etiqueta.strNroGuia.Length == 0)
                    return "";

                string strNroGuia = etiqueta.strNroGuia;
                //GuiaUrbano oGuia = guiaUrbano;

                // Generar Formato de Información
                string cliente = etiqueta.cliente; ;//RemoverDiacriticos(oGuia.nom_cliente);
                string empresa = etiqueta.empresa;//RemoverDiacriticos(oGuia.nom_empresa);
                string nro_pedido = etiqueta.nro_pedido; //oGuia.nro_o_compra;
                string direccion = etiqueta.direccion;//RemoverDiacriticos(oGuia.dir_entrega + " " + oGuia.nro_via + " " + oGuia.nro_int);
                string referencia = etiqueta.referencia;//; RemoverDiacriticos(oGuia.ref_direc);
                string ubigeo = etiqueta.ubigeo;// RemoverDiacriticos(GenerarNombreUbigeo(oGuia.ubi_direc) + oGuia.ubi_direc);
                string cod_refer = etiqueta.cod_refer;
                string telefonos = etiqueta.telefono;

                direccion = direccion + " - " + ubigeo;

                //-- Modificado por  : Henry Morales - 17/05/2018
                //-- Asunto          : Se modifico para que muestre el código Alfanumérico en lugar del número
                StringBuilder strb = new StringBuilder();
                strb.Append("^XA\n");                       // - Inic. Etiqueta
                strb.Append("^CI27\n");                     // - Imprimir Caracteres Latinos
                strb.Append("^JMA\n");                      // - Resolución: A=8d/mm, B=8d/mm 
                strb.Append("^PRC\n");                      // - Velocidad impresion 4pulg/seg.
                strb.Append("^FWN\n");                      // - Sin Rotar
                strb.Append("^BY2,,20^FS\n");               // - Ancho y Alto de Código de Barras
                strb.Append("^LH 0,20\n");                  // - Set Coordenada Inicial
                                                            //strb.Append("^FO040,105^A0,030,020^FD" + cliente.ToUpper() + "^FS\n");
                                                            //strb.Append("^FO040,135^A0,030,020^FDPEDIDO: " + cod_refer + "^FS\n");
                                                            ////strb.Append("^FO040,135^A0,030,020^FDPEDIDO: " + nro_pedido + "^FS\n");
                                                            //strb.Append("^FO040,015^BCN,62,Y,N,N^FD" + strNroGuia + "^FS\n");
                                                            //strb.Append("^FO460,105^A0,030,020^^FD" + cliente.ToUpper() + "^FS\n");
                                                            //strb.Append("^FO460,135^A0,030,020^FDPEDIDO: " + cod_refer + "^FS\n");
                                                            ////strb.Append("^FO460,135^A0,030,020^FDPEDIDO: " + nro_pedido + "^FS\n");
                                                            //strb.Append("^FO460,015^BCN,62,Y,N,N^FD" + strNroGuia + "^FS\n");
                                                            ////strb.Append("^PQ3^FS\n"); // Imprimir Triple Copia de Etiqueta

                /// agregado 04/06/2018 - Henry Morales
                /// Se ajusta para agregarle datos en la impresión; se modificó el primer margen
                /////////1ra Impresión///////////
                strb.Append("^FO030,105^A0,030,020^FD" + cliente.ToUpper() + "^FS\n");
                strb.Append("^FO030,135^A0,030,020^FDPEDIDO: " + cod_refer + "^FS\n");
                strb.Append("^FO030,165^A0,030,020^FDTELEF.: " + telefonos + "^FS\n");
                // DIRECCION
                if (direccion.Length <= 30)
                {
                    strb.Append("^FO030,195^A0,030,020^FDDIREC.: " + direccion + "^FS\n");
                }
                else
                {
                    strb.Append("^FO030,195^A0,030,020^FDDIREC.: " + direccion.Substring(0, 30) + "^FS\n");
                    if (direccion.Length > 60)
                    {
                        strb.Append("^FO030,225^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(30, 30) + "^FS\n");

                        if (direccion.Length > 90)
                        {
                            strb.Append("^FO030,255^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(60, 30) + "^FS\n");

                            if (direccion.Length > 120)
                            {
                                strb.Append("^FO030,285^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(90, 30) + "^FS\n");
                            }
                            else
                            {
                                strb.Append("^FO030,285^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(90, direccion.Length - 90) + "^FS\n");
                            }
                        }
                        else
                        {
                            strb.Append("^FO030,255^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(60, direccion.Length - 60) + "^FS\n");
                        }
                    }
                    else
                    {
                        strb.Append("^FO030,225^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(30, direccion.Length - 30) + "^FS\n");
                    }
                }
                // REFERENCIA
                if (referencia.Length > 30)
                {
                    strb.Append("^FO030,315^A0,030,020^FDREFER.: " + referencia.Substring(0, 30) + "^FS\n");
                }
                else
                {
                    strb.Append("^FO030,315^A0,030,020^FDREFER.: " + referencia + "^FS\n");
                }
                if (referencia.Length > 30)
                {
                    if (referencia.Length > 60)
                    {
                        strb.Append("^FO030,345^A0,030,020^FD" + "".PadLeft(12) + referencia.Substring(30, 30) + "^FS\n");
                    }
                    else
                    {
                        strb.Append("^FO030,345^A0,030,020^FD" + "".PadLeft(12) + referencia.Substring(30, referencia.Length - 30) + "^FS\n");
                    }
                }
                strb.Append("^FO030,015^BCN,62,Y,N,N^FD" + strNroGuia + "^FS\n");
                /////////////////////////////////
                /////////2da Impresión///////////
                /////////////////////////////////
                strb.Append("^FO460,105^A0,030,020^^FD" + cliente.ToUpper() + "^FS\n");
                strb.Append("^FO460,135^A0,030,020^FDPEDIDO: " + cod_refer + "^FS\n");
                strb.Append("^FO460,165^A0,030,020^FDTELEF.: " + telefonos + "^FS\n");
                // DIRECCION
                if (direccion.Length <= 30)
                {
                    strb.Append("^FO460,195^A0,030,020^FDDIREC.: " + direccion + "^FS\n");
                }
                else
                {
                    strb.Append("^FO460,195^A0,030,020^FDDIREC.: " + direccion.Substring(0, 30) + "^FS\n");
                    if (direccion.Length > 60)
                    {
                        strb.Append("^FO460,225^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(30, 30) + "^FS\n");

                        if (direccion.Length > 90)
                        {
                            strb.Append("^FO460,255^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(60, 30) + "^FS\n");

                            if (direccion.Length > 120)
                            {
                                strb.Append("^FO460,285^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(90, 30) + "^FS\n");
                            }
                            else
                            {
                                strb.Append("^FO460,285^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(90, direccion.Length - 90) + "^FS\n");
                            }
                        }
                        else
                        {
                            strb.Append("^FO460,255^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(60, direccion.Length - 60) + "^FS\n");
                        }
                    }
                    else
                    {
                        strb.Append("^FO460,225^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(30, direccion.Length - 30) + "^FS\n");
                    }
                }
                // REFERENCIA
                if (referencia.Length > 30)
                {
                    strb.Append("^FO460,315^A0,030,020^FDREFER.: " + referencia.Substring(0, 30) + "^FS\n");
                }
                else
                {
                    strb.Append("^FO460,315^A0,030,020^FDREFER.: " + referencia + "^FS\n");
                }
                if (referencia.Length > 30)
                {
                    if (referencia.Length > 60)
                    {
                        strb.Append("^FO460,345^A0,030,020^FD" + "".PadLeft(12) + referencia.Substring(30, 30) + "^FS\n");
                    }
                    else
                    {
                        strb.Append("^FO460,345^A0,030,020^FD" + "".PadLeft(12) + referencia.Substring(30, referencia.Length - 30) + "^FS\n");
                    }
                }
                strb.Append("^FO460,015^BCN,62,Y,N,N^FD" + strNroGuia + "^FS\n");
                /// agregado 04/06/2018 - Henry Morales
                strb.Append("^XZ\n");
                return strb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void imp_etiqueta(string ven_id)
        {
            try
            {
                string strGuia = str_etiqueta(ven_id);
                if (strGuia.Length == 0)
                    return;

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

        public void imp_etiqueta2(string ven_id)
        {
            try
            {
                string strGuia = str_etiqueta2(ven_id);
                if (strGuia.Length == 0)
                    return;

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
        #endregion


        #region<REGION PARA LA IMPRESION DE ETIQUETAS AQUARELLA>
        private string aq_str_etiqueta(string ven_id)
        {
            try
            {
                Dat_Etiquetas_Catalogo dat_etiqueta = new Dat_Etiquetas_Catalogo();
                //Dat_Urbano dat_etiqueta = new Dat_Urbano();
                Ent_Etiquetas_Catalogo etiqueta = dat_etiqueta.get_etiquetas(ven_id);

                if (etiqueta == null)
                    return "";

                string liq_tipo_prov= etiqueta.liq_tipo_prov;
                string liq_tipodes = etiqueta.liq_tipodes;
                string provincia = etiqueta.provincia;
                string pedido = etiqueta.pedido;
                string unidades = etiqueta.unidades;
                string lider = etiqueta.lider;
                string promotor = etiqueta.promotor;
                string direccion = etiqueta.direccion;
                string distrito = etiqueta.distrito;
                string referencia = etiqueta.referencia;
                string destino = etiqueta.destino;
                string agencia = etiqueta.agencia;
                string agencia_direccion = etiqueta.agencia_direccion;
                
                //string strNroGuia = etiqueta.pedido;
                ////GuiaUrbano oGuia = guiaUrbano;

                //// Generar Formato de Información
                //string cliente = etiqueta.cliente; ;//RemoverDiacriticos(oGuia.nom_cliente);
                //string empresa = etiqueta.empresa;//RemoverDiacriticos(oGuia.nom_empresa);
                //string nro_pedido = etiqueta.nro_pedido; //oGuia.nro_o_compra;
                //string direccion = etiqueta.direccion;//RemoverDiacriticos(oGuia.dir_entrega + " " + oGuia.nro_via + " " + oGuia.nro_int);
                //string referencia = etiqueta.referencia;//; RemoverDiacriticos(oGuia.ref_direc);
                //string ubigeo = etiqueta.ubigeo;// RemoverDiacriticos(GenerarNombreUbigeo(oGuia.ubi_direc) + oGuia.ubi_direc);
                //string cod_refer = etiqueta.cod_refer;

                // *** 2018-04-27
                // Generar Código ZPL
                StringBuilder strb = new StringBuilder();
                /*FORMATO LIMA-CALLAO*/
                if (liq_tipo_prov=="LC")
                {
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
                    strb.Append("^FO060,055^A0,060,030^FDProvincia: " + provincia + "^FS\n");
                    strb.Append("^FO060,055^A0,060,030^FDNro.Pedido: " + pedido + "^FS\n");
                    strb.Append("^FO060,055^A0,060,030^FDUni: " + unidades + "^FS\n");
                    strb.Append("^FO060,430^A0,035,025^FDDestinatario :^FS");
                    strb.Append("^FO195,428^A0,042,035^FD" + lider + "^FS\n");
                    strb.Append("^FO195,495^A0,040,028^FD" + promotor + "^FS\n");
                    strb.Append("^FO195,562^A0,040,028^FD" + direccion + "^FS\n");
                    strb.Append("^FO195,632^A0,040,028^FD" + distrito + "^FS\n");
                    strb.Append("^FO195,632^A0,040,028^FD" + referencia + "^FS\n");

                    //strb.Append("^FO630,068^A0,040,030^FDCourier^FS\n");
                    //strb.Append("^FO270,300^A0,040,028^FDInformacion de Envio^FS\n");
                    //strb.Append("^FO270,300^A0,041,028^FDInformacion de Envio^FS\n");
                    //strb.Append("^FO060,365^A0,035,025^FDRemitente :^FS\n");
                    //strb.Append("^FO195,362^A0,042,035^FD" + empresa + "^FS\n");
                    //strb.Append("^FO060,430^A0,035,025^FDDestinatario :^FS");
                    //strb.Append("^FO195,428^A0,042,035^FD" + cliente + "^FS\n");
                    //strb.Append("^FO195,495^A0,040,028^FD" + direccion + "^FS\n");
                    //strb.Append("^FO195,562^A0,040,028^FD" + referencia + "^FS\n");
                    //strb.Append("^FO195,632^A0,040,028^FD" + ubigeo + "^FS\n");
                    //strb.Append("^FO060,687^A0,060,030^FDNro. Pedido: " + cod_refer + "^FS\n");
                    strb.Append("^FO200,130^BCN,110,Y,N,N^FD" + pedido + "^FS\n");
                    strb.Append("^PQ2^FS\n");
                    strb.Append("^XZ\n");


                }
                else
                {
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
                    strb.Append("^FO060,055^A0,060,030^FDProvincia: " + provincia + "^FS\n");
                    strb.Append("^FO060,055^A0,060,030^FDNro.Pedido: " + pedido + "^FS\n");
                    strb.Append("^FO060,055^A0,060,030^FDUni: " + unidades + "^FS\n");
                    strb.Append("^FO060,430^A0,035,025^FDDestinatario :^FS");
                    strb.Append("^FO195,428^A0,042,035^FD" + lider + "^FS\n");
                    strb.Append("^FO195,495^A0,040,028^FD" + promotor + "^FS\n");
                    strb.Append("^FO195,562^A0,040,028^FD" + destino + "^FS\n");
                    strb.Append("^FO195,632^A0,040,028^FD" + agencia + "^FS\n");
                    strb.Append("^FO195,632^A0,040,028^FD" + agencia_direccion + "^FS\n");

                    //strb.Append("^FO630,068^A0,040,030^FDCourier^FS\n");
                    //strb.Append("^FO270,300^A0,040,028^FDInformacion de Envio^FS\n");
                    //strb.Append("^FO270,300^A0,041,028^FDInformacion de Envio^FS\n");
                    //strb.Append("^FO060,365^A0,035,025^FDRemitente :^FS\n");
                    //strb.Append("^FO195,362^A0,042,035^FD" + empresa + "^FS\n");
                    //strb.Append("^FO060,430^A0,035,025^FDDestinatario :^FS");
                    //strb.Append("^FO195,428^A0,042,035^FD" + cliente + "^FS\n");
                    //strb.Append("^FO195,495^A0,040,028^FD" + direccion + "^FS\n");
                    //strb.Append("^FO195,562^A0,040,028^FD" + referencia + "^FS\n");
                    //strb.Append("^FO195,632^A0,040,028^FD" + ubigeo + "^FS\n");
                    //strb.Append("^FO060,687^A0,060,030^FDNro. Pedido: " + cod_refer + "^FS\n");
                    strb.Append("^FO200,130^BCN,110,Y,N,N^FD" + pedido + "^FS\n");
                    strb.Append("^PQ2^FS\n");
                    strb.Append("^XZ\n");
                }


               

                return strb.ToString();



            }
            catch (Exception)
            {

                throw;
            }
        }


        private string aq_str_etiqueta2(string ven_id)
        {
            try
            {

                Dat_Etiquetas_Catalogo dat_etiqueta = new Dat_Etiquetas_Catalogo();
                //Dat_Urbano dat_etiqueta = new Dat_Urbano();
                //Ent_Etiquetas_Catalogo
                Ent_Etiquetas_Catalogo etiqueta = dat_etiqueta.get_etiquetas(ven_id);

                if (etiqueta ==null)
                    return "";

                //string strNroGuia = etiqueta.strNroGuia;
                ////GuiaUrbano oGuia = guiaUrbano;

                string liq_tipo_prov = etiqueta.liq_tipo_prov;
                string liq_tipodes = etiqueta.liq_tipodes;
                string provincia = etiqueta.provincia;
                string pedido = etiqueta.pedido;
                string unidades = etiqueta.unidades;
                string lider = etiqueta.lider;
                string promotor = etiqueta.promotor;
                string direccion = etiqueta.direccion;
                string distrito = etiqueta.distrito;
                string referencia = etiqueta.referencia;
                string destino = etiqueta.destino;
                string agencia = etiqueta.agencia;
                string agencia_direccion = etiqueta.agencia_direccion;
                string numdoc = etiqueta.numdoc;
                string tipo_des = etiqueta.tipo_des;

                //// Generar Formato de Información
                //string cliente = etiqueta.cliente; ;//RemoverDiacriticos(oGuia.nom_cliente);
                //string empresa = etiqueta.empresa;//RemoverDiacriticos(oGuia.nom_empresa);
                //string nro_pedido = etiqueta.nro_pedido; //oGuia.nro_o_compra;
                //string direccion = etiqueta.direccion;//RemoverDiacriticos(oGuia.dir_entrega + " " + oGuia.nro_via + " " + oGuia.nro_int);
                //string referencia = etiqueta.referencia;//; RemoverDiacriticos(oGuia.ref_direc);
                //string ubigeo = etiqueta.ubigeo;// RemoverDiacriticos(GenerarNombreUbigeo(oGuia.ubi_direc) + oGuia.ubi_direc);
                //string cod_refer = etiqueta.cod_refer;
                //string telefonos = etiqueta.telefono;

                //direccion = direccion + " - " + ubigeo;

                //-- Modificado por  : Henry Morales - 17/05/2018
                //-- Asunto          : Se modifico para que muestre el código Alfanumérico en lugar del número
                StringBuilder strb = new StringBuilder();
                if (liq_tipo_prov == "LC")
                {
                    strb.Append("^XA\n");                       // - Inic. Etiqueta
                    strb.Append("^CI27\n");                     // - Imprimir Caracteres Latinos
                    strb.Append("^JMA\n");                      // - Resolución: A=8d/mm, B=8d/mm 
                    strb.Append("^PRC\n");                      // - Velocidad impresion 4pulg/seg.
                    strb.Append("^FWN\n");                      // - Sin Rotar
                    strb.Append("^BY2,,20^FS\n");               // - Ancho y Alto de Código de Barras
                    strb.Append("^LH 0,20\n");                  // - Set Coordenada Inicial
                                                                //strb.Append("^FO040,105^A0,030,020^FD" + cliente.ToUpper() + "^FS\n");
                                                                //strb.Append("^FO040,135^A0,030,020^FDPEDIDO: " + cod_refer + "^FS\n");
                                                                ////strb.Append("^FO040,135^A0,030,020^FDPEDIDO: " + nro_pedido + "^FS\n");
                                                                //strb.Append("^FO040,015^BCN,62,Y,N,N^FD" + strNroGuia + "^FS\n");
                                                                //strb.Append("^FO460,105^A0,030,020^^FD" + cliente.ToUpper() + "^FS\n");
                                                                //strb.Append("^FO460,135^A0,030,020^FDPEDIDO: " + cod_refer + "^FS\n");
                                                                ////strb.Append("^FO460,135^A0,030,020^FDPEDIDO: " + nro_pedido + "^FS\n");
                                                                //strb.Append("^FO460,015^BCN,62,Y,N,N^FD" + strNroGuia + "^FS\n");
                                                                ////strb.Append("^PQ3^FS\n"); // Imprimir Triple Copia de Etiqueta

                    /// agregado 04/06/2018 - Henry Morales
                    /// Se ajusta para agregarle datos en la impresión; se modificó el primer margen
                    /////////1ra Impresión///////////
                    strb.Append("^FO030,105^A0,030,020^FDProvincia: " + provincia.ToUpper() + "^FS\n");
                    strb.Append("^FO030,135^A0,030,020^FDNro.Pedido: " + pedido + "^FS\n");
                    strb.Append("^FO030,165^A0,030,020^FDUni: " + unidades + "^FS\n");
                    strb.Append("^FO030,195^A0,030,020^FDLider: " + lider + "^FS\n");
                    strb.Append("^FO030,225^A0,030,020^FDPromor: " + promotor + "^FS\n");
                    // DIRECCION
                    if (direccion.Length <= 30)
                    {
                        strb.Append("^FO030,255^A0,030,020^FDDirec.: " + direccion + "^FS\n");
                    }
                    else
                    {
                        strb.Append("^FO030,255^A0,030,020^FDDirec.: " + direccion.Substring(0, 30) + "^FS\n");
                        if (direccion.Length > 60)
                        {
                            strb.Append("^FO030,285^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(30, 30) + "^FS\n");

                            if (direccion.Length > 90)
                            {
                                strb.Append("^FO030,315^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(60, 30) + "^FS\n");

                                if (direccion.Length > 120)
                                {
                                    strb.Append("^FO030,345^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(90, 30) + "^FS\n");
                                }
                                else
                                {
                                    strb.Append("^FO030,345^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(90, direccion.Length - 90) + "^FS\n");
                                }
                            }
                            else
                            {
                                strb.Append("^FO030,315^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(60, direccion.Length - 60) + "^FS\n");
                            }
                        }
                        else
                        {
                            strb.Append("^FO030,285^A0,030,020^FD" + "".PadLeft(12) + direccion.Substring(30, direccion.Length - 30) + "^FS\n");
                        }
                    }
                    // REFERENCIA
                    if (referencia.Length > 30)
                    {
                        strb.Append("^FO030,375^A0,030,020^FDRefer.: " + referencia.Substring(0, 30) + "^FS\n");
                    }
                    else
                    {
                        strb.Append("^FO030,375^A0,030,020^FDRefer.: " + referencia + "^FS\n");
                    }
                    if (referencia.Length > 30)
                    {
                        if (referencia.Length > 60)
                        {
                            strb.Append("^FO030,405^A0,030,020^FD" + "".PadLeft(12) + referencia.Substring(30, 30) + "^FS\n");
                        }
                        else
                        {
                            strb.Append("^FO030,405^A0,030,020^FD" + "".PadLeft(12) + referencia.Substring(30, referencia.Length - 30) + "^FS\n");
                        }
                    }
                    strb.Append("^FO030,435^A0,030,020^FDDistri: " + distrito + "^FS\n");

                    strb.Append("^FO030,465^A0,030,020^FDTipDes: " + tipo_des + "^FS\n");

                    strb.Append("^FO030,015^BCN,62,Y,N,N^FD" + numdoc + "^FS\n");
                    strb.Append("^XZ\n");
                }
                else
                {
                    strb.Append("^XA\n");                       // - Inic. Etiqueta
                    strb.Append("^CI27\n");                     // - Imprimir Caracteres Latinos
                    strb.Append("^JMA\n");                      // - Resolución: A=8d/mm, B=8d/mm 
                    strb.Append("^PRC\n");                      // - Velocidad impresion 4pulg/seg.
                    strb.Append("^FWN\n");                      // - Sin Rotar
                    strb.Append("^BY2,,20^FS\n");               // - Ancho y Alto de Código de Barras
                    strb.Append("^LH 0,20\n");                  // - Set Coordenada Inicial
                                                                //strb.Append("^FO040,105^A0,030,020^FD" + cliente.ToUpper() + "^FS\n");
                                                                //strb.Append("^FO040,135^A0,030,020^FDPEDIDO: " + cod_refer + "^FS\n");
                                                                ////strb.Append("^FO040,135^A0,030,020^FDPEDIDO: " + nro_pedido + "^FS\n");
                                                                //strb.Append("^FO040,015^BCN,62,Y,N,N^FD" + strNroGuia + "^FS\n");
                                                                //strb.Append("^FO460,105^A0,030,020^^FD" + cliente.ToUpper() + "^FS\n");
                                                                //strb.Append("^FO460,135^A0,030,020^FDPEDIDO: " + cod_refer + "^FS\n");
                                                                ////strb.Append("^FO460,135^A0,030,020^FDPEDIDO: " + nro_pedido + "^FS\n");
                                                                //strb.Append("^FO460,015^BCN,62,Y,N,N^FD" + strNroGuia + "^FS\n");
                                                                ////strb.Append("^PQ3^FS\n"); // Imprimir Triple Copia de Etiqueta

                    /// agregado 04/06/2018 - Henry Morales
                    /// Se ajusta para agregarle datos en la impresión; se modificó el primer margen
                    /////////1ra Impresión///////////
                    strb.Append("^FO030,105^A0,030,020^FDProvincia: " + provincia.ToUpper() + "^FS\n");
                    strb.Append("^FO030,135^A0,030,020^FDNro.Pedido: " + pedido + "^FS\n");
                    strb.Append("^FO030,165^A0,030,020^FDUni: " + unidades + "^FS\n");
                    strb.Append("^FO030,195^A0,030,020^FDLider: " + lider + "^FS\n");
                    strb.Append("^FO030,225^A0,030,020^FDPromor: " + promotor + "^FS\n");
                    // DIRECCION
                    if (destino.Length <= 30)
                    {
                        strb.Append("^FO030,255^A0,030,020^FDDesti.: " + destino + "^FS\n");
                    }
                    else
                    {
                        strb.Append("^FO030,255^A0,030,020^FDDesti.: " + destino.Substring(0, 30) + "^FS\n");
                        if (destino.Length > 60)
                        {
                            strb.Append("^FO030,285^A0,030,020^FD" + "".PadLeft(12) + destino.Substring(30, 30) + "^FS\n");

                            if (destino.Length > 90)
                            {
                                strb.Append("^FO030,315^A0,030,020^FD" + "".PadLeft(12) + destino.Substring(60, 30) + "^FS\n");

                                if (destino.Length > 120)
                                {
                                    strb.Append("^FO030,345^A0,030,020^FD" + "".PadLeft(12) + destino.Substring(90, 30) + "^FS\n");
                                }
                                else
                                {
                                    strb.Append("^FO030,345^A0,030,020^FD" + "".PadLeft(12) + destino.Substring(90, destino.Length - 90) + "^FS\n");
                                }
                            }
                            else
                            {
                                strb.Append("^FO030,315^A0,030,020^FD" + "".PadLeft(12) + destino.Substring(60, destino.Length - 60) + "^FS\n");
                            }
                        }
                        else
                        {
                            strb.Append("^FO030,285^A0,030,020^FD" + "".PadLeft(12) + destino.Substring(30, destino.Length - 30) + "^FS\n");
                        }
                    }
                    // REFERENCIA
                    if (agencia.Length > 30)
                    {
                        strb.Append("^FO030,375^A0,030,020^FDAgen.: " + agencia.Substring(0, 30) + "^FS\n");
                    }
                    else
                    {
                        strb.Append("^FO030,375^A0,030,020^FDAgen.: " + agencia + "^FS\n");
                    }
                    if (agencia.Length > 30)
                    {
                        if (agencia.Length > 60)
                        {
                            strb.Append("^FO030,405^A0,030,020^FD" + "".PadLeft(12) + agencia.Substring(30, 30) + "^FS\n");
                        }
                        else
                        {
                            strb.Append("^FO030,405^A0,030,020^FD" + "".PadLeft(12) + agencia.Substring(30, agencia.Length - 30) + "^FS\n");
                        }
                    }
                    strb.Append("^FO030,435^A0,030,020^FDDirAgen: " + agencia_direccion + "^FS\n");

                    strb.Append("^FO030,465^A0,030,020^FDTipDes: " + tipo_des + "^FS\n");

                    strb.Append("^FO030,015^BCN,62,Y,N,N^FD" + numdoc + "^FS\n");
                    strb.Append("^XZ\n");
                }



              
                return strb.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void aq_imp_etiqueta(string ven_id)
        {
            try
            {
                string strGuia = aq_str_etiqueta(ven_id);
                if (strGuia.Length == 0)
                    return;

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

        public void aq_imp_etiqueta2(string ven_id)
        {
            try
            {
                string strGuia = aq_str_etiqueta2(ven_id);
                if (strGuia.Length == 0)
                    return;

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
        #endregion

    }
}

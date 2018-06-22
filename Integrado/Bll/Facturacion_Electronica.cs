using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDato.Bll.Venta;
using Carvajal.FEPE.PreSC.Core;
using System.Xml.Linq;
using CapaEntidad.Bll.Util;

namespace Integrado.Bll
{
    public class Facturacion_Electronica
    {
        public static void FE_QR(string _tipo_doc, string _num_doc, ref Byte[] img_qr, ref string _error)
        {
            string _formato_doc = "";
            try
            {
                _formato_doc = Dat_Venta._leer_formato_electronico(_tipo_doc, _num_doc, ref _error);
                GeneratorCdp generatorCdp = new GeneratorCdp();
                if (_tipo_doc == "B" || _tipo_doc == "F")
                {
                    img_qr = generatorCdp.GetImageQrCodeForInvoiceCdp(_formato_doc);

                }
                else
                {
                    img_qr = generatorCdp.GetImageQrCodeForNoteCdp(_formato_doc);
                }

            }
            catch (Exception exc)
            {

                _error = exc.Message;
            }
        }
        public static void ejecutar_factura_electronica(string _tipo_doc, string _num_doc, ref string cod_hash, ref string _error, ref string url_pdf)
        {
            string _formato_doc = "";

            try
            {
                /*QUIERE DECIR QUE QUE SE USA LA FACTURACION ELECTRONICA DE CARVAJAL*/
                if (Ent_Global.pr_facturador=="C")
                { 
                    _formato_doc =Dat_Venta._leer_formato_electronico(_tipo_doc, _num_doc, ref _error);
                    GeneratorCdp generatorCdp = new GeneratorCdp();
                    //XmlDocument xmlDoc = new XmlDocument();
                    //xmlDoc.Load("C:\\carvajal\\xml\\20101951872_07_F030_22.xml");

                    //byte[] _valor=generatorCdp.GetImageBarCodeForNoteCdp(_formato_doc);

                    if (_tipo_doc == "B" || _tipo_doc == "F")
                    {
                        cod_hash = generatorCdp.GetHashForInvoiceCdp(_formato_doc);
                    }
                    else
                    {
                        cod_hash = generatorCdp.GetHashForNoteCdp(_formato_doc);
                    }
                }
                /*ESTA CONDICION ES EL PROVEEDOR PAPERLESS*/
                if (Ent_Global.pr_facturador == "P")
                {
                    _formato_doc = Dat_Venta._leer_formato_electronico_PAPERLESS(_tipo_doc, _num_doc, ref _error);
                    string ruc_empresa = Ent_Global._ws_ruc; string ws_login = Ent_Global._ws_login; string ws_pass = Ent_Global._ws_password; Int32 tipofoliacion = 1;

                    ///*0 = ID asignado
                    //1 = URL del XML
                    //2 = URL del PDF
                    //3 = Estado en la SUNAT
                    //4 = Folio Asignado(Serie - Correlativo)
                    //5 = Bytes del PDF en Base64
                    //6 = PDF417(Cadena de texto a imprimir en el PDF 417)
                    //7 = HASH(Cadena de texto)*/

                    FEBata.OnlinePortTypeClient gen_fe = new FEBata.OnlinePortTypeClient();
                    string consulta = gen_fe.OnlineGeneration(ruc_empresa, ws_login, ws_pass, _formato_doc, tipofoliacion, 7);

                    consulta = consulta.Replace("&", "amp;");

                    var doc = XDocument.Parse(consulta);
                    var result = from factura in doc.Descendants("Respuesta")
                                 select new
                                 {
                                     Codigo = factura.Element("Codigo").Value,
                                     Mensaje = factura.Element("Mensaje").Value.Replace("amp;", "&"),
                                 };

                    foreach (var item in result)
                    {
                        if (item.Codigo != "0")
                        {
                            _error = item.Mensaje;
                            break;
                        }
                        else
                        {
                            cod_hash = item.Mensaje;

                            /*SI LA GENERACION ES EXITOSA ENTONCES EXTRAEMOS EL PDF URL*/
                            consulta = gen_fe.OnlineGeneration(ruc_empresa, ws_login, ws_pass, _formato_doc, tipofoliacion, 2);
                            consulta = consulta.Replace("&", "amp;");
                            var docpdf = XDocument.Parse(consulta);
                            var resultpdf = from factura in docpdf.Descendants("Respuesta")
                                            select new
                                            {
                                                Codigo = factura.Element("Codigo").Value,
                                                Mensaje = factura.Element("Mensaje").Value.Replace("amp;", "&"),
                                            };
                            foreach (var itempdf in resultpdf)
                            {
                                url_pdf = itempdf.Mensaje;
                            }
                            /*******/
                        }
                    }
                }
                //enviar_xml_webservice bata===>>>




            }
            catch (Exception exc)
            {
                _error = exc.Message;
            }
        }

        public static void ejecutar_factura_electronica_ws(string _tipo_doc, string _num_doc, ref string cod_hash, ref string _error,ref string url_pdf)
        {
            string _formato_doc = "";

            try
            {

                _formato_doc = Dat_Venta._leer_formato_electronico_PAPERLESS(_tipo_doc, _num_doc, ref _error);
                string ruc_empresa =Ent_Global._ws_ruc ; string ws_login = Ent_Global._ws_login; string ws_pass =Ent_Global._ws_password; Int32 tipofoliacion = 1;

                ///*0 = ID asignado
                //1 = URL del XML
                //2 = URL del PDF
                //3 = Estado en la SUNAT
                //4 = Folio Asignado(Serie - Correlativo)
                //5 = Bytes del PDF en Base64
                //6 = PDF417(Cadena de texto a imprimir en el PDF 417)
                //7 = HASH(Cadena de texto)*/

                FEBata.OnlinePortTypeClient gen_fe = new FEBata.OnlinePortTypeClient();
                string consulta = gen_fe.OnlineGeneration(ruc_empresa, ws_login, ws_pass, _formato_doc, tipofoliacion, 7);

                consulta = consulta.Replace("&", "amp;");

                var doc = XDocument.Parse(consulta);
                var result = from factura in doc.Descendants("Respuesta")
                             select new
                             {
                                 Codigo = factura.Element("Codigo").Value,
                                 Mensaje = factura.Element("Mensaje").Value.Replace("amp;", "&"),
                             };

                foreach(var item in result)
                {
                    if (item.Codigo != "0")
                    { 
                        _error = item.Mensaje;
                        break;
                    }
                    else
                    {
                        cod_hash = item.Mensaje;

                        /*SI LA GENERACION ES EXITOSA ENTONCES EXTRAEMOS EL PDF URL*/
                        consulta = gen_fe.OnlineGeneration(ruc_empresa, ws_login, ws_pass, _formato_doc, tipofoliacion, 2);
                        consulta = consulta.Replace("&", "amp;");
                        var resultpdf = from factura in doc.Descendants("Respuesta")
                                     select new
                                     {
                                         Codigo = factura.Element("Codigo").Value,
                                         Mensaje = factura.Element("Mensaje").Value.Replace("amp;", "&"),
                                     };
                        foreach (var itempdf in resultpdf)
                        {
                            url_pdf = itempdf.Mensaje;
                        }
                        /*******/
                    }
                }

                //GeneratorCdp generatorCdp = new GeneratorCdp();
                //XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.Load("C:\\carvajal\\xml\\20101951872_07_F030_22.xml");

                //byte[] _valor=generatorCdp.GetImageBarCodeForNoteCdp(_formato_doc);

                //if (_tipo_doc == "B" || _tipo_doc == "F")
                //{
                //    cod_hash = generatorCdp.GetHashForInvoiceCdp(_formato_doc);
                //}
                //else
                //{
                //    cod_hash = generatorCdp.GetHashForNoteCdp(_formato_doc);
                //}
                //enviar_xml_webservice bata===>>>




            }
            catch (Exception exc)
            {
                _error = exc.Message;
            }
        }
    }
}

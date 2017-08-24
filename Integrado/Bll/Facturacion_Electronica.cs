using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDato.Bll.Venta;
using Carvajal.FEPE.PreSC.Core;

namespace Integrado.Bll
{
    public class Facturacion_Electronica
    {
        public static void ejecutar_factura_electronica(string _tipo_doc, string _num_doc, ref string cod_hash, ref string _error)
        {
            string _formato_doc = "";

            try
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
                //enviar_xml_webservice bata===>>>




            }
            catch (Exception exc)
            {
                _error = exc.Message;
            }
        }
      
    }
}

using Carvajal.FEPE.PreSC.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrado.Bll
{
    public class Fe_Sunat_Qr
    {
        #region<VARIABLES LOCALES>
        private string RUCEmisor { get; set; }
        private string TipoComprobante { get; set; }
        private string Numero_serie { get; set; }
        private string Numero_correlativo { get; set; }
        private string IGV { get; set; }
        private string Total { get; set; }
        private string FechaEmision { get; set; }
        private string TipoDocumentoReceptor { get; set; }
        private string numeroDocumentoReceptor { get; set; }
        private string textpipestr { get; set; }
        #endregion
        #region<METODOS NO ESTATICOS>
        public byte[] GetQrSunatInvoiceCdp(string str)
        {
            byte[] qr = null;            
            try
            {
                /*este metodo es por defecto factura o boleta*/
                textpipestr = GetQRtextPipeSeparated("01",str);

               if (textpipestr.Length>0)
                {
                    GeneratorCdp generatorCdp = new GeneratorCdp();
                    qr = generatorCdp.GetImageQrCodeFromString(textpipestr);
                }

            }
            catch (Exception)
            {
                qr = null;
            }
            return qr;
        }
        public byte[] GetQrSunatNoteCdp(string str)
        {
            byte[] qr = null;
            try
            {
                /*este metodo es por defecto nota de credito*/
                textpipestr = GetQRtextPipeSeparated("07", str);

                if (textpipestr.Length > 0)
                {
                    GeneratorCdp generatorCdp = new GeneratorCdp();
                    qr = generatorCdp.GetImageQrCodeFromString(textpipestr);
                }
            }
            catch (Exception)
            {
                qr = null;
            }
            return qr;
        }
        private string GetQRtextPipeSeparated(string tipodoc,String str)
        {          
            try
            {
                /*depende del tipo de documento extrae los datos del formato string*/
                List<string> lines = new List<string>();
                string line;
                switch (tipodoc)
                {

                    case "01":
                    case "03":                        
                        using (StringReader sr = new StringReader(str))
                        {
                            /*en este caso vamos a leer linea por linea y agregar al list<string>*/
                            while ((line = sr.ReadLine()) != null)
                            {
                                lines.Add(line);
                            }
                        }
                        /*ahora recorremos lines de string array*/
                        if (lines != null)
                        {
                            string[] campos = lines[1].ToString().Split('+');
                            RUCEmisor = campos[9].ToString();
                            TipoComprobante = campos[5].ToString();
                            Numero_serie = Basico.Left(campos[6].ToString(),4);
                            Numero_correlativo = Basico.Right(campos[6].ToString(), 8);
                            IGV = campos[25].ToString();
                            Total = campos[29].ToString();
                            FechaEmision = campos[7].ToString();
                            TipoDocumentoReceptor = campos[21].ToString();
                            numeroDocumentoReceptor = campos[20].ToString();
                            
                        }
                        break;
                    case "07":
                      
                        using (StringReader sr = new StringReader(str))
                        {
                            /*en este caso vamos a leer linea por linea y agregar al list<string>*/
                            while ((line = sr.ReadLine()) != null)
                            {
                                lines.Add(line);
                            }
                        }
                        /*ahora recorremos lines de string array*/
                        if (lines != null)
                        {
                            string[] campos = lines[1].ToString().Split('+');
                            RUCEmisor = campos[8].ToString();
                            TipoComprobante = campos[4].ToString();
                            Numero_serie = Basico.Left(campos[5].ToString(), 4);
                            Numero_correlativo = Basico.Right(campos[5].ToString(), 8);
                            IGV = campos[23].ToString();
                            Total = campos[26].ToString();
                            FechaEmision = campos[6].ToString();
                            TipoDocumentoReceptor = campos[20].ToString();
                            numeroDocumentoReceptor = campos[19].ToString();

                        }
                        break;
                }
                
            }
            catch
            {
                throw;
            }
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|", (object)this.RUCEmisor, (object)this.TipoComprobante, (object)this.Numero_serie, (object)this.Numero_correlativo, (object)this.IGV, (object)this.Total, (object)this.FechaEmision, (object)this.TipoDocumentoReceptor, (object)this.numeroDocumentoReceptor); ;
        }
        #endregion
    }
}

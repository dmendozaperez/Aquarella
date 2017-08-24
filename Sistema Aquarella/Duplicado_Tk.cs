using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Variables;
using Impresora_Epson;
using Carvajal.FEPE.PreSC.Core;
using System.Xml;
namespace Sistema_Aquarella
{
    public partial class Duplicado_Tk : Form
    {
        public Duplicado_Tk()
        {
            InitializeComponent();
        }

        private void btnimprimir_Click(object sender, EventArgs e)
        {
            
            try
            {
                //Basico._enviar_webservice_xml();

                //System.IO.Ports.SerialPort port = new System.IO.Ports.SerialPort(("USB001", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                //port.Open();
                //ASCIIEncoding ascii = new ASCIIEncoding();
                //port.Write(ascii.GetString(new byte[] { 28, 112, 1, 0 })); //Printing the Above uploaded Logo
                //port.WriteLine("Your Text");
                //port.Close();

                //Carvajal.FEPE.PreSC.Configuration.CertificateConfiguration config = new Carvajal.FEPE.PreSC.Configuration.CertificateConfiguration();
                //config.CertificatePath = "C:\\certificado\\20101951872.pfx";
                //config.CertificatePwd = "BATAPERU1108";
                ////config.Thumbprint = "‎7b 24 0f 24 b9 c5 3a f8 e4 15 f5 15 a8 44 3f 77 99 46 05 5b";
                //config.Ruc = "20101951872";
                //config.IsCertificateFromFile = true;
                //string _txt = Facturacion_Electronica._leer_formato_electronico();

                //GeneratorCdp generatorCdp = new GeneratorCdp();

                ////Carvajal.FEPE.PreSC.Configuration.Configuration con = new Carvajal.FEPE.PreSC.Configuration.Configuration();
                ////con.AddCertificateConfiguration(config);

                ////Carvajal.FEPE.PreSC.Configuration. GetCertificateManager("C:\\certificado\\20101951872.pfx", "BATAPERU1108");                                
                //string hash = generatorCdp.GetHashForInvoiceCdp(_txt);    
                string _num_doc = "B03000000360";
                string _error = "";
                string _codigo_hash = "";
                Facturacion_Electronica.ejecutar_factura_electronica("B", _num_doc,ref _codigo_hash, ref _error);

                Facturacion_Electronica.insertar_codigo_hash(_num_doc, _codigo_hash, "V");
                Basico._enviar_webservice_xml();
                if (_error.Length==0)
                {
                    //Config_Imp_NC.GenerarTicketNC(_num_doc, 1, _codigo_hash);
                }
                
     
            }
            catch(Exception exc)
            {

            }

          
            //if (_codigo_hash=="error")
            //{
            //    MessageBox.Show("ERROR EN LA COMUNICACION DE DOCUMENTO A SUNAT . POR FAVOR CONSULTE CON SISTEMAS..", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //    else
            //{
            //    imprimir_copia(_codigo_hash);
            //}
            //Cursor.Current = Cursors.Default;
        }
        private void imprimir_copia(string _codigo_hash)
        {
            if (txtcomprobante.Text.Length == 0)
            {
                MessageBox.Show("Ingrese el numero de comprobante..", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtcomprobante.Focus();
                return;
            }


            //string tickets = Config_Imp.GenerarTicketFact(txtcomprobante.Text, 2,"");

            //string tickets = Config_Imp.GenerarTicketFact(txtcomprobante.Text, 1, _codigo_hash);

            //if (tickets == null)
            //{
            //    lblmensaje.Text = " >> Se producjo un error en la impresión del ticket";
            //}
            //else
            //{
            //    lblmensaje.Text = " > Ticket Generado con exito";
            //}              
        }
    }
}

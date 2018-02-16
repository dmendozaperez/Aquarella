using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Threading;
using CapaEntidad.Bll.Util;
using Integrado.Design.WPF_Master;
using Integrado.Bll;
using CapaDato.Bll.Control;
using System.IO;
using Epson_Ticket;
//using Epson_QR;
//using System.IO;
//using Epson_Ticket;
//using System.Drawing;
namespace Integrado.Design.WPF_Master
{
    /// <summary>
    /// Lógica de interacción para InicioWin.xaml
    /// </summary>
    public partial class InicioWin : MetroWindow
    {
        private string _server,_base_datos,_user,_password;
        public InicioWin()
        {
            InitializeComponent();

            lblhora.Content = DateTime.Now.ToLongTimeString();//Doy la hora actual al reloj

            //Actualiza reloj
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            DateTime myDt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);//obtiene datos fecha/hora
            string dtString = myDt.ToString(@"dd/MM/yyyy");//formato a entregar
            lblfecha.Content = dtString;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblhora.Content = DateTime.Now.ToLongTimeString();
        }
        private System.Drawing.Image byteArrayToImage(byte[] bytesArr)
        {
            MemoryStream memstr = new MemoryStream(bytesArr);
            System.Drawing.Image img = System.Drawing.Image.FromStream(memstr);
            return img;
        }
        private void btnaquarella_Click(object sender, RoutedEventArgs e)
        {
            _server = "www.aquarellaperu.com.pe";
            _base_datos = "BdAquarella";
            _user = "sa";
            _password = "Bata2013";
           

            inicia_modulo(_server, _base_datos, _user, _password);
            //StreamReader sr = new StreamReader("D:/10002.txt", Encoding.Default);
            //string _formato_doc = sr.ReadToEnd();
            //sr.Close();


            //string[] str2 = System.Text.RegularExpressions.Regex.Split(_formato_doc, "<td>");

            //RawPrinterHelper.SendStringToPrinter("aq", str2[0].ToString()); //Imprime texto.
            //CrearTicket tk = new CrearTicket();
            //Fe_Sunat_Qr fesunat_qr = new Fe_Sunat_Qr();

            //Carvajal.FEPE.PreSC.Core.GeneratorCdp generatorCdp = new Carvajal.FEPE.PreSC.Core.GeneratorCdp();
            //byte[]  qr = generatorCdp.GetImageQrCodeFromString(str2[1].ToString().Trim());

            //System.Drawing.Image im = byteArrayToImage(qr);
            //System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(im, new System.Drawing.Size(100, 100));
            //tk.HeaderImage = bmp;
            //tk.PrintQR("AQ");

            //string abrecaj = str2[2].ToString().Trim();

            //RawPrinterHelper.SendStringToPrinter("aq", str2[3].ToString());
            //tk.CortaTicket();

            //if (abrecaj == "1")
            //{
            //    RawPrinterHelper.SendStringToPrinter("aq", "\x1B" + "p" + "\x00" + "\x0F" + "\x96");
            //}

            //StreamReader srQR = new StreamReader(@"D:\INTERFA\CARVAJAL\F1021545.TXT", Encoding.Default);
            ////string _formato_doc_QR = srQR.ReadToEnd();
            ////sr.Close();

            //Carvajal.FEPE.PreSC.Core.GeneratorCdp generatorCdp = new Carvajal.FEPE.PreSC.Core.GeneratorCdp();
            //byte[] codigo_qr = generatorCdp.GetImageQrCodeFromString(textpipestr);

            //byte[] codigo_qr = fesunat_qr.GetQrSunatInvoiceCdp(_formato_doc_QR);

            //System.Drawing.Image im = byteArrayToImage(codigo_qr);
            //Bitmap bmp = new Bitmap(im, new System.Drawing.Size(100, 100));
            //tk.HeaderImage = bmp;
            //tk.PrintQR("AQ");

            //RawPrinterHelper.SendStringToPrinter("aq", str2[1].ToString());

            //Al cabar de imprimir limpia la linea de todo el texto agregado.

            //string _formato_doc = "";
            //StreamReader sr = new StreamReader(@"D:\F03000000053.txt", Encoding.Default);
            //_formato_doc = sr.ReadToEnd();
            //sr.Close();
            //Fe_Sunat_Qr qr = new Fe_Sunat_Qr();
            //qr.GetQrSunatNoteCdp(_formato_doc);

            ////Ticket ticket = new Ticket();
            ////ticket.TextoCentro("BATA");
            ////ticket.PrintTicket("AQ");
            //return;
            _referenciar_Base_Datos("AQ");
        }

        private void btnbata_Click(object sender, RoutedEventArgs e)
        {
            _server = "ecommerce.bgr.pe";
            _base_datos = "BD_ECOMMERCE";
            _user = "ecommerce";
            _password = "Bata2018.*@=?++";
            
            inicia_modulo(_server, _base_datos, _user, _password);
            
            _referenciar_Base_Datos("BA");





        }

        private void _referenciar_Base_Datos(string _base)
        {
            Ent_Global._canal_venta = _base;
            switch (_base)
            {
                case "AQ":
                    //Ent_Conexion._Base_Datos = "BdAQ";//"BdAquarella"; //"BdAQ";
                    Ent_Global._modulo_activo = _base;
                    Ent_Global._nom_modulo = "Sistema Aquarella";
                   
                    OpcionesMenu frm = new OpcionesMenu();                    
                    frm.Show();
                    this.Close();
                    break;
                case "BA":
                    Ent_Global._modulo_activo = _base;
                    Ent_Global._nom_modulo = "Sistema E-Commerce";
                    OpcionesMenu frmbata = new OpcionesMenu();
                    frmbata.Show();
                    this.Close();
                    break;
            }
        }

        private void inicia_modulo(string _server,string _base,string _user,string _password)
        {
            try
            {
                Ent_Conexion._Base_Datos = _base;
                Ent_Conexion._server = _server;
                Ent_Conexion._user = _user;
                Ent_Conexion._password = _password;
              
                string _error = "";
                Basico.copiar_archivo_config(ref _error);
                if (_error.Length > 0)
                {
                    MessageBox.Show(_error + "==>> CONSULTE CON SISTEMA ACERCA DEL ERROR....", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }



                //string _entorno = "AQHIG1";
                string _entorno = "";
                //Ent_Global._session_activa = true;
                //Ent_Usuario user = new Ent_Usuario();           
                //Ent_Global._usuario_var = "david";
                if (Environment.GetEnvironmentVariable("aq") != null)
                    _entorno = Environment.GetEnvironmentVariable("aq");


                Boolean inicio_config = Dat_Acceso.getpunto_vta(_entorno);

                if (!inicio_config)
                {
                    btnaquarella.IsEnabled = false;
                    lblconfig.Content = "!El Entorno del sistema no esta configurado correctamente";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {          
            btnaquarella.Focus();
        }
    }
}

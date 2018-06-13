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
using MahApps.Metro;
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
            Mouse.OverrideCursor = Cursors.Wait;
            //_server = "www.aquarellaperu.com.pe";
            _server = "10.10.10.206";
            _base_datos = "BdAquarellaDes4";
            _user = "sa";
            _password = "Bata2013";

            Boolean ini_mod = false;
            Ent_Global._canal_venta = "AQ";
            inicia_modulo(_server, _base_datos, _user, _password,ref ini_mod);

            if (ini_mod) _referenciar_Base_Datos("AQ");
            Mouse.OverrideCursor = null;
        }

        private void btnbata_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            _server = "ecommerce.bgr.pe";
            _base_datos = "BD_ECOMMERCE";
            _user = "ecommerce";
            _password = "Bata2018.*@=?++";

            Boolean ini_mod = false;

            Ent_Global._canal_venta = "BA";

            inicia_modulo(_server, _base_datos, _user, _password, ref ini_mod);

            if (ini_mod) _referenciar_Base_Datos("BA");

            //MahApps.Metro.Accent accent = new MahApps.Metro.Accent("Green", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Green.xaml", UriKind.RelativeOrAbsolute));
            //MahApps.Metro.AppTheme LightTheme = new MahApps.Metro.AppTheme("BaseLight", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml", UriKind.RelativeOrAbsolute));
            //MahApps.Metro.AppTheme DarkTheme = new MahApps.Metro.AppTheme("BaseDark", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseDark.xaml", UriKind.RelativeOrAbsolute));

            //ThemeManager.ChangeAppStyle(Application.Current, accent, DarkTheme);
            //        Dim accent As MahApps.Metro.Accent = New MahApps.Metro.Accent("Green", New Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Green.xaml", UriKind.RelativeOrAbsolute))
            //Dim LightTheme As MahApps.Metro.AppTheme = New MahApps.Metro.AppTheme("BaseLight", New Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml", UriKind.RelativeOrAbsolute))
            //Dim DarkTheme As MahApps.Metro.AppTheme = New MahApps.Metro.AppTheme("BaseDark", New Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseDark.xaml", UriKind.RelativeOrAbsolute))
            //If Me.DarkThemeSelect_check.IsChecked = True Then
            //    ThemeManager.ChangeAppStyle(Application.Current, accent, DarkTheme)
            //ElseIf Me.LightThemeSelect_check.IsChecked = True Then
            //    ThemeManager.ChangeAppStyle(Application.Current, accent, LightTheme)
            //End If




            Mouse.OverrideCursor = null;

        }
        /// <summary>
        /// Color de acento
        /// </summary>
        /// <param name="color"></param>
        private void accent_metro(string color="B")
        {
            try
            {
                Accent accent = null;
                if (color!="B")
                { 
                    accent = new Accent("Red", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Red.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    accent = new Accent("Blue", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/Blue.xaml", UriKind.RelativeOrAbsolute));
                }

                AppTheme LightTheme = new AppTheme("BaseLight", new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/BaseLight.xaml", UriKind.RelativeOrAbsolute));
                ThemeManager.ChangeAppStyle(Application.Current, accent, LightTheme);

            }
            catch (Exception)
            {

                
            }
        }
        private void _referenciar_Base_Datos(string _base)
        {
            Ent_Global._canal_venta = _base;
            switch (_base)
            {
                case "AQ":
                    //Ent_Conexion._Base_Datos = "BdAQ";//"BdAquarella"; //"BdAQ";

                    accent_metro();
                    Ent_Global._modulo_activo = _base;
                    Ent_Global._nom_modulo = "Sistema Aquarella";
                   
                    OpcionesMenu frm = new OpcionesMenu();                    
                    frm.Show();
                    this.Close();
                    break;
                case "BA":
                    accent_metro("R");

                    Ent_Global._modulo_activo = _base;
                    Ent_Global._nom_modulo = "Sistema E-Commerce";
                    OpcionesMenu frmbata = new OpcionesMenu();
                    frmbata.Show();
                    this.Close();
                    break;
            }
        }

        private void inicia_modulo(string _server,string _base,string _user,string _password,ref Boolean _ini)
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

                _ini = inicio_config;
                if (!inicio_config)
                {
                    
                    //btnaquarella.IsEnabled = false;
                    lblconfig.Content = "!El Entorno del sistema no esta configurado correctamente ó la conexion esta cerrada";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            accent_metro();
            btnaquarella.Focus();
        }
    }
}

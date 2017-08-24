using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Aquarella.bll;
using Aquarella.Form.Control;
using Aquarella.Form.Ventas;
namespace Aquarella
{
    /// <summary>
    /// Lógica de interacción para MainAQWindow.xaml
    /// </summary>
    public partial class MainAQWindow : Window
    {
        Usuario  _user;

        /// <summary>
        /// 
        /// </summary>
        DispatcherTimer timer;
        public MainAQWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_user == null)
                this.DisplayLoginScreen();
            else
                this.loadUserInSesion(_user);
            ///            
            DateTime date = DateTime.Now;
            TimeZone time = TimeZone.CurrentTimeZone;

            ///
            ///txtClock.Text = DateTime.Now.ToShortTimeString();
            ///
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1.0);
            timer.Start();
            /*
            timer.Tick += new EventHandler(delegate(object s, EventArgs a)
            {

                /*txtClock.Text = "" + DateTime.Now.Hour + ":"
              //+ DateTime.Now.Minute + ":"
              //+ DateTime.Now.Second;
                txtClock.Text = DateTime.Now.ToLongTimeString();
            });
            */
        }
        private void DisplayLoginScreen()
        {
            ///
            LoginForm frm = new LoginForm();
            ///
            frm.Owner = this;
            frm.ShowDialog();
            ///
            if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            {
                ///
                this.loadUserInSesion(frm.getUser());
            }
            else
                this.Close();
        }
        public void loadUserInSesion(Usuario user)
        {
            _user = user;
            ///                
            lblInfoUser.Text = "Usuario | " + _user._nombre;
            ///
            this.gDateTime.Visibility = Visibility.Visible;
        }
        private void btStartPack_Click(object sender, RoutedEventArgs e)
        {
            ///
            PanelLiquidationForInvoiceWindow plfiW = new PanelLiquidationForInvoiceWindow();
            ///
            plfiW.PanelLiquidationForInvoiceWindowStart(_user);
            ///
            plfiW.Show();
            ///
            this.Close();
        }

        private void btStartInvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnota_Click(object sender, RoutedEventArgs e)
        {
            NotaCredito nota = new NotaCredito();
            nota.NotaWindowStart(_user);
            nota.Show();
            this.Close();
        }

        private void btDuplicadoGuia_Click(object sender, RoutedEventArgs e)
        {
            ///
            //MainAQWindow maw = new MainAQWindow();
            /////
            //maw.Show();
            /////
            //this.Close();
        }

        private void btCloseSesion_Click(object sender, RoutedEventArgs e)
        {
            MainAQWindow maw = new MainAQWindow();
            ///
            maw.Show();
            ///
            this.Close();

        }

        private void btDuplicado_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

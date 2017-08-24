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

namespace Integrado.Design.WPF_Master
{
    /// <summary>
    /// Lógica de interacción para InicioWin.xaml
    /// </summary>
    public partial class InicioWin : MetroWindow
    {
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

        private void btnaquarella_Click(object sender, RoutedEventArgs e)
        {
            _referenciar_Base_Datos("AQ");
        }
        private void _referenciar_Base_Datos(string _base)
        {
            switch(_base)
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

                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Ent_Conexion._Base_Datos = "BdAquarella";//"BdAquarella"; //"BdAQ";
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
            btnaquarella.Focus();
        }
    }
}

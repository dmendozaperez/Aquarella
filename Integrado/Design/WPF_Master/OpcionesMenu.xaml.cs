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
using MahApps.Metro.Controls.Dialogs;
using Integrado.Design.Menu;
using CapaEntidad.Bll.Util;
using CapaEntidad.Bll.Control;
using System.Windows.Threading;
using Integrado.Design.Control;
using System.Threading.Tasks;
using System.Data;
using CapaDato.Bll.Control;
using Integrado.Sistemas.Logistica;
using Integrado.Sistemas.Ventas;
using CapaDato.Bll.Util;

namespace Integrado.Design.WPF_Master
{
    /// <summary>
    /// Lógica de interacción para OpcionesMenu.xaml
    /// </summary>
    public partial class OpcionesMenu : MetroWindow
    {

        
        private CustomDialog _customDialog;//declaracion de message
        private LoginForm _loginwindow;//declaracion de objeto form

       

        public OpcionesMenu()
        {
            InitializeComponent();
            //DataContext = new MainViewModel();
            //MainViewModel._principal = this;
            lblnom_modulo.Content = "{" + Ent_Global._nom_modulo.ToString() + "}";
            //lblusuario.Content = Ent_Usuario.var_usuario._usu_nombres;

            lblhora.Content = DateTime.Now.ToLongTimeString();//Doy la hora actual al reloj

            //Actualiza reloj
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            DateTime myDt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);//obtiene datos fecha/hora
            string dtString = myDt.ToString(@"dd/MM/yyyy");//formato a entregar
            lblfecha.Content = dtString;
            this.Title = "PAGINA PRINCIPAL [" +  Ent_Global._nom_modulo + "]";

            this.lblalmacen.Content = Ent_Global._pvt_nombre.ToString().ToUpper();


            if (Ent_Global._pvt_directo)
            {

                Dat_Basico.VerificaFechaServer_Cierre();
                lbliniciov.Visibility = Visibility.Visible;
                lblfinalv.Visibility = Visibility.Visible;
                btninicia.Visibility = Visibility.Visible;
                btnfinal.Visibility = Visibility.Visible;
                btnfinal.IsEnabled = false;

                //this.btndup.IsEnabled = false;
                this.lblfac.Content = "Facturacion";
                // this.btndup.Visibility =Visibility.Hidden;
                this.lbldupl.Content = "Consultas Cierres de Venta";
                //this.lin1.Width = 321;
                //this.lbldupl.Visibility = Visibility.Hidden;
                Dat_Basico.VerificaCierreVenta();
            }                

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblhora.Content = DateTime.Now.ToLongTimeString();
            if (Ent_Global._pvt_directo)
            {
                btninicia.IsEnabled = (Ent_Global._inicio_caja) ? false:true;
                btnfinal.IsEnabled = (Ent_Global._inicio_caja) ? true : false;
            }
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //Mat_Menu.Toggle();
        }
        private void limpiar()
        {
            lblusuario.Content = "";

        }
        private void inicio()
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            if (!(Ent_Global._session_activa))
            { 
                limpiar();
                inicio_windows();
            }
            else
            {
                Ent_Usuario user = Ent_Global._usuario_var;

                lblnombre_login.Content = "Usuario | " + user._nombre;
                lblusuario.Content = user._nombre;
            }
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            inicio();
            //Mat_Menu.Toggle();
        }
        private async void inicio_windows()
        {
            lblnombre_login.Content = "";
            lblusuario.Content = "";
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            _customDialog = new CustomDialog();
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                AnimateShow = true,
                NegativeButtonText = "Go away!",
                FirstAuxiliaryButtonText = "Cancel",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };
            _loginwindow = new LoginForm();
            _loginwindow.btncancelar.Click += btncancelarOnClick;
            _loginwindow.btniniciar.Click += btniniciarOnClick;
            _customDialog.Content = _loginwindow;
            await this.ShowMetroDialogAsync(_customDialog);

            _loginwindow.txtlogin.Focus();
        }
        private void btniniciarOnClick(object sender, RoutedEventArgs e)
        {
            //iniciando validacion
            //iniciar();
            Mouse.OverrideCursor = Cursors.Wait;
            iniciasession();
            Mouse.OverrideCursor = null;
        }
        async Task<Boolean> validainicio()
        {
            bool valida = true;
            var metroWindow = this;
            if (_loginwindow.txtlogin.Text.Length == 0)
            {
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Ingrese el usuario...", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                valida = false;
                _loginwindow.txtlogin.Focus();
                return valida;
            }
            if (_loginwindow.txtpassword.Password.Length == 0)
            {
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Ingrese la contraseña...", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                valida = false;
                _loginwindow.txtpassword.Focus();
                return valida;
            }
            return valida;
        }
        private  Ent_Usuario cargardatos(string nombre)
        {
            DataTable dtusuario = Dat_Acceso.F_LeerUsuario(nombre);
            if (dtusuario == null || dtusuario.Rows.Count <= 0)
            {
                return null;
            }
            DataRow dr = dtusuario.Rows[0];
            Ent_Global._bas_id_codigo = Convert.ToInt32(dr["bas_id"].ToString());
            Ent_Usuario  u = new Ent_Usuario
            {
                _bas_id = Convert.ToInt32(dr["bas_id"].ToString()),
                _usu_nombre = dr["usu_nombre"].ToString(),
                _usu_contraseña = dr["usu_contraseña"].ToString(),
                _usu_est_id = dr["usu_est_id"].ToString(),
                _nombre = dr["nombre"].ToString(),
                _usu_tip_id = dr["usu_tip_id"].ToString(),
                _usu_tip_nombre = dr["usu_tip_nombre"].ToString(),
                _usv_area = dr["bas_Are_id"].ToString(),
                _usn_userid = Convert.ToInt32(dr["bas_id"].ToString()),
                _usv_username = dr["usu_nombre"].ToString(),
                _usd_creation = System.DateTime.Parse(dr["usu_fecha_cre"].ToString()),              
                _usv_postpago = dr["postpago"].ToString()
            };

            return u;
        }
        private async void iniciasession()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            Ent_Global._usuario_var = null;
            try
            {
                bool result = await validainicio();
                if (!result) return;              
                string usuario =_loginwindow.txtlogin.Text;
                string contraseña =_loginwindow.txtpassword.Password;
                Ent_Usuario user = new Ent_Usuario();
                user = cargardatos(usuario);
                Ent_Global._usuario_var = user;
                if (user != null)
                {
                    if (user._usu_est_id.Equals(Ent_Constantes.IdStatusActive))
                    {
                        string con_usu =Ent_Cryptographic.decrypt(user._usu_contraseña);
                        if (contraseña.Equals(con_usu))
                        {
                            Ent_Global._session_activa = true;
                            lblnombre_login.Content= "Usuario | " + user._nombre;
                            lblusuario.Content= user._nombre;
                            //lblnombres.Text = "Usuario | " + user._nombre;
                            //activadesctiva(true);
                            await this.HideMetroDialogAsync(_customDialog);
                        }
                        else
                        {                            
                            await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Usuario y/o contraseña invalidos.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                            
                        }
                    }
                }
                else
                {
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Validaccion del usuario fallo.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);                    
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion,MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        private void btncancelarOnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btCloseSesion_Click(object sender, RoutedEventArgs e)
        {
            Ent_Global._session_activa = false;
            this.Close();
        }

        private async void btnsession_Click(object sender, RoutedEventArgs e)
        {
               var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
               };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Esta seguro de cerrar session!",
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);
            if (result == MessageDialogResult.Affirmative)
            {
                Ent_Global._session_activa = false;
                inicio_windows();
                //cerrar session
                //Modulos._session_activa = false;
                //Modulos frm = new Modulos();
                //frm.Show();                               
            }
        }

        private async void btnprincipal_Click(object sender, RoutedEventArgs e)
        {
               var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Esta seguro Volver a la ventana principal de Modulos!",
                MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, mySettings);
            if (result == MessageDialogResult.Affirmative)
            {
                Ent_Global._session_activa = false;
                InicioWin frm = new InicioWin();
                frm.Show();                
                this.Close();
               
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private async void btnfac_Click(object sender, RoutedEventArgs e)
        {
            if (!Ent_Global._pvt_directo)
            { 
                PanelPedidos frm = new PanelPedidos();
                frm.Show();
                this.Close();
            }
            else
            {
                if (Ent_Global._inicio_caja && Ent_Global._fecha_cierre_valida)
                { 
                    FacturacionDirecta frm = new FacturacionDirecta();
                    frm.Show();
                    this.Close();
                }
                else
                {
                    var metroWindow = this;
                    metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Debe de iniciar el dia de venta.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                }
            }
        }

        private void btndup_Click(object sender, RoutedEventArgs e)
        {
            if (!Ent_Global._pvt_directo)
            { 
                DuplicaGuia frm = new DuplicaGuia();
                frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                frm.Owner = this;
                frm.Show();
            }
            else
            {
                ConsultaCierre frm = new ConsultaCierre();
                frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                frm.Owner = this;
                frm.Show();
            }
        }

        private async void btnnc_Click(object sender, RoutedEventArgs e)
        {
          
            if (!Ent_Global._pvt_directo)
            {
                NotaCredito frm = new NotaCredito();
                frm.Show();
                this.Close();
            }
            else
            {
                if (Ent_Global._inicio_caja && Ent_Global._fecha_cierre_valida)
                {
                    NotaCredito frm = new NotaCredito();
                    frm.Show();
                    this.Close();
                }
                else
                {
                    var metroWindow = this;
                    metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Debe de iniciar el dia de venta.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                }
            }
               
        }

        private void btnconfac_Click(object sender, RoutedEventArgs e)
        {
            ConsultaFactura frm = new ConsultaFactura();
            frm.Show();
            this.Close();
        }

        private void btnconnc_Click(object sender, RoutedEventArgs e)
        {
            ConsultaNotaCredito frm = new ConsultaNotaCredito();
            frm.Show();
            this.Close();
        }

        private void btninicia_Click(object sender, RoutedEventArgs e)
        {
          
            InicioVenta frm = new InicioVenta();
            frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            frm.Owner = this;
            frm.Show();
                    
        }

        private void btnfinal_Click(object sender, RoutedEventArgs e)
        {

            FinalVenta frm = new FinalVenta();
            frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            frm.Owner = this;
            frm.Show();
        }
    }
}

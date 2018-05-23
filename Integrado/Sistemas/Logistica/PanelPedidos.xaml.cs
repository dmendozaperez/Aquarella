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
using MahApps.Metro.Controls.Dialogs;
using Integrado.Design.WPF_Master;
using System.Data;
using CapaDato.Bll.Logistica;
using Integrado.Sistemas.Ventas;
using Integrado.Prestashop;
using System.Threading.Tasks;

namespace Integrado.Sistemas.Logistica
{
    /// <summary>
    /// Lógica de interacción para PanelPedidos.xaml
    /// </summary>
    public partial class PanelPedidos : MetroWindow
    {
        DataTable dt;

        private string _liq { set; get; }
        private string _cliente { set; get; }
        public PanelPedidos()
        {
            InitializeComponent();
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

            Ent_Usuario user = Ent_Global._usuario_var;

            lblnombre_login.Content = "Usuario | " + user._nombre;
            lblusuario.Content = user._nombre;

            this.Title = "PAGINA DE PEDIDOS [" + Ent_Global._nom_modulo + "]";

            // Mostrar Boton, solo para Ecommerce (CHRISTIAN-TAWA)
            btnreenvio.Visibility = (Ent_Global._canal_venta == "AQ") ? Visibility.Hidden : Visibility.Visible;
            btnimprimirurbano.Visibility = (Ent_Global._canal_venta == "AQ") ? Visibility.Hidden : Visibility.Visible;

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblhora.Content = DateTime.Now.ToLongTimeString();
        }
        private async void btnsession_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Esta seguro de cerrar session!",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);
            if (result == MessageDialogResult.Affirmative)
            {
                //cerrar session
                Ent_Global._session_activa = false;
                OpcionesMenu frm = new OpcionesMenu();
                frm.Show();
                this.Close();
                //Modulos._session_activa = false;
                //Modulos frm = new Modulos();
                //frm.Show();                               
            }
        }

        private void btnprincipal_Click(object sender, RoutedEventArgs e)
        {
            Ent_Global._session_activa = true;
            OpcionesMenu frm = new OpcionesMenu();
            frm.Show();
            this.Close();
        }

        private void btCloseSesion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InicioWindows();
        }
        private void InicioWindows()
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            Integrado.Bll.Basico.cambio_img(imglogo);
            if (Ent_Global._canal_venta == "AQ")
            {
                refrescagrilla();
            }
            else
            {
                cargar_prestashop();
            }

            //refrescagrilla();
        }
        public void refrescagrilla()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {

                dg1.AutoGenerateColumns = false;
                dt = Dat_Liquidacion.liquidacionXfacturar();
                dg1.ItemsSource = dt.DefaultView;
                //modificacion Junior M. para Visualizar las operaciones Gratuitas

               

                //fin modificacion Junior 

                //grillaformato(dg1);
                totales(dt);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Mouse.OverrideCursor = null;
        }
        private void totales(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count == 0)
                {
                    lblnped.Content = "0";
                    lblcanemp.Content = "0";
                    lblcantot.Content = "0";
                    lblresemp.Content = "0";
                }
                else
                {
                    lblnped.Content = dt.Rows.Count.ToString("N0");
                    Int32 tpares = 0; Int32 temp = 0;
                    for (Int32 i = 0; dt.Rows.Count > i; ++i)
                    {
                        tpares += Convert.ToInt32(dt.Rows[i]["tpares"]);
                        temp += Convert.ToInt32(dt.Rows[i]["cantidadp"]);
                    }
                    lblcantot.Content = tpares.ToString("N0");
                    lblcanemp.Content = temp.ToString("N0");
                    lblresemp.Content = Convert.ToInt32((tpares - temp)).ToString("N0");
                }
            }
            else
            {
                lblnped.Content = "0";
                lblcanemp.Content = "0";
                lblcantot.Content = "0";
                lblresemp.Content = "0";
            }
        }

        private void btnrefresh_Click(object sender, RoutedEventArgs e)
        {
            if (Ent_Global._canal_venta == "AQ")
            {
                refrescagrilla();
            }
            else
            {
                cargar_prestashop();
            }

        }

        public async void refrescagrilla_prestashop()
        {
            try
            {

                //dg1.AutoGenerateColumns = false;
                dt = await Task.Run(() => Dat_Liquidacion.liquidacionXfacturar());

                dg1.ItemsSource = dt.DefaultView;

                totales(dt);
            }
            catch (Exception exc)
            {

            }

        }
        private async void cargar_prestashop()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            LeerPedidos carga_data = null;
            try
            {
                dg1.Columns[13].Visibility = Visibility.Collapsed;

                carga_data = new LeerPedidos();

                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Espere un momento por favor, cargando pedidos");  //show message
                ProgressAlert.SetIndeterminate();
                string _cargar_data = await Task.Run(() => carga_data.ImportaDataPrestaShop());
                if (_cargar_data.Length == 0)
                {
                    //await Task.Run(() => refrescagrilla_prestashop());
                    dt = await Task.Run(() => Dat_Liquidacion.liquidacionXfacturar());
                    await ProgressAlert.CloseAsync();
                    dg1.AutoGenerateColumns = false;
                    dg1.ItemsSource = dt.DefaultView;
                    totales(dt);
                }
                else
                {

                    dt = await Task.Run(() => Dat_Liquidacion.liquidacionXfacturar());
                    //await ProgressAlert.CloseAsync();
                    dg1.AutoGenerateColumns = false;
                    dg1.ItemsSource = dt.DefaultView;
                    totales(dt);

                    await ProgressAlert.CloseAsync();
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA IMPORTACION DE DATOS.. POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _cargar_data + ")", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);


                }

            }
            catch (Exception exc)
            {
                await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA IMPORTACION DE DATOS.. POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + exc.Message + ")", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                //throw;
            }
        }
        private void txtbuscar_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string fieldName = string.Concat("[", dt.Columns["Liq_Id"].ColumnName, "]");
                dt.DefaultView.Sort = fieldName;
                DataView view = dt.DefaultView;
                view.RowFilter = string.Empty;
                if (txtbuscar.Text != string.Empty)
                    view.RowFilter = fieldName + " LIKE '%" + txtbuscar.Text + "%'";
                dg1.ItemsSource = view;
            }
            catch
            {

            }
        }

        private void btnguia_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = this;
            metroWindow.LeftWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
            metroWindow.RightWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
            metroWindow.WindowButtonCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
            metroWindow.IconOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;

            //capturar fila seleccionada
            DataRowView row = (DataRowView)((Button)e.Source).DataContext;
            _liq = (string)row["Liq_Id"];
            _cliente = (string)row["nombres"];
            load_configurar_guia();
            this.ToggleFlyout(0, "Configuracion de Guia y Transporte");
        }
        private void load_configurar_guia()
        {
            defecto();
        }
        private void defecto()
        {
            try
            {
                lblmsg.Content = "";
                llenarcombo();
                cbotransportadora.SelectedIndex = -1;
                lblliq.Content = _liq;
                lblcliente.Content = _cliente;
                txtguia.Text = Dat_ConfigGuia.guiasecuencia();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void llenarcombo()
        {
            cbotransportadora.ItemsSource = Dat_ConfigGuia.leertrasnportador().DefaultView;
            cbotransportadora.DisplayMemberPath = "Tra_Descripcion";
            cbotransportadora.SelectedValuePath = "Tra_id";
        }
        private void ToggleFlyout(int index, string _header)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }
            flyout.Header = _header;
            flyout.IsOpen = !flyout.IsOpen;
        }

        private void btncancelarguia_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleFlyout(0, "Configuracion de Guia y Transporte");
        }

        private void btnaceptarguia_Click(object sender, RoutedEventArgs e)
        {
            aceptar_guia();
        }
        private void aceptar_guia()
        {
            try
            {
                if (!(fvalida())) return;
                Int32 _valida_guia;
                Int32 _idtrans = Convert.ToInt32(cbotransportadora.SelectedValue);
                string _gui_no = txtguia.Text;
                Mouse.OverrideCursor = Cursors.Wait;
                Dat_ConfigGuia.insertar_guia(_gui_no, _idtrans, _liq, out _valida_guia);

                if (_valida_guia == 0)
                {

                    refrescagrilla();
                    this.ToggleFlyout(0, "Configuracion de Guia y Transporte");

                }
                else
                {
                    lblmsg.Content = "Es probable que la guia ya exista, por favor dígite un nuevo numero de guia.";
                    txtguia.Focus();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Mouse.OverrideCursor = null;
        }
        private Boolean fvalida()
        {
            Boolean valida = true;
            lblmsg.Content = "";
            if (Convert.ToString(cbotransportadora.SelectedValue) == "")
            {
                lblmsg.Content = "Por Favor, seleccione la transportadora";
                valida = false;
                cbotransportadora.Focus();
                return valida; ;
            }
            if (txtguia.Text.Length == 0)
            {
                lblmsg.Content = "Por favor dígite el numero de guia.";
                valida = false;
                txtguia.Focus();
                return valida;
            }



            return valida;
        }

        private async void btnfacturar_Click(object sender, RoutedEventArgs e)
        {


            //var metroWindow = this;
            //metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            //ProgressDialogController ProgressAlert = null;

            //string msj_eccomer = "sssssss  \n" + "xxxxx";
            ////await ProgressAlert.CloseAsync();
            //await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, msj_eccomer, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

            //return;

            DataRowView row = (DataRowView)((Button)e.Source).DataContext;

            string _guia_no = (string)row["tra_gui_no"].ToString();
            string _liq_no = (string)row["Liq_Id"];
            string _cliente_name = (string)row["nombres"];

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "¿Realmente desea iniciar el empacado de la liquidación No. : " + _liq_no + " perteneciente al cliente : " + _cliente_name,
                MessageDialogStyle.AffirmativeAndNegative, mySettings);
            if (result == MessageDialogResult.Affirmative)
            {
                Facturacion._liq_id = _liq_no;
                Facturacion._liq_fecha = (string)row["Liq_FechaIng"].ToString();
                Facturacion._guia_no = (string)row["tra_gui_no"].ToString();
                Facturacion._transportadora = (string)row["tra_descripcion"].ToString();
                Facturacion._cli_id = (string)row["Liq_BasId"].ToString();
                Facturacion._cli_doc = (string)row["Bas_Documento"].ToString();
                Facturacion._cli_nombre = (string)row["nombres"].ToString();
                Facturacion._cli_direccion = (string)row["Bas_Direccion"].ToString();
                Facturacion._cli_ciudad = (string)row["ubicacion"].ToString();
                Int32 cantida_liq = Convert.ToInt32(row["tpares"]);
                Facturacion._liq_cliente = "(" + _liq_no + " | Cliente: " + (string)row["nombres"].ToString() + ")";
                Facturacion._liq_cantidad = cantida_liq;
                Facturacion frm = new Facturacion();
                frm.Show();
                this.Close();

            }

        }

        private void MetroWindow_Activated(object sender, EventArgs e)
        {

        }

        private void btnreenvio_Click(object sender, RoutedEventArgs e)
        {
            if (!Ent_Global._pvt_directo)
            {
                ConsultaUrbano frm = new ConsultaUrbano();
                frm.Show();
                this.Close();
            }
        }

        private void btnimprimirurbano_Click(object sender, RoutedEventArgs e)
        {
            if (!Ent_Global._pvt_directo)
            {
                Window win = null;

                foreach (Window openWin in System.Windows.Application.Current.Windows)
                {
                    if (openWin.GetType() == typeof(ImprimeUrbano))
                    {
                        win = openWin;
                    }
                }

                if (win != null)
                {
                    win.Focus();
                }
                else {
                    ImprimeUrbano frm = new ImprimeUrbano();
                    frm.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
                    frm.Owner = this;
                    frm.Show();
                }

            }
        }
    }
}

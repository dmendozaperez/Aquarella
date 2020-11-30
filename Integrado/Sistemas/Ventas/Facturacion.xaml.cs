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
using CapaEntidad.Bll.Util;
using System.Windows.Threading;
using Integrado.Sistemas.Logistica;
using MahApps.Metro.Controls.Dialogs;
using Integrado.Design.WPF_Master;
using CapaDato.Bll.Venta;
using System.Data;
using System.Threading.Tasks;
using Integrado.Bll;
using Epson_Ticket;
using Integrado.Prestashop;
using Integrado.Urbano;

namespace Integrado.Sistemas.Ventas
{
    /// <summary>
    /// Lógica de interacción para Facturacion.xaml
    /// </summary>
    public partial class Facturacion : MetroWindow
    {
        #region<VARIABLES>
        public static string _liq_id { set; get; }
        public static string _liq_fecha { set; get; }
        public static string _guia_no { set; get; }
        public static string _transportadora { set; get; }

        public static string _cli_id { set; get; }
        public static string _cli_doc { set; get; }
        public static string _cli_nombre { set; get; }
        public static string _cli_direccion { set; get; }
        public static string _cli_ciudad { set; get; }

        public static string _liq_cliente { set; get; }

        public static Int32 _liq_cantidad { set; get; }

        private Decimal _paq_id = 0;
        private decimal _paq_no = 0;
        #endregion
        public Facturacion()
        {
            InitializeComponent();
            lblnom_modulo.Content = "{" + Ent_Global._nom_modulo.ToString() + "}";
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

            this.Title = "PAGINA DE FACTURACION [" + Ent_Global._nom_modulo + "]";
            if (!Ent_Global._err_con_mysql) lblconexion_presta.Visibility = Visibility.Hidden;
            this.DataContext = this;
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblhora.Content = DateTime.Now.ToLongTimeString();
        }
        private void btCloseSesion_Click(object sender, RoutedEventArgs e)
        {
            PanelPedidos frm = new PanelPedidos();
            frm.Show();
            this.Close();
        }

        private void btnprincipal_Click(object sender, RoutedEventArgs e)
        {
            PanelPedidos frm = new PanelPedidos();
            frm.Show();
            this.Close();
        }
        private void InicioWindows()
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            Integrado.Bll.Basico.cambio_img(imglogo);
            Mouse.OverrideCursor = Cursors.Wait;
            chkbarra.IsChecked = true;
            determina_paquete();
            crear_tabla();
            cargar_grilla_packing();
            lblcanliq.Content = _liq_cantidad.ToString();
            txtarticulo.Focus();
            Mouse.OverrideCursor = null;
        }
        private void crear_tabla()
        {
            DataTable dt = new DataTable();

            dg1.ItemsSource = dt.DefaultView;
        }
        private void cargar_grilla_packing()
        {

            DataTable dt = Dat_Venta.leerarticulopaqliq(_liq_id);
            dg3.ItemsSource = dt.DefaultView;
            calculartotal();

        }
        private void calculartotal()
        {

            DataTable dt = ((DataView)dg1.ItemsSource).ToTable();
            Int32 paquete_actual = 0;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dt.Rows.Count; ++i)
                    {

                        paquete_actual += Convert.ToInt32(dt.Rows[i]["cant_paq"].ToString());
                    }
                }
            }

            dt = null;
            dt = ((DataView)dg3.ItemsSource).ToTable();
            Int32 paquete_empacados = 0;
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (Int32 i = 0; i < dt.Rows.Count; ++i)
                    {

                        paquete_empacados += Convert.ToInt32(dt.Rows[i]["paq_cantidad"].ToString());
                    }
                }
            }

            lblcantidadpa.Content = paquete_actual.ToString();
            lblcantidade.Content = paquete_empacados.ToString();
            lblcantidadre.Content = (_liq_cantidad - paquete_empacados).ToString();
        }
        private void determina_paquete()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                _paq_id = Dat_Venta.insertar_leer_paquete(_liq_id);

                if (_paq_id == -2)
                {
                    txtarticulo.IsEnabled = false;
                    btnpaquete.IsEnabled = false;
                    btnbuscar.IsEnabled = false;
                }


                _paq_no = Dat_Venta.leer_maxnopaqliq(_liq_id);
                lblnpaquete.Content = _paq_no.ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Mouse.OverrideCursor = null;
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InicioWindows();
        }

        private void btninfo_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = this;
            metroWindow.LeftWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
            metroWindow.RightWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
            metroWindow.WindowButtonCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
            metroWindow.IconOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
            this.ToggleFlyout(0, "Informacion Adicional sobre el pedido");
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

        private void txtarticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                pnlarticulo.Background = Brushes.Yellow;
                lblmensaje.Content = "";
                if (chkbarra.IsChecked.Value)
                {
                    paqartcodbarra();
                }
                else
                {
                }
                txtarticulo.Focus();
                Mouse.OverrideCursor = null;
            }
        }
        private async void paqartcodbarra()
        {
            try
            {
                var metroWindow = this;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                if (txtarticulo.Text.Trim().Length == 0)
                {
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Ingrese el codigo de articulo...", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                    pnlarticulo.Background = Brushes.Red;
                    txtarticulo.Focus();
                    return;
                }

                //en este caso vamos a digitar la calidad
                //string v_articulo = txtarticulo.Text.Substring(0,txtarticulo.Text.Length-1);

                string v_articulo = txtarticulo.Text.Trim();
                //
                string _barra = (txtarticulo.Text.Trim().Length == 18 || txtarticulo.Text.Trim().Length == 13) ? txtarticulo.Text.Trim() : "";


                string[] info_articulo = Ent_BarCodes.getInfoFromTheBarCode(v_articulo);
                if (info_articulo != null && info_articulo.Length > 0)
                {
                    String sizeToAdd = info_articulo[1];
                    /// Article 
                    String articleToAdd = info_articulo[0];

                    String calidadToAdd = info_articulo[2];

                    string varreturn = Dat_Venta.insertar_articulopaq(_paq_id, _liq_id, articleToAdd, sizeToAdd, 1, calidadToAdd, _barra);

                    if (varreturn.Equals("1"))
                    {
                        cargar_grilla();
                        pnlarticulo.Background = Brushes.YellowGreen;
                        lblmensaje.Content = " > Artículo " + articleToAdd + " adicionado correctamente.";
                        txtarticulo.Text = "";
                        txtarticulo.Focus();
                    }
                    else
                    {

                        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "El Código Leído ( " + txtarticulo.Text + " ) no Corresponde a un Artículo en el Pedido o ya Ha Sido Empacado en Su Totalidad.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                        pnlarticulo.Background = Brushes.Red;
                        txtarticulo.Focus();
                    }

                }
                else
                {
                    lblmensaje.Foreground = Brushes.Maroon;
                    lblmensaje.Content = " > Articulo " + txtarticulo.Text + " desconocido o codigo de barras incorrecto !!.";
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Articulo desconocido o codigo de barras incorrecto !!.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                    pnlarticulo.Background = Brushes.Red;
                    txtarticulo.Focus();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void cargar_grilla()
        {
            cargar_grilla_packing();
            cargar_grilla_actualpacking();
        }
        private void cargar_grilla_actualpacking()
        {

            DataTable dt = Dat_Venta.leer_articulopacking_paquete(_liq_id, _paq_id);
            dg1.ItemsSource = dt.DefaultView;
            calculartotal();
        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            if (chkbarra.IsChecked.Value)
            {
                txtarticulo_KeyDown(btnbuscar, new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, (Key.Enter)));
            }
            else
            {
                dg2.ItemsSource = Dat_Venta.Datos_art_tallaemp(_liq_id, txtarticulo.Text).DefaultView;
            }
            Mouse.OverrideCursor = null;
        }

        private async void btneliminar_Click(object sender, RoutedEventArgs e)
        {
            if (!(txtarticulo.IsEnabled)) return;

            DataRowView row = (DataRowView)((Button)e.Source).DataContext;

            string articulo = (string)row["Art_Id"].ToString();
            string marca = (string)row["Mar_Descripcion"].ToString();
            string talla = (string)row["Liq_Det_TalId"].ToString();

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "¿Realmente desea eliminar el artículo : " + articulo + " en marca : " +
                   marca + "?",
              MessageDialogStyle.AffirmativeAndNegative, mySettings);

            String msge = " > El artículo : " + articulo + " en marca : " + marca + " ha sido eliminado.";
            if (result == MessageDialogResult.Affirmative)
            {
                string respuesta = Dat_Venta.borrar_lineapaquete(_paq_id, articulo, talla);
                if (respuesta.Equals("1"))
                {
                    cargar_grilla();
                }
                lblmensaje.Content = msge;
            }

        }

        private void btnpaquete_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            determina_paquete();
            if (!(txtarticulo.IsEnabled)) cargar_grilla_actualpacking();
            lblmensaje.Content = " > Nuevo paquete creado.";
            txtarticulo.Select(0, txtarticulo.Text.Length);
            txtarticulo.Focus();
            Mouse.OverrideCursor = null;
        }

        private void btnfacturar_Click(object sender, RoutedEventArgs e)
        {
            //Mouse.OverrideCursor = Cursors.Wait;            
            facturar();
            //Mouse.OverrideCursor = null;
        }
        async Task<Int32> valida_facturar()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            Int32 validaf = 0;
            if (_guia_no.Length == 0)
            {
                lblmensaje.Content = " > Por favor configure su guia de remision.";

                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Configure su numero de guia de remision.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);


                validaf = 1;
                return validaf;
            }



            DataTable dt = ((DataView)dg3.ItemsSource).ToTable();
            if (dt.Rows.Count == 0)
            {

                ///                 
                lblmensaje.Content = " > No puede facturar un pedido sin ningun artículo empacado.";

                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "No puede facturar un pedido sin ningun artículo empacado.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                ///               
                validaf = 1;
                return validaf;
            }

            if (Ent_Global._canal_venta != "AQ")
            {
                decimal saldo = 0;
                decimal despacho = Convert.ToDecimal(lblcantidade.Content);

                saldo = (_liq_cantidad - despacho);

                if (saldo != 0)
                {
                    lblmensaje.Content = " > Tiene que despachar el total de articulos del pedido.";

                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Tiene que despachar el total de articulos del pedido.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);

                    ///               
                    validaf = 1;
                    return validaf;
                }
            }


            return validaf;
        }
        private void deshabilita_controles()
        {
            this.btnfacturar.IsEnabled = false;
            this.btnpaquete.IsEnabled = false;
            this.btnbuscar.IsEnabled = false;
            this.txtarticulo.IsEnabled = false;
        }
        private async void facturar()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {

                Int32 _valida = await valida_facturar();

                if (_valida == 1) return;



                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Si",
                    NegativeButtonText = "No",
                    //FirstAuxiliaryButtonText = "Cancelar",
                    ColorScheme = MetroDialogOptions.ColorScheme,
                };


                var okSettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Ok",
                    ColorScheme = MetroDialogOptions.ColorScheme,
                };



                MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "¿Realmente desea FACTURAR este pedido ? " + _liq_id,
              MessageDialogStyle.AffirmativeAndNegative, mySettings);


                if (result == MessageDialogResult.Affirmative)
                {
                    string _error_venta = "";
                    //Mouse.OverrideCursor = Cursors.Wait;
                    ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Generando Facturacion Electronica del pedido N°:" + _liq_id);  //show message
                    ProgressAlert.SetIndeterminate();
                    string grabar_numerodoc = await Task.Run(() => Dat_Venta.insertar_venta(_liq_id, ref _error_venta));
                    //string grabar_numerodoc = Dat_Venta.insertar_venta(_liq_id);

                    if (grabar_numerodoc != "-1")
                    {
                        lblmensaje.Content = " > Factura generada con exito - Número : " + grabar_numerodoc + ".";
                        ///
                        deshabilita_controles();

                        //aca generamos el codigo hash
                        string _codigo_hash = "";
                        string _error = "";
                        string _url_pdf = "";

                        await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica(Basico.Left(grabar_numerodoc, 1), grabar_numerodoc, ref _codigo_hash, ref _error,ref _url_pdf));

                        //await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica_ws (Basico.Left(grabar_numerodoc, 1), grabar_numerodoc, ref _codigo_hash, ref _error,ref _url_pdf));

                        //*************




                        //****enviar los xml al server

                        if (_codigo_hash.Length == 0 || _codigo_hash == null)
                        {
                            await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica(Basico.Left(grabar_numerodoc, 1), grabar_numerodoc, ref _codigo_hash, ref _error,ref _url_pdf));
                        }
                        if (_codigo_hash.Length == 0 || _codigo_hash == null)
                        {
                            _error = "GENERACION DE HASH";
                            await ProgressAlert.CloseAsync();
                            await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                            //MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Ent_Msg.msginfomacion,MessageBoxButton.OK,MessageBoxImage.Error);
                            return;
                        }
                        
                        if (_error.Length > 0)
                        {
                            await ProgressAlert.CloseAsync();
                            await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                            //MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Ent_Msg.msginfomacion,MessageBoxButton.OK,MessageBoxImage.Error);
                            return;
                        }
                        await Task.Run(() => Basico._enviar_webservice_xml());
                        //EN ESTE PASO VAMOS A GRABAR EL CODIGO HASH
                        await Task.Run(() => Dat_Venta.insertar_codigo_hash(grabar_numerodoc, _codigo_hash, "V",_url_pdf));
                        ///
                        //byte[] img_qr = null;
                        string _genera_tk = await Task.Run(() => Imprimir_Doc.Generar_Impresion("F", grabar_numerodoc) /*Impresora_Epson.Config_Imp.GenerarTicketFact(grabar_numerodoc, 1, _codigo_hash)*/);

                        /*IMPRESION DE ETIQUETAS*/
                        if (Ent_Global._canal_venta == "AQ")
                        {
                            GenerarEtiqueta genera_etiqueta = new GenerarEtiqueta();
                            await Task.Run(() => genera_etiqueta.aq_imp_etiqueta2(grabar_numerodoc));
                        }

                            #region<SOLO PARA E-CCOMMERCE>

                            if (Ent_Global._canal_venta == "BA")
                        {
                            string _cod_urbano = "";
                            await Task.Run(() => Basico.act_presta_urbano(grabar_numerodoc, ref _error, ref _cod_urbano));
                            //string mensaje_urb = (_cod_urbano.Trim().Length == 0) ? "" : "Se envío correctamente la solicitud a Urbano, Nro guía obtenida: " + _cod_urbano + "\n\n";
                            await ProgressAlert.CloseAsync();
                            string msj_eccomer = "";

                            /*si el codigo de urbano esta null entonces no va el mensaje*/
                            if (_cod_urbano.Trim().Length > 0)
                            {
                                msj_eccomer = "Se Genero correctamente la factura nro: " + grabar_numerodoc + "\n"
                                                   + "Se envío correctamente la solicitud al Courier, Nro guía obtenida: " + _cod_urbano + "\n\n"
                                                   + "¿Desea imprimir la etiqueta de este pedido? " + _liq_id;

                                MessageDialogResult resultetiq = await this.ShowMessageAsync(Ent_Msg.msginfomacion, msj_eccomer, MessageDialogStyle.AffirmativeAndNegative, mySettings);

                                if (resultetiq == MessageDialogResult.Affirmative)
                                {
                                    ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Generando Facturacion Electronica del pedido N°:" + _liq_id);  //show message
                                    ProgressAlert.SetIndeterminate();
                                    /*FALTA PONER LA VALIDACION DE LA ETIQUETA*/
                                    //resultetiq
                                    GenerarEtiqueta genera_etiqueta = new GenerarEtiqueta();
                                    await Task.Run(() => genera_etiqueta.imp_etiqueta2(grabar_numerodoc));
                                }
                                else
                                {
                                    ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Generando Facturacion Electronica del pedido N°:" + _liq_id);  //show message
                                    ProgressAlert.SetIndeterminate();
                                }
                                // await ProgressAlert.CloseAsync();
                                
                            }
                            else
                            {
                                msj_eccomer = "Se Genero correctamente la factura nro: " + grabar_numerodoc + "\n"
                                                   + "No se pudo enviar la solicitud a Courier.\n\n";
                                MessageDialogResult resultetiq = await this.ShowMessageAsync(Ent_Msg.msginfomacion, msj_eccomer, MessageDialogStyle.Affirmative, okSettings);
                            }

                            //await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, msj_eccomer, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                        }
                        #endregion

                        //string _genera_tk = Impresora_Epson.Config_Imp.GenerarTicketFact(grabar_numerodoc, 1, _codigo_hash);

                        if (_genera_tk == null)
                        {
                            lbltickets.Content = " >> Se producjo un error en la impresion del ticket";
                        }
                        else
                        {
                            lbltickets.Content = " > Ticket Generado con exito";
                        }
                        Reporte_Guia_Remision._idv_invoice = grabar_numerodoc;
                        Reporte_Guia_Remision form = new Reporte_Guia_Remision();
                        form.Show();

                        if (ProgressAlert.IsOpen)
                            await ProgressAlert.CloseAsync();

                    }
                    else
                    {

                        deshabilita_controles();
                        lblmensaje.Foreground = Brushes.Maroon;
                        /// 
                        //lblmensaje.Content = " > Ha ocurrido un problema y no se ha podido generar la factura.";
                        lblmensaje.Content = _error_venta;
                        ///
                        await ProgressAlert.CloseAsync();
                        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, _error_venta, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);


                    }
                }
            }
            catch (Exception exc)
            {


                deshabilita_controles();
                lblmensaje.Foreground = Brushes.Maroon;
                /// 
                lblmensaje.Content = exc.Message;
                ///
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
            //Mouse.OverrideCursor = null;
        }
    }
}

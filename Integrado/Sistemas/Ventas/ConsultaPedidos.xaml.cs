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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Integrado.Design.WPF_Master;
using CapaEntidad.Bll.Util;
using System.Windows.Threading;
using System.Data;
using CapaDato.Bll.Venta;
using Epson_Ticket;
using Integrado.Bll;
using Integrado.Prestashop;
using CapaDato.Bll.Logistica;
using Integrado.Sistemas.Logistica;

namespace Integrado.Sistemas.Ventas
{
    /// <summary>
    /// Lógica de interacción para ConsultaPedido.xaml
    /// </summary>
    public partial class ConsultaPedido : MetroWindow
    {
        DataTable dt;
        public ConsultaPedido()
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

            this.Title = "PAGINA DE CONSULTA PEDIDOS POR FACTURAR [" + Ent_Global._nom_modulo + "]";

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblhora.Content = DateTime.Now.ToLongTimeString();
        }

        /// <summary>
        /// Metodo que actualiza y obtiene pedidos desde prestashop
        /// </summary>
        private async void cargar_prestashop()
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            LeerPedidos carga_data = null;
            try
            {
                carga_data = new LeerPedidos();

                ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Espere un momento por favor, cargando pedidos");  //show message
                ProgressAlert.SetIndeterminate();
                string _cargar_data = await Task.Run(() => carga_data.ImportaDataPrestaShop());
                if (_cargar_data.Length == 0)
                {
                    //dt = await Task.Run(() => Dat_Liquidacion.liquidacionXfacturar());
                    await ProgressAlert.CloseAsync();
                    //dg1.AutoGenerateColumns = false;
                    //dg1.ItemsSource = dt.DefaultView;
                    //totales(dt);
                }
                else
                {
                    //dt = await Task.Run(() => Dat_Liquidacion.liquidacionXfacturar());
                    //dg1.AutoGenerateColumns = false;
                    //dg1.ItemsSource = dt.DefaultView;
                    //totales(dt);

                    await ProgressAlert.CloseAsync();
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA IMPORTACION DE DATOS.. POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _cargar_data + ")", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                    
                }

            }
            catch (Exception exc)
            {
                await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA IMPORTACION DE DATOS.. POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + exc.Message + ")", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
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
            Inicio_Windows();
        }
        private void Inicio_Windows()
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            Integrado.Bll.Basico.cambio_img(imglogo);
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpdesde.Text = fecha.ToString();
            dtphasta.Text = DateTime.Today.ToString();

            txtdoc.IsEnabled = false;
            chkactivar.IsChecked = false;
            //chkactivar .Checked = false;
            consultar();

            // obtiene datos de prestashop antes de iniciar el reporte
            cargar_prestashop();
        }

        private void chkactivar_Checked(object sender, RoutedEventArgs e)
        {
           
        }
        private async void consultar()
        {
            Mouse.OverrideCursor=Cursors.Wait;
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            try
            {
                Boolean _tipo = chkactivar.IsChecked.Value;

                if (_tipo)
                {
                    if (txtdoc.Text.Length == 0)
                    {
                        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Por favor ingrese el numero de documento a consultar", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                        Mouse.OverrideCursor = null;
                        txtdoc.Focus();
                        return;
                    }
                }

                DateTime _fechaini =Convert.ToDateTime(dtpdesde.Text);
                DateTime _fechafin = Convert.ToDateTime(dtphasta.Text);
                string _doc =txtdoc.Text;                
                dt = Dat_Venta.dt_consulta_pedido(_tipo, _fechaini, _fechafin, _doc);
                dg1.ItemsSource = dt.DefaultView;
               
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Mouse.OverrideCursor = null;
        }
       
        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            consultar();
        }

        private void txtdoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
                consultar();
        }

        private void chkactivar_Click(object sender, RoutedEventArgs e)
        {
            if (chkactivar.IsChecked.Value)
            {
                dtpdesde.IsEnabled = false;
                dtphasta.IsEnabled = false;
                txtdoc.IsEnabled = true;
                txtdoc.Focus();
            }
            else
            {
                txtdoc.Text = "";
                consultar();
                dtpdesde.IsEnabled = true;
                dtphasta.IsEnabled = true;
                txtdoc.IsEnabled = false;
            }
        }

        private void btnreimprimir_Click(object sender, RoutedEventArgs e)
        {

            Mouse.OverrideCursor = Cursors.Wait;

            DataRowView row = (DataRowView)((Button)e.Source).DataContext;
            
            string _numero = (string)row["id"].ToString();// dg1.Rows[e.RowIndex].Cells["tipodoc"].Value.ToString();


            vistaprevia(_numero);

            Mouse.OverrideCursor = null;

        }
        private void vistaprevia(string id)
        {
            if (valida_pedido(id) == 0)
            {
                return;
            }
            else
            {
                Reporte_Liquidacion._id_invoice = id.Trim();
                Reporte_Liquidacion frm = new Reporte_Liquidacion();
                frm.Show();
            }
        }

        private Int32 valida_pedido(string id)
        {
            return Dat_Venta.valida_pedido(id);
        }

        private async void btnanular_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)((Button)e.Source).DataContext;

            string _not_id = (string)row["ven_id"].ToString();// dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
            string _tipo = (string)row["tipodoc"].ToString();//dg1.Rows[e.RowIndex].Cells["tipodoc"].Value.ToString();
            string _numdoc = (string)row["numdoc"].ToString();//dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
            string _doc = (string)row["ven_id"].ToString();//dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
            Boolean _anulado = (Boolean)row["anulado"];// Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["anulado"].Value);
            //verificar si el documento paso las 72 horas de enviarse a la web service efact
            Boolean _valida = (Boolean)row["docu_vencido"];// Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["docu_vencido"].Value);


           

            //string _not_numero = dg1.Rows[e.RowIndex].Cells["not_numero"].Value.ToString();

            if (_anulado)
            {
                MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " no se puede anular, porque YA ESTA ANULADO...", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Exclamation);                
                return;
            }

            _valida = false;

            if (_valida)
            {
                MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " no se puede anular, porque no es de la fecha actual...", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Exclamation);                
                return;
            }

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "¿Realmente desea anular el documento de tipo : " + _tipo + " con numero : " + _numdoc,
               MessageDialogStyle.AffirmativeAndNegative, mySettings);


            if (result == MessageDialogResult.Affirmative)
            {
                //Mouse.OverrideCursor = Cursors.Wait;
                string _error = "";

                //Facturacion_Electronica.anular_facturacion_electronica(_doc, ref _error,"NC");


                //if (_error.Length == 0)
                //{
                    var ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Anulando Documento N°:" + _not_id);  //show message
                    ProgressAlert.SetIndeterminate(); //Infinite   

                string _error_venta = await Task.Run(() => Dat_Venta._anular_venta(_not_id.ToString()));
                //show info

                #region<REGION EXCLUSIVA PARA E-COMMERCE>
                if (Ent_Global._canal_venta == "BA")
                {
                    string _tipo_doc = "";
                    if(_tipo == "Factura") { _tipo_doc = "FA"; }
                    if(_tipo == "Boleta") { _tipo_doc = "BO"; }
                    string _error_act_venta = await Task.Run(() => Dat_Venta._act_estado_anular_venta(_tipo_doc.ToString(), _not_id.ToString()));
                }
                #endregion

                //string _error_venta =Dat_Venta._anular_venta(_not_id.ToString());
                string _codigo_hashn = "";

                    if (_error_venta.Length==0)
                       {                            
                            await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica("F", _not_id.ToString(), ref _codigo_hashn, ref _error));
                            
                            if (_error.Length == 0)
                            {
                                await Task.Run(() => Basico._enviar_webservice_xml());
                                Boolean _tipoc_b = chkactivar.IsChecked.Value;
                                DateTime _fechaini_b = Convert.ToDateTime(dtpdesde.Text);
                                DateTime _fechafin_b = Convert.ToDateTime(dtphasta.Text);
                                string _doc_b = txtdoc.Text;
                        //MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " Se Anulo con exito...", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Information);                        
                                DataTable dt= await Task.Run(() => Dat_Venta.dt_consulta_pedido(_tipoc_b, _fechaini_b, _fechafin_b, _doc_b));                                                                
                                await ProgressAlert.CloseAsync();
                                dg1.ItemsSource = dt.DefaultView;
                                await this.ShowMessageAsync(Ent_Msg.msginfomacion, "!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " Se Anulo con exito...");
                                
                            }
                            else
                            {
                               await ProgressAlert.CloseAsync();
                               await this.ShowMessageAsync(Ent_Msg.msginfomacion, _error);
                               //MessageBox.Show(_error, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                       }                       
                       else
                       {
                            await ProgressAlert.CloseAsync();
                            await this.ShowMessageAsync(Ent_Msg.msginfomacion, _error_venta);
                            //MessageBox.Show(_error_venta, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
                       }

        }
            //}
            //Mouse.OverrideCursor = null;
        }
    }
}

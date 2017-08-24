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
using System.Windows.Threading;
using CapaEntidad.Bll.Util;
using System.Data;
using Integrado.Design.WPF_Master;
using CapaDato.Bll.Venta;
using Epson_Ticket;
using Integrado.Bll;

namespace Integrado.Sistemas.Ventas
{
    /// <summary>
    /// Lógica de interacción para ConsultaNotaCredito.xaml
    /// </summary>
    public partial class ConsultaNotaCredito : MetroWindow
    {
        DataTable dt;
        public ConsultaNotaCredito()
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

            this.Title = "PAGINA DE CONSULTA NOTA DE CREDITO [" + Ent_Global._nom_modulo + "]";
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Inicio_Windows();
        }
        private void Inicio_Windows()
        {
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpdesde.Text = fecha.ToString();
            dtphasta.Text = DateTime.Today.ToString();

            txtdoc.IsEnabled = false;
            chkactivar.IsChecked = false;
            //chkactivar .Checked = false;
            consultar();
        }
        private  void consultar_refresh()
        {
            
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            try
            {
                Boolean _tipo = chkactivar.IsChecked.Value;

                //if (_tipo)
                //{
                //    if (txtdoc.Text.Length == 0)
                //    {
                //        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Por favor ingrese el numero de documento a consultar", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                //        Mouse.OverrideCursor = null;
                //        txtdoc.Focus();
                //        return;
                //    }
                //}

                DateTime _fechaini = Convert.ToDateTime(dtpdesde.Text);
                DateTime _fechafin = Convert.ToDateTime(dtphasta.Text);
                string _doc = txtdoc.Text;
                dt = Dat_NotaCredito.dt_consulta_notacredito(_tipo, _fechaini, _fechafin, _doc);
                dg1.ItemsSource = dt.DefaultView;

            }
            catch 
            {
               
            }
            
        }

        private async void consultar()
        {
            Mouse.OverrideCursor = Cursors.Wait;
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

                DateTime _fechaini = Convert.ToDateTime(dtpdesde.Text);
                DateTime _fechafin = Convert.ToDateTime(dtphasta.Text);
                string _doc = txtdoc.Text;
                dt = Dat_NotaCredito.dt_consulta_notacredito(_tipo, _fechaini, _fechafin, _doc);
                dg1.ItemsSource = dt.DefaultView;

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Mouse.OverrideCursor = null;
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

        private void chkactivar_Checked(object sender, RoutedEventArgs e)
        {

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

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            consultar();
        }

        private async void btnanular_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)((Button)e.Source).DataContext;

            decimal _not_id = Convert.ToDecimal(row["Not_Id"]);// dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
            string _tipo = (string)row["tipodoc"].ToString();//dg1.Rows[e.RowIndex].Cells["tipodoc"].Value.ToString();
            string _numdoc = (string)row["numdoc"].ToString();//dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
            //string _doc = (string)row["ven_id"].ToString();//dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
            Boolean _anulado = (Boolean)row["anulado"];// Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["anulado"].Value);
            //verificar si el documento paso las 72 horas de enviarse a la web service efact
            Boolean _valida = (Boolean)row["docu_vencido"];// Convert.ToBoolean(dg1.Rows[e.RowIndex].Cells["docu_vencido"].Value);

            string _not_numero = (string)row["not_numero"].ToString(); //dg1.Rows[e.RowIndex].Cells["not_numero"].Value.ToString();
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
                Mouse.OverrideCursor = Cursors.Wait;
                string _error = "";

                //Facturacion_Electronica.anular_facturacion_electronica(_doc, ref _error, "NC");


                //if (_error.Length == 0)
                //{
                    var ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Anulando Nota de Credito N°:" + _numdoc);  //show message
                    ProgressAlert.SetIndeterminate(); //Infinite   
                    string _error_venta = await Task.Run(() => Dat_NotaCredito._anular_ncredito(_not_id, Ent_Global._bas_id_codigo));
                //string _error_venta =Dat_NotaCredito._anular_ncredito(_not_id, Ent_Global._bas_id_codigo);
                    string _codigo_hashn = "";
                    
                if (_error_venta.Length==0)
                {

                    await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica("N", _not_id.ToString(), ref _codigo_hashn, ref _error));                    


                    if (_error.Length == 0)
                    {
                        await Task.Run(() => Basico._enviar_webservice_xml());
                        Boolean _tipoc_b = chkactivar.IsChecked.Value;
                        DateTime _fechaini_b = Convert.ToDateTime(dtpdesde.Text);
                        DateTime _fechafin_b = Convert.ToDateTime(dtphasta.Text);
                        string _doc_b = txtdoc.Text;
                        //MessageBox.Show("!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " Se Anulo con exito...", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Information);
                        //consultar();
                        DataTable dt = await Task.Run(() => Dat_NotaCredito.dt_consulta_notacredito(_tipoc_b, _fechaini_b, _fechafin_b, _doc_b));
                        await ProgressAlert.CloseAsync();
                        dg1.ItemsSource = dt.DefaultView;
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "!El documento de tipo : " + _tipo + " con numero : " + _numdoc + " Se Anulo con exito...");
                    }
                    else
                    {
                        await ProgressAlert.CloseAsync();
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, _error);
                        //MessageBox.Show(_error_venta, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    await ProgressAlert.CloseAsync();
                    await this.ShowMessageAsync(Ent_Msg.msginfomacion, _error_venta);
                }
                //}
                //else
                //{
                //    MessageBox.Show(_error, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
                //}

            }
            //}
            Mouse.OverrideCursor = null;
        }

        private async void btnreimprimir_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)((Button)e.Source).DataContext;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            //string _tipo_r = (string)row["tipodoc"].ToString();// dg1.Rows[e.RowIndex].Cells["tipodoc"].Value.ToString();
            //string _numdoc_r = (string)row["numdoc"].ToString();// dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString();
            //string _doc_r = (string)row["ven_id"].ToString();// dg1.Rows[e.RowIndex].Cells["ven_id"].Value.ToString();
            //string _cod_hash = (string)row["cod_hash"].ToString();// dg1.Rows[e.RowIndex].Cells["cod_hash"].Value.ToString();


            string _codigo_hash = (string)row["Not_Cod_Hash"].ToString();// dg1.Rows[e.RowIndex].Cells["Not_Cod_Hash"].Value.ToString(); ;
            decimal _not_id_imp = Convert.ToDecimal(row["Not_Id"]);// Convert.ToDecimal(dg1.Rows[e.RowIndex].Cells["Not_Id"].Value.ToString());
            string _numdoc_r = (string)row["numdoc"].ToString();// dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString(); ;


            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "¿Realmente desea REIMPRIMIR el documento de tipo : Nota de Credito  con numero : " + _numdoc_r,
               MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                //Mouse.OverrideCursor = Cursors.Wait;
                var ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Imprimiendo Documento");  //show message
                ProgressAlert.SetIndeterminate();
                string tickets = await Task.Run(() => Imprimir_Doc.Generar_Impresion("N", _not_id_imp.ToString()) /*Config_Imp_NC.GenerarTicketNC(_not_id_imp.ToString(), 1, _codigo_hash)*/);
                //string tickets = Config_Imp_NC.GenerarTicketNC(_not_id_imp.ToString(), 1, _codigo_hash);
                await ProgressAlert.CloseAsync();
                if (tickets == null)
                {
                    await this.ShowMessageAsync(Ent_Msg.msginfomacion, " >> Se producjo un error en la impresión del ticket");
                    //MessageBox.Show(" >> Se producjo un error en la impresión del ticket", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
                    //Mouse.OverrideCursor = null;
                }

            }
            //Mouse.OverrideCursor = null;
        }

        private void txtdoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                consultar();
        }

        private void dg1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            DataRowView item = e.Row.Item as DataRowView;
            if (item != null)
            {
                DataRow row = item.Row;

                var colValue = row["Not_Cod_Hash"].ToString();

                if (colValue == "")
                    e.Row.Background = new SolidColorBrush(Colors.BlanchedAlmond);

            }
        }

        private async void  btnxml_Click(object sender, RoutedEventArgs e)
        {
            ProgressDialogController ProgressAlert = null;
            try
            {                      

                DataRowView row = (DataRowView)((Button)e.Source).DataContext;

                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Si",
                    NegativeButtonText = "No",
                    //FirstAuxiliaryButtonText = "Cancelar",
                    ColorScheme = MetroDialogOptions.ColorScheme,
                };


               

                string _codigo_hash = (string)row["Not_Cod_Hash"].ToString();// dg1.Rows[e.RowIndex].Cells["Not_Cod_Hash"].Value.ToString(); ;
                decimal _not_id_imp = Convert.ToDecimal(row["Not_Id"]);// Convert.ToDecimal(dg1.Rows[e.RowIndex].Cells["Not_Id"].Value.ToString());
                string _numdoc_r = (string)row["numdoc"].ToString();// dg1.Rows[e.RowIndex].Cells["numdoc"].Value.ToString(); ;


                MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "¿Realmente desea enviar a sunat el XML DEL DOCUMENTO de tipo : Nota de Credito  con numero : " + _numdoc_r,
                   MessageDialogStyle.AffirmativeAndNegative, mySettings);

                if (result == MessageDialogResult.Affirmative)
                {
                    ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Enviando a sunat la Nota de Credito...");
                    //string _codigo_hash = "";
                    string _error = "";
                    await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica("N", _not_id_imp.ToString(), ref _codigo_hash, ref _error));


                    if (_error.Length > 0)
                    {
                        if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR AL ENVIAR EL XML A SUNAT. CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")");                    
                        return;
                    }

                    //EN ESTE PASO VAMOS A GRABAR EL CODIGO HASH
                    await Task.Run(() => Dat_Venta.insertar_codigo_hash(_not_id_imp.ToString(), _codigo_hash, "N"));


                    //****enviar los xml al server

                    await Task.Run(() => Basico._enviar_webservice_xml());

                    await Task.Run(() => consultar_refresh());

                    if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                    //Mouse.OverrideCursor = Cursors.Wait;


                }
                else
                {
                    if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                }
            }
            catch
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
            }
        }
    }
}

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

#region<SOLO PARA E-CCOMMERCE>
namespace Integrado.Sistemas.Ventas
{
    /// <summary>
    /// Lógica de interacción para ConsultaUrbano.xaml
    /// </summary>
    public partial class ConsultaUrbano : MetroWindow
    {

        public ConsultaUrbano()
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

            this.Title = "RE-ENVIO DE SOLICITUD DE SERVICIO A URBANO (COURIER) [" + Ent_Global._nom_modulo + "]";
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblhora.Content = DateTime.Now.ToLongTimeString();
        }

        private void btnprincipal_Click(object sender, RoutedEventArgs e)
        {
            Ent_Global._session_activa = true;
            OpcionesMenu frm = new OpcionesMenu();
            frm.Show();
            this.Close();
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

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Esta seguro de cerrar sessión!", MessageDialogStyle.AffirmativeAndNegative, mySettings);
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
            Integrado.Bll.Basico.cambio_img(imglogo);
            DateTime fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpdesde.Text = fecha.ToString();
            dtphasta.Text = DateTime.Today.ToString();

            txtdoc.IsEnabled = false;
            chkactivar.IsChecked = false;
            //chkactivar .Checked = false;
            consultar();
        }

        DataTable dt;
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
                dt = Dat_Venta.dt_consulta_pedido_urbano(_tipo, _fechaini, _fechafin, _doc);
                dg1.ItemsSource = dt.DefaultView;

                lblTotal.Content = "Total: " + dt.Rows.Count + " Reg.";

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Mouse.OverrideCursor = null;
        }


        private async void btnenviar_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };
            var okSettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Aceptar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };
            DataRowView row = (DataRowView)((Button)e.Source).DataContext;
            string msj_eccomer = "";
            string _cod_urbano = "";
            string _error = "";
            string _venid = (string)row["Ven_Id"].ToString();
            
            msj_eccomer = "¿Está seguro de Enviar la Solicitud a Urbano referente al Doc. Nro. " + _venid + "?";
            MessageDialogResult resultetiq = await this.ShowMessageAsync(Ent_Msg.msginfomacion, msj_eccomer, MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (resultetiq == MessageDialogResult.Affirmative)
            {
                if (Ent_Global._canal_venta == "BA")
                {

                    await Task.Run(() => Basico.act_presta_urbano(_venid, ref _error, ref _cod_urbano));


                    /*si el codigo de urbano esta null entonces no va el mensaje*/
                    if (_cod_urbano.Trim().Length > 0)
                    {
                        msj_eccomer = "Se envío correctamente la solicitud a Urbano, Nro. Código obtenido: " + _cod_urbano + ".\n¿Desea Imprimir la etiqueta de este pedido?";
                        resultetiq = await this.ShowMessageAsync(Ent_Msg.msginfomacion, msj_eccomer, MessageDialogStyle.AffirmativeAndNegative, mySettings);

                        if (resultetiq == MessageDialogResult.Affirmative)
                        {
                            GenerarEtiqueta genera_etiqueta = new GenerarEtiqueta();
                            await Task.Run(() => genera_etiqueta.imp_etiqueta(_venid));
                        }
                        // Actualizar
                        consultar();
                    }
                    else
                    {
                        msj_eccomer = "No se pudo enviar la solicitud a Urbano.";
                        resultetiq = await this.ShowMessageAsync(Ent_Msg.msginfomacion, msj_eccomer, MessageDialogStyle.Affirmative, okSettings);
                    }

                }
            }


        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
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

    }
}
#endregion

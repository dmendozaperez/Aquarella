using CapaDato.Bll.Util;
using CapaDato.Bll.Venta;
using CapaEntidad.Bll.Util;
using CapaEntidad.Bll.Venta;
using Epson_Ticket;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

namespace Integrado.Sistemas.Ventas
{
    /// <summary>
    /// Lógica de interacción para FinalVenta.xaml
    /// </summary>
    public partial class FinalVenta : MetroWindow
    {
        public FinalVenta()
        {
            InitializeComponent();
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            inicio();
        }
        private Dat_Cierre_Venta _datac = null;
        private Ent_Cierre_Venta get_cierre =null;
        private void inicio()
        {
            leer_tarjeta();
            dwbanco.EditValue = "-1";
            _datac = new Dat_Cierre_Venta();
            get_cierre=_datac.leer_data_cierre(Ent_Global._fecha_cierre_ult);
            if (get_cierre!=null)
            { 
                lblfechaventa.Content = get_cierre.fecha_venta.ToString("dd-MM-yyyy");
                lblventa.Content = string.Format("{0:C2}", get_cierre.total_venta);
                lblefectivo.Content= string.Format("{0:C2}", get_cierre.efectivo);
                lblvuelto.Content = string.Format("{0:C2}", get_cierre.vuelto);
                lbltefectivo.Content= string.Format("{0:C2}", get_cierre.total_efectivo);
                lbltarjeta.Content = string.Format("{0:C2}", get_cierre.total_tarjeta);
                lblefecneto.Content= string.Format("{0:C2}", get_cierre.total_efectivo);
                lblinicaja.Content = string.Format("{0:C2}", get_cierre.inicio_caja);
                lbltcaja.Content= string.Format("{0:C2}", get_cierre.total_caja);
            }
          
        }
        private void leer_tarjeta()
        {
            Dat_Banco banco = new Dat_Banco();
            List<Ent_Banco> listar = banco.listar();
            dwbanco.ItemsSource = listar;
        }
        private async void btnaceptar_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Boolean chkfin = chkcierre.IsChecked.Value;

            if (chkfin)
            {                
                if (get_cierre.total_efectivo>0)
                {                    
                    string _ban_id = dwbanco.EditValue.ToString();
                    string _ope = txtoperacion.Text.ToString();
                    decimal _monto_edit = Convert.ToDecimal(txtmonto.Text);

                    if (_ban_id== "-1")
                    {
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Debe de seleccionar el banco del deposito.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                        dwbanco.Focus();
                        Mouse.OverrideCursor = null;
                        return;
                    }
                    if (_ope.Length == 0)
                    {
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Ingrese el numero de operacion.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                        txtoperacion.Focus();
                        Mouse.OverrideCursor = null;
                        return;
                    }

                    if (_monto_edit==0)
                    {
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Ingrese el monto de la operacion.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                        txtmonto.Focus();
                        Mouse.OverrideCursor = null;
                        return;
                    }
                }
            }


            string mensaje = (chkfin) ? "Esta seguro de realizar el cierre de dia" : "Solo se va imprimir el cierre y no se cerrara el dia de venta";

            this.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;

            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, mensaje,
            MessageDialogStyle.AffirmativeAndNegative, mySettings);
            if (result == MessageDialogResult.Affirmative)
            {

                if (chkfin)
                {
                    string _ban_id = dwbanco.EditValue.ToString();
                    string _nroope = txtoperacion.Text;
                    Decimal _monto_op =Convert.ToDecimal(txtmonto.Text);
                    Dat_Basico updatecierre = new Dat_Basico();
                    Boolean _valida = updatecierre.update_cierre_venta(2, Ent_Global._fecha_cierre_ult, 0,_ban_id,_nroope,_monto_op);
                    if (_valida)
                    {
                        ImprimirCierre.Generar_Impresion_Cierre(Ent_Global._fecha_cierre_ult);
                        Dat_Basico.VerificaFechaServer_Cierre();
                        Dat_Basico.VerificaCierreVenta();                        
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Se Realizo el cierre de venta.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                        Mouse.OverrideCursor = null;
                        this.Close();
                    }
                    else
                    {
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "!Hubo un problema al momento de cerrar la venta, por favor consulte con sistemas.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                    }
                }
                else
                { 
                    string _imprime = ImprimirCierre.Generar_Impresion_Cierre(Ent_Global._fecha_cierre_ult);
                    if (_imprime!="ok")
                    {                
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Hubo un problema con la impresion.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                    }
                }

            }                            
            Mouse.OverrideCursor = null;
        }

        private void txtoperacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter)
            {
                txtmonto.Focus();
            }
        }
    }
}

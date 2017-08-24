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
    /// Lógica de interacción para ConsultaCierre.xaml
    /// </summary>
    public partial class ConsultaCierre : MetroWindow
    {
        public ConsultaCierre()
        {
            InitializeComponent();
        }
        Dat_Cierre_Venta _datac = null;
        Ent_Cierre_Venta get_cierre;
        private async void consultar()
        {
            Mouse.OverrideCursor = Cursors.Hand;
            limpiar();
           
            DateTime _fecha =Convert.ToDateTime(dtpfecha.Text);
            _datac = new Dat_Cierre_Venta();
            get_cierre = _datac.leer_data_cierre(_fecha);
            if (get_cierre != null)
            {
                lblfechaventa.Content = get_cierre.fecha_venta.ToString("dd-MM-yyyy");
                lblventa.Content = string.Format("{0:C2}", get_cierre.total_venta);
                lblefectivo.Content = string.Format("{0:C2}", get_cierre.efectivo);
                lblvuelto.Content = string.Format("{0:C2}", get_cierre.vuelto);
                lbltefectivo.Content = string.Format("{0:C2}", get_cierre.total_efectivo);
                lbltarjeta.Content = string.Format("{0:C2}", get_cierre.total_tarjeta);
                lblefecneto.Content = string.Format("{0:C2}", get_cierre.total_efectivo);
                lblinicaja.Content = string.Format("{0:C2}", get_cierre.inicio_caja);
                lbltcaja.Content = string.Format("{0:C2}", get_cierre.total_caja);

                lblbanco.Content = get_cierre.banco_des;
                lbloperacion.Content = get_cierre.nro_operacion;
                lblmontoop.Content = string.Format("{0:C2}", get_cierre.monto_opera);
            }
            else
            {
                var metroWindow = this;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "No hay datos para mostrar.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
            Mouse.OverrideCursor = null;
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dtpfecha.Text = DateTime.Today.ToString();
            consultar();
        }
        private void limpiar()
        {
            lblfechaventa.Content = "";
            lblventa.Content = string.Format("{0:C2}", 0);
            lblefectivo.Content = string.Format("{0:C2}", 0);
            lblvuelto.Content = string.Format("{0:C2}", 0);
            lbltefectivo.Content = string.Format("{0:C2}", 0);
            lbltarjeta.Content = string.Format("{0:C2}", 0);
            lblefecneto.Content = string.Format("{0:C2}", 0);
            lblinicaja.Content = string.Format("{0:C2}", 0);
            lbltcaja.Content = string.Format("{0:C2}", 0);
            lblbanco.Content = "";
            lbloperacion.Content = "";
            lblmontoop.Content= string.Format("{0:C2}", 0);
        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            consultar();
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnaceptar_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            var metroWindow = this;
            if (get_cierre==null)
            {
               
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "No hay datos para imprimir.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
            else
            {
                DateTime _fecha = Convert.ToDateTime(dtpfecha.Text);
                string _imprime = ImprimirCierre.Generar_Impresion_Cierre(_fecha);
                if (_imprime != "ok")
                {
                    await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Hubo un problema con la impresion.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                }
            }
            Mouse.OverrideCursor = null;
        }

        private void txtoperacion_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}

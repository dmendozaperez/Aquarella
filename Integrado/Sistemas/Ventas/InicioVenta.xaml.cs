using CapaDato.Bll.Util;
using CapaEntidad.Bll.Util;
using Integrado.Design.WPF_Master;
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
    /// Lógica de interacción para InicioVenta.xaml
    /// </summary>
    public partial class InicioVenta : MetroWindow
    {
        public InicioVenta()
        {
            InitializeComponent();
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnaceptar_Click(object sender, RoutedEventArgs e)
        {
            this.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            DateTime fecha_select = calfecha.SelectedDate.Value;
            decimal _monto =Convert.ToDecimal(txtmonto.Text);
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };


            if (fecha_select != Ent_Global._fecha_server)
            {                
                await this.ShowMessageAsync(Ent_Msg.msginfomacion, "La fecha seleccionada no puede ser diferente a la fecha del servidor de aquarella.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
            }
            else
            {
                MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Esta seguro de iniciar el dia de venta",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);
                if (result == MessageDialogResult.Affirmative)
                {
                    Dat_Basico updatecierre = new Dat_Basico();
                    Boolean _valida= updatecierre.update_cierre_venta(1,fecha_select, _monto,"","",0);
                    if (_valida)
                    {
                        Dat_Basico.VerificaFechaServer_Cierre();
                        Dat_Basico.VerificaCierreVenta();
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Ya puedes realizar la venta.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                        this.Close();
                    }
                    else
                    {
                        await this.ShowMessageAsync(Ent_Msg.msginfomacion, "!Hubo un problema al momento de la incializacion de venta, por favor consulte con sistemas.", MessageDialogStyle.Affirmative, this.MetroDialogOptions);
                    }
                }
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Dat_Basico.GetFechaServer();
            DateTime fecha_actual = DateTime.Today;
            calfecha.SelectedDate = fecha_actual;

            txtmonto.Focus();

        }

        private void txtmonto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                btnaceptar.Focus();
            }
        }
    }
}

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
using CapaDato.Bll.Venta;
using Integrado.Urbano;

namespace Integrado.Sistemas.Logistica
{
    /// <summary>
    /// Lógica de interacción para ImprimeUrbano.xaml
    /// </summary>
    public partial class ImprimeUrbano : MetroWindow
    {
        public ImprimeUrbano()
        {
            InitializeComponent();
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtnumero.Focus();
        }

        private async void btnimprimir_Click(object sender, RoutedEventArgs e)
        {
            string _venid = txtnumero.Text.Replace("-","");
            GenerarEtiqueta genera_etiqueta = new GenerarEtiqueta();
            await Task.Run(() => genera_etiqueta.imp_etiqueta(_venid));
        }

    }

}

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
namespace Integrado.Sistemas.Logistica
{
    /// <summary>
    /// Lógica de interacción para DuplicaGuia.xaml
    /// </summary>
    public partial class DuplicaGuia : MetroWindow
    {
        public DuplicaGuia()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtnumero.Focus();
        }

        private void btnimprimir_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;





            //string _codigo_hash = "";
            //string _error = "";

            //#region<VENTA DE LA FACTURACION ELECTRONICA>
            //Bll.Facturacion_Electronica.ejecutar_factura_electronica("B", "B03100000088", ref _codigo_hash, ref _error);
            //Dat_Venta.insertar_codigo_hash("B03100000088", _codigo_hash, "V");
            //Bll.Basico._enviar_webservice_xml();
            //#endregion


            //#region<VENTA DE LA NOTA DE CREDITO ELECTRONICA>

            //Integrado.Bll.Facturacion_Electronica.ejecutar_factura_electronica("N", "2678", ref _codigo_hash, ref _error);


            //if (_codigo_hash.Length>0)
            //{
            //    Integrado.Bll.Basico._enviar_webservice_xml();
            //    CapaDato.Bll.Venta.Dat_Venta.insertar_codigo_hash("2678", _codigo_hash, "N");
            //}


            //#endregion
            vistaprevia();
            Mouse.OverrideCursor = null;
        }
        private void vistaprevia()
        {
            if (valida_guia() == 0)
            {
                lblmensaje.Content = " >> El numero de Tickets no existe...";
                return;
            }
            else
            {
                Reporte_Guia_Remision._idv_invoice = txtnumero.Text.Trim();
                lblmensaje.Content = " >> Generando Vista Previa...";
                Reporte_Guia_Remision frm = new Reporte_Guia_Remision();
                frm.Show();
            }
        }
        private Int32 valida_guia()
        {
            return Dat_Venta.valida_guia(txtnumero.Text);
        }
    }
}

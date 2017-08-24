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
using Aquarella.bll;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Aquarella.Form.Logistica;
namespace Aquarella.Form.Ventas
{
    /// <summary>
    /// Lógica de interacción para PanelLiquidationForInvoiceWindow.xaml
    /// </summary>
    public partial class PanelLiquidationForInvoiceWindow : Window
    {
        Usuario _user;
        ObservableCollection<Liquidation_Hdr> _ocLiqHdr;
        Liquidation_HdrViewModel _LiquHdVM;

     
        String _status = "PF";
        public PanelLiquidationForInvoiceWindow()
        {
            InitializeComponent();
        }
        public void PanelLiquidationForInvoiceWindowStart(Usuario user)
        {
            ///
            _user = new Usuario();
            _user = user;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            lblInfoUser.Text = "Usuario | " + _user._nombre;
            ///
            _ocLiqHdr = new ObservableCollection<Liquidation_Hdr>();
            ///
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(loadLiquisForInvoice));
        }
        #region < CARGA DE PANEL DE LIQUIDACIONES PARA FACTURAR >

        /// <summary>
        /// Cargar liquidaciones en espera por ser facturadas
        /// </summary>
        public void loadLiquisForInvoice()
        {
            ///
            _LiquHdVM = new Liquidation_HdrViewModel();
            ///
            _ocLiqHdr = _LiquHdVM.getLiquidationsByStatus();
            ///
            if (_ocLiqHdr != null)
                dgOrdersForInv.ItemsSource = _ocLiqHdr;
            else
                dgOrdersForInv.ItemsSource = new ObservableCollection<Liquidation_Hdr>();

            // Realizar totalizados
            calculeTotals();

            ///
            isLoading();
        }

        #endregion

        #region < Totalizados >

        private void calculeTotals()
        {
            this.lblTotalLiq.Text = _ocLiqHdr.Count().ToString();
            //
            this.lblTotalQtys.Text = _ocLiqHdr.Sum(x => x._qtystotals).ToString("N0");
            //
            this.lblTotalQtysPack.Text = _ocLiqHdr.Sum(x => x._pdn_qty).ToString("N0");
            //
            this.lblTotalQtysRest.Text = (_ocLiqHdr.Sum(x => x._qtystotals) - _ocLiqHdr.Sum(x => x._pdn_qty)).ToString("N0");
            //throw new NotImplementedException();
        }

        #endregion
        public void isLoading()
        {
            if (meLoader.Visibility == Visibility.Hidden)
                meLoader.Visibility = Visibility.Visible;
            else
                meLoader.Visibility = Visibility.Hidden;
        }
        private void txtFilterOrders_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            ///this.dgOrdersForInv.ItemsSource = _ocLiqHdr.Select(x => x._ldv_liquidation_no = txtFilterOrders.Text);
            this.dgOrdersForInv.ItemsSource = from x in _ocLiqHdr
                                              where x._ldv_liquidation_no.StartsWith(txt.Text) || x._lhv_customer_name.ToUpper().StartsWith(txt.Text.ToUpper())
                                              select x;
        }

        private void btRefresh_Click(object sender, RoutedEventArgs e)
        {          
            loadLiquisForInvoice();
            ///
            isLoading();
        }

        private void btConfigGuide_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                try
                {
                    ///
                    var task = (Liquidation_Hdr)(button.DataContext as Liquidation_Hdr);
                    ///
                    if (task != null)
                    {
                        ///
                        SetupGuideLiquiWindow frm = new SetupGuideLiquiWindow();
                        ///
                        frm.Owner = this;
                        ///
                        frm.SetupGuideLiquiWindowStart( task._ldv_liquidation_no, task._lhv_customer_name, task._lhn_customer.ToString());
                        frm.ShowDialog();
                        ///
                        if (frm.DialogResult.HasValue && frm.DialogResult.Value)
                        {
                            /// Refrescar paneles de liquidacion para cargar nuevas guias

                            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(loadLiquisForInvoice));
                            ///
                            isLoading();
                        }
                        //else
                        //this.Close();
                    }
                }
                catch { }
            }
        }
        public void startPackOrder(String noLiq)
        {
            try
            {

                InvoiceWindow invW = new InvoiceWindow();
                invW.InvoiceWindowStart(_user, noLiq);
                invW.Show();
                this.Close();
            }
            catch
            {
                ///
                MessageBox.Show("No se puede cargar la liquidación solicitada, ha ocurrido un error.",
                                    ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        

        private void btPackOrder_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                try
                {
                    ///
                    var task = (Liquidation_Hdr)(button.DataContext as Liquidation_Hdr);
                    ///
                    if (task != null)
                    {
                        /// Really delete
                        MessageBoxResult msbResult = MessageBox.Show("¿Realmente desea iniciar el empacado de la liquidación No. : " + task._ldv_liquidation_no + " perteneciente al cliente : " +
                            task._lhv_customer_name + "?"
                        , ValuesDB.captionHeaderWarningWindow, MessageBoxButton.OKCancel, MessageBoxImage.Information);
                        ///
                        if (msbResult == MessageBoxResult.OK)
                        {
                            ///1.ero. Verificar si la liquidacion posee guia.
                            //if (!String.IsNullOrEmpty(task._lhv_guide_no) && !String.IsNullOrEmpty(task._lhv_transporter))
                            //{
                            
                                Liquidation_Hdr._liq_id = task._ldv_liquidation_no;
                                Liquidation_Hdr._liq_bas_id = task._lhn_customer;
                                Liquidation_Hdr._liq_fecha_ing = task._lhd_date;
                                Liquidation_Hdr._liq_guia = Convert.ToDecimal((task._lhv_guide_no.Length == 0) ? "0" : task._lhv_guide_no);
                                Liquidation_Hdr._liq_total = task._qtystotals;

                                //variables para el cliente
                                Coordinator._bas_documento = task._bas_documento;
                                Coordinator._bas_nombre = task._lhv_customer_name;
                                Coordinator._direccion = task._bas_direccion;
                                Coordinator._lugar = task._lhv_customer_ubication;


                                Transporters_Guides._guia = task._lhv_guide_no;
                                Transporters_Guides._guia_id = task._lhn_guide;
                                Transporters_Guides._transporte = task._lhv_transporter;
                                
                                /// Cargar la ventana de facturacion
                                /// 
                                startPackOrder(task._ldv_liquidation_no);
                            //}
                            //else
                            //{                               

                            //    MessageBox.Show("Asigne una guia a la liquidación No. " + task._ldv_liquidation_no + " antes de proceder al empacado.",
                            //       ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Error);
                            //}                          
                        }
                    }
                }
                catch
                {
                   
                }
            }
            else
            {
                return;
            }
        }

        private void btCloseSesion_Click(object sender, RoutedEventArgs e)
        {
            ///
            MainAQWindow maw = new MainAQWindow();
            ///
            this.Close();
            ///
            maw.Show();

        }

        private void btBackPanelLiq_Click(object sender, RoutedEventArgs e)
        {
            MainAQWindow maw = new MainAQWindow();
            ///
            maw.loadUserInSesion(_user);
            ///
            maw.Show();
            ///
            this.Close();
        }
    }
}

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
namespace Aquarella.Form.Logistica
{
    /// <summary>
    /// Lógica de interacción para SetupGuideLiquiWindow.xaml
    /// </summary>
    public partial class SetupGuideLiquiWindow : Window
    {
        String _co;

        /// <summary>
        /// Id del cliente al cual se le enviara el pedido
        /// </summary>
        String _custDestiny;

        /// <summary>
        /// Numero de liquidacion
        /// </summary>
        String _noLiq;

        /// <summary>
        /// 
        /// </summary>
        ///ObservableCollection<Transporters> _transOC;

        /// <summary>
        /// Clase de vista de modelo
        /// </summary>
        TransportersViewModel _transVM;

        /// <summary>
        /// 
        /// </summary>
        Transporters_GuidesViewModel _transGuidesVM;

        /// <summary>
        /// 
        /// </summary>
        Liquidation_HdrViewModel _liqHdrVM;
        public SetupGuideLiquiWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadTransporters();
        }
        public void SetupGuideLiquiWindowStart( String noLiq, String customerName, String custDestiny)
        {
            ///            
            _custDestiny = custDestiny;
            _noLiq = noLiq;
            ///
            this.txtInfoLiq.Text = "Liquidación Número : " + noLiq + Environment.NewLine + "Nombre Cliente : " + customerName;
        }
        public void loadTransporters()
        {
            ///
            _liqHdrVM = new Liquidation_HdrViewModel();


            _transVM = new TransportersViewModel();
            ///
            this.cbTransport.ItemsSource = _transVM.getAllTransportsByCompany();
            object obj = _liqHdrVM.getNumGuiaBL();
            txtGuide.Text = obj.ToString();
        }
        private void btCancel_Click(object sender, RoutedEventArgs e)
        {
            ///
            DialogResult = false;
            this.Close();
        }
        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            ///
            //Item seleccionado
            Transporters transSelected = (Transporters)cbTransport.SelectedItem;
            //
            if (transSelected != null)
            {
                ///
                if (!String.IsNullOrEmpty(txtGuide.Text))
                {
                    ///
                    _transGuidesVM = new Transporters_GuidesViewModel();
                    ///
                    String newIdGuide = "";
                    Liquidacion.insertar_guia(txtGuide.Text, Convert.ToInt32(transSelected._trv_transporters_id), _noLiq, out newIdGuide);                    
                    ///
                    if (!newIdGuide.Equals("-1") && !newIdGuide.Equals("1"))
                    {
                        //_liqHdrVM = new Liquidation_HdrViewModel();
                        ///// Actualizar el numero de guia en la liquidacion ///
                        //String resp = _liqHdrVM.updateGuideLiquidation( _noLiq, newIdGuide);
                        /////
                        //if (!resp.Equals("-1"))

                            // All process is successfully
                            DialogResult = true;
                        this.Close();
                    }
                    else
                    {
                        txtInfoSave.Foreground = Brushes.Red;
                        ///
                        txtInfoSave.Text = "Es probable que la guia ya exista, por favor dígite un nuevo numero de guia.";
                    }
                }
                else
                {
                    txtInfoSave.Foreground = Brushes.Red;
                    ///
                    txtInfoSave.Text = "Por favor dígite el numero de guia.";
                }
            }
            else
            {
                txtInfoSave.Foreground = Brushes.Red;
                ///
                txtInfoSave.Text = "Por favor, seleccione la transportadora.";
            }
        }
    }
}

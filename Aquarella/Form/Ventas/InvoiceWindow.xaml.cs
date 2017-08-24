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
namespace Aquarella.Form.Ventas
{
    /// <summary>
    /// Lógica de interacción para InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        /// Usuario, empleado o persona logueada en el sistema
        /// </summary>
        Usuario _user;

        /// <summary>
        /// Liquidation Header Observable Collection
        /// </summary>
        ObservableCollection<Liquidation_Hdr> _ocLiqHdr;

        /// <summary>
        /// Liquidation Detail Observable Collection
        /// </summary>
        ObservableCollection<Liquidation_Dtl> _ocLiqDtl;

        /// <summary>
        /// Liquidation Detail Observable Collection de articulos faltantes por empacar
        /// </summary>
        ObservableCollection<Liquidation_Dtl> _ocLiqDtlArtsRemaining;

        /// <summary>
        /// Coordinator Observable Collection
        /// </summary>
        ObservableCollection<Coordinator> _ocCoordinator;

        /// <summary>
        /// Transportes guides observable collection
        /// </summary>
        ObservableCollection<Transporters_Guides> _ocTransGuides;

        /// <summary>
        /// Packages observable collection
        /// </summary>
        ObservableCollection<Packages> _ocPackage;

        /// <summary>
        /// Packages Detail observable collection
        /// </summary>
        ObservableCollection<Packages_Dtl> _ocPackDtl;

        /// <summary>
        /// View Model Liquidation Header
        /// </summary>
        Liquidation_HdrViewModel _LiquHdVM;

        /// <summary>
        /// View Model Liquidation Detail
        /// </summary>
        Liquidation_DtlViewModel _LiquDtlVM;

        /// <summary>
        /// View Model Liquidation Detail articulos aun si empacar
        /// </summary>
        Liquidation_DtlViewModel _LiquDtlArtsRemaining;

        /// <summary>
        /// View Model cabecera de factura
        /// </summary>
        Invoice_HdrViewModel _InvHdrVM;

        /// <summary>
        /// View Model whtransfers
        /// </summary>
        WhtransfersViewModel _WhtranVM;

        /// <summary>
        /// View Model Coordinator
        /// </summary>
        CoordinatorViewModel _CoordinatorVM;

        /// <summary>
        /// View Model Transporters guides
        /// </summary>
        Transporters_GuidesViewModel _TransGuidesVM;

        /// <summary>
        /// View Model Packages
        /// </summary>
        PackagesViewModel _packagesVM;

        /// <summary>
        /// View Model Packages
        /// </summary>
        Packages_DtlViewModel _packDtlVM;

        /// <summary>
        /// Número de la liquidacion que se esta empacando
        /// </summary>
        String _noLiquidation; //"25087";//"25085";

        /// <summary>
        /// Id del customer al cual pertenece la liquidacion
        /// </summary>
        Decimal _idCustomer;

        /// <summary>
        /// Tipo de Customer: coordinador, cedi, epecial etc.
        /// </summary>
        String _CustomerType;

        /// <summary>
        /// Numero del paquete
        /// </summary>
        Decimal _PackNo;

        /// <summary>
        /// 
        /// </summary>
        String _co;

        ///llamar a la clase GenerarTickets

        //GenerarTicket objGenerarTicket;

        /// <summary>
        /// Tipo de coordinador = 4 cliente tipo cedi
        /// </summary>
        String _typeCoordCedi = ValuesDB.typeCoordCedi;
        public static string _guia { set; get; }
     

        #region < METODOS DEL LOAD DE LA PAGINA >

        /// <summary>
        /// 
        /// </summary>
        public InvoiceWindow()
        {
            InitializeComponent();
            ///
            _noLiquidation = "";
            ///
            this.txtArticlesRef.Focus();
            ///
            this.startObjects();
        }

        /// <summary>
        /// Inicializacion de todos los objetos necesarios para el funcionamiento correcto del formulario
        /// </summary>
        public void startObjects()
        {
            _LiquHdVM = new Liquidation_HdrViewModel();
            ///
            _CoordinatorVM = new CoordinatorViewModel();
            ///
            _TransGuidesVM = new Transporters_GuidesViewModel();
            ///
            _packagesVM = new PackagesViewModel();
            ///
            _packDtlVM = new Packages_DtlViewModel();
            ///
            _ocPackDtl = new ObservableCollection<Packages_Dtl>();
            ///
            _CustomerType = "";
            ///
            //objGenerarTicket = new GenerarTicket();
            ///
            ///GridLiqInfo.DataContext = _LiquHdVM.getLiquidationHdr(_co, "25085");
            ///
        }


        /// <summary>
        /// Cargar los datos del usuario
        /// </summary>
        /// <param name="user"></param>
        /// <param name="liquidation"></param>
        public void InvoiceWindowStart(Usuario user, String liquidation)
        {
            ///
            _user = new Usuario();
            _user = user;
            _noLiquidation = liquidation;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {            
            lblInfoUser.Text = "Usuario | " + _user._nombre;

            this.startWindowInvoice();           
        }

        /// <summary>
        /// Inicializacion de todos los componentes necesarios para el inicializado del empacado
        /// </summary>
        public void startWindowInvoice()
        {
            ///
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(loadInfoData));
            ///
            this.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(loadTotArticlesPacking));
            ///
        }

        #endregion

        #region < CARGADO DE ARTICULOS EMPACADOS >

        /// <summary>
        /// 
        /// </summary>
        public void loadAllGrids()
        {
            this.loadArtsPackInActualPackage();
            ///
            this.loadTotArticlesPacking();
        }

        /// <summary>
        /// Cargado de articulos totales empacados
        /// </summary>
        public void loadTotArticlesPacking()
        {
            ///
            _LiquDtlVM = new Liquidation_DtlViewModel();
            ///
            _ocLiqDtl = _LiquDtlVM.getArticlesPackByLiq(_noLiquidation);
            ///
            if (_ocLiqDtl != null)
            {
                ///this.lvArtsTotPacking.ItemsSource = _ocLiqDtl;
                this.dgArtsTotPack.ItemsSource = _ocLiqDtl;
                ///
                this.calculateQtyTotals();
                ///
                this.dgArtsTotPack.ScrollIntoView(this.dgArtsTotPack.Items.MoveCurrentToLast());
                this.dgArtsTotPack.UpdateLayout();
            }
            else
                this.dgArtsTotPack.ItemsSource = new ObservableCollection<Liquidation_Dtl>();
        }

        /// <summary>
        /// Cargado de todos los articulos o items empacados en el paquete actual
        /// </summary>
        public void loadArtsPackInActualPackage()
        {
            _packDtlVM = new Packages_DtlViewModel();
            ///
            _ocPackDtl = _packDtlVM.getArticlesPackingByNoPackage(_noLiquidation, _PackNo);
            ///
            if (_ocPackDtl != null)
                ///this.dgArtsInActualPackage.DataContext = _ocPackDtl;
                this.dgArtsInActualPackage.ItemsSource = _ocPackDtl;
            else
                this.dgArtsInActualPackage.ItemsSource = new ObservableCollection<Packages_Dtl>();
          
        }

        #endregion

        #region < CARGADO DE INFORMACION DE LIQUIDACION, DESTINATARIO, GUIA Y NUMERO DE PAQUETE>

        /// <summary>
        /// Carga inicial de toda la informacion requerida para el inicio del empacado del pedido
        /// </summary>
        public void loadInfoData()
        {
            /// Load Liquidation Data
            this._ocLiqHdr = _LiquHdVM.getLiquidationHdr(_noLiquidation);
            itmsCrlLiqHdrInfo.ItemsSource = _ocLiqHdr;
            /// Cargar cantidades totales de articulos en la liquidacion
            ///
            _noLiquidation = _ocLiqHdr.First<Liquidation_Hdr>()._ldv_liquidation_no;
            ///
            _idCustomer = _ocLiqHdr.First<Liquidation_Hdr>()._lhn_customer;
            ///
            _ocCoordinator = _CoordinatorVM.getInfoCoordinator(_idCustomer);
            ///
           // _CustomerType = _ocCoordinator.First<Coordinator>()._cov_coordinator_type;
            ///
            itmsCrlLiqCusto.ItemsSource = _ocCoordinator;
            ///
            _ocTransGuides = _TransGuidesVM.getGuidesByPrimaryKey(_ocLiqHdr.First<Liquidation_Hdr>()._lhn_guide);
            ///
            itmsCrlTransGuides.ItemsSource = _ocTransGuides;
            ///
            lblHdrInfoLiq.Text = " ( No.Liq:" + _noLiquidation + " | Cliente : " + _ocCoordinator.First<Coordinator>()._cov_name + ")";
            ///
            this.determineIdForPackage();
        }

        /// <summary>
        /// Determinar el numero del paquete para la facturacion en curso
        /// </summary>
        public void determineIdForPackage()
        {
            ///
            /// Número del paquete seleccionado ó se debe crear un nuevo paquete
            _PackNo = _packagesVM.addOrGetPackage(_noLiquidation, _user._bas_id.ToString());
            ///
            if (_PackNo == -2)
            {
                /// No quedan mas articulos que empacar, bloquear botones de nuevo paquete etc.
                ///
                this.txtArticlesRef.IsEnabled = false;
                this.btSearchArt.IsEnabled = false;
                ///
                this.blockFieldsPack();

                /// Consultar el numero total de paquetes que se generaron para la liquidacion
                /// 
                _ocPackage = _packagesVM.getMaxNoPackageByLiqui(_noLiquidation);
                ///
                if (_ocPackage != null)
                {
                    this.lblNoPackTitl.Content = " Núm. Total ";
                    this.lblNoPack.Text = _ocPackage.First<Packages>()._pan_no.ToString();
                }
                else
                    this.lblNoPackTitl.Content = "Sin Def.";
            }
            else
            {
                /// Se ha creado un nuevo paquete, consultar el numero del paquete, osea 1 de N.
                /// Imprimir el numero del paquete referente a este nuevo registro. numero paquete de N.
                /// 
                Packages._paq_id = _PackNo;

                Packages._paq_no = Venta.leer_maxnopaqliq(_noLiquidation).ToString();

                _ocPackage = _packagesVM.getPackagesByPrimaryKey(_noLiquidation, _PackNo);
                ///
                if (_ocPackage != null)
                    this.lblNoPack.Text = _ocPackage.First<Packages>()._pan_no;
                else
                    this.lblNoPack.Text = "0";
            }
        }

        #endregion

        #region < EVENTOS SOBRE BT - TXT ETC >

        /// <summary>
        /// Evento de manejo del enter en el campo de texto de ingreso de referencia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtArticlesRef_KeyDown(object sender, KeyEventArgs e)
        {
            ///
            if (e.Key == Key.Enter)
            {
                /// Semaforo en amarillo, en espera de respuesta
                this.txtArticlesRef.Dispatcher.Invoke(DispatcherPriority.Send, new Action(changeSemaphoreY));

                ///
                if ((Boolean)this.chkbBarCode.IsChecked)
                    ///
                    this.packArticleCodeBar();
                else
                    ///
                    this.fillGridArtsRemainingForPack();
                ///
                txtArticlesRef.Select(0, txtArticlesRef.Text.Length);
                txtArticlesRef.Focus();
                ///
                e.Handled = true;
            }
        }

        /// <summary>
        /// Boton de buscar referencia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSearchArt_Click(object sender, RoutedEventArgs e)
        {
            /// Semaforo en amarillo, en espera de respuesta
            this.Dispatcher.Invoke(DispatcherPriority.Send, new Action(changeSemaphoreY));
            /// 
            ///lsvArticles.ItemsSource = vmArticles.getArticlesByref(_co, txtArticlesRef.Text);
            if ((Boolean)this.chkbBarCode.IsChecked)
                ///
                this.packArticleCodeBar();
            else
                ///
                this.fillGridArtsRemainingForPack();
            ///
            txtArticlesRef.Select(0, txtArticlesRef.Text.Length);
            txtArticlesRef.Focus();
        }

        /// <summary>
        /// Desempacar o borrar un articulo de un paquete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btUnPackArticle_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                try
                {
                    ///
                    var task = button.DataContext as Packages_Dtl;
                    ///
                    if (task != null)
                    {
                        /// Really delete
                        MessageBoxResult msbResult = MessageBox.Show("¿Realmente desea eliminar el artículo : " + ((Packages_Dtl)task)._pdv_article + " en marca : " +
                            ((Packages_Dtl)task)._pdv_article_brand + "?"
                        , "SIC - Mensaje De Advertencia", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
                        ///
                        String msge = " > El artículo : " + ((Packages_Dtl)task)._pdv_article + " en marca : " + ((Packages_Dtl)task)._pdv_article_brand + " ha sido eliminado.";
                        ///
                        if (msbResult == MessageBoxResult.OK)
                        {
                            ///((ObservableCollection<Article>)lsvArticles.ItemsSource).Remove(task);
                            /// Realizar la eliminación 
                            String respuesta = _packDtlVM.deleteLineFromPackagesDtl(task._pdn_package, task._pdv_article, task._pdv_size);

                            ///
                            if (respuesta.Equals("1"))
                            {
                                this.loadAllGrids();
                            }
                            ///
                            lblMessage.Text = msge;
                        }
                    }
                }
                catch
                {
                    ///
                    lblMessage.Foreground = Brushes.Maroon;
                    ///
                    lblMessage.Text = "> Error al intentar eliminar el artículo.";
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Borrar un articulo del paquete actual, mediante el presionado de la tecla suprimir o borrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgArtsInActualPackage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                try
                {
                    ///button.DataContext 
                    var task = dgArtsInActualPackage.CurrentItem as Packages_Dtl;
                    ///
                    if (task != null)
                    {
                        /// Really delete
                        MessageBoxResult msbResult = MessageBox.Show("¿Realmente desea eliminar el artículo : " + ((Packages_Dtl)task)._pdv_article + " en marca : " +
                            ((Packages_Dtl)task)._pdv_article_brand + "?"
                        , "SIC - Mensaje De Advertencia", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
                        ///
                        String msge = " > El artículo : " + ((Packages_Dtl)task)._pdv_article + " en marca : " + ((Packages_Dtl)task)._pdv_article_brand + " ha sido eliminado.";
                        ///
                        if (msbResult == MessageBoxResult.OK)
                        {
                            ///((ObservableCollection<Article>)lsvArticles.ItemsSource).Remove(task);
                            /// Realizar la eliminación 
                            String respuesta = _packDtlVM.deleteLineFromPackagesDtl( task._pdn_package, task._pdv_article, task._pdv_size);

                            ///
                            if (respuesta.Equals("1"))
                            {
                                this.loadAllGrids();
                            }
                            ///
                            lblMessage.Text = msge;
                        }
                    }
                }
                catch
                {
                    ///
                    lblMessage.Foreground = Brushes.Maroon;
                    ///
                    lblMessage.Text = "> Error al intentar eliminar el artículo.";
                }
            }

        }

        /// <summary>
        /// Cargado de reporte de paquete para impresion o guardado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPrintPackage_Click(object sender, RoutedEventArgs e)
        {
            ///
            var button = sender as Button;
            if (button != null)
            {
                try
                {
                    ///
                    var task = (Packages)(button.DataContext as Packages);
                    ///
                    if (task != null)
                    {
                        /////
                        //String msge = " > Se ha generado el reporte del paquete : " + task._pdn_packageid;
                        ///// Realizar la carga del reporte del paquete
                        ///// 
                        //ReportPackagesWindow rpW = new ReportPackagesWindow();
                        /////
                        //rpW.setVarsForReport(_co, _noLiquidation, task._pdn_packageid);
                        ///// Mostrar el reporte
                        //rpW.Show();
                        /////
                        //lblMessage.Text = msge;

                    }
                }
                catch
                {
                    ///
                    lblMessage.Foreground = Brushes.Maroon;
                    ///
                    lblMessage.Text = "> Error al intentar cargar el reporte del paquete.";
                }
            }
            else
            {
                return;
            }
        }

        #endregion

        #region < FUNCIONES DE LA LOGICA DE EMPACADO DE ARTICULOS Y FACTURACION >

        /// <summary>
        /// Empacar un articulo
        /// </summary>
        public void packArticleCodeBar()
        {
            try
            {
                /// Descomponer la informacion contenida en el codigo de barras
                /// Determinar la codificacion de la referencia del articulo.
                /// Si es Ean13, o si es referencia mas talla, entre otros 
                /// 

                string v_articulo = txtArticlesRef.Text.Trim();
                string _barra = (txtArticlesRef.Text.Trim().Length == 18) ? txtArticlesRef.Text.Trim() : "";
                String[] infoArticle = BarCodes.getInfoFromTheBarCode(txtArticlesRef.Text);

                
                ///
                if (infoArticle != null && infoArticle.Length > 0)
                {

                    String sizeToAdd = infoArticle[1];
                    /// Article 
                    String articleToAdd = infoArticle[0];

                    String calidadToAdd = infoArticle[2];

                    string varReturn = Venta.insertar_articulopaq(Packages._paq_id, Liquidation_Hdr._liq_id, articleToAdd, sizeToAdd, 1, calidadToAdd, _barra);


                    ///
                    if (varReturn.Equals("1"))
                    {
                       
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(loadAllGrids));
                        ///
                        this.changeSemaphore(Brushes.YellowGreen);
                        ///
                        lblMessage.Text = " > Artículo " + articleToAdd + " adicionado correctamente.";
                    }
                    else
                    {
                        ///
                        MessageBox.Show("El Código Leído ( " + txtArticlesRef.Text + " ) no Corresponde a un Artículo en el Pedido o ya Ha Sido Empacado en Su Totalidad.", "Aquarella - Mensaje De Advertencia",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        /// Adicionar articulo a textarea de errores de lectura
                        this.txtAreaArticlesError.Text += Environment.NewLine + " > Articulo desconocido o código de barras incorrecto - " + txtArticlesRef.Text + " - ";
                        /// Semaforo en rojo, adicion de articulo incompleta
                        changeSemaphore(Brushes.Red);

                    }
                }/// codigo de barras erroneo
                else
                {
                    ///
                    lblMessage.Foreground = Brushes.Maroon;
                    /// Articulo desconocido o codigo de barras incorrecto
                    /// 
                    lblMessage.Text = " > Articulo " + txtArticlesRef.Text + " desconocido o codigo de barras incorrecto !!.";
                    ///
                    MessageBox.Show("Articulo desconocido o codigo de barras incorrecto !!.", "Aquarella - Mensaje De Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    /// Adicionar articulo a textarea de errores de lectura
                    this.txtAreaArticlesError.Text += Environment.NewLine + " > Articulo desconocido o código de barras incorrecto - " + txtArticlesRef.Text + " - ";

                    /// Semaforo en rojo, adicion de articulo incompleta
                    changeSemaphore(Brushes.Red);
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 
        /// </summary>
        public void fillGridArtsRemainingForPack()
        {
            ///
            try
            {
                _LiquDtlArtsRemaining = new Liquidation_DtlViewModel();
                ///
                _ocLiqDtlArtsRemaining = _LiquDtlArtsRemaining.getArtSizesLiquiNotPacking( _noLiquidation, txtArticlesRef.Text);
                ///
                this.dgArtsRemainingForPack.ItemsSource = _ocLiqDtlArtsRemaining;
                /// Succes
                this.changeSemaphore(Brushes.GreenYellow);
            }
            catch
            {
                ///
                this.changeSemaphore(Brushes.Salmon);
                ///
                MessageBox.Show("Ha ocurrido un error en la carga de los articulos en la grilla de resultados, por favor verifique el código digitado.",
                    "Aquarella - Mensaje De Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #endregion

        #region < TOTALIZACION DE CANTIDADES EMPACADAS >

        /// <summary>
        /// Totaliza las cantidades de articulos empacados, cantidades en el paquete actual y cantidades
        /// restantes por empacar
        /// </summary>
        public void calculateQtyTotals()
        {
            this.packagesGlobals();
            ///
            Decimal qtyLiq = this._ocLiqHdr.First<Liquidation_Hdr>()._qtystotals;
            Decimal qtyPack = _ocLiqDtl.Sum(x => x._ldn_pqty);
            Decimal qtysInActualPackage = _ocPackDtl.Sum(x => x._pdn_qty);

            ///
            this.lblQtysInActualPackage.Text = qtysInActualPackage.ToString();
            ///
            this.lblTotalQtysPack.Text = qtyPack.ToString();
            /// 
            this.lblTotalQtysInLiq.Text = qtyLiq.ToString();
            ///
            this.lblQtysRemainingForPack.Text = (qtyLiq - qtyPack).ToString();
        }

        /// <summary>
        /// Calcula las unidades empacadas en un determinado paquete, ademas del número del paquete
        /// </summary>
        public void packagesGlobals()
        {
            List<Packages> packages = (
            from p in _ocLiqDtl
            group p by new { p._ldn_pdn_packageid, p._ldn_pan_no } into g ///._ldn_pdn_packageid into g            
            select new Packages
            {
                _pdn_packageid = g.Key._ldn_pdn_packageid,
                _pan_no = g.Key._ldn_pan_no.ToString(),
                _pan_qty_total = g.Sum(p => p._ldn_pqty),

            }).ToList<Packages>();
            ///var result = _ocLiqDtl.GroupBy(x => x._ldn_pdn_packageid, x => x._ldn_pan_no);
            this.dgInfoPackages.ItemsSource = packages;
        }

        #endregion

        #region < SEMAFORO >

        /// <summary>
        /// 
        /// </summary>
        public void changeSemaphoreG()
        {
            this.spSemaphore.Background = Brushes.Green;
        }

        /// <summary>
        /// 
        /// </summary>
        public void changeSemaphoreY()
        {
            this.spSemaphore.Background = Brushes.Yellow;
        }

        public void changeSemaphore(SolidColorBrush color)
        {
            this.spSemaphore.Background = color;
        }


        #endregion

        /// <summary>
        /// Boton de creacion de un nuevo paquete y finalizacion del anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btNewPackage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!txtPackWeight.Text.Equals(String.Empty) && Utilities.isNumeric(txtPackWeight.Text))
                {
                    /// actualizar el peso del paquete antes de finalizarlo
                    String resp = this.updatePackageWeigth();
                    ///
                    if (!resp.Equals("-1"))
                    {
                        ///
                        _PackNo = -1;
                        /// Iniciar todo de nuevo.
                        this.startObjects();
                        /// Llamar la funcion que cargara todo deacuerdo al nuevo paqute
                        this.loadInfoData();
                        ///
                        this.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(loadAllGrids));
                        ///
                        lblMessage.Text = " > Nuevo paquete creado.";
                        ///
                        txtArticlesRef.Select(0, txtArticlesRef.Text.Length);
                        txtArticlesRef.Focus();
                    }
                    else
                    {
                        /// Problemas en la actualizacion del paquete
                        lblMessage.Foreground = Brushes.Maroon;
                        /// 
                        lblMessage.Text = " > Ha ocurrido un problema en la actualización del peso del paquete.";
                        ///
                        MessageBox.Show("Ha ocurrido un problema en la actualización del peso del paquete.",
                            ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
                else
                {
                    /// No hay peso del paquete
                    lblMessage.Foreground = Brushes.Maroon;
                    /// 
                    lblMessage.Text = " > Por favor digite el peso aproximado del paquete; sólo números.";
                    ///
                    MessageBox.Show("Por favor digite el peso aproximado del paquete; sólo números.",
                        ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                /// Error en la generacion de un nuevo paquete
                lblMessage.Foreground = Brushes.Maroon;
                /// 
                lblMessage.Text = " > Ha ocurrido un problema y no se ha podido crear el nuevo pauqte; se recomienda reiniciar.";
                ///
                MessageBox.Show("Ha ocurrido un problema y no se ha podido crear el nuevo pauqte; se recomienda reiniciar.", ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
        }

        /// <summary>
        /// Facturar el pedido, con todos los paquetes se empacaron
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                /// 1ero. Verificar que existan articulos empacados
                if (_ocLiqDtl != null && _ocLiqDtl.Count > 0)
                {
                    /// 2do. Verificar el peso del paquete
                    if (!txtPackWeight.Text.Equals(String.Empty) && Utilities.isNumeric(txtPackWeight.Text))
                    {
                        /// 3ero. Verificar que el pedido posea guia y transportadora
                        ///                     
                        if (!_ocTransGuides.First<Transporters_Guides>()._tgv_guide.Equals(String.Empty) && !_ocTransGuides.First<Transporters_Guides>()._tgv_transport.Equals(String.Empty))
                        {
                            /// actualizar el peso del paquete antes de finalizarlo
                            String resp = this.updatePackageWeigth();
                            ///
                            if (!resp.Equals("-1"))
                            {
                                /// Really invoice
                                MessageBoxResult msbResult = MessageBox.Show("¿Realmente desea FACTURAR este pedido ? ",
                                "AQUARELLA - Mensaje De Advertencia", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                                ///
                                if (msbResult == MessageBoxResult.OK)
                                {
                                    /// 4to.
                                    this.invoice();
                                }
                                ///btInvoice.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(loadInfoData));
                                ///btInvoice.IsEnabled = false;            
                            }
                            else
                            {
                                /// Problemas en la actualizacion del paquete
                                lblMessage.Foreground = Brushes.Maroon;
                                /// 
                                lblMessage.Text = " > Ha ocurrido un problema en la actualización del peso del paquete.";
                                ///
                                MessageBox.Show("Ha ocurrido un problema en la actualización del peso del paquete.",
                                    ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                        else
                        {
                            /// No hay guia
                            lblMessage.Foreground = Brushes.Maroon;
                            /// 
                            lblMessage.Text = " > No puede facturar un pedido sin guia de transporte asociada, por favor tramite la transportadora y el no. de guia.";
                            ///
                            MessageBox.Show("No puede facturar un pedido sin guia de transporte asociada, por favor tramite la transportadora y el no. de guia.",
                                ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }
                    else
                    {
                        /// No hay peso del paquete
                        lblMessage.Foreground = Brushes.Maroon;
                        /// 
                        lblMessage.Text = " > Por favor digite el peso aproximado del paquete; sólo números.";
                        ///
                        MessageBox.Show("Por favor digite el peso aproximado del paquete; sólo números.",
                            ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }

                }
                else
                {
                    /// Error en la generacion de un nuevo paquete
                    lblMessage.Foreground = Brushes.Maroon;
                    /// 
                    lblMessage.Text = " > No puede facturar un pedido sin ningun artículo empacado.";
                    ///
                    MessageBox.Show("No puede facturar un pedido sin ningun artículo empacado.", ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch
            {
                /// Problemas en la generacion de la factura o en la evaluacion de campos requeridos para facturacion
                lblMessage.Foreground = Brushes.Maroon;
                /// 
                lblMessage.Text += " > Ha ocurrido un problema y no se ha podido facturar.";
                ///
                MessageBox.Show("Ha ocurrido un problema y no se ha podido facturar.",
                    ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }


        /// <summary>
        /// Actualizar el peso final del paquete
        /// </summary>
        /// <returns></returns>
        public String updatePackageWeigth()
        {
            /// Peso
            Decimal packageWeigth = Convert.ToDecimal(txtPackWeight.Text);
            /// Realizar la actualizacion
            String resp = _packagesVM.updatePackageWeigth(Liquidation_Hdr._liq_id);
            ///
            return resp;
        }


        /// <summary>
        /// Realizar facturacion
        /// </summary>
        /// <returns></returns>
        public void invoice()
        {
            /// Instanciar
            _InvHdrVM = new Invoice_HdrViewModel();

            /// 1ero. Verificar el tipo de cliente, si es CEDI realizar traspaso, si es cliente realizar factura
            /// 
            if (!_CustomerType.Equals(_typeCoordCedi))
            {
                /// Coordinador tipo cliente comun
                /// 

                string grabar_numerodoc = Venta.insertar_venta(Liquidation_Hdr._liq_id);
                //String newNoInvoice = _InvHdrVM.doInvoice(_noLiquidation,0);               
                /// 
                if (!grabar_numerodoc.Equals("-1"))
                {
                    /// 
                    lblMessage.Text = " > Factura generada con exito - Número : " + grabar_numerodoc + ".";
                    ///
                    this.afterInvoice();
                    try
                    {

                        string _codigo_hash = "";
                        string _error = "";
                        Facturacion_Electronica.ejecutar_factura_electronica(Basico.Left(grabar_numerodoc, 1), grabar_numerodoc, ref _codigo_hash, ref _error);
                      
                     

                        if (_error.Length > 0)
                        {
                            MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }

                        //EN ESTE PASO VAMOS A GRABAR EL CODIGO HASH
                        Facturacion_Electronica.insertar_codigo_hash(grabar_numerodoc, _codigo_hash, "V");
                        ///
                        // es para enviar a imprimir a la ticketera
                        ReportInvoiceWindow._idv_invoice = grabar_numerodoc;
                        ReportInvoiceWindow rpI = new ReportInvoiceWindow();
                        rpI.Show();                        

                        try
                        {
                            int varOriginal = 1;
                            string noInvoice = grabar_numerodoc.ToUpper();
                            string _genera_tk = Impresora_Epson.Config_Imp.GenerarTicketFact(grabar_numerodoc, 1, _codigo_hash);

                            if (_genera_tk == null)
                            {
                                lblMessageErrTick.Text = " >> Se producjo un error en la impresion del ticket";
                            }
                            else
                            {
                                lblMessageErrTick.Text = " > Ticket Generado con exito";
                            }
                         
                           
                        }
                        catch (Exception ex)
                        {
                            lblMessageErrTick.Text = ex.Message;
                        }

                    }
                    catch
                    {
                        lblMessage.Text += " > Imposible generar reporte local; se enviara a { Aquarella } Web, espere por favor.";                       
                    }


                }
                else
                {
                    this.afterInvoice();
                    /// Problemas en la generacion de la factura
                    lblMessage.Foreground = Brushes.Maroon;
                    /// 
                    lblMessage.Text = " > Ha ocurrido un problema y no se ha podido generar la factura.";
                    ///
                    MessageBox.Show("Ha ocurrido un problema y no se ha podido generar la factura.",
                        ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            else if (_CustomerType.Equals(_typeCoordCedi))
            {
                /// Coordinador tipo CEDI
                /// REALIZAR UN TRASPASO DE BODEGA NO UNA FACTURA
                _WhtranVM = new WhtransfersViewModel();
                /// 0 documento de salida, 1 documento de entrada
                String[] trax = new String[2];
                ///
                trax = _WhtranVM.saveWhTransference( _noLiquidation);
                ///
                if (trax != null && trax.Length > 0)
                {
                    /// 
                    lblMessage.Text = " > Factura generada con exito - Número : " + trax[0] + ".";
                    ///
                    this.afterInvoice();                   

                }
                else
                {
                    this.afterInvoice();
                    /// Problemas en la generacion de la factura
                    lblMessage.Foreground = Brushes.Maroon;
                    /// 
                    lblMessage.Text = " > Ha ocurrido un problema y no se ha podido generar la factura.";
                    ///
                    MessageBox.Show("Ha ocurrido un problema y no se ha podido generar la factura.",
                        ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            ///return null;
        }

        /// <summary>
        /// Despues de facturado inhabilitar controles
        /// </summary>
        public void afterInvoice()
        {
            this.btInvoice.IsEnabled = false;
            this.btNewPackage.IsEnabled = false;
            this.btSearchArt.IsEnabled = false;
            this.txtArticlesRef.IsEnabled = false;
            this.txtPackWeight.IsEnabled = false;
            this.dgArtsInActualPackage.ItemsSource = new ObservableCollection<Packages_Dtl>();
        }

        /// <summary>
        /// Bloquear todo lo asociado a empacar, solo dejar el boton de facturacion.
        /// </summary>
        public void blockFieldsPack()
        {
            this.btNewPackage.IsEnabled = false;
            this.btSearchArt.IsEnabled = false;
            this.txtArticlesRef.IsEnabled = false;
            this.txtPackWeight.IsEnabled = false;
            this.dgArtsInActualPackage.ItemsSource = new ObservableCollection<Packages_Dtl>();
        }

        /// <summary>
        /// Volver al panel de liquidaciones en espera de facturacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBackPanelLiq_Click(object sender, RoutedEventArgs e)
        {
            ///
            PanelLiquidationForInvoiceWindow panelLiqW = new PanelLiquidationForInvoiceWindow();
            ///
            panelLiqW.PanelLiquidationForInvoiceWindowStart(_user);
            ///
            panelLiqW.Show();
            ///
            this.Close();
        }

        /// <summary>
        /// Cerrar sesion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCloseSesion_Click(object sender, RoutedEventArgs e)
        {
            ///
            MainAQWindow maw = new MainAQWindow();
            ///
            this.Close();
            ///
            maw.Show();
        }
        private Int32 valida_facturar()
        {
            Int32 validaf = 0;
            if (_guia.Length == 0)
            {
                lblMessage.Foreground = Brushes.Maroon;
                /// 
                lblMessage.Text = " > Por favor configure su guia de remision.";
                ///
                MessageBox.Show("Configure su numero de guia de remision.", ValuesDB.captionHeaderErrorWindow, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                validaf = 1;
                return validaf;
            }
            return validaf;
        }
    }
}

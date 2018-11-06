using CapaDato.Bll.Admonred;
using CapaDato.Bll.Util;
using CapaDato.Bll.Venta;
using CapaEntidad.Bll.Util;
using CapaEntidad.Bll.Venta;
using Epson_Ticket;
using Integrado.Bll;
using Integrado.Design.WPF_Master;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Threading;

namespace Integrado.Sistemas.Ventas
{
    /// <summary>
    /// Lógica de interacción para FacturacionDirecta.xaml
    /// </summary>
    public partial class FacturacionDirecta : MetroWindow
    {
        #region<REGION DE PANTALLA VENTA>
      
        private ObservableCollection<Ent_Venta> venta_det_list = null;
        private DataTable venta_det_dt = null;
        private Dat_ClienteVenta datclie_ven = null;
        private Decimal igv_monto = 0;
        private decimal percep_monto = 0;
        private decimal percep_monto_real = 0;
        private decimal _total_pago_nc = 0;
        private Boolean comision_calcula = true;
        private List<Ent_Venta_PagoNota> lista_pago_nc_grid = null;
        private List<Ent_Venta_PagoNota> lista_pago_nc = null; 
        //private List<Ent_Venta_PagoNota> forma_nc = null;
        public FacturacionDirecta()
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

            this.Title = "PAGINA DE FACTURACION DIRECTA [" + Ent_Global._nom_modulo + "]";
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
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            InicioWindows();
        }
        private void InicioWindows()
        {
            MetroDialogOptions.ColorScheme = MetroDialogColorScheme.Accented;
            inicio();
        }
        private void limpiar_general()
        {
            btnnc.IsEnabled = false;
            lista_pago_nc = null;
            lista_pago_nc_grid = null;
            comision_calcula = true;
            _total_pago_nc = 0;
            igv_monto = 0;
            percep_monto = 0;
            percep_monto_real = 0;
            datclie_ven = null;
            lblmensaje.Content = "";
            pnlarticulo.Background = Brushes.Transparent;
            pnlarticulo.Visibility = Visibility.Hidden;
            txtbuscarcli.Text = "";
            lbldniruc.Content = "";
            lblnombres.Content = "";
            lbldireccion.Content = "";
            lbltipcli.Content = "";
            lbltelefono.Content = "";
            lblemail.Content = "";
            grdoc.Header = "Tipo de Documento";
            lblndoc.Content = "";
            txtarticulo.Text = "";
            chkbarra.IsChecked = true;
            lblinfo.Content = "";
            lblcantidad.Content = "0";
            lblsubtotal.Content =string.Format("{0:C2}",0);
            lbligv.Content= string.Format("{0:C2}", 0);
            lblnc.Content = string.Format("{0:C2}", 0);
            lbltotal.Content= string.Format("{0:C2}", 0);
            lblper.Content= string.Format("{0:C2}", 0);
            lbltpagar.Content= string.Format("{0:C2}", 0);
            crear_tabla();
             //ObservableCollection<Ent_Venta>();
            refreshgrilla();
        }
        private void calcular_totales()
        {
            lblcantidad.Content = "0";
            lblsubtotal.Content = string.Format("{0:C2}", 0);
            lbligv.Content = string.Format("{0:C2}", 0);
            //lblnc.Content = "S/.0.00";
            lbltotal.Content = string.Format("{0:C2}", 0);
            lblper.Content = string.Format("{0:C2}", 0);
            lbltpagar.Content = string.Format("{0:C2}", 0);
            if (venta_det_list!=null)
            {
                Boolean _valida_percepcion = true;
                var var = venta_det_list.Where(b => b.afec_percepcion == "0");
                if (var.Count() > 0) _valida_percepcion = false;
                
                

                var total_par = venta_det_list.Sum(t => t.total_pares);
                var sub_total = venta_det_list.Sum(s => s.total_linea);
                var tcomision = venta_det_list.Sum(c => c.comision);

                comision_calcula = (tcomision == 0) ? false:true ;

                lblcantidad.Content = total_par;                
                lblsubtotal.Content = string.Format("{0:C2}", sub_total);

                igv_monto = Math.Round(sub_total * (Ent_Global._igv / 100),2,MidpointRounding.AwayFromZero);
                lbligv.Content= string.Format("{0:C2}", igv_monto);


                percep_monto_real = (_valida_percepcion) ? (Math.Round((sub_total + igv_monto) * (Ent_Global._percepcion / 100), 2, MidpointRounding.AwayFromZero)) : 0;

                Decimal total_monto = (sub_total + igv_monto) - _total_pago_nc;

                lbltotal.Content= string.Format("{0:C2}", total_monto);

                percep_monto = (_valida_percepcion)? (Math.Round(total_monto * (Ent_Global._percepcion / 100), 2, MidpointRounding.AwayFromZero)):0;

                lblper.Content= string.Format("{0:C2}", percep_monto);

                decimal total_pagar =Math.Round(total_monto + percep_monto,2,MidpointRounding.AwayFromZero);


                _total_pagar = total_pagar;

                lbltpagar.Content = string.Format("{0:C2}", total_pagar); ;
            }
        }
        private void crear_tabla()
        {
            venta_det_dt = new DataTable();
            venta_det_dt.Columns.Add("articulo", typeof(string));
            venta_det_dt.Columns.Add("marca", typeof(string));
            venta_det_dt.Columns.Add("color", typeof(string));
            venta_det_dt.Columns.Add("foto", typeof(string));
            venta_det_dt.Columns.Add("comision_bol", typeof(Boolean));
            venta_det_dt.Columns.Add("comision_monto", typeof(Decimal));
            venta_det_dt.Columns.Add("precio", typeof(decimal));
            venta_det_dt.Columns.Add("talla", typeof(string));
            venta_det_dt.Columns.Add("cantidad", typeof(decimal));
            venta_det_dt.Columns.Add("stock", typeof(decimal));
            venta_det_dt.Columns.Add("ofe_id", typeof(decimal));
            venta_det_dt.Columns.Add("ofe_maxPares", typeof(decimal));
            venta_det_dt.Columns.Add("ofe_porc", typeof(decimal));
            venta_det_dt.Columns.Add("afec_percepcion", typeof(string));
        }

        public class ent_oferta
        {
            public int ofertaId { get; set; }
            public int residuo { get; set; }
        }
        private void refreshgrilla()
        {
             venta_det_list=new ObservableCollection<Ent_Venta>();
            if (venta_det_dt!=null)
            {
                var querydisarticulo = from dfila in venta_det_dt.AsEnumerable()
                                       group dfila by new
                                       {
                                           articulo = dfila["articulo"],
                                           marca = dfila["marca"],
                                           color = dfila["color"],
                                           foto=dfila["foto"],
                                           afec_percepcion= dfila["Afec_Percepcion"],
                                           ofe_id = dfila["ofe_id"],
                                           ofe_maxPares = dfila["ofe_maxPares"],
                                           ofe_porc = dfila["ofe_porc"],
                                       }
                                       into g
                                       select new
                                       {
                                           articulo =g.Key.articulo,
                                           marca = g.Key.marca,
                                           color =g.Key.color,
                                           foto=g.Key.foto,
                                           afec_percepcion=g.Key.afec_percepcion,
                                           ofe_id = g.Key.ofe_id,
                                           ofe_maxPares = g.Key.ofe_maxPares,
                                           ofe_porc = g.Key.ofe_porc,
                                           comisionmonto = g.Sum(com => com.Field<Decimal>("comision_monto")),
                                           precioavg = g.Average(avg => avg.Field<Decimal>("precio")),
                                                                               
                                       };

                //var query
                
                var entobj = new { Id = 0, Name = "" };
                List<ent_oferta> listOfe = new List<ent_oferta>();

                foreach (var row in querydisarticulo)
                {
                    string _art = row.articulo.ToString();
                    int nroDescuento = 0;
                    List<Ent_Venta_Talla> talla_arti = new List<Ent_Venta_Talla>();
                    talla_arti = ((from myfila in venta_det_dt.AsEnumerable()
                                   where myfila.Field<string>("articulo") == _art
                                   select new Ent_Venta_Talla
                                   {
                                        talla = myfila["talla"].ToString(),
                                        cantidad = Convert.ToDecimal(myfila["cantidad"]),
                                        stock=Convert.ToDecimal(myfila["stock"]),
                                   })).ToList<Ent_Venta_Talla>();

                    talla_arti = talla_arti.OrderBy(d => d.talla).ToList<Ent_Venta_Talla>();

                    Decimal _total_pares = talla_arti.Sum(d => d.cantidad);
                    Decimal _total_linea = (_total_pares * row.precioavg) - row.comisionmonto;
                    Decimal _ofeId = Convert.ToDecimal(row.ofe_id);
                                        
                    Decimal _ofePar = Convert.ToDecimal(row.ofe_maxPares);
                    Decimal _ofePrc = Convert.ToDecimal(row.ofe_porc);
                    Decimal _TotalDes = Convert.ToDecimal("0.00");
                   

                    if (_ofeId >0) {
                  
                        Decimal Residuo = Convert.ToDecimal("0.00");
                       foreach (ent_oferta ent in listOfe)
                        {
                            if (Convert.ToDecimal(ent.ofertaId) == Convert.ToDecimal(_ofeId)){
                                Residuo = Convert.ToDecimal(ent.residuo);
                            }
                        }

                        if (Residuo == 0) {
                            ent_oferta objEnt = new ent_oferta();
                            objEnt.ofertaId = Convert.ToInt32(_ofeId);
                            objEnt.residuo = Convert.ToInt32(Residuo);
                            listOfe.Add(objEnt);
                        }

                        Decimal TotalP = _total_pares + Residuo;

                        if (TotalP >= _ofePar)
                        {
                            nroDescuento = Decimal.ToInt32(TotalP) / Decimal.ToInt32(_ofePar);
                            Residuo = TotalP % _ofePar;
                            _TotalDes = ((nroDescuento * _ofePrc) * (row.precioavg-(Convert.ToDecimal(row.comisionmonto) / Convert.ToDecimal(_total_pares)))) / 100;

                            if (_ofePrc == 100) {
                                _TotalDes = Convert.ToDecimal(_TotalDes) / Convert.ToDecimal(2); //Convert.ToDecimal(_ofePar);
                                int auxParesDes = Convert.ToInt32(_ofePar);

                                foreach (Ent_Venta venta in venta_det_list)
                                {
                                    if (venta.ofe_id == _ofeId && venta.total_descto == 0){

                                        venta.total_descto = _TotalDes;
                                        venta.ofe_nroItem = 1;
                                        nroDescuento = 1;
                                    }

                                }

                            }


                            _total_linea = _total_linea - _TotalDes;
                        }
                        else {
                            Residuo = TotalP;
                        }

                        foreach (ent_oferta ent in listOfe)
                        {
                            if (Convert.ToDecimal(ent.ofertaId) == Convert.ToDecimal(_ofeId)){
                                ent.residuo= Convert.ToInt32(Residuo);
                            }
                        }
                    }

                    venta_det_list.Add(new Ent_Venta
                    {
                        articulo = row.articulo.ToString(),
                        marca = row.marca.ToString(),
                        color = row.color.ToString(),
                        foto = row.foto.ToString(),
                        articulo_talla = talla_arti,
                        precio = row.precioavg,
                        total_pares = _total_pares,
                        comision = row.comisionmonto,
                        total_linea = _total_linea,
                        afec_percepcion = row.afec_percepcion.ToString(),
                        ofe_id = Convert.ToDecimal(row.ofe_id),
                        ofe_maxPares = Convert.ToDecimal(row.ofe_maxPares),
                        ofe_porc = Convert.ToDecimal(row.ofe_porc),
                        total_descto = _TotalDes,
                        ofe_nroItem = nroDescuento,

                    }
                    );
                }
            }
            calcular_totales();
            dg0.ItemsSource = venta_det_list;
        }
        private void agrega_items(string articulo, string marca, string color,string foto,Boolean comi_bool,string afec_percepcion, decimal precio,List<Ent_Venta_Talla> tallas_list,
            Boolean _edit=false, decimal ofe_id = 0, decimal ofe_MaxPares=0, decimal Ofe_Porce=0)
        {
            if (venta_det_dt != null)
            {

                /*en este caso si se esta editando las tallas*/
                if (_edit)
                {
                    DataRow[] fila_deleted = venta_det_dt.Select("articulo='" + articulo + "'");
                    foreach(DataRow fila_del in fila_deleted)
                    {
                        venta_det_dt.Rows.Remove(fila_del);
                    }
                    
                }
                
                foreach(Ent_Venta_Talla fila_talla in tallas_list)
                {
                    string _talla = fila_talla.talla;
                    decimal _cant = fila_talla.cantidad;
                    Decimal _stock = fila_talla.stock;
                    decimal _comi_monto = 0;
                    decimal _subtotal = 0;

                    DataRow[] fila_exists = venta_det_dt.Select("articulo='" + articulo + "' and talla='" + _talla + "'");
                    if (fila_exists.Length==0)
                    {
                   
                        _subtotal = (precio * _cant);
                        _comi_monto =(afec_percepcion== "1") ? Math.Round((_subtotal * (Ent_Global._comision_porc / 100)), 2, MidpointRounding.AwayFromZero):0;
                        venta_det_dt.Rows.Add(articulo, marca, color,foto, comi_bool, _comi_monto, precio, _talla, _cant, _stock, ofe_id, ofe_MaxPares, Ofe_Porce, afec_percepcion);
                    }
                    else
                    {
                        for(Int32 a=0;a<venta_det_dt.Rows.Count;++a)
                        {
                            string cod_art_g = venta_det_dt.Rows[a]["articulo"].ToString();
                            string cod_tal_g= venta_det_dt.Rows[a]["talla"].ToString();

                            if (cod_art_g==articulo && cod_tal_g==_talla)
                            {
                                 venta_det_dt.Rows[a]["cantidad"]=Convert.ToDecimal(venta_det_dt.Rows[a]["cantidad"])  + _cant;
                               
                                _subtotal = (precio * Convert.ToDecimal(venta_det_dt.Rows[a]["cantidad"]));
                                _comi_monto =(afec_percepcion == "1") ?Math.Round((_subtotal * (Ent_Global._comision_porc / 100)), 2, MidpointRounding.AwayFromZero):0;
                                venta_det_dt.Rows[a]["comision_monto"] = Convert.ToDecimal(_comi_monto);
                            }
                        }
                    }
                    
                }                
            }
        }
        private void inicio()
        {

            llenar_combo_pago();
            limpiar_general();
            txtbuscarcli.Focus();
        }
        private void limpiardataclie()
        {
            btnnc.IsEnabled = false;
            lista_pago_nc_grid =null;
            lista_pago_nc = null;
            _total_pago_nc = 0;
            //forma_nc = null;
            lbldniruc.Content = "";
            lblnombres.Content = "";
            lbldireccion.Content = "";
            lbltipcli.Content = "";
            lbltelefono.Content = "";
            lblemail.Content = "";
            grdoc.Header = "Tipo de Documento";
            lblndoc.Content = "";
        }

        private void recalcular_precio_lider(string tipo_precio)
        {
            try
            {
                if (venta_det_dt!=null)
                {
                    if (venta_det_dt.Rows.Count>0)
                    {
                        for(Int32 i=0;i<venta_det_dt.Rows.Count;++i)
                        {
                            string codart = venta_det_dt.Rows[i]["articulo"].ToString();
                            decimal precio=Dat_Venta.getprecio_sinigv(codart, tipo_precio);

                            venta_det_dt.Rows[i]["precio"] = precio;

                        }

                        /*refresh grilla*/
                        refreshgrilla();

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async void buscarcliente()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            string dniruc = txtbuscarcli.Text.Trim();
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            try
            {
                //limpiardataclie();
                datclie_ven = new Dat_ClienteVenta(dniruc);
                if (datclie_ven.existe_cli)
                {

                    #region<VERIFICAR SI ES QUE HAY DATOS EN EL DETALLE PARA RECALCULAR PRECIOS>
                    recalcular_precio_lider(datclie_ven.tipoprecio);
                    
                    #endregion

                    Dat_Venta_Directa dat_formanc = new Dat_Venta_Directa();
                    lista_pago_nc_grid = dat_formanc.leer_formapago_nota(datclie_ven.bas_id);

                    if (lista_pago_nc_grid!=null)
                    {
                        if (lista_pago_nc_grid.Count>0)
                        {
                            btnnc.IsEnabled = true;
                        }
                    }

                    lbldniruc.Content = datclie_ven.dniruccli;
                    lblnombres.Content = datclie_ven.nomcli;
                    lbldireccion.Content = datclie_ven.direccion;
                    lbltipcli.Content = datclie_ven.tipocli;
                    lbltelefono.Content = datclie_ven.telefono;
                    lblemail.Content = datclie_ven.email;
                    grdoc.Header = datclie_ven.tipodoc;
                    lblndoc.Content =datclie_ven.sernum;
                    txtarticulo.Focus();
                }   
                else
                {
                    //MessageBox.Show("El numero de dni no existe en nuestra base de datos..", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "El numero de dni no existe en nuestra base de datos..", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                    txtbuscarcli.Focus();
                }             
            }
            catch(Exception exc)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                txtbuscarcli.Focus();
                //MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private void txtbuscarcli_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
            if (e.Key == Key.Enter)
            {
                btndoc_Click(btndoc, new RoutedEventArgs());
            }
        }

        private async void btndoc_Click(object sender, RoutedEventArgs e)
        {
            limpiardataclie();
            var metroWindow = this;            
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            string _dni_ruc =txtbuscarcli.Text;
            if (_dni_ruc.Length == 0)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Ingrese el numero DNI ó RUC", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                //MessageBox.Show("Ingrese el numero DNI ó RUC",
                //      "Bata - Mensaje De Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtbuscarcli.Focus();
                Mouse.OverrideCursor = null;
                return;
            }

            if (_dni_ruc.Length != 8 && _dni_ruc.Length != 11)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "El numero DNI ó RUC es incorrecto", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                //MessageBox.Show("El numero DNI ó RUC es incorrecto",
                //      "Bata - Mensaje De Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtbuscarcli.Focus();
                Mouse.OverrideCursor = null;
                return;
            }
            buscarcliente();
        }
        private void ToggleFlyout(int index, string _header)
        {
            var flyout = this.Flyouts.Items[index] as Flyout;
            if (flyout == null)
            {
                return;
            }
            flyout.Header = _header;
            flyout.IsOpen = !flyout.IsOpen;
        }

        private decimal cant_tall_agregado_lista(string _articulo,string _talla)
        {
            Decimal _total = 0;

            if (venta_det_dt!=null)
            {
                DataRow[] vfila = venta_det_dt.Select("articulo='" + _articulo + "' and talla='" + _talla + "'");

                if (vfila.Length>0)
                {
                    var tot = vfila.Sum(s => s.Field<Decimal>("cantidad"));
                    _total = tot;
                }

            }

            return _total;
        }
        private async void paqartcodbarra()
        {
            try
            {
                var metroWindow = this;
                pnlarticulo.Visibility = Visibility.Collapsed;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                lblmensaje.Content = "";
                if (txtarticulo.Text.Trim().Length == 0)
                {
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Ingrese el codigo de articulo...", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                    pnlarticulo.Background = Brushes.Red;
                    lblmensaje.Content = " > Ingrese el codigo de articulo....";
                    pnlarticulo.Visibility = Visibility.Visible;
                    txtarticulo.Focus();
                    return;
                }

                //en este caso vamos a digitar la calidad
                //string v_articulo = txtarticulo.Text.Substring(0,txtarticulo.Text.Length-1);

                string v_articulo = txtarticulo.Text.Trim();
                //
                string _barra = (txtarticulo.Text.Trim().Length == 18) ? txtarticulo.Text.Trim() : "";


                string[] info_articulo = (_barra.Length==18)? Ent_BarCodes.getInfoFromTheBarCode(v_articulo):null;

                if ((info_articulo != null && info_articulo.Length > 0) || (v_articulo.Length==7))
                {


                    String sizeToAdd = (info_articulo==null)? "": info_articulo[1];
                    /// Article 
                    String articleToAdd = (info_articulo==null)?v_articulo: info_articulo[0];

                    String calidadToAdd = (info_articulo==null)?"": info_articulo[2];

                    Dat_Venta bus_stk =new Dat_Venta();

                    ObservableCollection<Dat_Venta> articulo_stock_var=null;

                    decimal bas_id = 0;
                    if (datclie_ven!=null)
                    {
                        bas_id = datclie_ven.bas_id;
                    }
                    //if datcl
                    //datclie_ven.bas_id

                    Boolean valida_stock=bus_stk.BuscarProductoStock(articleToAdd, sizeToAdd, _barra, (_barra.Length==18)?true:false, bas_id, ref articulo_stock_var);

                    //string varreturn = Dat_Venta.insertar_articulopaq(_paq_id, _liq_id, articleToAdd, sizeToAdd, 1, calidadToAdd, _barra);

                    if (valida_stock)
                    {
                        //var metroWindow = this;
                        if (_barra.Length != 18)
                        { 
                            metroWindow.LeftWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                            metroWindow.RightWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                            metroWindow.WindowButtonCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                            metroWindow.IconOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;    
                            
                            if (venta_det_dt!=null)
                            { 
                                if (venta_det_dt.Rows.Count>0)
                                {
                                    ObservableCollection<Dat_Venta> articulo_stock_copy = articulo_stock_var;

                                    foreach (Dat_Venta dat_cab in articulo_stock_var)
                                    {
                                        string _articulo = dat_cab.articulo_venta.ToString();
                                        foreach(Vent_Talla_Cant talla_list in dat_cab.ven_tall)
                                        {
                                            string _talla = talla_list._talla;
                                            decimal cant_select = cant_tall_agregado_lista(_articulo, _talla);
                                            /*refresh stock de articulo y talla*/
                                            if (cant_select>0)
                                            {                                                                                             
                                                talla_list._cant = talla_list._cant - cant_select;                                             
                                            }

                                        }
                                    }
                                }
                            }

                            string rutafot= articulo_stock_var[0].articulo_foto.ToString();

                            imgfoto.Source = new BitmapImage(new Uri(rutafot));                         

                            dg1.ItemsSource = from c in articulo_stock_var
                                              select new Dat_Venta
                                              {
                                                  articulo_venta = c.articulo_venta,
                                                  articulo_marca = c.articulo_marca,
                                                  articulo_color = c.articulo_color,
                                                  articulo_ofe_id = c.articulo_ofe_id,
                                                  articulo_ofe_MaxPares = c.articulo_ofe_MaxPares,
                                                  articulo_Ofe_Porce = c.articulo_Ofe_Porce,
                                                  articulo_comi_bool = c.articulo_comi_bool,
                                                  articulo_afec_percepcion=c.articulo_afec_percepcion,
                                                  ven_tall = c.ven_tall.Where(o => o._cant > 0).ToList(),
                                                  preciosinigv = c.preciosinigv,
                                                  preciosigv=c.preciosigv,
                                                  articulo_foto = c.articulo_foto,                                         
                                              };                           

                            this.ToggleFlyout(0, "Ingresar las tallas a vender del articulo ==> " + v_articulo);
                        }
                        else
                        {

                            decimal _saldo = bus_stk.articulo_stock_cant - cant_tall_agregado_lista(bus_stk.articulo_venta, bus_stk.articulo_talla);

                            if (_saldo>0)
                            { 
                                List<Ent_Venta_Talla> ent_talla_list = new List<Ent_Venta_Talla>();
                                Ent_Venta_Talla ent_talla = new Ent_Venta_Talla();
                                ent_talla.talla = bus_stk.articulo_talla;
                                ent_talla.stock = _saldo;
                                ent_talla.cantidad = bus_stk.articulo_cantidad;
                                ent_talla_list.Add(ent_talla);

                                agrega_items(bus_stk.articulo_venta, bus_stk.articulo_marca, bus_stk.articulo_color,bus_stk.articulo_foto, bus_stk.articulo_comi_bool, bus_stk.articulo_afec_percepcion,
                                bus_stk.articulo_precio_sinigv, ent_talla_list);
                                refreshgrilla();
                                pnlarticulo.Visibility = Visibility.Visible;
                                pnlarticulo.Background = Brushes.YellowGreen;
                                lblmensaje.Content = " > Artículo " + articleToAdd + " adicionado correctamente.";
                                txtarticulo.Text = "";
                                txtarticulo.Focus();
                            }
                            else
                            {
                                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "El Código Leído ( " + txtarticulo.Text + " ) ya no hay stock disponible.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                                lblmensaje.Content = " > Artículo " + articleToAdd + " ya no hay stock disponible.";
                                pnlarticulo.Background = Brushes.Red;
                                pnlarticulo.Visibility = Visibility.Visible;
                                txtarticulo.Focus();
                            }
                        }
                    }
                    else
                    {
                       
                        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "El Código Leído ( " + txtarticulo.Text + " ) no Corresponde a un Artículo o ya no hay stock disponible.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                        pnlarticulo.Background = Brushes.Red;
                        pnlarticulo.Visibility = Visibility.Visible;
                        txtarticulo.Focus();
                    }

                }
                else
                {
                    lblmensaje.Foreground = Brushes.Maroon;
                    lblmensaje.Content = " > Articulo " + txtarticulo.Text + " desconocido o codigo de barras incorrecto !!.";
                    await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Articulo desconocido o codigo de barras incorrecto !!.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                    pnlarticulo.Background = Brushes.Red;
                    pnlarticulo.Visibility = Visibility.Visible;
                    txtarticulo.Focus();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void txtarticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                pnlarticulo.Background = Brushes.Yellow;
                lblmensaje.Content = "";               
                btnarticulo_Click(btnarticulo, new RoutedEventArgs());               
                txtarticulo.Focus();
                Mouse.OverrideCursor = null;
            }
        }

        private async void btnagregarart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button!=null)
            {
                var task = button.DataContext as Dat_Venta;
                if (task!=null)
                {
                    var metroWindow = this;
                    var tcant = task.ven_tall.Sum(r=>r._edit_talla);

                    if (tcant==0)
                    {
                        
                        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "No ha ingresado la cantidad para agregar...", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                        return;
                    }
                    else
                    {
                        var filas_edit = task.ven_tall;

                        foreach(var fila in filas_edit.ToList())
                        {
                            decimal stkreal = fila._cant;
                            decimal stkedit = fila._edit_talla;
                            string talla = fila._talla.ToString();                            
                            if (stkedit>stkreal)
                            {
                                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "La cantidad ingresada supero al stock en la talla " + talla, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);                                
                                return;
                            }
                        }

                        List<Ent_Venta_Talla> agr_talla = new List<Ent_Venta_Talla>(); 

                        foreach(var vfila in filas_edit.ToList())
                        {
                            decimal stkedit = vfila._edit_talla;
                            string talla = vfila._talla.ToString();
                            decimal stkcant = vfila._cant;
                            if (stkedit>0)
                            {
                                Ent_Venta_Talla tallaobj = new Ent_Venta_Talla();
                                tallaobj.talla = talla;tallaobj.cantidad = stkedit;tallaobj.stock = stkcant;
                                agr_talla.Add(tallaobj);
                            }
                        }

                        string codart= task.articulo_venta.ToString();
                        string marart= task.articulo_marca.ToString(); 
                        string colart= task.articulo_color.ToString(); 
                        string fotoart = task.articulo_foto.ToString();
                        string afec_percepcion = task.articulo_afec_percepcion.ToString();
                        decimal preart = Convert.ToDecimal(task.preciosinigv);
                        decimal ofe_id = Convert.ToDecimal(task.articulo_ofe_id);
                        decimal ofe_porc = Convert.ToDecimal(task.articulo_Ofe_Porce);
                        decimal ofe_maxPares = Convert.ToDecimal(task.articulo_ofe_MaxPares);
                        Boolean combool = task.articulo_comi_bool;
                        agrega_items(codart, marart, colart,fotoart,combool, afec_percepcion, preart, agr_talla,false, ofe_id, ofe_maxPares, ofe_porc);
                        refreshgrilla();
                        this.ToggleFlyout(0, "");


                    }

                }
            }
        }

        private void txteditcant_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
            {
                e.Handled = true;
            }
        }

        private void btnarticulo_Click(object sender, RoutedEventArgs e)
        {
            paqartcodbarra();
        }

        private async void btneliminar_items_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var task = button.DataContext as Ent_Venta;
                if (task != null)
                {
                    string _articulo = task.articulo.ToString();
                    string _marca = task.marca.ToString();


                    var metroWindow = this;

                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "Si",
                        NegativeButtonText = "No",
                        //FirstAuxiliaryButtonText = "Cancelar",
                        ColorScheme = MetroDialogOptions.ColorScheme,
                    };

                    MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "¿Realmente desea eliminar el artículo : " + _articulo + " en marca : " +
                            _marca + "?",
                        MessageDialogStyle.AffirmativeAndNegative, mySettings);
                    if (result == MessageDialogResult.Affirmative)
                    {
                        DataRow[] fila_deleted = venta_det_dt.Select("articulo='" + _articulo + "'");
                        foreach (DataRow delfila in fila_deleted)
                        {
                            venta_det_dt.Rows.Remove(delfila);
                        }
                        refreshgrilla();
                    }
                     
                }
            }
        }

        private void btneditar_items_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var task = button.DataContext as Ent_Venta;
                if (task != null)
                {
                    var metroWindow = this;
                    metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                    metroWindow.LeftWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                    metroWindow.RightWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                    metroWindow.WindowButtonCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                    metroWindow.IconOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                    string v_articulo =task.articulo;

                    string rutafot =task.foto.ToString();

                    imgfotoedit.Source = new BitmapImage(new Uri(rutafot));

                    ObservableCollection<Dat_Venta> articulos_edit_list = new ObservableCollection<Dat_Venta>();

                    List<Vent_Talla_Cant> talla_arti_list = new List<Vent_Talla_Cant>();
                  

                    List<Ent_Venta_Talla> talla_edit = task.articulo_talla;
                    foreach(Ent_Venta_Talla fila_talla in talla_edit)
                    {
                        string _talla =fila_talla.talla;decimal _cant = fila_talla.cantidad;decimal _stock = fila_talla.stock;

                        Vent_Talla_Cant talla_gregar = new Vent_Talla_Cant();
                        talla_gregar._talla = _talla;
                        talla_gregar._cant = _stock;
                        talla_gregar._edit_talla = _cant;
                        talla_arti_list.Add(talla_gregar);
                    }
                    List<Dat_Venta> articulos_edit = new List<Dat_Venta>();
                    articulos_edit.Add(new Dat_Venta
                    {
                        articulo_venta = task.articulo.ToString(),
                        articulo_marca = task.marca.ToString(),
                        articulo_color = task.color.ToString(),
                        articulo_ofe_id = task.ofe_id,
                        articulo_ofe_MaxPares = task.ofe_maxPares,
                        articulo_Ofe_Porce = task.ofe_porc,

                        articulo_afec_percepcion =task.afec_percepcion.ToString(),
                        preciosinigv = task.precio,
                        articulo_foto = task.foto,
                        ven_tall = talla_arti_list,
                    });
                  

                    dg1edit.ItemsSource = articulos_edit;

                    this.ToggleFlyout(1, "Editanto tallas del articulo==> " + v_articulo);
                }
            }
           

        }

        private async void btneditrart_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var task = button.DataContext as Dat_Venta;
                if (task != null)
                {
                    var metroWindow = this;
                    var tcant = task.ven_tall.Sum(r => r._edit_talla);

                    if (tcant == 0)
                    {
                        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "No ha ingresado la cantidad para editar...", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                        return;
                    }
                    else
                    {
                        var filas_edit = task.ven_tall;

                        foreach (var fila in filas_edit.ToList())
                        {
                            decimal stkreal = fila._cant;
                            decimal stkedit = fila._edit_talla;
                            string talla = fila._talla.ToString();
                            if (stkedit > stkreal)
                            {
                                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "La cantidad ingresada supero al stock en la talla " + talla, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                                return;
                            }
                        }
                        List<Ent_Venta_Talla> agr_talla = new List<Ent_Venta_Talla>();

                        foreach (var vfila in filas_edit.ToList())
                        {
                            decimal stkedit = vfila._edit_talla;
                            string talla = vfila._talla.ToString();
                            decimal stkcant = vfila._cant;
                            if (stkedit > 0)
                            {
                                Ent_Venta_Talla tallaobj = new Ent_Venta_Talla();
                                tallaobj.talla = talla; tallaobj.cantidad = stkedit; tallaobj.stock = stkcant;
                                agr_talla.Add(tallaobj);
                            }
                        }

                        string codart = task.articulo_venta.ToString();
                        string marart = task.articulo_marca.ToString();
                        string colart = task.articulo_color.ToString();
                        string fotoart = task.articulo_foto.ToString();
                        decimal ofe_id = Convert.ToDecimal(task.articulo_ofe_id);
                        decimal ofe_porc = Convert.ToDecimal(task.articulo_Ofe_Porce);
                        decimal ofe_maxPares = Convert.ToDecimal(task.articulo_ofe_MaxPares);
                        decimal preart = Convert.ToDecimal(task.preciosinigv);
                        string afec_percepcion = task.articulo_afec_percepcion.ToString();
                        Boolean combool = task.articulo_comi_bool;
                        agrega_items(codart, marart, colart, fotoart, combool, afec_percepcion, preart, agr_talla,true, ofe_id, ofe_maxPares, ofe_porc);
                        refreshgrilla();
                        this.ToggleFlyout(1, "");

                    }
                }
            }
        }

        private async void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            if (venta_det_dt != null && venta_det_dt.Rows.Count == 0) return;
            
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Esta seguro de cancelar la operacion!",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);
            if (result == MessageDialogResult.Affirmative)
            {
                //cerrar session
                limpiar_general();
                txtbuscarcli.Focus();                
            }
        }
      

        private async void btnefectivo_Click(object sender, RoutedEventArgs e)
        {


            //Ent_Tarjeta tar = new Ent_Tarjeta();
            //tar.idtarjeta = "02";
            //tar.tarnom = "Mastercard";
            //BitmapDecoder bitCoder;
            //string _ruta = "";
            //Microsoft.Win32.OpenFileDialog OD = new Microsoft.Win32.OpenFileDialog();
            //OD.Filter = "Imagenes|*.jpg;*.gif;*.png;*.bmp";
            //if (OD.ShowDialog() == true)
            //{
            //    using (System.IO.Stream stream = OD.OpenFile())
            //    {
            //        bitCoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat,
            //            BitmapCacheOption.OnLoad);
            //        //  imgmodulo.Source = bitCoder.Frames[0];
            //        _ruta = OD.FileName;
            //    }
            //    tar.tarimagen = ReadImageFile(_ruta);

            //    Dat_Tarjeta upd = new Dat_Tarjeta();
            //    upd.insertar(tar);
            //}
            //else
            //{

            //}


            Boolean _valida = await fvalida();

            if (!_valida) return;
            /*si el total a pagar es cero entonces quiere decir que la nota de credito es igual*/
            if (_total_pagar==0)
            {
                inicio_forma_pago();
                factura_directa(true);
            }
            else
            { 

                inicio_forma_pago();
                var metroWindow = this;
                metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
                metroWindow.LeftWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                metroWindow.RightWindowCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                metroWindow.WindowButtonCommandsOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                metroWindow.IconOverlayBehavior = MahApps.Metro.Controls.WindowCommandsOverlayBehavior.Never;
                this.ToggleFlyout(2, "Realizar el pago del documento");

            }
        }
        private async Task<Boolean> fvalida()
        {
           
            Boolean _valida = true;
            string strdni = lbldniruc.Content.ToString();

            pnlarticulo.Visibility = Visibility.Collapsed;
            lblmensaje.Content = "";
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;

            if (strdni.Length==0)
            {
                lblmensaje.Content = " > Por favor ingrese el cliente.";
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Por favor Ingrese el cliente.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                _valida = false;
                pnlarticulo.Background = Brushes.Red;
                pnlarticulo.Visibility = Visibility.Visible;
                txtbuscarcli.Focus();
                return _valida;
            }

            if (venta_det_dt==null || venta_det_dt.Rows.Count==0)
            {
                lblmensaje.Content = " > Ingrese los articulos a vender.";
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "Ingrese los articulos a vender.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                _valida = false;
                pnlarticulo.Background = Brushes.Red;
                pnlarticulo.Visibility = Visibility.Visible;             
                return _valida;
            }
            string strartp = "";
            for (Int32 i = 0; i < venta_det_dt.Rows.Count; ++i)
            {
                if (i == 0)
                {
                    strartp = venta_det_dt.Rows[i]["afec_percepcion"].ToString();
                }
                else
                {
                    if (!(strartp == venta_det_dt.Rows[i]["afec_percepcion"].ToString()))
                    {
                        _valida = false;
                       
                        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "No se puede generar la venta, porque en el detalle hay articulo sin percepcion", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                        return _valida;                      
                    }
                    strartp = venta_det_dt.Rows[i]["afec_percepcion"].ToString();
                }

            }
            if (_total_pagar<0)
            {
                lblmensaje.Content = " > El total de pago de nota de credito debe de ser igual o menor al monto de la compra.";
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "El total de pago de nota de credito debe de ser igual o menor al monto de la compra.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                _valida = false;
                pnlarticulo.Background = Brushes.Red;
                pnlarticulo.Visibility = Visibility.Visible;
                return _valida;
            }

            /*verifica el precio del detalle con cero*/
            /**/
            DataRow[] fila_p = venta_det_dt.Select("precio=0");

            if (fila_p.Length>0)
            {
                lblmensaje.Content = " > El detalle de venta tiene articulos con precio Cero.";
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "El detalle de venta tiene articulos con precio Cero.", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                _valida = false;
                pnlarticulo.Background = Brushes.Red;
                pnlarticulo.Visibility = Visibility.Visible;
                return _valida;
            }

            return _valida;
        }
        #endregion
        #region<REGION DE FORMA DE PAGO>


        private decimal _total_pagar,_total_efectivo,_total_tarjeta,_total_vuelto,_total_saldo = 0;
        private List<Ent_Venta_FormaPago> venta_pagos = null;
        
        private byte[] ReadImageFile(string imageLocation)
        {
            byte[] imageData = null;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(imageLocation);
            long imageFileLength = fileInfo.Length;
            System.IO.FileStream fs = new System.IO.FileStream(imageLocation, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            imageData = br.ReadBytes((int)imageFileLength);
            return imageData;
        }

        private void dwforma_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (dwforma.EditValue.ToString() == "EFE")
                {
                    txtnum1.IsEnabled = false;
                    txtnum2.IsEnabled = false;
                    txtnum3.IsEnabled = false;
                    txtnum4.IsEnabled = false;                 
                    txtmonto.Focus();
                }
                else
                {
                    txtnum1.IsEnabled = true;
                    txtnum2.IsEnabled = true;
                    txtnum3.IsEnabled = true;
                    txtnum4.IsEnabled = true;
                    txtnum1.Focus();
                }
                
            }
        }

        private void llenar_combo_pago()
        {
            /*opciones de tarjeta*/
            Dat_Tarjeta tar_get = new Dat_Tarjeta();
            List<Ent_Tarjeta> list_tar = tar_get.Leer();            
            dwtarjeta.ItemsSource = list_tar;

            /*opciones de forma de pago*/
            Dat_FormaPago form_pago = new Dat_FormaPago();
            List<Ent_FormaPago> list_form = form_pago.select();
            dwforma.ItemsSource = list_form;
        }

        private void calcular_formapago()
        {
            _total_efectivo = venta_pagos.Where(pag=>pag.forma_pago_id=="EFE").Sum(efe => efe.forma_monto);
            _total_tarjeta  = venta_pagos.Where(pag => pag.forma_pago_id == "TAR").Sum(efe => efe.forma_monto);
            _total_vuelto = ((_total_efectivo + _total_tarjeta) - _total_pagar < 0) ? 0 : (_total_efectivo + _total_tarjeta) - _total_pagar;
            _total_saldo = (_total_pagar - (_total_efectivo + _total_tarjeta) < 0) ? 0 : _total_pagar - (_total_efectivo + _total_tarjeta);
        }

        private void agregar_forma(string bines_ser="",string bines_cod="",string bines_des="",string num_tarjeta="")
        {
            lblinforma.Text = "";
            string _forma_pago_id = dwforma.EditValue.ToString();
            string _forma_pago_nombre = dwforma.Text.ToString();

            /*VERIFICAR LA FORMA DE PAGO EFECTIVO O TARJETA*/
            string _tarjeta_bines_ser = "";
            string _tarjeta_bines_cod = "";
            string _tarjeta_nombre = "";
            string _tarjeta_numero = "";
            if (dwforma.EditValue.ToString()!="EFE")
            {
                _tarjeta_bines_ser = bines_ser;
                _tarjeta_bines_cod = bines_cod;
                _tarjeta_nombre = bines_des;
                _tarjeta_numero = num_tarjeta;
            }


            /*validacion de pagoz con tarjeta*/
            Boolean valida_pago_tarjeta = (_forma_pago_id == "TAR" ? true : false);

            if (valida_pago_tarjeta)
            {
                if (_tarjeta_numero.Length==0)
                {
                    lblinforma.Text = ">Debe de ingresar el numero de las tarjeta....";
                    return;
                }
            }

            Decimal _forma_monto = Convert.ToDecimal(txtmonto.Text);


            if (_forma_monto == 0)
            {

                lblinforma.Text = ">El Monto ingresado no puede ser con valor cero....";
            }
            else
            {
                agregar_forma_pago("",_forma_pago_id, _forma_pago_nombre, _tarjeta_bines_ser, _tarjeta_bines_cod, _tarjeta_nombre, _tarjeta_numero, _forma_monto);


                calcular_formapago();

                print_forma_pago();
               

                if (_total_saldo > 0)
                {
                    txtmonto.Text = 0.ToString();
                }
                else
                {
                    txtmonto.IsEnabled = false;
                    btnagregar.IsEnabled = false;
                    dwforma.IsEnabled = false;
                    txtmonto.Text = 0.ToString();
                    btnaceptar.IsEnabled = true;
                    btnaceptar.Focus();
                }

            }
        }

        private void btnagregar_Click(object sender, RoutedEventArgs e)
        {

            agregar_forma();
        }

        private void txtmonto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                btnagregar_Click(btnagregar, new RoutedEventArgs());
            }
        }

        private void btneliminar_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var task = button.DataContext as Ent_Venta_FormaPago;
                if (task != null)
                {                    
                    venta_pagos.Remove(task);
                    ordenar_items();
                    calcular_formapago();
                    print_forma_pago();
                    btnaceptar.IsEnabled = (_total_saldo == 0) ? true:false;
                    if (_total_saldo > 0)
                    {
                        txtmonto.IsEnabled = true;
                        btnagregar.IsEnabled = true;
                        dwforma.IsEnabled = true;
                        txtmonto.Focus();
                    }
                        
                    dgforma.ItemsSource = venta_pagos;
                    dgforma.Items.Refresh();
                }
            }
        }

        private void ordenar_items()
        {
            if (venta_pagos!=null)
            {
                for(Int32 i=0;i<venta_pagos.Count();++i)
                {
                    venta_pagos[i].forma_items = i + 1;
                }
            }
        }

        private void inicio_forma_pago()
        {
            txtmonto.IsEnabled = true;
            btnagregar.IsEnabled = true;
            dwforma.IsEnabled = true;
            btnaceptar.IsEnabled = false;
            txtnum1.IsEnabled = false;
            txtnum2.IsEnabled = false;
            txtnum3.IsEnabled = false;
            txtnum4.IsEnabled = false;
            limpiar_forma_pago();
            calcular_formapago();
            print_forma_pago();
        }
        private void print_forma_pago()
        {
            lbltpagare.Content = string.Format("{0:C2}", _total_pagar);
            lblefectivo.Content = string.Format("{0:C2}", _total_efectivo);
            lbltarjeta.Content = string.Format("{0:C2}", _total_tarjeta);
            lblvuelto.Content = string.Format("{0:C2}", _total_vuelto);
            lblsaldo.Content = string.Format("{0:C2}", _total_saldo);
        }

        private void btnaceptar_Click(object sender, RoutedEventArgs e)
        {
            factura_directa();
        }

        private void btnnc_Click(object sender, RoutedEventArgs e)
        {


            if (lista_pago_nc != null)
            {
                List<Ent_Venta_PagoNota> tmpnota = new List<Ent_Venta_PagoNota>();
                foreach (Ent_Venta_PagoNota r in lista_pago_nc)
                {
                    Ent_Venta_PagoNota it = new Ent_Venta_PagoNota();
                    it.chknota = r.chknota;
                    it.doc_tra_id = r.doc_tra_id;
                    it.nc_num = r.nc_num;
                    it.total_nc = r.total_nc;
                    tmpnota.Add(it);
                }

                if (lista_pago_nc.Count > 0)
                {
                    for (Int32 a = 0; a < lista_pago_nc_grid.Count; ++a)
                    {
                        lista_pago_nc_grid[a].chknota = false;
                    }

                    for (Int32 a = 0; a < lista_pago_nc_grid.Count; ++a)
                    {
                        for (Int32 b = 0; b < tmpnota.Count; ++b)
                        {
                            if (lista_pago_nc_grid[a].doc_tra_id == tmpnota[b].doc_tra_id)
                            {
                                lista_pago_nc_grid[a].chknota = tmpnota[b].chknota;
                                break;
                            }
                        }
                    }
                }

                lista_pago_nc = tmpnota;
            }



            dgformanc.ItemsSource = lista_pago_nc_grid;
            dgformanc.Items.Refresh();


            _total_pago_nc = lista_pago_nc_grid.Where(t => t.chknota == true).Sum(s=>s.total_nc);
            lbltotpagonc.Content = string.Format("{0:C2}", _total_pago_nc);
            this.ToggleFlyout(3, "(Forma de Pago) Nota de Credito ");
        }

        private void chkok_Click(object sender, RoutedEventArgs e)
        {
            var check = sender as CheckBox;
            if (check != null)
            {
                var task = check.DataContext as Ent_Venta_PagoNota;
                if (task != null)
                {

                    for (Int32 i = 0; i < lista_pago_nc_grid.Count(); ++i)
                    {
                        if (task.doc_tra_id == lista_pago_nc_grid[i].doc_tra_id)
                        {
                            lista_pago_nc_grid[i].chknota = task.chknota;
                            break;
                        }
                    }
                   
                    _total_pago_nc = lista_pago_nc_grid.Where(v => v.chknota == true).Sum(s => s.total_nc);
                  
                    lbltotpagonc.Content = string.Format("{0:C2}", _total_pago_nc);
                }
            }
        }

        private void btncancelarnc_Click(object sender, RoutedEventArgs e)
        {
            this.ToggleFlyout(3, "");
        }

        private async void btnaceptarnc_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;

            var tmplista= lista_pago_nc_grid.Where(v => v.chknota == true);
            if (tmplista.Count() == 0)
            {
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "No se ha seleccionado ningun items", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
            else
            {

                lista_pago_nc = new List<Ent_Venta_PagoNota>();

                foreach(Ent_Venta_PagoNota rownc in tmplista)
                {
                    lista_pago_nc.Add(rownc);
                }
                lblnc.Content = string.Format("{0:C2}", _total_pago_nc);
                calcular_totales();
                this.ToggleFlyout(3, "");
            }

           
        }

        private void dwforma_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            if (dwforma.EditValue.ToString() == "EFE")
            {
                txtnum1.IsEnabled = false;
                txtnum2.IsEnabled = false;
                txtnum3.IsEnabled = false;
                txtnum4.IsEnabled = false;
                
            }
            else
            {
                txtnum1.IsEnabled = true;
                txtnum2.IsEnabled = true;
                txtnum3.IsEnabled = true;
                txtnum4.IsEnabled = true;
                
            }

            // dwtarjeta.Visibility = (dwforma.EditValue.ToString() == "EFE") ? Visibility.Hidden : Visibility.Visible;            

        }

        private void txtnum1_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = txtnum1.Text.Trim();
            if (str.Length == 4)
            {
                txtnum2.SelectAll(); 
                txtnum2.Focus();
            }
        }

        private void txtnum2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = txtnum2.Text.Trim();
            if (str.Length == 4)
            {
                txtnum3.SelectAll();
                txtnum3.Focus();
            }
        }

        private void txtnum3_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = txtnum3.Text.Trim();
            if (str.Length == 4)
            {
                txtnum4.SelectAll();
                txtnum4.Focus();
            }
        }

        private void txtnum4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                procesarpines();
            }
        }
        private void procesarpines()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            lblinforma.Text = "";

            string str1 = txtnum1.Text.Trim(); string str2 = txtnum2.Text.Trim(); string str3 = txtnum3.Text.Trim(); string str4 = txtnum4.Text.Trim();

            if (str1.Length == 4 || str2.Length == 4 || str3.Length == 4 || str4.Length == 4)
            {
                Ent_Pines_Tarjeta str_bines = null;
                if (existe_pines(ref str_bines))
                {
                    txtmonto.Text = _total_saldo.ToString();
                    agregar_forma(str_bines.bin_tar_ser, str_bines.bin_tar_cod, str_bines.bin_tar_des, str1.ToString() + str2.ToString() + str3.ToString() + str4.ToString());
                    txtnum1.Text = "";
                    txtnum2.Text = "";
                    txtnum3.Text = "";
                    txtnum4.Text = "";
                }
                else
                {
                    lblinforma.Text = "El Numero de tarjeta no existe";
                }
            }
            Mouse.OverrideCursor = null;
        }
        private Boolean existe_pines(ref Ent_Pines_Tarjeta str_bines)
        {
            Boolean valida=false;
            try
            {

                string bines = txtnum1.Text.ToString() + Basico.Left(txtnum2.Text,2).ToString();

                Dat_Tarjeta dat_pines = new Dat_Tarjeta();
              

                Ent_Pines_Tarjeta get_pines = dat_pines.lista_pines(bines);

                if (get_pines!=null)
                {
                    str_bines = get_pines;
                    valida = get_pines.existe_bines;                  
                }

            }
            catch
            {
                valida = false;
            }
            return valida;
        }

        private void limpiar_forma_pago()
        {
            _total_efectivo = 0;
            _total_tarjeta=0;
            _total_vuelto=0;
            _total_saldo = 0;
           // _total_pagar = 1000;
            dwforma.EditValue= "EFE";
            txtmonto.Text = 0.ToString();
            txtnum1.Text = "";
            txtnum2.Text = "";
            txtnum3.Text = "";
            txtnum4.Text = "";
            print_forma_pago();
            venta_pagos = new List<Ent_Venta_FormaPago>();
            dgforma.ItemsSource = venta_pagos;
            dgforma.Items.Refresh();
        }
        private void btncancelare_Click(object sender, RoutedEventArgs e)
        {            
            this.ToggleFlyout(2, "");
        }

        private void agregar_forma_pago(string _doc_tra_id,string _forma_pago_id, string _forma_pago_nombre, string _tarjeta_bin_ser,string _tarjeta_bin_cod,string _tarjeta_nombre,string _tarjeta_numero,decimal _forma_monto)
        {
            decimal saldo = 0;
          
            if (venta_pagos!=null)
            {
                saldo = venta_pagos.Sum(t => t.forma_monto);                
                Ent_Venta_FormaPago ins_forma = new Ent_Venta_FormaPago();
                ins_forma.doc_tra_id = _doc_tra_id;
                ins_forma.forma_items = venta_pagos.Count() + 1;
                ins_forma.forma_pago_id = _forma_pago_id;
                ins_forma.forma_pago_nombre = _forma_pago_nombre;
                ins_forma.tarjeta_bines_ser = _tarjeta_bin_ser;
                ins_forma.tarjeta_bines_cod = _tarjeta_bin_cod;
                ins_forma.tarjeta_nombre = _tarjeta_nombre;
                ins_forma.tarjeta_numero = _tarjeta_numero;
                ins_forma.forma_monto = _forma_monto;
                venta_pagos.Add(ins_forma);
                dgforma.ItemsSource = venta_pagos;
                dgforma.Items.Refresh();                             
            }
        }

        private async void factura_directa(Boolean directo_nc = false)
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            ProgressDialogController ProgressAlert = null;
            try
            {
                lblinforma.Text = "";
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Si",
                    NegativeButtonText = "No",
                    //FirstAuxiliaryButtonText = "Cancelar",
                    ColorScheme = MetroDialogOptions.ColorScheme,
                };



                MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "¿Realmente desea realizar la facturacion ? ",
                MessageDialogStyle.AffirmativeAndNegative, mySettings);
                if (result == MessageDialogResult.Affirmative)
                {
                    string _error_venta = "";
                    ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Generando Facturacion Electronica"); 
                    ProgressAlert.SetIndeterminate();


                    /*en este caso si se cancela son nota de credito*/

                    if (lista_pago_nc != null)
                    {
                        var result_nota = lista_pago_nc.Where(b => b.chknota == true);

                        if (result_nota.Count()>0)
                        {
                            foreach(Ent_Venta_PagoNota vrow in result_nota)
                            {
                                /*concepto 98 nota de credito*/
                                string _con_nc = "98";
                                agregar_forma_pago(vrow.doc_tra_id, _con_nc, "","", "", "", "", vrow.total_nc);
                            }
                        }
                    }

                    

                    string grabar_numerodoc = await Task.Run(() => Dat_Venta_Directa.insertar_venta_directa(datclie_ven.bas_id, igv_monto,(comision_calcula) ? Ent_Global._comision_porc:0, percep_monto_real, Ent_Global._percepcion, venta_det_list, venta_pagos, ref _error_venta));
                    if (grabar_numerodoc != "-1")
                    {
                        string _codigo_hash = "";
                        string _error = "";
                        string _url_pdf = "";
                        await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica(Basico.Left(grabar_numerodoc, 1), grabar_numerodoc, ref _codigo_hash, ref _error,ref _url_pdf));

                        if (_codigo_hash.Length == 0 || _codigo_hash == null)
                        {
                            await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica(Basico.Left(grabar_numerodoc, 1), grabar_numerodoc, ref _codigo_hash, ref _error,ref _url_pdf));
                        }

                        if (_codigo_hash.Length == 0 || _codigo_hash == null)
                        {
                            _error = "GENERACION DE HASH";
                            await ProgressAlert.CloseAsync();
                            await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                            //MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Ent_Msg.msginfomacion,MessageBoxButton.OK,MessageBoxImage.Error);
                            return;
                        }
                        if (_error.Length > 0)
                        {
                            await ProgressAlert.CloseAsync();
                            await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                            //MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Ent_Msg.msginfomacion,MessageBoxButton.OK,MessageBoxImage.Error);
                            return;
                        }

                        await Task.Run(() => Basico._enviar_webservice_xml());
                        //byte[] img_qr = null;
                        await Task.Run(() => Dat_Venta.insertar_codigo_hash(grabar_numerodoc, _codigo_hash, "V", _url_pdf));
                        string _genera_tk = await Task.Run(() => Imprimir_Doc.Generar_Impresion("F", grabar_numerodoc) /*Impresora_Epson.Config_Imp.GenerarTicketFact(grabar_numerodoc, 1, _codigo_hash)*/);
                        //string _genera_tk = Impresora_Epson.Config_Imp.GenerarTicketFact(grabar_numerodoc, 1, _codigo_hash);
                        
                        limpiar_general();
                        await ProgressAlert.CloseAsync();
                        if (_genera_tk == null)
                        {
                            pnlarticulo.Visibility = Visibility.Visible;
                            pnlarticulo.Background = Brushes.Red;
                            lblmensaje.Content = " >> Se producjo un error en la impresion del ticket";
                            
                        }
                        else
                        {
                            pnlarticulo.Visibility = Visibility.Visible;
                            pnlarticulo.Background = Brushes.YellowGreen;
                            lblmensaje.Content = " > Ticket Generado con exito";
                        }

                        if (!directo_nc)
                        { 
                            this.ToggleFlyout(2, "");
                        }
                        txtbuscarcli.Focus();

                    }
                    else
                    {
                        lblinforma.Text= _error_venta;

                        await ProgressAlert.CloseAsync();
                        await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, _error_venta, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
                    }
                }
            }
            catch (Exception exc)
            {
                lblinforma.Text = exc.Message;
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                await metroWindow.ShowMessageAsync(Ent_Msg.msginfomacion, exc.Message, MessageDialogStyle.Affirmative, metroWindow.MetroDialogOptions);
            }
        }
                          
        #endregion


    }
}

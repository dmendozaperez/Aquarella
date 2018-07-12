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
using CapaEntidad.Bll.Util;
using System.Windows.Threading;
using Integrado.Design.WPF_Master;
using MahApps.Metro.Controls.Dialogs;
using System.Data;
using CapaDato.Bll.Venta;
using Integrado.Bll;
using CapaEntidad.Bll.Nota;
using Epson_Ticket;
namespace Integrado.Sistemas.Ventas
{
    /// <summary>
    /// Lógica de interacción para NotaCredito.xaml
    /// </summary>
    public partial class NotaCredito : MetroWindow
    {
        DataView dtv_grilla = null;
        private string _cliente_id = "";
        public NotaCredito()
        {
            InitializeComponent();

            lblnom_modulo.Content = "{" + Ent_Global._nom_modulo.ToString() + "}";
            //lblusuario.Content = Ent_Usuario.var_usuario._usu_nombres;

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

            this.Title = "PAGINA DE NOTA DE CREDITO [" + Ent_Global._nom_modulo + "]";

        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblhora.Content = DateTime.Now.ToLongTimeString();
        }
        private void btCloseSesion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Inicio_Windows();
        }       
        private DataTable createStructureDataTable()
        {
            DataTable dtEstructura = new DataTable();
            ///
            ///dtEstructura.Columns.Add("idRow", typeof(int));
            ///
            /// 
            dtEstructura.Columns.Add("item_det", typeof(Decimal));

            dtEstructura.Columns.Add("idn_qty_line", typeof(Decimal));

            dtEstructura.Columns.Add("checked", typeof(Boolean));

            /// 
            /// 
            ///
            dtEstructura.Columns.Add("IDV_INVOICE", typeof(String));
            ///
            dtEstructura.Columns.Add("IDV_ARTICLE", typeof(String));

            ///
            dtEstructura.Columns.Add("ARV_NAME", typeof(String));

            ///
            dtEstructura.Columns.Add("brv_description", typeof(String));

            dtEstructura.Columns.Add("CALIDAD", typeof(String));

            ///
            dtEstructura.Columns.Add("cov_description", typeof(String));
            ///
            dtEstructura.Columns.Add("IDV_SIZE", typeof(String));


            ///

            ///
            dtEstructura.Columns.Add("IDN_QTY", typeof(Decimal));
            ///
            dtEstructura.Columns.Add("IDN_SELLPRICE", typeof(Decimal));
            ///
            dtEstructura.Columns.Add("TAXES", typeof(Decimal));
            ///
            dtEstructura.Columns.Add("IDN_COMMISSION", typeof(Decimal));
            ///
            dtEstructura.Columns.Add("IDN_DISSCOUNT", typeof(Decimal));
            ///



            /// Valores de la columna unicos /// 


            /// Primary key
            ///DataColumn[] keys = new DataColumn[1];
            DataColumn[] keys = { dtEstructura.Columns["idv_invoice"],
                                        dtEstructura.Columns["idv_article"],
                                    dtEstructura.Columns["idv_size"],
                                dtEstructura.Columns["calidad"]};

            /// Set Prmary Key
            dtEstructura.PrimaryKey = keys;

            ///
            return dtEstructura;
        }
        private void Inicio_Windows()
        {
            Integrado.Bll.Basico.cambio_img(imglogo);
            DataTable dt = this.createStructureDataTable();
            dtv_grilla = new DataView(dt);

            Mouse.OverrideCursor = Cursors.Wait;            
            DataSet dsCustomers = Dat_NotaCredito.getCoordinators("%%");

            // Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            dwcliente.ItemsSource = dsCustomers.Tables[0].DefaultView;
            dwcliente.DisplayMemberPath = "Nombres";
            dwcliente.SelectedValuePath = "bas_id";
            dwcliente.SelectedIndex = -1;
            dwcliente.Focus();

            dwclientex.ItemsSource= dsCustomers.Tables[0].DefaultView;
            dwclientex.DisplayMember = "Nombres";
            dwclientex.ValueMember = "bas_id";
            dwclientex.SelectedIndex = -1;
            dwclientex.Focus();

            limpiar();
            //pninfo.Visible = false;
            lblinfo.Content = "";
            limpiar();
            //cargar_grilla();           
            estado_notacredito();
            Mouse.OverrideCursor = null;
            calculateTotals();
            dwcliente.Focus();
            
        }
        private void limpiar()
        {
            lbldocumento.Content = "";
            lbldireccion.Content = "";
            lblubicacion.Content = "";
            lblnombre.Content = "";
            lbltelefono.Content = "";
            lblemail.Content = "";
        }
        public void calculateTotals()
        {
            try
            {
                DataView dv = (DataView)dtv_grilla;
                ///
                Decimal query = dv.Table.AsEnumerable().Sum(x => x.Field<Decimal>("IDN_QTY"));
                lblcantidad.Content = query.ToString();
                ///

                lbltotal.Content = String.Format("{0:C2}",
                    dv.Table.AsEnumerable().Sum(y => (y.Field<Decimal>("IDN_SELLPRICE") * y.Field<Decimal>("IDN_QTY")) -
                        (y.Field<Decimal>("idn_commission") + y.Field<Decimal>("idn_disscount")) + y.Field<Decimal>("taxes")));
            }
            catch
            { }
        }
        private void estado_notacredito()
        {
            DataSet dsestado = Dat_NotaCredito.getStatusByModule("7");
            dwestado.ItemsSource = dsestado.Tables[0].DefaultView;
            dwestado.DisplayMemberPath = "Est_Descripcion";
            dwestado.SelectedValuePath = "Est_Id";


            dwestado.SelectedValue = "06";

            //dwestado.SelectedIndex = 1;
        }
        protected void paintInfoUser(DataTable dtable)
        {
            /// Verificar que efectivamente el dataTable contenga resgistros
            if (dtable.Rows.Count > 0)
            {
                /// Activar el panel de impresion de la informacion
                /// 
                ///panelInfoUser.Visible = true;
                /// Recorrer los resultados del dataTable e imprimirlos en pantalla
                /// Documento
                /// 
                lbldocumento.Content = Convert.ToString(dtable.Rows[0]["Bas_Documento"]);
                /// Nombre completo
                /// 
                lblnombre.Content = Convert.ToString(dtable.Rows[0]["NombreCompleto"]);
                //lblnombre.too .ToolTip = Convert.ToString(dtable.Rows[0]["NombreCompleto"]);
                /// Dirección
                /// 
                lbldireccion.Content = Convert.ToString(dtable.Rows[0]["Bas_Direccion"]);
                //txtDireccion.ToolTip = Convert.ToString(dtable.Rows[0]["Bas_Direccion"]);
                /// Teléfono
                /// 
                lbltelefono.Content = Convert.ToString(dtable.Rows[0]["Bas_Telefono"]);
                /// E-Mail
                /// 
                lblemail.Content = Convert.ToString(dtable.Rows[0]["bas_correo"]);
                //txtMail.ToolTip = Convert.ToString(dtable.Rows[0]["bas_correo"]);
                ///
                /// Ubicacion customer
                /// 
                lblubicacion.Content = Convert.ToString(dtable.Rows[0]["Ubicacion"]);
                ///
                //txtUbicacion.ToolTip = Convert.ToString(dtable.Rows[0]["Ubicacion"]);
            }

        }
        private void dwcliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //if (!(dwcliente.Focused)) return;
                String coordinatorSelect = dwcliente.SelectedValue.ToString();
                Mouse.OverrideCursor = Cursors.Wait;
                /// Verificar que sea una selección valida
                if (coordinatorSelect != "-1")
                {
                    /// Obtener Informacion del coordinador
                    DataTable dtable = new DataTable();
                    dtable = Dat_NotaCredito.getCoordinatorByPk(Convert.ToDecimal(coordinatorSelect)).Tables[0];
                    /// Llamar la funcion que imprimira la informacion del coordinador
                    this.paintInfoUser(dtable);

                    /// Consultar saldos y montos en pedidos del cliente
                    //this.ordersCustomer(Documents_Trans.getBalanceCoordById(_user._usv_co, coordinatorSelect).Tables[0]);

                    /// Cargar el id del coordinador seleccionado
                    /// 
                    _cliente_id = coordinatorSelect;



                    ///
                    this.cleanForm();

                }
            }
            catch
            {
            }
            Mouse.OverrideCursor = null;
        }
        public void cleanForm()
        {
            /// Reiniciar el HashTable de articulos retornados
            ///this.setHashTableInSession(new Hashtable());
            ///
            ///this.fillGridView(new Hashtable());
            ///
            ///
            DataTable dt = this.createStructureDataTable();
            dtv_grilla = new DataView(dt);


            
            ///
            this.dg1.ItemsSource = dtv_grilla;
            ///
            this.calculateTotals();
            ///
            //this.dwcliente.Focus();
            ///
            txtfactura.Text = "";
            txtarticulo.Text = "";
            ///
            lblinfo.Content = "";
            //pninfo.Visible = false;

            //lblarticuloselect.Text = "0";
            ///
            ///this.sumQtysReturned(0);
            ///this.sumValueOfMoneyToReturn(0);
        }

        private void btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            lblinfo.Content = "";
            //pninfo.Visible = false;
            ///
            String article = "";
            ///
            String size = "";
            string calidad = "";
            ///
            String[] infoCodeBars;
            ///
            if (!txtarticulo.Text.Equals(String.Empty))
            {
                /// Pos 0: Articulo referencia
                /// Pos 1: Plano
                infoCodeBars =Ent_BarCodes.getInfoFromTheBarCode(txtarticulo.Text);
                ///
                if (infoCodeBars == null)
                {
                    //pninfo.Visible = true;
                    lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749"); //System.Drawing.ColorTranslator.FromHtml("#ee7749");
                    lblinfo.Content = "Codigo de articulo invalido.";
                    //msnMessage.LoadMessage("Codigo de articulo invalido.", UserControl.ucMessage.MessageType.Error);
                    return;
                }
                ///
                article = infoCodeBars[0].ToString();
                calidad = infoCodeBars[2].ToString();
                /// Para saber si se debe consultar la talla legible se debe medir el numero de digitos; El EAN13 posee 14
                /// 
                if (txtarticulo.Text.Length == 13)
                {
                    ///
                    DataTable dtInfoArticle = null;// Article.getInfoDecodifyCodeBars(_user._usv_co, article, "", (Convert.ToDecimal(infoCodeBars[1]) - 1).ToString());

                    ///
                    if (dtInfoArticle != null && dtInfoArticle.Rows.Count > 0)
                    {
                        ///
                        size = dtInfoArticle.Rows[0]["sgv_size_display"].ToString();
                    }
                }
                else
                {
                    ///
                    size = infoCodeBars[1].ToString();
                }
                ///

                loadArticleForReturn(txtfactura.Text.Trim(), article, size, calidad);
            }
            ///
            txtarticulo.Focus();
        }
        public void loadArticleForReturn(String noInvoice, String article, String size, String calidad)
        {
            ///
            //pninfo.Visible = false;
            lblinfo.Content = "";
            DataView dv = (DataView)dtv_grilla;
            ///
            if (dv != null)
            {
                if (dv.Table.Rows.Count > 0)
                {
                    try
                    {
                        /// verificar si el articulo ya ha sido adicionado, en igual talla e igual factura
                        DataView dvResult = new DataView((from myRow in dv.Table.AsEnumerable()
                                                          where myRow.Field<String>("idv_article") == article
                                                          && myRow.Field<String>("idv_size") == size
                                                          && myRow.Field<String>("idv_invoice") == noInvoice.ToUpper()
                                                          && myRow.Field<String>("calidad") == calidad
                                                          select myRow).CopyToDataTable<DataRow>());
                        DataTable dtArticleResult = dvResult.Table;
                        ///
                        if (dtArticleResult != null && dtArticleResult.Rows.Count > 0)
                        {
                            /// Articulo existente, ya adicionado previamente, verificar si tiene mas cantidades habilitadas para su devolucion
                            /// 
                            Decimal qtysAvailableToReturn = Convert.ToDecimal(dtArticleResult.Rows[0]["idn_qty_line"]);
                            /// Cantidades actualmente devueltas
                            Decimal qtysActualReturn = Convert.ToDecimal(dtArticleResult.Rows[0]["idn_qty"]);
                            ///
                            if ((qtysActualReturn + 1) <= qtysAvailableToReturn)
                            {
                                ///
                                String[] keys = { dtArticleResult.Rows[0]["idv_invoice"].ToString(),
                                        dtArticleResult.Rows[0]["idv_article"].ToString(),
                                    dtArticleResult.Rows[0]["idv_size"].ToString(),
                                                 dtArticleResult.Rows[0]["calidad"].ToString()};

                                /// Actualizar datos
                                dv.Table.Rows.Find(keys)["idn_qty"] = qtysActualReturn + 1;

                                /// Iva Formula : Obtener el individual y luego multiplicar por las cantidades lineales totales
                                dv.Table.Rows.Find(keys)["taxes"] = (Convert.ToDecimal(dtArticleResult.Rows[0]["taxes"]) / qtysActualReturn) * (qtysActualReturn + 1);
                                /// Comision
                                dv.Table.Rows.Find(keys)["idn_commission"] = (Convert.ToDecimal(dtArticleResult.Rows[0]["idn_commission"]) / qtysActualReturn) * (qtysActualReturn + 1);
                                /// 
                                dv.Table.Rows.Find(keys)["idn_disscount"] = (Convert.ToDecimal(dtArticleResult.Rows[0]["idn_disscount"]) / qtysActualReturn) * (qtysActualReturn + 1);

                                /// 
                                //MessageBox.Show(" > Se han sumado las cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.",Global.mensaje,MessageBoxButtons.OK,MessageBoxIcon.Exclamation
                                //pninfo.Visible = true;
                                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0068FF");// System.Drawing.ColorTranslator.FromHtml("#e7df65");
                                lblinfo.Content = " > Se han sumado las cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.";
                                //msnMessage.LoadMessage(" > Se han sumado las cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Information);
                                ///
                                dtv_grilla = dv;
                                //Session[_nameSessionArtsRet] = dv;
                                ///
                                this.calculateTotals();
                                this.dg1.ItemsSource = dv;
                                //formato_grilla(dg0);
                                //this.GridViewArticlesToReturn.DataBind();
                            }
                            else
                            {
                                //pninfo.Visible = true;
                                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");//System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                lblinfo.Content = " > No puede devolver más cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.";
                            }
                            //msnMessage.LoadMessage(" > No puede devolver más cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.", UserControl.ucMessage.MessageType.Error);
                        }
                    }
                    catch
                    {
                        /// En ocaciones hay catch cuando la consulta linq no obtiene ningun resultado
                        /// Adicionar una nueva linea
                        addArticle(txtfactura.Text, article, size, _cliente_id, calidad);
                    }
                }
                else
                    /// Adicionar una nueva linea
                    addArticle(txtfactura.Text, article, size, _cliente_id, calidad);
            }

            actualizar_item(dg1);
        }
        private void actualizar_item(DataGrid dg)
        {
            DataView dt_V = dtv_grilla;

            if (dt_V != null)
            {
                if (dt_V.Count > 0)
                {
                    for (Int32 i = 0; i < dt_V.Count; ++i)
                    {
                        dt_V[i]["item_det"] = (i + 1).ToString();
                    }
                }
                //formato_grilla(dg);
                dg.ItemsSource = dt_V;
                dtv_grilla = dt_V;
                //dg.Refresh();
            }

        }
        public void addArticle(String noInvoice, String article, String size, String customer, string calidad)
        {
            lblinfo.Content = "";
            //pninfo.Visible = false;
            /// Realizar la busqueda del articulo en la factura
            DataTable dtArticleInvoiced =Dat_NotaCredito.searchArticleInvoice(noInvoice, article, size, customer, calidad);

            /// Verificar que existan datos /// 
            if (dtArticleInvoiced != null && dtArticleInvoiced.Rows.Count > 0)
            {
                ///
                DataView dv = (DataView)dtv_grilla;
                ///
                if (dv != null)
                {
                    DataRow drow = dtArticleInvoiced.Rows[0];
                    ///
                    DataRow newRow = dv.Table.NewRow();
                    ///
                    /// Número de factura ///
                    String noInvo = drow["idv_invoice"].ToString();
                    newRow["idv_invoice"] = noInvo;
                    ///
                    String art = drow["idv_article"].ToString();
                    newRow["idv_article"] = art;
                    /// Nombre Artículo ///
                    newRow["arv_name"] = drow["arv_name"].ToString();
                    ///
                    newRow["brv_description"] = drow["brv_description"].ToString();
                    /// Color Artículo ;
                    newRow["cov_description"] = drow["cov_description"].ToString();
                    /// Talla 
                    newRow["idv_size"] = drow["idv_size"].ToString();

                    newRow["calidad"] = drow["calidad"].ToString();

                    //newRow["idv_size"] = drow["idv_size"].ToString();
                    ///
                    /// Cantidades facturadas del articulo en la misma linea
                    Decimal qtyInvoice = Convert.ToDecimal((drow["IDN_QTY"].ToString().Equals(String.Empty)) ? "1" : (drow["IDN_QTY"].ToString()));
                    /// Cantidades en la misma linea
                    Decimal qtyInLine = Convert.ToDecimal(drow["totalqtyscanbereturned"]);
                    newRow["idn_qty_line"] = qtyInLine;

                    /// Cantidad ///
                    newRow["idn_qty"] = 1;
                    /// Precio de venta 
                    Decimal sellPrice = Convert.ToDecimal(drow["IDN_SELLPRICE"].ToString());
                    newRow["IDN_SELLPRICE"] = sellPrice;
                    /// Comision
                    Decimal comision = Convert.ToDecimal(drow["IDN_COMMISSION"].ToString());
                    /// Comision individual
                    newRow["IDN_COMMISSION"] = comision / qtyInLine;
                    /// Dscto Gnral Total de la factura ///
                    /// 
                    Decimal dsctGeneralFactura = Convert.ToDecimal(drow["ion_disscount"].ToString());

                    ///
                    /// Dscto Lineal ///
                    /// El dscto lineal es una sumatoria las cantidades del articulo en la linea
                    Decimal dctoLinealArt = Convert.ToDecimal((drow["IDN_DISSCOUNT"].ToString().Equals(String.Empty)) ? "0" : (drow["IDN_DISSCOUNT"].ToString()));
                    /// Solo si existe descuento
                    if (dctoLinealArt > 0)
                        dctoLinealArt = dctoLinealArt / qtyInvoice;
                    newRow["IDN_DISSCOUNT"] = dctoLinealArt;

                    /// Flete ///
                    /// 
                    Decimal handling = Convert.ToDecimal(drow["ihn_handling"].ToString());
                    ///////////////////////////////////////////////////////////////////////////////////////////////////////
                    /// Sumar todas las cantidades que se puden devolver de este articulo
                    /// /// Muchas veces la consulta podra devolver el mismo articulo en varias lineas, debido
                    /// a la diferencia entre numero de linea y numero de orden entre los registros, por eso se
                    /// debera sumar las cantidades posibles a devolver.
                    // Decimal qtysDelArticulo = 0;

                    /// Determinar comision para un articulo
                    /////if (comision > 0)
                    ////    comision = Math.Round((comision / qtysDelArticulo), 2);
                    ///
                    /// Cantidades TOTALes en la facura
                    Decimal cantTot = Convert.ToDecimal(drow["qtytotalinvoiced"].ToString());
                    /// Iva Total ///
                    Decimal ivaTot = Convert.ToDecimal(drow["ihn_taxes"].ToString());

                    ///
                    /// Base total de la facturacion ///
                    Decimal baseFac = Convert.ToDecimal(drow["base"].ToString());

                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    Decimal ivaIndividual = this.calculoIva(ivaTot, handling, dsctGeneralFactura, baseFac);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    Decimal fleteIndividual = 0;
                    if (handling > 0)
                        fleteIndividual = Math.Round((handling / cantTot), 2);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////
                    Decimal dsctoGralIndividualizado = 0;
                    if (dsctGeneralFactura > 0)
                        dsctoGralIndividualizado = Math.Round((dsctGeneralFactura / cantTot), 2);

                    /// Calcular el valor del iva a nivel unitario .
                    /// Formula = (Precio venta + fleteIndividual - comision lineal a nivel unitario) * porcentIva
                    ivaIndividual = (sellPrice + fleteIndividual - (comision / qtyInvoice) - dsctoGralIndividualizado - dctoLinealArt) * ivaIndividual; ///sellPrice* ivaIndividual; 
                                                                                                                                                        ///
                    //newRow["TAXES"] = Math.Round(ivaIndividual, 2);
                    newRow["TAXES"] = ivaIndividual;
                    ///
                    newRow["checked"] = false;

                    ///
                    dv.Table.Rows.Add(newRow);
                    ///        
                    //pninfo.Visible = true;
                    lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0068FF");// System.Drawing.ColorTranslator.FromHtml("#e7df65");
                    lblinfo.Content = " > Se ha agregado el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.";
                    //msnMessage.LoadMessage(" > Se ha agregado el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Information);
                    this.dg1.ItemsSource = dv;
                    //this.GridViewArticlesToReturn.DataBind();
                    dtv_grilla = dv;
                    //formato_grilla(dg0);
                    //Session[_nameSessionArtsRet] = dv;
                    this.calculateTotals();
                    txtarticulo.SelectAll();
                    txtarticulo.Focus();
                }
            }
            else
            {
                //pninfo.Visible = true;
                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749"); //System.Drawing.ColorTranslator.FromHtml("#ee7749");
                lblinfo.Content = " > No se puede devolver el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.";
            }

            //msnMessage.LoadMessage(" > No se puede devolver el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.", UserControl.ucMessage.MessageType.Error);
        }
        private Decimal calculoIva(Decimal taxes, Decimal handling, Decimal dsctoGral, Decimal baseFac)
        {
            ///
            Decimal taxPercent = taxes / (baseFac + handling - dsctoGral);
            ///
            return taxPercent;
        }

        private void txtfactura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                if (!_key_combo)
                { 
                     txtarticulo.Focus();
                }
                else
                {
                    _key_combo = false;
                }
            }
        }

        private void txtarticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                btnbuscar_Click(btnbuscar, new RoutedEventArgs());
            }
        }

        private void txtfactura_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtfactura.CharacterCasing = CharacterCasing.Upper;
        }
        private Boolean _key_combo = false;
        private void dwcliente_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                //dwcliente.SelectedValue = _cliente_id;
                //_key_combo = true;
                // txtfactura.Focus();
                    //txtarticulo.Focus();
            }
        }

        private void dwestado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dwestado.SelectedValue == null) return;
            if (!dwestado.Focusable) return;
            string _cod = dwestado.SelectedValue.ToString();

            if (_cod == "01")
            {
                MessageBox.Show("SI SELECCIONA ESTA OPCION [" + dwestado.Text.ToUpper() + "] , ANULARA LA OPERACION DEL DOCUMENTO DE REFERENCIA", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);                
            }
        }

        private void btneliminar_Click(object sender, RoutedEventArgs e)
        {
                        
            lblinfo.Content = "";                           
            try
                    {

                        DataRowView row = (DataRowView)((Button)e.Source).DataContext;

                        //Int32 _fila = dg0.CurrentCell.RowIndex;
                        /// Recuperar información de la linea deseada a eliminar. formato NoFactura@Articulo@Talla
                        string invo = (string)row["IDV_INVOICE"].ToString();// dg0.CurrentRow.Cells["IDV_INVOICE"].Value.ToString(); // e.CommandArgument.ToString().Split('@')[0];
                        ///
                        string art = (string)row["IDV_ARTICLE"].ToString();// dg0.CurrentRow.Cells["IDV_ARTICLE"].Value.ToString(); //e.CommandArgument.ToString().Split('@')[1];
                                                                           ///
                        string size = (string)row["IDV_SIZE"].ToString();//  dg0.CurrentRow.Cells["IDV_SIZE"].Value.ToString(); //dg0.Rows[_fila].Cells["IDV_SIZE"].ToString();//e.CommandArgument.ToString().Split('@')[2];

                        string calidad = (string)row["CALIDAD"].ToString();//  dg0.CurrentRow.Cells["CALIDAD"].Value.ToString(); //e.CommandArgument.ToString().Split('@')[3];
                                                                               ///
                        string[] keys = { invo, art, size, calidad };
                        ///
                        DataView dv = (DataView)dtv_grilla;// Session[_nameSessionArtsRet];
                        /// 
                        dv.Table.Rows.Find(keys).Delete();
                        /// Set the new DataView
                        /// 
                        dtv_grilla = dv;
                        //Session[_nameSessionArtsRet] = dv;
                        ///
                        this.calculateTotals();
                        ///          

                        
                        lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0068FF");// System.Drawing.ColorTranslator.FromHtml("#e7df65");
                        lblinfo.Content = " > El artículo -" + art + "- en talla -" + size + "- de la factura -" + invo + "- ha sido eliminado.";

                        //msnMessage.LoadMessage(" > El artículo -" + art + "- en talla -" + size + "- de la factura -" + invo + "- ha sido eliminado.", UserControl.ucMessage.MessageType.Information);
                        ///
                        this.dg1.ItemsSource = dv;
                        actualizar_item(dg1);
                        //this.GridViewArticlesToReturn.DataBind();
                    }
                    catch
                    {
                        lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");// System.Drawing.ColorTranslator.FromHtml("#ee7749");
                        lblinfo.Content = " > Error intentando eliminar el artículo.";
                        //msnMessage.LoadMessage(" > Error intentando eliminar el artículo.", UserControl.ucMessage.MessageType.Error);
                    }

                
                //chkmover.CheckedChanged += new EventHandler(chk_evento);
            
        }

        private async void btngenera_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = this;
            metroWindow.MetroDialogOptions.ColorScheme = MetroDialogOptions.ColorScheme;
            lblinfo.Content = "";
            //Mouse.OverrideCursor = Cursors.Wait;            
            if (dtv_grilla.Count == 0)
            {                
                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");// System.Drawing.ColorTranslator.FromHtml("#ee7749");
                lblinfo.Content = " > No hay datos para generar la nota de credito.";
                //Mouse.OverrideCursor = null;
                return;
            }

            #region<REGION PARA VALIDAR LA NOTA DE CREDITO DEVOLUCION TOTAL>
            if (Ent_Global._canal_venta != "AQ")
            {
                DataView dv = ((DataView)dtv_grilla);
                if (dv != null && dv.Table.Rows.Count > 0)
                {
                    DataTable dt = dv.ToTable();

                    if (dt.Rows.Count>0)
                    {
                        String ndoc = dt.Rows[0]["IDV_INVOICE"].ToString();
                        var cantidad = dt.AsEnumerable().Sum(s => s.Field<Decimal>("IDN_QTY"));

                        Boolean valida_tot= Dat_NotaCredito.getvalidaNota_DevTot(ndoc, cantidad);

                        // Modificado por : Henry Morales - 12/07/2018
                        // Se agergó validación para que solo Afecte cuando Seleccione Devolución Total
                        if (!valida_tot && dwestado.SelectedValue.ToString()!="07")
                        {
                            lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");// System.Drawing.ColorTranslator.FromHtml("#ee7749");
                            lblinfo.Content = " > Se tiene que realizar la devolucion en su totalidad.";
                            return;
                        }

                    }
                }

            }
                #endregion

                var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Si",
                NegativeButtonText = "No",
                //FirstAuxiliaryButtonText = "Cancelar",
                ColorScheme = MetroDialogOptions.ColorScheme,
            };

            MessageDialogResult result = await this.ShowMessageAsync(Ent_Msg.msginfomacion, "Esta seguro de generar la nota de credito",
              MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result == MessageDialogResult.Affirmative)
            {
                facturar();
            }
            Mouse.OverrideCursor = null;
        }
        private async void facturar()
        {
            ///
            string _almacen = Ent_Global._pvt_almaid ;
            ProgressDialogController ProgressAlert = null;
            try
            {
                /// Lista de artículos a devolver
                List<Ent_Nota_Dtl> lstArticlesReturned = new List<Ent_Nota_Dtl>();

                /// Lista de articulos a realizar el cambio de storage
                List<Transaction_det> itemsTransDetails = new List<Transaction_det>();

                /// Bandera de que si existieron elementos que enviar
                Decimal bandera = -1;
                /// Numero de articulos a mover de storage
                //int noLinea = 0;
                Boolean moveArticles = false;

                ///
                DataView dv = ((DataView)dtv_grilla);

                // Existen articulos que mover de storage
                //Decimal articlesToMove = dv.Table.AsEnumerable().Count(x => x.Field<Boolean>("checked") == true);

                //if (articlesToMove > 0)
                //{
                    //
                    //if (string.IsNullOrEmpty(dwStorages.SelectedValue))
                    //{
                    //    dwStorages.Focus();
                    //    msnMessage.LoadMessage("> Seleccione el area o storage a donde se enviaran los artículos seleccionados.", UserControl.ucMessage.MessageType.Information);

                    //}
                    //else
                    moveArticles = true;
                //}
                //else
                //    moveArticles = true;

                ///
                if (dv != null && dv.Table.Rows.Count > 0 && moveArticles == true)
                {
                    ///
                    foreach (DataRow drow in dv.Table.Rows)
                    {
                        //
                        string invo = drow["idv_invoice"].ToString();
                        //
                        string art = drow["idv_article"].ToString();
                        //
                        string size = drow["idv_size"].ToString();
                        //
                        Decimal qty = Convert.ToDecimal(drow["idn_qty"]);
                        //
                        String calidad = drow["calidad"].ToString();

                        //Boolean ck = drow["checked"].Equals("") ? false : (Boolean)drow["checked"];
                        //
                        //if (ck)
                        //{
                            Ent_Nota_Dtl objReturned = new Ent_Nota_Dtl(invo, art, size, qty, _almacen, calidad);
                            /// Agregar el objeto a la lista generica
                            lstArticlesReturned.Add(objReturned);
                            ///                         
                        //}
                        //else
                        //{
                        //    Ent_Nota_Dtl objReturned = new Ent_Nota_Dtl(invo, art, size, qty, "", calidad);
                        //    /// Agregar el objeto a la lista generica
                        //    lstArticlesReturned.Add(objReturned);
                        //}
                        ///
                        bandera = 1;
                    }
                }
                ///
                if (bandera > 0)
                {

                    string _codigo_estado = dwestado.SelectedValue.ToString();
                    ProgressAlert = await this.ShowProgressAsync(Ent_Msg.msgcargando, "Generando Nota de Credito Electronica...");
                    // Devolucion                        
                    string[] results = await Task.Run(() => Dat_NotaCredito.saveReturnOrder(_cliente_id, _almacen, lstArticlesReturned, Ent_Global._bas_id_codigo, _codigo_estado));
                    //string[] results =Dat_NotaCredito.saveReturnOrder(_cliente_id, _almacen, lstArticlesReturned, Ent_Global._bas_id_codigo, _codigo_estado);

                    if (results != null)
                    {
                        //                
                        //string url = _pageReportArtsReturned + "?noReturn=" + results[0];// + "&st=" + Constants.StatusReturnForAprob;

                        // Async                         
                        //
                        try
                        {
                            //
                            //string winAlert = "location.href='" + url + "'";
                            //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel3, Page.GetType(), "click", winAlert, true);
                            ///      
                            string _codigo_hash = "";
                            string _error = "";
                            string _url_pdf = "";
                            await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica("N", results[0].ToString(), ref _codigo_hash, ref _error,ref _url_pdf));
                            //Facturacion_Electronica.ejecutar_factura_electronica_NC(results[0].ToString(), ref _codigo_hash);

                            
                            if (_codigo_hash.Length==0 || _codigo_hash==null)
                            {
                                await Task.Run(() => Facturacion_Electronica.ejecutar_factura_electronica("N", results[0].ToString(), ref _codigo_hash, ref _error,ref _url_pdf));
                            }
                            if (_codigo_hash.Length == 0 || _codigo_hash == null)
                            {
                                _error = "GENERACION DE HASH";
                                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                                await this.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")");
                                //MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);

                                this.cleanForm();

                                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");// System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                lblinfo.Content = " >> Se producjo un error en la generacion de codigo hash FE";
                                return;
                            }
                                //

                                //Facturacion_Electronica.ejecutar_factura_electronica_NC(results[0].ToString(), ref _codigo_hash);

                            if (_error.Length > 0)
                            {
                                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                                await this.ShowMessageAsync(Ent_Msg.msginfomacion, "ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")");
                                //MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);

                                this.cleanForm();

                                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");// System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                lblinfo.Content = " >> Se producjo un error en la impresion del ticket";
                                return;
                            }

                            //EN ESTE PASO VAMOS A GRABAR EL CODIGO HASH
                            await Task.Run(() => Dat_Venta.insertar_codigo_hash(results[0].ToString(), _codigo_hash, "N", _url_pdf));


                            //****enviar los xml al server

                            await Task.Run(() => Basico._enviar_webservice_xml());
                            //byte[] img_qr = null;
                            string _genera_tk = await Task.Run(() => Imprimir_Doc.Generar_Impresion("N", results[0].ToString()) /*Impresora_Epson.Config_Imp_NC.GenerarTicketNC(results[0].ToString(), 1, _codigo_hash)*/);
                            //string _genera_tk = Impresora_Epson.Config_Imp_NC.GenerarTicketNC(results[0].ToString(), 1, _codigo_hash);
                            if (_genera_tk == null)
                            {                                                                
                                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                                await this.ShowMessageAsync(Ent_Msg.msginfomacion, " >> Se producjo un error en la impresion del ticket");
                                //MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Ent_Msg.msginfomacion, MessageBoxButton.OK, MessageBoxImage.Error);
                                this.cleanForm();
                                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");//System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                lblinfo.Content = " >> Se producjo un error en la impresion del ticket";
                                

                                //lbltickets.Text = " >> Se producjo un error en la impresion del ticket";
                            }
                            else
                            {
                                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                                this.cleanForm();
                                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF0068FF");//System.Drawing.ColorTranslator.FromHtml("#e7df65");
                                lblinfo.Content = " > Ticket Generado con exito";
                                //lbltickets.Text = " > Ticket Generado con exito";
                            }


                            //pninfo.Visible = true;
                            //lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7df65");
                            //lblinfo.Text = " > La Devolución Se Ha Realizado Correctamente : " + results[0] + "";
                            //msnMessage.LoadMessage(" > La Devoluci&oacute;n Se Ha Realizado Correctamente : " + results[0] + "<br /> > <a href='" + url + "' target='Blank'>Ver Reporte Devolución</a>", UserControl.ucMessage.MessageType.Information);

                        }
                        catch
                        {
                            if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                            //        
                            //this.cleanForm();
                            lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");//System.Drawing.ColorTranslator.FromHtml("#ee7749");
                            lblinfo.Content = " > La Devolución No Se Ha Realizado Correctamente." + results[0] + "";
                            
                            //msnMessage.LoadMessage(" > La Devoluci&oacute;n No Se Ha Realizado Correctamente." + results[0] + "<br /> > El reporte de la devoluci&oacute;n no se ha generado; <a href='" + url + "' target='Blank'>Ver Reporte Devolucion</a>", UserControl.ucMessage.MessageType.Error);
                        }
                    }
                    else
                    {
                        if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                        ///   
                        //this.cleanForm();
                        lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");//System.Drawing.ColorTranslator.FromHtml("#ee7749");
                        lblinfo.Content = " > La Devolución No Se Ha Realizado Correctamente.";
                       
                        //msnMessage.LoadMessage(" > La Devoluci&oacute;n No Se Ha Realizado Correctamente.", UserControl.ucMessage.MessageType.Error);
                    }
                    //}
                }
            }
            catch
            {
                if (ProgressAlert != null) await ProgressAlert.CloseAsync();
                ///  
                //this.cleanForm();
                lblinfo.Foreground = (Brush)new BrushConverter().ConvertFromString("#ee7749");//System.Drawing.ColorTranslator.FromHtml("#ee7749");
                lblinfo.Content = " > Ha ocurrido un error y la devoluciión no se ha realizado correctamente.";
               
                //msnMessage.LoadMessage(" > Ha ocurrido un error y la devoluci&oacute;n no se ha realizado correctamente.", UserControl.ucMessage.MessageType.Error);
            }
        }

        private void dwclientex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                try
                {
                    //if (!(dwcliente.Focused)) return;
                    String coordinatorSelect = dwclientex.EditValue.ToString(); //SelectedItemValue SelectedItemValue.ToString();
                    Mouse.OverrideCursor = Cursors.Wait;
                    /// Verificar que sea una selección valida
                    if (coordinatorSelect != "-1")
                    {
                        /// Obtener Informacion del coordinador
                        DataTable dtable = new DataTable();
                        dtable = Dat_NotaCredito.getCoordinatorByPk(Convert.ToDecimal(coordinatorSelect)).Tables[0];
                        /// Llamar la funcion que imprimira la informacion del coordinador
                        this.paintInfoUser(dtable);

                        /// Consultar saldos y montos en pedidos del cliente
                        //this.ordersCustomer(Documents_Trans.getBalanceCoordById(_user._usv_co, coordinatorSelect).Tables[0]);

                        /// Cargar el id del coordinador seleccionado
                        /// 
                        _cliente_id = coordinatorSelect;



                        ///
                        this.cleanForm();
                        _key_combo = true;
                        txtfactura.Focus();
                        _key_combo = false;

                    }
                }
                catch(Exception exc)
                {
                }
                Mouse.OverrideCursor = null;
            }
        }

        private void dwclientex_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (!(dwcliente.Focused)) return;
                String coordinatorSelect = dwclientex.EditValue.ToString();
                //String coordinatorSelect = dwclientex.SelectedItemValue.ToString();
                Mouse.OverrideCursor = Cursors.Wait;
                /// Verificar que sea una selección valida
                if (coordinatorSelect != "-1")
                {
                    /// Obtener Informacion del coordinador
                    DataTable dtable = new DataTable();
                    dtable = Dat_NotaCredito.getCoordinatorByPk(Convert.ToDecimal(coordinatorSelect)).Tables[0];
                    /// Llamar la funcion que imprimira la informacion del coordinador
                    this.paintInfoUser(dtable);

                    /// Consultar saldos y montos en pedidos del cliente
                    //this.ordersCustomer(Documents_Trans.getBalanceCoordById(_user._usv_co, coordinatorSelect).Tables[0]);

                    /// Cargar el id del coordinador seleccionado
                    /// 
                    _cliente_id = coordinatorSelect;



                    ///
                    this.cleanForm();
                    _key_combo = true;
                    txtfactura.Focus();
                    _key_combo = false;


                }
            }
            catch
            {
            }
            Mouse.OverrideCursor = null;
        }

        private void dwclientex_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                //try
                //{
                //    //if (!(dwcliente.Focused)) return;
                //    String coordinatorSelect = dwclientex.EditValue.ToString(); //SelectedItemValue SelectedItemValue.ToString();
                //    Mouse.OverrideCursor = Cursors.Wait;
                //    /// Verificar que sea una selección valida
                //    if (coordinatorSelect != "-1")
                //    {
                //        /// Obtener Informacion del coordinador
                //        DataTable dtable = new DataTable();
                //        dtable = Dat_NotaCredito.getCoordinatorByPk(Convert.ToDecimal(coordinatorSelect)).Tables[0];
                //        /// Llamar la funcion que imprimira la informacion del coordinador
                //        this.paintInfoUser(dtable);

                //        /// Consultar saldos y montos en pedidos del cliente
                //        //this.ordersCustomer(Documents_Trans.getBalanceCoordById(_user._usv_co, coordinatorSelect).Tables[0]);

                //        /// Cargar el id del coordinador seleccionado
                //        /// 
                //        _cliente_id = coordinatorSelect;



                //        ///
                //        this.cleanForm();
                //        _key_combo = true;
                //        txtfactura.Focus();

                //    }
                //}
                //catch
                //{
                //}
                //Mouse.OverrideCursor = null;
            }
        }
    }
}

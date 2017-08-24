using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Aquarella.bll;
using System.Data;
using System.ComponentModel;
using Microsoft.Windows.Controls;
namespace Aquarella.Form.Ventas
{
    /// <summary>
    /// Lógica de interacción para NotaCredito.xaml
    /// </summary>
    public partial class NotaCredito : Window
    {
        Usuario _user;
        DataView dtv_grilla = null;
        private string _cliente_id = "";
        public NotaCredito()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblInfoUser.Text = "Usuario | " + _user._nombre;
            inicio();
        }
        public DataTable createStructureDataTable()
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
        private void inicio()
        {
            DataTable dt = this.createStructureDataTable();
            dtv_grilla = new DataView(dt);

            //Mouse.OverrideCursor = Cursors.Wait;
            Mouse.OverrideCursor = Cursors.Wait;
            DataSet dsCustomers = NotaCredito_Negocio.getCoordinators("%%");

            // Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            cbcliente.DataContext= dsCustomers.Tables[0];

            cbcliente.ItemsSource = ((IListSource)dsCustomers.Tables[0]).GetList();
            cbcliente.Focus();
            Mouse.OverrideCursor = null;
            calculateTotals();
            lblarticuloselect.Content= "0";
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
        private void btBackPanelLiq_Click(object sender, RoutedEventArgs e)
        {
            ///
            MainAQWindow maw = new MainAQWindow();
            ///
            maw.loadUserInSesion(_user);
            ///
            maw.Show();
            ///
            this.Close();
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
        public void NotaWindowStart(Usuario user)
        {
            ///
            _user = new Usuario();
            _user = user;          
        }

        private void cbcliente_SelectionChanged(object sender,  System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                //if (!(dwcliente.Focused)) return;
                String coordinatorSelect =cbcliente.SelectedValue.ToString();
                Mouse.OverrideCursor = Cursors.Wait;
                /// Verificar que sea una selección valida
                if (coordinatorSelect != "-1")
                {
                    /// Obtener Informacion del coordinador
                    DataTable dtable = new DataTable();
                    dtable = (NotaCredito_Negocio.getCoordinatorByPk(Convert.ToDecimal(coordinatorSelect)).Tables[0]);
                    /// Llamar la funcion que imprimira la informacion del coordinador
                    this.paintInfoUser(dtable);

                    /// Consultar saldos y montos en pedidos del cliente
                    //this.ordersCustomer(Documents_Trans.getBalanceCoordById(_user._usv_co, coordinatorSelect).Tables[0]);

                    /// Cargar el id del coordinador seleccionado
                    /// 
                    _cliente_id = coordinatorSelect;



                    ///
                    this.cleanForm();

                    txtfac.Focus();

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
            this.dg1.DataContext= dtv_grilla;
            ///
            this.calculateTotals();
            ///
            //this.dwcliente.Focus();
            ///
            txtfac.Text = "";
            txtarticulo.Text = "";
            ///
            //pninfo.Visible = false;

            lblarticuloselect.Content = "0";
            ///
            ///this.sumQtysReturned(0);
            ///this.sumValueOfMoneyToReturn(0);
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
                txtdoc.Text = Convert.ToString(dtable.Rows[0]["Bas_Documento"]);
                /// Nombre completo
                /// 
                txtnombre.Text = Convert.ToString(dtable.Rows[0]["NombreCompleto"]);
                //lblnombre.too .ToolTip = Convert.ToString(dtable.Rows[0]["NombreCompleto"]);
                /// Dirección
                /// 
                txtdir.Text = Convert.ToString(dtable.Rows[0]["Bas_Direccion"]);
                //txtDireccion.ToolTip = Convert.ToString(dtable.Rows[0]["Bas_Direccion"]);
                /// Teléfono
                /// 
                txttelefono.Text = Convert.ToString(dtable.Rows[0]["Bas_Telefono"]);
                /// E-Mail
                /// 
                txtemail.Text = Convert.ToString(dtable.Rows[0]["bas_correo"]);
                //txtMail.ToolTip = Convert.ToString(dtable.Rows[0]["bas_correo"]);
                ///
                /// Ubicacion customer
                /// 
                txtubi.Text = Convert.ToString(dtable.Rows[0]["Ubicacion"]);
                ///
                //txtUbicacion.ToolTip = Convert.ToString(dtable.Rows[0]["Ubicacion"]);
            }

        }

        private void txtfac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key ==Key.Enter)
            {               
                txtarticulo.Focus();
            }
        }

        private void txtarticulo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btbuscar_Click(btbuscar, new RoutedEventArgs());
            }

        }

        private void btbuscar_Click(object sender, RoutedEventArgs e)
        {
            ///
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
                infoCodeBars = BarCodes.getInfoFromTheBarCode(txtarticulo.Text);
                ///
                if (infoCodeBars == null)
                {
                    //pninfo.Visible = true;
                    //lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                    //lblinfo.Text = "Codigo de articulo invalido.";
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

                loadArticleForReturn(txtfac.Text.Trim(), article, size, calidad);
            }
            ///
            txtarticulo.Focus();
        }
        public void loadArticleForReturn(String noInvoice, String article, String size, String calidad)
        {
            ///
            //pninfo.Visible = false;
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
                                //lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7df65");
                                //lblinfo.Text = " > Se han sumado las cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.";
                                //msnMessage.LoadMessage(" > Se han sumado las cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Information);
                                ///
                                dtv_grilla = dv;
                                //Session[_nameSessionArtsRet] = dv;
                                ///
                                this.calculateTotals();
                                //this.dg1.DataContext= dv;
                                this.dg1.ItemsSource = dv;
                                //formato_grilla(dg0);
                                //this.GridViewArticlesToReturn.DataBind();
                            }
                            else
                            {
                                //pninfo.Visible = true;
                                //lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                //lblinfo.Text = " > No puede devolver más cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.";
                            }
                            //msnMessage.LoadMessage(" > No puede devolver más cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.", UserControl.ucMessage.MessageType.Error);
                        }
                    }
                    catch
                    {
                        /// En ocaciones hay catch cuando la consulta linq no obtiene ningun resultado
                        /// Adicionar una nueva linea
                        addArticle(txtfac.Text, article, size, _cliente_id, calidad);
                    }
                }
                else
                    /// Adicionar una nueva linea
                    addArticle(txtfac.Text, article, size, _cliente_id, calidad);
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
                dg.DataContext = dt_V;
                dtv_grilla = dt_V;
                //dg.Refresh();
            }
        }
        public void addArticle(String noInvoice, String article, String size, String customer, string calidad)
        {
            //pninfo.Visible = false;
            /// Realizar la busqueda del articulo en la factura
            DataTable dtArticleInvoiced = NotaCredito_Negocio.searchArticleInvoice(noInvoice, article, size, customer, calidad);

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
                    //lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7df65");
                    //lblinfo.Text = " > Se ha agregado el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.";
                    //msnMessage.LoadMessage(" > Se ha agregado el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Information);
                    //this.dg1.DataContext = dv;
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
                //lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                //lblinfo.Text = " > No se puede devolver el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.";
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

        private void chkMover_Click(object sender, RoutedEventArgs e)
        {
            var chkmover = sender as System.Windows.Controls.CheckBox;
            //var task = (DataRow)(chkmover.DataContext as Datarow);
            
            bool _mover = (bool)chkmover.IsChecked;
           
            //Int32 _columna = dg0.CurrentCell.ColumnIndex;
            //pninfo.Visible = false;

            //if (_columna == 2)
            //{
                //if (dg0.Rows.Count > 0)
                //{
                    //

                    //DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dg0.CurrentRow.Cells["checked1"];// dg0.Rows[e.RowIndex].Cells["checked1"] as DataGridViewCheckBoxCell;
                    //DataGridViewCheckBoxCell cellSelecion = dg0.CurrentRow.Cells["checked1"] as DataGridViewCheckBoxCell;
                    //dg0.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    //Boolean chk = Convert.ToBoolean(dg0.Rows[e.RowIndex].Cells["checked1"].Value);
                    //DataGridViewCheckBoxColumn checkbox))
                    //CheckBox checkbox = ((CheckBox)sender);
                    ///
                    //string expresion = checkbox.ToolTip;
                    ///
                    string article = ((DataRowView)dg1.SelectedItem).Row["IDV_ARTICLE"].ToString(); //dg0.CurrentRow.Cells["IDV_ARTICLE"].Value.ToString();// expresion.Split('-')[1].Trim();
                    string size = ((DataRowView)dg1.SelectedItem).Row["IDV_SIZE"].ToString(); //dg0.CurrentRow.Cells["IDV_SIZE"].Value.ToString();// expresion.Split('-')[2].Trim();
                    string noInvoice = ((DataRowView)dg1.SelectedItem).Row["IDV_INVOICE"].ToString();// dg0.CurrentRow.Cells["IDV_INVOICE"].Value.ToString();// expresion.Split('-')[0].Trim();
                    string calidad = ((DataRowView)dg1.SelectedItem).Row["CALIDAD"].ToString();// dg0.CurrentRow.Cells["CALIDAD"].Value.ToString();// expresion.Split('-')[3].Trim();
                    ///
                    DataView dv = (DataView)dtv_grilla;// Session[_nameSessionArtsRet]);
                    ///
                    if (dv != null)
                    {
                        if (dv.Table.Rows.Count > 0)
                        {
                            try
                            {
                                /// Llaves de consulta
                                String[] keys = { noInvoice, article, size, calidad };

                                /// Actualizar datos
                                dv.Table.Rows.Find(keys)["checked"] = _mover;// cellSelecion.Value;

                                lblarticuloselect.Content = dv.Table.AsEnumerable().Count(x => x.Field<Boolean>("checked") == true).ToString();

                                ///
                                dtv_grilla = dv;
                                //Session[_nameSessionArtsRet] = dv;
                            }
                            catch
                            {
                                //pninfo.Visible = true;
                                //lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                //lblinfo.Text = " > Ha ocurrido un error seleccionando el artículo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.";

                                //msnMessage.LoadMessage(" > Ha ocurrido un error seleccionando el artículo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Error);
                            }
                        }
                    }
                //}
            //}
        }
    }
}

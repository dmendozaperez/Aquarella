using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Variables;
using Sistema_Aquarella;
namespace Sistema_Aquarella
{
    public partial class NotaCredito : Form
    {
        private string _cliente_id="";
        DataView dtv_grilla=null;
        public NotaCredito()
        {
            InitializeComponent();
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
            

            Cursor.Current = Cursors.WaitCursor;
            DataSet dsCustomers = NotaCredito_Negocio.getCoordinators("%%");
           
            // Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            dwcliente.DataSource = dsCustomers.Tables[0];
            dwcliente.DisplayMember = "Nombres";
            dwcliente.ValueMember = "bas_id";
            dwcliente.SelectedIndex = -1;
            dwcliente.Focus();
            pninfo.Visible = false;
            //cargar_grilla();
            formato_grilla(dg0);
            estado_notacredito();            
            Cursor.Current = Cursors.Default;
            calculateTotals();
            lblarticuloselect.Text = "0";
        }

        private void estado_notacredito()
        {
            DataSet dsestado = Basico.getStatusByModule("7");
            dwestado.DataSource = dsestado.Tables[0];
            dwestado.DisplayMember = "Est_Descripcion";
            dwestado.ValueMember = "Est_Id";
            

            dwestado.SelectedIndex = 1;
        }
        private void Facturacion_Load(object sender, EventArgs e)
        {
            inicio();
            
        }
        

       
        private void formato_grilla(DataGridView dg)
        {          

            dg.AutoGenerateColumns = false;
            dg.RowHeadersWidth = 22;
            dg.AllowUserToResizeColumns = true;

            dg.Columns[0].ReadOnly = true;
            dg.Columns[2].ReadOnly = true;
            dg.Columns[3].ReadOnly = false;
            dg.Columns[4].ReadOnly = true;
            dg.Columns[5].ReadOnly = true;
            dg.Columns[6].ReadOnly = true;
            dg.Columns[7].ReadOnly = true;
            dg.Columns[8].ReadOnly = true;
            dg.Columns[9].ReadOnly = true;
            dg.Columns[10].ReadOnly = true;
            dg.Columns[11].ReadOnly = true;
            dg.Columns[12].ReadOnly = true;
            dg.Columns[13].ReadOnly = true;
            dg.Columns[14].ReadOnly = true;


        }
      
        private void calculartotal()
        {
           
        }
      

        private void dg1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void btniniciar_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;          
            Cursor.Current = Cursors.Default;
        }

        private void btnfacturar_Click(object sender, EventArgs e)
        {
            pninfo.Visible = false;
            Cursor.Current = Cursors.WaitCursor;
            if (dtv_grilla.Count == 0)
            {
                pninfo.Visible = true;
                lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                lblinfo.Text = " > No hay datos para generar la nota de credito.";
                return;
            }
            DialogResult resultado = MessageBox.Show("Esta seguro de generar la nota de credito", Global.mensaje, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (resultado == DialogResult.OK)
            {
                facturar();
            }
            Cursor.Current = Cursors.Default;
        }
        private void facturar()
        {
            ///
            string _almacen = "11";
            try
            {
                /// Lista de artículos a devolver
                List<Returns_Dtl> lstArticlesReturned = new List<Returns_Dtl>();

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
                Decimal articlesToMove = dv.Table.AsEnumerable().Count(x => x.Field<Boolean>("checked") == true);

                if (articlesToMove > 0)
                {
                    //
                    //if (string.IsNullOrEmpty(dwStorages.SelectedValue))
                    //{
                    //    dwStorages.Focus();
                    //    msnMessage.LoadMessage("> Seleccione el area o storage a donde se enviaran los artículos seleccionados.", UserControl.ucMessage.MessageType.Information);

                    //}
                    //else
                        moveArticles = true;
                }
                else
                    moveArticles = true;

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

                        Boolean ck = drow["checked"].Equals("") ? false : (Boolean)drow["checked"];
                        //
                        if (ck)
                        {
                            Returns_Dtl objReturned = new Returns_Dtl(invo, art, size, qty, _almacen, calidad);
                            /// Agregar el objeto a la lista generica
                            lstArticlesReturned.Add(objReturned);
                            ///                         
                        }
                        else
                        {
                            Returns_Dtl objReturned = new Returns_Dtl(invo, art, size, qty, "", calidad);
                            /// Agregar el objeto a la lista generica
                            lstArticlesReturned.Add(objReturned);
                        }
                        ///
                        bandera = 1;
                    }
                }
                ///
                if (bandera > 0)
                {

                    string _codigo_estado = dwestado.SelectedValue.ToString();
                    // Devolucion                        
                    string[] results = Returns_Hdr.saveReturnOrder(_cliente_id, _almacen, lstArticlesReturned, Global._bas_id_codigo, _codigo_estado);

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
                            string _error="";
                            Facturacion_Electronica.ejecutar_factura_electronica("N", results[0].ToString(), ref _codigo_hash, ref _error);
                            //Facturacion_Electronica.ejecutar_factura_electronica_NC(results[0].ToString(), ref _codigo_hash);

                            //EN ESTE PASO VAMOS A GRABAR EL CODIGO HASH
                            Facturacion_Electronica.insertar_codigo_hash(results[0].ToString(), _codigo_hash, "N");


                            //****enviar los xml al server

                            Basico._enviar_webservice_xml();

                            //

                            //Facturacion_Electronica.ejecutar_factura_electronica_NC(results[0].ToString(), ref _codigo_hash);

                            if (_error.Length > 0)
                            {
                                MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.cleanForm();
                                pninfo.Visible = true;
                                lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                lblinfo.Text = " >> Se producjo un error en la impresion del ticket";
                                return;
                            }

                            //string _genera_tk = Impresora_Epson.Config_Imp_NC.GenerarTicketNC(results[0].ToString(), 1, _codigo_hash);

                            //this.cleanForm();
                            //if (_genera_tk == null)
                            //{
                            //    pninfo.Visible = true;
                            //    lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                            //    lblinfo.Text = " >> Se producjo un error en la impresion del ticket";
                            //    MessageBox.Show("ERROR EN LA GENERACION POR FAVOR CONSULTE CON SISTEMAS..==>> TIPO DE ERROR (" + _error + ")", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    this.cleanForm();

                            //    //lbltickets.Text = " >> Se producjo un error en la impresion del ticket";
                            //}
                            //else
                            //{                               
                            //    pninfo.Visible = true;
                            //    lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7df65");
                            //    lblinfo.Text = " > Ticket Generado con exito";
                            //    //lbltickets.Text = " > Ticket Generado con exito";
                            //}

                           
                            //pninfo.Visible = true;
                            //lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7df65");
                            //lblinfo.Text = " > La Devolución Se Ha Realizado Correctamente : " + results[0] + "";
                            //msnMessage.LoadMessage(" > La Devoluci&oacute;n Se Ha Realizado Correctamente : " + results[0] + "<br /> > <a href='" + url + "' target='Blank'>Ver Reporte Devolución</a>", UserControl.ucMessage.MessageType.Information);

                        }
                        catch
                        {
                            //
                            pninfo.Visible = true;
                            lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                            lblinfo.Text = " > La Devolución No Se Ha Realizado Correctamente." + results[0] + "";
                            this.cleanForm();
                            //msnMessage.LoadMessage(" > La Devoluci&oacute;n No Se Ha Realizado Correctamente." + results[0] + "<br /> > El reporte de la devoluci&oacute;n no se ha generado; <a href='" + url + "' target='Blank'>Ver Reporte Devolucion</a>", UserControl.ucMessage.MessageType.Error);
                        }                       
                    }
                    else
                    {
                        ///
                        pninfo.Visible = true;
                        lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                        lblinfo.Text = " > La Devolución No Se Ha Realizado Correctamente.";
                        this.cleanForm();
                        //msnMessage.LoadMessage(" > La Devoluci&oacute;n No Se Ha Realizado Correctamente.", UserControl.ucMessage.MessageType.Error);
                    }
                    //}
                }
            }
            catch
            {
                ///
                pninfo.Visible = true;
                lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                lblinfo.Text = " > Ha ocurrido un error y la devoluciión no se ha realizado correctamente.";
                this.cleanForm();
                //msnMessage.LoadMessage(" > Ha ocurrido un error y la devoluci&oacute;n no se ha realizado correctamente.", UserControl.ucMessage.MessageType.Error);
            }
        }       

        private void dwcliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!(dwcliente.Focused)) return;
            //String coordinatorSelect = dwcliente.SelectedValue.ToString();
            //Cursor.Current = Cursors.WaitCursor;
            ///// Verificar que sea una selección valida
            //if (coordinatorSelect != "-1")
            //{
            //    /// Obtener Informacion del coordinador
            //    DataTable dtable = new DataTable();
            //    dtable = (NotaCredito_Negocio.getCoordinatorByPk(Convert.ToDecimal(coordinatorSelect)).Tables[0]);
            //    /// Llamar la funcion que imprimira la informacion del coordinador
            //    this.paintInfoUser(dtable);

            //    /// Consultar saldos y montos en pedidos del cliente
            //    //this.ordersCustomer(Documents_Trans.getBalanceCoordById(_user._usv_co, coordinatorSelect).Tables[0]);

            //    /// Cargar el id del coordinador seleccionado
            //    /// 
            //    _cliente_id = coordinatorSelect;

            //    ///
            //    this.cleanForm();
            //}
            //Cursor.Current = Cursors.Default;
            ///
            //this.txtfactura.Focus();
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


            estado_notacredito();
            ///
            this.dg0.DataSource = dtv_grilla;            
            ///
            this.calculateTotals();
            ///
            //this.dwcliente.Focus();
            ///
            txtfactura.Text = "";
            txtarticulo.Text = "";
            ///
            pninfo.Visible = false;

            lblarticuloselect.Text = "0";
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
                lbldocumento.Text = Convert.ToString(dtable.Rows[0]["Bas_Documento"]);
                /// Nombre completo
                /// 
                lblnombre.Text = Convert.ToString(dtable.Rows[0]["NombreCompleto"]);
                //lblnombre.too .ToolTip = Convert.ToString(dtable.Rows[0]["NombreCompleto"]);
                /// Dirección
                /// 
                lbldireccion.Text = Convert.ToString(dtable.Rows[0]["Bas_Direccion"]);
                //txtDireccion.ToolTip = Convert.ToString(dtable.Rows[0]["Bas_Direccion"]);
                /// Teléfono
                /// 
                lbltelefono.Text = Convert.ToString(dtable.Rows[0]["Bas_Telefono"]);
                /// E-Mail
                /// 
                lblemail.Text = Convert.ToString(dtable.Rows[0]["bas_correo"]);
                //txtMail.ToolTip = Convert.ToString(dtable.Rows[0]["bas_correo"]);
                ///
                /// Ubicacion customer
                /// 
                lblubicacion.Text = Convert.ToString(dtable.Rows[0]["Ubicacion"]);
                ///
                //txtUbicacion.ToolTip = Convert.ToString(dtable.Rows[0]["Ubicacion"]);
            }

        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            ///
            pninfo.Visible= false;
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
                    pninfo.Visible = true;
                    lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                    lblinfo.Text = "Codigo de articulo invalido.";
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
            pninfo.Visible = false;
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
                                pninfo.Visible = true;
                                lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7df65");
                                lblinfo.Text = " > Se han sumado las cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.";
                                //msnMessage.LoadMessage(" > Se han sumado las cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Information);
                                ///
                                dtv_grilla = dv;
                                //Session[_nameSessionArtsRet] = dv;
                                ///
                                this.calculateTotals();
                                this.dg0.DataSource = dv;
                                formato_grilla(dg0);
                                //this.GridViewArticlesToReturn.DataBind();
                            }
                            else
                            { 
                                pninfo.Visible = true;
                                lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                lblinfo.Text = " > No puede devolver más cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.";
                            }
                                //msnMessage.LoadMessage(" > No puede devolver más cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.", UserControl.ucMessage.MessageType.Error);
                        }
                    }
                    catch
                    {
                        /// En ocaciones hay catch cuando la consulta linq no obtiene ningun resultado
                        /// Adicionar una nueva linea
                        addArticle(txtfactura.Text, article, size,_cliente_id, calidad);
                    }
                }
                else
                    /// Adicionar una nueva linea
                    addArticle(txtfactura.Text, article, size, _cliente_id, calidad);
            }

            actualizar_item(dg0);
        }
        public void addArticle(String noInvoice, String article, String size, String customer, string calidad)
        {
            pninfo.Visible = false;
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
                    pninfo.Visible = true;                    
                    lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7df65");
                    lblinfo.Text = " > Se ha agregado el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.";
                    //msnMessage.LoadMessage(" > Se ha agregado el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Information);
                    this.dg0.DataSource = dv;
                    //this.GridViewArticlesToReturn.DataBind();
                    dtv_grilla = dv;
                    formato_grilla(dg0);
                    //Session[_nameSessionArtsRet] = dv;
                    this.calculateTotals();
                    txtarticulo.SelectAll();
                    txtarticulo.Focus();
                }
            }
            else
            {
                pninfo.Visible = true;
                lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                lblinfo.Text = " > No se puede devolver el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.";
            }
           
                //msnMessage.LoadMessage(" > No se puede devolver el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.", UserControl.ucMessage.MessageType.Error);
        }
        public Decimal calculoIva(Decimal taxes, Decimal handling, Decimal dsctoGral, Decimal baseFac)
        {
            ///
            Decimal taxPercent = taxes / (baseFac + handling - dsctoGral);
            ///
            return taxPercent;
        }
        private void actualizar_item(DataGridView dg)
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
                formato_grilla(dg);
                dg.DataSource = dt_V;                
                dtv_grilla = dt_V;
                dg.Refresh();
            }

        }
        public void calculateTotals()
        {
            try
            {
                DataView dv = (DataView)dtv_grilla;
                ///
                Decimal query = dv.Table.AsEnumerable().Sum(x => x.Field<Decimal>("IDN_QTY"));
                lblcantidad.Text = query.ToString();
                ///
                lbltotal.Text = String.Format("{0:C2}",
                    dv.Table.AsEnumerable().Sum(y => (y.Field<Decimal>("IDN_SELLPRICE") * y.Field<Decimal>("IDN_QTY")) -
                        (y.Field<Decimal>("idn_commission") + y.Field<Decimal>("idn_disscount")) + y.Field<Decimal>("taxes")));
            }
            catch
            { }
        }

        private void btniniciar_Click_1(object sender, EventArgs e)
        {
            this.cleanForm();
        }

        private void txtarticulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnbuscar_Click(btnbuscar, new EventArgs());
            }
        }

        private void dwcliente_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                //if (!(dwcliente.Focused)) return;
                String coordinatorSelect = dwcliente.SelectedValue.ToString();
                Cursor.Current = Cursors.WaitCursor;
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

                }
            }
            catch
            {
            }
            Cursor.Current = Cursors.Default;
        }

        private void dwcliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dwcliente.SelectedValue = _cliente_id;
                txtfactura.Focus();
            }
        }

        private void dwcliente_Click(object sender, EventArgs e)
        {

        }

        private void txtfactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //btnbuscar_Click(btnbuscar, new EventArgs());
                txtarticulo.Focus();
            }
        }

        private void dg0_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //Int32 _columna = dg0.CurrentCell.ColumnIndex;
            //if (_columna == 1)
            //{
            //    //CheckBox chkmover = (CheckBox)(dg0.EditingControl);
            //    //chkmover.CheckedChanged += new EventHandler(chk_evento);
            //}
        }

        private void dg0_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;

            Int32 _columna = dg0.CurrentCell.ColumnIndex;
            pninfo.Visible = false;
          

            if (_columna == 0)
            {
                if (dg0.Rows.Count>0)
                {
                    try
                    {
                        Int32 _fila=dg0.CurrentCell.RowIndex;
                        /// Recuperar información de la linea deseada a eliminar. formato NoFactura@Articulo@Talla
                        string invo = dg0.CurrentRow.Cells["IDV_INVOICE"].Value.ToString(); // e.CommandArgument.ToString().Split('@')[0];
                        ///
                        string art = dg0.CurrentRow.Cells["IDV_ARTICLE"].Value.ToString(); //e.CommandArgument.ToString().Split('@')[1];
                        ///
                        string size = dg0.CurrentRow.Cells["IDV_SIZE"].Value.ToString(); //dg0.Rows[_fila].Cells["IDV_SIZE"].ToString();//e.CommandArgument.ToString().Split('@')[2];

                        string calidad = dg0.CurrentRow.Cells["CALIDAD"].Value.ToString(); //e.CommandArgument.ToString().Split('@')[3];
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

                        pninfo.Visible = true;
                        lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#e7df65");
                        lblinfo.Text = " > El artículo -" + art + "- en talla -" + size + "- de la factura -" + invo + "- ha sido eliminado.";

                        //msnMessage.LoadMessage(" > El artículo -" + art + "- en talla -" + size + "- de la factura -" + invo + "- ha sido eliminado.", UserControl.ucMessage.MessageType.Information);
                        ///
                        this.dg0.DataSource = dv;
                        actualizar_item(dg0);
                        //this.GridViewArticlesToReturn.DataBind();
                    }
                    catch
                    {
                        pninfo.Visible = true;
                        lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                        lblinfo.Text = " > Error intentando eliminar el artículo.";
                        //msnMessage.LoadMessage(" > Error intentando eliminar el artículo.", UserControl.ucMessage.MessageType.Error);
                    }

                }
                //chkmover.CheckedChanged += new EventHandler(chk_evento);
            }
        }
       

        private void NotaCredito_Activated(object sender, EventArgs e)
        {
            dwcliente.Focus();
        }

        private void dg0_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            Int32 _columna = dg0.CurrentCell.ColumnIndex;
            pninfo.Visible = false;

            if (_columna == 2)
            {
                if (dg0.Rows.Count > 0)
                {
                    //

                    //DataGridViewCheckBoxCell checkbox = (DataGridViewCheckBoxCell)dg0.CurrentRow.Cells["checked1"];// dg0.Rows[e.RowIndex].Cells["checked1"] as DataGridViewCheckBoxCell;
                    DataGridViewCheckBoxCell cellSelecion = dg0.CurrentRow.Cells["checked1"] as DataGridViewCheckBoxCell;
                    dg0.CommitEdit(DataGridViewDataErrorContexts.Commit);

                    //Boolean chk = Convert.ToBoolean(dg0.Rows[e.RowIndex].Cells["checked1"].Value);
                    //DataGridViewCheckBoxColumn checkbox))
                    //CheckBox checkbox = ((CheckBox)sender);
                    ///
                    //string expresion = checkbox.ToolTip;
                    ///
                    string article = dg0.CurrentRow.Cells["IDV_ARTICLE"].Value.ToString();// expresion.Split('-')[1].Trim();
                    string size = dg0.CurrentRow.Cells["IDV_SIZE"].Value.ToString();// expresion.Split('-')[2].Trim();
                    string noInvoice = dg0.CurrentRow.Cells["IDV_INVOICE"].Value.ToString();// expresion.Split('-')[0].Trim();
                    string calidad = dg0.CurrentRow.Cells["CALIDAD"].Value.ToString();// expresion.Split('-')[3].Trim();
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
                                dv.Table.Rows.Find(keys)["checked"] = cellSelecion.Value;

                                lblarticuloselect.Text = dv.Table.AsEnumerable().Count(x => x.Field<Boolean>("checked") == true).ToString();

                                ///
                                dtv_grilla = dv;
                                //Session[_nameSessionArtsRet] = dv;
                            }
                            catch
                            {
                                pninfo.Visible = true;
                                lblinfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#ee7749");
                                lblinfo.Text = " > Ha ocurrido un error seleccionando el artículo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.";

                                //msnMessage.LoadMessage(" > Ha ocurrido un error seleccionando el artículo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Error);
                            }
                        }
                    }
                }
            }
        }

        private void dwestado_Leave(object sender, EventArgs e)
        {
            dwestado.Text = dwestado.Text.ToUpper();
        }

        private void dwestado_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!dwestado.Focused) return;
            string _cod = dwestado.SelectedValue.ToString();

            if (_cod == "01")
            {
                MessageBox.Show("SI SELECCIONA ESTA OPCION [" + dwestado.Text.ToUpper() + "] , ANULARA LA OPERACION DEL DOCUMENTO DE REFERENCIA", Global.mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      
        //private void chk_evento(object sender, System.EventArgs e)
        //{

        //}

       
    }
}

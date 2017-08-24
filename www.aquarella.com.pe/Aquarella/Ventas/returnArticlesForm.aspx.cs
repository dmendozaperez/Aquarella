using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Logistica;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Logistica;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll.Ventas;
//using Bata.Aquarella.BLL.Ventas;
using Jayrock.Json;

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class returnArticlesForm : System.Web.UI.Page
    {
        
        /// <summary>
        /// Pagina del reporte de la factura
        /// </summary>
        String _pageReportInvoice = "../../Reports/Ventas/reportInvoice.aspx";

        /// <summary>
        /// Nombre de sesion en la cual se guarda el dataview de articulos adicionados a la devolucion
        /// </summary>
        String _nameSessionArtsRet = "nameSessionArtsRet";

        /// <summary>
        /// Pagina del reporte de los articulos devueltos
        /// </summary>
        String _pageReportArtsReturned = "../../Reports/Ventas/reportArticlesReturned.aspx";

        /// <summary>
        /// Objeto User que contiene toda la información del usuario.
        /// </summary>
        Users _user;


        /// <summary>
        /// Load de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            /// Verificar que no se haya vencido la session
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session,Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            

            /// Agregar el evento que manejara el enter dado desde la caja de texto
            txtCodigoArticulo.Attributes.Add("onkeypress", "KeyboardHandler('" + btFindArticle.ClientID + "')");
            /// Agregar control de seleccion de texto
            txtCodigoArticulo.Attributes.Add("onfocus", "SetSelected('" + txtCodigoArticulo.ClientID + "');");
            

            if (!IsPostBack)
            {

                if ((_user._usu_tip_id == "02"))
                {
                    Utilities.logout(Page.Session, Page.Response);
                }
                else
                {
                    this.formForEmployee();
                }

                //if (_user._usv_employee)
                //    this.formForEmployee();
                //else Utilities.logout(Page.Session, Page.Response);


                if (!_user._usn_userid.Equals("%%"))
                {
                    //lblWhoIs.Text = " ( " + _user._usv_warehouse_name + " ) - " + _user._usv_name;
                    lblWhoIs.Text = " ( " + _user._nombre + " )  ";// - " + _user._usv_name;
                }
                else {
                    lblWhoIs.Text = " ( Usuario Nal. ) - " + _user._usv_name;
                }
                ///
                DataTable dt = this.createStructureDataTable();
                Session[_nameSessionArtsRet] = new DataView(dt);

                ///
                DataSet dtStorages = Storages.getStorageByWereHouse();
                dwStorages.DataSource = dtStorages;
                dwStorages.DataBind();

                dwStorages.SelectedValue = "11";
                dwStorages.Enabled = false;
               

            }
            
        }

        protected void formForEmployee()
        {
            /// Realizar la consulta de coordinadores
            DataSet dsCustomers = Coordinator.getCoordinators(_user._usv_area,_user._asesor);
            dwCoordinadores.Focus();
            // Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            dwCoordinadores.DataSource = dsCustomers;
            dwCoordinadores.DataBind();
        }

        /// <summary>
        /// Seleccion de coordinadores
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dwCoordinadores_SelectedIndexChanged(object sender, EventArgs e)
        {           
            String coordinatorSelect = dwCoordinadores.SelectedValue;

            /// Verificar que sea una selección valida
            if (coordinatorSelect != "-1")
            {
                /// Obtener Informacion del coordinador
                DataTable dtable = new DataTable();
                dtable = (Coordinator.getCoordinatorByPk(Convert.ToDecimal(coordinatorSelect)).Tables[0]);
                /// Llamar la funcion que imprimira la informacion del coordinador
                this.paintInfoUser(dtable);

                /// Consultar saldos y montos en pedidos del cliente
                //this.ordersCustomer(Documents_Trans.getBalanceCoordById(_user._usv_co, coordinatorSelect).Tables[0]);

                /// Cargar el id del coordinador seleccionado
                /// 
                hdIdCoordinator.Value = coordinatorSelect;
                
                ///
                this.cleanForm();
            }
            ///
            this.txtNoInvoice.Focus();
        }


        /// <summary>
        /// Método que imprime los datos principales de los clientes
        /// </summary>
        /// <param name="dt"></param>
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
                txtDocumento.Text = Convert.ToString(dtable.Rows[0]["Bas_Documento"]);
                /// Nombre completo
                /// 
                txtNombre.Text = Convert.ToString(dtable.Rows[0]["NombreCompleto"]);
                txtNombre.ToolTip = Convert.ToString(dtable.Rows[0]["NombreCompleto"]);
                /// Dirección
                /// 
                txtDireccion.Text = Convert.ToString(dtable.Rows[0]["Bas_Direccion"]);
                txtDireccion.ToolTip = Convert.ToString(dtable.Rows[0]["Bas_Direccion"]);
                /// Teléfono
                /// 
                txtTelefono.Text = Convert.ToString(dtable.Rows[0]["Bas_Telefono"]);
                /// E-Mail
                /// 
                txtMail.Text = Convert.ToString(dtable.Rows[0]["bas_correo"]);
                txtMail.ToolTip = Convert.ToString(dtable.Rows[0]["bas_correo"]);
                ///
                /// Ubicacion customer
                /// 
                txtUbicacion.Text = Convert.ToString(dtable.Rows[0]["Ubicacion"]);
                ///
                txtUbicacion.ToolTip = Convert.ToString(dtable.Rows[0]["Ubicacion"]);
            }

        }

        /// <summary>
        /// Pedidos, saldos y faltantes en la cuenta del cliente al cual se le realiza la devolucion de mercancia
        /// </summary>
        /// <param name="dtable"></param>
        public void ordersCustomer(DataTable dtable)
        {
            /// Verificar que efectivamente el dataTable contenga resgistros
            if (dtable.Rows.Count > 0)
            {
                ///
                lblSaldo.Text = (Convert.ToDecimal(dtable.Rows[0]["total"])).ToString("N0");
                lblSaldo.ForeColor = Color.Black;
                ///
                Decimal ordersValue = Convert.ToDecimal(dtable.Rows[0]["liquidaciones"]);
                ///
                if (ordersValue > 0)
                {
                    lblOrders.ForeColor = Color.Red;
                    lblRest.ForeColor = Color.Red;
                }
                else
                {
                    lblOrders.ForeColor = Color.Black;
                    lblRest.ForeColor = Color.Black;
                }
                lblOrders.Text = (ordersValue).ToString("N0");
                //
                lblRest.Text = (Convert.ToDecimal(dtable.Rows[0]["consignar"]) < 0 ? 0 : Convert.ToDecimal(dtable.Rows[0]["consignar"])).ToString("N0");
            }
        }


        /// <summary>
        /// Limpiar forumulario de devolucion e iniciar uno nuevo
        /// </summary>
        public void cleanForm()
        {
            /// Reiniciar el HashTable de articulos retornados
            ///this.setHashTableInSession(new Hashtable());
            ///
            ///this.fillGridView(new Hashtable());
            ///
            ///
            DataTable dt = this.createStructureDataTable();
            Session[_nameSessionArtsRet] = new DataView(dt);
            ///
            this.GridViewArticlesToReturn.DataSource = Session[_nameSessionArtsRet];
            this.GridViewArticlesToReturn.DataBind();
            ///
            this.calculateTotals();
            ///
            this.dwCoordinadores.Focus();
            ///
            txtNoInvoice.Text = "";
            txtCodigoArticulo.Text = "";
            ///
            
            msnMessage.Visible = false;
            ///
            ///this.sumQtysReturned(0);
            ///this.sumValueOfMoneyToReturn(0);
        }



        /// <summary>
        /// Calculo del iva
        /// </summary>
        /// <param name="taxes"></param>
        /// <param name="handling"></param>
        /// <param name="dsctoGral"></param>
        /// <param name="baseFac"></param>
        /// <returns></returns>
        public Decimal calculoIva(Decimal taxes, Decimal handling, Decimal dsctoGral, Decimal baseFac)
        {
            ///
            Decimal taxPercent = taxes / (baseFac + handling - dsctoGral);
            ///
            return taxPercent;
        }

        /// <summary>
        /// 
        /// </summary>
        public void calculateTotals()
        {
            try
            {
                DataView dv = (DataView)Session[_nameSessionArtsRet];
                ///
                Decimal query = dv.Table.AsEnumerable().Sum(x => x.Field<Decimal>("IDN_QTY"));
                lblCantsReturned.Text = query.ToString();
                ///
                lblTotalValorDevolucion.Text = String.Format("{0:C2}",
                    dv.Table.AsEnumerable().Sum(y => (y.Field<Decimal>("IDN_SELLPRICE") * y.Field<Decimal>("IDN_QTY")) -
                        (y.Field<Decimal>("idn_commission") + y.Field<Decimal>("idn_disscount")) + y.Field<Decimal>("taxes")));
            }
            catch
            { }
        }


        /// <summary>
        /// Creacion de la estructura que compone el dataview de articulos a devolver
        /// </summary>
        /// <returns></returns>
        public DataTable createStructureDataTable()
        {
            DataTable dtEstructura = new DataTable();
            ///
            ///dtEstructura.Columns.Add("idRow", typeof(int));
            ///
            dtEstructura.Columns.Add("IDV_INVOICE", typeof(String));
            ///
            dtEstructura.Columns.Add("IDV_ARTICLE", typeof(String));
            ///
            dtEstructura.Columns.Add("ARV_NAME", typeof(String));
            ///
            dtEstructura.Columns.Add("brv_description", typeof(String));
            ///
            dtEstructura.Columns.Add("cov_description", typeof(String));
            ///
            dtEstructura.Columns.Add("IDV_SIZE", typeof(String));

            dtEstructura.Columns.Add("CALIDAD", typeof(String));
            ///
            dtEstructura.Columns.Add("idn_qty_line", typeof(Decimal));
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
            dtEstructura.Columns.Add("checked", typeof(Boolean));


            /// Valores de la columna unicos /// 
            ///dtEstructura.Columns["idRow"].Unique = true;

            /// Primary key
            ///DataColumn[] keys = new DataColumn[1];
            DataColumn[] keys = { dtEstructura.Columns["idv_invoice"],
                                        dtEstructura.Columns["idv_article"],
                                    dtEstructura.Columns["idv_size"],
                                dtEstructura.Columns["calidad"]};
            ///keys[0] = dtEstructura.Columns["ldv_article"];
            ///keys[0] = dtEstructura.Columns["idRow"];
            ///keys[1] = dtEstructura.Columns["IDV_ARTICLE"];

            /// Set Prmary Key
            dtEstructura.PrimaryKey = keys;

            ///
            return dtEstructura;
        }


        /// <summary>
        /// Buscar articulo y/o verificar que sea un articulo que se pueda retornar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btFindArticle_Click(object sender, EventArgs e)
        {
            
            ///
            msnMessage.Visible = false;
            ///
            String article = "";
            ///
            String size = "";
            string calidad = "";
            ///
            String[] infoCodeBars;
            ///
            if (!txtCodigoArticulo.Text.Equals(String.Empty))
            {
                /// Pos 0: Articulo referencia
                /// Pos 1: Plano
                infoCodeBars = BarCodes.getInfoFromTheBarCode(txtCodigoArticulo.Text);
                ///
                if (infoCodeBars == null)
                {
                    msnMessage.LoadMessage("Codigo de articulo invalido.", UserControl.ucMessage.MessageType.Error);                   
                    return;
                }
                ///
                article = infoCodeBars[0].ToString();
                calidad = infoCodeBars[2].ToString();
                /// Para saber si se debe consultar la talla legible se debe medir el numero de digitos; El EAN13 posee 14
                /// 
                if (txtCodigoArticulo.Text.Length == 13)
                {
                    ///
                    DataTable dtInfoArticle = Article.getInfoDecodifyCodeBars(_user._usv_co, article, "", (Convert.ToDecimal(infoCodeBars[1]) - 1).ToString());

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

                loAQUARELLArticleForReturn(txtNoInvoice.Text.Trim(), article, size,calidad);
            }
            ///
            txtCodigoArticulo.Focus();
            
        }

        /// <summary>
        /// Adicionar articulo a la devolucion
        /// </summary>
        /// <param name="noInvoice"></param>
        /// <param name="article"></param>
        /// <param name="size"></param>
        public void loAQUARELLArticleForReturn(String noInvoice, String article, String size,String calidad)
        {
            ///
            DataView dv = ((DataView)Session[_nameSessionArtsRet]);
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
                            /// Articulo existente, ya adicionado previamente, verificar si tiene mas cantidades habilitAQUARELLAs para su devolucion
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
                                msnMessage.LoadMessage(" > Se han sumado las cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Information);
                                ///
                                Session[_nameSessionArtsRet] = dv;
                                ///
                                this.calculateTotals();
                                this.GridViewArticlesToReturn.DataSource = dv;
                                this.GridViewArticlesToReturn.DataBind();
                            }
                            else
                               msnMessage.LoadMessage(" > No puede devolver más cantidades del articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.", UserControl.ucMessage.MessageType.Error);
                        }
                    }
                    catch
                    {
                        /// En ocaciones hay catch cuando la consulta linq no obtiene ningun resultado
                        /// Adicionar una nueva linea
                        addArticle(txtNoInvoice.Text, article, size, hdIdCoordinator.Value,calidad);
                    }
                }
                else
                    /// Adicionar una nueva linea
                    addArticle(txtNoInvoice.Text, article, size, hdIdCoordinator.Value,calidad);
            }
        }


        /// <summary>
        /// Adicionar articulo a la devolucion en proceso
        /// </summary>
        /// <param name="noInvoice"></param>
        /// <param name="article"></param>
        /// <param name="size"></param>
        /// <param name="customer"></param>
        public void addArticle(String noInvoice, String article, String size, String customer,string calidad)
        {
            /// Realizar la busqueda del articulo en la factura
            DataTable dtArticleInvoiced = Facturacion.searchArticleInvoice( noInvoice, article, size, customer,calidad);

            /// Verificar que existan datos /// 
            if (dtArticleInvoiced != null && dtArticleInvoiced.Rows.Count > 0)
            {
                ///
                DataView dv = ((DataView)Session[_nameSessionArtsRet]);
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
                    /// Cantidades facturAQUARELLAs del articulo en la misma linea
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
                    ivaIndividual = (sellPrice + fleteIndividual - (comision/qtyInvoice) - dsctoGralIndividualizado - dctoLinealArt) * ivaIndividual; ///sellPrice* ivaIndividual; 
                    ///
                    //newRow["TAXES"] = Math.Round(ivaIndividual, 2);
                    newRow["TAXES"] = ivaIndividual;
                    ///
                    newRow["checked"] = false;

                    ///
                    dv.Table.Rows.Add(newRow);
                    ///                    
                    msnMessage.LoadMessage(" > Se ha agregado el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Information);
                    this.GridViewArticlesToReturn.DataSource = dv;
                    this.GridViewArticlesToReturn.DataBind();
                    Session[_nameSessionArtsRet] = dv;
                    this.calculateTotals();

                }
            }
            else                
             msnMessage.LoadMessage(" > No se puede devolver el articulo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-; busque otra factura.", UserControl.ucMessage.MessageType.Error);
        }

        /// <summary>
        /// Realizar la devolucion de articulos cargados en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btReturnArticles_Click(object sender, EventArgs e)
        {   
            ///
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
                DataView dv = ((DataView)Session[_nameSessionArtsRet]);

                // Existen articulos que mover de storage
                Decimal articlesToMove = dv.Table.AsEnumerable().Count(x => x.Field<Boolean>("checked") == true);

                if (articlesToMove > 0)
                {
                    //
                    if (string.IsNullOrEmpty(dwStorages.SelectedValue))
                    {
                        dwStorages.Focus();
                        msnMessage.LoadMessage("> Seleccione el area o storage a donde se enviaran los artículos seleccionados.", UserControl.ucMessage.MessageType.Information);
                     
                    }
                    else
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
                            Returns_Dtl objReturned = new Returns_Dtl(invo, art, size, qty, dwStorages.SelectedValue, calidad);
                            /// Agregar el objeto a la lista generica
                            lstArticlesReturned.Add(objReturned);
                            ///
                            //itemsTransDetails.Add(new Transaction_det(noLinea, art, size, Convert.ToInt16(qty), 0, 0));
                            // Aumentar el numero de linea.
                            //noLinea++;
                        }
                        else {
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
                    //if (string.IsNullOrEmpty(_user._usv_warehouse) || _user._usv_warehouse.Equals("%%"))
                    //{
                    //    ///
                    //    msnMessage.LoadMessage(" > Usted no esta asociado a ninguna bodega, la devoluci&oacute;n no podrá ser realizAQUARELLA.", UserControl.ucMessage.MessageType.Error);                        
                    //}
                    //else
                    //{
                        // Devolucion                        
                    string[] results = Returns_Hdr.saveReturnOrder(hdIdCoordinator.Value, dwStorages.SelectedValue, lstArticlesReturned,_user._bas_id);

                        if (results != null)
                        {
                            //                
                            string url = _pageReportArtsReturned + "?noReturn=" + results[0];// + "&st=" + Constants.StatusReturnForAprob;

                            // Async 
                            //Log_Transaction.registerUserInfo(_user, "CREATE RETURN_HDR:" + results[0] + " USER:" + _user._usv_username);
                            //
                            try
                            {
                                //
                                string winAlert = "location.href='" + url + "'";
                                System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel3, Page.GetType(), "click", winAlert, true);
                                ///      
                                this.cleanForm();
                                msnMessage.LoadMessage(" > La Devoluci&oacute;n Se Ha Realizado Correctamente : " + results[0] + "<br /> > <a href='" + url + "' target='Blank'>Ver Reporte Devolución</a>", UserControl.ucMessage.MessageType.Information);
                                
                            }
                            catch
                            {
                                //
                                msnMessage.LoadMessage(" > La Devoluci&oacute;n No Se Ha Realizado Correctamente." + results[0]+"<br /> > El reporte de la devoluci&oacute;n no se ha generado; <a href='" + url + "' target='Blank'>Ver Reporte Devolucion</a>", UserControl.ucMessage.MessageType.Error);
                            }
                            /*finally
                            {
                                /// Movimiento de storage
                                if (noLinea > 0)
                                {
                                    String resultado = "";
                                    String storageOut = results[2];
                                    String storageIn = dwStorages.SelectedValue;
                                    String refDoc = "1000";


                                    //Insertar en base de datos el documento.
                                    resultado = STOCKTANSFERSTORAGE.SaveTransfer(_user._usv_co, storageOut, storageIn, refDoc, Bata.Aquarella.BLL.Util.ValuesDB.acronymStatusFinalized, itemsTransDetails).Trim();
                                    //
                                    if (!resultado.Equals("0 - 0"))
                                    {                                     
                                        msnMessage.LoadMessage(msnMessage.Message + "<br /> > Éxito al cambiar de Storage los artículos seleccionados; Nuevos Documentos : " + resultado + ".", UserControl.ucMessage.MessageType.Information);                                       
                                    }
                                    else
                                    {
                                        msnMessage.LoadMessage(msnMessage.Message + "<br /> > ERROR al realizar el intercambio de storages hacia : " + dwStorages.SelectedItem.Text, UserControl.ucMessage.MessageType.Error);                                      
                                    }
                                }
                                else
                                {
                                    msnMessage.LoadMessage(msnMessage.Message + "<br /> >Sin artículos seleccionados que mover de area o Storage.", UserControl.ucMessage.MessageType.Error);                                    
                                }
                            }*/
                        }
                        else
                        {
                            ///
                            msnMessage.LoadMessage(" > La Devoluci&oacute;n No Se Ha Realizado Correctamente.", UserControl.ucMessage.MessageType.Error);                            
                        }
                    //}
                }
            }
            catch
            {
                ///
                msnMessage.LoadMessage(" > Ha ocurrido un error y la devoluci&oacute;n no se ha realizado correctamente.", UserControl.ucMessage.MessageType.Error);                
            }
             
        }

        /// <summary>
        /// Limpiar toda la pantalla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btReset_Click(object sender, EventArgs e)
        {
            ///
            this.cleanForm();
        }


        /// <summary>
        /// Generar reporte de factura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imbPrintInvoice_Click(object sender, ImageClickEventArgs e)
        {
            ///        
            if (!txtNoInvoice.Text.Equals(String.Empty))
            {
                ///
                DataTable dtInfoHdrInvoice = Facturacion.getInvoiceHdrByNoInvoice(txtNoInvoice.Text);

                ///
                if (dtInfoHdrInvoice != null && dtInfoHdrInvoice.Rows.Count > 0)
                {
                    ///
                    String noLiquidation = dtInfoHdrInvoice.Rows[0]["Ven_LiqId"].ToString();
                    String noInvoice = dtInfoHdrInvoice.Rows[0]["Ven_Id"].ToString();

                    ///
                    this.showReportOfInvoice(noLiquidation, noInvoice);
                }
            }
        }

        protected void RowLevelCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //
            CheckBox checkbox = ((CheckBox)sender);
            ///
            string expresion = checkbox.ToolTip;
            ///
            string article = expresion.Split('-')[1].Trim();
            string size = expresion.Split('-')[2].Trim();
            string noInvoice = expresion.Split('-')[0].Trim();
            string calidad = expresion.Split('-')[3].Trim();
            ///
            DataView dv = ((DataView)Session[_nameSessionArtsRet]);
            ///
            if (dv != null)
            {
                if (dv.Table.Rows.Count > 0)
                {
                    try
                    {
                        /// Llaves de consulta
                        String[] keys = { noInvoice, article, size,calidad };

                        /// Actualizar datos
                        dv.Table.Rows.Find(keys)["checked"] = checkbox.Checked;

                        lblUndsSelected.Text = dv.Table.AsEnumerable().Count(x => x.Field<Boolean>("checked") == true).ToString();

                        ///
                        Session[_nameSessionArtsRet] = dv;
                    }
                    catch
                    {
                        msnMessage.LoadMessage(" > Ha ocurrido un error seleccionando el artículo -" + article + "- en talla -" + size + "- en la factura No. -" + noInvoice + "-.", UserControl.ucMessage.MessageType.Error);                          
                    }
                }
            }
        }

        protected void GridViewArticlesToReturn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            /// verificar que el boton de eliminar articulo empacado sea el presionado ///
            if (e.CommandName == "ibDeleteArticleReturned")
            {
                try
                {
                    /// Recuperar información de la linea deseAQUARELLA a eliminar. formato NoFactura@Articulo@Talla
                    string invo = e.CommandArgument.ToString().Split('@')[0];
                    ///
                    string art = e.CommandArgument.ToString().Split('@')[1];
                    ///
                    string size = e.CommandArgument.ToString().Split('@')[2];

                    string calidad = e.CommandArgument.ToString().Split('@')[3];
                    ///
                    string[] keys = { invo, art, size,calidad };
                    ///
                    DataView dv = (DataView)Session[_nameSessionArtsRet];
                    /// 
                    dv.Table.Rows.Find(keys).Delete();
                    /// Set the new DataView
                    Session[_nameSessionArtsRet] = dv;
                    ///
                    this.calculateTotals();
                    ///                    
                    msnMessage.LoadMessage(" > El artículo -" + art + "- en talla -" + size + "- de la factura -" + invo + "- ha sido eliminado.", UserControl.ucMessage.MessageType.Information);
                    ///
                    this.GridViewArticlesToReturn.DataSource = dv;
                    this.GridViewArticlesToReturn.DataBind();
                }
                catch
                {                    
                    msnMessage.LoadMessage(" > Error intentando eliminar el artículo.", UserControl.ucMessage.MessageType.Error);
                }

            }
        }

        protected void GridViewArticlesToReturn_RowCreated(object sender, GridViewRowEventArgs e)
        {
            // The GridViewCommandEventArgs class does not contain a 
            // property that indicates which row's command button was
            // clicked. To identify which row's button was clicked, use 
            // the button's CommandArgument property by setting it to the 
            // row's index.
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                /// Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());

                /// Numeracion de registros /// 
                e.Row.Cells[0].Text = (idRow + 1) + "";
            }
        }


        #region < REPORTES >

        /// <summary>
        /// Reporte de factura
        /// </summary>
        /// <param name="noLiquidation"></param>
        /// <param name="noFactura"></param>
        public void showReportOfInvoice(String noLiquidation, String noFactura)
        {
            if (!noFactura.Equals("-1"))
            {
                ///
                msnMessage.LoadMessage("Registro Realizado Satisfactoriamente !!; Número de Factura : " + noFactura, UserControl.ucMessage.MessageType.Information);

                //                
                string url = _pageReportInvoice + "?NoLiquidation=" + noLiquidation + "&NoInvoice=" + noFactura;

                // Actualizar el numero de veces impresas de la factura
                //Facturacion.updateNoPrintsInvoice(_user._usv_co, noFactura);

                //Response.Redirect(url);
            }
            else
                msnMessage.LoadMessage("Error en la generación del Reporte.", UserControl.ucMessage.MessageType.Error);         
        }

        #endregion

        #region < AJAX AND JSON METHOD'S >

        //[AcceptVerbs(HttpVerbs.Post)]
        [WebMethod()]
        public static Object ajaxGetInvoicesForArticle(String article, String customer)
        {
            ///
            /// Deido a que la funcion es estatica se deben crear referencias para podre hacer llamados a algunos metodos
            System.Web.SessionState.HttpSessionState sessions = HttpContext.Current.Session;


            Users user = (Users)sessions[Constants.NameSessionUser];

            // Cargar session de compañia
            String company = user._usv_co;

            String art = "";
            ///
            String size = "";
            ///
            String[] infoCodeBars;
            ///
            if (!String.IsNullOrEmpty(article))
            {
                /// Pos 0: Articulo referencia
                /// Pos 1: Plano
                infoCodeBars = BarCodes.getInfoFromTheBarCode(article);
                ///
                if (infoCodeBars == null)
                {
                    ///lblMsg.Text = "Codigo de articulo invalido.";
                    //return;
                }
                ///
                art = infoCodeBars[0].ToString();
                /// Para saber si se debe consultar la talla legible se debe medir el numero de digitos; El EAN13 posee 14
                /// 
                if (article.Length == 13)
                {
                    size = (Convert.ToDecimal(infoCodeBars[1]) - 1).ToString();
                }
                else
                {
                    ///
                    size = infoCodeBars[1].ToString();
                }
            }
            ///
            DataTable results = Facturacion.getSalesDevolByCoord(company, customer, art, size);

            ///
            JsonArray jj = new JsonArray();
            ///
            JsonObject jso = new JsonObject();
            ///
            if (results != null && results.Rows.Count > 0)
            {
                foreach (DataRow dr in results.Rows)
                {
                    ///
                    jso = new JsonObject();
                    /// Fecha factura
                    jso.Put("ihd_date", dr["ihd_date"].ToString().Trim());
                    /// Nombre articulo
                    jso.Put("Nombre", dr["arv_name"].ToString().Trim());
                    /// Referencia
                    jso.Put("Ref", dr["idv_article"].ToString().Trim());
                    /// Marca
                    jso.Put("brand", dr["brv_description"].ToString().Trim());
                    /// Color
                    jso.Put("Color", dr["cov_description"].ToString().Trim());
                    /// Talla
                    jso.Put("Size", dr["idv_size"].ToString().Trim());
                    /// Cantidades en factura
                    jso.Put("Qty", dr["idn_qty"].ToString().Trim());
                    /// Factura
                    jso.Put("idv_invoice", dr["idv_invoice"].ToString().Trim());
                    /// qty devueltas
                    jso.Put("qty_devol", dr["rdn_qty"].ToString().Trim());
                    /// Sell price
                    jso.Put("idn_sellprice", Convert.ToDecimal(dr["idn_sellprice"]).ToString("n0"));
                    /// Cantidades habilitAQUARELLAs para la devolucion
                    jso.Put("qty_to_devol", dr["qty_to_devol"].ToString());
                    ///  precio neto aproximado al cual se realizara la devolucion
                    jso.Put("idn_neto", Convert.ToDecimal(dr["idn_neto"]).ToString("n0"));
                    /// Numero de devolucion en caso tal de que haya sido devuelto
                    jso.Put("rdv_return", dr["rdv_return"].ToString());
                    ///
                    jj.Put(jso);
                }
            }
            else
            {
                ///
                jso = new JsonObject();
                jso.Put("Nombre", "No Existen Compras resgistrAQUARELLAs de este cliente en este articulo.");
                jso.Put("Size", "-1");
                jj.Put(jso);
            }
            ///
            JsonObject jsoGeneral = new JsonObject();
            ///
            Object[] obj = new Object[2];
            ///
            var ret = jj.ToArray();
            return ret;
        }

        #endregion

        
       
    }
}
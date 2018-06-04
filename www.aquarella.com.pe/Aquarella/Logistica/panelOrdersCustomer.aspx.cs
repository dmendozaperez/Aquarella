using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI;
using www.aquarella.com.pe.UserControl;
using System.Globalization;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class panelOrdersCustomer : System.Web.UI.Page
    {
        Users _user;
        Coordinator _cust;
        string _nameSessionData = "OrdersCustomers_", _pageLiquidReport = "panelFramesLiqReports.aspx",
            _nameSessionShipTo = "ShippingInfoObj", _nameSessionCustomer = "nameSessionCustomer";

        string _nameSessionDataC = "liquidationValues";

        //#region <variables del crystal reports>
        private ArrayList _liqValsReport;
        private ArrayList _liqValsSubReport;
        private ArrayList _liqValsPagoSubReport;

        string reportPath;
        string _nameFileCrystalReport = "liquidationReport.rpt";

        private ReportDocument _liqObjReport;

        //#endregion

        #region < Funciones de inicio >
        
        /// <summary>
        /// Load de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session["codigo_promotor"]=null;
                Session["aplica_percepcion_cliente"] = null;
                h_numConfigPagoPOS.Value = ConfigurationManager.AppSettings["ID_Num_Tarjeta_POS"];
                hdestado.Value = "0";
                Session[_nameSessionData] = new object();
                Session[_nameSessionShipTo] = new Transporters_Guides();
                Session[_nameSessionCustomer] = new Coordinator();

                if ((_user._usu_tip_id == "02"))
                {
                    this.formForCustomer();
                }
                else
                {
                    this.formForEmployee();
                }

                //if (_user._usv_employee)
                //    this.formForEmployee();
                //else if (_user._usv_customer)
                //    this.formForCustomer();
            }
        }
       
        /// <summary>
        /// Set de variables necesarias para el funcionamiento del modulo
        /// </summary>
        /// <param name="idWare"></param>
        /// <param name="areaId"></param>
        protected void setEnviromentVars( string areaId)
        {
            /// Cargar el id de la bodega para realizar la consulta.
            

            /// Area
            hdArea.Value = areaId;
        }

        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        protected void formForEmployee()
        {
            this.setEnviromentVars( _user._usv_area);

            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de coordinadores
            DataSet dsCustomers =  Coordinator.getCoordinators( _user._usv_area,_user._asesor);
            dwCustomers.Focus();
            // Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            dwCustomers.DataSource = dsCustomers;
            dwCustomers.DataBind();
        }

        /// <summary>
        /// Preparar formulario para cliente
        /// </summary>
        protected void formForCustomer()
        {
            this.setEnviromentVars( _user._usv_area);

            // Ocultar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = false;

            Session["codigo_promotor"] = _user._usn_userid;

            this.paintInfoCustomer(Coordinator.getCoordinatorByPk( _user._usn_userid));
            // Cargar los pedidos por promotor, las liquidaciones y las devoluciones pertenecientes a este coordinador
            getOrdLiqAnsRet(_user._usn_userid);
        }

        /// <summary>
        /// Mostrar informacion del cliente
        /// </summary>
        /// <param name="dsCustomer"></param>
        protected void paintInfoCustomer(DataSet dsCustomer)
        {
            msnMessage.HideMessage();
            if (dsCustomer == null || dsCustomer.Tables[0].Rows.Count <= 0)
                return;

            pnlCustInfo.Visible = true;
            DataRow dRow = dsCustomer.Tables[0].Rows[0];
            
            Session["aplica_percepcion_cliente"] = Convert.ToBoolean(dRow["aplica_percepcion"].ToString());

            Coordinator cust = new Coordinator
            {
                //_co = dRow["cov_co"].ToString(),
                _commission = Convert.ToDecimal(dRow["Con_Fig_PorcDesc"]),
                _idCust = Convert.ToDecimal(dRow["bas_id"]),
                //_idWare = dRow["cov_warehouseid"].ToString(),
                _taxRate = Convert.ToDecimal(dRow["Con_Fig_Igv"]),
                _commission_POS_visaUnica = Convert.ToDecimal(dRow["Con_Fig_PorcDescPos"]),
                _percepcion = Convert.ToDecimal(dRow["Con_Fig_Percepcion"]),
                _email = dRow["bas_correo"].ToString(),
                _nombrecompleto = dRow["nombrecompleto"].ToString(),
                _aplica_percepcion =Convert.ToBoolean(dRow["aplica_percepcion"].ToString())
            };
            Session[_nameSessionCustomer] = cust;

            Transporters_Guides shipping = new Transporters_Guides
            {
                _tgv_name_cust = dRow["nombrecompleto"].ToString(),
                _tgv_phone_cust = dRow["Bas_Telefono"].ToString(),
                _tgv_movil_cust = dRow["Bas_Celular"].ToString(),
                _tgv_shipp_add = dRow["Bas_Direccion"].ToString(),
                _tgv_city = dRow["dis_descripcion"].ToString(),
                _tgv_depto = dRow["dis_dep_id"].ToString()
            };
            Session[_nameSessionShipTo] = shipping;

            // Documento
            lblDocument.Text = dRow["Bas_Documento"].ToString();
            // Nombre completo
            lblFullName.Text = dRow["NombreCompleto"].ToString();
            lblFullName.ToolTip = dRow["NombreCompleto"].ToString();
            // Dirección y telefono
            lblDirPhones.Text = dRow["Bas_Direccion"].ToString() + " - " + dRow["Bas_Telefono"].ToString() + " - " + dRow["Bas_Celular"].ToString();
            lblDirPhones.ToolTip = dRow["Bas_Direccion"].ToString() + " - " + dRow["Bas_Telefono"].ToString() + " - " + dRow["Bas_Celular"].ToString();
            // E-Mail
            lblMail.Text = (string.IsNullOrEmpty(dRow["bas_correo"].ToString()) ? "Sin correo" : "<a href='mailto:" + dRow["bas_correo"].ToString() + "' target='_Blank'>" + dRow["bas_correo"].ToString() + "</a>");
            lblMail.ToolTip = "Clcik sobre la dirección de correo electronico para enviar un mensaje.";
            // Ubicacion customer
            lblUbication.Text = dRow["Ubicacion"].ToString();
            lblUbication.ToolTip = dRow["Ubicacion"].ToString();

            lblagencia.Text = dRow["Bas_Agencia"].ToString();
            lblagencia.ToolTip = dRow["Bas_Agencia"].ToString();

            lbldestino.Text= dRow["Bas_Destino"].ToString();
            lbldestino.ToolTip = dRow["Bas_Destino"].ToString();

            // Logistica
            lblLogistica.Text = dRow["Are_Descripcion"].ToString();// +", " + dRow["arv_description"].ToString();

            lblasesor.Text = dRow["asesor"].ToString();

            lblPremio.Text = "Sin premio";

            lblPremio.Text = dRow["Premio"].ToString();
        }


        #endregion
                
        /// <summary>
        /// Consulta de pedidos borrador, liquidaciones y devoluciones
        /// </summary>
        /// <param name="co"></param>
        /// <param name="idCustomer"></param>
        protected void getOrdLiqAnsRet(decimal idCustomer)
        {
            try
            {
                DataSet dsResults = Coordinator.getOrdLiqAnsRet(idCustomer);

                if (dsResults == null || dsResults.Tables.Count <= 0)
                    return;

                Session[_nameSessionData] = dsResults;

                gvEraseOrders.DataSourceID = odsOrders.ID;
                gvEraseOrders.DataBind();

                gvLiquidations.DataSourceID = odsLiquidations.ID;
                gvLiquidations.DataBind();

                gvReturns.DataSourceID = odsReturns.ID;
                gvReturns.DataBind();

                gvconsignacion.DataSourceID = odsconsignacion.ID;
                gvconsignacion.DataBind();

                gvsf.DataSourceID = odsfavor.ID;
                gvsf.DataBind();

                gvventa.DataSourceID = odsventa.ID;
                gvventa.DataBind();
            }
            catch(Exception exc)
            {
                return;
            }
        }

        /// <summary>
        /// Cambio en la seleccion de cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dwCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nuevo cliente seleccionado
            string selectedCustomer = ((DropDownList)sender).SelectedValue;

            /// Verificar que sea una selección valida
            if (!string.IsNullOrEmpty(selectedCustomer) && selectedCustomer != "-1")
            {
                Session["codigo_promotor"] = Convert.ToDecimal(selectedCustomer);
                // Funcion que imprimira la informacion del cliente
                this.paintInfoCustomer(Coordinator.getCoordinatorByPk(Convert.ToDecimal(selectedCustomer)));
                // Cargar pedidos en borrador, liquidaciones y devoluciones
                getOrdLiqAnsRet( Convert.ToDecimal(selectedCustomer));
                
            }
            else
                Session[_nameSessionCustomer] = new Coordinator();
        }

        #region < Eventos sobre GridView >

        /// <summary>
        /// Evento de creacion de fila del grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLiquidations_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                // Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());
               
                try
                {
                    string noInv = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Ven_Id").ToString();
                    if (!string.IsNullOrEmpty(noInv))
                    {
                        //
                        string noLiq = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Liq_Id").ToString();
                        ///
                        Image imgShowReportInvoice = (Image)e.Row.FindControl("imgInv");
                        ///
                        imgShowReportInvoice.Visible = true;
                    }

                    //edicion de la liquidacion
                    string status = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Liq_EstId").ToString();
                    if (status == "PS" || status == "PC")
                    {
                        //
                        string noLiq = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Liq_Id").ToString();
                        ///
                        ImageButton imgShowReportInvoice = (ImageButton)e.Row.FindControl("imgedit");
                        ImageButton imganular = (ImageButton)e.Row.FindControl("ibanular");
                        
                        ///
                        imgShowReportInvoice.Visible = true;
                        imganular.Visible = true;
                    }
                }
                catch
                {
                }
            }
        }

        protected void gvventa_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                // Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());

                try
                {
                    string status = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Ven_Est_Id").ToString();
                    if (status== "FA")
                    {
                        //
                        string noLiq = System.Web.UI.DataBinder.Eval(e.Row.DataItem, "Ven_Id").ToString();
                        ///
                        Image imgShowReportInvoice = (Image)e.Row.FindControl("imgInv");
                        ///
                        imgShowReportInvoice.Visible = true;
                    }                  
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Cargar para edición un nuevo pedido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEraseOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msnMessage.HideMessage();
            if (e.CommandName.Equals("EditOrder"))
            {
                Response.Redirect("ordersForm.aspx?noOrder=" + e.CommandArgument.ToString());
            }
            if (e.CommandName.Equals("starnular"))
            {
                string _ped = e.CommandArgument.ToString();
                {
                    try
                    {
                         _user = (Users)Session[Constants.NameSessionUser];
                         Liquidations_Hdr.sbanularpedido(_ped, _user._bas_id);

                        string selectedCustomer = dwCustomers.SelectedValue;
                        DataSet dsResults = Coordinator.getOrdLiqAnsRet(Convert.ToDecimal(selectedCustomer));

                        Session[_nameSessionData] = dsResults;
                        gvEraseOrders.DataSourceID = odsOrders.ID;
                        gvEraseOrders.DataBind();
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error realizando la Anulacion del pedido borrador No." + _ped + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }

                }
            }
        }

        #endregion

        #region < Eventos sobre data sources >

        protected void odsOrders_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessionData];
        }

        protected void odsLiquidations_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessionData];
        }

        protected void odsReturns_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessionData];
        }

        protected void odsconsignacion_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessionData];
        }
        protected void odsfavor_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessionData];
        }
        protected void odsventa_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessionData];
        }
        #endregion

        /// <summary>
        /// Generación de nueva liquidación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btCreateLiq_Click(object sender, EventArgs e)
        {
            Transporters_Guides shipping = (Transporters_Guides)Session[_nameSessionShipTo];
            string typeLiq = string.Empty;  

            if (ConfigLiq.getTypeLiqRc())
            {
                typeLiq = Constants.IdStatusLiqRecolCed;
                shipping._tgn_transport = Constants.IdTypeTransportPerson;
            }
            else
            {
                typeLiq = string.Empty;
                shipping._tgn_transport = decimal.Zero;
                if (ConfigLiq.getConfigShipping())
                    shipping._configShipping = true;
                else
                    shipping._configShipping = false;
            }
            
            string ordersChain = string.Empty;
            getGvCheckRows(out ordersChain);

            string articulo = "";
            string talla = "";

            
            

            if (!string.IsNullOrEmpty(ordersChain))
            {

                bool valida_pedido = ordersChain.Contains(",");

                if (valida_pedido)
                {
                    msnMessage.LoadMessage("Solo Puedes Seleccionar un pedido", UserControl.ucMessage.MessageType.Error);
                    string script = string.Empty;
                    script += "closeDialogLoad();scrollTopOfPage();";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);                    
                    return;
                }

                if (!(Liquidations_Hdr.fvalidastockpedido(ordersChain, ref articulo, ref talla)))
                {
                    msnMessage.LoadMessage("No se ha generado la liquidación, porque no hay stock en el Producto: " + articulo + " Talla: " + talla + " ; por favor edite el pedido para generar su liquidacion", UserControl.ucMessage.MessageType.Error);
                    string script = string.Empty;
                    script += "closeDialogLoad();scrollTopOfPage();";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                    return;
                }
            }

            if (!string.IsNullOrEmpty(ordersChain))
                doLiquidation(_user, ordersChain, shipping, typeLiq,-1);
            else
            {
                msnMessage.LoadMessage("No ha seleccionado ningún pedido borrador para generar la liquidación; debe al menos seleccionar uno.", UserControl.ucMessage.MessageType.Error);
                string script = string.Empty;
                script += "closeDialogLoad();scrollTopOfPage();";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true); 
            }
        }
        
        #region < Creacion de nueva liquidacion >

        /// <summary>
        /// Consultar filas chequeAQUARELLAs
        /// </summary>
        /// <param name="orders"></param>
        protected void getGvCheckRows(out string orders)
        {
            msnMessage.Visible = false;
            CheckBox chkBox;
            int i = 0;
            orders = string.Empty;
            foreach (GridViewRow row in gvEraseOrders.Rows)
            {
                chkBox = ((CheckBox)row.FindControl("chkBoxSelectOrder"));
                if (chkBox.Checked)
                {
                    if (i > 0)
                        orders += ",";
                    orders += chkBox.ToolTip;
                    i++;
                }
            }
        }

        /// <summary>
        /// Realizar liquidacion
        /// </summary>
        /// <param name="co"></param>
        /// <param name="ordersChain"></param>
        protected void doLiquidation(Users user, string ordersChain, Transporters_Guides shipping, string typeLiq,Decimal _varpercepcion)
        {
            List<Order_Dtl> order = (List<Order_Dtl>)HttpContext.Current.Session[""];
            // Generar liquidación.
            string[] noOrder = Liquidations_Hdr.Gua_Mod_Liquidacion(user._bas_id,0,"",0,0,"","",order,0,1,ordersChain,"",1);//  .liquidation(user._usv_co, ordersChain, shipping, typeLiq,_varpercepcion);
            if (!noOrder[0].Equals("-1"))
            {
                /*if (!string.IsNullOrEmpty(_typeLiq))
                    Liquidations_Hdr.updateStatusLiquidation(co, noOrder[0], _typeLiq);*/
                //enviar correo 
                string verror="";
                sbenviarcorreo(noOrder[0],ref verror);

                // Async 
                //Log_Transaction.registerUserInfo(user, "CREATE LIQUIDATION:" + noOrder[0] + " STATUS:" + typeLiq);

                // Reporte de liquidacion                
                string url = _pageLiquidReport + "?noLiq=" + noOrder[0] + "&typeLiq=" + typeLiq;//"?NoOrder=" + noOrder + "&TypeReport=2";
                //
                Response.Redirect(url);
            }
            else
            {
                string script = "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true); 
                msnMessage.LoadMessage("Error en la generación de la liquidación; intente de nuevo." + noOrder[1], UserControl.ucMessage.MessageType.Error);
            }
        }
        //#region <diseñador del crystal reports>
        protected void sbejecutarcrystal(string var_idliquidacion, ref string ruta, ref string verror)
        {
            try
            {
                PopulateValueCrystalReport(var_idliquidacion);

                // Ubicacion del reporte crystal
                reportPath = Server.MapPath(_nameFileCrystalReport);


                string vrutaserver = MapPath("../../Correo/Pedido/" + var_idliquidacion.ToString() + ".doc");


                //Instanciar el objeto de reporte de crystal
                _liqObjReport = new ReportDocument();

                //Enlazar el archivo del reporte y el objeto instanciado
                _liqObjReport.Load(reportPath);

                //Establecer el dataSource dirigido al reporte crystal
                _liqObjReport.SetDataSource(_liqValsReport);

                _liqObjReport.OpenSubreport("pagonc").SetDataSource(_liqValsSubReport);
                _liqObjReport.OpenSubreport("pagoforma").SetDataSource(_liqValsPagoSubReport);


                // ScriptManager.RegisterStartupScript(Page, GetType(), "mensaje", "alert('" + vrutaserver + "');", true);

                _liqObjReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, vrutaserver);



                ruta = vrutaserver;

                _liqObjReport.Close();
                _liqObjReport.Dispose();

                //Objeto crystal reports presente en la pagina aspx
                //crvLiquidation.ReportSource = _liqObjReport;

                //----------------------------------------------------------------------------
            }
            catch (Exception ex)
            {
                verror = ex.Message;
            }
        }
        protected void PopulateValueCrystalReport(string var_idliquidacion)
        {
            if (Session[_nameSessionDataC] == null)
            {
                _liqValsReport = new ArrayList();

                DataSet dsLiqInfo = Liquidations_Hdr.getLiquidationHdrInfo(var_idliquidacion);

                if (dsLiqInfo == null)
                    return;

                //DataSet dsLiqDtl =  Liquidation_Dtl.getLiquidationDtl(_user._usv_co, _noLiq);
                DataSet dsLiqDtl = new DataSet();
                dsLiqDtl.Tables.Add(dsLiqInfo.Tables[1].Copy());

                if (dsLiqDtl == null)
                    return;

                DataRow dRow = dsLiqInfo.Tables[0].Rows[0];

                foreach (DataRow dRowDtl in dsLiqDtl.Tables[0].Rows)
                {
                    string vncredito = ""; decimal VtotalcreditoTotal = 0;
                    string vfecha = DateTime.Today.ToString("dd/MM/yyyy");



                    //Bata.Aquarella.BLL.Reports.Liquidation objLiqReport = new BLL.Reports.Liquidation(dRow["ohv_warehouseid"].ToString(), dRow["wav_description"].ToString(),
                    //    dRow["wav_address"].ToString(), dRow["wav_telephones"].ToString(), dRow["ubicationwav"].ToString(), dRow["con_coordinator_id"].ToString(), dRow["bdv_document_no"].ToString(),
                    //    dRow["name"].ToString(), dRow["bdv_address"].ToString(), dRow["bdv_phone"].ToString(), dRow["bdv_movil_phone"].ToString(), dRow["bdv_email"].ToString(),
                    //    dRow["ubicationcustomer"].ToString(), dRow["lhv_liquidation_no"].ToString(), Convert.ToDateTime(dRow["lhd_date"]), Convert.ToDateTime(dRow["lhd_expiration_date"].ToString()),
                    //    dRow["stv_description"].ToString(), Convert.ToDecimal(dRow["lon_disscount"]), Convert.ToDecimal(dRow["tax_rate"]), Convert.ToDecimal(dRow["lhn_tax_rate"]), Convert.ToDecimal(dRow["lhn_handling"]),
                    //    dRowDtl["ldv_article"].ToString(), dRowDtl["brv_description"].ToString(), dRowDtl["cov_description"].ToString(), dRowDtl["arv_name"].ToString(), dRowDtl["ldv_size"].ToString(), Convert.ToDecimal(dRowDtl["ldn_qty"]),
                    //    Convert.ToDecimal(dRowDtl["ldn_sell_price"]), Convert.ToDecimal(dRowDtl["ldn_commission"]), Convert.ToDecimal(dRowDtl["ldn_disscount"]), Convert.ToDecimal(dRow["percepcion"]), Convert.ToDecimal(dRow["porc_percepcion"]),
                    //    Convert.ToDecimal(dRow["ncredito"]), vncredito, Convert.ToDateTime(vfecha), VtotalcreditoTotal, _noLiq, Convert.ToDecimal(dRow["totalop"]));


                    www.aquarella.com.pe.bll.Reports.Liquidation objLiqReport = new www.aquarella.com.pe.bll.Reports.Liquidation("1", dRow["almacen"].ToString(),
                     dRow["alm_direccion"].ToString(), dRow["Alm_Telefono"].ToString(), "", dRow["Bas_Id"].ToString(), dRow["Bas_Documento"].ToString(),
                     dRow["nombres"].ToString(), dRow["Bas_Direccion"].ToString(), dRow["Bas_Telefono"].ToString(), dRow["Bas_Celular"].ToString(), dRow["Bas_Correo"].ToString(),
                     dRow["ubicacion"].ToString(), dRow["Liq_Id"].ToString(), Convert.ToDateTime(dRow["Liq_FechaIng"]), Convert.ToDateTime(dRow["Liq_Fecha_Expiracion"].ToString()),
                     dRow["estado"].ToString(), 0, Convert.ToDecimal(dRow["igvporc"]), Convert.ToDecimal(dRow["igvmonto"]), 0,
                     dRowDtl["Art_Id"].ToString(), dRowDtl["Mar_Descripcion"].ToString(), dRowDtl["Col_Descripcion"].ToString(), dRowDtl["art_descripcion"].ToString(), dRowDtl["Liq_Det_TalId"].ToString(), Convert.ToDecimal(dRowDtl["Liq_Det_Cantidad"]),
                     Convert.ToDecimal(dRowDtl["Liq_Det_Precio"]), Convert.ToDecimal(dRowDtl["Liq_Det_Comision"]), 0, Convert.ToDecimal(dRow["Percepcionm"]), Convert.ToDecimal(dRow["Percepcionp"]),
                     Convert.ToDecimal(dRow["ncredito"]), vncredito, Convert.ToDateTime(vfecha), VtotalcreditoTotal, var_idliquidacion, Convert.ToDecimal(dRow["totalop"]), Convert.ToDecimal(dRowDtl["Liq_Det_OfertaM"]));

                    _liqValsReport.Add(objLiqReport);
                }




                _liqValsSubReport = new ArrayList();

                //DataSet dsLiqpagoInfo = Liquidations_Hdr.getpagoncreditoliqui(_noLiq);
                DataSet dsLiqpagoInfo = new DataSet();
                dsLiqpagoInfo.Tables.Add(dsLiqInfo.Tables[2].Copy());

                if (dsLiqpagoInfo == null)
                    return;

                foreach (DataRow dRowDtl in dsLiqpagoInfo.Tables[0].Rows)
                {
                    string vncredito = dRowDtl["ncredito"].ToString();
                    decimal VtotalcreditoTotal = Convert.ToDecimal(dRowDtl["Total"].ToString());
                    DateTime vfecha = Convert.ToDateTime(dRowDtl["fecha"].ToString());




                    www.aquarella.com.pe.bll.Reports.LiqNcSubinforme objLiqpagoReport = new www.aquarella.com.pe.bll.Reports.LiqNcSubinforme("", vncredito, vfecha, VtotalcreditoTotal);

                    _liqValsSubReport.Add(objLiqpagoReport);
                }


                _liqValsPagoSubReport = new ArrayList();
                //DataSet dsLiqpagoformainfo = Liquidations_Hdr.getpagonformaliqui(_noLiq);
                DataSet dsLiqpagoformainfo = new DataSet();
                dsLiqpagoformainfo.Tables.Add(dsLiqInfo.Tables[3].Copy());
                if (dsLiqpagoformainfo == null)
                    return;
                foreach (DataRow drowdtl in dsLiqpagoformainfo.Tables[0].Rows)
                {
                    string vpago = drowdtl["pago"].ToString();
                    string vdocumento = drowdtl["Documento"].ToString();
                    DateTime vfecha = Convert.ToDateTime(drowdtl["fecha"].ToString());
                    Decimal vtotal = Convert.ToDecimal(drowdtl["Total"].ToString());
                    www.aquarella.com.pe.bll.Reports.VentaPagoSubInforme objLiqpagoformaReport = new bll.Reports.VentaPagoSubInforme(vpago, vdocumento, vfecha, vtotal);
                    _liqValsPagoSubReport.Add(objLiqpagoformaReport);
                }

            }
            else
            {
                _liqValsReport = (ArrayList)Session[_nameSessionDataC];
                _liqValsSubReport = (ArrayList)Session[_nameSessionDataC];
                _liqValsPagoSubReport = (ArrayList)Session[_nameSessionDataC];
            }
        }
        //#endregion
        protected void sbenviarcorreo(string vid, ref string verror)
        {
            //enviar correo automatico la liquidacion 
            string vruta = "";
            sbejecutarcrystal(vid, ref vruta, ref verror);
            if (verror.Length > 0)
            {
                return;
            }

            Liquidations_Hdr.enviar_correos(vid);
            // string path = MapPath("../../Design/templateMailliquidacion.htm");
            // string destinatario = _cust._email;

            // //string vrutaarchivoweb = MapPath("../../Correo/Pedido/" + noOrder[0] + ".doc");

            // //PRODUCCION//
            // string vrutaarchivoweb ="http://" + Request.Url.Authority + "/Correo/Pedido/" + vid + ".doc";

            // //DESARROLLO//
            //// string vrutaarchivoweb = "http://" + Request.Url.Authority + "/DESARROLLO/Correo/Pedido/" + vid  + ".doc";

            // //string vr = Server.MapPath(""); 

            // Utilities.sendInstitutionalMessage(destinatario, "Copia de respaldo del pedido N° " + vid + " [AQUARELLA]",
            //      "Estimado usuario, este es una copia del pedido generado por el sistema de ventas por catalogo Aquarella; a continuación se detalla la información:",
            //      "<b>Para descargar su pedido haga click <a href='" + vrutaarchivoweb + "' target='_blank'>aqui</a></b>", path);

            // // Utilities.sendInstitutionalMessage(destinatario, "Copia de respado del pedido N°  [AQUARELLA]",
            // //     "Estimado usuario, este es una copia del pedido generado por el sistema de ventas por catalogo Aquarella; a continuación se detalla la información:",
            // //     "", path);
        }

        #endregion

        #region < Ajax >

        [WebMethod()]
        public static object ajaxSetShipping(string name, string phone, string movil, string shippingAdd, string shippingBlock, string city, string depto)
        {
            Transporters_Guides shipping = new Transporters_Guides
            {
                _tgv_name_cust = name,
                _tgv_phone_cust = phone,
                _tgv_movil_cust = movil,
                _tgv_shipp_add = shippingAdd,
                _tgv_shipp_block = shippingBlock,
                _tgv_city = city,
                _tgv_depto = depto
            };

            HttpContext.Current.Session["ShippingInfoObj"] = shipping;

            return new object();
        }

        #endregion

        protected void Button2_Click(object sender, EventArgs e)
        {
            string vidcliente = dwCustomers.SelectedValue;

            if (dwCustomers.Visible)
            {

                if (Session[_nameSessionCustomer] == null)
                {
                    msnMessage.LoadMessage("Debe seleccionar un cliente para poder proceder a la creación de nuevos pedidos.", UserControl.ucMessage.MessageType.Error);
                }
                else
                {
                    if (vidcliente == "-1")
                    {
                        msnMessage.LoadMessage("Debe seleccionar un cliente para poder proceder a la creación de nuevos pedidos.", UserControl.ucMessage.MessageType.Error);
                    }
                    else
                    {
                        // Response.Redirect("ordersForm.aspx");
                        //Response.Redirect("orderpago.aspx");
                        string mcodigo = "005";
                        string mnombrepago = "PAGO POR BANCO";
                        Session.Add("idpago", mcodigo);
                        Session.Add("nombrepago", mnombrepago);
                        Response.Redirect("ordersForm.aspx");
                    }
                }
            }
            else
            {
                string mcodigo = "005";
                string mnombrepago = "PAGO POR BANCO";
                Session.Add("idpago", mcodigo);
                Session.Add("nombrepago", mnombrepago);
                Response.Redirect("ordersForm.aspx");
            }
        }

        private void Reimp_tickets(string _numero)
        {
            try
            {
                string cultureName = "es-ES";
                CultureInfo culture = new CultureInfo(cultureName);

                String vnumero = _numero;
                String VFormatoTK = invoice.get_formatoTickets(vnumero,3);

                if (VFormatoTK == "0")
                {
                    string vmensaje = "El Numero de tickets : " + vnumero + " no se encuenta registrado en el sistema";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "mensaje", "alert('" + vmensaje + "');", true);
                    return;
                }
                VFormatoTK = VFormatoTK.Replace("\r\n", "\r\n");
                VFormatoTK = VFormatoTK.Replace("|", " ");
                VFormatoTK = VFormatoTK.Replace("&nbsp", " ");

                System.Text.StringBuilder str = new System.Text.StringBuilder();
                str.Append(VFormatoTK);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "text/plain";
                Response.AddHeader("Content-Disposition", "attachment;filename=Tk" + vnumero + " .txt");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = System.Text.Encoding.Default;
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                Response.Write(str.ToString());
                Response.End();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void gvventa_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msnMessage.HideMessage();           
            if (e.CommandName.Equals("strfac"))
            {
                string _ven_id = e.CommandArgument.ToString();
                {
                    try
                    {
                        Reimp_tickets(_ven_id);
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage(ex.Message, ucMessage.MessageType.Error);
                    }

                }
            }

        }
        protected void gvLiquidations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msnMessage.HideMessage();
            if (e.CommandName.Equals("EditOrder"))
            {
                Response.Redirect("ordersForm.aspx?noOrder=" + e.CommandArgument.ToString());
            }
            if (e.CommandName.Equals("starnular"))
            {             
                string _liq = e.CommandArgument.ToString();             
                {
                    try
                    {
                        Liquidations_Hdr.sbanularliquidacion(_liq);

                        string selectedCustomer = dwCustomers.SelectedValue;
                        DataSet dsResults = Coordinator.getOrdLiqAnsRet( Convert.ToDecimal(selectedCustomer));

                        Session[_nameSessionData] = dsResults;

                        gvLiquidations.DataSourceID = odsLiquidations.ID;
                        gvLiquidations.DataBind();
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error realizando la Anulacion de la liquidacion No." + _liq + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }

                }
            }

        }

        protected void gvLiquidations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)               
                return;
            //string estadocredito = e.Row.Cells[

            Label lblpagoc = (Label)e.Row.FindControl("lblpagoc");
            string pagoc = lblpagoc.Text;
            if (pagoc == "1")
            {
                e.Row.BackColor = System.Drawing.Color.Khaki;
            }

            //string str = DataBinder.Eval(e.Row.DataItem, "pin_employee").ToString();
            ImageButton imageButton2 = (ImageButton)e.Row.FindControl("ibanular");
            imageButton2.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Anular la liquidacion N° : -" + DataBinder.Eval(e.Row.DataItem, "Liq_Id") + "- ?')");
        }

        protected void gvEraseOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            //string str = DataBinder.Eval(e.Row.DataItem, "pin_employee").ToString();
            ImageButton imageButton2 = (ImageButton)e.Row.FindControl("ibanular");
            imageButton2.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Anular el pedido borrador N° : -" + DataBinder.Eval(e.Row.DataItem, "ped_id") + "- ?')");
        }

     
    }
}
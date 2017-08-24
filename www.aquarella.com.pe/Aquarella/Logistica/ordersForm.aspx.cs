﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Util;
using System.Configuration;
using www.aquarella.com.pe.bll.Control;
//using Bata.Aquarella.BLL.Control;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using www.aquarella.com.pe.Bll.Util;
//using www.aquarella.com.pe.bll.
//using Bata.Aquarella.BLL.Logistica;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class ordersForm : System.Web.UI.Page
    {
        //#region <variables del crystal reports>
        private ArrayList _liqValsReport;
        private ArrayList _liqValsSubReport;
        private ArrayList _liqValsPagoSubReport;

        string reportPath;
        string _nameFileCrystalReport = "liquidationReport.rpt";

        private ReportDocument _liqObjReport;

        //#endregion


        Coordinator _cust;
        string _nameList = "ListDocTx";
        List<Documents_Trans> _lstDocTx;
        public static string _valida = "monto";
        public static string _nSOrder = "_nSOrder", _pageLiquidReport = "panelFramesLiqReports.aspx", _nSNewOrdrLine = "_nSNewOrdrLine",
            _nSArtSiz = "_nSArtSiz", _nameSessionShipTo = "ShippingInfoObj", _nameSessionCustomer = "nameSessionCustomer",
            _nSCatalog = "_nSCatalog", _nsDtlArticle = "_nsDtlArticle", _nSOrderUrl = "_nSorderUrl", _estadoliqui = "_estadoliqui",_pagocredito="pagocredito", _nropedido = "_nropedido", _idliquidacion = "_idliquidacion",_valor_subtotal="_valor_subtotal",
            _number = ConfigurationManager.AppSettings["kNumber"], _currency = ConfigurationManager.AppSettings["kCurrency"],
            varIdOperacionPOS = ConfigurationManager.AppSettings["ID_Num_Tarjeta_POS"];


        string _formParent = "panelOrdersCustomer.aspx";
        string _nameSessionData = "liquidationValues";

        static string _opcional_percepcion = "_opcional_percepcion";

        #region < Eventos load >

        /// <summary>
        /// Load de la página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
       
        protected void Page_Load(object sender, EventArgs e)
        {
            //SingleTon.ClearCache();
            // Vencimiento de sesion
            if (this.Session[Constants.NameSessionUser] == null)
                Utilities.logout(this.Page.Session, this.Page.Response);
            if (this.IsAsync)
                return;
            //en este caso vamos a refrescar el usuario
            decimal _codigo_promotor = Convert.ToDecimal(Session["codigo_promotor"]);
            DataSet _ds_refrescar = Coordinator.getCoordinatorByPk(_codigo_promotor);
            if (_ds_refrescar == null || _ds_refrescar.Tables[0].Rows.Count <= 0)
            {
                msnMessage.LoadMessage("Hubo un problema con la informacion del cliente , por favor intentelo de nuevo o reinicie su session  ", UserControl.ucMessage.MessageType.Error);
                return;
            }


            DataRow dRow = _ds_refrescar.Tables[0].Rows[0];

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
           
            //*************************************

            if (!this.IsPostBack)
            {

               
                
                ////nota de credito
                //setParamsDataSource("1");
                ////
                ////
                //refreshGrid();    
                ////*************************
                Session[_nameList] = new List<Documents_Trans>();
                (HttpContext.Current.Session[_valida])=0;

                h_numConfigPagoPOS.Value = ConfigurationManager.AppSettings["ID_Num_Tarjeta_POS"];
                this._cust = (Coordinator)this.Session[ordersForm._nameSessionCustomer];



                h_numTipPago.Value = (string)(Session["idpago"]);
                LblPago.Text = (string)(Session["nombrepago"]);


                if (h_numTipPago.Value == varIdOperacionPOS)
                {
                  //  pnlDwCustomers.Visible = true;
                }
                   
                if (this._cust == null)
                    this.Response.Redirect(this._formParent);

                string str = this.Request.Params["noOrder"] != null ? ((object)this.Request.Params["noOrder"]).ToString() : string.Empty;

                if (string.IsNullOrEmpty(str))
                {
                    this.Session[_nSOrderUrl] = null;
                    this.Session[_estadoliqui] = null;
                    this.Session[_nropedido] = null;
                    this.Session[_idliquidacion] = null;
                    this.Session[_pagocredito] = null;
                    this.Session[_valor_subtotal] = null;
                    Session[_opcional_percepcion] = 0;
                    return;
                }

                try
                {
                    string noOrder = ((object)str.Split(new char[1] { '@' })[0].Trim()).ToString();
                    this.Session[_nSOrderUrl] = noOrder;

                    hdNoOrder.Value = noOrder;

                    if ((str.Split(new char[1] { '@' }).Length) > 2)
                    {
                        string estadoliqui = ((object)str.Split(new char[1] { '@' })[2].Trim()).ToString();
                        string nropedido = ((object)str.Split(new char[1] { '@' })[3].Trim()).ToString();
                        String pagocredito = ((object)str.Split(new char[1] { '@' })[4].Trim()).ToString();
                        this.Session[_estadoliqui] = estadoliqui;
                        this.Session[_nropedido] = nropedido;
                        this.Session[_pagocredito] = pagocredito;
                        hdestado.Value = "1";

                        //verificar si el pedido es al credito
                        if (estadoliqui == "PC") h_numTipPago.Value = "007";


                        //vamos a ver si esta liquidacion se ha pagado con nota de credito ,
                        //para validar su modificacion de liquidacion con pago de nota de credito
                        string vidliq = ((object)str.Split(new char[1] { '@' })[0].Trim()).ToString();
                        this.Session[_idliquidacion] = vidliq;
                        DataTable dtgetncliq = www.aquarella.com.pe.bll.Liquidations_Hdr.get_montoliqnc(vidliq);
                        if (dtgetncliq.Rows.Count > 0)
                        {
                            decimal vmonto=0;
                            _lstDocTx = getListFromSes();

                            for (Int32 i = 0; i < dtgetncliq.Rows.Count; ++i)
                            {
                                _lstDocTx.Add(new Documents_Trans
                                {
                                    _check = Convert.ToBoolean(dtgetncliq.Rows[i]["checks"].ToString()),
                                    _docNo = dtgetncliq.Rows[i]["ncredito"].ToString(),
                                    _numeroid =dtgetncliq.Rows[i]["rhv_return_no"].ToString(),
                                    _value = Convert.ToDecimal(dtgetncliq.Rows[i]["importe"].ToString()),
                                    //_date="NOTA DE CREDITO",
                                    //_fechadoc = (Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "dtd_document_date")).ToShortDateString()),
                                    //* Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase")),
                                    // _increase = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase"))
                                });
                                vmonto+=Convert.ToDecimal(dtgetncliq.Rows[i]["importe"].ToString());
                            }
                            Session[_nameList] = _lstDocTx;
                            HttpContext.Current.Session[_valida] = vmonto;
                        }

                        //
                    }
                    else
                    {
                        this.Session[_estadoliqui] = null;
                        this.Session[_nropedido] = null;
                        this.Session[_idliquidacion] = null;
                        this.Session[_pagocredito] = null;
                        this.Session[_valor_subtotal] = null;
                        this.Session[_valida] = 0;
                        Session[_opcional_percepcion] = 0;
                        hdestado.Value = "0";
                    }
                           
                }
                catch { this.Response.Redirect(this._formParent); }

            }
            else
                this._cust = (Coordinator)this.Session[ordersForm._nameSessionCustomer];
        }

        /// <summary>
        /// Inicio de formulario
        /// </summary>
        /// <returns></returns>
        [WebMethod()]
        public static List<Order_Dtl> intiPage()
        {
            HttpContext.Current.Session[_nSOrder] = new List<Order_Dtl>();
            HttpContext.Current.Session[_nSNewOrdrLine] = new List<Order_Dtl>();
            HttpContext.Current.Session[_nSArtSiz] = new List<Articles_Sizes>();

            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];

            string noOrder = (string)HttpContext.Current.Session[_nSOrderUrl];
            string estadoliquid = (string)HttpContext.Current.Session[_estadoliqui];
            string nropedido = (string)HttpContext.Current.Session[_nropedido];
            
            if (string.IsNullOrEmpty(noOrder))
                return new List<Order_Dtl>();


            if (string.IsNullOrEmpty(estadoliquid))
            {
                getOrderDtl(cust, noOrder);
            }
            else
            {
                getOrderDtl(cust, noOrder,estadoliquid);
            }

            return getOrderDtl();
        }

        /// <summary>
        /// Consultar pedido, y cargar para su edicion
        /// </summary>
        /// <param name="co"></param>
        /// <param name="noOrder"></param>
        public static void getOrderDtl(Coordinator cust, string noOrder,string estadoliq="")
        {
            // Lista de pedido
            List<Order_Dtl> order = new List<Order_Dtl>();
            DataTable dtOrderDtl = new DataTable();
            if (string.IsNullOrEmpty(estadoliq))
            {
                dtOrderDtl = Order_Dtl.getDtlOrder(noOrder);
            }
            else
            {
                dtOrderDtl = Order_Dtl.getDtlliqui(noOrder);
            }
            

            // Realizar de nuevo la carga de cAQUARELLA linea del pedido, para verificar perdida de descuentos o aumentos de precios
            foreach (DataRow dr in dtOrderDtl.Rows)
            {
                //getArticle(dr["Art_Id"].ToString());
                // Line
                Order_Dtl newLine = new Order_Dtl
                {
                    _code = dr["Art_Id"].ToString(),
                    _brand = dr["Mar_Descripcion"].ToString(),
                    _artName = dr["Art_Descripcion"].ToString(),
                    _size = dr["Tal_Descripcion"].ToString(),
                    _color = dr["Col_Descripcion"].ToString(),
                    _qty = Convert.ToInt16(dr["Ped_Det_Cantidad"]),
                    _price = Convert.ToDecimal(dr["Ped_Det_Precio"]),
                    _priceDesc = Convert.ToDecimal(dr["Ped_Det_Precio"]).ToString(_currency),                    
                    _ofe_id =Convert.ToDecimal(dr["Ped_Det_OfeID"]),
                    _ofe_porc= Convert.ToDecimal(dr["Ped_Det_OfertaP"]),
                    _ofe_maxpares= Convert.ToDecimal(dr["Ofe_MaxPares"]),
                    _dscto= Convert.ToDecimal(dr["Ped_Det_OfertaM"]),
                    _dsctoDesc = Convert.ToDecimal(dr["Ped_Det_OfertaM"]).ToString(_currency),
                    //_ofe_maxpares=dr[""]
                    //_priceigv = Convert.ToDecimal(dr["PRV_PRICE_IGV"]),
                    //_priceigvDesc = Convert.ToDecimal(dr["PRV_PRICE_IGV"]).ToString(_currency),
                    _comm = Convert.ToInt16(dr["Ped_Por_Com"]),
                    _ap_percepcion = dr["Ped_Por_Perc"].ToString()
                };
                loadOrderDtl(ref order, newLine , cust._commission);                
                //
                //addArticle(dr["odv_size"].ToString(), Convert.ToInt16(dr["odn_order_qty"]));
            }
            HttpContext.Current.Session[_nSOrder] = order;
            string script = string.Empty;
            /*script += "getOrderDtl()";
            System.Web.UI.ScriptManager.RegisterStartupScript(ScriptManager1, Page.GetType(), "click", script, true); */
        }

        /// <summary>
        /// Cargar lineas de pedido
        /// </summary>
        /// <param name="order"></param>
        /// <param name="newLine"></param>
        /// <param name="commPercent"></param>
        public static void loadOrderDtl(ref List<Order_Dtl> order, Order_Dtl newLine , decimal custComm)
        {
            //
            Order_Dtl resultLine = order.Where(x => x._code.Equals(newLine._code) && x._size.Equals(newLine._size)).FirstOrDefault();
            //
            decimal commPercent = (custComm / 100);

            if (fvalidaartcatalogo())
            {
                commPercent = 0;
            }

            //recalcular el descuento de catalogo 
            if (!(newLine==null))
            {
                if (newLine._ap_percepcion == "0")
                {
                    commPercent = 0;
                }
            }
            //***************************


            if (resultLine != null)
            {
                if (order.Remove(resultLine))
                {
                    int newQty = resultLine._qty + newLine._qty;
                    resultLine._qty = newQty;
                    resultLine._commission =Math.Round((((resultLine._price * newQty)) * commPercent) * resultLine._comm,2,MidpointRounding.AwayFromZero);
                    //resultLine._commissionigv = (((resultLine._priceigv * newQty) - (resultLine._dscto * newQty)) * commPercent) * resultLine._comm;
                    resultLine._commissionPctg = commPercent;
                    resultLine._commissionDesc = resultLine._commission.ToString(_currency);
                    //resultLine._commissionigvDesc = resultLine._commissionigv.ToString(_currency);
                    //resultLine._lineTotal =Math.Round ((resultLine._price * newQty) - (resultLine._dscto * newQty) - resultLine._commission,2,MidpointRounding.AwayFromZero);

                    resultLine._lineTotal = Math.Round((resultLine._price * newQty) - (resultLine._dscto + resultLine._commission), 2, MidpointRounding.AwayFromZero);

                    //resultLine._lineTotDesc = ((resultLine._price * newQty) - (resultLine._dscto * newQty) - resultLine._commission).ToString(_currency);

                    resultLine._lineTotDesc = resultLine._lineTotal.ToString(_currency);
                    //resultLine._lineTotDesc = ((resultLine._priceigv * newQty) - (resultLine._dscto * newQty) - resultLine._commissionigv).ToString(_currency);
                    order.Add(resultLine);
                }
            }
            else
            {
                newLine._commission = (((newLine._price * newLine._qty)) * commPercent) * newLine._comm;
               // newLine._commissionigv = (((newLine._priceigv * newLine._qty) - (newLine._dscto * newLine._qty)) * commPercent) * newLine._comm;
                newLine._commissionDesc = newLine._commission.ToString(_currency);
                //newLine._commissionigvDesc = newLine._commissionigv.ToString(_currency);
                newLine._commissionPctg = commPercent;
                //newLine._lineTotal =Math.Round( (newLine._price * newLine._qty) - (newLine._dscto * newLine._qty) - newLine._commission,2,MidpointRounding.AwayFromZero);

                newLine._lineTotal = Math.Round((newLine._price * newLine._qty) - (newLine._dscto + newLine._commission), 2, MidpointRounding.AwayFromZero);

                newLine._lineTotDesc = newLine._lineTotal.ToString(_currency);

                //newLine._lineTotDesc = ((newLine._price * newLine._qty) - (newLine._dscto * newLine._qty) - newLine._commission).ToString(_currency);
                //newLine._lineTotDesc = ((newLine._priceigv * newLine._qty) - (newLine._dscto * newLine._qty) - newLine._commissionigv).ToString(_currency);
                order.Add(newLine);
            }
        }

        #endregion

        #region < Ajax >

        /// <summary>
        /// Consultar un articulo, tallas y descuentos
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [WebMethod()]
        public static List<Order_Dtl> getArticle(string code)
        {
            try
            {
                Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
                string co = cust._co;

                List<Articles_Sizes> artSizes = new List<Articles_Sizes>();
                Order_Dtl newLineOrder = new Order_Dtl();
                List<Order_Dtl> order = new List<Order_Dtl>();

                DataSet dsArt = Article.getArticle(code.Replace("-", string.Empty).Trim());

                if (dsArt == null || dsArt.Tables[0].Rows.Count == 0)
                    throw new Exception("El artículo digitado es inexistente.");

                newLineOrder = Order_Dtl.getNewLineOrder(dsArt.Tables[0]);
                artSizes = Articles_Sizes.getObjectSizes(dsArt.Tables[1], false);

                if (dsArt == null || dsArt.Tables[1].Rows.Count == 0)
                    throw new Exception("Lamentablemente no existen tallas habilitas para este artículo");

                HttpContext.Current.Session[_nSArtSiz] = artSizes;                

                order.Add(newLineOrder);

                HttpContext.Current.Session[_nSNewOrdrLine] = order;

                return order;
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }

        /// <summary>
        /// Obtener las tallas de un articulo previamente consultado
        /// </summary>
        /// <returns></returns>
        [WebMethod()]
        public static List<Articles_Sizes> getArticleSizes()
        {
            return (List<Articles_Sizes>)HttpContext.Current.Session[_nSArtSiz];
        }

        /// <summary>
        /// Adicionar un articulo previamente consultado a una lista de pedido
        /// </summary>
        /// <param name="size"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        [WebMethod()]
        public static List<Order_Dtl> addArticle(string size, int qty, string varTipoPago)
        {
            // Nueva linea de pedido
            Order_Dtl newLineOrder = ((List<Order_Dtl>)HttpContext.Current.Session[_nSNewOrdrLine]).FirstOrDefault();

            //
            newLineOrder._size = size;

            return addArticle(newLineOrder, qty, varTipoPago);
        }

        public static List<Order_Dtl> addArticle(Order_Dtl newLine, int qty, string varTipoPago)
        {
            decimal commPercent;
            //decimal ofertporcentaje;
            //decimal ofertamaxpares;
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];

            cust._vartipopago = varTipoPago;

            if (varTipoPago == varIdOperacionPOS)
            {
                commPercent = cust._commission_POS_visaUnica / 100;
            }
            else
            {
                 commPercent = cust._commission / 100;
            }
            

            if (newLine._ap_percepcion=="0")
            {
                commPercent = 0;
            }
            //este quiere decir que tiene oferta
            //if (newLine._ofe_id!=0)
            //{
            //    ofertporcentaje = newLine._ofe_porc;
            //    ofertamaxpares = newLine._ofe_maxpares;
            //}

            //decimal commPercent = (cust._commission / 100);
            int newQty = 0;            

            // Lista de pedido
            List<Order_Dtl> orderLines = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>()); ;

            Order_Dtl resultLine;

            if (orderLines != null)
                resultLine = orderLines.Where(x => x._code.Equals(newLine._code) && x._size.Equals(newLine._size)).FirstOrDefault();
            else
                resultLine = new Order_Dtl();

            if (resultLine != null)
            {
                if (orderLines.Remove(resultLine))
                {
                    newQty = resultLine._qty + qty;
                    resultLine._qty = newQty;
                    resultLine._commission = Math.Round((((resultLine._price * newQty) - (resultLine._dscto * newQty)) * commPercent) * resultLine._comm, 2, MidpointRounding.AwayFromZero);
                    resultLine._commissionPctg = commPercent;
                    resultLine._commissionDesc = resultLine._commission.ToString(_currency);


                  //  resultLine._dsctoDesc = newLine._commission.ToString(_currency);
                    //resultLine._commissionigv = Math.Round((((resultLine._priceigv * newQty) - (resultLine._dscto * newQty)) * commPercent) * resultLine._comm, 2, MidpointRounding.AwayFromZero);
                    //resultLine._commissionigvDesc = resultLine._commissionigv.ToString(_currency);
                    resultLine._lineTotal = Math.Round((resultLine._price * newQty) - (resultLine._dscto * newQty) - resultLine._commission, 2, MidpointRounding.AwayFromZero);
                    resultLine._lineTotDesc = ((resultLine._price * newQty) - (resultLine._dscto * newQty) - resultLine._commission).ToString(_currency);
                   // resultLine._lineTotDesc = ((resultLine._priceigv * newQty) - (resultLine._dscto * newQty) - resultLine._commissionigv).ToString(_currency);
                    orderLines.Add(resultLine);
                }
            }
            else
            {
                newLine._qty = qty;  
                newLine._commission =  Math.Round((((newLine._price * qty) - (newLine._dscto * qty)) * commPercent) * newLine._comm,2,MidpointRounding.AwayFromZero);
                newLine._commissionDesc = newLine._commission.ToString(_currency);
                //newLine._commissionigv = Math.Round((((newLine._priceigv * qty) - (newLine._dscto * qty)) * commPercent) * newLine._comm, 2, MidpointRounding.AwayFromZero);
                //newLine._commissionigvDesc = newLine._commissionigv.ToString(_currency);
                newLine._commissionPctg = commPercent;

                //newLine._dscto =Math.Round (((newLine._price * qty) - newLine._commission) * ((newLine._ofe_porc / 100)));

                //newLine._dsctoDesc = (((newLine._price * qty) - newLine._commission)*((newLine._ofe_porc/100))).ToString(_currency);

                newLine._lineTotal = Math.Round((newLine._price * qty) - (newLine._dscto * qty) - newLine._commission, 2, MidpointRounding.AwayFromZero);
                newLine._lineTotDesc = ((newLine._price * qty) - (newLine._dscto * qty) - newLine._commission).ToString(_currency);
                //newLine._lineTotDesc = ((newLine._priceigv * qty) - (newLine._dscto * qty) - newLine._commissionigv).ToString(_currency);
                orderLines.Add(newLine);
            }

            return orderLines;
        }

        /// <summary>
        /// Actualizacion de las cantidades pedidas de un articulo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="size"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        /// 

        [WebMethod()]
        public static List<Order_Dtl> fupdateitemoferta()
        {
            List<Order_Dtl> orderLines = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>()); ;
            try
            {

                if (orderLines!=null)
                {

                    /*formatear las promociones*/
                    for (Int32 c=0;c<orderLines.Count;++c)
                    {
                        //verificar las promociones
                        if (orderLines[c]._ofe_id!=0)
                        {
                            orderLines[c]._dscto = 0;
                            orderLines[c]._dsctoDesc = orderLines[c]._dscto.ToString(_currency);

                            orderLines[c]._lineTotal = Math.Round((orderLines[c]._qty * orderLines[c]._price) - (orderLines[c]._commission) - (orderLines[c]._dscto), 2, MidpointRounding.AwayFromZero);
                            orderLines[c]._lineTotDesc = orderLines[c]._lineTotal.ToString(_currency);
                        }
                    }
                    /********************/

                    /*si es que tiee oferta entonces vamos a filtrar*/
                    List<Order_Dtl> orderLinesOferta_filter = orderLines.Where(c => c._ofe_id != 0).ToList();

                    if (orderLinesOferta_filter.Count>0)
                    {                                                

                        /*capturamos el maximo de pares y por descuento*/
                        Decimal _max_pares = orderLinesOferta_filter[0]._ofe_maxpares;
                        Decimal _por_desc = orderLinesOferta_filter[0]._ofe_porc/100;

                        Decimal _total = orderLinesOferta_filter.Sum(x => x._qty);

                        /*ahora capturado el total de pares le hacemos un for para */

                        decimal _res = _total / _max_pares;
                        /*para saber si es un entero es true si no es false decimal*/
                        bool isInt = (int)_res == _res;

                        DataTable dt = new DataTable();
                        dt.Columns.Add("articulo", typeof(string));
                        dt.Columns.Add("talla", typeof(string));
                        dt.Columns.Add("precio", typeof(Decimal));
                        dt.Columns.Add("cantidad", typeof(Decimal));
                        dt.Columns.Add("porc_comision", typeof(Decimal));
                        dt.Columns.Add("descuento", typeof(Decimal));

                        for (Int32 a=0;a< orderLinesOferta_filter.Count;++a)
                        {

                            for (Int32 c=0;c< orderLinesOferta_filter[a]._qty;++c)
                            {
                                dt.Rows.Add(orderLinesOferta_filter[a]._code.ToString(),
                                        orderLinesOferta_filter[a]._size.ToString(),
                                        orderLinesOferta_filter[a]._price,
                                        1,
                                        orderLinesOferta_filter[a]._commissionPctg,
                                        0);
                            }

                            
                        }

                        if (!isInt)
                            _res = Convert.ToInt32((_res) - Convert.ToDecimal(0.1));


                        if (_res != 0)
                        {
                            DataRow[] _filas = dt.Select("len(articulo)>0", "precio asc");
                            if (_filas.Length>0)
                            { 
                                for (Int32 i = 0; i < _res; ++i)
                                {
                                    string _articulo = _filas[i]["articulo"].ToString();
                                    string _talla = _filas[i]["talla"].ToString(); 
                                    Decimal _precio =Convert.ToDecimal(_filas[i]["precio"]);
                                    Decimal _com_porc = Convert.ToDecimal(_filas[i]["porc_comision"]);
                                    Decimal _cant= Convert.ToDecimal(_filas[i]["cantidad"]);
                                    decimal _com_mon =Math.Round((_precio * _cant) * _com_porc,2,MidpointRounding.AwayFromZero);
                                    Decimal _des_oferta =Math.Round(((_precio * _cant) - _com_mon) * (_por_desc),2,MidpointRounding.AwayFromZero);

                                    _filas[i]["descuento"] = _des_oferta;
                                }

                                for (Int32 i = 0; i < orderLines.Count; ++i)
                                {
                                    string _articulo = orderLines[i]._code.ToString();
                                    string _talla = orderLines[i]._size.ToString();
                                    foreach(DataRow vfila in _filas)
                                    {
                                        if( _articulo==vfila["articulo"].ToString() && _talla==vfila["talla"].ToString())
                                        {
                                            orderLines[i]._dscto += Convert.ToDecimal(vfila["descuento"]);
                                            orderLines[i]._dsctoDesc = orderLines[i]._dscto.ToString(_currency);

                                            orderLines[i]._lineTotal = Math.Round((orderLines[i]._qty * orderLines[i]._price) - (orderLines[i]._commission) - (orderLines[i]._dscto), 2, MidpointRounding.AwayFromZero);
                                            orderLines[i]._lineTotDesc = orderLines[i]._lineTotal.ToString(_currency);
                                        }
                                    }
                                }
                            }
                        }
                        //else
                        //{
                        //    _res = Convert.ToInt32((_res) - Convert.ToDecimal(0.1));

                        //    if (_res!=0)
                        //    {

                        //        DataRow[] _filas = dt.Select("len(articulo)>0", "precio asc");
                        //        if (_filas.Length > 0)
                        //        {
                        //            for (Int32 i = 0; i < _res; ++i)
                        //            {
                        //                string _articulo = _filas[i]["articulo"].ToString();
                        //                string _talla = _filas[i]["talla"].ToString();
                        //                Decimal _precio = Convert.ToDecimal(_filas[i]["precio"]);
                        //                Decimal _com_porc = Convert.ToDecimal(_filas[i]["porc_comision"]);
                        //                Decimal _cant = Convert.ToDecimal(_filas[i]["cantidad"]);
                        //                decimal _com_mon = Math.Round((_precio * _cant) * _com_porc, 2, MidpointRounding.AwayFromZero);
                        //                Decimal _des_oferta = Math.Round(((_precio * _cant) - _com_mon) * (_por_desc), 2, MidpointRounding.AwayFromZero);

                        //                _filas[i]["descuento"] = _des_oferta;
                        //            }

                        //            for (Int32 i = 0; i < orderLines.Count; ++i)
                        //            {
                        //                string _articulo = orderLines[i]._code.ToString();
                        //                string _talla = orderLines[i]._size.ToString();
                        //                foreach (DataRow vfila in _filas)
                        //                {
                        //                    if (_articulo == vfila["articulo"].ToString() && _talla == vfila["talla"].ToString())
                        //                    {
                        //                        orderLines[i]._dscto += Convert.ToDecimal(vfila["descuento"]);
                        //                        orderLines[i]._dsctoDesc = orderLines[i]._dscto.ToString(_currency);

                        //                        orderLines[i]._lineTotal = Math.Round((orderLines[i]._qty * orderLines[i]._price) - (orderLines[i]._commission) - (orderLines[i]._dscto), 2, MidpointRounding.AwayFromZero);
                        //                        orderLines[i]._lineTotDesc = orderLines[i]._lineTotal.ToString(_currency);
                        //                    }
                        //                }
                        //            }
                        //        }

                        //    }
                        //}




                       
                        

                        /*capturamos el total de pares que se tiene con esta oferta*/




                        /*verificar que hay 1 numero de pares por items*/
                        //List<Order_Dtl> orderLinesOferta_filter_par = orderLinesOferta_filter.Where(c => c._qty == 1).ToList();

                        //if (orderLinesOferta_filter_par.Count>0)
                        //{
                        //    Decimal _contar_max = 0;

                        /*para verificar el precio menor*/
                        //DataTable dt = new DataTable();
                        //dt.Columns.Add("articulo", typeof(string));
                        //dt.Columns.Add("talla", typeof(string));
                        //dt.Columns.Add("precio", typeof(decimal));

                        //for(Int32 a=0;a< orderLinesOferta_filter_par.Count;++a)
                        //{                                
                        //    _contar_max += 1;
                        //    dt.Rows.Add(orderLinesOferta_filter_par[a]._code.ToString(), orderLinesOferta_filter_par[a]._size.ToString(), orderLinesOferta_filter_par[a]._price);
                        //    if (_max_pares==_contar_max)
                        //    {
                        //        DataRow[] fila_precio_menor = dt.Select("len(articulo)>0", "precio asc");

                        //        string _articulo = fila_precio_menor[0]["articulo"].ToString(); 
                        //        string _talla = fila_precio_menor[0]["talla"].ToString();
                        //        Decimal _precio =Convert.ToDecimal(fila_precio_menor[0]["precio"]);

                        //        /*ahora en este paso vamos hacer update al padre*/
                        //        for (Int32 i=0;i<orderLines.Count;++i)
                        //        {
                        //            if(_articulo==orderLines[i]._code.ToString() && _talla==orderLines[i]._size.ToString() && _precio==orderLines[i]._price)
                        //            {

                        //                orderLines[i]._dscto =Math.Round(((orderLines[i]._qty * orderLines[i]._price) - (orderLines[i]._commission)) * _por_desc,2, MidpointRounding.AwayFromZero);
                        //                orderLines[i]._dsctoDesc= orderLines[i]._dscto.ToString(_currency);

                        //                orderLines[i]._lineTotal = Math.Round((orderLines[i]._qty * orderLines[i]._price) - (orderLines[i]._commission) - (orderLines[i]._dscto), 2, MidpointRounding.AwayFromZero);
                        //                orderLines[i]._lineTotDesc = orderLines[i]._lineTotal.ToString(_currency);

                        //                break;
                        //            }
                        //        }

                        //        /**/
                        //        dt.Rows.Clear();
                        //        _contar_max= 0;
                        //    }                                
                        //}
                        //}
                        //else
                        //{
                        /*en este ver si es que es mayor a un par*/
                        //List<Order_Dtl> orderLinesOferta_filter_max = orderLinesOferta_filter.Where(c => c._qty >= _max_pares).ToList();

                        //for (Int32 b=0;b< orderLinesOferta_filter_max.Count;++b)
                        //{
                        //    Decimal porc_com = orderLinesOferta_filter_max[b]._commissionPctg;
                        //    string _articulo = orderLinesOferta_filter_max[b]._code.ToString();
                        //    string _talla = orderLinesOferta_filter_max[b]._size.ToString();

                        //    for (Int32 i = 0; i < orderLines.Count; ++i)
                        //    {
                        //        if (_articulo == orderLines[i]._code.ToString() && _talla == orderLines[i]._size.ToString())
                        //        {
                        //            Decimal _can_tot = orderLines[i]._qty;
                        //            Int32 _cant_calcula =Convert.ToInt32((_can_tot / _max_pares).ToString().Substring(0,1));

                        //            orderLines[i]._dscto = Math.Round(((_cant_calcula * orderLines[i]._price) - ((orderLines[i]._price* _cant_calcula) * porc_com)) * _por_desc, 2, MidpointRounding.AwayFromZero);
                        //            orderLines[i]._dsctoDesc = orderLines[i]._dscto.ToString(_currency);

                        //            orderLines[i]._lineTotal = Math.Round((orderLines[i]._qty * orderLines[i]._price) - (orderLines[i]._commission) - (orderLines[i]._dscto), 2, MidpointRounding.AwayFromZero);
                        //            orderLines[i]._lineTotDesc = orderLines[i]._lineTotal.ToString(_currency);

                        //            break;
                        //        }
                        //    }

                        //}

                        //}

                    }
                   
                    




                    //Decimal _max_pares=


                    //for (Int32 i=0;i<orderLines.Count;++i)
                    //{

                    //}
                }   

            }
            catch { }
            return (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];
        }


        [WebMethod()]
        public static List<Order_Dtl> fupdateitemforma()
        {
            List<Order_Dtl> orderLines = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>()); ;
            try
            {
                

                if (orderLines != null)
                {
                    for (Int32 i = 0; i < orderLines.Count; ++i)
                    {
                        string vcode = orderLines[i]._code;
                        string vsize = orderLines[i]._size;
                        Int32 vqty = orderLines[i]._qty;
                        updateItem(vcode, vsize, vqty);
                    }
                        
                }
                HttpContext.Current.Session[_nSOrder] = orderLines;
               
            }
            catch { }
            return (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];
            
        }

        [WebMethod()]
        public static List<Order_Dtl> updateItem(string code, string size, int qty)
        {
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            decimal commPercent = (cust._commission / 100);

            if (cust._vartipopago == varIdOperacionPOS)
            {
                commPercent = cust._commission_POS_visaUnica / 100;
            }
            else
            {
                commPercent = cust._commission / 100;
            }

            if (fvalidaartcatalogo())
            {
                commPercent = 0;
            }

            // Lista de pedido
            List<Order_Dtl> orderLines = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>()); ;

            try
            {
                Order_Dtl resultLine;

                if (orderLines != null)
                    resultLine = orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault();
                else
                    resultLine = new Order_Dtl();

                if (resultLine != null)
                {
                    // Nuevos calculos
                    resultLine._commission = Math.Round((((resultLine._price * qty) /*- (resultLine._dscto * qty)*/) * commPercent) * resultLine._comm,2,MidpointRounding.AwayFromZero);
                    resultLine._commissionigv = (((resultLine._priceigv * qty) /*- (resultLine._dscto * qty)*/) * commPercent) * resultLine._comm;
                    resultLine._commissionPctg = commPercent;
                    resultLine._commissionDesc = resultLine._commission.ToString(_currency);
                    //resultLine._commissionigvDesc = resultLine._commissionigv.ToString(_currency);
                    resultLine._lineTotal = Math.Round((resultLine._price * qty) - (resultLine._dscto * qty) - resultLine._commission,2,MidpointRounding.AwayFromZero);
                    resultLine._lineTotDesc = ((resultLine._price * qty) - (resultLine._dscto * qty) - resultLine._commission).ToString(_currency);
                    //resultLine._lineTotDesc = ((resultLine._priceigv * qty) - (resultLine._dscto * qty) - resultLine._commissionigv).ToString(_currency);

                    // Update
                    orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault()._qty = qty;
                    orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault()._commission = resultLine._commission;
                    orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault()._commissionigv = resultLine._commissionigv;
                    orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault()._commissionPctg = resultLine._commissionPctg;
                    orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault()._commissionDesc = resultLine._commission.ToString(_currency);
                    //orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault()._commissionigvDesc = resultLine._commissionigv.ToString(_currency);
                    orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault()._lineTotal = resultLine._lineTotal;
                    orderLines.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault()._lineTotDesc = resultLine._lineTotDesc;

                    HttpContext.Current.Session[_nSOrder] = orderLines;
                }
            }
            catch { }

            return (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];
        }

        /// <summary>
        /// Borrar una linea de la lista de pedido
        /// </summary>
        /// <param name="code"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [WebMethod()]
        public static List<Order_Dtl> deleteArticle(string code, string size)
        {
            // Lista de pedido
            List<Order_Dtl> order = (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];

            //
            Order_Dtl resultLine = order.Where(x => x._code.Equals(code) && x._size.Equals(size)).FirstOrDefault();

            // Delete line
            if (resultLine != null)
                order.Remove(resultLine);

            HttpContext.Current.Session[_nSOrder] = order;

            return order;
        }

        /// <summary>
        /// Consultar la lista de pedidos guardAQUARELLA en sesion
        /// </summary>
        /// <returns></returns>
        [WebMethod()]
        public static List<Order_Dtl> getOrderDtl()
        {
            List<Order_Dtl> order = (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];            
            // Lista de pedido
            return order;
        }

        [WebMethod()]
        public static string getOrderDtls()
        {
            List<Order_Dtl> order = (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];
            System.Web.Script.Serialization.JavaScriptSerializer jSearializer =
                   new System.Web.Script.Serialization.JavaScriptSerializer();
            return jSearializer.Serialize(order);
        }

        /// <summary>
        /// Calcular y devolver los totales de una lista de pedido
        /// </summary>
        /// <returns></returns>
        /// 

        public static Boolean fvalidaartcatalogo()
        {
            Boolean validaartcatag = false;
            try
            {
                List<Order_Dtl> order = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>());
                if (order != null)
                {

                    for (Int32 i = 0; i < order.Count; ++i)
                    {
                        if (order[i]._ap_percepcion=="0")
                        {
                            validaartcatag = true;
                            break;
                        }
                        

                    }


                }
                else
                {
                    validaartcatag = false;
                }

            }
            catch
            {
                return false;
            }
            return validaartcatag;
        }

        public static Boolean fvalidaartpercepcion()
        {
            string strartp = "";
            Boolean validaartp = false;
            try
            {
                List<Order_Dtl> order = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>());
                if (order != null)
                {

                    for (Int32 i = 0; i < order.Count; ++i)
                    {
                        if (i == 0)
                        {
                            strartp = order[i]._ap_percepcion;
                        }
                        else
                        {
                            if (!(strartp==order[i]._ap_percepcion))
                            {
                                validaartp = true;
                                break;
                            }
                            strartp = order[i]._ap_percepcion;
                        }
                      
                    }


                }
                else
                {
                    validaartp = false;
                }

            }
            catch
            {
               return false;
            }
            return validaartp;
        }

        public static Boolean fvalida_sin_percepcion()
        {
            string strartp = "";
            Boolean validaartp = false;
            try
            {
                List<Order_Dtl> order = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>());
                if (order != null)
                {

                    

                    for (Int32 i = 0; i < order.Count; ++i)
                    {
                        String _art_percepcion = strartp = order[i]._ap_percepcion;
                        if (_art_percepcion=="1")
                        {
                            validaartp = true;
                            break;
                        }                      

                    }

                }
                else
                {
                    validaartp = false;
                }

            }
            catch
            {
                return false;
            }
            return validaartp;
        }


        [WebMethod()]
        public static Order_Hdr getTotalsoptional(string vnc)
        {
            Order_Hdr orderHdr;

            try
            {
                // Lista de pedido
                List<Order_Dtl> order = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>());

                if (order != null)
                {
                    Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
                    decimal taxRate = (cust._taxRate / 100);
                    int totalQty = order.Sum(q => q._qty);
                    decimal subTotal = Math.Round(order.Sum(x => x._lineTotal), 2, MidpointRounding.AwayFromZero);
                    string subTotalDesc = subTotal.ToString(_currency);
                    decimal taxes = Math.Round((order.Sum(x => x._lineTotal)) * taxRate, 2, MidpointRounding.AwayFromZero);
                    string taxesDesc = taxes.ToString(_currency);

                    decimal mtoncredito = Convert.ToDecimal(vnc);
                    string mtoncreditodesc = mtoncredito.ToString(_currency);

                    HttpContext.Current.Session[_valor_subtotal] = subTotal + taxes;

                    decimal grandTotal = (subTotal + taxes) - mtoncredito;

                    //si el paso es mayor a la venta
                    if (grandTotal < 0)
                    {
                        grandTotal = 0;
                    }
                    //

                    string grandTotalDesc = grandTotal.ToString(_currency);

                    cust._mtoimporte = (subTotal + taxes);


                    Boolean aplicap = true;

                    //verificar si estos articulos tiene percepcion 0
                    for (Int32 i = 0; i < order.Count; ++i)
                    {
                        string vaplicap = order[i]._ap_percepcion;
                        if (vaplicap == "0")
                        {
                            aplicap = false;
                            break;
                        }
                    }

                    decimal Percepcionrate = (aplicap) ? cust._percepcion / 100 : 0;

                    //decimal Percepcionrate = (cust._percepcion / 100);

                    decimal percepcion = Math.Round(grandTotal * Percepcionrate, 2, MidpointRounding.AwayFromZero);



                    string percepciondesc = percepcion.ToString(_currency);

                    decimal mtopercepcion = grandTotal + percepcion;
                    string mtopercepciondesc = mtopercepcion.ToString(_currency);

                    

                    //variable de percepcion*********
                    cust._mtopercepcion = percepcion;


                    HttpContext.Current.Session[_opcional_percepcion] = percepcion;
                    //*******************************
                    orderHdr = new Order_Hdr
                    {
                        _qtys = totalQty,
                        _subTotalDesc = subTotalDesc,
                        _grandTotalDesc = grandTotalDesc,
                        _taxesDesc = taxesDesc,
                        _grandTotal = grandTotal,
                        _percepcion = percepcion,
                        _percepciondesc = percepciondesc,
                        _mtopercepcion = mtopercepcion,
                        _mtopercepciondesc = mtopercepciondesc,
                        _mtoncredito = mtoncredito,
                        _mtoncreditodesc = mtoncreditodesc,
                    };
                }
                else
                    orderHdr = new Order_Hdr();
            }
            catch
            {
                orderHdr = new Order_Hdr();
            }

            return orderHdr;

        }

        [WebMethod()]
        public static Order_Hdr get_verificacredito()
        {
            Order_Hdr orderHdr;
            try
            {
                string noOrder = (string)HttpContext.Current.Session[_nSOrderUrl];
                string pagocredito = (string)HttpContext.Current.Session[_pagocredito];
                Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];

                orderHdr = new Order_Hdr
                {
                    _estadocredito = pagocredito
                  
                };
            }
            catch
            {
                orderHdr = new Order_Hdr();
            }
            return orderHdr;
        }

        [WebMethod()]
        public static Order_Hdr get_promotorestado()
        {
            Order_Hdr orderHdr;
            try
            {
                string noOrder = (string)HttpContext.Current.Session[_nSOrderUrl];
                string estadoliquid = (string)HttpContext.Current.Session[_estadoliqui];

                string _vestadoliquid = "";
                Int32 _vestadoboton = 0;

                if (string.IsNullOrEmpty(noOrder))
                {
                    _vestadoliquid = "Nuevo Pedido";
                }
                else
                {
                    if (string.IsNullOrEmpty(estadoliquid))
                    {
                        _vestadoliquid = "Modificando Pedido N° " + noOrder;
                    }
                    else
                    {
                        _vestadoliquid = "Modificando Liquidacion N° " + noOrder;
                        _vestadoboton = 1;
                    }
                }



                Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
                orderHdr = new Order_Hdr
                {
                    _namecompleto = cust._nombrecompleto,
                    _estadoliqui=_vestadoliquid,
                    _estadoboton=_vestadoboton
                };
            }
            catch
            {
                orderHdr = new Order_Hdr();
            }
            return orderHdr;
        }



        [WebMethod()]
        public static Order_Hdr getTotals()
        {
            Order_Hdr orderHdr;
            
            try
            {   
                // Lista de pedido
                List<Order_Dtl> order = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>());

                if (order != null)
                {
                    Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
                    decimal taxRate = (cust._taxRate / 100);                    
                    int totalQty = order.Sum(q => q._qty);
                    decimal subTotal = Math.Round(order.Sum(x => x._lineTotal), 2, MidpointRounding.AwayFromZero);
                    string subTotalDesc = subTotal.ToString(_currency);
                    decimal taxes = Math.Round((order.Sum(x => x._lineTotal)) * taxRate, 2, MidpointRounding.AwayFromZero);
                    string taxesDesc = taxes.ToString(_currency);

                    

                    decimal mtoncredito =Convert.ToDecimal(HttpContext.Current.Session[_valida]);
                    string mtoncreditodesc = mtoncredito.ToString(_currency);

                    HttpContext.Current.Session[_valor_subtotal] = subTotal + taxes;

                    

                    decimal grandTotal = (subTotal + taxes) - mtoncredito;

                    //si el paso es mayor a la venta
                    if (grandTotal < 0)
                    {
                        grandTotal = 0;
                    }
                    //

                    cust._mtoimporte = (subTotal + taxes);

                    string grandTotalDesc = grandTotal.ToString(_currency);

                    Boolean aplicap = true;

                    //verificar si estos articulos tiene percepcion 0
                    for (Int32 i = 0; i < order.Count; ++i)
                    {
                        string vaplicap = order[i]._ap_percepcion;
                        if (vaplicap == "0")
                        {
                            aplicap = false;
                            break;
                        }
                    }
                    //
                    decimal Percepcionrate = (aplicap) ? cust._percepcion / 100 : 0;
                    //decimal Percepcionrate = (cust._percepcion / 100);

                    decimal percepcion = Math.Round(grandTotal * Percepcionrate, 2, MidpointRounding.AwayFromZero);
                    string percepciondesc = percepcion.ToString(_currency);

                    decimal mtopercepcion = grandTotal + percepcion;
                    string mtopercepciondesc = mtopercepcion.ToString(_currency);

                    

                    //variable de percepcion*********
                    cust._mtopercepcion = percepcion;

                    //opcion de variable de percepcion
                    HttpContext.Current.Session[_opcional_percepcion] = percepcion;
                    //*******************************
                    orderHdr = new Order_Hdr
                    {
                        _qtys = totalQty,
                        _subTotalDesc = subTotalDesc,
                        _grandTotalDesc = grandTotalDesc,
                        _taxesDesc = taxesDesc,
                        _grandTotal = grandTotal,
                        _percepcion=percepcion,
                        _percepciondesc=percepciondesc,
                        _mtopercepcion=mtopercepcion,
                        _mtopercepciondesc=mtopercepciondesc,
                        _mtoncredito=mtoncredito,
                        _mtoncreditodesc=mtoncreditodesc,
                    };
                }
                else
                    orderHdr = new Order_Hdr();
            }
            catch
            {
                orderHdr = new Order_Hdr();
            }

            return orderHdr;

        }

        [WebMethod()]
        public static stockResponse getItemIconStock(string article, string size, decimal qty, string ctrlImgStock)
        {
            string noOrder = (string)HttpContext.Current.Session[_nSOrderUrl];
            if (string.IsNullOrEmpty(noOrder))
            {
                noOrder = "";
            }

            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            // Cargar session de compañia
            string co = cust._co;
            string iconStock = string.Empty;

            ///
            DataSet dsItems = Stock.getStockArticleSize(article, size, noOrder);

            if (dsItems != null && dsItems.Tables[0].Rows.Count > 0)
            {                
                //
                decimal qtyAv = dsItems.Tables[0].AsEnumerable().Where(x => x.Field<string>("Articulo").Equals(article) && x.Field<string>("Talla").Equals(size)).Sum(y => y.Field<decimal>("Cantidad"));
                if (qtyAv >= qty)
                    iconStock = "../../Design/images/Botones/b_info.png";
                else
                    iconStock = "../../Design/images/Botones/b_info_red.png";
            }
            else
                iconStock = "../../Design/images/Botones/b_info_red.png";

            return new stockResponse { _tableHtml = string.Empty, _iconStock = iconStock, _ctrlImgStock = ctrlImgStock };
        }
        
        /// <summary>
        /// Consultar info y stock de un articulo y su grupo de articulos asociados
        /// </summary>
        /// <param name="article"></param>
        /// <param name="size"></param>
        /// <param name="qty"></param>
        /// <param name="ctrlImgStock"></param>
        /// <returns></returns>
        [WebMethod()]
        public static stockResponse getArticleStock(string article, string size, decimal qty, string ctrlImgStock)
        {
            string noOrder = (string)HttpContext.Current.Session[_nSOrderUrl];
            if (string.IsNullOrEmpty(noOrder))
            {
                noOrder = "";
            }

            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            // Cargar session de compañia
            string co = cust._co;
            string chain = string.Empty;
            string iconStock = string.Empty;

            ///
            DataSet dsItems = Stock.getStockByArtWithAllSizes(co, cust._idWare, "", article, noOrder);

            if (dsItems != null && dsItems.Tables[0].Rows.Count > 0)
            {
                // All distinct items by brand and name
                var itemsDisc = (from x in dsItems.Tables[0].AsEnumerable() //group x by x.Field<string>("asv_article") into y 
                                 select new
                                 {
                                     itemName = x.Field<string>("Art_Descripcion"),
                                     itemBrand = x.Field<string>("Mar_Descripcion"),
                                 }).Distinct();

                chain += "<table><tr>";
                chain += "<td valign='top'><table><tr><td colspan='2'><div style='width: 280px; max-width: 258px; height: 290px; padding-left: 19px; text-align: left; border: 1px solid #E8E8E8;'>";
                chain += "<img id='imgArtDesc' src='" + dsItems.Tables[0].Rows[0]["Art_Foto"].ToString() + "'/></div></td></tr>";
                chain += "<tr><td>Imagen correspondiente:</td><td><label id='lblCodeImg'>" + dsItems.Tables[0].Rows[0]["Art_Id"].ToString() + "</label></td></tr></table></td>";

                foreach (var current in itemsDisc)
                {
                    chain += "<td valign='top'><table>";
                    chain += "<tr><td class='f13'><b>" + current.itemBrand + ": " + current.itemName + "<b></td></tr>";

                    var itemsByBrand = (from x in dsItems.Tables[0].AsEnumerable()
                                        where x.Field<string>("Art_Descripcion").Equals(current.itemName) &&
                                        x.Field<string>("Mar_Descripcion").Equals(current.itemBrand)
                                        select new
                                        {
                                            itemCode = x.Field<string>("Art_Id"),
                                            itemColor = x.Field<string>("Col_Descripcion"),
                                            itemMaj = x.Field<string>("Cat_Pri_Descripcion"),
                                            itemCat = x.Field<string>("Cat_Descripcion"),
                                            itemPhoto = x.Field<string>("Art_Foto")
                                        }).Distinct();

                    foreach (var currentItem in itemsByBrand)
                    {
                        string undlineCode = currentItem.itemCode;

                        if(article.Equals(currentItem.itemCode))
                            undlineCode = "<u>" + currentItem.itemCode + "</u>";

                        chain += "<tr><td onclick=\"javascript:changeImage('imgArtDesc','" + currentItem.itemPhoto + "','lblCodeImg','" + currentItem.itemCode + "');\"><span class='fsal f12'>" + undlineCode + "</span> (" + currentItem.itemColor + ") : <i>" + currentItem.itemMaj + "> "
                            + currentItem.itemCat + "</i> (<a style='color:#2C5987; text-decoration:none;' href='../Maestros/informationarticle.aspx?elcitra=" + currentItem.itemCode + "&isForPublicAcces=T' target='_blank'>Más detalles</a>)</td></tr>";
                        chain += "<tr><td>Disponibilidad:</td></tr>";

                        var itemSize = (from x in dsItems.Tables[0].AsEnumerable()
                                        where x.Field<string>("Art_Id").Equals(currentItem.itemCode)
                                        orderby x.Field<string>("Stk_TalId") ascending
                                            select new
                                            {
                                                itemSize = x.Field<string>("Stk_TalId"),
                                                itemQty = x.Field<decimal>("Stk_Cantidad")
                                            }).Distinct();
                        
                        chain += "<tr><td><table><tr><td><b>Talla:</b><br />Cant:</td>";
                        foreach (var currItemSize in itemSize)
                        {
                            if (size.Equals(currItemSize.itemSize))
                                undlineCode = "<u>" + currItemSize.itemSize + "</u>";
                            else
                                undlineCode = currItemSize.itemSize;
                            chain += "<td align='center'><b>" + undlineCode + "</b><br />" + currItemSize.itemQty + "</td>";
                        }
                        chain += "</tr></table></td></tr>";
                    }
                    chain += "</table></td>";
                }
                chain += "</tr></table>";

                //
                decimal qtyAv = dsItems.Tables[0].AsEnumerable().Where(x => x.Field<string>("Art_Id").Equals(article) && x.Field<string>("Stk_TalId").Equals(size)).Sum(y => y.Field<decimal>("Stk_Cantidad"));
                if (qtyAv >= qty)
                    iconStock = "../../Design/images/Botones/b_info.png";
                else
                    iconStock = "../../Design/images/Botones/b_info_red.png";

            }

            return new stockResponse { _tableHtml = chain, _iconStock = iconStock, _ctrlImgStock = ctrlImgStock };
        }

        /// <summary>
        /// Objeto de resultado de consulta de stock e info de un articulo
        /// </summary>
        public class stockResponse
        {
            public string _tableHtml { get; set; }
            public string _iconStock { get; set; }
            public string _ctrlImgStock { get; set; }
        }

        /// <summary>
        /// Funcion predictiva segun lo que se este tecleando en el campo de escritura del nombre del catalogo
        /// </summary>
        /// <param name="keywordStartsWith"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<Catalog> getPredictions(string keywordStartsWith)
        {
            //
            IList<Catalog> catalog = (IList<Catalog>)(HttpContext.Current.Session[_nSCatalog]);

            if (catalog != null && catalog.Count > 0)
                return catalog.Where(x => x._description.ToUpper().Contains(keywordStartsWith.ToUpper())).ToList<Catalog>();

            DataSet ds = Catalog.getAllCalatogues();

            if (ds == null)
                return null;

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                catalog.Add(new Catalog
                {
                    _idCatalog = item["cav_catalog_id"].ToString(),
                    _description = item["cav_description"].ToString(),
                    _pages = int.Parse(item["can_pages"].ToString()),
                    _endDate = DateTime.Parse(item["cad_init_date"].ToString()),
                    _initDate = DateTime.Parse(item["cad_last_date"].ToString()),
                    _descAdd = item["description"].ToString()
                });
            }


            //HttpContext.Current.Session[_nSCatalog] = catalog;

            return catalog.Where(x => x._description.ToUpper().Contains(keywordStartsWith.ToUpper())).ToList<Catalog>();
        }

        /// <summary>
        /// Consultar articulos por catalogo y pagina
        /// </summary>
        /// <param name="idCatalog"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<Order_Dtl> getItemsByCatalog(string idCatalog, string page)
        {
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];

            DataTable dt = Article_Catalog.getItemsByCatalog(cust._co, idCatalog, int.Parse(page), cust._idWare).Tables[0];

            if (dt == null)
                return null;

            IList<Order_Dtl> linesItems = new List<Order_Dtl>();

            foreach (DataRow item in dt.Rows)
            {
                linesItems.Add(Order_Dtl.getNewLineOrder(item));
            }

            HttpContext.Current.Session[_nsDtlArticle] = linesItems;

            return linesItems;
        }
                
        [WebMethod]
        public static Order_Dtl loadDtlArticle(string code)
        {
            IList<Order_Dtl> linesItems = (List<Order_Dtl>)HttpContext.Current.Session[_nsDtlArticle];

            Order_Dtl article = linesItems.Where(x => x._code.Equals(code.Trim())).FirstOrDefault<Order_Dtl>();

            return article;
        }

        /// <summary>
        /// Consultar las tallas de un item y las cantidades en stock
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [WebMethod]
        public static IList<Articles_Sizes> getItemSizes(string code)
        {
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];

            IList<Articles_Sizes> itemSizes = new List<Articles_Sizes>();

            itemSizes = Articles_Sizes.getObjectSizes(Articles_Sizes.getItemSizes(cust._co, code,cust._idWare).Tables[0], true);
            
            return itemSizes;
        }

        /// <summary>
        /// Agregar un item a la orden de pedido borrador, teniendo los datos cargados desde el dialog de búsqueda de articulo
        /// </summary>
        /// <param name="code"></param>
        /// <param name="size"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        [WebMethod]   //   aca lo comente porque salia error
        public static List<Order_Dtl> addItemToOrder(string code, string size, int unit)
        {
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            string co = cust._co;

            Order_Dtl newLineO = new Order_Dtl();            

            newLineO = loadDtlArticle(code);            
            newLineO._units = 0;
            newLineO._size = size;

            DataSet ds = Discounts.getArticleDiscount(cust._co, code);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                newLineO._dscto = Convert.ToDecimal(dr["valdiscount"]);
                newLineO._dsctoDesc = newLineO._dscto.ToString(_currency);
                newLineO._dsctoPerc = Convert.ToDecimal(dr["porcentaje"]);
                newLineO._dsctoVale = newLineO._price - newLineO._dscto;
                newLineO._dsctoValeDesc = newLineO._dsctoVale.ToString(_currency);
                newLineO._dsctoMsg = dr["div_message"].ToString();
            }
            
            //return addArticle(newLineO, unit , string varTipoPago);
            return addArticle(newLineO, unit, varIdOperacionPOS);
        }


        [WebMethod()]
        public static object ajaxValidarTarjetaPos(string numTarjetaPos)
        {
            string respuesta = Functions.ValidarTarjetaPOS_UPD(numTarjetaPos);

            if (respuesta == "1")
                return "1";
            else
                return respuesta;
        }

        #endregion
        
        #region < Eventos sobre botones >

        /// <summary>
        /// Crear liquidacion de la lista de pedido actualmente en creacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected List<Documents_Trans> getListFromSes()
        {
            if (Session[_nameList] == null)
                Session[_nameList] = new List<Documents_Trans>();
            return (List<Documents_Trans>)Session[_nameList];
        }
        protected void btCreateLiq_Click(object sender, EventArgs e)
        {
            //ahora vemos es es que tiene una nota de cedito como forma de pago

            Boolean _pago_credito = false;

            

            if (h_numTipPago.Value == "007") _pago_credito = true;

            List<Order_Dtl> orderLines = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>()); ;
            if (orderLines.Count == 0)
            {
                msnMessage.LoadMessage("No se genero la liquidacion, porque no hay detalle  ", UserControl.ucMessage.MessageType.Information);
                string script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                return;
            }

            if (fvalidaartpercepcion())
            {
                msnMessage.LoadMessage("No se guardo el pedido, porque en el detalle hay articulo sin percepcion  ", UserControl.ucMessage.MessageType.Information);
                string script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                
                return;
            }
            
            Users user = (Users)Session[Constants.NameSessionUser];
            string typeLiq = string.Empty;
            
            //verificar que no se guarde usuario igual a cero para que nose grabe con este tipo de usuario

            if (user._bas_id==0)
            {
                msnMessage.LoadMessage("No se genero la liquidacion, porque hubo un problema en los datos.. por favor vuelva a intentarlo o reinicie su session: " , UserControl.ucMessage.MessageType.Error);
                string script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);

                return;
            }


            
          

            #region <calculao de la percepcion>
            decimal mtopercepcion;

            mtopercepcion = Convert.ToDecimal(Session[_opcional_percepcion]); //cust._mtopercepcion;

            if (mtopercepcion==0)
            {
                Boolean aplica_percepcion = Convert.ToBoolean(Session["aplica_percepcion_cliente"]);

                //verifcar el esta percepcion es cero porque viene de unos articulos sin percepcion si nno es cierto entonces
                //vemos que esta percepcion no sea cero y pasamos a error
                if (aplica_percepcion)
                { 
                      if (fvalida_sin_percepcion())
                      {
                          decimal _mto_ncredito=Convert.ToDecimal(HttpContext.Current.Session[_valida]);
                          if (_mto_ncredito==0)
                          {
                              msnMessage.LoadMessage("No se genero la liquidacion, porque hubo un problema con el calculo de la percepcion.. por favor vuelva a intentarlo o reinicie su session: ", UserControl.ucMessage.MessageType.Error);
                              string script = string.Empty;
                              script += "closeDialogLoad()";
                              System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                              return;
                          }
                      }
                }
            }
            #endregion

            Transporters_Guides shipping = (Transporters_Guides)Session[_nameSessionShipTo];

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

            int btnLiquidation = 1;
            if (h_numTipPago.Value == varIdOperacionPOS)
            {
                string articulo = "";
                string talla = "";

                if (!(fvalidastock(ref articulo, ref talla)))
                {
                    msnMessage.LoadMessage("No se genero la liquidacion, porque no hay suficiente stock en el producto: " + articulo + " Talla: " + talla, UserControl.ucMessage.MessageType.Error);
                    string script = string.Empty;
                    script += "closeDialogLoad()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);

                    return;
                }               
                    saveOrder_And_Liquidation_POS(false, btnLiquidation);                           
            }
            else
            {
                string articulo = "";
                string talla = "";
                if (!(fvalidastock(ref articulo, ref talla)))
                {
                    msnMessage.LoadMessage("No se genero la liquidacion, porque no hay suficiente stock en el producto: " + articulo + " Talla: " + talla, UserControl.ucMessage.MessageType.Error);
                    string script = string.Empty;
                    script += "closeDialogLoad()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);

                    return;
                }

                //string ordersChain = saveOrderDtl(false, btnLiquidation);

                //if (!string.IsNullOrEmpty(ordersChain))
                doLiquidation(user, "", shipping, typeLiq, mtopercepcion,0, _pago_credito);
            }

                //int btnLiquidation = 1;
                //string ordersChain = saveOrderDtl(false, btnLiquidation);

                //if (!string.IsNullOrEmpty(ordersChain))
                //    doLiquidation(user, ordersChain, shipping, typeLiq);
        }



        /// <summary>
        /// Salir de la lista de pedidos sin guardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btExit_Click(object sender, EventArgs e)
        {
            redirectToParentForm();
        }

        /// <summary>
        /// Guardar la lista de pedido y proceguir con la edicion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSave_Click(object sender, EventArgs e)
        {
            string script = string.Empty;
            List<Order_Dtl> orderLines = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>()); ;
            if (orderLines.Count == 0)
            {
                script += "closeDialogLoadPedido()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                msnMessage.LoadMessage("No se guardo el pedido, porque no hay detalle  ", UserControl.ucMessage.MessageType.Information);
                
                return;
            }

            if (fvalidaartpercepcion())
            {
                script += "closeDialogLoadPedido()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                msnMessage.LoadMessage("No se guardo el pedido, porque en el detalle hay articulo sin percepcion  ", UserControl.ucMessage.MessageType.Information);
                return;
            }

            if (h_numTipPago.Value == varIdOperacionPOS)
            {                
                script += "closeDialogLoadPedido()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                msnMessage.LoadMessage("No se puede guardar un pedido borrador cuando se paga por POS: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
            }
            else
            {
                if (h_numTipPago.Value == "007")
                {
                    script += "closeDialogLoadPedido()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                    msnMessage.LoadMessage("No se puede guardar un pedido borrador cuando se paga por Credito: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                    return;
                }

                _lstDocTx = getListFromSes();
                if (_lstDocTx.Count > 0)
                {
                    List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();
                    if (lstDocTxCheck.Count > 0)
                    {
                        script += "closeDialogLoadPedido()";
                        System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                        msnMessage.LoadMessage("No se puede guardar un pedido borrador cuando se paga con nota de credito o saldo a favor: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                        return;
                    }
                }

                int btnSave = 0;
                string articulo = "";
                string talla = "";

                if (!(fvalidastock(ref articulo, ref talla)))
                {
                    script += "closeDialogLoadPedido()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                    msnMessage.LoadMessage("No se guardo el pedido, porque no hay suficiente stock en el producto: " + articulo + " Talla: " + talla, UserControl.ucMessage.MessageType.Error);
                    //string script = string.Empty;
                    //script += "closeDialogLoad()";
                    //System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);

                    return;
                }

                Users user = (Users)Session[Constants.NameSessionUser];
                if (user._bas_id == 0)
                {
                    msnMessage.LoadMessage("No se genero el pedido, porque hubo un problema en los datos.. por favor vuelva a intentarlo o reinicie su session: ", UserControl.ucMessage.MessageType.Error);
                    script = string.Empty;
                    script += "closeDialogLoadPedido()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);

                    return;
                }

                #region <calculao de la percepcion>
                decimal mtopercepcion;

                mtopercepcion = Convert.ToDecimal(Session[_opcional_percepcion]); //cust._mtopercepcion;

                if (mtopercepcion == 0)
                {
                    Boolean aplica_percepcion = Convert.ToBoolean(Session["aplica_percepcion_cliente"]);

                    if (aplica_percepcion)
                    { 
                    //verifcar el esta percepcion es cero porque viene de unos articulos sin percepcion si nno es cierto entonces
                    //vemos que esta percepcion no sea cero y pasamos a error
                        if (fvalida_sin_percepcion())
                        {
                            decimal _mto_ncredito = Convert.ToDecimal(HttpContext.Current.Session[_valida]);
                            if (_mto_ncredito == 0)
                            {
                                msnMessage.LoadMessage("No se generar el pedido, porque hubo un problema con el calculo de la percepcion.. por favor vuelva a intentarlo o reinicie su session: ", UserControl.ucMessage.MessageType.Error);
                                script = string.Empty;
                                script += "closeDialogLoadPedido()";
                                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                                return;
                            }
                        }
                    }
                }
                #endregion

                saveOrderDtl(true, btnSave);
                script += "closeDialogLoadPedido()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
            }
        }

        /// <summary>
        /// Guardar y salir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSaveExit_Click(object sender, EventArgs e)
        {
            string script = string.Empty;
            List<Order_Dtl> orderLines = (List<Order_Dtl>)(((object)HttpContext.Current.Session[_nSOrder]) != null ? (object)HttpContext.Current.Session[_nSOrder] : new List<Order_Dtl>()); ;
            if (orderLines.Count == 0)
            {
                script += "closeDialogLoadPedido()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                msnMessage.LoadMessage("No se guardo el pedido, porque no hay detalle  ", UserControl.ucMessage.MessageType.Information);
                return;
            }

            if (fvalidaartpercepcion())
            {
                script += "closeDialogLoadPedido()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                msnMessage.LoadMessage("No se guardo el pedido, porque en el detalle hay articulo sin percepcion  ", UserControl.ucMessage.MessageType.Information);
                return;
            }

            if (h_numTipPago.Value == varIdOperacionPOS)
            {
                script += "closeDialogLoadPedido()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                msnMessage.LoadMessage("No se puede guardar un pedido borrador cuando se paga por POS: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
            }
            else 
            {
                if (h_numTipPago.Value == "007")
                {
                    script += "closeDialogLoadPedido()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                    msnMessage.LoadMessage("No se puede guardar un pedido borrador cuando se paga por Credito: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                    return;
                }


                _lstDocTx = getListFromSes();
                if (_lstDocTx.Count > 0)
                {
                    List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();
                    if (lstDocTxCheck.Count > 0)
                    {
                        script += "closeDialogLoadPedido()";
                        System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                        msnMessage.LoadMessage("No se puede guardar un pedido borrador cuando se paga con nota de credito o saldo a favor: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                        return;
                    }
                }
                string articulo = "";
                string talla = "";

                if (!(fvalidastock(ref articulo, ref talla)))
                {
                    script += "closeDialogLoadPedido()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                    msnMessage.LoadMessage("No se guardo el pedido, porque no hay suficiente stock en el producto: " + articulo + " Talla: " + talla, UserControl.ucMessage.MessageType.Error);
                    // string script = string.Empty;
                    // script += "closeDialogLoad()";
                    // System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);

                    return;
                }

                Users user = (Users)Session[Constants.NameSessionUser];
                if (user._bas_id == 0)
                {
                    msnMessage.LoadMessage("No se genero el pedido, porque hubo un problema en los datos.. por favor vuelva a intentarlo o reinicie su session: ", UserControl.ucMessage.MessageType.Error);
                    script = string.Empty;
                    script += "closeDialogLoadPedido()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);

                    return;
                }


                #region <calculao de la percepcion>
                decimal mtopercepcion;

                mtopercepcion = Convert.ToDecimal(Session[_opcional_percepcion]); //cust._mtopercepcion;

                if (mtopercepcion == 0)
                {
                    Boolean aplica_percepcion = Convert.ToBoolean(Session["aplica_percepcion_cliente"]);
                    if (aplica_percepcion)
                    { 
                    //verifcar el esta percepcion es cero porque viene de unos articulos sin percepcion si nno es cierto entonces
                    //vemos que esta percepcion no sea cero y pasamos a error
                        if (fvalida_sin_percepcion())
                        {
                            decimal _mto_ncredito = Convert.ToDecimal(HttpContext.Current.Session[_valida]);
                            if (_mto_ncredito == 0)
                            {
                                msnMessage.LoadMessage("No se generar el pedido, porque hubo un problema con el calculo de la percepcion.. por favor vuelva a intentarlo o reinicie su session: ", UserControl.ucMessage.MessageType.Error);
                                script = string.Empty;
                                script += "closeDialogLoadPedido()";
                                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                                return;
                            }
                        }
                    }
                }
                #endregion

                int btnSaveExit = 0;
                saveOrderDtl(false, btnSaveExit);
                script += "closeDialogLoadPedido()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                redirectToParentForm();
            }
            
        }
        private Boolean fvalidastock(ref string articulo, ref string talla)
        {
            Boolean valida = true;
            string estadoliquid = (string)HttpContext.Current.Session[_estadoliqui];
            string nroliq=(string)HttpContext.Current.Session[_nSOrderUrl];
            
            List<Order_Dtl> order = (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];

            foreach (Order_Dtl item in order)
            {

                Int32 vcantidad = Order_Hdr.fvalidastock(item._code, item._size, item._qty, (!(string.IsNullOrEmpty(estadoliquid))) ? nroliq : "");
                if (vcantidad == 0)
                {
                    articulo = item._code;
                    talla = item._size;
                    valida = false;
                    break;
                }

            }
            return valida;
        }
        #endregion
        
        #region < Crear liquidacion, guardar y actualizar pedidos >
        
        /// <summary>
        /// Realizar liquidacion
        /// </summary>
        /// <param name="co"></param>
        /// <param name="ordersChain"></param>
        /// <param name="shipping"></param>
        protected void doLiquidation(Users user, string ordersChain, Transporters_Guides shipping, string typeLiq,Decimal _varPercepcion,int pagospos=0,Boolean _pago_credito=false)
        {
            // Generar liquidación.
            string[] noOrder;
            decimal comision_customer;
            decimal mtopercepcion;
            string estadoliquid = (string)HttpContext.Current.Session[_estadoliqui];
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            List<Order_Dtl> order = (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];
            comision_customer = cust._commission;

            mtopercepcion =Convert.ToDecimal(Session[_opcional_percepcion]); //cust._mtopercepcion;


            if (fvalidaartcatalogo())
            {
                comision_customer = 0;
                cust._percepcion = 0;
            }

            //string nropedido = "";
            //nropedido = (string)HttpContext.Current.Session[_nropedido];
            //this.Session[_estadoliqui] = estadoliqui;
            //this.Session[_nropedido] = nropedido;
            string _liq = "";
            if (this.Session[_idliquidacion] != null)
            {
                _liq = this.Session[_idliquidacion].ToString(); 
            }

            DataTable dtpago = new DataTable();
            dtpago.Columns.Add("Liq_Id", typeof(string));
            dtpago.Columns.Add("Doc_Tra_Id", typeof(string));
            dtpago.Columns.Add("Monto", typeof(Double));

            _lstDocTx = getListFromSes();
            List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();
            foreach (Documents_Trans dTx in lstDocTxCheck)
            {
                dtpago.Rows.Add("", dTx._numeroid, dTx._value);

                //if (!string.IsNullOrEmpty(listDoc)) listDoc += ",";
                //listDoc += dTx._numeroid;
                //dtpago.Rows.Add(dTx._numeroid);
                ////}
                //vpagonc += dTx._value;
            }



            if (string.IsNullOrEmpty(estadoliquid))
            {

                noOrder = Liquidations_Hdr.Gua_Mod_Liquidacion(user._bas_id, cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion, 1, hdNoOrder.Value, "", 0, 0, "", "", 0, dtpago, _pago_credito, cust._percepcion);

                //noOrder = Liquidations_Hdr.liquidation(user._usv_co, ordersChain, shipping, typeLiq, _varPercepcion);
            }
            else
            {
                string listDoc = f_document_tvo();
                string varNumTarjeta = txtNoTarjeta.Text.Trim();
                string varNumVoucher = txtNoVoucher.Text.Trim();
                decimal varMonto = Convert.ToDecimal(txtValue.Text);
                string vpedido = this.Session[_nropedido].ToString();
                noOrder = Liquidations_Hdr.Gua_Mod_Liquidacion(user._bas_id, cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion, 2, vpedido, _liq, 0, 0, "", "", 0, dtpago, _pago_credito, cust._percepcion);
                //noOrder = Liquidations_Hdr.modyliquidation(user._usv_co, ordersChain, shipping, typeLiq, _varPercepcion, pagospos, varNumTarjeta, varNumVoucher, varMonto, user._usn_userid, h_numConfigPagoPOS.Value, listDoc);
            }


            //saveclear(noOrder[0].ToString());


            
            //en este caso verificamos si es que se va pagar con credito
            if (h_numTipPago.Value == "007")
            {
                Clear.setpagocredito(noOrder[0].ToString());
            }

            //*********************************************************
            if (!noOrder[0].Equals("-1"))
            {
                /*if (!string.IsNullOrEmpty(_typeLiq))
                    Liquidations_Hdr.updateStatusLiquidation(co, noOrder[0], _typeLiq);*/
                string verror = "";
                sbenviarcorreo(noOrder[0], ref verror);

                //if (verror.Length > 0)
                //{
                //    msnMessage.LoadMessage(verror, UserControl.ucMessage.MessageType.Error);
                //    string script = string.Empty;
                //    script += "closeDialogLoad()";
                //    System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                //}
                //else

                //
                // Async 
                //{
                    //Log_Transaction.registerUserInfo(user, "CREATE LIQUIDATION:" + noOrder[0] + " STATUS:" + typeLiq);

                    // Reporte de liquidacion                
                    string url = _pageLiquidReport + "?noLiq=" + noOrder[0];//"?NoOrder=" + noOrder + "&TypeReport=2";
                    //
                    Response.Redirect(url);
                //}
            }
            else
            {
                msnMessage.LoadMessage("Error en la generación de la liquidación; intente de nuevo." + noOrder[1], UserControl.ucMessage.MessageType.Error);
                string script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true); 
            }
        }
        protected void sbenviarcorreo(string vid,ref string verror)
        {
            //enviar correo automatico la liquidacion 
            string vruta = "";
            sbejecutarcrystal(vid , ref vruta,ref verror);
            if (verror.Length > 0)
            {
                return;
            }

            //Liquidations_Hdr.enviar_correos(vid);
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
            if (Session[_nameSessionData] == null)            
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
                _liqValsReport = (ArrayList)Session[_nameSessionData];
                _liqValsSubReport = (ArrayList)Session[_nameSessionData];
                _liqValsPagoSubReport = (ArrayList)Session[_nameSessionData];
            }
        }
        //#endregion

        /// <summary>
        /// Redireccionar al formulario padre
        /// </summary>
        protected void redirectToParentForm()
        {
            Session[_nSOrder] = new List<Order_Dtl>();
            Session[_nSNewOrdrLine] = new List<Order_Dtl>();
            Session[_nSArtSiz] = new List<Articles_Sizes>();
            Session[_nSOrderUrl] = null;
            Session[_estadoliqui] = null;
            Session[_nropedido] = null;
            Response.Redirect(_formParent);
        }

        /// <summary>
        /// Guardar lista de pedido
        /// </summary>
        protected string saveOrderDtl(bool reloadOrder, int btnLiquidation,int modlqpos=0)
        {
            decimal comision_customer;
            decimal mtopercepcion;
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            // Lista de pedido
            List<Order_Dtl> order = (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];
            string noNewOrder = string.Empty;
            string answUpdate = string.Empty;

          //  decimal varMonto = Convert.ToDecimal();
          //  decimal varpercepcion = Math.Round((varMonto * (cust._percepcion / 100)), 2);
            if (modlqpos == 0)
            {
                if (h_numTipPago.Value == varIdOperacionPOS && btnLiquidation == 0)
                {
                    msnMessage.LoadMessage("No se puede guardar un pedido borrador cuando se paga por POS: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                    string rpta = null;
                    return rpta; //EXIT 
                }
            }

            comision_customer = cust._commission;
            mtopercepcion =Convert.ToDecimal(Session[_opcional_percepcion]); //cust._mtopercepcion;

            //para este caso vamos a ver si es que es venta sin comicion catalogo
            if (fvalidaartcatalogo())
            {
                comision_customer = 0;
                cust._percepcion = 0;
            }

            //***************************************

            

            if (string.IsNullOrEmpty(hdNoOrder.Value))
            {
                Users user = (Users)Session[Constants.NameSessionUser];
                noNewOrder = Order_Hdr.saveCompleteOrder(user._bas_id , cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion,1,"",cust._percepcion);
               
                // Async 
               // Log_Transaction.registerUserInfo(user, "CREATE ORDER_HDR:" + noNewOrder);
            }
            else
            {

                string estadoliquid = (string)HttpContext.Current.Session[_estadoliqui];
                string nropedido = "";

                if (!(string.IsNullOrEmpty(estadoliquid)))
                {
                    nropedido = (string)HttpContext.Current.Session[_nropedido];
                    hdNoOrder.Value = nropedido;
                    answUpdate = Order_Hdr.modifyCompleteOrder(cust._co, hdNoOrder.Value, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion);
                }
                else
                {
                    Users user = (Users)Session[Constants.NameSessionUser];
                    answUpdate = Order_Hdr.saveCompleteOrder(user._bas_id, cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion, 2, hdNoOrder.Value,cust._percepcion);
                    //answUpdate = Order_Hdr.modifyCompleteOrder(cust._co, hdNoOrder.Value, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion);
                }
                //{
                    
                //}
                //else
                //{
                //    answUpdate = Order_Hdr.modifyCompleteliquidacion(cust._co, hdNoOrder.Value, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion);
                //}
                if (!answUpdate.Equals("0"))
                    noNewOrder = hdNoOrder.Value;
                else
                    noNewOrder = answUpdate;
            }

            if (!noNewOrder.Equals("0"))
            {
                //
                msnMessage.LoadMessage("Pedido guardado correctamente. Última actualización: " + DateTime.Now, UserControl.ucMessage.MessageType.Information);
                //
                if (reloadOrder)
                    getOrderDtl(cust, noNewOrder);
            }
            else
                msnMessage.LoadMessage("Lamentablemente el pedido no se ha guardado", UserControl.ucMessage.MessageType.Error);

            return noNewOrder;
        }

        private string  f_document_tvo()
        {
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            //en este codigo vamos a grabar el clear si es que tiene nota d ecredito
            string clear = string.Empty;
            string listDoc = string.Empty;
            decimal vpagonc = 0;
            _lstDocTx = getListFromSes();
            List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();
            foreach (Documents_Trans dTx in lstDocTxCheck)
            {
                if (!string.IsNullOrEmpty(listDoc)) listDoc += ",";
                listDoc += dTx._numeroid;
                //}
                vpagonc += dTx._value;
            }
            //grabamos el clear
            return listDoc;
        }

        private void saveclear(string vorder)
        {

            //en este borramos de la tabla liquidacion_nc el pedido y su nota de credito
            Clear.sb_borraliqnc(vorder);
            //

            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            //en este codigo vamos a grabar el clear si es que tiene nota d ecredito
            string clear = string.Empty;
            string listDoc = string.Empty;
            string listLiq = vorder;
            DataTable dtpago = new DataTable();
            dtpago.Columns.Add("Doc_Tra_Id", typeof(string));
            decimal vpagonc = 0;
            _lstDocTx = getListFromSes();
            List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();

            foreach (Documents_Trans dTx in lstDocTxCheck)
            {
                if (!string.IsNullOrEmpty(listDoc)) listDoc += ",";
                listDoc += dTx._numeroid;
                dtpago.Rows.Add(dTx._numeroid);
                //}
                vpagonc+=dTx._value;   
            }
            //grabamos el clear
            if (!(listLiq == "-1"))
            {
                if (listDoc.Length > 0)
                {
                    if (cust._mtoimporte <= vpagonc)
                    {
                        clear = Clear.setPreClear(listLiq, dtpago);
                        //en este caso vamoa a enviar un pedido pagado con nc quedando en su totalidad
                        //procedimiento envio de correo  al usuario admin

                        //DataTable dt = Clear.fgetcorreoenvio();

                        //if (dt.Rows.Count > 0)
                        //{
                        //    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                        //    {
                        //        sbenviarcorreopago(dt.Rows[i]["email"].ToString(), vorder);
                        //    }
                        //}
                        ////////////////////////////////////
                    }
                    else
                    {
                        Clear.sb_addliquidacionnc(vorder, listDoc);
                        //en este caso vamoa a enviar un pedido pagado con nc quedando un saldo
                        //procedimiento envio de correo  al usuario admin

                        //DataTable dt = Clear.fgetcorreoenvio();

                        //if (dt.Rows.Count > 0)
                        //{
                        //    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                        //    {
                        //        sbenviarcorreopago(dt.Rows[i]["email"].ToString(), vorder);
                        //    }
                        //}
                        ////////////////////////////////////

                    }
                }
            }
        }

        #region <Envio de Correos pago>
        protected void sbenviarcorreopago(string destinatario,string vpedido)
        {
            //enviar correo automatico la liquidacion 

            string path = MapPath("../../Design/templateMailliquidacion.htm");
            //string destinatario = _cust._email;

            //string vrutaarchivoweb = MapPath("../../Correo/Pedido/" + noOrder[0] + ".doc");

            //string vr = Server.MapPath(""); 
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            string vcliente = cust._nombrecompleto;// _cust._n

            string vhtml = fhtml(vpedido);

            Utilities.sendInstitutionalMessage(destinatario, "Copia de respaldo del pago con Nota de credito del Cliente: " + vcliente + " [AQUARELLA]",
                "Estimado usuario, este es una copia del pago con nota de credito generado por el Cliente; a continuación se detalla la información:",
                vhtml, path);


        }
        protected string fhtml(string vpedido)
        {
            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            decimal vimporte = cust._mtoimporte;

            _lstDocTx = getListFromSes();

            _lstDocTx.Add(new Documents_Trans
            {
                _check = true,
                _docNo = "",
                _numeroid = "",
                _value = vimporte,
                _date = "PEDIDO No." + vpedido,
                _fechadoc=DateTime.Today.ToShortDateString()
                //* Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase")),
                // _increase = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase"))
            });

            Session[_nameList] = _lstDocTx;

            
            decimal vtotalnc = _lstDocTx.Where(x => x._check).Sum(y => (y._value));//decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value * y._increase));
            decimal grandTotal = vtotalnc - vimporte;

            string grandTotalstr = grandTotal.ToString(ConfigurationManager.AppSettings["kCurrency"]);


            string vhtml = "";
            List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();
            Int32 vbucle = 0;

            string vestilos = "<style type='text/css'> <!-- .estilo1 { font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #003366; }" +
                         ".estilo2 {font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: #990000; font-weight: bold; } .estilo3 {font-family: Arial, Helvetica, sans-serif; font-size: 13px;font-weight: bold; } --> " +
                         "</style> ";

            foreach (Documents_Trans dTx in lstDocTxCheck)
            {
                decimal valor = dTx._value;
                string valorstr = "";
                if (valor < 0)
                {
                    valor = valor * -1;
                }

                valorstr = String.Format("{0:C}", valor);

                if (vbucle == 0)
                {
                    vhtml = vestilos + "<table cellpadding='0' cellspacing='0' border='1'>" +
                            "<thead><tr><th>Fecha</th><th>Concepto</th><th>Importe</th></tr>" +
                            "</thead><tr><td class='estilo1'>" + dTx._fechadoc + "</td><td class='estilo1'>" + dTx._date.ToString() + "</td><td class='estilo1'>" + valorstr + "</td></tr>";

                    vbucle = 1;
                }
                else
                {
                    if (vbucle == lstDocTxCheck.Count)
                    {
                        vhtml += "<tr><td class='estilo1'>" + dTx._fechadoc + "</td><td class='estilo1'>" + dTx._date.ToString() + "</td><td class='estilo1'>" + valorstr + "</td></tr>";
                        vhtml += "<tr><td></td><td class='estilo3'>Saldo</td><td class='estilo3'>" + grandTotalstr + "</td></tr></table>";
                    }
                    else
                    {
                        vhtml += "<tr><td class='estilo1'>" + dTx._fechadoc + "</td><td class='estilo1'>" + dTx._date.ToString() + "</td><td class='estilo1'>" + valorstr + "</td></tr>";
                    }
                }

                

                vbucle += 1;
                //if (dTx._conceptid.Equals("PAGOS"))
                //{

                //}
                //if (dTx._conceptid.Equals("LIQUIDACIONES"))
                //{

                //}
            }

            return vhtml;
        }

        #endregion

        protected void saveOrder_And_Liquidation_POS(bool reloadOrder, int btnLiquidation)
        {
            decimal comision_customer;
            decimal mtopercepcion;
            string varNumTarjeta = txtNoTarjeta.Text.Trim();
            string varNumVoucher = txtNoVoucher.Text.Trim();
            decimal varMonto = Convert.ToDecimal(txtValue.Text);

            

            Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
            Users user = (Users)Session[Constants.NameSessionUser];

            //para guardar la percepcion
            mtopercepcion = cust._mtopercepcion;
            // Lista de pedido
            List<Order_Dtl> order = (List<Order_Dtl>)HttpContext.Current.Session[_nSOrder];
            string noNewOrder = string.Empty;
            string answUpdate = string.Empty;
            
            string[] noOrder;

            string typeLiq = string.Empty;
            Transporters_Guides shipping = (Transporters_Guides)Session[_nameSessionShipTo];
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


           // if (h_numTipPago.Value == varIdOperacionPOS)
              comision_customer = cust._commission_POS_visaUnica;

              if (fvalidaartcatalogo())
              {
                  comision_customer = 0;
              }  

              if (string.IsNullOrEmpty(hdNoOrder.Value) || (!(string.IsNullOrEmpty(hdNoOrder.Value))))  
              {

                  string listDoc = f_document_tvo();

                  //modificamos la liquidacion para todos lo pagoa POS
                  string estadoliquid = (string)HttpContext.Current.Session[_estadoliqui];
                  string nropedido = "";

                  string strformapagoid = "";

                  cust._vartipopago = strformapagoid;


                  if (!(string.IsNullOrEmpty(estadoliquid)))
                  {
                      nropedido = (string)HttpContext.Current.Session[_nropedido];
                      hdNoOrder.Value = nropedido;

                      //answUpdate = Order_Hdr.modifyCompleteOrder(cust._co, hdNoOrder.Value, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion);

                      //if (!string.IsNullOrEmpty(answUpdate))
                         //doLiquidation(user, nropedido, shipping, typeLiq, mtopercepcion, 1);
                         string _liq = this.Session[_idliquidacion].ToString(); 
                         noOrder = Liquidations_Hdr.Gua_Mod_Liquidacion(user._bas_id, cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion, 2, hdNoOrder.Value, _liq, 0, 1, varNumTarjeta, varNumVoucher, varMonto,null,false,cust._percepcion);

                      //return;
                  }
                  else
                  {
                      //vamos a ver si es que se esta modifcancon el pedido y se cambia la forma de pago

                      string vnroped = (string)this.Session[_nSOrderUrl];
                      if (string.IsNullOrEmpty(vnroped))
                      {
                          noOrder = Liquidations_Hdr.Gua_Mod_Liquidacion(user._bas_id, cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion, 1, hdNoOrder.Value, "", 0, 1, varNumTarjeta, varNumVoucher, varMonto, null, false, cust._percepcion);


                          //noOrder = Order_Hdr.saveOrder_And_Liquidation_POS_BL(cust._co, cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, shipping, typeLiq, varNumTarjeta, varNumVoucher, varMonto, user._usn_userid, h_numConfigPagoPOS.Value, mtopercepcion, listDoc);
                      }
                      else
                      {
                          nropedido = (string)HttpContext.Current.Session[_nSOrderUrl];
                          hdNoOrder.Value = nropedido;

                          noOrder = Liquidations_Hdr.Gua_Mod_Liquidacion(user._bas_id, cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion, 1, hdNoOrder.Value, "", 0, 1, varNumTarjeta, varNumVoucher, varMonto, null, false, cust._percepcion);

                          //answUpdate = Order_Hdr.modifyCompleteOrder(cust._co, hdNoOrder.Value, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, mtopercepcion);

                          //noOrder = Order_Hdr.saveOrder_And_ped_POS_BL(cust._co, cust._idCust, string.Empty, comision_customer, 0, string.Empty, string.Empty, order, shipping, typeLiq, varNumTarjeta, varNumVoucher, varMonto, user._usn_userid, h_numConfigPagoPOS.Value, mtopercepcion, listDoc, nropedido);
                      }

                  }

                  //***************************************************

                  //saveclear(noOrder[0].ToString());
                
                  // Async 
                  //(noOrder.ToString());
                  //**************************
                  if (!noOrder[0].Equals("-1") /*&& !noOrder[1].Equals("-1")*/)
                  {
                      /*if (!string.IsNullOrEmpty(_typeLiq))
                          Liquidations_Hdr.updateStatusLiquidation(co, noOrder[0], _typeLiq);*/

                      // Async 
                      //Log_Transaction.registerUserInfo(user, "CREATE ORDER_HDR:" + noOrder[0]);
                      //Log_Transaction.registerUserInfo(user, "CREATE LIQUIDATION:" + noOrder[1] + " STATUS:" + typeLiq);
                      ////enviar correo para la liquidacion
                      string verror = "";
                      sbenviarcorreo(noOrder[0],ref verror);

                      //msnMessage.LoadMessage(verror, UserControl.ucMessage.MessageType.Error);
                      //string script = string.Empty;
                      //script += "closeDialogLoad()";
                      //System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);

                      ////////////////////////////////////////////////////
                      // Reporte de liquidacion                
                      string url = _pageLiquidReport + "?noLiq=" + noOrder[0];//"?NoOrder=" + noOrder + "&TypeReport=2";
                      //
                      Response.Redirect(url);
                  }
                  else
                  {
                      msnMessage.LoadMessage("Error en la generación de la liquidación; intente de nuevo." + noOrder[1], UserControl.ucMessage.MessageType.Error);
                      string script = string.Empty;
                      script += "closeDialogLoad()";
                      System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
                  }
              }
              else
              {
                  msnMessage.LoadMessage("No se debe de modificar cuando se realiza un pago por POS" , UserControl.ucMessage.MessageType.Error);
                  string script = string.Empty;
                  script += "closeDialogLoad()";
                  System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "CloseDialog", script, true);
              }
        }


        #endregion

        protected void btncredito_Click(object sender, EventArgs e)
        {
            //pnlDwCustomers.Visible = false;

            ////
            //setParamsDataSource("1");
            ////
           
            ////
            //refreshGrid();    
        }
        //protected void refreshGrid()
        //{
        //    gvnc.DataSourceID = odsnc.ID;
        //    gvnc.DataBind();
        //}
        //protected void setParamsDataSource(string co)
        //{
        //    odsnc.SelectParameters[0].DefaultValue = co;
            
        //}
        //protected void odsnc_Selected(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
        //{
        //    try
        //    {
        //        /*DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
        //        Session[_nameSessionData] = dt;*/
        //        Session[_nameList] = new List<Documents_Trans>();
        //    }
        //    catch
        //    { }
        //}

     
        //protected void totals()
        //{
        //    try
        //    {
        //        _lstDocTx = getListFromSes();
        //        string imag = "<img src='../../Design/images/";
        //        decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value));//decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value * y._increase));

        //        decimal creditValue = decimal.Zero;

        //        decimal.TryParse(hdCreditValue.Value, out creditValue);

        //        gvnc.FooterRow.Cells[2].Text = grandTotal.ToString(ConfigurationManager.AppSettings["kCurrency"]);
        //        if (grandTotal + creditValue < 0)
        //        {
        //           // gvnc.FooterRow.Cells[3].ForeColor = System.Drawing.Color.Red;
        //           // imag += "b_inactive.png' />";
        //            //btCreateClear.Enabled = false;
        //        }
        //        else if (grandTotal + creditValue >= 0)
        //        {
        //            gvnc.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
        //            //imag += "b_active.png' />";
        //            //btCreateClear.Enabled = true;
        //        }
        //        else
        //        {
        //            gvnc.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
        //            //imag = string.Empty;
        //            //btCreateClear.Enabled = false;
        //        }

        //       // gvClear.FooterRow.Cells[4].Text = imag;
        //    }
        //    catch { }
        //}

        //protected void setNoDocTx(string docTx, bool action)
        //{
        //    try
        //    {
        //        _lstDocTx = getListFromSes();
        //        Documents_Trans docTxObj = _lstDocTx.Where(x => x._docNo.Equals(docTx)).FirstOrDefault();
        //        _lstDocTx.Remove(docTxObj);
        //        docTxObj._check = action;
        //        _lstDocTx.Add(docTxObj);

        //        Session[_nameList] = _lstDocTx;
        //    }
        //    catch
        //    { }
        //}
        //protected List<Documents_Trans> getListFromSes()
        //{
        //    if (Session[_nameList] == null)
        //        Session[_nameList] = new List<Documents_Trans>();
        //    return (List<Documents_Trans>)Session[_nameList];
        //}

        //protected void gvnc_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //decimal increase = 0;
        //        decimal value = 0;

        //        if (decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "importe").ToString(), out value))
        //        {
        //            //
        //            //if (increase < 0 || value < 0)
        //            //{
        //                e.Row.Cells[2].ForeColor = System.Drawing.Color.Salmon;

        //                if (!DataBinder.Eval(e.Row.DataItem, "importe").ToString().Equals("LIQUIDACIONES"))
        //                {
        //                    //
        //                    bool temp;
        //                    CheckBox chk = (CheckBox)e.Row.FindControl("chkDocument");
        //                    bool.TryParse(DataBinder.Eval(e.Row.DataItem, "active").ToString(), out temp);
        //                    chk.Enabled = temp;
        //                    bool.TryParse(DataBinder.Eval(e.Row.DataItem, "checks").ToString(), out temp);
        //                    chk.Checked = temp;
        //                    if (temp)
        //                        setNoDocTx(DataBinder.Eval(e.Row.DataItem, "ncredito").ToString(), temp);
        //                }
        //            //}
        //            else
        //                e.Row.Cells[2].ForeColor = System.Drawing.Color.Green;
        //        }

        //        setListDocTx(e);
        //    }
        //}
        //protected void setListDocTx(GridViewRowEventArgs e)
        //{
        //    _lstDocTx = getListFromSes();

        //    _lstDocTx.Add(new Documents_Trans
        //    {
        //        _check = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "checks").ToString()),
        //        _docNo = DataBinder.Eval(e.Row.DataItem, "ncredito").ToString(),
        //        _conceptid = DataBinder.Eval(e.Row.DataItem, "rhv_return_no").ToString(),
        //        _value = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "importe")))  //* Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase")),
        //       // _increase = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase"))
        //    });

        //    Session[_nameList] = _lstDocTx;
        //}

        //protected void chkDocument_CheckedChanged(object sender, EventArgs e)
        //{
        //    string docTxCheck = ((CheckBox)sender).ToolTip;
        //    setNoDocTx(docTxCheck, ((CheckBox)sender).Checked);
        //    totals();
        //}

        //protected void gvnc_DataBound(object sender, EventArgs e)
        //{
        //    totals();
        //}

        //metodo de forma de pago de la liquidacion
        [WebMethod()]
        public static List<formapago> getformapago()
        {
            List<formapago> formaList = new List<formapago>();

            Users _user = new Users();
            _user = (Users)HttpContext.Current.Session[Constants.NameSessionUser];

            DataSet dsformapago = formapago.Get_CARGAR_POS( _user._usv_postpago);

            if (dsformapago != null)
            {
                if (dsformapago.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsformapago.Tables[0].Rows)
                    {
                        formapago formap = new formapago(row["Con_Descripcion"].ToString(), row["Con_Id"].ToString());
                        formaList.Add(formap);
                    }
                }
            }
            return formaList;
        }
        [WebMethod()]
        public static formapago get_formapago(string strformapagoid, string strformapago)
        {
            formapago formap;
            try
            {
                Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];

                cust._vartipopago = strformapagoid;

                if (strformapagoid=="007")
                {
                    HttpContext.Current.Session["ListDocTx"] = new List<Documents_Trans>();
                    (HttpContext.Current.Session[_valida]) = 0;
                }

                formap = new formapago(strformapago, strformapagoid);



                //sbupdateitemforma();
                //{
                //    _namecompleto = cust._nombrecompleto,
                //    _estadoliqui = _vestadoliquid,
                //    _estadoboton = _vestadoboton
                //};
            }
            catch
            {
                formap = new formapago("", "");
            }
            return formap;
        }
    }
}
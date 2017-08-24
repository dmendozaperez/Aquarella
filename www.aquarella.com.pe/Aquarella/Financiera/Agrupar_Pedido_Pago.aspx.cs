using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Jayrock.Json;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using System.Configuration;
using System.Data;

namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class Agrupar_Pedido_Pago : System.Web.UI.Page
    {
        Users _user;

        List<Documents_Trans> _lstDocTx;

        string _nameList = "ListDocTx";

        String _pageLiquidReport = "../../Aquarella/Logistica/Panel_Liquidacion_Lider.aspx";

        #region <Envio de Correos>
        protected void sbenviarcorreo(string destinatario)
        {
            //enviar correo automatico la liquidacion 

            string path = MapPath("../../Design/templateMailliquidacion.htm");
            //string destinatario = _cust._email;

            //string vrutaarchivoweb = MapPath("../../Correo/Pedido/" + noOrder[0] + ".doc");

            //string vr = Server.MapPath(""); 

            string vcliente = dwCustomers.SelectedItem.Text.ToString();

            string vhtml = fhtml();

            Utilities.sendInstitutionalMessage(destinatario, "Copia de respaldo del Cruce de pago del Cliente: " + vcliente + " [AQUARELLA]",
                "Estimado usuario, este es una copia del cruce generado por el Cliente; a continuación se detalla la información:",
                vhtml, path);


        }
        protected string fhtml()
        {

            _lstDocTx = getListFromSes();
            decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value));//decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value * y._increase));
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
                            "</thead><tr><td class='estilo1'>" + dTx._fechadoc + "</td><td class='estilo1'>" + dTx._date + "</td><td class='estilo1'>" + valorstr + "</td></tr>";
                    vbucle = 1;
                }
                else
                {
                    if (vbucle == lstDocTxCheck.Count)
                    {
                        vhtml += "<tr><td class='estilo1'>" + dTx._fechadoc + "</td><td class='estilo1'>" + dTx._date + "</td><td class='estilo1'>" + valorstr + "</td></tr>";
                        vhtml += "<tr><td></td><td class='estilo3'>Saldo</td><td class='estilo3'>" + grandTotalstr + "</td></tr></table>";
                    }
                    else
                    {
                        vhtml += "<tr><td class='estilo1'>" + dTx._fechadoc + "</td><td class='estilo1'>" + dTx._date + "</td><td class='estilo1'>" + valorstr + "</td></tr>";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session["_saldo"] ="0";
                Session["_list_liq"] = string.Empty;
                Session[_nameList] = new List<Documents_Trans>();
                //btCreateClear.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de ANULAR el pedido con número : -" + "" + "-, Perteneciente A : -" + "" + "- ?')");
                //
                ((BoundField)(gvClear.Columns[3])).DataFormatString = "{0:" + ConfigurationManager.AppSettings["kCurrency"] + "}";

                if ((_user._usu_tip_id == "01") || (_user._usu_tip_id == "03"))
                {
                    formForCustomer();
                }
                else
                {
                    formForEmployee();
                }

                //if (_user._usv_employee)
                //    formForEmployee();
                //else if (_user._usv_customer)
                //    formForCustomer();
            }



        }       
        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        protected void formForEmployee()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de coordinadores
            DataSet dsCustomers = Lider.Lider.getlider();// Coordinator.getCoordinators(_user._usv_area);
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
            // Ocultar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = false;

            //
            setParamsDataSource(_user._usn_userid.ToString());
            //
            setCreditValueCust(_user._usv_co, _user._usn_userid);
            //
            refreshGrid();
        }
        /// <summary>
        /// Set de los parametros necesarion en el datasource para realizar la consulta
        /// </summary>
        /// <param name="co"></param>
        /// <param name="idCust"></param>
        protected void setParamsDataSource(string idCust)
        {
            odsClear.SelectParameters[0].DefaultValue = idCust;
        }
        protected void dwCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            // Nuevo cliente seleccionado
            string selCust = ((DropDownList)sender).SelectedValue;

            Session[_nameList] = new List<Documents_Trans>();
            _lstDocTx = getListFromSes();

            /// Verificar que sea una selección valida
            if (!string.IsNullOrEmpty(selCust) && selCust != "-1")
            {
                setParamsDataSource(selCust);
                setCreditValueCust(_user._usv_co, Convert.ToDecimal(selCust));
                refreshGrid();
            }

            //btCreateClear.Enabled = (_lstDocTx.Count==0)? false:true;
        }

        protected void setCreditValueCust(string co, decimal cust)
        {
            //,
            //DataTable dt = Coordinator.getCoordinator(co, cust).Tables[0];
            ////
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    //
            //    DataRow dr = dt.Rows[0];
            //    if (!string.IsNullOrEmpty(dr["cov_credit_flag"].ToString()) && dr["cov_credit_flag"].ToString().Equals("T"))
            //        hdCreditValue.Value = dr["con_credit_limit"].ToString();
            //    else
            //        hdCreditValue.Value = decimal.Zero.ToString();
            //}
        }
        protected void gvClear_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                decimal increase = 0;
                decimal value = 0;

                if (decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "credito").ToString(), out value)
                    || decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "von_increase").ToString(), out increase))
                {
                    //
                    if (increase < 0 || value < 0)
                    {
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Salmon;                        
                        if (!DataBinder.Eval(e.Row.DataItem, "dtv_concept_id").ToString().Equals("LIQUIDACIONES"))
                        {
                            //
                            bool temp;
                            CheckBox chk = (CheckBox)e.Row.FindControl("chkDocument");
                            bool.TryParse(DataBinder.Eval(e.Row.DataItem, "active").ToString(), out temp);
                            chk.Enabled = temp;
                            bool.TryParse(DataBinder.Eval(e.Row.DataItem, "checks").ToString(), out temp);
                            chk.Checked = temp;
                            if (temp)
                                setNoDocTx(DataBinder.Eval(e.Row.DataItem, "dtv_transdoc_id").ToString(), temp);
                        }
                    }
                    else
                        e.Row.Cells[7].ForeColor = System.Drawing.Color.Green;
                }

                setListDocTx(e);
            }
        }
        /// <summary>
        /// Refrescar datos
        /// </summary>
        protected void refreshGrid()
        {
            gvClear.DataSourceID = odsClear.ID;
            gvClear.DataBind();
        }

        protected void gvClear_DataBound(object sender, EventArgs e)
        {
            totals();
        }
        protected void totals()
        {
            try
            {
                _lstDocTx = getListFromSes();
                string imag = "<img src='../../Design/images/";
                decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value));//decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value * y._increase));

                decimal grandTotal_pago = _lstDocTx.Sum(y => y._valuepago);

                decimal creditValue = decimal.Zero;

                decimal.TryParse(hdCreditValue.Value, out creditValue);

                gvClear.FooterRow.Cells[6].Text = grandTotal.ToString(ConfigurationManager.AppSettings["kCurrency"]);

                gvClear.FooterRow.Cells[5].Text = grandTotal_pago.ToString(ConfigurationManager.AppSettings["kCurrency"]);

                gvClear.FooterRow.Cells[3].Text = (grandTotal_pago - grandTotal).ToString(ConfigurationManager.AppSettings["kCurrency"]);

                total_saldo.Value = (grandTotal_pago - grandTotal).ToString();
                Session["_saldo"] = (grandTotal_pago - grandTotal).ToString();
                if (grandTotal + creditValue < 0)
                {
                    gvClear.FooterRow.Cells[7].ForeColor = System.Drawing.Color.Red;
                    imag += "b_inactive.png' />";
                    //btCreateClear.Enabled = false;
                }
                else if (grandTotal + creditValue >= 0)
                {
                    gvClear.FooterRow.Cells[7].ForeColor = System.Drawing.Color.White;
                    imag += "b_active.png' />";
                //    btCreateClear.Enabled = true;
                }
                else
                {
                    gvClear.FooterRow.Cells[7].ForeColor = System.Drawing.Color.White;
                    imag = string.Empty;
                  //  btCreateClear.Enabled = false;
                }

                gvClear.FooterRow.Cells[7].Text = imag;
                //btCreateClear.Enabled = true;
            }
            catch { }
        }
        /// <summary>
        /// Chequeo de fila en gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkDocument_CheckedChanged(object sender, EventArgs e)
        {
            string docTxCheck = ((CheckBox)sender).ToolTip;
            setNoDocTx(docTxCheck, ((CheckBox)sender).Checked);
            totals();
        }
        protected void btCreateLiq_Click(object sender, EventArgs e)
        {

            //hpedido.Value = "120";
                                              
            msnMessage.HideMessage();
            string co = _user._usv_co;
            string listLiq = string.Empty;
            string listDoc = string.Empty;
            string clear = string.Empty;

            _lstDocTx = getListFromSes();

            List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();
                                            
            if (lstDocTxCheck.Count == 0)
            {
                msnMessage.LoadMessage("no hay pedidos para generar su liquidacion", UserControl.ucMessage.MessageType.Error);
                string script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "CloseDialog", script, true);
                return;
            }

           

            DataTable dt_pedido = new DataTable();
            dt_pedido.Columns.Add("pedido", typeof(string));
            //dt_cruce.Columns.Add("Monto", typeof(Double));
            Int32 fvalidapedido = 0;
            //DataTable dtpagos = new DataTable();
            //dtpagos.Columns.Add("Doc_Tra_Id", typeof(string));
            foreach (Documents_Trans dTx in lstDocTxCheck)
            {
                if (dTx._conceptid.Equals("PAGOS"))
                {
                    //dt_cruce.Rows.Add('P', dTx._value);
                    //if (!string.IsNullOrEmpty(listDoc)) listDoc += ",";
                    //listDoc += dTx._docNo;
                    //dtpagos.Rows.Add(dTx._docNo);
                }
                if (dTx._conceptid.Equals("LIQUIDACIONES"))
                {
                    //dt_cruce.Rows.Add('L', dTx._value * -1);
                    dt_pedido.Rows.Add(dTx._docNo);
                    fvalidapedido += 1;                    
                    if (!string.IsNullOrEmpty(listLiq)) listLiq += ",";
                    listLiq += dTx._docNo;
                }
            }

            decimal _saldo = Convert.ToDecimal(Session["_saldo"].ToString());
            if (_saldo < 0)
            {
                _saldo = _saldo * -1;
                msnMessage.LoadMessage("no se puede agrupar porque queda un saldo de " + _saldo.ToString(ConfigurationManager.AppSettings["kCurrency"]) + " por pagar", UserControl.ucMessage.MessageType.Error);
                string script = string.Empty;
                script += "closeDialogLoad()";
                System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "CloseDialog", script, true);
                return;
            }

            //*************************************

            // Cruce financiero de pagos y liquidaciones
            if (!string.IsNullOrEmpty(listLiq) )
            {
                //
                try
                {
                    string _lider_codigo = (pnlDwCustomers.Visible) ? dwCustomers.SelectedValue : _user._usn_userid.ToString();                    


                    string _validaref = string.Empty;
                    string _liq_ref = "";
                    clear = Clear._ejecutar_agrupar_pedido(Convert.ToDecimal(_lider_codigo), _user._bas_id, dt_pedido, ref _liq_ref);

                    // en este caso vamos hacer un update a la nota de credito de financiera de document_trans
                    //Clear.sbupdateclearncredito(listLiq, clear);

                    //


                    if (clear.Length==0)
                    {
                        Session["_saldo"] = "0";
                        string script = string.Empty;
                        script += "closeDialogLoad()";
                        System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "CloseDialog", script, true);
                        msnMessage.LoadMessage("El agrupamiento genero un numero de liquidacion para el lider , se genera el pago numero: " + clear, UserControl.ucMessage.MessageType.Information);
                        string url = _pageLiquidReport + "?noLiq=" + _liq_ref;//"?NoOrder=" + noOrder + "&TypeReport=2";
                        //
                        Response.Redirect(url);

                        //procedimiento envio de correo  al usuario admin

                        //DataTable dt = Clear.fgetcorreoenvio();

                        //if (dt.Rows.Count > 0)
                        //{
                        //    for (Int32 i = 0; i <= dt.Rows.Count - 1; i++)
                        //    {
                        //        sbenviarcorreo(dt.Rows[i]["email"].ToString());
                        //    }
                        //}
                        ////////////////////////////////////
                        // Async 
                        //Log_Transaction.registerUserInfo(_user, "CREATE CLEAR:" + clear);
                    }
                    else
                        throw new InvalidCastException();

                    refreshGrid();
                }
                catch (InvalidCastException ic)
                {
                    msnMessage.LoadMessage("Error ocurrido realizando el cruce financiero; por favor intente de nuevo; detalle: " + ic.Message, UserControl.ucMessage.MessageType.Error);
                    string script = string.Empty;
                    script += "closeDialogLoad()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "CloseDialog", script, true);
                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage("Error ocurrido realizando el cruce financiero; por favor intente de nuevo; detalle: " + ex.Message, UserControl.ucMessage.MessageType.Error);
                    string script = string.Empty;
                    script += "closeDialogLoad()";
                    System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "CloseDialog", script, true);
                }
            }
            // Cruce financiero de documentos
            else if (!string.IsNullOrEmpty(listDoc))
            {
                clear = Clear.setClearingDoc(co, listDoc);
                try
                {
                    if (!string.IsNullOrEmpty(clear))
                    {
                        msnMessage.LoadMessage("El cruce de información fue grabado correctamente; número del cruce: " + clear, UserControl.ucMessage.MessageType.Information);
                        // Async 
                        Log_Transaction.registerUserInfo(_user, "CREATE CLEAR:" + clear);
                    }
                    else
                        throw new InvalidCastException();
                    refreshGrid();
                }
                catch (InvalidCastException ic)
                {
                    msnMessage.LoadMessage("Error ocurrido realizando el cruce financiero; por favor intente de nuevo; detalle: " + ic.Message, UserControl.ucMessage.MessageType.Error);
                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage("Error ocurrido realizando el cruce financiero; por favor intente de nuevo; detalle: " + ex.Message, UserControl.ucMessage.MessageType.Error);
                }
            }
            else
                msnMessage.LoadMessage("Por favor, debe seleccionar los documentos que formaran el cruce financiero.", UserControl.ucMessage.MessageType.Error);
        }
        protected void setListDocTx(GridViewRowEventArgs e)
        {
            _lstDocTx = getListFromSes();

            _lstDocTx.Add(new Documents_Trans
            {
                _check = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "checks").ToString()),
                _docNo = DataBinder.Eval(e.Row.DataItem, "pedido").ToString(),
                _date = DataBinder.Eval(e.Row.DataItem, "cov_description").ToString(),
                _conceptid = DataBinder.Eval(e.Row.DataItem, "dtv_concept_id").ToString(),
                _value = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "val"))) ,//* Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase")),
                _valuepago = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "totalop"))) ,
                _increase = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase")),
                _fechadoc = (DataBinder.Eval(e.Row.DataItem, "document_date_desc").ToString()),                
            });

            Session[_nameList] = _lstDocTx;
        }
        /// <summary>
        /// Consulta de lista de document_trans en session
        /// </summary>
        /// <returns></returns>
        protected List<Documents_Trans> getListFromSes()
        {
            if (Session[_nameList] == null)
                Session[_nameList] = new List<Documents_Trans>();
            return (List<Documents_Trans>)Session[_nameList];
        }
        protected void setNoDocTx(string docTx, bool action)
        {
            try
            {
                _lstDocTx = getListFromSes();
                Documents_Trans docTxObj = _lstDocTx.Where(x => x._docNo.Equals(docTx)).FirstOrDefault();
                _lstDocTx.Remove(docTxObj);
                docTxObj._check = action;
                _lstDocTx.Add(docTxObj);

                Session[_nameList] = _lstDocTx;
            }
            catch
            { }
        }
        protected void odsClear_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                /*DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessionData] = dt;*/
                Session[_nameList] = new List<Documents_Trans>();
            }
            catch
            { }
        }
        #region < Ajax For Pays Online >

        [WebMethod()]
        public static Object ajaxGetLiqCheck()
        {
            /// Deido a que la funcion es estatica se deben crear referencias para podre hacer llamados a algunos metodos
            System.Web.SessionState.HttpSessionState sessions = HttpContext.Current.Session;

            //
            List<Documents_Trans> lstDocTx = (List<Documents_Trans>)sessions["ListDocTx"];

            string list_liq = string.Empty;

            JsonArray jj = new JsonArray();
            ///
            JsonObject jso = new JsonObject();

            List<Documents_Trans> lstDocTxCheck = lstDocTx.Where(x => x._check).Where(y => y._conceptid.Equals("LIQUIDACIONES")).ToList();

            foreach (Documents_Trans dTx in lstDocTxCheck)
            {
                if (!string.IsNullOrEmpty(list_liq)) list_liq += ",";
                list_liq += dTx._docNo;
            }

            jso = new JsonObject();
            jso.Put("list_liq", list_liq);

            jj.Put(jso);

            var ret = jj.ToArray();
            return ret;
        }

        #endregion

        //protected void btCreateClear_Command(object sender, CommandEventArgs e)
        //{
        //    btCreateClear.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de ANULAR el pedido con número : -" + "" + "-, Perteneciente A : -" + "" + "- ?')");
        //    if (e.CommandName.Equals("enviar"))
        //    {
        //        //string url = _pageReportInvoice + "?NoLiquidation=" + 100 + "&NoInvoice=" + 668000012;
        //        string url = "../../Aquarella/Logistica/panelLiqReports.aspx" + "?noliq=" + 100;

        //        // Actualizar el numero de veces impresas de la factura
        //        //Facturacion.updateNoPrintsInvoice(_user._usv_co, noFactura);
        //        //btCreateClear.hre
        //       // Response.Redirect(url);

        //        //this.ClientScript.RegisterStartupScript(WIND, "StartupOpen", "<script language=" + "'" + "javascript" + "'" + "> window.open( " + "'" + url + "'" + ");" + "<" + "/script>"); 
        //        btCreateClear.Attributes.Add("onclick", "javascript:url();");
        //        //Response.Write("<script language='JavaScript'> varWinSettings= 'center:yes;resizable:no;dialogHeight:450px;dialogWidth:800px';var MyArgs = window.showModalDialog("+ url+ ",MyArgs,WinSettings);");
        //    }
        //}

       

      
    }
}
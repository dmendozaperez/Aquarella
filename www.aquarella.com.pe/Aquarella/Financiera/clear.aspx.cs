using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Util;
using Jayrock.Json;

namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class clear : System.Web.UI.Page
    {
        Users _user;

        List<Documents_Trans> _lstDocTx;

        string _nameList = "ListDocTx";
        public static string _nameSessionCustomer = "nameSessionCustomer", _nSNewOrdrLine = "_nSNewOrdrLine", _nSArtSiz = "_nSArtSiz",_currency = ConfigurationManager.AppSettings["kCurrency"];

        #region < Eventos del Load >

        /// <summary>
        /// Load de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
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
            string grandTotalstr= grandTotal.ToString(ConfigurationManager.AppSettings["kCurrency"]);
            

            string vhtml = "";
            List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();
            Int32 vbucle = 0;
            
            string vestilos="<style type='text/css'> <!-- .estilo1 { font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #003366; }" +
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

                valorstr =String.Format("{0:C}", valor);

                if (vbucle == 0)
                {
                    vhtml =vestilos + "<table cellpadding='0' cellspacing='0' border='1'>" +
                            "<thead><tr><th>Fecha</th><th>Concepto</th><th>Importe</th></tr>" +
                            "</thead><tr><td class='estilo1'>" + dTx._fechadoc + "</td><td class='estilo1'>" + dTx._date + "</td><td class='estilo1'>" + valorstr + "</td></tr>";
                    vbucle = 1;
                }
                else
                {
                    if (vbucle == lstDocTxCheck.Count )
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
                
                Session["_list_liq"] = string.Empty;
                Session[_nameList] = new List<Documents_Trans>();

               
                ((BoundField)(gvClear.Columns[3])).DataFormatString = "{0:" + ConfigurationManager.AppSettings["kCurrency"] + "}";

                if ((_user._usu_tip_id == "02"))
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
            DataSet dsCustomers = Coordinator.getCoordinators( _user._usv_area,"");
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
            setParamsDataSource( _user._usn_userid.ToString());
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


        #endregion
                
        /// <summary>
        /// Cambio en la seleccion de cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                setParamsDataSource( selCust);
                setCreditValueCust(_user._usv_co, Convert.ToDecimal(selCust));
                refreshGrid();
            }
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

        #region < Eventos sobre el grid >

        /// <summary>
        /// Fila del grid cargAQUARELLA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Salmon;

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
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Green;
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

        #endregion
        
        /// <summary>
        /// Totales del cruce de pagos y liquidaciones
        /// </summary>
        protected void totals()
        {
            try
            {
                _lstDocTx = getListFromSes();
                string imag = "<img src='../../Design/images/";
                decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value));//decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value * y._increase));

                decimal creditValue = decimal.Zero;

                decimal.TryParse(hdCreditValue.Value, out creditValue);

                gvClear.FooterRow.Cells[3].Text = grandTotal.ToString(ConfigurationManager.AppSettings["kCurrency"]);
                if (grandTotal + creditValue < 0)
                {
                    gvClear.FooterRow.Cells[3].ForeColor = System.Drawing.Color.Red;
                    imag += "b_inactive.png' />";
                    btCreateClear.Enabled = false;
                }
                else if (grandTotal + creditValue >= 0)
                {
                    gvClear.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;
                    imag += "b_active.png' />";
                    btCreateClear.Enabled = true;
                }
                else
                {
                    gvClear.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;
                    imag = string.Empty;
                    btCreateClear.Enabled = false;
                }

                gvClear.FooterRow.Cells[4].Text = imag;
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

        /// <summary>
        /// Realizar el cruce
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btCreateClear_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            string co = _user._usv_co;
            string listLiq = string.Empty;
            string listDoc = string.Empty;
            string clear = string.Empty;

            _lstDocTx = getListFromSes();

            List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();

            if (lstDocTxCheck.Count == 0)
            {
                msnMessage.LoadMessage("no ha seleccionado ningun items", UserControl.ucMessage.MessageType.Error);
                return;
            }

            DataTable dt_cruce = new DataTable();
            dt_cruce.Columns.Add("Tip", typeof(string));
            dt_cruce.Columns.Add("Monto", typeof(Double));
            Int32 fvalidapedido = 0;
            DataTable dtpagos = new DataTable();
            dtpagos.Columns.Add("Doc_Tra_Id", typeof(string));
            foreach (Documents_Trans dTx in lstDocTxCheck)
            {
                if (dTx._conceptid.Equals("PAGOS"))
                {
                    dt_cruce.Rows.Add('P', dTx._value);
                    if (!string.IsNullOrEmpty(listDoc)) listDoc += ",";
                    listDoc += dTx._docNo;
                    dtpagos.Rows.Add(dTx._docNo);
                }
                if (dTx._conceptid.Equals("LIQUIDACIONES"))
                {
                    dt_cruce.Rows.Add('L', dTx._value*-1);
                    fvalidapedido += 1;
                    if (fvalidapedido > 1)
                    {
                        msnMessage.LoadMessage("No se puede realizar cruce de pagos con 2 o más Pedidos" + 
                           " ,por favor seleccione solo 1 pedido.", UserControl.ucMessage.MessageType.Error);
                        return;
                    }
                        
                    if (!string.IsNullOrEmpty(listLiq)) listLiq += ",";
                    listLiq += dTx._docNo;
                }
            }

            if (listLiq.Length == 0)
            {
                msnMessage.LoadMessage("no ha seleccionado ningun pedido para cruzar el pago", UserControl.ucMessage.MessageType.Error);
                return;
            }

            //validar para que solo haya un limite de pago y no seleccione varios pagos cuando en un solo op ya pago todo el pedido
            DataRow[] fila_L = dt_cruce.Select("Tip='L'");
            Decimal monto_liq = Convert.ToDecimal(fila_L[0][1].ToString());

            DataRow[] fila_P = dt_cruce.Select("Tip='P'");
            Decimal _pagos = 0;
            Int32 _limite = 0;
            Int32 _cur = 0;
            for (Int32 i = 0; fila_P.Length > i; ++i)
            {
                _cur += 1;
                //si ya existe pagos seleccionados
                if (_cur>1)
                {
                    decimal _pago_cancelado = Convert.ToDecimal(fila_P[i][1].ToString());
                    if (_pago_cancelado > monto_liq)
                    {
                        msnMessage.LoadMessage("por favor solo seleccione el pago necesario para pagar su pedido", UserControl.ucMessage.MessageType.Error);
                        return;
                    }
                }
                _pagos += Convert.ToDecimal(fila_P[i][1].ToString());
                if (_pagos > monto_liq)
                {
                    if (_limite == 0)
                    {
                        _limite = 1;
                    }
                    else
                    {
                        msnMessage.LoadMessage("por favor solo seleccione el pago necesario para pagar su pedido", UserControl.ucMessage.MessageType.Error);
                        return;
                    }
                }
            }

            

            //*************************************

            // Cruce financiero de pagos y liquidaciones
            if (!string.IsNullOrEmpty(listLiq) && !string.IsNullOrEmpty(listDoc))
            {
                //
                try
                {

                    string vrefnc="";
                    string vreffec="";
                    string _validaref = string.Empty;

                    _validaref=Clear.setvalidaclear(listLiq, ref vrefnc, ref vreffec); 
                    if (!(string.IsNullOrEmpty(_validaref)))
                    {
                        msnMessage.LoadMessage("No se puede realizar cruce de pagos; porque la fecha de referencia de la nota de credito N " + vrefnc +
                            " pertenece a otro mes con fecha " + vreffec + "  ,por favor anule este pedido y vuelva a generar otro pedido", UserControl.ucMessage.MessageType.Error);
                        return;
                    }

                    string strIdPromotor = dwCustomers.SelectedValue.ToString();
                    clear = Clear.setPreClear(listLiq, dtpagos);
                                        
                    // en este caso vamos hacer un update a la nota de credito de financiera de document_trans
                    //Clear.sbupdateclearncredito(listLiq, clear);

                    //
                    if (!string.IsNullOrEmpty(clear))
                    {
                        string[] prems = clear.Split('|');
                        string strpremio = prems[1].ToString();
                        string strmensaje = "";
                        
                        msnMessage.LoadMessage("El cruce de información fue grabado correctamente, su pedido sera enviado  marcación y posterior facturación; número del cruce: " + prems[0].ToString() + strmensaje, UserControl.ucMessage.MessageType.Information);

                        if (strpremio != "N" && strpremio != "0")
                        {
                            string strregalo = prems[2].ToString();
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ConfirmacionPremio(" + strpremio + ", " + strIdPromotor + ",'" + strregalo + "'); ", true);
                        }
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
                _docNo = DataBinder.Eval(e.Row.DataItem, "dtv_transdoc_id").ToString(),
                _date = DataBinder.Eval(e.Row.DataItem, "cov_description").ToString(),
                _conceptid = DataBinder.Eval(e.Row.DataItem, "dtv_concept_id").ToString(),
                _value = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "val"))) * Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase")),
                _increase = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase")),
                _fechadoc=(DataBinder.Eval(e.Row.DataItem, "document_date_desc").ToString())
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

        /// <summary>
        /// Set fila chequeadd
        /// </summary>
        /// <param name="docTx"></param>
        /// <param name="action"></param>
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
            catch(Exception ex)
            { }
        }

        #region < Data Sources >

        /// <summary>
        /// Evento sobre el datasource despues de realizar la consulta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        #endregion
                
        #region < Ajax For Pays Online >

        [WebMethod()]
        public static Object ajaxGetLiqCheck()
        {
            /// Deido a que la funcion es estatica se deben crear referencias para podre hacer llamados a algunos metodos
            System.Web.SessionState.HttpSessionState sessions = HttpContext.Current.Session;
            
            List<Documents_Trans> lstDocTx = (List<Documents_Trans>)sessions["ListDocTx"];

            string list_liq = string.Empty;

            JsonArray jj = new JsonArray();
            ///
            JsonObject jso = new JsonObject();

            List<Documents_Trans> lstDocTxCheck = lstDocTx.Where(x => x._check).Where(y => y._conceptid.Equals("LIQUIDACIONES")).ToList();

            foreach(Documents_Trans dTx in lstDocTxCheck)
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


        [WebMethod()]
        public static string GenerarLiquidacionPremio(string strPremId, string strBasId)
        {
            string IdLiquidacion = "";

            IdLiquidacion = Clear.setCrearLiquidacionPremio(Convert.ToInt32(strBasId), Convert.ToInt32(strPremId));

            return IdLiquidacion;
        }
    }
}
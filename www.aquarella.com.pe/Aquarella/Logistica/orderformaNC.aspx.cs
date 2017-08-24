using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using System.Configuration;
using System.Web.Services;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class orderformaNC : System.Web.UI.Page
    {
        string _nameList = "ListDocTx";
        public static string _valida = "monto", _nameSessionCustomer = "nameSessionCustomer", _idliquidacion = "_idliquidacion";
        List<Documents_Trans> _lstDocTx;   
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (this.Session[Constants.NameSessionUser] == null)
                Utilities.logout(this.Page.Session, this.Page.Response);
            if (this.IsAsync)
                return;
            if (!this.IsPostBack)
            {
                Coordinator cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
                string vidliq = "";

                  
                if (!(Object.ReferenceEquals((HttpContext.Current.Session[_idliquidacion]),null)))
                {
                     vidliq = HttpContext.Current.Session[_idliquidacion].ToString();
                }
                    

                ////nota de credito
                setParamsDataSource(cust._idCust.ToString(), vidliq);
                ////
                ////
                refreshGrid();
               // CheckBox vcheck = (CheckBox)gvnc.Rows[0].FindControl("chkDocument");
               // vcheck.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de restaurar la liquidacion Vencida con N° : -" + "" + "- ?')");
                ////*************************
            }
        }

        protected void totals(string _nrodocumento)
        {
            try
            {
                _lstDocTx = getListFromSes();

                decimal grandTotal = 0;

               // string imag = "<img src='../../Design/images/";
                grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value));//decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value * y._increase));

                //decimal total_disponible = 0;
                decimal totalpedido =(Decimal)Session["_valor_subtotal"];
                decimal total_monto_seleccion = 0;

                List<Documents_Trans> lstDocTxCheck = _lstDocTx.Where(x => x._check).ToList();



                if (grandTotal > totalpedido)
                {
                    if (lstDocTxCheck.Count != 1)
                    {
                        for (Int32 i = 0; i < lstDocTxCheck.Count; ++i)
                        {
                            total_monto_seleccion = (Decimal)lstDocTxCheck[i]._value;

                            if (total_monto_seleccion > totalpedido)
                            {

                                for (Int32 a = 0; a < _lstDocTx.Count; ++a)
                                {
                                    CheckBox vcheck = (CheckBox)gvnc.Rows[a].FindControl("chkDocument");
                                    //vcheck.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de restaurar la liquidacion Vencida con N° : -" + _nrodocumento + "- ?')");

                                    if (vcheck.ToolTip == _nrodocumento)
                                    {
                                        
                                        //msnMessage.LoadMessage("Error en la actualizacion de precios; intente de nuevo.", UserControl.ucMessage.MessageType.Error);
                                        vcheck.Checked = false;
                                        string docTxCheck = ((CheckBox)vcheck).ToolTip;
                                        setNoDocTx(docTxCheck, ((CheckBox)vcheck).Checked);
                                        _lstDocTx = getListFromSes();
                                        //string imag = "<img src='../../Design/images/";
                                        grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value));//decimal grandTotal = _lstDocTx.Where(x => x._check).Sum(y => (y._value * y._increase));

                                        //vcheck.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de restaurar la liquidacion Vencida con N° : -" + _nrodocumento + "- ?')");
                                        string VMensaje = "alert('" + "No se puede seleccionar este registro, porque el total de saldos es mayor al total pedido" + "');";
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", VMensaje, true);
                                        break;
                                    }


                                }

                                
                            }

                        }
                    }

                    //for (Int32 i = 0; i < _lstDocTx.Count; ++i)
                    //{
                    //    if (_lstDocTx[i]._check)
                    //    {
                    //        total_monto_seleccion =(Decimal) _lstDocTx[i]._value;

                    //        if (total_monto_seleccion >= totalpedido)
                    //        {
                    //            DataTable dt_tabla = (DataTable)gvnc.DataSource;

                    //            break;

                    //        }

                    //       // total_disponible += (Decimal)_lstDocTx[i]._value;
                    //       // decimal valor=0;

                    //    }
                    //}
                }
                decimal creditValue = decimal.Zero;

                decimal.TryParse(hdCreditValue.Value, out creditValue);

                gvnc.FooterRow.Cells[2].Text = grandTotal.ToString(ConfigurationManager.AppSettings["kCurrency"]);
                HttpContext.Current.Session[_valida] = grandTotal;
                if (grandTotal + creditValue < 0)
                {
                    // gvnc.FooterRow.Cells[3].ForeColor = System.Drawing.Color.Red;
                    // imag += "b_inactive.png' />";
                    //btCreateClear.Enabled = false;
                }
                else if (grandTotal + creditValue >= 0)
                {
                    gvnc.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
                    //imag += "b_active.png' />";
                    //btCreateClear.Enabled = true;
                }
                else
                {
                    gvnc.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
                    //imag = string.Empty;
                    //btCreateClear.Enabled = false;
                }

                // gvClear.FooterRow.Cells[4].Text = imag;
            }
            catch { }
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
        protected List<Documents_Trans> getListFromSes()
        {
            if (Session[_nameList] == null)
                Session[_nameList] = new List<Documents_Trans>();
            return (List<Documents_Trans>)Session[_nameList];
        }

        protected void gvnc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //decimal increase = 0;
                decimal value = 0;

                if (decimal.TryParse(DataBinder.Eval(e.Row.DataItem, "importe").ToString(), out value))
                {
                    //
                    //if (increase < 0 || value < 0)
                    //{
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Salmon;

                    if (!DataBinder.Eval(e.Row.DataItem, "importe").ToString().Equals("LIQUIDACIONES"))
                    {
                        //
                        bool temp;
                        CheckBox chk = (CheckBox)e.Row.FindControl("chkDocument");
                        bool.TryParse(DataBinder.Eval(e.Row.DataItem, "active").ToString(), out temp);
                        chk.Enabled = temp;
                        bool.TryParse(DataBinder.Eval(e.Row.DataItem, "checks").ToString(), out temp);
                        chk.Checked = temp;
                        if (temp)
                            setNoDocTx(DataBinder.Eval(e.Row.DataItem, "ncredito").ToString(), temp);
                    }
                    //}
                    else
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Green;
                }

                setListDocTx(e);
            }
        }
        protected void setListDocTx(GridViewRowEventArgs e)
        {
            _lstDocTx = getListFromSes();

            _lstDocTx.Add(new Documents_Trans
            {
                _check = Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "checks").ToString()),
                _docNo = DataBinder.Eval(e.Row.DataItem, "ncredito").ToString(),
                _numeroid = DataBinder.Eval(e.Row.DataItem, "rhv_return_no").ToString(),
                _value = (Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "importe"))),
                //_date="NOTA DE CREDITO",
                //_fechadoc = (Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "dtd_document_date")).ToShortDateString()),
                //* Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase")),
                // _increase = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "von_increase"))
            });

            Session[_nameList] = _lstDocTx;
        }

        protected void chkDocument_CheckedChanged(object sender, EventArgs e)
        {
            string docTxCheck = ((CheckBox)sender).ToolTip;
            setNoDocTx(docTxCheck, ((CheckBox)sender).Checked);
            totals(docTxCheck);
        }

        protected void gvnc_DataBound(object sender, EventArgs e)
        {
            totals("");
        }
        protected void refreshGrid()
        {
            gvnc.DataSourceID = odsnc.ID;
            gvnc.DataBind();
        }
        protected void setParamsDataSource(string bas_id, string idliq)
        {
            odsnc.SelectParameters[0].DefaultValue = bas_id;
            odsnc.SelectParameters[1].DefaultValue = idliq;
        }
        protected void odsnc_Selected(object sender, System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs e)
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
        [WebMethod()]
        public static string ajaxupdatefunction()
        {
            //List<Documents_Trans> vmonto = (List<Documents_Trans>)(((object)HttpContext.Current.Session[_valida]) != null ? (object)HttpContext.Current.Session[_valida] : new List<Documents_Trans>());
            return HttpContext.Current.Session[_valida].ToString();
        }
        [WebMethod()]
        public static string verificanc()
        {
            //return "0";
           Coordinator  cust = (Coordinator)HttpContext.Current.Session[_nameSessionCustomer];
           string vidliq = "";
           if (!(Object.ReferenceEquals((HttpContext.Current.Session[_idliquidacion]), null)))
           {
               vidliq = HttpContext.Current.Session[_idliquidacion].ToString();
           }
           

           DataTable dt = Documents_Trans.get_PagoNcredito(cust._idCust.ToString(), vidliq).Tables[0];

           if (dt.Rows.Count == 0)
           {
               return "0";
           }
           else
           {
               return "1";
           }
        }
    }
}
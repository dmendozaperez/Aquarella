using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;

namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class ClearCN : System.Web.UI.Page
    {
        Users _user;

        List<Documents_Trans> _lstDocTx;

        string _nameList = "ListDocTx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
            if (!IsPostBack)
            {
                Session["_list_fac"] = string.Empty;
                Session[_nameList] = new List<Documents_Trans>();

                ((BoundField)(gvClear.Columns[3])).DataFormatString = "{0:" + ConfigurationManager.AppSettings["kCurrency"] + "}";
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

                buscar();
            }

            
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            buscar();
        }
        protected List<Documents_Trans> getListFromSes()
        {
            if (Session[_nameList] == null)
                Session[_nameList] = new List<Documents_Trans>();
            return (List<Documents_Trans>)Session[_nameList];
        }
        protected void setParamsDataSource(DateTime fechaini,DateTime fechafin)
        {
            odsClear.SelectParameters[0].DefaultValue = fechaini.ToString("dd/MM/yyyy");
            odsClear.SelectParameters[1].DefaultValue = fechafin.ToString("dd/MM/yyyy");
        }
        private void buscar()
        {
            msnMessage.HideMessage();
            // Nuevo cliente seleccionado            
            DateTime fecha_ini =Convert.ToDateTime(txtDateStart.Text);
            DateTime fecha_fin = Convert.ToDateTime(txtDateEnd.Text);

            Session[_nameList] = new List<Documents_Trans>();
            _lstDocTx = getListFromSes();

            /// Verificar que sea una selección valida
            
                setParamsDataSource(fecha_ini,fecha_fin);
              
                refreshGrid();
            
        }
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

                decimal creditValue = decimal.Zero;

                decimal.TryParse(hdCreditValue.Value, out creditValue);

                gvClear.FooterRow.Cells[4].Text = grandTotal.ToString(ConfigurationManager.AppSettings["kCurrency"]);
                if (grandTotal + creditValue < 0)
                {
                    gvClear.FooterRow.Cells[4].ForeColor = System.Drawing.Color.Red;
                    imag += "b_inactive.png' />";
                    btCreateClear.Enabled = false;
                }
                else if (grandTotal + creditValue >= 0)
                {
                    gvClear.FooterRow.Cells[4].ForeColor = System.Drawing.Color.White;
                    imag += "b_active.png' />";
                    btCreateClear.Enabled = true;
                }
                else
                {
                    gvClear.FooterRow.Cells[4].ForeColor = System.Drawing.Color.White;
                    imag = string.Empty;
                    btCreateClear.Enabled = false;
                }

                gvClear.FooterRow.Cells[5].Text = imag;
            }
            catch { }
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
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Salmon;

                        if (!DataBinder.Eval(e.Row.DataItem, "dtv_concept_id").ToString().Equals("FACTURACION"))
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
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Green;
                }

                setListDocTx(e);
            }
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
                _fechadoc = (DataBinder.Eval(e.Row.DataItem, "document_date_desc").ToString())
            });

            Session[_nameList] = _lstDocTx;
        }
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
                if (dTx._conceptid.Equals("FACTURACION"))
                {
                    dt_cruce.Rows.Add('L', dTx._value * -1);
                    fvalidapedido += 1;
                    //if (fvalidapedido > 1)
                    //{
                    //    msnMessage.LoadMessage("No se puede realizar cruce de pagos con 2 o más Pedidos" +
                    //       " ,por favor seleccione solo 1 pedido.", UserControl.ucMessage.MessageType.Error);
                    //    return;
                    //}

                    if (!string.IsNullOrEmpty(listLiq)) listLiq += ",";
                    listLiq += dTx._docNo;

                    dtpagos.Rows.Add(dTx._docNo);
                }
            }

            if (listLiq.Length == 0)
            {
                msnMessage.LoadMessage("no ha seleccionado ninguna factura para cruzar el pago", UserControl.ucMessage.MessageType.Error);
                return;
            }

            //validar para que solo haya un limite de pago y no seleccione varios pagos cuando en un solo op ya pago todo el pedido
            DataRow[] fila_L = dt_cruce.Select("Tip='L'");
            Decimal monto_liq = Convert.ToDecimal(fila_L[0][1].ToString());

            DataRow[] fila_P = dt_cruce.Select("Tip='P'");
            Decimal _pagos = 0;
            Int32 _limite = 0;
            Int32 _cur = 0;
            //for (Int32 i = 0; fila_P.Length > i; ++i)
            //{
            //    _cur += 1;
            //    //si ya existe pagos seleccionados
            //    if (_cur > 1)
            //    {
            //        decimal _pago_cancelado = Convert.ToDecimal(fila_P[i][1].ToString());
            //        if (_pago_cancelado > monto_liq)
            //        {
            //            msnMessage.LoadMessage("por favor solo seleccione el pago necesario para pagar su factura", UserControl.ucMessage.MessageType.Error);
            //            return;
            //        }
            //    }
                //_pagos += Convert.ToDecimal(fila_P[i][1].ToString());
                //if (_pagos > monto_liq)
                //{
                //    if (_limite == 0)
                //    {
                //        _limite = 1;
                //    }
                //    else
                //    {
                //        msnMessage.LoadMessage("por favor solo seleccione el pago necesario para pagar su factura", UserControl.ucMessage.MessageType.Error);
                //        return;
                //    }
                //}
            //}



            //*************************************

            // Cruce financiero de pagos y liquidaciones
            if (!string.IsNullOrEmpty(listLiq) && !string.IsNullOrEmpty(listDoc))
            {
                //
                try
                {

                    string vrefnc = "";
                    string vreffec = "";
                   


                    clear = Clear.setPreClear(_user._bas_id, dtpagos);

                    // en este caso vamos hacer un update a la nota de credito de financiera de document_trans
                    //Clear.sbupdateclearncredito(listLiq, clear);

                    //
                    if (!string.IsNullOrEmpty(clear))
                    {
                        msnMessage.LoadMessage("El cruce de información fue grabado correctamente; número del cruce: " + clear, UserControl.ucMessage.MessageType.Information);

                       
                        
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

        protected void chkDocument_CheckedChanged(object sender, EventArgs e)
        {
            string docTxCheck = ((CheckBox)sender).ToolTip;
            setNoDocTx(docTxCheck, ((CheckBox)sender).Checked);
            totals();
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
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Interfaces;
using www.aquarella.com.pe.bll.Util;

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class reportventaformaCN : System.Web.UI.Page
    {
        Users _user;
        string _nameSessDatavenazonaconsulta = "session_ventazona_consulta";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                cargarforma();
                DateTime fechatemp;
                fechatemp = DateTime.Today;
                txtDateStart.Text = fechatemp.Date.ToString("dd/MM/yyyy");
                txtDateEnd.Text = fechatemp.Date.ToString("dd/MM/yyyy");

                _consultar();

            }
        }
        private void _consultar()
        {
            msnMessage.HideMessage();
            try
            {
                Boolean resu = chkresumido.Checked;
                DateTime _fecha_ini = Convert.ToDateTime(txtDateStart.Text);
                DateTime _fecha_fin = Convert.ToDateTime(txtDateEnd.Text);
                string _concepto = dwconcepto.SelectedValue;
                DataTable dt = invoice.get_ventaformacn(_fecha_ini, _fecha_fin, _concepto, resu);
                Session[_nameSessDatavenazonaconsulta] = dt;
                gvReturns.DataSource = dt;
                gvReturns.DataBind();
            }
            catch (Exception ex)
            {
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        private void cargarforma()
        {
            dwconcepto.DataSource = invoice.getconeptopago_ce();
            dwconcepto.DataTextField = "con_descripcion";
            dwconcepto.DataValueField = "con_Id";
            dwconcepto.DataBind();
        }
        protected void btConsult_Click(object sender, EventArgs e)
        {
            _consultar();
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.DataSource = (DataTable)Session[_nameSessDatavenazonaconsulta];
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "ventaforma_cn";

            Decimal[] columna = { 1 };
            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns, false, columna);
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable)Session[_nameSessDatavenazonaconsulta];
            gvReturns.DataBind();
        }

        protected void gvReturns_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string _estado = DataBinder.Eval(e.Row.DataItem, "cta").ToString();

                if (_estado == "TOTAL")
                {
                    e.Row.BackColor = System.Drawing.Color.Khaki;
                    e.Row.Style.Add(HtmlTextWriterStyle.FontWeight, "Bold");
                }


            }
        }


        protected void ibExportDoc_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dt = null;
            try
            {
                DateTime fechaini =Convert.ToDateTime(txtDateStart.Text);
                DateTime fechafin = Convert.ToDateTime(txtDateEnd.Text);
                dt=Documents_Trans.get_Docn_TransAdonis(fechaini, fechafin, "-1", "2").Tables[0];
                if (dt.Rows.Count>0)
                {
                    DateTime _fecStart = Convert.ToDateTime((txtDateStart.Text.Equals(string.Empty)) ? "01/01/1900" : txtDateStart.Text);
                    DateTime _fecEnd = Convert.ToDateTime((txtDateEnd.Text.Equals(string.Empty)) ? "01/01/1900" : txtDateEnd.Text);

                    DataSet _interf = Adonis.Get_Comercial_Interface(_fecStart, _fecEnd);
                    DataTable dtinter = _interf.Tables[0];

                    DataRow[] foundRows = dtinter.Select("", "");

                    System.Text.StringBuilder str = new System.Text.StringBuilder();

                    for (int i = 0; i <= foundRows.GetUpperBound(0); i++)
                    {
                        str.Append(foundRows[i][0].ToString() + "\r\n");
                    }

                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "text/plain";
                    Response.AddHeader("Content-Disposition", "attachment;filename=InterComerAdonis.txt");
                    Response.Charset = "UTF-8";
                    Response.ContentEncoding = System.Text.Encoding.Default;

                    System.IO.StringWriter tw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

                    Response.Write(str.ToString());
                    Response.End();
                }
                else
                {
                    msnMessage.LoadMessage("No hay datos para exportar", UserControl.ucMessage.MessageType.Error);
                }

            }
            catch (Exception exc)
            {
                msnMessage.LoadMessage(exc.Message, UserControl.ucMessage.MessageType.Error);
                dt = null;
            }
        }

        protected void chkresumido_CheckedChanged(object sender, EventArgs e)
        {
            _consultar();
        }
    }
}
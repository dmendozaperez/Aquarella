using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
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
                DateTime _fecha_ini = Convert.ToDateTime(txtDateStart.Text);
                DateTime _fecha_fin = Convert.ToDateTime(txtDateEnd.Text);
                string _concepto = dwconcepto.SelectedValue;
                DataTable dt = invoice.get_ventaformacn(_fecha_ini, _fecha_fin, _concepto);
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
    }
}
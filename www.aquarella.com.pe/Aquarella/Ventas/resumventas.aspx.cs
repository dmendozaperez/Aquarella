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
    public partial class resumventas : System.Web.UI.Page
    {
        Users _user;
        string _nameSessDataresvena = "session_resventa";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];
            if (!IsPostBack)
            {
                sbinicio();
            }

        }
        private void sbconsultar()
        {
            msnMessage.HideMessage();
            try
            {
                Int32 vanio = Convert.ToInt32(dwanio.SelectedValue);
                
                DataTable dt = invoice.getresventa(vanio);
                Session[_nameSessDataresvena] = dt;
                gvReturns.DataSource = dt;
                gvReturns.DataBind();
            }
            catch (Exception ex)
            {
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);
            }
        }
        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }
        protected void sbinicio()
        {
            cargaranio();
            dwanio.SelectedValue = DateTime.Today.Year.ToString();
            sbconsultar();
        }
        protected void cargaranio()
        {
            // Mostrar Panel de Seleccion de Coordinador


            dwanio.Focus();
            dwanio.DataSource = Area.getAllaño();
            dwanio.DataBind();
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable)Session[_nameSessDataresvena];
            gvReturns.DataBind();
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.DataSource = (DataTable)Session[_nameSessDataresvena];
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "ventaResum";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }
    }
}
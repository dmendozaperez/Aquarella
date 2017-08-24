using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.UserControl;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class LiquidacionVsDespacho : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData",_nameSessionDataFiltered = "InfoLiqSeparatedFilter";
        DataSet _dsResult;
        SortDirection _sortDir;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {              
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");
                Session[_nameSessionData] = null;
                if ((_user._usu_tip_id == "02"))
                {
                    Utilities.logout(Page.Session, Page.Response);
                }
                else
                {
                    formForEmployee();
                }                                                
            }
        }
        protected void formForEmployee()
        {           
                  
                getSource_buscar();
         
         
        }
        protected DataTable getSource()
        {
            // Filtered source
            if (!string.IsNullOrEmpty(GridViewSourceType) && GridViewSourceType.Equals("filtered"))
                return (DataTable)Session[_nameSessionDataFiltered];
            else//originalsource
                return (DataTable)Session[_nameSessionData];
        }
        protected void getSource_buscar()
        {
            DateTime _fecha_ini=Convert.ToDateTime(txtDateStart.Text);
            DateTime _fecha_fin=Convert.ToDateTime(txtDateEnd.Text);
            _dsResult = Picking.liquidacion_vs_despacho(_fecha_ini,_fecha_fin);
            Session[_nameSessionData] = _dsResult.Tables[0];
            GridViewSourceType = "originalsource";
            gvReturns.DataSource = _dsResult;
            refreshGridView();
        }
        private void refreshGridView()
        {
           gvReturns.DataBind();
        }
        public string GridViewSourceType
        {
            get
            {
                if (ViewState["GridViewSourceType"] == null)
                    ViewState["GridViewSourceType"] = string.Empty;
                return (string)ViewState["GridViewSourceType"];
            }
            set { ViewState["GridViewSourceType"] = value; }
        }
        protected void btConsult_Click(object sender, EventArgs e)
        {
            getSource_buscar();
            sbfiltrar();
        }
         
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {

            //gvSepLiq.AllowPaging = false;
            //GridViewExportUtil.removeFormats(ref gvSepLiq);
            //gridStatus();
            ////  pass the grid that for exporting ...
            //GridViewExportUtil.Export("LiquidacionesSeparAQUARELLAs.xls", gvSepLiq);

          //  gvReturns.DataSource = Session[_nameSessionData];
          //  gvReturns.DataBind();
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gridStatus();
            //  pass the grid that for exporting ...
            GridViewExportUtil.Export("liquidespacho.xls", gvReturns);
        }
        protected void gridStatus()
        {
            pageAndSort(gvReturns, GridViewSortExpresion, getSource(), gvReturns.PageIndex);
        }
       
        public string GridViewSortExpresion
        {
            get
            {
                if (ViewState["sortExpresion"] == null)
                    ViewState["sortExpresion"] = string.Empty;
                return (string)ViewState["sortExpresion"];
            }
            set { ViewState["sortExpresion"] = value; }
        }

        protected void pageAndSort(GridView gv, string sortField, DataTable dt, int iPage)
        {
            _sortDir = GridViewSortDirection;
            Utilities.pageAndSort(gv, ref _sortDir, sortField, dt, iPage);
            GridViewSortDirection = _sortDir;
        }
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set { ViewState["sortDirection"] = value; }
        }
       

        protected void gvReturns_DataBound(object sender, EventArgs e)
        {
            calculateTotals(gvReturns, getSource());
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pageAndSort(gvReturns, GridViewSortExpresion, getSource(), e.NewPageIndex);
        }

        protected void gvReturns_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvReturns_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpresion = e.SortExpression;
            pageAndSort(gvReturns, GridViewSortExpresion, getSource(), gvReturns.PageIndex);
        }

        protected void chksd_CheckedChanged(object sender, EventArgs e)
        {
            sbfiltrar();
            //if (chksd.Checked)
            //{
            //    string filterValue = "0";
            //    DataTable dt = Utilities.getFilterObject((DataTable)Session[_nameSessionData], "SALDO", filterValue);
            //    GridViewSourceType = "filtered";
            //    Session[_nameSessionDataFiltered] = dt;
            //    gvReturns.DataSource = dt;
            //    refreshGridView();
            //}
            //else
            //{
            //    string filterValue = "";
            //    DataTable dt = Utilities.getFilterObject((DataTable)Session[_nameSessionData], "SALDO", filterValue);
            //    GridViewSourceType = "filtered";
            //    Session[_nameSessionDataFiltered] = dt;
            //    gvReturns.DataSource = dt;
            //    refreshGridView();
            //}
        }
        protected void calculateTotals(GridView gv, DataTable dt)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    var t = (from x in dt.AsEnumerable()
                             group x by x.Table into y
                             select new
                             {
                                 x1 = y.Count(),
                                 x2 = y.Sum(x => x.Field<decimal>("PEDORIGINAL")),
                                 x3 = y.Sum(x => x.Field<decimal>("PEDI_DESPACHADO")),
                                 x4 = y.Sum(x => x.Field<decimal>("SALDO"))
                             }).FirstOrDefault();

                    gv.FooterRow.Cells[4].Text = "TOTALES:";
                    gv.FooterRow.Cells[5].Text = t.x2.ToString("N0");
                    gv.FooterRow.Cells[6].Text = t.x3.ToString("N0");
                    gv.FooterRow.Cells[7].Text = t.x4.ToString("N0");

                    //lblQtysLiq.Text = t.x2.ToString("N0");
                    //lblQtysOrder.Text = t.x3.ToString("N0");
                    //lblNumLiq.Text = t.x1.ToString("N0");
                    //lblNumLiq.Text = t.x1.ToString("N0");
                    //lblLiqValue.Text = t.x4.ToString("C0");
                }
            }
            catch { }
        }
        private void sbfiltrar()
        {
            string _str = txtFilter.Text;
            if (chksd.Checked)
            {
                decimal filterValue = 0;
                DataTable dt = Utilities.getFilterObject((DataTable)Session[_nameSessionData], "SALDO", "LIQ", "Ven_Id", filterValue, _str,_str);
                GridViewSourceType = "filtered";
                Session[_nameSessionDataFiltered] = dt;
                gvReturns.DataSource = dt;
                refreshGridView();
            }
            else
            {
                Decimal filterValue = -1;
                DataTable dt = Utilities.getFilterObject((DataTable)Session[_nameSessionData], "SALDO", "LIQ", "Ven_Id", filterValue, _str,_str);
                GridViewSourceType = "filtered";
                Session[_nameSessionDataFiltered] = dt;
                gvReturns.DataSource = dt;
                refreshGridView();
            }
        }

        protected void btFilter_Click(object sender, EventArgs e)
        {
            sbfiltrar();
        }
      

       

       

      
      

        

        

    }
}
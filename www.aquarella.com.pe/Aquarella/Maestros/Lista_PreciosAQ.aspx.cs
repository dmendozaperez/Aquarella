using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
namespace www.aquarella.com.pe.Aquarella.Maestros
{
    public partial class Lista_PreciosAQ : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {              
                 sbconsultar();
            }
        }
        protected void sbconsultar()
        {
            try
            {
                //Session[_nameSessionData]= Lider.Lider.fget_afiliados(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text)).Tables[0];
                gvReturns.DataSource = Article.get_listaprecios().Tables[0];
                gvReturns.DataBind();
                Session[_nameSessionData] = gvReturns.DataSource;
                MergeRows(gvReturns, 1);
            }
            catch
            {
                return;
            }
        }
        private void MergeRows(GridView gv, int rowPivotLevel)
        {
            for (int rowIndex = gv.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gv.Rows[rowIndex];
                GridViewRow prevRow = gv.Rows[rowIndex + 1];
                for (int colIndex = 0; colIndex < rowPivotLevel; colIndex++)
                {
                    if (row.Cells[colIndex].Text == prevRow.Cells[colIndex].Text)
                    {
                        row.Cells[colIndex].RowSpan = (prevRow.Cells[colIndex].RowSpan < 2) ? 2 : prevRow.Cells[colIndex].RowSpan + 1;
                        prevRow.Cells[colIndex].Visible = false;
                    }
                }
            }
        }
        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable)Session[_nameSessionData];

            gvReturns.DataBind();

            MergeRows(gvReturns, 1);
        }

       
        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.DataSource = (DataTable)Session[_nameSessionData];
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "ListaPrecios";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }
    }
}
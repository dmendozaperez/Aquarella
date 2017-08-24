using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
//using www.aquarella.com.pe.Aquarella.Admonred.
//using Bata.Aquarella.Pe.Aquarella.Admonred.Lider;
using System.Data;
using System.Drawing;
namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class AfiliadosClientes : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

               // sbconsultar();
            }
        }
        protected void sbconsultar()
        {         
            try
            {
                //Session[_nameSessionData]= Lider.Lider.fget_afiliados(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text)).Tables[0];
                gvReturns.DataSource = Lider.Lider.fget_afiliados(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text),chksf.Checked).Tables[0];// (DataTable)Session[_nameSessionData];
                gvReturns.DataBind();
                Session[_nameSessionData] = gvReturns.DataSource;
                MergeRows(gvReturns, 4);
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

        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }
        #region < Exportar Excel>

        /// <summary>
        /// Exportar a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.DataSource = (DataTable)Session[_nameSessionData];
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "ClienteAfiliados";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }

        #endregion

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            gvReturns.DataSource = (DataTable) Session[_nameSessionData];

            gvReturns.DataBind();

            MergeRows(gvReturns, 4);
        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             if (e.Row.RowType == DataControlRowType.DataRow)
           {
               CheckBox vestado =(CheckBox)e.Row.FindControl("cbxStatus");

               Boolean vcheck = vestado.Checked;

               if (vcheck)
               {
                   e.Row.BackColor = Color.Khaki;
                 //e.Row.BackColor = Color.FromName("#c6efce");
                 //e.Row.Cells[2].BackColor = Color.FromName("#c6efce");
              }
             
          }
        }

        protected void chksf_CheckedChanged(object sender, EventArgs e)
        {
            sbconsultar();
        }
    }
}
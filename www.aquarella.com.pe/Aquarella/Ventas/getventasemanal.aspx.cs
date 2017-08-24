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
using www.aquarella.com.pe.bll.Ventas;
//using Bata.Aquarella.BLL.Ventas;
using System.Data;
//using Bata.Aquarella.Bll;
namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class getventasemanal : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        string _Separator = ".";
        protected void Page_Load(object sender, EventArgs e)
        {

            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                DateTime vprimerdia = DateTime.Now;
                DateTime vpri = DateTime.MinValue;
                vpri=vprimerdia.AddDays(1-Convert.ToDouble(vprimerdia.DayOfWeek));
                DateTime Vult = vpri.AddDays(6);

                txtDateStart.Text = vpri.Date.ToString("dd/MM/yyyy");
                //txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = Vult.Date.ToString("dd/MM/yyyy");

                sbconsultar();
            }
        }
        protected void sbconsultar()
        {
            try
            {
                //Session[_nameSessionData]= Lider.Lider.fget_afiliados(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text)).Tables[0];
                msnMessage.Visible = false;
                DataSet dsreturn = Facturacion.Get_VentaSemanal(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text));
                Pivot pvt = new Pivot(dsreturn.Tables[0]);
                //string[] fila = {  };
                string[] fila = { "AQ" };
                //string[] col = { "Ano","Mes","Semana", "Dia" };
                string[] col = { "anio", "Mes","dia" };
               
                gvReturns.DataSource = pvt.PivotData("Total", AggregateFunction.Sum, fila, col);
               


                gvReturns.DataBind();
               // MergeRows(gvReturns, 2);

                Session[_nameSessionData] = dsreturn.Tables[0];

                
                //gvReturns.DataSource = Documents_Trans.get_reportsemana(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text)).Tables[0];// (DataTable)Session[_nameSessionData];
                //gvReturns.DataBind();
                //Session[_nameSessionData] = gvReturns.DataSource;
                //MergeRows(gvReturns, 4);
            }
            catch (Exception ex)
            {
                msnMessage.Visible = true;
                msnMessage.LoadMessage(ex.Message, UserControl.ucMessage.MessageType.Error);

            }
        }
        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            sbconsultar();
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {

        }

        #region <METODO DE FORMATO PIVOT>

        protected void gvReturns_RowCreated(object sender, GridViewRowEventArgs e)
        {
            
            
            if (e.Row.RowType == DataControlRowType.Header)
                MergeHeader((GridView)sender, e.Row, 3);

            //gvReturns.FooterRow.Cells[0].HorizontalAlign = HorizontalAlign.Center;
          
        }
        private void MergeHeader(GridView gv, GridViewRow row, int PivotLevel)
        {
            for (int iCount = 1; iCount <= PivotLevel; iCount++)
            {
                GridViewRow oGridViewRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                var Header = (row.Cells.Cast<TableCell>()
                    .Select(x => GetHeaderText(x.Text, iCount, PivotLevel)))
                    .GroupBy(x => x);

                foreach (var v in Header)
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    cell.Text = v.Key.Substring(v.Key.LastIndexOf(_Separator) + 1);
                    cell.ColumnSpan = v.Count();
                    oGridViewRow.Cells.Add(cell);
                }
                gv.Controls[0].Controls.AddAt(row.RowIndex, oGridViewRow);
            }
            row.Visible = false;
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
        private string GetHeaderText(string s, int i, int PivotLevel)
        {
            if (!s.Contains(_Separator) && i != PivotLevel)
                return string.Empty;
            else
            {
                int Index = NthIndexOf(s, _Separator, i);
                if (Index == -1)
                    return s;
                return s.Substring(0, Index);
            }
        }
        private int NthIndexOf(string str, string SubString, int n)
        {
            int x = -1;
            for (int i = 0; i < n; i++)
            {
                x = str.IndexOf(SubString, x + 1);
                if (x == -1)
                    return x;
            }
            return x;
        }

        #endregion
    }
}
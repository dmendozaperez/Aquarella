using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Ventas;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.Pe.BLL.Ventas;
using System.Collections.Generic;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.BLL;
using System.Data;
using www.aquarella.com.pe.bll.Ventas;
using System.IO;

using System.Text;

namespace www.aquarella.com.pe.Aquarella.Admonred

{
    public partial class prospectacionLider : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        string _Separator = ".";
              
        private string _nombreSession = "ValoresventaxLider";
         

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

                formUsuario();

            }
        }

        #region <METODOS DEL CRYSTAL>

        #endregion

        private void formUsuario()
        {
            try
            {
                this.msnMessage.Visible = false;
                sbconsulta();
               
            }
            catch (Exception ex)
            {
                this.msnMessage.LoadMessage("Error de Consulta: " + ex.Message, ucMessage.MessageType.Error);
                return;
            }
        }
      
        
        protected void btConsult_Click(object sender, EventArgs e)
        {

            formUsuario();
        }

        private void sbconsulta()
        {
            DataSet dsreturn = www.aquarella.com.pe.Aquarella.Lider.Lider.getprospectacionXLider(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text));
            DataTable dt1 = new DataTable("tabla1");

            if (dsreturn.Tables.Count > 0)
            {
                dt1 = dsreturn.Tables[0];
            }
            else {
                DataTable dt2 = new DataTable();
                dsreturn.Tables.Add(dt2);
            }
            
            gvReturns.DataSource = dt1;
            gvReturns.DataBind();

            if (dsreturn.Tables.Count > 0)
            {
                MergeRows(gvReturns, 2);
            }

            Session[_nameSessionData] = dsreturn.Tables[0];
        }

        #region <METODO DE FORMATO PIVOT>

        protected void grdPivot2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            
            if ((e.Row.RowType == DataControlRowType.Header))
            {
                e.Row.Cells[1].Visible = false;
               
            }

            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                e.Row.Cells[1].Visible = false;
            }

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

        /// <returns></returns>
        protected DataTable getSource()
        {
         
            return (DataTable)Session[_nameSessionData];
        }

      
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {

            ExportarExcel();

        }

      


        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
           
            DataTable dt1 = new DataTable();
           
            dt1 = (DataTable)Session[_nameSessionData];
            gvReturns.DataSource = dt1;
            gvReturns.DataBind();

            MergeRows(gvReturns, 2);

        }

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string  dato  = e.Row.Cells[1].Text;
                switch (dato)
                {
                    case "0":
                       e.Row.Cells[4].BackColor = System.Drawing.Color.FromName("#00CC00");
                       
                        break;
                    case "1":
                        e.Row.Cells[4].BackColor = System.Drawing.Color.FromName("#0099FF");
                        break;
                    case "2":
                        e.Row.Cells[4].BackColor = System.Drawing.Color.FromName("#FF0000");
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }

              

            }
        }

        private void ExportarExcel()
        {
            
            DataTable dt = (DataTable)Session[_nameSessionData];
          
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();

            String inicio;

            Style stylePrueba = new Style();
            stylePrueba.Width = Unit.Pixel(200);
            string strRows = "";
            string strRowsHead = "";
            strRowsHead = strRowsHead + "<tr height=38 >";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
             
                strRowsHead = strRowsHead + "<td height=38  bgcolor='#969696' width='38'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</ td > ";
            }

            strRowsHead = strRowsHead + "</tr>";

            foreach (DataRow row in dt.Rows)
            {
                strRows = strRows + "<tr height='38' >";
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (i == 1)
                    {
                        string intActivo = row[i].ToString();
                        switch (intActivo)
                        {
                            case "0":
                                strRows = strRows + "<td width='400'   >Activo</ td > ";
                                break;
                            case "1":
                                strRows = strRows + "<td width='400'   >Activo</ td > ";
                                break;
                            case "2":
                                strRows = strRows + "<td width='400'   >Inactivo</ td > ";
                                break;
                            default:
                                Console.WriteLine("Default case");
                                break;
                        }

                    }
                    else {
                        strRows = strRows + "<td width='400'   >" + row[i].ToString() + "</ td > ";
                    } 
                 

                   


                }

                strRows = strRows + "</tr>";
            }

            inicio = "<div> " +
                    "<table <Table border='1' bgColor='#ffffff' " +
                    "borderColor='#000000' cellSpacing='2' cellPadding='2' " +
                    "style='font-size:10.0pt; font-family:Calibri; background:white;'>" +
                    strRowsHead +
                     strRows +
                    "</table>" +
                    "</div>";

            sb.Append(inicio);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=prospectacionXlider.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }


    }
}
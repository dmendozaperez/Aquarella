using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Text;

namespace www.aquarella.com.pe.bll.Util
{
    public class GridViewExportUtil
    {
        public GridViewExportUtil()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="gv"></param>
        public static void Export(string fileName, GridView gv, bool vformatstring = false,Decimal[] _columna_caracter=null)
        {
            ///
            String style = "";
            if (vformatstring)
            {
                style = @"<style> TD { mso-number-format:\@; } </style> ";
            }
            else
            {
                style = @"<style> .textmode { mso-number-format:\@; } </script> ";
            }
            //String style = @"<style> .textmode { mso-number-format:\@; } </script> ";
            //String style = @"<style> TD { mso-number-format:\@; } </style> ";
            ///
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();

                    //  include the gridline settings
                    table.GridLines = gv.GridLines;


                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        GridViewExportUtil.PrepareControlForExport(row);
                        table.Rows.Add(row);

                        foreach (TableCell cell in row.Cells)
                        {
                            

                            //  if (cell.Text.Substring(0,1).ToString()=="0")
                            //    {
                            //    if (cell.Text.Substring(1, 1).ToString() != ".")
                            //    { 
                            if (_columna_caracter != null)
                            {

                                for (Int32 i = 0; i < _columna_caracter.Length; ++i)
                                {
                                    if (row.Cells.GetCellIndex(cell) == _columna_caracter[i])
                                    {
                                        cell.CssClass = "textmode";
                                    }
                                }
                                //if (row.Cells.GetCellIndex(cell) == _columna_caracter)
                                //{
                                //    cell.CssClass = "textmode";
                                //}
                                //if (row.Cells[])
                                //{ 
                                //    cell.CssClass = "textmode";
                                //}
                            }
                            //    }
                            //}
                            //cell.CssClass = "textmode";

                            //cell.CssClass = "string";
                        }                        

                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    /// Type text
                    HttpContext.Current.Response.Write(style);
                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        public static void ExportarExcel(DataTable dt, string ColumnasOcultas, string ColumnasTexto, string NombreArchivo)
        {

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            String style = style = @"<style> .textmode { mso-number-format:\@; } </script> ";
            Page page = new Page();
            String inicio;
            ColumnasOcultas = ',' + ColumnasOcultas + ",";
            ColumnasTexto = ',' + ColumnasTexto + ",";

            Style stylePrueba = new Style();
            stylePrueba.Width = Unit.Pixel(200);
            string strRows = "";
            string strRowsHead = "";
            strRowsHead = strRowsHead + "<tr height=38 >";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                bool ocultar = false;
                string comp = "," + i.ToString() + ",";

                if (ColumnasOcultas != ",,")
                {
                    ocultar = ColumnasOcultas.Contains(comp);
                }

                if (!ocultar)
                    strRowsHead = strRowsHead + "<td height=38  bgcolor='#969696' width='38'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Columns[i].ColumnName + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</ td > ";
            }

            strRowsHead = strRowsHead + "</tr>";

            foreach (DataRow row in dt.Rows)
            {
                strRows = strRows + "<tr height='38' >";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    bool ocultar = false;
                    string comp = "," + i.ToString() + ",";
                    string strClass = "";

                    if (ColumnasTexto != ",,")
                    {

                        if (ColumnasTexto.Contains(comp))
                            strClass = " class='textmode'";
                    }

                    if (ColumnasOcultas != ",,")
                    {

                        ocultar = ColumnasOcultas.Contains(comp);

                    }

                    if (!ocultar)
                        strRows = strRows + "<td width='400' " + strClass + " >" + row[i].ToString() + "</ td > ";
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

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + NombreArchivo + ".xls");
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = Encoding.Default;
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport(System.Web.UI.Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                System.Web.UI.Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    if (current.Visible)
                        control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                    else
                        control.Controls.AddAt(i, new LiteralControl(""));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    ///control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                    control.Controls.AddAt(i, new LiteralControl(""));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }
                else if (current is TextBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as TextBox).Text));
                }
                else if (current is Label)
                {
                    control.Controls.Remove(current);
                    if ((current as Label).Text.Equals("*"))
                        control.Controls.AddAt(i, new LiteralControl(""));
                    else
                        control.Controls.AddAt(i, new LiteralControl((current as Label).Text));
                }
                else if (current is RequiredFieldValidator)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(""));
                }
                else if (current is CompareValidator)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(""));
                }

                else if (current is HiddenField)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(""));
                }
                else if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(""));
                }
                else if (current is Image)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl(""));
                }
                if (current.HasControls())
                {
                    GridViewExportUtil.PrepareControlForExport(current);
                }
            }
        }

        /// <summary>
        /// Remover formatos de columnas en gridview
        /// </summary>
        /// <param name="grid"></param>
        public static void removeFormats(ref GridView grid)
        {
            int cols = grid.Columns.Count;
            for (int i = 0; i < cols; i++)
            {
                try
                {

                    ((BoundField)(grid.Columns[i])).DataFormatString = "";
                }
                catch
                { }
            }
        }
    }
}
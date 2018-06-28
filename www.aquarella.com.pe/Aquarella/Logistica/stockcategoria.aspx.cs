using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.UserControl;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using System.Data;
using www.aquarella.com.pe.bll.Ventas;
using System.IO;


using System.Text;
using System.Configuration;
using System.Data.OleDb;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using Bata.Aquarella.Pe.BLL.Ventas;
//using Bata.Aquarella.Bll;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class stockcategoria : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) bll.Util.Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                cargarCategoria();
                cargarTemporada();
                formForEmployee();
                
            }
        }
        protected void cargarCategoria()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de lideres        
            dwcategoria.Focus();
            dwcategoria.DataSource =www.aquarella.com.pe.bll.Stock.getAllcategoria();
            dwcategoria.DataBind();

        }

        protected void cargarTemporada()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de lideres        
            dwtemporada.Focus();
            dwtemporada.DataSource = www.aquarella.com.pe.bll.Stock.getAlltemporada();
            dwtemporada.DataBind();

        }

        private void formForEmployee()
        {
            try
            {
                this.msnMessage.Visible = false;
                sbconsulta(Convert.ToString(dwcategoria.SelectedValue));
                //odsReturns.SelectParameters[0].DefaultValue = _user._usv_co;
                //odsReturns.SelectParameters[1].DefaultValue = dwCustomers.SelectedValue;
            }
            catch (Exception ex)
            {
                this.msnMessage.LoadMessage("Error de Consulta: " + ex.Message, ucMessage.MessageType.Error);
                return;
            }
        }
        private void sbconsulta(string valor)
        {
            this.msnMessage.Visible = false;
            string _tempo = dwtemporada.SelectedValue;

            DataSet dsreturn = www.aquarella.com.pe.bll.Stock.getstockcategoria(valor,_tempo);

            Pivot pvt = new Pivot(dsreturn.Tables[0]);
            string[] fila = { "Categoria", "Codigo", "Descripcion", "tempo", "stock", "foto" };
            //string[] col = { "Ano","Mes","Semana", "Dia" };
            //string[] col = { "Ano", "Mes", "Semana" };
            string[] col = {"talla"};
           
            gvReturns.DataSource = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);
           

            gvReturns.DataBind();


            fijarcolumna();

            MergeRows(gvReturns, 1);

            Session[_nameSessionData] = dsreturn.Tables[0];
        }
        protected void fijarcolumna()
        {
            
             for (int rowIndex = gvReturns.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gvReturns.Rows[rowIndex];
                row.Cells[2].Width = 200;
                row.Cells[1].Wrap = true;
            }
             gvReturns.HeaderStyle.Wrap = true;
             gvReturns.RowStyle.Wrap = true;
            
           // var grid=gvReturns.view
           // gvReturns.HeaderRow.AccessKey
        }
       
        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            Pivot pvt = new Pivot((DataTable)Session[_nameSessionData]);
            string[] fila = { "Categoria", "Codigo", "Descripcion", "tempo", "stock", "foto" };
            //string[] col = {"Ano", "Mes","Semana", "Dia" };
            string[] col = { "talla" };

            gvReturns.DataSource = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);
           
            gvReturns.DataBind();
            fijarcolumna();
            MergeRows(gvReturns, 1);
        }

        protected void grdPivot2_RowCreated(object sender, GridViewRowEventArgs e)
        {


            //        gvReturns.Columns[2].ItemStyle.Width = Unit.Parse("10px");

            if ((e.Row.RowType == DataControlRowType.Header))
            {
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[7].Visible = false;
                //e.Row.Cells[3].Visible = false;
            }

            

            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                
                

                /// Numero de la fila
                int idRow = Convert.ToInt16(e.Row.RowIndex.ToString());

                try
                {


                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[2].Visible = false;
                   
                    //
                    string referencia = DataBinder.Eval(e.Row.DataItem, "Codigo").ToString();

                    string rutafoto= DataBinder.Eval(e.Row.DataItem, "foto").ToString();
                    //
                    string nameArticle = DataBinder.Eval(e.Row.DataItem, "Codigo").ToString();
                    //
                    Label lblPhotoArticle = (Label)e.Row.FindControl("lblPhotoArticle");

                    string pAcc = (_user._usv_employee) ? "F" : "T";

                    //
                    lblPhotoArticle.Text = "<a class='iframe' href='../Maestros/informationarticle.aspx?elcitra=" + referencia + "&isForPublicAcces=" + pAcc + "' title='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "'>" +
                        //"<img src='../../Design/images/Botones/8564884_1.jpg' border='0' width='30' height='30' alt='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "' />" +
                         "<img src='" +  rutafoto + "' border='0' width='30' height='30' alt='Informaci&oacute;n  Y Fotograf&iacute;a del Art&iacute;culo : <" + nameArticle + ">  - " + referencia + "' />" +
                        "</a>";
                }
                catch
                {
                }
            }
            
        }

        protected void dwcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vidcategoria = dwcategoria.SelectedValue;
            sbconsulta(vidcategoria);
        }
        protected DataTable getSource()
        {
            // Chequeado es ventas por semana y categoria
            /*if (chkGroupByWeek.Checked)
                return (DataTable)Session[_nameSessionData];
            // No chequeado es ventas netas entre las fechas dAQUARELLAs
            else*/
            return (DataTable)Session[_nameSessionData];
        }

        #region <metodos de pivot>
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
        #endregion

        #region < Exportar Excel>

        /// <summary>
        /// Exportar a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            
            ExportarExcel();
        }

        #endregion

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            //Int32 widestData = 0;
            //System.Data.DataRowView drv;
            //drv = (System.Data.DataRowView)e.Row.DataItem;
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (drv != null)
            //    {
            //        String catName = drv[1].ToString();
            //        Response.Write(catName + "/");

            //        int catNameLen = catName.Length;
            //        if (catNameLen > widestData)
            //        {
            //            widestData = catNameLen;
            //            gvReturns.Columns[2].ItemStyle.Width = 20;
            //            gvReturns.Columns[2].ItemStyle.Wrap = false;
            //        }

            //    }
            //}


        }

        protected void gvReturns_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void dwtemporada_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vidcategoria = dwcategoria.SelectedValue;
            sbconsulta(vidcategoria);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int rpta;
            this.msnMessage.Visible = false;
            try
            {
                if (FileUpload1.HasFile)
                {

                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    Random r = new Random();
                    int varAleatorio = r.Next(0, 999);
                    string FilePath = Server.MapPath(FolderPath + FileName + varAleatorio);
                    FileUpload1.SaveAs(FilePath);
                    int val_dwTipArc = 1;
                    rpta = validarArchivo(FilePath, Extension, val_dwTipArc);
                    Int32 nfilas = 0;
                    DataTable dtExcel = new DataTable();
                    if (rpta == 1)
                    {
                        DataTable dt = new DataTable();
                        dt = Getdt(FilePath, Extension, val_dwTipArc, ref nfilas);

                        DataSet dsreturn = www.aquarella.com.pe.bll.Stock.getstockcategoriaExcel(dt);
                        dtExcel = dsreturn.Tables[0];
                        Session[_nameSessionData] = dtExcel;

                    }
                    else
                    {
                        msnMessage.LoadMessage("Debe seleccionar un tipo de archivo correcto.", UserControl.ucMessage.MessageType.Error);
                        dtExcel = (DataTable)Session[_nameSessionData];
                    }

                    File.Delete(FilePath);
                    Pivot pvt = new Pivot(dtExcel);
                    string[] fila = { "Categoria", "Codigo", "Descripcion", "tempo", "stock", "foto" };
                    string[] col = { "talla" };
                    gvReturns.DataSource = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);
                    gvReturns.DataBind();
                    fijarcolumna();
                    MergeRows(gvReturns, 1);

                }
                else {
                    msnMessage.LoadMessage("Debe seleccionar un de archivo.", UserControl.ucMessage.MessageType.Error);
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session[_nameSessionData];
                    Pivot pvt = new Pivot(dt);
                    string[] fila = { "Categoria", "Codigo", "Descripcion", "tempo", "stock", "foto" };
                    string[] col = { "talla" };
                    gvReturns.DataSource = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);
                    gvReturns.DataBind();
                    fijarcolumna();
                    MergeRows(gvReturns, 1);

                }
            }
                    catch (Exception ex)
            {
          
                string msg;
                msg = ex.Message;

                msnMessage.LoadMessage("Ocurrio un error: " + ex.Message, UserControl.ucMessage.MessageType.Error);

            }
       }

        public static int validarArchivo(string FilePath, string Extension, int val_dwTipArc)
        {
            int rpta = 0;
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;

                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }

            if (conStr != "")
            {
                conStr = String.Format(conStr, FilePath, "A4:F5", true);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                DataTable dt_Complete = new DataTable();
                cmdExcel.Connection = connExcel;
                //Get the name of First Sheet
                connExcel.Open();

                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                connExcel.Open();
                if (val_dwTipArc == 1)
                {
                    cmdExcel.CommandText = "SELECT count(*) AS articulo  From [" + SheetName + "]";
                }


                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);

                int a = 0;
                foreach (DataRow dr in dt.Rows) //.Tables[0].Rows)
                {
                    a = System.Convert.ToInt32(dr["articulo"]);
                    if (val_dwTipArc == 1)
                    {
                        if (a > 0)
                        {
                            rpta = 1;
                            break;
                        }
                    }
                    else
                    {
                        if (a > 0)
                        {
                            rpta = 1;
                            break;
                        }
                    }
                }

                connExcel.Close();
            }
            return rpta;
        }

        private DataTable Getdt(string FilePath, string Extension, int val_dwTipArc, ref Int32 filas)
        {
            string conStr = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("articulo", typeof(string));
            
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;

                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
          
            conStr = String.Format(conStr, FilePath, "A4:F5", true);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
          

            DataTable dt_Complete = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();

            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();

            if (val_dwTipArc == 1)
            {
                cmdExcel.CommandText = "SELECT [articulo] From [" + SheetName + "]";
            }
          
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
                        
            return dt;

        }


        public void ExportarExcel()
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "mostrardescarga(); ", true);
            DataTable dt = new DataTable();

            DataTable dt1 = (DataTable)Session[_nameSessionData];
            Pivot pvt = new Pivot((DataTable)Session[_nameSessionData]);
            string[] fila = { "Categoria", "Codigo", "foto", "Descripcion", "tempo", "stock" };
            string[] col = { "talla" };

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            dt = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);

            string attachment = "attachment; filename=stockdetallado.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter stw = new StringWriter();
            HtmlTextWriter htextw = new HtmlTextWriter(stw);
            GridView gvexport = new GridView();
            gvexport.DataSource = dt;
            gvexport.DataBind();
            gvexport.RenderControl(htextw);

            string cadena = stw.ToString();

            string iniImagen = "<img WIDTH = '34' HEIGHT = '34' alt = 'Logo_FR'  style = 'margin: 50px 50px 50px 50px; vertical-align:middle;' src = 'http";
            string finImagen = ".jpg' />";

            cadena = cadena.Replace("http", iniImagen);
            cadena = cadena.Replace(".jpg", finImagen);
            cadena = cadena.Replace("<tr", "<tr height = 40");
            cadena = cadena.Replace("<td", "<td height = 40");
            cadena = cadena.Replace("foto", "&nbsp;&nbsp;&nbsp;&nbsp;Foto&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", "ocultarDescarga(); ", true);

            Response.Write(cadena);
            Response.End();

        }

        private void ExportarExcelBK()
        {
            DataTable dt = new DataTable();

            DataTable dt1 = (DataTable)Session[_nameSessionData];
            Pivot pvt = new Pivot((DataTable)Session[_nameSessionData]);
            string[] fila = { "Categoria", "Codigo", "foto", "Descripcion", "tempo", "stock" };
            string[] col = { "talla" };

            dt = pvt.PivotData("Cantidad", AggregateFunction.Sum, fila, col);

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
                    string dato = row[i].ToString();
                    if (i == 2)
                    {
                        strRows = strRows + "<td height='38'  align='center' style='text-align:center'  WIDTH='38'><img WIDTH='34' HEIGHT='34' alt='Logo_FR'  style='margin: 50px 50px 50px 50px' src='" + row[i].ToString() + "'/></td>";
                    }
                    else
                    {
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
            Response.AddHeader("Content-Disposition", "attachment;filename=stockdetallado.xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }
    }

}
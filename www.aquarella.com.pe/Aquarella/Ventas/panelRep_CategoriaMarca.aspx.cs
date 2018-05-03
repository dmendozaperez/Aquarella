using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using System.Web.UI.DataVisualization.Charting;
using System.Text;
using System.IO;

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class panelRep_CategoriaMarca : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "InfoSales", _nameSessionDataFiltered = "InfoSalesFilter", _sesionCategoriaMarca="CategoriaMarca", _sesionListLider = "ListaLideres", _sesionListLiderCategoria = "ListaLideresCategoria", _sesionLiderCategoriaSelect = "LiderCategoriaSelect", _sesionCategoriaMarcaSelect = "CategoriaMarcaSelect";
        DataSet _dsResult;
        SortDirection _sortDir;

        #region < Inicio >

        /// <summary>
        /// Inicio de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = null;
                Session[_nameSessionDataFiltered] = null;

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

  
        protected void sbllenarcombo()
        {
            DataSet dsCombos = new DataSet();
            DataTable dtMarca = new DataTable();
            DataTable dtLider = new DataTable();

            dsCombos = www.aquarella.com.pe.Bll.Ventas.ReporteMarcaxCategoria.getObtenerComboMarcaLider();
            dtMarca = dsCombos.Tables[0];
            dtLider = dsCombos.Tables[1];

            dw_Marca.DataSource = dtMarca;
            dw_Marca.DataTextField = "Mar_Descripcion";
            dw_Marca.DataValueField = "Mar_Id";
            dw_Marca.DataBind();

            dw_Lider.DataSource = dtLider;
            dw_Lider.DataTextField = "Are_Descripcion";
            dw_Lider.DataValueField = "Are_Id";
            dw_Lider.DataBind();

        }
        protected void formForEmployee()
        {
            sbllenarcombo();
          
        }

        #endregion        
       
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
                                 x2 = y.Sum(x => x.Field<decimal>("ventas")),
                                 x3 = y.Sum(x => x.Field<decimal>("podv")),
                                 x4 = y.Sum(x => x.Field<decimal>("pventas")),
                                 x5 = y.Sum(x => x.Field<decimal>("pventasneto")),
                                 x6 = y.Sum(x => x.Field<decimal>("pmargen"))                                 
                             }).FirstOrDefault();

                    gv.FooterRow.Cells[0].Text = "TOTALES:";
                    gv.FooterRow.Cells[1].Text = t.x1.ToString("N0");
                    gv.FooterRow.Cells[3].Text = t.x2.ToString("N0");
                    gv.FooterRow.Cells[4].Text = t.x3.ToString("N2");
                    gv.FooterRow.Cells[5].Text = t.x4.ToString("N2");
                    gv.FooterRow.Cells[6].Text = t.x5.ToString("N2");
                    gv.FooterRow.Cells[7].Text = t.x6.ToString("N2");
                    gv.FooterRow.Cells[8].Text = ((t.x5 - t.x3) / t.x5).ToString("P2");

                    //lblTotSales1.Text = t.x5.ToString("C0");
                    //lblTotSales2.Text = t.x5.ToString("C0");
                }
            }
            catch { }
        }

        #region < Eventos sobre data source >

        /// <summary>
        /// Settear el objeto fuente de datos
        /// </summary>
        /// <param name="co"></param>
        /// <param name="ware"></param>
        /// <param name="area"></param>
        protected void setParametersDataSources()
        {
            _dsResult =www.aquarella.com.pe.Bll.Ventas.ReporteMarcaxCategoria.getReporteMarcaCategoria(dw_Marca.SelectedValue.ToString(), dw_Lider.SelectedValue.ToString(), DateTime.Parse(txtDateStart.Text), DateTime.Parse(txtDateEnd.Text));
            Session[_nameSessionData] = _dsResult.Tables[0];
            Session[_sesionListLider] = _dsResult.Tables[1];
            Session[_sesionListLiderCategoria] = _dsResult.Tables[2];
            Session[_sesionCategoriaMarca] = _dsResult.Tables[3];
            gvSales.DataSource = _dsResult;

            //llenamos el combo de categoria
            var Categoria =
           from p in _dsResult.Tables[0].AsEnumerable()
           group p by p.Field<string>("Categoria") into g
           select new { Categoria = g.Key, Monto = g.Sum(p => p.Field<decimal>("Monto")), Cantidad = g.Sum(p => p.Field<int>("Cantidad")) };

            DataTable dtCategoria = DataUtil.toDataTable(Categoria.ToList());

            ddlCategoria.DataSource = dtCategoria;
            ddlCategoria.DataTextField = "Categoria";
            ddlCategoria.DataValueField = "Categoria";
            ddlCategoria.DataBind();


            //llevamos el combo de lideres

            var Lider =
          from p in _dsResult.Tables[0].AsEnumerable()
          group p by p.Field<string>("NombreLider") into g
          select new { NombreLider = g.Key, Monto = g.Sum(p => p.Field<decimal>("Monto")), Cantidad = g.Sum(p => p.Field<int>("Cantidad")) };
            Lider.OrderByDescending(x => x.Monto);

            DataTable dtlider = DataUtil.toDataTable(Lider.ToList());
          

            ddlLider.DataSource = dtlider;
            ddlLider.DataTextField = "NombreLider";
            ddlLider.DataValueField = "NombreLider";
            ddlLider.DataBind();

            this.gvSales2.DataSource = null;
            gvSales2.DataBind();
            this.gvLiderCategoria.DataSource = null;
            gvLiderCategoria.DataBind();

            CargarGrillaCategoria();
            cargarGillaLider();

        }

        #endregion

        #region < Paginacion, ordenacion y administracion del GridView >

        /// <summary>
        /// Refresh
        /// </summary>
        private void refreshGridView()
        {
            gvSales.DataBind();
            MergeRows(gvSales, 1);
          
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

        /// <summary>
        /// Finalizacion de enlace con fuente de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSales_DataBound(object sender, EventArgs e)
        {
            calculateTotals(gvSales, getSource());
        }

        /// <summary>
        /// Control de ordenacion en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSales_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpresion = e.SortExpression;            
            pageAndSort(gvSales, GridViewSortExpresion, getSource(), gvSales.PageIndex);            
        }

        /// <summary>
        /// Control de paginación en la grilla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pageAndSort(gvSales, GridViewSortExpresion, getSource(), e.NewPageIndex);            
        }

        /// <summary>
        /// Interfaz con la funcion de paginacion y ordenacion
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="sortField"></param>
        /// <param name="dt"></param>
        /// <param name="iPage"></param>
        protected void pageAndSort(GridView gv, string sortField, DataTable dt, int iPage)
        {
            _sortDir = GridViewSortDirection;
            Utilities.pageAndSort(gv, ref _sortDir, sortField, dt, iPage);
            GridViewSortDirection = _sortDir;
            MergeRows(gvSales, 1);
        }

        /// <summary>
        /// Manejo de las direcciones de ordenacion en la grilla
        /// </summary>
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

        /// <summary>
        /// Manejo de los campos de ordenacion o exp. de ordenacion
        /// </summary>
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

        protected void gridStatus()
        {
            pageAndSort(gvSales, GridViewSortExpresion, getSource(), gvSales.PageIndex);
        }

        #endregion
        
        #region < Web Controls >

        /// <summary>
        /// Consultar en BD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btConsult_Click(object sender, EventArgs e)
        {
          
            setParametersDataSources();
            buildGraph(1, 1);
            refreshGridView();
        
        }       
        
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            //gvSales.AllowPaging = false;
            //GridViewExportUtil.removeFormats(ref gvSales);
            //gridStatus();

            //string nameFile;

            //nameFile = "VentaxCategoria";
          
            //GridViewExportUtil.Export(nameFile + ".xls", gvSales);

            DataTable dt = (DataTable)Session[_nameSessionData];

            ExportarExcel(dt, "", "", "ReporteMarcaxCategoria");
        }


        private void ExportarExcel(DataTable dt, string ColumnasOcultas, string ColumnasTexto, string NombreArchivo)
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
            int j = 0;
                foreach (DataRow row in dt.Rows)
                {
                j++;
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

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + NombreArchivo + ".xls");
                Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Default;
                Response.Write(style);
                Response.Write(sb.ToString());
                Response.End();
            }


        #endregion


        protected DataTable getSource()
        {
           return (DataTable)Session[_nameSessionData];
        }

        #region < Gráficos >

        /// <summary>
        /// Construccion de graficos
        /// </summary>
        /// <param name="bars"></param>
        /// <param name="piramid"></param>
        public void buildGraph(int bars, int pie)
        {
            DataTable dt = (DataTable)Session[_nameSessionData];
            DataTable dtLider = (DataTable)Session[_sesionListLider];

            if (ddlVerPor.SelectedValue == "U") 
                pie = 2;

            if (ddlLverPor.SelectedValue == "U")
                bars = 2;
           

            var sales =
            from p in dt.AsEnumerable()
            group p by p.Field<string>("Categoria") into g
            select new { category = g.Key, sales = g.Sum(p => p.Field<decimal>("Monto")) };

            var Lider =
          from p in dtLider.AsEnumerable()
          group p by p.Field<string>("NombreLider") into g
          select new { category = g.Key, sales = g.Sum(p => p.Field<decimal>("porc")) };

            sales.OrderByDescending(x => x.sales);
            if (bars ==2 )
            {
                //foreach (var series in chartSales.Series)
                //{
                //    series.Points.Clear();
                //}

                //chartSales.DataBind();
                
                var Lider2 =
                  from p in dtLider.AsEnumerable()
                  group p by p.Field<string>("NombreLider") into g
                  select new { category = g.Key, sales = g.Sum(p => p.Field<decimal>("Cantidadporc")) };

                DataTable dtvew = new DataTable();
                dtvew = DataUtil.toDataTable(Lider2.ToList());

                chartSales.DataSource = Lider2;
                // Show as 2D or 3D
                if (CheckboxShow3D.Checked)
                    chartSales.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                else
                    chartSales.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

                chartSales.Series[0].ToolTip = "#LEGENDTEXT: #VAL{P0} - #PERCENT";
                chartSales.ChartAreas[0].AxisY.LabelStyle.Format = "{#,##}%";
                chartSales.Series[0].LegendToolTip = "#PERCENT";

                chartSales.Series[0].LabelFormat = "{#,##}%";
                chartSales.Series[0].IsValueShownAsLabel = true;
                // Set series members names for the X and Y values 
                chartSales.Series[0].XValueType = System.Web.UI.DataVisualization.Charting.ChartValueType.String;
                chartSales.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "#.##";
                chartSales.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
                chartSales.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
                // Set axis labels angle                
                //chartSales.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = int.Parse(FontAngleList.SelectedItem.Text);
                chartSales.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -40;
                // Set offset labels style
                chartSales.AntiAliasing = System.Web.UI.DataVisualization.Charting.AntiAliasingStyles.All;

                // Paint labels of all bars
                chartSales.ChartAreas[0].AxisX.Interval = 1;

                chartSales.DataBind();
            } else if (bars > 0)
            {
                chartSales.DataSource = Lider;
                // Show as 2D or 3D
                if (CheckboxShow3D.Checked)
                    chartSales.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                else
                    chartSales.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

                chartSales.Series[0].ToolTip = "#LEGENDTEXT: #VAL{P0} - #PERCENT";
                chartSales.ChartAreas[0].AxisY.LabelStyle.Format = "{#}%";
                chartSales.Series[0].LegendToolTip = "#PERCENT";

                chartSales.Series[0].LabelFormat = "{#}%";
                chartSales.Series[0].IsValueShownAsLabel = true;
                // Set series members names for the X and Y values 
                chartSales.Series[0].XValueType = System.Web.UI.DataVisualization.Charting.ChartValueType.String;

                // Set axis labels angle                
                //chartSales.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = int.Parse(FontAngleList.SelectedItem.Text);
                chartSales.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -40;
                // Set offset labels style
                chartSales.AntiAliasing = System.Web.UI.DataVisualization.Charting.AntiAliasingStyles.All;

                // Paint labels of all bars
                chartSales.ChartAreas[0].AxisX.Interval = 1;                

                chartSales.DataBind();
            }

            if (pie == 2)
            {
              
                var sales2 =
                from p in dt.AsEnumerable()
                group p by p.Field<string>("Categoria") into g
                select new { category = g.Key, sales = g.Sum(p => p.Field<int>("Cantidad")) };
                               
                // Set series and legend tooltips        
                ChartPie.Series[0].ToolTip = "#LEGENDTEXT: #VAL{C0}";
                ChartPie.Series[0].LegendToolTip = "Participación: #PERCENT - Unidades: #VAL";/// Label ="#PERCENT{P1}"

                ChartPie.Titles[0].Text = "Venta Global - Participación por categoría";

                // Enlace de ods con el control de graficacion
                ChartPie.DataSource = sales2;
                ChartPie.DataBind();

                // Set labels style
                ChartPie.Series[0]["PieLabelStyle"] = "outside";

                // Enable 3D
                ChartPie.ChartAreas["Default"].Area3DStyle.Enable3D = chkShow3d.Checked;
                // Set chart type
                ChartPie.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), comboBoxChartType.SelectedItem.Value.ToString(), true);
                if (this.comboBoxChartType.SelectedItem.Value.ToString() == "Doughnut")
                {
                    ChartPie.Series[0]["DoughnutRadius"] = "20";
                }
            }else if (pie > 0)
            {                
                // Set series and legend tooltips        
                ChartPie.Series[0].ToolTip = "#LEGENDTEXT: #VAL{C0}";
                ChartPie.Series[0].LegendToolTip = "Participación: #PERCENT - Soles: #VAL{C0}";/// Label ="#PERCENT{P1}"
             
                ChartPie.Titles[0].Text = "Venta Global - Participación por categoría";

                // Enlace de ods con el control de graficacion
                ChartPie.DataSource = sales;
                ChartPie.DataBind();

                // Set labels style
                ChartPie.Series[0]["PieLabelStyle"] = "outside";
                
                // Enable 3D
                ChartPie.ChartAreas["Default"].Area3DStyle.Enable3D = chkShow3d.Checked;
                // Set chart type
                ChartPie.Series[0].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), comboBoxChartType.SelectedItem.Value.ToString(), true);
                if (this.comboBoxChartType.SelectedItem.Value.ToString() == "Doughnut")
                {
                    ChartPie.Series[0]["DoughnutRadius"] = "20";
                }
            }
           
        }

        protected void CheckboxShow3D_CheckedChanged(object sender, EventArgs e)
        {
            buildGraph(1, 0);
        }

        protected void FontAngleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(1, 0);
        }

        protected void chkShow3d_CheckedChanged(object sender, EventArgs e)
        {
            buildGraph(0, 1);
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(0, 1);
            CargarGrillaCategoria();

        }

        protected void CargarGrillaCategoria() {

            string strCategoria = ddlCategoria.SelectedValue;

            DataTable dt = (DataTable)Session[_sesionCategoriaMarca];
            DataTable dtMarca = dt.Clone();

            DataRow[] rows = dt.Select("Categoria ='" + strCategoria + "'");

            foreach (DataRow dr in rows)
                dtMarca.ImportRow(dr);

            if (ddlVerPor.SelectedValue == "U")
            {
                var Unidades =
                   from p in dtMarca.AsEnumerable()
                   group p by p.Field<string>("Marca") into g
                   select new { Marca = g.Key, Prc = g.Sum(p => p.Field<decimal>("CantidadPrc")) };

                dtMarca = new DataTable();
                dtMarca = DataUtil.toDataTable(Unidades.ToList());
            }


            Session[_sesionCategoriaMarcaSelect] = dtMarca;

            gvSales2.DataSource = dtMarca;
            gvSales2.DataBind();

        }

        protected void ddlLider_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(1, 1);
            cargarGillaLider();

        }


        protected void cargarGillaLider() {

            string strLider = ddlLider.SelectedValue;

            DataTable dt = (DataTable)Session[_sesionListLiderCategoria];
            DataTable dtCategoria = dt.Clone();

            DataRow[] rows = dt.Select("NombreLider ='" + strLider + "'");

            foreach (DataRow dr in rows)
                dtCategoria.ImportRow(dr);

            if (ddlLverPor.SelectedValue == "U")
            {
                var Unidades =
                   from p in dtCategoria.AsEnumerable()
                   group p by p.Field<string>("Categoria") into g
                   select new { Categoria = g.Key, Prc = g.Sum(p => p.Field<decimal>("CantidadPrc")) };

                dtCategoria = new DataTable();
                dtCategoria = DataUtil.toDataTable(Unidades.ToList());
            }
            Session[_sesionLiderCategoriaSelect] = dtCategoria;

            gvLiderCategoria.DataSource = dtCategoria;
            gvLiderCategoria.DataBind();

        }
        protected void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(0, 1);
        }

        protected void ddlVerPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(0, 1);
        }

        protected void ddlLVerPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(1, 1);
        }

        protected void gvLiderCategoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLiderCategoria.PageIndex = e.NewPageIndex;

            DataTable dt1 = new DataTable();

            dt1 = (DataTable)Session[_sesionLiderCategoriaSelect];
            gvLiderCategoria.DataSource = dt1;
            gvLiderCategoria.DataBind();

        }

        protected void gvSales2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSales2.PageIndex = e.NewPageIndex;

            DataTable dt1 = new DataTable();

            dt1 = (DataTable)Session[_sesionCategoriaMarcaSelect];
            gvSales2.DataSource = dt1;
            gvSales2.DataBind();

        }

        #endregion

    }
}
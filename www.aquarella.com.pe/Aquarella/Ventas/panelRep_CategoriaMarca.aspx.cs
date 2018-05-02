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

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class panelRep_CategoriaMarca : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "InfoSales", _nameSessionDataFiltered = "InfoSalesFilter", _sesionCategoriaMarca="CategoriaMarca", _sesionListLider = "ListaLideres";
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

        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        /// 
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
            //if (!string.IsNullOrEmpty(_user._usv_warehouse))
            //{
            //    if (!string.IsNullOrEmpty(_user._usv_area))
            //    {
            //        WareAreaForm.wareAreaForm(_user._usv_co, _user._usv_region);
            //        //WareAreaForm.setFormByUser(_user);
            //    }
            //    else
            //        msnMessage.LoadMessage("No se encuentra asociado a ninguna area", UserControl.ucMessage.MessageType.Error);
            //}
            //else
            //    msnMessage.LoadMessage("No se encuentra asociado a ninguna bodega", UserControl.ucMessage.MessageType.Error);

            // Enlazar datoS
            //refreshGridView();
        }

        #endregion        
        
        /// <summary>
        /// Calculo de totales
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="dt"></param>
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

                    lblTotSales1.Text = t.x5.ToString("C0");
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
            Session[_sesionCategoriaMarca] = _dsResult.Tables[2];
            gvSales.DataSource = _dsResult;

            var Categoria =
           from p in _dsResult.Tables[0].AsEnumerable()
           group p by p.Field<string>("Categoria") into g
           select new { Categoria = g.Key, Monto = g.Sum(p => p.Field<decimal>("Monto")), Cantidad = g.Sum(p => p.Field<int>("Cantidad")) };

            DataTable dtCategoria = DataUtil.toDataTable(Categoria.ToList());

            ddlCategoria.DataSource = dtCategoria;
            ddlCategoria.DataTextField = "Categoria";
            ddlCategoria.DataValueField = "Categoria";
            ddlCategoria.DataBind();

        }

        #endregion

        #region < Paginacion, ordenacion y administracion del GridView >

        /// <summary>
        /// Refresh
        /// </summary>
        private void refreshGridView()
        {
            gvSales.DataBind();
          
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
        

        protected void chkGroupByWeek_CheckedChanged(object sender, EventArgs e)
        {
            // Realizar filtro siempre sobre la fuente de datos original
            DataTable dt = (DataTable)Session[_nameSessionData];

            if (!chkGroupByWeek.Checked)
            {
                if (dt != null)
                {
                    var t = (from x in dt.AsEnumerable()
                             group x by x.Field<object>("mcv_description") into y
                             select new
                             {
                                 anno = string.Empty,
                                 can_week_no = string.Empty,
                                 mcv_description = y.Key,
                                 ventas = y.Sum(x => x.Field<decimal>("ventas")),
                                 podv = y.Sum(x => x.Field<decimal>("podv")),
                                 pventas = y.Sum(x => x.Field<decimal>("pventas")),
                                 pventasneto = y.Sum(x => x.Field<decimal>("pventasneto")),
                                 pmargen = y.Sum(x => x.Field<decimal>("pmargen")),
                                 pmargenpor = y.Sum(x => x.Field<decimal>("pmargenpor"))
                             }).Distinct().ToList();

                    DataTable dtGroup = DataUtil.toDataTable(t.ToList());
                    Session[_nameSessionDataFiltered] = dtGroup;
                    gvSales.DataSource = dtGroup;
                }
            }
            else
            {
                gvSales.DataSource = dt;
              
            }
            gvSales.DataBind();

          
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvSales.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvSales);
            gridStatus();

            string nameFile;

            if (chkGroupByWeek.Checked)
                nameFile = "VentaxCategoriaxSemana";
            else
                nameFile = "VentaxCategoria";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvSales);
        }

        #endregion
        
        /// <summary>
        /// Fuente de datos con la cual se este trabajando
        /// </summary>
        /// <returns></returns>
        protected DataTable getSource()
        {
            // Chequeado es ventas por semana y categoria
            if (chkGroupByWeek.Checked)
                return (DataTable)Session[_nameSessionData];
                // No chequeado es ventas netas entre las fechas dAQUARELLAs
            else
                return (DataTable)Session[_nameSessionDataFiltered];
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

            if (ddlVerPor.SelectedValue == "U") {
                pie = 2;
            }

            var sales =
            from p in dt.AsEnumerable()
            group p by p.Field<string>("Categoria") into g
            select new { category = g.Key, sales = g.Sum(p => p.Field<decimal>("Monto")) };

            var Lider =
          from p in dt.AsEnumerable()
          group p by p.Field<string>("NombreLider") into g
          select new { category = g.Key, sales = g.Sum(p => p.Field<decimal>("Monto")) };

            sales.OrderByDescending(x => x.sales);

            if (bars > 0)
            {
                chartSales.DataSource = Lider;
                // Show as 2D or 3D
                if (CheckboxShow3D.Checked)
                    chartSales.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
                else
                    chartSales.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

                chartSales.Series[0].ToolTip = "#LEGENDTEXT: #VAL{C0} - #PERCENT";
                chartSales.Series[0].LegendToolTip = "#PERCENT";

                chartSales.Series[0].LabelFormat = "{C0}";
                chartSales.Series[0].IsValueShownAsLabel = true;
                // Set series members names for the X and Y values 
                chartSales.Series[0].XValueType = System.Web.UI.DataVisualization.Charting.ChartValueType.String;

                // Set axis labels angle                
                chartSales.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = int.Parse(FontAngleList.SelectedItem.Text);
                chartSales.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -90;
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
            string strCategoria = ddlCategoria.SelectedValue;
           
            DataTable dt = (DataTable)Session[_sesionCategoriaMarca];
            DataTable dtMarca = dt.Clone(); 

            DataRow[] rows = dt.Select("Categoria ='" + strCategoria + "'");

            foreach (DataRow dr in rows)
                dtMarca.ImportRow(dr);
                      
            if (ddlVerPor.SelectedValue == "U") {
                var Unidades =
                   from p in dtMarca.AsEnumerable()
                   group p by p.Field<string>("Marca") into g
                   select new { Marca = g.Key, Prc = g.Sum(p => p.Field<decimal>("CantidadPrc")) };

                dtMarca = new DataTable();
                dtMarca = DataUtil.toDataTable(Unidades.ToList());
            }



            gvSales2.DataSource = dtMarca;
            gvSales2.DataBind();

        }

        protected void comboBoxChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(0, 1);
        }

        protected void ddlVerPor_SelectedIndexChanged(object sender, EventArgs e)
        {
            buildGraph(0, 1);
        }
      
        #endregion

    }
}
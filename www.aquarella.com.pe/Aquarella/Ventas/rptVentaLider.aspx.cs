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
//using Bata.Aquarella.UserControl;


namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class rptVentaLider : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";
        string _Separator = ".";

        private ReportDocument  ventasObjectsReport = new ReportDocument();
        private string _nombreSession = "Valoresventa";
        private string _pathFile = "rptReporteVenta.rpt";
       // private string _pathFile = "~//Reports//Ventas//ReporteVenta.rpt";
        
        private ArrayList ventaValues;
  
        private string reportPath; 

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                cargarLider();
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

                if ((_user._usu_tip_id == "01") || (_user._usu_tip_id == "03"))
                {
                    formForCustomer();
                }
                else
                {
                    formForEmployee();
                }

                //if (_user._usv_employee == true && _user._usv_area == "%%")
                //    formForEmployee();
                //else if (_user._usv_employee==true  && _user._usv_area != "%%")
                //    formForCustomer();
            }
        }

#region <METODOS DEL CRYSTAL>


        private void sbcargarcrystal(int customer)
        {
            try
            {
                this.PopulateValueCrystalReportI(optunidad.Checked, customer);
                this.reportPath = this.Server.MapPath(this._pathFile);
                //this.reportPath = this._pathFile;
                this.ventasObjectsReport = new ReportDocument();
                this.ventasObjectsReport.Load(this.reportPath);

                this.ventasObjectsReport.SetDataSource((IEnumerable)this.ventaValues);

                //if (this.Request.Params["ShowReportOnWeb"] == null)
               this.ventasObjectsReport.ExportToHttpResponse(ExportFormatType.ExcelWorkbook, this.Response, true, "VentasLider");
                
                //this.ventasObjectsReport.E
                
                
                //this.crvreport.ReportSource = (object)this.ventasObjectsReport;
            }
            catch
            {
             
            }
            
        }
        public void PopulateValueCrystalReportI(bool validauni,int customer)
        {
            if (this.Session[this._nombreSession] == null)
            {
                DataTable dtventa =www.aquarella.com.pe.bll.Payments.getventaunmo( customer, Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text),_user._asesor).Tables[0];
                if (dtventa.Rows.Count > 0)
                {
                    this.ventaValues = new ArrayList();
                    String validanumero="";
                    string _resumen = (chkresumen.Checked) ? "1" : "0";
                    if (validauni)
                    {
                        validanumero = "1";
                    }
                    else
                    {
                        validanumero = "2";
                    }
                    foreach (DataRow dataRow in (InternalDataCollectionBase)dtventa.Rows)
                    {
                        string lider = dataRow["lider"].ToString();
                        string mes = dataRow["mes"].ToString();
                        Int16 dia =Convert.ToInt16 (dataRow["dia"].ToString());
                        string cliente = dataRow["Cliente"].ToString();
                        string año=dataRow["Ano"].ToString();
                        string Semana = dataRow["Semana"].ToString();
                        string asesor= dataRow["asesor"].ToString();
                        string dni= dataRow["dni"].ToString();
                        Decimal totalparesmonto = 0;
                        if (validauni)
                        {
                            
                            totalparesmonto = Convert.ToDecimal(dataRow["Total Pares"].ToString());
                        }
                        else
                        {
                            totalparesmonto = Convert.ToDecimal(dataRow["Venta Total"].ToString());
                        }
                        //int TotalPares = Convert.ToInt16(dataRow["Total Pares"].ToString());
                        //Decimal VentaTotal = Convert.ToDecimal(dataRow["Venta Total"].ToString());

                        this.ventaValues.Add((object)new ReporteVentas(lider, mes, dia, cliente, totalparesmonto,validanumero,año,Semana, _resumen, asesor,dni));
                    }
                }
                this.Session[this._nombreSession] = (object)this.ventaValues;
            }
            else
                this.ventaValues = (ArrayList)this.Session[this._nombreSession];
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (this.ventasObjectsReport == null || !this.ventasObjectsReport.IsLoaded)
                return;
            this.ventasObjectsReport.Close();
        }
#endregion
        protected void cargarLider()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de lideres        
            dwCustomers.Focus();
            dwCustomers.DataSource =www.aquarella.com.pe.bll.Area.getAllAreas(_user._asesor);
            dwCustomers.DataBind();

        }


        private void formForEmployee()
        {
            try
            {
                this.msnMessage.Visible = false;
                sbconsulta(Convert.ToInt16(dwCustomers.SelectedValue));
                //odsReturns.SelectParameters[0].DefaultValue = _user._usv_co;
                //odsReturns.SelectParameters[1].DefaultValue = dwCustomers.SelectedValue;
            }
            catch (Exception ex)
            {
                this.msnMessage.LoadMessage("Error de Consulta: "  + ex.Message, ucMessage.MessageType.Error);
                return;
            }
        }

        protected void formForCustomer()
        {
            try
            {
                this.msnMessage.Visible = false;
                // Ocultar Panel de Seleccion de Coordinador
                pnlDwCustomers.Visible = false;
                //
                sbconsulta(Convert.ToInt16(_user._usv_area));
                //setParamsDataSource(_user._usv_co, _user._usv_area.ToString());
                //
                //refreshGrid();
            }
            catch (Exception ex)
            {
                this.msnMessage.LoadMessage("Error de Consulta: " + ex.Message, ucMessage.MessageType.Error);
            }
        }

        protected void setParamsDataSource(string co, string idCust)
        {
            //odsReturns.SelectParameters[0].DefaultValue = co;
            //odsReturns.SelectParameters[1].DefaultValue = idCust;
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
         //   sbcargarcrystal();   
            
            //
            if ((_user._usu_tip_id == "01") || (_user._usu_tip_id == "03"))
            {
                formForCustomer();
            }
            else
            {
                formForEmployee();
            }
            //if (_user._usv_employee == true && _user._usv_area == "%%")
            //    formForEmployee();
            //else if (_user._usv_employee == true && _user._usv_area != "%%")
            //    formForCustomer();
            //gvReturns.DataSourceID = odsReturns.ID;

           
        }

        private void sbconsulta(int valor)
        {
            DataSet dsreturn = www.aquarella.com.pe.bll.Payments.getventaunmo( valor, Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text),_user._asesor);

            Pivot pvt = new Pivot(dsreturn.Tables[0]);
            string[] filagru = { "Asesor", "Lider", "Cliente","Dni" };
            string[] filalid = { "Asesor", "Lider"};

            string[] fila = (chkresumen.Checked) ? filalid : filagru;

            //string[] col = { "Ano","Mes","Semana", "Dia" };
            string[] col = { "Ano", "Mes", "Semana"};
            if (optunidad.Checked)
            {
                gvReturns.DataSource = pvt.PivotData("Total Pares", AggregateFunction.Sum, fila, col);
            }
            else
            {
                gvReturns.DataSource = pvt.PivotData("Venta Total", AggregateFunction.Sum, fila, col);
            }


            gvReturns.DataBind();
            if (!chkresumen.Checked)
            { 
                MergeRows(gvReturns, 3);
            }
            else
            {
                MergeRows(gvReturns, 2);
            }

            Session[_nameSessionData] = dsreturn.Tables[0];
        }

        #region <METODO DE FORMATO PIVOT>

        protected void grdPivot2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
                MergeHeader((GridView)sender, e.Row, 3);
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

        //protected void odsReturns_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        //{
        //    try
        //    {
        //        DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                

                
        //        Session[_nameSessionData] = dt;
        //    }
        //    catch
        //    { }
        //}


        /// <summary>
        /// Fuente de datos con la cual se este trabajando
        /// </summary>
        /// <returns></returns>
        protected DataTable getSource()
        {
            // Chequeado es ventas por semana y categoria
            /*if (chkGroupByWeek.Checked)
                return (DataTable)Session[_nameSessionData];
            // No chequeado es ventas netas entre las fechas dAQUARELLAs
            else*/
            return (DataTable)Session[_nameSessionData];
        }

        #region < Exportar Excel>

        /// <summary>
        /// Exportar a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            this.Session[this._nombreSession] = null;


            if ((_user._usu_tip_id == "02"))
            {
                sbcargarcrystal(Convert.ToInt16(_user._usv_area));
            }
            else
            {
                sbcargarcrystal(Convert.ToInt16(dwCustomers.SelectedValue));
            }

            //if (_user._usv_employee == true && _user._usv_area == "%%")
            //    sbcargarcrystal(Convert.ToInt16(dwCustomers.SelectedValue));
            //else if (_user._usv_employee == true && _user._usv_area != "%%")
            //    sbcargarcrystal(Convert.ToInt16(_user._usv_area));

            

            //Pivot pvt = new Pivot((DataTable)Session[_nameSessionData]);
            //string[] fila = { "lider", "Cliente" };
            //string[] col = { "Mes", "Dia" };
            //DataTable dt =new DataTable();
            //if (optunidad.Checked)
            //{
            //    dt = pvt.PivotData("Total Pares", AggregateFunction.Sum, fila, col);
            //}
            //else
            //{
            //    dt = pvt.PivotData("Venta Total", AggregateFunction.Sum, fila, col);
            //}
            //gvReturns.DataSource =dt;
            //gvReturns.DataBind();
            //MergeRows(gvReturns, 2);

            //gvReturns.AllowPaging = false;
            //GridViewExportUtil.removeFormats(ref gvReturns);
            //gvReturns.DataBind();

            //string nameFile = "VentasLider";

            ////  pass the grid that for exporting ...
            //GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }

        #endregion

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;
            Pivot pvt = new Pivot((DataTable)Session[_nameSessionData]);

            string[] filagru = { "Asesor", "Lider", "Cliente", "Dni" };
            string[] filalid = { "Asesor", "Lider" };

            string[] fila = (chkresumen.Checked) ? filalid : filagru;

            //string[] fila = { "lider", "Cliente" };
            //string[] col = {"Ano", "Mes","Semana", "Dia" };
            string[] col = { "Ano", "Mes", "Semana"};
            if (optunidad.Checked)
            {
                gvReturns.DataSource = pvt.PivotData("Total Pares", AggregateFunction.Sum, fila, col);
            }
            else
            {
                gvReturns.DataSource = pvt.PivotData("Venta Total", AggregateFunction.Sum, fila, col);
            }
            gvReturns.DataBind();
            if (!chkresumen.Checked)
            {
                MergeRows(gvReturns, 3);
            }
            else
            {
                MergeRows(gvReturns, 2);
            }
            //MergeRows(gvReturns, 2);
         
        }

        protected void chkresumen_CheckedChanged(object sender, EventArgs e)
        {
            if ((_user._usu_tip_id == "01") || (_user._usu_tip_id == "03"))
            {
                formForCustomer();
            }
            else
            {
                formForEmployee();
            }
        }
    }

    
}
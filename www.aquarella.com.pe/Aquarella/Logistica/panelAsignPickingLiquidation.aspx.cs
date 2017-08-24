using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.UserControl;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Collections;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.UserControl;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class panelAsignPickingLiquidation : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "InfoLiqPicking",_nameSessionEmployees = "InfoEmployees", _pathReporPicking = "../../Reports/Logistica/reportPicking.aspx?NoLiq=";

        #region < Funciones de inicio >

        /// <summary>
        /// Load de la pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (this.Session[Constants.NameSessionUser] == null)
                Utilities.logout(this.Page.Session, this.Page.Response);
            else
                this._user = (Users)this.Session[Constants.NameSessionUser];
            if (this.IsPostBack)
                return;
            this.Session[this._nameSessionData] = (object)null;

            if ((_user._usu_tip_id == "02"))
            {
                Utilities.logout(this.Page.Session, this.Page.Response);   
            }
            else
            {
                this.formForEmployee();
            }
            //if (this._user._usv_employee)
            //    this.formForEmployee();
            //else
            //    Utilities.logout(this.Page.Session, this.Page.Response);   
        }

        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        protected void formForEmployee()
        {
            //if (!string.IsNullOrEmpty(this._user._usv_warehouse))
            //{
                if (!string.IsNullOrEmpty(this._user._usv_area))
                {
                    this.getEmployees(this._user._usv_co, this._user._usv_warehouse, Constants.IdTypeEmployeePicking);
                    this.initGrid();
                }
                else
                    this.msnMessage.LoadMessage("No se encuentra asociado a ninguna area", ucMessage.MessageType.Error);
            //}
            //else
            //    this.msnMessage.LoadMessage("No se encuentra asociado a ninguna bodega", ucMessage.MessageType.Error);   
        }

        #endregion
        //private string _nombreSession = "Valoresventa";
        /// <summary>
        /// Consulta de empleados por cargo
        /// </summary>
        /// <param name="co"></param>
        /// <param name="ware"></param>
        /// <param name="idProfes"></param>
        /// 
        private ReportDocument ventasObjectsReport = new ReportDocument();
        private string _nombreSession = "Valoresventa";
        private string _pathFile = "liquidacion_grupo_consulta.rpt";
        // private string _pathFile = "~//Reports//Ventas//ReporteVenta.rpt";

        private ArrayList ventaValues;

        private string reportPath; 
        protected void getEmployees(string co, string ware, string idProfes)
        {
            this.Session[this._nameSessionEmployees] = (object)Employees.getEmployeesByCharge("07");
        }

        /// <summary>
        /// Inicializacion de grid
        /// </summary>
        /// <param name="co"></param>
        /// <param name="ware"></param>
        /// <param name="area"></param>
        protected void initGrid()
        {
            this.Session[this._nameSessionData] = (object)Liquidations_Hdr.getLiquidationPicking().Tables[0];
            this.gvLiqPicking.DataSourceID = this.odsLiqsPick.ID;
            this.gvLiqPicking.DataBind();
            this.gvLiqHall.DataSourceID = this.odsLiqsHall.ID;
            this.gvLiqHall.DataBind();
        }

        /// <summary>
        /// Filtro de liquidaciones para marcar envio a residencia
        /// </summary>
        /// <returns></returns>
        public DataTable getLigsPick()
        {
            try
            {
                DataTable dt = (DataTable)this.Session[this._nameSessionData];
                if (dt == null || dt.Rows.Count == 0)
                    return new DataTable();

                return DataTableExtensions.CopyToDataTable<DataRow>((IEnumerable<DataRow>)EnumerableRowCollectionExtensions.Where<DataRow>(DataTableExtensions.AsEnumerable(dt), (Func<DataRow, bool>)(x => DataRowExtensions.Field<string>(x, "liq_estid").Equals("PM"))));
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Filtro de liquidaciones a marcar para entrega personal
        /// </summary>
        /// <returns></returns>
        public DataTable getLiqsHalls()
        {
            try
            {
                DataTable dt = (DataTable)this.Session[this._nameSessionData];
                if (dt == null || dt.Rows.Count == 0)
                    return new DataTable();

                return DataTableExtensions.CopyToDataTable<DataRow>((IEnumerable<DataRow>)EnumerableRowCollectionExtensions.Where<DataRow>(DataTableExtensions.AsEnumerable(dt), (Func<DataRow, bool>)(x => !DataRowExtensions.Field<string>(x, "lhv_status").Equals("PM"))));
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Calculo de totales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLiqPicking_DataBound(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            try
            {
                this.calculateTotals(this.gvLiqPicking, this.getLigsPick(), true, chkAutoUpdate.Checked);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Calculo de totales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLiqHall_DataBound(object sender, EventArgs e)
        {
            this.calculateTotals(this.gvLiqHall, this.getLiqsHalls(), false, chkAutoUpdate.Checked);
        }

        /// <summary>
        /// Construccion de dialogs para confirmaciones e inicios de marcacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLiqPicking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            string str = DataBinder.Eval(e.Row.DataItem, "pin_employee").ToString();
            ImageButton imageButton1 = (ImageButton)e.Row.FindControl("ibStartPicking");
            ImageButton imageButton2 = (ImageButton)e.Row.FindControl("ibEndPicking");
            ImageButton imageButton3 = (ImageButton)e.Row.FindControl("ibComm");
            imageButton1.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de INICIAR la marcación de la liquidación número : -" + DataBinder.Eval(e.Row.DataItem, "liq_id") + "-, Perteneciente A : -" + (string)DataBinder.Eval(e.Row.DataItem, "nombres") + "- ?')");
            if (!string.IsNullOrEmpty(str))
            {
                DropDownList dropDownList = (DropDownList)e.Row.FindControl("dwEmployees");
                dropDownList.SelectedValue = str;
                dropDownList.Enabled = false;
                imageButton1.Enabled = false;
                imageButton1.ToolTip = "Liquidación en proceso de marcación.";
                imageButton1.Style.Value = "filter: alpha(opacity=50); opacity: .5;";
                imageButton3.Visible = true;
                imageButton3.ToolTip = "Ver información de marcación del pedido";
                imageButton2.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de FINALIZAR La marcación de la liquidación número : -" + DataBinder.Eval(e.Row.DataItem, "liq_id") + "-, Perteneciente A : -" + (string)DataBinder.Eval(e.Row.DataItem, "nombres") + "- ?')");
            }
            else
            {
                imageButton2.Enabled = false;
                imageButton2.ToolTip = "La liquidación aún no inicia la marcación.";
                imageButton2.Style.Value = "filter: alpha(opacity=50); opacity: .5;";
            }
        }

        /// <summary>
        /// Incio, finalizacion o imprecion de liquidacion para marcacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLiqPicking_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.msnMessage.HideMessage();
            string str1;
            if (e.CommandName.Equals("startPicking"))
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _noLiq = e.CommandArgument.ToString();
                string selectedValue = ((DropDownList)row.FindControl("dwEmployees")).SelectedValue;
                if (string.IsNullOrEmpty(selectedValue) || selectedValue.Equals("-1"))
                {
                    this.msnMessage.LoadMessage("Seleccione el empleado encargado de la marcación para la liquidación No." + _noLiq, ucMessage.MessageType.Error);
                }
                else
                {
                    str1 = string.Empty;
                    try
                    {
                        str1 = Picking.addOrderToPicking( _noLiq, selectedValue);
                        
                        // Async
                        //Log_Transaction.registerUserInfo(_user, "CREATE PICKING:" + _noLiq);

                        this.msnMessage.LoadMessage("Se ha iniciado la marcación para la liquidación No." + _noLiq + ".", ucMessage.MessageType.Information);
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error realizando la finalización de marcación liq No." + _noLiq + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }
                    this.refreshGridView();
                }
            }
            else if (e.CommandName.Equals("endPicking"))
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _noLiq = e.CommandArgument.ToString();
                string selectedValue = ((DropDownList)row.FindControl("dwEmployees")).SelectedValue;
                if (string.IsNullOrEmpty(selectedValue) || selectedValue.Equals("-1"))
                {
                    this.msnMessage.LoadMessage("Seleccione el empleado encargado de la marcación para la liquidación No." + _noLiq, ucMessage.MessageType.Error);
                }
                else
                {
                    str1 = string.Empty;
                    try
                    {
                        str1 = Picking.finalizePicking( _noLiq);
                        this.msnMessage.LoadMessage("Se finalizado la marcación de la liquidación No." + _noLiq + ".", ucMessage.MessageType.Information);

                        // Async 
                       // Log_Transaction.registerUserInfo(_user, "END PICKING:" + _noLiq);
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error realizando la finalización de marcación liq No." + _noLiq + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }
                    this.refreshGridView();
                }
            }
            else
            {
                if (e.CommandName.Equals("print"))
                {
                    //return;
                string str2 = e.CommandArgument.ToString();
                GridViewRow gridViewRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string selectedValue = ((ListControl)gridViewRow.FindControl("dwEmployees")).SelectedValue;
                if (string.IsNullOrEmpty(selectedValue) || selectedValue.Equals("-1"))
                    this.msnMessage.LoadMessage("Seleccione el empleado encargado de la marcación para la liquidación No." + str2, ucMessage.MessageType.Error);
                else
                    ScriptManager.RegisterStartupScript(this.upMsg, this.Page.GetType(), "click", "location.href='" + this._pathReporPicking + str2 + "&EmpPick=" + ((ListControl)gridViewRow.FindControl("dwEmployees")).SelectedItem.Text + "'", true);
                }

                if (!e.CommandName.Equals("excel"))
                    return;
                string str3 = e.CommandArgument.ToString();
                GridViewRow gridViewRow1 = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string selectedValue1 = ((ListControl)gridViewRow1.FindControl("dwEmployees")).SelectedValue;
                //if (string.IsNullOrEmpty(selectedValue) || selectedValue.Equals("-1"))
                //    this.msnMessage.LoadMessage("Seleccione el empleado encargado de la marcación para la liquidación No." + str2, ucMessage.MessageType.Error);
                //else
                ScriptManager.RegisterStartupScript(this.upMsg, this.Page.GetType(), "click", "location.href='" + this._pathReporPicking + str3 + "&EmpPick=" + ((ListControl)gridViewRow1.FindControl("dwEmployees")).SelectedItem.Text +  "&excel=" + "1" + "'", true);
            }
        }

        /// <summary>
        /// Actualizacion de grilla
        /// </summary>
        protected void refreshGridView()
        {
            this.initGrid();
        }

        /// <summary>
        /// Calculo de totales
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="dt"></param>
        /// <param name="printLabels"></param>
        protected void calculateTotals(GridView gv, DataTable dt, bool printLabels, bool showAlert)
        {
            try
            {
                if (dt == null || dt.Rows.Count <= 0)
                    return;
                var fAnonymousType3 = Enumerable.FirstOrDefault(Enumerable.Select(Enumerable.GroupBy<DataRow, DataTable>((IEnumerable<DataRow>)DataTableExtensions.AsEnumerable(dt), (Func<DataRow, DataTable>)(x => x.Table)), y =>
                {
                    var local_0 = new
                    {
                        x1 = Enumerable.Count<DataRow>((IEnumerable<DataRow>)y),
                        x2 = Enumerable.Sum<DataRow>((IEnumerable<DataRow>)y, (Func<DataRow, Decimal>)(x => DataRowExtensions.Field<Decimal>(x, "Cantidad"))),
                        x3 = Enumerable.Count<DataRow>(Enumerable.Where<DataRow>((IEnumerable<DataRow>)y, (Func<DataRow, bool>)(x => !string.IsNullOrEmpty(x["pin_employee"].ToString())))),
                        x4 = Enumerable.Sum<DataRow>(Enumerable.Where<DataRow>((IEnumerable<DataRow>)y, (Func<DataRow, bool>)(x => !string.IsNullOrEmpty(x["pin_employee"].ToString()))), (Func<DataRow, Decimal>)(x => DataRowExtensions.Field<Decimal>(x, "Cantidad")))
                    };
                    return local_0;
                }));
                gv.FooterRow.Cells[0].Text = "TOTALES:";
                gv.FooterRow.Cells[1].Text = fAnonymousType3.x1.ToString("N0");
                gv.FooterRow.Cells[6].Text = fAnonymousType3.x2.ToString("N0");

                // Pedidos sin asignacion de marcacion
                if (showAlert)
                {
                    int num = (fAnonymousType3.x1 - fAnonymousType3.x3);
                    if (num > 0)
                    {
                        string script = string.Empty;
                        script += "dialogInfo('Marcación de pedidos','Nuevos pedidos para marcación','Tiene <b> " + num + " nuevos pedidos </b> sin inicio o asignación de marcador.');";
                        System.Web.UI.ScriptManager.RegisterStartupScript(upMsg, Page.GetType(), "dialogInfo", script, true); 
                    }                    
                }

                if (printLabels)
                {
                    Label label1 = this.lblLiqInPick;
                    int num1 = fAnonymousType3.x3;
                    string str1 = num1.ToString("N0");
                    label1.Text = str1;
                    Label label2 = this.lblQtysInPick;
                    Decimal num2 = fAnonymousType3.x4;
                    string str2 = num2.ToString("N0");
                    label2.Text = str2;
                    Label label3 = this.lblLiqNotPick;
                    num1 = fAnonymousType3.x1 - fAnonymousType3.x3;
                    string str3 = num1.ToString("N0");
                    label3.Text = str3;
                    Label label4 = this.lblQtyNotPick;
                    num2 = fAnonymousType3.x2 - fAnonymousType3.x4;
                    string str4 = num2.ToString("N0");
                    label4.Text = str4;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Filtrado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void odsFilter_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            if (this.chkTypeFilter.Checked)
                e.InputParameters[(object)"dtObj"] = (object)this.getLigsPick();
            else
                e.InputParameters[(object)"dtObj"] = (object)this.getLiqsHalls();
        }

        /// <summary>
        /// Boton de realizar filtro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btFilter_Click(object sender, EventArgs e)
        {
            this.msnMessage.HideMessage();
            if (this.chkTypeFilter.Checked)
            {
                this.gvLiqPicking.DataSourceID = this.odsFilter.ID;
                this.gvLiqPicking.DataBind();
                this.gvLiqHall.DataSourceID = this.odsLiqsHall.ID;
                this.gvLiqHall.DataBind();
            }
            else
            {
                this.gvLiqPicking.DataSourceID = this.odsLiqsPick.ID;
                this.gvLiqPicking.DataBind();
                this.gvLiqHall.DataSourceID = this.odsFilter.ID;
                this.gvLiqHall.DataBind();
            }
        }

        /// <summary>
        /// Boton de refrescar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btRefresh_Click(object sender, EventArgs e)
        {
            this.msnMessage.HideMessage();
            this.txtFilter.Text = string.Empty;
            this.refreshGridView();
        }

        /// <summary>
        /// Carga de dropdownlist para seleccion de marcador
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvLiqPicking_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            try
            {
                ((BaseDataBoundControl)e.Row.FindControl("dwEmployees")).DataSource = (object)(DataTable)this.Session[this._nameSessionEmployees];
            }
            catch
            {
            }
        }

        /// <summary>
        /// Timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TimerOrders_Tick(object sender, EventArgs e)
        {
            if (chkAutoUpdate.Checked)
            {
                this.msnMessage.LoadMessage("Última actualizacion automatica:" + DateTime.Now.ToLongTimeString(), ucMessage.MessageType.Information);
                this.txtFilter.Text = string.Empty;
                this.refreshGridView();
            }            
        }

        #region < Ajax >

        [WebMethod]
        public static string ajaxGetInfoPicking(string noLiq)
        {
            string str = string.Empty;
            HttpContext.Current.Session["ShippingInfoObj"] = (object)"";
            Users user = (Users)HttpContext.Current.Session[Constants.NameSessionUser];

            DataTable dataTable = Picking.getInfoLiqPicking( noLiq).Tables[0];
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow dataRow = dataTable.Rows[0];
                TimeSpan timeSpan = DateTime.Now - Convert.ToDateTime(dataRow["pick_start"]);
                str = "<table cellpadding='2' cellspacing='2'><tr><td>Fecha de creación de liquidación:</td><td>" + (object)dataRow["datedesc"].ToString() + "</td></tr><tr><td>Fecha de liberación de la liquidación:</td><td>" + dataRow["datedesclear"].ToString() + "</td></tr><tr><td>Marcador:</td><td>" + dataRow["nameemployee"].ToString() + "</td></tr><tr><td>Fecha exacta de incio de marcación:</td><td>" + dataRow["pick_startdesc"].ToString() + "</td></tr><tr><td>Tiempo corrido:</td><td>" + (timeSpan.Days > 0 ? (string)(" " + timeSpan.Days + " Dias - ") : (string)"") + timeSpan.Hours + "Horas - " + timeSpan.Minutes + "Min - " + timeSpan.Seconds + "seg.</td></tr><tr><td>Número de liquidaciones asignAQUARELLAs a este marcador:</td><td>" + dataRow["noLiq"].ToString() + "</td></tr><tr><td>Número total de pares asignados a este marcador:</td><td>" + dataRow["ldn_qty"].ToString() + "</td></tr></table>";
            }
            return str;
        }

        [WebMethod]
        public static string ajaxGetInfoPickingEmpl()
        {
            string str1 = string.Empty;
            Users user = (Users)HttpContext.Current.Session[Constants.NameSessionUser];
            DataTable dataTable = Picking.getInfoPickingEmp().Tables[0];
            Decimal num1 = new Decimal(0);
            Decimal num2 = new Decimal(0);

            string str2;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                string str3 = str1 + "<table cellpadding='3' cellspacing='3'><tr><td><b>Empleado</b></td><td align='center'><b>Núm. Liquidaciones</b></td><td align='center'><b>Unidades</b></td></tr>";
                foreach (DataRow dataRow in (InternalDataCollectionBase)dataTable.Rows)
                {
                    num1 += Convert.ToDecimal(dataRow["no_Liq"]);
                    num2 += Convert.ToDecimal(dataRow["ldn_qty"]);
                    str3 = str3 + "<tr><td>" + dataRow["nameemployee"].ToString() + "</td><td align='center'>" + dataRow["no_Liq"].ToString() + "</td><td align='center'>" + dataRow["ldn_qty"].ToString() + "</td></tr>";
                }
                str2 = str3 + "<tr><td><b>Totales</b></td><td align='center'><b>" + num1.ToString() + "</b></td><td align='center'><b>" + num2.ToString() + "</b></td>" + "</table>";
            }
            else
                str2 = "No existe información sobre la marcación en bodega.";
            return str2;
        }

        #endregion

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            msnMessage.HideMessage() ;
            this.Session[this._nombreSession] = null;
            sbcargarcrystal();
        }
        public void PopulateValueCrystalReportI()
        {
            if (this.Session[this._nombreSession] == null)
            {
                DataTable dtventa = www.aquarella.com.pe.bll.Liquidations_Hdr.get_pedido_lidergrupo();

                if (dtventa.Rows.Count > 0)
                {
                    this.ventaValues = new ArrayList();                    
                    
                    foreach (DataRow dataRow in (InternalDataCollectionBase)dtventa.Rows)
                    {
                        string lider = dataRow["lider"].ToString();
                        string lider_documento = dataRow["lider_documento"].ToString();
                        string promotor = dataRow["promotor"].ToString();
                        string promotor_doc = dataRow["promotor_doc"].ToString();
                        string liq_id = dataRow["liq_id"].ToString();
                        Decimal cantidad =Convert.ToDecimal(dataRow["cantidad"].ToString());
                        string lider_direccion = dataRow["lider_direccion"].ToString();
                        this.ventaValues.Add((object)new Pedido_Lider_Grupo(lider,lider_documento,promotor,promotor_doc,liq_id,cantidad,lider_direccion));// ReporteVentas(lider, mes, dia, cliente, totalparesmonto, validanumero, año, Semana));
                    }
                }
                else
                {
                    this.msnMessage.LoadMessage("No hay datos para visualizar:" + DateTime.Now.ToLongTimeString(), ucMessage.MessageType.Information);
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
        private void sbcargarcrystal()
        {
            try
            {
                this.PopulateValueCrystalReportI();
                this.reportPath = this.Server.MapPath(this._pathFile);
                //this.reportPath = this._pathFile;
                this.ventasObjectsReport = new ReportDocument();
                this.ventasObjectsReport.Load(this.reportPath);

                this.ventasObjectsReport.SetDataSource((IEnumerable)this.ventaValues);

                //if (this.Request.Params["ShowReportOnWeb"] == null)
                this.ventasObjectsReport.ExportToHttpResponse(ExportFormatType.PortableDocFormat, this.Response, true, "lider_liq_x_Grupo");

                //this.ventasObjectsReport.E


                //this.crvreport.ReportSource = (object)this.ventasObjectsReport;
            }
            catch
            {

            }

        }
    }
}
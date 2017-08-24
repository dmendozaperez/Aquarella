using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.UserControl;
using System.Data;
using System.Web.Services;
namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class Anular_Pedido_Marcacion : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "InfoLiqPicking", _nameSessionEmployees = "InfoEmployees";
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
        protected void getEmployees(string co, string ware, string idProfes)
        {
            this.Session[this._nameSessionEmployees] = (object)Employees.getEmployeesByCharge("07");
        }
        protected void initGrid()
        {
            this.Session[this._nameSessionData] = (object)Liquidations_Hdr.getLiquidationPicking_marcacion().Tables[0];
            this.gvLiqPicking.DataSourceID = this.odsLiqsPick.ID;
            this.gvLiqPicking.DataBind();
            this.gvLiqHall.DataSourceID = this.odsLiqsHall.ID;
            this.gvLiqHall.DataBind();
        }
        public DataTable getLigsPick()
        {
            DataTable dt1;
            try
            {
                DataTable dt = (DataTable)this.Session[this._nameSessionData];
                if (dt == null || dt.Rows.Count == 0)
                    return new DataTable();

                dt1 = DataTableExtensions.CopyToDataTable<DataRow>((IEnumerable<DataRow>)EnumerableRowCollectionExtensions.Where<DataRow>(DataTableExtensions.AsEnumerable(dt), (Func<DataRow, bool>)(x => DataRowExtensions.Field<string>(x, "liq_estid").Equals("PM") || DataRowExtensions.Field<string>(x, "liq_estid").Equals("PF"))));
                return dt1;

                //return DataTableExtensions.CopyToDataTable<DataRow>((IEnumerable<DataRow>)EnumerableRowCollectionExtensions.Where<DataRow>(DataTableExtensions.AsEnumerable(dt), (Func<DataRow, bool>)(x => DataRowExtensions.Field<string>(x, "liq_estid").Equals("PF"))));
            }
            catch
            {
                return new DataTable();
            }
        }
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
        protected void gvLiqHall_DataBound(object sender, EventArgs e)
        {
            this.calculateTotals(this.gvLiqHall, this.getLiqsHalls(), false, chkAutoUpdate.Checked);
        }
        protected void gvLiqPicking_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType != DataControlRowType.DataRow)
                    return;
                string str = DataBinder.Eval(e.Row.DataItem, "pin_employee").ToString();
                ImageButton imageButton1 = (ImageButton)e.Row.FindControl("ibanular");
                imageButton1.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de ANULAR el pedido con número : -" + DataBinder.Eval(e.Row.DataItem, "liq_id") + "-, Perteneciente A : -" + (string)DataBinder.Eval(e.Row.DataItem, "nombres") + "- ?')");
                if (!string.IsNullOrEmpty(str))
                {
                    DropDownList dropDownList = (DropDownList)e.Row.FindControl("dwEmployees");
                    dropDownList.SelectedValue = str;
                    dropDownList.Enabled = false;
                    imageButton1.Enabled = false;
                    imageButton1.ToolTip = "Liquidación en proceso de marcación.";
                    imageButton1.Style.Value = "filter: alpha(opacity=50); opacity: .5;";
                }
            }
            catch
            {
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
            if (e.CommandName.Equals("anularpedido"))
            {
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _noLiq = e.CommandArgument.ToString();                            
               
                    str1 = string.Empty;
                    try
                    {
                        str1 = Picking.anular_liquidacion_marcacion(_noLiq);

                        // Async
                        //Log_Transaction.registerUserInfo(_user, "CREATE PICKING:" + _noLiq);

                        this.msnMessage.LoadMessage("Se Anulo la marcación para la liquidación No." + _noLiq + ".", ucMessage.MessageType.Information);
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error al anular el pedido  No." + _noLiq + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }
                    this.refreshGridView();
                
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
                        script += "dialogInfo('Marcación de pedidos','Nuevos pedidos en marcación','Tiene <b> " + num + " nuevos pedidos </b>');";
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

            DataTable dataTable = Picking.getInfoLiqPicking(noLiq).Tables[0];
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
    }
}
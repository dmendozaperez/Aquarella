using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.BLL;
using System.Data;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.UserControl;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class panelSeparatedOrders : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "InfoLiqSeparated", _nameSessionDataFiltered = "InfoLiqSeparatedFilter";
        DataSet _dsResult;
        SortDirection _sortDir;

        /// <summary>
        /// Inicio
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
                if ((_user._usu_tip_id == "02"))
                {
                     Utilities.logout(Page.Session, Page.Response);
                }
                else
                {
                      formForEmployee();
                }
                //if (_user._usv_employee)
                  
               
            }
        }

        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        protected void formForEmployee()
        {
            //if (!string.IsNullOrEmpty(_user._usv_warehouse))
            //{
                if (!string.IsNullOrEmpty(_user._usv_area))
                {
                    getSource(_user._usv_area);
                }
                else
                    msnMessage.LoadMessage("No se encuentra asociado a ninguna area", UserControl.ucMessage.MessageType.Error);
            //}
            //else
            //    msnMessage.LoadMessage("No se encuentra asociado a ninguna bodega", UserControl.ucMessage.MessageType.Error);
        }

        /// <summary>
        /// Obtener informacion desde la fuente de datos
        /// </summary>
        /// <param name="co"></param>
        /// <param name="ware"></param>
        /// <param name="area"></param>
        protected void getSource(string area)
        {
            _dsResult = Liquidations_Hdr.getSeparateLiquidations(area);
            Session[_nameSessionData] = _dsResult.Tables[0];
            GridViewSourceType = "originalsource";
            gvSepLiq.DataSource = _dsResult;            
            refreshGridView();
        }

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
                                 x2 = y.Sum(x => x.Field<decimal>("totalpares")),
                                 x3 = y.Sum(x => x.Field<decimal>("totalpares")),
                                 x4 = y.Sum(x => x.Field<decimal>("subtotal"))
                             }).FirstOrDefault();

                    gv.FooterRow.Cells[0].Text = "TOTALES:";
                    gv.FooterRow.Cells[1].Text = t.x1.ToString("N0");                    

                    lblQtysLiq.Text = t.x2.ToString("N0");
                    lblQtysOrder.Text = t.x3.ToString("N0");
                    lblNumLiq.Text = t.x1.ToString("N0");
                    lblNumLiq.Text = t.x1.ToString("N0");
                    lblLiqValue.Text = t.x4.ToString("C0");
                }
            }
            catch { }
        }

        protected void btFilter_Click(object sender, EventArgs e)
        {
            string filterValue = txtFilter.Text.Trim();
            DataTable dt = Utilities.getFilterObject((DataTable)Session[_nameSessionData], 0, "Liq_Id", "nombres",
                "Are_Descripcion", filterValue, filterValue, filterValue, string.Empty);
            GridViewSourceType = "filtered";
            Session[_nameSessionDataFiltered] = dt;
            gvSepLiq.DataSource = dt;
            refreshGridView();
        }

        protected void btRefresh_Click(object sender, EventArgs e)
        {
            txtFilter.Text = string.Empty;
            getSource(_user._usv_area);            
        }

        #region < Paginacion, ordenacion y administracion del GridView >

        /// <summary>
        /// Refresh
        /// </summary>
        private void refreshGridView()
        {
            gvSepLiq.DataBind();
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

        /// <summary>
        /// Manejo de los campos de ordenacion o exp. de ordenacion
        /// </summary>
        public string GridViewSourceType
        {
            get
            {
                if (ViewState["GridViewSourceType"] == null)
                    ViewState["GridViewSourceType"] = string.Empty;
                return (string)ViewState["GridViewSourceType"];
            }
            set { ViewState["GridViewSourceType"] = value; }
        }

        #endregion

        protected void gvSepLiq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pageAndSort(gvSepLiq, GridViewSortExpresion, getSource(), e.NewPageIndex);
        }

        protected void gvSepLiq_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridViewSortExpresion = e.SortExpression;
            pageAndSort(gvSepLiq, GridViewSortExpresion, getSource(), gvSepLiq.PageIndex);
        }

        protected DataTable getSource()
        {
            // Filtered source
            if (!string.IsNullOrEmpty(GridViewSourceType) && GridViewSourceType.Equals("filtered"))
                return (DataTable)Session[_nameSessionDataFiltered];
            else//originalsource
                return (DataTable)Session[_nameSessionData];
        }

        protected void gvSepLiq_DataBound(object sender, EventArgs e)
        {
            calculateTotals(gvSepLiq, getSource());
        }

        /// <summary>
        /// Click boton de export grid a archivo de excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvSepLiq.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvSepLiq);
            gridStatus();
            //  pass the grid that for exporting ...
            GridViewExportUtil.Export("LiquidacionesSeparAQUARELLAs.xls", gvSepLiq);
        }

        protected void gridStatus()
        {
            pageAndSort(gvSepLiq, GridViewSortExpresion, getSource(), gvSepLiq.PageIndex);
        }

        protected void gvSepLiq_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.msnMessage.HideMessage();
            if (e.CommandName == "AddDay")
            {
                string _noLiquidation = Convert.ToString(e.CommandArgument);
                if (Liquidations_Hdr.updateExpirationDateOnLiq( _noLiquidation, this._user._usn_userid).Equals("-1"))
                    this.msnMessage.LoadMessage("La liquidación " + _noLiquidation + ", no puede modificarce.", ucMessage.MessageType.Error);
                else
                    this.msnMessage.LoadMessage("Proceso realizado satisfactoriamente sobre la liquidación numero : <b>" + _noLiquidation + "</b>", ucMessage.MessageType.Information);
                this.getSource( this._user._usv_area);
            }
            else
            {
                if (!(e.CommandName == "MinusDay"))
                    return;
                string _noLiquidation = Convert.ToString(e.CommandArgument);
                if (Liquidations_Hdr.updateExpirationDateOnLiq(_noLiquidation, this._user._usn_userid).Equals("-1"))
                    this.msnMessage.LoadMessage("La liquidación " + _noLiquidation + ", no puede modificarce.", ucMessage.MessageType.Error);
                else
                    this.msnMessage.LoadMessage("Proceso realizado satisfactoriamente sobre la liquidación numero : <b>" + _noLiquidation + "</b>", ucMessage.MessageType.Information);
                this.getSource( this._user._usv_area);
            }
        }
    }
}
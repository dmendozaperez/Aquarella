using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class panelLiqForInvoice : System.Web.UI.Page
    {
        Users _user;

        string _nameSessDataOri = "InfoLiqs", _nameSessDataFiltered = "InfoLiqsFiltered";

        #region < Funciones de inicio >

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
                Session[_nameSessDataOri] = null;

                if ((_user._usu_tip_id == "02"))
                {
                    Utilities.logout(Page.Session, Page.Response);
                }
                else
                {
                    formForEmployee();
                }
                //if (_user._usv_employee)
                //    formForEmployee();
                //else Utilities.logout(Page.Session, Page.Response);
            }
        }

        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        protected void formForEmployee()
        {
            setParametersDataSources(_user._usv_area);

            // Enlazar datoS
            refreshGridView(odsLiqs.ID);
        }

        /// <summary>
        /// Set de variables del data source
        /// </summary>
        /// <param name="co"></param>
        /// <param name="ware"></param>
        /// <param name="area"></param>
        protected void setParametersDataSources(string area)
        {
           
            odsLiqs.SelectParameters[0].DefaultValue = area;
        }

        #endregion

        #region < Carga de combos >

        /// <summary>
        /// Carga de combo
        /// </summary>
        /// <param name="dt"></param>
        private void loadDwStatus(DataTable dt)
        {            
            if (dt == null)
                return;

            // Calcular totales
            //calculateTotals(gvPortfolioStores, dt.Table);

            // Select Distinc Brands in order
            var queryDistinct = (from dRow in dt.AsEnumerable()
                                 orderby dRow["Liq_EstId"]
                                 select new { status = dRow["Liq_EstId"] }).Distinct();

            this.dwStatus.Items.Clear();
            this.dwStatus.Items.Add(new ListItem("Seleccione estado", "-1"));            
            this.dwStatus.DataSource = queryDistinct;
            this.dwStatus.DataBind();
        }

        #endregion

        #region < Eventos sobre el grid >

        /// <summary>
        /// Refrescar grid con la fuente principal de datos
        /// </summary>
        protected void refreshGridView(string odsId)
        {
            gvLiqs.DataSourceID = odsId;
            gvLiqs.DataBind();
        }

        protected void gvLiqs_DataBound(object sender, EventArgs e)
        {
            //
            int type;
            DataTable dt = getDataSource(gvLiqs, out type);
            if(type != -1)
                loadDwStatus(dt);
            calculateTotals(gvLiqs,dt);
        }

        /// <summary>
        /// Calculo de totales
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="dt"></param>
        protected void calculateTotals(GridView gv,DataTable dt)
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
                                 x3 = y.Sum(x => x.Field<decimal>("Paq_Cantidad")),
                                 x4 = y.Sum(x => x.Field<decimal>("liq_value"))
                             }).FirstOrDefault();

                    gv.FooterRow.Cells[0].Text = "TOTALES:";
                    gv.FooterRow.Cells[4].Text = t.x1.ToString("N0");
                    gv.FooterRow.Cells[9].Text = t.x2.ToString("N0");
                    gv.FooterRow.Cells[10].Text = t.x3.ToString("N0");
                    gv.FooterRow.Cells[11].Text = t.x4.ToString("N0");
                }
            }
            catch { }
        }

        #endregion

        #region < Eventos sobre botones >

        protected void btFilter_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            refreshGridView(odsFilter.ID);
        }

        protected void btRefresh_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            txtFilter.Text = string.Empty;
            refreshGridView(odsLiqs.ID);
        }

        #endregion

        #region < Eventos sobre data source >

        protected void odsLiqs_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessDataOri] = dt;
            }
            catch
            { }
        }

        protected void odsFilter_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessDataOri];
        }

        protected void odsFilter_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataTable)e.ReturnValue);
                Session[_nameSessDataFiltered] = dt;
            }
            catch
            { }
        }

        protected void odsFilter2_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessDataOri];
        }

        /// <summary>
        /// Devolver la informacion segun el datasource relacionado a la grilla actualmente; esto para efectos de totalizados etc
        /// </summary>
        /// <param name="gv"></param>
        /// <returns></returns>
        private DataTable getDataSource(GridView gv, out int type)
        {
            type = 1;
            if (gv.DataSourceID.Equals(odsLiqs.ID))
                return (DataTable)Session[_nameSessDataOri];
            if (gv.DataSourceID.Equals(odsFilter.ID))
                return (DataTable)Session[_nameSessDataFiltered];
            else
            {
                type = -1;
                return (DataTable)Session[_nameSessDataFiltered];
            }
        }

        #endregion

        protected void dwStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!((DropDownList)sender).SelectedValue.Equals("-1"))
                refreshGridView(odsFilter2.ID);
        }

        #region < Exportar Excel>

        /// <summary>
        /// Exportar Grid a Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvLiqs.AllowPaging = false;            
            gvLiqs.ShowFooter = false;
            GridViewExportUtil.removeFormats(ref gvLiqs);
            gvLiqs.DataSourceID = string.Empty;
            DataTable dt = (DataTable)Session[_nameSessDataOri];
            gvLiqs.DataSource = dt;
            gvLiqs.DataBind();

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export("Info_Liq_" + DateTime.Today.ToShortDateString() + "_.xls", gvLiqs);
        }

        #endregion
    }
}
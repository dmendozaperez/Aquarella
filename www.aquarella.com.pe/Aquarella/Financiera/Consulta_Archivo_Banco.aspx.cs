using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class Consulta_Archivo_Banco : System.Web.UI.Page
    {
        Users _user;
        string _nameSessDataOri = "InfoLiqs", _nameSessDataFiltered = "InfoLiqsFiltered";
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
        protected void formForEmployee()
        {
            setParametersDataSources(_user._usv_area);

            // Enlazar datoS
            refreshGridView(odsbanco.ID);
        }
        private DataTable getDataSource(GridView gv, out int type)
        {
            type = 1;
            if (gv.DataSourceID.Equals(odsbanco.ID))
                return (DataTable)Session[_nameSessDataOri];
            if (gv.DataSourceID.Equals(odsFilter.ID))
                return (DataTable)Session[_nameSessDataFiltered];
            else
            {
                type = -1;
                return (DataTable)Session[_nameSessDataFiltered];
            }
        }
        protected void setParametersDataSources(string area)
        {

            //odsLiqs.SelectParameters[0].DefaultValue = area;
        }
        protected void refreshGridView(string odsId)
        {
            gvbanco.DataSourceID = odsId;
            gvbanco.DataBind();
        }
        protected void btFilter_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            refreshGridView(odsFilter.ID);
        }

        protected void btRefresh_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            txtFilter.Text = string.Empty;
            refreshGridView(odsbanco.ID);
        }

        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvbanco.AllowPaging = false;
            gvbanco.ShowFooter = false;
            GridViewExportUtil.removeFormats(ref gvbanco);
            gvbanco.DataSourceID = string.Empty;
            DataTable dt = (DataTable)Session[_nameSessDataOri];
            gvbanco.DataSource = dt;
            gvbanco.DataBind();

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export("Info_Liq_" + DateTime.Today.ToShortDateString() + "_.xls", gvbanco);
        }

        protected void gvLiqs_DataBound(object sender, EventArgs e)
        {
            int type;
            DataTable dt = getDataSource(gvbanco, out type);
            if (type != -1)
                loadDwStatus(dt);
            //calculateTotals(gvLiqs, dt);

        }
        private void loadDwStatus(DataTable dt)
        {
            //if (dt == null)
            //    return;

            //// Calcular totales
            ////calculateTotals(gvPortfolioStores, dt.Table);

            //// Select Distinc Brands in order
            //var queryDistinct = (from dRow in dt.AsEnumerable()
            //                     orderby dRow["Liq_EstId"]
            //                     select new { status = dRow["Liq_EstId"] }).Distinct();

            //this.dwStatus.Items.Clear();
            //this.dwStatus.Items.Add(new ListItem("Seleccione estado", "-1"));
            //this.dwStatus.DataSource = queryDistinct;
            //this.dwStatus.DataBind();
        }
        protected void dwStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void odsbanco_Selected(object sender, ObjectDataSourceStatusEventArgs e)
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
    }
}
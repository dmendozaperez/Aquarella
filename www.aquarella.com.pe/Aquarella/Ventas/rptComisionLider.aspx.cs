using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class rptComisionLider : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";

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

                btConsult_Click(btConsult, new EventArgs());

                //if ((_user._usu_tip_id == "01") || (_user._usu_tip_id == "03"))
                //{
                //    formForCustomer();
                //}
                //else
                //{
                //    formForEmployee();
                //}

                //if (_user._usv_employee == true && _user._usv_area == "%%")
                //    formForEmployee();
                //else if (_user._usv_employee == true && _user._usv_area != "%%")
                //    formForCustomer();
            }
        }

        protected void cargarLider()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de lideres        
            dwCustomers.Focus();
            dwCustomers.DataSource = Area.getAllAreas(_user._asesor);
            dwCustomers.DataBind();
        }


        private void formForEmployee()
        {
            try
            {
                //odsReturns.SelectParameters[0].DefaultValue = _user._usv_co;
                odsReturns.SelectParameters[0].DefaultValue = dwCustomers.SelectedValue;
                odsReturns.SelectParameters[1].DefaultValue = _user._asesor;
            }
            catch
            {
                return;
            }
        }

        protected void formForCustomer()
        {
            // Ocultar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = false;
            //
            setParamsDataSource(_user._usv_area.ToString());
            //
            //refreshGrid();
        }

        protected void setParamsDataSource(string idarea)
        {
            //odsReturns.SelectParameters[0].DefaultValue = co;
            odsReturns.SelectParameters[0].DefaultValue = idarea;
            odsReturns.SelectParameters[1].DefaultValue = _user._asesor;
            //odsReturns.SelectParameters[3].DefaultValue = _user._asesor;
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {


            if ((_user._usu_tip_id == "01") || (_user._usu_tip_id == "03"))
            {
                formForCustomer();
            }
            else
            {
                formForEmployee();
            }

            //
            //if (_user._usv_employee == true && _user._usv_area == "%%")
            //    formForEmployee();
            //else if (_user._usv_employee == true && _user._usv_area != "%%")
            //    formForCustomer();
            gvReturns.DataSourceID = odsReturns.ID;
            gvReturns.DataBind();
        }

        protected void odsReturns_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessionData] = dt;
            }
            catch
            { }
        }


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
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "ComisionLider";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }

        #endregion
    }
}
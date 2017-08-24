using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Maestros;
namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class Saldo_Cliente : System.Web.UI.Page
    {
         Users _user;
        string _nameSessionData = "_ReturnData";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                cargarLider();
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

                if ((_user._usu_tip_id == "02"))
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
            }
        }
             protected void cargarLider()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de lideres        
            dwCustomers.Focus();
            dwCustomers.DataSource = Area.getAllclientes();
            dwCustomers.DataBind();


            dwconcepto.DataSource = Concepts.getconcepto();
            dwconcepto.DataBind();
        }


        private void formForEmployee()
        {
            try
            {
                _user = (Users)Session[Constants.NameSessionUser];
                string _tipo_user = "";
                if (_user._usu_tip_id == "08")
                {
                    _tipo_user = "08";
                }
                //odsReturns.SelectParameters[0].DefaultValue = _user._usv_co;
                odsReturns.SelectParameters[0].DefaultValue = dwCustomers.SelectedValue;
                odsReturns.SelectParameters[1].DefaultValue = dwconcepto.SelectedValue;
                odsReturns.SelectParameters[4].DefaultValue = _tipo_user;
                gvReturns.DataSourceID = odsReturns.ID;
                gvReturns.DataBind();
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
            setParamsDataSource(_user._usv_area.ToString(), dwconcepto.SelectedValue);
            gvReturns.DataSourceID = odsReturns.ID;
            gvReturns.DataBind();
            //
            //refreshGrid();
        }
         protected void setParamsDataSource(string bas_id,string con_id)
        {
            _user = (Users)Session[Constants.NameSessionUser];
            string _tipo_user = "";
            if (_user._usu_tip_id == "08")
            {
                _tipo_user = "08";
            }
            //odsReturns.SelectParameters[0].DefaultValue = co;
            odsReturns.SelectParameters[0].DefaultValue = bas_id;
            odsReturns.SelectParameters[1].DefaultValue = con_id;
            odsReturns.SelectParameters[4].DefaultValue = _tipo_user;
        }
         protected void btConsult_Click(object sender, EventArgs e)
        {


            if ((_user._usu_tip_id == "02"))
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
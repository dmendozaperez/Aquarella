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
using System.Text;
using System.IO;


namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class reportOpg : System.Web.UI.Page
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
                dwconcepto.DataSource = Concepts.getconceptoGratuito();
                dwconcepto.DataBind();
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

                ConsultaGratuito();

                //if (_user._usv_employee == true && _user._usv_area == "%%")
                //    formForEmployee();
                //else if (_user._usv_employee == true && _user._usv_area != "%%")
                //    formForCustomer();
            }
        }
  

      

      
        

        protected void ConsultaGratuito()
        {
           
                string tipo = dwconcepto.SelectedValue;
                DataSet dsResultLiq = Coordinator.getOrdLiqOpgGratuitas(Convert.ToDateTime(txtDateStart.Text), Convert.ToDateTime(txtDateEnd.Text), tipo);
                gvReturns.DataSource = dsResultLiq.Tables[0];
                gvReturns.DataBind();
                Session[_nameSessionData] = dsResultLiq.Tables[0];

        }
        protected void btConsult_Click(object sender, EventArgs e)
        {

            ConsultaGratuito();
        }

        protected void gvReturns_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvReturns.PageIndex = e.NewPageIndex;

            DataTable dt1 = new DataTable();

            dt1 = (DataTable)Session[_nameSessionData];
            gvReturns.DataSource = dt1;
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
            //gvReturns.AllowPaging = false;
            //GridViewExportUtil.removeFormats(ref gvReturns);
            //gvReturns.DataBind();

            //string nameFile = "OperacionesGratuitas";

            ////  pass the grid that for exporting ...
            //GridViewExportUtil.Export(nameFile + ".xls", gvReturns);

            DataTable dt = (DataTable)Session[_nameSessionData];
            GridViewExportUtil.ExportarExcel(dt, "", "2", "OPeracionesGratuitas");

       }

        #endregion
    }

    
}
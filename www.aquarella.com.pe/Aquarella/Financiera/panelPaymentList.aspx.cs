using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class panelPaymentList : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "InfoLiqPicking";

        #region < Eventos del Load >

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
                       formForCustomer();
                }
                else
                {
                    formForEmployee();
                }
                //if (_user._usv_employee)
                //    formForEmployee();
                //else if (_user._usv_customer)
                 
            }
        }

        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        protected void formForEmployee()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de coordinadores
            DataSet dsCustomers = Coordinator.getCoordinators(_user._usv_area,_user._asesor);
            dwCustomers.Focus();
            // Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            dwCustomers.DataSource = dsCustomers;
            dwCustomers.DataBind();
        }

        /// <summary>
        /// Preparar formulario para cliente
        /// </summary>
        protected void formForCustomer()
        {
            // Ocultar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = false;

            //
            setParamsDataSource( _user._usn_userid.ToString());            
            //
            refreshGrid();
        }

        /// <summary>
        /// Set de los parametros necesarion en el datasource para realizar la consulta
        /// </summary>
        /// <param name="co"></param>
        /// <param name="idCust"></param>
        protected void setParamsDataSource(string idCust)
        {            
            odsPays.SelectParameters[0].DefaultValue = idCust;
        }

        #endregion

        /// <summary>
        /// Cambio en la seleccion de cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dwCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            // Nuevo cliente seleccionado
            string selCust = ((DropDownList)sender).SelectedValue;

            /// Verificar que sea una selección valida
            if (!string.IsNullOrEmpty(selCust) && selCust != "-1")
            {
                setParamsDataSource(selCust);                
                refreshGrid();
            }
        }

        /// <summary>
        /// Refrescar datos
        /// </summary>
        protected void refreshGrid()
        {
            gvPays.DataSourceID = odsPays.ID;
            gvPays.DataBind();
        }

        #region < Eventos sobre botones >

        protected void btFilter_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            gvPays.DataSourceID = odsFilter.ID;
            gvPays.DataBind();
        }

        protected void btRefresh_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();
            txtFilter.Text = string.Empty;
            refreshGrid();
        }

        #endregion

        #region < Eventos sobre data source >
        
        protected void odsPays_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessionData] = dt;
            }
            catch
            { }
        }

        protected void odsFilter_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["dtObj"] = (object)Session[_nameSessionData];
        }

        #endregion

        #region < Exportar Excel>

        /// <summary>
        /// Exportar Grid a Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvPays.AllowPaging = false;
            gvPays.ShowFooter = false;
            GridViewExportUtil.removeFormats(ref gvPays);
            gvPays.DataSourceID = string.Empty;
            DataTable dt = (DataTable)Session[_nameSessionData];
            gvPays.DataSource = dt;
            gvPays.DataBind();

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export("Payments_.xls", gvPays);
        }

        #endregion
    }
}
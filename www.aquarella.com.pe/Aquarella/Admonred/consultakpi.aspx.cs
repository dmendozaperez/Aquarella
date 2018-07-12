using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.Bll.Admonred;

namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class consultakpi : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnDataKPI";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                //rbt_G.Checked = true;
                cargarasesor();
                cargarLider();
                txtDateStart.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDateEnd.Text = DateTime.Today.ToString("dd/MM/yyyy");

               // btConsult_Click(btConsult, new EventArgs());

            }
        }
        private void cargarasesor()
        {
            dwasesor.DataSource = Asesor.getasesor();
            dwasesor.DataTextField = "Nombres";
            dwasesor.DataValueField = "Bas_Aco_Id";
            dwasesor.DataBind();
        }
        protected void cargarLider()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de lideres        
            dwCustomers.Focus();
            dwCustomers.DataSource = Area.getAllAreas(_user._asesor);
            dwCustomers.DataBind();

            if (_user._asesor.Length>0)
            {
                dwasesor.SelectedValue = _user._asesor;
                dwasesor.Enabled = false;
            }

        }
        protected void btConsult_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        protected void Consultar()
        {
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
            gvReturns.DataSourceID = odsReturns.ID;
            gvReturns.DataBind();
        }


        protected void onchange_lider(object sender, EventArgs e)
        {
            if (rbt_L.Checked)
            {
                dwasesor.SelectedValue = "";
                dwasesor.Enabled = false;
                dwCustomers.Enabled = true;
                //Consultar();
            }
           
         
        }

        protected void onchange_Asesor(object sender, EventArgs e)
        {
            if (rbt_A.Checked)
            {
                dwCustomers.SelectedValue = "-1";
                dwCustomers.Enabled = false;
                dwasesor.Enabled = true;
                //Consultar();
            }
           
                     
        }

        protected void onchange_gral(object sender, EventArgs e)
        {
            if (rbt_G.Checked)
            {
                dwCustomers.Enabled = true;
                dwasesor.Enabled = true;
                //Consultar();
            }

        }

        private void formForEmployee()
        {
            try
            {
                string gral = "N";
                string ase_lid = "0";

                if (rbt_G.Checked)
                    gral = "S";

                if (rbt_A.Checked)
                    ase_lid = "1"; ;


                odsReturns.SelectParameters[0].DefaultValue = dwCustomers.SelectedValue;
                odsReturns.SelectParameters[1].DefaultValue =  (_user._asesor.Length>0)?_user._asesor:dwasesor.SelectedValue;
                odsReturns.SelectParameters[2].DefaultValue = ase_lid;
                odsReturns.SelectParameters[3].DefaultValue = gral;
            }
            catch
            {
                return;
            }
        }
        protected void setParamsDataSource(string co, string idCust)
        {
            string gral = "N";
            string ase_lid = "0";

            if (rbt_G.Checked)
                gral = "S";

            if (rbt_A.Checked)
                ase_lid = "1"; ;

            odsReturns.SelectParameters[0].DefaultValue = idCust;
            odsReturns.SelectParameters[1].DefaultValue = (_user._asesor.Length > 0) ? _user._asesor : dwasesor.SelectedValue;
            odsReturns.SelectParameters[2].DefaultValue = ase_lid;
            odsReturns.SelectParameters[3].DefaultValue = gral;
        }
        protected void formForCustomer()
        {
            // Ocultar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = false;
            //
            setParamsDataSource(_user._usv_co, _user._usv_area.ToString());
            //
            //refreshGrid();
        }
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "ResultadoKPI";

            Decimal[] columna = { 1 };
            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns, false, columna);
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
        protected DataTable getSource()
        {
            // Chequeado es ventas por semana y categoria
            /*if (chkGroupByWeek.Checked)
                return (DataTable)Session[_nameSessionData];
            // No chequeado es ventas netas entre las fechas dAQUARELLAs
            else*/
            return (DataTable)Session[_nameSessionData];
        }
    }
}
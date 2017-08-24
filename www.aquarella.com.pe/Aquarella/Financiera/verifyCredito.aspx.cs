using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;

namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class verifyCredito : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "InfoLiqPicking";

        #region < Funciones de inicio >

        /// <summary>
        /// Load de la pagina
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
                //    formForEmployee();
                //else
                //    Utilities.logout(Page.Session, Page.Response);
            }
        }

        /// <summary>
        /// Preparar formulario para empleado
        /// </summary>
        protected void formForEmployee()
        {
            Payments.sbactualizar();
            loadOds();
        }

        #endregion

        #region < Eventos sobre botones >

        //protected void btFilter_Click(object sender, EventArgs e)
        //{
        //    msnMessage.HideMessage();
        //    gvListPays.DataSourceID = odsFilter.ID;
        //    gvListPays.DataBind();
        //}

        protected void btRefresh_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();            
            Payments.sbactualizar();
            refreshGridView();
        }

        #endregion

        #region < Eventos sobre el grid >

        /// <summary>
        /// Aplicar un nuevo estado a un pago
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvListPays_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            msnMessage.HideMessage();
            if (e.CommandName.Equals("ApplyUpdate"))
            {
                //
                // Inicio de marcacion
                string noPay = e.CommandArgument.ToString();

                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                string newStatus = ((DropDownList)row.FindControl("dwStatusPay")).SelectedValue;

                if (string.IsNullOrEmpty(newStatus) || newStatus.Equals("-1"))
                    msnMessage.LoadMessage("Seleccione el NUEVO ESTADO que se le aplicará al pago No." + noPay, UserControl.ucMessage.MessageType.Error);
                else
                {
                    bool answ = Payments.updatepagocredito(noPay, newStatus,_user._bas_id);

                    if (!answ)
                        msnMessage.LoadMessage("Error intentando aplicar el nuevo estado al pedido No." + noPay + "; por favor intente de nuevo.", UserControl.ucMessage.MessageType.Error);
                    else
                    {
                        msnMessage.LoadMessage("Nuevo estado aplicado correctamente al Pedido No." + noPay + ".", UserControl.ucMessage.MessageType.Information);
                        // Async 
                        //Log_Transaction.registerUserInfo(_user, "UPDATE PAYMENT NO.:"+ noPay +" STATUS:" + newStatus);
                    }

                    refreshGridView();
                }
            }
        }

        /// <summary>
        /// Refrescar grid con el datasource principal
        /// </summary>
        protected void refreshGridView()
        {
            //
            gvListPays.DataSourceID = odsPays.ID;
            gvListPays.DataBind();
        }

        protected void gvListPays_DataBound(object sender, EventArgs e)
        {
            calculateTotals(gvListPays, (DataTable)Session[_nameSessionData]);
        }

        protected void gvListPays_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton ibApplyUpdate = (ImageButton)e.Row.FindControl("ibApplyUpdate");

                ibApplyUpdate.Attributes.Add("onclick", "javascript:return " +
                "confirm('¿Esta seguro de APLICAR el cambio de estado a la liquidacion No. " +
                DataBinder.Eval(e.Row.DataItem, "Liquidacion") + ", Perteneciente A : -" +
                DataBinder.Eval(e.Row.DataItem, "Promotor") + "- ?')");
            }
        }

        #endregion

        #region < Eventos sobre data source >

        protected void loadOds()
        {
            // Listado de pagos
            //odsPays.SelectParameters[0].DefaultValue = _user._usv_co;
            //odsPays.SelectParameters[2].DefaultValue = _user._usv_warehouse;
            //odsPays.SelectParameters[3].DefaultValue = _user._usv_area;

            // Actualizacion de pagos
            //odsPays.UpdateParameters[0].DefaultValue = _user._usv_co;

            // Estado de pagos
            //odsStatusPays.SelectParameters[0].DefaultValue = _user._usv_co;
            odsStatusPays.SelectParameters[0].DefaultValue = "5";//Constants.IdStatuscredito;

            //
            gvListPays.DataSourceID = odsPays.ID;
            gvListPays.DataBind();
        }

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
                                 x2 = y.Sum(x => x.Field<decimal>("pan_amount"))
                             }).FirstOrDefault();

                    gv.FooterRow.Cells[0].Text = "TOTALES:";
                    gv.FooterRow.Cells[6].Text = t.x1.ToString();
                    gv.FooterRow.Cells[7].Text = t.x2.ToString(System.Configuration.ConfigurationManager.AppSettings["kCurrency"]);
                }
            }
            catch { }
        }

        /// <summary>
        /// Click boton de export grid a archivo de excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvListPays.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvListPays);
            gvListPays.DataBind();

            string nameFile = "ListPayments_"+ DateTime.Now;

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvListPays);
        }
    }
}
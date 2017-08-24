using System;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Control;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.BLL.Control;


namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class paymentsUpd : System.Web.UI.Page
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
                fillDropDawn();
                Session[_nameSessionData] = null;

                if ((_user._usu_tip_id == "02"))
                {
                    this.formForCustomer();
                }
                else
                {
                    this.formForEmployee();
                }

                //if (_user._usv_employee)
                //    formForEmployee();
                //else if (_user._usv_customer)
                //    formForCustomer();
            }
        }

        protected void fillDropDawn()
        {

            DataTable datos = Functions.Get_PAYMENT_STATUS();

            datos.DefaultView.Sort = "Est_Descripcion";
            DDPadre2.DataSource = datos;
            DDPadre2.DataTextField = "Est_Descripcion";
            DDPadre2.DataValueField = "Est_Id";
            DDPadre2.DataBind();
            //DDPadre2.Items.Insert(0, new ListItem("(vacio)", ""));
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
                setParamsDataSource( selCust);
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

        [WebMethod()]
        public static string ajaxUpdateFunction( string FUN_ID, string FUV_NAME, string FUV_DESCRIPTION, string _FUN_ORDER, string _FUN_FATHER)
        {
            
            decimal? FUN_ORDER;

            // Convierte la seleccion del orden en nulo si no hay seleccion
            if (_FUN_ORDER == "")
                FUN_ORDER = null;
            else
                FUN_ORDER = Convert.ToDecimal(_FUN_ORDER);

            //bool respuesta = Functions.updateFunction_UPD(FUV_CO, FUN_ID, FUV_NAME, FUV_DESCRIPTION, FUN_ORDER, _FUN_FATHER);
            //if (respuesta)
            //    return "1";
            //else
            //    return "-1";

            string respuesta = Functions.updateFunction_UPD(FUN_ID, FUV_NAME, FUV_DESCRIPTION, FUN_ORDER, _FUN_FATHER);

            if (respuesta == "bien")
                return "1";
            else
                return respuesta;

        }

        #endregion

        protected void gvPays_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            //string str = DataBinder.Eval(e.Row.DataItem, "pin_employee").ToString();
            ImageButton imageButton2 = (ImageButton)e.Row.FindControl("ibeliminar");
            imageButton2.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Eliminar la consignacion N° : -" + DataBinder.Eval(e.Row.DataItem, "Pag_Num_Consignacion") + "- ?')");
        }

        protected void gvPays_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("steliminar"))
            {
                this.msnMessage.Visible = false;
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _op = e.CommandArgument.ToString();                
                {                    
                    try
                    {
                            Functions.eliminar_pago(_op);                                                        
                            //Returns_Hdr.sbeanularnc(_nc, _user._bas_id);
                            this.msnMessage.LoadMessage("Se elimino la consignacion No." + _op + ".", ucMessage.MessageType.Information);
                            this.refreshGrid();
                            //this.refreshGridView();                     
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error realizando la eliminacion de la consignacion No." + _op + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }

                }
            }
        }

    }
}
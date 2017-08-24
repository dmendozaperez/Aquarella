using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using System.Data;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.UserControl;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class ordervencido : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "OrdersCustomers_", _nameSessionCustomer = "nameSessionCustomer";
        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = new object();
                Session[_nameSessionCustomer] = new Coordinator();
                if ((_user._usu_tip_id == "02"))
                {
                    this.formForCustomer();
                }
                else
                {
                    this.formForEmployee();
                }
                //if (_user._usv_employee)
                //    this.formForEmployee();
                //else if (_user._usv_customer)
                //    this.formForCustomer();
            }
        }
        protected void formForEmployee()
        {
            // Mostrar Panel de Seleccion de Coordinador
            pnlDwCustomers.Visible = true;
            /// Realizar la consulta de coordinadores
            DataSet dsCustomers = Coordinator.getCoordinators( _user._usv_area,_user._asesor);
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
           // pnlDwCustomers.Visible = false;

          //  this.paintInfoCustomer(Coordinator.getCoordinatorByPk(_user._usv_co, _user._usn_userid));
            // Cargar los pedidos por promotor, las liquidaciones y las devoluciones pertenecientes a este coordinador
            //getOrdLiqAnsRet(_user._usv_co, _user._usn_userid);
        }
      
        protected void refreshGridView()
        {
            string selectedCustomer = ((DropDownList)dwCustomers).SelectedValue;
            this.initGrid(selectedCustomer);
        }
        protected void initGrid(string var_lhn_customer)
        {
            try
            {
                this.Session[this._nameSessionData] = (object)Liquidations_Hdr.getpedidosvencidos(var_lhn_customer).Tables[0];
                this.gvvencido.DataSourceID = this.odsvencido.ID;
                this.gvvencido.DataBind();
            }
            catch
            { 
            }
        }
        protected void dwCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nuevo cliente seleccionado
            string selectedCustomer = ((DropDownList)sender).SelectedValue;

            /// Verificar que sea una selección valida
            if (!string.IsNullOrEmpty(selectedCustomer) && selectedCustomer != "-1")
            {
                // getOrdvencido(selectedCustomer); 
                odsvencido.SelectParameters[0].DefaultValue = selectedCustomer;
                // Cargar pedidos vencidos
                gvvencido.DataSourceID = odsvencido.ID;
                gvvencido.DataBind();
            }
            else
                Session[_nameSessionCustomer] = new Coordinator();
        }

        protected void odsvencido_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessionData] = dt;
            }
            catch
            { }
        }
      
        protected void gvvencido_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            
            ImageButton imageButton2 = (ImageButton)e.Row.FindControl("ibrestaurar");
            imageButton2.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de restaurar la liquidacion Vencida con N° : -" + DataBinder.Eval(e.Row.DataItem, "noliquid") + "- ?')");
        }

        protected void gvvencido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("strestaurar"))
            {
                this.msnMessage.Visible = false;
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _liquid = e.CommandArgument.ToString();
                {
                    try
                    {
                        decimal valor = 0;
                        Liquidations_Hdr.sbresliqvenc(_liquid,ref valor);

                        if (valor == 0)
                        {
                            this.msnMessage.LoadMessage("Error realizando la restauracion de liquidacion No." + _liquid + "; Detalle: " + "No hay Stock suficiente", ucMessage.MessageType.Error);
                            return;
                        }

                        this.refreshGridView();
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error realizando la restauracion de liquidacion No." + _liquid + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }

                }
            }
        }
    }
}
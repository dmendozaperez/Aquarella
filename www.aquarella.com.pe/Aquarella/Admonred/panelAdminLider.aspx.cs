using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.Aquarella.Lider;
namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class panelAdminLider : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "_CoordPromoters",
            _urlPageModify = "updateProfileFormLider.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = null;
                //if (_user._usv_employee)
                    this.formForEmployee();
                
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
            //DataSet dsCustomers = Lider.getLiders(_user._usv_co, _user._usn_userid);
            //dwCustomers.Focus();
            //// Enlazar datos al dropdown list encargado de mostrar la informacion de los coordinadores
            //dwCustomers.DataSource = dsCustomers;
            //dwCustomers.DataBind();
        }

        /// <summary>
        /// Preparar formulario para cliente
        /// </summary>
      

        /// <summary>
        /// Cambio en la seleccion de cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dwCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nuevo cliente seleccionado
            string selectedCustomer = ((DropDownList)sender).SelectedValue;
        }

        protected void btNewCust_Click(object sender, EventArgs e)
        {
            // Crear nuevo cliente
            string url = _urlPageModify;
            Response.Redirect(url);
        }
    }
}
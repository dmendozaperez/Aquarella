using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;

namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class panelAdminPromoters : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "_CoordPromoters",  _nameSessionCoord = "IdCoord",
            _urlPageModify = "updateProfileForm.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
                Session[_nameSessionData] = null;

                if ((_user._usu_tip_id == "02") )
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

            hdIdCustomer.Value = _user._usn_userid.ToString();
            this.paintInfoCustomer(Coordinator.getCoordinatorByPk(_user._usn_userid));
        }

        /// <summary>
        /// Mostrar informacion del cliente
        /// </summary>
        /// <param name="dsCustomer"></param>
        protected void paintInfoCustomer(DataSet dsCustomer)
        {
            msnMessage.HideMessage();
            if (dsCustomer == null || dsCustomer.Tables[0].Rows.Count <= 0)
                return;

            pnlCustInfo.Visible = true;
            DataRow dRow = dsCustomer.Tables[0].Rows[0];


            Users cust = new Users
            {
                _usn_userid = Convert.ToDecimal(dRow["con_coordinator_id"]),
                _usv_area = dRow["bdv_area_id"].ToString(),
                _usv_warehouse = dRow["cov_warehouseid"].ToString()
            };
            Session[_nameSessionCoord] = cust;
            
            // Documento
            lblDocument.Text = dRow["bdv_document_no"].ToString();
            // Nombre completo
            lblFullName.Text = dRow["nombrecompleto"].ToString();
            lblFullName.ToolTip = dRow["nombrecompleto"].ToString();
            // Dirección y telefono
            lblDirPhones.Text = dRow["bdv_address"].ToString() + " - " + dRow["bdv_phone"].ToString();
            lblDirPhones.ToolTip = dRow["bdv_address"].ToString() + " - " + dRow["bdv_phone"].ToString();
            // E-Mail
            lblMail.Text = (string.IsNullOrEmpty(dRow["bdv_email"].ToString()) ? "Sin correo" : "<a href='mailto:" + dRow["bdv_email"].ToString() + "' target='_Blank'>" + dRow["bdv_email"].ToString() + "</a>");
            lblMail.ToolTip = "Clcik sobre la dirección de correo electronico para enviar un mensaje.";
            // Ubicacion customer
            lblUbication.Text = dRow["ubicationcustomer"].ToString();
            lblUbication.ToolTip = dRow["ubicationcustomer"].ToString();
            // Logistica
            lblLogistica.Text = dRow["wav_description"].ToString() + ", " + dRow["arv_description"].ToString();
        }

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
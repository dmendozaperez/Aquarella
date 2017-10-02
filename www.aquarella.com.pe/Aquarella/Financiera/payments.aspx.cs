using System;
using System.Data;
using System.Web.UI;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Maestros;
//using Bata.Aquarella.BLL.Util;
//using www.aquarella.com.pe.bll
//using Bata.Aquarella.BLL.Maestros;

namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class payments : System.Web.UI.Page
    {
        Users _user;

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
                dwBanks.DataSource = Banks.getAllBanks();
                dwBanks.DataBind();

                dwDepositType.DataSource = Concepts.getConceptsByType();
                dwDepositType.DataBind();

               // calendar.StartDate = DateTime.Now.AddDays(-4);
               // calendar.EndDate = DateTime.Now;
                calendar.SelectedDate = DateTime.Now;

                if ((_user._usu_tip_id == "02"))
                {
                    this.formForCustomer();
                }
                else
                {
                    this.formForEmployee();
                }
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
        }

        /// <summary>
        /// Boton de registro de recaudo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSavePay_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();

            string idCust = string.Empty;

            if ((_user._usu_tip_id == "02"))
            {
                idCust = _user._usn_userid.ToString();
            }
            else
            {
                idCust = dwCustomers.SelectedValue;
            }
            string _banid = dwBanks.SelectedValue;


            //if (_user._usv_employee)
            //    idCust = dwCustomers.SelectedValue;
            //else if (_user._usv_customer)
            //    idCust = _user._usn_userid.ToString();

            if (!string.IsNullOrEmpty(idCust) && !idCust.Equals("-1"))

                if (!(Payments.existe_op(txtNoVoucher.Text.Trim(), _banid,Convert.ToDecimal(idCust),Convert.ToDecimal(txtValue.Text),Convert.ToDateTime(txtDate.Text))))
                {
                    savePayment(idCust);
                }
                else
                {
                    msnMessage.LoadMessage("El numero de operacion ya esta registrado.", UserControl.ucMessage.MessageType.Error);
                }
                else
                    msnMessage.LoadMessage("Seleccione un cliente sobre el cual aplicar el registro del recaudo.", UserControl.ucMessage.MessageType.Error);
        }

        /// <summary>
        /// Registrar recaudo
        /// </summary>
        /// <param name="idCust"></param>
        protected void savePayment(string idCust)
        {
            try
            {
                
                if (Page.IsValid)
                {
                    _user = (Users)Session[Constants.NameSessionUser];
                    decimal cust = decimal.Zero;
                    string bank = dwBanks.SelectedValue;
                    string typePay = dwDepositType.SelectedValue;
                    string noCons = txtNoVoucher.Text.Trim();
                    DateTime datePay;
                   
                    decimal amount = decimal.Zero;

                    if (decimal.TryParse(idCust, out cust))
                        if (DateTime.TryParse(txtDate.Text.Trim(), out datePay))
                            if (decimal.TryParse(txtValue.Text.Trim(), out amount))

                                Payments.savePayment( cust, bank, noCons, datePay, amount, typePay,txtNotes.Text,_user._bas_id);
                            else
                                throw new InvalidCastException();
                        else
                            throw new InvalidCastException();
                    else
                        throw new InvalidCastException();
                    //
                    msnMessage.LoadMessage("El registro del recuado se ha realizado correctamente; ahora espera por aprobación.", UserControl.ucMessage.MessageType.Information);

                    // Async 
                    //Log_Transaction.registerUserInfo(_user, "CREATE PAYMENT VOUC:" + noCons + " VALUE:" + amount +_user._usv_username);

                    //
                    cleanInfo();
                }
                else
                    msnMessage.LoadMessage("Los datos que intenta enviar no son validos.", UserControl.ucMessage.MessageType.Error);
            }
            catch (InvalidCastException) { msnMessage.LoadMessage("Los datos que intenta enviar no son validos.", UserControl.ucMessage.MessageType.Error); }
            catch (Exception ex) { msnMessage.LoadMessage("Error intentando registrar la información: " + ex.ToString(), UserControl.ucMessage.MessageType.Error); }
        }

        /// <summary>
        /// Limpiar formulario
        /// </summary>
        protected void cleanInfo()
        {
            dwBanks.SelectedValue = "-1";
            dwDepositType.SelectedValue = "-1";
            txtDate.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtNoVoucher.Text = string.Empty;
            txtValue.Text = string.Empty;
        }
    }
}
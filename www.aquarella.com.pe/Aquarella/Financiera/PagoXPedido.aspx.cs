using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Maestros;
using www.aquarella.com.pe.bll;
using System.Data;
namespace www.aquarella.com.pe.Aquarella.Financiera
{
    public partial class PagoXPedido : System.Web.UI.Page
    {
        Users _user;
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

                if ((_user._usu_tip_id == "01" || _user._usu_tip_id=="03" ))
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
            //DataSet dsCustomers =Lider.Lider.getlider(  Coordinator.getCoordinators(_user._usv_area);
            DataSet dsCustomers =Lider.Lider.getlider();
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
        /// 
        protected void btSavePay_Click(object sender, EventArgs e)
        {
            msnMessage.HideMessage();

            string idCust = string.Empty;

            if ((_user._usu_tip_id == "01") || (_user._usu_tip_id == "03"))
            {
                idCust = _user._usn_userid.ToString();
            }
            else
            {
                idCust = dwCustomers.SelectedValue;
            }



            //if (_user._usv_employee)
            //    idCust = dwCustomers.SelectedValue;
            //else if (_user._usv_customer)
            //    idCust = _user._usn_userid.ToString();

            if (!string.IsNullOrEmpty(idCust) && !idCust.Equals("-1"))

                if (!(Payments.existe_op(txtNoVoucher.Text.Trim())))
                {
                    //en esta opcion validamos lo del pedido relacionado con el lider
                    string _valida= Payments.valida_pedido_pago(Convert.ToDecimal(idCust), txtpedido.Text);
                    //en esta opcion si la funcion retorna un error
                    if (_valida == "-1")
                    {
                        msnMessage.LoadMessage("Ocurrio un error en la validacion de pago con el pedido.", UserControl.ucMessage.MessageType.Error);
                        return;
                    }
                    //en esta opcion si el pedido no existe
                    if (_valida == "0")
                    {
                        msnMessage.LoadMessage("El numero de pedido no existe.", UserControl.ucMessage.MessageType.Error);
                        return;
                    }
                    //en esta opcion si el pedido no concuierda con el lider seleccionado
                    if (_valida == "2")
                    {
                        msnMessage.LoadMessage("El numero de pedido no pertenece a un promotor de la lider seleccionAQUARELLA.", UserControl.ucMessage.MessageType.Error);
                        return;
                    }
                    //si el numero de pedido ya esta regiatrado en los pagos
                    if (_valida == "3")
                    {
                        msnMessage.LoadMessage("El numero de pedido ya esta registrado", UserControl.ucMessage.MessageType.Error);
                        return;
                    }
                    //************************************
                    //en este caso vamos a ver si es que si hay corcondancia con el lider y pedido
                    if (_valida == "1")
                    {
                        savePayment(idCust);
                    }
                }
                else
                {
                    msnMessage.LoadMessage("El numero de operacion ya esta registrado.", UserControl.ucMessage.MessageType.Error);
                }
            else
                msnMessage.LoadMessage("Seleccione un lider sobre el cual aplicar el registro del recaudo.", UserControl.ucMessage.MessageType.Error);
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

                                Payments.savePayment(cust, bank, noCons, datePay, amount, typePay, txtNotes.Text, _user._bas_id,txtpedido.Text);
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
            txtpedido.Text = string.Empty;
        }
    }
}
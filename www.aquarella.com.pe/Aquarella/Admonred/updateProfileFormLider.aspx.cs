using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Admonred;

namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class updateProfileFormLider : System.Web.UI.Page
    {
        Users _user;

        string _personDefaultToInvoice = ValuesDB.idDefaultPersonToInvoiceForCoordinators, _nameSessionIdPromotor = "_IdPromotor",
            _nameSessionCoord = "IdCoord", _formUpdProm = "1", _formUpdCust = "3", _formCreProm = "2", _formCreCust = "4";

        /// <summary>
        /// Load de la página
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
                //// opc:1-> Update promoter, 2-> New Promoter, 3-> update customer
                //if (Request.Params["opc"] != null)
                //{
                //    if (Request.Params["opc"].ToString().Equals("1"))
                //    {
                //        formUpdateProm(_user._usv_co, string.Empty, Convert.ToDecimal(Session[_nameSessionIdPromotor]));

                //        lblTitle.Text = "Modificación de datos de promotor";
                //        btValidateDoc.Visible = false;
                //    }
                //    else if (Request.Params["opc"].ToString().Equals("2"))
                //    {
                //        formNewProm();
                //        btValidateDoc.Visible = false;
                //        btSaveNewProm.Visible = true;
                //    }
                //}
                //else
                    lblTitle.Text = "Creación de un nuevo Lider";

                initDws();
                btSaveNewCust.Visible = false;

                /*if (_user._usv_employee)
                    this.formForEmployee();
                else if (_user._usv_customer)
                    this.formForCustomer();*/
            }
        }

        /// <summary>
        /// Enlazar dw's con la informacion correspondiente
        /// </summary>
        protected void initDws()
        {
            DataSet dsInfoNewCust;
            try
            {
                dsInfoNewCust = Basic_Data.getInfoNewLider();
            }
            catch
            {
                msnMessage.LoadMessage("Ha ocurrido un error intentado relaizar la carga de información.", UserControl.ucMessage.MessageType.Error);
                return;
            }

            if (dsInfoNewCust != null & dsInfoNewCust.Tables.Count > 0)
            {
                dwCustType.DataSource = dsInfoNewCust.Tables[1];
                dwCustType.DataBind();

                //dwCity.DataSource = dsInfoNewCust.Tables[3];
                //dwCity.DataBind();

                dwArea.DataSource = dsInfoNewCust.Tables[4];
                dwArea.DataBind();
                ListItem l = new ListItem("--Seleccionar de la lista--", "-1", true); l.Selected = true;
                dwArea.Items.Add(l);

                //dwHandlingType.DataSource = dsInfoNewCust.Tables[5];
                //dwHandlingType.DataBind();

                dwPersonType.DataSource = dsInfoNewCust.Tables[0];
                dwPersonType.DataBind();

                //dwWare.DataSource = dsInfoNewCust.Tables[7];
                //dwWare.DataBind();

                dwDocType.DataSource = dsInfoNewCust.Tables[2];
                dwDocType.DataBind();

                //**agregando departamento,provincia y distrito
                dwdepartamento.DataSource = dsInfoNewCust.Tables[3];
                dwdepartamento.DataBind();
                // pos[0]-> Tipo de coordinador,pos[1]-> Tipo de impuestos,
                // pos[2]-> Regimenes,pos[3]-> Ciudades,pos[4]-> Areas,pos[5]-> Tipos de flete
                // pos[6]-> Tipo de persona,pos[7]-> Bodegas,pos[8]-> Tipos de documento
                //dwCustType.DataSource = dsInfoNewCust.Tables[0];
                //dwCustType.DataBind();

                ////dwCity.DataSource = dsInfoNewCust.Tables[3];
                ////dwCity.DataBind();

                ////dwArea.DataSource = dsInfoNewCust.Tables[4];
                ////dwArea.DataBind();
                //ListItem l = new ListItem("--Seleccionar de la lista--", "-1", true); l.Selected = true;
                //dwArea.Items.Add(l);


                //dwHandlingType.DataSource = dsInfoNewCust.Tables[5];
                //dwHandlingType.DataBind();

                //dwPersonType.DataSource = dsInfoNewCust.Tables[6];
                //dwPersonType.DataBind();

                //dwWare.DataSource = dsInfoNewCust.Tables[7];
                //dwWare.DataBind();

                //dwDocType.DataSource = dsInfoNewCust.Tables[8];
                //dwDocType.DataBind();

                ////**agregando departamento,provincia y distrito
                //dwdepartamento.DataSource = dsInfoNewCust.Tables[9];
                //dwdepartamento.DataBind();
            }
        }

        /// <summary>
        /// Prepara formulario segun la accion
        /// </summary>
        /// <param name="co"></param>
        /// <param name="noDoc"></param>
        /// <param name="idPers"></param>
        protected void prepareForm(string noDoc, decimal idPers)
        {
            string ocultar_datasunat =  "$('#fsSunat').hide();";
            System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "HideDivs", ocultar_datasunat, true);

            if (noDoc.Length != 8 && noDoc.Length != 11)
            {
                msnMessage.LoadMessage("El Numero de Documento es incorrecto. por favor verifique", UserControl.ucMessage.MessageType.Error);
                return;
            }

            DataTable dtPerson;
            string showDivs = "$('#fsBasicData').show();$('#fsTypePerson').show();$('#fsUbi').show();$('#fsInfoCust').show();";
            try
            {
                dtPerson = Basic_Data.getPersonLider(noDoc, idPers).Tables[0];
            }
            catch
            {
                msnMessage.LoadMessage("Ha ocurrido un error intentando relaizar la búsqueda.", UserControl.ucMessage.MessageType.Error);
                return;
            }

            if (dtPerson != null && dtPerson.Rows.Count > 0)
            {
                cargarAreaLideres();
                dwArea.Enabled = false;
                DataRow infoPerson = dtPerson.Rows[0];
                Boolean valor = false;
                if (infoPerson["dis_dep_id"] != null && !string.IsNullOrEmpty(infoPerson["dis_dep_id"].ToString()))
                {
                    valor = true;
                }
                cleanInfo(_formCreCust,valor);
                
                printInfo(infoPerson);
                // Cliente
                string idCliente = infoPerson["bas_id"].ToString();

                if (!string.IsNullOrEmpty(idCliente))
                {
                    Users cust = new Users { _usn_userid = Convert.ToDecimal(idCliente) };
                    Session[_nameSessionCoord] = cust;
                    btUpdateCust.Visible =true;
                    btUpdateProm.Visible = false;
                    btSaveNewCust.Visible = false;
                    btSaveNewProm.Visible = false;
                   //btUpdateCust.Visible = true;
                    //btSaveNewCust.Visible = false;
                    //showDivs += "$('#fsInfoCust').show();";
                }
                dwArea.Enabled = false;
                msnMessage.LoadMessage("Puede realizar una actualización de la información.", UserControl.ucMessage.MessageType.Information);
            }
            else
            {
               


                //showDivs += "$('#fsInfoCust').show();";
                msnMessage.LoadMessage("Número de documento disponible.", UserControl.ucMessage.MessageType.Information);
                cargarAreaLiderZonal();
                cleanInfo("4");
                btUpdateCust.Visible = false;
                btUpdateProm.Visible = false;
                btSaveNewCust.Visible = false;
                btSaveNewProm.Visible = true;

                Boolean validadni = false;
                if (noDoc.Length == 8)
                {
                    dwDocType.SelectedValue = "1";
                    validadni = true;
                }
                else
                {
                    dwDocType.SelectedValue = "2";
                }

                //verificando dni existe en la web service sunat 

                Consultar_Documento myRucDni = new Consultar_Documento((noDoc.Length == 8) ? Microsoft.VisualBasic.Strings.Trim("10" + noDoc + Consultar_Documento.getDigito("10" + noDoc).ToString()) : noDoc);

                if (string.IsNullOrEmpty(myRucDni.Error))
                {
                    showDivs = "$('#fsBasicData').show();$('#fsTypePerson').show();$('#fsUbi').show();$('#fsSunat').show();$('#fsInfoCust').show();";
                    if (validadni)
                    {
                        string _primer_nombre = "";  string _segundo_nombre = "";  string _primer_apellido=""; string _segundo_apellido="";
                        Consultar_Documento.divide_nombres(myRucDni.GetInfo.RazonSocial, ref _primer_nombre, ref _segundo_nombre, ref _primer_apellido, ref _segundo_apellido);
                        txtFirstName.Text = Consultar_Documento.Convert_MayusMin(_primer_nombre);
                        txtMiddleName.Text = Consultar_Documento.Convert_MayusMin(_segundo_nombre);
                        txtFirstSurname.Text = Consultar_Documento.Convert_MayusMin(_primer_apellido);
                        txtSecondSurname.Text = Consultar_Documento.Convert_MayusMin(_segundo_apellido);
                    }
                    else
                    {
                        this.txtFirstName.Text = myRucDni.GetInfo.RazonSocial;
                    }
                    lblnombreruc.Text =Consultar_Documento.Convert_MayusMin(myRucDni.GetInfo.RazonSocial);
                    lbldireccionruc.Text = Consultar_Documento.Convert_MayusMin(myRucDni.GetInfo.Direccion);                                     
                    this.txtPhone.Text = myRucDni.GetInfo.Telefono;                    
                    this.txtAddress.Text = Consultar_Documento.Convert_MayusMin(myRucDni.GetInfo.Direccion);
                    this.txtBirth.Text = myRucDni.GetInfo.Fecha_Nac;
                    
                }
                //**************************************************

                //cargar dwArea

            }

            System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "ShowDivs", showDivs, true);
        }


        public void cargarAreaLiderZonal()
        {
            //DataSet dsArea;
            try
            {
                dwArea.Items.Clear();
                //dsArea = Basic_Data.getInfoNewLiderZonal(_user._usv_co);
                //dwArea.DataSource = dsArea.Tables[0];
                //dwArea.DataBind();

                ListItem l = new ListItem("--Nuevo Lider--", "-1", true); l.Selected = true;
                dwArea.Items.Add(l);
            }
            catch
            {
                msnMessage.LoadMessage("Ha ocurrido un error intentando relaizar la búsqueda.", UserControl.ucMessage.MessageType.Error);
                return;
            }
        }

        public void cargarAreaLideres()
        {
            DataSet dsArea;
            try
            {
                dwArea.Items.Clear();
                dsArea = Basic_Data.getInfoNewLideres();
                dwArea.DataSource = dsArea.Tables[0];
                dwArea.DataBind();

                ListItem l = new ListItem("--Seleccionar de la lista--", "-1", true); l.Selected = true;
                dwArea.Items.Add(l);
            }
            catch
            {
                msnMessage.LoadMessage("Ha ocurrido un error intentando relaizar la búsqueda.", UserControl.ucMessage.MessageType.Error);
                return;
            }
        }

        /// <summary>
        /// Forma para actualizacion de promotor
        /// </summary>
        /// <param name="co"></param>
        /// <param name="noDoc"></param>
        /// <param name="idPers"></param>
        protected void formUpdateProm(string co, string noDoc, decimal idPers)
        {
            DataTable dtPerson;
            string showDivs = "$('#fsBasicData').show();$('#fsTypePerson').show();$('#fsUbi').show();$('#fsInfoCust').show();";
            try
            {
                dtPerson = Basic_Data.getPersonCoorProm(noDoc, idPers).Tables[0];
            }
            catch
            {
                msnMessage.LoadMessage("Ha ocurrido un error intentando relaizar la búsqueda.", UserControl.ucMessage.MessageType.Error);
                return;
            }

            if (dtPerson != null && dtPerson.Rows.Count > 0)
            {
                DataRow infoPerson = dtPerson.Rows[0];
                printInfo(infoPerson);
                // Promotor                
                string stProm = infoPerson["bdv_status"].ToString();

                btUpdateProm.Visible = true;
                txtDoc.Enabled = false;
                dwArea.Enabled = false;
                dwWare.Enabled = false;
                dwWare.Visible = false;
                //showDivs += "$('#fsInfoCust').hide();";
                msnMessage.LoadMessage("Puede realizar una actualización de la información.", UserControl.ucMessage.MessageType.Information);
            }
            else
                Utilities.logout(Page.Session, Page.Response);
            System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "ShowDivs", showDivs, true);
        }

        /// <summary>
        /// Forma para creacion de nuevo promotor
        /// </summary>
        protected void formNewProm()
        {
            string showDivs = "$('#fsBasicData').show();$('#fsTypePerson').show();$('#fsUbi').show();$('#fsInfoCust').show();";

            Users cust = (Users)Session[_nameSessionCoord];

            dwArea.SelectedValue = cust._usv_area;
            dwArea.Enabled = false;

            dwWare.SelectedValue = cust._usv_warehouse;
            dwWare.Enabled = false;

            panelDwBodegas.Visible = true;

            System.Web.UI.ScriptManager.RegisterStartupScript(upPanelMsg, Page.GetType(), "ShowDivs", showDivs, true);
        }

        /// <summary>
        /// Impresion de informacion del cliente
        /// </summary>
        /// <param name="infoPerson"></param>
        protected void printInfo(DataRow infoPerson)
        {
            txtDoc.Text = infoPerson["bas_id"].ToString();
            // Tipo documento
            dwDocType.SelectedValue = infoPerson["Bas_Doc_Tip_Id"].ToString();

            // 1er Nombre  
            txtFirstName.Text = infoPerson["Bas_Primer_Nombre"].ToString();
            // 2do Nombre
            txtMiddleName.Text = infoPerson["bas_segundo_nombre"].ToString();
            // 1er Apellido
            txtFirstSurname.Text = infoPerson["Bas_Primer_Apellido"].ToString();
            // 2do Apellido
            txtSecondSurname.Text = infoPerson["Bas_Segundo_Apellido"].ToString();
            // Fecha Nacimiento 
            txtBirth.Text = infoPerson["Bas_Fec_nac"].ToString();

            // Tipo de persona
            dwPersonType.SelectedValue = infoPerson["Bas_Per_Tip_Id"].ToString();
            // Sexo
            rbSex.SelectedValue = (infoPerson["Bas_Sex_Id"].ToString().Equals(string.Empty)) ? "F" : infoPerson["Bas_Sex_Id"].ToString();

            // Dirección
            txtAddress.Text = infoPerson["Bas_Direccion"].ToString();
            // Teléfono
            txtPhone.Text = infoPerson["Bas_Telefono"].ToString();
            // E-Mail
            txtMail.Text = infoPerson["bas_correo"].ToString();
            // Celular
            txtCel.Text = infoPerson["Bas_Celular"].ToString();
            // Fax
            txtFax.Text = infoPerson["Bas_Fax"].ToString();

            txtagencia.Text = infoPerson["Bas_Agencia"].ToString(); ;
            txtdestino.Text = infoPerson["Bas_Destino"].ToString(); ;

            txtagenciaruc.Text = infoPerson["bas_agencia_ruc"].ToString();

             //Ahora vamos a mostrar , departamento,provincia y distrito del cliente
            if (infoPerson["dis_dep_id"] != null && !string.IsNullOrEmpty(infoPerson["dis_dep_id"].ToString()))
            {
                String vcod_dpto = infoPerson["dis_dep_id"].ToString();
                dwdepartamento.SelectedValue = vcod_dpto;
                fcombo_prov(vcod_dpto);

                //*****
                if (infoPerson["Dis_Prv_Id"] != null && !string.IsNullOrEmpty(infoPerson["Dis_Prv_Id"].ToString()))
                {
                    String vcod_prov = infoPerson["Dis_Prv_Id"].ToString();
                    dwprovincia.SelectedValue = vcod_prov;
                    fcombo_dis(vcod_prov);

                    if (infoPerson["Dis_Id"] != null && !string.IsNullOrEmpty(infoPerson["Dis_Id"].ToString()))
                    {
                        String vid_dist = infoPerson["Dis_Id"].ToString();
                        dwCity.SelectedValue = vid_dist;
                    }
                }
            }
            // Ciudad
            //if (infoPerson["bdv_city_cd"] != null && !string.IsNullOrEmpty(infoPerson["bdv_city_cd"].ToString()))
            //    dwCity.SelectedValue = infoPerson["bdv_city_cd"].ToString();
            //
            //if (infoPerson["cov_warehouseid"] != null && !string.IsNullOrEmpty(infoPerson["cov_warehouseid"].ToString()))
            //    dwWare.SelectedValue = infoPerson["cov_warehouseid"].ToString();


            // Estado Cuenta
            //rbEstado.SelectedValue = Convert.ToString(dtable.Rows[0]["BDV_STATUS"]);
            //
            this.dwArea.SelectedValue = (infoPerson["bas_are_id"].ToString().Equals(string.Empty)) ? "-1" : infoPerson["bas_are_id"].ToString();
        }

        /// <summary>
        /// Limpiar formulario
        /// </summary>
        protected void cleanInfo(string typeForm, Boolean valor = false)
        {
            // Tipo documento
            dwDocType.SelectedValue = "-1";

            // 1er Nombre  
            txtFirstName.Text = string.Empty;
            // 2do Nombre
            txtMiddleName.Text = string.Empty;
            // 1er Apellido
            txtFirstSurname.Text = string.Empty;
            // 2do Apellido
            txtSecondSurname.Text = string.Empty;
            // Fecha Nacimiento 
            txtBirth.Text = string.Empty;

            // Tipo de persona
            dwPersonType.SelectedValue = "-1";

            // Dirección
            txtAddress.Text = string.Empty;
            // Teléfono
            txtPhone.Text = string.Empty;
            // E-Mail
            txtMail.Text = string.Empty;
            // Celular
            txtCel.Text = string.Empty;
            // Fax
            txtFax.Text = string.Empty;

            txtagencia.Text = string.Empty;

            txtdestino.Text = string.Empty;

            txtagenciaruc.Text = String.Empty;

            //por defecto vamos aponer lima,lima
            if (!(valor))
            {
                dwdepartamento.SelectedValue = "15";

                fcombo_prov("15");
                dwprovincia.SelectedValue = "128";

                fcombo_dis("128");

                // Ciudad
                dwCity.SelectedValue = "-1";
                // Estado Cuenta
                //rbEstado.SelectedValue = Convert.ToString(dtable.Rows[0]["BDV_STATUS"]);
                //
            }
            if (!typeForm.Equals(_formUpdCust))
                txtDoc.Text = string.Empty;

            if (!typeForm.Equals(_formCreProm) || !typeForm.Equals(_formUpdProm))
                dwArea.SelectedValue = "-1";
            if (typeForm.Equals(_formCreCust))
                //if (_user._usv_employee)
                    if ( _user._usv_area.Equals("%%"))
                    {
                        dwWare.SelectedValue = "-1";
                        panelDwBodegas.Visible = true;
                        dwWare.Enabled = true;
                        dwArea.Enabled = true;
                    }
                    else
                    {
                        dwArea.SelectedValue = _user._usv_area;
                        dwWare.SelectedValue = _user._usv_warehouse;
                        panelDwBodegas.Visible = true;
                        dwWare.Enabled = false;
                        dwArea.Enabled = false;
                    }
                //else
                //{
                //    panelDwBodegas.Visible = false;
                //    dwWare.Enabled = false;
                //    dwArea.Enabled = false;
                //}
        }

        #region < Botones >

        /// <summary>
        /// validacion de documento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btValidateDoc_Click(object sender, EventArgs e)
        {
            prepareForm( txtDoc.Text.Trim(), decimal.Zero);
        }

        /// <summary>
        /// Boton de volver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("panelAdminlider.aspx");
        }

        /// <summary>
        /// Boton de actualizar promotor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btUpdateProm_Click(object sender, EventArgs e)
        {
            actionToSave(_formUpdProm);
        }

        /// <summary>
        /// Boton de creacion de nuevo promotor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSaveNewProm_Click(object sender, EventArgs e)
        {
            actionToSave(_formCreProm);
        }

        /// <summary>
        /// Bioton de creacion de nuevo cliente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSaveNewCust_Click(object sender, EventArgs e)
        {
            actionToSave(_formCreCust);
        }

        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btUpdateCust_Click(object sender, EventArgs e)
        {
            actionToSave(_formUpdCust);
        }

        #endregion

        /// <summary>
        /// Save, update
        /// </summary>
        /// <param name="typeAction">opc:1-> Update promoter, 2-> New Promoter, 3-> update customer, 4-> Create customer</param>
        public void actionToSave(string typeAction)
        {
            // Cedula
            string cedula = txtDoc.Text.Trim();
            //veificando documento
            if (cedula.Length != 8 && cedula.Length != 11)
            {
                msnMessage.LoadMessage("El Numero de Documento es incorrecto. por favor verifique", UserControl.ucMessage.MessageType.Error);
                return;
            }

            // Tipo doc
            string tipoDoc = dwDocType.SelectedValue;
            // 1er Nombre 
            string primerNombre = txtFirstName.Text.Trim();
            // 2do Nombre
            string segundoNombre = txtMiddleName.Text.Trim();
            // 1er Apellido
            string primerApellido = txtFirstSurname.Text.Trim();
            // 2do Apellido
            string segApellido = txtSecondSurname.Text.Trim();
            // Fecha Nacimiento
            DateTime fechaNacimiento;
            try
            {
                fechaNacimiento = Convert.ToDateTime((txtBirth.Text.Equals(string.Empty)) ? "01/01/1900" : txtBirth.Text);
            }
            catch
            {
                fechaNacimiento = Convert.ToDateTime("01/01/1900");
            }

            // Tipo de persona
            string tipoPersona = dwPersonType.SelectedValue;
            // Sexo 
            string sexo = rbSex.SelectedItem.Value;
            // Dirección
            string direccion = txtAddress.Text.Trim();
            // Teléfono
            string telefono = txtPhone.Text.Trim();
            // E-Mail
            string mail = txtMail.Text.Trim();
            // Celular
            string celular = txtCel.Text.Trim();
            // Fax
            string fax = txtFax.Text.Trim();
            // Ciudad
            string ciudad = dwCity.SelectedItem.Value;

            string agencia = txtagencia.Text.Trim();
            string destino = txtdestino.Text.Trim();

            string agencia_ruc = txtagenciaruc.Text.Trim();

            // Estado Cuenta
            //string estado = "A";

            // Usuario que realiza la accion
            string User = Convert.ToString(Page.User.Identity.Name);

            Users cust = (Users)Session[_nameSessionCoord];

            if (typeAction.Equals(_formUpdCust))
            {
                // Area de localizacion
                string area = this.dwArea.SelectedValue;
                //
                string tipoFlete = this.dwHandlingType.SelectedValue;

                // Bodega 
                string warehouseId = dwWare.SelectedValue;

                try
                {
                    // Realizar procesos de actualizacion de un coordinador
                    string respuesta = Basic_Data.updateBasicDatausers(cust._usn_userid,primerNombre, segundoNombre, primerApellido, segApellido, fechaNacimiento, tipoDoc, tipoPersona,
                        direccion,telefono,fax,celular,mail,area,User,sexo,ciudad,"",true,agencia,destino, agencia_ruc);


                    //string respuesta = Basic_Data.updateBasicData(_user._usv_co, cust._usn_userid, primerNombre, segundoNombre, primerApellido, segApellido,
                    //                   fechaNacimiento, cedula, "", tipoDoc, tipoPersona, direccion, telefono, fax,
                    //                   celular, mail, ciudad, estado, "1", area, User, sexo);

                    //Users.createUserLider(_user._usv_co, Convert.ToDecimal(cust._usn_userid), mail, Cryptographic.encrypt(cedula));
                    //
                    //respuesta = Coordinator.updateCoordinator(_user._usv_co, cust._usn_userid.ToString(), dwCustType.SelectedValue,
                    //    warehouseId, tipoFlete);

                    msnMessage.LoadMessage("Actualización correcta.", UserControl.ucMessage.MessageType.Information);
                    cleanInfo(typeAction);
                    txtDoc.Focus();
                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage("Ha ocurrido un error intentado actualizar." + ex.Message, UserControl.ucMessage.MessageType.Error);
                    cleanInfo(typeAction);
                    txtDoc.Focus();
                }
            }
            // Create customer
            else if (typeAction.Equals(_formCreProm))
            {
                //string tipoDePago = "3";
                ///
                //string terminosEntrega = "1";
                ///
                //string tipoMoneda = "PEN";///"1";
                // Bodega 
                string warehouseId = dwWare.SelectedValue;
                //
                string tipoFlete = this.dwHandlingType.SelectedValue; ///"2";            
                //
                //string creditoBandera = "F";
                //
                //string limiteCredito = "0";
                //
                //string autoretenedor = "F";
                //
                //string granContri = "T";

                // Area de localizacion
                string area = this.dwArea.SelectedValue;

                try
                {
                    // Insertar el nevo coordinador
                    //string resp = Coordinator.addNewLider(_user._usv_co, primerNombre, segundoNombre, primerApellido, segApellido,
                    //                   fechaNacimiento, cedula, "", tipoDoc, tipoPersona, direccion, telefono, fax,
                    //                   celular, mail, ciudad, estado, "1", area, User, User, sexo, dwCustType.SelectedValue,
                    //                   tipoDePago, terminosEntrega, tipoMoneda, warehouseId, "", "", "", tipoFlete, creditoBandera,
                    //                   limiteCredito, autoretenedor, granContri);

                    string resp = Basic_Data.crear_usuario(primerNombre, segundoNombre, primerApellido, segApellido, fechaNacimiento, cedula, tipoDoc, tipoPersona, direccion,
                                                         telefono, fax, celular, mail, area, User, sexo, ciudad, "01", Cryptographic.encrypt(cedula),0,true,agencia,destino, agencia_ruc);

                    // Crear el login de usuario para el coordinador con el cual podra iniciar sesion en el sistema
                    //Users.createUserLider(_user._usv_co, Convert.ToDecimal(resp), mail, Cryptographic.encrypt(cedula));
                    //
                    msnMessage.LoadMessage("El nuevo Lider se ha creado correctamente; recuerde que su usuario sera el correo electronico (" + mail
                        + ") y la contraseña su número de documento.", UserControl.ucMessage.MessageType.Information);
                    cleanInfo(typeAction);
                    txtDoc.Focus();
                }
                catch (Exception ex)
                {
                    msnMessage.LoadMessage("Ha ocurrido un error intentado actualizar." + ex.Message, UserControl.ucMessage.MessageType.Error);
                    //cleanInfo(typeAction);
                    txtDoc.Focus();
                }
            }
        }

        protected void dwdepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string viddepartemtno = dwdepartamento.SelectedValue;
            if (!(dwdepartamento.SelectedValue == "-1"))
            {
                fcombo_prov(viddepartemtno);
            }
            else
            {
                dwprovincia.Items.Clear();
                dwCity.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwprovincia.Items.Add(vlista);
                dwprovincia.DataBind();

                dwCity.Items.Add(vlista);
                dwCity.DataBind();
            }
        }

        protected void dwprovincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vidprovincia = dwprovincia.SelectedValue;
            if (!(dwprovincia.SelectedValue == "-1"))
            {
                fcombo_dis(vidprovincia);
            }
            else
            {
                dwCity.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwCity.Items.Add(vlista);
                dwCity.DataBind();
            }
        }
        protected void fcombo_prov(string var_id_dpto)
        {
            try
            {
                DataSet ds = Basic_Data.getinfoprovincia(var_id_dpto);
                dwprovincia.Items.Clear();
                dwCity.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwprovincia.Items.Add(vlista);
                dwprovincia.DataSource = ds.Tables[0];
                dwprovincia.DataBind();

                dwCity.Items.Add(vlista);
                dwCity.DataBind();
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
        protected void fcombo_dis(string var_id_prov)
        {
            try
            {
                DataSet ds = Basic_Data.getinfodistrito(var_id_prov);
                dwCity.Items.Clear();
                ListItem vlista = new ListItem();
                vlista.Text = "--Seleccionar de la lista--";
                vlista.Value = "-1";
                dwCity.Items.Add(vlista);
                dwCity.DataSource = ds.Tables[0];
                dwCity.DataBind();
            }
            catch (Exception e) { throw new Exception(e.Message, e.InnerException); }
        }
    }
}
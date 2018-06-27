using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
using www.aquarella.com.pe.bll.Util;
using www.aquarella.com.pe.bll.Admonred;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.BLL;
//using Bata.Aquarella.BLL.Admonred;
//using Bata.Aquarella.BLL.Logistica;
//using Bata.Aquarella.BLL.Util;
//using Bata.Aquarella.UserControl;

namespace www.aquarella.com.pe.Aquarella.Admonred
{
    public partial class panelAdminNetPromoters : System.Web.UI.Page
    {
        Users _user;

        string _nameSessionData = "_InfoCust", _nameSessionTypeCust = "_nameSessionTypeCust",
            _nameSessionArea = "_nameSessionArea", _nameSessionWare = "_nameSessionWare", _nameSessionStatus = "_nameSessionStatus";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (this.Session[Constants.NameSessionUser] == null)
                Utilities.logout(this.Page.Session, this.Page.Response);
            else
                this._user = (Users)this.Session[Constants.NameSessionUser];

            if (this.IsPostBack)
                return;

            this.Session[this._nameSessionData] = (object)null;
            this.Session[this._nameSessionTypeCust] = (object)null;
            this.Session[this._nameSessionArea] = (object)null;
            this.Session[this._nameSessionWare] = (object)null;
            this.Session[this._nameSessionStatus] = (object)null;

            //if (_user._usv_employee)
                initForm();
            //else
                //Utilities.logout(Page.Session, Page.Response);
        }

        private void initForm()
        {
            dwCoordinadores.DataSource = Coordinator.getCoordinators( _user._usv_area,_user._asesor);
            dwCoordinadores.DataBind();

            //
            //this.Session[this._nameSessionTypeCust] = Coordinator_Type.getAllCoordinatorType(_user._usv_co);
            //dwTipoCoordinador.DataSource = this.Session[this._nameSessionTypeCust];
            //dwTipoCoordinador.DataBind();

            this.Session[this._nameSessionArea] = Area.getAllAreas(_user._asesor);
            //this.Session[this._nameSessionWare] = warehouses.getWarehousesByCo(_user._usv_co);
            this.Session[this._nameSessionStatus] = Status.getStatusByModule("0");

            //
            dwArea.DataSource = Area.getAllAreas(_user._asesor);
            dwArea.DataBind();
        }

        public DataSet getTypeCustomer()
        {
            return (DataSet)this.Session[this._nameSessionTypeCust];
        }

        public DataSet getAreas()
        {
            return (DataSet)this.Session[this._nameSessionArea];
        }

        public DataSet getWare()
        {
            return (DataSet)this.Session[this._nameSessionWare];
        }

        public DataSet getStatus()
        {
            return (DataSet)this.Session[this._nameSessionStatus];
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            string opt = rdGroup.SelectedValue;

            if (opt.Equals("P"))
            {
                this.dvCustomer.Visible = false;
                this.dvPromoter.Visible = true;

                this.odsPromoters.SelectParameters[0].DefaultValue = _user._usv_co;

                this.dvPromoter.DataSourceID = this.odsPromoters.ID;
                this.dvPromoter.DataBind();
            }
            else
            {
                this.dvCustomer.Visible = true;
                this.dvPromoter.Visible = false;

                //this.odsCustomer.SelectParameters[0].DefaultValue = _user._usv_co;

                this.dvCustomer.DataSourceID = this.odsCustomer.ID;
                this.dvCustomer.DataBind();
            }
        }      
        
        protected void dvCustomer_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {

            if (e.CommandName.Equals("EditCustomer"))
            {
                DataRow dr = ((DataTable)Session[_nameSessionData]).Rows[0];
                ////
                //string newTypeCustomer = ((DropDownList)dvCustomer.FindControl("dwTypeCust")).SelectedValue;

                //if (string.IsNullOrEmpty(newTypeCustomer))
                //{
                //    newTypeCustomer = dr["cov_coordinator_type"].ToString();
                //}

                // 
                //string newWare = ((DropDownList)dvCustomer.FindControl("dwWare")).SelectedValue;
                //
                string newArea = ((DropDownList)dvCustomer.FindControl("dwArea")).SelectedValue;
                //
                string newStatus = ((DropDownList)dvCustomer.FindControl("dwStatus")).SelectedValue;

                try
                {
                    string idCust = dr["bas_id"].ToString();
                    //string oldTypeCustomer = dr["cov_coordinator_type"].ToString();                    
                    string OldArea = dr["bas_are_id"].ToString();
                    string OldStatus = dr["bas_est_id"].ToString();

                    string respuesta = Coordinator.updateCoord(idCust, newArea,  newStatus);
                    //
                    // Async 
                    //Log_Transaction.registerUserInfo(_user, _user._usv_username + "Update Customer id:" + idCust + " TypeO/N:" + oldTypeCustomer + "/" + newTypeCustomer + " WareO/N:" + oldWare + "/" + newWare +
                    //" AreaO/N:" + OldArea + "/" + newArea + " StatusO/N:" + OldStatus + "/" + newStatus);
                    if(respuesta=="1")
                        this.msnMessage.LoadMessage("Cliente actualizado correctamente.", ucMessage.MessageType.Information);
                    else
                        this.msnMessage.LoadMessage("El lider no debe tener promotores asociados. ", ucMessage.MessageType.Error);
                }
                catch (Exception ex)
                {
                    this.msnMessage.LoadMessage("Error realizando la actualización del cliente; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                }
            }

            dvCustomer.DataBind();
        }

        protected void odsCustomer_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            try
            {
                DataTable dt = ((DataSet)e.ReturnValue).Tables[0];
                Session[_nameSessionData] = dt;
            }
            catch
            { }
        }

        #region Consultas ajax

        /// <summary>
        /// Cambiar a un promotor de coordinador
        /// </summary>
        /// <param name="promoter"></param>
        /// <param name="newCoord"></param>
        /// <returns></returns>
        [WebMethod()]
        public static string ajaxUpdatePromoter(Decimal promoter, Decimal newCoord)
        {
            ///
            /// Deido a que la funcion es estatica se deben crear referencias para podre hacer llamados a algunos metodos
            System.Web.SessionState.HttpSessionState sessions = HttpContext.Current.Session;

            // Cargar session de compañia
            Users us = (Users)sessions[Constants.NameSessionUser];

            try
            {
                // Async 
                Log_Transaction.registerUserInfo(us,
                            us._usv_username + "Change Stencil Promoter id:" + promoter + " New Coord:" + newCoord);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }

            return "1";
        }

        /// <summary>
        /// Cambiar un promotor a cliente directo y crear una cuenta de inicio de sesion
        /// </summary>
        /// <param name="promoter"></param>
        /// <param name="newCoord"></param>
        /// <returns></returns>
        [WebMethod()]
        public static string ajaxUpgradePromoter(decimal promoter, string typeCoord, string area, string mail)
        {
            ///
            /// Deido a que la funcion es estatica se deben crear referencias para podre hacer llamados a algunos metodos
            System.Web.SessionState.HttpSessionState sessions = HttpContext.Current.Session;

            // Cargar session
            Users us = (Users)sessions[Constants.NameSessionUser];

            try
            {
                Log_Transaction.insertLogTransaction(us._usv_co, us._usn_userid, DateTime.Now,
                        us._usv_username + "Upgrade Promoter id:" + promoter + " Type Coord:" + typeCoord + " Area:" + area, us._logTx._ip);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }

            return "1";
        }

        #endregion
    }
}
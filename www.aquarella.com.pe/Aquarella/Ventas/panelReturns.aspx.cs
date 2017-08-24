using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;
using www.aquarella.com.pe.UserControl;
//using Bata.Aquarella.UserControl;
using www.aquarella.com.pe.bll.Ventas;
//using Bata.Aquarella.BLL.Ventas;
namespace www.aquarella.com.pe.Aquarella.Ventas
{
    public partial class panelReturns : System.Web.UI.Page
    {
        Users _user;
        string _nameSessionData = "_ReturnData";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Vencimiento de sesion
            if (Session[Constants.NameSessionUser] == null) Utilities.logout(Page.Session, Page.Response);
            else
                _user = (Users)Session[Constants.NameSessionUser];

            if (!IsPostBack)
            {
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
                //else Utilities.logout(Page.Session, Page.Response);
            }
        }

        private void formForEmployee()
        {
            try
            {
                //odsReturns.SelectParameters[0].DefaultValue = _user._usv_co;                
                //odsReturns.SelectParameters[3].DefaultValue = _user._usv_warehouse;
                //odsReturns.SelectParameters[4].DefaultValue = _user._usv_area;                
            }
            catch
            {
                return;
            }
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            //
            this.msnMessage.Visible = false;
            gvReturns.DataSourceID = odsReturns.ID;
            gvReturns.DataBind();
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
                                 x2 = y.Sum(x => x.Field<decimal>("cantidad")),
                                 x3 = y.Sum(x => x.Field<decimal>("Total"))
                             }).FirstOrDefault();

                    gv.FooterRow.Cells[0].Text = "TOTALES:";
                    gv.FooterRow.Cells[1].Text = t.x1.ToString("N0");
                    gv.FooterRow.Cells[5].Text = t.x2.ToString("N0");
                    gv.FooterRow.Cells[6].Text = t.x3.ToString("N0");
                }
            }
            catch { }
        }

        /// <summary>
        /// Fuente de datos con la cual se este trabajando
        /// </summary>
        /// <returns></returns>
        protected DataTable getSource()
        {
            // Chequeado es ventas por semana y categoria
            /*if (chkGroupByWeek.Checked)
                return (DataTable)Session[_nameSessionData];
            // No chequeado es ventas netas entre las fechas dadas
            else*/
            return (DataTable)Session[_nameSessionData];
        }

        /// <summary>
        /// Generacion de calculo de totales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvReturns_DataBound(object sender, EventArgs e)
        {
            calculateTotals(this.gvReturns, getSource());
        }

        #region < Exportar Excel>

        /// <summary>
        /// Exportar a excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ibExportToExcel_Click(object sender, ImageClickEventArgs e)
        {
            gvReturns.AllowPaging = false;
            GridViewExportUtil.removeFormats(ref gvReturns);
            gvReturns.DataBind();

            string nameFile = "Devoluciones";

            //  pass the grid that for exporting ...
            GridViewExportUtil.Export(nameFile + ".xls", gvReturns);
        }

        #endregion

        protected void gvReturns_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            //string str = DataBinder.Eval(e.Row.DataItem, "pin_employee").ToString();
            ImageButton imageButton2 = (ImageButton)e.Row.FindControl("ibanular");
            imageButton2.Attributes.Add("onclick", "javascript:return confirm('¿Esta seguro de Anular la Nota de Credito N° : -" + DataBinder.Eval(e.Row.DataItem, "Not_Numero") + "- ?')");
        }

        protected void gvReturns_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName.Equals("starnular"))
            {
                this.msnMessage.Visible = false;
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                string _nc = e.CommandArgument.ToString();
                //string selectedValue = ((DropDownList)row.FindControl("dwEmployees")).SelectedValue;
                //if (string.IsNullOrEmpty(selectedValue) || selectedValue.Equals("-1"))
               // {
                 //   this.msnMessage.LoadMessage("Seleccione el empleado encargado de la marcación para la liquidación No." + _noLiq, ucMessage.MessageType.Error);
                //}
                //else
                {
                    //str1 = string.Empty;
                    try
                    {
                     //   str1 = Picking.finalizePicking(this._user._usv_co, _noLiq);
                        //validar finanzas si es que no se puede anular este documento porque ya esta haciando utilizada como medio de pago
                        int validafinanzas = Returns_Hdr.FValidaNC(_nc);


                        if (validafinanzas == 0)
                        {
                            Returns_Hdr.sbeanularnc(_nc,_user._bas_id);
                            this.msnMessage.LoadMessage("Se Anulo la Nota de Credito No." + _nc + ".", ucMessage.MessageType.Information);
                            this.refreshGridView();
                        }
                        else
                        {
                            msnMessage.LoadMessage("No se puede Anular esta N/C; porque ha sido utilizada por FINANZAS.", UserControl.ucMessage.MessageType.Error);
                        }

                        // Async 
                        //Log_Transaction.registerUserInfo(_user, "END PICKING:" + _noLiq);
                    }
                    catch (Exception ex)
                    {
                        this.msnMessage.LoadMessage("Error realizando la Anulacion de NC No." + _nc + "; Detalle: " + ex.Message, ucMessage.MessageType.Error);
                    }
                   
                }
            }
        }
        protected void refreshGridView()
        {
            this.initGrid(this._user._usv_co,txtDateStart.Text,txtDateEnd.Text,  this._user._usv_warehouse, this._user._usv_area);

          
        }
        protected void initGrid(string co,string _startDate, string _endDate, string ware, string area)
        {

            this.Session[this._nameSessionData] = (object)Returns_Hdr.getReturnsByDate(_startDate,_endDate).Tables[0];
            this.gvReturns.DataSourceID = this.odsReturns.ID;
            this.gvReturns.DataBind();
        }
    }
}
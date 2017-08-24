using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using www.aquarella.com.pe.bll;
//using Bata.Aquarella.BLL;
using www.aquarella.com.pe.bll.Util;
//using Bata.Aquarella.BLL.Util;

namespace www.aquarella.com.pe.Aquarella.Logistica
{
    public partial class panelSearchOrder : System.Web.UI.Page
    {
        private Users _user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session[Constants.NameSessionUser] == null)
                Utilities.logout(this.Page.Session, this.Page.Response);
            else
                this._user = (Users)this.Session[Constants.NameSessionUser];
            //if (this.IsPostBack || this._user._usv_employee)
            //    return;
            //this.btConsult.Enabled = false;
        }

        protected void btConsult_Click(object sender, EventArgs e)
        {
            this.gvLiq.DataSource = (object)Liquidations_Hdr.getLiquidations(this.txtConsult.Text.Trim());
            this.gvLiq.DataBind();
        }

        protected void gvLiq_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            int num = (int)Convert.ToInt16(e.Row.RowIndex.ToString());
            try
            {
                if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "Ven_Id").ToString()))
                {
                    DataBinder.Eval(e.Row.DataItem, "Liq_Id").ToString();
                    e.Row.FindControl("imgInv").Visible = true;
                }
            }
            catch
            {
            }
        }
    }
}
using System;
using System.Text;
using System.Web.UI;


namespace www.aquarella.com.pe.UserControl
{
    public partial class ucMessage : System.Web.UI.UserControl
    {        
        
        public enum MessageType
        {
            Error,
            Information,
            None
        }
     
        public string Message { set; get; }
        public MessageType Type { set; get; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadMessage(string msg, UserControl.ucMessage.MessageType msgtype)
        {
            lblMsg.Text = msg;
            this.Visible = true;
            pnlErrors.Focus();
            switch (msgtype)
            {
                case MessageType.Information:
                    lblType.Text = "Información: ";
                    pnlMsnType.CssClass = "ui-state-highlight ui-corner-all";
                    break;
                case MessageType.Error:
                    lblType.Text = "Alerta: ";
                    pnlMsnType.CssClass = "ui-state-error ui-corner-all";
                    break;
                case MessageType.None:
                    break;
            }
            ajaxScrollTop();
        }

        public void LoadWithOutScrollMessage(string msg, UserControl.ucMessage.MessageType msgtype)
        {
            lblMsg.Text = msg;
            this.Visible = true;
            pnlErrors.Focus();
            switch (msgtype)
            {
                case MessageType.Information:
                    lblType.Text = "Información: ";
                    pnlMsnType.CssClass = "ui-state-highlight ui-corner-all";
                    break;
                case MessageType.Error:
                    lblType.Text = "Alerta: ";
                    pnlMsnType.CssClass = "ui-state-error ui-corner-all";
                    break;
                case MessageType.None:
                    break;
            }
          
        }

        public void HideMessage()
        {
            lblMsg.Text = string.Empty;
            lblType.Text = string.Empty;
            this.Visible = false;
        }

        public void ajaxScrollTop()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("$(function() { ");
            sb.Append(" $('html, body').animate({");
            sb.Append("    scrollTop: '0px',");
            sb.Append("    resizable: false,");
            sb.Append("    modal: true");
            sb.Append(" },800);");
            sb.Append("});");            
            ScriptManager requestSM = ScriptManager.GetCurrent(this.Page);
            if (requestSM != null && requestSM.IsInAsyncPostBack)
            {
                ScriptManager.RegisterClientScriptBlock(this,
                                                        typeof(Page),
                                                        Guid.NewGuid().ToString(),
                                                        sb.ToString(),
                                                        true);
            }
            else
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page),
                                                       Guid.NewGuid().ToString(),
                                                       sb.ToString(),
                                                       true);
            }

        }
    }
}
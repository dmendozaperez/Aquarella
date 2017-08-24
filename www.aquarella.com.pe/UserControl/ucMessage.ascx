<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucMessage.ascx.cs" Inherits="www.aquarella.com.pe.UserControl.ucMessage" %>
<asp:Panel ID="pnlErrors" runat="server" CssClass="ui-widget">
    <asp:Panel runat="server" ID="pnlMsnType" Style="padding: 0 .7em;">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>
            <strong>
                <asp:Label ID="lblType" runat="server"></asp:Label></strong><asp:Label ID="lblMsg"
                    runat="server"></asp:Label></p>
    </asp:Panel>
</asp:Panel>

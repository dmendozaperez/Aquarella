<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucWareAreaForm.ascx.cs"
    Inherits="www.aquarella.com.pe.UserControl.ucWareAreaForm" %>
<script language="javascript" type="text/javascript">
    function hideControl(ctrl) {
        $('#' + ctrl).hide();
    }
</script>
<table width="100%" cellpadding="1" cellspacing="1">
    <tr>
        <td width="50%">
            <table cellpadding="1" cellspacing="1">
                <tr>
                    <td class="f12" colspan="3">
                        Bodega:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upWare" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="dwWare" runat="server" AppendDataBoundItems="true" DataValueField="wav_warehouseid"
                                    DataTextField="wav_description">
                                    <asp:ListItem Text="Seleccione Bodega" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Todas las opciones" Value="%%"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDwWare" runat="server"
                            ToolTip="Seleccione una bodega válida" CssClass="error_asterisck" ErrorMessage="Seleccione una bodega válida"
                            Display="Dynamic" SetFocusOnError="true" ControlToValidate="dwWare">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
        <td width="50%">
            <table id="tblArea" cellpadding="1" cellspacing="1">
                <tr>
                    <td class="f12" colspan="3">
                        Area:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upArea" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="dwArea" runat="server" AppendDataBoundItems="true" DataValueField="arv_area_id"
                                    DataTextField="arv_description">
                                    <asp:ListItem Text="Seleccione Area" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Todas las opciones" Value="%%"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="padding-left: 5px;">
                        <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDwArea" runat="server"
                            ToolTip="Seleccione un area válida" CssClass="error_asterisck" ErrorMessage="Seleccione un area válida"
                            Display="Dynamic" SetFocusOnError="true" ControlToValidate="dwArea">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

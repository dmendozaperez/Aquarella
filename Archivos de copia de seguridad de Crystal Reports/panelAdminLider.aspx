<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Design/Site.Master" CodeBehind="panelAdminLider.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.panelAdminLider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Gestión de personal
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Permite realizar la creación, modificación y eliminación de promotores; además podrá
    cambiar o actualizar sus datos personales.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdIdCustomer" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upPanelMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <center>
                <div style="position: absolute; left: 0; background: #f5f5f5; filter: alpha(opacity=85);
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 100%; font-size: medium;">
                    <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
                    Cargando información...
                </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!-- SEL CLIENTE -->
    <asp:Panel ID="pnlDwCustomers" Visible="false" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
                <tr>
                    <td class="fsal f13" colspan="2">
                        Selección de cliente
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="1" cellspacing="1">
                            <tr>
                                <td class="f12" width="60%">
                                    Seleccione un cliente de la lista:<br />
                                    (Desc: Seleccione un cliente sobre el cual realizará las acciones.)
                                </td>
                                <td>
                                    <asp:DropDownList ID="dwCustomers" AutoPostBack="true" DataTextField="nombreCompleto"
                                        DataValueField="bdn_id" AppendDataBoundItems="true" runat="server"
                                        ToolTip="Selecionar un cliente" Width="220px" OnSelectedIndexChanged="dwCustomers_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Text=" -- Seleccione de la Lista --"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button AccessKey="c" ID="btNewCust" runat="server" Text="(C)rear un nuevo Lider"
                                        ToolTip="Cree un nuevo cliente" onclick="btNewCust_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <!-- PANEL INFO CUSTOMER-->
    <asp:Panel runat="server" ID="pnlInfoCustomer">
        <table width="100%" class="f12" cellpadding="1" cellspacing="1">
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="upCustInfo" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlCustInfo" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 150px;">
                                            <div style="margin: 5px; padding: 5px; border: 1px solid silver;">
                                                <img src="../../Design/images/card_user.png" alt="Fotografia Cliente" title="Tarjeta de presentación" />
                                            </div>
                                        </td>
                                        <td>
                                            <table width="100%" cellpadding="2">
                                                <tr>
                                                    <td>
                                                        Número de documento :
                                                    </td>
                                                    <td width="80%">
                                                        <asp:Label ID="lblDocument" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Nombres y apellidos :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblFullName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Dirección y teléfono :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDirPhones" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Información de atención :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLogistica" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Ubicación :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUbication" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        E-Mail de contacto :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMail" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>


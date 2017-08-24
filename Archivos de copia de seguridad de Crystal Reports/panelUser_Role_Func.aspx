<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    CodeBehind="panelUser_Role_Func.aspx.cs" Inherits=" www.aquarella.com.pe.Aquarella.Control.panelUser_Role_Func"
    Theme="SiteTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <title>Asignacion Roles y Funciones</title>
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ width: "40%", height: "50%", iframe: true });
            $("#tabs").tabs();
        });

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "40%", height: "50%", iframe: true });
                $("input:submit,button").button();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Usuarios (Roles) y (Funciones)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Formularios para asignar roles y funciones a los diferentes usuarios.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Area de errores -->
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <AQControl:Message ID="msnMessage" Visible="false" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="tabs">
    <ul>
        <li><a href="#tabs-1">Roles de Usuario</a></li>
    </ul>
    <div id="tabs-1">
         <table cellpadding="6" cellspacing="6" width="100%">
        <tr>
            <td>
                <label class="f12">
                    Consultar por primer nombre:
                </label>
                <br />
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" AccessKey="u"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="ValidatorCedula" runat="server" ControlToValidate="txtNombre"
                    CssClass="error" Display="Dynamic" ErrorMessage="Digite un numero de cedula."
                    ForeColor="" SetFocusOnError="True" ValidationGroup="G1">*</asp:RequiredFieldValidator>
            </td>
            <td>
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" ValidationGroup="G1"
                    OnClick="btnConsultar_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:UpdatePanel ID="PanelGrid" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridUsers" runat="server" AutoGenerateColumns="False" SkinID="gridviewSkin"
                            OnPageIndexChanging="GridUsers_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="usu_id" HeaderText="ID" />
                                <asp:BoundField DataField="usu_nombre" HeaderText="Nombre Usuario" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="usu_est_id" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Rol">
                                    <ItemTemplate>
                                        <center>
                                            <a class="iframe" href="panelUser_Role.aspx?USN_USERID=<%# Eval("usu_id") %>&NAME=<%# Eval("nombre") %>"
                                              title="Asignar rol a usuario">
                                                <img src="../../Design/images/Botones/b_edit_order.png" alt="Editar" />
                                            </a>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AppFunc" Visible="False">
                                    <ItemTemplate>
                                        <center>
                                            <a class="iframe" href="panelUser_AppliFunction.aspx?USN_USERID=<%# Eval("usu_id") %>&NAME=<%# Eval("nombre") %>"
                                                title="Aignar ApliFunction a usuario.">
                                                <img src="../../Design/images/Botones/b_edit_order.png" alt="Editar" />
                                            </a>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="GridUsers" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </div>
    </div>
</asp:Content>

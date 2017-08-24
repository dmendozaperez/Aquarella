<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    CodeBehind="panelEditUsers.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Control.panelEditUsers"
    Theme="SiteTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("input:text").width('250px');
            $("#tabs").tabs();
        });

        function editUser(USN_USERID, USV_USERNAME, USV_PASSWORD,USD_CREATION, USV_STATUS) {
            removeFieldsErrors();
            $("#impUSV_PASSWORD").val("");
            $("#impUSV_USERNAME").val(USV_USERNAME);                        
            $("#<%= DDUSV_State.ClientID %> option[value='" + USV_STATUS + "']").attr('selected', 'selected');
            $("#dialog").dialog({ width: 400, height: 180, modal: true, title: 'Editar ' + USV_USERNAME, open: true });
            $("#dialog").dialog({ buttons: [{
                text: "Actualizar Usuario",
                click: function () {
                    if (Validate())
                        updateFunctionAjax(USN_USERID, USD_CREATION);
                }
            }]
            });
        }

        function updateFunctionAjax(USN_USERID, USD_CREATION) {
            $.ajax({
                type: "POST",
                data: "{'USN_USERID': '" + USN_USERID + "','USV_USERNAME': '" + $("#impUSV_USERNAME").val() + "','USV_PASSWORD': '" + $("#impUSV_PASSWORD").val() + "','USD_CREATION': '" + USD_CREATION + "','USV_STATUS': '" + $("#<%= DDUSV_State.ClientID %>").val() + "'}",
                url: "panelEditUsers.aspx/ajaxUpdateFunction",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d == new String("1")) {
                        $('#dialog').dialog("close");
                    }
                    else {
                        alert("Ocurrio un problema durante la actualizacion");
                    }
                },
                error: function (result) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + result); }
            });
        }

        function insertUserToControl(BDN_ID, BDV_EMAIL, BDV_DOCUMENT_NO) {
            $.ajax({
                type: "POST",
                data: "{'BDN_ID': '" + BDN_ID + "','BDV_EMAIL': '" + BDV_EMAIL + "','BDV_DOCUMENT_NO': '" + BDV_DOCUMENT_NO + "'}",
                url: "panelEditUsers.aspx/ajaxInsertUserToControl",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d == new String("1")) {
                        alert("Usuario creado correctamente. ");
                    }
                    else {
                        alert("Ocurrio un problema durante la insercion");
                    }
                },
                error: function (result) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + result); }
            });
        }

        function Validate() {
            var bValid = true;
            removeFieldsErrors();

            bValid = bValid && checkNull($("#impUSV_USERNAME"), "* Nombre Usuario no puede estar vacio");
            bValid = bValid && checkNull($("#impUSV_PASSWORD"), "* Password no puede estar vacio");
            bValid = bValid && checkLength($("#impUSV_PASSWORD"), "Password ", 8, 20);
            

            return bValid;
        }

        function removeFieldsErrors() {
            $("#validateTips").text("");
            $("#impUSV_USERNAME").removeClass("ui-state-error");
            $("#impUSV_PASSWORD").removeClass("ui-state-error");            
        }

        // Validaciones
        function checkNull(field, t) {
            if (field.val() == "") {
                field.addClass("ui-state-error")
                updateTips(t);
                return false;
            }
            else { return true; }
        }

        function updateTips(t) {
            $("#validateTips")
			.text($("#validateTips").text() + t)
            .addClass("ui-state-highlight");
            setTimeout(function () {
                $("#validateTips").removeClass("ui-state-highlight", 3500);
            }, 500);
        }

        function checkLength(o, n, min, max) {
            if (o.val().length > max || o.val().length < min) {
                o.addClass("ui-state-error");
                updateTips("Tamaño del campo " + n + " debe estar entre " +
					min + " y " + max + ". ");
                return false;
            } else {
                return true;
            }
        }

        // Habilitar los botones despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                $("input:submit,button").button();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Actualizar Contraseña de usuario
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    <asp:Label ID="lblExpiration" runat="server" Text=""></asp:Label>Realice el cambio
    de la clave de acceso a su cuenta, recuerde que la clave debe de poseer mas de 8
    caracteres alfanumericos.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ValidationSummary ID="summaryError" runat="server" CssClass="ui-state-highlight"
        HeaderText="Corrija los siguientes campos" ShowMessageBox="True" ValidationGroup="valerror"
        ForeColor="" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
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
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="btnConsultarCedula" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Buscar por Nombre</a></li>
            <li><a href="#tabs-2">Buscar por Cedula</a></li>
            <%--<li><a href="#tabs-3">Adicionar Usuario</a></li>--%>
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
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click"
                            ValidationGroup="G1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:UpdatePanel ID="PanelGrid" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GridUsers" runat="server" AutoGenerateColumns="false" SkinID="gridviewSkin"
                                    OnPageIndexChanging="GridUsers_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="usu_id" HeaderText="ID" />
                                        <asp:BoundField DataField="usu_nombre" HeaderText="Nombre Usuario" />
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />                                        
                                        <asp:BoundField DataField="usu_est_id" HeaderText="Estado" />
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <a href="#" onclick="editUser('<%# Eval("usu_id") %>','<%# Eval("usu_nombre") %>','<%# Eval("usu_contraseña") %>','<%# Eval("usu_fecha_cre") %>','<%# Eval("usu_est_id") %>')">
                                                    <img src="../../Design/images/Botones/b_edit_order.png" alt="Editar" />
                                                </a>
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
            </table>
        </div>
        <div id="tabs-2">
            <asp:UpdatePanel ID="uPanelUser" runat="server">
                <ContentTemplate>
                    <table cellpadding="6" cellspacing="6" width="100%">
                        <tr>
                            <td>
                                <label class="f12">
                                    Numero de cedula del Usuario:
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCedula" runat="server" AccessKey="u"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCedula"
                                    CssClass="error" Display="Dynamic" ErrorMessage="Digite un numero de cedula."
                                    ForeColor="" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnConsultarCedula" runat="server" Text="Consultar" OnClick="btnConsultarCedula_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="f13">
                                    Cedula <u>u</u>suario:
                                </label>
                                <asp:Label ID="lblCedula" Text="" runat="server" />
                            </td>
                            <td>
                                <label class="f13">
                                    Nombre <u>u</u>suario:
                                </label>
                                <asp:Label ID="lblNombre" Text="" runat="server" />
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <div>
                                    <asp:Panel ID="pnlInforUser" runat="server" Visible="false">
                                        <table border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <label class="f13">
                                                        Nombre de Usuario:
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUSVUserName" runat="server" Width="250px" />
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUSVUserName"
                                                        CssClass="error" Display="Dynamic" ErrorMessage="Digite nombre de usuario" ValidationGroup="valerror"
                                                        ForeColor="" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label class="f13">
                                                        Password:
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtUSVPassword" TextMode="Password" runat="server" Width="250px" />
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUSVPassword"
                                                        CssClass="error" Display="Dynamic" ErrorMessage="Digite nueva contraseña " ValidationGroup="valerror"
                                                        ForeColor="" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="regexTextBox1" Display="Dynamic" ControlToValidate="txtUSVPassword"
                                                        runat="server" ValidationExpression="^[\s\S]{8,100}$" Text="La nueva clave debe de poseer minimo 8 caracteres"
                                                        ErrorMessage="La nueva clave debe de poseer minimo 8 caracteres." ValidationGroup="valerror">*</asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <label class="f13">
                                                        Estado:
                                                    </label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DDUSVStatus" runat="server">
                                                        <asp:ListItem Text="Activo" Value="A" />
                                                        <asp:ListItem Text="Inactivo" Value="I" />
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btnCancel" Text="Cancelar" runat="server" Width="100px" OnClick="btnCancel_Click" />
                                                </td>
                                                <td align="center">
                                                    <asp:Button ID="btnSave" Text="Guardar" runat="server" Width="100px" ValidationGroup="valerror"
                                                        OnClick="btnSave_Click" />
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnConsultarCedula" EventName="click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
<%--        <div id="tabs-3" >
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table cellpadding="6" cellspacing="6" width="100%">
                        <tr>
                            <td>
                                <label class="f12">
                                    Numero de cedula del Usuario:
                                </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCedulaUSR" runat="server" AccessKey="u" ValidationGroup="G2"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCedulaUSR"
                                    CssClass="error" Display="Dynamic" ErrorMessage="Digite un numero de cedula."
                                    ForeColor="" SetFocusOnError="True" ValidationGroup="G2">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:Button ID="btnConstulatUSR" runat="server" Text="Consultar" ValidationGroup="G2"
                                    OnClick="btnConstulatUSR_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <div>
                                    <asp:GridView ID="GridUsersBasicData" SkinID="gridviewSkin" runat="server" AutoGenerateColums="fase">
                                        <Columns>
                                            <asp:BoundField DataField="bdn_id" HeaderText="ID" />
                                            <asp:BoundField DataField="name" HeaderText="Nombre" />
                                            <asp:BoundField DataField="bdv_document_no" HeaderText="Documento" />
                                            <asp:BoundField DataField="bdv_email" HeaderText="Correo Electronico" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <center>
                                                        <a href="#" onclick="insertUserToControl('<%# Eval("bdv_co") %>','<%# Eval("bdn_id") %>','<%# Eval("bdv_email") %>','<%# Eval("bdv_document_no") %>')" />
                                                        Adicionar Usuario Sistema Aquarella </a>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>--%>
    </div>
    <div id="dialog" class="f13" style="display: none; font-size: 10px">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Nombre Usuario:
                </td>
                <td>
                    <input id="impUSV_USERNAME" type="text" name="name" value=" " />
                </td>
            </tr>
            <tr>
                <td>
                    Contraseña:
                </td>
                <td>
                    <input id="impUSV_PASSWORD" type="password" name="name" value=" " />
                </td>
            </tr>            
            <tr>
                <td>
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="DDUSV_State" runat="server">
                        <asp:ListItem Text="Activo" Value="A" />
                        <asp:ListItem Text="Inactivo" Value="I" />
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <p id="validateTips">
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

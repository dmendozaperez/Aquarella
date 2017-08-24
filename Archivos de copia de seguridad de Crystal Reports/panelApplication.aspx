<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    CodeBehind="panelApplication.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Control.panelApplication"
    Theme="SiteTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <title>Aplicaciones</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tabs").tabs({
                collapsible: true
            });
            $('#tabs').tabs('select', '#tab-1'); // Para seleccionar el tab 1 y que este colapsado el panel de insercion de rol 

        });

        function updateApp(APN_ID, APV_NAME, APV_TYPE, APV_URL, APN_ORDER, APV_STATUS, APV_HELP, APV_COMMENTS) {
            removeFieldsErrors();
            $("#impName").val(APV_NAME);
            $("#<%= DDType2.ClientID %> option[value='" + APV_TYPE + "']").attr('selected', 'selected');
            $("#impURL").val(APV_URL);
            $("#impOrden").val(APN_ORDER);
            $("#<%= DDState2.ClientID %> option[value='" + APV_STATUS + "']").attr('selected', 'selected');
            $("#impUrlHelp").val(APV_HELP);
            $("#impComent").val(APV_COMMENTS);
            $("#dialog_update").dialog({ width: 400, height: 290, modal: true, title: 'Editar ' + APV_NAME, open: true });
            $("#dialog_update").dialog({ buttons: [{
                text: "Actualizar Aplicacion",
                click: function () {
                    if (Validacion())
                        updateAppAJAX(APN_ID);
                }
            }]
            });
        }

        function updateAppAJAX(APN_ID) {
            $.ajax({
                type: "POST",
                data: "{'APN_ID': '" + APN_ID + "','APV_NAME': '" + $("#impName").val() + "','APV_TYPE': '" + $("#<%= DDType2.ClientID %>").val() +
                "','APV_URL': '" + $("#impURL").val() + "','APN_ORDER': '" + $("#impOrden").val() + "','APV_STATUS': '" + $("#<%= DDState2.ClientID %>").val() +
                "','APV_HELP': '" + $("#impUrlHelp").val() + "','APV_COMMENTS': '" + $("#impComent").val() + "'}",
                url: "panelApplication.aspx/ajaxUpdateApp",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d == new String("1")) {
                        $('#dialog_update').dialog("close");
                    }
                    else {
                        alert("Occurrio un error Durante la Actualizacion");
                    }
                },
                error: function (result) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + result); }
            });
        }

        function Validacion() {
            var bValid = true;
            removeFieldsErrors();

            bValid = bValid && checkNull($("#impName"), "*Nombre de la Aplicacion no puede estar vacio");

            return bValid;
        }

        function removeFieldsErrors() {
            $("#validateTips").text("");
            $("#impName").removeClass("ui-state-error");
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

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $("input:submit,button").button();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Control de Aplicaciones
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Muestra la lista de Aplicaciones (Formularios Web). Permite crear nuevas Aplicación
    para utilizar en el Sistema y editar las existentes.
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
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <AQControl:Message runat="server" ID="msnMessage" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Nueva Aplicacion</a></li>
        </ul>
        <div id="tabs-1">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        Nombre:
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombre" runat="server" Width="220px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RFValidator" runat="server" ControlToValidate="txtNombre"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo:
                    </td>
                    <td>
                        <asp:DropDownList ID="DDType" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Ruta URL App:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRutaUrl" runat="server" Width="220px" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Orden:
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrden" runat="server" Width="220px" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Estado
                    </td>
                    <td>
                        <asp:DropDownList ID="DDStatus" runat="server">
                            <asp:ListItem Text="Activo" Value="A" Selected="True" />
                            <asp:ListItem Text="Inactivo" Value="I" />
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Ruta URL Help:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRutaUrlHelp" runat="server" Width="220px" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Comentario:
                    </td>
                    <td>
                        <asp:TextBox ID="txtComentario" runat="server" Width="220px" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <asp:Button ID="SaveApp" Text="Guardar" runat="server" OnClick="SaveApp_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 70%">
            </td>
            <td style="width: 20%" align="right">
                <asp:TextBox ID="txtFilterGrid" runat="server" />
            </td>
            <td style="width: 10%">
                <asp:Button ID="btnFiltrar" Text="Filtrar Por nombre" runat="server" OnClick="btnFiltrar_Click"
                    ValidationGroup="G1" />
            </td>
        </tr>
    </table>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridApplication" runat="server" AutoGenerateColumns="false" SkinID="gridviewSkin"
                OnPageIndexChanging="GridApplication_PageIndexChanging" Width="450px">
                <Columns>
                    <asp:BoundField DataField="apl_id" HeaderText="Id" />                    
                    <asp:BoundField DataField="apl_nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="apl_tip_id" HeaderText="Tipo" />
                    <asp:BoundField DataField="apl_orden" HeaderText="Orden" />
                    <asp:BoundField DataField="apl_est_id" HeaderText="Estado" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <center>
                                <a href="#" onclick="updateApp('<%# Eval("apl_id") %>','<%# Eval("apl_nombre") %>','<%# Eval("apl_tip_id") %>','<%# Eval("apl_url") %>','<%# Eval("apl_orden") %>','<%# Eval("apl_est_id") %>','<%# Eval("apl_ayuda") %>','<%# Eval("apl_comentario") %>')">
                                    <asp:Image ID="Image1" ImageUrl="~/Design/images/Botones/editOrder.png" runat="server" />
                                </a>
                            </center>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="dialog_update" class="f13" style="display: none; font-size: 10px;">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Nombre:
                </td>
                <td>
                    <input id="impName" type="text" name="name" value=" " style="width: 220px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Tipo:
                </td>
                <td>
                    <asp:DropDownList ID="DDType2" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Ruta URL App:
                </td>
                <td>
                    <input id="impURL" type="text" name="name" value=" " style="width: 220px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Orden:
                </td>
                <td>
                    <input id="impOrden" type="text" name="name" value=" " style="width: 220px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="DDState2" runat="server">
                        <asp:ListItem Text="Activo" Value="A" Selected="True" />
                        <asp:ListItem Text="Inactivo" Value="I" />
                    </asp:DropDownList>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Ruta URL Help:
                </td>
                <td>
                    <input id="impUrlHelp" type="text" name="name" value=" " style="width: 220px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Comentario:
                </td>
                <td>
                    <input id="impComent" type="text" name="name" value=" " style="width: 220px" />
                </td>
                <td>
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

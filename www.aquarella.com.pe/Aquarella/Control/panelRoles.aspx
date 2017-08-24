<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="panelRoles.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Control.panelRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <title>Control de Roles</title>
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ width: "35%", height: "50%", iframe: true });
            $("#tabs").tabs({
                collapsible: true
            });
            $('#tabs').tabs('select', '#tab-1'); // Para seleccionar el tab 1 y que este colapsado el panel de insercion de rol 
        });

        function updateRol(ron_id, rov_name, rov_description) {
            removeFieldsErrors();
            $("#impRov_Name").val(rov_name);
            $("#impRov_Desc").val(rov_description);
            $("#dialog").dialog({ width: 400, height: 190, modal: true, title: 'Editar ' + rov_name, open: true });
            $("#dialog").dialog({ buttons: [{
                text: "Actualizar Rol",
                click: function () {
                    if (Validate())
                        updateRolAJAX(ron_id);
                }
            }]
            });
        }

        function updateRolAJAX(ron_id) {
            $.ajax({
                type: "POST",
                data: "{ 'RON_ID': '" + ron_id + "','ROV_NAME': '" + $("#impRov_Name").val() + "','ROV_DESCRIPTION': '" + $("#impRov_Desc").val() + "'}",
                url: "panelRoles.aspx/ajaxUpdateRole",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    $('#dialog').dialog("close");
                },
                error: function (result) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + result); }
            });
        }

        function showdialog() {
            $("#dialog_add").dialog({ width: 380, height: 150, modal: true, title: 'Nuevo Rol', open: true });
        }

        function Validate() {
            var bValid = true;
            removeFieldsErrors();

            bValid = bValid && checkNull($("#impRov_Name"), "* Nombre Rol no puede esta vacio");

            return bValid;
        }

        function removeFieldsErrors() {
            $("#validateTips").text("");
            $("#impRov_Name").removeClass("ui-state-error");
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

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "35%", height: "50%", iframe: true });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Control de Roles
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Muestra la lista de Roles utilizados en el Sistema. Permite crear nuevos Roles para
    utilizar en el Sistema y editar los existentes.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label Text="" ID="lblMsg" runat="server" />
    <asp:ValidationSummary ID="summaryError" runat="server" CssClass="ui-state-highlight"
        HeaderText="Corrija los siguientes campos" ShowMessageBox="True" ValidationGroup="valerror"
        ForeColor="" />
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
    <br />
    <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Nuevo Rol</a></li>
        </ul>
        <div id="tabs-1">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="f12">
                        Nombre:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txbNombreRol" Width="250px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txbNombreRol"
                            ErrorMessage="*" />
                    </td>
                </tr>
                <tr>
                    <td class="f12">
                        Descripción:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txbDescRol" Width="250px" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <asp:Button ID="BtnSaveRol" runat="server" Text="Guardar Rol" OnClick="BtnSaveRol_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dialog_add" class="f13" style="display: block; font-size: 10px;">
    </div>
    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridRoles" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="GridRoles_PageIndexChanging">
                <EmptyDataTemplate>
                    No hay datos para mostrar</EmptyDataTemplate>
                <Columns>                    
                    <asp:BoundField DataField="rol_id" HeaderText="ID" />
                    <asp:BoundField DataField="rol_nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="rol_descripcion" HeaderText="Descripcion" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <center>
                                <a href="#" onclick="javascript:updateRol('<%# Eval("rol_id")%>','<%# Eval("rol_nombre") %>', '<%# Eval("rol_descripcion") %>')">
                                    <asp:Image ID="Image1" ImageUrl="~/Design/images/Botones/editOrder.png" runat="server" />
                                </a>
                            </center>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Funciones">
                        <ItemTemplate>
                            <center>
                                <a class="iframe" href="panelRole_Func.aspx?RON_ID=<%# Eval("rol_id")%>&ROV_NAME=<%# Eval("rol_nombre") %>"
                                   title="Adicionar funcion a Rol.">
                                    <asp:Image ID="Image2" ImageUrl="~/Design/images/Botones/b_app.png" runat="server" />
                                </a>
                            </center>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="dialog" class="f13" style="display: none; font-size: 10px;">
        <br />
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Nombre:
                </td>
                <td>
                    <input id="impRov_Name" type="text" style="width: 300px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Descripcion:
                </td>
                <td>
                    <input id="impRov_Desc" type="text" style="width: 300px" />
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

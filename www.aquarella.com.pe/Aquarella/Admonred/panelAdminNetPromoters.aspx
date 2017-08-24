<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="panelAdminNetPromoters.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.panelAdminNetPromoters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#rbList").buttonset();
        });

        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                $("#rbList").buttonset();
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        var promoter = '', idTxtEmail = '',
            txtEmail = '',
            idDwTypeCoord = '',
            dwTypeCoord = '',
            idDwArea = '',
            dwArea = '',
            idDwNewCoord = '',
            dwNewCoord = '',
            valid = '',
            allFields = '',
            isr = ' es requerido.',
            btRefresh = '';
        $(function () {
            // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
            $("#dialog:ui-dialog").dialog("destroy");
            $("#dialog-form2").dialog("destroy");

            //
            function checkRegexp(o, regexp, n) {
                if (!(regexp.test(o.val()))) {
                    o.addClass("ui-state-error");
                    //updateTips(n);
                    return false;
                } else {
                    return true;
                }
            }
            $("#dialog-form2").dialog({
                autoOpen: false,
                height: 300,
                width: 620,
                modal: true,
                buttons: {
                    "Cambiar coordinador": function () {
                        var bValid = true;
                        allFields.removeClass("ui-state-error");
                        dwNewCoord = $("#" + idDwNewCoord + " :selected").val();

                        if (dwNewCoord == '-1') {
                            valid += '\n > Seleccionar el nuevo coordinador ' + isr;
                            $("#" + idDwNewCoord).addClass("ui-state-error");
                            bValid = false;
                            $("#" + idDwNewCoord).focus();
                        }
                        if (bValid) {
                            // Cambiar de coordinador el promotor seleccionado
                            var a = updatePromoter(promoter, dwNewCoord);
                            $(this).dialog("close");
                        }
                        else
                            alert("Error :" + valid);
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#dialog-form").dialog({
                autoOpen: false,
                height: 340,
                width: 620,
                modal: true,
                buttons: {
                    "Crear Nuevo Cliente": function () {
                        var bValid = true;
                        allFields.removeClass("ui-state-error");
                        dwTypeCoord = $("#" + idDwTypeCoord + " :selected").val();
                        dwArea = $("#" + idDwArea + " :selected").val();
                        txtEmail = $("#" + idTxtEmail).val();

                        if (txtEmail == null || txtEmail.length < 7) {
                            valid += '\n > Un correo real ' + isr;
                            $("#" + idTxtEmail).focus();
                        }
                        // From jquery.validate.js (by joern), contributed by Scott Gonzalez: http://projects.scottsplayground.com/email_address_validation/
                        bValid = bValid && checkRegexp($("#" + idTxtEmail), /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i, "eg. ui@manisol.com.co");

                        if (dwTypeCoord == '-1') {
                            valid += '\n > Seleccionar un tipo de coordinador ' + isr;
                            $("#" + idDwTypeCoord).addClass("ui-state-error");
                            bValid = false;
                            $("#" + idDwTypeCoord).focus();
                        }
                        if (dwArea == '-1') {
                            valid += '\n > Seleccionar un area de influencia ' + isr;
                            $("#" + idDwArea).addClass("ui-state-error");
                            bValid = false;
                            $("#" + idDwArea).focus();
                        }
                        if (bValid) {
                            // Convertir a coordinador                            
                            upgradePromoter(promoter, dwTypeCoord, dwArea, txtEmail);
                            $(this).dialog("close");
                        }
                        else
                            alert("Error :" + valid);
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    allFields.val("").removeClass("ui-state-error");
                }
            });
        });
        // Cambiar de coordinador
        function changeCoord(idProm, nameProm, nameCoord, dwNewCoord, idBtRefresh) {
            var url = this.href;
            promoter = idProm;
            idDwNewCoord = dwNewCoord;
            $('#txtCoordActual').val(nameCoord);
            allFields = $([]).add($('#' + dwNewCoord));
            btRefresh = $('#' + idBtRefresh);
            $('#dialog-form2').dialog('option', 'title', 'Cambiar de coordinador a : (' + nameProm + ')');
            $("#dialog-form2").dialog("open");
            //var dialog = $('<div style="display:hidden;font-size: 9px;" title="Estadísticas del artículo : ' + a + '"></div>').appendTo('body');
        }
        // Convertir a cordinador
        function convertToCoord(idProm, nameProm, dwTipoCoordinador, dwArea, txtEmail) {
            var url = this.href;
            promoter = idProm;
            idDwTypeCoord = dwTipoCoordinador;
            idDwArea = dwArea;
            idTxtEmail = txtEmail;
            allFields = $([]).add($('#' + dwTipoCoordinador)).add($('#' + dwArea)).add($('#' + txtEmail));
            $('#dialog-form').dialog('option', 'title', 'Convertir en nuevo cliente a : (' + nameProm + ')');
            $("#dialog-form").dialog("open");
            //var dialog = $('<div style="display:hidden;font-size: 9px;" title="Estadísticas del artículo : ' + a + '"></div>').appendTo('body');
        }

        // Ajax: Cambio de coordinador asociado a un promotor
        function updatePromoter(promoter, newCoord) {
            $.ajax({
                type: "POST",
                ///data: "{}",
                data: "{ promoter: " + promoter + ",newCoord:" + newCoord + "}",
                url: "panelAdminNetPromoters.aspx/ajaxUpdatePromoter",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    ///
                    alert('Cambios realizados con exito.');
                    $("input[id$='btConsult']").click();
                },
                error: function (msg) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + msg); }
            });        // Fin jquery AJAX
        }

        // Ajax: Convertir un promotor a coordinador
        function upgradePromoter(promoter, typeCoord, area, mail) {
            $.ajax({
                type: "POST",
                ///data: "{}",
                data: "{ promoter: " + promoter + ",typeCoord:'" + typeCoord + "',area:'" + area + "',mail:'" + mail + "'}",
                url: "panelAdminNetPromoters.aspx/ajaxUpgradePromoter",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    //
                    ///
                    alert('Cambios realizados con exito.');
                    $("input[id$='btConsult']").click();
                },
                error: function (msg) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + msg); }
            });      // Fin jquery AJAX
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Administrador de pomotores y coordinadores
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Formulario de actualizacion de promotor a coordinador, o de cambio de caracteristicas
    principales del coordinador; para realizarlo de forma correcta por favor debera
    tramitar todos los campos; el tipo de cliente de consulta se refiere a sobre que
    clase de cliente desea realizar la consulta.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <asp:UpdatePanel ID="upPanelMsg" runat="server">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
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
    <!-- -->
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="fsal f13" colspan="2">
                    Formulario de consulta
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="4" cellspacing="4">
                        <tr>
                            <!---->
                            <td>
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td class="f12" colspan="2">
                                            <u>D</u>ocumento de cliente:
                                        </td>
                                        <td class="f12">
                                            Tipo de cliente consulta
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDoc" runat="server" AccessKey="d"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="RequiredFieldValidator1"
                                                runat="server" ToolTip="Fecha final" CssClass="error_asterisck" ErrorMessage="Dígite fecha final*"
                                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDoc">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <div id="rbList">
                                                <asp:UpdatePanel ID="upRbl" runat="server">
                                                    <ContentTemplate>
                                                        <asp:RadioButtonList ID="rdGroup" runat="server" RepeatDirection="Horizontal" AppendDataBoundItems="true">
                                                            
                                                            <asp:ListItem Value="C"  Selected="True">Cliente</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                                                CausesValidation="true" OnClick="btConsult_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>
    </div>
    <!-- -->
    <div style="margin: 10px auto 0 auto;">
        <div>
            <table border="0" width="100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td align="center">
                        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DetailsView ID="dvCustomer" Font-Size="Medium" runat="server" Width="700px"
                                    Visible="False" Caption="Información de cliente directo" FooterText="Puede cambiar la comision o la regional"
                                    AutoGenerateRows="False" SkinID="detailsviewSkin" 
                                    OnItemCommand="dvCustomer_ItemCommand">
                                    <Fields>
                                        <asp:BoundField DataField="bas_id" HeaderText="Identificador" ReadOnly="True"
                                            SortExpression="bas_id" />
                                        <asp:BoundField DataField="bas_documento" HeaderText="Documento" ReadOnly="True"
                                            SortExpression="bas_documento" />
                                        <asp:BoundField DataField="nombrecompleto" HeaderText="Nombre CLiente" ReadOnly="True"
                                            SortExpression="nombrecompleto" />
                                        <asp:BoundField DataField="Ubicacion" HeaderText="Ciudad" ReadOnly="True"
                                            SortExpression="Ubicacion" />
                                        <asp:BoundField DataField="bas_fecha_cre" HeaderText="Fecha de inscripción" ReadOnly="True"
                                            SortExpression="bas_fecha_cre" DataFormatString="{0:D}" />
                                        <asp:BoundField DataField="Bas_Direccion" HeaderText="Dirección" ReadOnly="True" SortExpression="bdv_address" />
                                        <asp:BoundField DataField="Bas_Telefono" HeaderText="Telefono" ReadOnly="True" SortExpression="Bas_Telefono" />                                       
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:DropDownList AccessKey="o" ID="dwStatus" runat="server" DataValueField="Est_Id"
                                                    SelectedValue='<%# Bind("bas_est_id") %>' DataTextField="Est_Descripcion" AppendDataBoundItems="True"
                                                    Width="320px" DataSourceID="odsStatus">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Lider">
                                            <ItemTemplate>
                                                <asp:DropDownList AccessKey="o" ID="dwArea" runat="server" Width="320px" DataValueField="are_id"
                                                    SelectedValue='<%# Bind("bas_are_id") %>' DataTextField="are_descripcion" AppendDataBoundItems="True"
                                                    DataSourceID="odsAreas">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>                                       
                                        <asp:TemplateField HeaderText="Cambiar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Button CommandArgument='<%# Eval("bas_documento")%>' CommandName="EditCustomer"
                                                    ID="Button1" runat="server" Text="Guardar Cambios" ToolTip="Guardar cambios realizados sobre el cliente" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Fields>
                                    <EmptyDataTemplate>
                                        No existen registros para el cliente; revise e inténtelo de nuevo.
                                    </EmptyDataTemplate>
                                    <RowStyle HorizontalAlign="Left" />
                                </asp:DetailsView>
                                <asp:DetailsView ID="dvPromoter" Font-Size="Medium" runat="server" Width="700px"
                                    Visible="false" Caption="Información del Promotor" FooterText="Puede realizar la conversión a Promotor Directo"
                                    AutoGenerateRows="False" SkinID="detailsviewSkin">
                                    <Fields>
                                        <asp:BoundField DataField="cedi" HeaderText="Bodega Coordinador" ReadOnly="True"
                                            SortExpression="cedi" />
                                        <asp:BoundField DataField="cedulac" HeaderText="Documento Coordinador" ReadOnly="True"
                                            SortExpression="cedulac" />
                                        <asp:BoundField DataField="nombrecompletoc" HeaderText="Nombre Coordinador" ReadOnly="True"
                                            SortExpression="nombrecompletoc" />
                                        <asp:BoundField DataField="bdv_status" HeaderText="Estado" ReadOnly="True" SortExpression="bdv_status" />
                                        <asp:BoundField DataField="ubicationcustomerc" HeaderText="Ciudad Coordinador" ReadOnly="True"
                                            SortExpression="ubicationcustomerc" />
                                        <asp:BoundField DataField="area" HeaderText="Área Coordinador" ReadOnly="True" SortExpression="area" />
                                        <asp:BoundField DataField="cedulap" HeaderStyle-CssClass="fsal" HeaderText="Documento Promotor"
                                            ReadOnly="True" SortExpression="cedulap" />
                                        <asp:BoundField DataField="nombrecompletop" HeaderStyle-CssClass="fsal" HeaderText="Nombre Promotor"
                                            ReadOnly="True" SortExpression="nombrecompletop" />
                                        <asp:BoundField DataField="ubicationcustomerp" HeaderStyle-CssClass="fsal" HeaderText="Ciudad Promotor"
                                            ReadOnly="True" SortExpression="ubicationcustomerp" />
                                        <asp:BoundField DataField="inscripcion" HeaderStyle-CssClass="fsal" HeaderText="Inscripción"
                                            ReadOnly="True" SortExpression="inscripcion" />
                                        <asp:BoundField DataField="prv_status" HeaderStyle-CssClass="fsal" HeaderText="Estado En Stencil"
                                            ReadOnly="True" SortExpression="prv_status" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Realizar Cambio de Coordinador">
                                            <ItemTemplate>
                                                <a style="cursor: pointer" title="Cambiar de coordinador asociado" onclick="javascript:changeCoord('<%# Eval("prn_promoter_id")%>','<%# Eval("nombrecompletop")%>','<%# Eval("nombrecompletoc")%>','<%= dwCoordinadores.ClientID %>','<%= btConsult.ClientID %>')">
                                                    <img alt="imprimir" src="../../Design/images/Botones/b_group.png" border="0" /></a></td>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Vincular Como Promotor Directo">
                                            <ItemTemplate>
                                                <a class='iframe' style="cursor: pointer" title="Convertir esta persona en un coordinador o cliente directo"
                                                    onclick="javascript:convertToCoord('<%# Eval("prn_promoter_id")%>','<%# Eval("nombrecompletop")%> Promotora de <%# Eval("nombrecompletoc")%>','<%= dwTipoCoordinador.ClientID%>','<%= dwArea.ClientID %>','<%= txtEmail.ClientID %>')">
                                                    <img alt="imprimir" src="../../Design/images/Botones/b_award.png" border="0" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Fields>
                                    <EmptyDataTemplate>
                                        No existen registros para el cliente; revise e inténtelo de nuevo.
                                    </EmptyDataTemplate>
                                    <RowStyle HorizontalAlign="Left" />
                                </asp:DetailsView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:ObjectDataSource ID="odsPromoters" runat="server" SelectMethod="getPromoter"
                            TypeName="www.aquarella.com.pe.bll.Basic_Data">
                            <SelectParameters>
                                <asp:Parameter Name="_co" Type="String" />
                                <asp:ControlParameter ControlID="txtDoc" DefaultValue="-1" Name="_doc" PropertyName="Text"
                                    Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsCustomer" runat="server" SelectMethod="getCustomerByDoc"
                            TypeName="www.aquarella.com.pe.bll.Coordinator" OnSelected="odsCustomer_Selected">
                            <SelectParameters>                                
                                <asp:ControlParameter ControlID="txtDoc" DefaultValue="-1" Name="_document" PropertyName="Text"
                                    Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsTypeCust" runat="server" SelectMethod="getTypeCustomer"
                            TypeName="www.aquarella.com.pe.Aquarella.Admonred.panelAdminNetPromoters"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsAreas" runat="server" SelectMethod="getAreas" TypeName="www.aquarella.com.pe.Aquarella.Admonred.panelAdminNetPromoters">
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsWare" runat="server" SelectMethod="getWare" TypeName="www.aquarella.com.pe.Aquarella.Admonred.panelAdminNetPromoters">
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsStatus" runat="server" SelectMethod="getStatus" TypeName="www.aquarella.com.pe.Aquarella.Admonred.panelAdminNetPromoters">
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!-- CONVERTIR PROMOTOR A COORDINADOR -->
    <div id="dialog-form" style="display: none" class="f13">
        <p class="validateTips">
            Formulario de actualizacion de promotor a coordinador; para realizarlo de forma
            correcta por favor debera tramitar todos los campos.</p>
        <fieldset>
            <table cellpadding="4" cellspacing="4" width="100%" align="center">
                <tr>
                    <td>
                        Usuario (E-Mail) :
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmail" Width="250px" CssClass="text ui-widget-content ui-corner-all"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo de coordinador :
                    </td>
                    <td>
                        <asp:DropDownList AccessKey="o" ID="dwTipoCoordinador" runat="server" CssClass="text ui-widget-content ui-corner-all"
                            Width="250px" DataValueField="CTV_COORDINATOR_ID" DataTextField="CTV_DESCRIPTION"
                            AppendDataBoundItems="True">
                            <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="dwTipoCoordinador"
                            Operator="NotEqual" ValueToCompare="-1" CssClass="error" ValidationGroup="valerror"
                            Display="Dynamic" ErrorMessage="Seleccione el tipo de coordinador">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Area de influencia :
                    </td>
                    <td>
                        <asp:DropDownList AccessKey="o" ID="dwArea" runat="server" CssClass="text ui-widget-content ui-corner-all"
                            Width="250px" DataValueField="are_id" DataTextField="are_descripcion" AppendDataBoundItems="True">
                            <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <!-- CAMBIAR DE COORDINADOR A UN PROMOTOR -->
    <div id="dialog-form2" style="display: none" class="f13">
        <p class="validateTips">
            Formulario de cambio de coordinador para un promotor; para realizarlo de forma correcta
            por favor debera tramitar todos los campos.</p>
        <fieldset>
            <table cellpadding="4" cellspacing="4" width="100%" align="center">
                <tr>
                    <td>
                        Coordinador actual :
                    </td>
                    <td>
                        <input type="text" readonly="readonly" id="txtCoordActual" name="txtCoordActual"
                            style="width: 250px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nuevo coordinador :
                    </td>
                    <td>
                        <asp:DropDownList ID="dwCoordinadores" Width="250px" DataTextField="nombres"
                            DataValueField="bas_id" AppendDataBoundItems="True" runat="server">
                            <asp:ListItem Value="-1" Text=" -- Seleccione de la Lista --"></asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="dwCoordinadores"
                            Operator="NotEqual" ValueToCompare="-1" CssClass="error" ValidationGroup="valerror"
                            Display="Dynamic" ErrorMessage="Seleccione el tipo de coordinador">*</asp:CompareValidator>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>

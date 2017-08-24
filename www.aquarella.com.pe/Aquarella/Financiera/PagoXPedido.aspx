<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="PagoXPedido.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.PagoXPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        var dwBanks, dwDepositType, txtNoVoucher, txtDate, txtValue, valid, allFields, allFieldsCust, allFieldsBd, isr = ' es requerido.', tips, btSavePay,txtpedido;
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
            $("input:text").width('200px');
            dwBanks = $("select[id$='dwBanks']");
            dwDepositType = $("select[id$='dwDepositType']");
            txtNoVoucher = $("input[id$='txtNoVoucher']");
            txtpedido = $("input[id$='txtpedido']");
            txtDate = $("input[id$='txtDate']");
            txtValue = $("input[id$='txtValue']");
            btSavePay = $("input[id$='btSavePay']");
            tips = $("#validateTips");
            allFields = $([]).add(dwBanks).add(dwDepositType).add(txtNoVoucher).add(txtDate).add(txtValue);

            $("input[id$='btSavePay']").click(function () {
                var bValid = true;
                tips.text("");
                allFields.removeClass("ui-state-error");

                //
                if (dwBanks.val() == "-1" || dwBanks.val() == "") {
                    bValid = false;
                    dwBanks.addClass("ui-state-error")
                    updateTips(" Seleccione la entidad recaudadora.");
                }

                //
                if (dwDepositType.val() == "-1" || dwDepositType.val() == "") {
                    bValid = false;
                    dwDepositType.addClass("ui-state-error")
                    updateTips(" Seleccione el tipo de medio de pago.");
                }

                bValid = bValid && checkLength(txtNoVoucher, "Número de consignación", 3, 20);
                bValid = bValid && checkLength(txtpedido, "Número de pedido", 3, 20);
                //bValid = bValid && checkLength(txtDate, "Fecha de recaudo", 5, 12);
                //bValid = bValid && checkRegexp(txtDate, /^[0,1]?\d{1}\/(([0-2]?\d{1})|([3][0,1]{1}))\/(([1]{1}[9]{1}[9]{1}\d{1})|([2-9]{1}\d{3}))$/, " Dígite fechas validas (dd/mm/yyyy).");
                bValid = bValid && checkRegexp(txtDate, /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/, " Dígite fechas validas (dd/mm/yyyy).");
                bValid = bValid && checkRegexp(txtValue, /^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$/, " Sólo números en el campo valor.");
                // /^([0-9])+$/ // solo numeros

                return bValid;
            });
        });
        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
            }
        }
    </script>
    <script type="text/javascript" language="javascript">

        // Validaciones
        function updateTips(t) {
            tips
            .text(tips.text() + t)
            .addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
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

        //
        function checkRegexp(o, regexp, n) {
            if (!(regexp.test(o.val()))) {
                o.addClass("ui-state-error");
                updateTips(n);
                return false;
            } else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Registro de pagos y consignaciones
    x pedido
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Realice el registro de consignaciones x pedido realizAQUARELLAs; recuerde que para el uso del monto
    registrado, deberá esperar a la verificación de valides frente a la empresa recaudadora.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p id="validateTips">
    </p>
    <asp:ScriptManager ID="ScriptManager1" runat="server" 
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <asp:UpdatePanel ID="upPanelMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
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
    <!-- SEL CUSTOMER -->
    <asp:Panel ID="pnlDwCustomers" Visible="false" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
                <tr>
                    <td>
                        <div style="font-family:Verdana; font-size:10px; font-weight:bold;">
                            <a class='iframe' href='panelPaymentList.aspx' title="Historial De Liquidaciones (LIQ. NO.) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                Consultar historial de consignaciones</a>&nbsp;&nbsp;
                            <a class='iframe' href='panelPaymentList.aspx' title="Historial De Liquidaciones (LIQ. NO.) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                <img alt="Consultar Historia de consignaciones"  style="border:0;" src="../../Design/images/lupa.jpg" />
                            </a>
                        </div> <br />
                    </td>
                </tr>
                <tr>
                    <td class="fsal f13" colspan="2">
                        Selección de Lider
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="1" cellspacing="1">
                            <tr>
                                <td class="f12" width="60%">
                                    Seleccione un Lider de la lista:<br />
                                    (Desc: Seleccione un Lider sobre el cual realizará las acciones.)
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="dwCustomers" DataTextField="Are_Descripcion" DataValueField="Are_Id"
                                                AppendDataBoundItems="true" runat="server" ToolTip="Selecionar un cliente" Width="220px">
                                                <asp:ListItem Value="-1" Text=" -- Seleccione de la Lista --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
                                            <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <!-- FORM PAYMENTS -->
    <table cellpadding="6" cellspacing="6" width="100%">
        <tr>
            <td class="fsal f13" colspan="2">
                Formulario de registro de recaudo
            </td>
        </tr>
        <tr>
            <td>
                <label for="dwBanks" class="f12">
                    *Entidad recaudadora:
                </label>
                <br />
                <label for="dwBanks">
                    (Ej: Banco de crédito del Perú, etc)
                </label>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="dwBanks" runat="server" AccessKey="b" AppendDataBoundItems="True"
                            DataTextField="Ban_Descripcion" DataValueField="Ban_Id" ValidationGroup="valerror">
                            <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <label for="dwDepositType" class="f12">
                    *<u>M</u>edio de pago:
                </label>
                <br />
                <label for="dwDepositType">
                    (Ej: Consignación en efectivo, mediante cheque, etc)
                </label>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="dwDepositType" runat="server" AccessKey="b" AppendDataBoundItems="True"
                            DataTextField="Con_Descripcion" DataValueField="Con_Id" ValidationGroup="valerror">
                            <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <label for="txtNoVoucher" class="f12">
                    *<u>C</u>omprobante de pago:</label><br />
                <label for="txtNoVoucher">
                    (Ej: 13487326-8)</label>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNoVoucher"  runat="server" AccessKey="c" MaxLength="8" 
                            ToolTip="Maximo 8 digitos"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
         <tr style="background-color: #CCFF66">
            <td>
                <label for="txtpedido" class="f12">
                    *Numero de Pedido:</label><br />
                <label for="txtpedido">
                    (Ej: Numero de pedido del promotor o lider)</label>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtpedido"  runat="server" AccessKey="c" MaxLength="8" 
                            ToolTip="ingrese el numero de pedido"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <label for="txtDate" class="f12">
                    *<u>F</u>echa de la consignacion:
                </label>
                <br />
                <label for="txtDate">
                    (dd/mm/aa Ej: 26/09/08, 1/9/2008)</label>
            </td>
            <td>
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtDate" runat="server" AccessKey="f"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calendar" runat="server" TargetControlID="txtDate"
                                        Format="dd/MM/yyyy" Animated="true" PopupButtonID="imgCalendar" FirstDayOfWeek="Monday" />
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
                                    <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                        <td style="padding-left: 10px;">
                            <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                Style="cursor: hand;" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <label for="txtValue" class="f12">
                    *Valor consignado:
                </label>
                <br />
                <label for="txtValue">
                    (Ej: 57000, 120000, 2500000, 0,23)</label>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtValue" runat="server" AccessKey="v" ValidationGroup="valerror"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <label for="txtNotes" class="f12">
                    <u>N</u>otas adicionales:
                </label>
                <br />
                <label for="txtNotes" class="f12">
                    (Ej: El banco me dice que esta consignacion se refleja dos dias despues.)</label>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtNotes" runat="server" AccessKey="n" MaxLength="100" Rows="5"
                            TextMode="MultiLine" Width="200px"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btSavePay" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="btReset" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <div style="font-family:Verdana; font-size:10px; font-weight:bold;">
                                <a class='iframe' href='panelPaymentList.aspx' title="Historial De Liquidaciones (LIQ. NO.) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                    Consultar historial de consignaciones</a>&nbsp;&nbsp;
                                <a class='iframe' href='panelPaymentList.aspx' title="Historial De Liquidaciones (LIQ. NO.) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                    <img alt="Consultar Historia de consignaciones"  style="border:0;" src="../../Design/images/lupa.jpg" />
                                </a>
                            </div>
                        </td>
                        <td align="center">
                            <asp:Button ID="btReset" runat="server" AccessKey="g" Text="Restablecer" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btSavePay" runat="server" AccessKey="g" Text="(G)uardar pago" OnClick="btSavePay_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

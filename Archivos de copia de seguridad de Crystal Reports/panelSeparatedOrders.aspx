<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="panelSeparatedOrders.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.panelSeparatedOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
            $('#deleteConfirmationDialog').dialog({
                autoOpen: false,
                resizable: false,
                width: 350,
                height: 180,
                modal: true,
                buttons: {
                    "Confirmar": function () {
                        $(this).dialog("close");
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }); // FIN DOC READY

        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
            }
        }

        function dialogConfirm(uniqueID, itemID, title, body) {
            var dialogTitle = title + itemID;

            $("#deleteConfirmationDialog").html('<p><span class="ui-icon ui-icon-alert" style="float:left; margin:0 7px 20px 0;"></span>' + body + '</p>');

            $("#deleteConfirmationDialog").dialog({
                title: dialogTitle,
                buttons: {
                    "Confirmar": function () { __doPostBack(uniqueID, ''); $(this).dialog("close"); },
                    "Cancelar": function () { $(this).dialog("close"); }
                }
            });

            $('#deleteConfirmationDialog').dialog('open');
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Liquidaciones separAQUARELLAs
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Lista de pedidos separados en bodega sin aun ser pagos por el cliente; podrá adelantar
    o atrazar la fecha de caducidad.
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
                    Lista de pedidos
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="f12">
                                Realice un filtro de los resultados:<br />
                                (Desc: Puede buscar por 
                                Promotor, nro. pedido, lider)
                            </td>
                            <td>
                                <table cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upFilter" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtFilter" runat="server" Width="190px"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:Button ID="btFilter" runat="server" Text="Filtrar" OnClick="btFilter_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btRefresh" runat="server" Text="Refrescar panel" OnClick="btRefresh_Click" />
                                        </td>
                                        <td align="right" style="border-left: solid 1px silver;">
                                            <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                                                onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                                                runat="server" Height="25px" Width="24px" ToolTip="Exportar Panel de Resultados a Excel."
                                                OnClick="ibExportToExcel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;" class="f-small">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvSepLiq" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    OnDataBound="gvSepLiq_DataBound" OnPageIndexChanging="gvSepLiq_PageIndexChanging"
                    OnSorting="gvSepLiq_Sorting" OnRowCommand="gvSepLiq_RowCommand">
                    <EmptyDataTemplate>
                        No existen liquidaciones en estado separado.
                    </EmptyDataTemplate>
                    <Columns>                       
                        <asp:BoundField DataField="Asesor" ItemStyle-BackColor="#ccffcc" HeaderText="Asesor Comercial" SortExpression="Asesor" />
                        <asp:BoundField DataField="Are_Descripcion" HeaderText="Lider" SortExpression="Are_Descripcion" />
                        <asp:BoundField DataField="Liq_Id" HeaderText="Pedido" ItemStyle-ForeColor="#47B224"
                            SortExpression="Liq_Id" >
                        <ItemStyle ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="Promotor" SortExpression="nombres" />
                        <asp:BoundField DataField="Liq_Fecha_Expiracion" HeaderText="Caduce" SortExpression="Liq_Fecha_Expiracion" />
                        <asp:BoundField HeaderText="Cnt.Pedida" DataField="totalpares" ItemStyle-ForeColor="DimGray"
                            SortExpression="totalpares" >
                        <ItemStyle ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Cnt.Liquid" DataField="totalpares" ItemStyle-ForeColor="DarkOrange"
                            SortExpression="totalpares" >
                        <ItemStyle ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="SubTot/SinIGV" DataFormatString="{0:C0}" DataField="subtotal"
                            SortExpression="subtotal" />
                        <asp:BoundField HeaderText="Telefono" DataField="bas_telefono" 
                            SortExpression="bas_telefono" />
                        <asp:BoundField DataField="Bas_Celular" HeaderText="Celular" 
                            SortExpression="Bas_Celular" />
                        <asp:TemplateField HeaderText="Mail" SortExpression="Bas_Correo" Visible="false">
                            <ItemTemplate>
                                <a href="mailto:<%#Eval("Bas_Correo")%>" title="Enviar un E-Mail a <%#Eval("nombres")%>"
                                    style="font-family: Georgia; color: gray; font-size: 10px;">
                                    <%#Eval("Bas_Correo")%></a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ubicacion" HeaderText="Ubicación" SortExpression="ubicacion" />
                        <asp:TemplateField HeaderText="D-" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibMinusDay" CommandName="MinusDay" CommandArgument='<%# Eval("Liq_Id")%>'
                                    AlternateText='<%# Eval("Liq_Id")%>' OnClientClick="javascript:return dialogConfirm(this.name, this.alt,'¿Disminuir tiempo de vencimiento? - Liq.','¿Realmente desea disminuir en un día el vencimiento de la liquidación?; por favor confirmar.');"
                                    runat="server" ImageUrl="~/Design/images/Botones/b_date_minus.png" ToolTip="Restar Un Dia A la Fecha De Expiración de la Liquidación" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="D+" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibAddDay" CommandName="AddDay" CommandArgument='<%# Eval("Liq_Id")%>'
                                    AlternateText='<%# Eval("Liq_Id")%>' OnClientClick="javascript:return dialogConfirm(this.name, this.alt,'¿Extender tiempo de vencimiento? - Liq.','¿Realmente desea aumentar en un día el vencimiento de la liquidación?; por favor confirmar.');"
                                    runat="server" ImageUrl="~/Design/images/Botones/b_date_add.png" ToolTip="Sumar Un Dia A la Fecha De Expiración de la Liquidación" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Liq" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a class='iframe' href='panelLiqReports.aspx?noliq=<%# Eval("Liq_Id")%> '
                                    title="Historial De Liquidaciones (LIQ. NO.<%# Eval("Liq_Id")%> ) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                    <img src="../../Design/images/b_print.png" border="0" alt="Ver/Imprimir Liquidación No.<%# Eval("Liq_Id")%>" /></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="false" />
                                                    <EnableAction AnimationTarget="btRefresh" Enabled="false" />
                                                    <EnableAction AnimationTarget="txtFilter" Enabled="false" />
                                                    <FadeOut minimumOpacity=".7" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="true" />
                                                    <EnableAction AnimationTarget="btRefresh" Enabled="true" />
                                                    <EnableAction AnimationTarget="txtFilter" Enabled="true" />                                                    
                                                    <FadeIn minimumOpacity=".7" /> 
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
    <%-- EXPLICACION DE CAMPOS --%>
    <table width="100%">
        <tr>
            <td>
                <div style="margin: 10px auto 0 auto;">
                    <p class="fsal f13">
                        Explicación de iconos</p>
                    <table>
                        <tr>
                            <td>
                                <img alt="Impresión de documento de liquidación" src="../../Design/images/b_print.png"
                                    onmouseover="this.style.background='silver';" onmouseout="this.style.background=''"
                                    border="0" />
                            </td>
                            <td>
                                Impresión de documento de liquidación.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img alt="Entrega de mercancía física al cliente" src="../../Design/images/Botones/b_date_add.png"
                                    onmouseover="this.style.background='silver';" onmouseout="this.style.background=''"
                                    border="0" />
                            </td>
                            <td>
                                Adicionar un día al tiempo de caducidad de un pedido.
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img alt="Entrega de mercancía física al cliente" src="../../Design/images/Botones/b_date_minus.png"
                                    onmouseover="this.style.background='silver';" onmouseout="this.style.background=''"
                                    border="0" />
                            </td>
                            <td>
                                Restar un día al tiempo de caducidad del pedido.
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td valign="top" align="right">
                <!-- TOTALIZADOS -->
                <div style="margin: 10px auto 0 auto;">
                    <p class="fsal f13">
                        Información de totalizado</p>
                    <table class="f12">
                        <tr>
                            <td>
                                Cantidades pedidas:
                            </td>
                            <td align="right" style="width: 100px;">
                                <asp:UpdatePanel ID="upQtysOrder" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblQtysOrder" runat="server" Text="0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Cantidades liquidAQUARELLAs:
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="upQtysLiq" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblQtysLiq" runat="server" Text="0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Número de pedidos:
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="upNumLiq" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblNumLiq" runat="server" Text="0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Valor total de pedidos:
                            </td>
                            <td align="right">
                                <asp:UpdatePanel ID="upLiqValue" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblLiqValue" runat="server" Text="0"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="deleteConfirmationDialog">
    </div>
</asp:Content>

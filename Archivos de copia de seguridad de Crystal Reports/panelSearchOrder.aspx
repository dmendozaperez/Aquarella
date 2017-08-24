<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" StylesheetTheme="SiteTheme"
    AutoEventWireup="true" CodeBehind="panelSearchOrder.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.panelSearchOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
        }); // FIN DOC READY

        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Búsqueda de pedidos y liquidaciones
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Busque una liquidación en el sistema y verifique su estado.
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
                    Consulta de pedidos
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="4" cellspacing="4">
                        <tr>
                            <td class="f12">
                                Campo de consulta:<br />
                                (Desc: Puede buscar por No.Liq y/ó No.Guia)
                            </td>
                            <td>
                                <table cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upFilter" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtConsult" runat="server" Width="190px"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:Button ID="btConsult" runat="server" Text="Consultar" OnClick="btConsult_Click" />
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
                <asp:GridView ID="gvLiq" runat="server" SkinID="gridviewSkin" ShowFooter="true" AllowPaging="false"
                    AllowSorting="false" OnRowCreated="gvLiq_RowCreated">
                    <EmptyDataTemplate>
                        No existen liquidaciones o pedidos que mostrar.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="lider" HeaderText="Area" SortExpression="lider" />
                        <asp:BoundField DataField="nombres" HeaderText="Coordinador" SortExpression="nombres" />
                        <asp:BoundField DataField="Liq_Id" HeaderText="Liq." SortExpression="Liq_Id" />
                        <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
                        <asp:BoundField DataField="Gru_Fecha" HeaderText="Liberación" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado" />
                        <asp:BoundField HeaderText="Unds" DataField="cantidad" SortExpression="cantidad" />
                        <asp:BoundField HeaderText="Valor" DataFormatString="{0:C0}" DataField="valor" SortExpression="valor" />
                        <asp:BoundField DataField="ubicacion" HeaderText="Ubicación" SortExpression="ubicacion" />
                        <asp:BoundField DataField="Ven_Id" HeaderText="Factura" SortExpression="Ven_Id" />
                        <asp:BoundField DataField="Tra_Descripcion" HeaderText="Transportadora" SortExpression="Tra_Descripcion" />
                        <asp:BoundField DataField="Tra_Gui_No" HeaderText="No.Guia" SortExpression="Tra_Gui_No" />
                        <asp:TemplateField HeaderText="Liq" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a class='iframe' href='panelLiqReports.aspx?noliq=<%# Eval("Liq_Id")%> '
                                    title="Historial De Liquidaciones (LIQ. NO.<%# Eval("Liq_Id")%> ) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                    <img src="../../Design/images/b_print.png" border="0" alt="Ver/Imprimir Liquidación No.<%# Eval("Liq_Id")%>" /></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fac" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a class='iframe' href='../Ventas/panelInvReports.aspx?noliq=<%# Eval("Liq_Id")%>&NoInvo=<%# Eval("Ven_Id")%>'
                                    title="Ver / Imprimir reporte de factura  <%# Eval("Ven_Id")%>.<b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                    <asp:Image ID="imgInv" Visible="false" ImageUrl="../../Design/images/b_print.png"
                                        runat="server" AlternateText="Fact" ToolTip="Ver/Imprimir Factura" BorderWidth="0" /></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
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
                                                    <FadeOut minimumOpacity=".7" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="true" />                                                   
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
                                <img alt="Impresión de documento de liquidación o Factura" src="../../Design/images/b_print.png"
                                    onmouseover="this.style.background='silver';" onmouseout="this.style.background=''"
                                    border="0" />
                            </td>
                            <td>
                                Impresión de documento de liquidación o factura.
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

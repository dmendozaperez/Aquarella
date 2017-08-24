<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="panelReturns.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.panelReturns" %>

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
    Consulta de devolución de mercancia
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulte entre un rango de fechas, todas las devoluciones realizAQUARELLAs en bodega;
    podrá observar por cliente, cantidades y valores.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" 
        EnableScriptGlobalization="True">
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
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%" align="left">
                                <table cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td class="f12" colspan="3">
                                            Fecha de inicio:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upStartDate" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDateStart" runat="server" AccessKey="f"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="calendar" runat="server" TargetControlID="txtDateStart"
                                                        Format="dd/MM/yyyy" Animated="true" PopupButtonID="imgCalendar" FirstDayOfWeek="Monday" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td align="left" style="padding-left: 5px;">
                                            <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                Style="cursor: hand;" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateStart" runat="server"
                                                ToolTip="Fecha de inicio" CssClass="error_asterisck" ErrorMessage="Dígite fecha inicial"
                                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateStart">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvDateStart" runat="server" ValidationGroup="vsConsultForm"
                                                Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateStart"
                                                Operator="DataTypeCheck" ErrorMessage="Dígite una fecha válida">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <!---->
                            <td width="50%">
                                <table cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td class="f12" colspan="3">
                                            Fecha de cierre:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upEndDate" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtDateEnd" runat="server" AccessKey="f"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateEnd"
                                                        Format="dd/MM/yyyy" Animated="true" PopupButtonID="imgCalendarDe" FirstDayOfWeek="Monday" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td align="left" style="padding-left: 5px;">
                                            <asp:Image ID="imgCalendarDe" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                Style="cursor: hand;" />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateEnd" runat="server"
                                                ToolTip="Fecha final" CssClass="error_asterisck" ErrorMessage="Dígite fecha final*"
                                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateEnd">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvDateEnd" runat="server" ValidationGroup="vsConsultForm"
                                                Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateEnd"
                                                Operator="DataTypeCheck" ErrorMessage="Dígite una fecha final válida">*</asp:CompareValidator>
                                            <asp:CompareValidator ID="cvDateStartDateEnd" runat="server" ValidationGroup="vsConsultForm"
                                                Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateEnd"
                                                ControlToCompare="txtDateStart" Operator="GreaterThanEqual" ErrorMessage="Dígite una fecha final superior a la fecha inicial">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="middle">
                    <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" />
                </td>
                <td align="right" style="border-left: solid 1px silver;">
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." 
                        onclick="ibExportToExcel_Click" />
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
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    Font-Size="Small" OnDataBound="gvReturns_DataBound" 
                    onrowcommand="gvReturns_RowCommand" 
                    onrowdatabound="gvReturns_RowDataBound">
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Not_Numero" HeaderText="No.N/C" SortExpression="Not_Numero"
                            ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Not_Fecha" HeaderText="Fecha" SortExpression="Not_Fecha"
                            ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Lider" HeaderText="Area" SortExpression="Lider" />
                        <asp:BoundField DataField="Promotor" HeaderText="Cliente" SortExpression="Promotor">
                        </asp:BoundField>
                        <asp:BoundField DataField="Bas_Direccion" HeaderText="Ciudad" SortExpression="Bas_Direccion" />
                        <asp:BoundField DataField="cantidad" HeaderText="Unidades" SortExpression="cantidad"
                            ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Total" HeaderText="Valor" DataFormatString="{0:C2}"
                            SortExpression="Total" ItemStyle-HorizontalAlign="Right" >
                         <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                   <a class='iframe' href='../../Reports/Ventas/reportArticlesReturned.aspx?noReturn=<%# Eval("Not_Id")%>&ShowReportOnWeb=true'
                                    title="Ver / Imprimir reporte de devolucion  <%# Eval("Not_Id")%>.<b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                    <img src="../../Design/images/botones/search.png" border="0" alt="Ver reporte de devolución No.<%# Eval("Not_Id")%>" /></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                   <a href='../../Reports/Ventas/reporArticlesReturnedCancel.aspx?noReturn=<%# Eval("Not_Id")%>&noNota=<%# Eval("Not_Numero")%>' 
                                   id="lnkAnular"
                                    onclick="return confirm('¿Esta seguro de anular la actual Nota de Credito y generar un nuevo numero?');" 
                                      title="Enviar la impresion, anulando la Nota de Credito anterior con numero de devolucion: <%# Eval("Not_Numero")%>">
                                      <img src="../../Design/images/b_print.png" border="0" alt="Ver/Imprimir reporte de devolución No.<%# Eval("Not_Id")%>" />
                                   </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibanular" CommandName="starnular"  CommandArgument='<%# Eval("Not_Id")%>'
                                                runat="server" ImageUrl="~/Design/images/Botones/delete_off.png" ToolTip="Anular Nota de Credito." />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle HorizontalAlign="Right" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsReturns" runat="server" SelectMethod="getReturnsByDate"
            TypeName="www.aquarella.com.pe.bll.Ventas.Returns_Hdr" OnSelected="odsReturns_Selected">
            <SelectParameters>               
                <asp:ControlParameter ControlID="txtDateStart" Name="_startDate" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtDateEnd" Name="_endDate" PropertyName="Text"
                    Type="String" />               
            </SelectParameters>
        </asp:ObjectDataSource>
        <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="false" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

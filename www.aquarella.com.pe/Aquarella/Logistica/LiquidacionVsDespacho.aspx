<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" 
    StylesheetTheme="SiteTheme"         CodeBehind="LiquidacionVsDespacho.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.LiquidacionVsDespacho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .style1
        {
            width: 374px;
        }
        .style2
        {
            width: 92px;
        }
        .style3
        {
            width: 202px;
        }
        .style4
        {
            width: 317px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
  Consulta  de Liquidacion vs Despacho
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulte entre un rango de fechas, todos los despachos detallado por articulo
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
                <td class="style1">
                    <table>
                        <tr>
                            <td class="f12">
                                Fecha de Inicio</td>
                             <td>
                            </td>
                             <td>
                            </td>
                             <td class="f12">
                                 Fecha de Cierre</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upStartDate" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDateStart" runat="server"  AccessKey="f"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendar" runat="server" Animated="true" 
                                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" 
                                            TargetControlID="txtDateStart" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                             <td>
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
                             <td>
                                 <asp:UpdatePanel ID="upEndDate" runat="server">
                                     <ContentTemplate>
                                         <asp:TextBox ID="txtDateEnd" runat="server"  AccessKey="f"></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                             Animated="true" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                                             PopupButtonID="imgCalendarDe" TargetControlID="txtDateEnd" />
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                            </td>
                             <td>
                                 <asp:Image ID="imgCalendarDe" runat="server" 
                                     ImageUrl="~/Design/images/Botones/b_calendar_ico.gif" 
                                     onmouseout="this.style.background=''" 
                                     onmouseover="this.style.background='red';" Style="cursor: hand;" />
                            </td>
                             <td>
                                 <asp:RequiredFieldValidator ID="rfvDateEnd" runat="server" 
                                     ControlToValidate="txtDateEnd" CssClass="error_asterisck" Display="Dynamic" 
                                     ErrorMessage="Dígite fecha final*" SetFocusOnError="true" ToolTip="Fecha final" 
                                     ValidationGroup="vsConsultForm">*</asp:RequiredFieldValidator>
                                 <asp:CompareValidator ID="cvDateEnd" runat="server" 
                                     ControlToValidate="txtDateEnd" CssClass="error_asterisck" 
                                     ErrorMessage="Dígite una fecha final válida" Operator="DataTypeCheck" 
                                     SetFocusOnError="true" Type="Date" ValidationGroup="vsConsultForm">*</asp:CompareValidator>
                                 <asp:CompareValidator ID="cvDateStartDateEnd" runat="server" 
                                     ControlToCompare="txtDateStart" ControlToValidate="txtDateEnd" 
                                     CssClass="error_asterisck" 
                                     ErrorMessage="Dígite una fecha final superior a la fecha inicial" 
                                     Operator="GreaterThanEqual" SetFocusOnError="true" Type="Date" 
                                     ValidationGroup="vsConsultForm">*</asp:CompareValidator>
                            </td>
                             <td>
                                 &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="style3"  >
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                               <asp:CheckBox ID="chksd" runat="server" AutoPostBack="True" 
                                   oncheckedchanged="chksd_CheckedChanged" Text="Filtrar pares sin despachar" />
                           </ContentTemplate>
                       </asp:UpdatePanel>
                </td>
                            <td class="style4">
                                Realice un filtro de los resultados:<br />
                                (Desc: Puede buscar por 
                                nro. pedido , nro venta)
                            </td>
                <td valign="middle" class="style2"  >
                                                    <asp:TextBox ID="txtFilter" runat="server" 
                           Width="87px"></asp:TextBox>
                </td>
                <td valign="middle" class="style2"  >
                                            <asp:Button ID="btFilter" runat="server" Text="Filtrar" OnClick="btFilter_Click" />
                </td>
                <td valign="middle" class="style2"  >
                       <asp:Button ID="btConsult" runat="server" Text="Refrescar Panel" ValidatioGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" Width="137px" /> 
                </td>
                <td align="left" style="border-left: solid 1px silver;">
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." 
                        onclick="ibExportToExcel_Click" /> 
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>       
    </div>
    <div style="margin: 10px auto 0 auto;" class="f-small">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    OnDataBound="gvReturns_DataBound" OnPageIndexChanging="gvReturns_PageIndexChanging"
                    OnSorting="gvReturns_Sorting" OnRowCommand="gvReturns_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" 
                    Font-Size="Small" PageSize="17" Width="1093px">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="LIQ" HeaderText="Nro.Liq">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Ven_Id" HeaderText="Nro.Venta">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha" HeaderText="Fecha Vent.">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ARTICULO" HeaderText="Articulo">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TALLA" HeaderText="Talla">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PEDORIGINAL" HeaderText="Pares Real" 
                            SortExpression="PEDORIGINAL">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PEDI_DESPACHADO" HeaderText="Pares Desp." 
                            SortExpression="PEDI_DESPACHADO">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SALDO" HeaderText="Pares S/D" SortExpression="SALDO">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        No existen datos para mostrar.
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />                
                <asp:AsyncPostBackTrigger ControlID="chksd" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="Click" />
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
                                                    <EnableAction AnimationTarget="btFilter" Enabled="false" />                                                  
                                                    <EnableAction AnimationTarget="chksd" Enabled="false" />                                                  
                                                    <FadeOut minimumOpacity=".7" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="true" />    
                                                    <EnableAction AnimationTarget="btFilter" Enabled="true" />  
                                                    <EnableAction AnimationTarget="chksd" Enabled="true" />                                                                                                                                                                                                   
                                                    <FadeIn minimumOpacity=".7" /> 
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

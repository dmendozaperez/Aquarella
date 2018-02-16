<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="premioscontinuo.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.premioscontinuo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
     <style type="text/css">
        .auto-style7 {
            width: 464px;
        }
        .auto-style8 {
            width: 125px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Reporte de Premios de Continuidad
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulte entre un rango de fechas de ventas
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
       <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="auto-style7">
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
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upStartDate" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDateStart" runat="server" AccessKey="f"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendar" runat="server" Animated="true" 
                                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" 
                                            TargetControlID="txtDateStart" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                             <td>
                                            <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                Style="cursor: pointer;" />
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
                                         <asp:TextBox ID="txtDateEnd" runat="server" AccessKey="f"></asp:TextBox>
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
                                     onmouseover="this.style.background='red';" Style="cursor: pointer;" />
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
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="auto-style8">
                    <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" />
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
                <td class="auto-style7">
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>
    </div>
     <!-- PANEL DE RESULTADOS -->
    
    <div style="margin: 1px 1px 1px 1px;  width: 1078px; overflow: auto;">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server"  BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    onrowcreated="gvReturns_RowCreated" AllowPaging="True" Font-Bold="False" 
                    Font-Size="Small" Height="100px" Width="1075px" 
                    onpageindexchanging="gvReturns_PageIndexChanging" PageSize="18" OnDataBinding="gvReturns_DataBinding" ForeColor="Black" GridLines="Vertical">
                    <RowStyle BackColor="#F7F7DE" />
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="gvReturns" EventName="PageIndexChanging" />
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

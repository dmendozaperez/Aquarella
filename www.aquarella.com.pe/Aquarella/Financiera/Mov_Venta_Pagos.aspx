﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Mov_Venta_Pagos.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.Mov_Venta_Pagos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 363px;
        }
        .auto-style2 {
            width: 94px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Movimiento de Ventas-Pagos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulte entre un rango de fechas. las ventas-pagos
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" 
        EnableScriptGlobalization="True" AsyncPostBackTimeout="0">
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
    <div style="margin: 10px auto 0 auto;" >
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="auto-style1">
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
                                                Style="cursor:pointer;" />
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
                <td valign="middle" class="auto-style2">
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
                <td class="auto-style1">
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
                <asp:GridView ID="gvReturns" runat="server" AllowSorting="True" 
                    AllowPaging="True" PageSize="12"
                 SkinID="gridviewSkin" PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                    CellPadding="4" Width="1072px" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                    onpageindexchanging="gvReturns_PageIndexChanging" 
                    onrowdatabound="gvReturns_RowDataBound">
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle CssClass="GridViewBlue-tf" BackColor="#99CCCC" ForeColor="#003399" 
                        HorizontalAlign="Left" />
                    <Columns>
                        <asp:BoundField DataField="fecha_op" HeaderText="Fecha">
                        </asp:BoundField>
                        <asp:BoundField DataField="des_operacion" HeaderText="Descripcion Operacion" DataFormatString="{0:N0}">
                        </asp:BoundField>
                        <asp:BoundField DataField="op_monto" HeaderText="Monto">
                        </asp:BoundField>
                        <asp:BoundField DataField="op_numero" HeaderText="Operacion Numero">
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_op2" HeaderText="Fecha">
                        </asp:BoundField>
                        <asp:BoundField DataField="dni_ruc" HeaderText="Dni/Ruc">
                        </asp:BoundField>
                        <asp:BoundField DataField="cliente" HeaderText="Nombre/Razon Social">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_doc" HeaderText="Fecha" />
                        <asp:BoundField DataField="num_doc" HeaderText="Numero" />
                        <asp:BoundField DataField="importe_doc" HeaderText="Importe" />
                        <asp:BoundField DataField="fecha_ncredito" HeaderText="Fecha" />
                        <asp:BoundField DataField="num_ncredito" HeaderText="Numero" />
                        <asp:BoundField DataField="importe_ncredito" HeaderText="Importe" />
                        <asp:BoundField DataField="fecha_ncredito2" HeaderText="Fecha" />
                        <asp:BoundField DataField="num_ncredito2" HeaderText="Numero" />
                        <asp:BoundField DataField="importe_ncredito2" HeaderText="Importe" />

                        <asp:BoundField DataField="fecha_ncredito3" HeaderText="Fecha" />
                        <asp:BoundField DataField="num_ncredito3" HeaderText="Numero" />
                        <asp:BoundField DataField="importe_ncredito3" HeaderText="Importe" />

                        <asp:BoundField DataField="fecha_ncredito4" HeaderText="Fecha" />
                        <asp:BoundField DataField="num_ncredito4" HeaderText="Numero" />
                        <asp:BoundField DataField="importe_ncredito4" HeaderText="Importe" />

                        <asp:BoundField DataField="fecha_ncredito5" HeaderText="Fecha" />
                        <asp:BoundField DataField="num_ncredito5" HeaderText="Numero" />
                        <asp:BoundField DataField="importe_ncredito5" HeaderText="Importe" />

                        <asp:BoundField DataField="fecha_ncredito6" HeaderText="Fecha" />
                        <asp:BoundField DataField="num_ncredito6" HeaderText="Numero" />
                        <asp:BoundField DataField="importe_ncredito6" HeaderText="Importe" />

                        <asp:BoundField DataField="fecha_ncredito7" HeaderText="Fecha" />
                        <asp:BoundField DataField="num_ncredito7" HeaderText="Numero" />
                        <asp:BoundField DataField="importe_ncredito7" HeaderText="Importe" />


                        <asp:BoundField DataField="base_imponible" HeaderText="Base Imponible" />
                        <asp:BoundField DataField="percepcion" HeaderText="Percepcion 2%" />
                        <asp:BoundField DataField="total" HeaderText="Total Ticket" />
                        <asp:BoundField DataField="fecha_saldo_ant" HeaderText="Fecha" />
                        <asp:BoundField DataField="importe_saldo_ant" HeaderText="Importe" />
                        <asp:BoundField DataField="pagar" HeaderText="A Pagar" />
                        <asp:BoundField DataField="deposito" HeaderText="Deposito" />
                        <asp:BoundField DataField="saldo_favor" HeaderText="Saldo Favor" />
                        <asp:BoundField DataField="ajuste" HeaderText="Ajuste" />
                    </Columns>
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
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

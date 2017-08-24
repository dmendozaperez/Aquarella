<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" EnableEventValidation = "false"  ValidateRequest = "false"
    StylesheetTheme="SiteTheme" CodeBehind="Consulta_OP_Automatic.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.Consulta_OP_Automatic" %>
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
         .auto-style1 {
             width: 206px;
         }
         .auto-style2 {
             width: 65px;
         }
         .auto-style3 {
             width: 8px;
         }
         .auto-style4 {
             width: 327px;
         }
         .auto-style5 {
             width: 82px;
         }
    </style>
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
     Consultar Pagos Automaticos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
     Información acerca de los pagos automatico
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
        <table width="100%" id="pnlGridView"  class="tablagris" cellpadding="4">
            <tr>
                <td class="auto-style4">
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
                                        <asp:TextBox ID="txtDateStart" runat="server"  AccessKey="f" Width="80px"></asp:TextBox>
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
                                         <asp:TextBox ID="txtDateEnd" runat="server"  AccessKey="f"  Width="80px"></asp:TextBox>
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
                             <td>
                                 &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="auto-style5"  >
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                               Filtrar op sin cancelacion<br />
                               <asp:CheckBox ID="chksd" runat="server" AutoPostBack="True" style="display:none;"
                                   oncheckedchanged="chksd_CheckedChanged" Text="Filtrar op sin cancelacion" />
                               <asp:DropDownList ID="dwestado" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dwestado_SelectedIndexChanged">
                                   <asp:ListItem Selected="True" Value="Todos">(TODOS)</asp:ListItem>
                                   <asp:ListItem>SI</asp:ListItem>
                                   <asp:ListItem>NO</asp:ListItem>
                               </asp:DropDownList>
                           </ContentTemplate>
                       </asp:UpdatePanel>
                </td>
                            <td class="auto-style1">
                                Realice un filtro de los resultados:<br />
                                (Desc: Puede buscar por. cliente,dni/ruc,monto,operacion)
                            </td>
                <td valign="middle" class="style2"  >
                                                    <asp:TextBox ID="txtFilter" runat="server" 
                           Width="80px" Height="16px"></asp:TextBox>
                </td>
                <td valign="middle" class="style2"  >
                                            <asp:Button ID="btFilter" runat="server" Text="Filtrar" OnClick="btFilter_Click" />
                </td>
                <td valign="middle" class="auto-style2"  >
                       <asp:Button ID="btConsult" runat="server" Text="Refrescar Panel" ValidatioGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" Width="137px" /> 
                </td>
                <td align="left" style="border-left: solid 1px silver;" class="auto-style3">
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." 
                        onclick="ibExportToExcel_Click" /> 
                </td>
                <td align="left" style="border-left: solid 1px silver;">
                    <asp:ImageButton ID="ibprinter" ImageUrl="~/Design/images/Botones/printer.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Imprimir Datos de la lista." 
                        onclick="ibprinter_Click"  /> 
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>       
    </div>
    <div style="margin: 10px auto 0 auto;" class="f-small" id="grid_v">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server" SkinID="gridviewSkin" ShowFooter="True" 
                    OnDataBound="gvReturns_DataBound" OnPageIndexChanging="gvReturns_PageIndexChanging"
                    OnSorting="gvReturns_Sorting" OnRowCommand="gvReturns_RowCommand" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" 
                    Font-Size="Small" Width="1093px" OnRowDataBound="gvReturns_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
<Columns>
                        <asp:BoundField DataField="Fecha_OP" HeaderText="Fecha" 
                            SortExpression="Fecha_OP" >                        
                        <HeaderStyle HorizontalAlign="Left" Width="30px" />
                        <ItemStyle HorizontalAlign="Left" ForeColor="Black" Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente" 
                            SortExpression="Cliente" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" ForeColor="Black" Width="250px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dni_ruc" HeaderText="Dni-Ruc" 
                            SortExpression="dni_ruc" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" ForeColor="Black" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Voucher_Monto" HeaderText="Monto" SortExpression="Voucher_Monto"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:N2}" >
                        <HeaderStyle HorizontalAlign="Right" Width="50px" />
                        <ItemStyle HorizontalAlign="Right" ForeColor="Black" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Voucher_Op" HeaderText="Operacion" 
                            SortExpression="Voucher_Op" >
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" ForeColor="Black" Width="50px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Cancelacion">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("cancelacion") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblcancelacion" runat="server" Text='<%# Bind("cancelacion") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="liq" HeaderText="Nro.Liq">
                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="Liqui." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a class='iframe' href='../Logistica/panelLiqReports.aspx?NoLiq=<%#Eval("liq")%>'
                                                title="Ver/Imprimir Liquidacion  <%# Eval("liq")%>.<b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                                <asp:Image ID="imgInv" Visible="false" ImageUrl="../../Design/images/b_print.png"
                                                    runat="server" AlternateText="Fact" ToolTip="Ver/Imprimir Liquidacion" BorderWidth="0" /></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                    </asp:TemplateField>
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
                <asp:AsyncPostBackTrigger ControlID="dwestado" EventName="SelectedIndexChanged" />                
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
    </div></asp:Content>

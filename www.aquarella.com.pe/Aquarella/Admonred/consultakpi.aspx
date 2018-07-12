<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="consultakpi.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.consultakpi" %>
<% @ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script type="text/javascript">
        var btncredito;
         $(document).ready(function () {
             marcar()
         });

         function marcar() {
            var general = document.getElementById("<%=rbt_G.ClientID %>");
             general.checked = true;
         }
        
    </script>
    
    <style type="text/css">
        .style1
        {
            width: 802px;
        }
        .style2
        {
            width: 183px;
        }
        .style3
        {
            width: 77px;
        }
        .style4
        {
            width: 141px;
        }
        .style5
        {
            width: 170px;
        }
        .style6
        {
            width: 170px;
            height: 23px;
        }
        .style7
        {
            height: 23px;
        }
        .auto-style1 {
            width: 314px;
            height: 63px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Consulta de Resultado KPI
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulte entre un rango de fechas, Asesor o por lideres
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
    <asp:Panel ID="pnlDwCustomers" Visible="false" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
                <tr>
                    <td>
                        <table cellpadding="1" cellspacing="1" width="100%">
                             <tr>
                                 <td class="f12" width="5%">
                                    Asesor:</td>
                                <td class="auto-style1">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                    <asp:DropDownList ID="dwasesor" runat="server" AppendDataBoundItems="true"                                       
                                        ToolTip="Selecionar un asesor" Width="280px" style="cursor:pointer">
                                        <asp:ListItem Text=" -- Seleccionar a todos --" Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                        </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="rbt_A" />
                                          <asp:AsyncPostBackTrigger ControlID="rbt_G" />
                                        <asp:AsyncPostBackTrigger ControlID="rbt_L" />
                                        
                                    </Triggers>
                                </asp:UpdatePanel>
                                </td>                              
                            </tr>

                            <tr>                                
                                <td class="f12" width="5%">
                                    Lider:</td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                    <asp:DropDownList ID="dwCustomers" runat="server" AppendDataBoundItems="true" 
                                        DataTextField="are_descripcion" 
                                        DataValueField="are_id"
                                        ToolTip="Selecionar un lider" Width="280px" style="cursor:pointer">
                                        <asp:ListItem Text=" -- Seleccionar a todos --" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="rbt_A" />
                                            <asp:AsyncPostBackTrigger ControlID="rbt_G" />
                                            <asp:AsyncPostBackTrigger ControlID="rbt_L" />
                                                
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
                            <td>
                                 <fieldset style="width:500px">
                                   <legend>Tipo de Vista:</legend>
                                         
                                 <table>
                                     <tr>
                                        <td style="width:200px"><asp:RadioButton ID="rbt_A" GroupName="TipoReporte"  Text="Detallado por Asesor" AutoPostBack="true"   OnCheckedChanged="onchange_Asesor" runat="server"/></td>
                                         <td style="width:200px"><asp:RadioButton ID="rbt_L" GroupName="TipoReporte"  Text="Detallado por Lider" AutoPostBack="true" runat="server" OnCheckedChanged="onchange_lider"/></td>
                                          <td style="width:200px"><asp:RadioButton ID="rbt_G" GroupName="TipoReporte"  Text="General" AutoPostBack="true" OnCheckedChanged="onchange_gral" runat="server"/></td> 
                                      </tr>
                                </table>
                                        
</fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="style2">
                    <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" />
                </td>
                <td align="left" style="border-left: solid 1px silver;">
                    <table style="width:100%;">
                        <tr>
                            <td class="style3">
                                &nbsp;</td>
                            <td class="style4">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3">
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." 
                        onclick="ibExportToExcel_Click" />
                            </td>
                            <td class="style4">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3">
                                &nbsp;</td>
                            <td class="style4">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
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
    <!-- PANEL DE RESULTADOS -->
    
    <div style="margin: 1px 1px 1px 1px;  width: 1078px; overflow: auto;">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server" AllowSorting="True" 
                    ShowFooter="True" AllowPaging="True" PageSize="14"
                 SkinID="gridviewSkin" PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="1072px">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Width="200px" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="GridViewBlue-tf" BackColor="#2461BF" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <RowStyle BackColor="#EFF3FB"  />
                   
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" Width="200px" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
              
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
               
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsReturns" runat="server" SelectMethod="getconsultaKPI"
            TypeName="www.aquarella.com.pe.bll.Ventas.Facturacion" OnSelected="odsReturns_Selected">
            <SelectParameters>
                <asp:Parameter Name="_area_id" Type="String" />
                <asp:Parameter Name="_asesor" Type="String" ConvertEmptyStringToNull="false"/>
                <asp:Parameter Name="_ase_lid" Type="String" />
                 <asp:Parameter Name="_gral" Type="String" />
                <asp:ControlParameter ControlID="txtDateStart" Name="_date_start" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="txtDateEnd" Name="_date_end" PropertyName="Text" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btConsult" Enabled="true" />  
                                                                                      
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

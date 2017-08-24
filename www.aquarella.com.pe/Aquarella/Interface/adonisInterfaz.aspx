<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="adonisInterfaz.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Interface.adonisInterfaz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .style1
        {
            width: 19%;
        }
        .style2
        {
            width: 45%;
        }
        .style3
        {
            width: 411px;
        }
        .style4
        {
            width: 29px;
        }
        .style5
        {
            width: 129px;
        }
        .style6
        {
            width: 18%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Interface Comercial de Aquarella
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulta de los movimientos comerciales del sistema de Aquarella por concepto; aparte
    puede generar la interfaz comercial en archivo plano para el sistema contable de
    Adonis.
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
      <!-- SEL CLIENTE -->
    <asp:Panel ID="pnlDwCustomers" Visible="true" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
                <tr>
                    <td class="fsal f13" colspan="2">
                        Selección de cliente
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="1" cellspacing="1">
                            <tr>
                                <!--<td class="style6">
                                    Seleccione un cliente de la lista:</td>-->
                                <td>
                                    <asp:DropDownList ID="dwCustomers" DataTextField="nombres"
                                        DataValueField="bas_id" AppendDataBoundItems="true" runat="server"
                                        ToolTip="Selecionar un cliente" Width="220px" 
                                        OnSelectedIndexChanged="dwCustomers_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Text=" -- Seleccionar a todos --"></asp:ListItem>
                                    </asp:DropDownList>
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
                <td class="fsal f13" colspan="2">
                    Formulario de consulta
                </td>
            </tr>
            <tr>
                <!--<td>
                    WARE AND AREA FORM 
                    <AQControl:WareAreaForm runat="server" Visible="false" ID="WareAreaForm" />
                </td>-->
                <td class="style3">
                    <table cellpadding="0" cellspacing="0" style="width: 104%">
                        <tr>
                            <td align="left" class="style1">
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
                                        <td class="style5">
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
                            <td class="style2">
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
                                        <td class="style4">
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
                <td>
                    <table>
                        <tr>
                            <th>
                                &nbsp;</th>
                            <th>
                                Adonis
                            </th>
                            <th>
                                Excel
                            </th>
                        </tr>
                        <tr>
                            <td valign="middle">
                                <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                                    CausesValidation="true" OnClick="btConsult_Click" />
                            </td>
                            <td align="center" style="border-left: solid 1px silver;">
                                <asp:ImageButton ID="ibExportDoc" ImageUrl="~/Design/images/Botones/b_doc.png" onmouseover="this.style.background='green';"
                                    onmouseout="this.style.background=''" runat="server" Height="25px" Width="24px"
                                    ToolTip="Exportar Archivo de Interfaz Para Adonis." OnClick="ibExportDoc_Click" />
                            </td>
                            <td align="center" style="border-left: solid 1px silver;">
                                <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                                    onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                                    runat="server" Height="25px" Width="24px" 
                                    ToolTip="Exportar Panel de Resultados a Excel." onclick="ibExportToExcel_Click"
                                    />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style3">
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
                <asp:GridView ID="gvDocTrans" runat="server" SkinID="gridviewSkin"
                    Font-Size="Medium" AllowPaging="True" 
                    onrowcreated="grdPivot2_RowCreated" CellPadding="4" ForeColor="#333333" PageSize="18" 
                    onrowdatabound="gvDocTrans_RowDataBound" GridLines="Vertical" 
                    onpageindexchanging="gvDocTrans_PageIndexChanging">
                    <EditRowStyle BackColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <AlternatingRowStyle ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Clear_ID" HeaderText="Clear_ID">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Cuenta" HeaderText="Cuenta Contable">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CuentaDes" HeaderText="Descripcion Cuenta">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TipoEntidad" HeaderText="Tipo de Entidad">
                            <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CodigoEntidad" HeaderText="Codigo Entidad">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DesEntidad" HeaderText="Descripcion Entidad">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                        <asp:BoundField DataField="Serie" HeaderText="Serie" />
                        <asp:BoundField DataField="Numero" HeaderText="Numero">
                        </asp:BoundField>
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha">
                        </asp:BoundField>
                        <asp:BoundField DataField="Debe" HeaderText="Debe">
                        </asp:BoundField>
                        <asp:BoundField DataField="Haber" HeaderText="Haber">
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle HorizontalAlign="Right" BackColor="#5D7B9D" Font-Bold="True" 
                        ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" 
                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" BorderColor="#003366" BorderStyle="Solid" 
                        BorderWidth="1px" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
        <asp:ObjectDataSource ID="odsConsult" runat="server" SelectMethod="getSource" TypeName="www.aquarella.com.pe.Aquarella.Interface.adonisInterfaz">
        </asp:ObjectDataSource>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="panelRep_CategoriaMarca.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.panelRep_CategoriaMarca" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //$("input:text").width('200px');
            $("#tabs").tabs();
        });
    </script>
    <style type="text/css">
        .style1
        {
            width: 40px;
        }
        .style2
        {
            width: 349px;
        }
        .style4
        {
            width: 122px;
        }
        .style5
        {
            width: 44px;
        }
        .style6
        {
            width: 117px;
        }
   
    html, body, form
    {
        height: 100%;
    }

    .chart
    {
        width: 100% ;
        height: 100%;
    }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Consulta de ventas de marca por categoría
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Información de ventas .
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
                <td class="style2" style="display:none;">
                    <!-- WARE AND AREA FORM -->
                    <AQControl:WareAreaForm runat="server" Visible="false"  ID="WareAreaForm" />
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="50%" align="left">
                                <table cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td class="f12" >
                                            Marca</td>
                                        <td class="f12" >
                                            Lider</td>
                                     
                                        <td class="f12" colspan="3">
                                            Fecha de inicio:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style5">
                                            <asp:DropDownList ID="dw_Marca" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style5">
                                            <asp:DropDownList ID="dw_Lider" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style4">
                                            &nbsp;</td>
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
                                        <td class="style1">
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
                                <table cellpadding="0" cellspacing="0">
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
                                        <td class="style6">
                                            <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateEnd" runat="server"
                                                ToolTip="Fecha final" CssClass="error_asterisck" ErrorMessage="Dígite fecha final*"
                                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateEnd">*</asp:RequiredFieldValidator>
                                        </td>
                                         <td class="style6">
                                            <asp:CompareValidator ID="cvDateEnd" runat="server" ValidationGroup="vsConsultForm"
                                                Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateEnd"
                                                Operator="DataTypeCheck" ErrorMessage="Dígite una fecha final válida">*</asp:CompareValidator>
                                         </td>
                                        <td class="style6">
                                            <asp:CompareValidator ID="cvDateStartDateEnd" runat="server" ValidationGroup="vsConsultForm"

                                                Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateEnd"
                                                ControlToCompare="txtDateStart" Operator="GreaterThanEqual" ErrorMessage="Dígite una fecha final superior a la fecha inicial">*</asp:CompareValidator>
                                        </td>
                                    </tr>
                                </table>
                            </ td>
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
                        runat="server" Height="25px" Width="24px" ToolTip="Exportar Panel de Resultados a Excel."
                        OnClick="ibExportToExcel_Click" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>
    </div>
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;">
        <div>
            <div id="tabs">
                <ul>
                    <li><a href="#fragment-1"><span>Información general de ventas</span></a></li>
                    <li><a href="#fragment-2"><span>Gráfico Barras</span></a></li>
                    <li><a href="#fragment-3"><span>Gráfico Pastel</span></a></li>
                </ul>
                <!-- RESULTADO DE LA CONSULTA -->
                <div id="fragment-1">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvSales" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                                            Font-Size="Small" OnDataBound="gvSales_DataBound" OnPageIndexChanging="gvSales_PageIndexChanging"
                                            OnSorting="gvSales_Sorting">
                                            <EmptyDataTemplate>
                                                No existen registros para mostrar.
                                            </EmptyDataTemplate>
                                            <Columns>
                                               
                                                <asp:BoundField DataField="asesor" HeaderText="Asesor" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="Asesor" >
                                                <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="NombreLider" HeaderText="Lider" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="Lider" >
                                                <ItemStyle HorizontalAlign="left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Promotora" HeaderText="Promotor" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="Promotor" >
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                 <asp:BoundField DataField="Categoria" HeaderText="Linea" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="Linea" >
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                 <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center"
                                                    SortExpression="Marca" >
                                                <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="Monto" HeaderText="Venta sin IGV" SortExpression="pventas"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}" >
                                                <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                               
                                                <asp:BoundField DataField="Cantidad" HeaderText="Pares" SortExpression="ventas"
                                                    ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" >
                                                <ItemStyle HorizontalAlign="Right" />
                                                </asp:BoundField>
                                                
                                              
                                            </Columns>
                                            <FooterStyle HorizontalAlign="Right" />
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
                            </td>
                        </tr>
                    </table>
                </div>
                <!-- GRAFICO BARRAS-->
                <div id="fragment-2">
                    <table width="100%">
                        <tr>
                            <td align="left" style="width:70%">
                                <asp:UpdatePanel ID="upGraph" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Chart ID="chartSales"  runat="server" Width="700px" BackColor="243, 223, 193" 
                                            Height="350px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2px"
                                            BorderColor="#B54001">
                                            <Titles>
                                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                                                    Text="Venta Neta Por Categorías" Name="Title1" ForeColor="26, 59, 105">
                                                </asp:Title>
                                            </Titles>
                                            <Legends>
                                                <asp:Legend TitleFont="Microsoft Sans Serif, 8pt, style=Bold" BackColor="Transparent"
                                                    Font="Trebuchet MS, 8.25pt, style=Bold" IsTextAutoFit="true" Enabled="false"
                                                    Name="tienda">
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                            <Series>
                                                <asp:Series XValueType="String" Name="Series1" BorderColor="180, 26, 59, 105" ChartArea="ChartArea1"
                                                    IsVisibleInLegend="true" XValueMember="category" YValueMembers="sales">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White"
                                                    BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                    <Area3DStyle Rotation="10" Perspective="10" Inclination="15" IsRightAngleAxes="False"
                                                        WallWidth="0" IsClustered="False" />
                                                    <AxisY LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8" IsLabelAutoFit="false">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" Format="C0" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisY>
                                                    <AxisX LineColor="64, 64, 64, 64" LabelAutoFitMaxFontSize="8" IsLabelAutoFit="false">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" IsEndLabelVisible="false" />
                                                        <MajorGrid LineColor="64, 64, 64, 64" />
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="CheckboxShow3D" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="ddlLVerPor" EventName="SelectedIndexChanged" />
                                      
                                        <%--<asp:AsyncPostBackTrigger ControlID="FontAngleList" EventName="SelectedIndexChanged" />--%>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td valign="top" align="right">
                                <table cellpadding="4">
                                    <tr>
                                        <td class="label" valign="top">
                                            Mostrar en 3D:
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="upShwo3d" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="CheckboxShow3D" runat="server" AutoPostBack="True" Text="" OnCheckedChanged="CheckboxShow3D_CheckedChanged">
                                                    </asp:CheckBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" style="width: 181px">
                                            Ver Por:
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlLverPor" runat="server" CssClass="spaceright" AutoPostBack="True"
                                                        Width="120px"  OnSelectedIndexChanged="ddlLider_SelectedIndexChanged" >
                                                        <asp:ListItem Value="M" Selected="True">Monto</asp:ListItem>
                                                        <asp:ListItem Value="U">Unidades</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" style="width: 181px">
                                            Lider:
                                        </td>
                                        <td>
                                           
                                        </td>
                                    </tr>
                                     <tr>
                                        <td colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlLider"   runat="server" CssClass="spaceright" AutoPostBack="True"
                                                        Width="310px" OnSelectedIndexChanged="ddlLider_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td colspan="2">
                                         
                                           <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
	                                            <ContentTemplate>
		                                            <asp:GridView ID="gvLiderCategoria" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                                                         onpageindexchanging="gvLiderCategoria_PageIndexChanging" PageSize="4"
			                                            Font-Size="Small" >
			                                            <EmptyDataTemplate>
				                                            No existen registros para mostrar.
			                                            </EmptyDataTemplate>
			                                            <Columns>
			                                                <asp:BoundField DataField="Categoria" HeaderText="Categoria" ItemStyle-HorizontalAlign="Center"
                                                                SortExpression="Categoria" >
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                              <asp:BoundField DataField="Prc" HeaderText="Porc. (%)" SortExpression="pventas"
                                                                    ItemStyle-HorizontalAlign="center" DataFormatString="{0:N2}" >
                                                                <ItemStyle HorizontalAlign="center" />
                                                                </asp:BoundField>

			  
			                                            </Columns>
			                                            <FooterStyle HorizontalAlign="Right" />
		                                            </asp:GridView>
	                                            </ContentTemplate>
	                                            <Triggers> 
                                                    <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                                                     <asp:AsyncPostBackTrigger ControlID="ddlLider" EventName="SelectedIndexChanged" />
                                                      <asp:AsyncPostBackTrigger ControlID="ddlLverPor" EventName="SelectedIndexChanged" />
		                                        </Triggers>
                                            </asp:UpdatePanel>
                                             <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender2" runat="server" TargetControlID="upGrid">
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
                                         
                                        </td>
                                        
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <!-- ACTIVITY PANEL -->
                    <ajaxToolkit:UpdatePanelAnimationExtender ID="upaeBars" runat="server" TargetControlID="upGraph">
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
                <!-- GRAFICO PASTEL-->
                <div id="fragment-3">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="upPie" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Chart ID="ChartPie" runat="server" ImageUrl="~/TempImages/ChartPic_#SEQ(300,3)"
                                            BackColor="243, 223, 193" Width="700px" Height="350px" borderlinestyle="Solid"
                                            backgradienttype="TopBottom" BorderlineWidth="2" BorderlineColor="181, 64, 1">
                                            <Titles>
                                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                                                    Text="Title" Alignment="TopLeft" ForeColor="26, 59, 105" Name="Title1">
                                                </asp:Title>
                                            </Titles>
                                            <Legends>
                                                <asp:Legend Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"
                                                    TitleFont="Microsoft Sans Serif, 8pt, style=Bold">
                                                </asp:Legend>
                                            </Legends>
                                            <BorderSkin SkinStyle="Emboss"></BorderSkin>
                                            <Series>
                                                <asp:Series Name="Series1" ChartType="Pie" BorderColor="180, 26, 59, 105" ShadowOffset="4"
                                                    XValueMember="category" YValueMembers="sales" CustomProperties="DoughnutRadius=25, PieDrawingStyle=Concave, CollectedLabel=Other, MinimumRelativePieSize=20"
                                                    MarkerStyle="Circle">
                                                </asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="Default" BorderColor="64, 64, 64, 64" BorderDashStyle="Solid"
                                                    BackSecondaryColor="White" BackColor="OldLace" ShadowColor="Transparent" BackGradientStyle="TopBottom">
                                                    <AxisY LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                    </AxisY>
                                                    <AxisX LineColor="64, 64, 64, 64">
                                                        <LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold"></LabelStyle>
                                                        <MajorGrid LineColor="64, 64, 64, 64"></MajorGrid>
                                                    </AxisX>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                                        <asp:AsyncPostBackTrigger ControlID="chkShow3d" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="comboBoxChartType" EventName="SelectedIndexChanged" /> 
                                         <asp:AsyncPostBackTrigger ControlID="ddlVerPor" EventName="SelectedIndexChanged" />
                                         <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                                         <asp:AsyncPostBackTrigger ControlID="ddlLVerPor" EventName="SelectedIndexChanged" />
                                         <asp:AsyncPostBackTrigger ControlID="ddlLider" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <!-- ACTIVITY PANEL -->
                                <ajaxToolkit:UpdatePanelAnimationExtender ID="upaePie" runat="server" TargetControlID="upPie">
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
                            </td>
                            <td valign="top" align="right">
                                <table cellpadding="4">
                                    <tr>
                                        <td class="label" valign="top">
                                            Mostrar en 3D:
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chkShow3d" TabIndex="6" runat="server" AutoPostBack="True" Text=""
                                                        OnCheckedChanged="chkShow3d_CheckedChanged"></asp:CheckBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" style="width: 181px">
                                            Tipo gráfico:
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="upTypeChart" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="comboBoxChartType" runat="server" CssClass="spaceright" AutoPostBack="True"
                                                        Width="120px" OnSelectedIndexChanged="comboBoxChartType_SelectedIndexChanged">
                                                        <asp:ListItem Value="Pie" Selected="True">Pastel</asp:ListItem>
                                                        <asp:ListItem Value="Doughnut">Dona</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td class="label" style="width: 181px">
                                            Ver Por:
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlVerPor" runat="server" CssClass="spaceright" AutoPostBack="True"
                                                        Width="120px"  OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" >
                                                        <asp:ListItem Value="M" Selected="True">Monto</asp:ListItem>
                                                        <asp:ListItem Value="U">Unidades</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label" style="width: 181px">
                                            Categoria:
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="spaceright" AutoPostBack="True"
                                                        Width="120px" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                         
                                           <asp:UpdatePanel ID="upGrid2" runat="server" UpdateMode="Conditional">
	                                            <ContentTemplate>
		                                            <asp:GridView ID="gvSales2" runat="server" SkinID="gridviewSkin" ShowFooter="True"
			                                            Font-Size="Small" onpageindexchanging="gvSales2_PageIndexChanging" PageSize="5" >
			                                            <EmptyDataTemplate>
				                                            No existen registros para mostrar.
			                                            </EmptyDataTemplate>
			                                            <Columns>
			                                                <asp:BoundField DataField="Marca" HeaderText="Marca" ItemStyle-HorizontalAlign="Center"
                                                                SortExpression="Marca" >
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                              <asp:BoundField DataField="Prc" HeaderText="Porc. (%)" SortExpression="pventas"
                                                                    ItemStyle-HorizontalAlign="center" DataFormatString="{0:N2}" >
                                                                <ItemStyle HorizontalAlign="center" />
                                                                </asp:BoundField>

			  
			                                            </Columns>
			                                            <FooterStyle HorizontalAlign="Right" />
		                                            </asp:GridView>
	                                            </ContentTemplate>
	                                            <Triggers>
                                                     <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                                                     <asp:AsyncPostBackTrigger ControlID="ddlCategoria" EventName="SelectedIndexChanged" />
                                                      <asp:AsyncPostBackTrigger ControlID="ddlVerPor" EventName="SelectedIndexChanged" />
		                                        </Triggers>
                                            </asp:UpdatePanel>
                                             <ajaxToolkit:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" runat="server" TargetControlID="upGrid">
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
                                         
                                        </td>
                                        
                                    </tr>
                                   
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
  
</asp:Content>

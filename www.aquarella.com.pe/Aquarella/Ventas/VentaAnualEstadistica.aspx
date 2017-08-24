<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="VentaAnualEstadistica.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.VentaAnualEstadistica" %>

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
            width: 257px;
        }
        .style3
        {
            width: 207px;
        }
        .style4
        {
            width: 116px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Estadistica de Venta&nbsp; Anual
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Esta consulta son las ventas neto Anual .  
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
                            <td class="style3" align="right">
                                <asp:RadioButton ID="optmonto" runat="server" GroupName="grupo" Text="Monto" />
                            </td>
                             <td>
                                 <asp:RadioButton ID="optunidad" runat="server" GroupName="grupo" 
                                     Text="Unidad" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                Año</td>
                             <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="dwanio" runat="server" AppendDataBoundItems="true" 
                                                                DataTextField="anio" DataValueField="idanio" ToolTip="Selecionar el año" 
                                                                Width="180px">
                                                                <asp:ListItem Text=" -- Seleccionar el año --" Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                            </td>
                             <td>
                       <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidatioGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" /> 
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="style4"  >
                       <br />
                </td>
                <td align="left" style="border-left: solid 1px silver;">
                    &nbsp;</td>
            </tr>
            </table>
         <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;">
        <div>
            <div id="tabs">
                <ul>
                  <li><a href="#fragment-2"><span>Gráfico Barras</span></a></li>
                </ul>
                                <div id="fragment-2">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="upGraph" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <asp:Chart ID="chartSales" runat="server" BackColor="243, 223, 193" Width="700px"
                                            Height="350px" BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2px"
                                            BorderColor="#B54001" Palette="Excel">

                                            <Titles>
                                                <asp:Title ShadowColor="32, 0, 0, 0" Font="Trebuchet MS, 14.25pt, style=Bold" ShadowOffset="3"
                                                    Text="Venta Neta Anual" Name="Title1" ForeColor="26, 59, 105">
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
                                        <asp:AsyncPostBackTrigger ControlID="FontAngleList" EventName="SelectedIndexChanged" />
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
                                        <td>
                                            Angulo leyenda del Eje X:
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="upFontAngle" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="FontAngleList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FontAngleList_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Selected="True">0</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                        <asp:ListItem Value="60">60</asp:ListItem>
                                                        <asp:ListItem Value="90">90</asp:ListItem>
                                                        <asp:ListItem Value="-30">-30</asp:ListItem>
                                                        <asp:ListItem Value="-45">-45</asp:ListItem>
                                                        <asp:ListItem Value="-60">-60</asp:ListItem>
                                                        <asp:ListItem Value="-90">-90</asp:ListItem>
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="bottom" class="f13 fsal">
                                            <asp:UpdatePanel ID="updletra" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblletra" runat="server" Text="Label"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                            <asp:UpdatePanel ID="upTotSales1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblTotSales1" CssClass="f13" runat="server" Font-Bold="True"></asp:Label>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <p style="text-align: justify;">
                                                <b>Tips:</b>Ubique el Scroll del mouse sobre las barras para observar el valor de
                                                la venta y la participación en porcentaje.</p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;</td>
                            <td valign="top" align="right">
                                &nbsp;</td>
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
                </div>
            </div>
        </div>
        <br />
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="SiteTheme" CodeBehind="panelPaymentList.aspx.cs"
    Inherits="www.aquarella.com.pe.Aquarella.Financiera.panelPaymentList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
    <link href="../../Styles/theme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("input:submit,button").button();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td>
                <!-- PAGE TITTLE -->
                <div class="title">
                    <h1>
                        Historial de consignaciones
                    </h1>
                </div>
                <!-- CONTENT PLACE HOLDER -->
                <div class="d-cont">
                    <!-- -->
                    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="99999">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="upHd" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdCreditValue" Value="0" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!-- Area de errores -->
                    <asp:UpdatePanel ID="upPanelMsg" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                        </Triggers>
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
                    <!-- SEL CUSTOMER -->
                    <asp:Panel ID="pnlDwCustomers" Visible="false" runat="server">
                        <div style="margin: 10px auto 0 auto;">
                            <table width="100%" class="tablagris" cellpadding="4">
                                <tr>
                                    <td class="fsal f13">
                                        Selección de cliente
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="1" cellspacing="1">
                                            <tr>
                                                <td class="f12" width="60%">
                                                    Seleccione un cliente de la lista:<br />
                                                    (Desc: Seleccione un cliente sobre el cual realizará las acciones.)
                                                </td>
                                                <td align="left">
                                                    <asp:DropDownList ID="dwCustomers" AutoPostBack="true" DataTextField="Nombres"
                                                        DataValueField="bas_id" AppendDataBoundItems="true" runat="server"
                                                        ToolTip="Selecionar un cliente" Width="220px" OnSelectedIndexChanged="dwCustomers_SelectedIndexChanged">
                                                        <asp:ListItem Value="-1" Text=" -- Seleccione de la Lista --"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                    <!-- -->
                    <div style="margin: 10px auto 0 auto;">
                        <table width="100%" cellpadding="0">
                            <tr>
                                <td class="fsal f13" colspan="2">
                                    Panel de listado de consignaciones
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td class="f12" width="60%">
                                                Realice un filtro de los resultados:<br />
                                                (Desc: Puede buscar por número de consignación, comprobante, valor)
                                            </td>
                                            <td align="left">
                                                <asp:UpdatePanel ID="upFilter" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtFilter" runat="server" Width="190px"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:Button ID="btFilter" runat="server" Text="Filtrar" OnClick="btFilter_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btRefresh" runat="server" Text="Refrescar panel" OnClick="btRefresh_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                                                    runat="server" Height="25px" Width="24px" ToolTip="Exportar Panel de Resultados a Excel."
                                                    onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                                                    OnClick="ibExportToExcel_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- PANEL DE RESULTADOS -->
                    <div style="margin: 10px auto 0 auto; min-height: 200px;" class="f13">
                        <asp:UpdatePanel ID="upGridClear" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvPays" runat="server" SkinID="gridviewSkin" ShowFooter="true"
                                    PageSize="15">
                                    <EmptyDataTemplate>
                                        Sin resultados.
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="Pag_Id" HeaderText="Pago No." ReadOnly="True"
                                            SortExpression="pagono" />
                                        <asp:BoundField DataField="Ban_Descripcion" HeaderText="Banco" SortExpression="banco">
                                            <ControlStyle CssClass="campo1" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Pag_Num_Consignacion" HeaderText="Compr. Pago" SortExpression="comprobante" />
                                        <asp:BoundField DataField="Pag_Num_ConsFecha" HeaderText="Fecha Pago" SortExpression="fecha"
                                            DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="Pag_Monto" HeaderText="Valor" SortExpression="valor"
                                            DataFormatString="{0:N}" />
                                        <asp:BoundField DataField="Est_Descripcion" HeaderText="Estado" SortExpression="estado" />
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:ObjectDataSource ID="odsPays" runat="server" SelectMethod="getPaymentsByCoordinator"
                            TypeName="www.aquarella.com.pe.bll.Payments" OnSelected="odsPays_Selected">
                            <SelectParameters>                                
                                <asp:Parameter Name="_idCust" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="odsFilter" runat="server" SelectMethod="getFilterObject"
                            TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsFilter_Selecting">
                            <SelectParameters>
                                <asp:Parameter Name="dtObj" Type="Object" />
                                <asp:Parameter DefaultValue="0" Name="posTable" Type="Int32" />
                                <asp:Parameter DefaultValue="Pag_Id" Name="f1" Type="String" />
                                <asp:Parameter DefaultValue="Pag_Num_Consignacion" Name="f2" Type="String" />
                                <asp:Parameter DefaultValue="Pag_Monto" Name="f3" Type="String" />
                                <asp:ControlParameter ControlID="txtFilter" DefaultValue="" Name="fieldValue1" PropertyName="Text"
                                    Type="String" />
                                <asp:ControlParameter ControlID="txtFilter" Name="fieldValue2" PropertyName="Text"
                                    Type="String" />
                                <asp:ControlParameter ControlID="txtFilter" Name="fieldValue3" PropertyName="Text"
                                    Type="String" />
                                <asp:Parameter DefaultValue="dateclear" Name="fieldOrder" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <!-- -->
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

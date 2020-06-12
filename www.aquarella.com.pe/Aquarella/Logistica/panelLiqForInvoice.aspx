<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="panelLiqForInvoice.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.panelLiqForInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Consulta de liquidaciones activas en bodega
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Información acerca del ciclo de vida de una liquidación en bodega; podrá encontrar
    una liquidación en cualquiera de las etapas del ciclo de vida del pedido, quiere
    decir que podrá ubicar el estado en el cual se encuentren las diferentes liquidaciones,
    siendo los estados: separado, en marcación o en proceso de facturación.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                <td class="fsal f13">
                    Panel de listado de liquidaciones
                </td>
                <td>
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        runat="server" Height="25px" Width="24px" ToolTip="Exportar Panel de Resultados a Excel."
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        OnClick="ibExportToExcel_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="f12" width="50%">
                                Realice un filtro de los resultados:<br />
                                (Desc: Puede buscar por estado, cliente, no.Liq, bodega)
                            </td>
                            <td>
                                Sel. de estado (Excluyente):
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="dwStatus" runat="server" DataTextField="status" DataValueField="status"
                                            AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="dwStatus_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                Dígite un filtro:
                                <asp:UpdatePanel ID="upFilter" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtFilter" runat="server" Width="190px"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <br />
                                <asp:Button ID="btFilter" runat="server" Text="Filtrar" OnClick="btFilter_Click" />
                            </td>
                            <td>
                                <br />
                                <asp:Button ID="btRefresh" runat="server" Text="Refrescar panel" OnClick="btRefresh_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;" class="f-small">
        <asp:UpdatePanel ID="upGridLiqPick" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvLiqs" runat="server" SkinID="gridviewSkin" ShowFooter="true"
                    OnDataBound="gvLiqs_DataBound" FooterStyle-HorizontalAlign="Right">
                    <EmptyDataTemplate>
                        No existen liquidaciones para marcar.
                    </EmptyDataTemplate>
                    <Columns>
                         <asp:BoundField DataField="asesor" HeaderText="Asesor" SortExpression="asesor" />    
                        <asp:BoundField DataField="Are_Descripcion" HeaderText="Área" SortExpression="Are_Descripcion" />                        
                        <asp:BoundField DataField="nombres" HeaderText="Cliente" SortExpression="nombres" />
                        <asp:BoundField DataField="dni_promotor" HeaderText="Dni-Cliente" SortExpression="dni_promotor" />
                        <asp:BoundField DataField="ubicacion" HeaderText="Ubicación" SortExpression="ubicacion" />
                        <asp:BoundField DataField="Liq_Id" HeaderText="No.Liq" SortExpression="Liq_Id" />
                        <asp:BoundField DataField="Liq_EstId" HeaderText="Estdo" SortExpression="Liq_EstId"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Liq_Fecha" HeaderText="Fecha" SortExpression="lhd_date" />
                        <asp:BoundField DataField="Liq_Fecha_Expiracion" HeaderText="Expiración" SortExpression="Liq_Fecha_Expiracion" />
                        <asp:BoundField DataField="fecha_grupo" HeaderText="Liberación" SortExpression="fecha_grupo" />
                        <asp:BoundField DataField="totalpares" HeaderText="Unds" SortExpression="totalpares" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Paq_Cantidad" HeaderText="Uds.Emp" SortExpression="Paq_Cantidad"
                            ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="liq_value" HeaderText="Valor" SortExpression="liq_value"
                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="dwStatus" EventName="selectedindexchanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsLiqs" runat="server" SelectMethod="getLiqActives" TypeName="www.aquarella.com.pe.bll.Liquidations_Hdr"
            OnSelected="odsLiqs_Selected">
            <SelectParameters>                
                <asp:Parameter DefaultValue="-1" Name="_area" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsFilter" runat="server" SelectMethod="getFilterObject"
            TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsFilter_Selecting"
            OnSelected="odsFilter_Selected">
            <SelectParameters>
                <asp:Parameter Name="dtObj" Type="Object" DefaultValue="" />
                <asp:Parameter DefaultValue="0" Name="posTable" Type="Int32" />
                <asp:Parameter DefaultValue="Liq_Id" Name="f1" Type="String" />
                <asp:Parameter DefaultValue="Are_Descripcion" Name="f2" Type="String" />
                <asp:Parameter DefaultValue="nombres" Name="f3" Type="String" />
                <asp:ControlParameter ControlID="txtFilter" DefaultValue="" Name="fieldValue1" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtFilter" Name="fieldValue2" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtFilter" DefaultValue="" Name="fieldValue3" PropertyName="Text"
                    Type="String" />
                <asp:Parameter DefaultValue="article" Name="fieldOrder" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <!-- -->
        <asp:ObjectDataSource ID="odsFilter2" runat="server" SelectMethod="getFilterObject"
            TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelected="odsFilter_Selected"
            OnSelecting="odsFilter2_Selecting">
            <SelectParameters>
                <asp:Parameter Name="dtObj" Type="Object" DefaultValue="" />
                <asp:Parameter DefaultValue="0" Name="posTable" Type="Int32" />
                <asp:Parameter DefaultValue="Liq_EstId" Name="f1" Type="String" />
                <asp:ControlParameter ControlID="dwStatus" Name="fieldValue1" PropertyName="SelectedValue"
                    Type="String" DefaultValue="" />
                <asp:Parameter DefaultValue="Liq_Fecha" Name="fieldOrder" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

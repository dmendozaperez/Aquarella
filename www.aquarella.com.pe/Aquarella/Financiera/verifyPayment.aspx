<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="verifyPayment.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.verifyPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Confirmación de pagos y consignaciones
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Realice la verificación de pagos y consignaciones, mediante la comparación de la
    información encontrAQUARELLA aquí frente a la información arrojAQUARELLA por los reportes de
    los recaudadores.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
        });
         // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
       
    </script>

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
                <td>
                    <div style="font-family:Verdana; font-size:10px; font-weight:bold;">
                        <a class='iframe' href='panelPaymentList.aspx' title="Historial De Liquidaciones (LIQ. NO.) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                            Consultar historial de consignaciones</a>&nbsp;&nbsp;
                        <a class='iframe' href='panelPaymentList.aspx' title="Historial De Liquidaciones (LIQ. NO.) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                            <img alt="Consultar Historia de consignaciones"  style="border:0;" src="../../Design/images/lupa.jpg" />
                        </a>
                    </div> <br />
                </td>
            </tr>
            <tr>
                <td class="fsal f13" colspan="2">
                    Lista de pagos y consignaciones
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="f12" width="60%">
                                Realice un filtro de los resultados:<br />
                                (Desc: Puede buscar por No. pago, consignación, valor)
                            </td>
                            <td>
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
                            <td align="right" style="border-left: solid 1px silver;">
                                <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                                    onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                                    runat="server" Height="25px" Width="24px" ToolTip="Exportar Panel de Resultados a Excel."
                                    OnClick="ibExportToExcel_Click" />
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
                <asp:GridView ID="gvListPays" runat="server" SkinID="gridviewSkin" ShowFooter="true"
                    DataKeyNames="Pag_Id" OnRowCommand="gvListPays_RowCommand" AllowPaging="true"
                    PageSize="10" OnDataBound="gvListPays_DataBound" OnRowDataBound="gvListPays_RowDataBound">
                    <EmptyDataTemplate>
                        No existen pagos o consignaciones en espera de revisión.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="Pag_Id" ItemStyle-ForeColor="Maroon" HeaderText="Pago No."
                            ReadOnly="True" SortExpression="Pag_Id" />
                        <asp:BoundField DataField="Lider" ItemStyle-ForeColor="#47B224" HeaderText="Lider"
                            ReadOnly="True" SortExpression="Lider" />
                        <asp:BoundField DataField="Bas_Documento" HeaderText="Documento" ReadOnly="True"
                            SortExpression="Bas_Documento" />
                        <asp:BoundField DataField="Promotor" HeaderText="Promotor" ReadOnly="True" SortExpression="Promotor" />
                        <asp:BoundField DataField="Ban_Descripcion" ItemStyle-ForeColor="DimGray" HeaderText="Banco"
                            SortExpression="Ban_Descripcion" ReadOnly="True">
                            <ControlStyle CssClass="campo1" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pag_Num_Consignacion" HeaderText="Compr. Pago" SortExpression="Pag_Num_Consignacion"
                            ReadOnly="True" />
                        <asp:BoundField DataField="Con_Descripcion" HeaderText="Tipo" SortExpression="Con_Descripcion"
                            ReadOnly="True" />
                        <asp:BoundField DataField="Pag_Num_ConsFecha" HeaderText="Fecha Pago" SortExpression="Pag_Num_ConsFecha"
                            DataFormatString="{0:d}" ReadOnly="True" />
                        <asp:BoundField DataField="Pag_Monto" HeaderText="Valor" SortExpression="Pag_Monto"
                            DataFormatString="{0:c}" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownList ID="dwStatusPay" runat="server" DataSourceID="odsStatusPays" DataTextField="Est_Descripcion"
                                    DataValueField="Est_Id" SelectedValue='<%# Bind("Est_Id") %>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aplicar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibApplyUpdate" CommandName="ApplyUpdate" CommandArgument='<%# Eval("Pag_Id")%>'
                                    runat="server" ImageUrl="~/Design/images/Botones/chulo.png" ToolTip="Aplicar cambio en estado" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsPays" runat="server" SelectMethod="loadPaymentsByWarehouseAndStatus"
            TypeName="www.aquarella.com.pe.bll.Payments" UpdateMethod="updatePayment" OnSelected="odsPays_Selected">
            <SelectParameters>                
                <asp:Parameter DefaultValue="PXV" Name="_status" Type="String" />                
                <asp:Parameter DefaultValue="" Name="_idArea" Type="String" />
            </SelectParameters>
            <UpdateParameters>               
                <asp:ControlParameter ControlID="gvListPays" Name="_paymentId" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="gvListPays" Name="_status" PropertyName="SelectedValue" />
            </UpdateParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsStatusPays" runat="server" SelectMethod="getStatusByModule"
            CacheDuration="10" EnableCaching="true" TypeName="www.aquarella.com.pe.bll.Status">
            <SelectParameters>                
                <asp:Parameter DefaultValue="" Name="_module" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsFilter" runat="server" SelectMethod="getFilterObject"
            TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsFilter_Selecting">
            <SelectParameters>
                <asp:Parameter Name="dtObj" Type="Object" />
                <asp:Parameter DefaultValue="0" Name="posTable" Type="Int32" />
                <asp:Parameter DefaultValue="pag_id" Name="f1" Type="String" />
                <asp:Parameter DefaultValue="pag_num_consignacion" Name="f2" Type="String" />
                <asp:Parameter DefaultValue="pag_monto" Name="f3" Type="String" />
                <asp:ControlParameter ControlID="txtFilter" DefaultValue="" Name="fieldValue1" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtFilter" Name="fieldValue2" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="txtFilter" Name="fieldValue3" PropertyName="Text"
                    Type="String" />
                <asp:Parameter DefaultValue="pav_payment_id" Name="fieldOrder" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

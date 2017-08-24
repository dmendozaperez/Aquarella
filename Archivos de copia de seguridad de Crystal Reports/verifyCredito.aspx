<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="verifyCredito.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.verifyCredito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .style1
        {
            width: 268px;
        }
        .style2
        {
            width: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Confirmación de pagos al Credito 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    En esta ventana usted debe validar el pedido al credito del cliente
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
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="fsal f13" colspan="2">
                    Lista de pedidos al credito</td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="f12" width="60%">
                                &nbsp;</td>
                            <td class="style2">
                                &nbsp;</td>
                            <td class="style1">
                                &nbsp;</td>
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
                <asp:GridView ID="gvListPays" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    DataKeyNames="liquidacion" OnRowCommand="gvListPays_RowCommand" 
                    AllowPaging="True" OnDataBound="gvListPays_DataBound" 
                    OnRowDataBound="gvListPays_RowDataBound">
                    <EmptyDataTemplate>
                        No existen pedidos con pago al credito en espera de revisión.
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField DataField="liquidacion" ItemStyle-ForeColor="Maroon" HeaderText="Nro.liq."
                            ReadOnly="True" SortExpression="liquidacion" >
                        <ItemStyle ForeColor="Maroon" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Lider" ItemStyle-ForeColor="#47B224" HeaderText="Lider"
                            ReadOnly="True" SortExpression="Lider" >
                        <ItemStyle ForeColor="#000099" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Promotor" HeaderText="Promotor" ReadOnly="True" 
                            SortExpression="Promotor" />
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" SortExpression="tipo"
                            ReadOnly="True" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha"
                            DataFormatString="{0:d}" ReadOnly="True" />
                        <asp:BoundField DataField="Valor" HeaderText="Valor" SortExpression="Valor"
                            DataFormatString="{0:c}" ReadOnly="True" />
                        <asp:TemplateField HeaderText="Estado" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:DropDownList ID="dwStatusPay" runat="server" DataSourceID="odsStatusPays" DataTextField="est_descripcion"
                                    DataValueField="est_id" SelectedValue='<%# Bind("pav_status") %>'>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aplicar" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibApplyUpdate" CommandName="ApplyUpdate" CommandArgument='<%# Eval("Liquidacion")%>'
                                    runat="server" ImageUrl="~/Design/images/Botones/chulo.png" ToolTip="Aplicar cambio en estado" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />               
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsPays" runat="server" SelectMethod="loadpagoalcredito"
            TypeName="www.aquarella.com.pe.bll.Payments" UpdateMethod="updatePayment" OnSelected="odsPays_Selected">            
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
    </div>
</asp:Content>

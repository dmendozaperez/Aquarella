<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="consultaHPedido.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.consultaHPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
        .style2
        {
            width: 99px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Consulta de Pedido por Dni o Ruc
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Busqueda de historial de pedido por Dni o Ruc 
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
    <br />
                    <table cellpadding="4" cellspacing="4">
                        <tr>
                            <td class="f12">
                                Campo de consulta:<br />
                                (Desc: Puede buscar por 
                                Nro de Dni o Ruc)
                            </td>
                            <td>
                                <table cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upFilter" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtdniruc" runat="server" Width="190px"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td class="style2">
                                            <asp:Button ID="btConsult" runat="server" Text="Consultar" 
                                                onclick="btConsult_Click"  />
                                        </td>
                                        <td>
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." 
                        onclick="ibExportToExcel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                            </td>
                            <td class="style1">
                            </td>
                        </tr>
                    </table>
         <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                
                        <asp:GridView ID="gvped" runat="server" AllowSorting="True" 
                             AllowPaging="True"
                 SkinID="gridviewSkin" PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="1072px" 
                    AutoGenerateColumns="False" ondatabound="gvped_DataBound">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="GridViewBlue-tf" BackColor="#2461BF" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Dni o Ruc" HeaderText="Dni o Ruc" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Cliente" HeaderText="Cliente" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nro Pedido" HeaderText="Nro Pedido" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="F.Pedido" HeaderText="F.Pedido" 
                                    DataFormatString="{0:d}" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total" 
                                    DataFormatString="{0:C2}" >
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Estado" HeaderText="Estado" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nro Liquidacion" HeaderText="Nro Liquid." >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="F.Liquidacion" HeaderText="F.Liqui." 
                                    DataFormatString="{0:d}" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nro.Doc" HeaderText="Nro.Venta" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="F.Doc" HeaderText="F.Venta" 
                                    DataFormatString="{0:d}" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nro.NC" HeaderText="Nro.NC" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="F.NC" HeaderText="F.NC" DataFormatString="{0:d}" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="stv_description" HeaderText="Estado NC" />
                            </Columns>
                           <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsReturns" runat="server" SelectMethod="fbuscarpedido_dniruc"
            TypeName="www.aquarella.com.pe.bll.Order_Hdr" 
        onselected="odsReturns_Selected">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtdniruc" Name="_dniruc" 
                    PropertyName="Text" />
            </SelectParameters>
        </asp:ObjectDataSource>

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
    <br />
    </asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="ListaLider.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.ListaLider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .style1
        {
            width: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Lista de Lideres
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                <div 
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
                </div>
                    <br />
                    <br />
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." onclick="ibExportToExcel_Click" 
                         />
                </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%; height: 91px;">
        <tr>
            <td>
            <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                                   <asp:GridView ID="gdlista" runat="server" AllowSorting="True" 
                    ShowFooter="True" AllowPaging="True" PageSize="10"
                 SkinID="gridviewSkin" PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="1072px" 
                                       >
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="GridViewBlue-tf" BackColor="#2461BF" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" />
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
                </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsReturns" runat="server" SelectMethod="getlistalider"
            TypeName="www.aquarella.com.pe.Aquarella.Lider.Lider" OnSelected="odsReturns_Selected">
            <SelectParameters>
                <asp:Parameter Name="valor" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

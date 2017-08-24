<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="InfoClienteFina.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.InfoClienteFina" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
    .style1
    {
        width: 329px;
    }
        .style2
        {
            width: 2221px;
        }
        .style3
        {
            width: 216px;
        }
        .style4
        {
            width: 82px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Informacion del cliente por dni o ruc  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulte el cliente por dni o ruc para agregar a la lista 
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
                <div 
                    
                    style="position: absolute; left: 0; background: #f5f5f5; filter: alpha(opacity=85);
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 100%; font-size: medium;">
                    <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
                    Cargando información...
                </div>
                  </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
            <br />
    <br />
    <table style="width: 93%; height: 29px;">
    <tr>
        <td class="style1">
            Buscar por Dni o Ruc</td>
        <td class="style3">
            <asp:TextBox ID="txtbuscar" runat="server" Width="148px"></asp:TextBox>
        </td>
        <td class="style4">
                    <asp:Button ID="btConsult" runat="server" Text="Agregar" 
                        onclick="btConsult_Click"  />
        </td>
        <td class="style2">
                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="25px" Width="24px" 
                        ToolTip="Exportar Panel de Resultados a Excel." 
                        onclick="ibExportToExcel_Click" />
        </td>
    </tr>
    </table>
    <br />
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvclientes" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    Font-Size="Small" 
                    AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="1094px" 
                    onrowcommand="gvclientes_RowCommand" onrowdatabound="gvclientes_RowDataBound" 
                    style="margin-right: 0px" PageSize="3">
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="rucdni" HeaderText="Ruc o Dni" SortExpression="rucdni"
                            ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombres" HeaderText="Nombres" SortExpression="nombres"
                            ItemStyle-HorizontalAlign="Center">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Direccion" 
                            SortExpression="direccion" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true">
                            <ItemTemplate>
                                   <asp:ImageButton ID="ibanular" runat="server" 
                                       CommandArgument='<%# Eval("rucdni")%>' CommandName="starnular" 
                                       ImageUrl="~/Design/images/Botones/delete_off.png" 
                                       ToolTip="Borrar Cliente de la Lista." />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle HorizontalAlign="Right" BackColor="#5D7B9D" Font-Bold="True" 
                        ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
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
    </asp:Content>

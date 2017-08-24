<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaPromotorXLider.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.ConsultaPromotorXLider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 309px;
        }
        .auto-style2 {
            height: 59px;
        }
        .auto-style3 {
            width: 145px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
     Consulta de Promotor por lider 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulte lista de promotores por lider
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
      <asp:Panel ID="pnlDwCustomers" Visible="true" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="auto-style2" cellpadding="4">
                <tr>
                    <td>
                        <table cellpadding="1" cellspacing="1" width="100%">
                            <tr>
                                <td class="f12" width="5%">
                                    Lider:</td>
                                <td class="auto-style1">
                                    <asp:DropDownList ID="dwlider" runat="server" AppendDataBoundItems="true" 
                                        DataTextField="Are_Descripcion" 
                                        DataValueField="Are_Id"
                                        ToolTip="Selecionar un lider" Width="280px" style="cursor:pointer">
                                        <asp:ListItem Text=" -- Seleccionar a todos --" Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td class="auto-style3">
                                      <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" />
                                </td>
                                  <td align="left" style="border-left: solid 1px silver;">
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
            </table>
        </div>
               <div  style="min-height: 200px; align-content:center; width: 1017px;" align="center">                   
                    <asp:UpdatePanel ID="uppromotor" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvpromotor" runat="server" Width="98%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False"  Font-Size="Small" AllowPaging="True" AllowSorting="True" CellPadding="3" ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"  OnPageIndexChanging="gvmanifiesto_PageIndexChanging" PageSize="12">
                                <EmptyDataTemplate>
                                    No existen datos que mostrar.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Lider"
                                        HeaderText="Lider" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="promotor" HeaderText="Promotor" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="documento" HeaderText="Documento" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="direccion" HeaderText="Direccion" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="telefono" HeaderText="Telefono" Visible="true" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Correo" HeaderText="Correo" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Celular" HeaderText="Celular" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" ForeColor="#000066" />
                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                            </asp:GridView>                         
                        </ContentTemplate>
                        <Triggers>                        
                            <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvpromotor" EventName="PageIndexChanging" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="uppromotor">
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
    </asp:Panel>
</asp:Content>

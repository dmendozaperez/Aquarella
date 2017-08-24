<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Lista_PreciosAQ.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Maestros.Lista_PreciosAQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">

    <style type="text/css">
        .auto-style1 {
            width: 161px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Lista de Precios de Articulos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Esta ventana muestra los precios de los articulos.
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
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 100%; font-size: medium; top: 0px;">
                    <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
                    Cargando información...
                </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td valign="middle" class="auto-style1">
                    <asp:Button ID="btConsult" runat="server" Text="Refresh" 
                         OnClick="btConsult_Click" />
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
    </div>
     <!-- PANEL DE RESULTADOS -->
    
    <div style="margin: 1px 1px 1px 1px;  width: 1078px; overflow: auto;">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server" AllowSorting="True" 
                    ShowFooter="True" AllowPaging="True" PageSize="20"
                 SkinID="gridviewSkin" PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                    CellPadding="4" Width="1072px" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                    onpageindexchanging="gvReturns_PageIndexChanging" 
                   >
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle CssClass="GridViewBlue-tf" BackColor="#99CCCC" ForeColor="#003399" 
                        HorizontalAlign="Left" />
                    <Columns>                      
                        <asp:BoundField DataField="cat_principal" HeaderText="Categoria">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="idarticulo" HeaderText="Articulo">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>                                              
                        <asp:BoundField DataField="precioigv" HeaderText="Precio Inc(Igv)" DataFormatString="{0:C}">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                        </asp:BoundField>                        
                        <asp:BoundField DataField="preciosinigv" HeaderText="Precio Sin(Igv)" DataFormatString="{0:C}">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="70px" />
                        </asp:BoundField>                        
                    </Columns>
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="gvReturns" EventName="PageIndexChanging" />                
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
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" 
StylesheetTheme="SiteTheme" CodeBehind="stockcategoria.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.stockcategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
   <script type="text/javascript" src="../../Scripts/ColorBox/jquery.colorbox.js"></script>
    <link href="../../Scripts/ColorBox/colorbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
//            if (isAsyncPostback) {
                $(".iframe").colorbox({ width: "55%", height: "70%", iframe: true });
//            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 382px;
        }
        .style2
        {
            width: 381px;
        }
        .style3
        {
            width: 231px;
        }
        .style4
        {
            width: 375px;
        }
        .style5
        {
            width: 373px;
        }
        .style6
        {
            width: 229px;
        }
        .style7
        {
            width: 153px;
        }
        .auto-style1 {
            width: 157px;
        }
        .auto-style2 {
            width: 148px;
        }
        .auto-style3 {
            width: 368%;
        }
        .auto-style4 {
            width: 92%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Stock de Articulo por categoria detallado
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Para realizar la busqueda seleccione una categoria especifica
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
    <table style="width:100%;">
        <tr>
            <td class="auto-style1">
    <asp:Panel ID="pnlDwCustomers" Visible="false" runat="server" Width="848px">
        <div style="margin: 10px auto 0 auto;">
            <table class="tablagris" cellpadding="4">
                <tr>
                    
                           
                                <td class="style7">
                                    Seleccione una categoria </td>
                                <td>
                                    <table class="auto-style4">
                                        <tr>
                                            <td class="style4">
                                                <asp:DropDownList ID="dwcategoria" runat="server" AppendDataBoundItems="true" 
                                                    DataTextField="Cat_Pri_Descripcion" DataValueField="Cat_Pri_Id" 
                                                    ToolTip="Selecionar una Categoria" Width="180px" AutoPostBack="True" 
                                                    onselectedindexchanged="dwcategoria_SelectedIndexChanged">
                                                    <asp:ListItem Text=" -- Seleccionar a todos --" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>                                           
                                            <td class="auto-style2">
                                            Seleccione la Temporada
                                            </td>
                                             <td class="style4">
                                                <asp:DropDownList ID="dwtemporada" runat="server" AppendDataBoundItems="true" 
                                                    DataTextField="des_tempo" DataValueField="cod_tempo" 
                                                    ToolTip="Selecionar una Temporada" Width="180px" AutoPostBack="True" 
                                                    onselectedindexchanged="dwtemporada_SelectedIndexChanged">
                                                    <asp:ListItem Text=" -- Seleccionar a todos --" Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style2">
                                                <asp:ImageButton ID="ibExportToExcel" runat="server" Height="25px" 
                                                    ImageUrl="~/Design/images/Botones/b_toExcel.png" 
                                                    onclick="ibExportToExcel_Click" onmouseout="this.style.background=''" 
                                                    onmouseover="this.style.background='green';" 
                                                    ToolTip="Exportar Panel de Resultados a Excel." Width="24px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>                              
                            
                      
                  
                </tr>
            </table>
        </div>
    </asp:Panel>
            </td>
            <td align="left"  >
                       <br />
                       <asp:Button ID="btConsult" style="visibility:hidden" runat="server" Text="Consultar" ValidatioGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" /> 
                </td>
        </tr>
    </table>
    <br />
     <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server"  BackColor="White" 
                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    onrowcreated="grdPivot2_RowCreated" AllowPaging="True" Font-Bold="False" 
                    Font-Size="Small" Height="327px" Width="1061px" 
                    onpageindexchanging="gvReturns_PageIndexChanging" PageSize="14" 
                    onrowdatabound="gvReturns_RowDataBound" 
                    onrowcommand="gvReturns_RowCommand">
                    <RowStyle ForeColor="#000066" Wrap="True" />
                    <Columns>
                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
                         <asp:TemplateField  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhotoArticle" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" Wrap="True" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="gvReturns" EventName="PageIndexChanging" />
                <asp:AsyncPostBackTrigger ControlID="dwcategoria" 
                    EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="dwtemporada" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
</asp:Content>

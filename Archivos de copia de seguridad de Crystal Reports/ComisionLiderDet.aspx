<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComisionLiderDet.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.ComisionLiderDet" StyleSheetTheme="SiteTheme" %>

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
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $("input:submit,button").button();
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 71px;
        }
        .style2
        {
            height: 22px;
        }
        .style3
        {
            width: 81px;
        }
        .style4
        {
            width: 81px;
            height: 19px;
        }
        .style5
        {
            height: 19px;
        }
        .style6
        {
            width: 540px;
            height: 19px;
        }
        .style7
        {
            width: 540px;
        }
        .style8
        {
            width: 540px;
            height: 26px;
        }
        .style9
        {
            width: 81px;
            height: 26px;
        }
        .style10
        {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <table class="style1">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;<table style="width: 100%;">
                                <tr>
                                    <td class="style6">
                                        Desde
                                        <asp:Label ID="lbldesde" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                                        &nbsp; hasta
                                        <asp:Label ID="lblhasta" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                                    </td>
                                    <td class="style4">
                                        <b>Total Pares</b>
                                    </td>
                                    <td align="right" class="style5">
                                        <asp:Label ID="lbltp" runat="server" Font-Bold="True" Font-Size="Small" 
                                            ForeColor="#003366" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style8">
                                       <asp:Label ID="lblasesor" runat="server" Font-Bold="True" Font-Italic="False" 
                                            Font-Size="Medium" ForeColor="#003399" Text="Label"></asp:Label>
                                    </td>
                                    <td class="style9">
                                        <b>Total Ventas</b></td>
                                    <td align="right" class="style10">
                                        <asp:Label ID="lbltv" runat="server" Font-Bold="True" Font-Size="Small" 
                                            ForeColor="#003366" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style7">
                                        <asp:Label ID="lbllider" runat="server" Font-Bold="True" Font-Italic="False" 
                                            Font-Size="Medium" ForeColor="#003399" Text="Label"></asp:Label></td>
                                    <td class="style3">
                                        <b>Total Comision</b>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbltc" runat="server" Font-Bold="True" Font-Size="Small" 
                                            ForeColor="#003366" Text="Label"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td rowspan="2">
                            <asp:GridView ID="GridFunctions" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="False"
                                ForeColor="#333333" GridLines="None" Font-Size="Small" 
                                onpageindexchanging="GridFunctions_PageIndexChanging" 
                                onrowdatabound="GridFunctions_RowDataBound" PageSize="8" ShowFooter="True" 
                                Width="727px" Height="16px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Cliente">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" 
                                                Text='<%# Bind("Cliente") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Cliente") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Pares">
                                        <FooterTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lbltpares" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Label ID="lblpares" runat="server" 
                                                            Text='<%# Bind("[Total Pares]", "{0:N0}") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Venta Total">
                                        <FooterTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lbltotalg" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("[Total Pares]") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lbltotal" runat="server" 
                                                            Text='<%# Bind("[Venta Total]", "{0:N4}") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Comision Lider">
                                        <FooterTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="right">
                                                        <asp:Label ID="lblcomisiong" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <table style="width:100%;">
                                                <tr>
                                                    <td align="right" class="style2">
                                                        <asp:Label ID="lblcomision" runat="server" 
                                                            Text='<%# Bind("[Comision Lider]", "{0:N4}") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox2" runat="server" 
                                                Text='<%# Bind("[Venta Total]") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EditRowStyle BackColor="#999999" />
                                <EmptyDataTemplate>
                                    No hay funciones asignAQUARELLAs.
                                </EmptyDataTemplate>
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>

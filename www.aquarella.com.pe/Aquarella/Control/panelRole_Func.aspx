<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="panelRole_Func.aspx.cs"
    StylesheetTheme="SiteTheme" Inherits="www.aquarella.com.pe.Aquarella.Control.panelRole_Func" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <table class="style1">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblInfo" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
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
                            <asp:DropDownList ID="DDListFunctions" runat="server">
                            </asp:DropDownList>
                            
                        </td>
                        <td>
                            <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" 
                                onclick="btnAdicionar_Click" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td colspan="2" rowspan="2">
                            <asp:GridView ID="GridFunctions" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                OnRowCommand="GridFunctions_RowCommand" Width="350px" 
                                onpageindexchanging="GridFunctions_PageIndexChanging">
                                <EmptyDataTemplate>
                                    No hay funciones asignAQUARELLAs.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="fun_id" HeaderText="ID" />
                                    <asp:BoundField DataField="fun_nombre" HeaderText="Nombre" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID='eliminar' Text="Eliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("FUN_ID") %>'
                                                OnClientClick="return confirm('¿Desea eliminar la Función?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
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

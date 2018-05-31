<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="panelArticulo_App.aspx.cs"
    StylesheetTheme="SiteTheme" Inherits="www.bata.aquarella.com.pe.Aquarella.Maestros.panelArticulo_App" %>

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
                <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix" style="font-size:10px">
                    <span class="ui-dialog-title" id="ui-dialog-title-dialog">
                        <br/>
                       Articulos del premio  : "<asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>"
                        <br/>
                    </span>

                </div>

                <table>
                  <tr>
                        
                        <td colspan="2" rowspan="2">
                            <asp:GridView ID="GridArticulos" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="true"
                                Width="100%" OnPageIndexChanging="GridArticulos_PageIndexChanging">
                                <EmptyDataTemplate>
                                    No hay Articulos asociados.
                                </EmptyDataTemplate>
                            </asp:GridView>
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

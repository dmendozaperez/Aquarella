<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="panelProm_App.aspx.cs"
    StylesheetTheme="SiteTheme" Inherits="www.bata.aquarella.com.pe.Aquarella.Maestros.panelProm_App" %>

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
                        Agregar Articulos a la Promoción : "<asp:Label ID="lblTitulo" runat="server" Text="Label"></asp:Label>"
                        <br/>
                    </span>

                </div>

                <table>
                    
                 <tr>
                    <td style="color:White;background-color:#2191c0;font-weight:bold;width:200px;text-align:center"  >
                       <label for="DDMarca" class="f12">
                            *Marca:
                        </label>
                     </td>
                     <td style="vertical-align:middle">
                         <br/>
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList AccessKey="o" ID="DDMarca" Width="400px"  runat="server" DataValueField="Mar_Id"
                                    DataTextField="Mar_Descripcion" AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="DDMarca_SelectedIndexChanged">
                                    <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </ContentTemplate>
                            <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="DDMarca" 
                                    EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                  
                </tr>
                    

                  <tr>
                    <td style="color:White;background-color:#2191c0;font-weight:bold;width:200px;text-align:center"  > 
                       <label for="dwArticulo" class="f12">
                            *Articulo:</label>
                      </td>
                    <td style="vertical-align:middle">
                             <br/>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtArticulo" runat="server"  OnTextChanged="txtArticulo_TextChanged" Width="390px" />
                            </ContentTemplate>
                      
                        </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           
                          
                        </td>
                       <td style="text-align:right">
                           <asp:Label ID="lblError" Width="200px" runat="server" ></asp:Label>
                           <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click" />
                        </td>
                       
                    </tr>


                    
                    <tr>
                        
                        <td colspan="2" rowspan="2">
                            <asp:GridView ID="GridArticulos" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                Width="100%" OnRowCommand="GridArticulos_RowCommand"  OnPageIndexChanging="GridArticulos_PageIndexChanging">
                                <EmptyDataTemplate>
                                    No hay Articulos asociados.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="marca"  ItemStyle-HorizontalAlign="center" HeaderText="Cod. Marca"></asp:BoundField>
                                     <asp:BoundField DataField="Mar_Descripcion" HeaderText="Marca"></asp:BoundField>
                                     <asp:BoundField DataField="articulo"  ItemStyle-HorizontalAlign="center" HeaderText="cod. Articulo"></asp:BoundField>
                                    <asp:BoundField DataField="Art_Descripcion" HeaderText="Articulo"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID='eliminar' Text="Eliminar" runat="server" CommandName="Eliminar" CommandArgument='<%# Eval("Marca_Articulo") %>'
                                                OnClientClick="return confirm('¿Desea eliminar el Articulo?');" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
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

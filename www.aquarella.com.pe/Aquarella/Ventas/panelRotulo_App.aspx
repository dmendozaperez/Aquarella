<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="panelRotulo_App.aspx.cs"
    StylesheetTheme="SiteTheme" Inherits="www.aquarella.com.pe.Aquarella.Ventas.panelRotulo_App" %>

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

        function Actualizar() {
           alert("casaaa")
          

           document.getElementById("ContentPlaceHolder1_TxtDescripcion").value = 23423;
                                    
           alert("2") 


        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
       
          
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
                            *Lider:
                        </label>
                     </td>
                     <td style="vertical-align:middle">
                         <br/>
                        <asp:TextBox ID="TxtLider" Enabled="false" runat="server"  Width="390px" />
                    </td>
                  
                </tr>
                    

                  <tr>
                    <td style="color:White;background-color:#2191c0;font-weight:bold;width:200px;text-align:center"  > 
                       <label for="dwArticulo" class="f12">
                            *Promotor:</label>
                      </td>
                    <td style="vertical-align:middle">
                             <br/>
                            <asp:TextBox ID="txtDescripcion" runat="server"  Width="390px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                          
                          
                        </td>
                       <td style="text-align:right">
                          
                           <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                        </td>
                       
                    </tr>


                    
                    <tr>
                        
                        <td colspan="2" rowspan="2">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridRotulos" runat="server" SkinID="gridviewSkin" AutoGenerateColumns="false"
                                            Width="100%" OnRowCommand="GridRotulos_RowCommand"  OnPageIndexChanging="GridRotulos_PageIndexChanging">
                                            <EmptyDataTemplate>
                                                No hay Promotores asociados.
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="Bas_Id"  ItemStyle-HorizontalAlign="center" HeaderText="Codigo"></asp:BoundField>
                                                 <asp:BoundField DataField="Descripcion" HeaderText="Promotor"></asp:BoundField>
                                  
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                            <asp:HiddenField ID="hf_descripcion" runat="server" Value='<%# Eval("Descripcion")%>' />
                                                            <asp:HiddenField ID="hf_documento" runat="server" Value='<%# Eval("Documento")%>' />
                                                      <%--  <asp:Button ID='eliminar' Text="Seleccionar" runat="server" CommandName="Seleccionar" CommandArgument='<%# Eval("Bas_Id") %>'
                                                            OnClientClick="Actualizar();" />--%>
                                                        <a href="#" onclick="Actualizar()">
                                                            <asp:Image ID="Image1" ImageUrl="~/Design/images/Botones/editOrder.png" runat="server" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                     </ContentTemplate>
                                  <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="click" />

                                </Triggers>
                           </asp:UpdatePanel>
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
          
     
    </div>
    </form>
</body>
</html>

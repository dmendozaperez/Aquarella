<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="updateprecioarticle.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Maestros.updateprecioarticle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
   
    <script type="text/javascript">
        $(document).ready(function () {
            $("input[id$='txtarticulo']").focus();         
        });

    </script>
    <script language="javascript" type="text/javascript">
        function FDecimal() {
            var key = window.event.keyCode; if (key < 45 || key > 57) { window.event.keyCode = 0; }
        }
    </script>

    <style type="text/css">
        .style2
        {
            width: 316px;
        }

.FileUpload
{
	color: navy;
	border: 1px solid navy;
	font: Verdana 10px;
	padding: 1px 4px;
	font-family: verdana,arial,sans-serif;
}

.button {
	background: #222;
	display: inline-block;
	padding: 5px 10px 6px;
	color: #FFF;
	position: relative;
	cursor:pointer;
	top: 0px;
	left: 4px;
	border-left-style: none;
	border-left-color: inherit;
	border-left-width: 0px;
	border-right-style: none;
	border-right-color: inherit;
	border-right-width: 0px;
	border-top-style: none;
	border-top-color: inherit;
	border-top-width: 0px;
}
     
        
        .style5
        {
            width: 927px;
        }
        .style6
        {
            width: 653px;
        }
        .style7
        {
            width: 456px;
        }
        .style8
        {
            width: 225px;
        }
        .style9
        {
            width: 1218px;
        }
    </style>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Actualizar Articulo - Precio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Esta ventana actualizara el precio de articulos
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
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
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="style5">
                    <table>
                        <tr>
                            <td class="style7">
                                    Tipo Precio:</td>
                                <td>
                                    <asp:DropDownList ID="dwtipo" runat="server" AppendDataBoundItems="true"                                       
                                         Width="100px" style="cursor:pointer">
                                       
                                    </asp:DropDownList>
                                </td>                
                            <td class="style7">
                               <b>Buscar por codigo</b></td>
                            
                             <td class="f12">
                                 <asp:TextBox ID="txtarticulo" runat="server"  Width="100px"></asp:TextBox>
                            </td>
                             <td>
                                 &nbsp;</td>
                             <td>
                    <asp:Button ID="btnbuscar" runat="server" Text="Buscar" onclick="btnbuscar_Click" 
                        />
                            </td>
                        </tr>
                        </table>
                </td>                   
                    <td class="style6" >
                        
                        Adjuntar archivo:<br __designer:mapid="77" /> (Unicamente extensión .XLSX)</td>
                <td valign="middle" class="style8">
                	     <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                
                <td valign="middle" class="style2">
                        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" 
                            Text="Cargar Archivo" />
                    </td>
                
            </tr>
          
        </table>
    </div>
    <asp:UpdatePanel ID="updarticulo" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvreturn" runat="server" 
                BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                CellPadding="1" Width="900px" 
                Font-Size="Small" 
                PageSize="14" AutoGenerateColumns="False" 
                PagerStyle-HorizontalAlign="Left" SkinID="gridviewSkin" Height="55px" 
                onrowcommand="gvreturn_RowCommand" ShowFooter="True" 
                onrowdatabound="gvreturn_RowDataBound">
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" 
                    CssClass="GridViewBlue-tf" />
                <Columns>
                    <asp:BoundField DataField="tipodes"  HeaderText="Tipo de Precio">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" BackColor="#B9F0C1" />
                    </asp:BoundField>
                    <asp:BoundField DataField="articulo" HeaderText="Codigo">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion">
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="precioigv" ItemStyle-Width="80px"  DataFormatString="{0:N2}" 
                        HeaderText="Precio">
                    <HeaderStyle HorizontalAlign="center" />
                    <ItemStyle HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Ingrese Precio">
                         <EditItemTemplate>
                        <asp:TextBox ID="TextBox4"  runat="server" Text='<%# Bind("precion") %>'></asp:TextBox>
                        </EditItemTemplate>
                         <FooterTemplate>
                             <table style="width: 100%;">
                                 <tr>
                                     <td align="center">
                                         Actualiza Precios<asp:ImageButton ID="imgactualizar" CommandName="actualizar" 
                                                  Visible="true" runat="server" ImageUrl="~/Design/images/Botones/b_refresh.png" ToolTip="Actualizar Precio" BorderWidth="0" />
                                     </td>
                                 </tr>
                             </table>
                         </FooterTemplate>
                        <ItemTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td align="center">
                                        <asp:TextBox ID="txtprecio" runat="server"  MaxLength="9" Height="13" style="text-align: right"  Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Temporada">
                         <EditItemTemplate>
                        <asp:TextBox ID="TextBox5"  runat="server" Text='<%# Bind("Art_Temporada") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <table style="width: 100%;">
                                <tr>
                                    <td align="center">
                                        <asp:TextBox ID="txttemporada" Text='<%# Bind("Art_Temporada") %>'  runat="server"  MaxLength="20" Height="13" style="text-align: right"  Width="60"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Borrar" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibanular" CommandName="starnular"  CommandArgument='<%# Eval("articulo")%>'
                                                  Visible="true" runat="server" ImageUrl="~/Design/images/Botones/delete_off.png" ToolTip="Borrar Articulo" BorderWidth="0" />
                      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                   
                </Columns>
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnbuscar" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="updarticulo">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btnbuscar" Enabled="false" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btnbuscar" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    CodeBehind="EditPremio.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Maestros.EditPremio"
    Theme="SiteTheme" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <title id="titulo"></title>
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            $(".iframe").colorbox({ width: "55%", height: "80%", iframe: true });
          });

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        }
        
        function CalcularTotal() {
             var sum = 0;
             var i = 0;
             var Grid_Table = document.getElementById('<%= gvStock.ClientID %>');

              $("#<%=gvStock.ClientID%> :checkbox").each(function() {
                    i += 1
                  if (this.checked){
                  
                      $('#<%=gvStock.ClientID%> tbody tr:eq(' + i + ')').each(function () {
                                                
                             var coltotal = $("td:eq(4)", this).html();
                             alert(coltotal)
                            //if (coltotal != undefined) {
                   
                            //    var totaldesc = coltotal.replace('S/', '')
                            //    var valor = parseFloat(totaldesc);
                            //    sum += valor;
                   
                            //}
               
                        });
                  }
              });
             //var valor = Math.round(sum*100)/100

             //$("[id$='txtMontoSelec']").val(valor);
         }
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "40%", height: "80%", iframe: true });
            }
        }

    </script>
</asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
          <asp:Label ID="lbltitulo" runat="server">
                                            </asp:Label>
    </asp:Content>
    <asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
          Busque Articulos para agregar al premio.
    </asp:Content>
    <asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
        </asp:ScriptManager>
        <!-- Area de errores -->
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
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <AQControl:Message ID="msnMessage" Visible="false" runat="server" />
            </ContentTemplate>
      
        </asp:UpdatePanel>
       
  
        <br />
          <div style="margin: 10px auto 0 auto;">
        <table class="tablagris" cellpadding="4" width="100%">
             <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <table cellpadding="4" cellspacing="4">
                                    <tr>
                                        <td class="f12">
                                            Código del artículo a buscar:
                            
                                        </td>
                                        <td>
                                            <table cellpadding="1" cellspacing="1">
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="upTxtItem" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="TxtItem" runat="server" AccessKey="c"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                     
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:Button ID="btConsult" runat="server" Text="Buscar"
                                                             OnClick="btConsult_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    
                </td>
            </tr>
        </table>
    </div>

     <div style="margin: 10px auto 0 auto;">
     <table width="100%">
            <tr>
                <td style="width:45%"><p class="parag f13"><b>Lista de Busqueda</b></p></td>
                <td style="width:10%"></td>
                <td style="width:45%"><p class="parag f13"><b>Lista de Articulos Agregados</b></p></td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvStock" runat="server" Width="50%" SkinID="gridviewSkin"
                            AutoGenerateColumns="False"  Font-Size="8" AllowPaging="True" AllowSorting="True" CellPadding="3"
                            ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                            OnRowCreated="gvStock_RowCreated"
                            OnPageIndexChanging="gvStock_PageIndexChanging"
                            PageSize="8">
                            
                                <Columns>
                                    <asp:TemplateField HeaderText="Foto" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhotoArticle" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <asp:BoundField DataField="Referencia" HeaderText="Referencia" ItemStyle-HorizontalAlign="center"  />
                                    <asp:BoundField DataField="Talla" HeaderText="Talla" ItemStyle-HorizontalAlign="center"  />
                                     <asp:BoundField DataField="Cantidad" HeaderText="Stock" ItemStyle-HorizontalAlign="center"  />
                                       <asp:TemplateField HeaderText="Cantidad" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                                        <ItemTemplate>
                                              <asp:TextBox id="txtCantidad"   Style="width:50px;font-size:12px" Columns="2" type="number" MaxLength="3"  Text='<%# Eval("Cantidad")%>'  runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                   <asp:TemplateField HeaderText="Agregar" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hf_ArtId" runat="server" Value='<%# Eval("Referencia")%>' />
                                            <asp:HiddenField ID="hf_Talla" runat="server" Value='<%# Eval("Talla")%>' />
                                            <asp:HiddenField ID="hf_Cantidad" runat="server" Value='<%# Eval("Cantidad")%>' />
                                             <asp:HiddenField ID="hf_Precio" runat="server" Value='<%# Eval("Precio")%>' />
                                           <asp:CheckBox id="chkSelec"  runat="server"  AutoPostBack="false"/>
                                        </ItemTemplate>
                                   </asp:TemplateField>
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
                            <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                        </Triggers>
                    
                    </asp:UpdatePanel>
             
                </td>
                <td style="text-align:center">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar >>" OnClick="btAgregar_Click" />

                </td>
                  <td  valign="top">
                   <div>
                     <asp:UpdatePanel ID="upGrid2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <asp:GridView ID="gridPremios" runat="server" Width="50%" SkinID="gridviewSkin"
                            AutoGenerateColumns="False"  Font-Size="8" AllowPaging="True" AllowSorting="True" CellPadding="3"
                                ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                            OnRowCommand="gridPremios_RowCommand" 
                                OnRowCreated="gvPremios_RowCreated"
                              OnRowDataBound="gridPremios_RowDataBound"  
                             OnPageIndexChanging="gridPremios_PageIndexChanging"
                                PageSize="8">
                              <EmptyDataTemplate>
                                    <span style="color: Maroon;align-content:center">No existe articulos asociados al premio.</span>
                              </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="Foto" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPhotoArticle" runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Referencia" HeaderText="Referencia" ItemStyle-HorizontalAlign="center"  />
                                <asp:BoundField DataField="Talla" HeaderText="Talla" ItemStyle-HorizontalAlign="center"  />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="center"  />
                                 <asp:BoundField DataField="stock" HeaderText="Stock" ItemStyle-HorizontalAlign="center"  />
                                  <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibEliminar" CommandArgument='<%# Eval("ID")%>'
                                                CommandName="Eliminar"  runat="server" ImageUrl="~/Design/images/Botones/delete_off.png"
                                                Visible="true" ToolTip="eliminar registro." BorderWidth="0" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
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
                          
                     </asp:UpdatePanel> 
					
                   </div>            
      
                    </td>
            </tr>
                  <tr>
                <td style="width:45%"></td>
                <td style="width:10%"><asp:Button ID="btnRegresarLista" runat="server" Text="Regresar al listado" OnClick="btListar_Click" /></td>
                <td style="width:45%"></td>
            </tr>
        </table>
      </div>

</asp:Content>

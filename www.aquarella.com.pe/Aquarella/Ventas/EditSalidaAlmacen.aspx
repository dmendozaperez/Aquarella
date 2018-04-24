<%@ Page Language="C#" AutoEventWireup="true"  StylesheetTheme="SiteTheme"  MasterPageFile="~/Design/Site.Master"
     CodeBehind="EditSalidaAlmacen.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.EditSalidaAlmacen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ width: "60%", height: "85%", iframe: true });
            $(".iframe2").colorbox({ width: "77%", height: "88%", iframe: true });
            $("#tabs").tabs({
                collapsible: true
            });
            $('#tabs').tabs('select', '#tab-1'); // Para seleccionar el tab 1 y que este colapsado el panel de insercion de rol 
        });

        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "60%", height: "85%", iframe: true });
                $(".iframe2").colorbox({ width: "77%", height: "88%", iframe: true });
            }
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 458px;
        }
        .style2
        {
            width: 94px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
   Salida de Despacho.
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
       
 <div style="margin: 10px auto 0 auto;">
        
         <table width="100%" class="tablagris" cellpadding="4">
        <tr>
             <td class="f12">
                Nro. Documento:
            </td>
            <td>
                    <asp:TextBox id="txtDocumento"   Enabled="false"  runat="server" />
            </td>
            <td class="f12">
                Descripcion:
            </td>
            <td>
                    <asp:TextBox id="TxtDescripcion"  Enabled="false" TextMode="multiline"   Style="font-size:12px"  Columns="50" Rows="4" runat="server" />
            </td>
           
        </tr>
       <tr>
            <td class="f12">
                Fecha Creacion:
            </td>
            <td>
                 <asp:TextBox id="TextFecha"  Enabled="false"    runat="server" />
            </td>
            <td class="f12">
               Estado:
            </td>
            <td>
                    <asp:TextBox id="txtEstado"   Enabled="false"  runat="server" />
            </td>
            
        </tr>
        </table>
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                
                <td align="left" style="border-left: solid 1px silver;">
                  
                </td>
                
                <td align="right">
                    <table>
                        <tr>
                            <td><asp:CheckBox ID="chkEstSalida" runat="server" /></td>
                            <td style="font-size:13px;" ><b>Dar salida al despacho.</b></td>
                            <td align="right" style="width:20px">
                                   <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/Botones/b_toExcel.png"
                                    onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                                    runat="server" Height="25px" Width="24px" 
                                    ToolTip="Exportar Panel de Resultados a Excel." 
                                    onclick="ibExportToExcel_Click" /> 
                             </td>
                            <td> <asp:Button ID="btGuardar" runat="server" Text="Guardar Cambios" CausesValidation="true" OnClick="btGuardar_Click" /> </td>
                             <td></td>
                             <td></td>

                        </tr>
                    </table>
                    
                    
                </td>
                <td align="left">
                 </td>
                <td align="left">
                 </td>
               
            </tr>
            <tr>
                <td class="style1">
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div style="overflow-x: hidden; overflow-y:auto; height: 378px;">
                  <asp:GridView ID="gvReturns" runat="server" AllowSorting="True" 
                        ShowFooter="True" AllowPaging="false" PageSize="12"
                        SkinID="gridviewSkin" PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                        OnPageIndexChanging="gvReturns_PageIndexChanging" 
                      
                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="1072px" 
                        AutoGenerateColumns="False">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle CssClass="GridViewBlue-tf" BackColor="#2461BF" ForeColor="White" 
                        HorizontalAlign="Center" />
                        <AlternatingRowStyle BackColor="White" />
                     <Columns>
                        <asp:BoundField DataField="asesor" HeaderText="Asesor" ItemStyle-Width="110px">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="NombreLider" HeaderText="Lider" ItemStyle-Width="110px">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Rotulo" HeaderText="Rotulo" ItemStyle-Width="110px">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="TotalPares" HeaderText="Pares" ItemStyle-Width="50px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         
                         <asp:TemplateField HeaderText="Enviado" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px">
                            <ItemTemplate>
                                <asp:HiddenField ID="hf_Descripcion" runat="server" Value='<%# Eval("Desp_Descripcion")%>' />
                                    <asp:HiddenField ID="hf_FecCrea" runat="server" Value='<%# Eval("Desp_FechaCre")%>' />
                                    <asp:HiddenField ID="hf_nroDoc" runat="server" Value='<%# Eval("Desp_NroDoc")%>' />
                                  <asp:HiddenField ID="hf_Estado" runat="server" Value='<%# Eval("estado")%>' />
                                <asp:HiddenField ID="hf_IdEstado" runat="server" Value='<%# Eval("IdEstado")%>' />
                                  <asp:HiddenField ID="hf_IdDetalle" runat="server" Value='<%# Eval("Desp_IdDetalle")%>' />
                                <asp:TextBox id="txtPares"  Style="width:50px;font-size:12px" Columns="2" type="number" MaxLength="3"  Text='<%# Eval("TotalParesEnviadoEdit")%>'  runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:BoundField DataField="Agencia" HeaderText="Agencia" ItemStyle-Width="100px">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                         <asp:BoundField DataField="Destino" HeaderText="Destino" ItemStyle-Width="100px">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                                               
                        <asp:BoundField DataField="TotalVenta" DataFormatString="{0:C}"  ItemStyle-Width="50px"
                            HeaderText="Monto">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                       
                           <asp:BoundField DataField="CobroFlete" ItemStyle-Width="50px"
                            HeaderText="Flete">
                        <HeaderStyle HorizontalAlign="center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>

                         <asp:BoundField DataField="Observacion" HeaderText="Observación" ItemStyle-Width="110px">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>

                   </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <RowStyle BackColor="#EFF3FB" />
                   <FooterStyle BackColor="#006699" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                </div>
            </ContentTemplate>
            <Triggers>
            
                <asp:AsyncPostBackTrigger ControlID="gvReturns" EventName="PageIndexChanging" />
          
            </Triggers>
        </asp:UpdatePanel>
        <br />
    </div>
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" style="border: 1px solid silver;" cellpadding="4" cellspacing="4"
            class="f-small">
            <tr>               
                <td align="center">
                    <asp:Button ID="btnList" runat="server"   
                        Text="(R)egresar a Listado"  OnClick="btnList_Click"/>                    
                </td>
            </tr>
        </table>         
    </div>
</asp:Content>


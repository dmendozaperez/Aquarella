<%@ Page Language="C#" AutoEventWireup="true"  StylesheetTheme="SiteTheme"  MasterPageFile="~/Design/Site.Master"
     CodeBehind="DespachoAlmacen.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.DespachoAlmacen" %>
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

            function updateFunction() {
             
                //$("#impFav_name").val();
                //$("#impFun_Order").val();
                //$("#impFuv_Desc").val();
           <%--     $("#<%= DDPadre2.ClientID %> option[value='" + FUN_FATHER + "']").attr('selected', 'selected');--%>
                $("#dialog").dialog({ width: 400, height: 230, modal: true, title: 'Editar ' , open: true });
                $("#dialog").dialog({ buttons: [{
                    text: "Actualizar Funcion",
                    click: function () {
                        if (Validacion())
                            updateFunctionAjax(FUN_ID);
                    }
                }]
                });
            }

        function AbrirPopup(idLider, Descripcion) {
           
            var options = 'location=1,status=1,scrollbars=1,width=600,height=500';
          
            var href='panelRotulo_App.aspx?LIDER_ID='+idLider+'&DESCRIPCION='+Descripcion;

            window.open(href, 'Proveedores', options);
        }

        $(function () {

            $('#btnOpenSupplierSearch').click(function () {
                document.getElementById("409").value = "junior";
                var porId = document.getElementById("409").value;
                alert(porId)
                var options = 'location=1,status=1,scrollbars=1,width=800,height=500';
             
                window.open('panelRotulo_App.aspx', 'Proveedores', options);

            });

        });
       
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
   Lista de despacho pendiente.
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
                <td class="style1">
                    <table>
                        <tr>
                            <td class="f12">
                                Fecha Desde</td>
                             <td>
                            </td>
                             <td>
                            </td>
                             <td class="f12">
                                 Fecha Hasta</td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="upStartDate" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDateStart" runat="server" ReadOnly="true"  AccessKey="f"></asp:TextBox>
                                     <%--   <ajaxToolkit:CalendarExtender ID="calendar" runat="server" Animated="true" 
                                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" 
                                            TargetControlID="txtDateStart" />--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                             <td>
                                            <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                Style="cursor: pointer;" />
                            </td>
                             <td>
                                            <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateStart" runat="server"
                                                ToolTip="Fecha de inicio" CssClass="error_asterisck" ErrorMessage="Dígite fecha inicial"
                                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateStart">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvDateStart" runat="server" ValidationGroup="vsConsultForm"
                                                Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateStart"
                                                Operator="DataTypeCheck" ErrorMessage="Dígite una fecha válida">*</asp:CompareValidator>
                            </td>
                             <td>
                                 <asp:UpdatePanel ID="upEndDate" runat="server">
                                     <ContentTemplate>
                                         <asp:TextBox ID="txtDateEnd" runat="server"  AccessKey="f"></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                             Animated="true" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                                             PopupButtonID="imgCalendarDe" TargetControlID="txtDateEnd" />
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                            </td>
                             <td>
                                 <asp:Image ID="imgCalendarDe" runat="server" 
                                     ImageUrl="~/Design/images/Botones/b_calendar_ico.gif" 
                                     onmouseout="this.style.background=''" 
                                     onmouseover="this.style.background='red';" Style="cursor:pointer;" />
                            </td>
                             <td>
                                 <asp:RequiredFieldValidator ID="rfvDateEnd" runat="server" 
                                     ControlToValidate="txtDateEnd" CssClass="error_asterisck" Display="Dynamic" 
                                     ErrorMessage="Dígite fecha final*" SetFocusOnError="true" ToolTip="Fecha final" 
                                     ValidationGroup="vsConsultForm">*</asp:RequiredFieldValidator>
                                 <asp:CompareValidator ID="cvDateEnd" runat="server" 
                                     ControlToValidate="txtDateEnd" CssClass="error_asterisck" 
                                     ErrorMessage="Dígite una fecha final válida" Operator="DataTypeCheck" 
                                     SetFocusOnError="true" Type="Date" ValidationGroup="vsConsultForm">*</asp:CompareValidator>
                                 <asp:CompareValidator ID="cvDateStartDateEnd" runat="server" 
                                     ControlToCompare="txtDateStart" ControlToValidate="txtDateEnd" 
                                     CssClass="error_asterisck" 
                                     ErrorMessage="Dígite una fecha final superior a la fecha inicial" 
                                     Operator="GreaterThanEqual" SetFocusOnError="true" Type="Date" 
                                     ValidationGroup="vsConsultForm">*</asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="style2"  >
                       <asp:Button ID="btConsult" runat="server" Text="Buscar" ValidatioGroup="vsConsultForm"
                        CausesValidation="true" OnClick="btConsult_Click" /> 
                </td>
                <td align="left">
                   
                 </td>
                <td align="left">
                   
                 </td>
                <td align="left">
                   
                 </td>
                <td align="left" style="border-left: solid 1px silver;">
                   <table>
                        <tr>
                            <td class="f12">
                               Descripcion:
                            </td>
                       </tr>
                        <tr>
                            <td>
                                 <%--<asp:TextBox id="TxtDescripcion"   TextMode="multiline"   Style="font-size:12px"  Columns="50" Rows="4" runat="server" />--%>
                                <asp:TextBox ID="TxtDescripcion"  TextMode="multiline"   Style="font-size:12px" MaxLength="500"   Columns="50" Rows="4" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
                
                <td align="left">
                  
                     <asp:Button ID="btGuardar" runat="server" Text="Agregar Despacho" 
                        CausesValidation="true" OnClick="btGuardar_Click" /> 
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
             
                        <asp:BoundField DataField="NombreLider" HeaderText="Lider" ItemStyle-Width="110px">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         
                         <asp:TemplateField HeaderText="Rotulo" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                            <ItemTemplate>
                                     <asp:HiddenField ID="hf_IdLider" runat="server" Value='<%# Eval("Area_Id")%>' />
                                     <asp:HiddenField ID="hf_Lider" runat="server" Value='<%# Eval("NombreLider")%>' />
                                     <asp:HiddenField ID="hf_Pares" runat="server" Value='<%# Eval("TotalPares")%>' />
                                     <asp:HiddenField ID="hf_Monto" runat="server" Value='<%# Eval("TotalVenta")%>' />
                                     <asp:HiddenField ID="hf_flete" runat="server" Value='<%# Eval("McaFlete")%>' />
                                  
                                 <textarea cols="10" rows="5" disabled  id='Rotulo_<%# Eval("Area_Id")%>' name='Rotulo_<%# Eval("Area_Id")%>'> <%# Eval("Rotulo")%></textarea>
                            </ItemTemplate>
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px">
                                <ItemTemplate>
                          
                                    <center>
                                      
                                         <a href="#" onclick="AbrirPopup('<%# Eval("Area_Id")%>','<%# Eval("NombreLider")%>')">
                                            <asp:Image ID="Image1" ImageUrl="~/Design/images/Botones/editOrder.png" runat="server" />
                                        </a>
                                       
                                    </center>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                         <asp:BoundField DataField="TotalPares" HeaderText="Pares" ItemStyle-Width="50px">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Agencia" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="110px">
                            <ItemTemplate>
                                <asp:TextBox id="txtAgencia" Text='<%# Eval("Agencia")%>' TextMode="multiline" MaxLength="500"  Columns="10" Rows="5" runat="server" />
                            </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Destino" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                            <ItemTemplate>
                                <asp:TextBox id="TxtDestino" Text='<%# Eval("Destino")%>' TextMode="multiline" MaxLength="500"  Columns="10" Rows="5" runat="server" />
                            </ItemTemplate>
                       </asp:TemplateField>
                        <asp:BoundField DataField="TotalVenta" DataFormatString="{0:C}"  ItemStyle-Width="50px"
                            HeaderText="Monto">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="Flete" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                            <ItemTemplate>
                               <asp:CheckBox id="chkFlete" runat="server"  AutoPostBack="false"/>
                            </ItemTemplate>
                       </asp:TemplateField>
                         <asp:TemplateField HeaderText="Observación" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                            <ItemTemplate>
                                <asp:TextBox id="TxtObservacion" Text='' TextMode="multiline" MaxLength="500"  Columns="10" Rows="5" runat="server" />
                            </ItemTemplate>
                       </asp:TemplateField>
                          <asp:TemplateField HeaderText="Detalle" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                            <ItemTemplate>
                                <asp:TextBox id="TxtDetalle" Text='' TextMode="multiline" MaxLength="500"  Columns="10" Rows="5" runat="server" />
                            </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Agregar" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                            <ItemTemplate>
                               <asp:CheckBox id="chkSelec" runat="server"  AutoPostBack="false"/>
                            </ItemTemplate>
                       </asp:TemplateField>

                         
                        
                      
                       
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
                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                 <asp:AsyncPostBackTrigger ControlID="btGuardar" EventName="click" />
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
                    <asp:Button ID="btnList" runat="server" Text="(R)egresar a Listado"  OnClick="btnList_Click"/> 
                     <asp:Button ID="btnEditDespacho" runat="server" Text="(R)egresar a Despacho"  OnClick="btnEdit_Click"/>                       
                </td>
            </tr>
        </table>         
    </div>
    <div id="dialog" title="Ingrese Pan de Accion" style="display:none;" >
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
                            <asp:TextBox ID="TextBox1" runat="server"  OnTextChanged="btnBuscar_Click" Width="390px" />
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
                                        <asp:AsyncPostBackTrigger ControlID="TextBox1"  EventName="TextChanged"  />
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
</asp:Content>


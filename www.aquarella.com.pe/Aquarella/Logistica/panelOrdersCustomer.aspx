<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    ValidateRequest="false" StylesheetTheme="SiteTheme" CodeBehind="panelOrdersCustomer.aspx.cs"
    Inherits="www.aquarella.com.pe.Aquarella.Logistica.panelOrdersCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btCreateLiquidation").button({
                icons: {
                    primary: "ui-icon-cart"
                },
                text: true
            });

            $("#tabs").tabs();
            $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
        }); // FIN DOC READY

        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
            }
        }

        function mostrarLiquidacion(strLiquidacion) {                     
            $("[id$='txtFlete']").val('');

            $("#dialog-confirm2").dialog({
                autoOpen: false,
                resizable: false,
                width: 400,
                height: 160,
                modal: true,
                buttons: {
                    "Cerrar": function () {
                        $(this).dialog("close");
                    }
                    
                }
            });

            $("#dialog-confirm2").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
            $('#dialog-confirm2').dialog('option', 'title', 'Liquidación del flete');
            $("#dialog-confirm2").dialog("open");
            document.getElementById('popupx2').innerHTML = 'El flete se generó en en el pedido :' + strLiquidacion
        }
               

        function SelectCheckbox(spanChk) {
            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
			  elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
        }

        function ocultarTab() {
            $('[href="#fragment-7"]').closest('li').hide();
        }

         function CalcularTotal() {
             var sum = 0;
             var i = 0;
             var Grid_Table = document.getElementById('<%= gvLiquiFlete.ClientID %>');

              $("#<%=gvLiquiFlete.ClientID%> :checkbox").each(function() {
                    i += 1
                  if (this.checked){
                  
                      $('#<%=gvLiquiFlete.ClientID%> tbody tr:eq(' + i + ')').each(function () {
                                                
                             var coltotal = $("td:eq(9)", this).html();
                         
                            if (coltotal != undefined) {
                   
                                var totaldesc = coltotal.replace('S/', '')
                                var valor = parseFloat(totaldesc);
                                sum += valor;
                   
                            }
               
                        });
                  }
              });
             var valor = Math.round(sum*100)/100

             $("[id$='txtMontoSelec']").val(valor);
         }

        function CalcularTotalChk() {
            var sum = 0;

            $('input:checkbox[name=chkValor]:checked').each(function () {
          
                var valor = parseFloat($(this).val());               
                sum += valor;
            })
            $("[id$='txtMontoSelec']").val(sum);
    
        } 

        function SelectAllCheckboxes(spanChk) {
            // Added as ASPX uses SPAN for checkbox
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?
			spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;

            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" &&
			  elm[i].id != theBox.id) {
                    //elm[i].click();
                    if (elm[i].checked != xState)
                        elm[i].click();
                    //elm[i].checked=xState;
                }
        }

        function numbersonly(e) {
            var unicode = e.charCode ? e.charCode : e.keyCode

            var valor = ($("[id$='txtFlete']").val()).trim()
            var n = valor.indexOf(".")

            if ((n >= 0 && unicode == 46) || (valor == "" && unicode == 46))
                return false

            if (unicode == 44)
                return false

            if (unicode != 8 && unicode != 44 && unicode != 46) {
                if (unicode < 48 || unicode > 57) //if not a number
                { return false } //disable key press    
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        //
        function scrollTopOfPage() {
            $('html, body').animate({ scrollTop: '0px' }, 800);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Módulo de pedidos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Creación y modificación de pedidos en borrador, creación y consulta de liquidaciones,
    consulta de facturas y devoluciones.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="99999">
    </asp:ScriptManager>
    <%-- Hidden del codigo de la bodega --%>
    <asp:HiddenField ID="hdIdWareHouse" runat="server" />
    <%-- Hidden id del area --%>
    <asp:HiddenField ID="hdArea" runat="server" />
    <!-- Area de errores -->
    <asp:UpdatePanel ID="upMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btCreateLiq" EventName="click" />
        </Triggers>
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
    <!-- SEL CLIENTE -->
    <asp:Panel ID="pnlDwCustomers" Visible="false" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
                <tr>
                    <td class="fsal f13" colspan="2">
                        Selección de cliente
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="1" cellspacing="1">
                            <tr>
                                <td class="f12" width="60%">
                                    Seleccione un cliente de la lista:<br />
                                    (Desc: Seleccione un promotor sobre el cual realizará las acciones.)
                                </td>
                                <td>
                                    <asp:DropDownList ID="dwCustomers" AutoPostBack="true" DataTextField="nombres"
                                        DataValueField="bas_id" AppendDataBoundItems="true" runat="server"
                                        ToolTip="Selecionar un cliente" Width="220px" OnSelectedIndexChanged="dwCustomers_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Text=" -- Seleccione de la Lista --"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <!-- PANEL INFO CUSTOMER-->
    <asp:Panel runat="server" ID="pnlInfoCustomer">
        <table width="100%" class="f12" cellpadding="1" cellspacing="1">
            <tr>
                <td colspan="2">
                    <asp:UpdatePanel ID="upCustInfo" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlCustInfo" runat="server" Visible="false">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 150px;">
                                            <div style="margin: 5px; padding: 5px; border: 1px solid silver;">
                                                <img src="../../Design/images/card_user.png" alt="Fotografia Cliente" title="Tarjeta de presentación" />
                                            </div>
                                        </td>
                                        <td>
                                            <table width="100%" cellpadding="2">
                                                <tr>
                                                    <td>
                                                        Número de documento :
                                                    </td>
                                                    <td width="80%">
                                                        <asp:Label ID="lblDocument" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Nombres y apellidos :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblFullName" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Dirección y teléfono :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblDirPhones" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td>
                                                        Asesor Comercial :
                                                    </td>
                                                    <td>
                                                       <b> <asp:Label ID="lblasesor" runat="server"></asp:Label></b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Lider :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblLogistica" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Ubicación :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblUbication" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        E-Mail de contacto :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblMail" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td>
                                                        Agencia :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblagencia" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Destino :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lbldestino" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Premio por ganar :
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblPremio" Font-Size="14" ForeColor="#DD4B39" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="fsal f12" colspan="2" style="padding-bottom: 8px; padding-top: 8px;">
                    Configuración de nuevas liquidaciones
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <AQControl:ConfigLiq ID="ConfigLiq" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;">
        <div>
            <div id="tabs">
                <ul>
                    <li><a href="#fragment-1"><span>Borrador de pedidos</span></a></li>
                    <li><a href="#fragment-2"><span>Historial de pedidos</span></a></li>
                    <li><a href="#fragment-3"><span>Historial de Nota de credito</span></a></li>
                    <li><a href="#fragment-4"><span>Historial de Consignaciones</span></a></li>
                    <li><a href="#fragment-5"><span>Nota de Credito (Saldos)</span></a></li>
                    <li><a href="#fragment-6"><span>Historial de Ventas (Higuereta)</span></a></li>
                    <li><a href="#fragment-7"><span>Liquidacion Flete</span></a></li>
                </ul>
                <!-- PEDIDOS EN BORRADOR -->
                <div id="fragment-1" style="min-height: 200px;">
                    <p>
                        Pedidos en estado borrador, seleccione los deseados y proceda a liquidarlos.</p>
                    <asp:UpdatePanel ID="upGridEraseOrders" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvEraseOrders" runat="server" Width="100%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False" AllowSorting="True" 
                                OnRowCommand="gvEraseOrders_RowCommand" 
                                onrowdatabound="gvEraseOrders_RowDataBound">
                                <EmptyDataTemplate>
                                    No existen pedidos en borrador
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkBoxSelectOrder" runat="server" ToolTip='<%# Eval("ped_id")%>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);"
                                                ToolTip="Seleccionar Todo" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ped_id" HeaderText="Pedido" SortExpression="ped_id"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Ped_FechaIng" />                                    
                                    <asp:BoundField DataField="Ped_Det_Cantidad" HeaderText="Unds" SortExpression="Ped_Det_Cantidad"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:C}"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>                                  
                                    <asp:BoundField DataField="Ped_Det_ComisionM" HeaderText="Ganancia" DataFormatString="{0:C}"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ped_Det_OfertaM" HeaderText="Desc." DataFormatString="{0:C}"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Igv" DataFormatString="{0:C}" HeaderText="Igv" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ped_Mto_Perc" DataFormatString="{0:C}" 
                                        HeaderText="Percepcion">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibEditOrder" CommandArgument='<%# Eval("Ped_Id") + "@" + Eval("Ped_BasId")%>'
                                                CommandName="EditOrder" OnClientClick="openDialogWait();" runat="server" ImageUrl="~/Design/images/Botones/b_edit_order.png"
                                                ToolTip="Cargar para edición." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Anular" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibanular" CommandName="starnular"  CommandArgument='<%# Eval("Ped_Id")%>'
                                                  Visible="true" runat="server" ImageUrl="~/Design/images/Botones/delete_off.png" ToolTip="Anular Pedido Borrado" BorderWidth="0" />
                      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                                </Columns>
                                <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsOrders" runat="server" SelectMethod="getTableFromDataset"
                                TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsOrders_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="dtObj" Type="Object" />
                                    <asp:Parameter DefaultValue="0" Name="posTable" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                            
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <!-- FACTURAS Y LIQUIDACIONES -->
                <div id="fragment-2" style="min-height: 200px;">
                    <p>
                        Historial de liquidaciones y estados; consulta de reportes de liquidacion y facturación
                        general e individual por promotor.</p>
                    <asp:UpdatePanel ID="upLiquidations" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvLiquidations" runat="server" Width="100%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" 
                                OnRowCreated="gvLiquidations_RowCreated" 
                                onrowcommand="gvLiquidations_RowCommand" 
                                onrowdatabound="gvLiquidations_RowDataBound">
                                <EmptyDataTemplate>
                                    No existen liquidaciones que mostrar.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Liq_Id" SortExpression="Liq_Id"
                                        HeaderText="Pedido" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ven_Id" SortExpression="Ven_Id" HeaderText="Factura" />
                                    <asp:BoundField DataField="Fecha" SortExpression="Liq_FechaIng" HeaderText="Fecha" />
                                    <asp:BoundField DataField="Liq_Det_Cantidad" HeaderText="Pares" SortExpression="Liq_Det_Cantidad" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Est_Descripcion" SortExpression="Est_Descripcion" HeaderText="Estado" />
                                    <asp:BoundField DataField="descuento" SortExpression="descuento" HeaderText="Descuento"
                                        DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" Visible="False">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Liq_Det_Comision" HeaderText="Ganancia" SortExpression="ganancia"
                                        DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubTotal" DataFormatString="{0:C}" SortExpression="SubTotal"
                                        HeaderText="Sub-Total" ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PagoNcSf" DataFormatString="{0:C}" 
                                        HeaderText="N.C y/o S.F">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="total" DataFormatString="{0:C}" HeaderText="Total">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="percepcion" DataFormatString="{0:C}" 
                                        HeaderText="Percepcion">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tpagar" DataFormatString="{0:C}" 
                                        HeaderText="T.Pagar">
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Liq" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a class='iframe' href='panelLiqReports.aspx?noliq=<%#Eval("Liq_Id")%>'
                                                title="Historial De Liquidaciones (LIQ. NO.<%# Eval("Liq_Id")%> ) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                                <img src="../../Design/images/b_print.png" border="0" alt="Ver/Imprimir Liquidación No.<%# Eval("Liq_Id")%>" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fac" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a class='iframe' href='../Ventas/panelInvReports.aspx?noliq=<%# Eval("Liq_Id")%>&NoInvo=<%# Eval("Ven_Id")%>'
                                                title="Ver / Imprimir reporte de factura  <%# Eval("Ven_Id")%>.<b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                                <asp:Image ID="imgInv" Visible="false" ImageUrl="../../Design/images/b_print.png"
                                                    runat="server" AlternateText="Fact" ToolTip="Ver/Imprimir Factura" BorderWidth="0" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Edit." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgedit" CommandArgument='<%# Eval("Liq_Id") + "@" + Eval("Liq_BasId") + "@" + Eval("Liq_EstId") + "@" + Eval("Liq_PedId") + "@" + Eval("pagocredito")%>'
                                                CommandName="EditOrder" OnClientClick="openDialogWait();" runat="server" ImageUrl="~/Design/images/Botones/b_edit_order.png"
                                                Visible="false" ToolTip="Cargar para edición." BorderWidth="0" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Anular" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibanular" CommandName="starnular"  CommandArgument='<%# Eval("Liq_Id")%>'
                                                  Visible="false" runat="server" ImageUrl="~/Design/images/Botones/delete_off.png" ToolTip="Anular Liquidacion" BorderWidth="0" />
                      
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="pagocredito" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpagoc" runat="server" Text='<%# Bind("pagocredito") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("pagocredito") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsLiquidations" runat="server" SelectMethod="getTableFromDataset"
                                TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsLiquidations_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="dtObj" Type="Object" />
                                    <asp:Parameter DefaultValue="1" Name="posTable" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                             <asp:AsyncPostBackTrigger ControlID="btGuardar" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <!-- HISTORIAL DE DEVOLUCIONES -->
                <div id="fragment-3" style="min-height: 200px;">
                    <p>
                        Historial de notas crédito o devoluciones realizAQUARELLAs.</p>
                    <asp:UpdatePanel ID="upReturns" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvReturns" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" AllowSorting="True" SkinID="gridviewSkin">
                                <EmptyDataTemplate>
                                    No existen notas crédito o devoluciones que mostrar.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Not_Numero" HeaderText="Nro.Nota Credito" 
                                        SortExpression="numnc" />
                                    <asp:BoundField DataField="Not_Fecha" HeaderText="Fecha" SortExpression="Not_Fecha" />
                                    <asp:BoundField DataField="Not_Det_Cantidad" HeaderText="Unidades" SortExpression="Not_Det_Cantidad"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Total" HeaderText="Valor" DataFormatString="{0:C}"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Devol" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a class='iframe' href='../../Reports/Ventas/reportArticlesReturned.aspx?noReturn=<%# Eval("Not_Id")%>&ShowReportOnWeb=true'
                                                title="Ver / Imprimir reporte de devolucion  <%# Eval("Not_Id")%>.<b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                                <img src="../../Design/images/b_print.png" border="0" alt="Ver/Imprimir reporte de devolución No.<%# Eval("Not_Id")%>" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsReturns" runat="server" SelectMethod="getTableFromDataset"
                                TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsReturns_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="dtObj" Type="Object" />
                                    <asp:Parameter DefaultValue="2" Name="posTable" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                 <!-- CONSIGNACIONES -->
                <div id="fragment-4" style="min-height: 200px;">
                    <p>
                        Historial de Consignaciones
                        general e individual por promotor.</p>
                    <asp:UpdatePanel ID="upconsignacion" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvconsignacion" runat="server" Width="100%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" >
                                <EmptyDataTemplate>
                                    No existen consignaciones que mostrar.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Ban_Descripcion" SortExpression="Pago"
                                        HeaderText="Pago" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pag_Num_Consignacion" SortExpression="OP" HeaderText="OP" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pag_Monto" HeaderText="Monto" SortExpression="valor" 
                                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:C2}">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pag_Num_ConsFecha" SortExpression="Fecha" HeaderText="Fecha" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsconsignacion" runat="server" SelectMethod="getTableFromDataset"
                                TypeName="www.aquarella.com.pe.bll.Util.Utilities" 
                                OnSelecting="odsconsignacion_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="dtObj" Type="Object" />
                                    <asp:Parameter DefaultValue="3" Name="posTable" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                       <!-- SALDOS A FAVOR -->
                <div id="fragment-5" style="min-height: 200px;">
                    <p>
                        Saldos A Favor
                        general e individual por promotor.</p>
                    <asp:UpdatePanel ID="upsf" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvsf" runat="server" Width="100%" SkinID="gridviewSkin" 
                                AllowPaging="True" AllowSorting="True" >
                                <Columns>
                                    <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripcion">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MONTO" DataFormatString="{0:C2}" HeaderText="Monto">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="percepcion" DataFormatString="{0:C2}" 
                                        HeaderText="Percepcion">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="saldo" DataFormatString="{0:C2}" 
                                        HeaderText="Saldo">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No existen saldo a favor que mostrar.
                                </EmptyDataTemplate>
                                <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsfavor" runat="server" SelectMethod="getTableFromDataset"
                                TypeName="www.aquarella.com.pe.bll.Util.Utilities" 
                                OnSelecting="odsfavor_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="dtObj" Type="Object" />
                                    <asp:Parameter DefaultValue="4" Name="posTable" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <!--HISTORIAL DE VENTA HIGUERETA-->
                 <div id="fragment-6" style="min-height: 200px;">
                    <p>
                        Historial de Ventas de Higuereta; consulta de facturación
                        general e individual por promotor.</p>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvventa" runat="server" Width="100%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" 
                                OnRowCreated="gvventa_RowCreated" 
                                 onrowcommand="gvventa_RowCommand" >
                                <EmptyDataTemplate>
                                    No existen Facturas que mostrar.
                                </EmptyDataTemplate>
                                <Columns>                                    
                                    <asp:BoundField DataField="Ven_Id" SortExpression="Ven_Id" HeaderText="Factura" >
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Fecha" SortExpression="Fecha" HeaderText="Fecha" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Ven_Det_Cantidad" HeaderText="Pares" SortExpression="Ven_Det_Cantidad" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Est_Descripcion" SortExpression="Est_Descripcion" HeaderText="Estado" >                                                                                                
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="total" DataFormatString="{0:C}" HeaderText="Total">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="percepcion" DataFormatString="{0:C}" 
                                        HeaderText="Percepcion">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tpagar" DataFormatString="{0:C}" 
                                        HeaderText="T.Pagar">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>                                  
                                  <%--  <asp:TemplateField HeaderText="Fac" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a class='iframe' href='../Ventas/panelInvReports.aspx?noliq=H&NoInvo=<%# Eval("Ven_Id")%>'
                                                title="Ver / Imprimir reporte de factura  <%# Eval("Ven_Id")%>.<b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                                <asp:Image ID="imgInv" Visible="false" ImageUrl="../../Design/images/b_print.png"
                                                    runat="server" AlternateText="Fact" ToolTip="Ver/Imprimir Factura" BorderWidth="0" /></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>   --%>
                                      <asp:TemplateField HeaderText="Fac" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                 <a class='iframe' href='../../Reports/Ventas/reportTickets.aspx?noventa=<%# Eval("Ven_Id")%>'
                                    title="Ver / Imprimir factura  <%# Eval("Ven_Id")%>.<b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                    <img src="../../Design/images/b_print.png" border="0" alt="Ver reporte de facturacion No.<%# Eval("Ven_Id")%>" /></a>                                                                  
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>                                                                                                       
                                </Columns>
                                <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="odsventa" runat="server" SelectMethod="getTableFromDataset"
                                TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsventa_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="dtObj" Type="Object" />
                                    <asp:Parameter DefaultValue="5" Name="posTable" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />                           
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
               
                <div id="fragment-7" style="min-height: 200px;">
                   
                    <table width="100%" class="tablagris" cellpadding="4">
               
                        <tr>
                            <td>
                                <table width="100%" cellpadding="1" cellspacing="1">
                                    <tr>
                                        <td class="auto-style1">
                                                             
                                        </td>
                                        <td class="auto-style2">
                                   
                                        </td>
                                         <td style="text-align:right">
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
                                                                <asp:TextBox ID="txtDateStart" runat="server" AccessKey="f"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="calendar" runat="server" Animated="true" 
                                                                    FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" 
                                                                    TargetControlID="txtDateStart" />
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
                                                                 <asp:TextBox ID="txtDateEnd" runat="server" AccessKey="f"></asp:TextBox>
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
                                                             onmouseover="this.style.background='red';" Style="cursor: pointer;" />
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
                                
                                        <td>
                                            <asp:Button ID="btConsult" runat="server" Text="Buscar" OnClick="btConsult_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                   </table>
                      <div style="overflow-x: hidden; overflow-y:auto; height: 378px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvLiquiFlete" runat="server" Width="100%" SkinID="gridviewSkin"
                                    AutoGenerateColumns="False"                                
                                  AllowSorting="True" ShowFooter="True" AllowPaging="false" PageSize="12"
                                PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                                
                                   CellPadding="4" ForeColor="#333333" GridLines="None" >
                                    <EmptyDataTemplate>
                                        No existen liquidaciones que mostrar.
                                    </EmptyDataTemplate>
                                    <Columns>
                                    
                                        <asp:BoundField DataField="Liq_Id" SortExpression="Liq_Id"
                                            HeaderText="Pedido" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>                 
                                        <asp:BoundField DataField="Fecha" SortExpression="Liq_FechaIng" HeaderText="Fecha" />
                                        <asp:BoundField DataField="Liq_Det_Cantidad" HeaderText="Pares" SortExpression="Liq_Det_Cantidad" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Est_Descripcion" SortExpression="Est_Descripcion" HeaderText="Estado" />
                                        <asp:BoundField DataField="descuento" SortExpression="descuento" HeaderText="Descuento"
                                            DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" Visible="False">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Liq_Det_Comision" HeaderText="Ganancia" SortExpression="ganancia"
                                            DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SubTotal" DataFormatString="{0:C}" SortExpression="SubTotal"
                                            HeaderText="Sub-Total" ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PagoNcSf" DataFormatString="{0:C}" 
                                            HeaderText="N.C y/o S.F">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="total" DataFormatString="{0:C}" HeaderText="Total">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="percepcion" DataFormatString="{0:C}" 
                                            HeaderText="Percepcion">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tpagar" DataFormatString="{0:C}" 
                                            HeaderText="T.Pagar">
                                        <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Liq" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                 <asp:HiddenField ID="hf_Liq_Id" runat="server" Value='<%# Eval("Liq_Id")%>' />
                                                <asp:HiddenField ID="hf_monto" runat="server" Value='<%# Eval("tpagar")%>' />
                                                <a class='iframe' href='panelLiqReports.aspx?noliq=<%#Eval("Liq_Id")%>'
                                                    title="Historial De Liquidaciones (LIQ. NO.<%# Eval("Liq_Id")%> ) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                                    <img src="../../Design/images/b_print.png" border="0" alt="Ver/Imprimir Liquidación No.<%# Eval("Liq_Id")%>" /></a>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                   
                                         <asp:TemplateField HeaderText="Agregar" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="110px">
                                            <ItemTemplate>
                                                 <%--<input name="chkValor" type="checkbox" class="checkbox" onclick="javascript: CalcularTotalChk(this);" id="tamanoAjo_0" value='<%# Eval("tpagar")%>' />--%>
                                               <asp:CheckBox id="chkSelec" Checked="True" runat="server" onclick="javascript: CalcularTotal(this);" value='<%# Eval("tpagar")%>'  AutoPostBack="false"/>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                    </Columns>
                                    <RowStyle BorderColor="#DEDFDE" BorderWidth="1px" BorderStyle="Solid" />
                                </asp:GridView>
                         
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                                    <asp:AsyncPostBackTrigger ControlID="btGuardar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                </div>
                    <div  style="margin: 0 0 0 70%;">
                        <table>
                            <tr>
                                <td class="f8">
                                   Total Seleccionado:
                                </td>  
                                <td>
                                    <asp:TextBox id="txtMontoSelec"   style="text-align:right" Enabled="false"  runat="server" />                                  
                                </td>
            
                            </tr>
                            <tr>
                                 <td class="f8">
                                   Monto Flete:
                                </td>
                                <td>
                                     <asp:TextBox id="txtFlete" style="text-align:right"  onkeypress="return numbersonly(event);"  runat="server" />
                                </td>
                          </tr>
                            <tr>
                                 <td class="f8">
                                  
                                </td>
                                <td align="center">
                                    <asp:Button ID="btGuardar" runat="server"  Text="Liquidar Flete" 
                                        CausesValidation="true" OnClick="btGuardarFlete_Click" /> 
                                </td>
                          </tr>
                    </table>                         

                    </div>

                </div>
            </div>
        </div>
    </div>
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" style="border: 1px solid silver;" cellpadding="4" cellspacing="4"
            class="f-small">
            <tr>
                <td align="center">
                    <asp:Button AccessKey="c" ID="Button2" runat="server" Text="(C)rear nuevo pedido borrador"
                        ToolTip="Cree un nuevo pedido borrador" OnClick="Button2_Click" />
                </td>
                <td align="center">
                    <asp:Button ID="btCreateLiq" runat="server"   OnClick="btCreateLiq_Click" 
                        Text="(L)iquidar pedidos seleccionados" style="display:none;"/>
                    <button type="button" value=" (L)iquidar pedidos seleccionados"  id="btCreateLiquidation"
                        title="Proceda a liquidar todos los pedidos borrador seleccionados">
                        (L)iquidar pedidos seleccionados</button>
                </td>
            </tr>
        </table>
         <asp:HiddenField ID="hdestado" Value="0" runat="server" />
    </div>
    <!-- SHIPPING FORM -->
    <AQControl:ShippingForm runat="server" Visible="true" ID="ShippForm" />
    <asp:HiddenField ID="h_numTipPago"   Value="0" runat="server" />
    <asp:HiddenField ID="h_numConfigPagoPOS"  Value="0" runat="server" />
    <div id="dialog-confirm2" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
       <div id="popupx2"></div>

    </p>
    </div>
</asp:Content>

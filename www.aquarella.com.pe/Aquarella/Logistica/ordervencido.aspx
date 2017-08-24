<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="ordervencido.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.ordervencido" %>
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
    </script>
    <script type="text/javascript" language="javascript">
        //
        function scrollTopOfPage() {
            $('html, body').animate({ scrollTop: '0px' }, 800);
        }
    </script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Restauracion de liquidaciones vencidos 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Te permite restaurar liquidaciones vencidos a 2 dias mas 
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
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
                                    (Desc: Seleccione un promotor sobre el cual realizará la restauracion de pedido.)
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
     <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvvencido" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    Font-Size="Small"
                    AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None" Width="1000px" 
                    AllowPaging="True" onrowdatabound="gvvencido_RowDataBound" 
                    onrowcommand="gvvencido_RowCommand">
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="noliquid" HeaderText="No.Liquid." SortExpression="noliquid"
                            ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cantidad" HeaderText="Articulos (Cant.)" 
                            SortExpression="cantidad">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado" >
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecaex" HeaderText="Fecha Exp." SortExpression="fecaex"
                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:d}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="true" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibrestaurar" CommandName="strestaurar"  CommandArgument='<%# Eval("noliquid")%>'
                                                runat="server" ImageUrl="~/Design/images/b_active.png" 
                                                ToolTip="Restaurar liquidacion vencido a 2 dias mas "/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle HorizontalAlign="Right" BackColor="#5D7B9D" Font-Bold="True" 
                        ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:ObjectDataSource ID="odsvencido" runat="server" 
                    OnSelected="odsvencido_Selected" SelectMethod="getpedidosvencidos" 
                    TypeName="www.aquarella.com.pe.bll.Liquidations_Hdr">
                    <SelectParameters>
                        <asp:Parameter Name="var_lhn_customer" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dwCustomers" 
                  EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <!-- ACTIVITY PANEL -->
       <!-- <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="dwCustomers" Enabled="true" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="dwCustomers" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>-->
    </div>
    <AQControl:ShippingForm runat="server" Visible="true" ID="ShippForm" />
</asp:Content>

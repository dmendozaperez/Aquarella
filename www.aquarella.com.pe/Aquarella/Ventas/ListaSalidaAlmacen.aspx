<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="ListaSalidaAlmacen.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.ListaSalidaAlmacen" StylesheetTheme="SiteTheme"  %>
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
    <style type="text/css">
        .auto-style1 {
            width: 16%;
        }
        .auto-style2 {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Módulo de Salida Despacho.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="99999">
    </asp:ScriptManager>
    <%-- Hidden del codigo de la bodega --%>
    <asp:HiddenField ID="hdIdWareHouse" runat="server" />
    <%-- Hidden id del area --%>
    <asp:HiddenField ID="hdArea" runat="server" />
    <!-- Area de errores -->
 <%--   <asp:UpdatePanel ID="upMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
        <Triggers>          
            
            <asp:AsyncPostBackTrigger ControlID="btnbuscar" EventName="btConsult_Click" />
        </Triggers>
    </asp:UpdatePanel>--%>
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
   <asp:Panel ID="pnlDwCustomers" Visible="true" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
               
                <tr>
                    <td>
                        <table width="100%" cellpadding="1" cellspacing="1">
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
                                <td class="auto-style1">
                                    Ingrese el numero de Documento:<br />                                    
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtdespacho" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btConsult" runat="server" Text="Buscar" OnClick="btConsult_Click" />
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
        <div>
            <div id="tabs">
                <ul>                    
                    <li><a href="#fragment-1"><span>Lista de Despacho.</span></a></li>                    
                </ul>                
                <!-- MANIFIESTO -->
                <div id="fragment-1" style="min-height: 200px;">
                  <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvReturns" runat="server" Width="98%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False"  Font-Size="8" AllowPaging="True" AllowSorting="True" CellPadding="3"
                                 ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
                                OnRowCommand="gvReturns_RowCommand" 
                                 PageSize="8">
                            
                                <Columns>
                                    <asp:BoundField DataField="Desp_NroDoc"
                                        HeaderText="N° de Despacho" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Desp_Descripcion" HeaderText="Descripcion" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Desp_FechaCre" HeaderText="Fecha" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                     <asp:BoundField DataField="estado" HeaderText="Estado" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                       <asp:TemplateField HeaderText="Edit." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgedit" CommandArgument='<%# Eval("Desp_id")%>'
                                                CommandName="EditOrder" OnClientClick="openDialogWait();" runat="server" ImageUrl="~/Design/images/Botones/b_edit_order.png"
                                                Visible="true" ToolTip="Cargar para edición." BorderWidth="0" />
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
                        <Triggers>                        
             
		                   <asp:AsyncPostBackTrigger ControlID="gvReturns" EventName="PageIndexChanging" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>                
            </div>
        </div>
    </div>
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" style="border: 1px solid silver;" cellpadding="4" cellspacing="4"
            class="f-small">
            <tr>               
                <td align="center">
                    <asp:Button ID="btncrear" runat="server"   
                        Text="(C)rear Nuevo Despacho"  OnClick="btncrear_Click"/>                    
                </td>
            </tr>
        </table>         
    </div>
    <AQControl:ShippingForm runat="server" Visible="true" ID="ShippForm" />
</asp:Content>

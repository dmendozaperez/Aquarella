<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="PanelManifiesto.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.PanelManifiesto" StylesheetTheme="SiteTheme"  %>
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
    Módulo de Manifiesto
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Creación y modificación de Manifiesto, creación y consulta de manifiesto,    
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
            <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnbuscar" EventName="Click" />
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
   <asp:Panel ID="pnlDwCustomers" Visible="true" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
                <tr>
                    <td class="fsal f13" colspan="2">
                        Buscar Manifiesto
                    </td>
                </tr>
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
                                    Ingrese el numero de manifiesto:<br />                                    
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtmanifiesto" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnbuscar" runat="server" Text="Buscar" OnClick="btnbuscar_Click" />
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
                    <li><a href="#fragment-1"><span>Historial de Manifiesto</span></a></li>                    
                </ul>                
                <!-- MANIFIESTO -->
                <div id="fragment-1" style="min-height: 200px;">
                    <p>
                        Historial de manifiesto; consulta de reportes de manifiesto.</p>
                    <asp:UpdatePanel ID="upmanifiesto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvmanifiesto" runat="server" Width="98%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False"  Font-Size="8" AllowPaging="True" AllowSorting="True" CellPadding="3" ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" OnRowCommand="gvmanifiesto_RowCommand" OnRowDataBound="gvmanifiesto_RowDataBound" OnPageIndexChanging="gvmanifiesto_PageIndexChanging" PageSize="8" OnRowCreated="gvmanifiesto_RowCreated" >
                              <%--  <EmptyDataTemplate>
                                    No existen manifiesto que mostrar.
                                </EmptyDataTemplate>--%>
                                <Columns>
                                    <asp:BoundField DataField="IdManifiesto"
                                        HeaderText="N° de Manifiesto" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fecha_manifiesto" HeaderText="Fecha de Manifiesto" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Est_Descripcion" HeaderText="Estado" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                      <asp:TemplateField HeaderText="Man." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <a class='iframe' href='panelManReports.aspx?noman=<%#Eval("IdManifiesto")%>'
                                                title="Historial De Manifiesto (Manifiesto. NO.<%# Eval("IdManifiesto")%> ) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                                 <asp:Image ID="imgInv" Visible="false" ImageUrl="../../Design/images/b_print.png"
                                                    runat="server" AlternateText="Fact" ToolTip="Ver/Imprimir Manifiesto" BorderWidth="0" />
                                               <%-- <img src="../../Design/images/b_print.png" border="0" alt="Ver/Imprimir Manifiesto No.<%# Eval("IdManifiesto")%>" /></a>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Edit." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgedit" CommandArgument='<%# Eval("IdManifiesto")%>'
                                                CommandName="EditOrder" OnClientClick="openDialogWait();" runat="server" ImageUrl="~/Design/images/Botones/b_edit_order.png"
                                                Visible="false" ToolTip="Cargar para edición." BorderWidth="0" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Anular" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibanular" CommandName="starnular"  CommandArgument='<%# Eval("IdManifiesto")%>'
                                                  Visible="false" runat="server" ImageUrl="~/Design/images/Botones/delete_off.png" ToolTip="eliminar registro" BorderWidth="0" />
                      
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
                           <%-- <asp:AsyncPostBackTrigger ControlID="btnagregar" EventName="Click" />        --%>                  
                            <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="PageIndexChanging" />
                            <%--<asp:AsyncPostBackTrigger ControlID="btcrearmanifiesto" EventName="Click" />--%>
                            <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="RowCommand" />
                           <%-- <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="RowCreated" />--%>
                            <asp:AsyncPostBackTrigger ControlID="btnbuscar" EventName="Click" />
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
                    <asp:Button ID="btncrearman" runat="server"   
                        Text="(C)rear Nuevo Manifiesto"  OnClick="btncrearman_Click"/>                    
                </td>
            </tr>
        </table>         
    </div>
    <AQControl:ShippingForm runat="server" Visible="true" ID="ShippForm" />
</asp:Content>

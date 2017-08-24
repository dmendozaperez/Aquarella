<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Design/Site.Master"  
    CodeBehind="expVentasAQ.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Interface.expVentasAQ" %>


<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">   
<script type="text/javascript">
    var ibExportDoc, txtDateStart, txtDateEnd, allFields, tips, msnMessage;
    </script> 
    <script type="text/javascript" language="javascript">
        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        $(document).ready(function () {
            logica();
        });

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                $("input:submit,button,ImageButton").button();
                logica();
            }
        }
     </script>
    <script type="text/javascript" language="javascript">
        function logica() {

            ibExportDoc = $("input[id$='ibExportDoc']");
            txtDateStart = $("input[id$='txtDateStart']");
            txtDateEnd = $("input[id$='txtDateEnd']");
            allFields = $([]).add(txtDateStart).add(txtDateEnd);
            tips = $("#validateTips");

            ibExportDoc.click(function () {
                var bValid = true;
                tips.text("");
                //allFieldsBd.removeClass("ui-state-error");

                bValid = bValid && checkRegexp(txtDateStart, /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/, " Dígite fechas validas (dd/mm/yyyy).");
                bValid = bValid && checkRegexp(txtDateEnd, /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/, " Dígite fechas validas (dd/mm/yyyy).");

                scrollTop();
                return bValid;
            });
        }

        function updateTips(t) {
            tips
			.text(tips.text() + t)
			.addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }
        function checkRegexp(o, regexp, n) {
            if (!(regexp.test(o.val()))) {
                o.addClass("ui-state-error");
                updateTips(n);
                return false;
            } else {
                return true;
            }
        }

        function scrollTop() {
            $('html, body').animate({ scrollTop: '0px' }, 800);
        }
   </script>
    <style type="text/css">
        .style1
        {
            width: 100px;
        }
        .style2
        {
            width: 129px;
        }
        .style3
        {
        	 width: 500px;
        }
        .style4
        {
            width: 168px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Ventas de Aquarella
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p id="validateTips">
    </p>
    <asp:ScriptManager ID="ScriptManager1" runat="server" 
        AsyncPostBackTimeout="99999" EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <asp:UpdatePanel ID="upPanelMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
        <Triggers>
           
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
    <!-- SEL CUSTOMER -->
    <asp:Panel ID="pnlDwCustomers" Visible="true" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" cellpadding="4" class="tablaGrisLetra">
                <tr>
                    <td class="style1">
                        *Fecha de venta</td>
                    <td class="style3">
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="50%" align="left">
                                    <table cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td colspan="3">
                                                Fecha de inicio:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upStartDate" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDateStart" runat="server" AccessKey="f"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="calendar" runat="server" TargetControlID="txtDateStart"
                                                            Format="dd/MM/yyyy" Animated="true" PopupButtonID="imgCalendar" FirstDayOfWeek="Monday" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="left" style="padding-left: 5px;">
                                                <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                    onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                    Style="cursor: hand;" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateStart" runat="server"
                                                    ToolTip="Fecha de inicio" CssClass="error_asterisck" ErrorMessage="Dígite fecha inicial"
                                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateStart">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvDateStart" runat="server" ValidationGroup="vsConsultForm"
                                                    Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateStart"
                                                    Operator="DataTypeCheck" ErrorMessage="Dígite una fecha válida">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <!---->
                                <td width="50%">
                                    <table cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td colspan="3">
                                                Fecha de cierre:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="upEndDate" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtDateEnd" runat="server" AccessKey="f"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateEnd"
                                                            Format="dd/MM/yyyy" Animated="true" PopupButtonID="imgCalendarDe" FirstDayOfWeek="Monday" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td align="left" style="padding-left: 5px;">
                                                <asp:Image ID="imgCalendarDe" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                    onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                    Style="cursor: hand;" />
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateEnd" runat="server"
                                                    ToolTip="Fecha final" CssClass="error_asterisck" ErrorMessage="Dígite fecha final*"
                                                    Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateEnd">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="cvDateEnd" runat="server" ValidationGroup="vsConsultForm"
                                                    Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateEnd"
                                                    Operator="DataTypeCheck" ErrorMessage="Dígite una fecha final válida">*</asp:CompareValidator>
                                                <asp:CompareValidator ID="cvDateStartDateEnd" runat="server" ValidationGroup="vsConsultForm"
                                                    Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateEnd"
                                                    ControlToCompare="txtDateStart" Operator="GreaterThanEqual" ErrorMessage="Dígite una fecha final superior a la fecha inicial">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>    
                    </td>
                    <td align="left" style="border-left: solid 1px silver;">
                        <div style="padding-left:20px">
                            <asp:ImageButton ID="ibExportDoc" ImageUrl="~/Design/images/Botones/b_doc.png" onmouseover="this.style.background='#7094DB';"
                            onmouseout="this.style.background=''" runat="server" Height="25px" Width="24px"
                            ToolTip="Exportar archivo de ventas." OnClick="ibExportDoc_Click" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
         
    </asp:Panel>
   
</asp:Content>

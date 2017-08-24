﻿<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="SiteTheme"  MasterPageFile="~/Design/Site.Master"
CodeBehind="paymentsUpdFec.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.paymentsUpdFec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
        <link href="../../Styles/theme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
        <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />

        <script src="../../jquery-1.9.1.js" type="text/javascript"></script>
	    <script src="../../ui/jquery.ui.core.js" type="text/javascript"></script>
	    <script src="../../ui/jquery.ui.widget.js" type="text/javascript"></script>
	    <script src="../../ui/jquery.ui.datepicker.js" type="text/javascript"></script>
	    <link rel="stylesheet" href="../demos.css">
      
	   <script type="text/javascript" language="javascript">
	       $(function () {
	           $("#impFuv_Desc").datepicker
               ({
                   /*showOn: 'both',*/
                   buttonImage: '../../Scripts/jquery-ui-date/development-bundle/demos/datepicker/images/b_calendar_ico.gif',
                   buttonImageOnly: true,
                   buttonText: 'Selecciona una fecha',
                   showButtonPanel: true,
                   dateFormat: 'dd/m/yy'

               });
	       });
       </script>
        
        <script type="text/javascript" language="javascript">

            function updateFunction_UPD(FUN_ID, FUV_NAME, FUV_DESCRIPTION, FUN_ORDER, FUN_FATHER) {
                removeFieldsErrors();
                $("#impFav_name").val(FUV_NAME);
                $("#impFun_Order").val(FUN_ORDER);
                $("#impFuv_Desc").val(FUV_DESCRIPTION);
                $("#<%= DDPadre2.ClientID %> option[value='" + FUN_FATHER + "']").attr('selected', 'selected');
                $("#dialog").dialog({ width: 420, height: 335, modal: true, title: 'Editar Comprobante Nro ' + FUV_NAME, open: true });
                $("#dialog").dialog({ buttons: [{
                    text: "Actualizar Comprobante",
                    click: function () {
                        if (Validacion())
                            updateFunctionAjax(FUN_ID);
                    }
                }]
                });
            }

            function updateFunctionAjax(FUN_ID) {
                $.ajax({
                    type: "POST",

                    data: "{ 'FUN_ID': '" + FUN_ID + "','FUV_NAME': '" + $("#impFav_name").val() + "','FUV_DESCRIPTION': '" + $("#impFuv_Desc").val() + "','_FUN_ORDER': '" + $("#impFun_Order").val() + "','_FUN_FATHER': '" + $("#<%= DDPadre2.ClientID %>").val() + "'}",
                    url: "paymentsUpd.aspx/ajaxUpdateFunction",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (result) {
                        if (result.d == new String("1")) {
                            $('#dialog').dialog("close");
                            $("input[id$='btRefresh']").click();

                        }
                        else {
                            alert("Ocurrio un error durante la actualizacion");
                        }
                    },
                    error: function (result) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + result); }
                });
            }

            function removeFieldsErrors() {
                $("#validateTips").text("");
                $("#impFav_name").removeClass("ui-state-error");
            }

            function Validacion() {
                var bValid = true;
                removeFieldsErrors();

                bValid = bValid && checkNull($("#impFav_name"), "*El numero del comprobante no puede estar vacio");

                return bValid;
            }
            // Validaciones
            function checkNull(field, t) {
                if (field.val() == "") {
                    field.addClass("ui-state-error")
                    updateTips(t);
                    return false;
                }
                else { return true; }
            }

        </script>
        <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
        <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
         <script type="text/javascript" language="javascript">
             $(document).ready(function () {
                 $(".iframe").colorbox({ width: "86%", height: "98%", iframe: true });
             });
             // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Editar de pagos y consignaciones 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <table width="100%">
        <tr>
            <td>
                <!-- CONTENT PLACE HOLDER -->
                <div class="d-cont">
                    <div style="font-family:Verdana; font-size:10px; font-weight:bold;">
                        <a class='iframe' href='panelPaymentList.aspx' title="Historial De Liquidaciones (LIQ. NO.) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                            Consultar historial de consignaciones</a>&nbsp;&nbsp;
                        <a class='iframe' href='panelPaymentList.aspx' title="Historial De Liquidaciones (LIQ. NO.) / Click En La <b>X</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                            <img alt="Consultar Historia de consignaciones"  style="border:0;" src="../../Design/images/lupa.jpg" />
                        </a>
                    </div>
                    <!-- -->
                    <asp:ScriptManager ID="ScriptManager1" runat="server" 
                        AsyncPostBackTimeout="99999" EnableScriptGlobalization="True">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="upHd" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdCreditValue" Value="0" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!-- Area de errores -->
                    <asp:UpdatePanel ID="upPanelMsg" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
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
                    <div style="margin: 10px auto 0 auto;">
                        <table width="100%" cellpadding="0">
                            <tr>
                                <td class="fsal f13" colspan="2">
                                    Panel de listado de consignaciones
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" cellpadding="1" cellspacing="1">
                                        <tr>
                                            <td class="f12" width="22%">
                                                Realice el rango de fecha de los resultados:<br />
                                            </td>
                                            <td align="left" width="35%">
                                               <table width="80%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="50%" align="left">
                                                            <table cellpadding="1" cellspacing="1">
                                                                <tr>
                                                                    <td class="f12" colspan="3">
                                                                        Fecha de inicio:
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:UpdatePanel ID="upStartDate" runat="server">
                                                                            <ContentTemplate>
                                                                                <asp:TextBox ID="txtDateStart" runat="server" AccessKey="f" Text=""></asp:TextBox>
                                                                                <ajaxToolkit:CalendarExtender ID="calendar"  runat="server" TargetControlID="txtDateStart"
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
                                                                    <td class="f12" colspan="3">
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
                                            <td>
                                             <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                                                CausesValidation="true" OnClick="btConsult_Click" />
                                            </td>
                                           
                                            <td>
                                                <asp:Button ID="btRefresh" runat="server" OnClick="btRefresh_Click" 
                                                    Text="Refrescar panel" />
                                            </td>
                                           
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- PANEL DE RESULTADOS -->
                    <div style="margin: 10px auto 0 auto; min-height: 200px;" class="f13">
                        <asp:UpdatePanel ID="upGridClear" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="gvPays" runat="server" SkinID="gridviewSkin" ShowFooter="true" AllowPaging="false" AllowSorting="false">
                                    <EmptyDataTemplate>
                                        Sin resultados.
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="Pag_Id" HeaderText="Pago No." ReadOnly="True"
                                            SortExpression="pagono" />
                                        <asp:BoundField DataField="Ban_Descripcion" HeaderText="Banco" SortExpression="banco">
                                            <ControlStyle CssClass="campo1" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Pag_Num_Consignacion" HeaderText="Compr. Pago" SortExpression="comprobante" />
                                        <asp:BoundField DataField="Pag_Num_ConsFecha" HeaderText="Fecha Pago" SortExpression="fecha"
                                            DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="Pag_Monto" HeaderText="Valor" SortExpression="valor"
                                            DataFormatString="{0:N}" />
                                        <asp:BoundField DataField="Est_Descripcion" HeaderText="Estado" SortExpression="estado" />
                                        <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <a href="#" onclick="updateFunction_UPD('<%# Eval("Pag_Id") %>','<%# Eval("Pag_Num_Consignacion") %>','<%# ((DateTime)Eval("Pag_Num_ConsFecha")).ToString("dd/MM/yyyy") %>','<%# Eval("Pag_Monto") %>','<%# Eval("Pag_EstId") %>')">
                                                    <asp:Image ID="Image2" style="border: 0"  ImageUrl="~/Design/images/Botones/editOrder.png" runat="server" />
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                              <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                                <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:ObjectDataSource ID="odsPays" runat="server" SelectMethod="get_Montos"
                            TypeName="www.aquarella.com.pe.bll.Payments" OnSelected="odsPays_Selected">
                            <SelectParameters>                               
                                <asp:Parameter Name="_date_start" Type="DateTime" />
                                <asp:Parameter Name="_date_end" Type="DateTime" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:Label ID="lblerror" runat="server" Text=""></asp:Label>
                    </div>
                    <!-- -->
                </div>
            </td>
        </tr>
    </table>
     <div id="dialog" class="f13" style="display: none; font-size: 10px;">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Comprobante:
                </td>
                <td>
                    <input id="impFav_name"  type="text" name="name" value=" "  style="width: 300px" /><br />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Fecha Pago:
                </td>
                <td>
                    <input id="impFuv_Desc" type="text" name="name" value=" " style="width: 300px" />
                    <br />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Valor:
                </td>
                <td>
                    <input id="impFun_Order" type="text" name="name" value=" " style="width: 300px" /><br />
                </td>
                <td>
                </td>
            </tr>
             <tr>
                <td>
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="DDPadre2" runat="server">
                    </asp:DropDownList><br />
                </td>
                <td>
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
                    <p id="validateTips">
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

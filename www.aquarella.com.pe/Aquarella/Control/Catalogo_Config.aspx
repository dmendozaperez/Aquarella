<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" 
    CodeBehind="Catalogo_Config.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Control.Catalogo_Config"
    Theme="SiteTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
   <%-- <title>Aplicaciones</title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tabs").tabs({
                collapsible: true
            });
            $('#tabs').tabs('select', '#tab-1'); // Para seleccionar el tab 1 y que este colapsado el panel de insercion de rol 

        });

        function updateApp(CAT_IDCATALOGO,CAT_DESCRIPCION,CAT_HEADERTITLE,CAT_NROPAGINA, CAT_ESTID) {
            removeFieldsErrors();
            $("#impdescripcion").val(CAT_DESCRIPCION);
            $("#impheadertitle").val(CAT_HEADERTITLE);
            $("#impnropagina").val(CAT_NROPAGINA);
            $("#<%= DDState2.ClientID %> option[value='" + CAT_ESTID + "']").attr('selected', 'selected');           
            $("#dialog_update").dialog({ width: 400, height: 200, modal: true, title: 'Editar ' + CAT_DESCRIPCION, open: true });
            $("#dialog_update").dialog({ buttons: [{
                text: "Actualizar Catalogo",
                click: function () {
                    if (Validacion())
                        updateAppAJAX(CAT_IDCATALOGO);
                        limpiar_obj();
                }
            }]
            });
        }
        function limpiar_obj() {
            $("#impdescripcion").val('');
            $("#impheadertitle").val('');
            $("#impnropagina").val('');
        }


        function updateAppAJAX(CAT_IDCATALOGO) {
            $.ajax({
                type: "POST",
                data: "{'CAT_IDCATALOGO': '" + CAT_IDCATALOGO + "','CAT_DESCRIPCION': '" + $("#impdescripcion").val() +
                "','CAT_HEADERTITLE': '" + $("#impheadertitle").val() + "','CAT_NROPAGINA': '" + $("#impnropagina").val() + "','CAT_ESTID': '" + $("#<%= DDState2.ClientID %>").val() + "'}",
                url: "Catalogo_Config.aspx/ajaxUpdateApp",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d == new String("1")) {
                        $('#dialog_update').dialog("close");
                        var bnaceptar = $("input[id$='btnrefresh']");
                        allFields = $([]).add(bnaceptar);
                        bnaceptar.click();

                    }
                    else {
                        alert("Occurrio un error Durante la Actualizacion");
                    }
                },
                error: function (result) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + result); }
            });
        }

        function Validacion() {
            var bValid = true;
            removeFieldsErrors();

            bValid = bValid && checkNull($("#impdescripcion"), "*Descripcion del catalogo no puede estar vacio");
            bValid = bValid && checkNull($("#impheadertitle"), "*Header Title no puede estar vacio");
            bValid = bValid && checkNull($("#impnropagina"), "*Numero de pagina no puede estar vacio");

            return bValid;
        }

        function removeFieldsErrors() {
            $("#validateTips").text("");
            $("#impdescripcion").removeClass("ui-state-error");
            $("#impheadertitle").removeClass("ui-state-error");
            $("#impnropagina").removeClass("ui-state-error");
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

        function updateTips(t) {
            $("#validateTips")
			.text($("#validateTips").text() + t)
            .addClass("ui-state-highlight");
            setTimeout(function () {
                $("#validateTips").removeClass("ui-state-highlight", 3500);
            }, 500);
        }

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //Examples of how to assign the ColorBox event to elements
                $("input:submit,button").button();
            }
        }
    </script>--%>
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
     Generacion de Catalogo - Virtual
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
        Generar o editar el catalogo virtual de la pagina informativa www.aquarellaperu.com.pe
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
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
            <AQControl:Message runat="server" ID="msnMessage" Visible="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
     <div id="tabs">
        <ul>
            <li><a href="#tabs-1">Nuevo Catalogo</a></li>
        </ul>
        <div id="tabs-1">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        Descripcion:
                    </td>
                    <td>
                        <asp:TextBox ID="txtdescripcion" runat="server" Width="220px" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RFValidator" runat="server" ControlToValidate="txtdescripcion"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>               
                <tr>
                    <td>
                        Header Title:
                    </td>
                    <td>
                        <asp:TextBox ID="txtheadertitle" runat="server" Width="220px" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nro Pag:
                    </td>
                    <td>
                        <asp:TextBox ID="txtnropagina" runat="server" Width="220px" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Estado
                    </td>
                    <td>
                        <asp:DropDownList ID="DDStatus" runat="server">
                            <asp:ListItem Text="Activo" Value="A" Selected="True" />
                            <asp:ListItem Text="Inactivo" Value="I" />
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>                             
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <asp:Button ID="SaveApp" Text="Guardar" runat="server" OnClick="SaveApp_Click" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
      <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 70%">
            </td>
            <td style="width: 20%" align="right">
                <asp:TextBox ID="txtFilterGrid" runat="server" />
            </td>
            <td style="width: 10%">
                <asp:Button ID="btnFiltrar" Text="Filtrar Por nombre" runat="server" OnClick="btnFiltrar_Click"
                    ValidationGroup="G1" />
            </td>
             <td>
                <asp:Button ID="btnrefresh" runat="server" style="display:none" OnClick="btnrefresh_Click"
                   ValidationGroup="G1"   />
            </td>
        </tr>
    </table>
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:GridView ID="GridApplication" runat="server" AutoGenerateColumns="false" SkinID="gridviewSkin"
                OnPageIndexChanging="GridApplication_PageIndexChanging" Width="450px">
                <Columns>
                    <asp:BoundField DataField="IdCatalogo" HeaderText="Id" />                    
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                    <asp:BoundField DataField="Header_Title" HeaderText="Header_Title" />
                    <asp:BoundField DataField="NroPagina" HeaderText="N° Pagina" />
                    <asp:BoundField DataField="Est_Id" HeaderText="Estado" />
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <center>
                                <a href="#" onclick="updateApp('<%# Eval("IdCatalogo") %>','<%# Eval("Descripcion") %>','<%# Eval("Header_Title") %>','<%# Eval("NroPagina") %>','<%# Eval("Est_Id") %>')">
                                    <asp:Image ID="Image1" ImageUrl="~/Design/images/Botones/editOrder.png" runat="server" />
                                </a>
                            </center>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnFiltrar" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <div id="dialog_update" class="f13" style="display: none; font-size: 10px;">
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    Descripcion:
                </td>
                <td>
                    <input id="impdescripcion" type="text" name="name" value=" " style="width: 220px" />
                </td>
                <td>
                </td>
            </tr>           
            <tr>
                <td>
                    Header Titulo:
                </td>
                <td>
                    <input id="impheadertitle" type="text" name="name" value=" " style="width: 220px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    N°Pagina:
                </td>
                <td>
                    <input id="impnropagina" type="text" name="name" value=" " style="width: 100px" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Estado:
                </td>
                <td>
                    <asp:DropDownList ID="DDState2" runat="server">
                        <asp:ListItem Text="Activo" Value="A" Selected="True" />
                        <asp:ListItem Text="Inactivo" Value="I" />
                    </asp:DropDownList>
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
    </div>--%>
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
                        Buscar Catalogo
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
                               </td>
                             <td>
                            </td>
                             <td>
                            </td>
                             <td class="f12">
                               </td>
                             <td>
                                 &nbsp;</td>
                             <td>
                                 &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                              <%--  <asp:UpdatePanel ID="upStartDate" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDateStart" runat="server" AccessKey="f"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendar" runat="server" Animated="true" 
                                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" 
                                            TargetControlID="txtDateStart" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </td>
                             <td>
                                            <%--<asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                Style="cursor: pointer;" />--%>
                            </td>
                             <td>
                                         <%--   <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateStart" runat="server"
                                                ToolTip="Fecha de inicio" CssClass="error_asterisck" ErrorMessage="Dígite fecha inicial"
                                                Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDateStart">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cvDateStart" runat="server" ValidationGroup="vsConsultForm"
                                                Type="Date" SetFocusOnError="true" CssClass="error_asterisck" ControlToValidate="txtDateStart"
                                                Operator="DataTypeCheck" ErrorMessage="Dígite una fecha válida">*</asp:CompareValidator>--%>
                            </td>
                             <td>
                               <%--  <asp:UpdatePanel ID="upEndDate" runat="server">
                                     <ContentTemplate>
                                         <asp:TextBox ID="txtDateEnd" runat="server" AccessKey="f"></asp:TextBox>
                                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                             Animated="true" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                                             PopupButtonID="imgCalendarDe" TargetControlID="txtDateEnd" />
                                     </ContentTemplate>
                                 </asp:UpdatePanel>--%>
                            </td>
                             <td>
                                <%-- <asp:Image ID="imgCalendarDe" runat="server" 
                                     ImageUrl="~/Design/images/Botones/b_calendar_ico.gif" 
                                     onmouseout="this.style.background=''" 
                                     onmouseover="this.style.background='red';" Style="cursor: pointer;" />--%>
                            </td>
                             <td>
                               <%--  <asp:RequiredFieldValidator ID="rfvDateEnd" runat="server" 
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
                                     ValidationGroup="vsConsultForm">*</asp:CompareValidator>--%>
                            </td>
                        </tr>
                    </table>
                </td>
                                <td class="auto-style1">
                                    Ingrese el catalogo:<br />                                    
                                </td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtcatalogo" runat="server"></asp:TextBox>
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
                    <li><a href="#fragment-1"><span>Lista de Catalogo Virtual</span></a></li>                    
                </ul>                
                <!-- MANIFIESTO -->
                <div id="fragment-1" style="min-height: 200px;">
                    <p>
                        Lista de Catalogo; consulta el catalogo virtual.</p>
                    <asp:UpdatePanel ID="upmanifiesto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvmanifiesto" runat="server" Width="98%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False"  Font-Size="8" AllowPaging="True" AllowSorting="True" CellPadding="3" ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" OnRowCommand="gvmanifiesto_RowCommand" OnRowDataBound="gvmanifiesto_RowDataBound" OnPageIndexChanging="gvmanifiesto_PageIndexChanging" PageSize="8" OnRowCreated="gvmanifiesto_RowCreated" >
                              <%--  <EmptyDataTemplate>
                                    No existen manifiesto que mostrar.
                                </EmptyDataTemplate>--%>
                                <Columns>
                                    <asp:BoundField DataField="IdCatalogo"
                                        HeaderText="Id" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Header_Title" HeaderText="Header Title" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Est_Descripcion" HeaderText="Estado" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>                                    
                                       <asp:TemplateField HeaderText="Edit." ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgedit" CommandArgument='<%# Eval("IdCatalogo")%>'
                                                CommandName="EditOrder" OnClientClick="openDialogWait();" runat="server" ImageUrl="~/Design/images/Botones/b_edit_order.png"
                                                Visible="false" ToolTip="Cargar para edición." BorderWidth="0" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Anular" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibanular" CommandName="starnular"  CommandArgument='<%# Eval("IdCatalogo")%>'
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
                        Text="(C)rear Nuevo Catalogo"  OnClick="btncrearman_Click"/>                    
                </td>
            </tr>
        </table>         
    </div>
    <AQControl:ShippingForm runat="server" Visible="true" ID="ShippForm" />
</asp:Content>

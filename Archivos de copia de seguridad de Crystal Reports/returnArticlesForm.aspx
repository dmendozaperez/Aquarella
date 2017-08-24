<%@ Page Language="C#" StylesheetTheme="SiteTheme" AutoEventWireup="true" CodeBehind="returnArticlesForm.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.returnArticlesForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Aquarella > Módulo de devolución a clientes</title>

    <!-- JAVASCRIPTS -->
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/ColorBox/jquery.colorbox.js"></script>


    <!-- STYLES JQUERY -->

    <link href="../../Scripts/ColorBox/colorbox.css" rel="stylesheet" type="text/css" />        
    <link href="../../Styles/theme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
         $(document).ready(function () {
             $("input:submit,button").button();
         });
    </script>       
    <script type="text/javascript">

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                /////////////////////////////////////////////////////////
                // COLOR BOX, SE CARGA AQUI TAMBIEN PARA LAS ACCIONES DE POSTBACK ASYNCRONOS
                //Examples of how to assign the ColorBox event to elements
                $(".iframe").colorbox({ width: "90%", height: "94%", iframe: true });
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function KeyboardHandler(idBtCliente) {
            /*Esta funcion funciona con KeyPress y recibe como parametro el nombre de la caja destino(que es una cadena)*/

            //Primero debes obtener el valor ascii de la tecla presionAQUARELLA 
            var key = window.event.keyCode;

            //Si es enter, codigo de enter(13) 
            if (key == 13) {
                if ($("#chkAutoSelect").is(':checked'))
                    $("#imgShowInvoQts").click();
                else
                ///Se pasa el foco a la caja destino
                    document.getElementById(idBtCliente).focus();
                return false;
            }
        }
        function SetSelected(idBtCliente) {
            if (document.getElementById(idBtCliente).value.length > 2) {
                document.getElementById(idBtCliente).select();
            }
        } 
    </script>


    <!-- AJAX DIALOGS DE MUESTRA DE ARTICULOS Y FACTURAS HABILITAQUARELLAS PARA REALIZAR DEVOLUCIONES -->
    <script type="text/javascript" language="javascript">
        ///
        function ajax_GetInvoices(fieldInvo, fieldArt, dwCustomers, btFindArticle) {
            ///
            try {
                var valid = '';
                ///
                var fieldInvoice = $("#" + fieldInvo);
                var fieldArticleValue = $("#" + fieldArt).val();
                ///
                var customerSelect = $("#" + dwCustomers + " :selected").val();
                var customerNameSelect = $("#" + dwCustomers + " :selected").text();
                ///
                var isr = ' es requerido.';
                ///
                if (fieldArticleValue == null || fieldArticleValue.length < 7) {
                    valid += '\n > Una referencia real de artículo ' + isr;
                    $("#" + fieldArt).focus();
                }
                if (customerSelect == '-1') {
                    valid += '\n > Seleccionar un cliente ' + isr;
                    $("#" + dwCustomers).focus();
                }
                ///
                if (valid != '') {
                    alert("Error :" + valid);
                }
                else {
                    /// No existen problemas, se puede consultar                    
                    var dialog = $("#dialog");  
                    dialog.dialog({ width: 780, height: 300, title: 'Facturas del cliente > ' + customerNameSelect, open: function (event, ui) {
                        /// Loading.. image
                        dialog.html('<img src="../../Design/images/ajax_loader_face.gif" />'); //dialog.text('dentro del dialog;');
                        var content = "";
                        var flagresults = false;
                        var resultserror = "";
                        var lastReference = "";
                        var autoAdd = false;
                        ///
                        $.ajax({
                            type: "POST",
                            ///data: "{}",
                            data: "{ article: '" + fieldArticleValue + "',customer:'" + customerSelect + "'}",
                            url: "returnArticlesForm.aspx/ajaxGetInvoicesForArticle",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                ///
                                $.each(msg, function (i, m) {
                                    $.each(m, function (j, m1) {
                                        if (m1.Size != '-1') {
                                            if (j == 0) {
                                                ///
                                                content += "<span style='font-size:13px;font-family:Georgia;padding-bottom:5px;'><b><i>" + m1.Nombre + "</i></b></span><br>";
                                                ///
                                                lastReference = m1.Ref;
                                                ///
                                                content += "<div style='font-size:9px;width:100%;border-bottom:solid 1px silver; font-family:Georgia;color:salmon;padding-bottom:5px;'><i>" + m1.Nombre + " <b> > </b> " + m1.brand + "<b> > </b> " + m1.Color + "</i></div>";
                                                content += "<br><span style='padding-bottom:5px;width:100%;'>Table 1: Facturas asociAQUARELLAs al articulo > " + m1.Nombre + " </span><br>";
                                                content += "<table class='tableInvoices' cellspacing='0' cellpadding='5' width='100%' border='0'>" +
                                                "<tr><th>Facura</th><th>Fecha</th><th>Article</th><th>Talla</th><th>Facturado</th><th>Devuelto</th><th>Disp.Devol</th><th>Precio</th><th>Neto</th><th>No.Dev</th><th></th></tr>";
                                                ///var fun = "javascript:addArt('" + fieldInvo + "','" + m1.idv_invoice + "', '" + fieldArticleValue + "','" + btFindArticle + "')";
                                                var fun = 'javascript:addArt("' + dialog + '","' + fieldInvo + '","' + m1.idv_invoice + '","' + fieldArt + '","' + btFindArticle + '")';
                                                ///
                                                flagresults = true;
                                                ///
                                                if (m1.qty_to_devol <= 0)
                                                    content += "<tr><td class='rowBlock'><b>" + m1.idv_invoice + "</b></td><td class='rowBlock'>" + m1.ihd_date + "</td><td class='rowBlock'>" + m1.Ref + "</td><td class='rowBlock'>" + m1.Size + "</td><td class='rowBlock'>" + m1.Qty + "</td><td class='rowBlock'>" + m1.qty_devol + "</td><td class='rowBlock'><b>" + m1.qty_to_devol + "</b></td><td class='rowBlock'>" + m1.idn_sellprice + "</td><td class='rowBlock'>" + m1.idn_neto + "</td><td class='rowBlock'>" + m1.rdv_return + "</td><td class='rowBlock'></td></tr>";
                                                else {
                                                    content += "<tr><td><b>" + m1.idv_invoice + "</b></td><td>" + m1.ihd_date + "</td><td>" + m1.Ref + "</td><td>" + m1.Size + "</td><td>" + m1.Qty + "</td><td>" + m1.qty_devol + "</td><td><b>" + m1.qty_to_devol + "</b></td><td>" + m1.idn_sellprice + "</td><td>" + m1.idn_neto + "</td><td>" + m1.rdv_return + "</td><td><img src='../../Design/images/Botones/chulo.png' style='cursor: hand;' onclick='" + fun + "' /></td></tr>";
                                                    /// Agregar automaticamente el primer articulo
                                                    if ($("#chkAutoSelect").is(':checked')) {
                                                        addArt(dialog, fieldInvo, m1.idv_invoice, fieldArt, btFindArticle);
                                                        autoAdd = true;
                                                    }
                                                }
                                            }
                                            else {
                                                ///
                                                var fun = 'javascript:addArt("' + dialog + '","' + fieldInvo + '","' + m1.idv_invoice + '","' + fieldArt + '","' + btFindArticle + '")';
                                                ///
                                                if (m1.qty_to_devol <= 0)
                                                    content += "<tr><td class='rowBlock'><b>" + m1.idv_invoice + "</b></td><td class='rowBlock'>" + m1.ihd_date + "</td><td class='rowBlock'>" + m1.Ref + "</td><td class='rowBlock'>" + m1.Size + "</td><td class='rowBlock'>" + m1.Qty + "</td><td class='rowBlock'>" + m1.qty_devol + "</td><td class='rowBlock'><b>" + m1.qty_to_devol + "</b></td><td class='rowBlock'>" + m1.idn_sellprice + "</td><td class='rowBlock'>" + m1.idn_neto + "</td><td class='rowBlock'>" + m1.rdv_return + "</td><td class='rowBlock'></td></tr>";
                                                else {
                                                    content += "<tr><td><b>" + m1.idv_invoice + "</b></td><td>" + m1.ihd_date + "</td><td>" + m1.Ref + "</td><td>" + m1.Size + "</td><td>" + m1.Qty + "</td><td>" + m1.qty_devol + "</td><td><b>" + m1.qty_to_devol + "</b></td><td>" + m1.idn_sellprice + "</td><td>" + m1.idn_neto + "</td><td>" + m1.rdv_return + "</td><td><img src='../../Design/images/Botones/chulo.png' style='cursor: hand;' onclick='" + fun + "'/></td></tr>";
                                                    /// Si aun no se ha agregado el articulo; solo en caso de tener la opcion agregar automaticamente
                                                    /// Agregar automaticamente el primer articulo
                                                    if ($("#chkAutoSelect").is(':checked') && !autoAdd) {
                                                        addArt(dialog, fieldInvo, m1.idv_invoice, fieldArt, btFindArticle);
                                                        autoAdd = true;
                                                    }
                                                }
                                                /// Diferenciar visualmente los stock en 0
                                                /*var styleQtyInfo = "";
                                                if (m1.Qty == 0)
                                                styleQtyInfo = "<span style='color:red'>" + m1.Qty + "</span>";
                                                else
                                                styleQtyInfo = m1.Qty;*/
                                                flagresults = true;
                                            }
                                        }
                                        else {
                                            resultserror = m1.Nombre;
                                        }
                                    }) /// Fin primer each
                                }) /// Fin segundo each
                                content += "</table><br>";
                                ///
                                if (flagresults)
                                    dialog.html(content);
                                else
                                    dialog.text(resultserror);
                            },
                            error: function (msg) { alert("Por favor, verifique que la referencia sea en codigo de barras, además, que su sesión no haya expirado, e intente de nuevo." + msg); }
                        });

                    } /// fin fuction dialog
                    });               /// fin dialog

                }
            }
            catch (Error) {
                alert("Error en la consulta; Puede ocurrir que los componentes y extensiones necesarias no hayan sido cargAQUARELLAs correctamente." + Error);
            }
        }
        function addArt(dial, invoField, invo, fieldArticle, btFindArticle) {
            /// Close the dialog
            $("#dialog").dialog("close");
            ///$("#dialog").remove();
            $("#dialog").dialog("destroy");
            ///            
            try {
                /// Adicionar el numero de la factura
                $("#" + invoField).val(invo);
                ///
                $("#" + fieldArticle).focus();
                /// Emular un click del boton de adicion de articulos al grid de devoluviones
                $("#" + btFindArticle).click();
            }
            catch (Error) {
                ///
                alert('No se ha podido adicionar el artículo al panel de devoluciones; realicelo manualmente.' + Error);
            }

        }
    </script>
</head>
<body style="font-size: 62.5%;">
    <form id="form1" runat="server">
    <div style="padding:10px;">
        <asp:UpdatePanel runat="server" ID="UpHids">
            <ContentTemplate>
                <asp:ScriptManager ID="ScriptManager1" runat="server" />                
                <asp:HiddenField ID="hdIdCoordinator" runat="server" />              
            </ContentTemplate>
        </asp:UpdatePanel>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <table class="tableheaderreturn">
                        <tr>
                            <td style="width: 60%;">
                                <span>Módulo de generación de devoluciones - Encargado |
                                </span>
                                <asp:Label ID="lblWhoIs" runat="server"></asp:Label>
                            </td>
                            <td style="width: 20%;" align="right">
                                <span style="text-align: right"><a title="Volver a la opción anterior"
                                    href="../../Default.aspx">Volver</a> </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                       <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <%-- ------------------- DROP DOWN LIST DE SELECCION DE COORDINADORES ---------------------- --%>
                        <tr>
                            <td>
                                <div style="margin: 10px auto 0 auto;">
                                    <table class="tableseparator" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellspacing="4" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td>
                                                            <span><span><u>S</u>eleccione Coordinador :</span></span>
                                                            <br />
                                                            <span>(Desc: Coordinador sobre quien se facturo.)</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="dwCoordinadores" Width="235px" AutoPostBack="True" DataTextField="nombres"
                                                                DataValueField="bas_id"  AppendDataBoundItems="True"
                                                                AccessKey="s" runat="server" OnSelectedIndexChanged="dwCoordinadores_SelectedIndexChanged">
                                                                <asp:ListItem Value="-1" Text=" -- Seleccione de la Lista --"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="error" ValueToCompare="-1"
                                                                Operator="NotEqual" ControlToValidate="dwCoordinadores" SetFocusOnError="true"
                                                                ValidationGroup="valerror" ErrorMessage="Seleccione el Coordinador Relacionado a la Factura.">*</asp:CompareValidator>                                                        
                                                        </td>
                                                        <td align="right">
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                <span style="font-size:14px;display:none" >
                                                                    <table cellpadding="4" cellspacing="4">
                                                                        <tr>
                                                                            <td>
                                                                                Saldo :
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="lblSaldo" Font-Size="14px" runat="server" Text="0"></asp:Label>
                                                                            </td>
                                                                            <td style="border-left: solid 1px silver;">
                                                                                Pedidos :
                                                                            </td>
                                                                            <td>
                                                                                 <asp:Label ID="lblOrders" Font-Size="14px" runat="server" Text="0"></asp:Label>
                                                                            </td>
                                                                            <td style="border-left: solid 1px silver;">
                                                                                Faltante :
                                                                            </td>
                                                                            <td>
                                                                                 <asp:Label ID="lblRest" Font-Size="14px" runat="server" Text="0"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </span>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="dwCoordinadores" EventName="SelectedIndexChanged" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <%-- ------------------- INFORMACION DEL COORDINADOR SELECCIONADO ---------------------- --%>
                        <tr>
                            <td colspan="2">
                                <div style="margin: 10px auto 0 auto;">
                                    <table class="tableseparator" cellspacing="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanelInfoCoordinator" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table cellspacing="4" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <span><span>No.Documento : </span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDocumento" ReadOnly="true" BorderColor="white"
                                                                        BorderWidth="0" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <span><span>Nombre : </span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtNombre" ReadOnly="true" Width="190px" BorderColor="white"
                                                                        BorderWidth="0" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span><span>Dirección : </span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtDireccion" ReadOnly="true" Width="180px" 
                                                                        BorderColor="white" BorderWidth="0" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                   <span><span>Teléfono :</span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTelefono" ReadOnly="true" Width="180px" 
                                                                        BorderColor="white" BorderWidth="0" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <span><span>Ubicación :</span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUbicacion" ReadOnly="true" Width="180px" 
                                                                        BorderColor="white" BorderWidth="0" runat="server"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <span><span>E-Mail :</span></span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtMail" ReadOnly="true" Width="180px" BorderColor="white"
                                                                        BorderWidth="0" runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="dwCoordinadores" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="tableseparator" style="border-top-width: 0;" cellspacing="0" width="100%"
                                    border="0">
                                    <tr>
                                        <td align="center">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                <ProgressTemplate>
                                                    <center>
                                                        <div style="position: absolute; background: #f5f5f5; filter: alpha(opacity=85); opacity: 0.85;
                                                            font-family: Georgia; text-align: center; width: 93%; top: 150px; left: 47px;
                                                            font-size: medium;">
                                                            <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
                                                            Cargando...
                                                        </div>
                                                    </center>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%-- ------------------- CAMPOS DE NO DE FAC Y REFERENCIA DEL ARTICULO A DEVOLVER ---------- --%>
                        <tr>
                            <td colspan="2" class="f12">
                                <div style="margin: 10px auto 0 auto;">
                                    <table width="100%" class="tableseparator" cellpadding="0">
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtNoInvoice" Width="200px" runat="server" MaxLength="14"
                                                                AutoCompleteType="None" AccessKey="f" autocomplete="off">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoInvoice"
                                                                ValidationGroup="valerror" CssClass="error" ErrorMessage="Suministre el No de la Factura.">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <div style="float: left;">
                                                    <span><span>No. <u>F</u>actura </span></span>
                                                </div>
                                                <div style="float: right;">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td style="padding-right: 5px;">
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td>
                                                                            <span>Sitema <u>D</u>ecide</span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkAutoSelect" runat="server" AccessKey="d" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="border-left: solid 1px silver; padding-left: 5px; padding-right: 5px;"
                                                                valign="bottom">
                                                                <span style="cursor: hand;" id="imgShowInvoQts" name="imgShowInvoQts" onclick="javascript:ajax_GetInvoices('<%= txtNoInvoice.ClientID %>','<%= txtCodigoArticulo.ClientID%>',  '<%= dwCoordinadores.ClientID %>', '<%= btFindArticle.ClientID %>')">
                                                                    <img border="0" src="../../Design/images/Botones/info.png" alt="Información de las facturas habilitAQUARELLAs para devoluciones de articulos" />
                                                                </span>
                                                            </td>
                                                            <td style="border-left: solid 1px silver; padding-left: 5px; padding-right: 5px;">
                                                                <asp:ImageButton ID="imbPrintInvoice" ToolTip="Ver / Imprimir la Factura." ImageUrl="~/Design/images/Botones/Invoice_go.png"
                                                                    runat="server" CausesValidation="true" ValidationGroup="valerror" OnClick="imbPrintInvoice_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:UpdatePanel ID="updatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtCodigoArticulo" Width="200px" AccessKey="r" runat="server" 
                                                                        MaxLength="14" AutoCompleteType="None" autocomplete="off">
                                                                    </asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="70%">
                                                <table width="100%" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <span><span><u>R</u>eferencia, tipo código de barras</span></span>
                                                        </td>
                                                        <td align="left">

                                                               <asp:Button ID="btFindArticle" runat="server" 
                                                                ValidationGroup="valerror" CausesValidation="true" 
                                                                AccessKey="b" Text="(B)uscar Artículo"
                                                                Width="200px" OnClick="btFindArticle_Click" />
                                                               
                                                        </td>
                                                        <td align="right">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <%-- ------------------- RESULTADOS Y ARTICULOS ADICIONADOS AL PANEL DE LA DEVOLUCION --------- --%>
                        <tr>
                            <td colspan="2" class="f12">
                                <div style="margin: 10px auto 0 auto;">
                                    <table width="100%" cellpadding="4" style="min-height:320px;">
                                        <tr>
                                            <td align="center" valign="top">
                                                <asp:UpdatePanel ID="UpdatePanelGridArticlesReturned" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                       <asp:GridView ID="GridViewArticlesToReturn" SkinID="gridviewSkin" Width="95%" runat="server"
                                                            AutoGenerateColumns="False" OnRowCommand="GridViewArticlesToReturn_RowCommand"
                                                            OnRowCreated="GridViewArticlesToReturn_RowCreated">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="№">
                                                                    <ItemStyle ForeColor="Maroon" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        Mover
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox runat="server" ID="RowLevelChkBox" Checked='<%# Eval("checked") %>'
                                                                            ToolTip='<%# Eval("IDV_INVOICE") + "-" + Eval("idv_article") + "-" + Eval("idv_size") + "-" + Eval("calidad") %>'
                                                                            AutoPostBack="True" OnCheckedChanged="RowLevelCheckBox_CheckedChanged" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="IDV_INVOICE" HeaderText="Factura">
                                                                    <ItemStyle ForeColor="Black" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Referencia" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <a class="iframe" href='../Maestros/informationarticle.aspx?elcitra=<%# Eval("idv_article")%>&isForPublicAcces=T'
                                                                            style="color: #034af3; text-decoration: underline;" title="Información del Artículo / Click En <b>Close</b> Para Cerrar (Esquina Inferior Derecha del Marco)">
                                                                            <%# Eval("idv_article")%></a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="arv_name" HeaderText="Art&#237;culo" />
                                                                <asp:BoundField DataField="Calidad" HeaderText="Calidad" />
                                                                <asp:BoundField DataField="brv_description" HeaderText="Marca" />
                                                                <asp:BoundField DataField="cov_description" HeaderText="Color" />
                                                                <asp:BoundField DataField="idv_size" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderText="Talla">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IDN_QTY" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                                                    HeaderText="Cant">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IDN_SELLPRICE" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                                                    HeaderText="Precio" DataFormatString="{0:C}">
                                                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TAXES" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-HorizontalAlign="Center" HeaderText="+Impuestos">
                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IDN_COMMISSION" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                                                    HeaderText="-Ganancia" DataFormatString="{0:C}">
                                                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IDN_DISSCOUNT" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right"
                                                                    HeaderText="-Descuento" DataFormatString="{0:C}">
                                                                    <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <div>
                                                                            <asp:ImageButton ID="ibDeleteArticleReturned" CommandName="ibDeleteArticleReturned"
                                                                                runat="server" ToolTip='<%# "Eliminar el artículo "+ Eval("idv_article") + " en talla " + Eval("idv_size") + " del Panel de Devoluciones."%>'
                                                                                CommandArgument='<%# Eval("idv_invoice") + "@" + Eval("idv_article") + "@" + Eval("idv_size") + "@" + Eval("calidad")%>'
                                                                                ImageUrl="~/Design/images/Botones/delete_off.png" OnClientClick='return confirm("¿ Esta usted seguro de eliminar el artículo de las devoluciones ?");' />
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>                                                           
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btFindArticle" EventName="Click" />
                                                          <asp:AsyncPostBackTrigger ControlID="btReturnArticles" EventName="Click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="tableseparator" style="border-top-width: 0;" cellspacing="0" width="100%"
                                    border="0">
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%-- ------------------- CAMPOS VALORES EN UNIDADES MONETARIAS A DEVOLVER ---------- --%>
                        <tr>
                            <td colspan="2">
                                <div style="margin: 10px auto 0 auto;">
                                    <table width="100%" class="tableseparator" cellpadding="2" style="background-color: #D8DFE6;">
                                        <tr>
                                            <td width="50%">
                                                <div style="padding: 10px; background-color: #F0F8FF; height: 80px;">
                                                <span style="font-size:14px">
                                                    <table width="100%" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td colspan="2">
                                                                Seleccione el area o Storage a donde se enviarán los artículos seleccionados :
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span><span><u>
                                                                    <br />
                                                                    S</u>torage de Destino :&nbsp;<br />
                                                                </span></span><span>(Ej: Fallados, estanterias,etc.)</span>
                                                            </td>
                                                            <td align="right">
                                                                <div style="padding-right: 25px;">
                                                                    <asp:UpdatePanel ID="upDwStorages" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList AccessKey="s" ID="dwStorages" runat="server"
                                                                                Width="235px" DataTextField="Alm_Descripcion" DataValueField="Alm_Id"
                                                                                AppendDataBoundItems="True" ValidationGroup="valerror">
                                                                                <asp:ListItem Value="">--Seleccione Storage--</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span><span>Número de artículos seleccionados :</span></span>
                                                            </td>
                                                            <td align="right" style="border-bottom: 1px solid silver; color: Green; font-size: large;">
                                                                <div style="padding-right: 25px;">
                                                                    <asp:UpdatePanel ID="upLblUndsSelected" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Label ID="lblUndsSelected" Font-Size="14px" runat="server" Text="0" Width="235px"></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </span>
                                                </div>
                                            </td>
                                            <td width="50%">
                                                <div style="padding: 10px; background-color: #FFFFCC; height: 80px;">
                                                    <table width="100%" cellpadding="4" cellspacing="4">
                                                        <tr>
                                                            <td style="border-bottom: 1px solid silver; font-size: medium;"
                                                                width="60%" align="right">
                                                                Cantidades Devueltas :
                                                            </td>
                                                            <td align="right" style="border-bottom: 1px solid silver; padding-right: 30px; color: Maroon;
                                                                font-size: large;">
                                                                <asp:UpdatePanel ID="updatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Label ID="lblCantsReturned" Font-Size="14px" runat="server" Text="0"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td  width="60%" align="right" style="border-bottom: 1px solid silver;
                                                                font-size: medium;">
                                                                Valor Total De Devoluci&oacute;n :
                                                            </td>
                                                            <td align="right" style="border-bottom: 1px solid silver; padding-right: 30px; color: Maroon;
                                                                font-size: large">
                                                                <asp:UpdatePanel ID="updatePanelTotalsPackageActual" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Label ID="lblTotalValorDevolucion" Font-Size="14px" runat="server" Text="0"></asp:Label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="tableseparator" style="border-top-width: 0;" cellspacing="8" cellpadding="8"
                                    width="100%" border="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btReset" runat="server" ValidationGroup="valerror"
                                                CausesValidation="true"  AccessKey="i" Text="(I)niciar una nueva devolución"
                                                Width="200px" OnClick="btReset_Click" />
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btReturnArticles" runat="server"
                                                ValidationGroup="valerror" AccessKey="z" CausesValidation="true" Text="Reali(z)ar devoluciones"
                                                Width="200px" OnClick="btReturnArticles_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="dialog" style="display: hidden; font-size: 10px;">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
      <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="UpdatePanelGridArticlesReturned">
            <Animations>
                                    <OnUpdating>
                                        <Sequence>
                                            <%-- Disable all the controls --%>
                                            <Parallel duration="0">
                                                <EnableAction AnimationTarget="btReturnArticles" Enabled="false" />                                             
                                            </Parallel>
                                        </Sequence>
                                    </OnUpdating>
                                    <OnUpdated>
                                        <Sequence>
                                            <%-- Enable all the controls --%>
                                            <Parallel duration="0">   
                                                <EnableAction AnimationTarget="btReturnArticles" Enabled="true" />
                                            </Parallel>
                                        </Sequence>
                                    </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </form>
</body>
</html>

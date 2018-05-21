<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucShippingForm.ascx.cs"
    Inherits="www.aquarella.com.pe.UserControl.ucShippingForm" %>
<script type="text/javascript">
    var txtName = '',
            txtPhone = '',
            txtMovil = '',
            txtAddress = '',
            txtBlock = '',
            txtCity = '',
            dwDptos,
            valid,
            allFields = '',
            isr = ' es requerido.',
            tips = '',
            btCreateLiq,
            btLiq,
            chkbInfoShipp,

            txtNoVoucher, txtValue, h_numTipPago, txtNoTarjeta, h_numConfigPagoPOS, rpta_Validate, hdestado

            ;

    
</script>
<script type="text/javascript" language="javascript">
   
    function ejecutapedido() {
        //Ajax
        var urlMethod = "ordersForm.aspx/get_despacho";
        var jsonData = '{}';
        SendAjax(urlMethod, jsonData, showdes);
    }
    function showdes(msg) {
        var val = msg.d;
        $("#tpedido").html(val.tpedido);
        $("#tdispo").html(val.tdisponible);
       
        if (val.tpedido!=val.tdisponible)
        {
            $("#dialogp").dialog().dialog("widget").find(".ui-dialog-titlebar-close").hide();
            $("#dialogp").dialog({
                title: "Observacion del Pedido",
                buttons: {
                    Ok: function () {
                        $(this).dialog('close');
                        openDialogLoad();
                        btCreateLiq.click();
                    }
                },
                modal: true,
                closeOnEscape: false,
                closeText: 'hide',
                resizable: false
            });
        }
        else
        {
            openDialogLoad();
            btCreateLiq.click();
        }
    }


    $(document).ready(function () {
        btsaveorder = $("#btsaveorder");
        btsaveorderexit = $("#btsaveorderexit");
        hdestado = $("input[id$='hdestado']");
        btsaveorderasp = $("input[id$='btSave']");
        btsaveorderexitasp = $("input[id$='btSaveExit']");
        var btLiqui;
        if (hdestado.val() == "0") {
            btLiqui = $("#btCreateLiquidation");
        }
        else {
            btLiqui = $("#btmodiliquidacion");
        }
        /*
        $("input[id$='btCreateLiq']").click(function () {
        // Do client side button click stuff here.
        //return confirm('Confirmar');              
        });*/

        txtNoTarjeta = $("input[id$='txtNoTarjeta']");
        txtValue = $("input[id$='txtValue']");
        txtNoVoucher = $("input[id$='txtNoVoucher']");
        h_numTipPago = $("input[id$='h_numTipPago']");
        h_numConfigPagoPOS = $("input[id$='h_numConfigPagoPOS']");

        tips = $("#validateTips");
        allFields = $([]).add(txtNoVoucher).add(txtValue).add(txtNoTarjeta);


        //--------------------------------------------aca me quede


        btLiqui.click(function (event) {
            setShipping('');
        });

        btsaveorder.click(function (event) {
            openDialogLoadPedido();
            btsaveorderasp.click();
        });



        btsaveorderexit.click(function (event) {
            openDialogLoadPedido();
            btsaveorderexitasp.click();
        });

        // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
        $("#dialog:ui-dialog").dialog("destroy");

        if (hdestado.val() == "0") {

            $("#dialog-confirm").dialog({
                autoOpen: false,
                resizable: false,
                width: 400,
                height: 160,
                modal: true,
                buttons: {
                    "Continuar": function () {
                        $(this).dialog("close");

                        /*---------------------es para validar los campos numero Voucher  y  numero de tarjeta de credito */
                       
                        if (h_numTipPago.val() == h_numConfigPagoPOS.val()) {
                            var bValid = true;
                            tips.text("");
                            allFields.removeClass("ui-state-error");

                            //
                            if (txtNoTarjeta.val() == "") {
                                bValid = false;
                                txtNoTarjeta.addClass("ui-state-error")
                                updateTips(" Debe de llenar los 16 digitos de la tarjeta de credito");
                            }

                            if (txtNoVoucher.val() == "") {
                                bValid = false;
                                txtNoVoucher.addClass("ui-state-error")
                                updateTips(" Debe de llenar el numero del voucher");
                            }

                            if (txtValue.val() == "") {
                                bValid = false;
                                txtValue.addClass("ui-state-error")
                                updateTips(" Debe de llenar el monto del voucher");
                            }

                            bValid = bValid && checkRegexp(txtNoTarjeta, /^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$/, " Sólo números en la tarjeta de credito.");
                            bValid = bValid && checkLength(txtNoTarjeta, "Número de tarjeta de credito", 16, 16);
                            bValid = bValid && checkRegexp(txtNoVoucher, /^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$/, " Sólo números en l numermo del voucher.");
                            bValid = bValid && checkRegexp(txtValue, /^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$/, " Sólo números en el monto.");

                            //para validar tarjeta Visa Unica
                            if (bValid) {
                                rpta_Validate = ValidarTarjetaPosAjax(txtNoTarjeta.val());
                                //alert("Salio:" + rpta_Validate + " - " + bValid)
                                if (rpta_Validate == false) {
                                    txtNoTarjeta.addClass("ui-state-error")
                                    updateTips(" debe ingresar una tarjeta Visa Unica");
                                    return bValid;
                                }
                            }

                            if (bValid == true) {
                                // alert('paso');
                            }
                            else {
                                // alert('No paso: ' + bValid);
                                return bValid;
                            }
                        }
                        /*---------------------FIN de validar los campos numero Voucher  y  numero de tarjeta de credito */

                        ejecutapedido();

                      
                        //if (valida_pedido=="1"){
                        //    $("#dialogp").dialog({
                        //        title: "Observacion del Pedido",
                        //        buttons: {
                        //            Ok: function () {
                        //                $(this).dialog('close');
                        //                openDialogLoad();
                        //                btCreateLiq.click();
                        //            }
                        //        },
                        //        modal: true
                        //    });
                        //}
                        //else
                        //{
                        //    openDialogLoad();
                        //    btCreateLiq.click();
                        //}
                       
                        //alert('grabo');
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
        else {
            $("#dialog-confirm-edit").dialog({
                autoOpen: false,
                resizable: false,
                width: 400,
                height: 160,
                modal: true,
                buttons: {
                    "Continuar": function () {
                        $(this).dialog("close");

                        /*---------------------es para validar los campos numero Voucher  y  numero de tarjeta de credito */

                        if (h_numTipPago.val() == h_numConfigPagoPOS.val()) {
                            var bValid = true;
                            tips.text("");
                            allFields.removeClass("ui-state-error");

                            //
                            if (txtNoTarjeta.val() == "") {
                                bValid = false;
                                txtNoTarjeta.addClass("ui-state-error")
                                updateTips(" Debe de llenar los 16 digitos de la tarjeta de credito");
                            }

                            if (txtNoVoucher.val() == "") {
                                bValid = false;
                                txtNoVoucher.addClass("ui-state-error")
                                updateTips(" Debe de llenar el numero del voucher");
                            }

                            if (txtValue.val() == "") {
                                bValid = false;
                                txtValue.addClass("ui-state-error")
                                updateTips(" Debe de llenar el monto del voucher");
                            }

                            bValid = bValid && checkRegexp(txtNoTarjeta, /^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$/, " Sólo números en la tarjeta de credito.");
                            bValid = bValid && checkLength(txtNoTarjeta, "Número de tarjeta de credito", 16, 16);
                            bValid = bValid && checkRegexp(txtNoVoucher, /^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$/, " Sólo números en l numermo del voucher.");
                            bValid = bValid && checkRegexp(txtValue, /^\$?\-?([1-9]{1}[0-9]{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\-?\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))$|^\(\$?([1-9]{1}\d{0,2}(\,\d{3})*(\.\d{0,2})?|[1-9]{1}\d{0,}(\.\d{0,2})?|0(\.\d{0,2})?|(\.\d{1,2}))\)$/, " Sólo números en el monto.");

                            //para validar tarjeta Visa Unica
                            if (bValid) {
                                rpta_Validate = ValidarTarjetaPosAjax(txtNoTarjeta.val());
                                //alert("Salio:" + rpta_Validate + " - " + bValid)
                                if (rpta_Validate == false) {
                                    txtNoTarjeta.addClass("ui-state-error")
                                    updateTips(" debe ingresar una tarjeta Visa Unica");
                                    return bValid;
                                }
                            }

                            if (bValid == true) {
                                // alert('paso');
                            }
                            else {
                                // alert('No paso: ' + bValid);
                                return bValid;
                            }
                        }
                        /*---------------------FIN de validar los campos numero Voucher  y  numero de tarjeta de credito */

                        ejecutapedido();
                        //openDialogLoad();
                        //btCreateLiq.click();
                        //alert('grabo');
                    },
                    "Cancelar": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }

        $("#dialog-form").dialog({
            autoOpen: false,
            height: 550,
            width: 800,
            modal: true,
            buttons: {
                "Proceder con mi liquidación": function () {
                    //
                    var bValid = true;
                    tips.text("");
                    allFields.removeClass("ui-state-error");

                    bValid = checkLength(txtName, "Nombre", 5, 50);
                    bValid = checkLength(txtPhone, "Teléfono", 7, 15);
                    bValid = checkLength(txtAddress, "Dirección", 5, 50);
                    bValid = checkLength(txtCity, "Ciudad", 5, 50);

                    if (dwDptos.val() == "-1" || dwDptos.val() == "") {
                        bValid = false;
                        dwDptos.addClass("ui-state-error")
                        updateTips("Seleccione un departamento");
                    }
                    if (bValid) {
                        setShippingAjax(txtName.val(), txtPhone.val(), txtMovil.val(), txtAddress.val(), txtBlock.val(), txtCity.val(), dwDptos.val());
                        $(this).dialog("close");
                    }


                },
                "Cancelar operación": function () {
                    allFields.removeClass("ui-state-error");
                    tips.text("");
                    $(this).dialog("close");
                }
            },
            close: function () {
                allFields.val("").removeClass("ui-state-error");
                tips.text("");
            }
        }); // FIN DIALOG

    });                      // FIN DOC READY

</script>

<script type="text/javascript" language="javascript">
    function ValidarTarjetaPosAjax(numTarjetaPos) {
    var bValid;
        $.ajax({
            type: "POST",
            data: "{ numTarjetaPos: '" + numTarjetaPos + "'}",
            url: "ordersForm.aspx/ajaxValidarTarjetaPos",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: false,  // es para devolver una resultado a otra funcion ajax
            success: function (msg) {
                if (msg.d == new String("1")) {
                    //alert('grabo_1');
                    bValid = true;
                }
                else {
                    $(this).dialog("close");
                    //alert('NOO grabo');
                    bValid = false;
                }
            },
            error: function (msg) {
                bValid = false;
                alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + msg);
            }
        });  // Fin jquery AJAX

        return bValid;
    }
</script>


<script type="text/javascript" language="javascript">

    // Validaciones
    function updateTips(t) {
        tips
            .text(tips.text() + t)
            .addClass("ui-state-highlight");
        setTimeout(function () {
            tips.removeClass("ui-state-highlight", 1500);
        }, 500);
    }


    function checkLength(o, n, min, max) {
        if (o.val().length > max || o.val().length < min) {
            o.addClass("ui-state-error");
            updateTips("Tamaño del campo " + n + " debe estar entre " +
                    min + " y " + max + ". ");
            return false;
        } else {
            return true;
        }
    }

    //
    function checkRegexp(o, regexp, n) {
        if (!(regexp.test(o.val()))) {
            o.addClass("ui-state-error");
            updateTips(n);
            return false;
        } else {
            return true;
        }
    }
</script>
<script type="text/javascript" language="javascript">
    function setShipping(idBtRefresh) {
        var url = this.href;        
        chkbInfoShipp = $("input[id$='chkbInfoShipp']");
        btLiq = $("input[id$='btCreateLiquidation']");
        btCreateLiq = $("input[id$='btCreateLiq']");
        hdestado = $("input[id$='hdestado']");

        if (chkbInfoShipp.is(':checked')) {
            txtName = $("input[id$='txtName']");
            txtPhone = $("input[id$='txtPhone']");
            txtMovil = $("input[id$='txtMovil']");
            txtAddress = $("input[id$='txtAddress']");
            txtBlock = $("input[id$='txtBlock']");
            txtCity = $("input[id$='txtCity']");
            dwDptos = $("select[id$='dwDptos']");
            tips = $(".validateTips");
            allFields = $([]).add(txtName).add(txtPhone).add(txtAddress).add(txtBlock).add(txtCity).add(dwDptos);
            $('#dialog-form').dialog('option', 'title', 'Información de dirección de envío');
            $("#dialog-form").dialog("open");
        }
        else {
            if (hdestado.val() == "0") {
                $('#dialog-confirm').dialog('option', 'title', 'Vamos a generar su nueva Liquidación ¿continuamos?');
                $("#dialog-confirm").dialog("open");
                var strMuestra = "";
                var valor = $("[id$='h_numTipPago']").val();

                if (valor == "008") {
                    strMuestra = ', con forma de pago POR MUESTRA de MERCADERIA'                  
                }

                document.getElementById('popupx').innerHTML = 'Generaremos su liquidación ' + strMuestra + ' ; ¿desea continuar?'
                
            }
            else {
                $('#dialog-confirm-edit').dialog('option', 'title', 'Vamos a Modificar su Liquidación ¿continuamos?');
                $("#dialog-confirm-edit").dialog("open");

                if (valor == "008") {
                    strMuestra = ', con forma de pago POR  MUESTRA de MERCADERIA'
                }

                document.getElementById('popupy').innerHTML = 'Modificaremos su liquidación ' + strMuestra + ' ; ¿desea continuar?'
            }
        }
    }

    // Ajax: Shipping information
    function setShippingAjax(name, phone, movil, shippingAdd, shippingBlock, city, depto) {
        $.ajax({
            type: "POST",
            data: "{ name: '" + name + "',phone: '" + phone + "',movil: '" + movil + "',shippingAdd: '" + shippingAdd + "',shippingBlock: '" + shippingBlock + "',city: '" + city + "',depto:'" + depto + "'}",
            url: "panelOrdersCustomer.aspx/ajaxSetShipping",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                openDialogLoad();
                btLiq.attr("disabled", true);
                btCreateLiq.click();
            },
            error: function (msg) { alert("A ocurrido un error y no se han realizado los cambios, verifique que su sesión no haya expirado, e intente de nuevo." + msg); }
        });  // Fin jquery AJAX
    }

</script>
<script type="text/javascript" language="javascript">
    hdestado = $("input[id$='hdestado']");
    function openDialogLoadPedido() {
        $("#dialog-load-pedido").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
        $("#dialog-load-pedido").dialog().dialog("widget").find(".ui-dialog-titlebar-close").hide();
    }

    function closeDialogLoadPedido() {
        $("#dialog-load-pedido").dialog("close");
    }

    function openDialogLoad() {
        if (hdestado.val() == "0") {
            $("#dialog-load").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
            $("#dialog-load").dialog().dialog("widget").find(".ui-dialog-titlebar-close").hide();
        }
        else {
            $("#dialog-load-mod").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
            $("#dialog-load-mod").dialog().dialog("widget").find(".ui-dialog-titlebar-close").hide();
        }
    }
    function closeDialogLoad() {
        if (hdestado.val() == "0") {
            $("#dialog-load").dialog("close");
        }
        else {
            $("#dialog-load-mod").dialog("close");
        }
    }
    function openDialogWait() {
        $("#dialog-wait").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
    }
</script>
<!-- SHIPPING FORM -->
<div id="dialog-form" style="display: none;">
    <p class="validateTips">
    </p>
    <p class="f13">
        Por favor, ingrese todos los datos correspondientes a la información de envio.</p>
    <fieldset class="f12">
        <table cellpadding="4" cellspacing="4" width="100%" align="center">
            <tr>
                <td align="right">
                    * Nombre (nombres y apellidos) :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtName" Width="270px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    * Número teléfonico:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPhone" Width="270px" MaxLength="12"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Número movíl:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMovil" Width="270px" MaxLength="12"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    * Dirección de envío:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtAddress" Width="270px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Continuación de dirección de envío:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBlock" Width="270px" MaxLength="30"></asp:TextBox>
                    <span class="f-small">Ejemplo: Apto, unidad, edificio, casa, barrio, bloque</span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    * Ciudad:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCity" Width="270px" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    * Departamento:
                </td>
                <td>
                    <asp:DropDownList AccessKey="o" DataTextField="Dep_Descripcion" DataValueField="Dep_Id"
                        ID="dwDptos" runat="server" Width="270px" AppendDataBoundItems="True">
                        <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:ObjectDataSource ID="odsDeptos" runat="server" SelectMethod="getAllDepartmens"
                        TypeName="www.aquarella.com.pe.bll.Departments"></asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </fieldset>
    <p>
        Toda la información aquí suministrada es con objeto de realizar la entrega de su
        pedido lo más exacto, rápido y veras posible; por favor, asegúrese de tramitarlo
        y finalizarlo correctamente.
    </p>
</div>
<!-- DIALOG CONFIRM -->
<div id="dialog-confirm" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
       <div id="popupx"> Generaremos su liquidación ; ¿desea continuar?</div></p>
</div>
<!-- DIALOG CONFIRM -->
<div id="dialog-confirm-pedido" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Generaremos su pedido; ¿desea continuar?</p>
</div>
<!-- DIALOG CONFIRM EDITAR -->
<div id="dialog-confirm-edit" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
       <div id="popupy">  Modificaremos su liquidación; ¿desea continuar?</div></p>


</div>
<!-- DIALOG WAIT -->
<div id="dialog-wait" style="display: none;" title="Procesando..">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Procesando, por favor espere.</p>
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        Aguarde un momento..
    </p>
</div>
<!-- DIALOG LOAD -->
<div id="dialog-load" style="display:none"   title="Procesando liquidación">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Su pedido se esta procesando, por favor aguarde un momento.</p>
    <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span>Recuerde que tiene la opción de realizar el pago mediante pagos online.
    </p>
    <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span><b>Se esta enviando una copia a su correo.</b>
    </p>
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        Creando liquidación y separando su mercancia ...
        </p>
</div>
<!-- DIALOG LOAD MOD-->
<div id="dialog-load-mod" style="display:none"   title="Modificanco liquidación">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Su liquidacion se esta modificando, por favor aguarde un momento.</p>
    <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span>Recuerde que tiene la opción de realizar el pago mediante pagos online.
    </p>
    <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span><b>Se esta enviando una copia a su correo.</b>
    </p>
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        modificando su liquidación y separando su mercancia ...
        </p>
</div>
<!-- DIALOG LOAD PEDIDO-->
<div id="dialog-load-pedido" style="display:none"   title="Ingresando o Actualizando su pedido">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Su esta ingresando o actualizando su pedido, por favor aguarde un momento.</p>
     <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span>Recuerde que tiene que generar su liquidacion de este pedido.
    </p>   
        <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        </p>
</div>
<div id="dialogp" style="display: none">
     <p>
    <b>Total Pares del Pedido:</b> <span id="tpedido" style="color:red;font-size:15px;font-weight:bold"></span>
          </p>  
    <br />
    <p>
    <b>Total Pares Disponible:</b> <span id="tdispo" style="color:red;font-size:15px;font-weight:bold"></span>
    <br />   
     </p>     
</div>

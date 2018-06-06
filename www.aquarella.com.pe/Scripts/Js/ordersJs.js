
/*CAMBIO DE IMAGEN*/
function changeImage(idCtrlImg, src, lblCodeImg, codeArt) {
    $('#' + lblCodeImg).text(codeArt);
    $('#' + idCtrlImg).attr("src", src);
}

/*VALIDACIONES*/
function updateTipsAlert(t) {
    alert(t);
}

function checkLengthAlert(o, n, min, max, msg) {
    if (o.val().length > max || o.val().length < min) {
        o.addClass("ui-state-error");
        o.focus();
        updateTipsAlert(msg);
        return false;
    } else {
        return true;
    }
}

//
function checkRegexpAlert(o, regexp, n) {
    if (!(regexp.test(o.val()))) {
        o.addClass("ui-state-error");
        updateTipsAlert(n);
        return false;
    } else {
        return true;
    }
}

// DOCUMENT READY
var i = 0;
var $el, $art, $brand, $color, $qty, $dwSizes, $majC, $origin, $photo, $price, $grandTotal, $subTotal, $totQty, $msge, $imgLoad, $imgAdd, $msgeGlobal,
        $descDesc, $descDescVal, $taxesDesc, $percepcion, $mtopercepcion, allFields, $btAddItems, $imgAddToOrder, $varTipoPago, $btncr, $lblpromotor, $lblestado, $dwpago ;
$(document).ready(function () {
    // Get the initial value
    $el = $('#txtCode');
    $art = $('#lblArt');
    $brand = $('#lblBrand');
    $color = $('#lblColor');
    $qty = $('#txtQty');
    $dwSizes = $('#dwSizes');
    $majC = $('#lblMaj');
    $origin = $('#lblOri');
    $photo = $('#photoItem');
    $price = $('#lblPrice');
    $grandTotal = $('#lblGrandTotal');
    $subTotal = $('#lblSubTotal');
    $totQty = $('#lblTotQty');
    $imgLoad = $('#imgLoad');
    $imgAdd = $('#imgAdd');
    $msge = $('#lblMsg');
    $descDesc = $('#lblDescDesc');
    $descDescVal = $('#lblDescDescVal');
    $taxesDesc = $('#lblTaxes');
    $percepcion = $('#lblpercepcion');
    $mtopercepcion = $('#lblmtopercepcion');
    $lblpromotor = $('#lblpromotor');
    $lblestado = $('#lblestado');
    $dwpago = $('#dwpago');
    $btncr = $('#btncr');

    allFields = $([]).add($el).add($dwSizes).add($qty).add($btncr);
    $btAddItems = $('#btAddItem');
    $imgAddToOrder = $('#imgAddToOrder');

    $varTipoPago = $('#h_numTipPago');
    $el.data('oldVal', $el.val());
    $el.focus();
    
    validanc("0");


    // User control - add articles
    loadUserControl();

    // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
    $("#dialog:ui-dialog").dialog("destroy");

    $btAddItems.button({
        icons: {
            primary: "ui-icon-check"
        },
        text: true
    });

    $("#btCreateLiquidation").button({
        icons: {
            primary: "ui-icon-cart"
        },
        text: true
    });

    // refrescar item forma de pago
    function updateitemsforma() {
        //
        var urlMethod = "ordersForm.aspx/fupdateitemforma";
        var jsonData = '{}';
        SendAjax(urlMethod, jsonData, renderTableInit);
    }
   

    // Evento dwpago 
    $dwpago.change(function () {
        //
        var formaid = $('#dwpago :selected').val();
        $("[id$='h_numTipPago']").val(formaid);
        var formaname = $('#dwpago :selected').text();
        allFields.removeClass("ui-state-error");
        getpago(formaid, formaname);
        updateitemsforma();
    });
    function getpago(idpago, formapago) {
        //Ajax
        var urlMethod = "ordersForm.aspx/get_formapago";
        var jsonData = '{strformapagoid:"' + idpago + '", strformapago:"' + formapago + '"}';
        SendAjax(urlMethod, jsonData, showpago);
    }
    // Impresion de estado
    function showpago(msg) {
        var val = msg.d;
        $("#LblPago").text(val.cov_description);

        if (val.cov_conceptid == "004") {
            $('div[id$=pnlDwCustomers]').show();
        }
        else {
            $('div[id$=pnlDwCustomers]').hide();
        }

        if (val.cov_conceptid == "007") {
            validanc("1");
            getTotals();

        }
        else {
            validanc("0");
        }
    }


    // Digit article code
    /*$el.change(function () {
    //
    $el.val($el.val().trim().replace("-", ""));
    allFields.removeClass("ui-state-error");
    var bValid = checkLengthAlert($el, "Código de artículo", 7, 8, "Código de artículo invalido, recuerde el código del artículo posee 8 caracteres.");
    if (bValid)
    getArticle($el.val());
    else
    $el.focus();
    });*/
    $el.keypress(function (event) {
        if (event.which == 13 || event.which == 9) {
            // Prevent postback event to the serever
            event.preventDefault();
            $el.val($el.val().trim().replace("-", ""));
            allFields.removeClass("ui-state-error");
            var bValid = checkLengthAlert($el, "Código de artículo", 7, 8, "Código de artículo invalido, recuerde el código del artículo posee 8 caracteres.");
            if (bValid)
                getArticle($el.val());
            else
                $el.focus();
        }
    });
    $el.on('keydown', function (e) {
        var keyCode = e.keyCode || e.which;
        if (keyCode == 9) {
            e.preventDefault();
            $el.val($el.val().trim().replace("-", ""));
            allFields.removeClass("ui-state-error");
            var bValid = checkLengthAlert($el, "Código de artículo", 7, 8, "Código de artículo invalido, recuerde el código del artículo posee 8 caracteres.");
            if (bValid)
                getArticle($el.val());
            else
                $el.focus();
        }
    });

    // Select a size
    $dwSizes.blur(function () {
        //
        var newValue = $dwSizes.val();
        if ($dwSizes.val() != null && $dwSizes.val() != '' && $dwSizes.val() != '-1' && $dwSizes.val() != 'Seleccione la talla')
            $qty.focus();
        //else
        //    $dwSizes.focus();
    });
    //
    $qty.keypress(function (event) {
        if (event.which == 13) {
            // Prevent postback event to the server
            event.preventDefault();
            valFieldsAddItem();
            $el.focus();
        }
    });
    /*$qty.change(function () {
    //
    valFieldsAddItem();
    $el.focus();
    });*/
    $btAddItems.click(function (event) {
        event.preventDefault();
        valFieldsAddItem();
    });

    $imgAddToOrder.click(function (event) {
        openDialogAddItem();
    });
    initPage();
    getproestado();
    geformapago();
    
   
});

    function validanc(credito) {
        $.ajax({
            type: "POST",
            url: 'orderformaNC.aspx/verificanc',
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                msg = msg.hasOwnProperty("d") ? msg.d : msg;


                if (msg == "0") {
                    $("#btncr").hide();
                    $('#lbltnc').html('(-) Nota de Credito:');
                }
                else
                    if (credito == "1") {
                        $("#btncr").hide();
                        $('#lbltnc').html('(-) Nota de Credito:');
                    }
                    else
                {
                    $("#btncr").show();
                    $('#lbltnc').html('(-)');
                }
            }
        });
    }

function openDialogAddItem() {
    $("#dialog-HelpForAdd").dialog({
        resizable: false,
        height: 450,
        width: 750,
        modal: true,
        closeOnEscape: false,
        buttons: {
            "Aceptar": function () {
                $(this).dialog("close");
            }
        },
        open: function (event, ui) { cleanAllFields(); },
        beforeClose: function (event, ui) { getOrderDtl(); }
    });
}

function valFieldsAddItem() {
    allFields.removeClass("ui-state-error");
    var bValid = checkLengthAlert($el, "Código de artículo", 5, 8, "Código de artículo invalido, recuerde el código del artículo posee 8 caracteres.");
    bValid = bValid && checkLengthAlert($dwSizes, "Seleccione la talla", 1, 8, "Seleccione talla.");
    bValid = bValid && checkLengthAlert($qty, "Cantidades", 1, 8, "Suministre las cantidades deseadas del artículo.");
    bValid = bValid && checkRegexpAlert($qty, /^([0-9])+$/, "El campo de unidades deseadas sólo permite números : 0-9");
    if (bValid) {
        //ddArticle($dwSizes.val(), $qty.val(), $('select[id$=ddlTipoPago] :selected').val());
        addArticle($dwSizes.val(), $qty.val(), $("[id$='h_numTipPago']").val());
        /*Verificamos para agregar premio*/
        VerificarPremio();
    }
}

function deleteRow(idRow, code, size) {
    $("#dialog-confirm-erase").dialog({
        resizable: false,
        height: 140,
        modal: true,
        buttons: {
            "Borrar linea": function () {
                deleteArticle(code, size);
                //var tr = 'tr#' + idRow;
                // remove row from table body
                //$('#dynTable').tinytbl('remove', $(tr));
                $(this).dialog("close");
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        }
    });
}

function showLoad(isShow, mges, showAdded) {
    if ($msge == null)
        return;
    $msge.text(mges);
    if (isShow)
        $imgLoad.show();
    else
        $imgLoad.hide();

    if (showAdded) {
        $imgAdd.show();
        setTimeout(function () {
            $imgAdd.hide();
            $msge.text('');
        }, 1000);
    }
}
function fupdateitemPremio() {
    var urlMethod = "ordersForm.aspx/fupdateitemPremio";
    var jsonData = '{}';
    SendAjax(urlMethod, jsonData, renderTableInit);
}

function fupdateitemoferta() {
    var urlMethod = "ordersForm.aspx/fupdateitemoferta";
    var jsonData = '{}';
    SendAjax(urlMethod, jsonData, renderTableInit);
}
function cleanForm() {
    $el.val('');
    $qty.val('');
    $dwSizes.html('');
    $art.html('');
    $brand.html('');
    $color.html('');
    $majC.html('');
    $origin.html('');
    $price.html('');
    var src = '../../Design/images/card_user.png'; 
    $photo.attr("src", src);
    $descDesc.text('');
    $descDescVal.text('');
    $price.css('text-decoration', 'none');
    $el.focus();
}

//Ajax
function SendAjax(urlMethod, jsonData, returnFunction) {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: urlMethod,
        data: jsonData,
        dataType: "json",
        //async: true,
        success: function (msg) {
            // 
            if (msg != null) {
                returnFunction(msg);
            }
        },
        error: function (xhr, status, error) {
            // Boil the ASP.NET AJAX error down to JSON.
            var err = eval("(" + xhr.responseText + ")");
            onAjaxError(err.Message);
        }
    });
}
//
function onAjaxError(errorMsg) {
    // Display the specific error raised by the server            
    showLoad(false, errorMsg, false);
    alert(errorMsg);
    $el.focus();
    afterLoad();
}
function whileLoad() {
    try {
        $el.attr("disabled", true);
        $dwSizes.attr("disabled", "disabled");
        $qty.attr("disabled", "disabled");
        $btAddItems.attr("disabled", "disabled");
    }
    catch (e)
            { }
}
function afterLoad() {
    try {
        $el.removeAttr("disabled");
        $dwSizes.removeAttr("disabled");
        $qty.removeAttr("disabled");
        $btAddItems.removeAttr("disabled");
    }
    catch (e)
            { }
}

// 0. Inicio formulario
function initPage() {
    //            
    whileLoad();
    showLoad(true, 'Iniciando módulo de pedidos..', false);
    var urlMethod = "ordersForm.aspx/intiPage";
    var jsonData = '{}';
    SendAjax(urlMethod, jsonData, renderTable);
}


//1. Consulta articulo y carga informacion y tallas
function getArticle(code) {
    //Ajax
    whileLoad();
    showLoad(true, 'Cargando información de articulo..', false);
    var urlMethod = "ordersForm.aspx/getArticle";
    var jsonData = '{code:"' + jQuery.trim(code) + '"}';
    SendAjax(urlMethod, jsonData, showArticle);
}

//2. Obtiene un objeto de tallas
function getArticleSizes() {
    //Ajax
    var urlMethod = "ordersForm.aspx/getArticleSizes";
    var jsonData = '{}';
    SendAjax(urlMethod, jsonData, loadDwSizes);
}

//obtiene la forma de pago
//-----------------------Metodo para cargar las bodegas-------------------------
function geformapago() {
    //Ajax
    whileLoad();
    showLoad(true, 'Cargando información de bodegas..', false);
    var urlMethod = "ordersForm.aspx/getformapago";
    var jsonData = '{}';
    SendAjax(urlMethod, jsonData, Loadforma);
    
}
//--------------------------------
function Loadforma(msg) {
    var dwitems = '';
    $dwpago.html('');
    $.each(msg.d, function (key, val) {
        dwitems += "<option value='"
                + val.cov_conceptid
                + "'>"
                + val.cov_description
                + "</option>";
    });
    afterLoad();
    $dwpago.html(dwitems);
    showLoad(false, '', false);

    getverificacredito();
    var _pago = $('input[id$=h_numTipPago]').val();
    if (_pago == "007")
    {
        $("#dwpago").prop("selectedIndex", 2);
        $("#btncr").hide();
        $('#lbltnc').html('(-) Nota de Credito:');
    }

    var strTipoPago = $("[id$='h_numTipPago']").val();
    $dwpago.val(strTipoPago);
  
    
        
}
//***************VERIFICA CREDITO
function getverificacredito() {
    //Ajax
    var urlMethod = "ordersForm.aspx/get_verificacredito";
    var jsonData = '{}';
    SendAjax(urlMethod, jsonData, showcredito);
}

//*************************
// Impresion de VERIFICACION DE CREDITO
function showcredito(msg) {
    var val = msg.d;
    var vpagoc = val._estadocredito;

    if (vpagoc == "1") {
        $dwpago.val('007');
        $("#btncr").hide();
        $('#lbltnc').html('(-) N.C y/o S.F:');
    }
}


//3. Adiciona un articulo al detalle de pedido
function addArticle(size, qty, TipoPago) {
    //Ajax
    $msgeGlobal = 'El artículo ha sido adicionado a su pedido.';
    //var strtipoPago = $dwpago.val();
    //$("[id$='h_numTipPago']").val(strtipoPago);
    var urlMethod = "ordersForm.aspx/addArticle";
    var jsonData = '{size:"' + size + '", qty:' + qty + ' , varTipoPago:"' + TipoPago + '"}';//,tipoPago:"' + strtipoPago + '"}';
    SendAjax(urlMethod, jsonData, renderTableInit);  //addArticlesTable);   
    cleanForm();
    /*en esta opcion vamos a ver si hay ofertas*/
    fupdateitemoferta();0
    /**********/
}

//4. Adiciona un articulo PREMIO al detalle de pedido
function addArticlePremio(codigo, size, qty, TipoPago, premId) {
    //Ajax
    $msgeGlobal = 'El artículo ha sido adicionado a su pedido.';
    //var strtipoPago = $dwpago.val();
    //$("[id$='h_numTipPago']").val(strtipoPago);
    var urlMethod = "ordersForm.aspx/addArticlePremio";
    var jsonData = '{size:"' + size + '",  code:' + codigo + ' , qty:' + qty + ' , PremId:"' + premId + '", varTipoPago:"' + TipoPago + '"}';//,tipoPago:"' + strtipoPago + '"}';
    SendAjax(urlMethod, jsonData, renderTableInit);  //addArticlesTable);   
    cleanForm();
    /*en esta opcion vamos a ver si hay ofertas*/
    //fupdateitemPremio(); 
    /**********/
}
function VerificarPremio() {
    
    var urlMethod = "ordersForm.aspx/verificarPremio";
    var jsonData = '{}';
    SendAjax(urlMethod, jsonData, invocarAddArticulo);

}

function invocarAddArticulo(msg) {
        
    var Premtalla = "";
    var premId = "";
    var strcodigo = "";
    var strtalla = "";
    $.each(msg.d, function (key, val) {

        strcodigo = val.articulo;
        strtalla = val.talla;
        premId = val.premId;
        return false;
    });

    if (premId != "") {
        //getArticle(strcodigo);
        addArticlePremio(strcodigo, strtalla, '1', $("[id$='h_numTipPago']").val(), premId);
        cleanForm();
    }

}

//Eliminar un item adicionado
function deleteArticle(code, size) {
    //Ajax
    $msgeGlobal = 'El artículo ' + code + 'ha sido ELIMINADO de su pedido.';
    var urlMethod = "ordersForm.aspx/deleteArticle";
    var jsonData = '{code:"' + code + '", size:"' + size + '"}';
    SendAjax(urlMethod, jsonData, renderTableInit);  //addArticlesTable);
    cleanForm();
    deletePremio();
    /*en esta opcion vamos a refrescar*/
    fupdateitemoferta();
}

function deletePremio() {
    $msgeGlobal = 'El artículo ha sido ELIMINADO de su pedido.';
    var urlMethod = "ordersForm.aspx/deletePremio";
    var jsonData = '{}';
    SendAjax(urlMethod, jsonData, renderTableInit);  //addArticlesTable);
    cleanForm();
}

    // Consultar el detalle de pedido
    function getOrderDtl() {
        //Ajax
        $msgeGlobal = 'Cargando su pedido.';
        var urlMethod = "ordersForm.aspx/getOrderDtl";
        var jsonData = '{}';
        SendAjax(urlMethod, jsonData, renderTableInit);  //addArticlesTable);
    }

    // Actualizar cantidades de un item adicionado al pedido
    function updateItemQtys(code, size, qty) {
     
        //Ajax
        $msgeGlobal = 'Actualizando.';
        var urlMethod = "ordersForm.aspx/updateItem";
        var jsonData = '{code:"' + code + '", size:"' + size + '", qty:' + qty + '}';
        SendAjax(urlMethod, jsonData, renderTableInit);

        cleanForm();
        /*en esta opcion vamos a refrescar*/
        fupdateitemoferta();
    }

    // Calculo de totales
    function getTotals() {
         //Ajax
        var urlMethod = "ordersForm.aspx/getTotals";
        var jsonData = '{}';
        SendAjax(urlMethod, jsonData, showTotals);
    }

    //pintar promotor y estado
    function getproestado() {
        //Ajax
        var urlMethod = "ordersForm.aspx/get_promotorestado";
        var jsonData = '{}';
        SendAjax(urlMethod, jsonData, showpromoesta); 
    }

    // Consulta de stock disponible de un item adicionado al pedido
    function getStockArticleNoDialog(art, artSize, artQty, ctrlImgStock) {
        //Ajax
        var urlMethod = "ordersForm.aspx/getItemIconStock";
        var jsonData = "{ article: '" + art + "', size : '" + artSize + "',qty : " + artQty + ",ctrlImgStock:'" + ctrlImgStock + "'}";
        SendAjax(urlMethod, jsonData, iconStock);
    }

    // Consulta y muestra informacion del stock de un articulo en un dialog
    function getStockArticle(art, artSize, artQty, ctrlImgStock) {
        //Ajax
        var urlMethod = "ordersForm.aspx/getArticleStock";
        var jsonData = "{ article: '" + art + "', size : '" + artSize + "',qty : " + artQty + ",ctrlImgStock:'" + ctrlImgStock + "'}";
        $("#dialog-InfoStock").dialog({ width: 700,
            height: 450,
            title: 'Información de Stock disponible y artículos sugeridos',
            modal: true,
            position: 'center',
            open: function (event, ui) {
                // Loading.. image
                $("#content_div").html('<img src="../../Design/images/ajax_loader_face.gif" />');
                SendAjax(urlMethod, jsonData, infoStock);
            },
            buttons: {
                "Aceptar": function () {
                    $(this).dialog("close");
                }
            }
        });
    }

    function infoStock(msg) {
        var val = msg.d;
        $("#content_div").html(val._tableHtml);
        // Scale Object Image
        $("#imgArtDesc").cjObjectScaler({
            method: "fit",
            fade: 550
        });
        $("#" + val._ctrlImgStock).attr("src", val._iconStock);
    }

    function iconStock(msg) {
        var val = msg.d;
        $("#" + val._ctrlImgStock).attr("src", val._iconStock);
    }

    // Impresion de estado
    function showpromoesta(msg){
        var val = msg.d;
        $('#lblpromotor').html(val._namecompleto);
        $('#lblestado').html(val._estadoliqui);
        $('#lblPremio').html(val._premio);
        if (val._premio=="")
            document.getElementById("etqPremio").style.display = "none";
        else
            document.getElementById("etqPremio").style.display = "";

        if (val._estadoboton > 0) {
            $('[id$=btSaveExit]').attr('disabled', 'true');
            $('[id$=btSave]').attr('disabled', 'true');
            $("#btCreateLiquidation").hide();
            $("#btsaveorderexit").attr("disabled", "disabled");
            $("#btsaveorder").attr("disabled", "disabled");
        }
        else {
            $("#btmodiliquidacion").hide();
        }
    }


    //function showTotals(msg) {
    //    var val = msg.d;
    
    //    $('#lblTotQty').html(val._qtys);
    //    $('#lblGrandTotal').html(val._grandTotalDesc);
    //    $('#lblSubTotal').html(val._subTotalDesc);
    //    $('#lblTaxes').html(val._taxesDesc);
    //    $('#lblpercepcion').html(val._percepciondesc);
    //    $('#lblmtopercepcion').html(val._mtopercepciondesc);
    //    $('#lblnc').html(val._mtoncreditodesc);
    //    $("input[id$='txtValue']").val(val._mtopercepcion).val();
    //    //$subTotal.text(val._subTotalDesc);
    //}

    // Impresion de informacion de articulo
    function showArticle(msg) {
        $.each(msg.d, function (key, val) {
            var idRow = "row_" + i;
            $art.html(val._artName);
            $brand.html(val._brand);
            $color.html(val._color);
            $majC.html(val._majorCat + '> ' + val._cat);
            if (val._originDesc != null && val._originDesc != '')
                $origin.html('*' + val._originDesc);
            else
                $origin.html('');
            $price.html(val._priceDesc);
            //$price.html(val._priceigvDesc);
            // Dissccounts
            if (val._dscto > 0) {
                //
                if (val._dsctoPerc > 0) {
                    //
                    $descDesc.html('Descuento:');
                    $descDescVal.html((val._dsctoPerc * 100).toString() + '%');
                }
                else {
                    $descDesc.html('Ahora:');
                    $descDescVal.html(val._dsctoValeDesc);
                    $price.css('text-decoration', 'line-through');
                }
            }
            var src = val._uriPhoto; ///$photo.attr("src").match(/[^\.]+/) + val._uriPhoto;     

            var srcBrand = val._brandImg;
            $photo.error(function () {
                if (srcBrand != '')
                    $photo.attr("src", srcBrand);
            }).attr("src", src);

            // Scale Image
            //        $photo.cjObjectScaler({
            //            method: "fit",
            //            fade: 550
            //        });

        });
        // Ajax for sizes
        getArticleSizes();
    }
 
    // Creacion de un select con las tallas del articulo
    function loadDwSizes(msg) {
        var dwitems = '';
        $dwSizes.html('');
        dwitems += "<option value=''>Seleccione la talla</option>";
        $.each(msg.d, function (key, val) {
            dwitems += "<option value='"
                    + val._size
                    + "'>"
                    + val._size_des
                    + "</option>";
        });
        afterLoad();
        $dwSizes.html(dwitems);
        $dwSizes.focus();
        showLoad(false, '', false);
    }

    // GRID
    //
    function buildGrid() {
        SendAjax('ordersForm.aspx/getOrderDtl', '', renderTable);
    }

    function renderTableInit(msg) {
        $("#list4").jqGrid('clearGridData');
        $.each(msg.d, function (key, val) {
            var idImg = "imgStock_" + i;
            var imgCellSt = '<img id="' + idImg + '" src="../../Design/images/Botones/b_info.png" onclick=\'javascript:getStockArticle("' + val._code + '","' + val._size + '",' + val._qty + ',"' + idImg + '")\'/>';
            var imgCellDel = '<img src="../../Design/images/Botones/delete_off.png" onclick=\'javascript:deleteRow("' + i + '","' + val._code + '","' + val._size + '")\' style="padding:0 3px 0 3px;"/>';
            if (val._premio == "S") {
               
                imgCellDel = '';
            }

            jQuery("#list4").jqGrid('addRowData', i + 1, { _code: val._code,
                _artName: val._artName,
                _brand: val._brand,
                _color: val._color,
                _size: val._size,
                _qty: val._qty,
                _priceDesc: val._priceDesc,
                _commissionDesc: val._commissionDesc,
                _dsctoDesc: val._dsctoDesc,
                _lineTotDesc: val._lineTotDesc,
                _imgSt: imgCellSt,
                _imgDel: imgCellDel,
                _premio: val._premio
            });
            getStockArticleNoDialog(val._code, val._size, val._qty, idImg);
            i = i + 1;
        });

        getTotals();
    }

    function renderTable(msg) {
        var i = 1;
      
        /*Habilita la edicion del campo de cantidades a nivel local, y luego pasa por Ajax */
        var lastSel,
                onclickSubmitLocal = function (options, postdata) {
            
                var $this = $(this), grid_p = this.p,
                idname = grid_p.prmNames.id,
                grid_id = this.id,
                id_in_postdata = grid_id + "_id",
                rowid = postdata[id_in_postdata],
                addMode = rowid === "_empty";
               /*oldValueOfSortColumn,
                new_id,
                tr_par_id,
                colModel = grid_p.colModel,
                cmName,
                iCol,
                cm;*/ 
                $this.jqGrid("setRowData", rowid, postdata);
                    
                 updateItemQtys(postdata._code, postdata._size, parseInt(postdata._qty));
                  
                    if ((addMode && options.closeAfterAdd) || (!addMode && options.closeAfterEdit)) {
                        // close the edit/add dialog
                     
                            $.jgrid.hideModal("#editmod" + grid_id, {
                                gb: "#gbox_" + grid_id,
                                jqm: options.jqModal,
                                onClose: options.onClose
                            });

                    }
                    options.processing = true;
                    return {};
                },
                editSettings = {
                    //recreateForm: true,
                    jqModal: false,
                    reloadAfterSubmit: false,
                    closeOnEscape: true,
                    savekey: [true, 13],
                    closeAfterEdit: true,
                    onclickSubmit: onclickSubmitLocal
                };

        // Definicion de Grilla
        jQuery("#list4").jqGrid({
            datatype: "local", //'JSON'
            colNames: ['Referencia', 'Articulo', 'Marca', 'Color', 'Talla', 'Cantidades', 'Precio', 'Comisión', 'Descuento', 'Total Linea', '', ''],
            colModel: [
                    { name: '_code', index: '_code', width: 90, editoptions: { readonly: true }, editable: true },
                    { name: '_artName', index: '_artName', width: 90, editoptions: { readonly: true }, editable: true },
                    { name: '_brand', index: '_brand', width: 100, editoptions: { readonly: true }, editable: true },
                    { name: '_color', index: '_color', width: 80, align: "right", editoptions: { readonly: true }, editable: true },
                    { name: '_size', index: '_size', width: 50, align: "right", sorttype: "float", editoptions: { readonly: true }, editable: true },
                    { name: '_qty', index: '_qty', width: 80, align: "center", sorttype: "float", editable: true, editrules: { number: true, required: true} },
                    { name: '_priceDesc', index: '_priceDesc', width: 80, align: "right" },
                    { name: '_commissionDesc', index: '_commissionDesc', width: 80, align: "right" },
                    { name: '_dsctoDesc', index: '_dsctoDesc', width: 80, align: "right" },
                    { name: '_lineTotDesc', index: '_lineTotDesc', width: 80, align: "right" },
                    { name: '_imgSt', index: '_imgSt', align: "center", width: 30 },
                    { name: '_imgDel', index: '_imgDel', align: "center", width: 30 }
            ],
            rowNum: 20,
            pager: '#pager2',
            //rowList: [10, 20, 30]
            rownumbers: true,
            gridview: true,
            forceFit: true,
            sortname: '_code',
            viewrecords: true,
            sortorder: "desc",
            autowidth: true,
            height: 250,
            caption: "Items Adicionados Al Pedido Borrador",
            editurl: 'clientArray',
            cellsubmit: 'clientArray',
            ondblClickRow: function (rowid, ri, ci) {
                var $this = $(this), p = this.p;
                if (p.selrow !== rowid) {
                    // prevent the row from be unselected on double-click
                    // the implementation is for "multiselect:false" which we use,
                    // but one can easy modify the code for "multiselect:true"
                    $this.jqGrid('setSelection', rowid);
                }
              
                $this.jqGrid('editGridRow', rowid, editSettings);
               
            },
            onSelectRow: function (id) {
                if (id && id !== lastSel) {
                    // cancel editing of the previous selected row if it was in editing state.
                    // jqGrid hold intern savedRow array inside of jqGrid object,
                    // so it is safe to call restoreRow method with any id parameter
                    // if jqGrid not in editing state
                    if (typeof lastSel !== "undefined") {
                        $(this).jqGrid('restoreRow', lastSel);
                    }
                    lastSel = id;
                }
            }
            // In line Edit: presenta problemas por eso se inactiva
            /*
            //cellEdit: true,
            afterSaveCell: function (rowid, name, val, iRow, iCol) {
            if (name == '_qty') {
            var qtyVal = jQuery("#celltbl").jqGrid('getCell', rowid, iCol + 1);
            var row = jQuery("#list4").getRowData(rowid);
            updateItemQtys(row._code, row._size, parseInt(val));
            //jQuery("#celltbl").jqGrid('setRowData', rowid, { total: parseFloat(val) + parseFloat(taxval) });
            }
            }*/
            // json object
            //datastr: msg.d
        });
        jQuery("#list4").jqGrid('navGrid', '#pager2', { edit: true, add: false, del: false }, editSettings);

        // Keyboard response
        // Bind the navigation and set the onEnter event
        jQuery("#list4").jqGrid('bindKeys', { "onEnter": function (rowid, ri, ci) {
            var $this = $(this), p = this.p;
            if (p.selrow !== rowid) {
                // prevent the row from be unselected on double-click
                // the implementation is for "multiselect:false" which we use,
                // but one can easy modify the code for "multiselect:true"
                $this.jqGrid('setSelection', rowid);
            }
            
            $this.jqGrid('editGridRow', rowid, editSettings);
        }
        });

        jQuery("#list4").jqGrid('setGroupHeaders', { useColSpanStyle: false,
            groupHeaders: [{ startColumnName: '_code', numberOfColumns: 6, titleText: '<em>Información Item</em>' },
                { startColumnName: '_priceDesc', numberOfColumns: 4, titleText: '<em>Precios y totales</em>'}]
        });


        $.each(msg.d, function (key, val) {
            var idImg = "imgStock_" + i;
            var imgCellSt = '<img id="' + idImg + '" alt="Consulta de disponibilidad de item" src="../../Design/images/Botones/b_info.png" onclick=\'javascript:getStockArticle("' + val._code + '","' + val._size + '",' + val._qty + ',"' + idImg + '")\'/>';
            var imgCellDel = '<img alt="Eliminar item del pedido" src="../../Design/images/Botones/delete_off.png" onclick=\'javascript:deleteRow("' + i + '","' + val._code + '","' + val._size + '")\' style="padding:0 3px 0 3px;"/>';

            if (val._premio == "S") {
                imgCellDel = '';
            }

            jQuery("#list4").jqGrid('addRowData', i + 1, { _code: val._code,
                _artName: val._artName,
                _brand: val._brand,
                _color: val._color,
                _size: val._size,
                _qty: val._qty,
                _premio: val._premio,
                _priceDesc: val._priceDesc,
                _commissionDesc: val._commissionDesc,
                _dsctoDesc: val._dsctoDesc,
                _lineTotDesc: val._lineTotDesc,
                _imgSt: imgCellSt,
                _imgDel: imgCellDel
            });
            getStockArticleNoDialog(val._code, val._size, val._qty, idImg);

            if (val._premio == "S") {
                $('#lblPremio').html(val._premioDesc);
            }
            
            i = i + 1;
        });

        afterLoad();
        showLoad(false, '', false);

        getTotals();

        
    }
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucAddItemsForm.ascx.cs"
    Inherits="www.aquarella.com.pe.UserControl.ucAddItemsForm" %>
<link href="../../Styles/style_others.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var autocompleteData,
        $dwPages,
        $dwItems,
        $hdCatalogId,
        $btSearch,
        $btLoad,
        $artd,
        $colord,
        $priced,
        $coded,
        $majCd,
        $photod, $dwSizesd, $btAddItemOrder, $txtUnits, allFields, $lblSelDesc, $lblOriginD, $lblBrandDT, $minus, $plus;

    //$(document).ready(function () {
    function loadUserControl() {
        $photod = $('#photoItemD');
        $dwPages = $('#dwPages');
        $dwItems = $('#dwItems');
        $hdCatalogId = $('#hdCatalogId');
        $btSearch = $('#btSearch');
        $btLoad = $('#btLoad');
        $artd = $('#lblArtDialog');
        $colord = $('#lblColorD');
        $priced = $('#lblPriceD');
        $majCd = $('#lblMajD');
        $coded = $('#lblCodeDialog');
        $dwSizesd = $('#dwSizesD');
        $btAddItemOrder = $('#btAddItemOrder');
        $txtUnits = $('#txtUnits');
        $lblSelDesc = $('#lblSelDesc');
        $lblOriginD = $('#lblOriginD');
        $lblBrandDT = $('#lblBrandDT');
        $minus = $('#minus');
        $plus = $('#plus');

        $btAddItemOrder.button({
            icons: {
                primary: "ui-icon-cart"
            },
            text: true
        });

        $btSearch.button({
            icons: {
                primary: "ui-icon-search"
            },
            text: false
        });

        $btLoad.button({
            icons: {
                primary: "ui-icon-circle-check"
            },
            text: false
        });

        // Select a page
        $btSearch.click(function (event) {
            //            
            event.preventDefault();
            validateFields(2);
            whileLoadD();
        });

        $minus.click(function (event) {
            //            
            event.preventDefault();
            incrementValue(-1);
        });

        $plus.click(function (event) {
            //            
            event.preventDefault();
            incrementValue(1);
        });
        $btLoad.click(function (event) {
            //
            whileLoadD();
            event.preventDefault();
            validateFields(3);
        });

        $btAddItemOrder.click(function (event) {
            event.preventDefault();
            validateFields(1);
        });

        $(".searchinput").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ordersForm.aspx/getPredictions",
                    data: "{'keywordStartsWith':'" + request.term + "'}",
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        // here I will store list of all cities which will be passed to autocomplete widget
                        var autocompleteOutput = [];

                        // looping to get all data returned by the webservice
                        $.each(data.d, function (index, catalog) {
                            autocompleteOutput.push({
                                value: catalog._description,
                                label: catalog._descAdd,
                                desc: catalog._description,
                                pages: catalog._pages,
                                id: catalog._idCatalog
                                //icon: "jquery_32x32.png"
                            });
                            //catalog._description;
                        }); // Fin each

                        // passing all the available options to autocomplete widget
                        response(autocompleteOutput);

                        // store results in global variable, it will be needed later to get the rest of details
                        // for selected option
                        autocompleteData = data.d;
                    },
                    error: function (result) {
                        alert("Unexpected errors, we were unable to load data");
                    }
                }); // Fin Ajax
            },
            minLength: 2,
            focus: function (event, ui) {
                $("#txtCatalog").val(ui.item.desc);
                return false;
            },
            select: function (event, ui) {
                loadDwPages(ui.item.pages);
                $("#txtCatalog").val(ui.item.value);
                $hdCatalogId.val(ui.item.id);
                return false;
            },
            change: function (event, ui) {
                //var selectedCity = ui.item ? selectedCity = ui.item.value : selectedCity = $("#txtCatalog").val();
                //$("#txtCatalog").val(ui.item.value);
                /*var matchingElementsArray = $.grep(autocompleteData, function (item) { return item.value == selectedCity; });
                if (matchingElementsArray[0]) {

                }
                else {
                }*/
            }
        }).data("autocomplete")._renderItem = function (ul, item) {
            return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<a>" + item.label + "<br>" + item.desc + "</a>")
				.appendTo(ul);
        };
    }
    //});  
</script>
<script type="text/javascript">

    function validateFields(type) {
        if (type == 1) {
            allFields = $([]).add($dwSizesd).add($txtUnits);
            allFields.removeClass("ui-state-error");
            var bValid = checkLengthAlert($dwSizesd, "Seleccione la talla", 1, 8, "Seleccione talla.");
            bValid = bValid && checkLengthAlert($txtUnits, "Cantidades", 1, 8, "Suministre las cantidades deseadas del artículo.");
            bValid = bValid && checkRegexpAlert($txtUnits, /^([0-9])+$/, "El campo de unidades sólo permite números : 0-9");
            if (bValid) {
                addItemToOrder($coded.html(), $dwSizesd.val(), $txtUnits.val());
                //addArticle($dwSizes.val(), $qty.val());
            }
            else
                afterLoadD();
        }
        else if (type == 2) {
            allFields = $([]).add($dwPages);
            allFields.removeClass("ui-state-error");
            var bValid = checkLengthAlert($dwPages, "Seleccione la Página", 1, 8, "Seleccione Página.");
            if (bValid) {
                getItemsByCatalog($hdCatalogId.val(), $dwPages.val());
            }
            else
                afterLoadD();
        }
        else if (type == 3) {
            allFields = $([]).add($dwItems);
            allFields.removeClass("ui-state-error");
            var bValid = checkLengthAlert($dwItems, "Seleccione item", 1, 8, "Seleccione item.");
            if (bValid) {
                showPath();
                loadDtlArticle($dwItems.val());
                getItemSizes($dwItems.val());
            }
            else
                afterLoadD();
        }
    }

    function whileLoadD() {
        try {
            $('#txtCatalog').attr("disabled", true);
            $dwPages.attr("disabled", "disabled");
            $dwItems.attr("disabled", "disabled");
            $btSearch.attr("disabled", "disabled");
            $btLoad.attr("disabled", "disabled");
            $dwSizesd.attr("disabled", "disabled");
        }
        catch (e)
            { }
    }
    function afterLoadD() {
        try {
            $('#txtCatalog').removeAttr("disabled");
            $dwPages.removeAttr("disabled");
            $dwItems.removeAttr("disabled");
            $btSearch.removeAttr("disabled");
            $btLoad.removeAttr("disabled");
            $dwSizesd.removeAttr("disabled");
        }
        catch (e)
            { }
    }

    function showPath() {
        $lblSelDesc.html($('#txtCatalog').val() + ' > ' + $("#dwPages option:selected").text() + ' > ' + $("#dwItems option:selected").text());
    }

    function loadDwPages(noPages) {
        var dwitems = '';
        $dwPages.html('');
        dwitems += "<option value=''>Página</option>";
        for (var i = 1; i <= noPages; i++) {
            dwitems += "<option value='"
                + i
                + "'>Pág."
                + i
                + "</option>";
        }
        $dwPages.html(dwitems);
        //$dwPages.focus();
    }

    function loadDwItems(msg) {
        var dwitems = '';
        var stock;
        $dwItems.html('');
        dwitems += "<option value=''>Artículos</option>";
        $.each(msg.d, function (key, val) {
            if (val._units <= 0)
                stock = 'style="color:#B40404;"';
            else
                stock = 'style="color:#38610B;"';

            dwitems += "<option value='"
                + val._code
                + "' " + stock + " >"
                + val._code + ' - ' + val._artName + ' (' + val._color + ')'
                + "</option>";
        });
        $dwItems.html(dwitems);
        afterLoadD();
        $dwItems.focus();
    }

    function getItemsByCatalog(code, page) {
        //Ajax        
        var urlMethod = "ordersForm.aspx/getItemsByCatalog";
        var jsonData = '{idCatalog:"' + jQuery.trim(code) + '",page:"' + jQuery.trim(page) + '"}';
        SendAjax(urlMethod, jsonData, loadDwItems);
    }

    function loadDtlArticle(code) {
        //Ajax        
        var urlMethod = "ordersForm.aspx/loadDtlArticle";
        var jsonData = '{code:"' + jQuery.trim(code) + '"}';
        SendAjax(urlMethod, jsonData, showInfoArticle);
    }

    function showInfoArticle(msg) {
        var val = msg.d;
        $coded.html(val._code);
        $lblBrandDT.html(val._brand);
        $artd.html(val._artName);
        $colord.html(val._color);
        $majCd.html(val._majorCat + ' / ' + val._cat + ' / ' + val._subcat);
        if (val._originDesc != null && val._originDesc != '')
            $lblOriginD.html('*' + val._originDesc);
        else
            $lblOriginD.html('');
        $priced.html(val._priceDesc);
        $txtUnits.val("1");

        var src = val._uriPhoto; ///$photo.attr("src").match(/[^\.]+/) + val._uriPhoto;
        var srcBrand = val._brandImg;
        $photod.error(function () {
            if (srcBrand != '')
                $photod.attr("src", srcBrand);
        }).attr("src", src);
        // Scale Image
        $photod.cjObjectScaler({
            method: "fit",
            fade: 550
        });
        afterLoadD();
    }


    function getItemSizes(code) {
        //Ajax
        var urlMethod = "ordersForm.aspx/getItemSizes";
        var jsonData = '{code:"' + code + '"}';
        SendAjax(urlMethod, jsonData, loadDwSizesd);
    }

    function addItemToOrder(code, size, qty) {
        //Ajax
        var urlMethod = "ordersForm.aspx/addItemToOrder";
        var jsonData = '{code:"' + code + '", size:"' + size + '", unit:' + qty + '}';
        SendAjax(urlMethod, jsonData, itemAdded);
        //cleanForm();
    }

    function itemAdded(msg) {
        $('#lblMsgD').html('Adicionando item...');
        setTimeout(function () {
            $('#lblMsgD').html('Item adicionado.');
            setTimeout(function () { $('#lblMsgD').html(''); }, 2000);
        }, 1000);

        //cleanFieldsAfterAdd();

        getItemsByCatalog($hdCatalogId.val(), $dwPages.val());
    }

    function loadDwSizesd(msg) {
        var dwitems = '', stock;
        $dwSizesd.html('');
        dwitems += "<option value=''>Seleccione talla</option>";
        $.each(msg.d, function (key, val) {
            if (val._qty <= 0)
                stock = 'style="color:#B40404;"';
            else
                stock = 'style="color:#38610B;"';
            dwitems += "<option value='"
                + val._size
                + "' " + stock + " >"
                + val._size
                + "</option>";
        });
        $dwSizesd.html(dwitems);
        $dwSizesd.focus();
    }

    function cleanAllFields() {
        $lblBrandDT.html('');
        $coded.html('');
        $colord.html('');
        $dwItems.html('');
        $dwPages.html('');
        $lblBrandDT.html('');
        $lblOriginD.html('');
        $dwSizesd.html('');
        $priced.html('');
        $majCd.html('');
        $('#txtCatalog').val('');
        $('#txtCatalog').focus();
        $photod.attr("src", "");
    }

    function cleanFieldsAfterAdd() {
        $coded.html('');
        $colord.html('');
        $dwSizesd.html('');
        $priced.html('');
        $majCd.html('');
        $dwItems.focus();
        $lblOriginD.html('');
    }

</script>
<script type="text/javascript">
    function incrementValue(e) {
        $txtUnits.val(Math.max(parseInt($txtUnits.val()) + e, 0));
        return false;
    }
</script>
<table width="100%">
    <tbody>
        <tr>
            <td>
                <label for="txtCatalog">
                    Escriba el nombre del catálogo:
                </label>
                <input id="txtCatalog" type="text" class="searchinput" style="width: 250px" />
                <input type="hidden" id="hdCatalogId" />
            </td>
            <td>
                <label for="txtCatalog">
                    Sel. página:
                </label>
                <select id="dwPages" style="width: 100px;">
                    <option></option>
                </select>
            </td>
            <td valign="bottom">
                <button id="btSearch">
                    Búscar</button>
            </td>
            <td colspan="2">
                <label for="dwItems">
                    Seleccione artículo:
                </label>
                <select id="dwItems" style="width: 250px;">
                    <option></option>
                </select>
            </td>
            <td valign="bottom">
                <button id="btLoad">
                    Cargar</button>
            </td>
        </tr>
    </tbody>
</table>
<hr />
<table width="100%">
    <tbody>
        <tr>
            <td colspan="2">
                <table cellpadding="0" cellpadding="0" width="100%">
                    <tr>
                        <td valign="middle" style="width: 300px;">
                            <div style="width: 270px; max-width: 270px; height: 270px; padding-left: 19px; text-align: left;
                                border: 1px solid #E8E8E8; position: relative;">
                                <img src="../../Design/images/card_user.png" id="photoItemD" alt="Fotografia item"
                                    title="Fotografia Item" />
                            </div>
                        </td>
                        <td valign="top">
                            <table cellpadding="2" cellspacing="2" width="100%">
                                <tbody>
                                    <tr>
                                        <td style="font-weight: bold" class="fsal f12">
                                            <label id="lblBrandDT">
                                            </label>
                                            <label id="lblArtDialog">
                                            </label>
                                        </td>
                                        <td align="right" class="fsal">
                                            Ref.#
                                            <label id="lblCodeDialog">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        <b>Color:</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <label id="lblColorD">
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <b>Talla:</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <select id="dwSizesD" style="width: 150px;">
                                                        </select>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <b>Unidades:</b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: left;">
                                                        <div class="quantity quantity-wrap buttons_added">
                                                            <span id="minus" class="minus"></span>
                                                            <input type="text" name="txtUnits" id="txtUnits" value="1" maxlength="4" />
                                                            <span id="plus" class="plus"></span>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="font-weight: bold;" valign="top">
                                            <table>
                                                <tr>
                                                    <td colspan="2">
                                                        <label id="lblOri">
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="f13">
                                                        <label id="lblDescDesc">
                                                        </label>
                                                    </td>
                                                    <td class="fsal f13">
                                                        <label id="lblDescDescVal">
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <label id="lblOriginD">
                                                        </label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-weight: normal; font-size: 10px; color: #38610B;">
                                                        <label id="lblMsgD">
                                                        </label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <b>Categoría: </b>
                                            <label id="lblMajD">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table width="100%" style="border: 1px solid silver; padding: 10px;" border="0" cellpadding="0"
                                                cellspacing="0">
                                                <tr>
                                                    <td class="f12" style="border-right: 1px solid silver;">
                                                        <b>Precio:</b><br />
                                                        <label class="fsal" id="lblPriceD">
                                                        </label>
                                                    </td>
                                                    <td align="right" class="f12">
                                                        <button id="btAddItemOrder">
                                                            Adicionar al pedido</button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <label id="lblSelDesc">
                </label>
            </td>
        </tr>
    </tbody>
</table>

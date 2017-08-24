<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" ValidateRequest="false"
    CodeBehind="ordersForm.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.ordersForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script src="../../Scripts/JqGrid/grid.locale-es.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
    <link href="../../Styles/ui.jqgrid.css" type="text/css" rel="stylesheet" />
    <script src="../../Scripts/ImgScale/jquery.cj-object-scaler.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Js/ordersJs.js" type="text/javascript"></script>
   

    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>

    <meta http-equiv="expires" content="0"/>
 
    <meta http-equiv="Cache-Control" content="no-cache"/>
 
    <meta http-equiv="Pragma"  content="no-cache"/>

    <%-- <script type="text/javascript">
        var btncredito;
         $(document).ready(function () {
            var btncr = $("#btncr");
            btncr.click(function (event) {
                        btncredito = $("input[id$='btncredito']");
                        btncredito.click();
            });
        });  
    </script>--%>    
    <script type="text/javascript">
        var valida;
        $(function () {
            var bnaceptar = $("input[id$='btnaceptar']");
            allFields = $([]).add(bnaceptar);
            var pagetitle = $(this).attr("Seleccione Nota de Credito");

            $("#dialog-NC").dialog({
                autoOpen: false,
                height: 400,
                width: 350,
                modal: true,
                title: pagetitle,
                resizable: false,
                autoResize: true,
                buttons: {
                    "Aceptar": function () {
                        var bValid = true;
                        var valida;
                        allFields.removeClass("ui-state-error");
                        //                        bValid=search();
                        search();
                        //                        if (bValid) {
                        //                            $(this).dialog("close");
                        //                        }
                    },
                    Cancelar: function () {
                        $(this).dialog("close");
                    }
                },
                close: function () {
                    allFields.val("").removeClass("ui-state-error");
                }
            });

            $("#btncr")
      .button()
      .click(function () {
          $("#dialog-NC").dialog("open");
      });
        });

  function search() {
  var val=true
  $.ajax({
      type: "POST",
      url: '<%= ResolveUrl("orderformaNC.aspx/ajaxupdatefunction") %>',
      data: "{}",
      contentType: "application/json",
      success: function (msg) {
          msg = msg.hasOwnProperty("d") ? msg.d : msg;
          if (msg == "0") {
              getTotalsoptional(msg);
              $("#dialog-NC").dialog("close");  
//              alert('Seleccione un items por favor.');
//              updateTips("Length of  must be between " +
//                              " and .");
          }
          else {
              getTotalsoptional(msg);
              $("#dialog-NC").dialog("close");
              // val=true;
          }
      }
  });
      return val;
  }

  function getTotalsoptional(vnc) {
      //Ajax
      var urlMethod = "ordersForm.aspx/getTotalsoptional";
      var jsonData = '{vnc:"' + vnc + '"}';
      SendAjax(urlMethod, jsonData, showTotals);
  }
  // Impresion de totales
  function showTotals(msg) {
      var val = msg.d;
      $('#lblTotQty').html(val._qtys);
      $('#lblGrandTotal').html(val._grandTotalDesc);
      $('#lblSubTotal').html(val._subTotalDesc);
      $('#lblTaxes').html(val._taxesDesc);
      $('#lblpercepcion').html(val._percepciondesc);
      $('#lblmtopercepcion').html(val._mtopercepciondesc);
      $('#lblnc').html(val._mtoncreditodesc);
      $("input[id$='txtValue']").val(val._mtopercepcion).val();

      //$subTotal.text(val._subTotalDesc);
  }

//  function updateFunctionAjax() {
//      var valor;
//      $.ajax({
//          type: "POST",
//          url: "orderformaNC.aspx/ajaxupdatefunction",
//          contentType: "application/json; charset=utf-8",
//          dataType: "json",
//          success: function (result) {
//              valor=result.d;
//          }
//      });
//      return valor;
//  }
    </script>
    <style type="text/css">
        .style1
        {
            width: 367px;
        }
        .style4
        {
            width: 536px;
        }
        .style6
        {
            width: 1288px;
        }
        .style9
        {
            width: 117px;
        }
        .style10
        {
            width: 724px;
        }
        .style11
        {
            width: 642px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Pedidos borrador
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Cree su pedido borrador, mediante el suministro de los codigos de artículos, o solicite
    ayuda del ubicador de artículo por catalogo y número de página; use la tecla Enter
    para la adición de sus referencias.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table style="width:100%;">
            <tr style="color: #000000;">
                                        <td align="left" 
                                            style="border-top: 1px solid silver; padding-left: 5px; font-weight: bold;" class="fsal f13"
                                          >
                                        <b>Promotor: </b>
                                        </td>
                                        <td align="left" 
                                            
                                            style="border-top: 1px solid silver; padding-left: 5px; font-weight: bold;" class="style6"
                                           >
                                            <b style="color: #000000; font-size: medium;">
                                            <label id="lblpromotor">
                                            </label>
                                            </b>
                                            </td>
                                        <td align="left" 
                                            style="border-top: 1px solid silver; padding-left: 5px; font-weight: bold;" class="fsal f13"
                                           >
                                        <b>Estado:</b></td>
                                         <td align="left" 
                                            style="border-top: 1px solid silver; padding-left: 5px; font-weight: bold;" class="style4"
                                           >
                                            <b style="color: #000000; font-size: medium;">
                                            <label id="lblestado">
                                            </label>
                                            </b>
                                            </td>
                                    </tr>
            </table>
    <p id="validateTips">
    </p>
     <table style="width:100%;">
         <tr>
             <td class="fsal f13">
                                        <b style="font-size: medium">Forma de Pago</b></td>
             <td>
                                                &nbsp;</td>
             <td>
                 &nbsp;</td>
         </tr>
         <tr>
             <td class="style9">
                                                <select id="dwpago" 
                     style="width: 230px; font-size: medium; font-weight: bold;" name="D1">
                                                    <option></option>
                                                </select></td>
             <td>
                                                <asp:Panel ID="pnprueba" runat="server" Style="display: none;">
                                                    <asp:Label ID="Label1" runat="server" Text="estado de prueba"></asp:Label>
                                                </asp:Panel>
             </td>
             <td>
                 &nbsp;</td>
         </tr>
     </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="999999">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upHdNoOrder" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdNoOrder" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="margin: 10px auto 0 auto;">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="upMsg" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btCreateLiq" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="btSave" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="btSaveExit" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="fsal f13">
                    Formulario de adición de artículos         
               </td>
            </tr>
             <tr>
                    <th align="left">
                        <table style="width:100%;">
                            <tr>
                                <td class="style1">
                                   
                                    <asp:Label ID="LblPago" runat="server"  style="font-weight: bold; display:none;" 
                                                                BackColor="White" BorderColor="#003366" Font-Bold="True" 
                                                                Font-Names="Times New Roman" Font-Size="X-Large" 
                                        ClientIDMode="Static"></asp:Label>
                                   
                                </td>
                            </tr>
                        </table>
                    </th>
                </tr>
            <tr>
                <td>
                    <!-- START DYNAMIC TABLE -->
                    <table>
                        <tr>
                            <td valign="middle">
                                <div style="width: 150px; max-width: 150px; height: 150px; padding-left: 19px; text-align: left;
                                    border: 1px solid #E8E8E8;">
                                    <br />
                                    <img src="../../Design/images/card_user.png" id="photoItem" alt="Fotografia item"
                                        title="Fotografia Item" style="position:relative;height:130px;width:130px;" />
                                </div>
                            </td>
                            <td>
                                <table cellpadding="2" cellspacing="2" class="f12">
                                    <thead>
                                        <tr>
                                            <th align="left">
                                                Código:
                                            </th>
                                            <th align="left">
                                                Talla:
                                            </th>
                                            <th align="left">
                                                Unidades:
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <input id="txtCode" type="text" maxlength="9" />
                                            </td>
                                            <td>
                                                <select id="dwSizes" style="width: 160px;">
                                                    <option></option>
                                                </select>
                                            </td>
                                            <td>
                                                <input id="txtQty" type="text" style="width: 50px" maxlength="5" />
                                            </td>
                                            <td>
                                                <button id="btAddItem">
                                                    Agregar Item</button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <b>Artículo:</b>
                                                        </td>
                                                        <td>
                                                            <label id="lblArt">
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Marca:</b>
                                                        </td>
                                                        <td>
                                                            <label id="lblBrand">
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Color:</b>
                                                        </td>
                                                        <td>
                                                            <label id="lblColor">
                                                            </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Categorización:</b>
                                                        </td>
                                                        <td>
                                                            <label id="lblMaj">
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="font-weight: bold;" valign="top">
                                                <table>
                                                    <tr>
                                                        <td class="f13">
                                                            Precio:
                                                        </td>
                                                        <td class="fsal f13">
                                                            <label id="lblPrice">
                                                            </label>
                                                        </td>
                                                    </tr>
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
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <table class="ui-state-highlight ui-corner-all"  style="display:none">
                                                <tr>
                                                    <td>
                                                        <img id="imgAddToOrder" src="../../Design/images/b_order.png" alt="Búsqueda de artículos para adicionar a la orden."
                                                            title="Búsqueda de artículos para adicionar a la orden." style="display:none" />
                                                    </td>
                                                    <td style="cursor: pointer;">
                                                        <span onclick="javascript:openDialogAddItem();"  style="cursor: pointer; display:none"><b>Adicionar
                                                            artículos por catálogo</b></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img src="../../Design/images/ajax_loader_face.gif" alt="Loading.." title="Loading.."
                                                id="imgLoad" style="display: none;" />
                                            <img src="../../Design/images/b_active.png" alt="Adicionado correctamente" title="adicionado correctamente"
                                                id="imgAdd" style="display: none;" />
                                        </td>
                                        <td>
                                            <label id="lblMsg">
                                            </label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="margin-left: auto; margin-right: auto;">
                        <!-- DYNAMIC TABLE -->
                        <table id="list4">
                        </table>
                        <div id="pager2" style="height: 60px">
                        </div>
                        <!-- END DYNAMIC TABLE -->
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    Usted se encuentra realizando un pedido borrador, en el cual se permite realizar
                    la solicitud de mercancia; recuerde que en ningún momento está separando stock,
                    para esto deberá liquidar el pedido.
                </td>
            </tr>
            <tr>
                <td style="padding-right: 10px;">
                    <table width="100%">
                        <tr>
                            <td width="60%" valign="top">
                                <table>
                                    <tr>
                                        <td colspan="2" class="fsal">
                                            <em>Explicaciones</em>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <img title="Consulta de disponibilidad de item" alt="Consulta de disponibilidad de item"
                                                src="../../Design/images/Botones/b_info.png" />
                                        </td>
                                        <td>
                                            Consulta de stock disponible de un artículo.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <img alt="Eliminar item del pedido" title="Eliminar item del pedido" src="../../Design/images/Botones/delete_off.png" />
                                        </td>
                                        <td>
                                            Eliminación de una linea de pedido
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            Puede modificar las cantidades de un artículo adicionado al pedido, mediante "doble
                                            click" y la posterior modificación de las cantidades.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <table class="f13" width="100%" cellpadding="4" cellspacing="4">
                                    <tr>
                                        <td>
                                            Cantidades totales:
                                        </td>
                                        <td align="right" style="padding-right: 5px;">
                                            <label id="lblTotQty">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sub-Total (Sin IGV):
                                        </td>
                                        <td align="right" style="padding-right: 5px;">
                                            <label id="lblSubTotal">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            (+) Impuestos (IGV): 
                                        </td>
                                        <td align="right" style="padding-right: 5px;">
                                            <label id="lblTaxes">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                            <td>
                               <label id="lbltnc">
                                            </label>  
                                <asp:Button ID="btncredito" runat="server"  OnClick="btncredito_Click" 
                                    Style="display: none;" />
                                <button type="button" value=" Nota de Credito" id="btncr"
                                    title="Pago con Nota de Credito">
                                    Nota de Credito</button>
                            </td>
                                <td align="right" style="padding-right: 5px;">
                                            <label id="lblnc">
                                            </label>
                                </td>     
                             </tr>
                                    <tr>
                                        <td> 
                                            Total:
                                        </td>
                                        <td align="right" style="padding-right: 5px;">
                                                <label id="lblGrandTotal">
                                                </label>
                                        </td>
                                    </tr>
                                    <tr> 
                                        <td>
                                            (+) Percepcion:
                                        </td>
                                         <td align="right" style="padding-right: 5px;">
                                                <label id="lblpercepcion">
                                                </label>
                                        </td>
                                    </tr>
                                    <tr style="color: #000000;">
                                        <td style="border-top: 1px solid silver; font-weight: bold;" class="f13">
                                        <b>(=) Gran Total:</b>
                                        </td>
                                        <td align="right" style="border-top: 1px solid silver; padding-right: 5px; font-weight: bold;"
                                            class="f13">
                                            <b>
                                            <label id="lblmtopercepcion">
                                            </label>
                                            </b>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="f12">
                    <!-- LIQUIDACION DE PEDIDO DIRECTAMENTE -->
                    <table width="100%" class="tablagris" cellpadding="4" cellspacing="4">
                        <tr>
                            <td class="fsal f13">
                                <b>Configuración de nuevas liquidaciones</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <p class="f12">
                                    Trámite las siguientes opciones, <b><i>sólo si desea liquidar este pedido en este momento:</i></b></p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <AQControl:ConfigLiq ID="ConfigLiq" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="f12">
                    <asp:Panel ID="pnlDwCustomers" style="display: none;" runat="server">
                        <div style="margin: 10px auto 0 auto;">
                            <table width="100%"  cellpadding="4">
                                <tr>
                                    <td>
                                        <table cellpadding="1" cellspacing="1" width="100%">
                                            <tr>
                                                <td class="fsal f13" colspan="2">
                                                    <b>Datos de la la Tarjeta Visa UNICA</b><br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style11">
                                                    <label class="f12" for="txtNoVoucher">
                                                    *<u>N</u>umero de tarjeta de credito:(16 digitos)</label>
                                                </td>
                                                <td>
                                                     <asp:TextBox ID="txtNoTarjeta" runat="server" AccessKey="c" MaxLength="16" 
                                                         ToolTip="Maximo 16 digitos"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style11">
                                                    <label class="f12" for="txtNoVoucher">
                                                    *<u>N</u>umero de voucher (OP):</label><br />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNoVoucher" runat="server" AccessKey="c" MaxLength="8" 
                                                        ToolTip="Maximo 8 digitos"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style11">
                                                    <label for="txtValue" class="f12">*Monto del voucher:</label>
                                                    <br />
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="txtValue" runat="server" AccessKey="v" ValidationGroup="valerror" Enabled="false" 
                                                    MaxLength="16" ToolTip="Maximo 16 digitos"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- BUTTONS -->
                    <table width="100%" class="f12" style="border: 1px solid silver; background-color: #f5f5f5;"
                        cellpadding="4" cellspacing="4">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btExit" runat="server" AccessKey="s" Text="Volver sin guardar" OnClick="btExit_Click" />
                            </td>
                            <td align="center">
                                <asp:Button ID="btSaveExit" runat="server" AccessKey="s" Text="Guardar y (s)alir"
                                    OnClick="btSaveExit_Click" Style="display: none;" />
                                    <button type="button" value=" (L)iquidar este pedido" id="btsaveorderexit"
                                    title="Guardar Pedido y Salir">
                                    Guardar y (s)alir</button>
                            </td>
                            <td align="center">
                                <asp:Button ID="btSave" runat="server" AccessKey="g" Text="(G)uardar pedido" OnClick="btSave_Click" 
                                Style="display: none;" />
                                <button type="button" value=" (L)iquidar este pedido" id="btsaveorder"
                                    title="Guardar Pedido">
                                    (G)uardar pedido</button>
                            </td>
                            <td align="center">
                                <asp:Button ID="btCreateLiq" runat="server"  OnClick="btCreateLiq_Click" 
                                    Style="display: none;"/>
                                <button type="button" value=" (L)iquidar este pedido" id="btCreateLiquidation"
                                    title="Proceda a liquidar el pedido que actualmente realiza">
                                    (L)iquidar este pedido</button>
                               <button type="button" value=" (L)iquidar este pedido" id="btmodiliquidacion"
                                    title="Proceda a liquidar el pedido que actualmente realiza">
                                    (M)odificar esta liquidacion</button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog-confirm-erase" style="display: none" title="¿Elminiar linea de pedido?">
        <p>
            <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
            Se eliminará la línea del pedido, ¿realmente desea hacerlo?</p>
    </div>
    <div id="dialog-InfoStock" style="display: none">
        <div id="content_div">
        </div>
    </div>
    <!-- SHIPPING FORM -->
    <AQControl:ShippingForm runat="server" Visible="true" ID="ShippForm" />
    <!-- -->
    <div id="dialog-HelpForAdd" style="display: none" title="Búsqueda de artículos para adicionar">
        <!-- SHIPPING FORM -->
        <AQControl:AddItemsForm runat="server" Visible="true" ID="AddItemsForm" />
    </div>
    <asp:HiddenField ID="h_numTipPago"   Value="0" runat="server" />
    <asp:HiddenField ID="h_numConfigPagoPOS"  Value="0" runat="server" />
            <asp:HiddenField ID="hdCreditValue" Value="0" runat="server" />
    <div id="dialog-NC" title="Seleccione Nota de Credito" style="text-align:center">
            <asp:HiddenField ID="hdestado" Value="0" runat="server" />
    <iframe style="border: 0px; " src="orderformaNC.aspx" width="100%" height="100%"></iframe>
</div>
</asp:Content>

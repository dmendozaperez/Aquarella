<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Manifiesto.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.Manifiesto" StylesheetTheme="SiteTheme" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
     <script src="../../Scripts/JqGrid/grid.locale-es.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
    <link href="../../Styles/ui.jqgrid.css" type="text/css" rel="stylesheet" />
    <script src="../../Scripts/ImgScale/jquery.cj-object-scaler.min.js" type="text/javascript"></script>    
   

    <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>
     <style type="text/css">
        .style1
        {
            width: 374px;
        }
        .style2
        {
            width: 92px;
        }
        .style3
        {
            width: 202px;
        }
        .style4
        {
            width: 317px;
        }
         .auto-style3 {
             width: 33px;
         }
         .auto-style4 {
             width: 210px;
         }
         .auto-style5 {
             width: 9px;
         }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            GetEstado();
        });

        function GetEstado()
        { 
            var estado = '<%= Session["estado"] %>';
           
            if (estado=='1')
            {
                $("#btmodimanifiesto").hide();                
            }
            else
            {
                $("#btcrearman").hide();
            }

        }
    </script>
    <script type="text/javascript">
     var valida, tips;
     $(function () {
         var bnaceptar = $("input[id$='btcrearmanifiesto']");
         allFields = $([]).add(bnaceptar);
            var estado = '<%= Session["estado"] %>';
         if (estado=='1')
{
         $("#dialog-confirm").dialog({
             autoOpen: false,
             resizable: false,
             width: 400,
             height: 160,
             title: 'Vamos a generar su manifiesto ¿continuamos?',
             modal: true,
             buttons: {
                 "Continuar": function () {
                     var bValid = true;
                     var valida;
                     allFields.removeClass("ui-state-error");
                     //bnaceptar.click();
                     search();

                 },
                 Cancelar: function () {
                     $(this).dialog("close");
                 }
             },
             close: function () {
                 allFields.val("").removeClass("ui-state-error");
             }
         });

         }
         else
         {
            
             $("#dialog-confirm-edit").dialog({
                 autoOpen: false,
                 resizable: false,
                 width: 400,
                 height: 160,
                 title: 'Vamos a modificar su manifiesto ¿continuamos?',
                 modal: true,
                 buttons: {
                     "Continuar": function () {
                         var bValid = true;
                         var valida;
                         allFields.removeClass("ui-state-error");
                         //bnaceptar.click();
                         search();

                     },
                     Cancelar: function () {
                         $(this).dialog("close");
                     }
                 },
                 close: function () {
                     allFields.val("").removeClass("ui-state-error");
                 }
             });
         }

         var estado = '<%= Session["estado"] %>';
         if (estado == '1')
         {
                 $("#btcrearman")
      .button()
      .click(function () {          
              $("#dialog-confirm").dialog("open");         
         
      });
         }
         else
         {
             $("#btmodimanifiesto")
      .button()
      .click(function () {                 
              $("#dialog-confirm-edit").dialog("open");          
      });
         }

     
     }); 
  //function getproestado() {
  //    //Ajax
  //    var urlMethod = "Genera_Saldos_nc.aspx/retorna_data";
  //    var jsonData = '{}';
  //    SendAjax(urlMethod, jsonData, showpromoesta);
  //}
  //function showpromoesta(msg) {
  //    tips = $("#validateTips");  
  //    tips.text("");
  //    var val = msg.d;
  //    if (val == "0") {
  //        $("#dialog-confirm").dialog("open");
  //    }
  //    else {
  //        if (val == "1") {
  //            updateTips(" No ha seleccionado ningun item o no hay datos en la lista...");
  //        }
  //        if (val == "2") {
  //            updateTips(" No se puede generar con saldo cero ó No se ha generado el correlativo...");
  //        }
  //    }
  //}
  function updateTips(t) {
      tips
			.text(tips.text() + t)
			.addClass("ui-state-highlight");
      setTimeout(function () {
          tips.removeClass("ui-state-highlight", 1500);
      }, 500);
  }
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
              onAjaxError("error");
          }
      });
  }
  function search() {
      var estado = '<%= Session["estado"] %>';
      if (estado == '1')
          {
          $("#dialog-confirm").dialog("close");
      }
      else
      {
          $("#dialog-confirm-edit").dialog("close");
      }
         openDialogLoad();
         btge = $("input[id$='btcrearmanifiesto']");
         btge.click();
     }
     function openDialogLoad() {

         var estado = '<%= Session["estado"] %>';

         if (estado == '1')
             {
             $("#dialog-load").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
             $("#dialog-load").dialog().dialog("widget").find(".ui-dialog-titlebar-close").hide();
         }
         else
         {
             $("#dialog-load-mod").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
             $("#dialog-load-mod").dialog().dialog("widget").find(".ui-dialog-titlebar-close").hide();
         }
     }
        function closeDialogLoad() {
            var estado = '<%= Session["estado"] %>';
            if (estado == '1')
                {
                $("#dialog-load").dialog("close");
            }
            else
            {
                $("#dialog-load-mod").dialog("close");
            }
     }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Crear Manifiesto
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
     Cree su manifiesto, mediante el numero de documento factura o boleta,
     ; use la tecla Enter
     para la adición de sus referencias.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <center>
                <div 
                    style="position: absolute; left: 0; background: #f5f5f5; filter: alpha(opacity=85);
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 100%; font-size: medium; top: 0px;">
                    <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Buscando Información." />
                    Buscando Información...
                </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
      <table style="width:100%;">
            <tr style="color: #000000;">
                                        <td align="left" 
                                            style="border-top: 1px solid silver; padding-left: 5px; font-weight: bold;" class="auto-style5"
                                          >
                                        <b style="font-size:14px">N° Manifiesto: </b>
                                        </td>
                                        <td align="left" 
                                            
                                            style="border-top: 1px solid silver; padding-left: 5px; font-weight: bold;" class="auto-style4"
                                           >
                                            <b style="color:darkblue ; font-size: 20px;">
                                                <asp:Label ID="lblnumero" runat="server" Text="100"></asp:Label>                                            
                                            </b>
                                            </td>
                                        <td align="left" 
                                            style="border-top: 1px solid silver; padding-left: 5px; font-weight: bold;" class="auto-style3"
                                           >
                                        <b style="font-size:14px">Estado:</b></td>
                                         <td align="left" 
                                            style="border-top: 1px solid silver; padding-left: 5px; font-weight: bold;" class="style4"
                                           >
                                            <b style="color: darkblue; font-size: 20px;">
                                            <asp:Label ID="lblestado" runat="server" Text="(Nuevo)"></asp:Label> 
                                            </b>
                                            </td>
                                    </tr>
            </table>
     
      <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
      <div style="margin: 10px auto 0 auto;">
        <table width="100%">
            <tr>
                <td>
                    <asp:UpdatePanel ID="upMsg" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
                        </ContentTemplate>
                        <Triggers>                           
                            <asp:AsyncPostBackTrigger ControlID="btnagregar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="RowDataBound" />
                            <asp:AsyncPostBackTrigger ControlID="btcrearmanifiesto" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
     <div style="margin: 10px auto 0 auto;">
        <table  class="tablagris" cellpadding="4">
            <tr>
                <td class="fsal f13">
                    Formulario de adición de Documento facturas o boletas         
               </td>
            </tr>
            <tr>
                <td >
                    <!-- START DYNAMIC TABLE -->
                    <table>
                        <tr>
                            <td valign="middle">                                
                            </td>
                            <td>
                                <table cellpadding="2" cellspacing="2" class="f12">
                                    <thead>
                                        <tr>
                                            <th align="left">
                                                N° de Documento:
                                            </th>
                                            <th align="left">
                                                
                                            </th>
                                            <th align="left">
                                                
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                    <asp:TextBox ID="txtdocumento" runat="server"></asp:TextBox>                                               
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                
                                            </td>
                                            <td>                                                
                                            </td>
                                            <td>                                                
                                            </td>
                                            <td>
                                                <asp:Button ID="btnagregar" runat="server" Text="Agregar" OnClick="btnagregar_Click"  />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>                          
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <div  style="min-height: 200px; align-content:center; width: 1017px;" align="center">                   
                    <asp:UpdatePanel ID="upmanifiesto" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvmanifiesto" runat="server" Width="98%" SkinID="gridviewSkin"
                                AutoGenerateColumns="False"  Font-Size="Small" AllowPaging="True" AllowSorting="True" CellPadding="3" ShowHeaderWhenEmpty="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" PageSize="10" OnRowCommand="gvmanifiesto_RowCommand" OnRowDataBound="gvmanifiesto_RowDataBound" OnPageIndexChanging="gvmanifiesto_PageIndexChanging" >
                              <%--  <EmptyDataTemplate>
                                    No existen manifiesto que mostrar.
                                </EmptyDataTemplate>--%>
                                <Columns>
                                    <asp:BoundField DataField="guia"
                                        HeaderText="N° de Guia" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="doc" HeaderText="N° de Documento" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="lider" HeaderText="Lider" >
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="promotor" HeaderText="Promotor" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="pares" HeaderText="Pares" Visible="true" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="agencia" HeaderText="Agencia" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="destino" HeaderText="Destino" ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                       <asp:TemplateField HeaderText="Anular" ItemStyle-HorizontalAlign="Center" >
                            <ItemTemplate>
                                            <asp:ImageButton ID="ibanular" CommandName="starnular"  CommandArgument='<%# Eval("doc")%>'
                                                  Visible="true" runat="server" ImageUrl="~/Design/images/Botones/delete_off.png" ToolTip="eliminar registro" BorderWidth="0" />
                      
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
                            <asp:AsyncPostBackTrigger ControlID="btnagregar" EventName="Click" />                          
                            <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="PageIndexChanging" />
                            <asp:AsyncPostBackTrigger ControlID="btcrearmanifiesto" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>        
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>    
                             
            <tr>
                <td>
                    Usted se encuentra realizando un manifiesto, generando uno nuevo o modificando.
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
                                        </td>
                                        <td>                                           
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <img alt="Eliminar item del manifiesto" title="Eliminar item del manifiesto" src="../../Design/images/Botones/delete_off.png" />
                                        </td>
                                        <td>
                                            Eliminación de una linea del manifiesto
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">                                           
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                                <table class="f13" width="100%" cellpadding="4" cellspacing="4">
                                    <tr>
                                       
                                    </tr>
                                    <tr>
                                       
                                    </tr>
                                    <tr>
                                      
                                    </tr>
                                    <tr>
                            <td>
                             
                            </td>
                                   
                             </tr>
                                    <tr>                                       
                                    </tr>
                                    <tr> 
                                     
                                    </tr>
                                    <tr style="color: #000000;">
                                                                              
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <!-- BUTTONS -->
                    <table width="100%" class="f12" style="border: 1px solid silver; background-color: #f5f5f5;"
                        cellpadding="4" cellspacing="4">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btExit" runat="server" AccessKey="s" Text="Volver sin guardar" OnClick="btExit_Click"  />
                            </td>
                            <td align="center">
                            
                            </td>
                            <td align="center">
                              
                            </td>
                            <td align="center">
                                <asp:Button ID="btcrearmanifiesto" runat="server" 
                                    Style="display: none;" OnClick="btcrearmanifiesto_Click"/>
                                <button type="button" value=" (G)enera Manifiesto" id="btcrearman"
                                    title="Proceda a generar manifiesto">
                                    (G)enera Manifiesto</button>  
                                <button type="button" value=" (M)odificar Manifiesto" id="btmodimanifiesto"
                                    title="Proceda a modificar manifiesto">
                                    (M)odificar Manifiesto</button>                            
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>       
    </div>
    <div id="dialog-confirm" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Generaremos su manifiesto; ¿desea continuar?</p>
    </div>
    <div id="dialog-load" style="display:none"   title="Procesando Provisiones">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Se esta generando su manifiesto , por favor aguarde un momento.</p>
    <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span>Recuerde que se esta generando el reporte con el codigo de manifiesto.
    </p>    
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        Creando Manifiesto ...
        </p>
</div>

    <div id="dialog-confirm-edit" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Modificaremos su manifiesto; ¿desea continuar?</p>
</div>
    <!-- DIALOG LOAD MOD-->
<div id="dialog-load-mod" style="display:none"   title="Modificanco manifiesto">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Su manifiesto se esta modificando, por favor aguarde un momento.</p>
    <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span>>Recuerde que se esta modificando el reporte con el codigo de manifiesto.
    </p>    
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        modificando manifiesto ...
        </p>
</div>
</asp:Content>

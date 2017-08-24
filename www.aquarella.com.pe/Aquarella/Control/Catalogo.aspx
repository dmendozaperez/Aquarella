<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Control.Catalogo" StylesheetTheme="SiteTheme" %>
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
             title: 'Vamos a generar su catalogo virtual ¿continuamos?',
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
                 title: 'Vamos a modificar su catalogo virtual ¿continuamos?',
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
    Crear o Edita Catalogo
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
     Cree su catalogo virtual,
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
                          <%--  <asp:AsyncPostBackTrigger ControlID="btnagregar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="gvmanifiesto" EventName="RowDataBound" />--%>
                            <asp:AsyncPostBackTrigger ControlID="btcrearmanifiesto" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
     <div style="margin: 10px auto 0 auto;vertical-align:central;">
              <table  class="tablagris" cellpadding="4">
                 <tr>
                <td class="fsal f13" colspan="2">
                    Formulario de adición de catalogo virtual         
               </td>
            </tr>
                <tr>
                    <td style="font-size:12px">
                        Descripcion:
                    </td>
                    <td>
                        <asp:TextBox ID="txtdescripcion" runat="server" Width="220px" />
                    </td>
                    <%--<td>
                        <asp:RequiredFieldValidator ID="RFValidator" runat="server" ControlToValidate="txtdescripcion"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>--%>
                </tr>
                <tr>
                    <td style="font-size:12px">
                        Header Title:
                    </td>
                    <td>
                        <asp:TextBox ID="txtheadertitle" runat="server" Width="220px" />
                    </td>
                  <%--  <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtheadertitle"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>--%>
                    <%--<td>
                    </td>--%>
                </tr>
                <tr>
                    <td style="font-size:12px">
                        Nro de Paginas:
                    </td>
                    <td>
                        <asp:TextBox ID="txtnropagina" runat="server" Width="120px" />
                    </td>
                   <%-- <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtnropagina"
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>--%>
                </tr>                                                                 
            </table>
        <table  class="tablagris" cellpadding="4">                                          
            <tr>
                <td>
                    Usted se encuentra realizando un catalogo virtual, generando uno nuevo o modificando.
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
                                <button type="button" value=" (G)enera Catalogo" id="btcrearman"
                                    title="Proceda a generar catalogo virtual">
                                    (G)enera Catalogo</button>  
                                <button type="button" value=" (M)odificar Catalogo" id="btmodimanifiesto"
                                    title="Proceda a modificar catalogo virtual">
                                    (M)odificar Catalogo</button>                            
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
        Generaremos su catalogo virtual; ¿desea continuar?</p>
    </div>
    <div id="dialog-load" style="display:none"   title="Procesando catalogo virtual">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Se esta generando su catalogo virtual , por favor aguarde un momento.</p>
   <%-- <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span>Recuerde que se esta generando el reporte con el codigo de manifiesto.
    </p>    --%>
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        Creando Catalogo Virtual ...
        </p>
</div>

    <div id="dialog-confirm-edit" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Modificaremos su catalogo virtual; ¿desea continuar?</p>
</div>
    <!-- DIALOG LOAD MOD-->
<div id="dialog-load-mod" style="display:none"   title="Modificanco catalogo virtual">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Su catalogo se esta modificando, por favor aguarde un momento.</p>
   
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        modificando catalogo virtual...
        </p>
</div>
</asp:Content>

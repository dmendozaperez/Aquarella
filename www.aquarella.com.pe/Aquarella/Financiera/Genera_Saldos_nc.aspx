<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Genera_Saldos_nc.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.Genera_Saldos_nc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <%--<script src="../../Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
<script src="../../Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
<script src="../../Scripts/calendar-en.min.js" type="text/javascript"></script>
<link href="../../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
  .boton{
        font-size:10px;
        font-family:Verdana,Helvetica;
        font-weight:bold;
        color:white;
        background:#808080;
         border-radius:2px;
        -moz-border-radius:2px;
        -webkit-border-radius:2px;    
        cursor:pointer;
        border:1px;
        border-color:white;
        width:80px;
        height:25px;
       }
</style>
<script type="text/javascript">
    var $msge; 
   
    //Ajax
    
    //
    function onAjaxError(errorMsg) {
        // Display the specific error raised by the server            
        showLoad(false, errorMsg, false);
        alert(errorMsg);            
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
</script>
 <script type="text/javascript">
     var valida, tips;
     $(function () {
         var bnaceptar = $("input[id$='btge']");
         allFields = $([]).add(bnaceptar);
         $("#dialog-confirm").dialog({
             autoOpen: false,
             resizable: false,
             width: 400,
             height: 160,
             title: 'Vamos a generar sus anticipos ¿continuamos?',
             modal: true,
             buttons: {
                 "Continuar": function () {
                     var bValid = true;
                     var valida;
                     allFields.removeClass("ui-state-error");
                     bnaceptar.click();
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

         $("#btgenerar")
      .button()
      .click(function () {
          //          page.    
          refrescar_tabla();     
          getproestado();
          //          $("#dialog-confirm").dialog("open");
      });
     });
  function refrescar_tabla() {
      var btrefrescar = $("input[id$='refresh']");
      btrefrescar.click();  
    
  }
  function getproestado() {
      //Ajax
      var urlMethod = "Genera_Saldos_nc.aspx/retorna_data";
      var jsonData = '{}';
      SendAjax(urlMethod, jsonData, showpromoesta);
  }
  function showpromoesta(msg) {
      tips = $("#validateTips");  
      tips.text("");
      var val = msg.d;
      if (val == "0") {
          $("#dialog-confirm").dialog("open");
      }
      else {
          if (val == "1") {
              updateTips(" No ha seleccionado ningun item o no hay datos en la lista...");
          }
          if (val == "2") {
              updateTips(" No se puede generar con saldo cero ó No se ha generado el correlativo...");
          }
      }
  }
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
         $("#dialog-confirm").dialog("close");
         openDialogLoad();
         btge = $("input[id$='btge']");
         btge.click();
     }
     function openDialogLoad() {
         $("#dialog-load").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
         $("#dialog-load").dialog().dialog("widget").find(".ui-dialog-titlebar-close").hide();
     }
     function closeDialogLoad() {
         $("#dialog-load").dialog("close");
     }

    </script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".Calender").dynDateTime({
            showsTime: true,
            ifFormat: "%d/%m/%Y",
            daFormat: "%l;%M %p, %e %m,  %Y",
            align: "BR",
            electric: false,
            singleClick: false,
            displayArea: ".siblings('.dtcDisplayArea')",
            button: ".next()"
        });
    });
</script>
    <style type="text/css">
        .style1
        {
            width: 987px;
        }
        .style2
        {
            width: 963px;
        }
        .style5
        {
            width: 167px;
        }
        .style7
        {
            width: 217px;
        }
        #btgenerar
        {
            width: 168px;
        }
        .auto-style3 {
            width: 1719px;
        }
        .auto-style4 {
            width: 169px;
        }
        .auto-style5 {
            width: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Facturación de Saldos
    Anticipos
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Esta opcion es para que el saldo se convierta en nota de credito y haci el 
    usuario lo pueda usar 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <p id="validateTips">
    </p>
    <asp:ScriptManager ID="ScriptManager1" runat="server" 
        EnableScriptGlobalization="True" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
      <!-- Area de errores -->
    <asp:UpdatePanel ID="upPanelMsg" runat="server">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
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
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="auto-style3">
                    <table>
                        <tr>
                            <td class="style5">                               
                                        <asp:Button ID="btfa" runat="server" CausesValidation="true" 
                                            onclick="btfa_Click" Text="Actualizar Fecha a todos" Width="166px" />                                    
                            </td>
                             <td class="f12">
                                 &nbsp;</td>
                             <td class="auto-style4">
                             <asp:Button ID="btge" runat="server" 
                                    Style="display: none;" onclick="btge_Click"/>                                
                                <button type="button" value=" (G)enerar Anticipos" id="btgenerar"
                                    title="(G)enerar Anticipos">
                                    (G)enerar Anticipos</button>                          
                            </td>
                             <td class="auto-style5">
                                 <asp:Button ID="refresh" runat="server" onclick="refresh_Click" Text="Button" style="display:none" />
                            </td>   
                             <td >
                                   <asp:Button ID="btgecor" runat="server"   onclick="btgecor_Click" Text="(G)enerar Correlativos" style="display:none;"/>                                                               
                                 </td>                         
                             <td >
                                   <asp:Button ID="btgecorrMan" runat="server" onclick="autogenerar_correlativo_manual" Text="(G)enerar Correlativos Manual" />                                                               
                                 </td>
                            
                            <td >
                                   Fec.Nota Credito
                                 </td>
                              <td>
                                 <%--<asp:TextBox ID="txtDateStart" runat="server" ></asp:TextBox>--%>
                                   <asp:UpdatePanel ID="upStartDate2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtDateStart" runat="server"  AccessKey="f"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendar2" runat="server" Animated="true" 
                                            FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendarIni" 
                                            TargetControlID="txtDateStart" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                 
                              </td>
                            <td>
                                                    <asp:Image ID="imgCalendarIni" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                        onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                        Style="cursor: pointer;" />
                                                </td>
                           
                        </tr>
                        </table>
                </td>
                <td valign="middle" >
                     
                </td>
               
            </tr>
            </table>
    </div>
    <div style="margin: 1px 1px 1px 1px;  width: 1078px; overflow: auto;">
        <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional" 
            RenderMode="Inline">
            <ContentTemplate>
                <asp:GridView ID="gvReturns" runat="server" AllowSorting="True" PageSize="12"
                 SkinID="gridviewSkin" PagerStyle-HorizontalAlign="Left" Font-Size="Small" 
                    CellPadding="3" Width="1072px" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#999999" 
                    BorderWidth="1px" onrowdatabound="gvReturns_RowDataBound" 
                    GridLines="Vertical" >
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle CssClass="GridViewBlue-tf" BackColor="#999999" ForeColor="Black" 
                        HorizontalAlign="Center" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Documento">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("documento") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbldocumento" runat="server" Text='<%# Bind("documento") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Cliente" HeaderText="Cliente">
                        </asp:BoundField>
                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" DataFormatString="{0:N2}">
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Saldo Util.">
                            <EditItemTemplate>                                
                            </EditItemTemplate>
                            <ItemTemplate>                                 
                                  <asp:UpdatePanel ID="Updmontoutil" runat="server">
                                    <ContentTemplate>
                                            <table style="width:11%;">
                                                 <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="uptxtmontoe" runat="server">
                                                            <ContentTemplate>
                                                        <asp:TextBox ID="txtmontoutil" style="text-align:right" runat="server"  Enabled="false"
                                                            Font-Size="Small" Height="14px" Text='<%# Bind("monto_util","{0:N2}") %>'
                                                            Width="60px"></asp:TextBox>
                                                            </ContentTemplate>
                                                         </asp:UpdatePanel>                                                        
                                                    </td>
                                                     <td>
                                                         <asp:UpdatePanel ID="Updactivar" runat="server">
                                                            <ContentTemplate>
                                                         <asp:CheckBox ID="chkactivar" runat="server" style="cursor:pointer;" 
                                                             AutoPostBack="True" oncheckedchanged="chkactivar_CheckedChanged" />                                               
                                                          </ContentTemplate>
                                                         </asp:UpdatePanel> 
                                                </td>
                                                 </tr>
                                            </table>
                                      </ContentTemplate>
                                </asp:UpdatePanel>                               
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SerieFac">
                            <EditItemTemplate>
                              
                            </EditItemTemplate>
                            <ItemTemplate>
                                 <asp:UpdatePanel ID="Updseriefac" runat="server">
                                    <ContentTemplate>
                                            <asp:TextBox ID="txtseriefac" runat="server" Font-Size="Small" Height="10px"  Width="30px" Enabled="false" ></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel>     
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NumeroFac">
                            <EditItemTemplate>
                              
                            </EditItemTemplate>
                            <ItemTemplate>
                                 <asp:UpdatePanel ID="Updnumerofac" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtnumerofac" runat="server" Font-Size="Small" Height="10px"  Width="60px" Enabled="false" ></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="fec_fac">
                            <EditItemTemplate>                                
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <table style="width:11%;">
                                            <tr>
                                                <td>
                                                     <asp:UpdatePanel ID="upStartDate" runat="server">
                                                     <ContentTemplate>
                                                        <asp:TextBox ID="txtfecfac" runat="server" Font-Size="Small" 
                                                            Height="10px" Enabled="false" Text='<%# Bind("fec_fac") %>' Width="70px"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="calendar" runat="server" Animated="true" 
                                                              FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="imgCalendar" 
                                                               TargetControlID="txtfecfac" />
                                                        </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                </td>
                                                 <td>
                                                    <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                                        onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                                        Style="cursor: pointer;" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MontoFac">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("montofac") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblmontofac" runat="server" 
                                    Text='<%# Bind("montofac", "{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SerieNC">
                            <EditItemTemplate>                               
                            </EditItemTemplate>
                            <ItemTemplate>
                                  <asp:UpdatePanel ID="Updserienc" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtserienc" runat="server" Font-Size="Small" Height="10px"  Width="30px" Enabled="false" ></asp:TextBox>
                                 </ContentTemplate>
                                </asp:UpdatePanel>  
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NumeroNc">
                            <EditItemTemplate>                                
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:UpdatePanel ID="Updnumeronc" runat="server">
                                    <ContentTemplate>
                                <asp:TextBox ID="txtnumeronc" runat="server" Font-Size="Small" Height="10px"  Width="60px" Enabled="false"  ></asp:TextBox>
                                </ContentTemplate>
                                </asp:UpdatePanel> 
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fec_Nc">
                            <EditItemTemplate>                               
                            </EditItemTemplate>
                            <ItemTemplate>
                                <table style="width:11%;">
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="upEndDate" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtfecnc" runat="server" Font-Size="Small" 
                                                    Height="10px" Enabled="false" Text='<%# Bind("fec_nc") %>' Width="70px"></asp:TextBox>
                                                     <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                     Animated="true" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" 
                                                      PopupButtonID="imgCalendarDe" TargetControlID="txtfecnc" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>
                                        <asp:Image ID="imgCalendarDe" runat="server" 
                                        ImageUrl="~/Design/images/Botones/b_calendar_ico.gif" 
                                        onmouseout="this.style.background=''" 
                                        onmouseover="this.style.background='red';" Style="cursor: pointer;" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MontoNc">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("montonc") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblmontonc" runat="server" 
                                    Text='<%# Bind("montonc", "{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Percepcion" Visible="False">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Percepcion") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblpercepcion" runat="server" Text='<%# Bind("Percepcion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Anular">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                  <asp:Button ID="btnanu" runat="server" 
                                    class="boton" Text="Anular" OnClick="btnanu_Click" />                                                                                                                     
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkanular" runat="server" Style="cursor:pointer;" OnCheckedChanged="chkanular_CheckedChanged" AutoPostBack="True" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        No existen registros para mostrar.
                    </EmptyDataTemplate>
                    <RowStyle BackColor="White" ForeColor="Black" BorderColor="#003300" BorderWidth="1px" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>               
                <asp:AsyncPostBackTrigger ControlID="btfa" EventName="Click" />
                               
                <asp:AsyncPostBackTrigger ControlID="btge" EventName="Click" />
                               
                <asp:AsyncPostBackTrigger ControlID="refresh" EventName="Click" />
                               
                <asp:AsyncPostBackTrigger ControlID="btgecor" EventName="Click" />
                               
            </Triggers>
        </asp:UpdatePanel>
        <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btfa" Enabled="false" />                                                      
                                                    <EnableAction AnimationTarget="btge" Enabled="false" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btfa" Enabled="true" />                                                    
                                                    <EnableAction AnimationTarget="btge" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
    <!-- DIALOG CONFIRM -->
<div id="dialog-confirm" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Generaremos sus anticipos; ¿desea continuar?</p>
</div>
<div id="dialog-load" style="display:none"   title="Procesando Provisiones">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Se esta generando sus anticipos , por favor aguarde un momento.</p>
    <p>
        <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
        </span>Recuerde que este saldo se factura y se conviernte en nota de credito.
    </p>    
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        Creando Facturacion y Nota de credito ...
        </p>
</div>
</asp:Content>

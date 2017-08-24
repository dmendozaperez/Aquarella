<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Agrupar_Pedido_Pago.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.Agrupar_Pedido_Pago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">   
 <link media="screen" rel="stylesheet" href="../../Scripts/Colorbox/colorbox.css" />
    <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>   
   

      
   <script type="text/javascript">
       var valida;
       $(function () {
           var bnaceptar = $("input[id$='btCreateLiq']");
           allFields = $([]).add(bnaceptar);
           $("#dialog-confirm").dialog({
               autoOpen: false,
               resizable: false,
               width: 400,
               height: 160,
               title: 'Vamos a generar su Liquidación al lider ¿continuamos?',
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

           $("#btCreateLiquidation")
      .button()
      .click(function () {
          $("#dialog-confirm").dialog("open");
      });
       });

  function search() {
      $("#dialog-confirm").dialog("close");
      openDialogLoad();
      btCreateLiq = $("input[id$='btCreateLiq']");
      btCreateLiq.click();
  }
  function openDialogLoad() {    
          $("#dialog-load").dialog({ modal: true, closeOnEscape: false, closeText: 'hide', resizable: false, width: 400 });
          $("#dialog-load").dialog().dialog("widget").find(".ui-dialog-titlebar-close").hide();
      }
      function closeDialogLoad() {         
              $("#dialog-load").dialog("close");                   
      }

    </script>
       
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Agrupar pedidos por Lider
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Por favor seleccione las liquidaciones.
    El sistema no permite grabar hasta que el "Gran Total" sea un número mayor a cero
    ó sus pagos superen el valor de sus pedidos o deudas. 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="99999">
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
            <asp:AsyncPostBackTrigger ControlID="btCreateLiq" EventName="click" />
            <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
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
    <!-- SEL CUSTOMER -->
    <asp:Panel ID="pnlDwCustomers" Visible="false" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" class="tablagris" cellpadding="4">
                <tr>
                    <td class="fsal f13" colspan="2">
                        Selección el lider
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="100%" cellpadding="1" cellspacing="1">
                            <tr>
                                <td class="f12" width="60%">
                                    Seleccione un lider de la lista:<br />
                                    (Desc: Seleccione un lider sobre el cual realizará las acciones.)
                                </td>
                                <td>
                                    <asp:DropDownList ID="dwCustomers" AutoPostBack="true" DataTextField="Are_Descripcion"
                                        DataValueField="Are_Id" AppendDataBoundItems="true" runat="server"
                                        ToolTip="Selecionar un cliente" Width="220px" OnSelectedIndexChanged="dwCustomers_SelectedIndexChanged">
                                        <asp:ListItem Value="-1" Text=" -- Seleccione de la Lista --"></asp:ListItem>
                                    </asp:DropDownList>
                                    </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto; min-height: 200px;" class="f13">
        <asp:UpdatePanel ID="upGridClear" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="gvClear" runat="server" SkinID="gridviewSkin" ShowFooter="True"
                    HorizontalAlign="Center" OnRowDataBound="gvClear_RowDataBound"
                    OnDataBound="gvClear_DataBound" Width="80%" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <EditRowStyle BackColor="#2461BF" />
                    <EmptyDataTemplate>
                        No posee ninguna pedido.
                    </EmptyDataTemplate>
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDocument" runat="server" OnCheckedChanged="chkDocument_CheckedChanged"
                                    AutoPostBack="True" ToolTip='<%# Eval("dtv_transdoc_id")%>' 
                                    Checked="True" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Promotor">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Cliente") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Cliente") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <FooterStyle CssClass="f13" Font-Bold="True" />
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="pedido" HeaderText="Pedido"  FooterText="Saldo: " >                       
                        <FooterStyle CssClass="f13" Font-Bold="True" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="document_date_desc" HeaderText="Fecha" SortExpression="dtd_document_date"
                            ReadOnly="True" FooterStyle-CssClass="f13" FooterStyle-Font-Bold="true" >
                         <FooterStyle CssClass="f13" Font-Bold="True" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cov_description" HeaderText="Concepto" ReadOnly="True"
                            SortExpression="cov_description" FooterStyle-CssClass="f13" FooterStyle-Font-Bold="true"
                            FooterText="Totales: " >
                        <FooterStyle CssClass="f13" Font-Bold="True" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="totalop" HeaderText="Total Pago" ReadOnly="True" SortExpression="totalop"
                            FooterStyle-CssClass="f13" FooterStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Right"
                            FooterStyle-HorizontalAlign="Right" >
                         <FooterStyle CssClass="f13" Font-Bold="True" HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle CssClass="f13" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="val" HeaderText="Total Pedido" ReadOnly="True" ItemStyle-CssClass="f13"
                            SortExpression="val" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                            FooterStyle-CssClass="f13" FooterStyle-Font-Bold="true" >
                        <FooterStyle CssClass="f13" Font-Bold="True" HorizontalAlign="Right" />
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle CssClass="f13" HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo" HeaderText="Tipo" ReadOnly="True" SortExpression="tipo"
                            ItemStyle-Font-Bold="true" ItemStyle-CssClass="f13" ItemStyle-HorizontalAlign="Center"
                            FooterStyle-HorizontalAlign="Center" Visible="False" >
                        <FooterStyle HorizontalAlign="Center" />
                        <ItemStyle CssClass="f13" Font-Bold="True" HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
            </ContentTemplate>
            <Triggers>              
                <asp:AsyncPostBackTrigger ControlID="btCreateLiq" EventName="click" />
                <asp:AsyncPostBackTrigger ControlID="dwCustomers" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:ObjectDataSource ID="odsClear" runat="server" SelectMethod="get_DocTranLiquiByPedidoAgruparLider"
            TypeName="www.aquarella.com.pe.bll.Documents_Trans" OnSelected="odsClear_Selected">
            <SelectParameters>
                <asp:Parameter Name="_idCust" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <ajaxToolkit:UpdatePanelAnimationExtender runat="server" ID="upae" TargetControlID="upGridClear">
            <Animations>
                    <OnUpdating>
                        <Sequence>
                            <%-- Disable all the controls --%>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btCreateLiq" Enabled="false" />
                                <EnableAction AnimationTarget="dwCustomers" Enabled="false" />
                            </Parallel>
                        </Sequence>
                    </OnUpdating>
                    <OnUpdated>
                        <Sequence>
                            <%-- Enable all the controls --%>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="dwCustomers" Enabled="true" />
                                <EnableAction AnimationTarget="btCreateLiq" Enabled="true" />
                            </Parallel>
                        </Sequence>
                    </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" cellpadding="4" cellspacing="4" class="f-small">
            <tr>
                <td align="center">                                                                                            
                            <asp:Button ID="btCreateLiq" runat="server"  OnClick="btCreateLiq_Click" 
                                    Style="display: none;"/>
                                <asp:HiddenField ID="total_saldo" runat="server" />
                                <button type="button" value=" (G)enerar liquidacion de pedidos" id="btCreateLiquidation"
                                    title="(G)enerar liquidacion de pedidos">
                                    (G)enerar liquidacion de pedidos</button>                                                                                                        
                   
                </td>
            </tr>
        </table>
    </div>
    <!-- DIALOG CONFIRM -->
<div id="dialog-confirm" style="display: none;">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        Generaremos su liquidación al lider; ¿desea continuar?</p>
</div> 
<div id="dialog-load" style="display:none"   title="Procesando liquidación">
    <p>
        <span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>
        La generacion de pedidos al lider se esta procesando, por favor aguarde un momento.</p>    
    <p style="text-align: center">
        <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
        <br />
        Creando liquidación del lider y entrar en marcacion para su despacho ...
        </p>
</div>
</asp:Content>

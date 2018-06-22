<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="panelAsignPickingLiquidation.aspx.cs"
    Inherits="www.aquarella.com.pe.Aquarella.Logistica.panelAsignPickingLiquidation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script type="text/javascript" language="javascript">
        function infoPicking(noLiq) {
            $("#dialog").dialog("close");
            $("#dialog").dialog("destroy");
            var dialog = $("#dialog");
            dialog.dialog({ width: 700,
                height: 380,
                title: 'Información de marcación de liquidación No.' + noLiq,
                modal: true,
                position: 'center',
                open: function (event, ui) {
                    // Loading.. image
                    $("#content_div").html('<img src="../../Design/images/ajax_loader_face.gif" />');
                    $.ajax({
                        type: "POST",
                        url: "panelAsignPickingLiquidation.aspx/ajaxGetInfoPicking",
                        data: "{ noLiq: '" + noLiq + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.d) {
                                // Loading..
                                $("#content_div").html(result.d);
                            }
                            else
                                alert("Ha ocurrido un error al momento de actualizar los datos.");
                        },
                        error: function (msg) { alert("Por favor, verifique la consulta, además, que su sesión no haya expirado, e intente de nuevo." + msg); }
                    });
                },
                buttons: {
                    "Aceptar": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    </script>
    <script type="text/javascript" language="javascript">
        function infoPickingEmpl() {
            $("#dialog").dialog("close");
            $("#dialog").dialog("destroy");
            var dialog = $("#dialog");
            dialog.dialog({ width: 700,
                height: 350,
                title: 'Información general de marcación en bodega',
                modal: true,
                position: 'center',
                open: function (event, ui) {
                    // Loading.. image
                    $("#content_div").html('<img src="../../Design/images/ajax_loader_face.gif" />');
                    $.ajax({
                        type: "POST",
                        url: "panelAsignPickingLiquidation.aspx/ajaxGetInfoPickingEmpl",
                        data: "{}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {
                            if (result.d) {
                                // Loading..
                                $("#content_div").html(result.d);
                            }
                            else
                                alert("Ha ocurrido un error al momento de actualizar los datos.");
                        },
                        error: function (msg) { alert("Por favor, verifique la consulta, además, que su sesión no haya expirado, e intente de nuevo." + msg); }
                    });
                },
                buttons: {
                    "Aceptar": function () {
                        $(this).dialog("close");
                    }
                }
            });
        }
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            //$("select:option").width('200px');
            $("#btInfo").click(function (event) {
                event.preventDefault();
                infoPickingEmpl();
            });
            $("#tabs").tabs();
        });
    </script>
    <script type="text/javascript" language="javascript">
        function dialogInfo(titleD, hdrMsg, bodyMsg) {
            var url = this.href;
            // a workaround for a flaw in the demo system (http://dev.jqueryui.com/ticket/4375), ignore!
            $("#dialog:ui-dialog").dialog("destroy");

            var lblB = $("#dialog-info-labelBody");
            var lblH = $("#dialog-info-labelHeader");
            lblH.html(hdrMsg);
            lblB.html(bodyMsg);
            $("#dialog-info").dialog({ modal: true,
                buttons: {
                    Ok: function () {
                        $(this).dialog("close");
                    }
                }, title: titleD
            });
            $("#dialog-info").dialog("open");
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 190px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Marcación de pedidos en bodega
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Asignacion de personal de marcacion a pedidos o liquidaciones en bodega.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <asp:UpdatePanel ID="upMsg" runat="server">
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
    <!-- -->
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="fsal f13" colspan="2">
                    Panel de asignación de marcación
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="1" cellspacing="1">
                        <tr>
                            <td class="f12" width="60%">
                                Realice un filtro de los resultados:<br />
                                (Desc: Puede buscar por cliente, no.Liq, empleado)
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            Pedidos_bodega
                                        </td>
                                        <td align="center">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chkTypeFilter" runat="server" Checked="true" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="upFilter" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtFilter" runat="server" Width="190px"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="btFilter" runat="server" Text="Filtrar" OnClick="btFilter_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btRefresh" runat="server" Text="Refrescar panel" OnClick="btRefresh_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto;">
        <div>
            <div id="tabs">
                <ul>
                    <li><a href="#fragment-1"><span>Pedidos Bodega</span></a></li>
                </ul>
                <!-- PEDIDOS PARA MARCAR EN BODEGA -->
                <div id="fragment-1" style="min-height: 200px;">
                    <table width="100%">
                        <tr>
                            <td>
                                <p>
                                    Pedidos para marcación en bodega y envío.</p>
                            </td>
                            <td class="auto-style1" style="font-weight:bold">
                                Reporte de pedidos agrupados
                            </td>
                            <td>
                                                    <asp:ImageButton ID="ibExportToExcel" ImageUrl="~/Design/images/b_order.png"
                        onmouseover="this.style.background='green';" onmouseout="this.style.background=''"
                        runat="server" Height="18px" Width="22px" 
                        ToolTip="Exportar pedidos agrupados a pdf." 
                        onclick="ibExportToExcel_Click" /> 
                
                            </td>
                            <td align="right">
                                <button id="btInfo">
                                    <img src='../../Design/images/Botones/b_comment.png' alt="Relación marcador, pedidos y unidades."
                                        title="Relación marcador, pedidos y unidades." /></button>
                            </td>
                        </tr>
                    </table>
                    <asp:Timer ID="TimerOrders" runat="server" Interval="60000" OnTick="TimerOrders_Tick">
                    </asp:Timer>
                    <asp:UpdatePanel ID="upGridLiqPick" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvLiqPicking" runat="server" SkinID="gridviewSkin" ShowFooter="true"
                                AllowSorting="true" AllowPaging="true" OnDataBound="gvLiqPicking_DataBound" OnRowDataBound="gvLiqPicking_RowDataBound"
                                OnRowCommand="gvLiqPicking_RowCommand" OnRowCreated="gvLiqPicking_RowCreated">
                                <EmptyDataTemplate>
                                    No existen liquidaciones para marcar.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Are_Descripcion" HeaderText="Área" SortExpression="Are_Descripcion" />
                                    <asp:BoundField DataField="Liq_Id" HeaderText="No.Liq" SortExpression="Liq_Id" />
                                    <asp:BoundField DataField="Liq_EstId" HeaderText="Estdo" SortExpression="Liq_EstId"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombres" HeaderText="Cliente" SortExpression="nombres" />
                                    <asp:BoundField DataField="datedesc" HeaderText="Fecha" SortExpression="dateclear" />
                                    <asp:BoundField DataField="cleardesc" HeaderText="Liberación" SortExpression="cleardate" />
                                    <asp:BoundField DataField="cantidad" HeaderText="Unds" SortExpression="cantidad" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Bas_Direccion" HeaderText="Ubicación" SortExpression="Bas_Direccion" />
                                    <asp:TemplateField HeaderText="Marcador" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dwEmployees" Width="140px" AppendDataBoundItems="true" ToolTip="Asigne el marcador que se encargara de este pedido."
                                                runat="server" DataTextField="nombres" DataValueField="Bas_Id">
                                                <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inf" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibComm" Visible="false" OnClientClick='<%# "infoPicking(" + Eval("liq_id") + ");"%>'
                                                runat="server" ImageUrl="~/Design/images/Botones/b_comment.png" ToolTip="Liquidación Lista, Parar Tiempo de Marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ini" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibStartPicking" CommandName="startPicking" CommandArgument='<%# Eval("liq_id")%>'
                                                runat="server" ImageUrl="~/Design/images/b_clock_play.png" ToolTip="Iniciar proceso de marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fin" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibEndPicking" CommandName="endPicking" CommandArgument='<%# Eval("liq_id")%>'
                                                runat="server" ImageUrl="~/Design/images/b_clock_stop.png" ToolTip="Finalización de proceso de marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Imp" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibPrint" CommandName="print" CommandArgument='<%# Eval("liq_id")%>'
                                                runat="server" ImageUrl="~/Design/images/b_print.png" ToolTip="Ver reporte de marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Excel" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibexcel" CommandName="excel" CommandArgument='<%# Eval("liq_id")%>'
                                                runat="server" ImageUrl="~/Design/images/Botones/b_toExcel_ic.png" ToolTip="Ver reporte de marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:CheckBox ID="chkAutoUpdate" Checked="true" runat="server" Text="Actualizar marcación automáticamente." />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="TimerOrders" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:ObjectDataSource ID="odsLiqsPick" runat="server" SelectMethod="getLigsPick"
                        TypeName="www.aquarella.com.pe.Aquarella.Logistica.panelAsignPickingLiquidation"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsFilter" runat="server" SelectMethod="getFilterObject"
                        TypeName="www.aquarella.com.pe.bll.Util.Utilities" OnSelecting="odsFilter_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="dtObj" Type="Object" />
                            <asp:Parameter DefaultValue="0" Name="posTable" Type="Int32" />
                            <asp:Parameter DefaultValue="Liq_Id" Name="f1" Type="String" />
                            <asp:Parameter DefaultValue="nombres" Name="f2" Type="String" />
                            <asp:Parameter DefaultValue="pin_employee" Name="f3" Type="String" />
                            <asp:ControlParameter ControlID="txtFilter" DefaultValue="" Name="fieldValue1" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="txtFilter" Name="fieldValue2" PropertyName="Text"
                                Type="String" />
                            <asp:ControlParameter ControlID="txtFilter" Name="fieldValue3" PropertyName="Text"
                                Type="String" />
                            <asp:Parameter DefaultValue="Liq_Id" Name="fieldOrder" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <!-- PEDIDOS PARA ENTREGA EN HALL -->
                <div id="fragment-2" style="min-height: 200px; display:none;">
                    <p>
                        Pedidos para marcación en bodega y entrega en hall, facturación via punto de venta
                        y entrega personalmente.</p>
                    <asp:UpdatePanel ID="upGridHall" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvLiqHall" runat="server" SkinID="gridviewSkin" ShowFooter="true"
                                AllowSorting="true" AllowPaging="true" OnDataBound="gvLiqHall_DataBound" OnRowDataBound="gvLiqPicking_RowDataBound"
                                OnRowCommand="gvLiqPicking_RowCommand" OnRowCreated="gvLiqPicking_RowCreated">
                                <EmptyDataTemplate>
                                    No existen liquidaciones para marcar.
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:BoundField DataField="Are_Descripcion" HeaderText="Área" SortExpression="Are_Descripcion" />
                                    <asp:BoundField DataField="Liq_Id" HeaderText="No.Liq" SortExpression="Liq_Id" />
                                    <asp:BoundField DataField="Liq_EstId" HeaderText="Estdo" SortExpression="Liq_EstId"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombres" HeaderText="Cliente" SortExpression="nombres" />
                                    <asp:BoundField DataField="datedesc" HeaderText="Fecha" SortExpression="dateclear" />
                                    <asp:BoundField DataField="cleardesc" HeaderText="Liberación" SortExpression="dateClear" />
                                    <asp:BoundField DataField="ldn_qty" HeaderText="Unds" SortExpression="ldn_qty" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="ubicationcustomer" HeaderText="Ubicación" SortExpression="ubicationcustomer" />
                                    <asp:TemplateField HeaderText="Marcador" SortExpression="pin_employee" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dwEmployees" AppendDataBoundItems="true" Width="140px" ToolTip="Asigne el marcador que se encargara de este pedido."
                                                runat="server" DataTextField="nombres" DataValueField="Bas_Id">
                                                <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inf" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibComm" Visible="false" OnClientClick='<%# "infoPicking(" + Eval("liq_id") + ");"%>'
                                                runat="server" ImageUrl="~/Design/images/Botones/b_comment.png" ToolTip="Liquidación Lista, Parar Tiempo de Marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ini" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibStartPicking" CommandName="startPicking" CommandArgument='<%# Eval("liq_id")%>'
                                                runat="server" ImageUrl="~/Design/images/b_clock_play.png" ToolTip="Iniciar proceso de marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fin" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibEndPicking" CommandName="endPicking" CommandArgument='<%# Eval("liq_id")%>'
                                                runat="server" ImageUrl="~/Design/images/b_clock_stop.png" ToolTip="Finalización de proceso de marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Imp" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ibPrint" CommandName="print" CommandArgument='<%# Eval("liq_id")%>'
                                                runat="server" ImageUrl="~/Design/images/b_print.png" ToolTip="Ver reporte de marcación." />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btRefresh" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="btFilter" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="TimerOrders" EventName="Tick" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:ObjectDataSource ID="odsLiqsHall" runat="server" SelectMethod="getLiqsHalls"
                        TypeName="www.aquarella.com.pe.Aquarella.Logistica.panelAsignPickingLiquidation"></asp:ObjectDataSource>
                </div>
            </div>
        </div>
    </div>
    <%-- TOTALIZADOS --%>
    <div style="margin: 10px auto 0 auto;">
        <p class="fsal f13">
            Información de totalizado</p>
        <table class="f12">
            <tr>
                <td>
                    Número de liquidaciones en marcación:
                </td>
                <td align="center" style="width: 100px;">
                    <asp:UpdatePanel ID="upLiqInPick" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblLiqInPick" runat="server" Text="0"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    Unidades en marcación:
                </td>
                <td align="center">
                    <asp:UpdatePanel ID="upQtysInPick" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblQtysInPick" runat="server" Text="0"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    Número de liquidaciones en espera por inico de marcación:
                </td>
                <td align="center">
                    <asp:UpdatePanel ID="upLiqNotPick" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblLiqNotPick" runat="server" Text="0"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    Unidades en espera por inico de marcación:
                </td>
                <td align="center">
                    <asp:UpdatePanel ID="upQtyNotPick" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblQtyNotPick" runat="server" Text="0"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog" style="display: none;">
        <div class="f13">
            <p>
                <b>Información sobre la marcación:</b>
            </p>
            <div id="content_div">
            </div>
        </div>
    </div>
    <!-- DIALOG INFO -->
    <div id="dialog-info" style="display: none;">
        <p>
            <span class="ui-icon ui-icon-circle-check" style="float: left; margin: 0 7px 50px 0;">
            </span>
            <label id="dialog-info-labelHeader">
            </label>
        </p>
        <p>
            <label id="dialog-info-labelBody">
            </label>
        </p>
    </div>
</asp:Content>

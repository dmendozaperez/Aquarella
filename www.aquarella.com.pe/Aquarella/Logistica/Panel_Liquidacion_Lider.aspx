﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Panel_Liquidacion_Lider.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.Panel_Liquidacion_Lider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">    
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#tabs").tabs();
        });
    </script>
    <script type="text/javascript" language="javascript">
        function loadedFrame() {
            $('#loading').hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Reporte de liquidación generAQUARELLA
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Su pedido ya se encuentra liquidado y listo para ser despachado por almacen, recuerde que esta liquidacion
    es un grupo de varios pedidos de sus promotores.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%
        // Preparar la direccion de los reportes mas el paso de la variable de id de liquidacion por url.
        string noOrderUrl = "";
        if (Request.Params["NoLiq"] != null)
        {
            noOrderUrl = Convert.ToString(Request.Params["NoLiq"]);
        }
        //
        string pageReportGnrls = "../../Reports/Logistica/reportLiquidation.aspx?NoLiq=" + noOrderUrl; 
    %>
    <input id="hdNoLiq" type="hidden" value="<%=Request.Params["NoLiquidation"]%>" />
    <div style="margin: 10px auto 0 auto;" class="f13">
        <div>
            <div id="tabs">
                <ul>
                    <li><a href="#fragment-1"><span>Reporte general de liquidación</span></a></li>
                    <li><a href="#fragment-3"><span>Información</span></a></li>
                </ul>
                <!-- LIQUIDACION GENERAL -->
                <div id="fragment-1" style="min-height: 400px; height: 400px;">
                    <div id="loading" class="loadProg">
                        Cargando Reporte, por favor aguarde un momento ...
                    </div>
                    <iframe src="<%=pageReportGnrls%>" id="frameLeft" frameborder="0" marginheight="1"
                        marginwidth="1" scrolling="auto" style="border: 1px solid silver; height: 100%;
                        width: 100%;" onload="javascript:loadedFrame();"></iframe>
                </div>
                <div id="fragment-3" style="min-height: 400px; height: 400px;">
                    <iframe width="100%" style="height: 98%;"></iframe>
                </div>
            </div>
        </div>
        <table width="100%" class="f12" style="border: 1px solid silver; background-color: #f5f5f5;
            margin-top: 10px;" cellpadding="4" cellspacing="4">
            <tr>
                <td align="center">
                    <asp:Button ID="btBack" runat="server" AccessKey="v" Text="(V)olver al Agrupamiento de pedidos"
                        PostBackUrl="~/Aquarella/Financiera/Agrupar_Pedido_Pago.aspx" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

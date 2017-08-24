<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="panelInvReports.aspx.cs"
    Inherits="www.aquarella.com.pe.Aquarella.Ventas.panelInvReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
    <link href="../../Styles/theme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#tabs").tabs();
            var wh = parseInt($(window).height());
            if ($.browser.opera) { wh = $.browser.version > "9.5" ? document.documentElement["clientHeight"] : wh; }
            $('#tabs').css("height", wh - 30);
            $('#fragment-1').css("height", wh - 135);
            $('#fragment-2').css("height", wh - 135);
        });
    </script>
    <script type="text/javascript" language="javascript">
        function loadedFrame() {
            $('#loading').hide();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%
            // Preparar la direccion de los reportes mas el paso de la variable de id de liquidacion por url.
            string noOrderUrl = string.Empty, noInvo = string.Empty;
            if (Request.Params["NoLiq"] != null)
            {
                noOrderUrl = Convert.ToString(Request.Params["NoLiq"]);
            }
            if (Request.Params["NoInvo"] != null)
            {
                noInvo = Convert.ToString(Request.Params["NoInvo"]);
            }
            string pageReportIndividuals = "frameReportOfALiquidationIndividuals.aspx?NoLiquidation=" + noOrderUrl;
            //
            string pageReportGnrls = "../../Reports/Ventas/reportInvoice.aspx?NoLiquidation=" + noOrderUrl + "&NoInvoice=" + noInvo + "&ShowReportOnWeb=true";
        %>
        <div style="margin: 10px;" class="f13">
            <div>
                <div id="tabs">
                    <ul>
                        <li><a href="#fragment-1"><span>Reporte general de factura</span></a></li>
                        <li><a href="#fragment-2"><span>Facturación por promotor</span></a></li>
                    </ul>
                    <!-- FACTURA GENERAL -->
                    <div id="fragment-1" style="min-height: 400px;">
                        <div id="loading" class="loadProg">
                            Cargando Reporte, por favor aguarde un momento ...
                        </div>
                        <iframe src="<%=pageReportGnrls%>" id="frameLeft" frameborder="0" marginheight="1"
                            marginwidth="1" scrolling="auto" style="border: 1px solid silver; height: 100%;
                            width: 100%;" onload="javascript:loadedFrame();"></iframe>
                    </div>
                    <!-- FACTURACION INDIVIDUAL POR PROMOTOR -->
                    <div id="fragment-2" style="min-height: 400px;">
                        <iframe></iframe>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

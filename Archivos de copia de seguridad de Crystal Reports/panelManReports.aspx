<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="panelManReports.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.panelManReports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
      <%
        // Preparar la direccion de los reportes mas el paso de la variable de id de liquidacion por url.
        string noman = "";
        if (Request.Params["noman"] != null)
        {
            noman = Convert.ToString(Request.Params["noman"]);
        }
        //
        string pageReportGnrls = "../../Reports/Logistica/reportManifiesto.aspx?NoMan=" + noman; 
    %>

    <div style="margin: 10px;" class="f13">
        <div>
            <div id="tabs">
                <ul>
                    <li><a href="#fragment-1"><span>Reporte general de Manifiesto</span></a></li>                   
                </ul>
                <!-- LIQUIDACION GENERAL -->
                <div id="fragment-1" style="min-height: 400px;">
                    <div id="loading" class="loadProg">
                        Cargando Reporte, por favor aguarde un momento ...
                    </div>
                    <iframe src="<%=pageReportGnrls%>" id="frameLeft" frameborder="0" marginheight="1"
                        marginwidth="1" scrolling="auto" style="border: 1px solid silver; height: 100%;
                        width: 100%;" onload="javascript:loadedFrame();"></iframe>
                </div>              
            </div>
        </div>
    </div>
    </form>
</body>
</html>

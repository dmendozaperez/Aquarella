<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="panelFramesManReport.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.panelFramesManReport" %>
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
     Reporte de Manifiesto AQUARELLA
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
     El manifiesto se ha generado correctamente 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
     <div style="margin: 10px auto 0 auto;" class="f13">
        <div>
            <div id="tabs">
                <ul>
                    <li><a href="#fragment-1"><span>Reporte general de Manifiesto</span></a></li>                  
                </ul>
                <!-- MANIFIESTO GENERAL -->
                <div id="fragment-1" style="min-height: 400px; height: 400px;">
                    <div id="loading" class="loadProg">
                        Cargando Reporte, por favor aguarde un momento ...
                    </div>
                    <iframe src="<%=pageReportGnrls%>" id="frameLeft" frameborder="0" marginheight="1"
                        marginwidth="1" scrolling="auto" style="border: 1px solid silver; height: 100%;
                        width: 100%;" onload="javascript:loadedFrame();"></iframe>
                </div>               
            </div>
        </div>
        <table width="100%" class="f12" style="border: 1px solid silver; background-color: #f5f5f5;
            margin-top: 10px;" cellpadding="4" cellspacing="4">
            <tr>
                <td align="center">
                    <asp:Button ID="btBack" runat="server" AccessKey="v" Text="(V)olver al panel de Manifiesto"
                        PostBackUrl="~/Aquarella/Logistica/panelManifiesto.aspx" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="www.aquarella.com.pe.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#tabs").tabs();
            $("#fbTab").tabs();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Página inicial del sistema de información de Aquarella
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="98%">
        <tr>
            <td>
                <p class="f13 fsal">
                    <b>Políticas Aquarella</b></p>
                <p class="f12">
                    Es importante que conozca los términos y condiciones, las políticas de envío y las
                    formas de pago con las cuales funciona el catálogo de Aquarella, si usted inicia
                    transacciones o compras en nuestro sistema se asume que ha leído y aceptado a conformidad
                    nuestras políticas de funcionamiento.</p>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="100%" cellpadding="4" cellspacing="4" style="border: 1px solid silver;
                    background-color: #f4f4f4;">
                    <thead>
                        <tr>
                            <th align="left">
                                <h2>
                                    Politicas Aquarella</h2>
                            </th>
                            <th align="left">
                                <h2>
                                    Encuesta</h2>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td align="center" width="70%" valign="top">
                                <div>
                                    <div id="tabs">
                                        <ul>
                                            <li><a href="#fragment-1"><span>Términos y condiciones</span></a></li>
                                            <li><a href="#fragment-2"><span>Politicas en envios</span></a></li>
                                            <li><a href="#fragment-3"><span>Formas de pago</span></a></li>
                                        </ul>
                                        <!-- TERMINOS Y CONDICIONES -->
                                        <div id="fragment-1" style="min-height: 300px;">
                                            <iframe src="Design/Terminos_Condiciones/terms.htm" id="frameLeft" frameborder="0"
                                                marginheight="1" marginwidth="1" scrolling="auto" style="border: 1px solid silver;
                                                height: 310px; width: 100%;"></iframe>
                                        </div>
                                        <!-- TPOLITICAS DE ENVIOS -->
                                        <div id="fragment-2" style="min-height: 300px;">
                                            <iframe src="Design/Terminos_Condiciones/shipping.htm" id="Iframe1" frameborder="0"
                                                marginheight="1" marginwidth="1" scrolling="auto" style="border: 1px solid silver;
                                                height: 310px; width: 100%;"></iframe>
                                        </div>
                                        <!-- FORMAS DE PAGO -->
                                        <div id="fragment-3" style="min-height: 300px;">
                                            <iframe src="Design/Terminos_Condiciones/payment.htm" id="Iframe2" frameborder="0"
                                                marginheight="1" marginwidth="1" scrolling="auto" style="border: 1px solid silver;
                                                height: 310px; width: 100%;"></iframe>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td align="center" width="20%">
                               <%-- <script src="https://d39v39m55yawr.cloudfront.net/assets/clr.js" type="text/javascript"></script>--%>
                                <a href="https://urtak.com/clr/gbdyygukbddadvikjszwfem3vztoumcd">Aquarella Perú</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

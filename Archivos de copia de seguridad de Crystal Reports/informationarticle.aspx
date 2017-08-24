<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="informationarticle.aspx.cs"
    Inherits="www.aquarella.com.pe.Aquarella.Maestros.informationarticle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Información del Artículo</title>
    <!-- JAVASCRIPTS -->
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
    <link href="../../Styles/theme/jquery-ui-1.8.16.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/imageZoomer/featuredimagezoomer.js" type="text/javascript"></script>
    <script type="text/javascript">
        /***********************************************
        * Featured Image Zoomer (w/ adjustable power)- By Dynamic Drive DHTML code library (www.dynamicdrive.com)
        * This notice MUST stay intact for legal use
        * Visit Dynamic Drive at http://www.dynamicdrive.com/ for this script and 100s more
        ***********************************************/

        jQuery(document).ready(function ($) {

            var t = $('#<%=ImageShoe.ClientID %>').attr('src');
            ///alert('hello' + t);
            $('#<%=ImageShoe.ClientID %>').addimagezoom({
                zoomrange: [3, 10], // 5,5 -> No wheel function to zoom
                magnifiersize: [200, 220],
                magnifierpos: 'right',
                largeimage: t //<-- No comma after last option!
            })
            $("#tabs").tabs();
            ///$('#image3').addimagezoom()

        })

       

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="2" cellpadding="2" width="640" align="center" border="0">
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblTitleArt" runat="server" Style="font-family: Verdana; font-size: medium;
                    font-weight: bold;"></asp:Label>
                <asp:Label ID="lblTitleArt2" runat="server" Style="font-family: Verdana; font-size: medium;
                    font-weight: bold;"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" style="max-width: 280px; max-height: 230px; min-height: 230px;
                min-width: 280px; width: 280px; height: 230px; border: solid 1px silver;">
                <asp:Image ID="ImageShoe" Style="max-width: 98%; max-height:60%;" 
                    runat="server" Height="194px" Width="239px" />
            </td>
            <td>
                <div>
                    <div id="tabs">
                        <ul>
                            <li><a href="#fragment1"><span>Artículo</span></a></li>
                            <li><a href="#fragment2" style="display:none"><span>Otros detalles</span></a></li>
                            <li id="infoemploye" runat="server"><a href="#fragment3"><span>Información empleado</span></a></li>
                        </ul>
                        <div id="fragment1" style="min-height: 200px;">
                            <table class="tableseparator" width="95%">
                                <tr>
                                    <td>
                                        <span><span>Cod Articulo :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCodArticle" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Nombre Articulo :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbNameArticle" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Marca Articulo :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbBrandArticle" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Tallage :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbTallas" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Color :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbColor" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Categorizacion<span>:</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbArticleType" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td>
                                        <span><span>Colección :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCollection" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="display:none">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <th class="f13" align="left">
                                        Precio Público :
                                    </th>
                                    <td style="font-weight: bold;" align="right">
                                        <asp:Label ID="lbPublic_Price" CssClass="fsal f13" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="fragment2" style="min-height: 200px;">
                            <table class="tableseparator">
                                <tr>
                                    <td>
                                        <span><span>Categorización :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbCategorization" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Material :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbMaterial" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Origen :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbOrigin" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Diseño :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbDesign" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Empaque :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbPacking" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Suela :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbSole" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>CapellAQUARELLA :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbUpper" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Tacon :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbHeeled" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="fragment3" style="min-height: 200px;" runat="server">
                            <table class="tableseparator">
                                <tr>
                                    <td>
                                        <span><span>Margen de Utilidad :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbMargen" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Proveedor :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSupplier" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span><span>Costo :</span></span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbODV" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btnCerrar" Visible="false" runat="server" ImageUrl="~/Design/images/Botones/b_cerrar.gif"
                                AlternateText="Cerrar ventana (Alt C)" AccessKey="c" CausesValidation="False"
                                OnClientClick="javascript:Close()" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>

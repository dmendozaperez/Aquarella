<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    StylesheetTheme="SiteTheme" CodeBehind="stockForm.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.stockForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script type="text/javascript" src="../../Scripts/ColorBox/jquery.colorbox.js"></script>
    <link href="../../Scripts/ColorBox/colorbox.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                $(".iframe").colorbox({ width: "90%", height: "94%", iframe: true });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Consulta de Stock
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Consulte el stock disponible de un articulo.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="99999">
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
    <!-- FORMULARIO DE BUSQUEDA -->
    <div style="margin: 10px auto 0 auto;">
        <table class="tablagris" cellpadding="4" width="100%">
            <tr>
                <td class="fsal f13" colspan="2">
                    Formulario de consulta
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <table cellpadding="4" cellspacing="4">
                                    <tr>
                                        <td class="f12">
                                            Código o nombre del artículo a consulta:<br />
                                            (Desc: consulte por código y/ó Nombre de artículo: use comodines:%_)
                                        </td>
                                        <td>
                                            <table cellpadding="1" cellspacing="1">
                                                <tr>
                                                    <td>
                                                        <asp:UpdatePanel ID="upTxtItem" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="TxtItem" runat="server" AccessKey="c"></asp:TextBox>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ValidationGroup="vsConsultForm" ID="rfvDateStart" runat="server"
                                                            ToolTip="Código o referencia de articulo" CssClass="error_asterisck" ErrorMessage="Dígite Código o referencia de articulo"
                                                            Display="Dynamic" SetFocusOnError="true" ControlToValidate="TxtItem">*</asp:RequiredFieldValidator>
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:Button ID="btConsult" runat="server" Text="Consultar" ValidationGroup="vsConsultForm"
                                                            CausesValidation="true" OnClick="btConsult_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>
    </div>
    <!-- RESULTADOS -->
    <div style="margin: 10px auto 0 auto;" class="f-small">
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gvStock" runat="server" Width="95%" SkinID="gridviewSkin" AutoGenerateColumns="true"
                                HorizontalAlign="Center" OnRowCreated="gvStock_RowCreated">
                                <EmptyDataTemplate>
                                    <span style="color: Maroon">Lo Sentimos Pero del Artículo que Busca no Se Encontraron
                                        Existencias en el Stock.</span>
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:TemplateField HeaderText="Foto" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhotoArticle" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btConsult" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <!-- ACTIVITY PANEL -->
                    <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
                        <Animations>
                                <OnUpdating>
                                    <Sequence>
                                        <%-- Disable all the controls --%>
                                        <Parallel duration="0">
                                            <EnableAction AnimationTarget="btConsult" Enabled="false" />                                                   
                                        </Parallel>
                                    </Sequence>
                                </OnUpdating>
                                <OnUpdated>
                                    <Sequence>
                                        <%-- Enable all the controls --%>
                                        <Parallel duration="0">
                                            <EnableAction AnimationTarget="btConsult" Enabled="true" />
                                        </Parallel>
                                    </Sequence>
                                </OnUpdated>
                        </Animations>
                    </ajaxToolkit:UpdatePanelAnimationExtender>
                    <asp:ObjectDataSource ID="odsConsult" runat="server" SelectMethod="getSource" TypeName="www.aquarella.com.pe.Aquarella.Logistica.stockForm">
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

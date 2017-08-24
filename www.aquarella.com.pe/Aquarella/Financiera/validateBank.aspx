<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Design/Site.Master"   StylesheetTheme="SiteTheme" 
CodeBehind="validateBank.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Financiera.validateBank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">   
 <script type="text/javascript">
     var btGuardarDatos_2, btnUpload, dwTipArc, allFieldsBd, tips, FileUpload1, msnMessage, dwBanks;
    </script> 
    <script type="text/javascript" language="javascript">
        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
       
        $(document).ready(function () {
            logica();
        });

        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                $("input:submit,button").button();
                    logica();
            }
        }
     </script>

    <script type="text/javascript" language="javascript">

        function logica() {

            dwTipArc = $("select[id$='dwTipArc']");
            dwBanks = $("select[id$='dwBanks']");
            btGuardarDatos_2 = $("input[id$='btGuardarDatos_2']");
            btnUpload = $("input[id$='btnUpload']");
            FileUpload1 = $("input[id$='FileUpload1']");
            allFieldsBd = $([]).add(dwTipArc).add(FileUpload1).add(validateTips).add(dwBanks);
            tips = $("#validateTips");
            btnUpload = $("input[id$='btnUpload']");
            msnMessage = $("input[id*='ContentPlaceHolder1_msnMessage_lblType']");

            btGuardarDatos_2.click(function () {
                var bValid = true;
                tips.text("");
                allFieldsBd.removeClass("ui-state-error");

                if (dwBanks.val() == "-1" || dwBanks.val() == "") {
                    bValid = false;
                    dwBanks.addClass("ui-state-error")
                    btGuardarDatos_2.addClass("ui-state-error")
                    updateTips(" Seleccione tipo de archivo.");
                }

                scrollTop();
                return bValid;
            });
        }

        function updateTips(t) {
            tips
			.text(tips.text() + t)
			.addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }

        function scrollTop() {
            $('html, body').animate({ scrollTop: '0px' }, 800);
        }

    </script>
    <style type="text/css">
        .style1
        {
            width: 222px;
        }
        .style2
        {
            width: 129px;
        }
        .style3
        {
            width: 241px;
        }
        .style4
        {
            width: 168px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Validacion de pagos por medio del archivo del banco
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p id="validateTips">
    </p>
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
            <asp:AsyncPostBackTrigger ControlID="btGuardarDatos_2" EventName="click" />
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
    <asp:Panel ID="pnlDwCustomers" Visible="true" runat="server">
        <div style="margin: 10px auto 0 auto;">
            <table width="100%" cellpadding="4" class="f-small">
                <tr>
                    <td class="style1">
                        *Entidad recaudadora</td>
                    <td class="style3">
                        <asp:DropDownList ID="dwBanks" runat="server" AccessKey="b" 
                            AppendDataBoundItems="True" DataTextField="ban_descripcion" 
                            DataValueField="ban_id" ValidationGroup="valerror" Width="280px">
                            <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    <td align="right" class="style4">
                        &nbsp;</td>
                    <td class="style2">
                        &nbsp;</td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                        *Tipo de archivo</td>
                    <td class="style3">
                        <asp:DropDownList ID="dwTipArc" runat="server" AccessKey="b" 
                            AppendDataBoundItems="True" ValidationGroup="valerror" Width="280px">
                            <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            <asp:ListItem Value="1">ARCHIVO DEL DIA</asp:ListItem>
                            <asp:ListItem Value="2">ARCHIVO HISTORIAL</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" class="style4">
                        &nbsp;</td>
                    <td class="style2">
                        &nbsp;</td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                        
                        *Adjuntar archivo del Banco:<br /> (Unicamente extensión .XLSX)</td>
                    <td class="style3">
                	     <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                    <td align="left" class="style4">
                        <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" 
                            Text="Cargar Archivo" />
                    </td>
                    <td class="style2">
                        
                        <asp:Button ID="btGuardarDatos_2" runat="server" AccessKey="o" Enabled="false" 
                            OnClick="btGuardarDatos_2_Click" Text="(G)uardar datos" />
                        
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
         
    </asp:Panel>
              
    <!-- PANEL DE RESULTADOS -->
    <div style="margin: 10px auto 0 auto; min-height: 200px;" class="f13">
        <asp:UpdatePanel ID="upGridClear" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" AllowPaging="false" AllowSorting="false"
                   SkinID="gridviewSkin"  Width="100%">
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btGuardarDatos_2" EventName="click" />
           
              </Triggers>
        </asp:UpdatePanel>
        <ajaxToolkit:UpdatePanelAnimationExtender runat="server" ID="upae" TargetControlID="upGridClear">
            <Animations>
                    <OnUpdating>
                        <Sequence>
                            <%-- Disable all the controls --%>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btGuardarDatos_2" Enabled="false" />
                                
                            </Parallel>
                        </Sequence>
                    </OnUpdating>
                    <OnUpdated>
                        <Sequence>
                            <%-- Enable all the controls --%>
                            <Parallel duration="0">
                                <EnableAction AnimationTarget="btnUpload" Enabled="true" />
                                <EnableAction AnimationTarget="FileUpload1" Enabled="true" />
                            </Parallel>
                        </Sequence>
                    </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
   
</asp:Content>

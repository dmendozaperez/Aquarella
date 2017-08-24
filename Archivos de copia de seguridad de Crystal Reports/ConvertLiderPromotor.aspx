<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="ConvertLiderPromotor.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.ConvertLiderPromotor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .style1
        {
            width: 46px;
        }
        .style2
        {
            width: 273px;
        }
        .style3
        {
            width: 46px;
            height: 42px;
        }
        .style4
        {
            width: 273px;
            height: 42px;
        }
        .style5
        {
            height: 42px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
<script type="text/javascript">
    var  dwlider, valid, allFields, allFieldsCust, allFieldsBd, isr = ' es requerido.', tips, btValidateDoc,
			btnaceptar;
    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            logica();
        });
        // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
        function pageLoad() {
            var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
            if (isAsyncPostback) {
                //$("input:text,select").width('200px');
                $("input:submit,button").button();
                logica();
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function logica() {
            dwlider = $("select[id$='dwlider']");
            //
            btnaceptar = $("input[id$='btnaceptar']");
          
           
            dwlider = $("select[id$='dwlider']");
           

            allFieldsBd = $([]).add(dwlider);
           
            tips = $("#validateTips");
            //
            $("input:text").width('200px');


            btnaceptar.click(function () {
                var bValid = true;

                bValid = bValid && validateBasicInfo();
             

                scrollTop();

                return bValid;
            });

         
        }

        function validateBasicInfo() {
            var bValid = true;
            tips.text("");

            allFieldsBd.removeClass("ui-state-error");


            //
            if (dwlider.val() == "-1" || dwlider.val() == "") {
                bValid = false;
                dwlider.addClass("ui-state-error")
                updateTips(" Seleccione Lider.");
            }
            
            //


            dwlider.focus();
            return bValid;
        }

       
        //
        function scrollTop() {
            $('html, body').animate({ scrollTop: '0px' }, 800);
        }
        //
    </script>
    <script type="text/javascript" language="javascript">

        // Validaciones
        function updateTips(t) {
            tips
			.text(tips.text() + t)
			.addClass("ui-state-highlight");
            setTimeout(function () {
                tips.removeClass("ui-state-highlight", 1500);
            }, 500);
        }

        

      
    </script>



    Convertir Lider-Promotor
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    &nbsp;Esta ventana convertira Lider-Promotor 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <p id="validateTips">
    </p>
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
     <asp:UpdatePanel ID="upPanelMsg" runat="server">
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
    <table style="width: 100%;">
        <tr>
            <td class="style3">
                Lider</td>
            <td class="style4">
                                    <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="dwlider" runat="server" AppendDataBoundItems="true" 
                                                DataTextField="arv_description" DataValueField="arv_area_id" 
                                                ToolTip="Selecionar un lider" Width="280px">
                                                <asp:ListItem Text=" -- Seleccionar un Lider --" Value="-1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnaceptar" EventName="Click" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
            <td class="style5">
                </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                Si usted desea que este Lider se convierta a Lider-Promotor, por favor dale 
                click en Aceptar</td>
            <td>
                <asp:Button ID="btnaceptar" runat="server" Text="Aceptar" SortExpression="lider" CausesValidation="true"
                    onclick="btnaceptar_Click" />
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
           <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btnaceptar" Enabled="false" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btnaceptar" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </table>
</asp:Content>

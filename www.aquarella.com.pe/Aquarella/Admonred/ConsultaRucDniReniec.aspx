<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaRucDniReniec.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.ConsultaRucDniReniec" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
        .style2
        {
            width: 171px;
        }
        .style8
        {
            width: 215px;
            height: 33px;
        }
        .style9
        {
            width: 130px;
            height: 33px;
        }
        .style10
        {
            height: 33px;
        }
        .style11
        {
            width: 215px;
            height: 24px;
        }
        .style12
        {
            width: 130px;
            height: 24px;
        }
        .style13
        {
            height: 24px;
        }
        .auto-style1 {
            width: 51%;
        }
        .auto-style4 {
            width: 164px;
        }
    </style>
    <script type="text/javascript">
        var txtDoc, btValidateDoc;			
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
             txtDoc = $("input[id$='txtDoc']");            
             btValidateDoc = $("input[id$='btValidateDoc']");             
             //
             allFields = $([]).add(txtDoc);
             allFieldsBd = $([]).add(txtDoc);
             
             //
             tips = $("#validateTips");
             //
             $("input:text").width('200px');
             //
             btValidateDoc.click(function () {
                 var bValid = true;
                 tips.text("");
                 allFields.removeClass("ui-state-error");

                 bValid = checkLength(txtDoc, "Número de documento", 8, 11);

                 scrollTop();

                 return bValid;
             });
                        
         }

         function validateBasicInfo() {
             var bValid = true;
             tips.text("");

             allFieldsBd.removeClass("ui-state-error");

             bValid = bValid && checkLength(txtDoc, " Número de documento", 8, 11);                                                                 

             txtDoc.focus();
             return bValid;
         }

         
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

        function checkLength(o, n, min, max) {
            if (o.val().length > max || o.val().length < min) {
                o.addClass("ui-state-error");
                updateTips("Tamaño del campo " + n + " debe estar entre " +
					min + " y " + max + ". ");
                return false;
            } else {
                return true;
            }
        }

        //
        function checkRegexp(o, regexp, n) {
            if (!(regexp.test(o.val()))) {
                o.addClass("ui-state-error");
                updateTips(n);
                return false;
            } else {
                return true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    <asp:Label ID="lblTitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Recuerde que esta consulta es un servicio web que brinda (Sunat-Reniec) con numero de documento (Dni-Ruc)
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
    <br />
     <asp:Panel ID="pnlValidateDoc" runat="server">
        <fieldset>
            <legend class="fsal f13">&nbsp;Consultar documento&nbsp;</legend>
            <p class="f12" style="padding: 5px;">
                Digite el número de documento (sin puntos, comas o espacios) y el codigo de verificacion
                , para verificar si es un documento existe en Sunat o Reniec..</p>
            <table cellpadding="6" cellspacing="6" class="auto-style1">
                <tr>
                    <td  style="color:black; font-weight: bold"" class="auto-style4">                        
                        <label for="txtDoc" class="f12">
                            Número de documento:</label>
                        </td>
                    <td class="style9">
                        <asp:TextBox ID="txtDoc" runat="server"></asp:TextBox>
                    </td>
                    <td class="style10">
                        <asp:Button ID="btValidateDoc" runat="server" Text="Consultar documento" 
                            OnClick="btValidateDoc_Click" TabIndex="2" />
                    </td>
                </tr>
              <%--  <tr>
                    <td class="style11">
                        <label for="txtimagen" class="f12">
                        Ingrese los caracteres de la Imagen:</label>
                        </td>
                    <td class="style12">
                        <asp:TextBox ID="txtimagen" runat="server" TabIndex="1"></asp:TextBox>
                    </td>
                    <td class="style13">
                        </td>
                </tr>--%>
               <%-- <tr>
                    <td class="style11">
                        <asp:Image ID="pictureCapcha" runat="server" Height="38px" Width="215px" />
                    </td>
                    <td class="style12">
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" 
                            ImageAlign="Baseline" ImageUrl="~/Design/images/bt_refresh.png" 
                            onclick="ImageButton1_Click" Width="25px" TabIndex="7" />
                    </td>
                    <td class="style13">
                        &nbsp;</td>
                </tr>--%>
            </table>
        </fieldset>
    </asp:Panel>
     <!-- INFORMACION BASICA DE PERSONA -->
    <br />
            <!-- DATOS ESPECIFICOS DE CLIENTE -->
    <fieldset id="fsSunat" style="display:none"  >  
        <legend class="fsal f13">Información Sunat ó Reniec [Verifique sus datos en el sistema]</legend>
        <table cellspacing="6" cellpadding="6" width="100%" bgcolor="#CCFF99">
             <tr>
                <td class="style2">
                   <label for="lblnomb" class="f12">
                                  <b>Dni ó Ruc</b> :</label></td>
                <td >
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbldniruc" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />                            
                            </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style2">
                   <label for="lblnomb" class="f12">
                                  <b>Nombres ó Razón Social</b> :</label></td>
                <td >
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblnombreruc" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />                            
                            </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="style2" >
                 <label for="lbldir" class="f12">
                                  <b>Dirección</b> :</label></td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbldireccionruc" runat="server" Font-Bold="True" 
                                Font-Size="Small"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />                            
                            </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

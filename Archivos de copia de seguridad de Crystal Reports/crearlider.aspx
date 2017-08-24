<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Design/Site.Master" CodeBehind="crearlider.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.crearlider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <script type="text/javascript">
        var txtDoc, dwDocType, dwWare, txtFirstName, txtFirstSurname, dwPersonType, txtPhone, txtMovil, txtAddress, txtMail, dwCity, dwArea,
			dwCustType, dwHandlingType, valid, allFields, allFieldsCust, allFieldsBd, isr = ' es requerido.', tips, btValidateDoc,
			btSaveNewCust, btUpdateCust, btUpdateProm, btSaveNewProm;
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
            dwDocType = $("select[id$='dwDocType']");
            dwWare = $("select[id$='dwWare']");
            dwCity = $("select[id$='dwCity']");
            dwPersonType = $("select[id$='dwPersonType']");
            txtFirstName = $("input[id$='txtFirstName']");
            txtFirstSurname = $("input[id$='txtFirstSurname']");
            txtPhone = $("input[id$='txtPhone']");
            txtAddress = $("input[id$='txtAddress']");
            txtMail = $("input[id$='txtMail']");
            //
            btValidateDoc = $("input[id$='btValidateDoc']");
            btSaveNewCust = $("input[id$='btSaveNewCust']");
            btUpdateProm = $("input[id$='btUpdateProm']");
            btSaveNewProm = $("input[id$='btSaveNewProm']");
            btUpdateCust = $("input[id$='btUpdateCust']");
            // Customer
            dwArea = $("select[id$='dwArea']");
            //dwCustType = $("select[id$='dwCustType']");
            //dwHandlingType = $("select[id$='dwHandlingType']");
            //
            allFields = $([]).add(txtDoc);
            allFieldsBd = $([]).add(txtDoc).add(txtFirstName).add(txtPhone).add(txtAddress).add(dwDocType).add(dwWare).add(dwCity).add(dwPersonType);
            //allFieldsCust = $([]).add(txtMail).add(dwArea).add(dwCustType).add(dwHandlingType);
            allFieldsCust = $([]).add(txtMail).add(dwArea);
            //
            tips = $("#validateTips");
            //
            $("input:text").width('200px');
            //
            btValidateDoc.click(function () {
                var bValid = true;
                tips.text("");
                allFields.removeClass("ui-state-error");

                bValid = checkLength(txtDoc, "Número de documento", 5, 12);

                scrollTop();

                return bValid;
            });

            btSaveNewCust.click(function () {
                var bValid = true;

                bValid = bValid && validateBasicInfo();
                bValid = bValid && validateCustomer();

                scrollTop();

                return bValid;
            });

            btUpdateProm.click(function () {
                var bValid = true;

                bValid = bValid && validateBasicInfo();

                scrollTop();

                return bValid;
            });


            btSaveNewProm.click(function () {
                var bValid = true;

                bValid = bValid && validateBasicInfo();

                scrollTop();

                return bValid;
            });

            btUpdateCust.click(function () {
                var bValid = true;

                bValid = bValid && validateBasicInfo();
                bValid = bValid && validateCustomer();

                scrollTop();

                return bValid;
            });
        }

        function validateBasicInfo() {
            var bValid = true;
            tips.text("");

            allFieldsBd.removeClass("ui-state-error");

            //bValid = bValid && checkLength(txtDoc, " Número de documento", 5, 12);
            bValid = bValid && checkLength(txtFirstName, " Primer nombre", 2, 50);
            if (txtFirstName.val().length== 0)
                {
                bValid = bValid && checkLength(txtFirstSurname, " Primer apellido", 2, 50);
            }
           // bValid = bValid && checkLength(txtPhone, " Telefono", 7, 20);
            bValid = bValid && checkLength(txtAddress, " Dirección", 10, 125);

            //
            if (dwDocType.val() == "-1" || dwDocType.val() == "") {
                bValid = false;
                dwDocType.addClass("ui-state-error")
                updateTips(" Seleccione tipo de documento.");
            }
            //
//            if (dwWare.val() == "-1" || dwWare.val() == "") {
//                bValid = false;
//                dwWare.addClass("ui-state-error")
//                updateTips(" Seleccione una bodega de atención.");
//            }
            //
            if (dwPersonType.val() == "-1" || dwPersonType.val() == "") {
                bValid = false;
                dwPersonType.addClass("ui-state-error")
                updateTips(" Seleccione el tipo de persona.");
            }
            //
            if (dwCity.val() == "-1" || dwCity.val() == "") {
                bValid = false;
                dwCity.addClass("ui-state-error")
                updateTips(" Seleccione ciudad.");
            }

            txtDoc.focus();
            return bValid;
        }

        function validateCustomer() {
            var bValid = true;
            tips.text("");
            allFieldsCust.removeClass("ui-state-error");
            bValid = bValid && checkRegexp(txtMail, /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i, " Dígite un correo válido. ej. user@bata.com.");
            //
            if (dwArea.val() == "-1" || dwArea.val() == "") {
                bValid = false;
                dwArea.addClass("ui-state-error")
                updateTips(" Seleccione el area de influencia.");
            }
            //
           
            txtDoc.focus();
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
    <style type="text/css">
        .style2
        {
            width: 10%;
        }
        .auto-style5 {
            width: 66px;
        }
        .auto-style9 {
            width: 187px;
        }
        .auto-style10 {
            width: 179px;
        }
        .auto-style11 {
            width: 576px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    <asp:Label ID="lblTitle" runat="server"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Recuerde que los campos marcados con un asterisco (*), son campos requeridos, y
    no podrán estar sin tramite.
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
            <legend class="fsal f13">&nbsp;Verificacion de documento de identidad&nbsp;ó Ruc</legend>
            <p class="f12" style="padding: 5px;">
                Por favor dale click en en boton consultar para verificar si el documento de identidad es valido.</p>
            <div id="dialog-documento" title="Ingrese Documento a Validar"  >
           <table cellpadding="6" cellspacing="6" class="auto-style11">
                <tr>
                    <td  style="color:black; font-weight: bold"" class="auto-style10">                        
                        <label for="txtDoc" class="f12">
                            Número de documento:</label>
                        </td>
                    <td class="style9">
                        <asp:TextBox ID="txtDoc" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style9">
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
        <fieldset id="fsSunatdata" style="display:none;"  >  
        <legend class="fsal f13">Información Sunat [Verifique sus datos en el sistema]</legend>
        <table cellspacing="6" cellpadding="6" width="100%" bgcolor="#CCFF99">
            <tr>
                <td class="auto-style5">
                   <label for="lblnomb" class="f12">
                                  <b>Nombres</b> :</label></td>
                <td >
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>                            
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />                            
                            </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>          
        </table>
    </fieldset>
    </div>
        </fieldset>
    </asp:Panel>
    <!-- INFORMACION BASICA DE PERSONA -->
    <br />
        <!-- DATOS ESPECIFICOS DE CLIENTE -->
    <fieldset id="fsSunat" style="display:none"   >
        <legend class="fsal f13">Información [Verifique sus datos en el sistema]</legend>
        <table cellspacing="6" cellpadding="6" width="100%" bgcolor="#CCFF99">
            <tr>
                <td class="style4">
                   <label for="lblnomb" class="f12">
                                  <b>Nombres</b> :</label></td>
                <td >
                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblnombreruc" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>           
        </table>
    </fieldset>


    <fieldset id="fsBasicData" style="display:none;" >
        <legend class="fsal f13">&nbsp;Datos basicos de información personal&nbsp; </legend>
        <table cellspacing="6" cellpadding="6" width="100%" border="0">
            <tr>
                <td width="50%">
                    DNI<asp:UpdatePanel ID="UpdatePanel29" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtdni" runat="server" MaxLength="54" AccessKey="p" ReadOnly="true"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td width="50%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="50%">
                    <label for="dwDocType" class="f12">
                        *<u>T</u>ipo documento:
                    </label>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="t" ID="dwDocType" runat="server" DataValueField="Doc_Tip_Id"
                                DataTextField="Doc_Tip_Descripcion" AppendDataBoundItems="True">
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <label for="dwDocType">
                        (Ej: c.c., ce, etc)
                    </label>
                </td>
                <td width="50%">
                    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="panelDwBodegas" Visible="false" style="display:none" runat="server">
                                <label for="dwWare">
                                    *<u>B</u>odega De Atención :
                                </label>
                                <asp:DropDownList ID="dwWare" DataValueField="wav_warehouseid" DataTextField="wav_description"
                                    runat="server" AppendDataBoundItems="true" AutoPostBack="True">
                                    <asp:ListItem Text="-- Seleccione De La Lista --" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="dwWare">
                                    (Desc: Bodega por donde se atenderá el cliente.)
                                </label>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <label for="txtFirstName" class="f12">
                        *<u>P</u>rimer Nombre :</label>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtFirstName" runat="server" MaxLength="54" AccessKey="p"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtFirstName">
                        (Desc: Primer nombre)</label>
                </td>
                <td width="50%">
                    <label for="txtMiddleName" class="f12">
                        <u>S</u>egundo Nombre :</label>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtMiddleName" runat="server" MaxLength="54" AccessKey="p"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtMiddleName">
                        (Desc: Segundo nombre)</label>
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <label for="txtFirstSurname" class="f12">
                        *P<u>r</u>imer Apellido :
                    </label>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtFirstSurname" runat="server" MaxLength="54" AccessKey="r"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtFirstSurname">
                        (Desc: Primer apellido)</label>
                </td>
                <td width="50%">
                    <label for="txtSecondSurname" class="f12">
                        S<u>e</u>gundo Apellido :</label>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtSecondSurname" runat="server" MaxLength="54" AccessKey="e"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtSecondSurname">
                        (Desc: Segundo apellido)</label>
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <table>
                        <tr>
                            <td colspan="2" class="f12">
                                <u>F</u>echa Nacimiento :
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtBirth" runat="server" MaxLength="12" AccessKey="f"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="calendar" runat="server" TargetControlID="txtBirth"
                                            Animated="true" PopupButtonID="ImageCalendarFecha" FirstDayOfWeek="Monday" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Image ID="ImageCalendarFecha" runat="server" ImageUrl="~/Design/images/Botones/b_calendar_ico.gif"
                                    onmouseover="this.style.background='red';" onmouseout="this.style.background=''"
                                    Style="cursor: hand;" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                (Desc: Fecha de nacimiento: dd/mm/aaaa)
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="50%">
                    <label for="rbSex" class="f12">
                        *Sexo</label>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:RadioButtonList ID="rbSex" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="F" Text="Femenino" Selected="true"></asp:ListItem>
                                <asp:ListItem Value="M" Text="Masculino"></asp:ListItem>
                            </asp:RadioButtonList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="rbSex">
                        (Desc: Masculino, Femenino)</label>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <!-- INFORMACION DE TIPO DE PERSONA -->
    <fieldset id="fsTypePerson" style="display: none;">
        <legend class="fsal f13">&nbsp;Tipo de persona y otros aspectos&nbsp;</legend>
        <table cellspacing="6" cellpadding="6" width="100%" border="0">
            <tr>
                <td width="50%">
                    <label for="dwPersonType" class="f12">
                        *Tip<u>o</u> de Persona:</label>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwPersonType" runat="server" DataValueField="per_tip_id"
                                DataTextField="per_tip_descripcion" AppendDataBoundItems="True">
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="dwPersonType">
                        (Desc: Persona jurídica, natural, etc. )
                    </label>
                </td>
                <td width="50%">
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <label for="txtMail" class="f12">
                        *E-Ma<u>i</u>l:</label>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtMail" runat="server" MaxLength="54" AccessKey="x"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtMail">
                        (Ej: NombreDeCorreo@AquarellaPeru.com.pe)</label>
                </td>
                <td width="50%">
                    <label for="txtPhone" class="f12">
                         Telé<u>f</u>ono:
                    </label>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="54" AccessKey="f"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtPhone">
                        (Desc: Teléfono)</label>
                </td>
            </tr>
            <tr>
                <td width="50%">
                    <label for="txtFax" class="f12">
                        Número Fa<u>x</u>:
                    </label>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtFax" runat="server" MaxLength="54" AccessKey="x"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtFax">
                        (Desc: Número de Fax)</label>
                </td>
                <td width="50%">
                    <label for="txtCel" class="f12">
                        Número Ce<u>l</u>ular:</label>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtCel" runat="server" MaxLength="54" AccessKey="l"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtCel">
                        (Desc: Teléfono)</label>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <!-- UBICACION -->
    <fieldset id="fsUbi" style="display:none;" >
        <legend class="fsal f13">&nbsp;Ubicación&nbsp;</legend>
        <table cellspacing="6" cellpadding="6" width="100%" border="0">
         <tr>
                <td>
                <label for="dwdepartamento" class="f12">
                        *Departamento:</label>
                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwdepartamento" runat="server" DataValueField="Dep_Id"
                                DataTextField="Dep_Descripcion" AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="dwdepartamento_SelectedIndexChanged">
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="dwdepartamento" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td> 
                     <label for="dwprovincia" class="f12">
                        *Provincia:</label>
                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwprovincia" runat="server" DataValueField="Prv_Id"
                                DataTextField="Prv_Descripcion" AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="dwprovincia_SelectedIndexChanged">
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                            <asp:AsyncPostBackTrigger ControlID="dwprovincia" 
                                EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
            </tr>
            <tr>
                <td>
                 <label for="dwdistrito" class="f12">
                        *Distrito:     </label><asp:UpdatePanel ID="UpdatePanel16" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwCity" runat="server" DataValueField="dis_id"
                                DataTextField="dis_descripcion" AppendDataBoundItems="True">
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    </td>
            </tr>
            <tr>
                <td width="50%">
                    <label for="txtAddress" class="f12">
                        *<u>D</u>irección:
                    </label>
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtAddress" runat="server" ToolTip="Maximo 125 caracteres" MaxLength="129" AccessKey="d"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="txtAddress">
                        (Desc: Dirección y Distrito)</label>
                </td>
            </tr>
            <tr>
                <td>
                    <label for="dwArea" class="f12">
                        *Lider o Lider Zonal:
                    </label>
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwArea" runat="server" DataValueField="Are_Id"
                                DataTextField="Are_Descripcion" AppendDataBoundItems="True">
                                
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <label for="dwArea">
                        (Ej: Lider lima, etc.)</label>
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <!-- DATOS ESPECIFICOS DE CLIENTE -->
     <fieldset id="fsInfoCust" style="display: none">
        <legend class="fsal f13">Información especifica de cliente</legend>
        <table cellspacing="6" cellpadding="6" width="100%">
            <tr style="display:none">
                <td width="50%">
                    <label for="dwCustType" class="f12">
                        *Tipo Cliente:</label>
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwCustType" runat="server" DataValueField="Usu_Tip_Id"
                                DataTextField="Usu_Tip_Nombre" AppendDataBoundItems="True">
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <label for="dwCustType">
                        (Ej: Cliente básico, etc.)</label>
                </td>
                <td width="50%">
                </td>
            </tr>
            <tr style="display:none">
                <td>
                    <label for="dwHandlingType" class="f12">
                        *Manejo/Envio De Pedidos:
                    </label>
                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwHandlingType" runat="server" DataValueField="htv_handling_id"
                                DataTextField="htv_description" AppendDataBoundItems="True">
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <label for="dwHandlingType">
                        (Ej: Tipo de flete o valor de envio.)
                    </label>
                </td>
                <td width="50%">
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <label for="lblagencia" class="f12">
                        Agencia :</label>
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtagencia" runat="server" MaxLength="100" AccessKey="p" Width="450px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>                    
                            </td>                                                      
                        </tr>   
                        <tr>
                            <td>
                                <label for="lbldestino" class="f12">
                        Destino :</label>
                    <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtdestino" runat="server" MaxLength="200" AccessKey="p" Width="450px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>            
                            </td>
                        </tr>    
                        <tr>
                             <td>
                                <label for="lblagencia_ruc" class="f12">
                        Agencia Ruc :</label>
                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtagenciaruc" runat="server" MaxLength="11" AccessKey="p" Width="450px"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btValidateDoc" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>                    
                            </td>  
                        </tr>               
                    </table>
                </td>
            </tr>
        </table>
    </fieldset>
    <!-- BOTONES -->
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" style="border: 1px solid silver;" cellpadding="4" cellspacing="4"
            class="f-small">
            <tr>
                <td align="center">
                    <asp:Button AccessKey="v" ID="btBack" runat="server" Text="(V)olver" ToolTip="Volver" style="display:none;"
                        OnClick="btBack_Click" />
                </td>
                <td align="center">
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btUpdateCust" runat="server" AccessKey="c" Text="A(c)tualizar cliente"
                                Visible="false" OnClick="btUpdateCust_Click" />
                            <asp:Button ID="btUpdateProm" runat="server" AccessKey="c" Text="A(c)tualizar promotor"
                                Visible="false" OnClick="btUpdateProm_Click" />
                            <asp:Button ID="btSaveNewProm" runat="server" AccessKey="c" Text="(C)rear Lider"
                                Visible="false" OnClick="btSaveNewProm_Click" />
                            <asp:Button ID="btSaveNewCust" runat="server" AccessKey="c" Text="(C) Crear cliente"
                                Visible="false" OnClick="btSaveNewCust_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

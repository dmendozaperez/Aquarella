<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contactenos.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.Contactenos" Culture="es-PE" StyleSheetTheme="" uiCulture="es-PE" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">    
 <%--<script src="../../Scripts/ImgScale/jquery.cj-object-scaler.min.js" type="text/javascript"></script>--%>    

 <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
 <script src="../../Scripts/jquery-ui-1.8.24.custom.min.js" type="text/javascript"></script>
 <script src="../../Scripts/ImgScale/jquery.cj-object-scaler.min.js" type="text/javascript"></script> 
 <script src="../../Scripts/Colorbox/jquery.colorbox.js" type="text/javascript"></script>


<html xmlns="http://www.w3.org/1999/xhtml"> 

<script type="text/JavaScript" language="JavaScript">
    function pageLoad() {
        var manager = Sys.WebForms.PageRequestManager.getInstance();
        manager.add_endRequest(endRequest);
        manager.add_beginRequest(OnBeginRequest);
    }
    function OnBeginRequest(sender, args) {
        var postBackElement = args.get_postBackElement();
        if (postBackElement.id == 'btnClear') {
            $get('UpdateProgress1').style.display = "block";
        }
    }
</script>


<script language="javascript" type="text/javascript">
    function cambiarEstilo() {
        document.getElementById('enviar').style.backgroundColor = '#eef1c4';       
    }
</script>

<script type="text/javascript">
    var txtnombres, txtapellidos, txttelefono, txtemail, valid, allFields, allFieldsCust, allFieldsBd, isr = ' es requerido.', tips, btenviar, dwdistrito;			
</script>     

<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        logica();
    });
    // Habilitar el thickbox despues de una llamAQUARELLA asincrona por el ajax
    function pageLoad() {
        var isAsyncPostback = Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();
        if (isAsyncPostback) {           
            logica();
        }
    }
</script>  
<script type="text/javascript" language="javascript">
    function logica() {
        txtnombres = $("input[id$='txtnombres']");
        txtapellidos = $("input[id$='txtapellidos']");
        txttelefono = $("input[id$='txttelefono']");
        txtemail = $("input[id$='txtemail']");
        dwdistrito = $("select[id$='dwdistrito']");                 
        btenviar = $("input[id$='enviar']");
       

        //
       
        // Customer

        //        
        allFieldsBd = $([]).add(txtnombres).add(txtapellidos).add(txttelefono).add(txtemail).add(dwdistrito);
        
        //
        tips = $("#validateTips");
        //
        $("input:text").width('200px');
        //
        if (txtnombres.val() == "") {
            txtnombres.focus();
        }

        btenviar.click(function () {
            var bValid = true;

            bValid = bValid && validateBasicInfo();           

//            scrollTop();            
            return bValid;
        });
                    
    }

    function validateBasicInfo() {
        var bValid = true;
        tips.text("");

        allFieldsBd.removeClass("ui-state-error");

        bValid = bValid && checkLength(txtnombres, " Ingrese el nombre", 2, 50);
        bValid = bValid && checkLength(txtapellidos, " Ingrese el apellido", 2, 50);
        bValid = bValid && checkLength(txttelefono, " Ingrese el telefono", 7, 30);
        bValid = bValid && checkRegexp(txtemail, /^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i, " Dígite un correo válido. ej. user@aquarella.com.");

        $("[id$='hdestado']").val('0');
        document.getElementById('txtnombres').style.backgroundColor = '';
        document.getElementById('txtapellidos').style.backgroundColor = '';
        document.getElementById('txttelefono').style.backgroundColor = '';
        document.getElementById('txtemail').style.backgroundColor = '';
        document.getElementById('dwdistrito').style.backgroundColor = ''; 


        //
        if (txtnombres.val() == "") {
            document.getElementById('txtnombres').style.backgroundColor = '#FF9966'; 
            txtnombres.focus();
            return bValid;
           
            
        }
        if (txtapellidos.val() == "") {
            document.getElementById('txtapellidos').style.backgroundColor = '#FF9966'; 
            txtapellidos.focus();
            return bValid;

        }
        if (txttelefono.val() == "") {
            document.getElementById('txttelefono').style.backgroundColor = '#FF9966'; 
            txttelefono.focus();
            return bValid;

        }
        if (txtemail.val() == "") {
            document.getElementById('txtemail').style.backgroundColor = '#FF9966'; 
            txtemail.focus();
            return bValid;

        }
        if (dwdistrito.val() == "-1" || dwdistrito.val() == "") {
            document.getElementById('dwdistrito').style.backgroundColor = '#FF9966'; 
            $("[id$='hdestado']").val('1');
            dwdistrito.addClass("ui-state-error")
            updateTips(" Seleccione el Distrito.");
            dwdistrito.focus();
            return bValid;
        }
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
<head runat="server">     
    <title>[AQ] Contáctenos</title>
    <style type="text/css">
    
    
        
    #Background
    {
        position: fixed; 
        top: 0px; 
        bottom: 0px; 
        left: 0px; 
        right: 0px; 
        overflow: hidden; 
        padding: 0; 
        margin: 0; 
        background-color: #F0F0F0; 
        filter: alpha(opacity=80); 
        opacity: 0.8; 
        z-index: 100000;
    }
        
    #Progress
    {
        position: fixed;
        top: 40%; 
        left: 40%; 
        height:14%; 
        width:20%; 
        z-index: 100001;  
        background-color: #FFFFFF; 
        border:1px solid Gray; 
        background-image: url('../../Design/images/loading2.gif'); 
        background-repeat: no-repeat; 
        background-position:center;
    }
                 
   .BtnStyle
   {
    border: thin ridge #CCE8F6; 
    background-color: #CCE8F6; 
    font-family: verdana; 
    font-size: 8pt; 
    font-weight: bold; 
    color: #000080;
    cursor: pointer;
    border-top-left-radius: 10px 5px; 
    border-bottom-right-radius: 10% 5%; 
    border-top-right-radius: 10px; 
    -moz-border-radius: 15px; 
    -webkit-border-radius: 15px; 
    border-radius: 15px; 
    }
        
        .btGrisNegrita{
        background-color: #CCE8F6;
        color: #000080;
        font-size: 11px;
        border: 1px solid #333333;
        }
        #form1
        {
            height: 454px;    
            margin-top: 0px;            
        }
        .style4
        {
            width: 516px;
            height: 15px;
        }
        .style6
        {
            height: 15px;
            width: 1219px;
        }
        .style8
        {
            width: 243px;
        }
        .style9
        {
            width: 400px;
        }
        .style10
        {
            height: 23px;
            width: 174px;
        }
        .style12
        {
            height: 23px;
            width: 118px;
        }
        .style13
        {
            width: 118px;
        }
        .style14
        {
            width: 174px;
        }
        .style15
        {
            width: 62px;
        }
        .style16
        {
            height: 23px;
            width: 62px;
        }
        .style17
        {
            height: 44px;
        }
        .style18
        {
            width: 118px;
            height: 13px;
        }
        .style19
        {
            width: 62px;
            height: 13px;
        }
        .style20
        {
            width: 174px;
            height: 13px;
        }
    </style>  
</head>
<body bgcolor="fbf9ec" style="margin-top:0px;"  >    
    
              
    <form id="form1" runat="server" style="margin-top: 0px;"  >  
       
    <%
       
        string pageReportGnrls = "Mapa.htm"; 
        
    %>    
    <table style="width: 81%; height: 638px; margin-left: 123px; margin-top: 0px;">
        <tr>
            <td valign="top" style="margin-top: 0px;">
                <table style="width: 100%; height: 74px; margin-top: 0px;">
                    <tr>
                        <td valign="top" class="style4">
                            <a href="http://www.aquarellaperu.com.pe/" 
                                style="border-style: 0; border-color: 0; border-width: 0px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                            <asp:Image ID="Image1" runat="server" 
                                ImageUrl="~/Design/images/Logo_Contacto.png" style="margin-top: 0px" 
                                Height="66px" />
                                </a>
                        </td>
                        <td valign="top"  
                            style="background-position: top; margin-top: 0px; background-image: url('../../Design/images/back_contact.jpg');" 
                            class="style6">
                            <table style="border-style: 0; border-color: 0; border-width: 0px; width:100%; margin-right: 0px;">
                                <tr>
                                    <td style="margin-right: 0px" class="style8">                                        
                                    </td>
                                    <td style="margin-right: 0px">
                                        </td>
                                </tr>
                                <tr>
                                    <td style="margin-right: 0px" class="style8">
                                        &nbsp;</td>
                                    <td style="color: #0099CC; font-style: inherit; font-weight: bold;">
                                      </td>
                                </tr>
                                <tr>
                                    <td style="margin-right: 0px" class="style8">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
               
                <table style="width:100%;">
                    <tr>
                        <td bgcolor="#CCE8F6">
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
                <table style="width:100%;">
                    <tr>
                        <td class="style9">
                            <br />
                            <asp:GridView ID="gvcontactenos" runat="server" Width="479px" 
                                AutoGenerateColumns="False" CellPadding="4" Font-Names="Times New Roman" 
                                ForeColor="#333333" GridLines="None" ShowHeader="False" Font-Size="Small">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="Titulo">
                                    <ItemStyle Font-Bold="True" ForeColor="#003399" BorderWidth="0px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descripcion" >
                                    <ItemStyle BorderWidth="0px" />
                                    </asp:BoundField>
                                </Columns>
                                <EditRowStyle BackColor="#2461BF" BorderColor="#CCE8F6" />
                                <EmptyDataRowStyle BorderColor="#CCE8F6" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#CCE8F6" BorderColor="#CCE8F6" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            </asp:GridView>
                        </td>
                        <td rowspan="2" 
                            style="margin-top: 0px;" valign="top">
                            <br />
                            <table style="width:100%;">
                                <tr>
                                    <td style="color: #000080; font-weight: bold">
<p id="validateTips" align="center" 
                                            style="color: #FF3300; font-family: 'Times New Roman'; font-weight: normal; font-size: small;">
   
    </p>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="color: #000080; font-weight: bold; font-size: large" class="style17">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                        Contáctenos</td>
                                    <td class="style17">
                                        </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel1" runat="server">
                                        </asp:Panel>
                                        <table style="width:100%;">
                                            <tr>
                                                <td class="style13" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style15" 
                                                    style="color: #000080; font-family: 'Times New Roman'; font-size: small;">
                                                    *Nombres</td>
                                                <td class="style14">
                                                 <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtnombres" runat="server" Width="203px" AccessKey="p" 
                                                            style="margin-left: 0px"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="enviar" EventName="click" />
                                                    </Triggers>
                                                  </asp:UpdatePanel>
                                                </td>
                                                <td style="color: #000080" align="left">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style13" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style15" 
                                                    style="color: #000080; font-family: 'Times New Roman'; font-size: small;">
                                                    *Apellidos</td>
                                                <td class="style14">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtapellidos" runat="server" Width="203px"></asp:TextBox>
                                                    </ContentTemplate>
                                                     <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="enviar" EventName="click" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                </td>
                                                <td class="style14" style="color: #000080">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style13" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style15" 
                                                    style="color: #000080; font-family: 'Times New Roman'; font-size: small;">
                                                    *Teléfono</td>
                                                <td class="style14">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server"> 
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txttelefono" runat="server" Width="203px"></asp:TextBox>
                                                        </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="enviar" EventName="click" />
                                                    </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="style14" style="color: #000080">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style13" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style15" 
                                                    style="color: #000080; font-family: 'Times New Roman'; font-size: small;">
                                                    E-mail</td>
                                                <td class="style14">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server"> 
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtemail" runat="server" Width="203px"></asp:TextBox>
                                                        </ContentTemplate>
                                                         <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="enviar" EventName="click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="style14">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style13" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style15" 
                                                    style="color: #000080; font-family: 'Times New Roman'; font-size: small;">
                                                    Departamento</td>
                                                <td class="style14">
                                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                                    <ContentTemplate>
                                                    <asp:DropDownList AccessKey="o" ID="dwdepartamento" runat="server" DataValueField="Dep_Id"
                                                    DataTextField="Dep_Descripcion" AppendDataBoundItems="True" 
                                                    AutoPostBack="True" Width="204px" Font-Size="Small" 
                                                            onselectedindexchanged="dwdepartamento_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                                                    </asp:DropDownList>                
                                                    </ContentTemplate> 
                                                         <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="enviar" EventName="click" />
                                                         </Triggers>                      
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="style14" style="color: #000080">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style12" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style16" 
                                                    style="color: #000080; font-family: 'Times New Roman'; font-size: small;">
                                                    Provincia</td>
                                                <td class="style10">
                                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwprovincia" runat="server" DataValueField="Prv_Id"
                                DataTextField="Prv_Descripcion" AppendDataBoundItems="True" 
                                AutoPostBack="True" Height="16px" Width="204px" Font-Size="Small" onselectedindexchanged="dwprovincia_SelectedIndexChanged" 
                                >
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </ContentTemplate>
                         <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="enviar" EventName="click" />
                                                    </Triggers>
                    </asp:UpdatePanel>
                                                </td>
                                                <td class="style10" style="color: #000080">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style13" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style15" 
                                                    style="color: #000080; font-family: 'Times New Roman'; font-size: small;">
                                                    *Distrito</td>
                                                <td class="style14">
                                                   <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList AccessKey="o" ID="dwdistrito" runat="server" DataValueField="dis_id"
                                DataTextField="dis_descripcion" AppendDataBoundItems="True" Width="204px">
                                <asp:ListItem Value="-1">--Seleccionar de la lista--</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                        </ContentTemplate> 
                         <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="enviar" EventName="click" />
                                                    </Triggers>                      
                    </asp:UpdatePanel>
                                                </td>
                                                <td class="style14" style="color: #000080">
                                                    </td>
                                            </tr>
                                            <tr>
                                                <td class="style13" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style15" style="color: #000080; font-family: 'Times New Roman'; font-size: small;" 
                                                    valign="top">
                                                    Comentario</td>
                                                <td class="style14">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server"> 
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtcomentario" runat="server" Height="80px" TextMode="MultiLine" 
                                                                Width="198px"></asp:TextBox>
                                                        </ContentTemplate>
                                                         <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="enviar" EventName="click" />
                                                    </Triggers>
                                                    </asp:UpdatePanel>
                                                </td>
                                                <td class="style14">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="style13" style="color: #000080; font-family: 'Times New Roman';">
                                                    &nbsp;</td>
                                                <td class="style15" style="color: #000080; font-family: 'Times New Roman'; font-size: small;" 
                                                    valign="top">
                                                    &nbsp;</td>
                                                <td class="style14">
                                                     <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Button ID="enviar" runat="server" Text="Enviar" Width="105px" 
                                                                CssClass="BtnStyle" onclick="enviar_Click" />
                                                         </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                </td>
                                                <td class="style14">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                            </td>
                    </tr>
                    <tr>
                        <td class="style9">
                            <div style="margin: 10px auto 0 auto;" class="f13">
        <div>
            <div id="tabs">
                <ul>                                   
                </ul>
                <div id="fragment-1" style="min-height: 400px; height: 400px;">                    
                    <iframe src="<%=pageReportGnrls%>" id="frameLeft" frameborder="0" marginheight="1"
                        marginwidth="1" scrolling="auto" style="border: 1px solid silver; height: 77%;
                        width: 100%;"></iframe>
                </div>               
            </div>
        </div>        
    </div></td>
                    </tr>
                    </table>
               
                <br />
            <asp:HiddenField ID="hdestado" Value="0" runat="server" />
               
            </td>
        </tr>
    </table>   
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel20">
    <ProgressTemplate>
        <div id="Background"></div>
        <div id="Progress">
            <h6> <p style="text-align:center"> <b>Enviando Datos, Espere por favor... <br /></b> </p> </h6>
        </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
    </form>
</body>
</html>

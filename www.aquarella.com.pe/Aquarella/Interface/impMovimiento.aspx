<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="impMovimiento.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Interface.impMovimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <style type="text/css">
   table.estiloTabla {
	font-family: verdana,arial,sans-serif;
	font-size:11px;
	color:#333333;
	border-width: 1px;
	border-color: #999999;
	border-collapse: collapse;
}
table.estiloTabla th {
	background:#b5cfd2;
	border-width: 1px;
	padding: 8px;
	border-style: solid;
}
table.estiloTabla td {
	background:#dcddc0;
	border-width: 1px;
	padding: 8px;
	border-style: solid;
}

.FileUpload
{
	color: navy;
	border: 1px solid navy;
	font: Verdana 10px;
	padding: 1px 4px;
	font-family: verdana,arial,sans-serif;
}

.cssListBox
{
    height: 200px !important;
    width: 1050px !important;
} 

.button {
	background: #222;
	display: inline-block;
	padding: 5px 10px 6px;
	color: #FFF;
	position: relative;
	cursor:pointer;
	top: 0px;
	left: 4px;
	border-left-style: none;
	border-left-color: inherit;
	border-left-width: 0px;
	border-right-style: none;
	border-right-color: inherit;
	border-right-width: 0px;
	border-top-style: none;
	border-top-color: inherit;
	border-top-width: 0px;
}
     
        
        .style1
        {
            width: 277px;
        }
        .style2
        {
            width: 639px;
        }
        .style3
        {
            width: 165px;
        }
     
        
        .style4
        {
            width: 151px;
        }
     
        
        .style5
        {
            width: 134px;
        }
     
        
    </style>
    
    <script type ="text/javascript">
        //var validFilesTypes = ["bmp", "gif", "png", "jpg", "jpeg", "doc", "docx", "xls", "xlsx"];
        var validFilesTypes = ["TXT", "txt"];
        function ValidateFile() {
            var file = document.getElementById("<%=fileUpload.ClientID%>");
            var label = document.getElementById("<%=lblmensaje.ClientID%>");
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }
            if (!isValidFile) {
                label.style.color = "red";
                label.innerHTML = "Archivo Incorrecto. por favor cargar el archivo con" +
         " extension:\n\n" + validFilesTypes.join(", ");
            }
            return isValidFile;
        }
</script> 
              
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Importar Movimiento de Stock
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style2">
                <table border="0" cellpadding="0" cellspacing="0" class="estiloTabla">
                    <tr>
                        <td>
    <p id="validateTips">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
     <!-- Area de errores -->
    <asp:UpdatePanel ID="upPanelMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnenviar" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <center>
                <div 
                    style="position: absolute; left: 0; background: #CCCCFF; filter: alpha(opacity=85);
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 100%; font-size: medium;">
                    <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
                    Cargando información...
                </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <!-- SEL CUSTOMER -->
    </p>
                            Carga de Archivos</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td class="style4">
                                        <asp:RadioButton ID="optmov" runat="server" Checked="True" GroupName="GRUPO" 
                                            Text="Importar Movimiento" />
                                    </td>
                                    <td class="style5">
                                        <asp:RadioButton ID="optart" runat="server" GroupName="GRUPO" 
                                            Text="Importar Articulos" />
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="optstock" runat="server" GroupName="GRUPO" 
                                            Text="Importar Stock" />
                                    </td>
                                </tr>
                            </table>
&nbsp;<table style="width:100%;">
                                <tr>
                                    <td class="style1">
                            <asp:FileUpload ID="fileUpload" runat="server" CssClass="FileUpload" />
                                        <asp:Label ID="lblmensaje" runat="server" />
                                    </td>
                                    <td class="style3">
                            <asp:Button ID="btnUpload" runat="server" CssClass="button" 
                               Text="Subir Archivo" OnClientClick = "return ValidateFile" onclick="btnUpload_Click" />
                                    </td>
                                    <td>
                            <asp:Button ID="btnenviar" runat="server" CssClass="button" 
                               Text="Enviar Informacion" onclick="btnenviar_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:UpdatePanel ID="upGrid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gdarchivo" runat="server" 
    Width="1050px" AllowPaging="True" 
                                AllowSorting="True" onpageindexchanging="gdarchivo_PageIndexChanging">
                                        <EmptyDataTemplate>
                                            No existen registros para mostrar.
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="gdarchivo" EventName="PageIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
          <!-- ACTIVITY PANEL -->
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btnenviar" Enabled="false" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="btnenviar" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </table>
</asp:Content>

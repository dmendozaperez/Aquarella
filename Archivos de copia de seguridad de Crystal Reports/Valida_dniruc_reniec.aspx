<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Valida_dniruc_reniec.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Admonred.Valida_dniruc_reniec" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 220px;
        }
        .style2
        {
            width: 131px;
        }
        #form1
        {
            height: 312px;
            width: 564px;
        }
    </style>
    </head>
<body style="width: 532px">
    <form id="form1" runat="server">    
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
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 40%; font-size: medium;">
                    <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Cargando Información." />
                    Cargando información...
                </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:Panel ID="pnlValidateDoc" runat="server" Width="565px">
        <fieldset>
            <legend class="fsal f13">&nbsp;Consultar documento&nbsp;</legend>
            <p class="f12" style="padding: 5px;">
                Digite el número de documento (sin puntos, comas o espacios) y el codigo de 
                verificacion , para verificar si es un documento existe en Sunat o Reniec..</p>
            <table cellpadding="6" cellspacing="6" style="width: 93%">
                <tr>
                    <td class="style1">
                        <label for="txtDoc" class="f12">
                            Número de documento:</label>
                        </td>
                    <td class="style2">
                        <asp:TextBox ID="txtDoc" runat="server"></asp:TextBox>
                    </td>
                    <td class="style10">
                        <asp:Button ID="btValidateDoc" runat="server" Text="Consultar documento" 
                            TabIndex="2" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <label for="txtimagen" class="f12">
                        Ingrese los caracteres de la Imagen:</label>
                        </td>
                    <td class="style2">
                        <asp:TextBox ID="txtimagen" runat="server" TabIndex="1"></asp:TextBox>
                    </td>
                    <td class="style13">
                        </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Image ID="pictureCapcha" runat="server" Height="38px" Width="215px" />
                    </td>
                    <td class="style2">
                        <asp:ImageButton ID="ImageButton1" runat="server" Height="20px" 
                            ImageAlign="Baseline" ImageUrl="~/Design/images/bt_refresh.png" 
                             Width="25px" TabIndex="7" />
                    </td>
                    <td class="style13">
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </asp:Panel>
    </form>    
</body>
</html>

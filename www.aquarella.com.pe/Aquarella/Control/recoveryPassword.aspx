<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" 
    CodeBehind="recoveryPassword.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Control.recoveryPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <!-- Require EasyJQuery After JQuery -->
    <%--<script language="Javascript" type="text/javascript" src="http://api.easyjquery.com/easyjquery.js"></script>--%>
    <%--<script src="../../Scripts/Js/logInJs.js" type="text/javascript"></script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Recuperacion de Contraseña.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Ingrese el nombre de usuario.&nbsp;
    El Sistema le enviara un correo electronico para la generación de una nueva contraseña.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
     <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upPanelMsg" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <AQControl:Message runat="server" Visible="false" ID="msnMessage"></AQControl:Message>
         
    <table width="100%" cellpadding="4" cellspacing="4">
        <tr>
            <td align="center">
            
                    <LayoutTemplate>
                        <table cellpadding="4" cellspacing="4" style="border-collapse: collapse; border: 1px solid silver;">
                            <tr>
                                <td>
                                    <table cellpadding="4" cellspacing="4">
                                        <tr>
                                            <td colspan="3" align="left" style="color: White; background-color: #333; font-weight: bold;">
                                                <h2>
                                                    Recuperación de contraseña</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName"><h3>Nombre de usuario:</h3></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server" CssClass="campo1" Width="200px" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    ErrorMessage="El nombre de usuario es obligatorio." CssClass="error" ToolTip="El nombre de usuario es obligatorio."
                                                    ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                       <tr>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                         <tr>
                                            <td align="center" colspan="3" style="color: Red;">
                                                <asp:label ID="FailureText" runat="server" EnableViewState="False"></asp:label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="color: green;">
                                                <asp:label ID="sussesText" runat="server" EnableViewState="False"></asp:label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="LoginButton" runat="server"  OnClick="btEnviar_Click" Text="Enviar Correo"
                                                    ValidationGroup="Login1"/>
                                                 
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                    
            </td>
        </tr>
    </table>
            </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="LoginButton" EventName="click" />
           
        </Triggers>
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upPanelMsg">
        <ProgressTemplate>
            <center>
                <div style="position: absolute; left: 0; background: #f5f5f5; filter: alpha(opacity=85);
                    opacity: 0.85; font-family: Georgia; text-align: center; width: 100%; font-size: medium;">
                    <img src="../../Design/images/ajax-loader.gif" alt="Por Favor Espere; Enviando correo Electronico." />
                    Enviando correo Electronico...
                </div>
            </center>
        </ProgressTemplate>
    </asp:UpdateProgress>  
</asp:Content>

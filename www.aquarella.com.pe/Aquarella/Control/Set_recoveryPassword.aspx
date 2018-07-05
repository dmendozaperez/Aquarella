<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true"
    CodeBehind="Set_recoveryPassword.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Control.Set_recoveryPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
    <!-- Require EasyJQuery After JQuery -->
    <%--<script language="Javascript" type="text/javascript" src="http://api.easyjquery.com/easyjquery.js"></script>--%>
    <script src="../../Scripts/Js/logInJs.js" type="text/javascript"></script>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Recuperacion de Contraseña.
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
   Realice el cambio de la clave de acceso a su cuenta, recuerde que la clave debe
    de poseer mas de 8 caracteres alfanumericos.
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
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
                                                    Recuperación de contraseña:</h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Label ID="UserNameLabel" runat="server" ><h3>Código:</h3></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Codigo" runat="server" CssClass="campo1" Width="200px" MaxLength="200"></asp:TextBox>
                                            </td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Label ID="Label1" runat="server"><h3>Contraseña:</h3></asp:Label>
                                            </td>
                                            <td>
                                                 
                                                <asp:TextBox ID="Password" runat="server" CssClass="campo1" Width="200px" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td>
                                             <asp:RequiredFieldValidator ID="ValidatorPassNew" runat="server" ControlToValidate="Password"
                                                CssClass="error" Display="Dynamic" ErrorMessage="Digite la nueva contraseña."
                                                ForeColor="" SetFocusOnError="True" ValidationGroup="valerror">*</asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="regexTextBox1" Display="Dynamic" ControlToValidate="Password"
                                                runat="server" ValidationExpression="^[\s\S]{8,100}$" Text="La nueva clave debe de poseer minimo 8 caracteres"
                                                ErrorMessage="La nueva clave debe de poseer minimo 8 caracteres." ValidationGroup="valerror">*</asp:RegularExpressionValidator>
                                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                <asp:Label ID="Label2" runat="server" ><h3>Repetir Contraseña</h3></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Password2" runat="server" CssClass="campo1" Width="200px" TextMode="Password"></asp:TextBox>
                                            </td>
                                            <td>
                                                 <asp:CompareValidator ID="ValidatorNews" runat="server" ControlToValidate="Password2"
                                            CssClass="error" Display="Dynamic" ErrorMessage="La contraseña nueva no es igual. Por favor vuelva y digitela."
                                            ForeColor="" ValidationGroup="valerror" SetFocusOnError="True" ControlToCompare="Password">*</asp:CompareValidator><asp:RequiredFieldValidator
                                                ID="ValidatorPassRenew" runat="server" ControlToValidate="Password2" CssClass="error"
                                                Display="Dynamic" ErrorMessage="Vuelva y digita la nueva contraseña." ForeColor=""
                                                SetFocusOnError="True" ValidationGroup="valerror">*</asp:RequiredFieldValidator>
                                        
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
                                                <asp:Button ID="LoginButton" runat="server"  OnClick="btEnviarPass_Click" Text="Confirmar contraseña"
                                                    ValidationGroup="Enviar"/>
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
</asp:Content>

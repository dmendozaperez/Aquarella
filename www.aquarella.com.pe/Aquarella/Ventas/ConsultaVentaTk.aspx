<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaVentaTk.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Ventas.ConsultaVentaTk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">

    <style type="text/css">
        .style1
        {
            width: 549px;
        }
        .style2
        {
            width: 24px;
        }
        .style5
        {
            width: 201px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Consulta de venta por numero de tickets
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    Esta Consulta muestra la venta en formato de tickets 
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" 
        EnableScriptGlobalization="True">
    </asp:ScriptManager>
    <!-- Area de errores -->
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablagris" cellpadding="4">
            <tr>
                <td class="style1">
                    <table>
                        <tr>
                            <td class="style5">
                               <b>Ingrese el numero de tickets</b></td>
                            <td class="f12">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style5">
                                        &nbsp;
                                        <asp:TextBox ID="txtbuscar" runat="server"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                    <asp:Button ID="btConsult" runat="server" Text="Consultar" 
                         OnClick="btConsult_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="middle" class="style2">
                    &nbsp;</td>
                <td align="left" style="border-left: solid 1px silver;">
                    &nbsp;</td>
            </tr>
            </table>
    </div>
</asp:Content>

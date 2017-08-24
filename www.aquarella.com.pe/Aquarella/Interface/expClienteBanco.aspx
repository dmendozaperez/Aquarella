<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Design/Site.Master" CodeBehind="expClienteBanco.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Interface.expClienteBanco" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
    Exportar Clientes 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- -->
    <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablaGrisLetra" cellpadding="4">
            <tr>
                <td class="fsal f13">
                    Formulario de exportacion
                </td>
            </tr>
            <tr>
                <td>
                <div style="width:100%">
                    <!-- WARE AND AREA FORM -->
                       
                        <div style="width:50%; padding-left:280px; position:absolute; text-align:right" align="right">
                            <div style="border-left: solid 1px silver;">
                                <asp:ImageButton ID="ibExportDoc" ImageUrl="~/Design/images/Botones/b_doc.png" onmouseover="this.style.background='navy';"
                                    onmouseout="this.style.background=''" runat="server" Height="25px" Width="24px"
                                    ToolTip="Exportar archivo de Clientes" OnClick="ibExportDoc_Click" />
                            </div>
                        </div>
                         <div style="width:100%; text-align:left">Pulse en el boton para exportar los datos<br /> de los Clientes: 
                        </div>
                        <br /><br />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>
            </tr>
        </table>
    </div>
    <!-- PANEL DE RESULTADOS -->
</asp:Content>

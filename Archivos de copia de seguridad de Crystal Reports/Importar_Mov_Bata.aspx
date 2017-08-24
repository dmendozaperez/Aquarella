<%@ Page Title="" Language="C#" MasterPageFile="~/Design/Site.Master" AutoEventWireup="true" CodeBehind="Importar_Mov_Bata.aspx.cs" Inherits="www.aquarella.com.pe.Aquarella.Logistica.Importar_Mov_Bata" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headCPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderTitle" runat="server">
      Importar Movimientos (Almacen - Bata)
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderPageDesc" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
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
       <div style="margin: 10px auto 0 auto;">
        <table width="100%" class="tablaGrisLetra" cellpadding="4">
            <tr>
                <td class="fsal f13">
                    Formulario Importar Data
                </td>
            </tr>
            <tr>
                <td>
                <div style="width:100%">
                    <!-- WARE AND AREA FORM -->
                       
                        <div style="width:50%; padding-left:280px; position:absolute; text-align:right" align="right">
                            <div style="border-left: solid 1px silver;">
                                <asp:ImageButton ID="ibrecalcular" 
                                    ImageUrl="~/Design/images/Botones/b_refresh.png" onmouseover="this.style.background='navy';"
                                    onmouseout="this.style.background=''" runat="server" Height="25px" Width="24px"
                                    ToolTip="Importar Stock" onclick="ibrecalcular_Click" />
                            </div>
                        </div>
                         <div style="width:100%; text-align:left">Pulse en el boton para importar el stock  <br /> del almacen:
                        </div>
                        <br /><br />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="upGrid" runat="server">
                     <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="ibrecalcular" EventName="click" />
                    </Triggers>
                    </asp:UpdatePanel>
                    <asp:ValidationSummary ID="vsConsultForm" ValidationGroup="vsConsultForm" runat="server"
                        EnableClientScript="true" HeaderText="Realice las siguientes correcciones" ShowMessageBox="true" />
                </td>                 
            </tr>
        </table>
        <ajaxToolkit:UpdatePanelAnimationExtender ID="upae" runat="server" TargetControlID="upGrid">
            <Animations>
                                        <OnUpdating>
                                            <Sequence>
                                                <%-- Disable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="ibrecalcular" Enabled="false" />                                                   
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdating>
                                        <OnUpdated>
                                            <Sequence>
                                                <%-- Enable all the controls --%>
                                                <Parallel duration="0">
                                                    <EnableAction AnimationTarget="ibrecalcular" Enabled="true" />
                                                </Parallel>
                                            </Sequence>
                                        </OnUpdated>
            </Animations>
        </ajaxToolkit:UpdatePanelAnimationExtender>
    </div>
</asp:Content>
